/*
McartColor2IntConverter.cs

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
using System.Windows.Data;
using TheXDS.MCART.Types;
using TheXDS.MCART.ValueConverters.Base;
using MT = Types;

/// <summary>
/// Convierte valores desde y hacia objetos de tipo
/// <see cref="Color"/> y <see cref="int"/>.
/// </summary>
public sealed class McartColor2IntConverter : IValueConverter<Color, int>
{
    /// <summary>
    /// Convierte un <see cref="Color"/> en un <see cref="int"/>.
    /// </summary>
    /// <param name="value">Objeto a convertir.</param>
    /// <param name="parameter">
    /// Parámetros personalizados para este <see cref="IValueConverter" />.
    /// </param>
    /// <param name="culture">
    /// <see cref="CultureInfo" /> a utilizar para la conversión.
    /// </param>
    /// <returns>
    /// Un <see cref="int"/> equivalente al <see cref="Color"/> original.
    /// </returns>
    public int Convert(Color value, object? parameter, CultureInfo? culture)
    {
        return new Abgr32ColorParser().To(value);
    }

    /// <summary>
    /// Convierte un <see cref="int"/> en un 
    /// <see cref="Color"/>.
    /// </summary>
    /// <param name="value">Objeto a convertir.</param>
    /// <param name="parameter">
    /// Parámetros personalizados para este <see cref="IValueConverter" />.
    /// </param>
    /// <param name="culture">
    /// <see cref="CultureInfo" /> a utilizar para la conversión.
    /// </param>
    /// <returns>
    /// Un <see cref="Color"/> creado a partir del
    /// valor del objeto, o <see langword="null"/> si no es posible
    /// realizar la conversión.
    /// </returns>
    public Color ConvertBack(int value, object? parameter, CultureInfo culture)
    {
        return new Abgr32ColorParser().From(value);
    }
}
