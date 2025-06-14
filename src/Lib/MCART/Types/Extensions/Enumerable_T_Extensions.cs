// Enumerable_T_Extensions.cs
//
// This file is part of Morgan's CLR Advanced Runtime (MCART)
//
// Author(s):
//      César Andrés Morgan <xds_xps_ivx@hotmail.com>
//
// Released under the MIT License (MIT)
// Copyright © 2011 - 2025 César Andrés Morgan
//
// Permission is hereby granted, free of charge, to any person obtaining a copy of
// this software and associated documentation files (the “Software”), to deal in
// the Software without restriction, including without limitation the rights to
// use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
// of the Software, and to permit persons to whom the Software is furnished to do
// so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using System.Diagnostics.CodeAnalysis;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Exceptions;
using TheXDS.MCART.Misc;
using TheXDS.MCART.Resources;

namespace TheXDS.MCART.Types.Extensions;

public static partial class EnumerableExtensions
{
    /// <summary>
    /// Iterates in sorted order over the collection.
    /// </summary>
    /// <param name="collection">Collection to iterate over</param>
    /// <typeparam name="T">Type of elements in the collection.</typeparam>
    /// <returns>
    /// An ordered enumerable object created from the collection.
    /// </returns>
    [Sugar]
    public static IOrderedEnumerable<T> Ordered<T>(this IEnumerable<T> collection) where T : IComparable<T>
    {
        return collection.OrderBy(p => p);
    }

    /// <summary>
    /// Executes a task asynchronously over each item to be enumerated.
    /// </summary>
    /// <typeparam name="T">
    /// Type of elements in the enumeration.
    /// </typeparam>
    /// <param name="input">Input enumeration.</param>
    /// <param name="processor">
    /// Asynchronous task that will be executed over each item before being
    /// enumerated.
    /// </param>
    /// <returns>
    /// An <see cref="IAsyncEnumerable{T}"/> that can be used to await
    /// the enumeration of each element.
    /// </returns>
    public static async IAsyncEnumerable<T> YieldAsync<T>(this IEnumerable<T> input, Func<T, Task> processor)
    {
        ArgumentNullException.ThrowIfNull(input, nameof(input));
        foreach (T? j in input)
        {
            await processor(j);
            yield return j;
        }
    }

    /// <summary>
    /// Transforms an input enumeration to the required output type
    /// asynchronously.
    /// </summary>
    /// <typeparam name="TIn">
    /// Type of elements in the input enumeration.
    /// </typeparam>
    /// <typeparam name="TOut">
    /// Type of elements in the output enumeration.
    /// </typeparam>
    /// <param name="input">Input enumeration.</param>
    /// <param name="selector">
    /// Asynchronous task that will transform the input data.
    /// </param>
    /// <returns>
    /// An <see cref="IAsyncEnumerable{T}"/> that can be used to await
    /// the enumeration of each element.
    /// </returns>
    public static async IAsyncEnumerable<TOut> SelectAsync<TIn, TOut>(this IEnumerable<TIn> input, Func<TIn, Task<TOut>> selector)
    {
        ArgumentNullException.ThrowIfNull(input, nameof(input));
        foreach (TIn? j in input)
        {
            yield return await selector(j);
        }
    }

    /// <summary>
    /// Gets the first element of the requested type within a
    /// collection.
    /// </summary>
    /// <typeparam name="T">
    /// Type of elements contained in the collection.
    /// </typeparam>
    /// <param name="collection">
    /// Collection over which to perform the search.
    /// </param>
    /// <param name="type">Type of element to search for.</param>
    /// <returns>
    /// The first element of type <paramref name="type"/> that is
    /// found in the collection, or <see langword="default"/> if no element
    /// of the specified type is found.
    /// </returns>
    [return: MaybeNull]
    [RequiresDynamicCode(Misc.AttributeErrorMessages.MethodCreatesNewTypes)]
    public static T FirstOf<T>(this IEnumerable<T> collection, Type type)
    {
        FirstOf_OfType_Contract<T>(type);
        return collection.FirstOrDefault(p => p?.GetType().IsAssignableTo(type) ?? false);
    }

