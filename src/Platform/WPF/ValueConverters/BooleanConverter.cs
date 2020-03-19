/*
BooleanConverter.cs

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
using System;

namespace TheXDS.MCART.ValueConverters
{
    /// <summary>
    /// Clase base para crear convertidores de valores booleanos.
    /// </summary>
    /// <typeparam name="T">
    /// Tipo de valores a convertir. Deben ser estructuras o enumeraciones.
    /// </typeparam>
    public class BooleanConverter<T> : IValueConverter where T : struct
    {
        /// <summary>
        /// Obtiene o establece el valor que equivale a <see langword="false" /> en este
        /// <see cref="BooleanConverter{T}" />.
        /// </summary>
        public T False { get; set; }

        /// <summary>
        /// Obtiene o establece el valor que equivale a <see langword="true" /> en este
        /// <see cref="BooleanConverter{T}" />.
        /// </summary>
        public T True { get; set; }

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="BooleanConverter{T}" />, configurando los valores que
        /// corresponderán a <see langword="true" /> y <see langword="false" />.
        /// </summary>
        /// <param name="trueValue">Valor equivalente a <see langword="true" />.</param>
        /// <param name="falseValue">Valor equivalente a <see langword="false" />.</param>
        public BooleanConverter(T trueValue, T falseValue = default)
        {
            True = trueValue;
            False = falseValue;
        }

        /// <summary>
        /// Convierte un <see cref="bool" /> a un objeto.
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
        /// <see cref="P:System.Windows.Converters.BooleanConverter`1.True" />, si el objeto es de tipo
        /// <see cref="bool" /> y su
        /// valor es <see langword="true" />; en caso contrario, se devuelve
        /// <see cref="P:System.Windows.Converters.BooleanConverter`1.False" />.
        /// </returns>
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo? culture)
        {
            if (value is bool b) return b ? True : False;
            return null;
        }

        /// <summary>
        /// Convierte un objeto en un <see cref="bool" />.
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
        /// <see langword="true" /> si el objeto es igual a <see cref="P:System.Windows.Converters.BooleanConverter`1.True" />;
        /// <see langword="false" /> en caso contrario.
        /// </returns>
        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo? culture)
        {
            if (value?.Equals(True) ?? false) return true;
            if (value?.Equals(False) ?? false) return false;
            return null;
        }
    }
}