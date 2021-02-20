/*
McartColorConverter.cs

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

using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using TheXDS.MCART.Types.Extensions;
using MT = TheXDS.MCART.Types;
using System;

namespace TheXDS.MCART.ValueConverters
{

    /// <summary>
    /// Convierte valores desde y hacia objetos de tipo
    /// <see cref="MT.Color"/>.
    /// </summary>
    public sealed class McartColorConverter : IValueConverter
    {
        private static readonly Dictionary<Type, IInValueConverter<MT.Color>> _converters = Objects.FindAllObjects<IInValueConverter<MT.Color>>().ToDictionary(p => p.TargetType);

        /// <summary>
        /// Convierte un <see cref="MT.Color"/> en un valor
        /// compatible con el campo de destino.
        /// </summary>
        /// <param name="value">Objeto a convertir.</param>
        /// <param name="targetType">Tipo del destino.</param>
        /// <param name="parameter">
        /// Parámetros personalizados para este <see cref="IValueConverter" />.
        /// </param>
        /// <param name="culture">
        /// <see cref="CultureInfo" /> a utilizar para la conversión.
        /// </param>
        /// <returns>
        /// Un valor compatible con el campo de destino, o
        /// <see langword="null"/> si no es posible realizar la conversión.
        /// </returns>
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo? culture)
        {
            if (value is not MT.Color c) return targetType.Default();
            if (targetType.IsAssignableFrom(value?.GetType())) return value;
            return _converters.ContainsKey(targetType) ? _converters[targetType].Convert(c, targetType, parameter, culture) : targetType.Default();
        }

        /// <summary>
        /// Convierte un objeto en un 
        /// <see cref="MT.Color"/>.
        /// </summary>
        /// <param name="value">Objeto a convertir.</param>
        /// <param name="targetType">Tipo del destino.</param>
        /// <param name="parameter">
        /// Parámetros personalizados para este <see cref="IValueConverter" />.
        /// </param>
        /// <param name="culture">
        /// <see cref="CultureInfo" /> a utilizar para la conversión.
        /// </param>
        /// <returns>
        /// Un <see cref="MT.Color"/> creado a partir del
        /// valor del objeto, o <see langword="null"/> si no es posible
        /// realizar la conversión.
        /// </returns>
        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo? culture)
        {
            if (targetType != typeof(MT.Color)) return null;
            if (targetType.IsAssignableFrom(value?.GetType())) return value;
            return _converters.ContainsKey(targetType) ? _converters[targetType].ConvertBack(value, targetType, parameter, culture) : targetType.Default();
        }
    }
}