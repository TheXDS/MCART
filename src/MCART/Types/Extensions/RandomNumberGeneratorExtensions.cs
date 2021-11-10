/*
RandomNumberGeneratorExtensions.cs

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

#if !NET6_0_OR_GREATER

using TheXDS.MCART.Attributes;
using System.Security.Cryptography;

namespace TheXDS.MCART.Types.Extensions
{
    /// <summary>
    /// Extensiones para la clase <see cref="RandomNumberGenerator" />
    /// </summary>
    public static class RandomNumberGeneratorExtensions
    {
        /// <summary>
        /// Obtiene un arreglo de bytes con contenido criptográficamente
        /// aleatorio.
        /// </summary>
        /// <param name="count">Cantidad de bytes a obtener.</param>
        /// <returns>
        /// Un arreglo de bytes con contenido criptográficamente aleatorio.
        /// </returns>
        [Thunk] public static byte[] GetBytes(int count)
        {
            byte[] bytes = new byte[count];
            using (var rng = RandomNumberGenerator.Create()) rng.GetBytes(bytes);
            return bytes;
        }
    }
}
#endif