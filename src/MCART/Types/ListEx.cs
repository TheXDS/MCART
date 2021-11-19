/*
ListEx.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2021 César Andrés Morgan

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
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Events;
using TheXDS.MCART.Exceptions;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.Types
{
    /// <summary>
    /// Extensión observable de la clase 
    /// <see cref="List{T}" />. Provee de toda la
    /// funcionalidad previamente disponible, e incluye algunas extensiones
    /// útiles.
    /// </summary>
    /// <typeparam name="T">
    /// Tipo de los elementos que contendrá esta lista.
    /// </typeparam>
    /// <remarks>
    /// Esta clase puede considerarse como una alternativa más completa a 
    /// <see cref="ObservableCollection{T}" /> con numerosos eventos
    /// adicionales y otras extensiones.
    /// </remarks>
    public class ListEx<T> : List<T>, ICloneable
    {
        /// <summary>
        /// Se produce cuando se agregará un elemento a la lista.
        /// </summary>
        public event EventHandler<AddingItemEventArgs<T>>? AddingItem;

        /// <summary>
        /// Se produce cuando se insertará un elemento en la lista.
        /// </summary>
        public event EventHandler<InsertingItemEventArgs<T>>? InsertingItem;

        /// <summary>
        /// Se produce cuando se modificará un elemento de la lista
        /// </summary>
        public event EventHandler<ModifyingItemEventArgs<T>>? ModifyingItem;

        /// <summary>
        /// Se produce cuando se eliminará un elemento de la lista.
        /// </summary>
        public event EventHandler<RemovingItemEventArgs<T>>? RemovingItem;

        /// <summary>
        /// Se produce cuando la lista será actualizada.
        /// </summary>
        public event EventHandler<ListUpdatingEventArgs<T>>? ListUpdating;

        /// <summary>
        /// Se produce cuando se ha agregado un elemento a la lista.
        /// </summary>
        public event EventHandler<AddedItemEventArgs<T>>? AddedItem;

        /// <summary>
        /// Se produce cuando se ha insertado un elemento en la lista.
        /// </summary>
        public event EventHandler<InsertedItemEventArgs<T>>? InsertedItem;

        /// <summary>
        /// Se produce cuando se ha modificado un elemento de la lista.
        /// </summary>
        public event EventHandler<ItemModifiedEventArgs<T>>? ModifiedItem;

        /// <summary>
        /// Se produce cuando se ha quitado un elemento de la lista.
        /// </summary>
        public event EventHandler<RemovedItemEventArgs<T>>? RemovedItem;

        /// <summary>
        /// Se produce cuando la lista será vaciada por medio de 
        /// <see cref="Clear"/>.
        /// </summary>
        public event EventHandler<CancelEventArgs>? ListClearing;

        /// <summary>
        /// Se produce cuando la lista ha sido vaciada por medio de 
        /// <see cref="Clear"/>.
        /// </summary>
        public event EventHandler? ListCleared;

        /// <summary>
        /// Se produce cuando la lista ha sido actualizada.
        /// </summary>
        public event EventHandler<ListUpdatedEventArgs<T>>? ListUpdated;

        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="ListEx{T}" />.
        /// </summary>
        public ListEx() { }

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="ListEx{T}" />.
        /// </summary>
        /// <param name="collection">
        /// Colección incial de este <see cref="ListEx{T}" />.
        /// </param>
        public ListEx(IEnumerable<T> collection) : base(collection) { }

        /// <summary>
        /// Activa o desactiva la generación de eventos.
        /// </summary>
        public bool TriggerEvents { get; set; } = true;

        /// <summary>
        /// Obtiene o establece el elemento ubicado en el íncide
        /// especificado.
        /// </summary>
        /// <param name="index">Índice del elemento.</param>
        /// <returns>El elemento en la posición especificada.</returns>
        public new T this[int index]
        {
            get => base[index];
            set
            {
                if (TriggerEvents)
                {
                    var a = new ModifyingItemEventArgs<T>(index, base[index], value);
                    ModifyingItem?.Invoke(this, a);
                    if (!a.Cancel)
                    {
                        base[index] = value;
                        ModifiedItem?.Invoke(this, (ItemModifiedEventArgs<T>)a);
                    }
                }
                else base[index] = value;
            }
        }

        /// <summary>
        /// Añade un objeto al final de la <see cref="ListEx{T}"/>.
        /// </summary>
        /// <param name="item">
        /// El objeto a ser añadido al final de la <see cref="ListEx{T}"/>.
        /// El valor puede ser <see langword="null"/> para tipos de
        /// referencia.
        /// </param>
        public new void Add(T item)
        {
            if (TriggerEvents)
            {
                var a = new AddingItemEventArgs<T>(item);
                AddingItem?.Invoke(this, a);
                if (!a.Cancel)
                {
                    base.Add(item);
                    AddedItem?.Invoke(this, a);
                }
            }
            else base.Add(item);
        }

        /// <summary>
        /// Agrega todos los elementos de una colección al final de la 
        /// <see cref="ListEx{T}"/>.
        /// </summary>
        /// <param name="collection">Colección a añadir.</param>
        public new void AddRange(IEnumerable<T> collection)
        {
            if (TriggerEvents)
            {
                var affectedItems = collection.ToList();
                var a = new ListUpdatingEventArgs<T>(ListUpdateType.ItemsAdded, affectedItems);
                ListUpdating?.Invoke(this, a);
                if (!a.Cancel)
                {
                    base.AddRange(affectedItems);
                    ListUpdated?.Invoke(this, a);
                }
            }
            else base.AddRange(collection);
        }

        /// <summary>
        /// Inserta un elemento en el índice especificado.
        /// </summary>
        /// <param name="index">Índice de destino del nuevo elemento.</param>
        /// <param name="item">Elemento a insertar.</param>
        public new void Insert(int index, T item)
        {
            if (TriggerEvents)
            {
                var a = new InsertingItemEventArgs<T>(index, item);
                InsertingItem?.Invoke(this, a);
                if (!a.Cancel)
                {
                    base.Insert(index, item);
                    InsertedItem?.Invoke(this, a);
                }
            }
            else base.Insert(index, item);
        }

        /// <summary>
        /// Inserta una colección de elementos a partir del índice
        /// especificado.
        /// </summary>
        /// <param name="index">Índice de destino para la inserción.</param>
        /// <param name="collection">Colección de elementos a insertar.</param>
        public new void InsertRange(int index, IEnumerable<T> collection)
        {
            if (TriggerEvents)
            {
                var affectedItems = collection.ToList();
                var a = new ListUpdatingEventArgs<T>(ListUpdateType.ItemsInserted, affectedItems);
                ListUpdating?.Invoke(this, a);
                if (!a.Cancel)
                {
                    base.InsertRange(index, affectedItems);
                    ListUpdated?.Invoke(this, a);
                }
            }
            else base.InsertRange(index, collection);
        }

        /// <summary>
        /// Quita la primera aparición de un objeto específico del 
        /// <see cref="ListEx{T}"/>.
        /// </summary>
        /// <param name="item">
        /// Objeto de tipo <typeparamref name="T"/> a remover de la
        /// colección.
        /// </param>
        /// <exception cref="IndexOutOfRangeException">
        /// Se produce si esta lista está vacía.
        /// </exception>
        public new void Remove(T item)
        {
            if (!this.Any()) throw new IndexOutOfRangeException(null, new EmptyCollectionException(this));
            if (TriggerEvents)
            {
                var a = new RemovingItemEventArgs<T>(IndexOf(item), this.Last());
                RemovingItem?.Invoke(this, a);
                if (!a.Cancel)
                {
                    base.Remove(item);
                    RemovedItem?.Invoke(this, a);
                }
            }
            else base.Remove(item);
        }

        /// <summary>
        /// Quita el elemento situado en el índice especificado del 
        /// <see cref="ListEx{T}"/>.
        /// </summary>
        /// <param name="index">Índice del elemento a remover.</param>
        /// <exception cref="IndexOutOfRangeException">
        /// Se produce si esta lista está vacía, o si se intenta remover un
        /// elemento de un índice que no existe.
        /// </exception>
        public new void RemoveAt(int index)
        {
            if (!this.Any()) throw new IndexOutOfRangeException(null, new EmptyCollectionException(this));
            if (TriggerEvents)
            {
                var a = new RemovingItemEventArgs<T>(index, this[index]);
                RemovingItem?.Invoke(this, a);
                if (!a.Cancel)
                {
                    base.RemoveAt(index);
                    RemovedItem?.Invoke(this, a);
                }
            }
            else base.RemoveAt(index);
        }

        /// <summary>
        /// Quita todos los elementos que cumplen con las condiciones
        /// definidas por el predicado especificado.
        /// </summary>
        /// <param name="match">
        /// Delegado <see cref="Predicate{T}"/> que define las condiciones
        /// de los elementos que se van a quitar.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Se produce si <paramref name="match"/> es <see langword="null"/>.
        /// </exception>
#if PreferExceptions
		/// <exception cref="EmptyCollectionException">
        /// Se produce si esta lista está vacía.
        /// </exception>
#endif
        public new int RemoveAll(Predicate<T> match)
        {
            if (!this.Any())
#if PreferExceptions
                throw new EmptyCollectionException(this);
#else
                return 0;
#endif
            if (!TriggerEvents) return base.RemoveAll(match);

            var tmp = this.Where(c => match(c));
            var a = new ListUpdatingEventArgs<T>(ListUpdateType.ItemsRemoved, tmp);
            ListUpdating?.Invoke(this, a);
            if (a.Cancel) return 0;
            var retVal = base.RemoveAll(match);
            ListUpdated?.Invoke(this, a);
            return retVal;
        }

        /// <summary>
        /// Invierte el orden de todos los elementos en este 
        /// <see cref="ListEx{T}"/>.
        /// </summary>
        public new void Reverse()
        {
            if (TriggerEvents)
            {
                var a = new ListUpdatingEventArgs<T>(ListUpdateType.ItemsMoved, Reversed());
                ListUpdating?.Invoke(this, a);
                if (!a.Cancel)
                {
                    base.Reverse();
                    ListUpdated?.Invoke(this, a);
                }
            }
            else base.Reverse();
        }

        /// <summary>
        /// Invierte el orden de los elementos en el intervalo
        /// especificado.
        /// </summary>
        /// <param name="index">
        /// Índice inicial de base cero del intervalo que se va a invertir.
        /// </param>
        /// <param name="count">
        /// Número de elementos del intervalo que se va a invertir.
        /// </param>
        public new void Reverse(int index, int count)
        {
            if (TriggerEvents)
            {
                var a = new ListUpdatingEventArgs<T>(ListUpdateType.ItemsMoved, Reversed(index, count));
                ListUpdating?.Invoke(this, a);
                if (!a.Cancel)
                {
                    base.Reverse(index, count);
                    ListUpdated?.Invoke(this, a);
                }
            }
            else base.Reverse(index, count);
        }

        /// <summary>
        /// Devuelve una copia invertida de los elementos de este 
        /// <see cref="ListEx{T}"/> sin alterar la colección original.
        /// </summary>
        /// <returns>
        /// Una copia inversa de los elementos de este 
        /// <see cref="ListEx{T}"/>.
        /// </returns>
        public IEnumerable<T> Reversed()
        {
            var tmp = (ListEx<T>)this.Copy();
            tmp.Reverse();
            return tmp;
        }

        /// <summary>
        /// Devuelve una copia invertida de los elementos de este 
        /// <see cref="ListEx{T}"/> sin alterar la colección original.
        /// </summary>
        /// <param name="index">
        /// Índice inicial de base cero del intervalo que se va a invertir.
        /// </param>
        /// <param name="count">
        /// Número de elementos del intervalo que se va a invertir.
        /// </param>
        /// <returns>
        /// Una copia inversa de los elementos de este 
        /// <see cref="ListEx{T}"/>.
        /// </returns>
        public IEnumerable<T> Reversed(int index, int count)
        {
            var tmp = (ListEx<T>)this.Copy();
            tmp.Reverse(index, count);
            return tmp;
        }

        /// <summary>
        /// Quita el último elemento de este <see cref="ListEx{T}"/>.
        /// </summary>
        [Sugar] public void RemoveLast() => Remove(this.Last());

        /// <summary>
        /// Quita el primer elemento de este <see cref="ListEx{T}"/>.
        /// </summary>
        [Sugar] public void RemoveFirst() => Remove(this.First());

        /// <summary>
        /// Quita todos los elementos de este <see cref="ListEx{T}"/>.
        /// </summary>
        public new void Clear()
        {
            if (TriggerEvents)
            {
                var a = new CancelEventArgs();
                ListClearing?.Invoke(this, a);
                if (!a.Cancel)
                {
                    base.Clear();
                    ListCleared?.Invoke(this, EventArgs.Empty);
                }
            }
            else base.Clear();
        }

        /// <summary>
        /// Ordena los elementos de todo el <see cref="ListEx{T}"/>
        /// utilizando el comparador predeterminado.
        /// </summary>
        public new void Sort()
        {
            if (TriggerEvents)
            {
                var a = new ListUpdatingEventArgs<T>(ListUpdateType.ItemsMoved, this);
                ListUpdating?.Invoke(this, a);
                if (!a.Cancel)
                {
                    base.Sort();
                    ListUpdated?.Invoke(this, a);
                }
            }
            else base.Sort();
        }

        /// <summary>
        /// Ordena los elementos de todo el <see cref="ListEx{T}"/>
        /// utilizando el <see cref="Comparison{T}"/> especificado.
        /// </summary>
        /// <param name="comparsion">
        /// <see cref="Comparison{T}"/> que se va a utilizar al comparar 
        /// elementos.
        /// </param>
        public new void Sort(Comparison<T> comparsion)
        {
            if (TriggerEvents)
            {
                var a = new ListUpdatingEventArgs<T>(ListUpdateType.ItemsMoved, this);
                ListUpdating?.Invoke(this, a);
                if (!a.Cancel)
                {
                    base.Sort(comparsion);
                    ListUpdated?.Invoke(this, a);
                }
            }
            else base.Sort(comparsion);
        }

        /// <summary>
        /// Ordena los elementos de todo el <see cref="ListEx{T}"/>
        /// utilizando el <see cref="IComparer{T}"/> especificado.
        /// </summary>
        /// <param name="comparer">
        /// Implementación de <see cref="IComparer{T}"/> que se va a
        /// utilizar al comparar elementos, o <see langword="null"/> para
        /// utilizar el comparador predeterminado.
        /// </param>
        public new void Sort(IComparer<T> comparer)
        {
            if (TriggerEvents)
            {
                var a = new ListUpdatingEventArgs<T>(ListUpdateType.ItemsMoved, this);
                ListUpdating?.Invoke(this, a);
                if (!a.Cancel)
                {
                    base.Sort(comparer);
                    ListUpdated?.Invoke(this, a);
                }
            }
            else base.Sort(comparer);
        }

        /// <summary>
        /// Ordena los elementos en un intevalo especificado del 
        /// <see cref="ListEx{T}"/> utilizando el comparador especificado.
        /// </summary>
        /// <param name="index">
        /// Índice inicial de base cero del intervalo que se va a ordenar.
        /// </param>
        /// <param name="count">
        /// Longitud del intervalo que se va a ordernar.
        /// </param>
        /// <param name="comparer">
        /// Implementación de <see cref="IComparer{T}"/> que se va a
        /// utilizar al comparar elementos, o <see langword="null"/> para
        /// utilizar el comparador predeterminado.
        /// </param>
        public new void Sort(int index, int count, IComparer<T> comparer)
        {
            if (TriggerEvents)
            {
                var a = new ListUpdatingEventArgs<T>(ListUpdateType.ItemsMoved, this.Range(index, count));
                ListUpdating?.Invoke(this, a);
                if (!a.Cancel)
                {
                    base.Sort(index, count, comparer);
                    ListUpdated?.Invoke(this, a);
                }
            }
            else base.Sort(index, count, comparer);
        }

        /// <summary>
        /// Implementa la interfaz <see cref="ICloneable"/>.
        /// </summary>
        /// <returns>Una copia de esta instancia.</returns>
        public object Clone() => this.Copy();

        /// <summary>
        /// Devuelve el tipo de elementos de <see cref="ListEx{T}"/>.
        /// </summary>
        /// <returns>
        /// El tipo de elementos que este <see cref="ListEx{T}"/> puede
        /// contener.
        /// </returns>
        public Type ItemType => typeof(T);
    }
}