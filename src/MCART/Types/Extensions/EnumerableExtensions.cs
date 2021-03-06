/*
EnumerableExtensions.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2021 César Andrés Morgan

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
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Exceptions;
using TheXDS.MCART.Helpers;
using static TheXDS.MCART.Misc.Internals;

namespace TheXDS.MCART.Types.Extensions
{
    /// <summary>
    /// Extensiones para todos los elementos de tipo
    /// <see cref="IEnumerable{T}" />.
    /// </summary>
    public static partial class EnumerableExtensions
    {
        /// <summary>
        /// Comprueba si la colección contiene al menos un elemento del tipo
        /// especificado.
        /// </summary>
        /// <typeparam name="T">Tipo de objeto a buscar.</typeparam>
        /// <param name="collection">
        /// Colección de elementos a comprobar.
        /// </param>
        /// <returns>
        /// <see langword="true"/> si existe un elemento del tipo especificado
        /// en la colección, <see langword="false"/> en caso contrario.
        /// </returns>
        [Sugar] public static bool IsAnyOf<T>(this IEnumerable collection)
        {
            return collection.OfType<T>().Any();
        }

        /// <summary>
        /// Comprueba si la colección contiene al menos un elemento del tipo
        /// especificado.
        /// </summary>
        /// <param name="collection">
        /// Colección de elementos a comprobar.
        /// </param>
        /// <param name="type">Tipo de objeto a buscar.</param>
        /// <returns>
        /// <see langword="true"/> si existe un elemento del tipo especificado
        /// en la colección, <see langword="false"/> en caso contrario.
        /// </returns>
        public static bool IsAnyOf(this IEnumerable collection, Type type)
        {
            foreach (var j in collection)
            {
                if (type.IsInstanceOfType(j)) return true;
            }
            return false;
        }

        /// <summary>
        /// Ejecuta una operación sobre una secuencia en un contexto
        /// bloqueado.
        /// </summary>
        /// <typeparam name="T">
        /// Tipo de elementos de la secuencia.
        /// </typeparam>
        /// <param name="collection">
        /// Secuencia sobre la cual ejecutar una operación bloqueada.
        /// </param>
        /// <param name="action">
        /// Acción a ejecutar sobre la secuencia.
        /// </param>
        public static void Locked<T>(this T collection, Action<T> action) where T : IEnumerable
        {
            if (collection is ICollection c)
            {
                if (c.IsSynchronized) action(collection);
                else lock (c.SyncRoot) action(collection);
            }
            else
            {
                lock (collection) action(collection);
            }
        }

        /// <summary>
        /// Ejecuta una operación sobre una secuencia en un contexto
        /// bloqueado.
        /// </summary>
        /// <typeparam name="TCollection">
        /// Tipo de elementos de la secuencia.
        /// </typeparam>
        /// <typeparam name="TResult">
        /// Tipo de resultado obtenido por la función.
        /// </typeparam>
        /// <param name="collection">
        /// Secuencia sobre la cual ejecutar una operación bloqueada.
        /// </param>
        /// <param name="func">
        /// Función a ejecutar sobre la secuencia.
        /// </param>
        public static TResult Locked<TCollection, TResult>(this TCollection collection, Func<TCollection, TResult> func) where TCollection : IEnumerable
        {
            if (collection is ICollection c)
            {
                if (c.IsSynchronized) return func(collection);
                lock (c.SyncRoot) return func(collection);
            }
            lock (collection) return func(collection);
        }

        /// <summary>
        /// Ejecuta una tarea de forma asíncrona sobre cada ítem a enumerar.
        /// </summary>
        /// <typeparam name="T">
        /// Tipo de elementos de la enumeración.
        /// </typeparam>
        /// <param name="input">Enumeración de entrada.</param>
        /// <param name="processor">
        /// Tarea asíncrona que se ejecutará sobre cada ítem antes de ser
        /// enumerado.
        /// </param>
        /// <returns>
        /// Un <see cref="IAsyncEnumerable{T}"/> que puede utilizarse para
        /// esperar la enumeración de cada elemento.
        /// </returns>
        public static async IAsyncEnumerable<T> YieldAsync<T>(this IEnumerable<T> input, Func<T, Task> processor)
        {
            NullCheck(input, nameof(input));
            foreach (var j in input)
            {
                await processor(j);
                yield return j;
            }
        }

        /// <summary>
        /// Transforma una enumeración de entrada al tipo de salida requerido
        /// de forma asíncrona.
        /// </summary>
        /// <typeparam name="TIn">
        /// Tipo de elementos de la enumeración de entrada.
        /// </typeparam>
        /// <typeparam name="TOut">
        /// Tipo de elementos de la enumeración de salida.
        /// </typeparam>
        /// <param name="input">Enumeración de entrada.</param>
        /// <param name="selector">
        /// Tarea asíncrona que transformará los datos de entrada.
        /// </param>
        /// <returns>
        /// Un <see cref="IAsyncEnumerable{T}"/> que puede utilizarse para
        /// esperar la enumeración de cada elemento.
        /// </returns>
        public static async IAsyncEnumerable<TOut> SelectAsync<TIn, TOut>(this IEnumerable<TIn> input, Func<TIn, Task<TOut>> selector)
        {
            NullCheck(input, nameof(input));
            foreach (var j in input)
            {
                yield return await selector(j);
            }
        }

        /// <summary>
        /// Obtiene la cuenta de elementos nulos dentro de una secuencia.
        /// </summary>
        /// <param name="collection">
        /// Secuencia desde la cual obtener la cuenta de elementos nulos.
        /// </param>
        /// <returns>
        /// La cuenta de elementos nulos dentro de la colección.
        /// </returns>
        public static int NullCount(this IEnumerable collection)
        {
            NullCheck(collection, nameof(collection));
            var count = 0;
            foreach (var j in collection)
            {
                if (j is null) count++;
            }
            return count;
        }

        /// <summary>
        /// Agrupa una secuencia de elementos de acuerdo al tipo de los
        /// mismos.
        /// </summary>
        /// <param name="c">Colección a agrupar.</param>
        /// <returns>
        /// Una secuencia de elementos agrupados de acuerdo a su tipo.
        /// </returns>
        public static IEnumerable<IGrouping<Type, object>> GroupByType(this IEnumerable c)
        {
            return c.ToGeneric().NotNull().GroupBy(p => p.GetType());
        }

        /// <summary>
        /// Enumera una colección no genérica como una genérica.
        /// </summary>
        /// <param name="collection">
        /// Colección a enumerar.
        /// </param>
        /// <returns>
        /// Una enumeración con el contenido de la enumeración no genérica
        /// expuesta como una genérica.
        /// </returns>
        public static IEnumerable<object?> ToGeneric(this IEnumerable collection)
        {
            foreach (var j in collection) yield return j;
        }
        
        /// <summary>
        /// Obtiene al primer elemento del tipo solicitado dentro de una
        /// colección.
        /// </summary>
        /// <typeparam name="T">
        /// Tipo de elemento a buscar.
        /// </typeparam>
        /// <param name="collection">
        /// Colección sobre la cual realizar la búsqueda.
        /// </param>
        /// <returns>
        /// El primer elemento de tipo <typeparamref name="T"/> que sea
        /// encontrado en la colección, o <see langword="default"/> si no se
        /// encuentra ningún elemento del tipo especificado.
        /// </returns>
        [Sugar]
        [return: MaybeNull]
        public static T FirstOf<T>(this IEnumerable collection)
        {
            return collection.OfType<T>().FirstOrDefault();
        }

        /// <summary>
        /// Obtiene al primer elemento del tipo solicitado dentro de una
        /// colección.
        /// </summary>
        /// <typeparam name="T">
        /// Tipo de elementos contenidos en la colección.
        /// </typeparam>
        /// <param name="collection">
        /// Colección sobre la cual realizar la búsqueda.
        /// </param>
        /// <param name="type">Tipo de elemento a buscar.</param>
        /// <returns>
        /// El primer elemento de tipo <paramref name="type"/> que sea
        /// encontrado en la colección, o <see langword="default"/> si no se
        /// encuentra ningún elemento del tipo especificado.
        /// </returns>
        [return: MaybeNull]
        public static T FirstOf<T>(this IEnumerable<T> collection, Type type)
        {
            FirstOf_OfType_Contract<T>(type);
            return collection.FirstOrDefault(p => p?.GetType().Implements(type) ?? false);
        }

        /// <summary>
        /// Enumera todos los elementos de la colección que sean del tipo
        /// especificado.
        /// </summary>
        /// <typeparam name="T">
        /// Tipo de elementos contenidos en la colección.
        /// </typeparam>
        /// <param name="collection">
        /// Colección sobre la cual realizar la búsqueda.
        /// </param>
        /// <param name="type">Tipo de elementos a devolver.</param>
        /// <returns>
        /// Una enumeración de todos los elementos de la colección que sean del
        /// tipo especificado.
        /// </returns>
        public static IEnumerable<T> OfType<T>(this IEnumerable<T> collection, Type type)
        {
            FirstOf_OfType_Contract<T>(type);
            return collection.Where(p => p?.GetType() == type);
        }

        /// <summary>
        /// Enumera todos los elementos de la colección, omitiendo los
        /// especificados.
        /// </summary>
        /// <typeparam name="T">Tipo de elementos de la colección.</typeparam>
        /// <param name="collection">Colección a enumerar.</param>
        /// <param name="exclusions">
        /// Elementos a excluir de la colección.
        /// </param>
        /// <returns>
        /// Una enumeración con los elementos de la colección, omitiendo
        /// las exclusiones especificadas.
        /// </returns>
        public static IEnumerable<T> ExceptFor<T>(this IEnumerable<T> collection, params T[] exclusions)
        {
            bool Compare(T value)
            {
                return value?.GetType().Default() == null
                    ? value.IsNeither(exclusions.AsEnumerable())
                    : !exclusions.Contains(value);
            }

            return collection.Where(Compare);
        }

        /// <summary>
        /// Enumera los elementos no nulos de una colección.
        /// </summary>
        /// <typeparam name="T">Tipo de elementos de la colección.</typeparam>
        /// <param name="collection">Colección a enumerar.</param>
        /// <returns>
        /// Una enumeración con los elementos de la colección, omitiendo
        /// aquellos que sean <see langword="null"/>, o una colección vacía si
        /// <paramref name="collection"/> es <see langword="null"/>.
        /// </returns>
        public static IEnumerable<T> NotNull<T>(this IEnumerable<T?>? collection) where T : class
        {
            return collection is null ? Array.Empty<T>() : collection.Where(p => !(p is null)).Select(p => p!);
        }

        /// <summary>
        /// Enumera los elementos no nulos de una colección.
        /// </summary>
        /// <param name="collection">Colección a enumerar.</param>
        /// <returns>
        /// Una enumeración con los elementos de la colección, omitiendo
        /// aquellos que sean <see langword="null"/>, o una colección vacía si
        /// <paramref name="collection"/> es <see langword="null"/>.
        /// </returns>
        public static IEnumerable NotNull(this IEnumerable? collection)
        {
            if (collection is null) yield break;
            foreach (var j in collection)
            {
                if (!(j is null)) yield return j;
            }
        }

        /// <summary>
        /// Enumera los elementos no nulos de una colección.
        /// </summary>
        /// <typeparam name="T">Tipo de elementos de la colección.</typeparam>
        /// <param name="collection">Colección a enumerar.</param>
        /// <returns>
        /// Una enumeración con los elementos de la colección, omitiendo
        /// aquellos que sean <see langword="null"/>, o una colección vacía si
        /// <paramref name="collection"/> es <see langword="null"/>.
        /// </returns>
        public static IEnumerable<T> NotNull<T>(this IEnumerable<T?>? collection) where T : struct
        {
            return collection is null ? Array.Empty<T>() : collection.Where(p => !(p is null)).Select(p => p!.Value);
        }

        /// <summary>
        /// Enumera la colección, y devuelve <see langword="null"/> si la
        /// colección no contiene elementos.
        /// </summary>
        /// <typeparam name="T">Tipo de elementos de la colección.</typeparam>
        /// <param name="collection">Colección a enumerar.</param>
        /// <returns>
        /// Una enumeración con los elementos de la colección, o
        /// <see langword="null"/> si la colección no contiene elementos.
        /// </returns>
        public static IEnumerable<T>? OrNull<T>(this IEnumerable<T> collection)
        {
            var c = collection.ToArray();
            return c.Any() ? c : null;
        }

        /// <summary>
        /// Enumera los elementos que sean distintos de su valor
        /// predeterminado dentro de una colección.
        /// </summary>
        /// <typeparam name="T">Tipo de elementos de la colección.</typeparam>
        /// <param name="collection">Colección a enumerar.</param>
        /// <returns>
        /// Una enumeración con los elementos de la colección, omitiendo
        /// aquellos que sean iguales a su valor predeterminado, o en el
        /// caso de tipos de referencia, omitiendo aquellos que sean 
        /// <see langword="null"/>.
        /// </returns>
        public static IEnumerable<T> NonDefaults<T>(this IEnumerable<T?> collection) where T : notnull 
        {
            return collection.Where(p => !Equals(p, default(T)!))!;
        }

        /// <summary>
        /// Obtiene un sub-rango de valores dentro de este
        /// <see cref="IEnumerable{T}" />.
        /// </summary>
        /// <param name="from">
        /// <see cref="IEnumerable{T}" /> desde el cual extraer la secuencia.
        /// </param>
        /// <param name="index">
        /// Índice a partir del cual obtener el sub-rango.
        /// </param>
        /// <param name="count">
        /// Cantidad de elementos a obtener.
        /// </param>
        /// <returns>
        /// Un <see cref="IEnumerable{T}" /> que contiene el sub-rango
        /// especificado.
        /// </returns>
        /// <exception cref="IndexOutOfRangeException">
        /// Se produce si <paramref name="index"/> está fuera del rango de
        /// la colección.
        /// </exception>
        public static IEnumerable<T> Range<T>(this IEnumerable<T> from, int index, int count)
        {
            using var e = from.GetEnumerator();
            e.Reset();
            e.MoveNext();
            var c = 0;
            while (c++ < index)
            {
                if (!e.MoveNext()) throw new IndexOutOfRangeException();
            }
            c = 0;
            while (c++ < count)
            {
                yield return e.Current;
                if (!e.MoveNext()) yield break;
            }
        }

        /// <summary>
        /// Obtiene una copia de los elementos de este <see cref="IEnumerable{T}" />
        /// </summary>
        /// <returns>
        /// Copia de esta lista. Los elementos de la copia representan la misma
        /// instancia del objeto original.
        /// </returns>
        /// <param name="collection">Colección a copiar.</param>
        /// <typeparam name="T">
        /// Tipo de elementos contenidos en el <see cref="IEnumerable{T}" />.
        /// </typeparam>
        public static IEnumerable<T> Copy<T>(this IEnumerable<T> collection)
        {
            var tmp = new List<T>();
            tmp.AddRange(collection);
            return tmp;
        }

        /// <summary>
        /// Devuelve una versión desordenada del <see cref="IEnumerable{T}" />
        /// sin alterar la colección original.
        /// </summary>
        /// <typeparam name="T">
        /// Tipo de elementos contenidos en el <see cref="IEnumerable{T}" />.
        /// </typeparam>
        /// <param name="collection"><see cref="IEnumerable{T}" /> a desordenar.</param>
        /// <returns>
        /// Una versión desordenada del <see cref="IEnumerable{T}" />.
        /// </returns>
        public static IEnumerable<T> Shuffled<T>(this IEnumerable<T> collection)
        {
            return Shuffled(collection, 1);
        }

        /// <summary>
        /// Devuelve una versión desordenada del <see cref="IEnumerable{T}" />
        /// sin alterar la colección original.
        /// </summary>
        /// <typeparam name="T">
        /// Tipo de elementos contenidos en el <see cref="IEnumerable{T}" />.
        /// </typeparam>
        /// <param name="collection"><see cref="IEnumerable{T}" /> a desordenar.</param>
        /// <param name="deepness">
        /// Profundidad del desorden. 1 es el más alto.
        /// </param>
        /// <returns>
        /// Una versión desordenada del <see cref="IEnumerable{T}" />.
        /// </returns>
        public static IEnumerable<T> Shuffled<T>(this IEnumerable<T> collection, in int deepness)
        {
            var enumerable = collection.ToList();
            return Shuffled(enumerable, 0, enumerable.Count - 1, deepness, RandomExtensions.Rnd);
        }

        /// <summary>
        /// Devuelve una versión desordenada del intervalo especificado de
        /// elementos del <see cref="IEnumerable{T}" /> sin alterar la colección
        /// original.
        /// </summary>
        /// <typeparam name="T">
        /// Tipo de elementos contenidos en el <see cref="IEnumerable{T}" />.
        /// </typeparam>
        /// <param name="collection"><see cref="IEnumerable{T}" /> a desordenar.</param>
        /// <param name="firstIdx">Índice inicial del intervalo.</param>
        /// <param name="lastIdx">Índice inicial del intervalo.</param>
        /// <returns>
        /// Una versión desordenada del intervalo especificado de elementos del
        /// <see cref="IEnumerable{T}" />.
        /// </returns>
        public static IEnumerable<T> Shuffled<T>(this IEnumerable<T> collection, in int firstIdx, in int lastIdx)
        {
            return Shuffled(collection, firstIdx, lastIdx, 1, RandomExtensions.Rnd);
        }

        /// <summary>
        /// Devuelve una versión desordenada del intervalo especificado de
        /// elementos del <see cref="IEnumerable{T}" /> sin alterar la colección
        /// original.
        /// </summary>
        /// <typeparam name="T">
        /// Tipo de elementos contenidos en el <see cref="IEnumerable{T}" />.
        /// </typeparam>
        /// <param name="collection"><see cref="IEnumerable{T}" /> a desordenar.</param>
        /// <param name="firstIdx">Índice inicial del intervalo.</param>
        /// <param name="lastIdx">Índice inicial del intervalo.</param>
        /// <param name="deepness">
        /// Profundidad del desorden. 1 es el más alto.
        /// </param>
        /// <param name="random">Generador de números aleatorios a utilizar.</param>
        /// <returns>
        /// Una versión desordenada del intervalo especificado de elementos del
        /// <see cref="IEnumerable{T}" />.
        /// </returns>
        public static IEnumerable<T> Shuffled<T>(this IEnumerable<T> collection, in int firstIdx, in int lastIdx, in int deepness, in Random random)
        {
            var tmp = new List<T>(collection);
            tmp.Shuffle(firstIdx, lastIdx, deepness, random);
            return tmp;
        }

        /// <summary>
        /// Selecciona un elemento aleatorio de la colección.
        /// </summary>
        /// <returns>Un objeto aleatorio de la colección.</returns>
        /// <param name="collection">Colección desde la cual seleccionar.</param>
        /// <typeparam name="T">
        /// Tipo de elementos contenidos en el <see cref="IEnumerable{T}" />.
        /// </typeparam>
        /// <returns>
        /// Un elemento aleatorio de la colección.
        /// </returns>
        public static T Pick<T>(this IEnumerable<T> collection)
        {
            return Pick(collection, RandomExtensions.Rnd);
        }

        /// <summary>
        /// Selecciona un elemento aleatorio de la colección.
        /// </summary>
        /// <returns>Un objeto aleatorio de la colección.</returns>
        /// <typeparam name="T">
        /// Tipo de elementos contenidos en el <see cref="IEnumerable{T}" />.
        /// </typeparam>
        /// <param name="collection">Colección desde la cual seleccionar.</param>
        /// <param name="random">Generador de números aleatorios a utilizar.</param>
        /// <returns>
        /// Un elemento aleatorio de la colección.
        /// </returns>
        public static T Pick<T>(this IEnumerable<T> collection, in Random random)
        {
            var c = collection.ToList();
#if PreferExceptions
            if (!c.Any()) throw new EmptyCollectionException(c);
            return c.ElementAt(RandomExtensions.Rnd.Next(0, c.Count));
#else
            return !c.Any() ? default! : c.ElementAt(random.Next(0, c.Count));
#endif
        }

        /// <summary>
        /// Selecciona un elemento aleatorio de la colección de forma asíncrona.
        /// </summary>
        /// <returns>Un objeto aleatorio de la colección.</returns>
        /// <typeparam name="T">
        /// Tipo de elementos contenidos en el <see cref="IEnumerable{T}" />.
        /// </typeparam>
        /// <returns>
        /// Una tarea que puede utilizarse para monitorear la operación.
        /// </returns>
        public static async Task<T> PickAsync<T>(this IEnumerable<T> collection)
        {
            var c = await collection.ToListAsync();
#if PreferExceptions
            if (!c.Any()) throw new EmptyCollectionException(c);
            return c.ElementAt(RandomExtensions.Rnd.Next(0, c.Count));
#else
            return !c.Any() ? default! : c.ElementAt(RandomExtensions.Rnd.Next(0, c.Count));
#endif
        }

        /// <summary>
        /// Crea un <see cref="ListEx{T}"/> a partir de un <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <param name="collection">Colección a convertir</param>
        /// <typeparam name="T">Tipo de la colección.</typeparam>
        /// <returns>
        /// Un <see cref="ListEx{T}" /> extendido del espacio de nombres
        /// <see cref="Extensions" />.
        /// </returns>
        public static ListEx<T> ToExtendedList<T>(this IEnumerable<T> collection)
        {
            return new(collection);
        }

        /// <summary>
        /// Crea un <see cref="System.Collections.Generic.List{T}"/> a partir de un <see cref="IEnumerable{T}"/> de forma asíncrona.
        /// </summary>
        /// <typeparam name="T">Tipo de la colección.</typeparam>
        /// <param name="enumerable"></param>
        /// <returns>
        /// Una tarea que puede utilizarse para monitorear la operación.
        /// </returns>
        public static async Task<List<T>> ToListAsync<T>(this IEnumerable<T> enumerable)
        {
            return await Task.Run(enumerable.ToList);
        }

        /// <summary>
        /// Crea un <see cref="ListEx{T}"/> a partir de un <see cref="IEnumerable{T}"/> de forma asíncrona.
        /// </summary>
        /// <typeparam name="T">Tipo de la colección.</typeparam>
        /// <param name="enumerable"></param>
        /// <returns>
        /// Una tarea que puede utilizarse para monitorear la operación.
        /// </returns>
        public static async Task<ListEx<T>> ToExtendedListAsync<T>(this IEnumerable<T> enumerable)
        {
            return await Task.Run(enumerable.ToExtendedList);
        }

        /// <summary>Rota los elementos de un arreglo, lista o colección.</summary>
        /// <param name="collection">Arreglo a rotar</param>
        /// <param name="steps">Dirección y unidades de rotación.</param>
        /// <remarks>
        /// Si <paramref name="steps" /> es positivo, la rotación ocurre de forma
        /// ascendente; en caso contrario, descendente.
        /// </remarks>
        /// <typeparam name="T">
        /// Tipo de elementos contenidos en el <see cref="IEnumerable{T}" />.
        /// </typeparam>
        public static IEnumerable<T> Rotate<T>(this IEnumerable<T> collection, int steps)
        {
            switch (steps)
            {
                case > 0:
                {
                    using var e = collection.GetEnumerator();
                    e.Reset();
                    var j = 0;
                    
                    while (j++ < steps) e.MoveNext();
                    while (e.MoveNext()) yield return e.Current;
                    e.Reset();
                    while (--j > 0)
                    {
                        e.MoveNext();
                        yield return e.Current;
                    }

                    break;
                }
                case < 0:
                {
                    var c = new List<T>();
                    using var e = collection.GetEnumerator();
                    e.Reset();

                    // HACK: La implementación para IList<T> es funcional, y no requiere de trucos inusuales para rotar.
                    while (e.MoveNext()) c.Add(e.Current);
                    c.ApplyRotate(steps);
                    foreach (var i in c) yield return i;
                    break;
                }
                default:
                {
                    foreach (var i in collection) yield return i;
                    break;
                }
            }
        }

        /// <summary>Desplaza los elementos de un arreglo, lista o colección.</summary>
        /// <param name="collection">Arreglo a desplazar</param>
        /// <param name="steps">Dirección y unidades de desplazamiento.</param>
        /// <remarks>
        /// Si <paramref name="steps" /> es positivo, la rotación ocurre de forma
        /// ascendente; en caso contrario, descendente.
        /// </remarks>
        /// <typeparam name="T">
        /// Tipo de elementos contenidos en el <see cref="IEnumerable{T}" />.
        /// </typeparam>
        public static IEnumerable<T> Shift<T>(this IEnumerable<T> collection, int steps)
        {

            switch (steps)
            {
                case > 0:
                {
                    using var e = collection.GetEnumerator();
                    e.Reset();
                    var j = 0;
                    while (j++ < steps) e.MoveNext();
                    while (e.MoveNext()) yield return e.Current;
                    while (--j > 0) yield return default!;
                    break;
                }
                case < 0:
                {
                    using var e = collection.GetEnumerator();
                    e.Reset();
                    var j = 0;
                    
                    var c = new List<T>();

                    // HACK: Enumeración manual
                    while (e.MoveNext()) c.Add(e.Current);
                    while (j-- > steps) yield return default!;
                    j += c.Count;
                    while (j-- >= 0) yield return c.PopFirst();
                    break;
                }
                default:
                {
                    foreach (var i in collection) yield return i;
                    break;
                }
            }
        }

        /// <summary>
        /// Compara dos colecciones y determina si sus elementos son iguales.
        /// </summary>
        /// <param name="collection">
        /// Enumeración a comprobar.
        /// </param>
        /// <param name="items">
        /// Enumeración contra la cual comprobar.
        /// </param>
        /// <returns>
        /// <see langword="true"/> si los elementos de ambas colecciones
        /// son iguales, <see langword="false"/> en caso contrario.
        /// </returns>
        public static bool ItemsEqual(this IEnumerable collection, IEnumerable items)
        {
            var ea = collection.GetEnumerator();
            var eb = items.GetEnumerator();
            while (ea.MoveNext())
            {
                if (!eb.MoveNext() || (!ea.Current?.Equals(eb.Current) ?? false)) return false;
            }
            return !eb.MoveNext();
        }

        /// <summary>
        /// Comprueba si la enumeración <paramref name="collection"/> contiene a 
        /// todos los elementos de la enumeración <paramref name="items"/>.
        /// </summary>
        /// <param name="collection">
        /// Enumeración a comprobar.
        /// </param>
        /// <param name="items">
        /// Elementos que deben existir en <paramref name="collection"/>.
        /// </param>
        /// <returns>
        /// <see langword="true"/> si <paramref name="collection"/> contiene a todos
        /// los elementos de <paramref name="items"/>, <see langword="false"/>
        /// en caso contrario.
        /// </returns>
        public static bool ContainsAll(this IEnumerable collection, IEnumerable items)
        {
            return items.Cast<object?>().All(collection.Contains);
        }

        /// <summary>
        /// Comprueba si la enumeración <paramref name="collection"/> contiene a 
        /// todos los elementos de la enumeración <paramref name="items"/>.
        /// </summary>
        /// <param name="collection">
        /// Enumeración a comprobar.
        /// </param>
        /// <param name="items">
        /// Elementos que deben existir en <paramref name="collection"/>.
        /// </param>
        /// <returns>
        /// <see langword="true"/> si <paramref name="collection"/> contiene a todos
        /// los elementos de <paramref name="items"/>, <see langword="false"/>
        /// en caso contrario.
        /// </returns>
        public static bool ContainsAll(this IEnumerable collection, params object?[] items)
        {
            return ContainsAll(collection, items.AsEnumerable());
        }

        /// <summary>
        /// Comprueba si la enumeración <paramref name="collection"/> contiene a 
        /// cualquiera de los elementos de la enumeración
        /// <paramref name="items"/>.
        /// </summary>
        /// <param name="collection">
        /// Enumeración a comprobar.
        /// </param>
        /// <param name="items">
        /// Elementos que deben existir en <paramref name="collection"/>.
        /// </param>
        /// <returns>
        /// <see langword="true"/> si <paramref name="collection"/> contiene a
        /// cualquiera de los elementos de <paramref name="items"/>,
        /// <see langword="false"/> en caso contrario.
        /// </returns>
        public static bool ContainsAny(this IEnumerable collection, IEnumerable items)
        {
            return items.Cast<object?>().Any(collection.Contains);
        }

        /// <summary>
        /// Comprueba si la enumeración <paramref name="collection"/> contiene a 
        /// cualquiera de los elementos de la enumeración
        /// <paramref name="items"/>.
        /// </summary>
        /// <param name="collection">
        /// Enumeración a comprobar.
        /// </param>
        /// <param name="items">
        /// Elementos que deben existir en <paramref name="collection"/>.
        /// </param>
        /// <returns>
        /// <see langword="true"/> si <paramref name="collection"/> contiene a
        /// cualquiera de los elementos de <paramref name="items"/>,
        /// <see langword="false"/> en caso contrario.
        /// </returns>
        public static bool ContainsAny(this IEnumerable collection, params object?[] items)
        {
            return ContainsAny(collection, items.AsEnumerable());
        }

        /// <summary>
        /// Versión no-genérica de la función
        /// <see cref="Enumerable.Contains{TSource}(IEnumerable{TSource}, TSource)"/>.
        /// </summary>
        /// <param name="enumerable">Colección a comprobar.</param>
        /// <param name="obj">Objeto a buscar dentro de la colección.</param>
        /// <returns>
        /// <see langword="true"/> si la colección contiene al objeto
        /// especificado, <see langword="false"/> en caso contrario.
        /// </returns>
        public static bool Contains(this IEnumerable enumerable, object? obj)
        {
            return enumerable.Cast<object>().Any(j => j?.Equals(obj) ?? obj is null);
        }

        /// <summary>
        /// Ordena una secuencia de elementos de acuerdo a su prioridad
        /// indicada por el atributo <see cref="PriorityAttribute"/>.
        /// </summary>
        /// <typeparam name="T">
        /// Tipo de elementos contenidos en el <see cref="IEnumerable{T}" />.
        /// </typeparam>
        /// <param name="c"></param>
        /// <returns></returns>
        [Sugar]
        public static IOrderedEnumerable<T> Prioritized<T>(this IEnumerable<T> c)
        {
            return Ordered<T, PriorityAttribute>(c);
        }

        /// <summary>
        /// Ordena una secuencia de elementos de acuerdo a un valor de
        /// atributo especificado.
        /// </summary>
        /// <typeparam name="T">
        /// Tipo de elementos contenidos en el <see cref="IEnumerable{T}" />.
        /// </typeparam>
        /// <typeparam name="TAttr">
        /// Tipo de atributo del cual obtener el valor de orden.
        /// </typeparam>
        /// <typeparam name="TAttrValue">Tipo de valor de orden.</typeparam>
        /// <param name="c">Colección a ordenar.</param>
        /// <returns>
        /// Una enumeración con los elementos de la secuencia especificada
        /// ordenados de acuerdo al valor del atributo especificado.
        /// </returns>
        public static IOrderedEnumerable<T> Ordered<T, TAttr, TAttrValue>(this IEnumerable<T> c)
            where TAttrValue : struct
            where TAttr : Attribute, IValueAttribute<TAttrValue>
        {
            var t = typeof(TAttrValue);
            var d = t.GetField(@"MaxValue", BindingFlags.Public | BindingFlags.Static) is { } f ? (TAttrValue)f.GetValue(null)! : default;
            return c.OrderBy(p => p?.GetAttr<TAttr>()?.Value ?? d);
        }

        /// <summary>
        /// Ordena una secuencia de elementos de acuerdo a un valor de
        /// atributo especificado.
        /// </summary>
        /// <typeparam name="T">
        /// Tipo de elementos contenidos en el <see cref="IEnumerable{T}" />.
        /// </typeparam>
        /// <typeparam name="TAttr">
        /// Tipo de atributo del cual obtener el valor de orden.
        /// </typeparam>
        /// <param name="c">Colección a ordenar.</param>
        /// <returns>
        /// Una enumeración con los elementos de la secuencia especificada
        /// ordenados de acuerdo al valor del atributo especificado.
        /// </returns>
        public static IOrderedEnumerable<T> Ordered<T, TAttr>(this IEnumerable<T> c) where TAttr : Attribute, IValueAttribute<int>
        {
            return c.OrderBy(p => p?.GetAttr<TAttr>()?.Value ?? int.MaxValue);
        }

        /// <summary>
        /// Versión no-genérica de la función
        /// <see cref="Enumerable.Count{TSource}(IEnumerable{TSource})"/>.
        /// Obtiene la cantidad de elementos de una secuencia.
        /// </summary>
        /// <param name="e">
        /// Secuencia a comprobar. La misma será enumerada.
        /// </param>
        /// <returns>
        /// La cantidad de elementos dentro de la secuencia.
        /// </returns>
        public static int Count(this IEnumerable e)
        {
            Count_Contract(e);
#if DynamicLoading
            if (e.GetType().GetProperty("Count") is PropertyInfo p && p.CanRead && p.PropertyType == typeof(int))
            {
                return (int)p.GetValue(e, null)!;
            }
            if (e.GetType().GetMethod("Count", BindingFlags.Public | BindingFlags.Instance, null, Type.EmptyTypes, null) is MethodInfo m && m.ReturnType == typeof(int) && !m.ContainsGenericParameters)
            {
                return (int)m.Invoke(e, new object[0])!;
            }
            return CountEnumerable(e);
#else
            return e switch
            {
                string s => s.Length,
                Array a => a.Length,
                ICollection c => c.Count,
                _ => CountEnumerable(e)
            };
#endif
        }

        /// <summary>
        /// Obtiene la cuenta de elementos de un
        /// <see cref="IAsyncEnumerable{T}"/>, enumerándolo de forma asíncrona.
        /// </summary>
        /// <param name="e">
        /// <see cref="IAsyncEnumerable{T}"/> para el cual obtener la cuenta de
        /// elementos.
        /// </param>
        /// <param name="ct">Token que permite cancelar la operación.</param>
        /// <typeparam name="T">Tipo de elementos de la colección.</typeparam>
        /// <returns>
        /// La cantidad de elementos contenidos en la colección.
        /// </returns>
        /// <exception cref="TaskCanceledException">
        /// Ocurre cuando la tarea es cancelada.
        /// </exception>
        public static async ValueTask<int> CountAsync<T>(this IAsyncEnumerable<T> e, CancellationToken ct)
        {
            var n = e.GetAsyncEnumerator(ct);
            if (ct.IsCancellationRequested) throw new TaskCanceledException();
            var c = 0;
            while (await n.MoveNextAsync())
            {
                c++;
                if (ct.IsCancellationRequested) throw new TaskCanceledException();
            }
            return c;
        }

        /// <summary>
        /// Obtiene la cuenta de elementos de un
        /// <see cref="IAsyncEnumerable{T}"/>, enumerándolo de forma asíncrona.
        /// </summary>
        /// <param name="e">
        /// <see cref="IAsyncEnumerable{T}"/> para el cual obtener la cuenta de
        /// elementos.
        /// </param>
        /// <typeparam name="T">Tipo de elementos de la colección.</typeparam>
        /// <returns>
        /// La cantidad de elementos contenidos en la colección.
        /// </returns>
        /// <exception cref="TaskCanceledException">
        /// Ocurre cuando la tarea es cancelada.
        /// </exception>
        public static ValueTask<int> CountAsync<T>(this IAsyncEnumerable<T> e)
        {
            return CountAsync(e, CancellationToken.None);
        }

        /// <summary>
        /// Obtiene un valor que indica si el valor de la propiedad de todos
        /// los objetos en la colección es igual.
        /// </summary>
        /// <typeparam name="T">Tipo de objetos de la colección.</typeparam>
        /// <param name="c">
        /// Colección que contiene los objetos a comprobar.
        /// </param>
        /// <param name="selector">
        /// Función selectora de valor.
        /// </param>
        /// <returns>
        /// <see langword="true"/> si el valor de la propiedad de todos los
        /// objetos de la colección es el mismo, <see langword="false"/> en 
        /// caso contrario.
        /// </returns>
        public static bool IsPropertyEqual<T>(this IEnumerable<T> c, Func<T, object> selector)
        {
            return AreAllEqual(c.Select(selector));
        }

        /// <summary>
        /// Comprueba si todos los objetos de la colección son iguales.
        /// </summary>
        /// <typeparam name="T">Tipo de objetos de la colección.</typeparam>
        /// <param name="c">
        /// Colección que contiene los objetos a comprobar.
        /// </param>
        /// <returns>
        /// <see langword="true"/> si todos los objetos de la colección son
        /// iguales, <see langword="false"/> en caso contrario.
        /// </returns>
        public static bool AreAllEqual<T>(this IEnumerable<T> c)
        { 
            using var j = c.GetEnumerator();
            if (!j.MoveNext())  throw new EmptyCollectionException(c);
            var eq = j.Current;
            while (j.MoveNext())
            {
                if (!eq?.Equals(j.Current) ?? j.Current is { }) return false;
            }
            return true;
        }

        /// <summary>
        /// Encuentra el índice de un objeto dentro de una colección.
        /// </summary>
        /// <typeparam name="T">Tipo de objetos de la colección.</typeparam>
        /// <param name="e">
        /// Colección que contiene los objetos a comprobar.
        /// </param>
        /// <param name="item">
        /// Ítem del cual obtener el índice.
        /// </param>
        /// <returns>
        /// El índice del objeto especificado, o <c>-1</c> si el objeto no
        /// existe dentro de la colección.
        /// </returns>
        public static int FindIndexOf<T>(this IEnumerable<T> e, T item)
        {
            return (e, item) switch
            {
                (string s, char i) => s.ToCharArray().FindIndexOf(i),
                (IList<T> l, _) => l.IndexOf(item),
                _ => IndexOfEnumerable(e, item)
            };
        }

        private static int IndexOfEnumerable(IEnumerable e, object? i)
        {
            var n = e.GetEnumerator();
            var c = 0;
            while (n.MoveNext())
            {
                if (n.Current?.Equals(i) ?? i is null) return c;
                c++;
            }
            (n as IDisposable)?.Dispose();
            return -1;
        }

        private static int CountEnumerable(IEnumerable e)
        {
            var n = e.GetEnumerator();
            var c = 0;
            while (n.MoveNext()) c++;
            (n as IDisposable)?.Dispose();
            return c;
        }
    }
}