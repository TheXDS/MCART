/*
InvalidMethodSignatureException.cs

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

using System.Reflection;
using System.Runtime.Serialization;
using static TheXDS.MCART.Resources.Strings.Errors;

namespace TheXDS.MCART.Exceptions;

/// <summary>
/// Excepción que se produce cuando la firma de un método representado en un
/// <see cref="MethodInfo" /> no es válida.
/// </summary>
[Serializable]
public class InvalidMethodSignatureException : OffendingException<MethodInfo>
{
    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="InvalidMethodSignatureException" />.
    /// </summary>
    public InvalidMethodSignatureException() : base(Msg())
    {
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="InvalidMethodSignatureException" />.
    /// </summary>
    /// <param name="offendingMethod">Referencia al método que ha causado la excepción.</param>
    public InvalidMethodSignatureException(MethodInfo offendingMethod) : base(Msg(offendingMethod), offendingMethod)
    {
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase 
    /// <see cref="InvalidMethodSignatureException" />.
    /// </summary>
    /// <param name="message">
    /// Un <see cref="string" /> que describe a la excepción.
    /// </param>
    public InvalidMethodSignatureException(string message) : base(message)
    {
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="InvalidMethodSignatureException" />.
    /// </summary>
    /// <param name="message">
    /// Un <see cref="string" /> que describe a la excepción.
    /// </param>
    /// <param name="offendingMethod">Referencia al método que ha causado la excepción.</param>
    public InvalidMethodSignatureException(string message, MethodInfo offendingMethod) : base(message,
        offendingMethod)
    {
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="InvalidMethodSignatureException" />.
    /// </summary>
    /// <param name="inner">
    /// <see cref="Exception" /> que es la causa de esta excepción.
    /// </param>
    public InvalidMethodSignatureException(Exception inner) : base(Msg(), inner)
    {
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="InvalidMethodSignatureException" />.
    /// </summary>
    /// <param name="inner">
    /// <see cref="Exception" /> que es la causa de esta excepción.
    /// </param>
    /// <param name="offendingMethod">Referencia al método que ha causado la excepción.</param>
    public InvalidMethodSignatureException(Exception inner, MethodInfo offendingMethod) : base(Msg(offendingMethod),
        inner, offendingMethod)
    {
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="InvalidMethodSignatureException" />.
    /// </summary>
    /// <param name="message">
    /// Un <see cref="string" /> que describe a la excepción.
    /// </param>
    /// <param name="inner">
    /// <see cref="Exception" /> que es la causa de esta excepción.
    /// </param>
    public InvalidMethodSignatureException(string message, Exception inner) : base(message, inner)
    {
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="InvalidMethodSignatureException" />.
    /// </summary>
    /// <param name="message">
    /// Un <see cref="string" /> que describe a la excepción.
    /// </param>
    /// <param name="inner">
    /// <see cref="Exception" /> que es la causa de esta excepción.
    /// </param>
    /// <param name="offendingMethod">Referencia al método que ha causado la excepción.</param>
    public InvalidMethodSignatureException(string message, Exception inner, MethodInfo offendingMethod) : base(
        message, inner, offendingMethod)
    {
    }

    private static string Msg()
    {
        return InvalidMethodSignature;
    }

    private static string Msg(MemberInfo offendingMethod)
    {
        return string.Format(InvalidMethodXSignature, $"{offendingMethod.DeclaringType?.FullName}.{offendingMethod.Name}");
    }
}
