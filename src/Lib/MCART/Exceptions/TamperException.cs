/*
TamperException.cs

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
/// Se produce cuando una función soportada detecta una de las 
/// siguientes situaciones:
/// <list type="bullet">
/// <item>
///     <description>
///         Valores de retorno alterados inesperadamente.
///     </description> 
/// </item>
/// <item>
///     <description>
///         Valor de retorno fuera del rango conocido esperado de
///         una función.
///     </description>
/// </item>
/// <item>
///     <description>
///         Corrupción de memoria no capturada por CLR.
///     </description>
/// </item>
/// <item>
///     <description>
///         Modificación externa de valores internos protegidos de
///         la aplicación.
///     </description>
/// </item>
/// <item>
///     <description>
///         Acceso inesperado a métodos internos.
///     </description>
/// </item>
/// </list>
/// </summary>
[Serializable]
public class TamperException : Exception
{
    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="TamperException" />.
    /// </summary>
    public TamperException() : base(Errors.TamperDetected)
    {
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="TamperException" />.
    /// </summary>
    /// <param name="message">
    /// Un <see cref="string" /> que describe a la excepción.
    /// </param>
    public TamperException(string message) : base(message)
    {
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="TamperException" />.
    /// </summary>
    /// <param name="inner">
    /// <see cref="Exception" /> que es la causa de esta excepción.
    /// </param>
    public TamperException(Exception inner) : this(Errors.TamperDetected, inner)
    {
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="TamperException" />.
    /// </summary>
    /// <param name="message">
    /// Un <see cref="string" /> que describe a la excepción.
    /// </param>
    /// <param name="inner">
    /// <see cref="Exception" /> que es la causa de esta excepción.
    /// </param>
    public TamperException(string message, Exception inner) : base(message, inner)
    {
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="TamperException" /> con datos serializados.
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
    protected TamperException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
