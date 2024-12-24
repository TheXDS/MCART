/*
ListEx.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2024 César Andrés Morgan

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

using System.Collections.ObjectModel;
using System.ComponentModel;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Events;
using TheXDS.MCART.Exceptions;
using TheXDS.MCART.Types.Base;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.Types;

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
public class ListEx<T> : List<T>, ICloneable<ListEx<T>>
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
    /// Initializes a new instance of the 
    /// <see cref="ListEx{T}" />.
    /// </summary>
    public ListEx() { }

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="ListEx{T}" />.
    /// </summary>
    /// <param name="collection">
    /// Colección inicial de este <see cref="ListEx{T}" />.
    /// </param>
    public ListEx(IEnumerable<T> collection) : base(collection) { }

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="ListEx{T}"/>.
    /// </summary>
    /// <param name="initialSize">Tamaño inicial de la lista.</param>
    public ListEx(int initialSize) : base(initialSize) { }
    
    /// <summary>
    /// Devuelve el tipo de elementos de <see cref="ListEx{T}"/>.
    /// </summary>
    /// <returns>
    /// El tipo de elementos que este <see cref="ListEx{T}"/> puede
    /// contener.
    /// </returns>
    public Type ItemType => typeof(T);

    /// <summary>
    /// Activa o desactiva la generación de eventos.
    /// </summary>
    public bool TriggerEvents { get; set; } = true;

    /// <summary>
    /// Obtiene o establece el elemento ubicado en el índice
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
                ModifyingItemEventArgs<T>? a = new(index, base[index], value);
                ModifyingItem?.Invoke(this, a);
                if (a.Cancel) return;
                base[index] = value;
                ModifiedItem?.Invoke(this, a);
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
            AddingItemEventArgs<T>? a = new(item);
            AddingItem?.Invoke(this, a);
            if (a.Cancel) return;
            base.Add(item);
            AddedItem?.Invoke(this, a);
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
            List<T>? affectedItems = collection.ToList();
            ListUpdatingEventArgs<T>? a = new(ListUpdateType.ItemsAdded, affectedItems);
            ListUpdating?.Invoke(this, a);
            if (a.Cancel) return;
            base.AddRange(affectedItems);
            ListUpdated?.Invoke(this, a);
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
            InsertingItemEventArgs<T>? a = new(index, item);
            InsertingItem?.Invoke(this, a);
            if (a.Cancel) return;
            base.Insert(index, item);
            InsertedItem?.Invoke(this, a);
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
            List<T>? affectedItems = collection.ToList();
            ListUpdatingEventArgs<T>? a = new(ListUpdateType.ItemsInserted, affectedItems);
            ListUpdating?.Invoke(this, a);
            if (a.Cancel) return;
            base.InsertRange(index, affectedItems);
            ListUpdated?.Invoke(this, a);
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
    public new bool Remove(T item)
    {
        if (!this.Any()) return false;
        if (TriggerEvents)
        {
            RemovingItemEventArgs<T>? a = new(IndexOf(item), item);
            RemovingItem?.Invoke(this, a);
            if (a.Cancel) return false;
            var result = base.Remove(item);
            RemovedItem?.Invoke(this, a);
            return result;
        }
        else return base.Remove(item);
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
        if (!this.Any()) return;
        if (TriggerEvents)
        {
            RemovingItemEventArgs<T>? a = new(index, this[index]);
            RemovingItem?.Invoke(this, a);
            if (a.Cancel) return;
            base.RemoveAt(index);
            RemovedItem?.Invoke(this, a);
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
    public new int RemoveAll(Predicate<T> match)
    {
        if (!this.Any()) return 0;
        if (!TriggerEvents) return base.RemoveAll(match);
        IEnumerable<T>? tmp = this.Where(c => match(c));
        ListUpdatingEventArgs<T>? a = new(ListUpdateType.ItemsRemoved, tmp);
        ListUpdating?.Invoke(this, a);
        if (a.Cancel) return 0;
        int retVal = base.RemoveAll(match);
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
            ListUpdatingEventArgs<T>? a = new(ListUpdateType.ItemsMoved, Reversed());
            ListUpdating?.Invoke(this, a);
            if (a.Cancel) return;
            base.Reverse();
            ListUpdated?.Invoke(this, a);
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
            ListUpdatingEventArgs<T>? a = new(ListUpdateType.ItemsMoved, Reversed(index, count));
            ListUpdating?.Invoke(this, a);
            if (a.Cancel) return;
            base.Reverse(index, count);
            ListUpdated?.Invoke(this, a);
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
        ListEx<T>? tmp = (ListEx<T>)this.Copy();
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
        ListEx<T>? tmp = (ListEx<T>)this.Copy();
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
            CancelEventArgs? a = new();
            ListClearing?.Invoke(this, a);
            if (a.Cancel) return;
            base.Clear();
            ListCleared?.Invoke(this, EventArgs.Empty);
        }
        else base.Clear();
    }

    /// <summary>
    /// Ordena todos los elementos del <see cref="ListEx{T}"/>
    /// utilizando el comparador predeterminado.
    /// </summary>
    public new void Sort()
    {
        if (TriggerEvents)
        {
            ListUpdatingEventArgs<T>? a = new(ListUpdateType.ItemsMoved, this);
            ListUpdating?.Invoke(this, a);
            if (a.Cancel) return;
            base.Sort();
            ListUpdated?.Invoke(this, a);
        }
        else base.Sort();
    }

    /// <summary>
    /// Ordena todos los elementos del <see cref="ListEx{T}"/>
    /// utilizando el <see cref="Comparison{T}"/> especificado.
    /// </summary>
    /// <param name="comparison">
    /// <see cref="Comparison{T}"/> que se va a utilizar al comparar 
    /// elementos.
    /// </param>
    public new void Sort(Comparison<T> comparison)
    {
        if (TriggerEvents)
        {
            ListUpdatingEventArgs<T>? a = new(ListUpdateType.ItemsMoved, this);
            ListUpdating?.Invoke(this, a);
            if (a.Cancel) return;
            base.Sort(comparison);
            ListUpdated?.Invoke(this, a);
        }
        else base.Sort(comparison);
    }

    /// <summary>
    /// Ordena todos los elementos del <see cref="ListEx{T}"/>
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
            ListUpdatingEventArgs<T>? a = new(ListUpdateType.ItemsMoved, this);
            ListUpdating?.Invoke(this, a);
            if (a.Cancel) return;
            base.Sort(comparer);
            ListUpdated?.Invoke(this, a);
        }
        else base.Sort(comparer);
    }

    /// <summary>
    /// Ordena los elementos en un intervalo especificado del 
    /// <see cref="ListEx{T}"/> utilizando el comparador especificado.
    /// </summary>
    /// <param name="index">
    /// Índice inicial de base cero del intervalo que se va a ordenar.
    /// </param>
    /// <param name="count">
    /// Longitud del intervalo que se va a ordenar.
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
            ListUpdatingEventArgs<T>? a = new(ListUpdateType.ItemsMoved, this.Range(index, count));
            ListUpdating?.Invoke(this, a);
            if (a.Cancel) return;
            base.Sort(index, count, comparer);
            ListUpdated?.Invoke(this, a);
        }
        else base.Sort(index, count, comparer);
    }

    /// <summary>
    /// Implementa la interfaz <see cref="ICloneable{T}"/>.
    /// </summary>
    /// <returns>Una copia de esta instancia.</returns>
    public ListEx<T> Clone() => new(this.Copy());
}
