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
using System;
using System.Diagnostics;

namespace TheXDS.MCART.Types
{
    /// <summary>
    ///     Envuelve una colección para proveerla de eventos de notificación de
    ///     cambio de propiedad y contenido.
    /// </summary>
    /// <typeparam name="T">
    ///     Tipo de elementos contenidos dentro de la colección.
    /// </typeparam>
    public class ObservableCollectionWrap<T> : ObservableWrap<T, ICollection<T>>
    {
    }


    public class ObservableListWrap : NotifyPropertyChanged, IList, INotifyCollectionChanged
    {
        public IList UnderlyingList { get; private set; }

        public object this[int index]
        {
            get => UnderlyingList[index];
            set
            {
                var oldItem = UnderlyingList[index];
                UnderlyingList[index] = value;
                CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, value, oldItem));
            }
        }

        public bool IsFixedSize => UnderlyingList.IsFixedSize;

        public bool IsReadOnly => UnderlyingList.IsReadOnly;

        public int Count => UnderlyingList.Count;

        public bool IsSynchronized => UnderlyingList.IsSynchronized;

        public object SyncRoot => UnderlyingList.SyncRoot;

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public int Add(object value)
        {
            var returnValue = UnderlyingList.Add(value);
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, value));
            Notify(nameof(Count));
            return returnValue;
        }

        public void Clear()
        {
            UnderlyingList.Clear();
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            Notify(nameof(Count));
        }

        public bool Contains(object value)
        {
            return UnderlyingList.Contains(value);
        }

        public void CopyTo(Array array, int index)
        {
            UnderlyingList.CopyTo(array, index);
        }

        public IEnumerator GetEnumerator()
        {
            return UnderlyingList.GetEnumerator();
        }

        public int IndexOf(object value)
        {
            return UnderlyingList.IndexOf(value);
        }

        public void Insert(int index, object value)
        {
            UnderlyingList.Insert(index, value);
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, value));
            Notify(nameof(Count));
        }

        public void Remove(object value)
        {
            if (UnderlyingList.Contains(value))
            {
                UnderlyingList.Remove(value);
                CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, value));
                Notify(nameof(Count));
            }
        }

        [DebuggerStepThrough]
        public void RemoveAt(int index)
        {
            var item = UnderlyingList[index];
            UnderlyingList.RemoveAt(index);
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item));
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
            UnderlyingList = new object[0];
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            UnderlyingList = newList;
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, newList));
        }
    }
}