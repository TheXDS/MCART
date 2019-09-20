/*
ObservableDictionaryWrap.cs

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

using System.Collections.Generic;
using System.Collections.Specialized;
using NcchEa = System.Collections.Specialized.NotifyCollectionChangedEventArgs;
using TheXDS.MCART.Types.Base;

namespace TheXDS.MCART.Types
{
    /// <summary>
    ///     Envuelve un diccionario para proveerlo de eventos de notificación
    ///     de cambio de propiedad y contenido.
    /// </summary>
    /// <typeparam name="TKey">
    ///     Tipo de índice del diccionario.
    /// </typeparam>
    /// <typeparam name="TValue">
    ///     Tipo de elementos contenidos dentro del diccionario.
    /// </typeparam>
    public class ObservableDictionaryWrap<TKey, TValue> : ObservableWrap<KeyValuePair<TKey, TValue>, IDictionary<TKey, TValue>>, IDictionary<TKey, TValue> where TKey : notnull
    {
        /// <summary>
        ///     Inicializa una nueva instancia de la clase 
        ///     <see cref="ObservableDictionaryWrap{TKey, TValue}"/>.
        /// </summary>
        public ObservableDictionaryWrap() { }

        /// <summary>
        ///     Inicializa una nueva instancia de la clase 
        ///     <see cref="ObservableDictionaryWrap{TKey, TValue}"/>.
        /// </summary>
        /// <param name="collection">
        ///     Colección a utilizar como el diccionario sunyacente.
        /// </param>
        public ObservableDictionaryWrap(IDictionary<TKey, TValue> collection) : base(collection) { }

        /// <summary>
        ///     Obtiene o establece el valor del objeto en el índice
        ///     especificado dentro del diccionario.
        /// </summary>
        /// <param name="key">
        ///     Índice del valor a obtener/establecer.
        /// </param>
        /// <returns>
        ///     El valor encontrado en el índice especificado dentro del
        ///     diccionario.
        /// </returns>
        public TValue this[TKey key]
        {
            get => UnderlyingCollection[key];
            set
            {                
                var oldValue = UnderlyingCollection[key];
                UnderlyingCollection[key] = value;
                RaiseCollectionChanged(new NcchEa(NotifyCollectionChangedAction.Replace, oldValue, value));
            }
        }

        /// <summary>
        ///     Obtiene una colección con todas las llaves del diccionario.
        /// </summary>
        public ICollection<TKey> Keys => UnderlyingCollection.Keys;

        /// <summary>
        ///     Obtiene una colección con todos los valores del diccionario.
        /// </summary>
        public ICollection<TValue> Values => UnderlyingCollection.Values;

        /// <summary>
        ///     Agrega un valor a este diccionario en el índice especificado.
        /// </summary>
        /// <param name="key">
        ///     Índice a utilizar.
        /// </param>
        /// <param name="value">
        ///     Valor a agregar al diccionario.
        /// </param>
        public void Add(TKey key, TValue value)
        {
            UnderlyingCollection.Add(key, value);
            RaiseCollectionChanged(new NcchEa(NotifyCollectionChangedAction.Add, new KeyValuePair<TKey, TValue>(key, value)));
        }

        /// <summary>
        ///     Determina si el índice existe en el diccionario.
        /// </summary>
        /// <param name="key">
        ///     Índice a buscar dentro del diccionario.
        /// </param>
        /// <returns>
        ///     <see langword="true"/> si el índice existe dentro del
        ///     diccionario, <see langword="false"/> en caso contrario.
        /// </returns>
        public bool ContainsKey(TKey key) => UnderlyingCollection?.ContainsKey(key) ?? false;

        /// <summary>
        ///     Quita al elemento con el índice especificado del diccionario.
        /// </summary>
        /// <param name="key">
        ///     Índice a quitar.
        /// </param>
        /// <returns>
        ///     <see langword="true"/> si se ha quitado el índice del
        ///     diccionario exitosamente, <see langword="false"/> si el índice
        ///     no existía en el diccionario o si ocurre otro problema al
        ///     intentar realizar la operación.
        /// </returns>
        /// 
        public bool Remove(TKey key)
        {
            if (!UnderlyingCollection.ContainsKey(key)) return false;
            var oldItem = UnderlyingCollection[key];
            UnderlyingCollection.Remove(key);
            RaiseCollectionChanged(new NcchEa(NotifyCollectionChangedAction.Remove, new KeyValuePair<TKey, TValue>(key, oldItem)));
            return true;
        }

        /// <summary>
        ///     Intenta obtener un valor dentro del diccionario.
        /// </summary>
        /// <param name="key">Índice del valor a obtener.</param>
        /// <param name="value">
        ///     Parámetro de salida. Valor obtenido del diccionario en el
        ///     índice especificado.
        /// </param>
        /// <returns>
        ///     <see langword="true"/> si se ha obtenido el valor del
        ///     diccionario correctamente, <see langword="false"/> si el índice
        ///     no existía en el diccionario o si ocurre otro problema
        ///     obteniendo el valor.
        /// </returns>
        public bool TryGetValue(TKey key, out TValue value) => UnderlyingCollection.TryGetValue(key, out value);
    }
}
