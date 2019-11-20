/*
ObservableListWrap.cs

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

#nullable enable

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using TheXDS.MCART.Types.Base;
using static System.Collections.Specialized.NotifyCollectionChangedAction;
using Nccha = System.Collections.Specialized.NotifyCollectionChangedAction;
using NcchEa = System.Collections.Specialized.NotifyCollectionChangedEventArgs;

namespace TheXDS.MCART.Types
{
    /// <summary>
    ///     Envuelve una lista para proveerla de notificación de cambios en el
    ///     contenido de la colección.
    /// </summary>
    [DebuggerStepThrough]
    public class ObservableListWrap : ObservableWrapBase, IList, INotifyCollectionChanged
    {
        /// <summary>
        ///     Obtiene acceso directo a la lista subyacente controlada por
        ///     este <see cref="ObservableListWrap"/>.
        /// </summary>
        public IList UnderlyingList { get; private set; }

        /// <summary>
        ///     Permite acceder de forma indexada al contenido de este
        ///     <see cref="ObservableListWrap"/>.
        /// </summary>
        /// <param name="index">
        ///     Índice del elemento a obtener o establecer.
        /// </param>
        /// <returns>
        ///     El elemento encontrado en el índice especificado dentro de la
        ///     colección.
        /// </returns>
        public object? this[int index]
        {
            get => UnderlyingList[index];
            set
            {
                var oldItem = UnderlyingList[index];
                UnderlyingList[index] = value;
                RaiseCollectionChanged(new NcchEa(Replace, value, oldItem));
            }
        }

        /// <summary>
        ///     Obliga a refrescar el estado de un elemento dentro de la lista.
        /// </summary>
        /// <param name="index">Índice del elemento a refrescar.</param>
        public void RefreshAt(int index)
        {
            RefreshItem(this[index]);
        }

        /// <summary>
        ///     Obtiene un valor que determina si esta lista es de tamaño fijo.
        /// </summary>
        public bool IsFixedSize => UnderlyingList.IsFixedSize;

        /// <summary>
        ///     Obtiene un valor que determina si esta lista es de solo
        ///     lectura.
        /// </summary>
        public bool IsReadOnly => UnderlyingList.IsReadOnly;

        /// <summary>
        ///     Obtiene la cuenta de elementos contenidos dentro de la
        ///     colección.
        /// </summary>
        public int Count => UnderlyingList.Count;

        /// <summary>
        ///     Obtiene un valor que indica si el acceso a esta
        ///     <see cref="ICollection"/> es sincronizado (seguro para
        ///     multihilo).
        /// </summary>
        public bool IsSynchronized => UnderlyingList.IsSynchronized;

        /// <summary>
        ///     Obtiene un objeto que puede ser utilizado para sincronizar el
        ///     acceso al <see cref="ICollection"/>.
        /// </summary>
        public object SyncRoot => UnderlyingList.SyncRoot;

        /// <summary>
        ///     Inicializa una nueva instancia de la clase 
        ///     <see cref="ObservableListWrap"/>.
        /// </summary>
        public ObservableListWrap()
        {
            UnderlyingList = new List<object>();
        }

        /// <summary>
        ///     Inicializa una nueva instancia de la clase 
        ///     <see cref="ObservableListWrap"/>.
        /// </summary>
        /// <param name="list">
        ///     Lista a establecer como la lista subyacente.
        /// </param>
        public ObservableListWrap(IList list)
        {
            UnderlyingList = list;
        }

        /// <summary>
        ///     Agrega un elemento a este <see cref="ObservableListWrap"/>.
        /// </summary>
        /// <param name="value">
        ///     Valor a agregar a este <see cref="ObservableListWrap"/>.
        /// </param>
        /// <returns>
        ///     El índice en el cual ha sido agregado el elemento.
        /// </returns>
        public int Add(object? value)
        {
            var returnValue = UnderlyingList.Add(value);
            RaiseCollectionChanged(new NcchEa(Nccha.Add, value));
            Notify(nameof(Count));
            return returnValue;
        }

        /// <summary>
        ///     Quita todos los elementos de este
        ///     <see cref="ObservableListWrap"/>.
        /// </summary>
        public void Clear()
        {
            UnderlyingList.Clear();
            RaiseCollectionChanged(new NcchEa(Reset));
            Notify(nameof(Count));
        }

        /// <summary>
        ///     Determina si este <see cref="ObservableListWrap"/> contiene un
        ///     valor especificado.
        /// </summary>
        /// <param name="value">
        ///     Valor a comprobar.
        /// </param>
        /// <returns>
        ///     <see langword="true"/> si este <see cref="ObservableListWrap"/>
        ///     contiene el valor especificado, <see langword="false"/> en caso
        ///     contrario.
        /// </returns>
        public override bool Contains(object? value)
        {
            return UnderlyingList.Contains(value);
        }

        /// <summary>
        ///     Copia el contenido de este <see cref="ObservableListWrap"/> a
        ///     un arreglo, iniciando en un índice en particular dentro del
        ///     arreglo.
        /// </summary>
        /// <param name="array">Arreglo de destino de la copia.</param>
        /// <param name="index">
        ///     Índice dentro del arreglo desde el cual iniciar a copiar.
        /// </param>
        public void CopyTo(Array array, int index)
        {
            UnderlyingList.CopyTo(array, index);
        }

        /// <summary>
        ///     Obtiene un enumerador que itera sobre la colección.
        /// </summary>
        /// <returns>
        ///     Un enumerador que puede ser utilizado para iterar sobre la colección.
        /// </returns>
        protected override IEnumerator OnGetEnumerator()
        {
            return UnderlyingList.GetEnumerator();
        }

        /// <summary>
        ///     Obtiene el índice de un elemento específico dentro de este
        ///     <see cref="ObservableListWrap"/>.
        /// </summary>
        /// <param name="value">
        ///     Valor del cual obtener el índice dentro de este
        ///     <see cref="ObservableListWrap"/>.
        /// </param>
        /// <returns>
        ///     El índice del elemento dentro de este
        ///     <see cref="ObservableListWrap"/>.
        /// </returns>
        public override int IndexOf(object? value)
        {
            return UnderlyingList.IndexOf(value);
        }

        /// <summary>
        ///     Inserta un elemento dentro de este
        ///     <see cref="ObservableListWrap"/> en el índice especificado.
        /// </summary>
        /// <param name="index">
        ///     Índice en el cual realizar la inserción.
        /// </param>
        /// <param name="value">
        ///     Valor a insertar en este <see cref="ObservableListWrap"/>.
        /// </param>
        public void Insert(int index, object? value)
        {
            UnderlyingList.Insert(index, value);
            RaiseCollectionChanged(new NcchEa(Nccha.Add, value));
            Notify(nameof(Count));
        }

        /// <summary>
        ///     Quita un elemento de este <see cref="ObservableListWrap"/>.
        /// </summary>
        /// <param name="value">
        ///     valor a quitar de este <see cref="ObservableListWrap"/>.
        /// </param>
        public void Remove(object? value)
        {
            if (UnderlyingList.Contains(value))
            {
                var index = UnderlyingList.IndexOf(value);
                UnderlyingList.Remove(value);
                RaiseCollectionChanged(new NcchEa(Nccha.Remove, value, index));
                Notify(nameof(Count));
            }
        }


        /// <summary>
        ///     Quita el elemento en el íncide especificado de este
        ///     <see cref="ObservableListWrap"/>.
        /// </summary>
        /// <param name="index">
        ///     Índice del elemento a remover.
        /// </param>
        public void RemoveAt(int index)
        {
            var item = UnderlyingList[index];
            UnderlyingList.RemoveAt(index);
            RaiseCollectionChanged(new NcchEa(Nccha.Remove, item, index));
            Notify(nameof(Count));
        }

        /// <summary>
        ///     Obliga a notificar un cambio en la lista.
        /// </summary>
        public override void Refresh()
        {
            base.Refresh();
            Substitute(UnderlyingList);
        }

        /// <summary>
        ///     Sustituye la lista subyacente por una nueva.
        /// </summary>
        /// <param name="newList">
        ///     Lista a establecer como la lista subyacente.
        /// </param>
        public void Substitute(IList newList)
        {
            UnderlyingList = Array.Empty<object>();
            RaiseCollectionChanged(new NcchEa(Reset));
            UnderlyingList = newList;
            if (!(newList is null)) RaiseCollectionChanged(new NcchEa(Nccha.Add, newList));
        }
    }

    /// <summary>
    ///     Envuelve una lista genérica para proveerla de notificación de
    ///     cambios en el contenido de la colección.
    /// </summary>
    /// <typeparam name="T">Tipo de elementos de la lista.</typeparam>
    [DebuggerStepThrough]
    public class ObservableListWrap<T> : ObservableWrap<T, IList<T>>, IList<T>
    {
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="ObservableListWrap{T}"/>.
        /// </summary>
        /// <param name="collection"></param>
        public ObservableListWrap(IList<T> collection) : base(collection)
        {
        }

        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="ObservableListWrap{T}"/>.
        /// </summary>
        public ObservableListWrap()
        {
        }

        /// <summary>
        ///     Permite acceder de forma indexada al contenido de este
        ///     <see cref="ObservableListWrap{T}"/>.
        /// </summary>
        /// <param name="index">
        ///     Índice del elemento a obtener o establecer.
        /// </param>
        /// <returns>
        ///     El elemento encontrado en el índice especificado dentro de la
        ///     colección.
        /// </returns>
        public T this[int index]
        {
            get => UnderlyingCollection[index];
            set
            {
                var oldItem = UnderlyingCollection[index];
                UnderlyingCollection[index] = value;
                RaiseCollectionChanged(new NcchEa(Nccha.Replace, value, oldItem));
            }
        }

        /// <summary>
        ///     Determina el índice de un elemento específico dentro del
        ///     <see cref="ObservableListWrap{T}"/>.
        /// </summary>
        /// <param name="item">
        ///     Elemento del cual obtener el índice.
        /// </param>
        /// <returns>
        ///     El índice del elemento especificado.
        /// </returns>
        public int IndexOf(T item) => UnderlyingCollection.IndexOf(item);

        /// <summary>
        ///     Determina si la secuencia subyacente contiene al elemento
        ///     especificado.
        /// </summary>
        /// <param name="item">
        ///     Elemento a buscar dentro de la secuencia.
        /// </param>
        /// <returns>
        ///     <see langword="true"/> si la secuencia contiene al elemento
        ///     especificado, <see langword="false"/> en caso contrario.
        /// </returns>
        public override bool Contains(T item) => UnderlyingCollection.Contains(item);

        /// <summary>
        ///     Inserta un elemento dentro de este
        ///     <see cref="ObservableListWrap{T}"/> en un índice específico.
        /// </summary>
        /// <param name="index">
        ///     Posición en la cual insertar el nuevo elemento.
        /// </param>
        /// <param name="item">
        ///     Elemento a insertar en el índice especificado.
        /// </param>
        public void Insert(int index, T item)
        {
            UnderlyingCollection.Insert(index, item);
            RaiseCollectionChanged(new NcchEa(Nccha.Add, item));
            Notify(nameof(Count));
        }

        /// <summary>
        ///     Quita el elemento de este <see cref="ObservableListWrap{T}"/>
        ///     en el índice especificado.
        /// </summary>
        /// <param name="index">
        ///     Índice del elemento a remover.
        /// </param>
        public void RemoveAt(int index)
        {
            var item = UnderlyingCollection[index];
            UnderlyingCollection.RemoveAt(index);
            RaiseCollectionChanged(new NcchEa(Nccha.Remove, item, index));
            Notify(nameof(Count));
        }
    }
}