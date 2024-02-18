/*
WpfColorExtensions.cs

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

using C = TheXDS.MCART.Types.Color;
using D = Avalonia.Media.Color;

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
        return new D(c.A, c.R, c.G, c.B);
    }

    /// <summary>
    /// Convierte una estructura <see cref="D"/> en un
    /// <see cref="C"/>.
    /// </summary>
    /// <param name="c"><see cref="D"/> a convertir.</param>
    public static C ToMcartColor(this D c)
    {
        return new C(c.R, c.G, c.B, c.A);
    }
}