    /// <summary>
    /// Enumerates all elements of the collection that are of the
    /// specified type.
    /// </summary>
    /// <typeparam name="T">
    /// Type of elements contained in the collection.
    /// </typeparam>
    /// <param name="collection">
    /// Collection over which to perform the search.
    /// </param>
    /// <param name="type">Type of elements to return.</param>
    /// <returns>
    /// An enumeration of all elements of the collection that are of the
    /// specified type.
    /// </returns>
    public static IEnumerable<T> OfType<T>(this IEnumerable<T> collection, Type type)
    {
        FirstOf_OfType_Contract<T>(type);
        return collection.Where(p => p?.GetType() == type);
    }

    /// <summary>
    /// Enumerates all elements of the collection, omitting the
    /// specified ones.
    /// </summary>
    /// <typeparam name="T">Type of elements in the collection.</typeparam>
    /// <param name="collection">Collection to enumerate.</param>
    /// <param name="exclusions">
    /// Elements to exclude from the collection.
    /// </param>
    /// <returns>
    /// An enumeration with the elements of the collection, omitting the
    /// specified exclusions.
    /// </returns>
    public static IEnumerable<T> ExceptFor<T>(this IEnumerable<T> collection, params T[] exclusions)
    {
        bool Compare(T value)
        {
            return value is not null && value.GetType().IsClass
                ? value.IsNeither(exclusions.AsEnumerable())
                : !exclusions.Contains(value);
        }
        return collection.Where(Compare);
    }

    /// <summary>
    /// Enumerates the non-null elements of a collection.
    /// </summary>
    /// <typeparam name="T">Type of elements in the collection.</typeparam>
    /// <param name="collection">Collection to enumerate.</param>
    /// <returns>
    /// An enumeration with the non-null elements of the collection, or an
    /// empty collection if <paramref name="collection"/> is
    /// <see langword="null"/>.
    /// </returns>
    public static IEnumerable<T> NotNull<T>(this IEnumerable<T?>? collection) where T : class
    {
        return collection is null ? [] : collection.Where(p => p is not null).Select(p => p!);
    }

    /// <summary>
    /// Enumerates the non-null elements of a collection.
    /// </summary>
    /// <typeparam name="T">Type of elements in the collection.</typeparam>
    /// <param name="collection">Collection to enumerate.</param>
    /// <returns>
    /// An enumeration with the non-null elements of the collection, or an
    /// empty collection if <paramref name="collection"/> is
    /// <see langword="null"/>.
    /// </returns>
    public static IEnumerable<T> NotNull<T>(this IEnumerable<T?>? collection) where T : struct
    {
        return collection is null ? [] : collection.Where(p => p is not null).Select(p => p!.Value);
    }

    /// <summary>
    /// Enumerates the collection, and returns <see langword="null"/> if the
    /// collection contains no elements.
    /// </summary>
    /// <typeparam name="T">Type of elements in the collection.</typeparam>
    /// <param name="collection">Collection to enumerate.</param>
    /// <returns>
    /// An enumeration with the elements of the collection, or
    /// <see langword="null"/> if the collection contains no elements.
    /// </returns>
    public static IEnumerable<T>? OrNull<T>(this IEnumerable<T> collection)
    {
        T[] c = [.. collection];
        return c.Length != 0 ? c : null;
    }

    /// <summary>
    /// Enumerates the elements that are distinct from their default value
    /// within a collection.
    /// </summary>
    /// <typeparam name="T">Type of elements in the collection.</typeparam>
    /// <param name="collection">Collection to enumerate.</param>
    /// <returns>
    /// An enumeration with the elements of the collection, omitting
    /// those that are equal to their default value, or in the
    /// case of reference types, omitting those that are
    /// <see langword="null"/>.
    /// </returns>
    public static IEnumerable<T> NonDefaults<T>(this IEnumerable<T?> collection) where T : notnull
    {
        return collection.Where(p => !Equals(p, default(T)!))!;
    }

    /// <summary>
    /// Gets a sub-range of values within this
    /// <see cref="IEnumerable{T}" />.
    /// </summary>
    /// <param name="from">
    /// <see cref="IEnumerable{T}" /> from which to extract the sequence.
    /// </param>
    /// <param name="index">
    /// Index from which to obtain the sub-range.
    /// </param>
    /// <param name="count">
    /// Quantity of elements to obtain.
    /// </param>
    /// <returns>
    /// An <see cref="IEnumerable{T}" /> that contains the specified sub-range.
    /// </returns>
    /// <exception cref="IndexOutOfRangeException">
    /// Thrown if <paramref name="index"/> is out of the range of the
    /// collection.
    /// </exception>
    public static IEnumerable<T> Range<T>(this IEnumerable<T> from, int index, int count)
    {
        using IEnumerator<T> e = from.GetEnumerator();
        e.Reset();
        e.MoveNext();
        int c = 0;
        while (c++ < index)
        {
            if (!e.MoveNext()) throw new IndexOutOfRangeException();
        }
        c = 0;
        while (c++ < count)
        {
            yield return e.Current;
            if (!e.MoveNext()) yield break;
        }
    }

