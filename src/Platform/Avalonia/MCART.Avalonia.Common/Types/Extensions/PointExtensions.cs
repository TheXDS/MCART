/*
PointExtensions.cs

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

using P = Avalonia.Point;
using M = TheXDS.MCART.Types.Point;

namespace TheXDS.MCART.Types.Extensions;

/// <summary>
/// Extensiones de conversión de estructuras <see cref="M"/> en
/// <see cref="P"/>.
/// </summary>
public static class AvaloniaPointExtensions
{
    /// <summary>
    /// Convierte un <see cref="M"/> en un <see cref="P"/>.
    /// </summary>
    /// <param name="x"><see cref="M"/> a convertir.</param>
    /// <returns>
    /// Un <see cref="P"/> equivalente al <see cref="M"/> especificado.
    /// </returns>
    public static P ToPoint(M x) => new(x.X, x.Y);

    /// <summary>
    /// Convierte un <see cref="P"/> en un <see cref="M"/>.
    /// </summary>
    /// <param name="x"><see cref="P"/> a convertir.</param>
    /// <returns>
    /// Un <see cref="M"/> equivalente al <see cref="P"/> especificado.
    /// </returns>
    public static M ToMcartPoint(P x) => new(x.X, x.Y);
}
