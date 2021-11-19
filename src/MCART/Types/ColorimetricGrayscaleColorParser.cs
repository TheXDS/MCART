/*
ColorimetricGrayscaleColorParser.cs

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
    /// de color un valor monocromático de 8 bits sin alfa, en el espacio
    /// colorimétrico de escala de grises.
    /// </summary>
    public class ColorimetricGrayscaleColorParser : IColorParser<byte>
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
            double yLinear = (double)value / 255;
            float ySrgb = (float)(yLinear > 0.0031308
                ? (System.Math.Pow(yLinear, 1 / 2.4) * 1.055) - 0.055
                : 12.92 * yLinear);
            return new Color(ySrgb, ySrgb, ySrgb);
        }

        /// <summary>
        /// Convierte un <see cref="Color" /> en un valor, utilizando el
        /// <see cref="IColorParser{T}" /> especificado.
        /// </summary>
        /// <param name="color"><see cref="Color" /> a convertir.</param>
        /// <returns>
        /// Un valor creado a partir de este <see cref="Color" />.
        /// </returns>
        public byte To(Color color)
        {
            double lr = color.ScR > 0.04045f ? System.Math.Pow((color.ScR + 0.055) / 1.055, 2.4) : color.ScR / 12.92;
            double lg = color.ScG > 0.04045f ? System.Math.Pow((color.ScG + 0.055) / 1.055, 2.4) : color.ScG / 12.92;
            double lb = color.ScB > 0.04045f ? System.Math.Pow((color.ScB + 0.055) / 1.055, 2.4) : color.ScB / 12.92;
            return (byte)((lr * 0.2126) + (lg * 0.7152) + (lb * 0.0722));
        }
    }
}