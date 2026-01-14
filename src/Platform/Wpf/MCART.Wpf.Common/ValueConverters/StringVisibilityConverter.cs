/*
StringVisibilityConverter.cs

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
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.ValueConverters;

/// <summary>
/// Converts a <see cref="string" /> to a <see cref="Visibility" />.
/// </summary>
public sealed class StringVisibilityConverter : IValueConverter
{
    /// <summary>
    /// Gets or sets the <see cref="Visibility" /> to return when the
    /// <see cref="string" /> is empty.
    /// </summary>
    public Visibility Empty { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="Visibility" /> to return when the
    /// <see cref="string" /> is not empty.
    /// </summary>
    public Visibility NotEmpty { get; set; }

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="StringVisibilityConverter" /> class.
    /// </summary>
    /// <param name="empty">
    /// <see cref="Visibility" /> to return when the string is empty.
    /// </param>
    public StringVisibilityConverter(Visibility empty) : this(empty, Visibility.Visible)
    {
    }

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="StringVisibilityConverter" /> class.
    /// </summary>
    /// <param name="empty">
    /// <see cref="Visibility" /> to return when the string is empty.
    /// </param>
    /// <param name="notEmpty">
    /// <see cref="Visibility" /> to return when the string is not empty.
    /// </param>
    public StringVisibilityConverter(Visibility empty, Visibility notEmpty)
    {
        NotEmpty = notEmpty;
        Empty = empty;
    }

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="StringVisibilityConverter" /> class.
    /// </summary>
    /// <param name="inverted">
    /// Whether to invert the visibility values.
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
    /// Initializes a new instance of the
    /// <see cref="StringVisibilityConverter" /> class.
    /// </summary>
    public StringVisibilityConverter()
    {
        NotEmpty = Visibility.Visible;
        Empty = Visibility.Collapsed;
    }

    /// <summary>
    /// Gets a <see cref="Visibility" /> based on the value.
    /// </summary>
    /// <param name="value">Object to convert.</param>
    /// <param name="targetType">Type of the target.</param>
    /// <param name="parameter">
    /// Custom parameters for this <see cref="IValueConverter" />.
    /// </param>
    /// <param name="culture">
    /// <see cref="CultureInfo" /> to use for conversion.
    /// </param>
    /// <returns>
    /// <see cref="Empty" /> if <paramref name="value" /> is an empty string,
    /// otherwise <see cref="NotEmpty" />.
    /// </returns>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return ((string)value)?.IsEmpty() ?? false ? Empty : NotEmpty;
    }

    /// <summary>
    /// Infers a value based on the provided <see cref="Visibility" />.
    /// </summary>
    /// <param name="value">Object to convert.</param>
    /// <param name="targetType">Type of the target.</param>
    /// <param name="parameter">
    /// Custom parameters for this <see cref="IValueConverter" />.
    /// </param>
    /// <param name="culture">
    /// <see cref="CultureInfo" /> to use for conversion.
    /// </param>
    /// <returns>
    /// <c>value.ToString()</c> if <paramref name="value" /> equals
    /// <see cref="NotEmpty" />, otherwise <see cref="F:System.String.Empty" />.
    /// </returns>
    public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Visibility v)
            return v == NotEmpty ? value.ToString() : string.Empty;
        return null;
    }
}
