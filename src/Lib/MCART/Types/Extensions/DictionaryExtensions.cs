/*
DictionaryExtensions.cs

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

namespace TheXDS.MCART.Types.Extensions;

/// <summary>
/// Extensiones para todos los elementos de tipo <see cref="IDictionary{TKey, TValue}" />.
/// </summary>
public static class DictionaryExtensions
{
    /// <summary>
    /// Agrega un valor al diccionario.
    /// </summary>
    /// <typeparam name="TKey">
    /// Tipo de llave a utilizar para identificar al valor.
    /// </typeparam>
    /// <typeparam name="TValue">Tipo de valor a agregar.</typeparam>
    /// <param name="dictionary">
    /// Diccionario al cual agregar el nuevo valor.
    /// </param>
    /// <param name="key">Llave para identificar al nuevo valor.</param>
    /// <param name="value">Valor a agregar.</param>
    /// <returns>
    /// La misma instancia que <paramref name="value"/>, permitiendo
    /// utilizar sintaxis Fluent.
    /// </returns>
    public static TValue Push<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue value) where TKey : notnull
    {
        dictionary.Add(key, value);
        return value;
    }

    /// <summary>
    /// Agrega un valor al diccionario especificado.
    /// </summary>
    /// <typeparam name="TKey">
    /// Tipo de llave a utilizar para identificar al valor.
    /// </typeparam>
    /// <typeparam name="TValue">Tipo de valor a agregar.</typeparam>
    /// <param name="dictionary">
    /// Diccionario al cual agregar el nuevo valor.
    /// </param>
    /// <param name="key">Llave para identificar al nuevo valor.</param>
    /// <param name="value">Valor a agregar.</param>
    /// <returns>
    /// La misma instancia que <paramref name="value"/>, permitiendo
    /// utilizar sintaxis Fluent.
    /// </returns>
    public static TValue PushInto<TKey, TValue>(this TValue value, TKey key, IDictionary<TKey, TValue> dictionary) where TKey : notnull
    {
        dictionary.Add(key, value);
        return value;
    }

    /// <summary>
    /// Obtiene el elemento con la llave especificada del diccionario,
    /// quitándolo.
    /// </summary>
    /// <typeparam name="TKey">
    /// Tipo de llave del objeto a obtener.
    /// </typeparam>
    /// <typeparam name="TValue">
    /// Tipo de valor contenido por el diccionario.
    /// </typeparam>
    /// <param name="dictionary">
    /// Diccionario desde el cual obtener y remover el objeto.
    /// </param>
    /// <param name="key">Llave del objeto a obtener.</param>
    /// <param name="value">Valor obtenido del diccionario.</param>
    /// <returns>
    /// <see langword="true"/> si el objeto fue quitado satisfactoriamente
    /// del diccionario, <see langword="false"/> en caso que el diccionario
    /// no contuviese a un elemento con la llave especificada.
    /// </returns>
    public static bool Pop<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, out TValue value) where TKey : notnull
    {
        if (!dictionary.ContainsKey(key))
        {
            value = default!;
            return false;
        }
        value = dictionary[key];
        return dictionary.Remove(key);
    }

    /// <summary>
    /// Comprueba la existencia de referencias circulares en un
    /// diccionario de objetos en forma de árbol.
    /// </summary>
    /// <typeparam name="T">
    /// Tipo de elementos contenidos en el diccionario.
    /// </typeparam>
    /// <param name="dictionary">
    /// Diccionario en el cual realizar la comprobación.
    /// </param>
    /// <param name="element">
    /// Elemento a comprobar.
    /// </param>r
    /// <returns>
    /// <see langword="false"/> si no existen referencias circulares 
    /// dentro del diccionario, <see langword="true"/> en caso 
    /// contrario.
    /// </returns>
    public static bool CheckCircularRef<T>(this IDictionary<T, IEnumerable<T>> dictionary, T element) where T : notnull
    {
        return BranchScanFails(element, element, dictionary, new HashSet<T>());
    }

    /// <summary>
    /// Comprueba la existencia de referencias circulares en un
    /// diccionario de objetos.
    /// </summary>
    /// <typeparam name="T">
    /// Tipo de elementos contenidos en el diccionario.
    /// </typeparam>
    /// <param name="dictionary">
    /// Diccionario en el cual realizar la comprobación.
    /// </param>
    /// <param name="element">
    /// Elemento a comprobar.
    /// </param>
    /// <returns>
    /// <see langword="false"/> si no existen referencias circulares 
    /// dentro del diccionario, <see langword="true"/> en caso 
    /// contrario.
    /// </returns>
    public static bool CheckCircularRef<T>(this IDictionary<T, ICollection<T>> dictionary, T element) where T : notnull
    {
        return BranchScanFails(element, element, dictionary, new HashSet<T>());
    }

    /// <summary>
    /// Comprueba la existencia de referencias circulares en un
    /// diccionario de objetos.
    /// </summary>
    /// <typeparam name="T">
    /// Tipo de elementos contenidos en el diccionario.
    /// </typeparam>
    /// <param name="dictionary">
    /// Diccionario en el cual realizar la comprobación.
    /// </param>
    /// <param name="element">
    /// Elemento a comprobar.
    /// </param>
    /// <returns>
    /// <see langword="false"/> si no existen referencias circulares 
    /// dentro del diccionario, <see langword="true"/> en caso 
    /// contrario.
    /// </returns>
    public static bool CheckCircularRef<T>(this IEnumerable<KeyValuePair<T, IEnumerable<T>>> dictionary, T element) where T : notnull
    {
        Dictionary<T, IEnumerable<T>>? d = new();
        foreach (KeyValuePair<T, IEnumerable<T>> j in dictionary) d.Add(j.Key, j.Value);
        return BranchScanFails(element, element, d, new HashSet<T>());
    }

    /// <summary>
    /// Comprueba la existencia de referencias circulares en un
    /// diccionario de objetos.
    /// </summary>
    /// <typeparam name="T">
    /// Tipo de elementos contenidos en el diccionario.
    /// </typeparam>
    /// <param name="dictionary">
    /// Diccionario en el cual realizar la comprobación.
    /// </param>
    /// <param name="element">
    /// Elemento a comprobar.
    /// </param>
    /// <returns>
    /// <see langword="false"/> si no existen referencias circulares 
    /// dentro del diccionario, <see langword="true"/> en caso 
    /// contrario.
    /// </returns>
    public static bool CheckCircularRef<T>(this IEnumerable<KeyValuePair<T, ICollection<T>>> dictionary, T element) where T : notnull
    {
        Dictionary<T, IEnumerable<T>>? d = new();
        foreach (KeyValuePair<T, ICollection<T>> j in dictionary) d.Add(j.Key, j.Value);
        return BranchScanFails(element, element, d, new HashSet<T>());
    }

    private static bool BranchScanFails<T>(T a, T b, IDictionary<T, IEnumerable<T>> tree, ICollection<T> keysChecked) where T : notnull
    {
        if (!tree.ContainsKey(b)) return false;
        foreach (T? j in tree[b])
        {
            if (keysChecked.Contains(j)) return false;
            keysChecked.Add(j);
            if (j.Equals(a)) return true;
            if (BranchScanFails(a, j, tree, keysChecked)) return true;
        }
        return false;
    }

    private static bool BranchScanFails<T>(T a, T b, IDictionary<T, ICollection<T>> tree, ICollection<T> keysChecked) where T : notnull
    {
        if (!tree.ContainsKey(b)) return false;
        foreach (T? j in tree[b])
        {
            if (keysChecked.Contains(j)) return false;
            keysChecked.Add(j);
            if (j.Equals(a)) return true;
            if (BranchScanFails(a, j, tree, keysChecked)) return true;
        }
        return false;
    }
}
