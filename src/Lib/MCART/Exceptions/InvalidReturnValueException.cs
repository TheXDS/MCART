/*
InvalidReturnValueException.cs

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
using static TheXDS.MCART.Resources.Strings.Errors;

namespace TheXDS.MCART.Exceptions;

/// <summary>
/// Excepción que se produce cuando se detecta que una función ha devuelto
/// un valor inválido sin generar una excepción.
/// </summary>
[Serializable]
public class InvalidReturnValueException : Exception
{
    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="InvalidReturnValueException" />.
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
    protected InvalidReturnValueException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="InvalidReturnValueException" />.
    /// </summary>
    public InvalidReturnValueException() : base(InvalidReturnValue)
    {
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="InvalidReturnValueException" /> especificando el delegado
    /// que causó esta excepción.
    /// </summary>
    /// <param name="call">
    /// <see cref="Delegate" /> cuyo resultado causó la excepción.
    /// </param>
    public InvalidReturnValueException(Delegate call) : this(call.Method.Name)
    {
        OffendingFunction = call;
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="InvalidReturnValueException" /> especificando el delegado
    /// que causó esta excepción.
    /// </summary>
    /// <param name="methodName">
    /// Nombre del método cuyo resultado ha causado la excepción.
    /// </param>
    public InvalidReturnValueException(string methodName) : base(string.Format(InvalidFuncReturnValue, methodName))
    {
        OffendingFunctionName = methodName;
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="InvalidReturnValueException" /> especificando el delegado
    /// que causó esta excepción.
    /// </summary>
    /// <param name="call">
    /// <see cref="Delegate" /> cuyo resultado causó la excepción.
    /// </param>
    /// <param name="returnValue">
    /// Valor inválido devuelto por <paramref name="call" />.
    /// </param>
    public InvalidReturnValueException(Delegate call, object? returnValue) : this(call.Method.Name, returnValue)
    {
        OffendingFunction = call;
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="InvalidReturnValueException" /> especificando el delegado
    /// que causó esta excepción.
    /// </summary>
    /// <param name="methodName">
    /// Nombre del método cuyo resultado ha causado la excepción.
    /// </param>
    /// <param name="returnValue">
    /// Valor inválido devuelto por el método.
    /// </param>
    public InvalidReturnValueException(string methodName, object? returnValue) : this(methodName, returnValue, null)
    {
        OffendingFunctionName = methodName;
        OffendingReturnValue = returnValue;
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="InvalidReturnValueException" />.
    /// </summary>
    /// <param name="message">
    /// Un <see cref="string" /> que describe a la excepción.
    /// </param>
    /// <param name="inner">
    /// <see cref="Exception" /> que es la causa de esta excepción.
    /// </param>
    public InvalidReturnValueException(string message, Exception? inner) : base(message, inner)
    {
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="InvalidReturnValueException" /> especificando el delegado
    /// que causó esta excepción.
    /// </summary>
    /// <param name="call">
    /// <see cref="Delegate" /> cuyo resultado causó la excepción.
    /// </param>
    /// <param name="returnValue">
    /// Valor inválido devuelto por <paramref name="call" />.
    /// </param>
    /// <param name="inner">
    /// <see cref="Exception" /> que es la causa de esta excepción.
    /// </param>
    public InvalidReturnValueException(Delegate call, object? returnValue, Exception? inner) : this(call.Method.Name, returnValue, inner)
    {
        OffendingFunction = call;
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="InvalidReturnValueException" /> especificando el delegado
    /// que causó esta excepción.
    /// </summary>
    /// <param name="methodName">
    /// Nombre del método cuyo resultado ha causado la excepción.
    /// </param>
    /// <param name="returnValue">
    /// Valor inválido devuelto por el método.
    /// </param>
    /// <param name="inner">
    /// <see cref="Exception" /> que es la causa de esta excepción.
    /// </param>
    public InvalidReturnValueException(string methodName, object? returnValue, Exception? inner) : base(string.Format(InvalidFuncXReturnValue, methodName, returnValue ?? Common.Null), inner)
    {
        OffendingFunctionName = methodName;
        OffendingReturnValue = returnValue;
    }

    /// <summary>
    /// Obtiene uan referencia al método que ha causado la excepción.
    /// </summary>
    /// <returns>
    /// Un <see cref="Delegate" /> que representa un método cuyo valor
    /// devuelto causó la excepción.
    /// </returns>
    public Delegate? OffendingFunction { get; }

    /// <summary>
    /// Obtiene el nombre del método que ha causado la excepción.
    /// </summary>
    /// <returns>
    /// El nombre de la función cuyo resultado causó la excepción.
    /// </returns>
    public string? OffendingFunctionName { get; }

    /// <summary>
    /// Obtiene el valor devuelto que ha causado la excepción.
    /// </summary>
    /// <returns>
    /// El valor inválido devuelto por método, causante de la excepción.
    /// </returns>
    public object? OffendingReturnValue { get; }
}
