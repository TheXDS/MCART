/*
AnyContentVisibilityConverter.cs

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

using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using System;
using System.Linq;
using System.Windows;

namespace TheXDS.MCART.ValueConverters;

/// <summary>
/// Para un <see cref="ContentControl" /> o un <see cref="Panel" />,
/// obtiene un valor <see cref="Visibility.Visible" /> si al menos un
/// control hijo directo es visible.
/// </summary>
public sealed class AnyContentVisibilityConverter : IValueConverter
{
    /// <summary>
    /// Obtiene un valor <see cref="Visibility.Visible" /> si al menos un
    /// control hijo directo es visible.
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
    /// Un nuevo <see cref="Visibility.Visible" /> si al
    /// menos un hijo directo del control es visible,
    /// <see cref="Visibility.Collapsed" /> en caso contrario.
    /// </returns>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value switch
        {
            Decorator decorator => decorator.Child?.Visibility ?? Visibility.Collapsed,
            ContentControl contentControl => (contentControl.Content as FrameworkElement)?.Visibility ??
                                             Visibility.Collapsed,
            Panel panel => panel.Children.Cast<object?>()
                .Any(j => j is FrameworkElement { Visibility: Visibility.Visible })
                ? Visibility.Visible
                : Visibility.Collapsed,
            _ => Visibility.Visible
        };
    }

    /// <summary>Convierte un valor.</summary>
    /// <param name="value">
    /// Valor generado por el destino de enlace.
    /// </param>
    /// <param name="targetType">Tipo al que se va a convertir.</param>
    /// <param name="parameter">
    /// Parámetro de convertidor que se va a usar.
    /// </param>
    /// <param name="culture">
    /// Referencia cultural que se va a usar en el convertidor.
    /// </param>
    /// <returns>
    /// Valor convertido.
    /// Si el método devuelve <see langword="null" />, se usa el valor nulo válido.
    /// </returns>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new InvalidOperationException();
    }
}
