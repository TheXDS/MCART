/*
NumberToBooleanConverter.cs

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
/// Converts a number directly to <see cref="bool" />.
/// </summary>
public sealed partial class NumberToBooleanConverter
{
    /// <summary>
    /// Converts an <see cref="int" /> to a <see cref="bool" />.
    /// </summary>
    /// <param name="value">Object to convert.</param>
    /// <param name="targetType">Target type.</param>
    /// <param name="parameter">
    /// Custom parameters used for the conversion.
    /// </param>
    /// <param name="culture"><see cref="CultureInfo" /> used for the conversion.</param>
    /// <returns>
    /// <see langword="true" /> if <paramref name="value" /> is non‑zero, 
    /// <see langword="false" /> otherwise.
    /// </returns>
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value is int i && i != 0;
    }

    /// <summary>
    /// Infers an <see cref="int" /> value from a <see cref="bool" />.
    /// </summary>
    /// <param name="value">Object to convert.</param>
    /// <param name="targetType">Target type.</param>
    /// <param name="parameter">
    /// Custom parameters used for the conversion.
    /// </param>
    /// <param name="culture"><see cref="CultureInfo" /> used for the conversion.</param>
    /// <returns>
    /// <c>-1</c> if <paramref name="value" /> is <see langword="true" />, 
    /// <c>0</c> otherwise.
    /// </returns>
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value is bool b ? (object)(b ? -1 : 0) : null;
    }
}
