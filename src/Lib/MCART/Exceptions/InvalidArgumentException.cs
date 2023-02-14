﻿/*
InvalidArgumentException.cs

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
using System.Runtime.Serialization;
using TheXDS.MCART.Resources.Strings;

namespace TheXDS.MCART.Exceptions;

/// <summary>
/// Se produce cuando un argumento contiene un valor inválido y ninguna de
/// las clases derivadas de <see cref="ArgumentException"/> describe
/// adecuadamente el error.
/// </summary>
[Serializable]
public class InvalidArgumentException : ArgumentException
{
    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="InvalidArgumentException" />.
    /// </summary>
    /// <param name="context">
    /// El <see cref="StreamingContext" /> que contiene información
    /// contextual acerca del origen o el destino.
    /// </param>
    /// <param name="info">
    /// El <see cref="SerializationInfo" /> que contiene la información
    /// serializada del objeto acerca de la excepción que está siendo
    /// lanzada.
    /// </param>
    protected InvalidArgumentException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="InvalidArgumentException" />.
    /// </summary>
    public InvalidArgumentException() : base(Errors.InvalidValue)
    {
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="InvalidArgumentException" />.
    /// </summary>
    /// <param name="argumentName">Nombre del argumento inválido.</param>
    public InvalidArgumentException(string argumentName) : base(string.Format(Errors.InvalidArgument, argumentName))
    {
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="InvalidArgumentException" />.
    /// </summary>
    /// <param name="message">
    /// Un <see cref="string" /> que describe a la excepción.
    /// </param>
    /// <param name="argumentName">Nombre del argumento inválido.</param>
    public InvalidArgumentException(string message, string argumentName) : base(message, argumentName)
    {
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="InvalidArgumentException" />.
    /// </summary>
    /// <param name="inner">
    /// <see cref="Exception" /> que es la causa de esta excepción.
    /// </param>
    public InvalidArgumentException(Exception inner) : base(Errors.InvalidValue, inner)
    {
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="InvalidArgumentException" />.
    /// </summary>
    /// <param name="inner">
    /// <see cref="Exception" /> que es la causa de esta excepción.
    /// </param>
    /// <param name="argumentName">Nombre del argumento inválido.</param>
    public InvalidArgumentException(Exception inner, string argumentName) : base(string.Format(Errors.InvalidArgument, argumentName), inner)
    {
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="InvalidArgumentException" />.
    /// </summary>
    /// <param name="message">
    /// Un <see cref="string" /> que describe a la excepción.
    /// </param>
    /// <param name="inner">
    /// <see cref="Exception" /> que es la causa de esta excepción.
    /// </param>
    public InvalidArgumentException(string message, Exception inner) : base(message, inner)
    {
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="InvalidArgumentException" />.
    /// </summary>
    /// <param name="message">
    /// Un <see cref="string" /> que describe a la excepción.
    /// </param>
    /// <param name="inner">
    /// <see cref="Exception" /> que es la causa de esta excepción.
    /// </param>
    /// <param name="argumentName">Nombre del argumento faltante.</param>
    public InvalidArgumentException(string message, Exception inner, string argumentName) : base(message,
        argumentName, inner)
    {
    }
}