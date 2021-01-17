/*
FloatVisibilityConverter.cs

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
    /// Convierte un valor <see cref="float" /> a <see cref="Visibility" />.
    /// </summary>
    public sealed class FloatVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// Obtiene o establece el <see cref="Visibility" /> a devolver cuando
        /// el valor sea mayor a cero.
        /// </summary>
        public Visibility Positives { get; set; }

        /// <summary>
        /// Obtiene o establece el <see cref="Visibility" /> a devolver cuando
        /// el valor sea menor o igual a cero.
        /// </summary>
        public Visibility ZeroOrNegatives { get; set; }

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="FloatVisibilityConverter" />.
        /// </summary>
        public FloatVisibilityConverter() : this(Visibility.Visible, Visibility.Collapsed)
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="FloatVisibilityConverter" />.
        /// </summary>
        /// <param name="positives">
        /// <see cref="Visibility" /> a utilizar para valores positivos.
        /// </param>
        public FloatVisibilityConverter(Visibility positives) : this(positives, Visibility.Visible)
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="FloatVisibilityConverter" />.
        /// </summary>
        /// <param name="positives">
        /// <see cref="Visibility" /> a utilizar para valores positivos.
        /// </param>
        /// <param name="zeroOrNegatives">
        /// <see cref="Visibility" /> a utilizar para cero y valores negativos.
        /// </param>
        public FloatVisibilityConverter(Visibility positives, Visibility zeroOrNegatives)
        {
            Positives = positives;
            ZeroOrNegatives = zeroOrNegatives;
        }

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
        /// <see cref="P:System.Windows.Converters.FloatVisibilityConverter.Positives" /> si <paramref name="value" /> es mayor
        /// a
        /// cero, <see cref="P:System.Windows.Converters.FloatVisibilityConverter.ZeroOrNegatives" /> en caso contrario.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is float v) return v > 0.0f ? Positives : ZeroOrNegatives;
            throw new InvalidCastException();
        }

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
        /// <c>1.0f</c> si <paramref name="value" /> es igual a
        /// <see cref="P:System.Windows.Converters.FloatVisibilityConverter.Positives" />, <c>0.0f</c> en caso contrario.
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Visibility v) return v == Positives ? 1.0f : 0.0f;
            throw new InvalidCastException();
        }
    }
}