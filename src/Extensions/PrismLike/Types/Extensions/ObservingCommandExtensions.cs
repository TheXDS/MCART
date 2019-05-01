/*
ObservingCommandExtensions.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2019 César Andrés Morgan

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

using System;
using System.Linq.Expressions;
using TheXDS.MCART.ViewModel;
using static TheXDS.MCART.ReflectionHelpers;
using TheXDS.MCART.Exceptions;
using System.Reflection;

namespace TheXDS.MCART.Types.Extensions
{
    /// <summary>
    ///     Brinda de extensiones sintácticas similares a Prism a los objetos
    ///     de tipo <see cref="ObservingCommand"/>.
    /// </summary>
    public static class ObservingCommandExtensions
    {
        /// <summary>
        ///     Indica que un <see cref="ObservingCommand"/> escuchará los
        ///     cambios anunciados de la propiedad seleccionada.
        /// </summary>
        /// <typeparam name="T">
        ///     Tipo de objeto para el cual seleccionar una propiedad.
        ///     Generalmente, se trata de la referencia <see langword="this"/>.
        /// </typeparam>
        /// <param name="command">
        ///     Comando para el cual se configurará la escucha.
        /// </param>
        /// <param name="propertySelector">
        ///     Expresión Lambda de selección de propiedad.
        /// </param>
        /// <returns>
        ///     <paramref name="command"/>, permitiendo el uso de sintáxis
        ///     Fluent.
        /// </returns>
        /// <exception cref="InvalidArgumentException">
        ///     Se produce si el elemento seleccionado por medio de
        ///     <paramref name="propertySelector"/> no es una propiedad.
        /// </exception>
        public static ObservingCommand ListensToProperty<T>(this ObservingCommand command, Expression<Func<T, object>> propertySelector)
        {
            var m = GetMember(propertySelector) as PropertyInfo ?? throw new InvalidArgumentException();
            command.RegisterObservedProperty(m.Name);
            return command;
        }

        /// <summary>
        ///     Indica que un <see cref="ObservingCommand"/> escuchará los
        ///     cambios anunciados de la propiedad seleccionada.
        /// </summary>
        /// <param name="command">
        ///     Comando para el cual se configurará la escucha.
        /// </param>
        /// <param name="propertySelector">
        ///     Expresión Lambda de selección de propiedad.
        /// </param>
        /// <returns>
        ///     <paramref name="command"/>, permitiendo el uso de sintáxis
        ///     Fluent.
        /// </returns>
        /// <exception cref="InvalidArgumentException">
        ///     Se produce si el elemento seleccionado por medio de
        ///     <paramref name="propertySelector"/> no es una propiedad.
        /// </exception>
        public static ObservingCommand ListensToProperty(this ObservingCommand command, Expression<Func<object>> propertySelector)
        {
            var m = GetMember(propertySelector) as PropertyInfo ?? throw new InvalidArgumentException();
            command.RegisterObservedProperty(m.Name);
            return command;
        }

        /// <summary>
        ///     Indica que un <see cref="ObservingCommand"/> escuchará los
        ///     cambios anunciados de la propiedad seleccionada o del método
        ///     para la bandera <see cref="System.Windows.Input.ICommand.CanExecute(object)"/>.
        /// </summary>
        /// <typeparam name="T">
        ///     Tipo de objeto para el cual seleccionar una propiedad o el
        ///     método. Generalmente, se trata de la referencia
        ///     <see langword="this"/>.
        /// </typeparam>
        /// <param name="command">
        ///     Comando para el cual se configurará la escucha.
        /// </param>
        /// <param name="selector">
        ///     Expresión Lambda de selección de propiedado de método con tipo
        ///     de retorno <see cref="bool"/>.
        /// </param>
        /// <returns>
        ///     <paramref name="command"/>, permitiendo el uso de sintáxis
        ///     Fluent.
        /// </returns>
        /// <exception cref="InvalidArgumentException">
        ///     Se produce si el elemento seleccionado por medio de
        ///     <paramref name="selector"/> no es una propiedad o un método con
        ///     un tipo de retorno <see cref="bool"/>.
        /// </exception>
        public static ObservingCommand ListensToCanExecute<T>(this ObservingCommand command, Expression<Func<T, bool>> selector)
        {
            return RegisterCanExecute(command, GetMember(selector));
        }

        /// <summary>
        ///     Indica que un <see cref="ObservingCommand"/> escuchará los
        ///     cambios anunciados de la propiedad seleccionada o del método
        ///     para la bandera <see cref="System.Windows.Input.ICommand.CanExecute(object)"/>.
        /// </summary>
        /// <param name="command">
        ///     Comando para el cual se configurará la escucha.
        /// </param>
        /// <param name="selector">
        ///     Expresión Lambda de selección de propiedado de método con tipo
        ///     de retorno <see cref="bool"/>.
        /// </param>
        /// <returns>
        ///     <paramref name="command"/>, permitiendo el uso de sintáxis
        ///     Fluent.
        /// </returns>
        /// <exception cref="InvalidArgumentException">
        ///     Se produce si el elemento seleccionado por medio de
        ///     <paramref name="selector"/> no es una propiedad o un método con
        ///     un tipo de retorno <see cref="bool"/>.
        /// </exception>
        public static ObservingCommand ListensToCanExecute(this ObservingCommand command, Expression<Func<bool>> selector)
        {
            return RegisterCanExecute(command, GetMember(selector));
        }

        private static ObservingCommand RegisterCanExecute(this ObservingCommand command, MemberInfo m)
        {
            switch (m)
            {
                case PropertyInfo pi:
                    command.SetCanExecute(_ => (bool)pi.GetValue(command.ObservedSource));
                    command.RegisterObservedProperty(m.Name);
                    break;
                case MethodInfo mi:
                    if (mi.ToDelegate<Func<object, bool>>() is var oFunc)
                        command.SetCanExecute(oFunc);
                    else if (mi.ToDelegate<Func<bool>>() is var func)
                        command.SetCanExecute(func);
                    else
                        throw new InvalidArgumentException(@"selector");
                    break;
            }
            return command;
        }
    }
}