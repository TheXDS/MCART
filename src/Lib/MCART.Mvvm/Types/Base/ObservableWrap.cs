/*
ObservableWrap.cs

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

using System.Collections;
using System.Diagnostics;
using TheXDS.MCART.Types.Extensions;
using static System.Collections.Specialized.NotifyCollectionChangedAction;
using Nccha = System.Collections.Specialized.NotifyCollectionChangedAction;
using NcchEa = System.Collections.Specialized.NotifyCollectionChangedEventArgs;

namespace TheXDS.MCART.Types.Base;

/// <summary>
/// Base class for observable collection wrappers.
/// </summary>
/// <typeparam name="T">Type of elements in the collection.</typeparam>
/// <typeparam name="TCollection">Type of the collection.</typeparam>
[DebuggerStepThrough]
public abstract class ObservableWrap<T, TCollection> : ObservableWrapBase, ICollection<T> where TCollection : ICollection<T>
{
    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="ObservableWrap{T, TCollection}"/> class.
    /// </summary>
    protected ObservableWrap()
    {
    }

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="ObservableWrap{T, TCollection}"/> class.
    /// </summary>
    /// <param name="collection">Initial underlying collection.</param>
    protected ObservableWrap(TCollection collection)
    {
        UnderlyingCollection = collection;
    }

    /// <summary>
    /// Gets direct access to the underlying collection wrapped by this
    /// <see cref="ObservableWrap{T, TCollection}"/>.
    /// </summary>
    public TCollection? UnderlyingCollection { get; private set; } = default;

    /// <summary>
    /// Gets the number of elements contained in the collection.
    /// </summary>
    public int Count => UnderlyingCollection?.Count ?? 0;

    /// <summary>
    /// Gets a value indicating whether the collection is read-only.
    /// </summary>
    public bool IsReadOnly => UnderlyingCollection?.IsReadOnly ?? true;

    /// <summary>
    /// Adds a new item to the end of the collection.
    /// </summary>
    /// <param name="item">Item to add.</param>
    public void Add(T item)
    {
        if (UnderlyingCollection is null) throw new InvalidOperationException();
        UnderlyingCollection.Add(item);
        RaiseCollectionChanged(new NcchEa(Nccha.Add, item, UnderlyingCollection.FindIndexOf(item)));
        Notify(nameof(Count));
    }

    /// <summary>
    /// Clears the collection.
    /// </summary>
    public void Clear()
    {
        if (UnderlyingCollection is null) return;
        UnderlyingCollection.Clear();
        RaiseCollectionChanged(new NcchEa(Reset));
        Notify(nameof(Count));
    }

    /// <summary>
    /// Copies the elements of this collection to an array.
    /// </summary>
    /// <param name="array">Destination array.</param>
    /// <param name="arrayIndex">
    /// Index in the destination at which copying begins.
    /// </param>
    public void CopyTo(T[] array, int arrayIndex)
    {
        UnderlyingCollection?.CopyTo(array, arrayIndex);
    }

    /// <summary>
    /// Removes an item from the collection.
    /// </summary>
    /// <param name="item">Item to remove.</param>
    /// <returns>
    /// <see langword="true"/> if the item was successfully removed from
    /// the collection; <see langword="false"/> otherwise. Also returns
    /// <see langword="false"/> if the item did not exist in the original
    /// <see cref="ICollection{T}"/>.
    /// </returns>
    public bool Remove(T item)
    {
        int idx = UnderlyingCollection?.FindIndexOf(item) ?? -1;
        if (idx == -1) return false;
        bool result = UnderlyingCollection!.Remove(item);
        if (result)
        {
            RaiseCollectionChanged(new NcchEa(Nccha.Remove, item, idx));
            Notify(nameof(Count));
        }

        return result;
    }

    /// <summary>
    /// Gets an enumerator that iterates through the collection.
    /// </summary>
    /// <returns>
    /// An enumerator that can be used to iterate over the collection.
    /// </returns>
    public new IEnumerator<T> GetEnumerator()
    {
        return UnderlyingCollection?.GetEnumerator() ?? new List<T>().GetEnumerator();
    }

    /// <summary>
    /// Gets an enumerator that iterates through the collection.
    /// </summary>
    /// <returns>
    /// An enumerator that can be used to iterate over the collection.
    /// </returns>
    protected override IEnumerator OnGetEnumerator()
    {
        return UnderlyingCollection?.GetEnumerator() ?? Array.Empty<T>().GetEnumerator();
    }

    /// <summary>
    /// Forces a notification that the collection has changed.
    /// </summary>
    public override void Refresh()
    {
        base.Refresh();
        Substitute(UnderlyingCollection);
    }

    /// <summary>
    /// Replaces the underlying collection with a new one.
    /// </summary>
    /// <param name="newCollection">
    /// The collection to set as the underlying collection.
    /// </param>
    public void Substitute(TCollection? newCollection)
    {
        UnderlyingCollection = default;
        RaiseCollectionChanged(new NcchEa(Reset));
        UnderlyingCollection = newCollection;
        if (newCollection is not null)
        {
            RaiseCollectionChanged(new NcchEa(Nccha.Add, (IList)newCollection));
        }
        Notify(nameof(Count));
    }

    /// <summary>
    /// Removes all items from the underlying collection and replaces
    /// them with the items from the specified collection.
    /// </summary>
    /// <param name="newCollection">
    /// Collection whose items will be added to the underlying
    /// collection.
    /// </param>
    public void Replace(TCollection newCollection)
    {
        UnderlyingCollection?.Locked(c =>
        {
            c.Clear();
            if (newCollection is null) return;
            foreach (T? j in newCollection)
            {
                UnderlyingCollection.Add(j);
            }
            RaiseCollectionChanged(new NcchEa(Nccha.Add, (IList)newCollection));
            Notify(nameof(Count));
        });
    }

    /// <summary>
    /// Determines whether the underlying sequence contains the specified
    /// item.
    /// </summary>
    /// <param name="item">Item to locate in the sequence.</param>
    /// <returns>
    /// <see langword="true"/> if the sequence contains the specified
    /// item; otherwise, <see langword="false"/>.
    /// </returns>
    public virtual bool Contains(T item) => UnderlyingCollection?.Contains(item) ?? false;
}
