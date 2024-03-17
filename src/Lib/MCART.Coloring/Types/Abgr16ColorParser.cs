﻿/*
Abgr16ColorParser.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2024 César Andrés Morgan

Permission is hereby granted, free of charge, to any person obtaining a copy of
this software and associated documentation files (the "Software"), to deal in
the Software without restriction, including without limitation the rights to
use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
of the Software, and to permit persons to whom the Software is furnished to do
so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using TheXDS.MCART.Math;
using TheXDS.MCART.Types.Base;

namespace TheXDS.MCART.Types;

/// <summary>
/// Implementa un <see cref="IColorParser{T}" /> que tiene como formato
/// de color un valor de 16 bits, 5 bits por canal, 1 bit de alfa.
/// </summary>
public class Abgr16ColorParser : IColorParser<short>
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
            (byte)((value & 0x1f) * 255 / 31).Clamp(0, 255),
            (byte)((((value & 0x3e0) >> 5) * 255 / 31) - 1).Clamp(0, 255),
            (byte)((((value & 0x7c00) >> 10) * 255 / 31) - 1).Clamp(0, 255),
            (byte)(((value & 0x8000) >> 15) * 255));
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
            ((short)System.Math.Round(color.B * 31f / 255) << 10) |
            (color.A >= 128 ? 0x8000 : 0));
    }
}



/// <summary>
/// Implementa un <see cref="IColorParser{T}" /> que tiene como formato
/// de color un valor de 16 bits, 5 bits por canal, 1 bit de alfa.
/// </summary>
public class Rgba16ColorParser : IColorParser<short>
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
            (byte)((((value & 0x7c00) >> 10) * 255 / 31) - 1).Clamp(0, 255),
            (byte)((((value & 0x3e0) >> 5) * 255 / 31) - 1).Clamp(0, 255),
            (byte)((value & 0x1f) * 255 / 31).Clamp(0, 255),
            (byte)(((value & 0x8000) >> 15) * 255));
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
            (byte)System.Math.Round(color.B * 31f / 255) |
            ((short)System.Math.Round(color.G * 31f / 255) << 5) |
            ((short)System.Math.Round(color.R * 31f / 255) << 10) |
            (color.A >= 128 ? 0x8000 : 0));
    }
}
