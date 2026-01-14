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
/// Converts an <see cref="int"/> value to <see cref="Visibility"/>.
/// </summary>
/// <param name="withItems">
/// The <see cref="Visibility"/> to use when the count is greater than
/// zero.
/// </param>
/// <param name="withoutItems">
/// The <see cref="Visibility"/> to use when the count is zero.
/// </param>
public sealed class CountVisibilityConverter(Visibility withItems, Visibility withoutItems) : IValueConverter
{
    /// <summary>
    /// Gets or sets the <see cref="Visibility"/> returned when the item
    /// count is greater than zero.
    /// </summary>
    public Visibility WithItems { get; set; } = withItems;

    /// <summary>
    /// Gets or sets the <see cref="Visibility"/> returned when the item
    /// count is zero.
    /// </summary>
    public Visibility WithoutItems { get; set; } = withoutItems;

    /// <summary>
    /// Initializes a new instance of the <see cref="CountVisibilityConverter"/>
    /// class.
    /// </summary>
    public CountVisibilityConverter() : this(Visibility.Visible, Visibility.Collapsed)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CountVisibilityConverter"/>
    /// class.
    /// </summary>
    /// <param name="withItems">
    /// The <see cref="Visibility"/> to use when the count is greater than
    /// zero.
    /// </param>
    public CountVisibilityConverter(Visibility withItems = Visibility.Visible) : this(withItems, Visibility.Collapsed)
    {
    }

    /// <summary>
    /// Gets a <see cref="Visibility"/> from a count value.
    /// </summary>
    /// <param name="value">Object to convert.</param>
    /// <param name="targetType">Target type.</param>
    /// <param name="parameter">
    /// Custom parameters for this <see cref="IValueConverter"/>.
    /// </param>
    /// <param name="culture">
    /// <see cref="CultureInfo"/> to use for conversion.
    /// </param>
    /// <returns>
    /// <see cref="WithItems"/> if <paramref name="value"/> is greater than
    /// zero, <see cref="WithoutItems"/> otherwise.
    /// </returns>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is int v) return v > 0 ? WithItems : WithoutItems;
        throw new InvalidCastException();
    }

    /// <summary>
    /// Infers a count of items based on the provided <see cref="Visibility"/>.
    /// </summary>
    /// <param name="value">Object to convert.</param>
    /// <param name="targetType">Target type.</param>
    /// <param name="parameter">
    /// Custom parameters for this <see cref="IValueConverter"/>.
    /// </param>
    /// <param name="culture">
    /// <see cref="CultureInfo"/> to use for conversion.
    /// </param>
    /// <returns>
    /// <c>1</c> if <paramref name="value"/> equals <see cref="WithItems"/>,
    /// <c>0</c> otherwise.
    /// </returns>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Visibility v) return v == WithItems ? 1 : 0;
        throw new InvalidCastException();
    }
}
