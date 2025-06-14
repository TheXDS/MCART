/*
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
/// Extensions for all elements of type <see cref="ICollection{T}" />.
/// </summary>
public static partial class CollectionExtensions
{
    /// <summary>
    /// Removes all elements of the specified type from the
    /// collection.
    /// </summary>
    /// <typeparam name="TItem">
    /// Type of elements contained in the collection.
    /// </typeparam>
    /// <typeparam name="TRemove">Type of elements to remove.</typeparam>
    /// <param name="collection">Collection from which to remove the elements.</param>
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
    /// Removes all elements from a collection that satisfy a condition.
    /// </summary>
    /// <typeparam name="T">Type of elements in the collection.</typeparam>
    /// <param name="collection">Collection to process.</param>
    /// <param name="check">Function that verifies if an element satisfies a condition.</param>
    /// <param name="beforeDelete">Action to execute before deleting a particular element.</param>
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
    /// Removes all elements from a collection.
    /// </summary>
    /// <typeparam name="T">Type of elements in the collection.</typeparam>
    /// <param name="collection">Collection to process.</param>
    /// <param name="beforeDelete">Action to execute before deleting a particular element.</param>
    public static void RemoveAll<T>(this ICollection<T> collection, in Action<T> beforeDelete) =>
        RemoveAll(collection, null, beforeDelete);

    /// <summary>
    /// Removes all elements from a collection that satisfy a condition.
    /// </summary>
    /// <typeparam name="T">Type of elements in the collection.</typeparam>
    /// <param name="collection">Collection to process.</param>
    /// <param name="check">Function that verifies if an element satisfies a condition.</param>
    public static void RemoveAll<T>(this ICollection<T> collection, in Predicate<T> check) =>
        RemoveAll(collection, check, null);

    /// <summary>
    /// Removes all elements from a collection, individually.
    /// </summary>
    /// <typeparam name="T">Type of elements in the collection.</typeparam>
    /// <param name="collection">Collection to process.</param>
    public static void RemoveAll<T>(this ICollection<T> collection) => RemoveAll(collection, null, null);

    /// <summary>
    /// Returns the last element in the list, removing it.
    /// </summary>
    /// <returns>The last element in the list.</returns>
    /// <param name="a">List from which to get the element.</param>
    /// <typeparam name="T">
    /// Type of elements contained in the <see cref="ICollection{T}" />.
    /// </typeparam>
    public static T Pop<T>(this ICollection<T> a)
    {
        T? x = a.Last();
        a.Remove(x);
        return x;
    }

    /// <summary>
    /// Returns the first element in the list, removing it.
    /// </summary>
    /// <returns>The first element in the list.</returns>
    /// <param name="a">List from which to get the element.</param>
    /// <typeparam name="T">
    /// Type of elements contained in the <see cref="ICollection{T}" />.
    /// </typeparam>
    public static T PopFirst<T>(this ICollection<T> a)
    {
        T? x = a.First();
        a.Remove(x);
        return x;
    }

    /// <summary>
    /// Alternative to <see cref="ICollection{T}.Add(T)"/> with support
    /// for fluent syntax.
    /// </summary>
    /// <typeparam name="T">
    /// Type of elements contained in the <see cref="ICollection{T}" />.
    /// </typeparam>
    /// <param name="collection">
    /// Collection to which to add the new element.
    /// </param>
    /// <returns>
    /// A new instance of <typeparamref name="T"/> q was added
    /// to the collection.
    /// </returns>
    public static T Push<T>(this ICollection<T> collection) where T : new()
    {
        return new T().PushInto(collection);
    }

    /// <summary>
    /// Alternative to <see cref="ICollection{T}.Add(T)"/> with support
    /// for fluent syntax.
    /// </summary>
    /// <typeparam name="TItem">
    /// Type of element to add to the collection.
    /// </typeparam>
    /// <typeparam name="TCollection">
    /// Type of elements contained in the <see cref="ICollection{T}" />.
    /// </typeparam>
    /// <param name="collection">
    /// Collection to which to add the new element.
    /// </param>
    /// <param name="value">Value to add to the collection.</param>
    /// <returns>The object added to the collection.</returns>
    public static TItem PushInto<TItem, TCollection>(this TItem value, ICollection<TCollection> collection) where TItem : TCollection
    {
        return collection.Push(value);
    }

    /// <summary>
    /// Alternative to <see cref="ICollection{T}.Add(T)"/> with support
    /// for fluent syntax.
    /// </summary>
    /// <typeparam name="TItem">
    /// Type of element to add to the collection.
    /// </typeparam>
    /// <typeparam name="TCollection">
    /// Type of elements contained in the <see cref="ICollection{T}" />.
    /// </typeparam>
    /// <param name="collection">
    /// Collection to which to add the new element.
    /// </param>
    /// <param name="value">Value to add to the collection.</param>
    /// <returns>The object added to the collection.</returns>
    public static TItem Push<TItem, TCollection>(this ICollection<TCollection> collection, TItem value) where TItem : TCollection
    {
        Push_Contract(collection);
        collection.Add(value);
        return value;
    }

    /// <summary>
    /// Alternative to <see cref="ICollection{T}.Add(T)"/> with support
    /// for fluent syntax.
    /// </summary>
    /// <typeparam name="TItem">
    /// Type of element to add to the collection.
    /// </typeparam>
    /// <typeparam name="TCollection">
    /// Type of elements contained in the <see cref="ICollection{T}" />.
    /// </typeparam>
    /// <param name="collection">
    /// Collection to which to add the new element.
    /// </param>
    /// <returns>A new instance of type <typeparamref name="TItem"/> added to the collection.</returns>
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
    /// Adds a copy of a set of elements to the
    /// <see cref="ICollection{T}"/>.
    /// </summary>
    /// <typeparam name="T">Type of elements in the collection.</typeparam>
    /// <param name="collection">
    /// Collection to which to add the elements.
    /// </param>
    /// <param name="source">
    /// Elements to add to the collection.
    /// </param>
    public static void AddClones<T>(this ICollection<T> collection, IEnumerable<T> source) where T : ICloneable
    {
        AddClones_Contract(collection, source);
        collection.AddRange(source.Select(p => p?.Clone()).NotNull().OfType<T>());
    }

    /// <summary>
    /// Clones an element and adds the copy to a collection.
    /// </summary>
    /// <typeparam name="T">Type of elements in the collection.</typeparam>
    /// <param name="collection">
    /// Collection to which to add the elements.
    /// </param>
    /// <param name="item">
    /// Element to copy and add to the collection.
    /// </param>
    public static void AddClone<T>(this ICollection<T> collection, T item) where T : ICloneable
    {
        AddClone_Contract(collection, item);
        collection.Add((T)item.Clone());
    }
}
