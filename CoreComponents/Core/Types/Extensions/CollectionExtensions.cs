/*
CollectionExtensions.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

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

using System;
using System.Collections.Generic;
using System.Linq;

namespace TheXDS.MCART.Types.Extensions
{
    /// <summary>
    ///     Extensiones para todos los elementos de tipo <see cref="ICollection{T}" />.
    /// </summary>
    public static class CollectionExtensions
    {
        /// <summary>
        ///     Elimina todos los elementos de una colección que cumplen con una condición.
        /// </summary>
        /// <typeparam name="T">Tipo de elementos en la colección.</typeparam>
        /// <param name="collection">Colección a procesar.</param>
        /// <param name="check">Función que verifica si un elemento cumple con una condición.</param>
        /// <param name="beforeDelete">Acción a ejecutar antes de borrar a un elemento en particular.</param>
        public static void RemoveAll<T>(this ICollection<T> collection, Predicate<T> check, Action<T> beforeDelete)
        {
            var lst = collection.ToList();
            foreach (var j in lst)
            {
                if (!(check?.Invoke(j) ?? true)) continue;
                beforeDelete?.Invoke(j);
                collection.Remove(j);
            }
        }

        /// <summary>
        ///     Elimina todos los elementos de una colección.
        /// </summary>
        /// <typeparam name="T">Tipo de elementos en la colección.</typeparam>
        /// <param name="collection">Colección a procesar.</param>
        /// <param name="beforeDelete">Acción a ejecutar antes de borrar a un elemento en particular.</param>
        public static void RemoveAll<T>(this ICollection<T> collection, Action<T> beforeDelete) =>
            RemoveAll(collection, null, beforeDelete);

        /// <summary>
        ///     Elimina todos los elementos de una colección que cumplen con una condición.
        /// </summary>
        /// <typeparam name="T">Tipo de elementos en la colección.</typeparam>
        /// <param name="collection">Colección a procesar.</param>
        /// <param name="check">Función que verifica si un elemento cumple con una condición.</param>
        public static void RemoveAll<T>(this ICollection<T> collection, Predicate<T> check) =>
            RemoveAll(collection, check, null);

        /// <summary>
        ///     Elimina todos los elementos de una colección.
        /// </summary>
        /// <typeparam name="T">Tipo de elementos en la colección.</typeparam>
        /// <param name="collection">Colección a procesar.</param>
        public static void RemoveAll<T>(this ICollection<T> collection) => RemoveAll(collection, null, null);

        /// <summary>
        ///     Devuelve el último elemento en la lista, quitándole.
        /// </summary>
        /// <returns>El último elemento en la lista.</returns>
        /// <param name="a">Lista de la cual obtener el elemento.</param>
        /// <typeparam name="T">
        ///     Tipo de elementos contenidos en el <see cref="IEnumerable{T}" />.
        /// </typeparam>
        public static T Pop<T>(this ICollection<T> a)
        {
            var x = a.Last();
            a.Remove(x);
            return x;
        }

        /// <summary>
        ///     Devuelve el primer elemento en la lista, quitándole.
        /// </summary>
        /// <returns>El primer elemento en la lista.</returns>
        /// <param name="a">Lista de la cual obtener el elemento.</param>
        /// <typeparam name="T">
        ///     Tipo de elementos contenidos en el <see cref="IEnumerable{T}" />.
        /// </typeparam>
        public static T PopFirst<T>(this ICollection<T> a)
        {
            var x = a.First();
            a.Remove(x);
            return x;
        }
    }
}