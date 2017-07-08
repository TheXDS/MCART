//
//  Events.cs
//
//  This file is part of MCART
//
//  Author:
//       César Andrés Morgan <xds_xps_ivx@hotmail.com>
//
//  Copyright (c) 2011 - 2017 César Andrés Morgan
//
//  MCART is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  MCART is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
namespace MCART.Types.Extensions
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
        public readonly T NewItem;
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
        public readonly T InsertedItem;
        /// <summary>
        /// Obtiene el índice en el cual el objeto será insertado.
        /// </summary>
        public readonly int Index;
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="InsertingItemEventArgs{T}"/>.
        /// </summary>
        /// <param name="Index">
        /// Índice en el cual se insertará el objeto.
        /// </param>
        /// <param name="InsertedItem">
        /// Objeto que se insertará en el <see cref="List{T}"/> que generó el
        /// evento.
        /// </param>
        internal InsertingItemEventArgs(int Index, T InsertedItem)
        {
            this.Index = Index;
            this.InsertedItem = InsertedItem;
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
        public readonly T NewValue;
        /// <summary>
        /// Obtiene el valor actual del objeto.
        /// </summary>
        public readonly T OldValue;
        /// <summary>
        /// Obtiene el índice del objeto dentro del <see cref="List{T}"/> que
        /// generó el evento.
        /// </summary>
        public readonly int Index;
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="ModifyingItemEventArgs{T}"/>.
        /// </summary>
        /// <param name="index">
        /// Índice del objeto en el <see cref="List{T}"/> que generó el evento.
        /// </param>
        /// <param name="oldv">Valor original del objeto.</param>
        /// <param name="newv">Nuevo valor del objeto.</param>
        internal ModifyingItemEventArgs(int index, T oldv, T newv)
        {
            OldValue = oldv;
            NewValue = newv;
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
        public readonly T RemovedItem;
        /// <summary>
        /// Índice del elemento que será removido del <see cref="List{T}"/> que
        /// generó el evento.
        /// </summary>
        public readonly int Index;
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="RemovingItemEventArgs{T}"/>.
        /// </summary>
        /// <param name="RemItm">
        /// Objeto que será removido del <see cref="List{T}"/> que generó el
        /// evento.
        /// </param>
        /// <param name="Index">
        /// Índice del elemento que será removido del <see cref="List{T}"/> que
        /// generó el evento.
        /// </param>
        internal RemovingItemEventArgs(T RemItm, int Index)
        {
            RemovedItem = RemItm;
            this.Index = Index;
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
        public readonly IReadOnlyCollection<T> Items;
        /// <summary>
        /// TIpo de actualización a realizar en el <see cref="List{T}"/> que
        /// generó el evento.
        /// </summary>
        public readonly ListUpdateType UpdateType;
        internal ListUpdatingEventArgs(ListUpdateType ut, IEnumerable<T> itms)
        {
            UpdateType = ut;
            Items = itms?.ToList().AsReadOnly();
        }
    }
    /// <summary>
    /// Contiene información para el evento <see cref="List{T}.AddedItem"/>.
    /// </summary>
    /// <typeparam name="T">Tipo de elementos de la lista.</typeparam>
    public class AddedItemEventArgs<T> : EventArgs
    {
        /// <summary>
        /// Elemento que fue agregado al <see cref="List{T}"/> que generó el
        /// evento.
        /// </summary>
        public readonly T NewItem;
        internal AddedItemEventArgs(T NewItem)
        {
            this.NewItem = NewItem;
        }
    }
    /// <summary>
    /// Contiene información para el evento <see cref="List{T}.InsertedItem"/>.
    /// </summary>
    /// <typeparam name="T">Tipo de elementos de la lista.</typeparam>
    public class InsertedItemEventArgs<T> : EventArgs
    {
        /// <summary>
        /// Elemento que fue insertado en el <see cref="List{T}"/> que generó
        /// el evento.
        /// </summary>
        public readonly T InsertedItem;
        /// <summary>
        /// Índice del objeto dentro del <see cref="List{T}"/> que generó el
        /// evento.
        /// </summary>
        public readonly int Index;
        internal InsertedItemEventArgs(int Index, T InsertedItem)
        {
            this.Index = Index;
            this.InsertedItem = InsertedItem;
        }
    }
    /// <summary>
    /// Contiene información para el evento <see cref="List{T}.ModifiedItem"/>.
    /// </summary>
    /// <typeparam name="T">Tipo de elementos de la lista.</typeparam>
    public class ItemModifiedEventArgs<T> : EventArgs
    {
        /// <summary>
        /// Objeto que ha sido modificado dentro del <see cref="List{T}"/> que
        /// generó el evento.
        /// </summary>
        public readonly T Item;
        /// <summary>
        /// Índice del objeto modificado dentro del <see cref="List{T}"/> que
        /// generó el evento.
        /// </summary>
        public readonly int Index;
        internal ItemModifiedEventArgs(T Itm, int Index)
        {
            Item = Itm;
            this.Index = Index;
        }
    }
    /// <summary>
    /// Contiene información para el evento <see cref="List{T}.RemovedItem"/>.
    /// </summary>
    /// <typeparam name="T">Tipo de elementos de la lista.</typeparam>
    public class RemovedItemEventArgs<T> : EventArgs
    {
        /// <summary>
        /// Objeto que fue quitado del <see cref="List{T}"/> que generó el
        /// evento.
        /// </summary>
        public readonly T RemovedItem;
        internal RemovedItemEventArgs(T RemItm)
        {
            RemovedItem = RemItm;
        }
    }
    /// <summary>
    /// Contiene información para el evento <see cref="List{T}.ListUpdated"/>.
    /// </summary>
    /// <typeparam name="T">Tipo de elementos de la lista.</typeparam>
    public class ListUpdatedEventArgs<T> : EventArgs
    {
        /// <summary>
        /// Elementos que fueron afectados por la actualización del 
        /// <see cref="List{T}"/> que generó el evento.
        /// </summary>
        public readonly IReadOnlyCollection<T> Items;
        /// <summary>
        /// Tipo de actualización ocurrida en el <see cref="List{T}"/> que
        /// generó el evento.
        /// </summary>
        public readonly ListUpdateType UpdateType;
        internal ListUpdatedEventArgs(ListUpdateType ut, IEnumerable<T> itms)
        {
            UpdateType = ut;
            Items = itms?.ToList().AsReadOnly();
        }
    }
    /// <summary>
    /// Se genera cuando se agregará un elemento a la lista.
    /// </summary>
    /// <typeparam name="T">Tipo de elemento.</typeparam>
    /// <param name="sender">Objeto que ha generado el evento.</param>
    /// <param name="e">Argumentos del evento.</param>
    public delegate void AddingItemEventHandler<T>(List<T> sender, AddingItemEventArgs<T> e);
    /// <summary>
    /// Se genera cuando se insertará un elemento en la lista.
    /// </summary>
    /// <typeparam name="T">Tipo de elemento.</typeparam>
    /// <param name="sender">Objeto que ha generado el evento.</param>
    /// <param name="e">Argumentos del evento.</param>
    public delegate void InsertingItemEventHandler<T>(List<T> sender, InsertingItemEventArgs<T> e);
    /// <summary>
    /// Se genera cuando se eliminará un elemento de la lista
    /// </summary>
    /// <typeparam name="T">Tipo de elemento.</typeparam>
    /// <param name="sender">Objeto que ha generado el evento.</param>
    /// <param name="e">Argumentos del evento.</param>
    public delegate void RemovingItemEventHandler<T>(List<T> sender, RemovingItemEventArgs<T> e);
    /// <summary>
    /// Se genera cuando se modificará un elemento de la lista
    /// </summary>
    /// <typeparam name="T">Tipo de elemento.</typeparam>
    /// <param name="sender">Objeto que ha generado el evento.</param>
    /// <param name="e">Argumentos del evento.</param>
    public delegate void ModifyingItemEventHandler<T>(List<T> sender, ModifyingItemEventArgs<T> e);
    /// <summary>
    /// Se genera cuando la lista será vaciada por <see cref="List{T}.Clear"/>
    /// </summary>
    /// <typeparam name="T">Tipo de elementos de la lista.</typeparam>
    /// <param name="sender">Objeto que ha generado el evento.</param>
    /// <param name="e">Argumentos del evento.</param>
    public delegate void ClearingListEventHandler<T>(List<T> sender, ListUpdatingEventArgs<T> e);
    /// <summary>
    /// Se genera cuando se ha agregado un elemento a la lista
    /// </summary>
    /// <typeparam name="T">Tipo de elemento.</typeparam>
    /// <param name="sender">Objeto que ha generado el evento.</param>
    /// <param name="e">Argumentos del evento.</param>
    public delegate void ItemAddedEventHandler<T>(List<T> sender, AddedItemEventArgs<T> e);
    /// <summary>
    /// Se genera cuando se ha insertado un elemento en la lista.
    /// </summary>
    /// <typeparam name="T">Tipo de elemento.</typeparam>
    /// <param name="sender">Objeto que ha generado el evento.</param>
    /// <param name="e">Argumentos del evento.</param>
    public delegate void ItemInsertedEventHandler<T>(List<T> sender, InsertedItemEventArgs<T> e);
    /// <summary>
    /// Se genera cuando se ha modificado un elemento de la lista
    /// </summary>
    /// <typeparam name="T">Tipo de elemento.</typeparam>
    /// <param name="sender">Objeto que ha generado el evento.</param>
    /// <param name="e">Argumentos del evento.</param>
    public delegate void ItemModifiedEventHandler<T>(List<T> sender, ItemModifiedEventArgs<T> e);
    /// <summary>
    /// Se genera cuando se ha quitado un elemento de la lista
    /// </summary>
    /// <typeparam name="T">Tipo de elemento.</typeparam>
    /// <param name="sender">Objeto que ha generado el evento.</param>
    /// <param name="e">Argumentos del evento.</param>
    public delegate void ItemRemovedEventHandler<T>(List<T> sender, RemovedItemEventArgs<T> e);
    /// <summary>
    /// Se genera cuando la lista ha sido vaciada por medio de <see cref="List{T}.Clear()"/>
    /// </summary>
    /// <typeparam name="T">Tipo de elemento.</typeparam>
    /// <param name="sender">Objeto que ha generado el evento.</param>
    /// <param name="e">Argumentos del evento.</param>
    public delegate void ListClearedEventHandler<T>(List<T> sender, EventArgs e);
    /// <summary>
    /// Se genera cuando la lista ha sido actualizada
    /// </summary>
    /// <typeparam name="T">Tipo de elemento.</typeparam>
    /// <param name="sender">Objeto que ha generado el evento.</param>
    /// <param name="e">Argumentos del evento.</param>
    public delegate void ListUpdatedEventHandler<T>(List<T> sender, ListUpdatedEventArgs<T> e);
    /// <summary>
    /// Se genera cuando la lista será actualizada
    /// </summary>
    /// <typeparam name="T">Tipo de elemento.</typeparam>
    /// <param name="sender">Objeto que ha generado el evento.</param>
    /// <param name="e">Argumentos del evento.</param>
    public delegate void ListUpdatingEventHandler<T>(List<T> sender, ListUpdatingEventArgs<T> e);
}
