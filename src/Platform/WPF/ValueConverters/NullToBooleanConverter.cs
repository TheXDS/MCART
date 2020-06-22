/*
NullToBooleanConverter.cs

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
using System.Windows;
using System.Windows.Data;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.ValueConverters
{
    /// <summary>
    /// Devuelve <see langword="true" /> si el elemento a convertir es <see langword="null" />
    /// </summary>
    public sealed class NullToBooleanConverter : IValueConverter
    {
        /// <summary>
        /// Obtiene un <see cref="Visibility" /> a partir del valor.
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
        /// <see cref="Visibility.Visible" /> si el elemento es
        /// <see langword="null" />, <see cref="Visibility.Collapsed" /> en caso
        /// contrario.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is null;
        }

        /// <summary>
        /// Implementa <see cref="IValueConverter.ConvertBack" />.
        /// </summary>
        /// <param name="value">Objeto a convertir.</param>
        /// <param name="targetType">Tipo del destino.</param>
        /// <param name="parameter">
        /// Parámetros personalizados para este <see cref="IValueConverter" />.
        /// </param>
        /// <param name="culture">
        /// <see cref="CultureInfo" /> a utilizar para la conversión.
        /// </param>
        /// <exception cref="InvalidCastException">
        /// Se produce si <paramref name="value" /> no es un <see cref="bool" />.
        /// </exception>
        /// <exception cref="TypeLoadException">
        /// Se produce si <paramref name="targetType" /> no es una clase o estructura instanciable con un constructor sin
        /// parámetros.
        /// </exception>
        /// <returns>
        /// Una nueva instancia de tipo <paramref name="targetType" /> si <paramref name="value" /> se evalúa como
        /// <see langword="true" />, <see langword="null" /> en caso contrario.
        /// </returns>
        public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool b) return b ? null : targetType.New();
            throw new InvalidCastException();
        }
    }
}