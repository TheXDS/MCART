// Enumerable_T_Extensions.cs
//
// This file is part of Morgan's CLR Advanced Runtime (MCART)
//
// Author(s):
//      César Andrés Morgan <xds_xps_ivx@hotmail.com>
//
// Released under the MIT License (MIT)
// Copyright © 2011 - 2025 César Andrés Morgan
//
// Permission is hereby granted, free of charge, to any person obtaining a copy of
// this software and associated documentation files (the “Software”), to deal in
// the Software without restriction, including without limitation the rights to
// use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
// of the Software, and to permit persons to whom the Software is furnished to do
// so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using System.Diagnostics.CodeAnalysis;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Exceptions;
using TheXDS.MCART.Misc;
using TheXDS.MCART.Resources;

namespace TheXDS.MCART.Types.Extensions;

public static partial class EnumerableExtensions
{
    /// <summary>
    /// Itera de manera ordenada sobre la colección.
    /// </summary>
    /// <param name="collection">Colección sobre la cual iterar</param>
    /// <typeparam name="T">Tipo de elementos de la colección.</typeparam>
    /// <returns>
    /// Un objeto enumerable ordenado creado a partir de la colección.
    /// </returns>
    [Sugar]
    public static IOrderedEnumerable<T> Ordered<T>(this IEnumerable<T> collection) where T : IComparable<T>
    {
        return collection.OrderBy(p => p);
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
        ArgumentNullException.ThrowIfNull(input, nameof(input));
        foreach (T? j in input)
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
        ArgumentNullException.ThrowIfNull(input, nameof(input));
        foreach (TIn? j in input)
        {
            yield return await selector(j);
        }
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
    [RequiresDynamicCode(Misc.AttributeErrorMessages.MethodCreatesNewTypes)]
    public static T FirstOf<T>(this IEnumerable<T> collection, Type type)
    {
        FirstOf_OfType_Contract<T>(type);
        return collection.FirstOrDefault(p => p?.GetType().IsAssignableTo(type) ?? false);
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
            return value is not null && value.GetType().IsClass
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
        return collection is null ? [] : collection.Where(p => p is not null).Select(p => p!);
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
        return collection is null ? [] : collection.Where(p => p is not null).Select(p => p!.Value);
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
        T[] c = [.. collection];
        return c.Length != 0 ? c : null;
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
        using IEnumerator<T> e = from.GetEnumerator();
        e.Reset();
        e.MoveNext();
        int c = 0;
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
        List<T> tmp = [.. collection];
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
        List<T> enumerable = [.. collection];
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
        List<T> tmp = [.. collection];
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
       T[] c = [.. collection];
        if (c.Length == 0) throw Errors.EmptyCollection(c);
        return c[random.Next(0, c.Length)];
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
        List<T> c = await collection.ToListAsync();
        if (c.Count == 0) throw Errors.EmptyCollection(collection);
        return c.ElementAt(RandomExtensions.Rnd.Next(0, c.Count));
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
    [Obsolete(AttributeErrorMessages.UnsuportedClass)]
    public static ListEx<T> ToExtendedList<T>(this IEnumerable<T> collection)
    {
        return [.. collection];
    }

    /// <summary>
    /// Crea un <see cref="List{T}"/> a partir de un <see cref="IEnumerable{T}"/> de forma asíncrona.
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
    [Obsolete(AttributeErrorMessages.UnsuportedClass)]
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
                using IEnumerator<T> e = collection.GetEnumerator();
                e.Reset();
                int j = 0;

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
                List<T> c = [];
                using IEnumerator<T> e = collection.GetEnumerator();
                e.Reset();

                // HACK: La implementación para IList<TValue> es funcional, y no requiere de trucos inusuales para rotar.
                while (e.MoveNext()) c.Add(e.Current);
                c.ApplyRotate(steps);
                foreach (T? i in c) yield return i;
                break;
            }
            default:
            {
                foreach (T? i in collection) yield return i;
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
                using IEnumerator<T> e = collection.GetEnumerator();
                e.Reset();
                int j = 0;
                while (j++ < steps) e.MoveNext();
                while (e.MoveNext()) yield return e.Current;
                while (--j > 0) yield return default!;
                break;
            }
            case < 0:
            {
                using IEnumerator<T> e = collection.GetEnumerator();
                e.Reset();
                int j = 0;

                List<T> c = [];

                // HACK: Enumeración manual
                while (e.MoveNext()) c.Add(e.Current);
                while (j-- > steps) yield return default!;
                j += c.Count;
                while (j-- >= 0) yield return c.PopFirst();
                break;
            }
            default:
            {
                foreach (T? i in collection) yield return i;
                break;
            }
        }
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
        using IEnumerator<T> j = c.GetEnumerator();
        if (!j.MoveNext()) throw new EmptyCollectionException(c);
        T eq = j.Current;
        while (j.MoveNext())
        {
            if (!eq?.Equals(j.Current) ?? j.Current is { }) return false;
        }
        return true;
    }

    /// <summary>
    /// Comprueba si la propiedad o valor dentro de todos los objetos de la
    /// colección son iguales.
    /// </summary>
    /// <typeparam name="T">Tipo de objetos de la colección.</typeparam>
    /// <typeparam name="TProp">Tipo de valor a comparar.</typeparam>
    /// <param name="c">
    /// Colección que contiene los objetos a comprobar.
    /// </param>
    /// <param name="selector">
    /// Función de selección del valor de cada objeto.
    /// </param>
    /// <returns>
    /// <see langword="true"/> si todos los valores seleccionados a partir
    /// de los objetos de la colección son iguales, <see langword="false"/>
    /// en caso contrario.
    /// </returns>
    [Sugar]
    public static bool AreAllEqual<T, TProp>(this IEnumerable<T> c, Func<T, TProp> selector)
    {
        return c.Select(selector).AreAllEqual();
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
}
