/*
ValueConverters.cs

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
using System.Windows.Data;
using System;

namespace TheXDS.MCART.ValueConverters
{
    /// <summary>
    /// Obtiene un objeto solamente si el valor original a convertir es 
    /// igual a <see langword="true"/>.
    /// </summary>
    public sealed class BooleanToObjectConverter : IValueConverter
    {
        /// <summary>
        /// Devuelve un objeto de acuerdo a un valor booleano.
        /// </summary>
        /// <param name="value">
        /// Valor booleano a comprobar.
        /// </param>
        /// <param name="targetType">
        /// Tipo objetivo de la conversión.
        /// </param>
        /// <param name="parameter">
        /// Objeto a devolver si <c><paramref name="value"/> == <see langword="true"/></c>.
        /// </param>
        /// <param name="culture">
        /// Cultura a utilizar durante la conversión.
        /// </param>
        /// <returns>
        /// <paramref name="parameter"/> si <c><paramref name="value"/> == <see langword="true"/></c>,
        /// <see langword="null"/> en caso contrario.
        /// </returns>
        public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value is bool b && b && targetType.IsInstanceOfType(parameter)) ? parameter : null;
        }

        /// <summary>
        /// Devuelve <see langword="true"/> si un objeto no es 
        /// <see langword="null"/>.
        /// </summary>
        /// <param name="value">
        /// Objeto a comprobar.
        /// </param>
        /// <param name="targetType">
        /// Tipo objetivo.
        /// </param>
        /// <param name="parameter">
        /// Parámetro de conversión.
        /// </param>
        /// <param name="culture">
        /// Cultura a utilizar durante la conversión.
        /// </param>
        /// <returns>
        /// <see langword="true"/> si el objeto no es
        /// <see langword="null"/>, <see langword="false"/> en caso
        /// contrario.
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is not null;
        }
    }
}