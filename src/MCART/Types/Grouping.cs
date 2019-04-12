/*
Grouping.cs

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

using System.Linq;
using System.Collections.Generic;

namespace TheXDS.MCART.Types
{
    /// <summary>
    ///     Representa una colección fuertemente tipeada de elementos que
    ///     comparten una llave única.
    /// </summary>
    /// <typeparam name="TKey">
    ///     Tipo de llave a utilizar.
    /// </typeparam>
    /// <typeparam name="TElement">
    ///     Tipo de elementos de la colección.
    /// </typeparam>
    public class Grouping<TKey, TElement> : List<TElement>, IGrouping<TKey, TElement>
    {
        /// <summary>
        ///     Llave asociada a este grupo de elementos.
        /// </summary>
        public TKey Key { get; }

        /// <summary>
        ///     Inicializa una nueva instancia de la clase 
        ///     <see cref="Grouping{TKey, TElement}"/>, estableciendo la llave a
        ///     utilizar.
        /// </summary>
        /// <param name="key">
        ///     Llave asociada a este grupo de elementos.
        /// </param>
        public Grouping(TKey key)
        {
            Key = key;
        }

        /// <summary>
        ///     Inicializa una nueva instancia de la clase 
        ///     <see cref="Grouping{TKey, TElement}"/>, estableciendo la llave a
        ///     utilizar para identificar a los elementos especificados.
        /// </summary>
        /// <param name="key">
        ///     Llave asociada a este grupo de elementos.
        /// </param>
        /// <param name="collection">
        ///     Colección a la cual asociar la llave especificada.
        /// </param>
        public Grouping(TKey key, IEnumerable<TElement> collection) : base(collection)
        {
            Key = key;

        }

        /// <summary>
        ///     Inicializa una nueva instancia de la clase 
        ///     <see cref="Grouping{TKey, TElement}"/>, estableciendo la llave a
        ///     utilizar, además de definir la capacidad de la colección.
        /// </summary>
        /// <param name="key">
        ///     Llave asociada a este grupo de elementos.
        /// </param>
        /// <param name="capacity">
        ///     Capacidad inicial de la colección subyacente.
        /// </param>
        public Grouping(TKey key, int capacity) : base(capacity)
        {
            Key = key;
        }

        /// <summary>
        ///     Convierte implícitamente un
        ///     <see cref="Grouping{TKey, TElement}"/> en un
        ///     <see cref="KeyValuePair{TKey, TValue}"/>.
        /// </summary>
        /// <param name="grouping">Objeto a convertir.</param>
        public static implicit operator KeyValuePair<TKey, IEnumerable<TElement>>(Grouping<TKey, TElement> grouping)
        {
            return new KeyValuePair<TKey, IEnumerable<TElement>>(grouping.Key, grouping);
        }
    }
}
