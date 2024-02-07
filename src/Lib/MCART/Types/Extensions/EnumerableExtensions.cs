/*
EnumerableExtensions.cs

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

using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Exceptions;
using TheXDS.MCART.Resources;
using static TheXDS.MCART.Misc.Internals;

namespace TheXDS.MCART.Types.Extensions;

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
    [Sugar]
    public static bool IsAnyOf<T>(this IEnumerable collection)
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
        foreach (object? j in collection)
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
            try
            {
                if (c.IsSynchronized) action(collection);
                else lock (c.SyncRoot) action(collection);
            }
            catch (NotSupportedException)
            {
                action(collection);
            }
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
    /// <typeparam name="T">
    /// Tipo de elementos de la secuencia.
    /// </typeparam>
    /// <typeparam name="TResult">
    /// Tipo de resultado obtenido por la función.
    /// </typeparam>
    /// <param name="collection">
    /// Secuencia sobre la cual ejecutar una operación bloqueada.
    /// </param>
    /// <param name="function">
    /// Función a ejecutar sobre la secuencia.
    /// </param>
    public static TResult Locked<T, TResult>(this T collection, Func<T, TResult> function) where T : IEnumerable
    {
        if (collection is ICollection c)
        {
            try
            {
                if (c.IsSynchronized) return function(collection);
                lock (c.SyncRoot) return function(collection);
            }
            catch (NotSupportedException)
            {
                return function(collection);
            }
        }
        lock (collection) return function(collection);
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
        int count = 0;
        foreach (object? j in collection)
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
        foreach (object? j in collection) yield return j;
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
        foreach (object? j in collection)
        {
            if (j is not null) yield return j;
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
        IEnumerator ea = collection.GetEnumerator();
        IEnumerator eb = items.GetEnumerator();
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

    private static int IndexOfEnumerable(IEnumerable e, object? i)
    {
        IEnumerator n = e.GetEnumerator();
        int c = 0;
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
        IEnumerator n = e.GetEnumerator();
        int c = 0;
        while (n.MoveNext()) c++;
        (n as IDisposable)?.Dispose();
        return c;
    }
}
