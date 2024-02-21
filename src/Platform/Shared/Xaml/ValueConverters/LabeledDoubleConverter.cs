/*
LabeledDoubleConverter.cs

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

namespace TheXDS.MCART.ValueConverters;

/// <summary>
/// Convierte un <see cref="double" /> en un <see cref="string" />,
/// opcionalmente mostrando una etiqueta si el valor es inferior a cero.
/// </summary>
public sealed partial class LabeledDoubleConverter
{
    /// <summary>
    /// Convierte un <see cref="double" /> en un <see cref="string" />,
    /// opcionalmente mostrando una etiqueta si el valor es inferior a cero.
    /// </summary>
    /// <param name="value">Objeto a convertir.</param>
    /// <param name="targetType">Tipo del destino.</param>
    /// <param name="parameter">
    /// Etiqueta a mostrar en caso que el valor sea inferior a cero.
    /// </param>
    /// <param name="culture">
    /// <see cref="CultureInfo" /> a utilizar para la conversión.
    /// </param>
    /// <returns>
    /// El valor de <paramref name="value"/> como una cadena.
    /// </returns>
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is double v)
        {
            parameter ??= v.ToString(CultureInfo.InvariantCulture);
            if (parameter is string label)
                return v > 0 ? v.ToString(CultureInfo.InvariantCulture) : label;
        }
        throw new InvalidCastException();
    }

    /// <summary>
    /// Revierte la conversión realizada por este objeto.
    /// </summary>
    /// <param name="value">Objeto a convertir.</param>
    /// <param name="targetType">Tipo del destino.</param>
    /// <param name="parameter">
    /// Función opcional de transformación de valor.
    /// </param>
    /// <param name="culture">
    /// <see cref="CultureInfo" /> a utilizar para la conversión.
    /// </param>
    /// <returns>
    /// Un <see cref="double" /> cuyo valor es equivalente a la cadena
    /// proporcionada.
    /// </returns>
    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value switch
        {
            double v => v,
            string s => double.TryParse(s, out double r) ? r : 0.0,
            _ => throw new InvalidCastException(),
        };
    }
}
