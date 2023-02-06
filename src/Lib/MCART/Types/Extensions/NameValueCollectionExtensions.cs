/*
NameValueCollectionExtensions.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Este archivo contiene numerosas extensiones para el tipo System.Type del CLR,
supliéndolo de nueva funcionalidad previamente no existente, o de invocación
compleja.

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2023 César Andrés Morgan

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

using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace TheXDS.MCART.Types.Extensions;

/// <summary>
/// Funciones misceláneas y extensiones para todos los elementos de
/// tipo <see cref="NameValueCollection"/>.
/// </summary>
public static class NameValueCollectionExtensions
{
    /// <summary>
    /// Convierte un <see cref="NameValueCollection"/> en una colección de
    /// <see cref="IGrouping{TKey, TElement}"/>.
    /// </summary>
    /// <param name="nvc">
    /// <see cref="NameValueCollection"/> a convertir.
    /// </param>
    /// <returns>
    /// Una colección de <see cref="IGrouping{TKey, TElement}"/> con las
    /// llaves y sus respectivos valores.
    /// </returns>
    public static IEnumerable<IGrouping<string?, string>> ToGroup(this NameValueCollection nvc)
    {
        foreach (string? j in nvc.AllKeys)
        {
            if (nvc.GetValues(j) is { } v) yield return new Grouping<string?, string>(j, v);
        }
    }

    /// <summary>
    /// Convierte un <see cref="NameValueCollection"/> en una colección de
    /// <see cref="NamedObject{T}"/>.
    /// </summary>
    /// <param name="nvc">
    /// <see cref="NameValueCollection"/> a convertir.
    /// </param>
    /// <returns>
    /// Una colección de <see cref="NamedObject{T}"/> con las llaves y sus
    /// respectivos valores.
    /// </returns>
    /// <remarks>
    /// Este método omitirá todos aquellos valores cuyo nombre sea
    /// <see langword="null"/>.
    /// </remarks>
    public static IEnumerable<NamedObject<string[]>> ToNamedObjectCollection(this NameValueCollection nvc)
    {
        foreach (string? j in nvc.AllKeys.NotNull())
        {
            if (nvc.GetValues(j) is { } v) yield return new NamedObject<string[]>(v, j);
        }
    }

    /// <summary>
    /// Convierte un <see cref="NameValueCollection"/> en una colección de
    /// <see cref="KeyValuePair{TKey, TValue}"/>.
    /// </summary>
    /// <param name="nvc">
    /// <see cref="NameValueCollection"/> a convertir.
    /// </param>
    /// <returns>
    /// Una colección de <see cref="KeyValuePair{TKey, TValue}"/> con las
    /// llaves y sus respectivos valores.
    /// </returns>
    public static IEnumerable<KeyValuePair<string?, string>> ToKeyValuePair(this NameValueCollection nvc)
    {
        foreach (string? j in nvc.AllKeys)
        {
            if (nvc.Get(j) is { } v) yield return new KeyValuePair<string?, string>(j, v);
        }
    }

    /// <summary>
    /// Convierte un <see cref="NameValueCollection"/> en una colección de
    /// <see cref="Dictionary{TKey, TValue}"/>.
    /// </summary>
    /// <param name="nvc">
    /// <see cref="NameValueCollection"/> a convertir.
    /// </param>
    /// <returns>
    /// Una colección de <see cref="Dictionary{TKey, TValue}"/> con las
    /// llaves y sus respectivos valores.
    /// </returns>
    /// <remarks>
    /// Este método omitirá todos aquellos valores cuyo nombre sea
    /// <see langword="null"/>.
    /// </remarks>
    public static Dictionary<string, string[]> ToDictionary(this NameValueCollection nvc)
    {
        Dictionary<string, string[]>? d = new();
        foreach (string? j in nvc.AllKeys.NotNull())
        {
            if (nvc.GetValues(j) is { } v) d.Add(j, v);
        }
        return d;
    }
}
