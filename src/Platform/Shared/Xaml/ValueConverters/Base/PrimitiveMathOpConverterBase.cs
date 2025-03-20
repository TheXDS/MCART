/*
PrimitiveMathOpConverterBase.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     C�sar Andr�s Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright � 2011 - 2025 C�sar Andr�s Morgan

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
    /// Delegado que describe una funci�n que genera una expresi�n binaria.
    /// </summary>
    /// <param name="opA">Primer operando de la expresi�n binaria.</param>
    /// <param name="opB">Segundo operando de la expresi�n binaria.</param>
    /// <returns>
    /// Una expresi�n binaria que ejecuta una operaci�n num�rica sobre los
    /// operandos especificados.
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
    /// Ejecuta una operaci�n num�rica sobre los objetos especificados.
    /// </summary>
    /// <param name="value">Primer operando.</param>
    /// <param name="targetType">
    /// Tipo objetivo del enlace de propiedad.
    /// </param>
    /// <param name="parameter">Segundo operando.</param>
    /// <param name="culture">
    /// Informaci�n cultural a utilizar durante la conversi�n.
    /// </param>
    /// <param name="func">
    /// Operaci�n matem�tica a realizar.
    /// </param>
    /// <returns>
    /// El resultado de la operaci�n matem�tica solicitada con los
    /// operandos especificados, <see cref="double.NaN"/> o
    /// <see cref="float.NaN"/> si ocurre un error en la operaci�n y el
    /// tipo objetivo es <see cref="double"/> o <see cref="float"/>
    /// respectivamente.
    /// </returns>
    /// <exception cref="OverflowException">
    /// Se produce si la operaci�n resulta en desbordamiento del tipo
    /// objetivo.
    /// </exception>
    /// <exception cref="NotSupportedException">
    /// Se produce si la operaci�n resulta en error debido a operandos
    /// inv�lidos y el tipo objetivo no es <see cref="double"/> ni
    /// <see cref="float"/>.
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
