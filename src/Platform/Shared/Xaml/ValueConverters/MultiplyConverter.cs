/*
MultiplyConverter.cs

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
using System.Linq.Expressions;
using TheXDS.MCART.ValueConverters.Base;

namespace TheXDS.MCART.ValueConverters;

/// <summary>
/// Enables the multiplication of numeric properties.
/// </summary>
public sealed partial class MultiplyConverter : PrimitiveMathOpConverterBase
{
    /// <summary>
    /// Returns the product of <paramref name="value"/> and
    /// <paramref name="parameter"/>.
    /// </summary>
    /// <param name="value">First operand of the multiplication.</param>
    /// <param name="targetType">Target type.</param>
    /// <param name="parameter">Second operand of the multiplication.</param>
    /// <param name="culture">
    /// <see cref="CultureInfo"/> to use during the conversion.
    /// </param>
    /// <returns>
    /// The product of <paramref name="value"/> and the specified operand.
    /// </returns>
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo? culture)
    {
        return Operate(value, targetType, parameter, culture, Expression.Multiply);
    }

    /// <summary>
    /// Reverses the multiplication applied to <paramref name="value"/>.
    /// </summary>
    /// <param name="value">Object to convert.</param>
    /// <param name="targetType">Target type.</param>
    /// <param name="parameter">
    /// Custom parameters used to perform the conversion.
    /// </param>
    /// <param name="culture">
    /// <see cref="CultureInfo"/> to use during the conversion.
    /// </param>
    /// <returns>
    /// The value of <paramref name="value"/> before the multiplication.
    /// </returns>
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo? culture)
    {
        return Operate(value, targetType, parameter, culture, Expression.Divide);
    }
}
