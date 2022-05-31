/*
InvalidUriException.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2022 César Andrés Morgan

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
using System;
using System.Runtime.Serialization;
using TheXDS.MCART.Resources.Strings;

/// <summary>
/// Excepción que se produce cuando un <see cref="Uri"/> no hace
/// referencia a un recurso válido.
/// </summary>
[Serializable]
public class InvalidUriException : OffendingException<Uri>
{
    /// <summary>
    /// Inicializa una nueva instancia de la clase 
    /// <see cref="InvalidUriException"/>.
    /// </summary>
    public InvalidUriException() : base(Msg())
    {
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase 
    /// <see cref="InvalidUriException"/>.
    /// </summary>
    /// <param name="offendingUri">
    /// <see cref="Uri"/> que apunta a un recurso inválido.
    /// </param>
    public InvalidUriException(Uri offendingUri) : base(Msg(offendingUri), offendingUri)
    {
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase 
    /// <see cref="InvalidUriException"/>.
    /// </summary>
    /// <param name="message">
    /// Un <see cref="string" /> que describe a la excepción.
    /// </param>
    public InvalidUriException(string message) : base(message)
    {
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase 
    /// <see cref="InvalidUriException"/>.
    /// </summary>
    /// <param name="message">
    /// Un <see cref="string" /> que describe a la excepción.
    /// </param>
    /// <param name="offendingUri">
    /// <see cref="Uri"/> que apunta a un recurso inválido.
    /// </param>
    public InvalidUriException(string message, Uri offendingUri) : base(message, offendingUri)
    {
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase 
    /// <see cref="InvalidUriException"/>.
    /// </summary>
    /// <param name="inner">
    /// <see cref="Exception" /> que es la causa de esta excepción.
    /// </param>
    public InvalidUriException(Exception inner) : base(inner)
    {
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase 
    /// <see cref="InvalidUriException"/>.
    /// </summary>
    /// <param name="message">
    /// Un <see cref="string" /> que describe a la excepción.
    /// </param>
    /// <param name="inner">
    /// <see cref="Exception" /> que es la causa de esta excepción.
    /// </param>
    public InvalidUriException(string message, Exception inner) : base(message, inner)
    {
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase 
    /// <see cref="InvalidUriException"/>.
    /// </summary>
    /// <param name="inner">
    /// <see cref="Exception" /> que es la causa de esta excepción.
    /// </param>
    /// <param name="offendingUri">
    /// <see cref="Uri"/> que apunta a un recurso inválido.
    /// </param>
    public InvalidUriException(Exception inner, Uri offendingUri) : base(inner, offendingUri)
    {
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase 
    /// <see cref="InvalidUriException"/>.
    /// </summary>
    /// <param name="message">
    /// Un <see cref="string" /> que describe a la excepción.
    /// </param>
    /// <param name="inner">
    /// <see cref="Exception" /> que es la causa de esta excepción.
    /// </param>
    /// <param name="offendingUri">
    /// <see cref="Uri"/> que apunta a un recurso inválido.
    /// </param>
    public InvalidUriException(string message, Exception inner, Uri offendingUri) : base(message, inner, offendingUri)
    {
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="InvalidUriException" />.
    /// </summary>
    /// <param name="info">
    /// El objeto que contiene la información de serialización.
    /// </param>
    /// <param name="context">
    /// La información contextual acerca del orígen o el destino.
    /// </param>
    protected InvalidUriException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    private static string Msg() => Errors.InvalidUri;
    private static string Msg(Uri uri) => string.Format(Errors.InvalidXUri, uri.ToString());
}
