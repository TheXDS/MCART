/*
VgaAttributeByteColorParser.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2022 César Andrés Morgan

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

namespace TheXDS.MCART.Types;

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
        byte i = (byte)(((value >> 3) & 1) == 1 ? 255 : 127);
        byte r = (byte)(((value >> 2) & 1) * i);
        byte g = (byte)(((value >> 1) & 1) * i);
        byte b = (byte)((value & 1) * i);
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
        byte b = (byte)(color.B >= 127 ? 1 : 0);
        byte g = (byte)(color.G >= 127 ? 2 : 0);
        byte r = (byte)(color.R >= 127 ? 4 : 0);
        byte i = (byte)((color.B | color.G | color.R) >= 191 ? 8 : 0);
        return (byte)(b | g | r | i);
    }
}
