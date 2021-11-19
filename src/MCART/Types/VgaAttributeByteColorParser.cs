/*
VgaAttributeByteColorParser.cs

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
    /// de color un byte de atributo VGA con información de color e
    /// intensidad, ignorando el color de fondo y el bit de Blink.
    /// </summary>
    public class VgaAttributeByteColorParser : IColorParser<byte>
    {
        /// <summary>
        /// Convierte una estructura compatible en un <see cref="Color" />.
        /// </summary>
        /// <param name="value">Valor a convertir.</param>
        /// <returns>
        /// Un <see cref="Color" /> creado a partir del valor especificado.
        /// </returns>
        public Color From(byte value)
        {
            var i = (byte)(((value >> 3) & 1) == 1 ? 255 : 127);
            var r = (byte)(((value >> 2) & 1) * i);
            var g = (byte)(((value >> 1) & 1) * i);
            var b = (byte)((value & 1) * i);
            return new Color(r, g, b);
        }

        /// <summary>
        /// Convierte un <see cref="Color" /> en su representación como un
        /// <see cref="byte" /> de atributo VGA.
        /// </summary>
        /// <returns>
        /// Un <see cref="byte" /> con la representación binaria de este
        /// <see cref="Color" />, en formato BGRI de 4 bits.
        /// El byte de atributo no incluirá color de fondo ni bit de
        /// Blink.
        /// </returns>
        public byte To(Color color)
        {
            var b = (byte)(color.B >= 127 ? 1 : 0);
            var g = (byte)(color.G >= 127 ? 2 : 0);
            var r = (byte)(color.R >= 127 ? 4 : 0);
            var i = (byte)((color.B | color.G | color.R) >= 191 ? 8 : 0);
            return (byte)(b | g | r | i);
        }
    }
}