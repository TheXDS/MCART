﻿/*
CollectionExtensions.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2025 César Andrés Morgan

Permission is hereby granted, free of charge, to any person obtaining a copy of
this software and associated documentation files (the "Software"), to deal in
the Software without restriction, including without limitation the rights to
use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
of the Software, and to permit persons to whom the Software is furnished to do
so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

namespace TheXDS.MCART.Types.Extensions;

/// <summary>
/// Extensiones para todos los elementos de tipo <see cref="ICollection{T}" />.
/// </summary>
public static partial class CollectionExtensions
{
    /// <summary>
    /// Quita todos los elementos del tipo especificado de la
    /// colección.
    /// </summary>
    /// <typeparam name="TItem">
    /// Tipo de elementos contenidos en la colección.
    /// </typeparam>
    /// <typeparam name="TRemove">Tipo de elementos a remover.</typeparam>
    /// <param name="collection">Colección de la cual remover los elementos.</param>
    public static void RemoveOf<TItem, TRemove>(this ICollection<TItem> collection)
        where TRemove : TItem
    {
        TItem[]? lst = collection.ToArray();
        foreach (TItem? j in lst)
        {
            if (j is TRemove) collection.Remove(j);
        }
    }

    /// <summary>
    /// Elimina todos los elementos de una colección que cumplen con una condición.
    /// </summary>
    /// <typeparam name="T">Tipo de elementos en la colección.</typeparam>
    /// <param name="collection">Colección a procesar.</param>
    /// <param name="check">Función que verifica si un elemento cumple con una condición.</param>
    /// <param name="beforeDelete">Acción a ejecutar antes de borrar a un elemento en particular.</param>
    public static void RemoveAll<T>(this ICollection<T> collection, in Predicate<T>? check, in Action<T>? beforeDelete)
    {
        T[]? lst = collection.ToArray();
        foreach (T? j in lst)
        {
            if (!(check?.Invoke(j) ?? true)) continue;
            beforeDelete?.Invoke(j);
            collection.Remove(j);
        }
    }

    /// <summary>
    /// Elimina todos los elementos de una colección.
    /// </summary>
    /// <typeparam name="T">Tipo de elementos en la colección.</typeparam>
    /// <param name="collection">Colección a procesar.</param>
    /// <param name="beforeDelete">Acción a ejecutar antes de borrar a un elemento en particular.</param>
    public static void RemoveAll<T>(this ICollection<T> collection, in Action<T> beforeDelete) =>
        RemoveAll(collection, null, beforeDelete);

    /// <summary>
    /// Elimina todos los elementos de una colección que cumplen con una condición.
    /// </summary>
    /// <typeparam name="T">Tipo de elementos en la colección.</typeparam>
    /// <param name="collection">Colección a procesar.</param>
    /// <param name="check">Función que verifica si un elemento cumple con una condición.</param>
    public static void RemoveAll<T>(this ICollection<T> collection, in Predicate<T> check) =>
        RemoveAll(collection, check, null);

    /// <summary>
    /// Elimina todos los elementos de una colección, individualmente.
    /// </summary>
    /// <typeparam name="T">Tipo de elementos en la colección.</typeparam>
    /// <param name="collection">Colección a procesar.</param>
    public static void RemoveAll<T>(this ICollection<T> collection) => RemoveAll(collection, null, null);

    /// <summary>
    /// Devuelve el último elemento en la lista, quitándole.
    /// </summary>
    /// <returns>El último elemento en la lista.</returns>
    /// <param name="a">Lista de la cual obtener el elemento.</param>
    /// <typeparam name="T">
    /// Tipo de elementos contenidos en el <see cref="ICollection{T}" />.
    /// </typeparam>
    public static T Pop<T>(this ICollection<T> a)
    {
        T? x = a.Last();
        a.Remove(x);
        return x;
    }

    /// <summary>
    /// Devuelve el primer elemento en la lista, quitándole.
    /// </summary>
    /// <returns>El primer elemento en la lista.</returns>
    /// <param name="a">Lista de la cual obtener el elemento.</param>
    /// <typeparam name="T">
    /// Tipo de elementos contenidos en el <see cref="ICollection{T}" />.
    /// </typeparam>
    public static T PopFirst<T>(this ICollection<T> a)
    {
        T? x = a.First();
        a.Remove(x);
        return x;
    }

    /// <summary>
    /// Alternativa a <see cref="ICollection{T}.Add(T)"/> con soporte
    /// para sintaxis fluent.
    /// </summary>
    /// <typeparam name="T">
    /// Tipo de elementos contenidos en el <see cref="ICollection{T}" />.
    /// </typeparam>
    /// <param name="collection">
    /// Colección a la cual agregar el nuevo elemento.
    /// </param>
    /// <returns>
    /// Una nueva instancia de <typeparamref name="T"/> q fue agregada
    /// a la colección.
    /// </returns>
    public static T Push<T>(this ICollection<T> collection) where T : new()
    {
        return new T().PushInto(collection);
    }

    /// <summary>
    /// Alternativa a <see cref="ICollection{T}.Add(T)"/> con soporte
    /// para sintaxis fluent.
    /// </summary>
    /// <typeparam name="TItem">
    /// Tipo de elemento a agregar a la colección.
    /// </typeparam>
    /// <typeparam name="TCollection">
    /// Tipo de elementos contenidos en el <see cref="ICollection{T}" />.
    /// </typeparam>
    /// <param name="collection">
    /// Colección a la cual agregar el nuevo elemento.
    /// </param>
    /// <param name="value">Valor a agregar a la colección.</param>
    /// <returns>El objeto agregado a la colección.</returns>
    public static TItem PushInto<TItem, TCollection>(this TItem value, ICollection<TCollection> collection) where TItem : TCollection
    {
        return collection.Push(value);
    }

    /// <summary>
    /// Alternativa a <see cref="ICollection{T}.Add(T)"/> con soporte
    /// para sintaxis fluent.
    /// </summary>
    /// <typeparam name="TItem">
    /// Tipo de elemento a agregar a la colección.
    /// </typeparam>
    /// <typeparam name="TCollection">
    /// Tipo de elementos contenidos en el <see cref="ICollection{T}" />.
    /// </typeparam>
    /// <param name="collection">
    /// Colección a la cual agregar el nuevo elemento.
    /// </param>
    /// <param name="value">Valor a agregar a la colección.</param>
    /// <returns>El objeto agregado a la colección.</returns>
    public static TItem Push<TItem, TCollection>(this ICollection<TCollection> collection, TItem value) where TItem : TCollection
    {
        Push_Contract(collection);
        collection.Add(value);
        return value;
    }

    /// <summary>
    /// Alternativa a <see cref="ICollection{T}.Add(T)"/> con soporte
    /// para sintaxis fluent.
    /// </summary>
    /// <typeparam name="TItem">
    /// Tipo de elemento a agregar a la colección.
    /// </typeparam>
    /// <typeparam name="TCollection">
    /// Tipo de elementos contenidos en el <see cref="ICollection{T}" />.
    /// </typeparam>
    /// <param name="collection">
    /// Colección a la cual agregar el nuevo elemento.
    /// </param>
    /// <returns>Una nueva instancia de tipo <typeparamref name="TItem"/> agregada a la colección.</returns>
    public static TItem Push<TItem, TCollection>(this ICollection<TCollection> collection) where TItem : TCollection, new() => Push(collection, new TItem());

    /// <summary>
    /// Adds a set of elements to the specified <see cref="ICollection{T}"/>.
    /// </summary>
    /// <typeparam name="T">
    /// Type of elements contained in the collection.
    /// </typeparam>
    /// <param name="collection">
    /// Collection onto which to add the elements.
    /// </param>
    /// <param name="items">
    /// Enumeration of elements to be added to the collection.
    /// </param>
    public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> items)
    {
        AddRange_Contract(collection, items);
        foreach (T? j in items) collection.Add(j);
    }

    /// <summary>
    /// Adds a set of elements to the specified <see cref="ICollection{T}"/>.
    /// </summary>
    /// <typeparam name="T">
    /// Type of elements contained in the collection.
    /// </typeparam>
    /// <param name="collection">
    /// Collection onto which to add the elements.
    /// </param>
    /// <param name="items">
    /// Async enumeration of elements to be added to the collection.
    /// </param>
    public static async Task AddRangeAsync<T>(this ICollection<T> collection, IAsyncEnumerable<T> items)
    {
        AddRange_Contract(collection, items);
        await foreach (T? j in items) collection.Add(j);
    }

    /// <summary>
    /// Añade una copia de un conjunto de elementos al
    /// <see cref="ICollection{T}"/>.
    /// </summary>
    /// <typeparam name="T">Tipo de elementos de la colección.</typeparam>
    /// <param name="collection">
    /// Colección a la cual agregar los elementos.
    /// </param>
    /// <param name="source">
    /// Elementos a agregar a la colección.
    /// </param>
    public static void AddClones<T>(this ICollection<T> collection, IEnumerable<T> source) where T : ICloneable
    {
        AddClones_Contract(collection, source);
        collection.AddRange(source.Select(p => p?.Clone()).NotNull().OfType<T>());
    }

    /// <summary>
    /// Clona un elemento y agrega la copia a una colección.
    /// </summary>
    /// <typeparam name="T">Tipo de elementos de la colección.</typeparam>
    /// <param name="collection">
    /// Colección a la cual agregar los elementos.
    /// </param>
    /// <param name="item">
    /// Elemento a copiar y agregar a la colección.
    /// </param>
    public static void AddClone<T>(this ICollection<T> collection, T item) where T : ICloneable
    {
        AddClone_Contract(collection, item);
        collection.Add((T)item.Clone());
    }
}
