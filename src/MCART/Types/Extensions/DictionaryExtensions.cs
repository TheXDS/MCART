/*
DictionaryExtensions.cs

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

using System.Collections.Generic;

namespace TheXDS.MCART.Types.Extensions
{
    /// <summary>
    ///     Extensiones para todos los elementos de tipo <see cref="IDictionary{TKey, TValue}" />.
    /// </summary>
    public static class DictionaryExtensions
    {
        /// <summary>
        ///     Comprueba la existencia de referencias circulares en un
        ///     diccionario de objetos.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dictionary"></param>
        /// <param name="element"></param>
        /// <returns></returns>
        public static bool CheckCircularRef<T>(this IDictionary<T, IEnumerable<T>> dictionary, T element)
        {
            return BranchScanFails(element, element, dictionary, new HashSet<T>());
        }
        public static bool CheckCircularRef<T>(this IDictionary<T, ICollection<T>> dictionary, T element)
        {
            return BranchScanFails(element, element, dictionary, new HashSet<T>());
        }

        /// <summary>
        ///     Comprueba la existencia de referencias circulares en un
        ///     diccionario de objetos.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dictionary"></param>
        /// <param name="element"></param>
        /// <returns></returns>
        public static bool CheckCircularRef<T>(this IEnumerable<KeyValuePair<T, IEnumerable<T>>> dictionary, T element)
        {
            var d = new Dictionary<T, IEnumerable<T>>();
            foreach (var j in dictionary) d.Add(j.Key, j.Value);
            return BranchScanFails(element, element, d, new HashSet<T>());
        }

        public static bool CheckCircularRef<T>(this IEnumerable<KeyValuePair<T, ICollection<T>>> dictionary, T element)
        {
            var d = new Dictionary<T, IEnumerable<T>>();
            foreach (var j in dictionary) d.Add(j.Key, j.Value);
            return BranchScanFails(element, element, d, new HashSet<T>());
        }

        private static bool BranchScanFails<T>(T a, T b, IDictionary<T, IEnumerable<T>> tree, ICollection<T> keysChecked)
        {
            if (!tree.ContainsKey(b)) return false;
            foreach (var j in tree[b])
            {
                if (keysChecked.Contains(j)) return false;
                keysChecked.Add(j);
                if (j.Equals(a)) return true;
                if (BranchScanFails(a, j, tree, keysChecked)) return true;
            }
            return false;
        }

        private static bool BranchScanFails<T>(T a, T b, IDictionary<T, ICollection<T>> tree, ICollection<T> keysChecked)
        {
            if (!tree.ContainsKey(b)) return false;
            foreach (var j in tree[b])
            {
                if (keysChecked.Contains(j)) return false;
                keysChecked.Add(j);
                if (j.Equals(a)) return true;
                if (BranchScanFails(a, j, tree, keysChecked)) return true;
            }
            return false;
        }
    }
}