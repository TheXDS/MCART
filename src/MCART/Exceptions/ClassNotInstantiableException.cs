/*
ClassNotInstantiableException.cs

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
/// Excepción que se produce cuando no es posible instanciar un tipo.
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
    /// Inicializa una nueva instancia de la clase
    /// <see cref="ClassNotInstantiableException" />.
    /// </summary>
    public ClassNotInstantiableException() : base(DefaultMessage()) { }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="ClassNotInstantiableException" />.
    /// </summary>
    /// <param name="offendingType">
    /// Tipo que es la causa de esta excepción.
    /// </param>
    public ClassNotInstantiableException(Type? offendingType) : base(DefaultMessage(offendingType), offendingType) { }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="ClassNotInstantiableException" />.
    /// </summary>
    /// <param name="message">
    /// Un <see cref="string" /> que describe a la excepción.
    /// </param>
    public ClassNotInstantiableException(string message) : base(message) { }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="ClassNotInstantiableException" />.
    /// </summary>
    /// <param name="message">
    /// Un <see cref="string" /> que describe a la excepción.
    /// </param>
    /// <param name="offendingType">
    /// Tipo que es la causa de esta excepción.
    /// </param>
    public ClassNotInstantiableException(string message, Type? offendingType) : base(message, offendingType) { }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="ClassNotInstantiableException" />.
    /// </summary>
    /// <param name="inner">
    /// <see cref="Exception" /> secundaria producida por esta excepción.
    /// </param>
    public ClassNotInstantiableException(Exception inner) : base(DefaultMessage(), inner) { }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="ClassNotInstantiableException" />.
    /// </summary>
    /// <param name="inner">
    /// <see cref="Exception" /> secundaria producida por esta excepción.
    /// </param>
    /// <param name="offendingType">
    /// Tipo que es la causa de esta excepción.
    /// </param>
    public ClassNotInstantiableException(Exception inner, Type? offendingType) : base(DefaultMessage(offendingType), inner, offendingType) { }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="ClassNotInstantiableException" />.
    /// </summary>
    /// <param name="message">
    /// Un <see cref="string" /> que describe a la excepción.
    /// </param>
    /// <param name="inner">
    /// <see cref="Exception" /> secundaria producida por esta excepción.
    /// </param>
    public ClassNotInstantiableException(string message, Exception inner) : base(message, inner) { }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="ClassNotInstantiableException" />.
    /// </summary>
    /// <param name="message">
    /// Un <see cref="string" /> que describe a la excepción.
    /// </param>
    /// <param name="inner">
    /// <see cref="Exception" /> secundaria producida por esta excepción.
    /// </param>
    /// <param name="offendingType">
    /// Tipo que es la causa de esta excepción.
    /// </param>
    public ClassNotInstantiableException(string message, Exception inner, Type? offendingType) : base(message, inner, offendingType) { }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="ClassNotInstantiableException" /> con datos serializados.
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
    protected ClassNotInstantiableException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}
