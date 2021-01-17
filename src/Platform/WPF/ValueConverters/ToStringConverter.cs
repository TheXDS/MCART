/*
ToStringConverter.cs

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

using System;
using System.Globalization;
using System.Windows.Data;

namespace TheXDS.MCART.ValueConverters
{
    /// <summary>
    /// Convierte un valor a su representación como un <see cref="string" />.
    /// </summary>
    public sealed class ToStringConverter : IValueConverter
    {
        /// <summary>
        /// Convierte cualquier objeto en un <see cref="string" />
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
        /// Un <see cref="string" /> que representa al objeto.
        /// </returns>
        public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value?.ToString();
        }

        /// <summary>
        /// Intenta una conversión de <see cref="string" /> a un objeto del tipo
        /// de destino especificado.
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
        /// Si la conversión desde <see cref="string" /> tuvo éxito, se
        /// devolverá al objeto, se devolverá <see langword="null" /> en caso contrario.
        /// </returns>
        public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                return targetType?.GetMethod(
                        "Parse", new[] {typeof(string)})?
                    .Invoke(null, new[] {value});
            }
            catch
            {
                return null;
            }
        }
    }
}