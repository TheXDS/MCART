/*
ValueConverters.cs

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

using System.Globalization;
using System.Windows.Data;
using TheXDS.MCART.Types.Extensions;
using System;
using System.Windows;

namespace TheXDS.MCART.ValueConverters
{
    /// <inheritdoc />
    /// <summary>
    /// Convierte un <see cref="string" /> a un <see cref="Visibility" />.
    /// </summary>
    public sealed class StringVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// Obtiene o establece el <see cref="Visibility" /> a devolver cuando
        /// el <see cref="string" /> esté vacío.
        /// </summary>
        public Visibility Empty { get; set; }

        /// <summary>
        /// Obtiene o establece el <see cref="Visibility" /> a devolver cuando
        /// el <see cref="string" /> no esté vacío.
        /// </summary>
        public Visibility NotEmpty { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="StringVisibilityConverter" />.
        /// </summary>
        /// <param name="empty">
        /// <see cref="Visibility" /> a devolver cuando la cadena esté vacía.
        /// </param>
        public StringVisibilityConverter(Visibility empty) : this(empty, Visibility.Visible)
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="StringVisibilityConverter" />.
        /// </summary>
        /// <param name="empty">
        /// <see cref="Visibility" /> a devolver cuando la cadena esté vacía.
        /// </param>
        /// <param name="notEmpty">
        /// <see cref="Visibility" /> a devolver cuando la cadena no esté vacía.
        /// </param>
        public StringVisibilityConverter(Visibility empty, Visibility notEmpty)
        {
            NotEmpty = notEmpty;
            Empty = empty;
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="StringVisibilityConverter" />.
        /// </summary>
        /// <param name="inverted">
        /// Inversión de valores de <see cref="Visibility" />.
        /// </param>
        public StringVisibilityConverter(bool inverted)
        {
            if (inverted)
            {
                NotEmpty = Visibility.Visible;
                Empty = Visibility.Collapsed;
            }
            else
            {
                NotEmpty = Visibility.Collapsed;
                Empty = Visibility.Visible;
            }
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="StringVisibilityConverter" />.
        /// </summary>
        public StringVisibilityConverter()
        {
            NotEmpty = Visibility.Visible;
            Empty = Visibility.Collapsed;
        }

        /// <inheritdoc />
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
        /// <see cref="P:System.Windows.Converters.StringVisibilityConverter.Empty" /> si <paramref name="value" /> es una
        /// cadena
        /// vacía, <see cref="P:System.Windows.Converters.StringVisibilityConverter.NotEmpty" /> en caso contrario.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((string) value)?.IsEmpty() ?? false ? Empty : NotEmpty;
        }

        /// <inheritdoc />
        /// <summary>
        /// Infiere un valor basado en el <see cref="Visibility" /> provisto.
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
        /// <c>value.ToString()</c> si <paramref name="value" /> no es una
        /// cadena vacía, <see cref="F:System.String.Empty" /> en caso contrario.
        /// </returns>
        public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Visibility v)
                return v == NotEmpty ? value.ToString() : string.Empty;
            return null;
        }
    }
}