/*
System_Drawing_ImageExtensions.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author:
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2021 César Andrés Morgan

Morgan's CLR Advanced Runtime (MCART) is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

Morgan's CLR Advanced Runtime (MCART) is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

namespace TheXDS.MCART.Helpers;
using System;
using System.IO;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Img = System.Drawing.Image;

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
