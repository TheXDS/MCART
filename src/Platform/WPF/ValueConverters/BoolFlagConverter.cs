/*
BoolFlagConverter.cs

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
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.ValueConverters
{
    /// <summary>
    /// Clase base para crear convertidores de valores booleanos que analizan
    /// banderas de una enumeración.
    /// </summary>
    /// <typeparam name="T">
    /// Tipo de valores a convertir. Deben ser enumeraciones.
    /// </typeparam>
    public class BoolFlagConverter<T> : IValueConverter
    {
        /// <summary>
        /// Obtiene o establece el valor que equivale a <see langword="true" /> en este
        /// <see cref="BoolFlagConverter{T}" />.
        /// </summary>
        public T True { get; set; }

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="BoolFlagConverter{T}" />.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Se produce si el tipo especificado al instanciar esta clase no es
        /// una enumeración.
        /// </exception>
        public BoolFlagConverter()
        {
            if (!typeof(T).IsEnum) throw new InvalidOperationException();
            True = (T)typeof(T).Default()!;
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="BoolFlagConverter{T}" />, configurando el valor que
        /// corresponderá a <see langword="true" />.
        /// </summary>
        /// <param name="trueValue">Valor equivalente a <see langword="true" />.</param>
        /// <exception cref="InvalidOperationException">
        /// Se produce si el tipo especificado al instanciar esta clase no es
        /// una enumeración.
        /// </exception>
        public BoolFlagConverter(T trueValue)
        {
            if (!typeof(T).IsEnum) throw new InvalidOperationException();
            True = trueValue;
        }

        /// <summary>
        /// Convierte un valor a <see cref="bool" />.
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
        /// Si no se ha establecido un valor para <see cref="P:System.Windows.Converters.BoolFlagConverter`1.True" />, se
        /// devolverá <see langword="true" /> si hay alguna bandera activa, o <see langword="false" />
        /// en caso contrario. Si se estableció un valor para
        /// <see cref="P:System.Windows.Converters.BoolFlagConverter`1.True" />, se devolverá <see langword="true" /> solo si
        /// dicha(s)
        /// bandera(s) se encuentra(n) activa(s), <see langword="false" /> en caso
        /// contrario.
        /// </returns>
        public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is T v) return (True?.Equals(default) ?? true) ? !v.Equals(True) : v.Equals(True);
            return null;
        }

        /// <summary>
        /// Convierte un <see cref="bool" /> al tipo establecido para este
        /// <see cref="BoolFlagConverter{T}" />.
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
        /// Si <paramref name="value" /> es <see langword="true" />, se devuelve la(s)
        /// bandera(s) a ser detectada(s), en caso de haberse establecido un
        /// valor para <see cref="P:System.Windows.Converters.BoolFlagConverter`1.True" />, o en caso contrario, se devolverá
        /// <c>default</c>.
        /// </returns>
        public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value?.Equals(true) ?? false ? True : default;
        }
    }
}