/*
NullBoolConverter.cs

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
/// Base class for creating boolean value converters that can be null.
/// </summary>
/// <typeparam name="T">Type of values to convert.</typeparam>
public sealed partial class NullBoolConverter<T>
{
    /// <summary>
    /// Gets or sets the value that represents
    /// <see langword="false" /> in this
    /// <see cref="NullBoolConverter{T}" />.
    /// </summary>
    public T False { get; set; }

    /// <summary>
    /// Gets or sets the value that represents
    /// <see langword="null" /> in this
    /// <see cref="NullBoolConverter{T}" />.
    /// </summary>
    public T Null { get; set; }

    /// <summary>
    /// Gets or sets the value that represents
    /// <see langword="true" /> in this
    /// <see cref="NullBoolConverter{T}" />.
    /// </summary>
    public T True { get; set; }

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="NullBoolConverter{T}" /> class, setting the values that
    /// correspond to <see langword="true" /> and <see langword="false" />.
    /// </summary>
    /// <param name="trueValue">Value equivalent to <see langword="true" />.</param>
    /// <param name="falseValue">Value equivalent to <see langword="false" />.</param>
    public NullBoolConverter(T trueValue, T falseValue = default!)
    {
        True = trueValue;
        Null = False = falseValue!;
    }

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="NullBoolConverter{T}" /> class, setting the values that
    /// correspond to <see langword="true" />, <see langword="false" /> and
    /// <see langword="null" />.
    /// </summary>
    /// <param name="trueValue">Value equivalent to <see langword="true" />.</param>
    /// <param name="falseValue">Value equivalent to <see langword="false" />.</param>
    /// <param name="nullValue">Value equivalent to <see langword="null" />.</param>
    public NullBoolConverter(T trueValue, T falseValue, T nullValue)
    {
        True = trueValue;
        False = falseValue;
        Null = nullValue;
    }

    /// <summary>
    /// Converts a value to <see cref="Nullable{T}" />.
    /// </summary>
    /// <param name="value">Object to convert.</param>
    /// <param name="targetType">Target type.</param>
    /// <param name="parameter">Custom parameters used for the conversion.</param>
    /// <param name="culture"><see cref="CultureInfo" /> used for the conversion.</param>
    /// <returns>
    /// <see cref="NullBoolConverter{T}.True" /> if <paramref name="value" /> is
    /// <see langword="true" />, <see cref="NullBoolConverter{T}.False" /> if
    /// <paramref name="value" /> is <see langword="false" />, and
    /// <see cref="NullBoolConverter{T}.Null" /> if <paramref name="value" /> is
    /// <see langword="null" />.
    /// </returns>
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value switch
        {
            bool b => b ? True : False,
            null => Null,
            _ => default!,
        };
    }

    /// <summary>
    /// Converts a <see cref="bool" /> to the type set for this
    /// <see cref="NullBoolConverter{T}" />.
    /// </summary>
    /// <param name="value">Object to convert.</param>
    /// <param name="targetType">Target type.</param>
    /// <param name="parameter">Custom parameters used for the conversion.</param>
    /// <param name="culture"><see cref="CultureInfo" /> used for the conversion.</param>
    /// <returns>
    /// <see langword="true" /> if <paramref name="value" /> is
    /// <see cref="NullBoolConverter{T}.True" />, <see langword="false" /> if
    /// <paramref name="value" /> is <see cref="NullBoolConverter{T}.False" />,
    /// and <see langword="null" /> if <paramref name="value" /> is
    /// <see cref="NullBoolConverter{T}.Null" />.
    /// </returns>
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is null) return null;
        if (value is not T) return default(T);
        if (value.Equals(Null) && !ReferenceEquals(Null, False)) return null;
        return ((T)value).Equals(True);
    }
}
