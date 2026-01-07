/*
DivideConverter.cs

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
/// Permite la adición de propiedades numéricas.
/// </summary>
public sealed partial class DivideConverter : PrimitiveMathOpConverterBase
{
    /// <summary>
    /// Devuelve la división entre <paramref name="value" /> y
    /// <paramref name="parameter" />.
    /// </summary>
    /// <param name="value">Primer operando de la división.</param>
    /// <param name="targetType">Tipo del destino.</param>
    /// <param name="parameter">Segundo operando de la división.</param>
    /// <param name="culture">
    /// <see cref="CultureInfo" /> a utilizar para la conversión.
    /// </param>
    /// <exception cref="ArgumentException">
    /// Se produce si no es posible realizar la división.
    /// </exception>
    /// <exception cref="DivideByZeroException">
    /// Se produce si <paramref name="parameter"/> se evalúa como cero.
    /// </exception>
    /// <returns>
    /// La división de <paramref name="value"/> y el operando especificado.
    /// </returns>
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo? culture)
    {
        return Operate(value, targetType, parameter, culture, Expression.Divide);
    }

    /// <summary>
    /// Revierte la operación de división aplicada a
    /// <paramref name="value" />.
    /// </summary>
    /// <param name="value">Objeto a convertir.</param>
    /// <param name="targetType">Tipo del destino.</param>
    /// <param name="parameter">
    /// Parámetros personalizados a utilizar para la conversión.
    /// </param>
    /// <param name="culture">
    /// <see cref="CultureInfo" /> a utilizar para la conversión.
    /// </param>
    /// <exception cref="ArgumentException">
    /// Se produce si no es posible realizar la división.
    /// </exception>
    /// <returns>
    /// El valor de <paramref name="value"/> antes de la división.
    /// </returns>
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return Operate(value, targetType, parameter, culture, Expression.Multiply);
    }
}
