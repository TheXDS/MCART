/*
ObservingCommandExtensions.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2021 César Andrés Morgan

Morgan's CLR Advanced Runtime (MCART) is free software: you can redistribute it
and/or modify it under the terms of the GNU General Public License as published
by the Free Software Foundation, either version 3 of the License, or (at your
option) any later version.

Morgan's CLR Advanced Runtime (MCART) is distributed in the hope that it will
be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General
Public License for more details.

You should have received a copy of the GNU General Public License along with
this program. If not, see <http://www.gnu.org/licenses/>.
*/

namespace TheXDS.MCART.Types.Extensions;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using TheXDS.MCART.Exceptions;
using TheXDS.MCART.ViewModel;
using static TheXDS.MCART.Helpers.ReflectionHelpers;

/// <summary>
/// Brinda de extensiones sintácticas similares a Prism a los objetos
/// de tipo <see cref="ObservingCommand"/>.
/// </summary>
public static partial class ObservingCommandExtensions
{
    /// <summary>
    /// Indica que un <see cref="ObservingCommand"/> escuchará los
    /// cambios anunciados de la propiedad seleccionada.
    /// </summary>
    /// <typeparam name="T">
    /// Tipo de objeto para el cual seleccionar una propiedad.
    /// Generalmente, se trata de la referencia <see langword="this"/>.
    /// </typeparam>
    /// <param name="command">
    /// Comando para el cual se configurará la escucha.
    /// </param>
    /// <param name="propertySelector">
    /// Expresión Lambda de selección de propiedad.
    /// </param>
    /// <returns>
    /// <paramref name="command"/>, permitiendo el uso de sintáxis
    /// Fluent.
    /// </returns>
    /// <exception cref="InvalidArgumentException">
    /// Se produce si el elemento seleccionado por medio de
    /// <paramref name="propertySelector"/> no es una propiedad.
    /// </exception>
    public static ObservingCommand ListensToProperty<T>(this ObservingCommand command, Expression<Func<T, object?>> propertySelector)
    {
        ListensToProperty_Contract(command, propertySelector);
        return command.RegisterObservedProperty(GetProperty(propertySelector).Name);
    }

    /// <summary>
    /// Indica que un <see cref="ObservingCommand"/> escuchará los
    /// cambios anunciados de la propiedad seleccionada.
    /// </summary>
    /// <param name="command">
    /// Comando para el cual se configurará la escucha.
    /// </param>
    /// <param name="propertySelector">
    /// Expresión Lambda de selección de propiedad.
    /// </param>
    /// <returns>
    /// <paramref name="command"/>, permitiendo el uso de sintáxis
    /// Fluent.
    /// </returns>
    /// <exception cref="InvalidArgumentException">
    /// Se produce si el elemento seleccionado por medio de
    /// <paramref name="propertySelector"/> no es una propiedad.
    /// </exception>
    public static ObservingCommand ListensToProperty<T>(this ObservingCommand command, Expression<Func<T>> propertySelector)
    {
        ListensToProperty_Contract(command, propertySelector);
        return command.RegisterObservedProperty(GetProperty(propertySelector).Name);
    }

    /// <summary>
    /// Registra un conjunto de propiedades a ser escuchadas por este
    /// <see cref="ObservingCommand"/>.
    /// </summary>
    /// <param name="command">
    /// Comando para el cual se configurará la escucha.
    /// </param>
    /// <param name="properties">
    /// Colección de selectores de propiedades a ser escuchadas.
    /// </param>
    /// <returns>
    /// <paramref name="command"/>, permitiendo el uso de sintáxis
    /// Fluent.
    /// </returns>
    /// <exception cref="InvalidArgumentException">
    /// Se produce si cualquiera de los elementos seleccionados por medio
    /// de <paramref name="properties"/> no es una propiedad.
    /// </exception>
    public static ObservingCommand ListensToProperties<T>(this ObservingCommand command, params Expression<Func<T>>[] properties)
    {
        ListensToProperties_Contract(command, properties);
        return command.RegisterObservedProperty(properties.Select(GetProperty).Select(p => p.Name).ToArray());
    }

