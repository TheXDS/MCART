/*
IValueConverter.cs

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

namespace TheXDS.MCART.ValueConverters.Base;
using System;
using System.Globalization;
using System.Windows.Data;
using TheXDS.MCART.Types.Extensions;

/// <summary>
/// Define una serie de miembros a implementar por un tipo que permita
/// convertir valores desde y hacia tipos específicos en ambos sentidos.
/// </summary>
/// <typeparam name="TIn">Tipo de entrada de la conversión.</typeparam>
/// <typeparam name="TOut">Tipo de salida de la conversión.</typeparam>
public interface IValueConverter<TIn, TOut> : IOneWayValueConverter<TIn, TOut>
{
    /// <summary>
    /// Realiza una conversión inversa entre el valor de destino y el de origen.
    /// </summary>
    /// <param name="value">
    /// Valor producido por el origen del enlace.
    /// </param>
    /// <param name="parameter">
    /// Parámetro pasado al convertidor a utilizar.
    /// </param>
    /// <param name="culture">
    /// La cultura a utilizar en el convertidor.
    /// </param>
    /// <returns>
    /// Un valor convertido. Si no se puede llevar a cabo la conversión,
    /// se devuelve el valor predeterminado del tipo
    /// <typeparamref name="TIn"/>.
    /// </returns>
    TIn ConvertBack(TOut value, object? parameter, CultureInfo culture);

    /// <inheritdoc/>
    object? IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return ConvertBack(value is TOut i ? i : (TOut)typeof(TOut).Default()!, parameter, culture);
    }
}
