/*
ListExtensions.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright (c) 2011 - 2018 César Andrés Morgan

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

namespace TheXDS.MCART.Types.Extensions
{
    /// <summary>
    ///     Extensiones para todos los elementos de tipo <see cref="IList{T}" />.
    /// </summary>
    public static class ListExtensions
    {
        /// <summary>
        /// Aplica una operación de rotación sobre el <see cref="IList{T}"/>.
        /// </summary>
        /// <typeparam name="T">Tipo de elementos de la colección.</typeparam>
        /// <param name="c">Colección a rotar.</param>
        /// <param name="steps">
        /// Pasos de rotación. Un valor positivo rotará los elementos hacia la
        /// izquierda, y uno negativo los rotará hacia la derecha.
        /// </param>
        public static void ApplyRotate<T>(this IList<T> c, int steps)
        {
            if (steps > 0)
                for (var j = 0; j < steps; j++)
                    c.Add(c.PopFirst());
            else if (steps < 0)
                for (var j = 0; j > steps; j--)
                    c.Insert(0, c.Pop());
        }

        /// <summary>
        /// Aplica una operación de desplazamiento sobre el
        /// <see cref="IList{T}"/>.
        /// </summary>
        /// <typeparam name="T">Tipo de elementos de la colección.</typeparam>
        /// <param name="c">Colección a desplazar.</param>
        /// <param name="steps">
        /// Pasos de desplazamiento. Un valor positivo desplazará los elementos
        /// hacia la izquierda, y uno negativo los desplazará hacia la derecha,
        /// en ambos casos rellenando con valores predeterminados cada posición
        /// vacía resultante.
        /// </param>
        public static void ApplyShift<T>(this IList<T> c, int steps)
        {
            if (steps > 0)
                for (var j = 0; j < steps; j++)
                {
                    c.PopFirst();
                    c.Add(default);
                }
            else if (steps < 0)
                for (var j = 0; j > steps; j--)
                {
                    c.Pop();
                    c.Insert(0, default);
                }
        }
    }
}