﻿// UIColorTypeExtensions.cs
//
// This file is part of Morgan's CLR Advanced Runtime (MCART)
//
// Author(s):
//      César Andrés Morgan <xds_xps_ivx@hotmail.com>
//
// Released under the MIT License (MIT)
// Copyright © 2011 - 2024 César Andrés Morgan
//
// Permission is hereby granted, free of charge, to any person obtaining a copy of
// this software and associated documentation files (the “Software”), to deal in
// the Software without restriction, including without limitation the rights to
// use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
// of the Software, and to permit persons to whom the Software is furnished to do
// so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using System.Windows.Media;
using Windows.UI.ViewManagement;
using DC = System.Drawing.Color;
using MC = TheXDS.MCART.Types.Color;
using WC = System.Windows.Media.Color;

namespace TheXDS.MCART.Types.Extensions;

/// <summary>
/// Contiene extensiones para el tipo <see cref="UIColorType"/>.
/// </summary>
[CLSCompliant(false)]
public static class UIColorTypeExtensions
{
    /// <summary>
    /// Convierte un <see cref="UIColorType"/> en un <see cref="Brush"/>.
    /// </summary>
    /// <param name="color">Color a convertir.</param>
    /// <returns>
    /// Un nuevo <see cref="Brush"/> equivalente al <see cref="UIColorType"/>
    /// especificado.
    /// </returns>
    public static Brush ToMediaBrush(this UIColorType color)
    {
        return new SolidColorBrush(color.ToMediaColor());
    }

    /// <summary>
    /// Convierte un <see cref="UIColorType"/> en un <see cref="MC"/>.
    /// </summary>
    /// <param name="color">Color a convertir.</param>
    /// <returns>
    /// Un nuevo <see cref="MC"/> equivalente al <see cref="UIColorType"/>
    /// especificado.
    /// </returns>
    public static WC ToMediaColor(this UIColorType color)
    {
        var c = new UISettings().GetColorValue(color);
        return WC.FromArgb(c.A, c.R, c.G, c.B);
    }

    /// <summary>
    /// Convierte un <see cref="UIColorType"/> en un <see cref="DC"/>.
    /// </summary>
    /// <param name="color">Color a convertir.</param>
    /// <returns>
    /// Un nuevo <see cref="DC"/> equivalente al <see cref="UIColorType"/>
    /// especificado.
    /// </returns>
    public static DC ToDrawingColor(this UIColorType color)
    {
        var c = new UISettings().GetColorValue(color);
        return DC.FromArgb(c.A, c.R, c.G, c.B);
    }

    /// <summary>
    /// Convierte un <see cref="UIColorType"/> en un <see cref="MC"/>.
    /// </summary>
    /// <param name="color">Color a convertir.</param>
    /// <returns>
    /// Un nuevo <see cref="MC"/> equivalente al <see cref="UIColorType"/>
    /// especificado.
    /// </returns>
    public static MC ToColor(this UIColorType color)
    {
        var c = new UISettings().GetColorValue(color);
        return new MC(c.A, c.R, c.G, c.B);
    }
}
