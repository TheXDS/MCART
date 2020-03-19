/*
DivideConverter.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2020 César Andrés Morgan

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

using System;
using System.Globalization;
using System.Linq.Expressions;
using System.Windows.Data;
using TheXDS.MCART.ValueConverters.Base;

namespace TheXDS.MCART.ValueConverters
{
    /// <inheritdoc />
    /// <summary>
    /// Permite la adición de propiedades numéricas.
    /// </summary>
    public sealed class DivideConverter : PrimitiveMathOpConverterBase, IValueConverter
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
        public object? Convert(object value, Type targetType, object? parameter, CultureInfo? culture)
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
        /// Parámetros personalizados para este <see cref="IValueConverter"/>.
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
        public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Operate(value, targetType, parameter, culture, Expression.Multiply);
        }
    }
}