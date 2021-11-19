/*
Abgr4444ColorParser.cs

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

namespace TheXDS.MCART.Types
{
    /// <summary>
    /// Implementa un <see cref="IColorParser{T}" /> que tiene como formato
    /// de color un valor de 16 bits, 4 bits por canal, 4 bits de alfa.
    /// </summary>
    public class Abgr4444ColorParser : IColorParser<short>
    {
        /// <summary>
        /// Convierte una estructura compatible en un <see cref="Color" />.
        /// </summary>
        /// <param name="value">Valor a convertir.</param>
        /// <returns>
        /// Un <see cref="Color" /> creado a partir del valor especificado.
        /// </returns>
        public Color From(short value)
        {
            return new(
                (byte)((value & 0xf) * 255 / 15),
                (byte)(((value & 0xf0) >> 4) * 255 / 15),
                (byte)(((value & 0xf00) >> 8) * 255 / 15),
                (byte)(((value & 0xf000) >> 12) * 255 / 15));
        }

        /// <summary>
        /// Convierte un <see cref="Color" /> en un valor, utilizando el
        /// <see cref="IColorParser{T}" /> especificado.
        /// </summary>
        /// <param name="color"><see cref="Color" /> a convertir.</param>
        /// <returns>
        /// Un valor creado a partir de este <see cref="Color" />.
        /// </returns>
        public short To(Color color)
        {
            return (short)(
                (color.R * 15 / 255) |
                ((color.G * 15 / 255) << 4) |
                ((color.B * 15 / 255) << 8) |
                ((color.A * 15 / 255) << 12));
        }
    }
}