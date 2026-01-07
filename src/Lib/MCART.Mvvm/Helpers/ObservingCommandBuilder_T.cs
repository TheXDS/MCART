/*
ObservingCommandBuilder_T.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2026 César Andrés Morgan

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
using TheXDS.MCART.Component;
using TheXDS.MCART.Math;
using TheXDS.MCART.Types.Extensions;
using static TheXDS.MCART.Helpers.ReflectionHelpers;

namespace TheXDS.MCART.Helpers;

/// <summary>
/// Wrapper class that allows for the configuration and generation of an 
/// <see cref="ObservingCommand"/>.
/// </summary>
/// <typeparam name="T">Type of the observed object.</typeparam>
public partial class ObservingCommandBuilder<T> where T : INotifyPropertyChanged
{
    private readonly ObservingCommand command;
    private readonly T observedObject;
    private readonly List<Func<object?, bool>> canExecuteTree = [];

    /// <summary>
    /// Gets a value indicating whether the 
    /// <see cref="ObservingCommand"/> has been fully defined 
    /// through this instance.
    /// </summary>
    public bool IsBuilt { get; private set; }

    internal ObservingCommandBuilder(T observedObject, Action action)
    {
        command = new(this.observedObject = observedObject, action);
    }

    internal ObservingCommandBuilder(T observedObject, Action<object?> action)
    {
        command = new(this.observedObject = observedObject, action);
    }

    internal ObservingCommandBuilder(T observedObject, Func<Task> action)
    {
        command = new(this.observedObject = observedObject, action);
    }

    internal ObservingCommandBuilder(T observedObject, Func<object?, Task> action)
    {
        command = new(this.observedObject = observedObject, action);
    }

    /// <summary>
    /// Indicates that the <see cref="ObservingCommand"/> being configured will listen 
    /// for changes to the specified properties.
    /// </summary>
    /// <param name="properties">
    /// A collection of selector expressions that specify the properties to listen to.
    /// </param>
    /// <returns>
    /// This instance of the 
    /// <see cref="ObservingCommandBuilder{T}"/>, allowing for Fluent syntax.
    /// </returns>
    public ObservingCommandBuilder<T> ListensTo(params Expression<Func<T, object?>>[] properties)
    {
        ListensTo<object?>(properties);
        return this;
    }

    /// <summary>
    /// Indicates that the <see cref="ObservingCommand"/> being configured will listen 
    /// for changes to the specified properties.
    /// </summary>
    /// <param name="properties">
    /// A collection of selector expressions that specify the properties to listen to.
    /// </param>
    /// <returns>
    /// This instance of the 
    /// <see cref="ObservingCommandBuilder{T}"/>, allowing for Fluent syntax.
    /// </returns>
    public ObservingCommandBuilder<T> ListensTo<TValue>(params Expression<Func<T, TValue>>[] properties)
    {
        ListensToProperty_Contract(properties);
        command.RegisterObservedProperty(properties.Select(GetProperty).Select(p => p.Name).ToArray());
        return this;
    }

    /// <summary>
    /// Indicates that the <see cref="ObservingCommand"/> being configured will 
    /// listen to a property of type <see cref="bool"/> and use it as the 
    /// determinant of whether the command can be executed.
    /// </summary>
    /// <param name="selector">
    /// Selector function for the property to be used.
    /// </param>
    /// <returns>
    /// This instance of the 
    /// <see cref="ObservingCommandBuilder{T}"/>, allowing for Fluent syntax.
    /// </returns>
    public ObservingCommandBuilder<T> ListensToCanExecute(Expression<Func<T, bool>> selector)
    {
        var property = GetProperty(selector);
        ListensToCanExecute_Contract(property, typeof(T));
        command.RegisterObservedProperty(property.Name);
        var callback = selector.Compile();
        canExecuteTree.Add(_ => callback.Invoke(observedObject));
        return this;
    }

    /// <summary>
    /// Explicitly sets the check function that determines whether the 
    /// <see cref="ObservingCommand"/> being configured can be executed.
    /// </summary>
    /// <param name="canExecute">
    /// A function that determines if the command can be executed. The function 
    /// will accept a parameter provided by the data binding.
    /// </param>
    /// <returns>
    /// This instance of the 
    /// <see cref="ObservingCommandBuilder{T}"/>, allowing for Fluent syntax.
    /// </returns>
    public ObservingCommandBuilder<T> CanExecute(Func<object?, bool> canExecute)
    {
        IsBuilt_Contract();
        canExecuteTree.Add(canExecute);
        return this;
    }

    /// <summary>
    /// Explicitly sets the check function that determines whether the 
    /// <see cref="ObservingCommand"/> being configured can be executed.
    /// </summary>
    /// <param name="canExecute">
    /// A function that determines if the command can be executed.
    /// </param>
    /// <returns>
    /// This instance of the 
    /// <see cref="ObservingCommandBuilder{T}"/>, allowing for Fluent syntax.
    /// </returns>
    public ObservingCommandBuilder<T> CanExecute(Func<bool> canExecute)
    {
        IsBuilt_Contract();
        canExecuteTree.Add(_ => canExecute());
        return this;
    }

    /// <summary>
    /// Configures the <see cref="ObservingCommand"/> to be executable 
    /// when the specified properties are not equal to 
    /// <see langword="null"/>.
    /// </summary>
    /// <param name="properties">
    /// A collection of selector expressions that specify the properties to listen to.
    /// </param>
    /// <returns>
    /// This instance of the 
    /// <see cref="ObservingCommandBuilder{T}"/>, allowing for Fluent syntax.
    /// </returns>
    public ObservingCommandBuilder<T> CanExecuteIfNotNull(params Expression<Func<T, object?>>[] properties)
    {
        return CanExecuteIf(p => p is not null, properties);
    }

    /// <summary>
    /// Configures the <see cref="ObservingCommand"/> to be executable 
    /// when the specified properties are not equal to 
    /// <see langword="null"/> or their default value if they are 
    /// value types.
    /// </summary>
    /// <param name="properties">
    /// A collection of selector expressions that specify the properties to listen to.
    /// </param>
    /// <returns>
    /// This instance of the 
    /// <see cref="ObservingCommandBuilder{T}"/>, allowing for Fluent syntax.
    /// </returns>
    public ObservingCommandBuilder<T> CanExecuteIfNotDefault(params Expression<Func<T, object?>>[] properties)
    {
        return CanExecuteIf(p => p is not null && !p.Equals(p.GetType().Default()), properties);
    }

    /// <summary>
    /// Configures the <see cref="ObservingCommand"/> to be executable 
    /// when the specified properties contain values different from 
    /// the default value for their respective types.
    /// </summary>
    /// <param name="properties">
    /// A collection of selector expressions that specify the properties to listen to.
    /// </param>
    /// <returns>
    /// This instance of the 
    /// <see cref="ObservingCommandBuilder{T}"/>, allowing for Fluent syntax.
    /// </returns>
    public ObservingCommandBuilder<T> CanExecuteIfFilled(params Expression<Func<T, string?>>[] properties)
    {
        return CanExecuteIf(p => !p.IsEmpty(), properties);
    }

    /// <summary>
    /// Configures the <see cref="ObservingCommand"/> to be executable 
    /// when the specified properties have valid floating-point values, 
    /// meaning they are not 
    /// <see cref="float.NegativeInfinity"/>, 
    /// <see cref="float.PositiveInfinity"/> or <see cref="float.NaN"/>.
    /// </summary>
    /// <param name="properties">
    /// A collection of selector expressions that specify the properties to listen to.
    /// </param>
    /// <returns>
    /// This instance of the 
    /// <see cref="ObservingCommandBuilder{T}"/>, allowing for Fluent syntax.
    /// </returns>
    public ObservingCommandBuilder<T> CanExecuteIfValid(params Expression<Func<T, float>>[] properties)
    {
        return CanExecuteIf(p => p.IsValid(), properties);
    }

    /// <summary>
    /// Configures the <see cref="ObservingCommand"/> to be executable 
    /// when the specified properties have valid floating-point values, 
    /// meaning they are not 
    /// <see cref="float.NegativeInfinity"/>, 
    /// <see cref="float.PositiveInfinity"/>, <see cref="float.NaN"/> or 
    /// <see langword="null"/>.
    /// </summary>
    /// <param name="properties">
    /// A collection of selector expressions that specify the properties to listen to.
    /// </param>
    /// <returns>
    /// This instance of the 
    /// <see cref="ObservingCommandBuilder{T}"/>, allowing for Fluent syntax.
    /// </returns>
    public ObservingCommandBuilder<T> CanExecuteIfValid(params Expression<Func<T, float?>>[] properties)
    {
        return CanExecuteIf(p => p.HasValue && p.Value.IsValid(), properties);
    }

    /// <summary>
    /// Configures the <see cref="ObservingCommand"/> to be executable 
    /// when the specified properties have valid floating-point values, 
    /// meaning they are not 
    /// <see cref="double.NegativeInfinity"/>, 
    /// <see cref="double.PositiveInfinity"/> or <see cref="double.NaN"/>.
    /// </summary>
    /// <param name="properties">
    /// A collection of selector expressions that specify the properties to listen to.
    /// </param>
    /// <returns>
    /// This instance of the 
    /// <see cref="ObservingCommandBuilder{T}"/>, allowing for Fluent syntax.
    /// </returns>
    public ObservingCommandBuilder<T> CanExecuteIfValid(params Expression<Func<T, double>>[] properties)
    {
        return CanExecuteIf(p => p.IsValid(), properties);
    }

    /// <summary>
    /// Configures the <see cref="ObservingCommand"/> to be executable 
    /// when the specified properties have valid floating-point values, 
    /// meaning they are not 
    /// <see cref="double.NegativeInfinity"/>, 
    /// <see cref="double.PositiveInfinity"/>, <see cref="double.NaN"/> or 
    /// <see langword="null"/>.
    /// </summary>
    /// <param name="properties">
    /// A collection of selector expressions that specify the properties to listen to.
    /// </param>
    /// <returns>
    /// This instance of the 
    /// <see cref="ObservingCommandBuilder{T}"/>, allowing for Fluent syntax.
    /// </returns>
    public ObservingCommandBuilder<T> CanExecuteIfValid(params Expression<Func<T, double?>>[] properties)
    {
        return CanExecuteIf(p => p.HasValue && p.Value.IsValid(), properties);
    }

    /// <summary>
    /// Configures the <see cref="ObservingCommand"/> to be executable 
    /// when the specified properties have values different from their 
    /// default ordinal value.
    /// </summary>
    /// <typeparam name="TValue">Type of the values.</typeparam>
    /// <param name="properties">
    /// A collection of selector expressions that specify the properties to listen to.
    /// </param>
    /// <returns>
    /// This instance of the 
    /// <see cref="ObservingCommandBuilder{T}"/>, allowing for Fluent syntax.
    /// </returns>
    public ObservingCommandBuilder<T> CanExecuteIfNotZero<TValue>(params Expression<Func<T, TValue>>[] properties) where TValue : notnull, IComparable<TValue>
    {
        return CanExecuteIf(p => p.CompareTo((TValue)p.GetType().Default()!) != 0, properties);
    }

    /// <summary>
    /// Configures the <see cref="ObservingCommand"/> to be executable 
    /// when all readable and writable properties of the observed object 
    /// have values different from their respective default values.
    /// </summary>
    /// <returns>
    /// This instance of the 
    /// <see cref="ObservingCommandBuilder{T}"/>, allowing for Fluent syntax.
    /// </returns>
    public ObservingCommandBuilder<T> CanExecuteIfObservedIsFilled()
    {
        static bool IsFilled(object? p)
        {
            return p switch
            {
                null => false,
                string s => !s.IsEmpty(),
                ValueType v => v != p.GetType().Default(),
                _ => true
            };
        }
        IsBuilt_Contract();
        var props = observedObject.GetType().GetProperties().Where(p => p.CanRead && p.CanWrite);
        command.RegisterObservedProperty(props.Select(p => p.Name).ToArray());
        canExecuteTree.Add(_ => props.Select(p => p.GetValue(observedObject)).All(IsFilled));
        return this;
    }

    /// <summary>
    /// Configures the <see cref="ObservingCommand"/> to be executable 
    /// when the specified collections contain at least one element.
    /// </summary>
    /// <param name="properties">
    /// A collection of selector expressions that specify the properties to listen to.
    /// </param>
    /// <returns>
    /// This instance of the 
    /// <see cref="ObservingCommandBuilder{T}"/>, allowing for Fluent syntax.
    /// </returns>
    public ObservingCommandBuilder<T> CanExecuteIfNotEmpty(params Expression<Func<T, IEnumerable<object?>?>>[] properties)
    {
        return CanExecuteIf(p => p?.Any() ?? false, properties);
    }

    /// <summary>
    /// Finalizes the configuration of the underlying <see cref="ObservingCommand"/> 
    /// and returns it.
    /// </summary>
    /// <returns>
    /// The <see cref="ObservingCommand"/> that has been configured through 
    /// this instance.
    /// </returns>
    public ObservingCommand Build()
    {
        if (IsBuilt) return command;
        IsBuilt = true;
        return command.SetCanExecute(o => canExecuteTree.All(p => p(o)));
    }

    private ObservingCommandBuilder<T> CanExecuteIf<TValue>(Func<TValue, bool> predicate, params Expression<Func<T, TValue>>[] properties)
    {
        IsBuilt_Contract();
        ListensTo(properties);
        CanExecute(_ => properties.Select(p => p.Compile().Invoke(observedObject)).All(predicate));
        return this;
    }

    /// <summary>
    /// Implicitly converts this instance to a 
    /// <see cref="ObservingCommand"/>.
    /// </summary>
    /// <param name="command">The object to convert.</param>
    public static implicit operator ObservingCommand(ObservingCommandBuilder<T> command) => command.Build();
}
