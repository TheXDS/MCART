/*
Inverter.cs

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

namespace TheXDS.MCART.ValueConverters.Base
{
    /// <inheritdoc />
    /// <summary>
    /// Clase base para crear convertidores de valores que inviertan el valor
    /// de una propiedad de dependencia.
    /// </summary>
    /// <typeparam name="T">Tipo de valor a invertir.</typeparam>
    public abstract class Inverter<T> : IValueConverter where T : struct
    {
        private readonly T _nay;
        private readonly T _yay;

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="Inverter{T}" />.
        /// </summary>
        /// <param name="yayValue">Valor invertible.</param>
        /// <param name="nayValue">
        /// Valor inverso de <paramref name="yayValue" />.
        /// </param>
        protected Inverter(T yayValue, T nayValue)
        {
            _yay = yayValue;
            _nay = nayValue;
        }

        /// <inheritdoc />
        /// <summary>
        /// Invierte el valor de  un <typeparamref name="T" />.
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
        /// Un <typeparamref name="T" /> cuyo valor es el inverso de
        /// <paramref name="value" />.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is T v)
                return v.Equals(_yay) ? _nay : _yay;
            throw new InvalidCastException();
        }

        /// <inheritdoc />
        /// <summary>
        /// Invierte el valor de  un <typeparamref name="T" />.
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
        /// Un <typeparamref name="T" /> cuyo valor es el inverso de
        /// <paramref name="value" />.
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is T v)
                return v.Equals(_yay) ? _nay : _yay;
            throw new InvalidCastException();
        }
    }
}