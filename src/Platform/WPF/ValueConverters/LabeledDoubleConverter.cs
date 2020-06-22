/*
LabeledDoubleConverter.cs

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

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace TheXDS.MCART.ValueConverters
{
    /// <summary>
    /// Convierte un <see cref="double" /> en un <see cref="string" />,
    /// opcionalmente mostrando una etiqueta si el valor es inferior a cero.
    /// </summary>
    public sealed class LabeledDoubleConverter : IValueConverter
    {
        /// <summary>
        /// Convierte un <see cref="double" /> en un <see cref="string" />,
        /// opcionalmente mostrando una etiqueta si el valor es inferior a cero.
        /// </summary>
        /// <param name="value">Objeto a convertir.</param>
        /// <param name="targetType">Tipo del destino.</param>
        /// <param name="parameter">
        /// Etiqueta a mostrar en caso que el valor sea inferior a cero.
        /// </param>
        /// <param name="culture">
        /// <see cref="CultureInfo" /> a utilizar para la conversión.
        /// </param>
        /// <returns>
        /// Un <see cref="Thickness" /> uniforme cuyos valores de grosor son
        /// iguales al valor especificado.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double v)
            {
                if (parameter is null) parameter = v.ToString(CultureInfo.InvariantCulture);
                if (parameter is string label)
                    return v > 0 ? v.ToString(CultureInfo.InvariantCulture) : label;
            }

            throw new InvalidCastException();
        }

        /// <summary>
        /// Revierte la conversión realizada por este objeto.
        /// </summary>
        /// <param name="value">Objeto a convertir.</param>
        /// <param name="targetType">Tipo del destino.</param>
        /// <param name="parameter">
        /// Función opcional de transformación de valor.
        /// </param>
        /// <param name="culture">
        /// <see cref="CultureInfo" /> a utilizar para la conversión.
        /// </param>
        /// <returns>
        /// Un <see cref="double" /> cuyo valor es el promedio del grosor
        /// establecido en el <see cref="Thickness" /> especificado.
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value switch
            {
                double v => v,
                string s => double.TryParse(s, out var r) ? r : 0.0,
                _ => throw new InvalidCastException(),
            };
        }
    }
}