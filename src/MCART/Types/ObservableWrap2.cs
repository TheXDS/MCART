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

using System.Collections.Specialized;
using TheXDS.MCART.Types.Base;
using System.Collections;
using System;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;
using TheXDS.MCART.Exceptions;

namespace TheXDS.MCART.Types
{
    /// <summary>
    ///     Define una serie de miembros a implementar por una clase que defina
    ///     un envoltorio observable sobre un <see cref="IList"/>.
    /// </summary>
    public interface IObservableListWrap : IList, INotifyCollectionChanged, IRefreshable
    {
        /// <summary>
        ///     Obtiene una referencia a la lista subyacente de este envoltorio
        ///     observable.
        /// </summary>
        IList UnderlyingList { get; }

        /// <summary>
        ///     Sustituye la lista subyacente por una nueva.
        /// </summary>
        /// <param name="newCollection">
        ///     Lista a establecer como la lista subyacente.
        /// </param>
        void Substitute(IList newCollection);
    }

    /// <summary>
    ///     Define una serie de miembros a implementar por una clase que defina
    ///     un envoltorio observable sobre un <see cref="ICollection{T}"/>.
    /// </summary>
    public interface IObservableCollectionWrap<T> : INotifyCollectionChanged, ICollection<T>, IRefreshable
    {
        /// <summary>
        ///     Obtiene una referencia a la colección subyacente de este
        ///     envoltorio observable.
        /// </summary>
        ICollection<T> UnderlyingCollection { get; }

        /// <summary>
        ///     Sustituye la colección subyacente por una nueva.
        /// </summary>
        /// <param name="newCollection">
        ///     Colección a establecer como la colección subyacente.
        /// </param>
        void Substitute(ICollection<T> newCollection);
    }

    /// <summary>
    ///     Define una serie de miembros a implementar por una clase que defina
    ///     un envoltorio observable sobre un <see cref="IList{T}"/>.
    /// </summary>
    public interface IObservableListWrap<T> : INotifyCollectionChanged, IList<T>, IRefreshable
    {
        /// <summary>
        ///     Obtiene una referencia a la lista subyacente de este envoltorio
        ///     observable.
        /// </summary>
        IList<T> UnderlyingList { get; }

        /// <summary>
        ///     Sustituye la lista subyacente por una nueva.
        /// </summary>
        /// <param name="newCollection">
        ///     Lista a establecer como la lista subyacente.
        /// </param>
        void Substitute(IList<T> newCollection);
    }


    /// <summary>
    ///     Clase base para los envoltorios observables de colecciones.
    /// </summary>
    /// <typeparam name="T">Tipo de elementos de la colección.</typeparam>
    /// <typeparam name="TCollection">Tipo de colección.</typeparam>
    public abstract class ObservableWrap<T, TCollection> : NotifyPropertyChanged, INotifyCollectionChanged, ICollection<T> where TCollection : ICollection<T>
    {
        /// <summary>
        ///     Se produce al ocurrir un cambio en la colección.
        /// </summary>
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="ObservableWrap{T, TCollection}"/>
        /// </summary>
        /// <param name="collection"></param>
        protected ObservableWrap(TCollection collection)
        {
            UnderlyingCollection = collection;
        }

        /// <summary>
        ///     Obtiene acceso directo a la colección subyacente envuelta por
        ///     este <see cref="ObservableCollectionWrap{T}"/>.
        /// </summary>
        public TCollection UnderlyingCollection { get; private set; }

        /// <summary>
        ///     Obtiene la cuenta de elementos contenidos dentro de la
        ///     colección.
        /// </summary>
        public int Count => UnderlyingCollection?.Count ?? 0;

        /// <summary>
        ///     Obtiene un valor que indica si la colección es de solo lectura.
        /// </summary>
        public bool IsReadOnly => UnderlyingCollection?.IsReadOnly ?? false;

