/*
BrushOpacityAdjust.cs

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
using System.Windows.Media;
using TheXDS.MCART.Helpers;

namespace TheXDS.MCART.ValueConverters
{
    /// <summary>
    /// Permite compartir un recurso de <see cref="Brush" /> entre controles,
    /// ajustando la opacidad del enlace de datos.
    /// </summary>
    public sealed class BrushOpacityAdjust : IValueConverter
    {
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
            if (value is not Brush brush) return null;
            var opacity = (double)System.Convert.ChangeType(parameter, typeof(double));
            if (!opacity.IsBetween(0, 1)) throw new ArgumentOutOfRangeException(nameof(opacity));
            var b = brush.Clone();
            b.Opacity = opacity;
            return b;
        }

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
            if (value is not Brush brush) return null;
            var b = brush.Clone();
            b.Opacity = 1;
            return b;
        }
    }
}