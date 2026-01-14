/*
BooleanToObjectConverter.cs

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
/// Obtains an object only when the original value to convert is equal to
/// <see langword="true"/>.
/// </summary>
public sealed partial class BooleanToObjectConverter
{
    /// <summary>
    /// Returns an object according to a boolean value.
    /// </summary>
    /// <param name="value">
    /// Boolean value to evaluate.
    /// </param>
    /// <param name="targetType">
    /// Target type of the conversion.
    /// </param>
    /// <param name="parameter">
    /// Object to return if <c><paramref name="value"/> == <see langword="true"/></c>.
    /// </param>
    /// <param name="culture">
    /// Culture used during the conversion.
    /// </param>
    /// <returns>
    /// <paramref name="parameter"/> if <c><paramref name="value"/> == <see langword="true"/></c>,
    /// <see langword="null"/> otherwise.
    /// </returns>
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value is bool b && b && targetType.IsInstanceOfType(parameter) ? parameter : null;
    }

    /// <summary>
    /// Returns <see langword="true"/> if an object is not
    /// <see langword="null"/>.
    /// </summary>
    /// <param name="value">
    /// Object to check.
    /// </param>
    /// <param name="targetType">
    /// Target type.
    /// </param>
    /// <param name="parameter">
    /// Conversion parameter.
    /// </param>
    /// <param name="culture">
    /// Culture used during the conversion.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the object is not <see langword="null"/>,
    /// <see langword="false"/> otherwise.
    /// </returns>
    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value is not null;
    }
}
