/*
FloatVisibilityConverter.cs

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
/// Converts a <see cref="float"/> to a <see cref="Visibility"/>.
/// </summary>
public sealed class FloatVisibilityConverter : IValueConverter
{
    /// <summary>
    /// Gets or sets the <see cref="Visibility"/> returned when the value is
    /// greater than zero.
    /// </summary>
    public Visibility Positives { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="Visibility"/> returned when the value is
    /// less than or equal to zero.
    /// </summary>
    public Visibility ZeroOrNegatives { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="FloatVisibilityConverter"/> 
    /// class.
    /// </summary>
    public FloatVisibilityConverter() : this(Visibility.Visible, Visibility.Collapsed)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FloatVisibilityConverter"/> 
    /// class.
    /// </summary>
    /// <param name="positives">
    /// The <see cref="Visibility"/> to use for positive values.
    /// </param>
    public FloatVisibilityConverter(Visibility positives) : this(positives, Visibility.Visible)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FloatVisibilityConverter"/> 
    /// class.
    /// </summary>
    /// <param name="positives">
    /// The <see cref="Visibility"/> to use for positive values.
    /// </param>
    /// <param name="zeroOrNegatives">
    /// The <see cref="Visibility"/> to use for zero and negative values.
    /// </param>
    public FloatVisibilityConverter(Visibility positives, Visibility zeroOrNegatives)
    {
        Positives = positives;
        ZeroOrNegatives = zeroOrNegatives;
    }

    /// <summary>
    /// Gets a <see cref="Visibility"/> based on the numeric value.
    /// </summary>
    /// <param name="value">Object to convert.</param>
    /// <param name="targetType">The target type.</param>
    /// <param name="parameter">Custom parameters for this <see cref="IValueConverter"/>.</param>
    /// <param name="culture">The <see cref="CultureInfo"/> used for conversion.</param>
    /// <returns>
    /// The <see cref="Positives"/> value if <paramref name="value"/> is greater
    /// than zero; otherwise the <see cref="ZeroOrNegatives"/> value.
    /// </returns>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is float v) return v > 0.0f ? Positives : ZeroOrNegatives;
        throw new InvalidCastException();
    }

    /// <summary>
    /// Infers a float value based on the supplied <see cref="Visibility"/>.
    /// </summary>
    /// <param name="value">Object to convert.</param>
    /// <param name="targetType">The target type.</param>
    /// <param name="parameter">Custom parameters for this <see cref="IValueConverter"/>.</param>
    /// <param name="culture">The <see cref="CultureInfo"/> used for conversion.</param>
    /// <returns>
    /// <c>1.0f</c> if <paramref name="value"/> equals <see cref="Positives"/>;
    /// otherwise <c>0.0f</c>.
    /// </returns>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Visibility v) return v == Positives ? 1.0f : 0.0f;
        throw new InvalidCastException();
    }
}
