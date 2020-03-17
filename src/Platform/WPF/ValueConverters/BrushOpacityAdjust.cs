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
using System.Windows.Media;
using System.Drawing;
using System;

namespace TheXDS.MCART.ValueConverters
{
    /// <inheritdoc />
    /// <summary>
    /// Permite compartir un recurso de <see cref="Brush" /> entre controles,
    /// ajustando la opacidad del enlace de datos.
    /// </summary>
    public sealed class BrushOpacityAdjust : IValueConverter
    {
        /// <inheritdoc />
        /// <summary>
        /// Aplica la nueva opacidad al <see cref="Brush" />.
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
        /// Un nuevo <see cref="Brush" /> con la opacidad establecida en este
        /// <see cref="BrushOpacityAdjust" />.
        /// </returns>
        public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is System.Windows.Media.Brush brush)) return null;
            if (!(parameter is double opacity || !double.TryParse(value.ToString(), out opacity)))
                throw new ArgumentException(string.Empty, nameof(parameter));
            if (!opacity.IsBetween(0, 1)) throw new ArgumentOutOfRangeException(nameof(opacity));
            var b = brush.Clone();
            b.Opacity = opacity;
            return b;
        }

        /// <inheritdoc />
        /// <summary>
        /// Devuelve un <see cref="Brush" /> con 100% opacidad.
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
        /// Un nuevo <see cref="Brush" /> con la opacidad al 100%.
        /// </returns>
        public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is System.Windows.Media.Brush brush)) return null;
            var b = brush.Clone();
            b.Opacity = 1;
            return b;
        }
    }
}