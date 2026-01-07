/*
OpenList.cs

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

namespace TheXDS.MCART.Types;

/// <summary>
/// Class that represents a generic collection that can contain queue elements at the end.
/// </summary>
/// <typeparam name="T">
/// The type of elements in the collection.
/// </typeparam>
public class OpenList<T> : IList<T>
{
    private readonly List<T> _head = [];
    private readonly List<T> _tail = [];

    /// <summary>
    /// Gets or sets the element at the specified index.
    /// </summary>
    /// <param name="index">
    /// Index of the element to get or set. A negative index refers
    /// to an element in the queue of the <see cref="OpenList{T}"/>.
    /// </param>
    /// <returns>
    /// The element at the specified index.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Occurs if <paramref name="index"/> does not refer to a valid
    /// element within this <see cref="OpenList{T}"/>.
    /// </exception>
    public T this[int index]
    {
        get
        {
            return index < _head.Count ? _head[index] : _tail[index - _head.Count];
        }
        set
        {
            if (index < _head.Count)
            {
                _head[index] = value;
            }
            else
            {
                _tail[index - _head.Count] = value;
            }
        }
    }

    /// <summary>
    /// Gets the number of elements included in this <see cref="OpenList{T}"/>.
    /// </summary>
    public int Count => _head.Count + _tail.Count;

    /// <summary>
    /// Gets a value indicating whether this implementation of the
    /// interface <see cref="ICollection{T}"/> is read-only.
    /// </summary>
    /// <value>
    /// For class <see cref="OpenList{T}"/>, this property always returns <see langword="false"/>.
    /// </value>
    public bool IsReadOnly => false;

    /// <summary>
    /// Gets a value indicating whether this <see cref="OpenList{T}"/>
    /// contains a queue of elements at the end.
    /// </summary>
    public bool HasTail => _tail.Count != 0;

    /// <summary>
    /// Enumerates all elements from the head of this
    /// <see cref="OpenList{T}"/>.
    /// </summary>
    public IEnumerable<T> Head => [.. _head];

    /// <summary>
    /// Enumerates all elements from the tail of this
    /// <see cref="OpenList{T}"/>.
    /// </summary>
    public IEnumerable<T> Tail => [.. _tail];

    /// <summary>
    /// Adds an object to this <see cref="OpenList{T}"/>.
    /// </summary>
    /// <param name="item">
    /// Object that is going to be added to this
    /// <see cref="OpenList{T}"/>. The value can be
    /// <see langword="null"/> for reference types.
    /// </param>
    public void Add(T item)
    {
        _head.Add(item);
    }

    /// <summary>
    /// Adds an object to the end of this <see cref="OpenList{T}"/>, ensuring that future additions via the method
    /// <see cref="Add(T)"/> will not add elements after this one.
    /// </summary>
    /// <param name="item">
    /// Object that is going to be added to the end of this
    /// <see cref="OpenList{T}"/>. The value can be
    /// <see langword="null"/> for reference types.
    /// </param>
    public void AddTail(T item)
    {
        _tail.Add(item);
    }

    /// <summary>
    /// Removes all elements from this <see cref="OpenList{T}"/>.
    /// </summary>
    public void Clear()
    {
        _head.Clear();
        _tail.Clear();
    }

    /// <summary>
    /// Moves all elements from the tail to the head of the collection.
    /// </summary>
    public void JoinTail()
    {
        _head.AddRange(_tail);
        _tail.Clear();
    }

    /// <summary>
    /// Separates the collection of elements into a head and a tail at the specified index.
    /// </summary>
    /// <param name="index">
    /// Index of the element that will be the start of the tail.
    /// </param>
    public void SplitTail(int index)
    {
        if (index > _head.Count - 1) JoinTail();
        IEnumerable<T>? newTail = _head.Skip(index);
        _head.RemoveRange(index, _head.Count - index);
        _tail.InsertRange(0, newTail);
    }

    /// <summary>
    /// Determines whether an element is present in this <see cref="OpenList{T}"/>.
    /// </summary>
    /// <param name="item">
    /// Object to search for in this <see cref="OpenList{T}"/>. The value can be
    /// <see langword="null"/> for reference types.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if item is found in this <see cref="OpenList{T}"/>; otherwise,
    /// <see langword="false"/>.
    /// </returns>
    public bool Contains(T item)
    {
        return _head.Contains(item) || _tail.Contains(item);
    }

    /// <summary>
    /// Copies the entire contents of this <see cref="OpenList{T}"/> to a one-dimensional
    /// <see cref="Array"/>, starting at the specified index in the destination array.
    /// </summary>
    /// <param name="array">
    /// One-dimensional <see cref="Array"/> that is the destination for the copied elements from this
    /// <see cref="OpenList{T}"/>. The <see cref="Array"/> must have zero-based indexing.
    /// </param>
    /// <param name="arrayIndex">
    /// Zero-based index in <paramref name="array"/> at which copying begins.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// The value of <paramref name="array"/> is <see langword="null"/>.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="arrayIndex"/> is less than zero.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// The number of elements in the source <see cref="OpenList{T}"/> is greater than
    /// the available space from <paramref name="arrayIndex"/> to the end of the destination
    /// <paramref name="array"/>.
    /// </exception>
    public void CopyTo(T[] array, int arrayIndex)
    {
        _head.Concat(_tail).ToArray().CopyTo(array, arrayIndex);
    }

    /// <summary>
    /// Returns an enumerator that iterates through this <see cref="OpenList{T}"/>.
    /// </summary>
    /// <returns>
    /// An <see cref="IEnumerator{T}"/> for this <see cref="OpenList{T}"/>.
    /// </returns>
    public IEnumerator<T> GetEnumerator()
    {
        return _head.Concat(_tail).GetEnumerator();
    }

    /// <summary>
    /// Removes the first occurrence of a specific object from this <see cref="OpenList{T}"/>.
    /// </summary>
    /// <param name="item">
    /// Object to remove from this <see cref="OpenList{T}"/>. The value can be
    /// <see langword="null"/> for reference types.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the <paramref name="item"/> is successfully removed;
    /// otherwise, <see langword="false"/>. This method also returns <see langword="false"/>
    /// if the <paramref name="item"/> was not found in this <see cref="OpenList{T}"/>.
    /// </returns>
    public bool Remove(T item)
    {
        return _head.Remove(item) || _tail.Remove(item);
    }

    /// <summary>
    /// Inserts an element into this <see cref="OpenList{T}"/> at the specified index.
    /// </summary>
    /// <param name="index">
    /// Index at which to insert the <paramref name="item"/>.
    /// </param>
    /// <param name="item">
    /// Object to insert. The value can be <see langword="null"/> for reference types.
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Occurs if <paramref name="index"/> does not refer to a valid element within this
    /// <see cref="OpenList{T}"/>.
    /// </exception>
    public void Insert(int index, T item)
    {
        if (index < _head.Count)
        {
            _head.Insert(index, item);
        }
        else
        {
            _tail.Insert(index - _head.Count, item);
        }
    }

    /// <summary>
    /// Removes the element at the specified index from this <see cref="OpenList{T}"/>.
    /// </summary>
    /// <param name="index">
    /// Index of the element to remove.
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Occurs if <paramref name="index"/> does not refer to a valid element within this
    /// <see cref="OpenList{T}"/>.
    /// </exception>
    public void RemoveAt(int index)
    {
        if (index < _head.Count)
        {
            _head.RemoveAt(index);
        }
        else
        {
            _tail.RemoveAt(index - _head.Count);
        }
    }

    /// <summary>
    /// Determines the zero-based index of a specific element within this <see cref="IList{T}"/>.
    /// </summary>
    /// <param name="item">
    /// Object to search for in the <see cref="IList{T}"/>.
    /// </param>
    /// <returns>
    /// The index of <paramref name="item"/> if it is found in the list; otherwise, returns -1.
    /// </returns>
    public int IndexOf(T item)
    {
        return _head.Concat(_tail).ToList().IndexOf(item);
    }

    /// <summary>
    /// Returns an enumerator that iterates through a collection.
    /// </summary>
    /// <returns>
    /// An <see cref="IEnumerator"/> that can be used to iterate through the collection.
    /// </returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)_head.Concat(_tail)).GetEnumerator();
    }
}
