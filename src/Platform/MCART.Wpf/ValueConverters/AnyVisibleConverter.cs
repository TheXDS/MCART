/*
AnyVisibleConverter.cs

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
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace TheXDS.MCART.ValueConverters;

/// <summary>
/// Determina un valor de visibilidad basado en que exista al menos un
/// valor igual a <see cref="Visibility.Visible" />.
/// </summary>
public sealed class AnyVisibleConverter : IMultiValueConverter
{
    /// <summary>
    /// Determina un valor de visibilidad basado en que exista al menos
    /// un valor igual a <see cref="Visibility.Visible" />
    /// </summary>
    /// <param name="values">
    /// Valores generados por los orígenes de enlace.
    /// </param>
    /// <param name="targetType">
    /// El tipo de la propiedad del destino de enlace.
    /// </param>
    /// <param name="parameter">
    /// Parámetro de convertidor que se va a usar.
    /// </param>
    /// <param name="culture">
    /// Referencia cultural que se va a usar en el convertidor.
    /// </param>
    /// <returns>
    /// <see cref="Visibility.Visible" /> si al menos uno de los valores
    /// es <see cref="Visibility.Visible" />,
    /// <see cref="Visibility.Collapsed" /> en caso contrario.
    /// </returns>
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        return values.OfType<Visibility>().Any(p => p == Visibility.Visible)
            ? Visibility.Visible
            : Visibility.Collapsed;
    }

    /// <summary>
    /// Implementa <see cref="IValueConverter.ConvertBack" />.
    /// </summary>
    /// <param name="value">Objeto a convertir.</param>
    /// <param name="targetTypes">Tipo del destino.</param>
    /// <param name="parameter">
    /// Parámetros personalizados para este <see cref="IValueConverter" />.
    /// </param>
    /// <param name="culture">
    /// <see cref="CultureInfo" /> a utilizar para la conversión.
    /// </param>
    /// <returns>
    /// Este método siempre genera un <see cref="InvalidOperationException" />.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Este método siempre genera esta excepción al ser llamado.
    /// </exception>
    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new InvalidOperationException();
    }
}
