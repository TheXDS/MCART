/*
CountVisibilityConverter.cs

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
using System.Windows;
using System.Windows.Data;

namespace TheXDS.MCART.ValueConverters
{
    /// <summary>
    /// Convierte un valor <see cref="int" />  a <see cref="Visibility" />
    /// </summary>
    public sealed class CountVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// Obtiene o establece el <see cref="Visibility" /> a devolver en caso
        /// de que la cuenta de elementos sea mayor a cero.
        /// </summary>
        public Visibility WithItems { get; set; }

        /// <summary>
        /// Obtiene o establece el <see cref="Visibility" /> a devolver en caso
        /// de que la cuenta de elementos sea igual a cero.
        /// </summary>
        public Visibility WithoutItems { get; set; }

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="CountVisibilityConverter" />.
        /// </summary>
        public CountVisibilityConverter() : this(Visibility.Visible, Visibility.Collapsed)
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="CountVisibilityConverter" />.
        /// </summary>
        /// <param name="withItems">
        /// <see cref="Visibility" /> a utilizar en caso de que la cuenta sea
        /// mayor a cero.
        /// </param>
        public CountVisibilityConverter(Visibility withItems = Visibility.Visible) : this(withItems,
            Visibility.Collapsed)
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="CountVisibilityConverter" />.
        /// </summary>
        /// <param name="withItems">
        /// <see cref="Visibility" /> a utilizar en caso de que la cuenta sea
        /// mayor a cero.
        /// </param>
        /// <param name="withoutItems">
        /// <see cref="Visibility" /> a utilizar en caso de que la cuenta sea
        /// igual a cero.
        /// </param>
        public CountVisibilityConverter(Visibility withItems, Visibility withoutItems)
        {
            WithItems = withItems;
            WithoutItems = withoutItems;
        }

        /// <summary>
        /// Obtiene un <see cref="Visibility" /> a partir de un valor de cuenta.
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
        /// <see cref="WithItems" /> si <paramref name="value" /> es mayor
        /// a
        /// cero, <see cref="WithoutItems" /> en caso contrario.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int v) return v > 0 ? WithItems : WithoutItems;
            throw new InvalidCastException();
        }

        /// <summary>
        /// Infiere una cuenta de elementos basado en el <see cref="Visibility" /> provisto.
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
        /// <c>1</c> si <paramref name="value" /> es igual a
        /// <see cref="WithItems" />, <c>0</c> en caso contrario.
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Visibility v) return v == WithItems ? 1 : 0;
            throw new InvalidCastException();
        }
    }
}