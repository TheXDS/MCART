/*
AnyContentVisibilityConverter.cs

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

using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using System;
using System.Linq;
using System.Windows;

namespace TheXDS.MCART.ValueConverters
{
    /// <summary>
    /// Para un <see cref="ContentControl" /> o un <see cref="Panel" />,
    /// obtiene un valor <see cref="Visibility.Visible" /> si al menos un
    /// control hijo directo es visible.
    /// </summary>
    public sealed class AnyContentVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// Obtiene un valor <see cref="Visibility.Visible" /> si al menos un
        /// control hijo directo es visible.
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
        /// Un nuevo <see cref="Visibility.Visible" /> si al
        /// menos un hijo directo del control es visible,
        /// <see cref="Visibility.Collapsed" /> en caso contrario.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value switch
            {
                Decorator decorator => decorator.Child?.Visibility ?? Visibility.Collapsed,
                ContentControl contentControl => (contentControl.Content as FrameworkElement)?.Visibility ??
                                                 Visibility.Collapsed,
                Panel panel => panel.Children.Cast<object?>()
                    .Any(j => (j as FrameworkElement)?.Visibility == Visibility.Visible)
                    ? Visibility.Visible
                    : Visibility.Collapsed,
                _ => Visibility.Visible
            };
        }

        /// <summary>Convierte un valor.</summary>
        /// <param name="value">
        /// Valor generado por el destino de enlace.
        /// </param>
        /// <param name="targetType">Tipo al que se va a convertir.</param>
        /// <param name="parameter">
        /// Parámetro de convertidor que se va a usar.
        /// </param>
        /// <param name="culture">
        /// Referencia cultural que se va a usar en el convertidor.
        /// </param>
        /// <returns>
        /// Valor convertido.
        /// Si el método devuelve <see langword="null" />, se usa el valor nulo válido.
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new InvalidOperationException();
        }
    }
}