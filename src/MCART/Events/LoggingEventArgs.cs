/*
LoggingEventArgs.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2021 César Andrés Morgan

Morgan's CLR Advanced Runtime (MCART) is free software: you can redistribute it
and/or modify it under the terms of the GNU General Public License as published
by the Free Software Foundation, either version 3 of the License, or (at your
option) any later version.

Morgan's CLR Advanced Runtime (MCART) is distributed in the hope that it will
be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General
Public License for more details.

You should have received a copy of the GNU General Public License along with
this program. If not, see <http://www.gnu.org/licenses/>.
*/

namespace TheXDS.MCART.Events;
using static TheXDS.MCART.Misc.Internals;

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
    /// <exception cref="System.ArgumentNullException">
    /// Se produce si <paramref name="message"/> es <see langword="null"/>.
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
    /// <exception cref="System.ArgumentNullException">
    /// Se produce si <paramref name="subject"/> o
    /// <paramref name="message"/> son <see langword="null"/>.
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
