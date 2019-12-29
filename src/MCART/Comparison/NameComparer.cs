/*
NameComparer.cs

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

using System.Collections.Generic;
using TheXDS.MCART.Types;
using TheXDS.MCART.Types.Base;

namespace TheXDS.MCART.Comparison
{
    /// <summary>
    /// Compara el nombre de dos objetos.
    /// </summary>
    public class NameComparer : IEqualityComparer<INameable>
    {
        /// <summary>
        /// Determina si los objetos especificados son iguales.
        /// </summary>
        /// <param name="x">Primer objeto a comparar.</param>
        /// <param name="y">Segundo objeto a comparar.</param>
        /// <returns>
        /// <see langword="true"/> si el nombre de ambos objetos es el
        /// mismo, <see langword="false"/> en caso contrario.
        /// </returns>
        public bool Equals(INameable x, INameable y)
        {
            return x.Name == y.Name;
        }

        /// <summary>
        /// Obtiene el código Hash para el nombre del objeto especificado.
        /// </summary>
        /// <param name="obj">
        /// Objeto para el cual obtener un código Hash.
        /// </param>
        /// <returns>
        /// El código hash para el nombre del objeto especificado.
        /// </returns>
        public int GetHashCode(INameable obj)
        {
            return obj.Name.GetHashCode();
        }
    }
}
