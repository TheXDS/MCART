/*
BitmapSourceExtensions.cs

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
using System.IO;
using System.Windows.Media.Imaging;

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
