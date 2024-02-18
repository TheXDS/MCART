/*
ListExtensions.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2024 César Andrés Morgan

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
/// Extensiones para todos los elementos de tipo <see cref="IList{T}" />.
/// </summary>
public static partial class ListExtensions
{
    /// <summary>
    /// Quita todos los elementos del tipo especificado de la
    /// colección.
    /// </summary>
    /// <typeparam name="T">Tipo de elementos a remover.</typeparam>
    /// <param name="c">Colección de la cual remover los elementos.</param>
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
    /// Aplica una operación de rotación sobre el <see cref="IList{T}"/>.
    /// </summary>
    /// <typeparam name="T">Tipo de elementos de la colección.</typeparam>
    /// <param name="c">colección a rotar.</param>
    /// <param name="steps">
    /// Pasos de rotación. Un valor positivo rotará los elementos hacia la
    /// izquierda, y uno negativo los rotará hacia la derecha.
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
    /// Aplica una operación de desplazamiento sobre el
    /// <see cref="IList{T}"/>.
    /// </summary>
    /// <typeparam name="T">Tipo de elementos de la colección.</typeparam>
    /// <param name="c">colección a desplazar.</param>
    /// <param name="steps">
    /// Pasos de desplazamiento. Un valor positivo desplazará los elementos
    /// hacia la izquierda, y uno negativo los desplazará hacia la derecha,
    /// en ambos casos rellenando con valores predeterminados cada posición
    /// vacía resultante.
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
    /// Ejecuta una operación de lista en un contexto bloqueado.
    /// </summary>
    /// <param name="list">
    /// Lista sobre la cual ejecutar una operación bloqueada.
    /// </param>
    /// <param name="action">
    /// Acción a ejecutar sobre la lista.
    /// </param>
    public static void Locked(this IList list, Action<IList> action)
    {
        if (list.IsSynchronized) action(list);
        else lock (list.SyncRoot) action(list);
    }

    /// <summary>
    /// Desordena los elementos de un <see cref="IList{T}" />.
    /// </summary>
    /// <typeparam name="T">
    /// Tipo de elementos contenidos en el <see cref="IList{T}" />.
    /// </typeparam>
    /// <param name="c"><see cref="IList{T}" /> a desordenar.</param>
    /// <exception cref="ArgumentNullException">
    /// Se produce si <paramref name="c" /> es <see langword="null" />.
    /// </exception>
    /// <exception cref="EmptyCollectionException">
    /// Se produce si <paramref name="c" /> hace referencia a una colección
    /// vacía.
    /// </exception>
    public static void Shuffle<T>(this IList<T> c)
    {
        Shuffle(c, 1);
    }

    /// <summary>
    /// Desordena los elementos de un <see cref="IList{T}" />.
    /// </summary>
    /// <typeparam name="T">
    /// Tipo de elementos contenidos en el <see cref="IList{T}" />.
    /// </typeparam>
    /// <param name="c"><see cref="IList{T}" /> a desordenar.</param>
    /// <param name="deepness">
    /// Profundidad del desorden. 1 es el valor más alto.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Se produce si <paramref name="c" /> es <see langword="null" />.
    /// </exception>
    /// <exception cref="EmptyCollectionException">
    /// Se produce si <paramref name="c" /> hace referencia a una colección
    /// vacía.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Se produce si <paramref name="deepness" /> es inferior a 1, o
    /// superior a la cuenta de elementos de la colección a desordenar.
    /// </exception>
    public static void Shuffle<T>(this IList<T> c, in int deepness)
    {
        Shuffle(c, 0, c.Count - 1, deepness);
    }

    /// <summary>
    /// Desordena los elementos del intervalo especificado de un
    /// <see cref="IList{T}" />.
    /// </summary>
    /// <typeparam name="T">
    /// Tipo de elementos contenidos en el <see cref="IList{T}" />.
    /// </typeparam>
    /// <param name="c"><see cref="IList{T}" /> a desordenar.</param>
    /// <param name="firstIdx">Índice inicial del intervalo.</param>
    /// <param name="lastIdx">Índice inicial del intervalo.</param>
    public static void Shuffle<T>(this IList<T> c, in int firstIdx, in int lastIdx)
    {
        Shuffle(c, firstIdx, lastIdx, 1);
    }

    /// <summary>
    /// Desordena los elementos del intervalo especificado de un
    /// <see cref="IList{T}" />.
    /// </summary>
    /// <typeparam name="T">
    /// Tipo de elementos contenidos en el <see cref="IList{T}" />.
    /// </typeparam>
    /// <param name="toShuffle"><see cref="IList{T}" /> a desordenar.</param>
    /// <param name="deepness">Profundidad del desorden. 1 es el más alto.</param>
    /// <param name="firstIdx">Índice inicial del intervalo.</param>
    /// <param name="lastIdx">Índice inicial del intervalo.</param>
    public static void Shuffle<T>(this IList<T> toShuffle, in int firstIdx, in int lastIdx, in int deepness)
    {
        Shuffle(toShuffle, firstIdx, lastIdx, deepness, RandomExtensions.Rnd);
    }

    /// <summary>
    /// Desordena los elementos del intervalo especificado de un
    /// <see cref="IList{T}" />.
    /// </summary>
    /// <typeparam name="T">
    /// Tipo de elementos contenidos en el <see cref="IList{T}" />.
    /// </typeparam>
    /// <param name="toShuffle"><see cref="IList{T}" /> a desordenar.</param>
    /// <param name="deepness">Profundidad del desorden. 1 es el más alto.</param>
    /// <param name="firstIdx">Índice inicial del intervalo.</param>
    /// <param name="lastIdx">Índice inicial del intervalo.</param>
    /// <param name="random">Generador de números aleatorios a utilizar.</param>
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
    /// Intercambia la posición de dos elementos dentro de un <see cref="IList{T}"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="collection">colección sobre la cual intercambiar la posición de dos elementos.</param>
    /// <param name="indexA">Índice del primer elemento.</param>
    /// <param name="indexB">Índice del segundo elemento.</param>
    public static void Swap<T>(this IList<T> collection, in int indexA, in int indexB)
    {
        if (indexA == indexB) return;
        (collection[indexB], collection[indexA]) = (collection[indexA], collection[indexB]);
    }

    /// <summary>
    /// Intercambia la posición de dos elementos dentro de un <see cref="IList{T}"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="collection">colección sobre la cual intercambiar la posición de dos elementos.</param>
    /// <param name="a">Primer elemento.</param>
    /// <param name="b">Segundo elemento.</param>
    public static void Swap<T>(this IList<T> collection, T a, T b)
    {
        if (!collection.ContainsAll(a, b)) throw Errors.ListMustContainBoth();
        Swap(collection, collection.IndexOf(a), collection.IndexOf(b));
    }
}
