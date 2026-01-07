/*
CollectionHelpers_BitwiseOps.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Este archivo contiene funciones de manipulación de objetos, 

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

using static TheXDS.MCART.Misc.Internals;

namespace TheXDS.MCART.Helpers;

/// <summary>
/// Auxiliary functions to work with collections and enumerations.
/// </summary>
public static partial class CollectionHelpers
{
    /// <summary>
    /// Applies the AND operator to a collection of <see cref="bool"/> values.
    /// </summary>
    /// <param name="collection">
    /// Collection to apply the operation from.
    /// </param>
    /// <returns>
    /// The result of applying the AND operator to all bits in the collection.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="collection"/> is <see langword="null"/>.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Thrown if <paramref name="collection"/> does not contain any elements.
    /// </exception>
    public static bool And(this IEnumerable<bool> collection)
    {
        CheckPopulatedCollection(collection);
        return collection.Aggregate(true, (current, j) => current & j);
    }

    /// <summary>
    /// Applies the AND operator to a collection of <see cref="bool"/> values.
    /// </summary>
    /// <param name="collection">
    /// Collection to apply the operation from.
    /// </param>
    /// <returns>
    /// The result of applying the AND operator to all bits in the collection.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="collection"/> is <see langword="null"/>.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Thrown if <paramref name="collection"/> does not contain any elements.
    /// </exception>
    public static byte And(this IEnumerable<byte> collection)
    {
        CheckPopulatedCollection(collection);
        return collection.Aggregate(byte.MaxValue, (current, j) => (byte)(current & j));
    }

    /// <summary>
    /// Applies the AND operator to a collection of <see cref="char"/> values.
    /// </summary>
    /// <param name="collection">
    /// Collection to apply the operation from.
    /// </param>
    /// <returns>
    /// The result of applying the AND operator to all bits in the collection.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="collection"/> is <see langword="null"/>.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Thrown if <paramref name="collection"/> does not contain any elements.
    /// </exception>
    public static char And(this IEnumerable<char> collection)
    {
        CheckPopulatedCollection(collection);
        return collection.Aggregate(char.MaxValue, (current, j) => (char)(current & j));
    }

    /// <summary>
    /// Applies the AND operator to a collection of <see cref="int"/> values.
    /// </summary>
    /// <param name="collection">
    /// Collection to apply the operation from.
    /// </param>
    /// <returns>
    /// The result of applying the AND operator to all bits in the collection.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="collection"/> is <see langword="null"/>.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Thrown if <paramref name="collection"/> does not contain any elements.
    /// </exception>
    public static int And(this IEnumerable<int> collection)
    {
        CheckPopulatedCollection(collection);
        return collection.Aggregate(-1, (current, j) => current & j);
    }

    /// <summary>
    /// Applies the AND operator to a collection of <see cref="long"/> values.
    /// </summary>
    /// <param name="collection">
    /// Collection to apply the operation from.
    /// </param>
    /// <returns>
    /// The result of applying the AND operator to all bits in the collection.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="collection"/> is <see langword="null"/>.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Thrown if <paramref name="collection"/> does not contain any elements.
    /// </exception>
    public static long And(this IEnumerable<long> collection)
    {
        CheckPopulatedCollection(collection);
        return collection.Aggregate((long)-1, (current, j) => current & j);
    }

    /// <summary>
    /// Applies the AND operator to a collection of <see cref="sbyte"/> values.
    /// </summary>
    /// <param name="collection">
    /// Collection to apply the operation from.
    /// </param>
    /// <returns>
    /// The result of applying the AND operator to all bits in the collection.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="collection"/> is <see langword="null"/>.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Thrown if <paramref name="collection"/> does not contain any elements.
    /// </exception>
    [CLSCompliant(false)]
    public static sbyte And(this IEnumerable<sbyte> collection)
    {
        CheckPopulatedCollection(collection);
        return collection.Aggregate((sbyte)-1, (current, j) => (sbyte)(current & j));
    }

    /// <summary>
    /// Applies the AND operator to a collection of <see cref="short"/> values.
    /// </summary>
    /// <param name="collection">
    /// Collection to apply the operation from.
    /// </param>
    /// <returns>
    /// The result of applying the AND operator to all bits in the collection.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="collection"/> is <see langword="null"/>.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Thrown if <paramref name="collection"/> does not contain any elements.
    /// </exception>
    public static short And(this IEnumerable<short> collection)
    {
        CheckPopulatedCollection(collection);
        return collection.Aggregate((short)-1, (current, j) => (short)(current & j));
    }

    /// <summary>
    /// Applies the AND operator to a collection of <see cref="uint"/> values.
    /// </summary>
    /// <param name="collection">
    /// Collection to apply the operation from.
    /// </param>
    /// <returns>
    /// The result of applying the AND operator to all bits in the collection.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="collection"/> is <see langword="null"/>.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Thrown if <paramref name="collection"/> does not contain any elements.
    /// </exception>
    [CLSCompliant(false)]
    public static uint And(this IEnumerable<uint> collection)
    {
        CheckPopulatedCollection(collection);
        return collection.Aggregate(uint.MaxValue, (current, j) => current & j);
    }

    /// <summary>
    /// Applies the AND operator to a collection of <see cref="ulong"/> values.
    /// </summary>
    /// <param name="collection">
    /// Collection to apply the operation from.
    /// </param>
    /// <returns>
    /// The result of applying the AND operator to all bits in the collection.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="collection"/> is <see langword="null"/>.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Thrown if <paramref name="collection"/> does not contain any elements.
    /// </exception>
    [CLSCompliant(false)]
    public static ulong And(this IEnumerable<ulong> collection)
    {
        CheckPopulatedCollection(collection);
        return collection.Aggregate(ulong.MaxValue, (current, j) => current & j);
    }

    /// <summary>
    /// Applies the AND operator to a collection of <see cref="ushort"/>
    /// values.
    /// </summary>
    /// <param name="collection">
    /// Collection to apply the operation from.
    /// </param>
    /// <returns>
    /// The result of applying the AND operator to all bits in the collection.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="collection"/> is <see langword="null"/>.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Thrown if <paramref name="collection"/> does not contain any elements.
    /// </exception>
    [CLSCompliant(false)]
    public static ushort And(this IEnumerable<ushort> collection)
    {
        CheckPopulatedCollection(collection);
        return collection.Aggregate(ushort.MaxValue, (current, j) => (ushort)(current & j));
    }

    /// <summary>
    /// Applies the OR operator to a collection of <see cref="bool"/> values.
    /// </summary>
    /// <param name="collection">
    /// Collection to apply the operation from.
    /// </param>
    /// <returns>
    /// The result of applying the OR operator to all bits in the collection.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="collection"/> is <see langword="null"/>.
    /// </exception>
    public static bool Or(this IEnumerable<bool> collection)
    {
        ArgumentNullException.ThrowIfNull(collection);
        return collection.Aggregate(false, (current, j) => current | j);
    }

    /// <summary>
    /// Applies the OR operator to a collection of <see cref="byte"/> values.
    /// </summary>
    /// <param name="collection">
    /// Collection to apply the operation from.
    /// </param>
    /// <returns>
    /// The result of applying the OR operator to all bits in the collection.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="collection"/> is <see langword="null"/>.
    /// </exception>
    public static byte Or(this IEnumerable<byte> collection)
    {
        ArgumentNullException.ThrowIfNull(collection);
        return collection.Aggregate(default(byte), (current, j) => (byte)(current | j));
    }

    /// <summary>
    /// Applies the OR operator to a collection of <see cref="char"/> values.
    /// </summary>
    /// <param name="collection">
    /// Collection to apply the operation from.
    /// </param>
    /// <returns>
    /// The result of applying the OR operator to all bits in the collection.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="collection"/> is <see langword="null"/>.
    /// </exception>
    public static char Or(this IEnumerable<char> collection)
    {
        ArgumentNullException.ThrowIfNull(collection);
        return collection.Aggregate(default(char), (current, j) => (char)(current | j));
    }

    /// <summary>
    /// Applies the OR operator to a collection of <see cref="int"/> values.
    /// </summary>
    /// <param name="collection">
    /// Collection to apply the operation from.
    /// </param>
    /// <returns>
    /// The result of applying the OR operator to all bits in the collection.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="collection"/> is <see langword="null"/>.
    /// </exception>
    public static int Or(this IEnumerable<int> collection)
    {
        ArgumentNullException.ThrowIfNull(collection);
        return collection.Aggregate(default(int), (current, j) => current | j);
    }

    /// <summary>
    /// Applies the OR operator to a collection of <see cref="long"/> values.
    /// </summary>
    /// <param name="collection">
    /// Collection to apply the operation from.
    /// </param>
    /// <returns>
    /// The result of applying the OR operator to all bits in the collection.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="collection"/> is <see langword="null"/>.
    /// </exception>
    public static long Or(this IEnumerable<long> collection)
    {
        ArgumentNullException.ThrowIfNull(collection);
        return collection.Aggregate(default(long), (current, j) => current | j);
    }

    /// <summary>
    /// Applies the OR operator to a collection of <see cref="sbyte"/> values.
    /// </summary>
    /// <param name="collection">
    /// Collection to apply the operation from.
    /// </param>
    /// <returns>
    /// The result of applying the OR operator to all bits in the collection.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="collection"/> is <see langword="null"/>.
    /// </exception>
    [CLSCompliant(false)]
    public static sbyte Or(this IEnumerable<sbyte> collection)
    {
        ArgumentNullException.ThrowIfNull(collection);
        return collection.Aggregate(default(sbyte), (current, j) => (sbyte)(current | j));
    }

    /// <summary>
    /// Applies the OR operator to a collection of <see cref="short"/> values.
    /// </summary>
    /// <param name="collection">
    /// Collection to apply the operation from.
    /// </param>
    /// <returns>
    /// The result of applying the OR operator to all bits in the collection.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="collection"/> is <see langword="null"/>.
    /// </exception>
    public static short Or(this IEnumerable<short> collection)
    {
        ArgumentNullException.ThrowIfNull(collection);
        return collection.Aggregate(default(short), (current, j) => (short)(current | j));
    }

    /// <summary>
    /// Applies the OR operator to a collection of <see cref="uint"/> values.
    /// </summary>
    /// <param name="collection">
    /// Collection to apply the operation from.
    /// </param>
    /// <returns>
    /// The result of applying the OR operator to all bits in the collection.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="collection"/> is <see langword="null"/>.
    /// </exception>
    [CLSCompliant(false)]
    public static uint Or(this IEnumerable<uint> collection)
    {
        ArgumentNullException.ThrowIfNull(collection);
        return collection.Aggregate(default(uint), (current, j) => current | j);
    }

    /// <summary>
    /// Applies the OR operator to a collection of <see cref="ulong"/> values.
    /// </summary>
    /// <param name="collection">
    /// Collection to apply the operation from.
    /// </param>
    /// <returns>
    /// The result of applying the OR operator to all bits in the collection.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="collection"/> is <see langword="null"/>.
    /// </exception>
    [CLSCompliant(false)]
    public static ulong Or(this IEnumerable<ulong> collection)
    {
        ArgumentNullException.ThrowIfNull(collection);
        return collection.Aggregate(default(ulong), (current, j) => current | j);
    }

    /// <summary>
    /// Applies the OR operator to a collection of <see cref="ushort"/> values.
    /// </summary>
    /// <param name="collection">
    /// Collection to apply the operation from.
    /// </param>
    /// <returns>
    /// The result of applying the OR operator to all bits in the collection.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="collection"/> is <see langword="null"/>.
    /// </exception>
    [CLSCompliant(false)]
    public static ushort Or(this IEnumerable<ushort> collection)
    {
        ArgumentNullException.ThrowIfNull(collection);
        return collection.Aggregate(default(ushort), (current, j) => (ushort)(current | j));
    }

    /// <summary>
    /// Applies the XOR operator to a collection of <see cref="bool"/> values.
    /// </summary>
    /// <param name="collection">
    /// Collection to apply the operation from.
    /// </param>
    /// <returns>
    /// The result of applying the XOR operator to all bits in the collection.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="collection"/> is <see langword="null"/>.
    /// </exception>
    public static bool Xor(this IEnumerable<bool> collection)
    {
        ArgumentNullException.ThrowIfNull(collection);
        return collection.Aggregate(false, (current, j) => current ^ j);
    }

    /// <summary>
    /// Applies the XOR operator to a collection of <see cref="byte"/> values.
    /// </summary>
    /// <param name="collection">
    /// Collection to apply the operation from.
    /// </param>
    /// <returns>
    /// The result of applying the XOR operator to all bits in the collection.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="collection"/> is <see langword="null"/>.
    /// </exception>
    public static byte Xor(this IEnumerable<byte> collection)
    {
        ArgumentNullException.ThrowIfNull(collection);
        return collection.Aggregate(default(byte), (current, j) => (byte)(current ^ j));
    }

    /// <summary>
    /// Applies the XOR operator to a collection of <see cref="char"/> values.
    /// </summary>
    /// <param name="collection">
    /// Collection to apply the operation from.
    /// </param>
    /// <returns>
    /// The result of applying the XOR operator to all bits in the collection.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="collection"/> is <see langword="null"/>.
    /// </exception>
    public static char Xor(this IEnumerable<char> collection)
    {
        ArgumentNullException.ThrowIfNull(collection);
        return collection.Aggregate(default(char), (current, j) => (char)(current ^ j));
    }

    /// <summary>
    /// Applies the XOR operator to a collection of <see cref="int"/> values.
    /// </summary>
    /// <param name="collection">
    /// Collection to apply the operation from.
    /// </param>
    /// <returns>
    /// The result of applying the XOR operator to all bits in the collection.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="collection"/> is <see langword="null"/>.
    /// </exception>
    public static int Xor(this IEnumerable<int> collection)
    {
        ArgumentNullException.ThrowIfNull(collection);
        return collection.Aggregate(default(int), (current, j) => current ^ j);
    }

    /// <summary>
    /// Applies the XOR operator to a collection of <see cref="long"/> values.
    /// </summary>
    /// <param name="collection">
    /// Collection to apply the operation from.
    /// </param>
    /// <returns>
    /// The result of applying the XOR operator to all bits in the collection.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="collection"/> is <see langword="null"/>.
    /// </exception>
    public static long Xor(this IEnumerable<long> collection)
    {
        ArgumentNullException.ThrowIfNull(collection);
        return collection.Aggregate(default(long), (current, j) => current ^ j);
    }

    /// <summary>
    /// Applies the XOR operator to a collection of <see cref="sbyte"/> values.
    /// </summary>
    /// <param name="collection">
    /// Collection to apply the operation from.
    /// </param>
    /// <returns>
    /// The result of applying the XOR operator to all bits in the collection.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="collection"/> is <see langword="null"/>.
    /// </exception>
    [CLSCompliant(false)]
    public static sbyte Xor(this IEnumerable<sbyte> collection)
    {
        ArgumentNullException.ThrowIfNull(collection);
        return collection.Aggregate(default(sbyte), (current, j) => (sbyte)(current ^ j));
    }

    /// <summary>
    /// Applies the XOR operator to a collection of <see cref="short"/> values.
    /// </summary>
    /// <param name="collection">
    /// Collection to apply the operation from.
    /// </param>
    /// <returns>
    /// The result of applying the XOR operator to all bits in the collection.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="collection"/> is <see langword="null"/>.
    /// </exception>
    public static short Xor(this IEnumerable<short> collection)
    {
        ArgumentNullException.ThrowIfNull(collection);
        return collection.Aggregate(default(short), (current, j) => (short)(current ^ j));
    }

    /// <summary>
    /// Applies the XOR operator to a collection of <see cref="uint"/> values.
    /// </summary>
    /// <param name="collection">
    /// Collection to apply the operation from.
    /// </param>
    /// <returns>
    /// The result of applying the XOR operator to all bits in the collection.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="collection"/> is <see langword="null"/>.
    /// </exception>
    [CLSCompliant(false)]
    public static uint Xor(this IEnumerable<uint> collection)
    {
        ArgumentNullException.ThrowIfNull(collection);
        return collection.Aggregate(default(uint), (current, j) => current ^ j);
    }

    /// <summary>
    /// Applies the XOR operator to a collection of <see cref="ulong"/> values.
    /// </summary>
    /// <param name="collection">
    /// Collection to apply the operation from.
    /// </param>
    /// <returns>
    /// The result of applying the XOR operator to all bits in the collection.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="collection"/> is <see langword="null"/>.
    /// </exception>
    [CLSCompliant(false)]
    public static ulong Xor(this IEnumerable<ulong> collection)
    {
        ArgumentNullException.ThrowIfNull(collection);
        return collection.Aggregate(default(ulong), (current, j) => current ^ j);
    }

    /// <summary>
    /// Applies the XOR operator to a collection of <see cref="ushort"/>
    /// values.
    /// </summary>
    /// <param name="collection">
    /// Collection to apply the operation from.
    /// </param>
    /// <returns>
    /// The result of applying the XOR operator to all bits in the collection.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="collection"/> is <see langword="null"/>.
    /// </exception>
    [CLSCompliant(false)]
    public static ushort Xor(this IEnumerable<ushort> collection)
    {
        ArgumentNullException.ThrowIfNull(collection);
        return collection.Aggregate(default(ushort), (current, j) => (ushort)(current ^ j));
    }
}
