/*
SizeVisibilityConverter.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2022 César Andrés Morgan

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

namespace TheXDS.MCART.ValueConverters.Base;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

/// <summary>
/// Clase base para convertidores de valores que tomen un valor <see cref="Size" /> para determinar un
/// valor de tipo <see cref="Visibility" /> basado en un umbral.
/// </summary>
public abstract class SizeVisibilityConverter : IValueConverter
{
    private readonly Visibility _above;
    private readonly Visibility _below;

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="SizeVisibilityConverter" />.
    /// </summary>
    /// <param name="below">Valor a devolver por debajo del umbral.</param>
    /// <param name="above">Valor a devolver por encima del umbral.</param>
    protected SizeVisibilityConverter(Visibility below, Visibility above)
    {
        _below = below;
        _above = above;
    }

    /// <summary>
    /// Convierte un <see cref="Size" /> a un <see cref="Visibility" /> basado en un valor de umbral.
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
    /// El valor de <see cref="Visibility" /> calculado a partir del tamaño especificado.
    /// </returns>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (!targetType.IsAssignableFrom(typeof(Visibility))) throw new InvalidCastException();
        Size size;
        switch (parameter)
        {
            case string str:
                try
                {
                    size = Size.Parse(str);
                }
                catch (Exception e)
                {
                    throw new ArgumentException(string.Empty, nameof(parameter), e);
                }
                break;
            case Size sz:
                size = sz;
                break;
            case double d:
                size = new Size(d, d);
                break;
            case int d:
                size = new Size(d, d);
                break;
            default:
                throw new ArgumentException(string.Empty, nameof(parameter));
        }

        return value switch
        {
            FrameworkElement f => f.ActualWidth < size.Width && f.ActualHeight < size.Height ? _below : _above,
            Size sz => sz.Width < size.Width && sz.Height < size.Height ? _below : _above,
            _ => _above,
        };
    }

    /// <summary>
    /// Infiere un valor basado en el <see cref="Visibility" /> provisto.
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
    /// Un valor de <see cref="Size" /> inferido a partir del valor de entrada.
    /// </returns>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value is Visibility v && v == _above ? parameter : new Size();
    }
}
