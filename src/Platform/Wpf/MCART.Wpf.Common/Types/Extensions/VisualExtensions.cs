﻿/*
VisualExtensions.cs

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

using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace TheXDS.MCART.Helpers;

/// <summary>
/// Contiene extensiones para la clase <see cref="Visual"/>.
/// </summary>
public static class VisualExtensions
{
    /// <summary>
    /// Crea un mapa de bits de un <see cref="Visual" />.
    /// </summary>
    /// <param name="visual">
    /// <see cref="Visual" /> a renderizar.
    /// </param>
    /// <param name="size">
    /// Tamaño del canvas en donde se renderizará el control.
    /// </param>
    /// <param name="dpi">
    /// Valor de puntos por pulgada a utilizar para crear el mapa de bits.
    /// </param>
    /// <returns>
    /// Un objeto <see cref="RenderTargetBitmap" /> que contiene una imagen
    /// renderizada de <paramref name="visual" />.
    /// </returns>
    public static RenderTargetBitmap Render(this Visual visual, Size size, int dpi)
    {
        RenderTargetBitmap? bmp = new(
            (int)size.Width,
            (int)size.Height,
            dpi, dpi,
            PixelFormats.Pbgra32);
        bmp.Render(visual);
        return bmp;
    }
}
