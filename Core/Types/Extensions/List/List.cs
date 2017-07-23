//
//  List.cs
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
using System.Linq;
using MCART.Exceptions;
namespace MCART.Types.Extensions
{
    /// <summary>
    /// Extensión de la clase <see cref="List{T}"/>. Provee de toda la
    /// funcionalidad previamente disponible, e incluye algunas extensiones
    /// útiles.
    /// </summary>
    /// <typeparam name="T">
    /// Tipo de los elementos que contendrá esta lista.
    /// </typeparam> 
    public partial class List<T> : System.Collections.Generic.List<T>, ICloneable
    {
        /// <summary>
        /// Activa o desactiva la generación global de eventos de todos los
        /// <see cref="List{T}"/>.
        /// </summary>
        public static bool GlobalTriggerEvents;
        /// <summary>
        /// Activa o desactiva la generación de eventos.
        /// </summary>
        public bool TriggerEvents;
        /// <summary>
        /// Obtiene o establece el elemento ubicado en el íncide especificado.
        /// </summary>
        /// <param name="index">Índice del elemento.</param>
        /// <returns>El elemento en la posición especificada.</returns>
        public new T this[int index]
        {
            get
            {
                try { return base[index]; }
                catch { throw; }
            }
            set
            {
                try
                {
                    ModifyingItemEventArgs<T> a = new ModifyingItemEventArgs<T>(index, base[index], value);
                    ModifyingItem?.Invoke(this, a);
                    if (!a.Cancel)
                    {
                        base[index] = value;
                        ModifiedItem?.Invoke(this, new ItemModifiedEventArgs<T>(base[index], index));
                    }
                }
                catch { throw; }
            }
        }
        /// <summary>
        /// Añade un objeto al final de la <see cref="List{T}"/>.
        /// </summary>
        /// <param name="item">
        /// El objeto a ser añadido al final de la <see cref="List{T}"/>. El
        /// valor puede ser <c>null</c> para tipos de referencia.
        /// </param>
        public new void Add(T item)
        {
            if (TriggerEvents && GlobalTriggerEvents)
            {
                AddingItemEventArgs<T> a = new AddingItemEventArgs<T>(item);
                AddingItem?.Invoke(this, a);
                if (!a.Cancel)
                {
                    base.Add(item);
                    AddedItem?.Invoke(this, new AddedItemEventArgs<T>(item));
                }
            }
            else { base.Add(item); }
        }
        /// <summary>
        /// Agrega todos los elementos de una colección al final de la 
        /// <see cref="List{T}"/>.
        /// </summary>
        /// <param name="collection">Colección a añadir.</param>
        public new void AddRange(IEnumerable<T> collection)
        {
            if (TriggerEvents && GlobalTriggerEvents)
            {
                ListUpdatingEventArgs<T> a = new ListUpdatingEventArgs<T>(ListUpdateType.ItemsAdded, collection);
                ListUpdating?.Invoke(this, a);
                if (!a.Cancel)
                {
                    base.AddRange(collection);
                    ListUpdated?.Invoke(this, new ListUpdatedEventArgs<T>(ListUpdateType.ItemsAdded, collection));
                }
            }
            else { base.AddRange(collection); }

        }
        /// <summary>
        /// Inserta un elemento en el índice especificado
        /// </summary>
        /// <param name="index">Índice de destino del nuevo elemento</param>
        /// <param name="item">Elemento a insertar</param>
        public new void Insert(int index, T item)
        {
            if (TriggerEvents && GlobalTriggerEvents)
            {
                InsertingItemEventArgs<T> a = new InsertingItemEventArgs<T>(index, item);
                InsertingItem?.Invoke(this, a);
                if (!a.Cancel)
                {
                    base.Insert(index, item);
                    InsertedItem?.Invoke(this, new InsertedItemEventArgs<T>(index, item));
                }
            }
            else { base.Insert(index, item); }
        }
        /// <summary>
        /// Inserta una colección de elementos a partir del índice especificado
        /// </summary>
        /// <param name="index">Índice de destino para la inserción</param>
        /// <param name="collection">Colección de elementos a insertar</param>
        public new void InsertRange(int index, IEnumerable<T> collection)
        {
            if (TriggerEvents && GlobalTriggerEvents)
            {
                ListUpdatingEventArgs<T> a = new ListUpdatingEventArgs<T>(ListUpdateType.ItemsInserted, collection);
                ListUpdating?.Invoke(this, a);
                if (!a.Cancel)
                {
                    base.InsertRange(index, collection);
                    ListUpdated?.Invoke(this, new ListUpdatedEventArgs<T>(ListUpdateType.ItemsInserted, collection));
                }
            }
            else { base.InsertRange(index, collection); }
        }
        /// <summary>
        /// Quita la primera aparición de un objeto específico del 
        /// <see cref="List{T}"/>.
        /// </summary>
        /// <param name="item">
        /// Objeto de tipo <typeparamref name="T"/> a remover de la colección.
        /// </param>
        /// <exception cref="EmptyCollectionException{T}">
        /// se produce si esta lista está vacía.
        /// </exception>
        public new void Remove(T item)
        {
            if (!this.Any()) throw new EmptyCollectionException<T>(this);
            if (TriggerEvents && GlobalTriggerEvents)
            {
                var a = new RemovingItemEventArgs<T>(this.Last(), IndexOf(item));
                RemovingItem?.Invoke(this, a);
                if (!a.Cancel)
                {
                    base.Remove(item);
                    RemovedItem?.Invoke(this, new RemovedItemEventArgs<T>(item));
                }
            }
            else { base.Remove(item); }

        }
        /// <summary>
        /// Quita el elemento situado en el índice especificado del <see cref="List{T}"/>
        /// </summary>
        /// <param name="index">Índice del elemento a remover</param>
        public new void RemoveAt(int index)
        {
            if (!this.Any()) throw new EmptyCollectionException<T>(this);
            if (TriggerEvents && GlobalTriggerEvents)
            {
                var a = new RemovingItemEventArgs<T>(this[index], index);
                RemovingItem?.Invoke(this, a);
                if (!a.Cancel)
                {
                    T x = this.ElementAt(index);
                    base.RemoveAt(index);
                    RemovedItem?.Invoke(this, new RemovedItemEventArgs<T>(x));
                }
            }
            else { base.RemoveAt(index); }
        }
        /// <summary>
        /// Quita todos los elementos que cumplen con las condiciones definidas
        /// por el predicado especificado.
        /// </summary>
        /// <param name="match">
        /// Delegado <see cref="Predicate{T}"/> que define las condiciones de
        /// los elementos que se van a quitar.
        /// </param>
        /// <exception cref="EmptyCollectionException{T}">
        /// se produce si esta lista está vacía.
        /// </exception>
        public new void RemoveAll(Predicate<T> match)
        {
            if (!this.Any()) throw new EmptyCollectionException<T>(this);
            if (TriggerEvents && GlobalTriggerEvents)
            {
                System.Collections.Generic.List<T> tmp = this.Where((c) => match(c)).ToList();
                var a = new ListUpdatingEventArgs<T>(ListUpdateType.ItemsRemoved, tmp);
                ListUpdating?.Invoke(this, a);
                if (!a.Cancel)
                {
                    foreach (T itm in tmp) base.Remove(itm);
                    ListUpdated?.Invoke(this, new ListUpdatedEventArgs<T>(ListUpdateType.ItemsRemoved, tmp));
                    tmp?.Clear();
                    tmp = null;
                }
            }
            else { base.RemoveAll(match); }
        }
        /// <summary>
        /// Invierte el orden de los elementos en este <see cref="List{T}"/> completo.
        /// </summary>
        public new void Reverse()
        {
            if (TriggerEvents && GlobalTriggerEvents)
            {
                ListUpdatingEventArgs<T> a = new ListUpdatingEventArgs<T>(ListUpdateType.ItemsMoved, Reversed());
                ListUpdating?.Invoke(this, a);
                if (!a.Cancel)
                {
                    base.Reverse();
                    ListUpdated?.Invoke(this, new ListUpdatedEventArgs<T>(ListUpdateType.ItemsInserted, this));
                }
            }
            else { base.Reverse(); }
        }
        /// <summary>
        /// Invierte el orden de los elementos en el intervalo especificado.
        /// </summary>
        /// <param name="index">
        /// Índice inicial de base cero del intervalo que se va a invertir.
        /// </param>
        /// <param name="count">
        /// Número de elementos del intervalo que se va a invertir.
        /// </param>
        public new void Reverse(int index, int count)
        {
            if (TriggerEvents && GlobalTriggerEvents)
            {
                ListUpdatingEventArgs<T> a = new ListUpdatingEventArgs<T>(ListUpdateType.ItemsMoved, Reversed(index, count));
                ListUpdating?.Invoke(this, a);
                if (!a.Cancel)
                {
                    base.Reverse(index, count);
                    ListUpdated?.Invoke(this, new ListUpdatedEventArgs<T>(ListUpdateType.ItemsInserted, this));
                }
            }
            else { base.Reverse(index, count); }
        }
        /// <summary>
        /// Devuelve una versión inversa del orden de los elementos de este 
        /// <see cref="List{T}"/> sin alterar la colección original.
        /// </summary>
        /// <returns>
        /// Una versión inversa del orden de los elementos de este 
        /// <see cref="List{T}"/>.
        /// </returns>
        public List<T> Reversed()
        {
            List<T> tmp = (List<T>)this.Copy();
            tmp.Reverse();
            return tmp;
        }
        /// <summary>
        /// Devuelve una versión inversa del orden de los elementos de este 
        /// <see cref="List{T}"/> sin alterar la colección original.
        /// </summary>
        /// <param name="index">
        /// Índice inicial de base cero del intervalo que se va a invertir.
        /// </param>
        /// <param name="count">
        /// Número de elementos del intervalo que se va a invertir.
        /// </param>
        /// <returns>
        /// Una versión inversa del orden de los elementos de este 
        /// <see cref="List{T}"/>.
        /// </returns>
        public List<T> Reversed(int index, int count)
        {
            List<T> tmp = (List<T>)this.Copy();
            tmp.Reverse(index, count);
            return tmp;
        }
        /// <summary>
        /// Quita el último elemento de la <see cref="List{T}"/>
        /// </summary>
        [Attributes.Thunk]
        public void RemoveLast()
        {
            try { Remove(this.Last()); }
            catch { throw; }
        }
        /// <summary>
        /// Quita el primer elemento de la <see cref="List{T}"/>
        /// </summary>
        [Attributes.Thunk]
        public void RemoveFirst()
        {
            try { Remove(this.First()); }
            catch { throw; }
        }
        /// <summary>
        /// Remueve todos los elementos de la <see cref="List{T}"/>
        /// </summary>
        public new void Clear()
        {
            if (TriggerEvents && GlobalTriggerEvents)
            {
                ListUpdatingEventArgs<T> a = new ListUpdatingEventArgs<T>(ListUpdateType.ListCleared, this);
                ClearingList?.Invoke(this, a);
                if (!a.Cancel)
                {
                    base.Clear();
                    ListCleared?.Invoke(this, EventArgs.Empty);
                }
            }
            else { base.Clear(); }
        }
        /// <summary>
        /// Ordena los elementos de todo el <see cref="List{T}"/> utilizando el
        /// comparador predeterminado.
        /// </summary>
        public new void Sort()
        {
            if (TriggerEvents && GlobalTriggerEvents)
            {
                ListUpdatingEventArgs<T> a = new ListUpdatingEventArgs<T>(ListUpdateType.ItemsMoved, this);
                ListUpdating?.Invoke(this, a);
                if (!a.Cancel)
                {
                    base.Sort();
                    ListUpdated?.Invoke(this, new ListUpdatedEventArgs<T>(ListUpdateType.ItemsMoved, this));
                }
            }
            else { base.Sort(); }
        }
        /// <summary>
        /// Ordena los elementos de todo el <see cref="List{T}"/> utilizando el
        /// <see cref="Comparison{T}"/> especificado.
        /// </summary>
        /// <param name="comparsion">
        /// <see cref="Comparison{T}"/> que se va a utilizar al comparar 
        /// elementos.
        /// </param>
        public new void Sort(Comparison<T> comparsion)
        {
            if (TriggerEvents && GlobalTriggerEvents)
            {
                ListUpdatingEventArgs<T> a = new ListUpdatingEventArgs<T>(ListUpdateType.ItemsMoved, this);
                ListUpdating?.Invoke(this, a);
                if (!a.Cancel)
                {
                    base.Sort(comparsion);
                    ListUpdated?.Invoke(this, new ListUpdatedEventArgs<T>(ListUpdateType.ItemsMoved, this));
                }
            }
            else { base.Sort(comparsion); }
        }
        /// <summary>
        /// Ordena los elementos de todo el <see cref="List{T}"/> utilizando el
        /// <see cref="IComparer{T}"/> especificado.
        /// </summary>
        /// <param name="comparer">
        /// Implementación de <see cref="IComparer{T}"/> que se va a utilizar
        /// al comparar elementos, o <c>null</c> para utilizar el comparador
        /// predeterminado <see cref="Comparer{T}.Default"/>.
        /// </param>
        public new void Sort(IComparer<T> comparer)
        {
            if (TriggerEvents && GlobalTriggerEvents)
            {
                ListUpdatingEventArgs<T> a = new ListUpdatingEventArgs<T>(ListUpdateType.ItemsMoved, this);
                ListUpdating?.Invoke(this, a);
                if (!a.Cancel)
                {
                    base.Sort(comparer);
                    ListUpdated?.Invoke(this, new ListUpdatedEventArgs<T>(ListUpdateType.ItemsMoved, this));
                }
            }
            else { base.Sort(comparer); }
        }
        /// <summary>
        /// Ordena los elementos en un intevalo especificado del 
        /// <see cref="List{T}"/> utilizando el comparador especificado.
        /// </summary>
        /// <param name="index">
        /// Índice inicial de base cero del intervalo que se va a ordenar.
        /// </param>
        /// <param name="count">
        /// Longitud del intervalo que se va a ordernar.
        /// </param>
        /// <param name="comparer">
        /// Implementación de <see cref="IComparer{T}"/> que se va a utilizar
        /// al comparar elementos, o <c>null</c> para utilizar el comparador
        /// predeterminado <see cref="Comparer{T}.Default"/>.
        /// </param>
        public new void Sort(int index, int count, IComparer<T> comparer)
        {
            if (TriggerEvents && GlobalTriggerEvents)
            {
                ListUpdatingEventArgs<T> a = new ListUpdatingEventArgs<T>(ListUpdateType.ItemsMoved, this);
                ListUpdating?.Invoke(this, a);
                if (!a.Cancel)
                {
                    base.Sort(index, count, comparer);
                    ListUpdated?.Invoke(this, new ListUpdatedEventArgs<T>(ListUpdateType.ItemsMoved, this));
                }
            }
            else { base.Sort(index, count, comparer); }
        }
        /// <summary>
        /// Implementa la interfaz <see cref="ICloneable"/>
        /// </summary>
        /// <returns>Una copia de esta instancia.</returns>
        [Attributes.Thunk] public object Clone() => this.Copy();
        /// <summary>
        /// Devuelve el tipo de elementos de <see cref="List{T}"/>
        /// </summary>
        /// <returns></returns>
        public Type GetListType
        {
            get { return typeof(T); }
        }
    }
}