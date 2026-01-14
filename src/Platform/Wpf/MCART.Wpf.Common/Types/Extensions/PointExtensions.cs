/*
PointExtensions.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2026 César Andrés Morgan

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

using P = System.Windows.Point;
using M = TheXDS.MCART.Types.Point;

namespace TheXDS.MCART.Types.Extensions;

/// <summary>
/// Extensions for converting <see cref="M"/> structures into <see cref="P"/>.
/// </summary>
public static class WpfPointExtensions
{
    /// <summary>
    /// Converts a <see cref="M"/> into a <see cref="P"/>.
    /// </summary>
    /// <param name="x">The <see cref="M"/> to convert.</param>
    /// <returns>
    /// A <see cref="P"/> equivalent to the specified <see cref="M"/>.
    /// </returns>
    public static P ToPoint(M x) => new(x.X, x.Y);

    /// <summary>
    /// Converts a <see cref="P"/> into a <see cref="M"/>.
    /// </summary>
    /// <param name="x">The <see cref="P"/> to convert.</param>
    /// <returns>
    /// An <see cref="M"/> equivalent to the specified <see cref="P"/>.
    /// </returns>
    public static M ToMcartPoint(P x) => new(x.X, x.Y);
}
