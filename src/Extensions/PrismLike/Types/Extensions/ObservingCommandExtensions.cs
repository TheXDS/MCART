/*
ObservingCommandExtensions.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright (c) 2011 - 2019 César Andrés Morgan

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
        public static ObservingCommand ListensToProperty<T>(this ObservingCommand command, Expression<Func<T, object>> propertySelector)
        {
            var m = GetMember(propertySelector) as PropertyInfo ?? throw new InvalidOperationException();
            command.RegisterObservedProperty(m.Name);
            return command;
        }
        public static ObservingCommand ListensToCanExecute<T>(this ObservingCommand command, Expression<Func<T, bool>> selector)
        {
            var m = GetMember(selector);

            switch (m)
            {
                case PropertyInfo pi:
                    command.SetCanExecute(_ => (bool)pi.GetValue(command.ObservedSource));
                    break;
                case MethodInfo mi:
                    if (mi.ToDelegate<Func<object, bool>>() is var oFunc)
                        command.SetCanExecute(oFunc);
                    else if (mi.ToDelegate<Func<bool>>() is var func)
                        command.SetCanExecute(func);
                    else
                        throw new InvalidArgumentException(nameof(selector));
                    break;
            }
            command.RegisterObservedProperty(m.Name);
            return command;
        }
    }
}