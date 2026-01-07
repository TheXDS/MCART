/*
InvalidUriException.cs

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
/// Exception that is thrown when an <see cref="Uri"/> does not point to a
/// valid resource.
/// </summary>
[Serializable]
public class InvalidUriException : OffendingException<Uri>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidUriException"/>
    /// class.
    /// </summary>
    public InvalidUriException() : base(Msg())
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidUriException"/>
    /// class.
    /// </summary>
    /// <param name="offendingUri">
    /// <see cref="Uri"/> that pointed to an invalid resource.
    /// </param>
    public InvalidUriException(Uri offendingUri) : base(Msg(offendingUri), offendingUri)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidUriException"/>
    /// class.
    /// </summary>
    /// <param name="message">
    /// Message that describes the exception.
    /// </param>
    public InvalidUriException(string message) : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidUriException"/>
    /// class.
    /// </summary>
    /// <param name="message">
    /// Message that describes the exception.
    /// </param>
    /// <param name="offendingUri">
    /// <see cref="Uri"/> that pointed to an invalid resource.
    /// </param>
    public InvalidUriException(string message, Uri offendingUri) : base(message, offendingUri)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidUriException"/>
    /// class.
    /// </summary>
    /// <param name="inner">
    /// <see cref="Exception" /> that is the cause of this exception.
    /// </param>
    public InvalidUriException(Exception inner) : base(inner)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidUriException"/>
    /// class.
    /// </summary>
    /// <param name="message">
    /// Message that describes the exception.
    /// </param>
    /// <param name="inner">
    /// <see cref="Exception" /> that is the cause of this exception.
    /// </param>
    public InvalidUriException(string message, Exception inner) : base(message, inner)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidUriException"/>
    /// class.
    /// </summary>
    /// <param name="inner">
    /// <see cref="Exception" /> that is the cause of this exception.
    /// </param>
    /// <param name="offendingUri">
    /// <see cref="Uri"/> that pointed to an invalid resource.
    /// </param>
    public InvalidUriException(Exception inner, Uri offendingUri) : base(inner, offendingUri)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidUriException"/>
    /// class.
    /// </summary>
    /// <param name="message">
    /// Message that describes the exception.
    /// </param>
    /// <param name="inner">
    /// <see cref="Exception" /> that is the cause of this exception.
    /// </param>
    /// <param name="offendingUri">
    /// <see cref="Uri"/> that pointed to an invalid resource.
    /// </param>
    public InvalidUriException(string message, Exception inner, Uri offendingUri) : base(message, inner, offendingUri)
    {
    }

    private static string Msg() => Errors.InvalidUri;
    private static string Msg(Uri uri) => string.Format(Errors.InvalidXUri, uri.ToString());
}
