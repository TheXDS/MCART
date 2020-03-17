/*
ValueConverters.cs

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
using System.Windows;

namespace TheXDS.MCART.ValueConverters
{
    /// <inheritdoc />
    /// <summary>
    /// Determina si un objeto es una instancia del tipo provisto, y devuelve
    /// <see cref="F:System.Windows.Visibility.Visible" /> si lo es.
    /// </summary>
    public sealed class StrictTypeVisibilityConverter : IValueConverter
    {
        /// <inheritdoc />
        /// <summary>
        /// Determina si un objeto es una instancia del tipo provisto.
        /// </summary>
        /// <param name="value">
        /// Valor generado por el origen de enlace.
        /// </param>
        /// <param name="targetType">
        /// El tipo de la propiedad del destino de enlace.
        /// </param>
        /// <param name="parameter">
        /// Parámetro de convertidor que se va a usar.
        /// </param>
        /// <param name="culture">
        /// Referencia cultural que se va a usar en el convertidor.
        /// </param>
        /// <returns>
        /// <see cref="F:System.Windows.Visibility.Visible" /> si el objeto es una instancia del
        /// tipo provisto, <see cref="F:System.Windows.Visibility.Collapsed" /> en caso contrario.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(parameter is Type t)) throw new ArgumentException();
            return t.IsInstanceOfType(value) ? Visibility.Visible : Visibility.Collapsed;
        }

        /// <inheritdoc />
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
        /// Se produce si <paramref name="value" /> no es un <see cref="Visibility" />.
        /// </exception>
        /// <exception cref="TypeLoadException">
        /// Se produce si <paramref name="targetType" /> no es una clase o estructura instanciable con un constructor sin
        /// parámetros.
        /// </exception>
        /// <returns>
        /// Este método siempre genera un <see cref="InvalidOperationException" />.
        /// </returns>
        /// <exception cref="InvalidOperationException">Este método siempre genera esta excepción al ser llamado.</exception>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new InvalidOperationException();
        }
    }
}