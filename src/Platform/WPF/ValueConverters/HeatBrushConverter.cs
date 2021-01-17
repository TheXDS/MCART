/*
HeatBrushConverter.cs

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
using System.Drawing;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using TheXDS.MCART.Types.Extensions;
using TheXDS.MCART.ValueConverters.Base;
using static TheXDS.MCART.Math.Common;

namespace TheXDS.MCART.ValueConverters
{
    /// <summary>
    /// Obtiene un <see cref="System.Windows.Media.Brush" /> correspondiente a la salud expresada
    /// com porcentaje.
    /// </summary>
    public sealed class HeatBrushConverter : FloatConverterBase, IValueConverter
    {
        /// <summary>Convierte un valor.</summary>
        /// <param name="value">
        ///   Valor generado por el origen de enlace.
        /// </param>
        /// <param name="targetType">
        ///   El tipo de la propiedad del destino de enlace.
        /// </param>
        /// <param name="parameter">
        ///   Parámetro de convertidor que se va a usar.
        /// </param>
        /// <param name="culture">
        ///   Referencia cultural que se va a usar en el convertidor.
        /// </param>
        /// <returns>
        ///   Valor convertido.
        ///    Si el método devuelve <see langword="null" />, se usa el valor nulo válido.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Types.Color.BlendHeat(GetFloat(value).Clamp(0f, 1f)).Brush();
        }

        /// <summary>Convierte un valor.</summary>
        /// <param name="value">
        ///   Valor generado por el destino de enlace.
        /// </param>
        /// <param name="targetType">Tipo al que se va a convertir.</param>
        /// <param name="parameter">
        ///   Parámetro de convertidor que se va a usar.
        /// </param>
        /// <param name="culture">
        ///   Referencia cultural que se va a usar en el convertidor.
        /// </param>
        /// <returns>
        ///   Valor convertido.
        ///    Si el método devuelve <see langword="null" />, se usa el valor nulo válido.
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new InvalidOperationException();
        }
    }
}