    /// <summary>
    /// Returns a copy of the elements of this <see cref="IEnumerable{T}" />
    /// </summary>
    /// <returns>
    /// Copy of this list. The elements of the copy represent the same
    /// instance of the original object.
    /// </returns>
    /// <param name="collection">Collection to copy.</param>
    /// <typeparam name="T">
    /// Type of elements contained in the <see cref="IEnumerable{T}" />.
    /// </typeparam>
    public static IEnumerable<T> Copy<T>(this IEnumerable<T> collection)
    {
        List<T> tmp = [.. collection];
        return tmp;
    }

    /// <summary>
    /// Returns an unsorted version of the <see cref="IEnumerable{T}" />
    /// without altering the original collection.
    /// </summary>
    /// <typeparam name="T">
    /// Type of elements contained in the <see cref="IEnumerable{T}" />.
    /// </typeparam>
    /// <param name="collection"><see cref="IEnumerable{T}" /> to shuffle.</param>
    /// <returns>
    /// An unsorted version of the <see cref="IEnumerable{T}" />.
    /// </returns>
    public static IEnumerable<T> Shuffled<T>(this IEnumerable<T> collection)
    {
        return Shuffled(collection, 1);
    }

    /// <summary>
    /// Returns an unsorted version of the <see cref="IEnumerable{T}" />
    /// without altering the original collection.
    /// </summary>
    /// <typeparam name="T">
    /// Type of elements contained in the <see cref="IEnumerable{T}" />.
    /// </typeparam>
    /// <param name="collection"><see cref="IEnumerable{T}" /> to shuffle.</param>
    /// <param name="deepness">
    /// Depth of the shuffle. 1 is the highest.
    /// </param>
    /// <returns>
    /// An unsorted version of the <see cref="IEnumerable{T}" />.
    /// </returns>
    public static IEnumerable<T> Shuffled<T>(this IEnumerable<T> collection, in int deepness)
    {
        List<T> enumerable = [.. collection];
        return Shuffled(enumerable, 0, enumerable.Count - 1, deepness, RandomExtensions.Rnd);
    }

    /// <summary>
    /// Returns an unsorted version of the specified range of elements of the
    /// <see cref="IEnumerable{T}" /> without altering the original collection.
    /// </summary>
    /// <typeparam name="T">
    /// Type of elements contained in the <see cref="IEnumerable{T}" />.
    /// </typeparam>
    /// <param name="collection"><see cref="IEnumerable{T}" /> to shuffle.</param>
    /// <param name="firstIdx">Starting index of the range.</param>
    /// <param name="lastIdx">Starting index of the range.</param>
    /// <returns>
    /// An unsorted version of the specified range of elements of the
    /// <see cref="IEnumerable{T}" />.
    /// </returns>
    public static IEnumerable<T> Shuffled<T>(this IEnumerable<T> collection, in int firstIdx, in int lastIdx)
    {
        return Shuffled(collection, firstIdx, lastIdx, 1, RandomExtensions.Rnd);
    }

    /// <summary>
    /// Returns an unsorted version of the specified range of elements of the
    /// <see cref="IEnumerable{T}" /> without altering the original collection.
    /// </summary>
    /// <typeparam name="T">
    /// Type of elements contained in the <see cref="IEnumerable{T}" />.
    /// </typeparam>
    /// <param name="collection"><see cref="IEnumerable{T}" /> to shuffle.</param>
    /// <param name="firstIdx">Starting index of the range.</param>
    /// <param name="lastIdx">Starting index of the range.</param>
    /// <param name="deepness">
    /// Depth of the shuffle. 1 is the highest.
    /// </param>
    /// <param name="random">Random number generator to use.</param>
    /// <returns>
    /// An unsorted version of the specified range of elements of the
    /// <see cref="IEnumerable{T}" />.
    /// </returns>
    public static IEnumerable<T> Shuffled<T>(this IEnumerable<T> collection, in int firstIdx, in int lastIdx, in int deepness, in Random random)
    {
        List<T> tmp = [.. collection];
        tmp.Shuffle(firstIdx, lastIdx, deepness, random);
        return tmp;
    }

