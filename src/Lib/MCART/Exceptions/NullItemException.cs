/*
EmptyCollectionException.cs

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

namespace TheXDS.MCART.Exceptions;

/// <summary>
/// Exception that is thrown when an element in a collection is unexpectedly
/// equal to <see langword="null"/>.
/// </summary>
[Serializable]
public class NullItemException : OffendingException<IList>
{
    /// <summary>
    /// Gets the index at which an element was unexpectedly
    /// <see langword="null"/> inside of the collection.
    /// </summary>
    public int NullIndex { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="NullItemException"/>
    /// class.
    /// </summary>
    public NullItemException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="NullItemException"/>
    /// class.
    /// </summary>
    /// <param name="offendingCollection">
    /// Collection in which a <see langword="null"/> element has been found.
    /// </param>
    public NullItemException(IList offendingCollection) : base(offendingCollection)
    {
    }

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="NullItemException"/>.
    /// </summary>
    /// <param name="message">Message that describes the exception.</param>
    public NullItemException(string message) : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="NullItemException"/>.
    /// </summary>
    /// <param name="message">Message that describes the exception.</param>
    /// <param name="offendingCollection">
    /// Collection in which a <see langword="null"/> element has been found.
    /// </param>
    public NullItemException(string message, IList offendingCollection) : base(message, offendingCollection)
    {
    }

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="NullItemException"/>.
    /// </summary>
    /// <param name="inner">
    /// <see cref="Exception" /> that is the cause of this exception.
    /// </param>
    public NullItemException(Exception inner) : base(inner)
    {
    }

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="NullItemException"/>.
    /// </summary>
    /// <param name="inner">
    /// <see cref="Exception" /> that is the cause of this exception.
    /// </param>
    /// <param name="offendingCollection">
    /// Collection in which a <see langword="null"/> element has been found.
    /// </param>
    public NullItemException(Exception inner, IList offendingCollection) : base(inner, offendingCollection)
    {
    }

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="NullItemException"/>.
    /// </summary>
    /// <param name="message">Message that describes the exception.</param>
    /// <param name="inner">
    /// <see cref="Exception" /> that is the cause of this exception.
    /// </param>
    public NullItemException(string message, Exception inner) : base(message, inner)
    {
    }

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="NullItemException"/>.
    /// </summary>
    /// <param name="message">Message that describes the exception.</param>
    /// <param name="inner">
    /// <see cref="Exception" /> that is the cause of this exception.
    /// </param>
    /// <param name="offendingCollection">
    /// Collection in which a <see langword="null"/> element has been found.
    /// </param>
    public NullItemException(string message, Exception inner, IList offendingCollection) : base(message, inner, offendingCollection)
    {
    }
}
