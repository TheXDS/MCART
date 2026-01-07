/*
BitmapSourceExtensions.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author:
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

using System.IO;
using System.Windows.Media.Imaging;

namespace TheXDS.MCART.Helpers;

/// <summary>
/// Contiene extensiones para la clase <see cref="BitmapSource"/>.
/// </summary>
public static class BitmapSourceExtensions
{
    /// <summary>
    /// Convierte un <see cref="BitmapSource" /> en un
    /// <see cref="BitmapImage" />.
    /// </summary>
    /// <param name="bs"><see cref="BitmapSource" /> a convertir.</param>
    /// <returns>
    /// Un <see cref="BitmapImage" /> que contiene la imagen obtenida desde
    /// un <see cref="BitmapSource" />.
    /// </returns>
    public static BitmapImage ToImage(this BitmapSource bs)
    {
        PngBitmapEncoder? ec = new();
        using MemoryStream? ms = new();
        BitmapImage? bi = new();
        ec.Frames.Add(BitmapFrame.Create(bs));
        ec.Save(ms);
        ms.Position = 0;
        bi.BeginInit();
        bi.StreamSource = ms;
        bi.EndInit();
        ms.Close();
        return bi;
    }
}
