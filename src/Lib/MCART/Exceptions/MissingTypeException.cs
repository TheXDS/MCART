/*
MissingTypeException.cs

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

namespace TheXDS.MCART.Exceptions;

/// <summary>
/// Exception that is thrown when a required type is not found.
/// </summary>
[Serializable]
public class MissingTypeException : OffendingException<Type>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MissingTypeException" />
    /// class.
    /// </summary>
    public MissingTypeException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MissingTypeException" />
    /// class.
    /// </summary>
    /// <param name="missingType">
    /// Definition for the missing type that caused the exception.
    /// </param>
    public MissingTypeException(Type missingType) : base(missingType)
    {
    }

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="MissingTypeException" /> class.
    /// </summary>
    /// <param name="message">
    /// Message that describes the exception.
    /// </param>
    public MissingTypeException(string message) : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MissingTypeException" />
    /// class.
    /// </summary>
    /// <param name="inner">
    /// <see cref="Exception" /> that is the cause of this exception.
    /// </param>
    public MissingTypeException(Exception inner) : base(inner)
    {
    }

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="MissingTypeException" /> class.
    /// </summary>
    /// <param name="message">
    /// Message that describes the exception.
    /// </param>
    /// <param name="missingType">
    /// Definition for the missing type that caused the exception.
    /// </param>
    public MissingTypeException(string message, Type missingType) : base(message, missingType)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MissingTypeException" />
    /// class.
    /// </summary>
    /// <param name="inner">
    /// <see cref="Exception" /> that is the cause of this exception.
    /// </param>
    /// <param name="missingType">
    /// Definition for the missing type that caused the exception.
    /// </param>
    public MissingTypeException(Exception inner, Type missingType) : base(inner, missingType)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MissingTypeException" />.
    /// class.
    /// </summary>
    /// <param name="message">Message that describes the exception.</param>
    /// <param name="inner">
    /// <see cref="Exception" /> that is the cause of this exception.
    /// </param>
    public MissingTypeException(string message, Exception inner) : base(message, inner)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MissingTypeException" />.
    /// class.
    /// </summary>
    /// <param name="message">Message that describes the exception.</param>
    /// <param name="inner">
    /// <see cref="Exception" /> that is the cause of this exception.
    /// </param>
    /// <param name="missingType">
    /// Definition for the missing type that caused the exception.
    /// </param>
    public MissingTypeException(string message, Exception inner, Type missingType) : base(message, inner, missingType)
    {
    }
}
