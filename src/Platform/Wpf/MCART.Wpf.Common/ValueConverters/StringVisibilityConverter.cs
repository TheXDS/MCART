/*
StringVisibilityConverter.cs

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
using System.Windows;
using System.Windows.Data;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.ValueConverters;

/// <summary>
/// Convierte un <see cref="string" /> a un <see cref="Visibility" />.
/// </summary>
public sealed class StringVisibilityConverter : IValueConverter
{
    /// <summary>
    /// Obtiene o establece el <see cref="Visibility" /> a devolver cuando
    /// el <see cref="string" /> esté vacío.
    /// </summary>
    public Visibility Empty { get; set; }

    /// <summary>
    /// Obtiene o establece el <see cref="Visibility" /> a devolver cuando
    /// el <see cref="string" /> no esté vacío.
    /// </summary>
    public Visibility NotEmpty { get; set; }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="StringVisibilityConverter" />.
    /// </summary>
    /// <param name="empty">
    /// <see cref="Visibility" /> a devolver cuando la cadena esté vacía.
    /// </param>
    public StringVisibilityConverter(Visibility empty) : this(empty, Visibility.Visible)
    {
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="StringVisibilityConverter" />.
    /// </summary>
    /// <param name="empty">
    /// <see cref="Visibility" /> a devolver cuando la cadena esté vacía.
    /// </param>
    /// <param name="notEmpty">
    /// <see cref="Visibility" /> a devolver cuando la cadena no esté vacía.
    /// </param>
    public StringVisibilityConverter(Visibility empty, Visibility notEmpty)
    {
        NotEmpty = notEmpty;
        Empty = empty;
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="StringVisibilityConverter" />.
    /// </summary>
    /// <param name="inverted">
    /// Inversión de valores de <see cref="Visibility" />.
    /// </param>
    public StringVisibilityConverter(bool inverted)
    {
        if (inverted)
        {
            NotEmpty = Visibility.Visible;
            Empty = Visibility.Collapsed;
        }
        else
        {
            NotEmpty = Visibility.Collapsed;
            Empty = Visibility.Visible;
        }
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="StringVisibilityConverter" />.
    /// </summary>
    public StringVisibilityConverter()
    {
        NotEmpty = Visibility.Visible;
        Empty = Visibility.Collapsed;
    }

    /// <summary>
    /// Obtiene un <see cref="Visibility" /> a partir del valor.
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
    /// <see cref="Empty" /> si <paramref name="value" /> es una
    /// cadena
    /// vacía, <see cref="NotEmpty" /> en caso contrario.
    /// </returns>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return ((string)value)?.IsEmpty() ?? false ? Empty : NotEmpty;
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
    /// <c>value.ToString()</c> si <paramref name="value" /> no es una
    /// cadena vacía, <see cref="F:System.String.Empty" /> en caso contrario.
    /// </returns>
    public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Visibility v)
            return v == NotEmpty ? value.ToString() : string.Empty;
        return null;
    }
}
