﻿/*
OffendingException.cs

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
/// Excepción estándar producida al encontrarse un problema con un objeto.
/// </summary>
[Serializable]
public class OffendingException<T> : Exception
{
    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="OffendingException{T}" />.
    /// </summary>
    public OffendingException() : base(Errors.InvalidValue)
    {
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="OffendingException{T}" />.
    /// </summary>
    /// <param name="offendingObject">
    /// Objeto que es la causa de esta excepción.
    /// </param>
    public OffendingException(T offendingObject) : this()
    {
        OffendingObject = offendingObject;
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="OffendingException{T}" />.
    /// </summary>
    /// <param name="message">
    /// Un <see cref="string" /> que describe a la excepción.
    /// </param>
    public OffendingException(string message) : base(message)
    {
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="OffendingException{T}" />.
    /// </summary>
    /// <param name="message">
    /// Un <see cref="string" /> que describe a la excepción.
    /// </param>
    /// <param name="offendingObject">
    /// Objeto que es la causa de esta excepción.
    /// </param>
    public OffendingException(string message, T offendingObject) : base(message)
    {
        OffendingObject = offendingObject;
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="OffendingException{T}" />.
    /// </summary>
    /// <param name="inner">
    /// <see cref="Exception" /> que es la causa de esta excepción.
    /// </param>
    public OffendingException(Exception inner) : base(Errors.InvalidValue, inner)
    {
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="OffendingException{T}" />.
    /// </summary>
    /// <param name="inner">
    /// <see cref="Exception" /> que es la causa de esta excepción.
    /// </param>
    /// <param name="offendingObject">
    /// Objeto que es la causa de esta excepción.
    /// </param>
    public OffendingException(Exception inner, T offendingObject) : this(inner)
    {
        OffendingObject = offendingObject;
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="OffendingException{T}" />.
    /// </summary>
    /// <param name="message">
    /// Un <see cref="string" /> que describe a la excepción.
    /// </param>
    /// <param name="inner">
    /// <see cref="Exception" /> que es la causa de esta excepción.
    /// </param>
    public OffendingException(string message, Exception inner) : base(message, inner)
    {
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="OffendingException{T}" />.
    /// </summary>
    /// <param name="message">
    /// Un <see cref="string" /> que describe a la excepción.
    /// </param>
    /// <param name="inner">
    /// <see cref="Exception" /> que es la causa de esta excepción.
    /// </param>
    /// <param name="offendingObject">
    /// Objeto que es la causa de esta excepción.
    /// </param>
    public OffendingException(string message, Exception inner, T offendingObject) : base(message, inner)
    {
        OffendingObject = offendingObject;
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="OffendingException{T}" /> con datos serializados.
    /// </summary>
    /// <param name="context">
    /// El <see cref="StreamingContext" /> que contiene información
    /// contextual acerca del orígen o el destino.
    /// </param>
    /// <param name="info">
    /// El <see cref="SerializationInfo" /> que contiene la información
    /// serializada del objeto acerca de la excepción que está siendo
    /// lanzada.
    /// </param>
    protected OffendingException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="OffendingException{T}" /> con datos serializados.
    /// </summary>
    /// <param name="context">
    /// El <see cref="StreamingContext" /> que contiene información
    /// contextual acerca del orígen o el destino.
    /// </param>
    /// <param name="info">
    /// El <see cref="SerializationInfo" /> que contiene la información
    /// serializada del objeto acerca de la excepción que está siendo
    /// lanzada.
    /// </param>
    /// <param name="offendingObject">
    /// Objeto que es la causa de esta excepción.
    /// </param>
    protected OffendingException(SerializationInfo info, StreamingContext context, T offendingObject) : base(info,
        context)
    {
        OffendingObject = offendingObject;
    }

    /// <summary>
    /// Objeto que ha causado la excepción.
    /// </summary>
    public T OffendingObject { get; } = default!;
}
