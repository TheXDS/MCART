/*
Events.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright (c) 2011 - 2019 César Andrés Morgan

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
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace TheXDS.MCART.Types.Extensions
{
    /// <summary>
    /// Contiene información para el evento <see cref="List{T}.AddingItem"/>.
    /// </summary>
    /// <typeparam name="T">Tipo de elementos de la lista.</typeparam>
    public class AddingItemEventArgs<T> : CancelEventArgs
    {
        /// <summary>
        /// Obtiene el objeto que se agregará al <see cref="List{T}"/>.
        /// </summary>
        public T NewItem { get; }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="AddingItemEventArgs{T}"/>.
        /// </summary>
        /// <param name="newItem">
        /// Objeto a ser agregado al <see cref="List{T}"/> que generó el 
        /// evento.
        /// </param>
        internal AddingItemEventArgs(T newItem) { NewItem = newItem; }
    }
    /// <summary>
    /// Contiene información para el evento <see cref="List{T}.InsertingItem"/>.
    /// </summary>
    /// <typeparam name="T">Tipo de elementos de la lista.</typeparam>
    public class InsertingItemEventArgs<T> : CancelEventArgs
    {
        /// <summary>
        /// Obtiene el objeto que se insertará en el <see cref="List{T}"/>.
        /// </summary>
        public T InsertedItem { get; }
        /// <summary>
        /// Obtiene el índice en el cual el objeto será insertado.
        /// </summary>
        public int Index { get; }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="InsertingItemEventArgs{T}"/>.
        /// </summary>
        /// <param name="index">
        /// Índice en el cual se insertará el objeto.
        /// </param>
        /// <param name="insertedItem">
        /// Objeto que se insertará en el <see cref="List{T}"/> que generó el
        /// evento.
        /// </param>
        internal InsertingItemEventArgs(int index, T insertedItem)
        {
            Index = index;
            InsertedItem = insertedItem;
        }
    }
    /// <summary>
    /// Contiene información para el evento <see cref="List{T}.ModifyingItem"/>.
    /// </summary>
    /// <typeparam name="T">Tipo de elementos de la lista.</typeparam>
    public class ModifyingItemEventArgs<T> : CancelEventArgs
    {
        /// <summary>
        /// Obtiene el nuevo valor del objeto.
        /// </summary>
        public T NewValue { get; }
        /// <summary>
        /// Obtiene el valor actual del objeto.
        /// </summary>
        public T OldValue { get; }
        /// <summary>
        /// Obtiene el índice del objeto dentro del <see cref="List{T}"/> que
        /// generó el evento.
        /// </summary>
        public int Index { get; }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="ModifyingItemEventArgs{T}"/>.
        /// </summary>
        /// <param name="index">
        /// Índice del objeto en el <see cref="List{T}"/> que generó el evento.
        /// </param>
        /// <param name="oldValue">Valor original del objeto.</param>
        /// <param name="newValue">Nuevo valor del objeto.</param>
        internal ModifyingItemEventArgs(int index, T oldValue, T newValue)
        {
            OldValue = oldValue;
            NewValue = newValue;
            Index = index;
        }
    }
    /// <summary>
    /// Contiene información para el evento <see cref="List{T}.RemovingItem"/>.
    /// </summary>
    /// <typeparam name="T">Tipo de elementos de la lista.</typeparam>
    public class RemovingItemEventArgs<T> : CancelEventArgs
    {
        /// <summary>
        /// Objeto que será removido del <see cref="List{T}"/> que generó el
        /// evento.
        /// </summary>
        public T RemovedItem { get; }
        /// <summary>
        /// Índice del elemento que será removido del <see cref="List{T}"/> que
        /// generó el evento.
        /// </summary>
        public int Index { get; }
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="RemovingItemEventArgs{T}"/>.
        /// </summary>
        /// <param name="index">
        /// Índice del elemento que será removido del <see cref="List{T}"/> que
        /// generó el evento.
        /// </param>
        /// <param name="removedItem">
        /// Objeto que será removido del <see cref="List{T}"/> que generó el
        /// evento.
        /// </param>
        internal RemovingItemEventArgs(int index, T removedItem)
        {
            RemovedItem = removedItem;
            Index = index;
        }
    }
    /// <summary>
    /// Contiene información para el evento <see cref="List{T}.ListUpdating"/>.
    /// </summary>
    /// <typeparam name="T">Tipo de elementos de la lista.</typeparam>
    public class ListUpdatingEventArgs<T> : CancelEventArgs
    {
        /// <summary>
        /// Elementos afectados por la actualización.
        /// </summary>
        public IReadOnlyCollection<T> AffectedItems { get; }
        /// <summary>
        /// TIpo de actualización a realizar en el <see cref="List{T}"/> que
        /// generó el evento.
        /// </summary>
        public ListUpdateType UpdateType { get; }
        internal ListUpdatingEventArgs(ListUpdateType updateType, IEnumerable<T> affectedItems)
        {
            UpdateType = updateType;
            AffectedItems = affectedItems?.ToList().AsReadOnly();
        }
    }
    /// <summary>
    /// Contiene información para el evento <see cref="List{T}.AddedItem"/>.
    /// </summary>
    /// <typeparam name="T">Tipo de elementos de la lista.</typeparam>
    public class AddedItemEventArgs<T> : EventArgs
    {
        /// <summary>
        /// Convierte implícitamente un <see cref="AddingItemEventArgs{T}"/> en
        /// un <see cref="AddedItemEventArgs{T}"/>.
        /// </summary>
        /// <param name="from">
        /// <see cref="AddingItemEventArgs{T}"/> a convertir.
        /// </param>
        public static implicit operator AddedItemEventArgs<T>(AddingItemEventArgs<T> from) => new AddedItemEventArgs<T>(from.NewItem);
        /// <summary>
        /// Elemento que fue agregado al <see cref="List{T}"/> que generó el
        /// evento.
        /// </summary>
        public T NewItem { get; }
        internal AddedItemEventArgs(T newItem)
        {
            NewItem = newItem;
        }
    }
    /// <summary>
    /// Contiene información para el evento <see cref="List{T}.InsertedItem"/>.
    /// </summary>
    /// <typeparam name="T">Tipo de elementos de la lista.</typeparam>
    public class InsertedItemEventArgs<T> : EventArgs
    {
        /// <summary>
        /// Convierte implícitamente un <see cref="InsertingItemEventArgs{T}"/>
        /// en un <see cref="InsertedItemEventArgs{T}"/>.
        /// </summary>
        /// <param name="from">
        /// <see cref="InsertingItemEventArgs{T}"/> a convertir.
        /// </param>
        public static implicit operator InsertedItemEventArgs<T>(InsertingItemEventArgs<T> from) => new InsertedItemEventArgs<T>(from.Index, from.InsertedItem);
        /// <summary>
        /// Elemento que fue insertado en el <see cref="List{T}"/> que generó
        /// el evento.
        /// </summary>
        public T InsertedItem { get; }
        /// <summary>
        /// Índice del objeto dentro del <see cref="List{T}"/> que generó el
        /// evento.
        /// </summary>
        public int Index { get; }
        internal InsertedItemEventArgs(int index, T insertedItem)
        {
            Index = index;
            InsertedItem = insertedItem;
        }
    }
    /// <summary>
    /// Contiene información para el evento <see cref="List{T}.ModifiedItem"/>.
    /// </summary>
    /// <typeparam name="T">Tipo de elementos de la lista.</typeparam>
    public class ItemModifiedEventArgs<T> : EventArgs
    {
        /// <summary>
        /// convierte explícitamente un <see cref="ModifyingItemEventArgs{T}"/>
        /// en un <see cref="ItemModifiedEventArgs{T}"/>.
        /// </summary>
        /// <param name="from">
        /// <see cref="ModifyingItemEventArgs{T}"/> a convertir.
        /// </param>
        public static explicit operator ItemModifiedEventArgs<T>(ModifyingItemEventArgs<T> from) => new ItemModifiedEventArgs<T>(from.Index, from.NewValue);
        /// <summary>
        /// Objeto que ha sido modificado dentro del <see cref="List{T}"/> que
        /// generó el evento.
        /// </summary>
        public T Item { get; }
        /// <summary>
        /// Índice del objeto modificado dentro del <see cref="List{T}"/> que
        /// generó el evento.
        /// </summary>
        public int Index { get; }
        internal ItemModifiedEventArgs(int index, T item)
        {
            Item = item;
            Index = index;
        }
    }
    /// <summary>
    /// Contiene información para el evento <see cref="List{T}.RemovedItem"/>.
    /// </summary>
    /// <typeparam name="T">Tipo de elementos de la lista.</typeparam>
    public class RemovedItemEventArgs<T> : EventArgs
    {
        /// <summary>
        /// convierte implícitamente un <see cref="RemovingItemEventArgs{T}"/>
        /// en un <see cref="RemovedItemEventArgs{T}"/>.
        /// </summary>
        /// <param name="from">
        /// <see cref="RemovingItemEventArgs{T}"/> a convertir.
        /// </param>
        public static implicit operator RemovedItemEventArgs<T>(RemovingItemEventArgs<T> from)=> new RemovedItemEventArgs<T>(from.RemovedItem);
        /// <summary>
        /// Objeto que fue quitado del <see cref="List{T}"/> que generó el
        /// evento.
        /// </summary>
        public T RemovedItem { get; }
        internal RemovedItemEventArgs(T removedItem)
        {
            RemovedItem = removedItem;
        }
    }
    /// <summary>
    /// Contiene información para el evento <see cref="List{T}.ListUpdated"/>.
    /// </summary>
    /// <typeparam name="T">Tipo de elementos de la lista.</typeparam>
    public class ListUpdatedEventArgs<T> : EventArgs
    {
        /// <summary>
        /// Convierte implícitamente un <see cref="ListUpdatingEventArgs{T}"/>
        /// en un <see cref="ListUpdatedEventArgs{T}"/>
        /// </summary>
        /// <param name="from">
        /// <see cref="ListUpdatingEventArgs{T}"/> a convertir.
        /// </param>
        public static implicit operator ListUpdatedEventArgs<T>(ListUpdatingEventArgs<T> from)=> new ListUpdatedEventArgs<T>(from.UpdateType,from.AffectedItems);
        /// <summary>
        /// Elementos que fueron afectados por la actualización del 
        /// <see cref="List{T}"/> que generó el evento.
        /// </summary>
        public IReadOnlyCollection<T> AffectedItems { get; }
        /// <summary>
        /// Tipo de actualización ocurrida en el <see cref="List{T}"/> que
        /// generó el evento.
        /// </summary>
        public readonly ListUpdateType UpdateType;
        internal ListUpdatedEventArgs(ListUpdateType updateType, IEnumerable<T> affectedItems)
        {
            UpdateType = updateType;
            AffectedItems = affectedItems?.ToList().AsReadOnly();
        }
    }

    public partial class List<T>
    {
        /// <summary>
        /// Se produce cuando se agregará un elemento a la lista.
        /// </summary>
        public event EventHandler<AddingItemEventArgs<T>> AddingItem;
        /// <summary>
        /// Se produce cuando se insertará un elemento en la lista.
        /// </summary>
        public event EventHandler<InsertingItemEventArgs<T>> InsertingItem;
        /// <summary>
        /// Se produce cuando se modificará un elemento de la lista
        /// </summary>
        public event EventHandler<ModifyingItemEventArgs<T>> ModifyingItem;
        /// <summary>
        /// Se produce cuando se eliminará un elemento de la lista.
        /// </summary>
        public event EventHandler<RemovingItemEventArgs<T>> RemovingItem;
        /// <summary>
        /// Se produce cuando la lista será actualizada.
        /// </summary>
        public event EventHandler<ListUpdatingEventArgs<T>> ListUpdating;
        /// <summary>
        /// Se produce cuando se ha agregado un elemento a la lista.
        /// </summary>
        public event EventHandler<AddedItemEventArgs<T>> AddedItem;
        /// <summary>
        /// Se produce cuando se ha insertado un elemento en la lista.
        /// </summary>
        public event EventHandler<InsertedItemEventArgs<T>> InsertedItem;
        /// <summary>
        /// Se produce cuando se ha modificado un elemento de la lista.
        /// </summary>
        public event EventHandler<ItemModifiedEventArgs<T>> ModifiedItem;
        /// <summary>
        /// Se produce cuando se ha quitado un elemento de la lista.
        /// </summary>
        public event EventHandler<RemovedItemEventArgs<T>> RemovedItem;
        /// <summary>
        /// Se produce cuando la lista será vaciada por medio de 
        /// <see cref="Clear"/>.
        /// </summary>
        public event EventHandler<CancelEventArgs> ListClearing;
        /// <summary>
        /// Se produce cuando la lista ha sido vaciada por medio de 
        /// <see cref="Clear"/>.
        /// </summary>
        public event EventHandler ListCleared;
        /// <summary>
        /// Se produce cuando la lista ha sido actualizada.
        /// </summary>
        public event EventHandler<ListUpdatedEventArgs<T>> ListUpdated;
    }
}