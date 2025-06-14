/*
ListExtensions.cs

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

using System.Collections;
using TheXDS.MCART.Exceptions;
using TheXDS.MCART.Resources;

namespace TheXDS.MCART.Types.Extensions;

/// <summary>
/// Extensions for all items of type <see cref="IList{T}" />.
/// </summary>
public static partial class ListExtensions
{
    /// <summary>
    /// Removes all items of the specified type from the
    /// collection.
    /// </summary>
    /// <typeparam name="T">Type of items to remove.</typeparam>
    /// <param name="c">Collection from which to remove items.</param>
    public static void RemoveOf<T>(this IList c)
    {
        int i = 0;
        while (i < c.Count)
        {
            if (c[i] is T)
            {
                c.RemoveAt(i);
            }
            else
            {
                i++;
            }
        }
    }

    /// <summary>
    /// Applies a rotation operation on the <see cref="IList{T}"/>.
    /// </summary>
    /// <typeparam name="T">Type of items in the collection.</typeparam>
    /// <param name="c">Collection to rotate.</param>
    /// <param name="steps">
    /// Rotation steps. A positive value will rotate the elements to the
    /// left, and a negative one will rotate them to the right.
    /// </param>
    public static void ApplyRotate<T>(this IList<T> c, in int steps)
    {
        if (steps > 0)
            for (int j = 0; j < steps; j++)
                c.Add(c.PopFirst());
        else if (steps < 0)
            for (int j = 0; j > steps; j--)
                c.Insert(0, c.Pop());
    }

    /// <summary>
    /// Applies a shift operation on the
    /// <see cref="IList{T}"/>.
    /// </summary>
    /// <typeparam name="T">Type of items in the collection.</typeparam>
    /// <param name="c">Collection to shift.</param>
    /// <param name="steps">
    /// Shift steps. A positive value will shift the elements
    /// to the left, and a negative one will shift them to the right,
    /// in both cases filling each resulting empty position with default values.
    /// </param>
    public static void ApplyShift<T>(this IList<T> c, in int steps)
    {
        if (steps > 0)
            for (int j = 0; j < steps; j++)
            {
                c.PopFirst();
                c.Add(default!);
            }
        else if (steps < 0)
            for (int j = 0; j > steps; j--)
            {
                c.Pop();
                c.Insert(0, default!);
            }
    }

    /// <summary>
    /// Executes a list operation in a locked context.
    /// </summary>
    /// <param name="list">
    /// List on which to execute a locked operation.
    /// </param>
    /// <param name="action">
    /// Action to execute on the list.
    /// </param>
    public static void Locked(this IList list, Action<IList> action)
    {
        if (list.IsSynchronized) action(list);
        else lock (list.SyncRoot) action(list);
    }

    /// <summary>
    /// Shuffles the elements of an <see cref="IList{T}" />.
    /// </summary>
    /// <typeparam name="T">
    /// Type of elements contained in the <see cref="IList{T}" />.
    /// </typeparam>
    /// <param name="c"><see cref="IList{T}" /> to shuffle.</param>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="c" /> is <see langword="null" />.
    /// </exception>
    /// <exception cref="EmptyCollectionException">
    /// Thrown if <paramref name="c" /> refers to an empty collection.
    /// </exception>
    public static void Shuffle<T>(this IList<T> c)
    {
        Shuffle(c, 1);
    }

    /// <summary>
    /// Shuffles the elements of an <see cref="IList{T}" />.
    /// </summary>
    /// <typeparam name="T">
    /// Type of elements contained in the <see cref="IList{T}" />.
    /// </typeparam>
    /// <param name="c"><see cref="IList{T}" /> to shuffle.</param>
    /// <param name="deepness">
    /// Depth of the shuffle. 1 is the highest value.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="c" /> is <see langword="null" />.
    /// </exception>
    /// <exception cref="EmptyCollectionException">
    /// Thrown if <paramref name="c" /> refers to an empty collection.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown if <paramref name="deepness" /> is less than 1, or
    /// greater than the count of elements in the collection to shuffle.
    /// </exception>
    public static void Shuffle<T>(this IList<T> c, in int deepness)
    {
        Shuffle(c, 0, c.Count - 1, deepness);
    }

    /// <summary>
    /// Shuffles the elements of the specified interval of an
    /// <see cref="IList{T}" />.
    /// </summary>
    /// <typeparam name="T">
    /// Type of elements contained in the <see cref="IList{T}" />.
    /// </typeparam>
    /// <param name="c"><see cref="IList{T}" /> to shuffle.</param>
    /// <param name="firstIdx">Initial index of the interval.</param>
    /// <param name="lastIdx">Initial index of the interval.</param>
    public static void Shuffle<T>(this IList<T> c, in int firstIdx, in int lastIdx)
    {
        Shuffle(c, firstIdx, lastIdx, 1);
    }

    /// <summary>
    /// Shuffles the elements of the specified interval of an
    /// <see cref="IList{T}" />.
    /// </summary>
    /// <typeparam name="T">
    /// Type of elements contained in the <see cref="IList{T}" />.
    /// </typeparam>
    /// <param name="toShuffle"><see cref="IList{T}" /> to shuffle.</param>
    /// <param name="deepness">Depth of the shuffle. 1 is the highest.</param>
    /// <param name="firstIdx">Initial index of the interval.</param>
    /// <param name="lastIdx">Initial index of the interval.</param>
    public static void Shuffle<T>(this IList<T> toShuffle, in int firstIdx, in int lastIdx, in int deepness)
    {
        Shuffle(toShuffle, firstIdx, lastIdx, deepness, RandomExtensions.Rnd);
    }

    /// <summary>
    /// Shuffles the elements of the specified interval of an
    /// <see cref="IList{T}" />.
    /// </summary>
    /// <typeparam name="T">
    /// Type of elements contained in the <see cref="IList{T}" />.
    /// </typeparam>
    /// <param name="toShuffle"><see cref="IList{T}" /> to shuffle.</param>
    /// <param name="deepness">Depth of the shuffle. 1 is the highest.</param>
    /// <param name="firstIdx">Initial index of the interval.</param>
    /// <param name="lastIdx">Initial index of the interval.</param>
    /// <param name="random">Random number generator to use.</param>
    public static void Shuffle<T>(this IList<T> toShuffle, int firstIdx, int lastIdx, in int deepness, Random random)
    {
        Shuffle_Contract(toShuffle, firstIdx, lastIdx, deepness, random);
        lastIdx++;
        for (int j = firstIdx; j < lastIdx; j += deepness)
        {
            toShuffle.Swap(j, random.Next(firstIdx, lastIdx));
        }
    }

    /// <summary>
    /// Swaps the position of two elements within an <see cref="IList{T}"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="collection">Collection on which to swap the position of two elements.</param>
    /// <param name="indexA">Index of the first element.</param>
    /// <param name="indexB">Index of the second element.</param>
    public static void Swap<T>(this IList<T> collection, in int indexA, in int indexB)
    {
        if (indexA == indexB) return;
        (collection[indexB], collection[indexA]) = (collection[indexA], collection[indexB]);
    }

    /// <summary>
    /// Swaps the position of two elements within an <see cref="IList{T}"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="collection">Collection on which to swap the position of two elements.</param>
    /// <param name="a">First element.</param>
    /// <param name="b">Second element.</param>
    public static void Swap<T>(this IList<T> collection, T a, T b)
    {
        if (!collection.ContainsAll(a, b)) throw Errors.ListMustContainBoth();
        Swap(collection, collection.IndexOf(a), collection.IndexOf(b));
    }
}
