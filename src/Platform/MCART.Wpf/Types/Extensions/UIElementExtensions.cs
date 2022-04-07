/*
UIElementExtensions.cs

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
using System.Windows;
using System.Windows.Media.Imaging;

/// <summary>
/// Contiene extensiones para la clase <see cref="UIElement"/>.
/// </summary>
public static class UIElementExtensions
{
    /// <summary>
    /// Crea un mapa de bits de un <see cref="UIElement" />.
    /// </summary>
    /// <param name="u">
    /// <see cref="UIElement" /> a renderizar.
    /// </param>
    /// <returns>
    /// Un objeto <see cref="RenderTargetBitmap" /> que contiene una imagen
    /// renderizada de <paramref name="u" />.
    /// </returns>
    public static RenderTargetBitmap Render(this UIElement u)
    {
        return u.Render(new Size((int)u.RenderSize.Width, (int)u.RenderSize.Height), (int)Windows.GetDpi().Width);
    }
}
