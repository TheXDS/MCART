/*
ViewModelBase.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2025 César Andrés Morgan

Permission is hereby granted, free of charge, to any person obtaining a copy of
this software and associated documentation files (the "Software"), to deal in
the Software without restriction, including without limitation the rights to
use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
of the Software, and to permit persons to whom the Software is furnished to do
so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using TheXDS.MCART.Exceptions;
using TheXDS.MCART.Helpers;

namespace TheXDS.MCART.Types.Base;

/// <summary>
/// Base class for creating ViewModels.
/// </summary>
public abstract partial class ViewModelBase : NotifyPropertyChanged
{
    private bool _isBusy;
    private readonly Dictionary<string, HashSet<Action>> _observeRegistry = [];

    /// <summary>
    /// Initializes a new instance of the <see cref="ViewModelBase"/> class.
    /// </summary>
    protected ViewModelBase()
    {
        PropertyChanged += OnInvokeObservedProps;
    }

    /// <summary>
    /// Gets a value indicating whether this <see cref="ViewModelBase"/> is
    /// busy.
    /// </summary>
    public bool IsBusy
    {
        get => _isBusy;
        protected set => Change(ref _isBusy, value);
    }

    /// <summary>
    /// Registers a property with change notification to be observed and
    /// handled by the specified delegate.
    /// </summary>
    /// <typeparam name="T">Type of the property.</typeparam>
    /// <param name="propertySelector">
    /// Selector function for the property to observe.
    /// </param>
    /// <param name="handler">
    /// Delegate to invoke when the property has changed.
    /// </param>
    /// <exception cref="InvalidArgumentException">
    /// Thrown if the property selector does not select a valid member on
    /// the instance to configure.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="propertySelector"/> or
    /// <paramref name="handler"/> is <see langword="null"/>.
    /// </exception>
    protected void Observe<T>(Expression<Func<T>> propertySelector, Action handler)
    {
        Observe_Contract(propertySelector);
        ObserveFrom(this, ReflectionHelpers.GetProperty(propertySelector), handler);
    }

    /// <summary>
    /// Registers properties with change notification to be observed and
    /// handled by the specified delegate.
    /// </summary>
    /// <param name="propertySelectors">
    /// Selector functions for the properties to observe.
    /// </param>
    /// <param name="handler">
    /// Delegate to invoke when any of the properties has changed.
    /// </param>
    /// <exception cref="InvalidArgumentException">
    /// Thrown if a property selector did not select a valid member on the
    /// instance to configure.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="propertySelectors"/> or
    /// <paramref name="handler"/> is <see langword="null"/>.
    /// </exception>
    /// <exception cref="EmptyCollectionException">
    /// Thrown if <paramref name="propertySelectors"/> contains no elements.
    /// </exception>
    protected void Observe(Expression<Func<object?>>[] propertySelectors, Action handler)
    {
        Observe_Contract(propertySelectors);
        foreach (var prop in propertySelectors)
        {
            ObserveFrom(this, ReflectionHelpers.GetProperty(prop), handler);
        }
    }

    /// <summary>
    /// Registers a property with change notification to be observed and
    /// handled by the specified delegate.
    /// </summary>
    /// <param name="propertyName">Name of the property to observe.</param>
    /// <param name="handler">
    /// Delegate to invoke when the property has changed.
    /// </param>
    /// <exception cref="InvalidArgumentException">
    /// Thrown if <paramref name="propertyName"/> is an empty or whitespace
    /// string.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="propertyName"/> or 
    /// <paramref name="handler"/> is <see langword="null"/>.
    /// </exception>
    protected void Observe(string propertyName, Action handler)
    {
        Observe_Contract(propertyName, handler);
        if (!_observeRegistry.TryGetValue(propertyName, out HashSet<Action>? value))
        {
            value = [];
            _observeRegistry.Add(propertyName, value);
        }
        value.Add(handler);
    }

    /// <summary>
    /// Registers properties with change notification to be observed and
    /// handled by the specified delegate.
    /// </summary>
    /// <param name="propertyNames">Names of the properties to observe.</param>
    /// <param name="handler">
    /// Delegate to invoke when any of the properties has changed.
    /// </param>
    /// <exception cref="InvalidArgumentException">
    /// Thrown if any element of <paramref name="propertyNames"/> is
    /// <see langword="null"/>, an empty string, or whitespace.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="propertyNames"/> or 
    /// <paramref name="handler"/> is <see langword="null"/>.
    /// </exception>
    protected void Observe(string[] propertyNames, Action handler)
    {
        Observe_Contract(propertyNames, handler);
        foreach (var prop in propertyNames)
        {
            if (!_observeRegistry.TryGetValue(prop, out HashSet<Action>? value))
            {
                value = [];
                _observeRegistry.Add(prop, value);
            }
            value.Add(handler);
        }
    }

    /// <summary>
    /// Registers a property with change notification to be observed and
    /// handled by the specified delegate.
    /// </summary>
    /// <typeparam name="T">Type of the property.</typeparam>
    /// <param name="source">Observed source.</param>
    /// <param name="propertySelector">
    /// Selector function for the property to observe.
    /// </param>
    /// <param name="handler">
    /// Delegate to invoke when the property has changed.
    /// </param>
    /// <exception cref="InvalidArgumentException">
    /// Thrown if the property selector does not select a valid member on
    /// the instance to configure.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="source"/>, <paramref name="propertySelector"/>,
    /// or <paramref name="handler"/> is <see langword="null"/>.
    /// </exception>
    protected void ObserveFrom<T>(T source, Expression<Func<T, object?>> propertySelector, Action handler) where T : notnull, INotifyPropertyChanged
    {
        Observe_Contract(propertySelector);
        ObserveFrom(source, ReflectionHelpers.GetProperty(propertySelector), handler);
    }

    /// <summary>
    /// Registers a property with change notification to be observed and
    /// handled by the specified delegate.
    /// </summary>
    /// <param name="source">Observed source.</param>
    /// <param name="property">Property to observe.</param>
    /// <param name="handler">
    /// Delegate to invoke when the property has changed.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="source"/>, <paramref name="property"/>,
    /// or <paramref name="handler"/> is <see langword="null"/>.
    /// </exception>
    protected void ObserveFrom(INotifyPropertyChanged source, PropertyInfo property, Action handler)
    {
        Observe_Contract(source, property);
        Observe(property.Name, handler);
    }

    /// <summary>
    /// Executes an action while automatically managing the 'busy' state
    /// of this ViewModel.
    /// </summary>
    /// <param name="action">Action to execute.</param>
    protected void BusyOp(Action action)
    {
        BusyOp_Contract(action);
        IsBusy = true;
        action.Invoke();
        IsBusy = false;
    }

    /// <summary>
    /// Executes a task while automatically managing the 'busy' state of
    /// this ViewModel.
    /// </summary>
    /// <param name="task">Task to execute.</param>
    /// <returns>
    /// A <see cref="Task"/> that can be awaited to monitor the async
    /// operation.
    /// </returns>
    protected async Task BusyOp(Task task)
    {
        BusyOp_Contract(task);
        IsBusy = true;
        await task;
        IsBusy = false;
    }

    /// <summary>
    /// Executes a function while automatically managing the 'busy' state
    /// of this ViewModel.
    /// </summary>
    /// <typeparam name="T">Type of the function result.</typeparam>
    /// <param name="function">Function to execute.</param>
    /// <returns>The result returned by the specified function.</returns>
    protected T BusyOp<T>(Func<T> function)
    {
        BusyOp_Contract(function);
        IsBusy = true;
        T? result = function.Invoke();
        IsBusy = false;
        return result;
    }

    /// <summary>
    /// Executes a task that returns a result while automatically managing
    /// the 'busy' state of this ViewModel.
    /// </summary>
    /// <typeparam name="T">Type of the task result.</typeparam>
    /// <param name="task">Task to execute.</param>
    /// <returns>
    /// A <see cref="Task{T}"/> that can be awaited to monitor the async
    /// operation.
    /// </returns>
    protected async Task<T> BusyOp<T>(Task<T> task)
    {
        BusyOp_Contract(task);
        IsBusy = true;
        T? result = await task;
        IsBusy = false;
        return result;
    }

    private void OnInvokeObservedProps(object? sender, PropertyChangedEventArgs e)
    {
        OnInvokeObservedProps_Contract(sender, e);
        if (_observeRegistry.TryGetValue(e.PropertyName!, out HashSet<Action>? c))
        {
            foreach (Action? j in c!)
            {
                j.Invoke();
            }
        }
    }

    /// <summary>
    /// Finalizes this instance of the <see cref="ViewModelBase"/> class.
    /// </summary>
    ~ViewModelBase()
    {
        PropertyChanged -= OnInvokeObservedProps;
    }
}
