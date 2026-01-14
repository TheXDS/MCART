/*
DoubleMarginConverter.cs

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
/// Converts a <see cref="double"/> to a <see cref="Thickness"/>.
/// </summary>
public sealed class DoubleMarginConverter : IValueConverter
{
    /// <summary>
    /// Converts a <see cref="double"/> to a <see cref="Thickness"/>.
    /// </summary>
    /// <param name="value">The object to convert.</param>
    /// <param name="targetType">The target type.</param>
    /// <param name="parameter">
    /// An optional value‑transforming function. Must be of type
    /// <see cref="Func{T,TResult}"/> where the argument and return
    /// types are both <see cref="double"/>.
    /// </param>
    /// <param name="culture">
    /// The <see cref="CultureInfo"/> used for conversion.
    /// </param>
    /// <returns>
    /// A uniform <see cref="Thickness"/> whose sides are equal to the
    /// supplied value.
    /// </returns>
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        Func<double, double>? f = parameter as Func<double, double>;
        return value is double v ? (object)new Thickness(f?.Invoke(v) ?? v) : null;
    }

    /// <summary>
    /// Reverts the conversion performed by this converter.
    /// </summary>
    /// <param name="value">The object to convert.</param>
    /// <param name="targetType">The target type.</param>
    /// <param name="parameter">
    /// Optional value‑transforming function.
    /// </param>
    /// <param name="culture">
    /// The <see cref="CultureInfo"/> used for conversion.
    /// </param>
    /// <returns>
    /// A <see cref="double"/> whose value is the average of the
    /// <see cref="Thickness"/> sides.
    /// </returns>
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value is Thickness v ? (object)((v.Top + v.Bottom + v.Left + v.Right) / 4.0) : null;
    }
}
