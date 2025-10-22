/*
ObservingCommand.cs

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
using TheXDS.MCART.Helpers;
using TheXDS.MCART.Types.Base;
using static TheXDS.MCART.Misc.Internals;

namespace TheXDS.MCART.Component;

/// <summary>
/// Describes a command that observes an object implementing
/// <see cref="INotifyPropertyChanged"/> and listens for any changes
/// to its property values to automatically enable or disable
/// command execution.
/// </summary>
/// <param name="observedSource">The data source to observe.</param>
/// <param name="action">The action to execute.</param>
public partial class ObservingCommand(INotifyPropertyChanged observedSource, Action<object?> action) : CommandBase(action)
{
    private Func<INotifyPropertyChanged, object?, bool>? _canExecute;
    private readonly HashSet<string> _properties = [];

    /// <summary>
    /// Initializes a new instance of <see cref="ObservingCommand"/>.
    /// </summary>
    /// <param name="observedSource">The data source to observe.</param>
    /// <param name="action">The action to execute.</param>
    public ObservingCommand(INotifyPropertyChanged observedSource, Action action) : this(observedSource, _ => action()) { }

    /// <summary>
    /// Initializes a new instance of <see cref="ObservingCommand"/>.
    /// </summary>
    /// <param name="observedSource">The data source to observe.</param>
    /// <param name="task">The asynchronous task to execute.</param>
    public ObservingCommand(INotifyPropertyChanged observedSource, Func<Task> task) : this(observedSource, (Action<object?>)(async _ => await task())) { }

    /// <summary>
    /// Initializes a new instance of <see cref="ObservingCommand"/>.
    /// </summary>
    /// <param name="observedSource">The data source to observe.</param>
    /// <param name="task">The asynchronous task to execute.</param>
    public ObservingCommand(INotifyPropertyChanged observedSource, Func<object?, Task> task) : this(observedSource, (Action<object?>)(async o => await task(o))) { }

    /// <summary>
    /// Reference to the data source observed by this
    /// <see cref="ObservingCommand"/>.
    /// </summary>
    public INotifyPropertyChanged ObservedSource { get; } = NullChecked(observedSource);

    /// <summary>
    /// Enumerates the property names being observed by this
    /// <see cref="ObservingCommand"/>.
    /// </summary>
    public IEnumerable<string> ObservedProperties => [.. _properties];

    /// <summary>
    /// Sets the method that determines whether the command can execute
    /// in its current state.
    /// </summary>
    /// <param name="parameter">
    /// Data used by the command. If the command does not require data,
    /// this object can be set to <see langword="null"/>.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if this command can execute; otherwise,
    /// <see langword="false"/>.
    /// </returns>
    public override bool CanExecute(object? parameter)
    {
        return _canExecute?.Invoke(ObservedSource, parameter) ?? true;
    }

    /// <summary>
    /// Registers one or more property names to observe for this command.
    /// </summary>
    /// <param name="properties">The name(s) of the property(ies) to
    /// observe.</param>
    /// <returns>This same instance to allow fluent usage.</returns>
    public ObservingCommand RegisterObservedProperty(params string[] properties)
    {
        foreach (string? j in properties.NotEmpty())
        {
            _properties.Add(j);
        }
        return this;
    }

    /// <summary>
    /// Registers a property to observe for this command using a lambda
    /// expression that selects the property.
    /// </summary>
    /// <param name="property">Expression selecting the property to observe.</param>
    /// <returns>
    /// This same instance, allowing fluent call chaining.
    /// </returns>
    public ObservingCommand RegisterObservedProperty(Expression<Func<object?>> property)
    {
        PropertyInfo? prop = (ReflectionHelpers.GetMember(property) as PropertyInfo) ?? throw new ArgumentException(null, nameof(property));
        RegisterObservedProperty(prop.Name);
        return this;
    }

    /// <summary>
    /// Sets the check function used to determine whether the command can
    /// execute.
    /// </summary>
    /// <param name="canExecute">
    /// Function used to determine whether the command can execute.
    /// </param>
    /// <returns>
    /// This same instance, allowing fluent call chaining.
    /// </returns>
    public ObservingCommand SetCanExecute(Func<bool> canExecute)
    {
        ArgumentNullException.ThrowIfNull(canExecute, nameof(canExecute));
        return SetCanExecute((_, _) => canExecute());
    }

    /// <summary>
    /// Sets the check function used to determine whether the command can
    /// execute.
    /// </summary>
    /// <param name="canExecute">
    /// Function that receives the command parameter and returns whether
    /// the command can execute.
    /// </param>
    /// <returns>
    /// This same instance, allowing fluent call chaining.
    /// </returns>
    public ObservingCommand SetCanExecute(Func<object?, bool> canExecute)
    {
        ArgumentNullException.ThrowIfNull(canExecute, nameof(canExecute));
        return SetCanExecute((_, o) => canExecute(o));
    }

    /// <summary>
    /// Sets the check function used to determine whether the command can
    /// execute.
    /// </summary>
    /// <param name="canExecute">
    /// Function that receives the observed source and the command
    /// parameter and returns whether the command can execute. Pass
    /// <see langword="null"/> to remove the check.
    /// </param>
    /// <returns>
    /// This same instance, allowing fluent call chaining.
    /// </returns>
    public ObservingCommand SetCanExecute(Func<INotifyPropertyChanged, object?, bool>? canExecute)
    {
        if (canExecute is null)
        {
            ObservedSource.PropertyChanged -= RaiseCanExecuteChanged;
        }
        else if (_canExecute is null)
        {
            ObservedSource.PropertyChanged += RaiseCanExecuteChanged;
        }
        _canExecute = canExecute;
        return this;
    }

    /// <summary>
    /// Disconnects the check function used to determine whether the
    /// command can execute.
    /// </summary>
    public void UnsetCanExecute() => SetCanExecute((Func<INotifyPropertyChanged, object?, bool>?)null);

    private void RaiseCanExecuteChanged(object? sender, PropertyChangedEventArgs e)
    {
        RaiseCanExecuteChanged_Contract(sender, e);
        if (_properties.Contains(e.PropertyName!)) RaiseCanExecuteChanged();
    }
}
