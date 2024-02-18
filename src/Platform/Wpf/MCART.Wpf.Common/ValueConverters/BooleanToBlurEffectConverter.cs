/*
BooleanToBlurEffectConverter.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2024 César Andrés Morgan

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
using System.Windows.Data;
using System.Windows.Media.Effects;
using TheXDS.MCART.Resources;

namespace TheXDS.MCART.ValueConverters;

/// <summary>
/// Genera un efecto de desenfoque basado en una condición booleana.
/// </summary>
public sealed class BooleanToBlurEffectConverter : IValueConverter
{
    /// <summary>
    /// Genera un efecto de desenfoque basado en una condición
    /// booleana.
    /// </summary>
    /// <param name="value">
    /// Valor generado por el origen del enlace.
    /// </param>
    /// <param name="targetType">
    /// El tipo de la propiedad del destino del enlace.
    /// </param>
    /// <param name="parameter">
    /// Parámetro de convertidor que se va a usar.
    /// </param>
    /// <param name="culture">
    /// Referencia cultural que se va a usar en el convertidor.
    /// </param>
    /// <returns>
    /// Un objeto <see cref="BlurEffect"/> que puede aplicarse a un
    /// <see cref="System.Windows.UIElement"/>, o <see langword="null"/> si
    /// <paramref name="parameter"/> no es un valor de tipo
    /// <see cref="bool"/>.
    /// </returns>
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        double TryConvert()
        {
            try
            {
                return (double)System.Convert.ChangeType(parameter, typeof(double));
            }
            catch
            {
                return 5.0;
            }
        }

        return value is bool b && b ? new BlurEffect
        {
            Radius = parameter switch
            {
                null => 5.0,
                string s => double.TryParse(s, out double r) ? r : throw Errors.InvalidValue(nameof(parameter), parameter),
                _ => TryConvert()
            }
        } : null;
    }

    /// <summary>
    /// Genera un efecto de desenfoque basado en una condición
    /// booleana.
    /// </summary>
    /// <param name="value">
    /// Valor generado por el origen del enlace.
    /// </param>
    /// <param name="targetType">
    /// El tipo de la propiedad del destino del enlace.
    /// </param>
    /// <param name="parameter">
    /// Parámetro de convertidor que se va a usar.
    /// </param>
    /// <param name="culture">
    /// Referencia cultural que se va a usar en el convertidor.
    /// </param>
    /// <returns>
    /// Un objeto <see cref="BlurEffect"/> que puede aplicarse a un
    /// <see cref="System.Windows.UIElement"/>.
    /// </returns>
    public object Convert(bool value, Type targetType, object? parameter, CultureInfo culture)
    {
        return Convert((object)value, targetType, parameter, culture)!;
    }

    /// <summary>
    /// Implementa <see cref="IValueConverter.ConvertBack" />.
    /// </summary>
    /// <param name="value">Objeto a convertir.</param>
    /// <returns>
    /// <see langword="true" /> si el valor es un <see cref="BlurEffect" />,
    /// <see langword="false" /> en caso contrario.
    /// </returns>
    public bool ConvertBack(object value)
    {
        return value is BlurEffect;
    }

    object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return ConvertBack(value);
    }
}