        /// <summary>
        ///     Agrega un nuevo elemento al final de la colección.
        /// </summary>
        /// <param name="item">Elemento a agregar.</param>
        public void Add(T item)
        {
            if (UnderlyingCollection is null) throw new InvalidOperationException();
            UnderlyingCollection.Add(item);
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item));
            Notify(nameof(Count));
        }

        /// <summary>
        ///     Limpia la colección.
        /// </summary>
        public void Clear()
        {
            if (UnderlyingCollection is null) return;
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
            return UnderlyingCollection?.Contains(item) ?? false;
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
            UnderlyingCollection?.CopyTo(array, arrayIndex);
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
            var result = UnderlyingCollection?.Remove(item) ?? false;

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
            return UnderlyingCollection?.GetEnumerator() ?? new List<T>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return UnderlyingCollection?.GetEnumerator() ?? new T[0].GetEnumerator();
        }

        /// <summary>
        ///     Obliga a notificar un cambio en la colección.
        /// </summary>
        public override void Refresh()
        {
            base.Refresh();

            Substitute(UnderlyingCollection);
        }

        /// <summary>
        ///     Sustituye la colección subyacente por una nueva.
        /// </summary>
        /// <param name="newCollection">
        ///     Colección a establecer como la colección subyacente.
        /// </param>
        public void Substitute(TCollection newCollection)
        {
            UnderlyingCollection = default;
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            UnderlyingCollection = newCollection;
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, (IList)UnderlyingCollection));
        }
    }














    //public class ObservableListWrap<T> : NotifyPropertyChanged, IObservableListWrap<T>
    //{
    //    private IList<T> _underlyingList;
    //    public IList<T> UnderlyingList { get => _underlyingList; set => Substitute(value); }
    //    IList IObservableListWrap.UnderlyingList => _underlyingList as IList;

    //    /// <summary>
    //    ///     Accesa a los objetos contenidos en esta lista por medio de un
    //    ///     índice.
    //    /// </summary>
    //    /// <param name="index">
    //    ///     Índice del objeto a obtener o establecer.
    //    /// </param>
    //    /// <returns>
    //    ///     El objeto localizado en el índice <paramref name="index"/> 
    //    ///     dentro de la lista.
    //    /// </returns>
    //    public T this[int index]
    //    {
    //        get => UnderlyingList[index];
    //        set
    //        {
    //            var oldItem = UnderlyingList[index];
    //            UnderlyingList[index] = value;
    //            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, value, oldItem));
    //        }
    //    }

    //    /// <summary>
    //    ///     Accesa a los objetos contenidos en esta lista por medio de un
    //    ///     índice.
    //    /// </summary>
    //    /// <param name="index">
    //    ///     Índice del objeto a obtener o establecer.
    //    /// </param>
    //    /// <returns>
    //    ///     El objeto localizado en el índice <paramref name="index"/> 
    //    ///     dentro de la lista.
    //    /// </returns>
    //    object IList.this[int index] { get => this[index]; set => this[index] = value is T v ? v : default; }


    //    public bool IsFixedSize => (_underlyingList as IList)?.IsFixedSize ?? false;

    //    public bool IsReadOnly => _underlyingList.IsReadOnly;

    //    public int Count => _underlyingList.Count;

    //    public bool IsSynchronized => (_underlyingList as IList)?.IsSynchronized ?? false;

    //    public object SyncRoot => (_underlyingList as IList)?.IsFixedSize ?? false;


    //    public event NotifyCollectionChangedEventHandler CollectionChanged;

    //    int IList.Add(object value)
    //    {
    //        if (!(UnderlyingList is IList l)) throw new InvalidOperationException();
    //        var returnValue = l.Add(value);
    //        CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, value));
    //        Notify(nameof(Count));
    //        return returnValue;
    //    }

    //    public void Add(T item)
    //    {
    //        UnderlyingList.Add(item);
    //        CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item));
    //        Notify(nameof(Count));
    //    }

    //    public void Clear()
    //    {
    //        UnderlyingList.Clear();
    //        CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
    //        Notify(nameof(Count));
    //    }

    //    bool IList.Contains(object value)
    //    {
    //        return (UnderlyingList as IList ?? throw new InvalidOperationException()).Contains(value);
    //    }

    //    public bool Contains(T item)
    //    {
    //        return UnderlyingList.Contains(item);
    //    }

    //    public void CopyTo(Array array, int index)
    //    {
    //        (UnderlyingList as IList ?? throw new InvalidOperationException()).CopyTo(array, index);
    //    }

    //    public void CopyTo(T[] array, int arrayIndex)
    //    {
    //        UnderlyingList.CopyTo(array, arrayIndex);
    //    }

    //    public IEnumerator<T> GetEnumerator()
    //    {
    //        return UnderlyingList.GetEnumerator();
    //    }

    //    int IList.IndexOf(object value)
    //    {
    //        return (UnderlyingList as IList ?? throw new InvalidOperationException()).IndexOf(value);
    //    }

    //    public int IndexOf(T item)
    //    {
    //        return UnderlyingList.IndexOf(item);
    //    }

    //    void IList.Insert(int index, object value)
    //    {
    //        if (!(UnderlyingList is IList l)) throw new InvalidOperationException();
    //        l.Insert(index, value);
    //        CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, value));
    //        Notify(nameof(Count));
    //    }

    //    public void Insert(int index, T item)
    //    {
    //        UnderlyingList.Insert(index, item);
    //        CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item));
    //        Notify(nameof(Count));
    //    }

    //    public void Refresh()
    //    {
    //        base.Refresh();
    //        Substitute(UnderlyingList);
    //    }

    //    void IList.Remove(object value)
    //    {
    //        if (!(UnderlyingList is IList l)) throw new InvalidOperationException();
    //        if (l.Contains(value))
    //        {
    //            l.Remove(value);
    //            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, value));
    //            Notify(nameof(Count));
    //        }
    //    }

    //    public bool Remove(T item)
    //    {
    //        if (UnderlyingList.Remove(item))
    //        { 
    //            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item));
    //            Notify(nameof(Count));
    //            return true;
    //        }
    //        return false;
    //    }

    //    public void RemoveAt(int index)
    //    {
    //        var item = UnderlyingList[index];
    //        UnderlyingList.RemoveAt(index);
    //        CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item));
    //        Notify(nameof(Count));
    //    }

    //    public void Substitute(IList<T> newList)
    //    {
    //        UnderlyingList = new T[0];
    //        CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
    //        UnderlyingList = newList;
    //        CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, newList));
    //    }


    //    IEnumerator IEnumerable.GetEnumerator()
    //    {
    //        return UnderlyingList.GetEnumerator();
    //    }

    //    void IObservableListWrap.Substitute(IList newCollection)
    //    {
    //        Substitute(newCollection as IList<T> ?? throw new InvalidOperationException());
    //    }
    //}
}