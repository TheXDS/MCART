/*
AllVisibleConverter.cs

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

namespace TheXDS.MCART.ValueConverters;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using System;
using System.Windows;

/// <summary>
/// Determina un valor de visibilidad basado en que todos los valores
/// sean igual a <see cref="Visibility.Visible" />.
/// </summary>
public sealed class AllVisibleConverter : IMultiValueConverter
{
    /// <summary>
    /// Determina un valor de visibilidad basado en que todos los
    /// valores sean igual a
    /// <see cref="Visibility.Visible" />.
    /// </summary>
    /// <param name="values">
    /// Valores generados por los orígenes de enlace.
    /// </param>
    /// <param name="targetType">
    /// El tipo de la propiedad del destino de enlace.
    /// </param>
    /// <param name="parameter">
    /// Parámetro de convertidor que se va a usar.
    /// </param>
    /// <param name="culture">
    /// Referencia cultural que se va a usar en el convertidor.
    /// </param>
    /// <returns>
    /// <see cref="Visibility.Visible" /> si todos los
    /// valores son igual a
    /// <see cref="Visibility.Visible" />,
    /// <see cref="Visibility.Collapsed" /> en caso contrario.
    /// </returns>
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        return values.OfType<Visibility>().All(p => p == Visibility.Visible)
            ? Visibility.Visible
            : Visibility.Collapsed;
    }

    /// <summary>
    /// Implementa <see cref="IValueConverter.ConvertBack" />.
    /// </summary>
    /// <param name="value">Objeto a convertir.</param>
    /// <param name="targetTypes">Tipo del destino.</param>
    /// <param name="parameter">
    /// Parámetros personalizados para este <see cref="IValueConverter" />.
    /// </param>
    /// <param name="culture">
    /// <see cref="CultureInfo" /> a utilizar para la conversión.
    /// </param>
    /// <returns>
    /// Este método siempre genera un <see cref="InvalidOperationException" />.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Este método siempre genera esta excepción al ser llamado.
    /// </exception>
    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new InvalidOperationException();
    }
}
