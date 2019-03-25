/*
Grouping.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright (c) 2011 - 2019 César Andrés Morgan

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
    /// <typeparam name="TValue">
    ///     Tipo de elementos de la colección.
    /// </typeparam>
    public class Grouping<TKey, TValue> : List<TValue>, IGrouping<TKey, TValue>
    {
        public TKey Key { get; }

        public Grouping(TKey key)
        {
            Key = key;
        }

        public Grouping(TKey key, IEnumerable<TValue> collection) : base(collection)
        {
            Key = key;

        }

        public Grouping(TKey key, int capacity) : base(capacity)
        {
            Key = key;
        }

        public static implicit operator KeyValuePair<TKey, IEnumerable<TValue>>(Grouping<TKey, TValue> grouping)
        {
            return new KeyValuePair<TKey, IEnumerable<TValue>>(grouping.Key, grouping);
        }
    }
}
