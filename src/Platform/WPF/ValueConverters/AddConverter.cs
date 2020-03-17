/*
AddConverter.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2019 César Andrés Morgan

Morgan's CLR Advanced Runtime (MCART) is free software: you can redistribute it
and/or modify it under the terms of the GNU General Public License as published
by the Free Software Foundation, either version 3 of the License, or (at your
option) any later version.

Morgan's CLR Advanced Runtime (MCART) is distributed in the hope that it will
be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General
Public License for more details.

You should have received a copy of the GNU General Public License along with
this program. If not, see <http://www.gnu.org/licenses/>.
*/

using System.Globalization;
using System.Windows.Data;
using System;
using TheXDS.MCART.Types.Extensions;
using System.Linq;
using System.Linq.Expressions;

namespace TheXDS.MCART.ValueConverters
{
    /// <summary>
    /// Clase base que permite la creación de <see cref="IValueConverter"/>
    /// para operaciones matemáticas numéricas.
    /// </summary>
    public abstract class PrimitiveMathOpConverterBase
    {
        /// <summary>
        /// Delegado que describe una función que genera una expresión binaria.
        /// </summary>
        /// <param name="opA">Primer operando de la expresión binaria.</param>
        /// <param name="opB">Segundo operando de la expresión binaria.</param>
        /// <returns>
        /// Una expresión binaria que ejecuta una operación numérica sobre los
        /// operandos especificados.
        /// </returns>
        protected delegate BinaryExpression ExpressionBuilder(Expression opA, Expression opB);

        private static (object opA, object opB) CastUp(object valA, object valB, IFormatProvider? provider)
        {
            var vals = new[] { valA, valB };            
            foreach (var j in new Type[]
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

            if (int.TryParse(valA.ToString(), NumberStyles.Any, provider, out var intA) && int.TryParse(valB.ToString(), out var intB))
            {
                return (intA, intB);
            }

            if (double.TryParse(valA.ToString(), NumberStyles.Any, provider, out var doubleA) && double.TryParse(valB.ToString(), out var doubleB))
            {
                return (doubleA, doubleB);
            }

            throw new NotSupportedException();
        }

        private static (object opA, object opB)? TryCast(object[] vals, Type t)
        {
            try
            {
                return vals.IsAnyOf(t) ? ((object opA, object opB)?)(Convert.ChangeType(vals[0], t), Convert.ChangeType(vals[1], t)) : null;
            }
            catch 
            {
                return null;
            }
        }

        /// <summary>
        /// Ejecuta una operación numérica sobre los objetos especificados.
        /// </summary>
        /// <param name="value">Primer operando.</param>
        /// <param name="targetType">
        /// Tipo objetivo del enlace de propiedad.
        /// </param>
        /// <param name="parameter">Segundo operando.</param>
        /// <param name="culture">
        /// Información cultural a utilizar durante la conversión.
        /// </param>
        /// <param name="func">
        /// Operación matemática a realizar.
        /// </param>
        /// <returns>
        /// El resultado de la operación matemática solicitada con los
        /// operandos especificados, <see cref="double.NaN"/> o
        /// <see cref="float.NaN"/> si ocurre un error en la operación y el
        /// tipo objetivo es <see cref="double"/> o <see cref="float"/>
        /// respectivamente.
        /// </returns>
        /// <exception cref="OverflowException">
        /// Se produce si la operación resulta en desbordamiento del tipo
        /// objetivo.
        /// </exception>
        /// <exception cref="NotSupportedException">
        /// Se produce si la operación resulta en error debido a operandos
        /// inválidos y el tipo objetivo no es <see cref="double"/> ni
        /// <see cref="float"/>.
        /// </exception>
        protected static object? Operate(object value, Type targetType, object? parameter, CultureInfo? culture, ExpressionBuilder func)
        {
            try
            {
                var (firstOperand, secondOperand) = targetType.IsPrimitive || targetType == typeof(decimal) 
                    ? CastUp(value ?? throw new ArgumentNullException(nameof(value)), parameter ?? targetType.Default()!, culture) 
                    : (value, parameter);

                return Convert.ChangeType(Expression.Lambda(func(Expression.Constant(firstOperand), Expression.Constant(secondOperand))).Compile().DynamicInvoke(), targetType);
            }
            catch (Exception ex)
            {
                if (targetType == typeof(double)) return double.NaN;
                if (targetType == typeof(float)) return float.NaN;
                throw ex;                    
            }
        }
    }

    /// <inheritdoc />
    /// <summary>
    /// Permite la adición de propiedades numéricas.
    /// </summary>
    public sealed class AddConverter : PrimitiveMathOpConverterBase, IValueConverter
    {
        /// <inheritdoc />
        /// <summary>
        /// Devuelve la suma entre <paramref name="value" /> y
        /// <paramref name="parameter" />.
        /// </summary>
        /// <param name="value">Primer operando de la suma.</param>
        /// <param name="targetType">Tipo del destino.</param>
        /// <param name="parameter">Segundo operando de la suma.</param>
        /// <param name="culture">
        /// <see cref="CultureInfo" /> a utilizar para la conversión.
        /// </param>
        /// <exception cref="ArgumentException">Se produce si no es posible realizar la suma.</exception>
        /// <returns>La suma de <paramref name="value" /> y el operando especificado.</returns>
        public object? Convert(object value, Type targetType, object? parameter, CultureInfo? culture)
        {
            return Operate(value, targetType, parameter, culture, Expression.Add);
        }

        /// <inheritdoc />
        /// <summary>
        /// Revierte la operación de suma aplicada a <paramref name="value" />.
        /// </summary>
        /// <param name="value">Objeto a convertir.</param>
        /// <param name="targetType">Tipo del destino.</param>
        /// <param name="parameter">
        /// Parámetros personalizados para este <see cref="IValueConverter" />.
        /// </param>
        /// <param name="culture">
        /// <see cref="CultureInfo" /> a utilizar para la conversión.
        /// </param>
        /// <exception cref="ArgumentException">Se produce si no es posible realizar la resta.</exception>
        /// <returns>
        /// El valor de <paramref name="value" /> antes de la suma.
        /// </returns>
        public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Operate(value, targetType, parameter, culture, Expression.Subtract);
        }
    }
}