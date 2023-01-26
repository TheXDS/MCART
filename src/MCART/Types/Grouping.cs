/*
Grouping.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

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

using System.Linq;
using System.Collections.Generic;

namespace TheXDS.MCART.Types;

/// <summary>
/// Representa una colección fuertemente tipeada de elementos que
/// comparten una llave única.
/// </summary>
/// <typeparam name="TKey">
/// Tipo de llave a utilizar.
/// </typeparam>
/// <typeparam name="TElement">
/// Tipo de elementos de la colección.
/// </typeparam>
public class Grouping<TKey, TElement> : List<TElement>, IGrouping<TKey, TElement>
{
    /// <summary>
    /// Inicializa una nueva instancia de la clase 
    /// <see cref="Grouping{TKey, TElement}"/>, estableciendo la llave a
    /// utilizar.
    /// </summary>
    /// <param name="key">
    /// Llave asociada a este grupo de elementos.
    /// </param>
    public Grouping(TKey key)
    {
        Key = key;
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase 
    /// <see cref="Grouping{TKey, TElement}"/>, estableciendo la llave a
    /// utilizar para identificar a los elementos especificados.
    /// </summary>
    /// <param name="key">
    /// Llave asociada a este grupo de elementos.
    /// </param>
    /// <param name="collection">
    /// Colección a la cual asociar la llave especificada.
    /// </param>
    public Grouping(TKey key, IEnumerable<TElement> collection) : base(collection)
    {
        Key = key;
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase 
    /// <see cref="Grouping{TKey, TElement}"/>, estableciendo la llave a
    /// utilizar, además de definir la capacidad de la colección.
    /// </summary>
    /// <param name="key">
    /// Llave asociada a este grupo de elementos.
    /// </param>
    /// <param name="capacity">
    /// Capacidad inicial de la colección subyacente.
    /// </param>
    public Grouping(TKey key, int capacity) : base(capacity)
    {
        Key = key;
    }

    /// <summary>
    /// Llave asociada a este grupo de elementos.
    /// </summary>
    public TKey Key { get; }

    /// <summary>
    /// Convierte implícitamente un
    /// <see cref="Grouping{TKey, TElement}"/> en un
    /// <see cref="KeyValuePair{TKey, TValue}"/>.
    /// </summary>
    /// <param name="grouping">Objeto a convertir.</param>
    public static implicit operator KeyValuePair<TKey, IEnumerable<TElement>>(Grouping<TKey, TElement> grouping)
    {
        return new(grouping.Key, grouping);
    }

    /// <summary>
    /// Convierte implícitamente un
    /// <see cref="KeyValuePair{TKey, TValue}"/> en un
    /// <see cref="Grouping{TKey, TElement}"/>.
    /// </summary>
    /// <param name="grouping">Objeto a convertir.</param>
    public static implicit operator Grouping<TKey, TElement>(KeyValuePair<TKey, IEnumerable<TElement>> grouping)
    {
        return new(grouping.Key, grouping.Value);
    }

    /// <summary>
    /// Convierte implícitamente un
    /// <see cref="System.Tuple{T1, T2}"/> en un
    /// <see cref="Grouping{TKey, TElement}"/>.
    /// </summary>
    /// <param name="grouping">Objeto a convertir.</param>
    public static implicit operator Grouping<TKey, TElement>((TKey Key, IEnumerable<TElement> Value) grouping)
    {
        return new(grouping.Key, grouping.Value);
    }

    /// <summary>
    /// Convierte implícitamente un
    /// <see cref="Grouping{TKey, TElement}"/> en un
    /// <see cref="System.Tuple{T1, T2}"/>.
    /// </summary>
    /// <param name="grouping">Objeto a convertir.</param>
    public static implicit operator (TKey, IEnumerable<TElement>)(Grouping<TKey, TElement> grouping)
    {
        return (grouping.Key, grouping);
    }
}
