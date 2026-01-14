/*
LabeledDoubleConverter.cs

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

namespace TheXDS.MCART.ValueConverters;

/// <summary>
/// Converts a <see cref="double"/> to a <see cref="string"/>, optionally
/// displaying a label if the value is less than zero.
/// </summary>
public sealed partial class LabeledDoubleConverter
{
    /// <summary>
    /// Converts a <see cref="double"/> to a <see cref="string"/>,
    /// optionally showing a label when the value is negative.
    /// </summary>
    /// <param name="value">Object to convert.</param>
    /// <param name="targetType">Target type.</param>
    /// <param name="parameter">
    /// Label to display if the value is less than zero.
    /// </param>
    /// <param name="culture">
    /// <see cref="CultureInfo"/> to use during the conversion.
    /// </param>
    /// <returns>
    /// The value of <paramref name="value"/> represented as a string.
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
    /// Reverses the conversion performed by this object.
    /// </summary>
    /// <param name="value">Object to convert.</param>
    /// <param name="targetType">Target type.</param>
    /// <param name="parameter">
    /// Optional value transformation function.
    /// </param>
    /// <param name="culture">
    /// <see cref="CultureInfo"/> to use during the conversion.
    /// </param>
    /// <returns>
    /// A <see cref="double"/> whose value is equivalent to the provided
    /// string.
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
