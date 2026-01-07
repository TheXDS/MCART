/*
CountVisibilityConverter.cs

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

namespace TheXDS.MCART.ValueConverters;

/// <summary>
/// Convierte un valor <see cref="int" />  a <see cref="Visibility" />
/// </summary>
public sealed class CountVisibilityConverter : IValueConverter
{
    /// <summary>
    /// Obtiene o establece el <see cref="Visibility" /> a devolver en caso
    /// de que la cuenta de elementos sea mayor a cero.
    /// </summary>
    public Visibility WithItems { get; set; }

    /// <summary>
    /// Obtiene o establece el <see cref="Visibility" /> a devolver en caso
    /// de que la cuenta de elementos sea igual a cero.
    /// </summary>
    public Visibility WithoutItems { get; set; }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="CountVisibilityConverter" />.
    /// </summary>
    public CountVisibilityConverter() : this(Visibility.Visible, Visibility.Collapsed)
    {
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="CountVisibilityConverter" />.
    /// </summary>
    /// <param name="withItems">
    /// <see cref="Visibility" /> a utilizar en caso de que la cuenta sea
    /// mayor a cero.
    /// </param>
    public CountVisibilityConverter(Visibility withItems = Visibility.Visible) : this(withItems,
        Visibility.Collapsed)
    {
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="CountVisibilityConverter" />.
    /// </summary>
    /// <param name="withItems">
    /// <see cref="Visibility" /> a utilizar en caso de que la cuenta sea
    /// mayor a cero.
    /// </param>
    /// <param name="withoutItems">
    /// <see cref="Visibility" /> a utilizar en caso de que la cuenta sea
    /// igual a cero.
    /// </param>
    public CountVisibilityConverter(Visibility withItems, Visibility withoutItems)
    {
        WithItems = withItems;
        WithoutItems = withoutItems;
    }

    /// <summary>
    /// Obtiene un <see cref="Visibility" /> a partir de un valor de cuenta.
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
    /// <see cref="WithItems" /> si <paramref name="value" /> es mayor
    /// a
    /// cero, <see cref="WithoutItems" /> en caso contrario.
    /// </returns>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is int v) return v > 0 ? WithItems : WithoutItems;
        throw new InvalidCastException();
    }

    /// <summary>
    /// Infiere una cuenta de elementos basado en el <see cref="Visibility" /> provisto.
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
    /// <c>1</c> si <paramref name="value" /> es igual a
    /// <see cref="WithItems" />, <c>0</c> en caso contrario.
    /// </returns>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Visibility v) return v == WithItems ? 1 : 0;
        throw new InvalidCastException();
    }
}