    /// <summary>
    /// Indica que un <see cref="ObservingCommand"/> escuchará los
    /// cambios anunciados de la propiedad seleccionada o del método
    /// para la bandera <see cref="System.Windows.Input.ICommand.CanExecute(object)"/>.
    /// </summary>
    /// <typeparam name="T">
    /// Tipo de objeto para el cual seleccionar una propiedad o el
    /// método. Generalmente, se trata de la referencia
    /// <see langword="this"/>.
    /// </typeparam>
    /// <param name="command">
    /// Comando para el cual se configurará la escucha.
    /// </param>
    /// <param name="selector">
    /// Expresión Lambda de selección de propiedado de método con tipo
    /// de retorno <see cref="bool"/>.
    /// </param>
    /// <returns>
    /// <paramref name="command"/>, permitiendo el uso de sintáxis
    /// Fluent.
    /// </returns>
    /// <exception cref="InvalidArgumentException">
    /// Se produce si el elemento seleccionado por medio de
    /// <paramref name="selector"/> no es una propiedad o un método con
    /// un tipo de retorno <see cref="bool"/>.
    /// </exception>
    public static ObservingCommand ListensToCanExecute<T>(this ObservingCommand command, Expression<Func<T, bool>> selector)
    {
        ListensToCanExecute_Contract(command, selector);
        return RegisterCanExecute(command, GetMember(selector));
    }

    /// <summary>
    /// Indica que un <see cref="ObservingCommand"/> escuchará los
    /// cambios anunciados de la propiedad seleccionada o del método
    /// para la bandera <see cref="System.Windows.Input.ICommand.CanExecute(object)"/>.
    /// </summary>
    /// <param name="command">
    /// Comando para el cual se configurará la escucha.
    /// </param>
    /// <param name="selector">
    /// Expresión Lambda de selección de propiedado de método con tipo
    /// de retorno <see cref="bool"/>.
    /// </param>
    /// <returns>
    /// <paramref name="command"/>, permitiendo el uso de sintáxis
    /// Fluent.
    /// </returns>
    /// <exception cref="InvalidArgumentException">
    /// Se produce si el elemento seleccionado por medio de
    /// <paramref name="selector"/> no es una propiedad o un método con
    /// un tipo de retorno <see cref="bool"/>.
    /// </exception>
    public static ObservingCommand ListensToCanExecute(this ObservingCommand command, Expression<Func<bool>> selector)
    {
        ListensToCanExecute_Contract(command, selector);
        return RegisterCanExecute(command, GetMember(selector));
    }

    /// <summary>
    /// Configura un <see cref="ObservingCommand"/> para poder ejecutarse
    /// cuando las propiedades inidicadas no sean <see langword="null"/>.
    /// </summary>
    /// <param name="command">Comando a configurar.</param>
    /// <param name="propertySelectors">
    /// Selectores de propiedades a observar.
    /// </param>
    /// <returns>
    /// <paramref name="command"/>, permitiendo el uso de sintáxis
    /// Fluent.
    /// </returns>
    public static ObservingCommand CanExecuteIfNotNull(this ObservingCommand command, params Expression<Func<object?>>[] propertySelectors)
    {
        return ListensToProperties(command, propertySelectors)
            .SetCanExecute(() => propertySelectors.Select(p => p.Compile().Invoke()).All(p => p is not null));
    }

    /// <summary>
    /// Configura un <see cref="ObservingCommand"/> para poder ejecutarse
    /// cuando las propiedades inidicadas no sean <see langword="null"/>.
    /// </summary>
    /// <param name="command">Comando a configurar.</param>
    /// <param name="propertySelectors">
    /// Selectores de propiedades a observar.
    /// </param>
    /// <returns>
    /// <paramref name="command"/>, permitiendo el uso de sintáxis
    /// Fluent.
    /// </returns>
    public static ObservingCommand CanExecuteIfNotDefault(this ObservingCommand command, params Expression<Func<ValueType>>[] propertySelectors)
    {
        return ListensToProperties(command, propertySelectors)
            .SetCanExecute(() => propertySelectors.Select(p => p.Compile().Invoke()).All(p => p != p.GetType().Default()));
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
                if (mi.ToDelegate<Func<object?, bool>>() is { } oFunc)
                    command.SetCanExecute(oFunc);
                else if (mi.ToDelegate<Func<bool>>() is { } func)
                    command.SetCanExecute(func);
                else
                    throw new InvalidArgumentException("selector");
                break;
        }
        return command;
    }
}
