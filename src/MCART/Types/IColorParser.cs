/*
IColorParser.cs

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

#nullable enable

namespace TheXDS.MCART.Types
{
    /// <summary>
    /// Define una serie de métodos a implementar por una clase que permita
    /// convertir un valor en un <see cref="Color" />.
    /// </summary>
    /// <typeparam name="T">Tipo de valor a convertir.</typeparam>
    public interface IColorParser<T> where T : struct
    {
        /// <summary>
        /// Convierte un <typeparamref name="T" /> en un
        /// <see cref="Color" />.
        /// </summary>
        /// <param name="value">Valor a convertir.</param>
        /// <returns>
        /// Un <see cref="Color" /> creado a partir del valor.
        /// </returns>
        Color From(T value);

        /// <summary>
        /// Convierte un <see cref="Color" /> en un valor de tipo
        /// <typeparamref name="T" />.
        /// </summary>
        /// <param name="color"><see cref="Color" /> a convertir.</param>
        /// <returns>
        /// Un valor de tipo <typeparamref name="T" /> creado a partir del
        /// <see cref="Color" /> especificado.
        /// </returns>
        T To(Color color);
    }
}