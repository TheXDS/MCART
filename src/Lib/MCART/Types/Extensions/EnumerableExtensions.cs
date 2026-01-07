/*
EnumerableExtensions.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2026 César Andrés Morgan

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

using System.Collections;
using System.Diagnostics.CodeAnalysis;
using TheXDS.MCART.Attributes;

namespace TheXDS.MCART.Types.Extensions;

/// <summary>
/// Extensions for all items of type
/// <see cref="IEnumerable{T}" />.
/// </summary>
public static partial class EnumerableExtensions
{
    /// <summary>
    /// Checks whether the collection contains at least one element of the
    /// specified type.
    /// </summary>
    /// <typeparam name="T">Type of object to search for.</typeparam>
    /// <param name="collection">
    /// Collection of elements to check.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if there is an element of the specified type
    /// in the collection, <see langword="false"/> otherwise.
    /// </returns>
    [Sugar]
    public static bool IsAnyOf<T>(this IEnumerable collection)
    {
        return collection.OfType<T>().Any();
    }

    /// <summary>
    /// Checks whether the collection contains at least one element of the
    /// specified type.
    /// </summary>
    /// <param name="collection">
    /// Collection of elements to check.
    /// </param>
    /// <param name="type">Type of object to search for.</param>
    /// <returns>
    /// <see langword="true"/> if there is an element of the specified type
    /// in the collection, <see langword="false"/> otherwise.
    /// </returns>
    public static bool IsAnyOf(this IEnumerable collection, Type type)
    {
        foreach (object? j in collection)
        {
            if (type.IsInstanceOfType(j)) return true;
        }
        return false;
    }

    /// <summary>
    /// Executes an operation on a sequence in a locked context.
    /// </summary>
    /// <typeparam name="T">
    /// Type of elements in the sequence.
    /// </typeparam>
    /// <param name="collection">
    /// Sequence on which to execute a locked operation.
    /// </param>
    /// <param name="action">
    /// Action to execute on the sequence.
    /// </param>
    public static void Locked<T>(this T collection, Action<T> action) where T : IEnumerable
    {
        if (collection is ICollection c)
        {
            try
            {
                if (c.IsSynchronized) action(collection);
                else lock (c.SyncRoot) action(collection);
            }
            catch (NotSupportedException)
            {
                action(collection);
            }
        }
        else
        {
            lock (collection) action(collection);
        }
    }

    /// <summary>
    /// Executes an operation on a sequence in a locked context.
    /// </summary>
    /// <typeparam name="T">
    /// Type of elements in the sequence.
    /// </typeparam>
    /// <typeparam name="TResult">
    /// Type of result obtained by the function.
    /// </typeparam>
    /// <param name="collection">
    /// Sequence on which to execute a locked operation.
    /// </param>
    /// <param name="function">
    /// Function to execute on the sequence.
    /// </param>
    public static TResult Locked<T, TResult>(this T collection, Func<T, TResult> function) where T : IEnumerable
    {
        if (collection is ICollection c)
        {
            try
            {
                if (c.IsSynchronized) return function(collection);
                lock (c.SyncRoot) return function(collection);
            }
            catch (NotSupportedException)
            {
                return function(collection);
            }
        }
        lock (collection) return function(collection);
    }

    /// <summary>
    /// Gets the count of null elements within a sequence.
    /// </summary>
    /// <param name="collection">
    /// Sequence from which to get the count of null elements.
    /// </param>
    /// <returns>
    /// The count of null elements within the collection.
    /// </returns>
    public static int NullCount(this IEnumerable collection)
    {
        ArgumentNullException.ThrowIfNull(collection, nameof(collection));
        int count = 0;
        foreach (object? j in collection)
        {
            if (j is null) count++;
        }
        return count;
    }

    /// <summary>
    /// Groups a sequence of elements according to their types.
    /// </summary>
    /// <param name="c">Collection to group.</param>
    /// <returns>
    /// A sequence of elements grouped according to their type.
    /// </returns>
    public static IEnumerable<IGrouping<Type, object>> GroupByType(this IEnumerable c)
    {
        return c.ToGeneric().NotNull().GroupBy(p => p.GetType());
    }

    /// <summary>
    /// Enumerates a non-generic collection as a generic one.
    /// </summary>
    /// <param name="collection">
    /// Collection to enumerate.
    /// </param>
    /// <returns>
    /// An enumeration with the contents of the non-generic enumeration
    /// exposed as a generic one.
    /// </returns>
    public static IEnumerable<object?> ToGeneric(this IEnumerable collection)
    {
        foreach (object? j in collection) yield return j;
    }

    /// <summary>
    /// Gets the first element of the requested type within a
    /// collection.
    /// </summary>
    /// <typeparam name="T">
    /// Type of element to search for.
    /// </typeparam>
    /// <param name="collection">
    /// Collection on which to perform the search.
    /// </param>
    /// <returns>
    /// The first element of type <typeparamref name="T"/> that is
    /// found in the collection, or <see langword="default"/> if no element
    /// of the specified type is found.
    /// </returns>
    [Sugar]
    [return: MaybeNull]
    public static T FirstOf<T>(this IEnumerable collection)
    {
        return collection.OfType<T>().FirstOrDefault();
    }

    /// <summary>
    /// Enumerates the non-null elements of a collection.
    /// </summary>
    /// <param name="collection">Collection to enumerate.</param>
    /// <returns>
    /// An enumeration with the elements of the collection, omitting
    /// those that are <see langword="null"/>, or a collection empty if
    /// <paramref name="collection"/> is <see langword="null"/>.
    /// </returns>
    public static IEnumerable NotNull(this IEnumerable? collection)
    {
        if (collection is null) yield break;
        foreach (object? j in collection)
        {
            if (j is not null) yield return j;
        }
    }

    /// <summary>
    /// Compares two collections and determines if their elements are equal.
    /// </summary>
    /// <param name="collection">
    /// Enumeration to check.
    /// </param>
    /// <param name="items">
    /// Enumeration against which to check.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the elements of both collections
    /// are equal, <see langword="false"/> otherwise.
    /// </returns>
    public static bool ItemsEqual(this IEnumerable collection, IEnumerable items)
    {
        IEnumerator ea = collection.GetEnumerator();
        IEnumerator eb = items.GetEnumerator();
        while (ea.MoveNext())
        {
            if (!eb.MoveNext() || (!ea.Current?.Equals(eb.Current) ?? false)) return false;
        }
        return !eb.MoveNext();
    }

    /// <summary>
    /// Checks if the <paramref name="collection"/> contains all
    /// the elements of the <paramref name="items"/> collection.
    /// </summary>
    /// <param name="collection">
    /// Enumeration to check.
    /// </param>
    /// <param name="items">
    /// Elements that must exist in <paramref name="collection"/>.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="collection"/> contains all
    /// the elements of <paramref name="items"/>, <see langword="false"/>
    /// otherwise.
    /// </returns>
    public static bool ContainsAll(this IEnumerable collection, IEnumerable items)
    {
        return items.Cast<object?>().All(collection.Contains);
    }

    /// <summary>
    /// Checks if the <paramref name="collection"/> contains all
    /// the elements of the <paramref name="items"/> collection.
    /// </summary>
    /// <param name="collection">
    /// Enumeration to check.
    /// </param>
    /// <param name="items">
    /// Elements that must exist in <paramref name="collection"/>.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="collection"/> contains all
    /// the elements of <paramref name="items"/>, <see langword="false"/>
    /// otherwise.
    /// </returns>
    public static bool ContainsAll(this IEnumerable collection, params object?[] items)
    {
        return ContainsAll(collection, items.AsEnumerable());
    }

    /// <summary>
    /// Checks if the <paramref name="collection"/> contains any
    /// of the elements of the <paramref name="items"/> collection.
    /// </summary>
    /// <param name="collection">
    /// Enumeration to check.
    /// </param>
    /// <param name="items">
    /// Elements that must exist in <paramref name="collection"/>.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="collection"/> contains any
    /// of the elements of <paramref name="items"/>,
    /// <see langword="false"/> otherwise.
    /// </returns>
    public static bool ContainsAny(this IEnumerable collection, IEnumerable items)
    {
        return items.Cast<object?>().Any(collection.Contains);
    }

    /// <summary>
    /// Checks if the <paramref name="collection"/> contains any
    /// of the elements of the <paramref name="items"/> collection.
    /// </summary>
    /// <param name="collection">
    /// Enumeration to check.
    /// </param>
    /// <param name="items">
    /// Elements that must exist in <paramref name="collection"/>.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="collection"/> contains any
    /// of the elements of <paramref name="items"/>,
    /// <see langword="false"/> otherwise.
    /// </returns>
    public static bool ContainsAny(this IEnumerable collection, params object?[] items)
    {
        return ContainsAny(collection, items.AsEnumerable());
    }

    /// <summary>
    /// Non-generic version of the function
    /// <see cref="Enumerable.Contains{TSource}(IEnumerable{TSource}, TSource)"/>.
    /// </summary>
    /// <param name="enumerable">Collection to check.</param>
    /// <param name="obj">Object to search within the collection.</param>
    /// <returns>
    /// <see langword="true"/> if the collection contains the specified object,
    /// <see langword="false"/> otherwise.
    /// </returns>
    public static bool Contains(this IEnumerable enumerable, object? obj)
    {
        return enumerable.Cast<object>().Any(j => j?.Equals(obj) ?? obj is null);
    }

    /// <summary>
    /// Non-generic version of the function
    /// <see cref="Enumerable.Count{TSource}(IEnumerable{TSource})"/>.
    /// Gets the number of elements in a sequence.
    /// </summary>
    /// <param name="e">
    /// Sequence to check.  The same will be enumerated.
    /// </param>
    /// <returns>
    /// The number of elements within the sequence.
    /// </returns>
    public static int Count(this IEnumerable e)
    {
        Count_Contract(e);
#if DynamicLoading
        if (e.GetType().GetProperty("Count") is PropertyInfo p && p.CanRead && p.PropertyType == typeof(int))
        {
            return (int)p.GetValue(e, null)!;
        }
        if (e.GetType().GetMethod("Count", BindingFlags.Public | BindingFlags.Instance, null, Type.EmptyTypes, null) is MethodInfo m && m.ReturnType == typeof(int) && !m.ContainsGenericParameters)
        {
            return (int)m.Invoke(e, new object[0])!;
        }
        return CountEnumerable(e);
#else
        return e switch
        {
            string s => s.Length,
            Array a => a.Length,
            ICollection c => c.Count,
            _ => CountEnumerable(e)
        };
#endif
    }

    private static int IndexOfEnumerable(IEnumerable e, object? i)
    {
        IEnumerator n = e.GetEnumerator();
        int c = 0;
        while (n.MoveNext())
        {
            if (n.Current?.Equals(i) ?? i is null) return c;
            c++;
        }
        (n as IDisposable)?.Dispose();
        return -1;
    }

    private static int CountEnumerable(IEnumerable e)
    {
        IEnumerator n = e.GetEnumerator();
        int c = 0;
        while (n.MoveNext()) c++;
        (n as IDisposable)?.Dispose();
        return c;
    }
}
