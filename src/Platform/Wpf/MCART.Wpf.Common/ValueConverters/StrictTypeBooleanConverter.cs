/*
StrictTypeBooleanConverter.cs

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
using System.Windows;
using System.Windows.Data;
using TheXDS.MCART.Resources;

namespace TheXDS.MCART.ValueConverters;

/// <summary>
/// Determina si un objeto es una instancia del tipo provisto, y devuelve
/// <see langword="true" /> si lo es.
/// </summary>
public sealed class StrictTypeBooleanConverter : IValueConverter
{
    /// <summary>
    /// Determina si un objeto es una instancia del tipo provisto.
    /// </summary>
    /// <param name="value">
    /// Valor generado por el origen de enlace.
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
    /// <see langword="true" /> si el objeto es una instancia del
    /// tipo provisto, <see langword="false" /> en caso contrario.
    /// </returns>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (parameter is not Type t) throw Errors.InvalidValue(nameof(parameter));
        return t.IsInstanceOfType(value);
    }

    /// <summary>
    /// Implementa <see cref="IValueConverter.ConvertBack" />.
    /// </summary>
    /// <param name="value">Objeto a convertir.</param>
    /// <param name="targetType">Tipo del destino.</param>
    /// <param name="parameter">
    /// Parámetros personalizados para este <see cref="IValueConverter" />.
    /// </param>
    /// <param name="culture">
    /// <see cref="CultureInfo" /> a utilizar para la conversión.
    /// </param>
    /// <exception cref="InvalidCastException">
    /// Se produce si <paramref name="value" /> no es un <see cref="Visibility" />.
    /// </exception>
    /// <exception cref="TypeLoadException">
    /// Se produce si <paramref name="targetType" /> no es una clase o estructura instanciable con un constructor sin
    /// parámetros.
    /// </exception>
    /// <returns>
    /// Este método siempre genera un <see cref="InvalidOperationException" />.
    /// </returns>
    /// <exception cref="InvalidOperationException">Este método siempre genera esta excepción al ser llamado.</exception>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new InvalidOperationException();
    }
}
