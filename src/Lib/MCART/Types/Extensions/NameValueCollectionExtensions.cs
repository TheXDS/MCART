/*
NameValueCollectionExtensions.cs

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

using System.Collections.Specialized;

namespace TheXDS.MCART.Types.Extensions;

/// <summary>
/// Miscellaneous functions and extensions for all elements of
/// the <see cref="NameValueCollection"/> type.
/// </summary>
public static class NameValueCollectionExtensions
{
    /// <summary>
    /// Converts a <see cref="NameValueCollection"/> into a collection of
    /// <see cref="IGrouping{TKey, TElement}"/>.
    /// </summary>
    /// <param name="nvc">
    /// <see cref="NameValueCollection"/> to convert.
    /// </param>
    /// <returns>
    /// A collection of <see cref="IGrouping{TKey, TElement}"/> with the
    /// keys and their respective values.
    /// </returns>
    public static IEnumerable<IGrouping<string?, string>> ToGroup(this NameValueCollection nvc)
    {
        foreach (string? j in nvc.AllKeys)
        {
            if (nvc.GetValues(j) is { } v) yield return new Grouping<string?, string>(j, v);
        }
    }

    /// <summary>
    /// Converts a <see cref="NameValueCollection"/> into a collection of
    /// <see cref="NamedObject{T}"/>.
    /// </summary>
    /// <param name="nvc">
    /// <see cref="NameValueCollection"/> to convert.
    /// </param>
    /// <returns>
    /// A collection of <see cref="NamedObject{T}"/> with the keys and their
    /// respective values.
    /// </returns>
    /// <remarks>
    /// This method will omit all values whose name is
    /// <see langword="null"/>.
    /// </remarks>
    public static IEnumerable<NamedObject<string[]>> ToNamedObjectCollection(this NameValueCollection nvc)
    {
        foreach (string? j in nvc.AllKeys.NotNull())
        {
            if (nvc.GetValues(j) is { } v) yield return new NamedObject<string[]>(j, v);
        }
    }

    /// <summary>
    /// Converts a <see cref="NameValueCollection"/> into a collection of
    /// <see cref="KeyValuePair{TKey, TValue}"/>.
    /// </summary>
    /// <param name="nvc">
    /// <see cref="NameValueCollection"/> to convert.
    /// </param>
    /// <returns>
    /// A collection of <see cref="KeyValuePair{TKey, TValue}"/> with the
    /// keys and their respective values.
    /// </returns>
    public static IEnumerable<KeyValuePair<string?, string>> ToKeyValuePair(this NameValueCollection nvc)
    {
        foreach (string? j in nvc.AllKeys)
        {
            if (nvc.Get(j) is { } v) yield return new KeyValuePair<string?, string>(j, v);
        }
    }

    /// <summary>
    /// Converts a <see cref="NameValueCollection"/> into a collection of
    /// <see cref="Dictionary{TKey, TValue}"/>.
    /// </summary>
    /// <param name="nvc">
    /// <see cref="NameValueCollection"/> to convert.
    /// </param>
    /// <returns>
    /// A collection of <see cref="Dictionary{TKey, TValue}"/> with the
    /// keys and their respective values.
    /// </returns>
    /// <remarks>
    /// This method will omit all values whose name is
    /// <see langword="null"/>.
    /// </remarks>
    public static Dictionary<string, string[]> ToDictionary(this NameValueCollection nvc)
    {
        Dictionary<string, string[]>? d = [];
        foreach (string? j in nvc.AllKeys.NotNull())
        {
            if (nvc.GetValues(j) is { } v) d.Add(j, v);
        }
        return d;
    }
}