    /// <summary>
    /// Selects a random element from the collection.
    /// </summary>
    /// <returns>A random object from the collection.</returns>
    /// <param name="collection">Collection from which to select.</param>
    /// <typeparam name="T">
    /// Type of elements contained in the <see cref="IEnumerable{T}" />.
    /// </typeparam>
    /// <returns>
    /// A random element from the collection.
    /// </returns>
    public static T Pick<T>(this IEnumerable<T> collection)
    {
        return Pick(collection, RandomExtensions.Rnd);
    }

    /// <summary>
    /// Selects a random element from the collection.
    /// </summary>
    /// <returns>A random object from the collection.</returns>
    /// <typeparam name="T">
    /// Type of elements contained in the <see cref="IEnumerable{T}" />.
    /// </typeparam>
    /// <param name="collection">Collection from which to select.</param>
    /// <param name="random">Random number generator to use.</param>
    /// <returns>
    /// A random element from the collection.
    /// </returns>
    public static T Pick<T>(this IEnumerable<T> collection, in Random random)
    {
        T[] c = [.. collection];
        if (c.Length == 0) throw Errors.EmptyCollection(c);
        return c[random.Next(0, c.Length)];
    }

    /// <summary>
    /// Selects a random element from the collection asynchronously.
    /// </summary>
    /// <returns>A random object from the collection.</returns>
    /// <typeparam name="T">
    /// Type of elements contained in the <see cref="IEnumerable{T}" />.
    /// </typeparam>
    /// <returns>
    /// A task that can be used to monitor the operation.
    /// </returns>
    public static async Task<T> PickAsync<T>(this IEnumerable<T> collection)
    {
        List<T> c = await collection.ToListAsync();
        if (c.Count == 0) throw Errors.EmptyCollection(collection);
        return c.ElementAt(RandomExtensions.Rnd.Next(0, c.Count));
    }

    /// <summary>
    /// Creates a <see cref="ListEx{T}"/> from an <see cref="IEnumerable{T}"/>.
    /// </summary>
    /// <param name="collection">Collection to convert</param>
    /// <typeparam name="T">Type of the collection.</typeparam>
    /// <returns>
    /// A <see cref="ListEx{T}" /> from the <see cref="Extensions" /> namespace.
    /// </returns>
    [Obsolete(AttributeErrorMessages.UnsuportedClass)]
    public static ListEx<T> ToExtendedList<T>(this IEnumerable<T> collection)
    {
        return [.. collection];
    }

    /// <summary>
    /// Creates a <see cref="List{T}"/> from an <see cref="IEnumerable{T}"/> asynchronously.
    /// </summary>
    /// <typeparam name="T">Type of the collection.</typeparam>
    /// <param name="enumerable"></param>
    /// <returns>
    /// A task that can be used to monitor the operation.
    /// </returns>
    public static async Task<List<T>> ToListAsync<T>(this IEnumerable<T> enumerable)
    {
        return await Task.Run(enumerable.ToList);
    }

    /// <summary>
    /// Creates a <see cref="ListEx{T}"/> from an <see cref="IEnumerable{T}"/> asynchronously.
    /// </summary>
    /// <typeparam name="T">Type of the collection.</typeparam>
    /// <param name="enumerable"></param>
    /// <returns>
    /// A task that can be used to monitor the operation.
    /// </returns>
    [Obsolete(AttributeErrorMessages.UnsuportedClass)]
    public static async Task<ListEx<T>> ToExtendedListAsync<T>(this IEnumerable<T> enumerable)
    {
        return await Task.Run(enumerable.ToExtendedList);
    }

