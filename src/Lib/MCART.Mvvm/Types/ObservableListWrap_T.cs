/*
ObservableListWrap_T.cs

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

using System.Diagnostics;
using TheXDS.MCART.Types.Base;
using Nccha = System.Collections.Specialized.NotifyCollectionChangedAction;
using NcchEa = System.Collections.Specialized.NotifyCollectionChangedEventArgs;

namespace TheXDS.MCART.Types;

/// <summary>
/// Wraps a generic list to provide notifications when the collection's
/// content changes.
/// </summary>
/// <typeparam name="T">Type of elements in the list.</typeparam>
[DebuggerStepThrough]
public class ObservableListWrap<T> : ObservableWrap<T, IList<T>>, IList<T>
{
    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="ObservableListWrap{T}"/> class.
    /// </summary>
    public ObservableListWrap()
    {
    }

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="ObservableListWrap{T}"/> class.
    /// </summary>
    /// <param name="collection">
    /// Collection to use as the underlying list.
    /// </param>
    public ObservableListWrap(IList<T> collection) : base(collection)
    {
    }

    /// <summary>
    /// Provides indexed access to the contents of this
    /// <see cref="ObservableListWrap{T}"/>.
    /// </summary>
    /// <param name="index">
    /// Index of the element to get or set.
    /// </param>
    /// <returns>
    /// The element at the specified index in the collection.
    /// </returns>
    public T this[int index]
    {
        get => UnderlyingCollection is not null ? UnderlyingCollection[index] : throw new InvalidOperationException();
        set
        {
            if (UnderlyingCollection is null) throw new InvalidOperationException();
            T? oldItem = UnderlyingCollection[index];
            UnderlyingCollection[index] = value;
            RaiseCollectionChanged(new NcchEa(Nccha.Replace, value, oldItem));
        }
    }

    /// <summary>
    /// Returns the index of a specific item in this
    /// <see cref="ObservableListWrap{T}"/>.
    /// </summary>
    /// <param name="item">
    /// Item whose index to get.
    /// </param>
    /// <returns>
    /// The index of the specified item.
    /// </returns>
    public int IndexOf(T item) => UnderlyingCollection?.IndexOf(item) ?? -1;

    /// <summary>
    /// Determines whether the underlying sequence contains the specified
    /// item.
    /// </summary>
    /// <param name="item">
    /// Item to locate in the sequence.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the sequence contains the specified
    /// item; otherwise <see langword="false"/>.
    /// </returns>
    public override bool Contains(T item) => UnderlyingCollection?.Contains(item) ?? false;

    /// <summary>
    /// Inserts an item into this <see cref="ObservableListWrap{T}"/> at
    /// the specified index.
    /// </summary>
    /// <param name="index">
    /// Position at which to insert the new item.
    /// </param>
    /// <param name="item">
    /// Item to insert at the specified index.
    /// </param>
    public void Insert(int index, T item)
    {
        if (UnderlyingCollection is null) throw new InvalidOperationException();
        UnderlyingCollection.Insert(index, item);
        RaiseCollectionChanged(new NcchEa(Nccha.Add, item));
        Notify(nameof(Count));
    }

    /// <summary>
    /// Removes the item at the specified index from this
    /// <see cref="ObservableListWrap{T}"/>.
    /// </summary>
    /// <param name="index">
    /// Index of the item to remove.
    /// </param>
    public void RemoveAt(int index)
    {
        if (UnderlyingCollection is null) throw new InvalidOperationException();
        T? item = UnderlyingCollection[index];
        UnderlyingCollection.RemoveAt(index);
        RaiseCollectionChanged(new NcchEa(Nccha.Remove, item, index));
        Notify(nameof(Count));
    }
}
