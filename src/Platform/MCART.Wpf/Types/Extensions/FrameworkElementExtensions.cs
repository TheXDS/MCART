/*
FrameworkElementExtensions.cs

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
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using TheXDS.MCART.Attributes;

/// <summary>
/// Contiene extensiones para la clase <see cref="FrameworkElement"/>.
/// </summary>
public static class FrameworkElementExtensions
{
    /// <summary>
    /// Enlaza una propiedad de dependencia de un <see cref="DependencyObject" /> a un <see cref="FrameworkElement" />.
    /// </summary>
    /// <param name="obj">Objeto destino del enlace</param>
    /// <param name="dp">Propiedad de dependencia a enlazar.</param>
    /// <param name="source">Orígen del enlace.</param>
    [Sugar]
    public static void Bind(this FrameworkElement obj, DependencyProperty dp, DependencyObject source)
    {
        obj.Bind(dp, (object)source, dp, BindingMode.TwoWay);
    }

    /// <summary>
    /// Enlaza una propiedad de dependencia de un <see cref="DependencyObject" /> a un <see cref="FrameworkElement" />.
    /// </summary>
    /// <param name="obj">Objeto destino del enlace</param>
    /// <param name="dp">Propiedad de dependencia a enlazar.</param>
    /// <param name="source">Orígen del enlace.</param>
    /// <param name="mode">Modo del enlace.</param>
    [Sugar]
    public static void Bind(this FrameworkElement obj, DependencyProperty dp, DependencyObject source, BindingMode mode)
    {
        obj.Bind(dp, (object)source, dp, mode);
    }

    /// <summary>
    /// Enlaza una propiedad de dependencia de un <see cref="DependencyObject" /> a un
    /// <see cref="INotifyPropertyChanged" />.
    /// </summary>
    /// <param name="obj">Objeto destino del enlace</param>
    /// <param name="dp">Propiedad de dependencia a enlazar.</param>
    /// <param name="source">Orígen del enlace.</param>
    [Sugar]
    public static void Bind(this FrameworkElement obj, DependencyProperty dp, INotifyPropertyChanged source)
    {
        obj.Bind(dp, (object)source, dp, BindingMode.TwoWay);
    }

    /// <summary>
    /// Enlaza una propiedad de dependencia de un <see cref="INotifyPropertyChanged" /> a un
    /// <see cref="FrameworkElement" />.
    /// </summary>
    /// <param name="obj">Objeto destino del enlace</param>
    /// <param name="dp">Propiedad de dependencia a enlazar.</param>
    /// <param name="source">Orígen del enlace.</param>
    /// <param name="mode">Modo del enlace.</param>
    [Sugar]
    public static void Bind(this FrameworkElement obj, DependencyProperty dp, INotifyPropertyChanged source, BindingMode mode)
    {
        obj.Bind(dp, (object)source, dp, mode);
    }

    /// <summary>
    /// Enlaza una propiedad de dependencia de un <see cref="DependencyObject" /> a un <see cref="FrameworkElement" />.
    /// </summary>
    /// <param name="obj">Objeto destino del enlace</param>
    /// <param name="targetDp">Propiedad de dependencia de destino del enlace.</param>
    /// <param name="source">Orígen del enlace.</param>
    /// <param name="sourceDp">Propiedad de dependencia de destino del enlace.</param>
    [Sugar]
    public static void Bind(this FrameworkElement obj, DependencyProperty targetDp, DependencyObject source, DependencyProperty sourceDp)
    {
        obj.Bind(targetDp, (object)source, sourceDp, BindingMode.TwoWay);
    }

    /// <summary>
    /// Enlaza una propiedad de dependencia de un <see cref="DependencyObject" /> a un <see cref="FrameworkElement" />.
    /// </summary>
    /// <param name="obj">Objeto destino del enlace</param>
    /// <param name="targetDp">Propiedad de dependencia de destino del enlace.</param>
    /// <param name="source">Orígen del enlace.</param>
    /// <param name="sourceDp">Propiedad de dependencia de destino del enlace.</param>
    /// <param name="mode">Modo del enlace.</param>
    [Sugar]
    public static void Bind(this FrameworkElement obj, DependencyProperty targetDp, DependencyObject source, DependencyProperty sourceDp, BindingMode mode)
    {
        obj.Bind(targetDp, (object)source, sourceDp, mode);
    }

    /// <summary>
    /// Enlaza una propiedad de dependencia de un <see cref="INotifyPropertyChanged" /> a un
    /// <see cref="FrameworkElement" />.
    /// </summary>
    /// <param name="obj">Objeto destino del enlace</param>
    /// <param name="targetDp">Propiedad de dependencia de destino del enlace.</param>
    /// <param name="source">Orígen del enlace.</param>
    /// <param name="sourceDp">Propiedad de dependencia de destino del enlace.</param>
    [Sugar]
    public static void Bind(this FrameworkElement obj, DependencyProperty targetDp, INotifyPropertyChanged source, DependencyProperty sourceDp)
    {
        obj.Bind(targetDp, (object)source, sourceDp, BindingMode.TwoWay);
    }

    /// <summary>
    /// Enlaza una propiedad de dependencia de un <see cref="INotifyPropertyChanged" /> a un
    /// <see cref="FrameworkElement" />.
    /// </summary>
    /// <param name="obj">Objeto destino del enlace</param>
    /// <param name="targetDp">Propiedad de dependencia de destino del enlace.</param>
    /// <param name="source">Orígen del enlace.</param>
    /// <param name="sourceDp">Propiedad de dependencia de destino del enlace.</param>
    /// <param name="mode">Modo del enlace.</param>
    [Sugar]
    public static void Bind(this FrameworkElement obj, DependencyProperty targetDp, INotifyPropertyChanged source, DependencyProperty sourceDp, BindingMode mode)
    {
        obj.Bind(targetDp, (object)source, sourceDp, mode);
    }

    /// <summary>
    /// Enlaza una propiedad de dependencia de un <see cref="object" /> a un <see cref="FrameworkElement" />.
    /// </summary>
    /// <param name="obj">Objeto destino del enlace</param>
    /// <param name="targetDp">Propiedad de dependencia de destino del enlace.</param>
    /// <param name="source">Orígen del enlace.</param>
    /// <param name="sourceDp">Propiedad de dependencia de destino del enlace.</param>
    /// <param name="mode">Modo del enlace.</param>
    [Sugar]
    public static void Bind(this FrameworkElement obj, DependencyProperty targetDp, object source, DependencyProperty sourceDp, BindingMode mode)
    {
        obj.SetBinding(targetDp, new Binding
        {
            Path = new PropertyPath(sourceDp),
            Mode = mode,
            Source = source
        });
    }

    /// <summary>
    /// Crea un mapa de bits de un <see cref="FrameworkElement" />.
    /// </summary>
    /// <param name="f">
    /// <see cref="FrameworkElement" /> a renderizar.
    /// </param>
    /// <returns>
    /// Un objeto <see cref="RenderTargetBitmap" /> que contiene una imagen
    /// renderizada de <paramref name="f" />.
    /// </returns>
    public static RenderTargetBitmap Render(this FrameworkElement f)
    {
        return f.Render(new Size((int)f.ActualWidth, (int)f.ActualHeight), (int)Windows.GetDpi().Width);
    }

    /// <summary>
    /// Crea un mapa de bits de un <see cref="FrameworkElement" />.
    /// </summary>
    /// <param name="f">
    /// <see cref="FrameworkElement" /> a renderizar.
    /// </param>
    /// <param name="dpi">
    /// Valor de puntos por pulgada a utilizar para crear el mapa de bits.
    /// </param>
    /// <returns>
    /// Un objeto <see cref="RenderTargetBitmap" /> que contiene una imagen
    /// renderizada de <paramref name="f" />.
    /// </returns>
    public static RenderTargetBitmap Render(this FrameworkElement f, int dpi)
    {
        return f.Render(new Size((int)f.ActualWidth, (int)f.ActualHeight), dpi);
    }

    /// <summary>
    /// Crea un mapa de bits de un <see cref="FrameworkElement" />
    /// estableciendo el tamaño en el cual se dibujará el control, por lo
    /// que no necesita haberse mostrado en la interfaz de usuario.
    /// </summary>
    /// <param name="f">
    /// <see cref="FrameworkElement" /> a renderizar.
    /// </param>
    /// <param name="inSize">
    /// Tamaño del control a renderizar.
    /// </param>
    /// <param name="outSize">
    /// Tamaño del canvas en donde se renderizará el control.
    /// </param>
    /// <param name="dpi">
    /// Valor de puntos por pulgada a utilizar para crear el mapa de bits.
    /// </param>
    /// <returns>
    /// Un objeto <see cref="RenderTargetBitmap" /> que contiene una imagen
    /// renderizada de <paramref name="f" />.
    /// </returns>
    public static RenderTargetBitmap Render(this FrameworkElement f, Size inSize, Size outSize, int dpi)
    {
        f.Measure(inSize);
        f.Arrange(new Rect(inSize));
        return f.Render(outSize, dpi);
    }
}
