/*
System_Drawing_ImageExtensions.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author:
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

using System.IO;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Img = System.Drawing.Image;

namespace TheXDS.MCART.Helpers;

/// <summary>
/// Contiene extensiones para la clase <see cref="Img"/>.
/// </summary>
public static class System_Drawing_ImageExtensions
{
    /// <summary>
    /// Convierte un <see cref="System.Drawing.Image" /> en un
    /// <see cref="BitmapImage" />.
    /// </summary>
    /// <param name="image"><see cref="System.Drawing.Image" /> a convertir.</param>
    /// <returns>
    /// Un <see cref="BitmapImage" /> que contiene la imagen obtenida desde
    /// un <see cref="System.Drawing.Image" />.
    /// </returns>
    public static BitmapImage ToImage(this Img image)
    {
        using MemoryStream? ms = new();
        BitmapImage? bi = new();
        image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
        ms.Position = 0;
        bi.BeginInit();
        bi.CacheOption = BitmapCacheOption.OnLoad;
        bi.StreamSource = ms;
        bi.EndInit();
        ms.Close();
        return bi;
    }

    /// <summary>
    /// Convierte un <see cref="System.Drawing.Image" /> en un
    /// <see cref="BitmapSource" />.
    /// </summary>
    /// <param name="image"><see cref="System.Drawing.Image" /> a convertir.</param>
    /// <returns>
    /// Un <see cref="BitmapImage" /> que contiene la imagen obtenida desde
    /// un <see cref="System.Drawing.Image" />.
    /// </returns>
    public static BitmapSource ToSource(this Img image)
    {
        using MemoryStream? ms = new();
        System.Drawing.Bitmap? bitmap = new(image);
        IntPtr bmpPt = bitmap.GetHbitmap();
        BitmapSource? bitmapSource = Imaging.CreateBitmapSourceFromHBitmap(
            bmpPt,
            IntPtr.Zero,
            Int32Rect.Empty,
            BitmapSizeOptions.FromEmptyOptions());
        bitmapSource.Freeze();
        Windows.TryDeleteObject(bmpPt);
        return bitmapSource;
    }

    /// <summary>
    /// Convierte un <see cref="System.Drawing.Image" /> en un
    /// <see cref="Visual" />.
    /// </summary>
    /// <param name="image"><see cref="System.Drawing.Image" /> a convertir.</param>
    /// <returns>
    /// Un <see cref="Visual" /> que contiene la imagen obtenida desde
    /// un <see cref="System.Drawing.Image" />.
    /// </returns>
    public static Visual ToVisual(this Img image)
    {
        DrawingVisual? v = new();
        using DrawingContext? dc = v.RenderOpen();
        dc.DrawImage(image.ToSource(), new Rect { Width = image.Width, Height = image.Height });
        return v;
    }
}
