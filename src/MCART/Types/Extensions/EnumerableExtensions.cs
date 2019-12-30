/*
EnumerableExtensions.cs

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
using System.Reflection;
using System.Threading.Tasks;
using TheXDS.MCART.Attributes;

namespace TheXDS.MCART.Types.Extensions
{

    /// <summary>
    /// Extensiones para todos los elementos de tipo
    /// <see cref="IEnumerable{T}" />.
    /// </summary>
    public static class EnumerableExtensions
    {
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
        /// Obtiene la cuenta de elementos nulos dentro de una secuencia.
        /// </summary>
        /// <param name="c">
        /// Secuencia desde la cual obtener la cuenta de elementos nulos.
        /// </param>
        /// <returns>
        /// La cuenta de elementos nulos dentro de la colección.
        /// </returns>
        public static int NullCount(this IEnumerable c)
        {
            var count = 0;
            foreach (var j in c)
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
        /// encontrado en la colección, o <see langword="null"/> si no se
        /// encuentra ningún elemento del tipo especificado.
        /// </returns>
        [Sugar]
        public static T FirstOf<T>(this IEnumerable collection)
        {
            return collection.OfType<T>().FirstOrDefault();
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
        /// aquellos que sean <see langword="null"/>.
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
        /// aquellos que sean <see langword="null"/>.
        /// </returns>
        public static IEnumerable NotNull(this IEnumerable collection)
        {
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
        /// aquellos que sean <see langword="null"/>.
        /// </returns>
        public static IEnumerable<T> NotNull<T>(this IEnumerable<T?>? collection) where T : struct
        {
            return collection is null ? Array.Empty<T>() : collection.Where(p => !(p is null)).Select(p=>p!.Value);
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
        /// caso de tipos de referencia, <see langword="null"/>.
        /// </returns>
        public static IEnumerable<T> NonDefaults<T>(this IEnumerable<T> collection)
        {
            return collection.Where(p => !Equals(p, default(T)!));
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
            return new ListEx<T>(collection);
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
            using var e = collection.GetEnumerator();
            e.Reset();
            var j = 0;

            if (steps > 0)
            {
                while (j++ < steps) e.MoveNext();
                while (e.MoveNext()) yield return e.Current;
                e.Reset();
                while (--j > 0)
                {
                    e.MoveNext();
                    yield return e.Current;
                }
            }
            else if (steps < 0)
            {
                var c = new List<T>();

                // HACK: La implementación para IList<T> es funcional, y no requiere de trucos inusuales para rotar.
                while (e.MoveNext()) c.Add(e.Current);
                c.ApplyRotate(steps);
                foreach (var i in c) yield return i;
            }
            else
            {
                foreach (var i in collection) yield return i;
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
            using var e = collection.GetEnumerator();
            e.Reset();
            var j = 0;

            if (steps > 0)
            {
                while (j++ < steps) e.MoveNext();
                while (e.MoveNext()) yield return e.Current;
                while (--j > 0) yield return default!;
            }
            else if (steps < 0)
            {
                var c = new List<T>();

                // HACK: Enumeración manual
                while (e.MoveNext()) c.Add(e.Current);
                while (j-- > steps) yield return default!;
                j += c.Count;
                while (j-- >= 0) yield return c.PopFirst();
            }
            else
            {
                foreach (var i in collection) yield return i;
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
                if (!eb.MoveNext() || (ea.Current?.Equals(eb.Current) ?? false)) return false;
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
            foreach (var j in items)
            {
                if (!collection.Contains(j)) return false;
            }
            return true;
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
            foreach (var j in items)
            {
                if (collection.Contains(j)) return true;
            }
            return false;
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
        public static bool ContainsAny(this IEnumerable collection, params object[] items)
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
            foreach (var j in enumerable)
            {
                if (j?.Equals(obj) ?? obj is null) return true;
            }
            return false;
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
            if (e is null) throw new ArgumentNullException(nameof(e));

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