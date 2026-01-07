/*
SizeVisibilityConverter.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
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

using System.Globalization;
using System.Windows;
using System.Windows.Data;
using T = TheXDS.MCART.Types;

namespace TheXDS.MCART.ValueConverters.Base;

/// <summary>
/// Clase base para convertidores de valores que tomen un valor <see cref="T.Size" /> para determinar un
/// valor de tipo <see cref="Visibility" /> basado en un umbral.
/// </summary>
public abstract class SizeVisibilityConverter : IValueConverter
{
    private record class ConvertParser(Type TargetType, Func<object, T.Size> ConvertCallback)
    {
        public static ConvertParser Create<TResult>(Func<TResult, T.Size> convertCallback)
        {
            return new ConvertParser(typeof(TResult), obj => convertCallback((TResult)obj));
        }
    }

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
    /// Convierte un <see cref="T.Size" /> a un <see cref="Visibility" /> basado en un valor de umbral.
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
        ConvertParser[] parsers = {
            ConvertParser.Create<string>(str => 
            {
                try
                {
                    return T.Size.Parse(str);
                }
                catch (Exception e)
                {
                    throw new ArgumentException(string.Empty, nameof(parameter), e);
                }
            }),
            ConvertParser.Create<T.Size>(p => p),
            ConvertParser.Create<double>(p => new T.Size(p, p)),
            ConvertParser.Create<int>(p => new T.Size(p, p)),
        };

        if (!targetType.IsAssignableFrom(typeof(Visibility))) throw new InvalidCastException();
        T.Size size = (parsers.FirstOrDefault(p => p.TargetType == parameter.GetType()) ?? throw new ArgumentException(string.Empty, nameof(parameter))).ConvertCallback(parameter);
        return value switch
        {
            FrameworkElement f => f.ActualWidth < size.Width && f.ActualHeight < size.Height ? _below : _above,
            T.Size sz => sz.Width < size.Width && sz.Height < size.Height ? _below : _above,
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
    /// Un valor de <see cref="T.Size" /> inferido a partir del valor de entrada.
    /// </returns>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value is Visibility v && v == _above ? parameter : new T.Size();
    }
}
