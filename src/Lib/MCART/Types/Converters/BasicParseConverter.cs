/*
BasicParseConverter.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Este archivo define la estructura Range<TValue>, la cual permite representar rangos
de valores.

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2025 César Andrés Morgan

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

using System.ComponentModel;
using System.Globalization;

namespace TheXDS.MCART.Types.Converters;

/// <summary>
/// Base class for a basic
/// <see cref="TypeConverter"/> that allows transforming a value of
/// <see cref="string"/> into a <typeparamref name="T"/>.
/// </summary>
/// <typeparam name="T">
/// Type of the output element of this
/// <see cref="TypeConverter"/>.
/// </typeparam>
public abstract class BasicParseConverter<T> : TypeConverter
{
    /// <summary>
    /// Returns whether this converter can convert an object of the specified type to the type of this converter, using the specified context.
    /// </summary>
    /// <param name="context">
    /// <see cref="ITypeDescriptorContext"/> that provides a formatting context.
    /// </param>
    /// <param name="sourceType">
    /// A <see cref="Type"/> representing the type to be converted.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if this converter can perform the conversion; otherwise, <see langword="false"/>.
    /// </returns>
    public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
    {
        return sourceType == typeof(string);
    }

    /// <summary>
    /// Returns whether this converter can convert the object to the specified type, with the specified context.
    /// </summary>
    /// <param name="context">
    /// <see cref="ITypeDescriptorContext"/> interface that provides a formatting context.
    /// </param>
    /// <param name="destinationType">
    /// A <see cref="Type"/> representing the type to which the conversion is intended.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if this converter can perform the conversion; otherwise, <see langword="false"/>.
    /// </returns>
    public override bool CanConvertTo(ITypeDescriptorContext? context, Type? destinationType)
    {
        return destinationType == typeof(T);
    }

    /// <summary>
    /// Converts the specified object to the type of this converter using the specified context and culture information.
    /// </summary>
    /// <param name="context">
    /// <see cref="ITypeDescriptorContext"/> that provides a formatting context.
    /// </param>
    /// <param name="culture">
    /// A <see cref="CultureInfo"/> to use as the current culture.
    /// </param>
    /// <param name="value">
    /// The <see cref="object"/> to be converted.
    /// </param>
    /// <returns>
    /// An <see cref="object"/> representing the converted value.
    /// </returns>
    /// <exception cref="NotSupportedException">
    /// The conversion cannot be performed.
    /// </exception>
    public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
    {
        return ConvertFrom(value?.ToString());
    }

    /// <summary>
    /// Executes the conversion from string to the type of this <see cref="BasicParseConverter{T}"/>.
    /// </summary>
    /// <param name="value">String to convert.</param>
    /// <returns>
    /// A value of type <typeparamref name="T"/> created from the specified string.
    /// </returns>
    protected abstract T ConvertFrom(string? value);

    /// <summary>
    /// Converts the specified value object to the specified type using the specified context and culture information.
    /// </summary>
    /// <param name="context">
    /// <see cref="ITypeDescriptorContext"/> that provides a formatting context.
    /// </param>
    /// <param name="culture">
    /// <see cref="CultureInfo"/> object.
    /// If null is passed, the current culture is assumed.
    /// </param>
    /// <param name="value">
    /// The <see cref="object"/> to be converted.
    /// </param>
    /// <param name="destinationType">
    /// The <see cref="Type"/> to convert the <paramref name="value"/> parameter.
    /// </param>
    /// <returns>
    /// An <see cref="object"/> representing the converted value.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// The <paramref name="destinationType"/> parameter is <see langword="null"/>.
    /// </exception>
    /// <exception cref="NotSupportedException">
    /// The conversion cannot be performed.
    /// </exception>
    public override object? ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type destinationType)
    {
        if (value is T v) return ConvertTo(v);
        return base.ConvertTo(context, culture, value, destinationType);
    }

    /// <summary>
    /// Executes the conversion from string to the type of this <see cref="BasicParseConverter{T}"/>.
    /// </summary>
    /// <param name="value">String to convert.</param>
    /// <returns>
    /// The string representation of the specified object.
    /// </returns>
    protected virtual string? ConvertTo(T value)
    {
        return value?.ToString();
    }
}