    /// <summary>Rotates the elements of an array, list, or collection.</summary>
    /// <param name="collection">Array to rotate</param>
    /// <param name="steps">Direction and units of rotation.</param>
    /// <remarks>
    /// If <paramref name="steps" /> is positive, the rotation occurs upwards; otherwise, downwards.
    /// </remarks>
    /// <typeparam name="T">
    /// Type of elements contained in the <see cref="IEnumerable{T}" />.
    /// </typeparam>
    public static IEnumerable<T> Rotate<T>(this IEnumerable<T> collection, int steps)
    {
        switch (steps)
        {
            case > 0:
            {
                using IEnumerator<T> e = collection.GetEnumerator();
                e.Reset();
                int j = 0;
                while (j++ < steps) e.MoveNext();
                while (e.MoveNext()) yield return e.Current;
                e.Reset();
                while (--j > 0)
                {
                    e.MoveNext();
                    yield return e.Current;
                }
                break;
            }
            case < 0:
            {
                List<T> c = [];
                using IEnumerator<T> e = collection.GetEnumerator();
                e.Reset();
                // HACK: The implementation for IList<TValue> is functional, and does not require unusual tricks to rotate.
                while (e.MoveNext()) c.Add(e.Current);
                c.ApplyRotate(steps);
                foreach (T? i in c) yield return i;
                break;
            }
            default:
            {
                foreach (T? i in collection) yield return i;
                break;
            }
        }
    }

    /// <summary>Shifts the elements of an array, list, or collection.</summary>
    /// <param name="collection">Array to shift</param>
    /// <param name="steps">Direction and units of shift.</param>
    /// <remarks>
    /// If <paramref name="steps" /> is positive, the shift occurs upwards; otherwise, downwards.
    /// </remarks>
    /// <typeparam name="T">
    /// Type of elements contained in the <see cref="IEnumerable{T}" />.
    /// </typeparam>
    public static IEnumerable<T> Shift<T>(this IEnumerable<T> collection, int steps)
    {
        switch (steps)
        {
            case > 0:
            {
                using IEnumerator<T> e = collection.GetEnumerator();
                e.Reset();
                int j = 0;
                while (j++ < steps) e.MoveNext();
                while (e.MoveNext()) yield return e.Current;
                while (--j > 0)
                {
                    // Yield default to avoid compilation issues when T is a value type.
                    // This assumes default is an appropriate value for a shifted element.
                    yield return default!;
                }
                break;
            }
            case < 0:
            {
                using IEnumerator<T> e = collection.GetEnumerator();
                e.Reset();
                int j = 0;
                List<T> c = [];
                // Manual enumeration
                while (e.MoveNext()) c.Add(e.Current);
                while (j-- > steps)
                {
                    // Yield default to avoid compilation issues when T is a value type.
                    // This assumes default is an appropriate value for a shifted element.
                    yield return default!;
                }
                j += c.Count;
                while (j-- >= 0) yield return c.PopFirst();
                break;
            }
            default:
            {
                foreach (T? i in collection) yield return i;
                break;
            }
        }
    }

    /// <summary>
    /// Gets a value that indicates whether the value of the property of all
    /// objects in the collection is equal.
    /// </summary>
    /// <typeparam name="T">Type of objects in the collection.</typeparam>
    /// <param name="c">
    /// Collection containing the objects to check.
    /// </param>
    /// <param name="selector">
    /// Selector function.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the value of the property of all
    /// objects in the collection is the same, <see langword="false"/>
    /// otherwise.
    /// </returns>
    public static bool IsPropertyEqual<T>(this IEnumerable<T> c, Func<T, object> selector)
    {
        return AreAllEqual(c.Select(selector));
    }

    /// <summary>
    /// Checks whether all objects in the collection are equal.
    /// </summary>
    /// <typeparam name="T">Type of objects in the collection.</typeparam>
    /// <param name="c">
    /// Collection containing the objects to check.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if all objects in the collection are
    /// equal, <see langword="false"/> otherwise.
    /// </returns>
    public static bool AreAllEqual<T>(this IEnumerable<T> c)
    {
        using IEnumerator<T> j = c.GetEnumerator();
        if (!j.MoveNext()) throw new EmptyCollectionException(c);
        T eq = j.Current;
        while (j.MoveNext())
        {
            if (!eq?.Equals(j.Current) ?? j.Current is { }) return false;
        }
        return true;
    }

