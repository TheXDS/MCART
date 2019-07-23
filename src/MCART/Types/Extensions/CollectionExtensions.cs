/*
CollectionExtensions.cs

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

using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace TheXDS.MCART.Types.Extensions
{
    /// <summary>
    ///     Extensiones para todos los elementos de tipo <see cref="ICollection{T}" />.
    /// </summary>
    public static class CollectionExtensions
    {
        /// <summary>
        ///     Quita todos los elementos del tipo especificado de la
        ///     colección.
        /// </summary>
        /// <typeparam name="TItem">
        ///     Tipo de elementos contenidos en la colección.
        /// </typeparam>
        /// <typeparam name="TRemove">Tipo de elementos a remover.</typeparam>
        /// <param name="collection">Colección de la cual remover los elementos.</param>
        public static void RemoveOf<TItem, TRemove>(this ICollection<TItem> collection)
            where TRemove : TItem
        {
            var lst = collection.ToList();
            foreach (var j in lst)
            {
                if (!(j is TRemove)) continue;
                collection.Remove(j);
            }
        }

        /// <summary>
        ///     Elimina todos los elementos de una colección que cumplen con una condición.
        /// </summary>
        /// <typeparam name="T">Tipo de elementos en la colección.</typeparam>
        /// <param name="collection">Colección a procesar.</param>
        /// <param name="check">Función que verifica si un elemento cumple con una condición.</param>
        /// <param name="beforeDelete">Acción a ejecutar antes de borrar a un elemento en particular.</param>
        public static void RemoveAll<T>(this ICollection<T> collection, in Predicate<T> check, in Action<T> beforeDelete)
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
        public static void RemoveAll<T>(this ICollection<T> collection, in Action<T> beforeDelete) =>
            RemoveAll(collection, null, beforeDelete);
        
        /// <summary>
        ///     Elimina todos los elementos de una colección que cumplen con una condición.
        /// </summary>
        /// <typeparam name="T">Tipo de elementos en la colección.</typeparam>
        /// <param name="collection">Colección a procesar.</param>
        /// <param name="check">Función que verifica si un elemento cumple con una condición.</param>
        public static void RemoveAll<T>(this ICollection<T> collection, in Predicate<T> check) =>
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
        ///     Tipo de elementos contenidos en el <see cref="ICollection{T}" />.
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
        ///     Tipo de elementos contenidos en el <see cref="ICollection{T}" />.
        /// </typeparam>
        public static T PopFirst<T>(this ICollection<T> a)
        {
            var x = a.First();
            a.Remove(x);
            return x;
        }

        /// <summary>
        ///     Alternativa a <see cref="ICollection{T}.Add(T)"/> con soporte
        ///     para sintáxis fluent.
        /// </summary>
        /// <typeparam name="T">
        ///     Tipo de elementos contenidos en el <see cref="ICollection{T}" />.
        /// </typeparam>
        /// <param name="collection">
        ///     Colección a la cual agregar el nuevo elemento.
        /// </param>
        /// <param name="value">Valor a agregar a la colección.</param>
        /// <returns>El objeto agregado a la colección.</returns>
        public static T Push<T>(this ICollection<T> collection, T value)
        {
            collection.Add(value);
            return value;
        }

        /// <summary>
        ///     Alternativa a <see cref="ICollection{T}.Add(T)"/> con soporte
        ///     para sintáxis fluent.
        /// </summary>
        /// <typeparam name="T">
        ///     Tipo de elementos contenidos en el <see cref="ICollection{T}" />.
        /// </typeparam>
        /// <param name="collection">
        ///     Colección a la cual agregar el nuevo elemento.
        /// </param>
        /// <returns>
        ///     Una nueva instancia de <typeparamref name="T"/> q fue agregada
        ///     a la colección.
        /// </returns>
        public static T Push<T>(this ICollection<T> collection) where T : new()
        {
            return Push(collection, new T());
        }

        /// <summary>
        ///     Alternativa a <see cref="ICollection{T}.Add(T)"/> con soporte
        ///     para sintáxis fluent.
        /// </summary>
        /// <typeparam name="TItem">
        ///     Tipo de elemento a agregar a la colección.
        /// </typeparam>
        /// <typeparam name="TCollection">
        ///     Tipo de elementos contenidos en el <see cref="ICollection{T}" />.
        /// </typeparam>
        /// <param name="collection">
        ///     Colección a la cual agregar el nuevo elemento.
        /// </param>
        /// <param name="value">Valor a agregar a la colección.</param>
        /// <returns>El objeto agregado a la colección.</returns>
        public static TItem Push<TItem, TCollection>(this TItem value, ICollection<TCollection> collection) where TItem : TCollection
        {
            collection.Add(value);
            return value;
        }

        /// <summary>
        ///     Alternativa a <see cref="ICollection{T}.Add(T)"/> con soporte
        ///     para sintáxis fluent.
        /// </summary>
        /// <typeparam name="TItem">
        ///     Tipo de elemento a agregar a la colección.
        /// </typeparam>
        /// <typeparam name="TCollection">
        ///     Tipo de elementos contenidos en el <see cref="ICollection{T}" />.
        /// </typeparam>
        /// <param name="collection">
        ///     Colección a la cual agregar el nuevo elemento.
        /// </param>
        /// <returns>Una nueva instancia de tipo <typeparamref name="TItem"/> agregada a la colección.</returns>
        public static TItem Push<TItem, TCollection>(this ICollection<TCollection> collection) where TItem : TCollection, new() => Push(new TItem(), collection);

        /// <summary>
        ///     Obtiene un <see cref="ObservableCollectionWrap{T}"/> que envuelve a la
        ///     colección especificada.
        /// </summary>
        /// <typeparam name="T">Tipo de elementos de la colección.</typeparam>
        /// <param name="collection">
        ///     Colección a envolver dentro del
        ///     <see cref="ObservableCollectionWrap{T}"/>.
        /// </param>
        /// <returns>
        ///     Un <see cref="ObservableCollectionWrap{T}"/> que envuelve a la colección
        ///     para brindar notificaciones de cambio por medio de la interfaz
        ///     <see cref="System.Collections.Specialized.INotifyCollectionChanged"/>.
        /// </returns>
        public static ObservableCollectionWrap<T> ToObservable<T>(this ICollection<T> collection)
        {
            return new ObservableCollectionWrap<T>(collection);
        }

        /// <summary>
        ///     Añade un conjunto de elementos al <see cref="ICollection{T}"/>.
        /// </summary>
        /// <typeparam name="T">
        ///     Tipode elementos de la colección.
        /// </typeparam>
        /// <param name="collection">
        ///     Colección a la cual agregar los elementos.
        /// </param>
        /// <param name="items">
        ///     Elementos a agregar a la colección.
        /// </param>
        public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> items)
        {
            foreach (var j in items) collection.Add(j);
        }
    }
}