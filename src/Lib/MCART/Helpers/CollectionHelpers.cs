/*
CollectionHelpers.cs

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
using TheXDS.MCART.Math;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.Helpers;

/// <summary>
/// Includes useful extension methods for common collections and enumerations.
/// </summary>
public static partial class CollectionHelpers
{
    /// <summary>
    /// Determines if all the specified strings are either
    /// <see langword="null"/>, <see cref="string.Empty"/> or a string
    /// consisting only of whitespaces.
    /// </summary>
    /// <param name="stringCollection">
    /// Async enumeration of strings to be checked.
    /// </param>
    /// <returns>
    /// <see langword="true" /> all of the strings are either
    /// <see langword="null" />, <see cref="string.Empty"/> or a string
    /// consisting only of whitespaces; or <see langword="false" /> otherwise.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="stringCollection"/> is
    /// <see langword="null"/>.
    /// </exception>
    public static async Task<bool> AllEmpty(this IAsyncEnumerable<string?> stringCollection)
    {
        ArgumentNullException.ThrowIfNull(stringCollection);
        await foreach (string? j in stringCollection.ConfigureAwait(false))
        {
            if (!j.IsEmpty()) return false;
        }
        return true;
    }

    /// <summary>
    /// Determines if all the specified strings are either
    /// <see langword="null"/>, <see cref="string.Empty"/> or a string
    /// consisting only of whitespaces.
    /// </summary>
    /// <param name="stringCollection">
    /// Enumeration of strings to be checked.
    /// </param>
    /// <returns>
    /// <see langword="true" /> all of the strings are either
    /// <see langword="null" />, <see cref="string.Empty"/> or a string
    /// consisting only of whitespaces; or <see langword="false" /> otherwise.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="stringCollection"/> is
    /// <see langword="null"/>.
    /// </exception>
    public static bool AllEmpty(this IEnumerable<string?> stringCollection)
    {
        ArgumentNullException.ThrowIfNull(stringCollection);
        return stringCollection.All(j => j.IsEmpty());
    }

    /// <summary>
    /// Determines if any the specified strings are either
    /// <see langword="null"/>, <see cref="string.Empty"/> or a string
    /// consisting only of whitespaces.
    /// </summary>
    /// <param name="stringCollection">
    /// Async enumeration of strings to be checked.
    /// </param>
    /// <returns>
    /// <see langword="true" /> any of the strings are either
    /// <see langword="null" />, <see cref="string.Empty"/> or a string
    /// consisting only of whitespaces; or <see langword="false" /> otherwise.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="stringCollection"/> is
    /// <see langword="null"/>.
    /// </exception>
    public static async Task<bool> AnyEmpty(this IAsyncEnumerable<string?> stringCollection)
    {
        ArgumentNullException.ThrowIfNull(stringCollection);
        await foreach (string? j in stringCollection.ConfigureAwait(false))
        {
            if (j.IsEmpty()) return true;
        }
        return false;
    }

    /// <summary>
    /// Determines if any the specified strings are either
    /// <see langword="null"/>, <see cref="string.Empty"/> or a string
    /// consisting only of whitespaces.
    /// </summary>
    /// <param name="stringCollection">
    /// Enumeration of strings to be checked.
    /// </param>
    /// <param name="index">
    /// Out parameter. Indices of the items that were <see langword="null"/>,
    /// empty or a string consisting only of whitespaces.
    /// </param>
    /// <returns>
    /// <see langword="true" /> any of the strings are either
    /// <see langword="null" />, <see cref="string.Empty"/> or a string
    /// consisting only of whitespaces; or <see langword="false" /> otherwise.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="stringCollection"/> is
    /// <see langword="null"/>.
    /// </exception>
    public static bool AnyEmpty(this IEnumerable<string?> stringCollection, out IEnumerable<int> index)
    {
        ArgumentNullException.ThrowIfNull(stringCollection);
        List<int> idx = [];
        int c = 0;
        foreach (string? j in stringCollection)
        {
            if (j.IsEmpty()) idx.Add(c);
            c++;
        }
        index = idx.AsEnumerable();
        return idx.Count != 0;
    }

    /// <summary>
    /// Determines if any the specified strings are either
    /// <see langword="null"/>, <see cref="string.Empty"/> or a string
    /// consisting only of whitespaces.
    /// </summary>
    /// <param name="stringCollection">
    /// Enumeration of strings to be checked.
    /// </param>
    /// <param name="firstIndex">
    /// Out parameter. Index of the first item that was <see langword="null"/>,
    /// empty or a string consisting only of whitespaces.
    /// </param>
    /// <returns>
    /// <see langword="true" /> any of the strings are either
    /// <see langword="null" />, <see cref="string.Empty"/> or a string
    /// consisting only of whitespaces; or <see langword="false" /> otherwise.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="stringCollection"/> is
    /// <see langword="null"/>.
    /// </exception>
    public static bool AnyEmpty(this IEnumerable<string?> stringCollection, out int firstIndex)
    {
        ArgumentNullException.ThrowIfNull(stringCollection);
        bool r = AnyEmpty(stringCollection, out IEnumerable<int> indexes);
        int[] a = [.. indexes];
        firstIndex = a.Length != 0 ? a.First() : -1;
        return r;
    }

    /// <summary>
    /// Determines if any the specified strings are either
    /// <see langword="null"/>, <see cref="string.Empty"/> or a string
    /// consisting only of whitespaces.
    /// </summary>
    /// <param name="stringCollection">
    /// Enumeration of strings to be checked.
    /// </param>
    /// <returns>
    /// <see langword="true" /> any of the strings are either
    /// <see langword="null" />, <see cref="string.Empty"/> or a string
    /// consisting only of whitespaces; or <see langword="false" /> otherwise.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="stringCollection"/> is
    /// <see langword="null"/>.
    /// </exception>
    public static bool AnyEmpty(this IEnumerable<string?> stringCollection)
    {
        ArgumentNullException.ThrowIfNull(stringCollection);
        return stringCollection.Any(j => j.IsEmpty());
    }

    /// <summary>
    /// Determines if all the specified strings are either
    /// <see langword="null"/>, <see cref="string.Empty"/> or a string
    /// consisting only of whitespaces.
    /// </summary>
    /// <param name="collection">
    /// Enumeration of strings to be checked.
    /// </param>
    /// <returns>
    /// <see langword="true" /> any of the strings are either
    /// <see langword="null" />, <see cref="string.Empty"/> or a string
    /// consisting only of whitespaces; or <see langword="false" /> otherwise.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="collection"/> is
    /// <see langword="null"/>.
    /// </exception>
    public static bool AreAllNull(this IEnumerable<object?> collection)
    {
        ArgumentNullException.ThrowIfNull(collection);
        return collection.All(p => p is null);
    }

    /// <summary>
    /// Determines if any of the specified objects is <see langword="null" />.
    /// </summary>
    /// <param name="collection">Collection of objects to be checked.</param>
    /// <returns>
    /// <see langword="true" /> if any of the specified objects is
    /// <see langword="null" />, <see langword="false" /> otherwise.
    /// </returns>
    public static bool IsAnyNull(this IEnumerable<object?>? collection)
    {
        return collection?.Any(p => p is null) ?? true;
    }

    /// <summary>
    /// Determines if any of the specified objects is <see langword="null" />.
    /// </summary>
    /// <param name="collection">Collection of objects to be checked.</param>
    /// <param name="index">
    /// Out parameter. Indices of the objects that were <see langword="null"/>.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if any of the specified objects is
    /// <see langword="null" />, <see langword="false" /> otherwise.
    /// </returns>
    public static bool IsAnyNull(this IEnumerable<object?> collection, out IEnumerable<int> index)
    {
        ArgumentNullException.ThrowIfNull(collection);
        List<int> idx = [];
        int c = 0;
        foreach (object? j in collection)
        {
            if (j is null) idx.Add(c);
            c++;
        }
        index = idx.AsEnumerable();
        return idx.Count != 0;
    }

    /// <summary>
    /// Determines if any of the specified objects is <see langword="null" />.
    /// </summary>
    /// <param name="collection">Collection of objects to be checked.</param>
    /// <param name="firstIndex">
    /// Out parameter. Index of the first object that was
    /// <see langword="null"/>.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if any of the specified objects is
    /// <see langword="null" />, <see langword="false" /> otherwise.
    /// </returns>
    public static bool IsAnyNull(this IEnumerable<object?> collection, out int firstIndex)
    {
        ArgumentNullException.ThrowIfNull(collection);
        foreach (var (index, element) in collection.WithIndex())
        {
            if (element is null)
            {
                firstIndex = index;
                return true;
            }
        }
        firstIndex = -1;
        return false;
    }

    /// <summary>
    /// Enumerates all non-empty strings in a collection.
    /// </summary>
    /// <param name="stringCollection">
    /// String collection to filter.
    /// </param>
    /// <returns>
    /// An enumeration of all non-empty strings in the collection.
    /// </returns>
    public static IEnumerable<string> NotEmpty(this IEnumerable<string?> stringCollection)
    {
        ArgumentNullException.ThrowIfNull(stringCollection);
        foreach (string? j in stringCollection)
        {
            if (!j.IsEmpty()) yield return j;
        }
    }

    /// <summary>
    /// Enumerates all non-empty strings in a collection.
    /// </summary>
    /// <param name="stringCollection">
    /// String collection to filter.
    /// </param>
    /// <returns>
    /// An enumeration of all non-empty strings in the collection.
    /// </returns>
    public static async IAsyncEnumerable<string> NotEmpty(this IAsyncEnumerable<string?> stringCollection)
    {
        ArgumentNullException.ThrowIfNull(stringCollection);
        await foreach (string? j in stringCollection)
        {
            if (!j.IsEmpty()) yield return j;
        }
    }

    /// <summary>
    /// Converts the values in a numerical collection to their respective values
    /// as percentages.
    /// </summary>
    /// <param name="collection">Collection to be converted.</param>
    /// <param name="min">Value that represents 0%.</param>
    /// <param name="max">Value that represents 100%.</param>
    /// <returns>
    /// A <see cref="double" /> collection of values as percentages.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown <paramref name="collection"/> is <see langword="null"/>.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Thrown if <paramref name="min"/> and <paramref name="max"/> are equal.
    /// </exception>
    public static async IAsyncEnumerable<double> ToPercent(this IAsyncEnumerable<double> collection, double min, double max)
    {
        ToPercent_Contract(collection, min, max);
        await foreach (double j in collection)
        {
            yield return j.IsValid()
                ? (j - min) / (max - min).Clamp(1, double.NaN)
                : double.NaN;
        }
    }

    /// <summary>
    /// Converts the values in a numerical collection to their respective values
    /// as percentages.
    /// </summary>
    /// <param name="collection">Collection to be converted.</param>
    /// <param name="max">Value that represents 100%.</param>
    /// <returns>
    /// A <see cref="double" /> collection of values as percentages.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown <paramref name="collection"/> is <see langword="null"/>.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Thrown if <paramref name="max"/> is equal to zero.
    /// </exception>
    public static IAsyncEnumerable<double> ToPercent(this IAsyncEnumerable<double> collection, in double max)
    {
        return ToPercent(collection, 0, max);
    }

    /// <summary>
    /// Converts the values in a numerical collection to their respective values
    /// as percentages.
    /// </summary>
    /// <param name="collection">Collection to be converted.</param>
    /// <param name="min">Value that represents 0%.</param>
    /// <param name="max">Value that represents 100%.</param>
    /// <returns>
    /// A <see cref="double" /> collection of values as percentages.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown <paramref name="collection"/> is <see langword="null"/>.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Thrown if <paramref name="min"/> and <paramref name="max"/> are equal.
    /// </exception>
    public static async IAsyncEnumerable<float> ToPercent(this IAsyncEnumerable<float> collection, float min, float max)
    {
        ToPercent_Contract(collection, min, max);
        await foreach (float j in collection)
            if (j.IsValid())
                yield return (j - min) / (max - min).Clamp(1, float.NaN);
            else
                yield return float.NaN;
    }

    /// <summary>
    /// Converts the values in a numerical collection to their respective values
    /// as percentages.
    /// </summary>
    /// <param name="collection">Collection to be converted.</param>
    /// <param name="max">Value that represents 100%.</param>
    /// <returns>
    /// A <see cref="double" /> collection of values as percentages.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown <paramref name="collection"/> is <see langword="null"/>.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Thrown if <paramref name="max"/> is equal to zero.
    /// </exception>
    public static IAsyncEnumerable<float> ToPercent(this IAsyncEnumerable<float> collection, in float max)
    {
        return ToPercent(collection, 0, max);
    }

    /// <summary>
    /// Converts the values in a numerical collection to their respective values
    /// as percentages.
    /// </summary>
    /// <param name="collection">Collection to be converted.</param>
    /// <param name="min">Value that represents 0%.</param>
    /// <param name="max">Value that represents 100%.</param>
    /// <returns>
    /// A <see cref="double" /> collection of values as percentages.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown <paramref name="collection"/> is <see langword="null"/>.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Thrown if <paramref name="min"/> and <paramref name="max"/> are equal.
    /// </exception>
    public static IEnumerable<double> ToPercent(this IEnumerable<double> collection, double min, double max)
    {
        ToPercent_Contract(collection, min, max);
        foreach (double j in collection)
            if (j.IsValid())
                yield return (j - min) / (max - min).Clamp(1, double.NaN);
            else
                yield return double.NaN;
    }

    /// <summary>
    /// Converts the values in a numerical collection to their respective values
    /// as percentages.
    /// </summary>
    /// <param name="collection">Collection to be converted.</param>
    /// <returns>
    /// A <see cref="double" /> collection of values as percentages.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown <paramref name="collection"/> is <see langword="null"/>.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the maximum value in the collection is equal to zero.
    /// </exception>
    public static IEnumerable<double> ToPercentAbsolute(this IEnumerable<double> collection)
    {
        ArgumentNullException.ThrowIfNull(collection);
        List<double> enumerable = [.. collection];
        return ToPercent(enumerable, 0, enumerable.Max());
    }

    /// <summary>
    /// Converts the values of a collection of elements
    /// <see cref="double" /> to percentages.
    /// </summary>
    /// <returns>
    /// A collection of <see cref="double" /> with its values
    /// expressed as percentages.
    /// </returns>
    /// <param name="collection">Collection to process.</param>
    /// <param name="max">Value that will represent 100%.</param>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="collection"/> is
    /// <see langword="null"/>.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the minimum value of the collection and
    /// <paramref name="max"/> are equal.
    /// </exception>
    public static IEnumerable<double> ToPercent(this IEnumerable<double> collection, in double max)
    {
        return ToPercent(collection, 0, max);
    }

    /// <summary>
    /// Converts the values of a collection of elements
    /// <see cref="double" /> to percentages.
    /// </summary>
    /// <returns>
    /// A collection of <see cref="double" /> with its values
    /// expressed as percentages.
    /// </returns>
    /// <param name="collection">Collection to process.</param>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="collection"/> is
    /// <see langword="null"/>.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the minimum and maximum values of the collection are equal.
    /// </exception>
    public static IEnumerable<double> ToPercent(this IEnumerable<double> collection)
    {
        ArgumentNullException.ThrowIfNull(collection);
        List<double> enumerable = [.. collection];
        return ToPercent(enumerable, enumerable.Min(), enumerable.Max());
    }

    /// <summary>
    /// Converts the values of a collection of elements
    /// <see cref="float" /> to percentages.
    /// </summary>
    /// <returns>
    /// A collection of <see cref="float" /> with its values
    /// expressed as percentages.
    /// </returns>
    /// <param name="collection">Collection to process.</param>
    /// <param name="min">Value that will represent 0%.</param>
    /// <param name="max">Value that will represent 100%.</param>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="collection"/> is
    /// <see langword="null"/>.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Thrown if <paramref name="min"/> and <paramref name="max"/> are
    /// equal.
    /// </exception>
    public static IEnumerable<float> ToPercent(this IEnumerable<float> collection, float min, float max)
    {
        ToPercent_Contract(collection, min, max);
        foreach (float j in collection)
        {
            if (j.IsValid())
            {
                yield return (j - min) / (max - min).Clamp(1, float.NaN);
            }
            else
            {
                yield return float.NaN;
            }
        }
    }

    /// <summary>
    /// Converts the values of a collection of elements
    /// <see cref="float" /> to percentages.
    /// </summary>
    /// <returns>
    /// A collection of <see cref="float" /> with its values
    /// expressed as percentages.
    /// </returns>
    /// <param name="collection">Collection to process.</param>
    /// <param name="baseZero">
    /// If <see langword="true" />, the base of
    /// percentage is zero; otherwise, the minimum value
    /// within the collection will be used.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="collection"/> is
    /// <see langword="null"/>.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the minimum and maximum values of the collection are equal.
    /// </exception>
    public static IEnumerable<float> ToPercent(this IEnumerable<float> collection, in bool baseZero)
    {
        ArgumentNullException.ThrowIfNull(collection);
        List<float> enumerable = [.. collection];
        return ToPercent(enumerable, baseZero ? 0 : enumerable.Min(), enumerable.Max());
    }

    /// <summary>
    /// Converts the values of a collection of elements
    /// <see cref="float" /> to percentages.
    /// </summary>
    /// <returns>
    /// A collection of <see cref="float" /> with its values
    /// expressed as percentages.
    /// </returns>
    /// <param name="collection">Collection to process.</param>
    /// <param name="max">Value that will represent 100%.</param>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="collection"/> is
    /// <see langword="null"/>.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the minimum value of the collection and
    /// <paramref name="max"/> are equal.
    /// </exception>
    public static IEnumerable<float> ToPercent(this IEnumerable<float> collection, in float max)
    {
        return ToPercent(collection, 0, max);
    }

    /// <summary>
    /// Converts the values of a collection of elements
    /// <see cref="float" /> to percentages.
    /// </summary>
    /// <returns>
    /// A collection of <see cref="float" /> with its values
    /// expressed as percentages.
    /// </returns>
    /// <param name="collection">Collection to process.</param>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="collection"/> is
    /// <see langword="null"/>.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the minimum and maximum values of the collection are equal.
    /// </exception>
    public static IEnumerable<float> ToPercent(this IEnumerable<float> collection)
    {
        ArgumentNullException.ThrowIfNull(collection);
        List<float> enumerable = [.. collection];
        return ToPercent(enumerable, enumerable.Min(), enumerable.Max());
    }

    /// <summary>
    /// Converts the values of a collection of elements
    /// <see cref="int" /> to percentages.
    /// </summary>
    /// <returns>
    /// A collection of <see cref="double" /> with its values
    /// expressed as percentages.
    /// </returns>
    /// <param name="collection">Collection to process.</param>
    /// <param name="max">Value that will represent 100%.</param>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="collection"/> is
    /// <see langword="null"/>.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the minimum value of the collection and
    /// <paramref name="max"/> are equal.
    /// </exception>
    public static IAsyncEnumerable<double> ToPercentDouble(this IAsyncEnumerable<int> collection, in int max)
    {
        return ToPercentDouble(collection, 0, max);
    }

    /// <summary>
    /// Converts the values of a collection of elements
    /// <see cref="int" /> to percentages.
    /// </summary>
    /// <returns>
    /// A collection of <see cref="double" /> with its values
    /// expressed as percentages.
    /// </returns>
    /// <param name="collection">Collection to process.</param>
    /// <param name="min">Value that will represent 0%.</param>
    /// <param name="max">Value that will represent 100%.</param>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="collection"/> is
    /// <see langword="null"/>.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Thrown if <paramref name="min"/> and <paramref name="max"/> are
    /// equal.
    /// </exception>
    public static async IAsyncEnumerable<double> ToPercentDouble(this IAsyncEnumerable<int> collection, int min, int max)
    {
        ToPercent_Contract(collection, min, max);
        await foreach (int j in collection) yield return (j - min) / (double)(max - min);
    }

    /// <summary>
    /// Converts the values of a collection of elements
    /// <see cref="int" /> to percentages.
    /// </summary>
    /// <returns>
    /// A collection of <see cref="double" /> with its values
    /// expressed as percentages.
    /// </returns>
    /// <param name="collection">Collection to process.</param>
    /// <param name="baseZero">
    /// Optional. If <see langword="true" />, the base of
    /// percentage is zero; otherwise, the minimum value
    /// within the collection will be used.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="collection"/> is
    /// <see langword="null"/>.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the minimum and maximum values of the collection are equal.
    /// </exception>
    public static IEnumerable<double> ToPercentDouble(this IEnumerable<int> collection, in bool baseZero)
    {
        ArgumentNullException.ThrowIfNull(collection);
        List<int> enumerable = [.. collection];
        return ToPercentDouble(enumerable, baseZero ? 0 : enumerable.Min(), enumerable.Max());
    }

    /// <summary>
    /// Converts the values of a collection of elements
    /// <see cref="int" /> to percentages.
    /// </summary>
    /// <returns>
    /// A collection of <see cref="double" /> with its values
    /// expressed as percentages.
    /// </returns>
    /// <param name="collection">Collection to process.</param>
    /// <param name="max">Value that will represent 100%.</param>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="collection"/> is
    /// <see langword="null"/>.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the minimum value of the collection and
    /// <paramref name="max"/> are equal.
    /// </exception>
    public static IEnumerable<double> ToPercentDouble(this IEnumerable<int> collection, in int max)
    {
        return ToPercentDouble(collection, 0, max);
    }

    /// <summary>
    /// Enumerates the elements of the collection, including the index of each
    /// returned element.
    /// </summary>
    /// <typeparam name="T">Type of elements in the collection.</typeparam>
    /// <param name="collection">
    /// Collection for which to enumerate the elements along with their index.
    /// </param>
    /// <returns>An enumeration of each element along with its index.</returns>
    public static IEnumerable<(int index, T element)> WithIndex<T>(this IEnumerable<T> collection)
    {
        int i = 0;
        foreach (var j in collection)
        {
            yield return (i++, j);
        }
    }

    /// <summary>
    /// Converts the values of a collection of elements
    /// <see cref="int" /> to percentages.
    /// </summary>
    /// <returns>
    /// A collection of <see cref="double" /> with its values
    /// expressed as percentages.
    /// </returns>
    /// <param name="collection">Collection to process.</param>
    /// <param name="min">Value that will represent 0%.</param>
    /// <param name="max">Value that will represent 100%.</param>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="collection"/> is
    /// <see langword="null"/>.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Thrown if <paramref name="min"/> and <paramref name="max"/> are
    /// equal.
    /// </exception>
    public static IEnumerable<double> ToPercentDouble(this IEnumerable<int> collection, int min, int max)
    {
        ToPercent_Contract(collection, min, max);
        foreach (int j in collection) yield return (j - min) / (double)(max - min);
    }

    /// <summary>
    /// Converts the values of a collection of elements
    /// <see cref="int" /> to percentages.
    /// </summary>
    /// <returns>
    /// A collection of <see cref="double" /> with its values
    /// expressed as percentages.
    /// </returns>
    /// <param name="collection">Collection to process.</param>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="collection"/> is
    /// <see langword="null"/>.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the minimum and maximum values of the collection are equal.
    /// </exception>
    public static IEnumerable<double> ToPercentDouble(this IEnumerable<int> collection)
    {
        ArgumentNullException.ThrowIfNull(collection);
        List<int> enumerable = [.. collection];
        return ToPercentDouble(enumerable, 0, enumerable.Max());
    }

    /// <summary>
    /// Converts the values of a collection of elements
    /// <see cref="int" /> to single-precision percentages.
    /// </summary>
    /// <returns>
    /// A collection of <see cref="float" /> with its values
    /// expressed as percentages.
    /// </returns>
    /// <param name="collection">Collection to process.</param>
    /// <param name="max">Value that will represent 100%.</param>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="collection"/> is
    /// <see langword="null"/>.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the minimum value of the collection and
    /// <paramref name="max"/> are equal.
    /// </exception>
    public static IAsyncEnumerable<float> ToPercentSingle(this IAsyncEnumerable<int> collection, in int max)
    {
        return ToPercentSingle(collection, 0, max);
    }

    /// <summary>
    /// Converts the values of a collection of elements
    /// <see cref="int" /> to single-precision percentages.
    /// </summary>
    /// <returns>
    /// A collection of <see cref="float" /> with its values
    /// expressed as percentages.
    /// </returns>
    /// <param name="collection">Collection to process.</param>
    /// <param name="min">Value that will represent 0%.</param>
    /// <param name="max">Value that will represent 100%.</param>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="collection"/> is
    /// <see langword="null"/>.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Thrown if <paramref name="min"/> and <paramref name="max"/> are
    /// equal.
    /// </exception>
    public static async IAsyncEnumerable<float> ToPercentSingle(this IAsyncEnumerable<int> collection, int min, int max)
    {
        ToPercent_Contract(collection, min, max);
        await foreach (int j in collection) yield return (j - min) / (float)(max - min);
    }

    /// <summary>
    /// Converts the values of a collection of elements
    /// <see cref="int" /> to single-precision percentages.
    /// </summary>
    /// <returns>
    /// A collection of <see cref="float" /> with its values
    /// expressed as percentages.
    /// </returns>
    /// <param name="collection">Collection to process.</param>
    /// <param name="baseZero">
    /// Optional. If <see langword="true" />, the base of
    /// percentage is zero; otherwise, the minimum value
    /// within the collection will be used.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="collection"/> is
    /// <see langword="null"/>.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the minimum and maximum values of the collection are equal.
    /// </exception>
    public static IEnumerable<float> ToPercentSingle(this IEnumerable<int> collection, in bool baseZero)
    {
        ArgumentNullException.ThrowIfNull(collection);
        List<int> enumerable = [.. collection];
        return ToPercentSingle(enumerable, baseZero ? 0 : enumerable.Min(), enumerable.Max());
    }

    /// <summary>
    /// Converts the values of a collection of elements
    /// <see cref="int" /> to single-precision percentages.
    /// </summary>
    /// <returns>
    /// A collection of <see cref="float" /> with its values
    /// expressed as percentages.
    /// </returns>
    /// <param name="collection">Collection to process.</param>
    /// <param name="max">Value that will represent 100%.</param>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="collection"/> is
    /// <see langword="null"/>.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the minimum value of the collection and
    /// <paramref name="max"/> are equal.
    /// </exception>
    public static IEnumerable<float> ToPercentSingle(this IEnumerable<int> collection, in int max)
    {
        return ToPercentSingle(collection, 0, max);
    }

    /// <summary>
    /// Converts the values of a collection of elements
    /// <see cref="int" /> to single-precision percentages.
    /// </summary>
    /// <returns>
    /// A collection of <see cref="float" /> with its values
    /// expressed as percentages.
    /// </returns>
    /// <param name="collection">Collection to process.</param>
    /// <param name="min">Value that will represent 0%.</param>
    /// <param name="max">Value that will represent 100%.</param>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="collection"/> is
    /// <see langword="null"/>.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Thrown if <paramref name="min"/> and <paramref name="max"/> are
    /// equal.
    /// </exception>
    public static IEnumerable<float> ToPercentSingle(this IEnumerable<int> collection, int min, int max)
    {
        ToPercent_Contract(collection, min, max);
        foreach (int j in collection) yield return (j - min) / (float)(max - min);
    }

    /// <summary>
    /// Converts the values of a collection of elements
    /// <see cref="int" /> to single-precision percentages.
    /// </summary>
    /// <returns>
    /// A collection of <see cref="float" /> with its values
    /// expressed as percentages.
    /// </returns>
    /// <param name="collection">Collection to process.</param>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="collection"/> is
    /// <see langword="null"/>.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the minimum and maximum values of the collection are equal.
    /// </exception>
    public static IEnumerable<float> ToPercentSingle(this IEnumerable<int> collection)
    {
        ArgumentNullException.ThrowIfNull(collection);
        List<int> enumerable = [.. collection];
        return ToPercentSingle(enumerable, 0, enumerable.Max());
    }

    /// <summary>
    /// Gets a list of the types of the specified objects.
    /// </summary>
    /// <param name="objects">
    /// Objects from which to generate the collection of types.
    /// </param>
    /// <returns>
    /// A list composed of the types of the provided objects.
    /// </returns>
    public static IEnumerable<Type> ToTypes(this IEnumerable objects)
    {
        foreach (object? j in objects) if (j is not null) yield return j.GetType();
    }

    /// <summary>
    /// Determines if any of the objects are <see langword="null" />.
    /// </summary>
    /// <returns>
    /// An enumerator with the indices of the objects that are <see langword="null" />.
    /// </returns>
    /// <param name="collection">Collection of objects to check.</param>
    public static IEnumerable<int> WhichAreNull(this IEnumerable<object?> collection)
    {
        ArgumentNullException.ThrowIfNull(collection);
        int c = 0;
        foreach (object? j in collection)
        {
            if (j is null) yield return c;
            c++;
        }
    }
}
