﻿/*
Abgr2222ColorParser.cs

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

using TheXDS.MCART.Types.Base;

namespace TheXDS.MCART.Types;

/// <summary>
/// Implementa un <see cref="IColorParser{T}" /> que tiene como formato
/// de color un valor de 8 bits, 2 bits por canal, 2 bits de alfa.
/// </summary>
public class Abgr2222ColorParser : IColorParser<byte>
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
        return new(
            (byte)((value & 0x3) * 255 / 3),
            (byte)(((value & 0xc) >> 2) * 255 / 3),
            (byte)(((value & 0x30) >> 4) * 255 / 3),
            (byte)(((value & 0xf0) >> 6) * 255 / 3));
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
        return (byte)(
            (color.R * 3 / 255) |
            ((color.G * 3 / 255) << 2) |
            ((color.B * 3 / 255) << 4) |
            ((color.A * 3 / 255) << 6));
    }
}
