/*
ClassNotInstantiableException.cs

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

using TheXDS.MCART.Resources.Strings;

namespace TheXDS.MCART.Exceptions;

/// <summary>
/// Exception that is thrown when it's not possible to create an instance of a
/// class.
/// </summary>
[Serializable]
public class ClassNotInstantiableException : OffendingException<Type?>
{
    private static string DefaultMessage(Type? offendingType = null)
    {
        if (offendingType is null) return Errors.ClassNotInstantiable;
        return string.Format(Errors.ClassXNotinstantiable, offendingType.Name);
    }

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="ClassNotInstantiableException" /> class.
    /// </summary>
    public ClassNotInstantiableException() : base(DefaultMessage()) { }

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="ClassNotInstantiableException" /> class.
    /// </summary>
    /// <param name="offendingType">
    /// Type that is the cause of this exception.
    /// </param>
    public ClassNotInstantiableException(Type? offendingType) : base(DefaultMessage(offendingType), offendingType) { }

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="ClassNotInstantiableException" /> class.
    /// </summary>
    /// <param name="message">
    /// Message that describes the exception.
    /// </param>
    public ClassNotInstantiableException(string message) : base(message) { }

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="ClassNotInstantiableException" /> class.
    /// </summary>
    /// <param name="message">
    /// Message that describes the exception.
    /// </param>
    /// <param name="offendingType">
    /// Type that is the cause of this exception.
    /// </param>
    public ClassNotInstantiableException(string message, Type? offendingType) : base(message, offendingType) { }

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="ClassNotInstantiableException" /> class.
    /// </summary>
    /// <param name="inner">
    /// <see cref="Exception" /> that is the cause of this exception.
    /// </param>
    public ClassNotInstantiableException(Exception inner) : base(DefaultMessage(), inner) { }

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="ClassNotInstantiableException" /> class.
    /// </summary>
    /// <param name="inner">
    /// <see cref="Exception" /> that is the cause of this exception.
    /// </param>
    /// <param name="offendingType">
    /// Type that is the cause of this exception.
    /// </param>
    public ClassNotInstantiableException(Exception inner, Type? offendingType) : base(DefaultMessage(offendingType), inner, offendingType) { }

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="ClassNotInstantiableException" /> class.
    /// </summary>
    /// <param name="message">
    /// Message that describes the exception.
    /// </param>
    /// <param name="inner">
    /// <see cref="Exception" /> that is the cause of this exception.
    /// </param>
    public ClassNotInstantiableException(string message, Exception inner) : base(message, inner) { }

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="ClassNotInstantiableException" /> class.
    /// </summary>
    /// <param name="message">
    /// Message that describes the exception.
    /// </param>
    /// <param name="inner">
    /// <see cref="Exception" /> that is the cause of this exception.
    /// </param>
    /// <param name="offendingType">
    /// Type that is the cause of this exception.
    /// </param>
    public ClassNotInstantiableException(string message, Exception inner, Type? offendingType) : base(message, inner, offendingType) { }
}
