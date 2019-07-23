/*
ListExtensions.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2019 César Andrés Morgan

Morgan's CLR Advanced Runtime (MCART) is free software: you can redistribute it
and/or modify it under the terms of the GNU General Public License as published
by the Free Software Foundation, either version 3 of the License, or (at your
option) any later version.

Morgan's CLR Advanced Runtime (MCART) is distributed in the hope that it will
be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General
Public License for more details.

You should have received a copy of the GNU General Public License along with
this program. If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TheXDS.MCART.Exceptions;

namespace TheXDS.MCART.Types.Extensions
{
    /// <summary>
    ///     Extensiones para todos los elementos de tipo <see cref="IList{T}" />.
    /// </summary>
    public static class ListExtensions
    {


        /// <summary>
        ///     Quita todos los elementos del tipo especificado de la
        ///     colección.
        /// </summary>
        /// <typeparam name="T">Tipo de elementos a remover.</typeparam>
        /// <param name="c">Colección de la cual remover los elementos.</param>
        public static void RemoveOf<T>(this IList c)
        {
            var i = 0;
            while (i < c.Count)
            {
                if (typeof(T).IsInstanceOfType(c[i]))
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
        /// <param name="c">Colección a rotar.</param>
        /// <param name="steps">
        /// Pasos de rotación. Un valor positivo rotará los elementos hacia la
        /// izquierda, y uno negativo los rotará hacia la derecha.
        /// </param>
        public static void ApplyRotate<T>(this IList<T> c, int steps)
        {
            if (steps > 0)
                for (var j = 0; j < steps; j++)
                    c.Add(c.PopFirst());
            else if (steps < 0)
                for (var j = 0; j > steps; j--)
                    c.Insert(0, c.Pop());
        }

        /// <summary>
        /// Aplica una operación de desplazamiento sobre el
        /// <see cref="IList{T}"/>.
        /// </summary>
        /// <typeparam name="T">Tipo de elementos de la colección.</typeparam>
        /// <param name="c">Colección a desplazar.</param>
        /// <param name="steps">
        /// Pasos de desplazamiento. Un valor positivo desplazará los elementos
        /// hacia la izquierda, y uno negativo los desplazará hacia la derecha,
        /// en ambos casos rellenando con valores predeterminados cada posición
        /// vacía resultante.
        /// </param>
        public static void ApplyShift<T>(this IList<T> c, int steps)
        {
            if (steps > 0)
                for (var j = 0; j < steps; j++)
                {
                    c.PopFirst();
                    c.Add(default);
                }
            else if (steps < 0)
                for (var j = 0; j > steps; j--)
                {
                    c.Pop();
                    c.Insert(0, default);
                }
        }

        /// <summary>
        ///     Ejecuta una operación de lista en un contexto bloqueado.
        /// </summary>
        /// <typeparam name="T">
        ///     Tipo de elementos de la colección.
        /// </typeparam>
        /// <param name="list">
        ///     Lista sobre la cual ejecutar una operación bloqueada.
        /// </param>
        /// <param name="action">
        ///     Acción a ejecutar sobre la lista.
        /// </param>
        public static void Locked<T>(this IList list, Action<IList> action)
        {
            if (list.IsSynchronized) action(list);
            else lock (list.SyncRoot) action(list);
        }

        /// <summary>
        ///     Desordena los elementos de un <see cref="IList{T}" />.
        /// </summary>
        /// <typeparam name="T">
        ///     Tipo de elementos contenidos en el <see cref="IList{T}" />.
        /// </typeparam>
        /// <param name="c"><see cref="IList{T}" /> a desordenar.</param>
        /// <exception cref="ArgumentNullException">
        ///     Se produce si <paramref name="c" /> es <see langword="null" />.
        /// </exception>
        /// <exception cref="EmptyCollectionException">
        ///     Se produce si <paramref name="c" /> hace referencia a una colección
        ///     vacía.
        /// </exception>
        public static void Shuffle<T>(this IList<T> c)
        {
            Shuffle(c, 1);
        }

        /// <summary>
        ///     Desordena los elementos de un <see cref="IList{T}" />.
        /// </summary>
        /// <typeparam name="T">
        ///     Tipo de elementos contenidos en el <see cref="IList{T}" />.
        /// </typeparam>
        /// <param name="c"><see cref="IList{T}" /> a desordenar.</param>
        /// <param name="deepness">
        ///     Profundidad del desorden. 1 es el valor más alto.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     Se produce si <paramref name="c" /> es <see langword="null" />.
        /// </exception>
        /// <exception cref="EmptyCollectionException">
        ///     Se produce si <paramref name="c" /> hace referencia a una colección
        ///     vacía.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     Se produce si <paramref name="deepness" /> es inferior a 1, o
        ///     superior a la cuenta de elementos de la colección a desordenar.
        /// </exception>
        public static void Shuffle<T>(this IList<T> c, in int deepness)
        {
            var enumerable = c.ToList();
            Shuffle(enumerable, 0, enumerable.Count - 1, deepness);
        }

        /// <summary>
        ///     Desordena los elementos del intervalo especificado de un
        ///     <see cref="IList{T}" />.
        /// </summary>
        /// <typeparam name="T">
        ///     Tipo de elementos contenidos en el <see cref="IList{T}" />.
        /// </typeparam>
        /// <param name="c"><see cref="IList{T}" /> a desordenar.</param>
        /// <param name="firstIdx">Índice inicial del intervalo.</param>
        /// <param name="lastIdx">Índice inicial del intervalo.</param>
        public static void Shuffle<T>(this IList<T> c, in int firstIdx, in int lastIdx)
        {
            Shuffle(c, firstIdx, lastIdx, 1);
        }

        /// <summary>
        ///     Desordena los elementos del intervalo especificado de un
        ///     <see cref="IList{T}" />.
        /// </summary>
        /// <typeparam name="T">
        ///     Tipo de elementos contenidos en el <see cref="IList{T}" />.
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
        ///     Desordena los elementos del intervalo especificado de un
        ///     <see cref="IList{T}" />.
        /// </summary>
        /// <typeparam name="T">
        ///     Tipo de elementos contenidos en el <see cref="IList{T}" />.
        /// </typeparam>
        /// <param name="toShuffle"><see cref="IList{T}" /> a desordenar.</param>
        /// <param name="deepness">Profundidad del desorden. 1 es el más alto.</param>
        /// <param name="firstIdx">Índice inicial del intervalo.</param>
        /// <param name="lastIdx">Índice inicial del intervalo.</param>
        /// <param name="random">Generador de números aleatorios a utilizar.</param>
        public static void Shuffle<T>(this IList<T> toShuffle, int firstIdx, int lastIdx, int deepness, Random random)
        {
            if (toShuffle is null) throw new ArgumentNullException(nameof(toShuffle));
            if (random is null) random = RandomExtensions.Rnd;
            if (!toShuffle.Any()) throw new EmptyCollectionException(toShuffle);
            if (!firstIdx.IsBetween(0, toShuffle.Count)) throw new IndexOutOfRangeException();
            if (!lastIdx.IsBetween(0, toShuffle.Count - 1)) throw new IndexOutOfRangeException();
            if (!deepness.IsBetween(1, lastIdx - firstIdx)) throw new ArgumentOutOfRangeException(nameof(deepness));
            if (firstIdx > lastIdx) Common.Swap(ref firstIdx, ref lastIdx);

            lastIdx++;
            for (var j = firstIdx; j < lastIdx; j += deepness)
            {
                toShuffle.Swap(j, random.Next(firstIdx, lastIdx));
            }
        }

        /// <summary>
        ///     Intercambia la posición de dos elementos dentro de un <see cref="IList{T}"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection">Colección sobre la cual intercambiar la posición de dos elementos.</param>
        /// <param name="indexA">Índice del primer elemento.</param>
        /// <param name="indexB">Índice del segundo elemento.</param>
        public static void Swap<T>(this IList<T> collection, int indexA, int indexB)
        {
            if (indexA == indexB) return;
            var tmp = collection[indexB];
            collection[indexB] = collection[indexA];
            collection[indexA] = tmp;
        }

        /// <summary>
        ///     Intercambia la posición de dos elementos dentro de un <see cref="IList{T}"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection">Colección sobre la cual intercambiar la posición de dos elementos.</param>
        /// <param name="a">Índice del primer elemento.</param>
        /// <param name="b">Índice del segundo elemento.</param>
        public static void Swap<T>(this IList<T> collection, T a, T b)
        {
            
            if (!collection.ContainsAll(a, b)) throw new Exception();
            Swap(collection, collection.IndexOf(a), collection.IndexOf(b));
        }
    }
}