/*
ColorimetricGrayscaleColorParser.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2025 César Andrés Morgan

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
        return (byte)(((lr * 0.2126) + (lg * 0.7152) + (lb * 0.0722)) * 255);
    }
}
