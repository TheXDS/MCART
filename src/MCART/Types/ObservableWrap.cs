/*
ObservableWrap.cs

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
using TheXDS.MCART.Types.Base;
using System.Collections;

namespace TheXDS.MCART.Types
{
    /// <summary>
    ///     Envuelve una colección para proveerla de eventos de notificación de
    ///     cambio de propiedad y contenido.
    /// </summary>
    /// <typeparam name="T">
    ///     Tipo de elementos contenidos dentro de la colección.
    /// </typeparam>
    public class ObservableWrap<T> : NotifyPropertyChanged, INotifyCollectionChanged, ICollection<T>
    {
        /// <summary>
        ///     Se produce al ocurrir un cambio en la colección.
        /// </summary>
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="ObservableWrap{T}"/>.
        /// </summary>
        /// <param name="underlyingCollection">
        ///     Colección a la cual hacer observable.
        /// </param>
        public ObservableWrap(ICollection<T> underlyingCollection)
        {
            UnderlyingCollection = underlyingCollection;            
        }

        /// <summary>
        ///     Obtiene acceso directo a la colección subyacente envuelta por
        ///     este <see cref="ObservableWrap{T}"/>.
        /// </summary>
        public ICollection<T> UnderlyingCollection { get; }

        /// <summary>
        ///     Obtiene la cuenta de elementos contenidos dentro de la
        ///     colección.
        /// </summary>
        public int Count => UnderlyingCollection.Count;

        /// <summary>
        ///     Obtiene un valor que indica si la colección es de solo lectura.
        /// </summary>
        public bool IsReadOnly => UnderlyingCollection.IsReadOnly;

        /// <summary>
        ///     Agrega un nuevo elemento al final de la colección.
        /// </summary>
        /// <param name="item">Elemento a agregar.</param>
        public void Add(T item)
        {
            UnderlyingCollection.Add(item);
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item));
            Notify(nameof(Count));
        }

        /// <summary>
        ///     Limpia la colección.
        /// </summary>
        public void Clear()
        {
            UnderlyingCollection.Clear();
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            Notify(nameof(Count));
        }

        /// <summary>
        ///     Obtiene un valor que determina si el elemento existe en la
        ///     colección.
        /// </summary>
        /// <param name="item">Elemento a comprobar.</param>
        /// <returns>
        ///     <see langword="true"/> si el elemento existe dentro de la
        ///     colección, <see langword="false"/> en caso contrario.
        /// </returns>
        public bool Contains(T item)
        {
            return UnderlyingCollection.Contains(item);
        }

        /// <summary>
        ///     Copia el contenido de esta colección sobre un arreglo.
        /// </summary>
        /// <param name="array">Destino de la copia.</param>
        /// <param name="arrayIndex">
        ///     Índice donde empezar a copiar elementos.
        /// </param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            UnderlyingCollection.CopyTo(array, arrayIndex);
        }

        /// <summary>
        ///     Quita un elemento de la colección.
        /// </summary>
        /// <param name="item">Elemento a quitar.</param>
        /// <returns>
        ///     <see langword="true"/> si el elemento ha sido quitado
        ///     exitosamente de la colección, <see langword="false"/> en caso
        ///     contrario. También se devuelve <see langword="false"/> si el
        ///     elemento no existía en la <see cref="ICollection{T}"/>
        ///     original.
        /// </returns>
        public bool Remove(T item)
        {
            var result = UnderlyingCollection.Remove(item);

            if (result)
            {
                CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item));
                Notify(nameof(Count));
            }

            return result;
        }

        /// <summary>
        ///     Obtiene un enumerador que itera sobre la colección.
        /// </summary>
        /// <returns>
        ///     Un enumerador que puede ser utilizado para iterar sobre la colección.
        /// </returns>
        public IEnumerator<T> GetEnumerator()
        {
            return UnderlyingCollection.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return UnderlyingCollection.GetEnumerator();
        }

        /// <summary>
        ///     Obliga a notificar un cambio en la colección.
        /// </summary>
        public override void Refresh()
        {
            base.Refresh();
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, (IList)UnderlyingCollection, (IList)UnderlyingCollection));
        }
    }
}