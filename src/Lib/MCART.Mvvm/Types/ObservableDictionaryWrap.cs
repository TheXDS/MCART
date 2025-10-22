/*
ObservableDictionaryWrap.cs

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

using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;
using TheXDS.MCART.Helpers;
using TheXDS.MCART.Types.Base;
using NcchEa = System.Collections.Specialized.NotifyCollectionChangedEventArgs;

namespace TheXDS.MCART.Types;

/// <summary>
/// Wraps a dictionary to provide property and collection changed
/// notification events.
/// </summary>
/// <typeparam name="TKey">
/// Type of the dictionary keys.
/// </typeparam>
/// <typeparam name="TValue">
/// Type of elements contained in the dictionary.
/// </typeparam>
public class ObservableDictionaryWrap<TKey, TValue> : ObservableWrap<KeyValuePair<TKey, TValue>, IDictionary<TKey, TValue>>, IDictionary<TKey, TValue> where TKey : notnull
{
    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="ObservableDictionaryWrap{TKey, TValue}"/> class.
    /// </summary>
    public ObservableDictionaryWrap() { }

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="ObservableDictionaryWrap{TKey, TValue}"/> class.
    /// </summary>
    /// <param name="collection">
    /// Collection to use as the underlying dictionary.
    /// </param>
    public ObservableDictionaryWrap(IDictionary<TKey, TValue> collection) : base(collection) { }

    /// <summary>
    /// Gets or sets the value at the specified key in the dictionary.
    /// </summary>
    /// <param name="key">
    /// Key of the value to get or set.
    /// </param>
    /// <returns>
    /// The value found at the specified key in the dictionary.
    /// </returns>
    public TValue this[TKey key]
    {
        get => UnderlyingCollection is not null ? UnderlyingCollection[key] : default!;
        set
        {
            if (UnderlyingCollection is null) throw new InvalidOperationException();
            int index;
            var oldKvPair = UnderlyingCollection.AsEnumerable().WithIndex().SingleOrDefault(p => p.element.Key.Equals(key));
            index = oldKvPair.element.Key is not null ? oldKvPair.index : -1;
            UnderlyingCollection[key] = value;
            var newKvPair = UnderlyingCollection.AsEnumerable().WithIndex().SingleOrDefault(p => p.element.Key.Equals(key));
            if (oldKvPair.element.Key is null)
            {
                RaiseCollectionChanged(new NcchEa(NotifyCollectionChangedAction.Add, newKvPair.element, newKvPair.index));
            }
            else
            {
                if (index == -1) index = newKvPair.index;
                RaiseCollectionChanged(new NcchEa(NotifyCollectionChangedAction.Replace, newKvPair.element, oldKvPair.element, index));
            }
        }
    }

    /// <summary>
    /// Gets a collection containing all keys in the dictionary.
    /// </summary>
    public ICollection<TKey> Keys => UnderlyingCollection?.Keys ?? [];

    /// <summary>
    /// Gets a collection containing all values in the dictionary.
    /// </summary>
    public ICollection<TValue> Values => UnderlyingCollection?.Values ?? [];

    /// <summary>
    /// Adds a value to this dictionary with the specified key.
    /// </summary>
    /// <param name="key">
    /// Key to use.
    /// </param>
    /// <param name="value">
    /// Value to add to the dictionary.
    /// </param>
    public void Add(TKey key, TValue value)
    {
        if (UnderlyingCollection is null) throw new InvalidOperationException();
        UnderlyingCollection.Add(key, value);
        var (index, element) = UnderlyingCollection.AsEnumerable().WithIndex().Single(p => p.element.Key.Equals(key));
        RaiseCollectionChanged(new NcchEa(NotifyCollectionChangedAction.Add, element, index));
    }

    /// <summary>
    /// Determines whether the specified key exists in the dictionary.
    /// </summary>
    /// <param name="key">
    /// Key to look for in the dictionary.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the key exists in the dictionary;
    /// otherwise <see langword="false"/>.
    /// </returns>
    public bool ContainsKey(TKey key) => UnderlyingCollection?.ContainsKey(key) ?? false;

    /// <summary>
    /// Removes the element with the specified key from the dictionary.
    /// </summary>
    /// <param name="key">
    /// Key to remove.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the key was successfully removed from
    /// the dictionary; <see langword="false"/> if the key did not exist
    /// in the dictionary or if another error occurred while attempting
    /// the operation.
    /// </returns>
    /// 
    public bool Remove(TKey key)
    {
        if (UnderlyingCollection is null) throw new InvalidOperationException();
        if (!UnderlyingCollection.ContainsKey(key)) return false;
        var (index, element) = UnderlyingCollection.AsEnumerable().WithIndex().SingleOrDefault(p => p.element.Key.Equals(key));
        UnderlyingCollection.Remove(key);
        RaiseCollectionChanged(new NcchEa(NotifyCollectionChangedAction.Remove, element, index));
        return true;
    }

    /// <summary>
    /// Attempts to get a value from the dictionary.
    /// </summary>
    /// <param name="key">Key of the value to get.</param>
    /// <param name="value">
    /// Output parameter. Value obtained from the dictionary at the
    /// specified key.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the value was obtained from the
    /// dictionary successfully; <see langword="false"/> if the key did
    /// not exist in the dictionary or if another error occurred while
    /// obtaining the value.
    /// </returns>
    public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
    {
        if (UnderlyingCollection is not null)
        {
            return UnderlyingCollection.TryGetValue(key, out value!);
        }
        value = default!;
        return false;
    }
}
