/*
ObservableDictionaryWrap.cs

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

using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;
using TheXDS.MCART.Types.Base;
using NcchEa = System.Collections.Specialized.NotifyCollectionChangedEventArgs;

namespace TheXDS.MCART.Types;

/// <summary>
/// Envuelve un diccionario para proveerlo de eventos de notificación
/// de cambio de propiedad y contenido.
/// </summary>
/// <typeparam name="TKey">
/// Tipo de índice del diccionario.
/// </typeparam>
/// <typeparam name="TValue">
/// Tipo de elementos contenidos dentro del diccionario.
/// </typeparam>
public class ObservableDictionaryWrap<TKey, TValue> : ObservableWrap<KeyValuePair<TKey, TValue>, IDictionary<TKey, TValue>>, IDictionary<TKey, TValue> where TKey : notnull
{
    /// <summary>
    /// Inicializa una nueva instancia de la clase 
    /// <see cref="ObservableDictionaryWrap{TKey, TValue}"/>.
    /// </summary>
    public ObservableDictionaryWrap() { }

    /// <summary>
    /// Inicializa una nueva instancia de la clase 
    /// <see cref="ObservableDictionaryWrap{TKey, TValue}"/>.
    /// </summary>
    /// <param name="collection">
    /// Colección a utilizar como el diccionario subyacente.
    /// </param>
    public ObservableDictionaryWrap(IDictionary<TKey, TValue> collection) : base(collection) { }

    /// <summary>
    /// Obtiene o establece el valor del objeto en el índice
    /// especificado dentro del diccionario.
    /// </summary>
    /// <param name="key">
    /// Índice del valor a obtener/establecer.
    /// </param>
    /// <returns>
    /// El valor encontrado en el índice especificado dentro del
    /// diccionario.
    /// </returns>
    public TValue this[TKey key]
    {
        get => UnderlyingCollection is not null ? UnderlyingCollection[key] : default!;
        set
        {
            if (UnderlyingCollection is null) throw new InvalidOperationException();
            TValue? oldValue = UnderlyingCollection[key];
            UnderlyingCollection[key] = value;
            RaiseCollectionChanged(new NcchEa(NotifyCollectionChangedAction.Replace, oldValue, value));
        }
    }

    /// <summary>
    /// Obtiene una colección con todas las llaves del diccionario.
    /// </summary>
    public ICollection<TKey> Keys => UnderlyingCollection?.Keys ?? Array.Empty<TKey>();

    /// <summary>
    /// Obtiene una colección con todos los valores del diccionario.
    /// </summary>
    public ICollection<TValue> Values => UnderlyingCollection?.Values ?? Array.Empty<TValue>();

    /// <summary>
    /// Agrega un valor a este diccionario en el índice especificado.
    /// </summary>
    /// <param name="key">
    /// Índice a utilizar.
    /// </param>
    /// <param name="value">
    /// Valor a agregar al diccionario.
    /// </param>
    public void Add(TKey key, TValue value)
    {
        if (UnderlyingCollection is null) throw new InvalidOperationException();
        UnderlyingCollection.Add(key, value);
        RaiseCollectionChanged(new NcchEa(NotifyCollectionChangedAction.Add, new KeyValuePair<TKey, TValue>(key, value)));
    }

    /// <summary>
    /// Determina si el índice existe en el diccionario.
    /// </summary>
    /// <param name="key">
    /// Índice a buscar dentro del diccionario.
    /// </param>
    /// <returns>
    /// <see langword="true"/> si el índice existe dentro del
    /// diccionario, <see langword="false"/> en caso contrario.
    /// </returns>
    public bool ContainsKey(TKey key) => UnderlyingCollection?.ContainsKey(key) ?? false;

    /// <summary>
    /// Quita al elemento con el índice especificado del diccionario.
    /// </summary>
    /// <param name="key">
    /// Índice a quitar.
    /// </param>
    /// <returns>
    /// <see langword="true"/> si se ha quitado el índice del
    /// diccionario exitosamente, <see langword="false"/> si el índice
    /// no existía en el diccionario o si ocurre otro problema al
    /// intentar realizar la operación.
    /// </returns>
    /// 
    public bool Remove(TKey key)
    {
        if (!UnderlyingCollection?.ContainsKey(key) ?? true) return false;
        TValue? oldItem = UnderlyingCollection![key];
        UnderlyingCollection.Remove(key);
        RaiseCollectionChanged(new NcchEa(NotifyCollectionChangedAction.Remove, new KeyValuePair<TKey, TValue>(key, oldItem)));
        return true;
    }

    /// <summary>
    /// Intenta obtener un valor dentro del diccionario.
    /// </summary>
    /// <param name="key">Índice del valor a obtener.</param>
    /// <param name="value">
    /// Parámetro de salida. Valor obtenido del diccionario en el
    /// índice especificado.
    /// </param>
    /// <returns>
    /// <see langword="true"/> si se ha obtenido el valor del
    /// diccionario correctamente, <see langword="false"/> si el índice
    /// no existía en el diccionario o si ocurre otro problema
    /// obteniendo el valor.
    /// </returns>
    public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
    {
        if (UnderlyingCollection is not null) 
        { 
            return UnderlyingCollection.TryGetValue(key, out value!);
        }
        value = default!;
        return false;
    }
}
