/*
AddConverter.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2021 César Andrés Morgan

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

namespace TheXDS.MCART.ValueConverters;
using System;
using System.Globalization;
using System.Linq.Expressions;
using System.Windows.Data;
using TheXDS.MCART.ValueConverters.Base;

/// <summary>
/// Permite la adición de propiedades numéricas.
/// </summary>
public sealed class AddConverter : PrimitiveMathOpConverterBase, IValueConverter
{
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
    /// <exception cref="ArgumentException">
    /// Se produce si no es posible realizar la suma.
    /// </exception>
    /// <returns>
    /// La suma de <paramref name="value" /> y el operando especificado.
    /// </returns>
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo? culture)
    {
        return Operate(value, targetType, parameter, culture, Expression.Add);
    }

    /// <summary>
    /// Revierte la operación de suma aplicada a <paramref name="value"/>.
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
    /// Se produce si no es posible realizar la resta.
    /// </exception>
    /// <returns>
    /// El valor de <paramref name="value" /> antes de la suma.
    /// </returns>
    public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return Operate(value, targetType, parameter, culture, Expression.Subtract);
    }
}
