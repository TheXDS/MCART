/*
ColorExtensions.cs

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

using TheXDS.MCART.Resources;

namespace TheXDS.MCART.Types.Extensions;

/// <summary>
/// Extensiones para todos los elementos de tipo <see cref="Color" />.
/// </summary>
public static partial class ColorExtensions
{
    /// <summary>
    /// Obtiene una versión opaca del color especificado.
    /// </summary>
    /// <param name="color">
    /// Color a partir del cual generar una versión opaca.
    /// </param>
    /// <returns>
    /// Un color equivalente a <paramref name="color"/>, pero con total
    /// opacidad del canal alfa.
    /// </returns>
    public static Color Opaque(in this Color color)
    {
        return color + Colors.Black;
    }

    /// <summary>
    /// Obtiene una versión transparente del color especificado.
    /// </summary>
    /// <param name="color">
    /// Color a partir del cual generar una versión transparente.
    /// </param>
    /// <returns>
    /// Un color equivalente a <paramref name="color"/>, pero con total
    /// transparencia del canal alfa.
    /// </returns>
    public static Color Transparent(in this Color color)
    {
        return color - Colors.Black;
    }

    /// <summary>
    /// Crea una copia del color especificado, ajustando el nivel de
    /// transparencia del mismo.
    /// </summary>
    /// <param name="color">Color base.</param>
    /// <param name="value">Nivel de transparencia a aplicar.</param>
    /// <returns>
    /// Una copia del color, con el nivel de transparencia especificado.
    /// </returns>
    public static Color WithAlpha(this Color color, in float value)
    {
        WithAlpha_Contract(value);
        Color c = color.Clone();
        c.ScA = value;
        return c;
    }
}
