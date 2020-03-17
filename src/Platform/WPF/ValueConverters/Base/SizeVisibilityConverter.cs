/*
SizeVisibilityConverter.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2019 César Andrés Morgan

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
using System.Windows;

namespace TheXDS.MCART.ValueConverters
{
    /// <inheritdoc />
    /// <summary>
    /// Clase base para convertidores de valores que tomen un valor <see cref="Size" /> para determinar un
    /// valor de tipo <see cref="Visibility" /> basado en un umbral.
    /// </summary>
    public abstract class SizeVisibilityConverter : IValueConverter
    {
        private readonly Visibility _above;
        private readonly Visibility _below;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="SizeVisibilityConverter" />.
        /// </summary>
        /// <param name="below">Valor a devolver por debajo del umbral.</param>
        /// <param name="above">Valor a devolver por encima del umbral.</param>
        protected SizeVisibilityConverter(Visibility below, Visibility above)
        {
            _below = below;
            _above = above;
        }

        /// <inheritdoc />
        /// <summary>
        /// Convierte un <see cref="Size" /> a un <see cref="Visibility" /> basado en un valor de umbral.
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
        /// El valor de <see cref="Visibility" /> calculado a partir del tamaño especificado.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!targetType.IsAssignableFrom(typeof(Visibility))) throw new InvalidCastException();
            Size size;
            switch (parameter)
            {
                case string str:
                    try
                    {
                        size = System.Windows.Size.Parse(str);
                    }
                    catch (Exception e)
                    {
                        throw new ArgumentException(string.Empty, nameof(parameter), e);
                    }

                    break;
                case Size sz:
                    size = sz;
                    break;
                case double d:
                    size = new Size(d, d);
                    break;
                case int d:
                    size = new Size(d, d);
                    break;
                default:
                    throw new ArgumentException(string.Empty, nameof(parameter));
            }

            return value switch
            {
                FrameworkElement f => f.ActualWidth < size.Width && f.ActualHeight < size.Height ? _below : _above,
                Size sz => sz.Width < size.Width && sz.Height < size.Height ? _below : _above,

                _ => _above,
            };
        }

        /// <inheritdoc />
        /// <summary>
        /// Infiere un valor basado en el <see cref="Visibility" /> provisto.
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
        /// Un valor de <see cref="Size" /> inferido a partir del valor de entrada.
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is Visibility v && v == _above ? parameter : new Size();
        }
    }
}