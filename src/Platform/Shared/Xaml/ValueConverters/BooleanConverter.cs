/*
BooleanConverter.cs

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
/// Base class for creating boolean value converters.
/// </summary>
/// <typeparam name="T">
/// Type of values to convert. Must be structs or enums.
/// </typeparam>
/// <remarks>
/// Initializes a new instance of the <see cref="BooleanConverter{T}" /> class,
/// configuring the values that correspond to <see langword="true" /> and
/// <see langword="false" />.
/// </remarks>
/// <param name="trueValue">Value equivalent to <see langword="true" />.</param>
/// <param name="falseValue">Value equivalent to <see langword="false" />.</param>
public partial class BooleanConverter<T>(T trueValue, T falseValue = default) where T : struct
{
    /// <summary>
    /// Gets or sets the value that corresponds to <see langword="false" /> in
    /// this <see cref="BooleanConverter{T}" />.
    /// </summary>
    public T False { get; set; } = falseValue;

    /// <summary>
    /// Gets or sets the value that corresponds to <see langword="true" /> in
    /// this <see cref="BooleanConverter{T}" />.
    /// </summary>
    public T True { get; set; } = trueValue;

    /// <summary>
    /// Converts a <see cref="bool" /> to an object.
    /// </summary>
    /// <param name="value">Object to convert.</param>
    /// <param name="targetType">Target type.</param>
    /// <param name="parameter">
    /// Custom parameters for performing the conversion.
    /// </param>
    /// <param name="culture">
    /// <see cref="CultureInfo" /> to use for the conversion.
    /// </param>
    /// <returns>
    /// <see cref="True" /> if the object is of type <see cref="bool" />
    /// and its value is <see langword="true" />; otherwise, returns
    /// <see cref="False" />.
    /// </returns>
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo? culture)
    {
        if (value is bool b) return b ? True : False;
        return null;
    }

    /// <summary>
    /// Converts an object to a <see cref="bool" />.
    /// </summary>
    /// <param name="value">Object to convert.</param>
    /// <param name="targetType">Target type.</param>
    /// <param name="parameter">
    /// Custom parameters for performing the conversion.
    /// </param>
    /// <param name="culture">
    /// <see cref="CultureInfo" /> to use for the conversion.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if the object equals <see cref="True" />;
    /// <see langword="false" /> otherwise.
    /// </returns>
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo? culture)
    {
        if (value?.Equals(True) ?? false) return true;
        if (value?.Equals(False) ?? false) return false;
        return null;
    }
}
