/*
ObservableListWrap.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2025 César Andrés Morgan

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
using System.Collections.Specialized;
using System.Diagnostics;
using TheXDS.MCART.Types.Base;
using static System.Collections.Specialized.NotifyCollectionChangedAction;
using Nccha = System.Collections.Specialized.NotifyCollectionChangedAction;
using NcchEa = System.Collections.Specialized.NotifyCollectionChangedEventArgs;

namespace TheXDS.MCART.Types;

/// <summary>
/// Wraps a list to provide notifications when the collection's
/// content changes.
/// </summary>
[DebuggerStepThrough]
public class ObservableListWrap : ObservableWrapBase, IList, INotifyCollectionChanged
{
    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="ObservableListWrap"/> class.
    /// </summary>
    public ObservableListWrap()
    {
    }

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="ObservableListWrap"/> class.
    /// </summary>
    /// <param name="list">
    /// List to use as the underlying list.
    /// </param>
    public ObservableListWrap(IList list)
    {
        UnderlyingList = list;
    }

    /// <summary>
    /// Gets direct access to the underlying list managed by this
    /// <see cref="ObservableListWrap"/>.
    /// </summary>
    public IList? UnderlyingList { get; private set; }

    /// <summary>
    /// Provides indexed access to the contents of this
    /// <see cref="ObservableListWrap"/>.
    /// </summary>
    /// <param name="index">
    /// Index of the element to get or set.
    /// </param>
    /// <returns>
    /// The element at the specified index in the collection.
    /// </returns>
    public object? this[int index]
    {
        get => GetUnderlyingList()[index];
        set
        {
            object? oldItem = GetUnderlyingList()[index];
            UnderlyingList![index] = value;
            RaiseCollectionChanged(new NcchEa(Replace, value, oldItem, index));
        }
    }

    /// <summary>
    /// Forces a refresh of the item at the specified index.
    /// </summary>
    /// <param name="index">Index of the item to refresh.</param>
    public void RefreshAt(int index)
    {
        RefreshItem(this[index]);
    }

    /// <summary>
    /// Gets a value indicating whether this list has a fixed size.
    /// </summary>
    public bool IsFixedSize => GetUnderlyingList().IsFixedSize;

    /// <summary>
    /// Gets a value indicating whether this list is read-only.
    /// </summary>
    public bool IsReadOnly => GetUnderlyingList().IsReadOnly;

    /// <summary>
    /// Gets the number of elements contained in the collection.
    /// </summary>
    public int Count => GetUnderlyingList().Count;

    /// <summary>
    /// Gets a value indicating whether access to this
    /// <see cref="ICollection"/> is synchronized (thread safe).
    /// </summary>
    public bool IsSynchronized => GetUnderlyingList().IsSynchronized;

    /// <summary>
    /// Gets an object that can be used to synchronize access to the
    /// <see cref="ICollection"/>.
    /// </summary>
    public object SyncRoot => GetUnderlyingList().SyncRoot;

    /// <summary>
    /// Adds an item to this <see cref="ObservableListWrap"/>.
    /// </summary>
    /// <param name="value">
    /// Value to add to this <see cref="ObservableListWrap"/>.
    /// </param>
    /// <returns>
    /// The index at which the item was added.
    /// </returns>
    public int Add(object? value)
    {
        int returnValue = GetUnderlyingList().Add(value);
        RaiseCollectionChanged(new NcchEa(Nccha.Add, value, returnValue));
        Notify(nameof(Count));
        return returnValue;
    }

    /// <summary>
    /// Removes all items from this <see cref="ObservableListWrap"/>.
    /// </summary>
    public void Clear()
    {
        GetUnderlyingList().Clear();
        RaiseCollectionChanged(new NcchEa(Reset));
        Notify(nameof(Count));
    }

    /// <summary>
    /// Determines whether this <see cref="ObservableListWrap"/> contains a
    /// specified value.
    /// </summary>
    /// <param name="value">
    /// Value to check for.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if this <see cref="ObservableListWrap"/>
    /// contains the specified value; otherwise <see langword="false"/>.
    /// </returns>
    public override bool Contains(object? value)
    {
        return GetUnderlyingList().Contains(value);
    }

    /// <summary>
    /// Copies the elements of this <see cref="ObservableListWrap"/> to an
    /// array, starting at a particular array index.
    /// </summary>
    /// <param name="array">Destination array for the copy.</param>
    /// <param name="index">
    /// Index in the destination array at which to start copying.
    /// </param>
    public void CopyTo(Array array, int index)
    {
        GetUnderlyingList().CopyTo(array, index);
    }

    /// <summary>
    /// Returns an enumerator that iterates through the collection.
    /// </summary>
    /// <returns>
    /// An enumerator that can be used to iterate through the collection.
    /// </returns>
    protected override IEnumerator OnGetEnumerator()
    {
        return GetUnderlyingList().GetEnumerator();
    }

    /// <summary>
    /// Returns the index of a specific item in this
    /// <see cref="ObservableListWrap"/>.
    /// </summary>
    /// <param name="value">
    /// Value whose index to get in this <see cref="ObservableListWrap"/>.
    /// </param>
    /// <returns>
    /// The index of the item in this <see cref="ObservableListWrap"/>.
    /// </returns>
    public override int IndexOf(object? value)
    {
        return GetUnderlyingList().IndexOf(value);
    }

    /// <summary>
    /// Inserts an item into this <see cref="ObservableListWrap"/> at the
    /// specified index.
    /// </summary>
    /// <param name="index">
    /// The index at which to insert the item.
    /// </param>
    /// <param name="value">
    /// Value to insert into this <see cref="ObservableListWrap"/>.
    /// </param>
    public void Insert(int index, object? value)
    {
        GetUnderlyingList().Insert(index, value);
        RaiseCollectionChanged(new NcchEa(Nccha.Add, value, index));
        Notify(nameof(Count));
    }

    /// <summary>
    /// Removes an item from this <see cref="ObservableListWrap"/>.
    /// </summary>
    /// <param name="value">
    /// Value to remove from this <see cref="ObservableListWrap"/>.
    /// </param>
    public void Remove(object? value)
    {
        if (GetUnderlyingList().Contains(value))
        {
            int index = UnderlyingList!.IndexOf(value);
            UnderlyingList.Remove(value);
            RaiseCollectionChanged(new NcchEa(Nccha.Remove, value, index));
            Notify(nameof(Count));
        }
    }

    /// <summary>
    /// Removes the item at the specified index from this
    /// <see cref="ObservableListWrap"/>.
    /// </summary>
    /// <param name="index">
    /// Index of the item to remove.
    /// </param>
    public void RemoveAt(int index)
    {
        object? item = GetUnderlyingList()[index];
        UnderlyingList!.RemoveAt(index);
        RaiseCollectionChanged(new NcchEa(Nccha.Remove, item, index));
        Notify(nameof(Count));
    }

    /// <summary>
    /// Forces notification that the list has changed.
    /// </summary>
    public override void Refresh()
    {
        _ = GetUnderlyingList();
        base.Refresh();
        Substitute(UnderlyingList!);
    }

    /// <summary>
    /// Replaces the underlying list with a new one.
    /// </summary>
    /// <param name="newList">
    /// List to set as the underlying list.
    /// </param>
    public void Substitute(IList newList)
    {
        UnderlyingList = Array.Empty<object>();
        RaiseCollectionChanged(new NcchEa(Reset));
        UnderlyingList = newList;
        if (newList is not null) RaiseCollectionChanged(new NcchEa(Nccha.Add, newList, 0));
    }

    private IList GetUnderlyingList()
    {
        return UnderlyingList ?? throw new InvalidOperationException();
    }
}
