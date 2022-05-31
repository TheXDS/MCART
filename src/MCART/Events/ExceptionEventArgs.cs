/*
ExceptionEventArgs.cs

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

namespace TheXDS.MCART.Events;
using System;

/// <summary>
/// Incluye información de evento para cualquier clase con eventos de
/// excepción.
/// </summary>
public class ExceptionEventArgs : ValueEventArgs<Exception?>
{
    /// <summary>
    /// Inicializa una nueva instancia de este objeto sin especificar
    /// una excepción producida.
    /// </summary>
    public ExceptionEventArgs() : base(null)
    {
    }

    /// <summary>
    /// Inicializa una nueva instancia de este objeto con la excepción
    /// especificada.
    /// </summary>
    /// <param name="ex">
    /// <see cref="Exception" /> que se ha producido en el código.
    /// </param>
    public ExceptionEventArgs(Exception? ex) : base(ex)
    {
    }

    /// <summary>
    /// Convierte implícitamente un <see cref="Exception"/> en un
    /// <see cref="ExceptionEventArgs"/>.
    /// </summary>
    /// <param name="ex">
    /// <see cref="Exception"/> a partir de la cual crear el nuevo
    /// <see cref="ExceptionEventArgs"/>.
    /// </param>
    public static implicit operator ExceptionEventArgs(Exception ex) => new(ex);
}
