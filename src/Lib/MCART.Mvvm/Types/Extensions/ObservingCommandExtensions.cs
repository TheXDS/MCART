/*
ObservingCommandExtensions.cs

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

using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Reflection;
using TheXDS.MCART.Component;
using TheXDS.MCART.Exceptions;
using static TheXDS.MCART.Helpers.ReflectionHelpers;

namespace TheXDS.MCART.Types.Extensions;

/// <summary>
/// Provides Prism-like syntactic extensions for
/// <see cref="ObservingCommand"/> instances.
/// </summary>
public static partial class ObservingCommandExtensions
{
    /// <summary>
    /// Specifies that an <see cref="ObservingCommand"/> will listen for
    /// change notifications of the selected property.
    /// </summary>
    /// <param name="command">
    /// The command for which the listener will be configured.
    /// </param>
    /// <param name="propertySelector">
    /// Lambda expression selecting the property to observe.
    /// </param>
    /// <returns>
    /// <paramref name="command"/>, allowing Fluent syntax.
    /// </returns>
    /// <exception cref="InvalidArgumentException">
    /// Thrown if the member selected by <paramref name="propertySelector"/>
    /// is not a property.
    /// </exception>
    public static ObservingCommand ListensToProperty(this ObservingCommand command, Expression<Func<object?>> propertySelector)
    {
        ListensToProperty_Contract(command, propertySelector);
        return command.RegisterObservedProperty(GetProperty(propertySelector).Name);
    }

    /// <summary>
    /// Specifies that an <see cref="ObservingCommand"/> will listen for
    /// change notifications of the selected property.
    /// </summary>
    /// <typeparam name="T">
    /// Type of object for which to select a property (commonly the
    /// reference <see langword="this"/>).
    /// </typeparam>
    /// <param name="command">
    /// The command for which the listener will be configured.
    /// </param>
    /// <param name="propertySelector">
    /// Lambda expression selecting the property to observe.
    /// </param>
    /// <returns>
    /// <paramref name="command"/>, allowing Fluent syntax.
    /// </returns>
    /// <exception cref="InvalidArgumentException">
    /// Thrown if the member selected by <paramref name="propertySelector"/>
    /// is not a property.
    /// </exception>
    public static ObservingCommand ListensToProperty<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] T>(this ObservingCommand command, Expression<Func<T, object?>> propertySelector)
    {
        ListensToProperty_Contract(command, propertySelector);
        return command.RegisterObservedProperty(GetProperty(propertySelector).Name);
    }

    /// <summary>
    /// Registers a set of properties to be listened to by this
    /// <see cref="ObservingCommand"/>.
    /// </summary>
    /// <param name="command">
    /// The command for which the listeners will be configured.
    /// </param>
    /// <param name="properties">
    /// Collection of property selectors to observe.
    /// </param>
    /// <returns>
    /// <paramref name="command"/>, allowing Fluent syntax.
    /// </returns>
    /// <exception cref="InvalidArgumentException">
    /// Thrown if any member selected by <paramref name="properties"/> is
    /// not a property.
    /// </exception>
    public static ObservingCommand ListensToProperties<TProperty>(this ObservingCommand command, params Expression<Func<TProperty>>[] properties)
    {
        ListensToProperties_Contract(command, properties);
        return command.RegisterObservedProperty([.. properties.Select(GetProperty).Select(p => p.Name)]);
    }

    /// <summary>
    /// Registers a set of properties to be listened to by this
    /// <see cref="ObservingCommand"/>.
    /// </summary>
    /// <param name="command">
    /// The command for which the listeners will be configured.
    /// </param>
    /// <param name="properties">
    /// Collection of property selectors to observe.
    /// </param>
    /// <returns>
    /// <paramref name="command"/>, allowing Fluent syntax.
    /// </returns>
    /// <exception cref="InvalidArgumentException">
    /// Thrown if any member selected by <paramref name="properties"/> is
    /// not a property.
    /// </exception>
    public static ObservingCommand ListensToProperties(this ObservingCommand command, params Expression<Func<object?>>[] properties)
    {
        ListensToProperties_Contract(command, properties);
        return command.RegisterObservedProperty([.. properties.Select(GetProperty).Select(p => p.Name)]);
    }

    /// <summary>
    /// Registers a set of properties to be listened to by this
    /// <see cref="ObservingCommand"/>.
    /// </summary>
    /// <param name="command">
    /// The command for which the listeners will be configured.
    /// </param>
    /// <param name="properties">
    /// Collection of property selectors to observe.
    /// </param>
    /// <returns>
    /// <paramref name="command"/>, allowing Fluent syntax.
    /// </returns>
    /// <exception cref="InvalidArgumentException">
    /// Thrown if any member selected by <paramref name="properties"/> is
    /// not a property.
    /// </exception>
    public static ObservingCommand ListensToProperties<T>(this ObservingCommand command, params Expression<Func<T, object?>>[] properties)
    {
        ListensToProperties_Contract(command, properties);
        return command.RegisterObservedProperty([.. properties.Select(GetProperty).Select(p => p.Name)]);
    }

    /// <summary>
    /// Registers a set of properties to be listened to by this
    /// <see cref="ObservingCommand"/>.
    /// </summary>
    /// <param name="command">
    /// The command for which the listeners will be configured.
    /// </param>
    /// <param name="properties">
    /// Collection of property selectors to observe.
    /// </param>
    /// <returns>
    /// <paramref name="command"/>, allowing Fluent syntax.
    /// </returns>
    /// <exception cref="InvalidArgumentException">
    /// Thrown if any member selected by <paramref name="properties"/> is
    /// not a property.
    /// </exception>
    public static ObservingCommand ListensToProperties<T, TProperty>(this ObservingCommand command, params Expression<Func<T, TProperty>>[] properties)
    {
        ListensToProperties_Contract(command, properties);
        return command.RegisterObservedProperty([.. properties.Select(GetProperty).Select(p => p.Name)]);
    }

    /// <summary>
    /// Specifies that an <see cref="ObservingCommand"/> will listen for
    /// change notifications of the selected property or method used for
    /// the <see cref="System.Windows.Input.ICommand.CanExecute(object)"/>
    /// flag.
    /// </summary>
    /// <typeparam name="T">
    /// Type of object for which to select a property or method (commonly
    /// the reference <see langword="this"/>).
    /// </typeparam>
    /// <param name="command">
    /// The command for which the listener will be configured.
    /// </param>
    /// <param name="selector">
    /// Lambda expression selecting a property or a method that returns
    /// <see cref="bool"/>.
    /// </param>
    /// <returns>
    /// <paramref name="command"/>, allowing Fluent syntax.
    /// </returns>
    /// <exception cref="InvalidArgumentException">
    /// Thrown if the member selected by <paramref name="selector"/> is not
    /// a property or a method with a <see cref="bool"/> return type.
    /// </exception>
    public static ObservingCommand ListensToCanExecute<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] T>(this ObservingCommand command, Expression<Func<T, bool>> selector)
    {
        ListensToCanExecute_Contract(command, selector);
        return RegisterCanExecute(command, GetMember(selector));
    }

    /// <summary>
    /// Specifies that an <see cref="ObservingCommand"/> will listen for
    /// change notifications of the selected property or method used for
    /// the <see cref="System.Windows.Input.ICommand.CanExecute(object)"/>
    /// flag.
    /// </summary>
    /// <param name="command">
    /// The command for which the listener will be configured.
    /// </param>
    /// <param name="selector">
    /// Lambda expression selecting a property or a method that returns
    /// <see cref="bool"/>.
    /// </param>
    /// <returns>
    /// <paramref name="command"/>, allowing Fluent syntax.
    /// </returns>
    /// <exception cref="InvalidArgumentException">
    /// Thrown if the member selected by <paramref name="selector"/> is not
    /// a property or a method with a <see cref="bool"/> return type.
    /// </exception>
    public static ObservingCommand ListensToCanExecute(this ObservingCommand command, Expression<Func<bool>> selector)
    {
        ListensToCanExecute_Contract(command, selector);
        return RegisterCanExecute(command, GetMember(selector));
    }

    /// <summary>
    /// Configures an <see cref="ObservingCommand"/> to be executable when the
    /// specified properties are not <see langword="null"/>.
    /// </summary>
    /// <param name="command">Command to configure.</param>
    /// <param name="propertySelectors">
    /// Property selectors to observe.
    /// </param>
    /// <returns>
    /// The <paramref name="command"/>, enabling fluent syntax.
    /// </returns>
    public static ObservingCommand CanExecuteIfNotNull(this ObservingCommand command, params Expression<Func<object?>>[] propertySelectors)
    {
        return ListensToProperties(command, propertySelectors)
            .SetCanExecute(() => propertySelectors.Select(p => p.Compile().Invoke()).All(p => p is not null));
    }

    /// <summary>
    /// Configures an <see cref="ObservingCommand"/> to be executable when the
    /// specified value-type properties are not equal to their default value.
    /// </summary>
    /// <param name="command">Command to configure.</param>
    /// <param name="propertySelectors">
    /// Property selectors to observe.
    /// </param>
    /// <returns>
    /// The <paramref name="command"/>, enabling fluent syntax.
    /// </returns>
    public static ObservingCommand CanExecuteIfNotDefault(this ObservingCommand command, params Expression<Func<ValueType>>[] propertySelectors)
    {
        return ListensToProperties(command, propertySelectors)
            .SetCanExecute(() => propertySelectors.Select(p => p.Compile().Invoke()).All(p => !Equals(p, p.GetType().Default())));
    }

    /// <summary>
    /// Configures an <see cref="ObservingCommand"/> to be executable when the
    /// specified properties on the observed source are not
    /// <see langword="null"/>.
    /// </summary>
    /// <param name="command">Command to configure.</param>
    /// <param name="propertySelectors">
    /// Property selectors (targeting the observed source) to observe.
    /// </param>
    /// <returns>
    /// The <paramref name="command"/>, enabling fluent syntax.
    /// </returns>
    public static ObservingCommand CanExecuteIfNotNull<T>(this ObservingCommand command, params Expression<Func<T, object?>>[] propertySelectors)
    {
        return ListensToProperties(command, propertySelectors)
            .SetCanExecute(() => propertySelectors.Select(p => p.Compile().Invoke((T)command.ObservedSource)).All(p => p is not null));
    }

    /// <summary>
    /// Configures an <see cref="ObservingCommand"/> to be executable when the
    /// specified value-type properties on the observed source are not equal
    /// to their default values.
    /// </summary>
    /// <param name="command">Command to configure.</param>
    /// <param name="propertySelectors">
    /// Property selectors (targeting the observed source) to observe.
    /// </param>
    /// <returns>
    /// The <paramref name="command"/>, enabling fluent syntax.
    /// </returns>
    public static ObservingCommand CanExecuteIfNotDefault<T>(this ObservingCommand command, params Expression<Func<T, ValueType>>[] propertySelectors)
    {
        return ListensToProperties(command, propertySelectors)
            .SetCanExecute(() => propertySelectors.Select(p => p.Compile().Invoke((T)command.ObservedSource)).All(p => !Equals(p, p.GetType().Default())));
    }

    private static ObservingCommand RegisterCanExecute(this ObservingCommand command, MemberInfo m)
    {
        switch (m)
        {
            case PropertyInfo pi:
                command
                    .SetCanExecute(_ => (bool)pi.GetValue(command.ObservedSource)!)
                    .RegisterObservedProperty(m.Name);
                break;
            case MethodInfo mi:
                if (mi.ToDelegate<Func<object?, bool>>(command.ObservedSource) is { } oFunc)
                    command.SetCanExecute(oFunc);
                else if (mi.ToDelegate<Func<bool>>(command.ObservedSource) is { } function)
                    command.SetCanExecute(function);
                else
                    throw new InvalidArgumentException("selector");
                break;
        }
        return command;
    }
}
