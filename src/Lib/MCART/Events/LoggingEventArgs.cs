/*
LoggingEventArgs.cs

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

using static TheXDS.MCART.Misc.Internals;

namespace TheXDS.MCART.Events;

/// <summary>
/// Incluye información de evento para cualquier clase con eventos de
/// logging (bitácora).
/// </summary>
public class LoggingEventArgs : ValueEventArgs<string>
{
    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="LoggingEventArgs" />, sin definir un objeto relacionado.
    /// </summary>
    /// <param name="message">Mensaje de esta entrada de log.</param>
    /// <exception cref="ArgumentException">
    /// Se produce si <paramref name="message"/> es <see langword="null"/> o
    /// una cadena vacía.
    /// </exception>
    public LoggingEventArgs(string message) : base(message)
    {
        EmptyCheck(message, nameof(message));
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="LoggingEventArgs" />, definiendo un objeto relacionado.
    /// </summary>
    /// <param name="subject">
    /// Objeto relacionado a esta entrada de log.</param>
    /// <param name="message">Mensaje de esta entrada de log.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Se produce si <paramref name="subject"/> es <see langword="null"/>.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// Se produce si <paramref name="message"/> es <see langword="null"/> o
    /// una cadena vacía.
    /// </exception>
    public LoggingEventArgs(object subject, string message) : this(message)
    {
        NullCheck(subject, nameof(subject));
        Subject = subject;
    }

    /// <summary>
    /// Objeto relacionado a esta entrada de log.
    /// </summary>
    public object? Subject { get; }
}
