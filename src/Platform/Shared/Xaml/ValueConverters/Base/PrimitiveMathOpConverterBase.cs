/*
PrimitiveMathOpConverterBase.cs

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
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.ValueConverters.Base;

public abstract partial class PrimitiveMathOpConverterBase
{
    /// <summary>
    /// Delegate that describes a function that builds a binary expression.
    /// </summary>
    /// <param name="opA">
    /// First operand of the binary expression.
    /// </param>
    /// <param name="opB">
    /// Second operand of the binary expression.
    /// </param>
    /// <returns>
    /// A binary expression that performs a numeric operation on the
    /// specified operands.
    /// </returns>
    protected delegate BinaryExpression ExpressionBuilder(Expression opA, Expression opB);

    private static (object opA, object opB) CastUp(object valA, object valB, IFormatProvider? provider)
    {
        object[] vals = [valA, valB];
        foreach (Type? j in new[]
        {
            typeof(decimal),
            typeof(ulong),
            typeof(long),
            typeof(double),
            typeof(uint),
            typeof(int),
            typeof(float),
            typeof(ushort),
            typeof(short),
            typeof(byte),
            typeof(sbyte)
        })
        {
            if (TryCast(vals, j) is { } result) return result;
        }

        if (int.TryParse(valA.ToString(), NumberStyles.Any, provider, out int intA) && int.TryParse(valB.ToString(), out int intB))
            return (intA, intB);

        if (double.TryParse(valA.ToString(), NumberStyles.Any, provider, out double doubleA) && double.TryParse(valB.ToString(), out double doubleB))
            return (doubleA, doubleB);

        throw new NotSupportedException();
    }

    private static (object opA, object opB)? TryCast(object[] vals, Type t)
    {
        try
        {
            return vals.IsAnyOf(t) ? (Convert.ChangeType(vals[0], t), Convert.ChangeType(vals[1], t)) : null;
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// Executes a numeric operation on the specified objects.
    /// </summary>
    /// <param name="value">
    /// First operand.
    /// </param>
    /// <param name="targetType">
    /// Target type of the property binding.
    /// </param>
    /// <param name="parameter">
    /// Second operand.
    /// </param>
    /// <param name="culture">
    /// Cultural information to use during the conversion.
    /// </param>
    /// <param name="func">
    /// Mathematical operation to perform.
    /// </param>
    /// <returns>
    /// The result of the requested mathematical operation with the
    /// specified operands, <see cref="double.NaN"/> or
    /// <see cref="float.NaN"/> if an error occurs in the operation and
    /// the target type is <see cref="double"/> or <see cref="float"/>
    /// respectively.
    /// </returns>
    /// <exception cref="OverflowException">
    /// Thrown if the operation results in an overflow of the target type.
    /// </exception>
    /// <exception cref="NotSupportedException">
    /// Thrown if the operation fails due to invalid operands and the
    /// target type is neither <see cref="double"/> nor <see cref="float"/>.
    /// </exception>
    protected static object? Operate(object? value, Type targetType, object? parameter, CultureInfo? culture, ExpressionBuilder func)
    {
        try
        {
            (object? firstOperand, object? secondOperand) = targetType.IsPrimitive || targetType == typeof(decimal)
                ? CastUp(value ?? throw new ArgumentNullException(nameof(value)), parameter ?? targetType.Default()!, culture)
                : (value, parameter);

            return Convert.ChangeType(Expression.Lambda(func(Expression.Constant(firstOperand), Expression.Constant(secondOperand))).Compile().DynamicInvoke(), targetType);
        }
        catch
        {
            if (targetType == typeof(double)) return double.NaN;
            if (targetType == typeof(float)) return float.NaN;
            throw;
        }
    }
}
