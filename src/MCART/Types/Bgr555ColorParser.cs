﻿/*
Bgr555ColorParser.cs

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

namespace TheXDS.MCART.Types;

/// <summary>
/// Implementa un <see cref="IColorParser{T}" /> que tiene como formato
/// de color un valor de 15 bits, 5 bits por canal, sin alfa.
/// </summary>
public class Bgr555ColorParser : IColorParser<short>
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
            (byte)((value & 0x1f) * 255 / 31),
            (byte)(((value & 0x3e0) >> 5) * 255 / 31),
            (byte)(((value & 0x7c00) >> 10) * 255 / 31),
            255);
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
            (byte)System.Math.Round(color.R * 31f / 255) |
            ((short)System.Math.Round(color.G * 31f / 255) << 5) |
            ((short)System.Math.Round(color.B * 31f / 255) << 10));
    }
}
