/*
CollectionHelpers.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Este archivo contiene funciones de manipulación de objetos, 

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

using System.Collections.Generic;
using System.Linq;
using TheXDS.MCART.Attributes;

namespace TheXDS.MCART
{
    /// <summary>
    ///     Funciones auxiliares para trabajar con colecciones y enumeraciones.
    /// </summary>
    public static class CollectionHelpers
    {
        /// <summary>
        ///     Aplica un operador OR a una colección de valores
        ///     <see cref="bool"/>.
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        [Sugar]
        public static bool Or(this IEnumerable<bool> collection)
        {
            return collection.Aggregate(false, (current, j) => current | j);
        }
        /// <summary>
        ///     Aplica un operador OR a una colección de valores
        ///     <see cref="bool"/>.
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        [Sugar]
        public static bool And(this IEnumerable<bool> collection)
        {
            return collection.Aggregate(false, (current, j) => current & j);
        }
        /// <summary>
        ///     Aplica un operador OR a una colección de valores
        ///     <see cref="bool"/>.
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        [Sugar]
        public static bool Xor(this IEnumerable<bool> collection)
        {
            return collection.Aggregate(false, (current, j) => current ^ j);
        }
    }
}