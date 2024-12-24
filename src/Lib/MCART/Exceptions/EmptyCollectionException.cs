/*
EmptyCollectionException.cs

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

using System.Collections;
using TheXDS.MCART.Resources.Strings;

namespace TheXDS.MCART.Exceptions;

/// <summary>
/// Exception that is thrown when a collection is empty when a method required
/// it to have at lest one element.
/// </summary>
[Serializable]
public class EmptyCollectionException : OffendingException<IEnumerable>
{
    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="EmptyCollectionException" /> class.
    /// </summary>
    public EmptyCollectionException() : base(Errors.EmptyCollection)
    {
    }

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="EmptyCollectionException" /> class.
    /// </summary>
    /// <param name="offendingCollection">
    /// Empty collection that produced this exception.
    /// </param>
    public EmptyCollectionException(IEnumerable offendingCollection) : base(Errors.EmptyCollection, offendingCollection)
    {
    }

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="EmptyCollectionException" /> class.
    /// </summary>
    /// <param name="message">
    /// Message that describes the exception.
    /// </param>
    public EmptyCollectionException(string message) : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="EmptyCollectionException" /> class.
    /// </summary>
    /// <param name="message">
    /// Message that describes the exception.
    /// </param>
    /// <param name="offendingCollection">
    /// Empty collection that produced this exception.
    /// </param>
    public EmptyCollectionException(string message, IEnumerable offendingCollection) : base(message,
        offendingCollection)
    {
    }

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="EmptyCollectionException" /> class.
    /// </summary>
    /// <param name="inner">
    /// <see cref="Exception" /> that is the cause of this exception.
    /// </param>
    public EmptyCollectionException(Exception inner) : base(Errors.EmptyCollection, inner)
    {
    }

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="EmptyCollectionException" /> class.
    /// </summary>
    /// <param name="inner">
    /// <see cref="Exception" /> that is the cause of this exception.
    /// </param>
    /// <param name="offendingCollection">
    /// Empty collection that produced this exception.
    /// </param>
    public EmptyCollectionException(Exception inner, IEnumerable offendingCollection) : base(Errors.EmptyCollection, inner,
        offendingCollection)
    {
    }

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="EmptyCollectionException" /> class.
    /// </summary>
    /// <param name="message">
    /// Message that describes the exception.
    /// </param>
    /// <param name="inner">
    /// <see cref="Exception" /> that is the cause of this exception.
    /// </param>
    public EmptyCollectionException(string message, Exception inner) : base(message, inner)
    {
    }

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="EmptyCollectionException" /> class.
    /// </summary>
    /// <param name="message">
    /// Message that describes the exception.
    /// </param>
    /// <param name="inner">
    /// <see cref="Exception" /> that is the cause of this exception.
    /// </param>
    /// <param name="offendingCollection">
    /// Empty collection that produced this exception.
    /// </param>
    public EmptyCollectionException(string message, Exception inner, IEnumerable offendingCollection) : base(
        message, inner, offendingCollection)
    {
    }
}
