/*
DictionaryExtensions.cs

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

namespace TheXDS.MCART.Types.Extensions;

/// <summary>
/// Extensions for all items of type <see cref="IDictionary{TKey, TValue}" />.
/// </summary>
public static class DictionaryExtensions
{
    /// <summary>
    /// Adds a value to the dictionary.
    /// </summary>
    /// <typeparam name="TKey">
    /// Key type to use to identify the value.
    /// </typeparam>
    /// <typeparam name="TValue">Type of value to add.</typeparam>
    /// <param name="dictionary">
    /// Dictionary to which to add the new value.
    /// </param>
    /// <param name="key">Key to identify the new value.</param>
    /// <param name="value">Value to add.</param>
    /// <returns>
    /// The same instance as <paramref name="value"/>, allowing
    /// Fluent syntax to be used.
    /// </returns>
    public static TValue Push<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue value) where TKey : notnull
    {
        dictionary.Add(key, value);
        return value;
    }

    /// <summary>
    /// Adds a value to the specified dictionary.
    /// </summary>
    /// <typeparam name="TKey">
    /// Key type to use to identify the value.
    /// </typeparam>
    /// <typeparam name="TValue">Type of value to add.</typeparam>
    /// <param name="dictionary">
    /// Dictionary to which to add the new value.
    /// </param>
    /// <param name="key">Key to identify the new value.</param>
    /// <param name="value">Value to add.</param>
    /// <returns>
    /// The same instance as <paramref name="value"/>, allowing
    /// Fluent syntax to be used.
    /// </returns>
    public static TValue PushInto<TKey, TValue>(this TValue value, TKey key, IDictionary<TKey, TValue> dictionary) where TKey : notnull
    {
        dictionary.Add(key, value);
        return value;
    }

    /// <summary>
    /// Gets the element with the specified key from the dictionary,
    /// removing it.
    /// </summary>
    /// <typeparam name="TKey">
    /// Key type of the object to get.
    /// </typeparam>
    /// <typeparam name="TValue">
    /// Value type contained by the dictionary.
    /// </typeparam>
    /// <param name="dictionary">
    /// Dictionary from which to get and remove the object.
    /// </param>
    /// <param name="key">Key of the object to get.</param>
    /// <param name="value">Value obtained from the dictionary.</param>
    /// <returns>
    /// <see langword="true"/> if the object was successfully removed
    /// from the dictionary, <see langword="false"/> if the dictionary
    /// did not contain an element with the specified key.
    /// </returns>
    public static bool Pop<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, out TValue value) where TKey : notnull
    {
        return dictionary.TryGetValue(key, out value!) && dictionary.Remove(key);
    }

    /// <summary>
    /// Checks for circular references in a
    /// dictionary of tree-shaped objects.
    /// </summary>
    /// <typeparam name="T">
    /// Type of elements contained in the dictionary.
    /// </typeparam>
    /// <param name="dictionary">
    /// Dictionary in which to perform the check.
    /// </param>
    /// <param name="element">
    /// Element to check.
    /// </param>
    /// <returns>
    /// <see langword="false"/> if no circular references
    /// exist within the dictionary, <see langword="true"/> otherwise.
    /// </returns>
    public static bool CheckCircularRef<T>(this IDictionary<T, IEnumerable<T>> dictionary, T element) where T : notnull
    {
        return BranchScanFails(element, element, dictionary, new HashSet<T>());
    }

    /// <summary>
    /// Checks for circular references in a
    /// dictionary of objects.
    /// </summary>
    /// <typeparam name="T">
    /// Type of elements contained in the dictionary.
    /// </typeparam>
    /// <param name="dictionary">
    /// Dictionary in which to perform the check.
    /// </param>
    /// <param name="element">
    /// Element to check.
    /// </param>
    /// <returns>
    /// <see langword="false"/> if no circular references
    /// exist within the dictionary, <see langword="true"/> otherwise.
    /// </returns>
    public static bool CheckCircularRef<T>(this IDictionary<T, ICollection<T>> dictionary, T element) where T : notnull
    {
        return BranchScanFails(element, element, dictionary, new HashSet<T>());
    }

    /// <summary>
    /// Checks for circular references in a
    /// dictionary of objects.
    /// </summary>
    /// <typeparam name="T">
    /// Type of elements contained in the dictionary.
    /// </typeparam>
    /// <param name="dictionary">
    /// Dictionary in which to perform the check.
    /// </param>
    /// <param name="element">
    /// Element to check.
    /// </param>
    /// <returns>
    /// <see langword="false"/> if no circular references
    /// exist within the dictionary, <see langword="true"/> otherwise.
    /// </returns>
    public static bool CheckCircularRef<T>(this IEnumerable<KeyValuePair<T, IEnumerable<T>>> dictionary, T element) where T : notnull
    {
        Dictionary<T, IEnumerable<T>>? d = [];
        foreach (KeyValuePair<T, IEnumerable<T>> j in dictionary) d.Add(j.Key, j.Value);
        return BranchScanFails(element, element, d, new HashSet<T>());
    }

    /// <summary>
    /// Checks for circular references in a
    /// dictionary of objects.
    /// </summary>
    /// <typeparam name="T">
    /// Type of elements contained in the dictionary.
    /// </typeparam>
    /// <param name="dictionary">
    /// Dictionary in which to perform the check.
    /// </param>
    /// <param name="element">
    /// Element to check.
    /// </param>
    /// <returns>
    /// <see langword="false"/> if no circular references
    /// exist within the dictionary, <see langword="true"/> otherwise.
    /// </returns>
    public static bool CheckCircularRef<T>(this IEnumerable<KeyValuePair<T, ICollection<T>>> dictionary, T element) where T : notnull
    {
        Dictionary<T, IEnumerable<T>>? d = [];
        foreach (KeyValuePair<T, ICollection<T>> j in dictionary) d.Add(j.Key, j.Value);
        return BranchScanFails(element, element, d, new HashSet<T>());
    }

    private static bool BranchScanFails<T>(T a, T b, IDictionary<T, IEnumerable<T>> tree, ICollection<T> keysChecked) where T : notnull
    {
        if (!tree.TryGetValue(b, out IEnumerable<T>? value)) return false;
        foreach (T? j in value)
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
        if (!tree.TryGetValue(b, out ICollection<T>? value)) return false;
        foreach (T? j in value)
        {
            if (keysChecked.Contains(j)) return false;
            keysChecked.Add(j);
            if (j.Equals(a)) return true;
            if (BranchScanFails(a, j, tree, keysChecked)) return true;
        }
        return false;
    }
}
