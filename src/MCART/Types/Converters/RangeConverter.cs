/*
RangeConverter.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Este archivo define la estructura Range<T>, la cual permite representar rangos
de valores.

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

using System;

namespace TheXDS.MCART.Types.Converters
{
    /// <inheritdoc />
    /// <summary>
    ///     Clase base para los convertidores de valor que permitan obtener
    ///     objetos de tipo <see cref="T:TheXDS.MCART.Types.Range`1" /> a partir de una cadena.
    /// </summary>
    /// <typeparam name="T">
    ///     Tipo del rango a obtener.
    /// </typeparam>
    public abstract class RangeConverter<T> : BasicParseConverter<Range<T>> where T : IComparable<T>
    {
        /// <inheritdoc />
        /// <summary>
        ///     Ejecuta la conversión de la cadena al tipo de este
        ///     <see cref="T:TheXDS.MCART.Types.Converters.BasicParseConverter`1"/>.
        /// </summary>
        /// <param name="value">Cadena a convertir.</param>
        /// <returns>
        ///     Un valor de tipo <see cref="Range{T}"/> creado a partir de la
        ///     cadena especificada.
        /// </returns>
        protected override Range<T> ConvertFrom(string value)
        {
            return Range<T>.Parse(value);
        }
    }
}