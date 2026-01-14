/*
ToStringConverter.cs

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
/// Converts a value to its string representation.
/// </summary>
public sealed partial class ToStringConverter
{
    /// <summary>
    /// Converts any object to a <see cref="string"/>.
    /// </summary>
    /// <param name="value">
    /// Object to convert.
    /// </param>
    /// <param name="targetType">
    /// Target type.
    /// </param>
    /// <param name="parameter">
    /// Custom parameters used for the conversion.
    /// </param>
    /// <param name="culture">
    /// <see cref="CultureInfo"/> used for the conversion.
    /// </param>
    /// <returns>
    /// A <see cref="string"/> that represents the object.
    /// </returns>
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value?.ToString();
    }

    /// <summary>
    /// Attempts to convert a <see cref="string"/> to an object of the
    /// specified target type.
    /// </summary>
    /// <param name="value">
    /// Object to convert.
    /// </param>
    /// <param name="targetType">
    /// Target type.
    /// </param>
    /// <param name="parameter">
    /// Custom parameters used for the conversion.
    /// </param>
    /// <param name="culture">
    /// <see cref="CultureInfo"/> used for the conversion.
    /// </param>
    /// <returns>
    /// If the conversion from string succeeds, the object is returned; otherwise
    /// <see langword="null"/> is returned.
    /// </returns>
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        try
        {
            return targetType?.GetMethod("Parse", [typeof(string)])?.Invoke(null, [value]);
        }
        catch
        {
            return null;
        }
    }
}
