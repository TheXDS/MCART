/*
IInValueConverter.cs

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
using System.Windows.Data;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.ValueConverters.Base;

/// <summary>
/// Defines members that must be implemented by a type capable of
/// converting values from a specific input type.
/// </summary>
/// <typeparam name="TIn">The input type of the conversion.</typeparam>
public interface IInValueConverter<TIn> : IValueConverter
{
    /// <summary>
    /// Gets a reference to the input type that can be converted.
    /// </summary>
    Type SourceType => typeof(TIn);

    /// <summary>
    /// Gets a reference to the output type produced by this converter.
    /// </summary>
    Type TargetType { get; }

    /// <inheritdoc/>
    object? IValueConverter.Convert(object? value, Type targetType, object? parameter, CultureInfo? culture)
    {
        return Convert(value is TIn i ? i : (TIn)typeof(TIn).Default()!, targetType, parameter, culture);
    }

    /// <summary>
    /// Converts a value.
    /// </summary>
    /// <param name="value">
    /// Value produced by the binding source.
    /// </param>
    /// <param name="targetType">
    /// The target type of the bound property.
    /// </param>
    /// <param name="parameter">
    /// Parameter passed to the converter.
    /// </param>
    /// <param name="culture">
    /// The culture to use for the conversion.
    /// </param>
    /// <returns>
    /// The converted value or <c>null</c> if the conversion fails.
    /// </returns>
    object? Convert(TIn value, Type targetType, object? parameter, CultureInfo? culture);
}
