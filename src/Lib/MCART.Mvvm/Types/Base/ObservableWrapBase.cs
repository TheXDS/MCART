/*
ObservableWrapBase.cs

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
using Nccha = System.Collections.Specialized.NotifyCollectionChangedAction;
using NcchEa = System.Collections.Specialized.NotifyCollectionChangedEventArgs;

namespace TheXDS.MCART.Types.Base;

/// <summary>
/// Base class for observable collection wrappers.
/// </summary>
[DebuggerStepThrough]
public abstract class ObservableWrapBase : NotifyPropertyChanged, INotifyCollectionChanged, IEnumerable
{
    /// <summary>
    /// Occurs when the collection changes.
    /// </summary>
    public event NotifyCollectionChangedEventHandler? CollectionChanged;

    /// <summary>
    /// Raises the <see cref="CollectionChanged"/> event.
    /// </summary>
    /// <param name="eventArgs">Event arguments.</param>
    protected void RaiseCollectionChanged(NcchEa eventArgs)
    {
        CollectionChanged?.Invoke(this, eventArgs);
    }

    /// <inheritdoc/>
    public IEnumerator GetEnumerator() => OnGetEnumerator();

    /// <summary>
    /// Gets an enumerator that iterates through the collection.
    /// </summary>
    /// <returns>
    /// An enumerator that can be used to iterate over the collection.
    /// </returns>
    protected abstract IEnumerator OnGetEnumerator();

    /// <summary>
    /// Gets the index of the specified item.
    /// </summary>
    /// <param name="item">Item for which to find the index.</param>
    /// <returns>
    /// The index of the specified item, or <c>-1</c> if the item is
    /// not found in the collection.
    /// </returns>
    public virtual int IndexOf(object item)
    {
        int c = 0;
        foreach (object? j in this)
        {
            if (j?.Equals(item) ?? item is null) return c;
            c++;
        }
        return -1;
    }

    /// <summary>
    /// Forces refresh of a single item within the collection.
    /// </summary>
    /// <param name="item">Item to refresh.</param>
    public void RefreshItem(object? item)
    {
        if (item is null || !Contains(item)) return;
        switch (item)
        {
            case IRefreshable refreshable:
                refreshable.Refresh();
                break;
            default:
                RaiseCollectionChanged(new NcchEa(Nccha.Replace, item, item, IndexOf(item)));
                break;
        }
    }

    /// <summary>
    /// Determines whether the underlying sequence contains the
    /// specified item.
    /// </summary>
    /// <param name="item">Item to locate in the sequence.</param>
    /// <returns>
    /// <see langword="true"/> if the sequence contains the specified
    /// item; otherwise, <see langword="false"/>.
    /// </returns>
    public virtual bool Contains(object item)
    {
        return IndexOf(item) != -1;
    }
}
