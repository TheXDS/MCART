/*
ListEx.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2026 César Andrés Morgan

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
using System.Diagnostics.CodeAnalysis;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Events;
using TheXDS.MCART.Misc;
using TheXDS.MCART.Types.Base;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.Types;

/// <summary>
/// Observable extension of the 
/// <see cref="List{T}" /> class. Provides all 
/// previously available functionality and 
/// includes some useful extensions.
/// </summary>
/// <typeparam name="T">
/// Type of elements this list will contain.
/// </typeparam>
/// <remarks>
/// This class can be considered as a more 
/// complete alternative to 
/// <see cref="ObservableCollection{T}" /> with 
/// numerous additional events and other 
/// extensions.
/// </remarks>
[Obsolete(AttributeErrorMessages.UnsuportedClass),
 ExcludeFromCodeCoverage]
public class ListEx<T> : List<T>, ICloneable<ListEx<T>>
{
    /// <summary>
    /// Occurs when an item is about to be added 
    /// to the list.
    /// </summary>
    public event EventHandler<AddingItemEventArgs<T>>? AddingItem;

    /// <summary>
    /// Occurs when an item is about to be 
    /// inserted into the list.
    /// </summary>
    public event EventHandler<InsertingItemEventArgs<T>>? InsertingItem;

    /// <summary>
    /// Occurs when an item in the list is about 
    /// to be modified.
    /// </summary>
    public event EventHandler<ModifyingItemEventArgs<T>>? ModifyingItem;

    /// <summary>
    /// Occurs when an item is about to be 
    /// removed from the list.
    /// </summary>
    public event EventHandler<RemovingItemEventArgs<T>>? RemovingItem;

    /// <summary>
    /// Occurs when the list is about to be 
    /// updated.
    /// </summary>
    public event EventHandler<ListUpdatingEventArgs<T>>? ListUpdating;

    /// <summary>
    /// Occurs when an item has been added to 
    /// the list.
    /// </summary>
    public event EventHandler<AddedItemEventArgs<T>>? AddedItem;

    /// <summary>
    /// Occurs when an item has been inserted 
    /// into the list.
    /// </summary>
    public event EventHandler<InsertedItemEventArgs<T>>? InsertedItem;

    /// <summary>
    /// Occurs when an item in the list has been 
    /// modified.
    /// </summary>
    public event EventHandler<ItemModifiedEventArgs<T>>? ModifiedItem;

    /// <summary>
    /// Occurs when an item has been removed 
    /// from the list.
    /// </summary>
    public event EventHandler<RemovedItemEventArgs<T>>? RemovedItem;

    /// <summary>
    /// Occurs when the list is about to be 
    /// cleared using <see cref="Clear"/>.
    /// </summary>
    public event EventHandler<CancelEventArgs>? ListClearing;

    /// <summary>
    /// Occurs when the list has been cleared 
    /// using <see cref="Clear"/>.
    /// </summary>
    public event EventHandler? ListCleared;

    /// <summary>
    /// Occurs when the list has been updated.
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
    /// Initial collection for this 
    /// <see cref="ListEx{T}" />.
    /// </param>
    public ListEx(IEnumerable<T> collection) : base(collection) { }

    /// <summary>
    /// Initializes a new instance of the 
    /// <see cref="ListEx{T}"/>.
    /// </summary>
    /// <param name="initialSize">Initial size of the list.</param>
    public ListEx(int initialSize) : base(initialSize) { }

    /// <summary>
    /// Returns the type of elements of 
    /// <see cref="ListEx{T}"/>.
    /// </summary>
    /// <returns>
    /// The type of elements that this 
    /// <see cref="ListEx{T}"/> can contain.
    /// </returns>
    public Type ItemType => typeof(T);

    /// <summary>
    /// Enables or disables event generation.
    /// </summary>
    public bool TriggerEvents { get; set; } = true;

    /// <summary>
    /// Gets or sets the element at the specified index.
    /// </summary>
    /// <param name="index">Index of the element.</param>
    /// <returns>The element at the specified position.</returns>
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
    /// Adds an object to the end of the 
    /// <see cref="ListEx{T}"/>.
    /// </summary>
    /// <param name="item">
    /// The object to be added to the end of the 
    /// <see cref="ListEx{T}"/>. The value can be 
    /// <see langword="null"/> for reference types.
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
    /// Adds all elements from a collection to the end of the 
    /// <see cref="ListEx{T}"/>.
    /// </summary>
    /// <param name="collection">Collection to add.</param>
    public new void AddRange(IEnumerable<T> collection)
    {
        if (TriggerEvents)
        {
            List<T>? affectedItems = [.. collection];
            ListUpdatingEventArgs<T>? a = new(ListUpdateType.ItemsAdded, affectedItems);
            ListUpdating?.Invoke(this, a);
            if (a.Cancel) return;
            base.AddRange(affectedItems);
            ListUpdated?.Invoke(this, a);
        }
        else base.AddRange(collection);
    }

    /// <summary>
    /// Inserts an element at the specified index.
    /// </summary>
    /// <param name="index">Destination index of the new element.</param>
    /// <param name="item">Element to insert.</param>
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
    /// Inserts a collection of elements starting at the specified index.
    /// </summary>
    /// <param name="index">Destination index for the insertion.</param>
    /// <param name="collection">Collection of elements to insert.</param>
    public new void InsertRange(int index, IEnumerable<T> collection)
    {
        if (TriggerEvents)
        {
            List<T>? affectedItems = [.. collection];
            ListUpdatingEventArgs<T>? a = new(ListUpdateType.ItemsInserted, affectedItems);
            ListUpdating?.Invoke(this, a);
            if (a.Cancel) return;
            base.InsertRange(index, affectedItems);
            ListUpdated?.Invoke(this, a);
        }
        else base.InsertRange(index, collection);
    }

    /// <summary>
    /// Removes the first occurrence of a specific object from the 
    /// <see cref="ListEx{T}"/>.
    /// </summary>
    /// <param name="item">
    /// Object of type <typeparamref name="T"/> to remove from the 
    /// collection.
    /// </param>
    /// <exception cref="IndexOutOfRangeException">
    /// Occurs if this list is empty.
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
    /// Removes the element at the specified index from the 
    /// <see cref="ListEx{T}"/>.
    /// </summary>
    /// <param name="index">Index of the element to remove.</param>
    /// <exception cref="IndexOutOfRangeException">
    /// Occurs if this list is empty, or if you try to remove an element 
    /// at an index that does not exist.
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
    /// Removes all elements that match the conditions defined by the 
    /// specified predicate.
    /// </summary>
    /// <param name="match">
    /// A <see cref="Predicate{T}"/> delegate that defines the conditions 
    /// of the elements to be removed.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Occurs if <paramref name="match"/> is <see langword="null"/>.
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
    /// Reverses the order of all elements in this 
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
    /// Reverses the order of the elements in the specified range.
    /// </summary>
    /// <param name="index">
    /// The zero-based starting index of the range to reverse.
    /// </param>
    /// <param name="count">
    /// The number of elements in the range to reverse.
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
    /// Returns a reversed copy of the elements in this 
    /// <see cref="ListEx{T}"/> without modifying the original collection.
    /// </summary>
    /// <returns>
    /// A reversed copy of the elements in this 
    /// <see cref="ListEx{T}"/>.
    /// </returns>
    public IEnumerable<T> Reversed()
    {
        ListEx<T>? tmp = (ListEx<T>)this.Copy();
        tmp.Reverse();
        return tmp;
    }

    /// <summary>
    /// Returns a reversed copy of the elements in the specified range 
    /// in this <see cref="ListEx{T}"/> without modifying the original 
    /// collection.
    /// </summary>
    /// <param name="index">
    /// The zero-based starting index of the range to reverse.
    /// </param>
    /// <param name="count">
    /// The number of elements in the range to reverse.
    /// </param>
    /// <returns>
    /// A reversed copy of the elements in the specified range in this 
    /// <see cref="ListEx{T}"/>.
    /// </returns>
    public IEnumerable<T> Reversed(int index, int count)
    {
        ListEx<T>? tmp = (ListEx<T>)this.Copy();
        tmp.Reverse(index, count);
        return tmp;
    }

    /// <summary>
    /// Removes the last element from this <see cref="ListEx{T}"/>.
    /// </summary>
    [Sugar] public void RemoveLast() => Remove(this.Last());

    /// <summary>
    /// Removes the first element from this <see cref="ListEx{T}"/>.
    /// </summary>
    [Sugar] public void RemoveFirst() => Remove(this.First());

    /// <summary>
    /// Removes all elements from this <see cref="ListEx{T}"/>.
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
    /// Sorts all elements in the <see cref="ListEx{T}"/> using the default 
    /// comparer.
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
    /// Sorts all elements in the <see cref="ListEx{T}"/> using the specified 
    /// <see cref="Comparison{T}"/>.
    /// </summary>
    /// <param name="comparison">
    /// A <see cref="Comparison{T}"/> to use when comparing elements.
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
    /// Sorts all elements in the <see cref="ListEx{T}"/> using the specified 
    /// <see cref="IComparer{T}"/>.
    /// </summary>
    /// <param name="comparer">
    /// An implementation of <see cref="IComparer{T}"/> to use when comparing 
    /// elements, or <see langword="null"/> to use the default comparer.
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
    /// Sorts the elements in a specified range in the <see cref="ListEx{T}"/> 
    /// using the specified comparer.
    /// </summary>
    /// <param name="index">
    /// The zero-based starting index of the range to sort.
    /// </param>
    /// <param name="count">
    /// The length of the range to sort.
    /// </param>
    /// <param name="comparer">
    /// An implementation of <see cref="IComparer{T}"/> to use when comparing 
    /// elements, or <see langword="null"/> to use the default comparer.
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
    /// Implements the <see cref="ICloneable{T}"/> interface.
    /// </summary>
    /// <returns>A copy of this instance.</returns>
    public ListEx<T> Clone() => [.. this.Copy()];
}
