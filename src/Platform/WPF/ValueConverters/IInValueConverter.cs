/*
IInValueConverter.cs

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
using System.Windows.Data;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.ValueConverters
{
    /// <summary>
    /// Define una serie de miembros a implementar por un tipo que permita
    /// convertir valores de un tipo específico de entrada.
    /// </summary>
    /// <typeparam name="TIn">Tipo de entrada de la conversión.</typeparam>
    public interface IInValueConverter<TIn> : IValueConverter
    {
        /// <summary>
        /// Obtiene una referencia al tipo de objeto de entrada que puede ser
        /// convertido.
        /// </summary>
        Type SourceType => typeof(TIn);

        /// <summary>
        /// Obtiene una referencia al tipo de salida producido por este 
        /// convertidor.
        /// </summary>
        Type TargetType { get; }

        /// <inheritdoc/>
        object? IValueConverter.Convert(object? value, Type targetType, object? parameter, CultureInfo? culture)
        {
            return Convert(value is TIn i ? i : (TIn)typeof(TIn).Default()!, targetType, parameter, culture);
        }

        /// <summary>
        /// Converte un valor.
        /// </summary>
        /// <param name="value">
        /// Valor producido por el origen del enlace.
        /// </param>
        /// <param name="targetType">El tipo objetivo de la propiedad enlazada.</param>
        /// <param name="parameter">
        /// Parámetro pasado al convertidor a utilizar.
        /// </param>
        /// <param name="culture">
        /// La cultura a utilizar en el convertidor.
        /// </param>
        /// <returns></returns>
        object? Convert(TIn value, Type targetType, object? parameter, CultureInfo? culture);
    }
}
