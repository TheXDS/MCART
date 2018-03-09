/*
EnumerableExtensions.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright (c) 2011 - 2018 César Andrés Morgan

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
using System.Collections.Generic;
using System.Linq;
using TheXDS.MCART.Exceptions;

namespace TheXDS.MCART.Types.Extensions
{
    /// <summary>
    ///     Extensiones para todos los elementos de tipo <see cref="IEnumerable{T}" />.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        ///     Obtiene un sub-rango de valores dentro de este
        ///     <see cref="List{T}" />.
        /// </summary>
        /// <param name="from">
        ///     <see cref="IEnumerable{T}" /> desde el cual extraer la secuencia.
        /// </param>
        /// <param name="index">
        ///     Índice a partir del cual obtener el sub-rango.
        /// </param>
        /// <param name="count">
        ///     Cantidad de elementos a obtener.
        /// </param>
        /// <returns>
        ///     Un <see cref="IEnumerable{T}" /> que contiene el sub-rango
        ///     especificado.
        /// </returns>
        public static IEnumerable<T> Range<T>(this IEnumerable<T> from, int index, int count)
        {
            using (var e = from.GetEnumerator())
            {
                e.Reset();
                var c = 0;
                while (c++ < index) e.MoveNext();
                c = 0;
                while (c++ < index)
                {
                    yield return e.Current;
                    if (!e.MoveNext()) yield break;
                }
            }
        }

        /// <summary>
        ///     Ontiene una copia de los elementos de este <see cref="IEnumerable{T}" />
        /// </summary>
        /// <returns>
        ///     Copia de esta lista. Los elementos de la copia representan la misma
        ///     instancia del objeto original.
        /// </returns>
        /// <param name="c">Colección a copiar.</param>
        /// <typeparam name="T">
        ///     Tipo de elementos contenidos en el <see cref="IEnumerable{T}" />.
        /// </typeparam>
        public static IEnumerable<T> Copy<T>(this IEnumerable<T> c)
        {
            var tmp = new List<T>();
            tmp.AddRange(c);
            return tmp;

            //return c;
        }

        /// <summary>
        ///     Desordena los elementos de un <see cref="IEnumerable{T}" />.
        /// </summary>
        /// <typeparam name="T">
        ///     Tipo de elementos contenidos en el <see cref="IEnumerable{T}" />.
        /// </typeparam>
        /// <param name="c"><see cref="IEnumerable{T}" /> a desordenar.</param>
        /// <exception cref="ArgumentNullException">
        ///     Se produce si <paramref name="c" /> es <see langword="null" />.
        /// </exception>
        /// <exception cref="EmptyCollectionException">
        ///     Se produce si <paramref name="c" /> hace referencia a una colección
        ///     vacía.
        /// </exception>
        public static void Shuffle<T>(this IEnumerable<T> c)
        {
            Shuffle(c, 1);
        }

        /// <summary>
        ///     Desordena los elementos de un <see cref="IEnumerable{T}" />.
        /// </summary>
        /// <typeparam name="T">
        ///     Tipo de elementos contenidos en el <see cref="IEnumerable{T}" />.
        /// </typeparam>
        /// <param name="c"><see cref="IEnumerable{T}" /> a desordenar.</param>
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
        public static void Shuffle<T>(this IEnumerable<T> c, int deepness)
        {
            Shuffle(c, 0, c.Count() - 1, deepness);
        }

        /// <summary>
        ///     Desordena los elementos del intervalo especificado de un
        ///     <see cref="IEnumerable{T}" />.
        /// </summary>
        /// <typeparam name="T">
        ///     Tipo de elementos contenidos en el <see cref="IEnumerable{T}" />.
        /// </typeparam>
        /// <param name="c"><see cref="IEnumerable{T}" /> a desordenar.</param>
        /// <param name="firstIdx">Índice inicial del intervalo.</param>
        /// <param name="lastIdx">Índice inicial del intervalo.</param>
        public static void Shuffle<T>(this IEnumerable<T> c, int firstIdx, int lastIdx)
        {
            Shuffle(c, firstIdx, lastIdx, 1);
        }

        /// <summary>
        ///     Desordena los elementos del intervalo especificado de un
        ///     <see cref="IEnumerable{T}" />.
        /// </summary>
        /// <typeparam name="T">
        ///     Tipo de elementos contenidos en el <see cref="IEnumerable{T}" />.
        /// </typeparam>
        /// <param name="toShuffle"><see cref="IEnumerable{T}" /> a desordenar.</param>
        /// <param name="deepness">Profundidad del desorden. 1 es el más alto.</param>
        /// <param name="firstIdx">Índice inicial del intervalo.</param>
        /// <param name="lastIdx">Índice inicial del intervalo.</param>
        public static void Shuffle<T>(this IEnumerable<T> toShuffle, int firstIdx, int lastIdx, int deepness)
        {
            if (toShuffle is null) throw new ArgumentNullException(nameof(toShuffle));
            var c = toShuffle as T[] ?? toShuffle.ToArray();
            if (!c.Any()) throw new EmptyCollectionException(c);
            if (!firstIdx.IsBetween(0, c.Length)) throw new IndexOutOfRangeException();
            if (!lastIdx.IsBetween(0, c.Length - 1)) throw new IndexOutOfRangeException();
            if (!deepness.IsBetween(1, lastIdx - firstIdx)) throw new ArgumentOutOfRangeException(nameof(deepness));
            if (firstIdx > lastIdx) Common.Swap(ref firstIdx, ref lastIdx);
            var a = c.ToArray();
            var rnd = new Random();
            lastIdx++;
            for (var j = firstIdx; j < lastIdx; j += deepness)
                Common.Swap(ref a[j], ref a[rnd.Next(firstIdx, lastIdx)]);
            c.ToList().Clear();
            c.ToList().AddRange(a);
        }

        /// <summary>
        ///     Devuelve una versión desordenada del <see cref="IEnumerable{T}" />
        ///     sin alterar la colección original.
        /// </summary>
        /// <typeparam name="T">
        ///     Tipo de elementos contenidos en el <see cref="IEnumerable{T}" />.
        /// </typeparam>
        /// <param name="c"><see cref="IEnumerable{T}" /> a desordenar.</param>
        /// <returns>
        ///     Una versión desordenada del <see cref="IEnumerable{T}" />.
        /// </returns>
        public static IEnumerable<T> Shuffled<T>(this IEnumerable<T> c)
        {
            return Shuffled(c, 1);
        }

        /// <summary>
        ///     Devuelve una versión desordenada del <see cref="IEnumerable{T}" />
        ///     sin alterar la colección original.
        /// </summary>
        /// <typeparam name="T">
        ///     Tipo de elementos contenidos en el <see cref="IEnumerable{T}" />.
        /// </typeparam>
        /// <param name="c"><see cref="IEnumerable{T}" /> a desordenar.</param>
        /// <param name="deepness">
        ///     Profundidad del desorden. 1 es el más alto.
        /// </param>
        /// <returns>
        ///     Una versión desordenada del <see cref="IEnumerable{T}" />.
        /// </returns>
        public static IEnumerable<T> Shuffled<T>(this IEnumerable<T> c, int deepness)
        {
            return Shuffled(c, 0, c.Count() - 1, deepness);
        }

        /// <summary>
        ///     Devuelve una versión desordenada del intervalo especificado de
        ///     elementos del <see cref="IEnumerable{T}" /> sin alterar la colección
        ///     original.
        /// </summary>
        /// <typeparam name="T">
        ///     Tipo de elementos contenidos en el <see cref="IEnumerable{T}" />.
        /// </typeparam>
        /// <param name="c"><see cref="IEnumerable{T}" /> a desordenar.</param>
        /// <param name="firstIdx">Índice inicial del intervalo.</param>
        /// <param name="lastIdx">Índice inicial del intervalo.</param>
        /// <returns>
        ///     Una versión desordenada del intervalo especificado de elementos del
        ///     <see cref="IEnumerable{T}" />.
        /// </returns>
        public static IEnumerable<T> Shuffled<T>(this IEnumerable<T> c, int firstIdx, int lastIdx)
        {
            return Shuffled(c, firstIdx, lastIdx, 1);
        }

        /// <summary>
        ///     Devuelve una versión desordenada del intervalo especificado de
        ///     elementos del <see cref="IEnumerable{T}" /> sin alterar la colección
        ///     original.
        /// </summary>
        /// <typeparam name="T">
        ///     Tipo de elementos contenidos en el <see cref="IEnumerable{T}" />.
        /// </typeparam>
        /// <param name="c"><see cref="IEnumerable{T}" /> a desordenar.</param>
        /// <param name="firstIdx">Índice inicial del intervalo.</param>
        /// <param name="lastIdx">Índice inicial del intervalo.</param>
        /// <param name="deepness">
        ///     Profundidad del desorden. 1 es el más alto.
        /// </param>
        /// <returns>
        ///     Una versión desordenada del intervalo especificado de elementos del
        ///     <see cref="IEnumerable{T}" />.
        /// </returns>
        public static IEnumerable<T> Shuffled<T>(this IEnumerable<T> c, int firstIdx, int lastIdx, int deepness)
        {
            var tmp = new System.Collections.Generic.List<T>(c);
            tmp.Shuffle(firstIdx, lastIdx, deepness);
            return tmp;
        }

        /// <summary>
        ///     Selecciona un elemento aleatorio de la colección.
        /// </summary>
        /// <returns>Un objeto aleatorio de la colección.</returns>
        /// <typeparam name="T">
        ///     Tipo de elementos contenidos en el <see cref="IEnumerable{T}" />.
        /// </typeparam>
        public static T Pick<T>(this IEnumerable<T> collection)
        {
            var c = collection.ToList();
            if (!c.Any()) throw new EmptyCollectionException(c);
            return c.ElementAt(RandomExtensions.Rnd.Next(0, c.Count));
        }

        /// <summary>
        ///     Convierte una colección a <see cref="List{T}" />
        /// </summary>
        /// <param name="c">Colección a convertir</param>
        /// <returns>
        ///     Un <see cref="List{T}" /> extendido del espacio de nombres
        ///     <see cref="Extensions" />.
        /// </returns>
        public static List<T> ToExtendedList<T>(this IEnumerable<T> c)
        {
            return (List<T>) c.ToList();
        }

        /// <summary>Rota los elementos de un arreglo, lista o colección.</summary>
        /// <param name="a">Arreglo a rotar</param>
        /// <param name="steps">Dirección y unidades de rotación.</param>
        /// <remarks>
        ///     Si <paramref name="steps" /> es positivo, la rotación ocurre de forma
        ///     ascendente; en caso contrario, descendente.
        /// </remarks>
        /// <typeparam name="T">
        ///     Tipo de elementos contenidos en el <see cref="IEnumerable{T}" />.
        /// </typeparam>
        public static void Rotate<T>(this IEnumerable<T> a, int steps)
        {
            var enumerable = a.ToList();
            if (!enumerable.Any()) throw new NullReferenceException();
            if (steps > 0)
                for (var j = 0; j < steps; j++)
                    enumerable.ToList().Add(enumerable.PopFirst());
            else if (steps < 0)
                for (var j = 0; j > steps; j--)
                    enumerable.ToList().Insert(0, enumerable.Pop());
        }

        /// <summary>Desplaza los elementos de un arreglo, lista o colección.</summary>
        /// <param name="a">Arreglo a desplazar</param>
        /// <param name="steps">Dirección y unidades de desplazamiento.</param>
        /// <remarks>
        ///     Si <paramref name="steps" /> es positivo, la rotación ocurre de forma
        ///     ascendente; en caso contrario, descendente.
        /// </remarks>
        /// <typeparam name="T">
        ///     Tipo de elementos contenidos en el <see cref="IEnumerable{T}" />.
        /// </typeparam>
        public static void Shift<T>(this IEnumerable<T> a, int steps)
        {
            var enumerable = a.ToList();
            if (!enumerable.Any()) throw new NullReferenceException();
            if (steps > 0)
            {
                for (var j = 0; j < steps; j++)
                {
                    enumerable.ToList().Remove(enumerable.First());
                    enumerable.ToList().Add(default);
                }
            }
            else if (steps < 0)
            {
                for (var j = 0; j > steps; j--)
                {
                    enumerable.ToList().Remove(enumerable.Last());
                    enumerable.ToList().Insert(0, default);
                }
            }
        }

        /// <summary>
        ///     Devuelve el último elemento en la lista, quitándolo.
        /// </summary>
        /// <returns>El último elemento en la lista.</returns>
        /// <param name="a">Lista de la cual obtener el elemento.</param>
        /// <typeparam name="T">
        ///     Tipo de elementos contenidos en el <see cref="IEnumerable{T}" />.
        /// </typeparam>
        public static T Pop<T>(this IEnumerable<T> a)
        {
            var enumerable = a.ToList();
            var x = enumerable.Last();
            enumerable.Remove(enumerable.Last());
            return x;
        }

        /// <summary>
        ///     Devuelve el primer elemento en la lista, quitándolo.
        /// </summary>
        /// <returns>El primer elemento en la lista.</returns>
        /// <param name="a">Lista de la cual obtener el elemento.</param>
        /// <typeparam name="T">
        ///     Tipo de elementos contenidos en el <see cref="IEnumerable{T}" />.
        /// </typeparam>
        public static T PopFirst<T>(this IEnumerable<T> a)
        {
            var enumerable = a.ToList();
            var x = enumerable.First();
            enumerable.Remove(enumerable.First());
            return x;
        }
    }
}