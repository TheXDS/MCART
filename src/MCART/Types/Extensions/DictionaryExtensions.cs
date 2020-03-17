/*
DictionaryExtensions.cs

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

using System.Collections.Generic;

namespace TheXDS.MCART.Types.Extensions
{
    /// <summary>
    /// Extensiones para todos los elementos de tipo <see cref="IDictionary{TKey, TValue}" />.
    /// </summary>
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Agrega un valor al diccionario.
        /// </summary>
        /// <typeparam name="TKey">
        /// Tipo de llave a utilizar para identificar al valor.
        /// </typeparam>
        /// <typeparam name="TValue">Tipo de valor a agregar.</typeparam>
        /// <param name="dictionary">
        /// Diccionario al cual agregar el nuevo valor.
        /// </param>
        /// <param name="key">Llave para identificar al nuevo valor.</param>
        /// <param name="value">Valor a agregar.</param>
        /// <returns>
        /// La misma instancia que <paramref name="value"/>, permitiendo
        /// utilizar sintáxis Fluent.
        /// </returns>
        public static TValue Push<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue value) where TKey : notnull
        {
            dictionary.Add(key, value);
            return value;
        }

        /// <summary>
        /// Agrega un valor al diccionario especificado.
        /// </summary>
        /// <typeparam name="TKey">
        /// Tipo de llave a utilizar para identificar al valor.
        /// </typeparam>
        /// <typeparam name="TValue">Tipo de valor a agregar.</typeparam>
        /// <param name="dictionary">
        /// Diccionario al cual agregar el nuevo valor.
        /// </param>
        /// <param name="key">Llave para identificar al nuevo valor.</param>
        /// <param name="value">Valor a agregar.</param>
        /// <returns>
        /// La misma instancia que <paramref name="value"/>, permitiendo
        /// utilizar sintáxis Fluent.
        /// </returns>
        public static TValue PushInto<TKey, TValue>(this TValue value, TKey key, IDictionary<TKey, TValue> dictionary) where TKey : notnull
        {
            dictionary.Add(key, value);
            return value;
        }

        /// <summary>
        /// Obtiene el elemento con la llave especificada del diccionario,
        /// quitándolo.
        /// </summary>
        /// <typeparam name="TKey">
        /// Tipo de llave del objeto a obtener.
        /// </typeparam>
        /// <typeparam name="TValue">
        /// Tipo de valor contenido por el diccionario.
        /// </typeparam>
        /// <param name="dict">
        /// Diccionario desde el cual obtener y remover el objeto.
        /// </param>
        /// <param name="key">Llave del objeto a obtener.</param>
        /// <param name="value">Valor obtenido del diccionario.</param>
        /// <returns>
        /// <see langword="true"/> si el objeto fue quitado satisfactoriamente
        /// del diccionario, <see langword="false"/> en caso que el diccionario
        /// no contuviese a un elemento con la llave especificada.
        /// </returns>
        public static bool Pop<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, out TValue value) where TKey : notnull
        {
            if (!dict.ContainsKey(key))
            {
                value = default!;
                return false;
            }
            value = dict[key];
            return dict.Remove(key);
        }

        /// <summary>
        /// Comprueba la existencia de referencias circulares en un
        /// diccionario de objetos en forma de árbol.
        /// </summary>
        /// <typeparam name="T">
        /// Tipo de elementos contenidos en el diccionario.
        /// </typeparam>
        /// <param name="dictionary">
        /// Diccionario en el cual realizar la comprobación.
        /// </param>
        /// <param name="element">
        /// Elemento a comprobar.
        /// </param>r
        /// <returns>
        /// <see langword="false"/> si no existen referencias circulares 
        /// dentro del diccionario, <see langword="true"/> en caso 
        /// contrario.
        /// </returns>
        public static bool CheckCircularRef<T>(this IDictionary<T, IEnumerable<T>> dictionary, T element) where T : notnull
        {
            return BranchScanFails(element, element, dictionary, new HashSet<T>());
        }

        /// <summary>
        /// Comprueba la existencia de referencias circulares en un
        /// diccionario de objetos.
        /// </summary>
        /// <typeparam name="T">
        /// Tipo de elementos contenidos en el diccionario.
        /// </typeparam>
        /// <param name="dictionary">
        /// Diccionario en el cual realizar la comprobación.
        /// </param>
        /// <param name="element">
        /// Elemento a comprobar.
        /// </param>
        /// <returns>
        /// <see langword="false"/> si no existen referencias circulares 
        /// dentro del diccionario, <see langword="true"/> en caso 
        /// contrario.
        /// </returns>
        public static bool CheckCircularRef<T>(this IDictionary<T, ICollection<T>> dictionary, T element) where T : notnull
        {
            return BranchScanFails(element, element, dictionary, new HashSet<T>());
        }

        /// <summary>
        /// Comprueba la existencia de referencias circulares en un
        /// diccionario de objetos.
        /// </summary>
        /// <typeparam name="T">
        /// Tipo de elementos contenidos en el diccionario.
        /// </typeparam>
        /// <param name="dictionary">
        /// Diccionario en el cual realizar la comprobación.
        /// </param>
        /// <param name="element">
        /// Elemento a comprobar.
        /// </param>
        /// <returns>
        /// <see langword="false"/> si no existen referencias circulares 
        /// dentro del diccionario, <see langword="true"/> en caso 
        /// contrario.
        /// </returns>
        public static bool CheckCircularRef<T>(this IEnumerable<KeyValuePair<T, IEnumerable<T>>> dictionary, T element) where T : notnull
        {
            var d = new Dictionary<T, IEnumerable<T>>();
            foreach (var j in dictionary) d.Add(j.Key, j.Value);
            return BranchScanFails(element, element, d, new HashSet<T>());
        }

        /// <summary>
        /// Comprueba la existencia de referencias circulares en un
        /// diccionario de objetos.
        /// </summary>
        /// <typeparam name="T">
        /// Tipo de elementos contenidos en el diccionario.
        /// </typeparam>
        /// <param name="dictionary">
        /// Diccionario en el cual realizar la comprobación.
        /// </param>
        /// <param name="element">
        /// Elemento a comprobar.
        /// </param>
        /// <returns>
        /// <see langword="false"/> si no existen referencias circulares 
        /// dentro del diccionario, <see langword="true"/> en caso 
        /// contrario.
        /// </returns>
        public static bool CheckCircularRef<T>(this IEnumerable<KeyValuePair<T, ICollection<T>>> dictionary, T element) where T : notnull
        {
            var d = new Dictionary<T, IEnumerable<T>>();
            foreach (var j in dictionary) d.Add(j.Key, j.Value);
            return BranchScanFails(element, element, d, new HashSet<T>());
        }

        private static bool BranchScanFails<T>(T a, T b, IDictionary<T, IEnumerable<T>> tree, ICollection<T> keysChecked) where T : notnull
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

        private static bool BranchScanFails<T>(T a, T b, IDictionary<T, ICollection<T>> tree, ICollection<T> keysChecked) where T : notnull
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