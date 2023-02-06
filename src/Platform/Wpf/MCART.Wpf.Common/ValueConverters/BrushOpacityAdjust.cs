/*
BrushOpacityAdjust.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2023 César Andrés Morgan

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

using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using TheXDS.MCART.Helpers;
using TheXDS.MCART.Resources;

namespace TheXDS.MCART.ValueConverters;

/// <summary>
/// Permite compartir un recurso de <see cref="Brush" /> entre controles,
/// ajustando la opacidad del enlace de datos.
/// </summary>
public sealed class BrushOpacityAdjust : IValueConverter
{
    /// <summary>
    /// Aplica la nueva opacidad al <see cref="Brush" />.
    /// </summary>
    /// <param name="value">Objeto a convertir.</param>
    /// <param name="targetType">Tipo del destino.</param>
    /// <param name="parameter">
    /// Parámetros personalizados para este <see cref="IValueConverter" />.
    /// </param>
    /// <param name="culture">
    /// <see cref="CultureInfo" /> a utilizar para la conversión.
    /// </param>
    /// <returns>
    /// Un nuevo <see cref="Brush" /> con la opacidad establecida en este
    /// <see cref="BrushOpacityAdjust" />.
    /// </returns>
    public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not Brush brush) return null;
        double opacity = (double)System.Convert.ChangeType(parameter, typeof(double));
        if (!opacity.IsBetween(0.0, 1.0)) throw Errors.ValueOutOfRange(nameof(opacity), 0.0, 1.0);
        Brush? b = brush.Clone();
        b.Opacity = opacity;
        return b;
    }

    /// <summary>
    /// Devuelve un <see cref="Brush" /> con 100% opacidad.
    /// </summary>
    /// <param name="value">Objeto a convertir.</param>
    /// <param name="targetType">Tipo del destino.</param>
    /// <param name="parameter">
    /// Parámetros personalizados para este <see cref="IValueConverter" />.
    /// </param>
    /// <param name="culture">
    /// <see cref="CultureInfo" /> a utilizar para la conversión.
    /// </param>
    /// <returns>
    /// Un nuevo <see cref="Brush" /> con la opacidad al 100%.
    /// </returns>
    public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not Brush brush) return null;
        Brush? b = brush.Clone();
        b.Opacity = 1;
        return b;
    }
}
