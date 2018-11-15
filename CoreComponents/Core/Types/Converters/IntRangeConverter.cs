/*
BasicParseConverter.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Este archivo define la estructura Range<T>, la cual permite representar rangos
de valores.

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright (c) 2011 - 2018 César Andrés Morgan

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

namespace TheXDS.MCART.Types.Converters
{
    /// <inheritdoc />
    /// <summary>
    ///     Permite realizar la conversión entre tipos
    ///     <see cref="T:System.String" /> y
    ///     <see cref="T:TheXDS.MCART.Types.Range`1" /> para rangos de tipo
    ///     <see cref="T:System.Int32" />.
    /// </summary>
    public class IntRangeConverter : BasicParseConverter<Range<int>>
    {
        /// <inheritdoc />
        /// <summary>
        ///     Ejecuta la conversión de la cadena al tipo de este
        ///     <see cref="T:TheXDS.MCART.Types.Converters.BasicParseConverter`1"/>.
        /// </summary>
        /// <param name="value">Cadena a convertir.</param>
        /// <returns>
        ///     Un valor de tipo <typeparamref name="T" /> creado a partir de la cadena especificada.
        /// </returns>
        protected override Range<int> ConvertFrom(string value)
        {
            return Range<int>.Parse(value);
        }
    }
}