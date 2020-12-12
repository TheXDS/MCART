/*
MethodInfoExtensions.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2020 César Andrés Morgan

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
using System.Reflection;

namespace TheXDS.MCART.Types.Extensions
{
    /// <summary>
    /// Contiene extensiones para la clase <see cref="MethodInfo"/>.
    /// </summary>
    public static partial class MethodInfoExtensions
    {
        /// <summary>
        /// Crea un delegado del tipo especificado a partir del método.
        /// </summary>
        /// <typeparam name="T">
        /// Tipo de delegado a obtener.
        /// </typeparam>
        /// <param name="m">Método del cual obtener un delegado.</param>
        /// <param name="targetInstance">
        /// Objetivo de instancia al cual enlazar el delegado generado, o
        /// <see langword="null"/> para generar un delegado de método estático.
        /// </param>
        /// <returns>
        /// Un delegado del tipo especificado a partir del método, o
        /// <see langword="null"/> si no es posible realizar la conversión.
        /// </returns>
        public static T? ToDelegate<T>(this MethodInfo m, object? targetInstance = null) where T : notnull, Delegate
        {
            ToDelegate_Contract(m, targetInstance);
            return (T?)Delegate.CreateDelegate(typeof(T), targetInstance, m, false);
        }

        /// <summary>
        /// Obtiene un valor que determina si el método no devuelve valores
        /// (si es <see langword="void"/>).
        /// </summary>
        /// <param name="m">Método a comprobar.</param>
        /// <returns>
        /// <see langword="true"/> si el método no devuelve valores, 
        /// <see langword="false"/> en caso contrario.
        /// </returns>
        public static bool IsVoid(this MethodInfo m)
        {
            if (m is null) throw new ArgumentNullException(nameof(m));
            return m.ReturnType == typeof(void);
        }
    }
}