/*
EmptyCollectionException.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2023 César Andrés Morgan

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

using System;
using System.Collections;
using System.Runtime.Serialization;

namespace TheXDS.MCART.Exceptions;

/// <summary>
/// Se produce cuando un elemento de una colección es
/// <see langword="null"/> de manera inesperada.
/// </summary>
[Serializable]
public class NullItemException : OffendingException<IList>
{
    /// <summary>
    /// Obtiene el índice del elemento que ha producido esta excepción.
    /// </summary>
    /// <value>
    /// El índice del elemento que es <see langword="null"/>.
    /// </value>
    public int NullIndex { get; set; }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="NullItemException"/>.
    /// </summary>
    public NullItemException()
    {
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="NullItemException"/>.
    /// </summary>
    /// <param name="offendingObject">
    /// Lista en la cual se ha encontrado un elemento nulo.
    /// </param>
    public NullItemException(IList offendingObject) : base(offendingObject)
    {
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="NullItemException"/>.
    /// </summary>
    /// <param name="message">Mensaje descriptivo de la excepción.</param>
    /// <param name="offendingObject">
    /// Lista en la cual se ha encontrado un elemento nulo.
    /// </param>
    public NullItemException(string message, IList offendingObject) : base(message, offendingObject)
    {
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="NullItemException"/>.
    /// </summary>
    /// <param name="inner">
    /// Excepción que es la causa de esta excepción.
    /// </param>
    public NullItemException(Exception inner) : base(inner)
    {
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="NullItemException"/>.
    /// </summary>
    /// <param name="inner">
    /// Excepción que es la causa de esta excepción.
    /// </param>
    /// <param name="offendingObject">
    /// Lista en la cual se ha encontrado un elemento nulo.
    /// </param>
    public NullItemException(Exception inner, IList offendingObject) : base(inner, offendingObject)
    {
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="NullItemException"/>.
    /// </summary>
    /// <param name="message">Mensaje descriptivo de la excepción.</param>
    /// <param name="inner">
    /// Excepción que es la causa de esta excepción.
    /// </param>
    public NullItemException(string message, Exception inner) : base(message, inner)
    {
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="NullItemException"/>.
    /// </summary>
    /// <param name="message">Mensaje descriptivo de la excepción.</param>
    /// <param name="inner">
    /// Excepción que es la causa de esta excepción.
    /// </param>
    /// <param name="offendingObject">
    /// Lista en la cual se ha encontrado un elemento nulo.
    /// </param>
    public NullItemException(string message, Exception inner, IList offendingObject) : base(message, inner, offendingObject)
    {
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="NullItemException"/>.
    /// </summary>
    /// <param name="info">
    /// Objeto que contiene la información de serialización de esta
    /// instancia.
    /// </param>
    /// <param name="context">
    /// Información de contexto de serialización.
    /// </param>
    protected NullItemException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="NullItemException"/>.
    /// </summary>
    /// <param name="info">
    /// Objeto que contiene la información de serialización de esta
    /// instancia.
    /// </param>
    /// <param name="context">
    /// Información de contexto de serialización.
    /// </param>
    /// <param name="offendingObject">
    /// Lista en la cual se ha encontrado un elemento nulo.
    /// </param>
    protected NullItemException(SerializationInfo info, StreamingContext context, IList offendingObject) : base(info, context, offendingObject)
    {
    }
}
