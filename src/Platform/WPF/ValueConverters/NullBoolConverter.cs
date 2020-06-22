/*
NullBoolConverter.cs

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
using System.Windows.Data;

namespace TheXDS.MCART.ValueConverters
{
    /// <summary>
    /// Clase base para crear convertidores de valores booleanos que pueden ser
    /// <see langword="null" />.
    /// </summary>
    /// <typeparam name="T">Tipo de valores a convertir.</typeparam>
    public sealed class NullBoolConverter<T> : IValueConverter
    {
        /// <summary>
        /// Obtiene o establece el valor que equivale a <see langword="false" /> en este
        /// <see cref="NullBoolConverter{T}" />.
        /// </summary>
        public T False { get; set; }

        /// <summary>
        /// Obtiene o establece el valor que equivale a <see langword="null" /> en este
        /// <see cref="NullBoolConverter{T}" />.
        /// </summary>
        public T Null { get; set; }

        /// <summary>
        /// Obtiene o establece el valor que equivale a <see langword="true" /> en este
        /// <see cref="NullBoolConverter{T}" />.
        /// </summary>
        public T True { get; set; }

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="NullBoolConverter{T}" />, configurando los valores que
        /// corresponderán a <see langword="true" /> y <see langword="false" />.
        /// </summary>
        /// <param name="trueValue">Valor equivalente a <see langword="true" />.</param>
        /// <param name="falseValue">Valor equivalente a <see langword="false" />.</param>
        public NullBoolConverter(T trueValue, T falseValue = default)
        {
            True = trueValue;
            False = falseValue;
            Null = False;
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="NullBoolConverter{T}" />, configurando los valores que
        /// corresponderán a <see langword="true" /> y <see langword="false" />.
        /// </summary>
        /// <param name="trueValue">Valor equivalente a <see langword="true" />.</param>
        /// <param name="falseValue">Valor equivalente a <see langword="false" />.</param>
        /// <param name="nullValue">Valor equivalente a <see langword="null" />.</param>
        public NullBoolConverter(T trueValue, T falseValue, T nullValue)
        {
            True = trueValue;
            False = falseValue;
            Null = nullValue;
        }

        /// <summary>
        /// Convierte un valor a <see cref="Nullable{T}" />.
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
        /// <see cref="NullBoolConverter{T}.True" /> si <paramref name="value" /> es
        /// <see langword="true" />,
        /// <see cref="NullBoolConverter{T}.False" /> si <paramref name="value" /> es
        /// <see langword="false" />,
        /// <see cref="NullBoolConverter{T}.Null" /> si <paramref name="value" /> es
        /// <see langword="null" />.
        /// </returns>
        public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value switch
            {
                bool b => b ? True : False,
                null => Null,
                _ => default!,
            };
        }

        /// <summary>
        /// Convierte un <see cref="bool" /> al tipo establecido para este
        /// <see cref="BooleanConverter{T}" />.
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
        /// <see langword="true" /> si <paramref name="value" /> es
        /// <see cref="NullBoolConverter{T}.True" />,
        /// <see langword="false" /> si <paramref name="value" /> es
        /// <see cref="NullBoolConverter{T}.False" />,
        /// <see langword="null" /> si <paramref name="value" /> es
        /// <see cref="NullBoolConverter{T}.Null" />.
        /// </returns>
        public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null) return null;
            if (value.Equals(Null) && !ReferenceEquals(Null, False)) return null;
            return ((T) value).Equals(True);
        }
    }
}