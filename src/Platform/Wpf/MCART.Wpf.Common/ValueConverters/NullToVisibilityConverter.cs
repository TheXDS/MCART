/*
NullToVisibilityConverter.cs

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
/// Returns <see cref="Visibility.Visible" /> if the element to convert is
/// <see langword="null" />; otherwise returns
/// <see cref="Visibility.Collapsed" />.
/// </summary>
public sealed class NullToVisibilityConverter : IValueConverter
{
    /// <summary>
    /// Gets a <see cref="Visibility"/> from the value.
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
    /// <see cref="Visibility.Visible"/> if the element is
    /// <see langword="null"/>, <see cref="Visibility.Collapsed"/> otherwise.
    /// </returns>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value is null ? Visibility.Visible : Visibility.Collapsed;
    }

    /// <summary>
    /// Implements <see cref="IValueConverter.ConvertBack"/>.
    /// </summary>
    /// <param name="value">Object to convert.</param>
    /// <param name="targetType">Target type.</param>
    /// <param name="parameter">
    /// Custom parameters for this <see cref="IValueConverter"/>.
    /// </param>
    /// <param name="culture">
    /// <see cref="CultureInfo"/> to use for conversion.
    /// </param>
    /// <exception cref="InvalidCastException">
    /// Thrown if <paramref name="value"/> is not a
    /// <see cref="Visibility"/>.
    /// </exception>
    /// <exception cref="TypeLoadException">
    /// Thrown if <paramref name="targetType"/> is not a class or struct that can
    /// be instantiated with a parameterless constructor.
    /// </exception>
    /// <returns>
    /// A new instance of type <paramref name="targetType"/> if
    /// <paramref name="value"/> is <see cref="Visibility.Visible"/>;
    /// otherwise <see langword="null"/>.
    /// </returns>
    public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Visibility b) return b == Visibility.Visible ? null : targetType.New();
        throw new InvalidCastException();
    }
}
