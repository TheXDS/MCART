/*
BooleanToBlurEffectConverter.cs

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
using System.Windows.Media.Effects;

namespace TheXDS.MCART.ValueConverters
{
    /// <summary>
    /// Genera un efecto de desenfoque basado en una condición booleana.
    /// </summary>
    public sealed class BooleanToBlurEffectConverter : IValueConverter
    {
        /// <summary>
        /// Genera un efecto de desenfoque basado en una condición
        /// booleana.
        /// </summary>
        /// <param name="value">
        /// Valor generado por el origen del enlace.
        /// </param>
        /// <param name="targetType">
        /// El tipo de la propiedad del destino del enlace.
        /// </param>
        /// <param name="parameter">
        /// Parámetro de convertidor que se va a usar.
        /// </param>
        /// <param name="culture">
        /// Referencia cultural que se va a usar en el convertidor.
        /// </param>
        /// <returns></returns>
        public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double TryConvert()
            {
                try
                {
                    return (double)System.Convert.ChangeType(parameter, typeof(double));
                }
                catch
                {
#if PreferExceptions
                    throw new ArgumentException(nameof(parameter));
#else
                    return 5.0;
#endif
                }
            }

            return (value is bool b && b) ? new BlurEffect
            {
                Radius = parameter switch
                {
                    null => 5.0,
                    string s => double.TryParse(s, out var r) ? r : throw new ArgumentException(nameof(parameter)),
                    _ => TryConvert()
                }
            } : null;
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
        /// <returns>
        /// <see langword="true" /> si el valor es un <see cref="BlurEffect" />,
        /// <see langword="false" /> en caso contrario.
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is BlurEffect;
        }
    }
}