/*
OffendingException.cs

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

using TheXDS.MCART.Resources.Strings;

namespace TheXDS.MCART.Exceptions;

/// <summary>
/// Describes an <see cref="Exception"/> that is thrown when a problem has been
/// found with an object, as well as being a base class for exceptions that
/// include information on the object or value that caused them.
/// </summary>
[Serializable]
public class OffendingException<T> : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="OffendingException{T}" />
    /// class.
    /// </summary>
    public OffendingException() : base(Errors.InvalidValue)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="OffendingException{T}" />
    /// class.
    /// </summary>
    /// <param name="offendingObject">
    /// Object/value that is the cause of the exception.
    /// </param>
    public OffendingException(T offendingObject) : this()
    {
        OffendingObject = offendingObject;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="OffendingException{T}" />
    /// class.
    /// </summary>
    /// <param name="message">
    /// Message that describes the exception.
    /// </param>
    public OffendingException(string message) : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="OffendingException{T}" />
    /// class.
    /// </summary>
    /// <param name="message">
    /// Message that describes the exception.
    /// </param>
    /// <param name="offendingObject">
    /// Object/value that is the cause of the exception.
    /// </param>
    public OffendingException(string message, T offendingObject) : base(message)
    {
        OffendingObject = offendingObject;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="OffendingException{T}" />
    /// class.
    /// </summary>
    /// <param name="inner">
    /// <see cref="Exception" /> that is the cause of this exception.
    /// </param>
    public OffendingException(Exception inner) : base(Errors.InvalidValue, inner)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="OffendingException{T}" />
    /// class.
    /// </summary>
    /// <param name="inner">
    /// <see cref="Exception" /> that is the cause of this exception.
    /// </param>
    /// <param name="offendingObject">
    /// Object/value that is the cause of the exception.
    /// </param>
    public OffendingException(Exception inner, T offendingObject) : this(inner)
    {
        OffendingObject = offendingObject;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="OffendingException{T}" />
    /// class.
    /// </summary>
    /// <param name="message">
    /// Message that describes the exception.
    /// </param>
    /// <param name="inner">
    /// <see cref="Exception" /> that is the cause of this exception.
    /// </param>
    public OffendingException(string message, Exception inner) : base(message, inner)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="OffendingException{T}" />
    /// class.
    /// </summary>
    /// <param name="message">
    /// Message that describes the exception.
    /// </param>
    /// <param name="inner">
    /// <see cref="Exception" /> that is the cause of this exception.
    /// </param>
    /// <param name="offendingObject">
    /// Object/value that is the cause of the exception.
    /// </param>
    public OffendingException(string message, Exception inner, T offendingObject) : base(message, inner)
    {
        OffendingObject = offendingObject;
    }

    /// <summary>
    /// Gets the object/value that is the cause of the exception.
    /// </summary>
    public T OffendingObject { get; } = default!;
}
