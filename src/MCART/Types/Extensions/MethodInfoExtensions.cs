/*
MethodInfoExtensions.cs

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

#nullable enable

using System;
using System.Reflection;

namespace TheXDS.MCART.Types.Extensions
{
    /// <summary>
    ///     Contiene extensiones para la clase <see cref="MethodInfo"/>.
    /// </summary>
    public static class MethodInfoExtensions
    {
        /// <summary>
        ///     Crea un delegado del tipo especificado a partir del método.
        /// </summary>
        /// <typeparam name="T">
        ///     Tipo de delegado a obtener.
        /// </typeparam>
        /// <param name="m">Método del cual obtener un delegado.</param>
        /// <returns>
        ///     Un delegado del tipo especificado a partir del método, o
        ///     <see langword="null"/> si no es posible realizar la conversión.
        /// </returns>
        public static T? ToDelegate<T>(this MethodInfo m) where T : Delegate
        {
            return m.IsSignatureCompatible<T>() ? (T)Delegate.CreateDelegate(typeof(T), m) : null;
        }
    }
}