/*
ConstantLoaderComparer.cs

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

using System.Collections.Generic;

namespace TheXDS.MCART.Types.Extensions
{
    /// <summary>
    /// Permite comparar la igualdad entre dos 
    /// <see cref="IConstantLoader"/> basado en el tipo de constante que
    /// ambos son capaces de cargar.
    /// </summary>
    public class ConstantLoaderComparer : IEqualityComparer<IConstantLoader>
    {
        /// <summary>
        /// Compara dos instancias de <see cref="IConstantLoader"/>.
        /// </summary>
        /// <param name="x">
        /// Primer objeto a comparar.
        /// </param>
        /// <param name="y">
        /// Segundo objeto a comparar.
        /// </param>
        /// <returns>
        /// <see langword="true"/> si ambos objetos cargan constantes del
        /// mismo tipo, <see langword="false"/> en caso contrario.
        /// </returns>
        public bool Equals(IConstantLoader? x, IConstantLoader? y)
        {
            return x?.ConstantType.Equals(y?.ConstantType) ?? y is null;
        }

        /// <summary>
        /// Obtiene el código hash de una instancia de 
        /// <see cref="IConstantLoader"/> que puede ser utilizado para
        /// comparar el tipo de constante que el objeto es capaz de cargar.
        /// </summary>
        /// <param name="obj">
        /// Objeto desde el cual obtener el código hash.
        /// </param>
        /// <returns>
        /// El código hash del tipo de constante que el objeto es capaz de
        /// cargar.
        /// </returns>
        public int GetHashCode(IConstantLoader obj)
        {
            return obj.ConstantType.GetHashCode();
        }
    }
}