    /// <summary>
    /// Checks whether the property or value within all objects in the
    /// collection are equal.
    /// </summary>
    /// <typeparam name="T">Type of objects in the collection.</typeparam>
    /// <typeparam name="TProp">Type of value to compare.</typeparam>
    /// <param name="c">
    /// Collection containing the objects to check.
    /// </param>
    /// <param name="selector">
    /// Selector function for the value of each object.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if all selected values from the objects in the
    /// collection are equal, <see langword="false"/> otherwise.
    /// </returns>
    [Sugar]
    public static bool AreAllEqual<T, TProp>(this IEnumerable<T> c, Func<T, TProp> selector)
    {
        return c.Select(selector).AreAllEqual();
    }

    /// <summary>
    /// Finds the index of an object within a collection.
    /// </summary>
    /// <typeparam name="T">Type of objects in the collection.</typeparam>
    /// <param name="e">
    /// Collection containing the objects to check.
    /// </param>
    /// <param name="item">
    /// Item to get the index of.
    /// </param>
    /// <returns>
    /// The index of the specified object, or <c>-1</c> if the object does
    /// not exist within the collection.
    /// </returns>
    public static int FindIndexOf<T>(this IEnumerable<T> e, T item)
    {
        return (e, item) switch
        {
            (string s, char i) => s.ToCharArray().FindIndexOf(i),
            (IList<T> l, _) => l.IndexOf(item),
            _ => IndexOfEnumerable(e, item)
        };
    }

    /// <summary>
    /// Calculates the total duration by summing all <see cref="TimeSpan"/>
    /// values in the collection.
    /// </summary>
    /// <param name="collection">
    /// The collection of <see cref="TimeSpan"/> values to sum. Cannot be
    /// <see langword="null"/>.
    /// </param>
    /// <returns>
    /// A <see cref="TimeSpan"/> representing the total duration of all
    /// elements in the collection. Returns <see cref="TimeSpan.Zero"/> if the
    /// collection is empty.
    /// </returns>
    public static TimeSpan Sum(this IEnumerable<TimeSpan> collection)
    {
        return collection.Aggregate(TimeSpan.Zero, (acc, timeSpan) => acc + timeSpan);
    }

    /// <summary>
    /// Determines the most frequently occurring value in the provided collection that meets the specified quorum count.
    /// </summary>
    /// <typeparam name="T">The type of the values in the collection. Must be a non-nullable type.</typeparam>
    /// <param name="values">The collection of values to evaluate. Cannot be <see langword="null"/>.</param>
    /// <param name="quorumCount">The minimum number of occurrences required for a value to be considered a quorum.</param>
    /// <returns>The value that meets the quorum count and occurs most frequently in the collection.</returns>
    /// <exception cref="InvalidOperationException">Thrown if no value in the collection meets the specified quorum count.</exception>
    public static T Quorum<T>(this IEnumerable<T> values, int quorumCount) where T : notnull
    {
        if (IsQuorum(values, quorumCount, out T? value)) return value;
        throw new InvalidOperationException("No quorum found for the provided values.");
    }

    /// <summary>
    /// Determines whether a quorum is met for a specified value in a collection.
    /// </summary>
    /// <remarks>This method evaluates the collection to determine if any value appears at least <paramref
    /// name="quorumCount"/> times.  If multiple values meet the quorum, the value with the highest occurrence is
    /// selected.  If there is a tie, the first value encountered in the collection is returned.</remarks>
    /// <typeparam name="T">The type of elements in the collection. Must be a non-nullable type.</typeparam>
    /// <param name="values">The collection of values to evaluate.</param>
    /// <param name="quorumCount">The minimum number of occurrences required for a value to meet the quorum. Must be greater than or equal to 1.</param>
    /// <param name="value">When this method returns <see langword="true"/>, contains the value that meets the quorum.  When this method
    /// returns <see langword="false"/>, contains the default value for the type <typeparamref name="T"/>.</param>
    /// <returns><see langword="true"/> if a value in the collection meets or exceeds the specified quorum count;  otherwise,
    /// <see langword="false"/>.</returns>
    public static bool IsQuorum<T>(this IEnumerable<T> values, int quorumCount, [MaybeNullWhen(false)]out T value) where T : notnull
    {
        ArgumentNullException.ThrowIfNull(values);
        ArgumentOutOfRangeException.ThrowIfLessThan(quorumCount, 1);
        var x = values.GroupBy(p => p).OrderByDescending(p => p.Count()).FirstOrDefault();
        if (x is null || x.Count() < quorumCount)
        {
            value = default!;
            return false;
        }
        value = x.First();
        return true;
    }
}
