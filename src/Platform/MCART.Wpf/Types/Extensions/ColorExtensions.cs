/*
WpfColorExtensions.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2023 César Andrés Morgan

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

using System.Linq;
using System.Windows.Media;
using TheXDS.MCART.Math;
using B = System.Windows.Media.Brush;
using C = TheXDS.MCART.Types.Color;
using D = System.Windows.Media.Color;

namespace TheXDS.MCART.Types.Extensions;

/// <summary>
/// Extensiones de conversión de colores de MCART en
/// <see cref="D"/>.
/// </summary>
public static class WpfColorExtensions
{
    /// <summary>
    /// Convierte una estructura <see cref="C"/> en un
    /// <see cref="D"/>.
    /// </summary>
    /// <param name="c"><see cref="C"/> a convertir.</param>
    public static D Color(this C c)
    {
        return D.FromScRgb(c.ScA, c.ScR, c.ScG, c.ScB);
    }

    /// <summary>
    /// Convierte una estructura <see cref="D"/> en un
    /// <see cref="C"/>.
    /// </summary>
    /// <param name="c"><see cref="D"/> a convertir.</param>
    public static C ToMcartColor(this D c)
    {
        return new C(
            c.ScR.Clamp(0.0f, 1.0f),
            c.ScG.Clamp(0.0f, 1.0f),
            c.ScB.Clamp(0.0f, 1.0f),
            c.ScA.Clamp(0.0f, 1.0f));
    }

    /// <summary>
    /// Convierte un <see cref="SolidColorBrush"/> en un
    /// <see cref="C"/>.
    /// </summary>
    /// <param name="c"><see cref="D"/> a convertir.</param>
    public static C ToMcartColor(this SolidColorBrush c)
    {
        return new C(
            c.Color.ScR.Clamp(0.0f, 1.0f),
            c.Color.ScG.Clamp(0.0f, 1.0f),
            c.Color.ScB.Clamp(0.0f, 1.0f),
            c.Color.ScA.Clamp(0.0f, 1.0f));
    }

    /// <summary>
    /// Convierte una estructura <see cref="C"/> en un
    /// <see cref="B"/>.
    /// </summary>
    /// <param name="c"><see cref="C"/> a convertir.</param>
    /// <returns>
    /// Un <see cref="B"/> de color sólido equivalente al
    /// <see cref="C"/> original.
    /// </returns>
    public static SolidColorBrush Brush(this C c)
    {
        return new SolidColorBrush(Color(c));
    }

    /// <summary>
    /// Mezcla todos los colores de un <see cref="GradientBrush"/> en
    /// un <see cref="C"/>.
    /// </summary>
    /// <param name="g"></param>
    /// <returns>
    /// Un <see cref="C"/> que es la mezcla de todos los colores del
    /// <see cref="GradientBrush"/>.
    /// </returns>
    public static C Blend(this GradientBrush g)
    {
        return C.Blend(g.GradientStops.Select(p => ToMcartColor(p.Color)));
    }
}
