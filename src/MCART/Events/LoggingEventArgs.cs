/*
LoggingEventArgs.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2020 César Andrés Morgan

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

namespace TheXDS.MCART.Events
{
    /// <inheritdoc />
    /// <summary>
    /// Incluye información de evento para cualquier clase con eventos de
    /// logging (bitácora).
    /// </summary>
    public class LoggingEventArgs : ValueEventArgs<string>
    {
        /// <inheritdoc />
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="LoggingEventArgs" />, sin definir un objeto relacionado.
        /// </summary>
        /// <param name="message">Mensaje de esta entrada de log.</param>
        public LoggingEventArgs(string message) : base(message)
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="LoggingEventArgs" />, definiendo un objeto relacionado.
        /// </summary>
        /// <param name="subject">Objeto relacionado a esta entrada de log.</param>
        /// <param name="message">Mensaje de esta entrada de log.</param>
        public LoggingEventArgs(object subject, string message) : base(message)
        {
            Subject = subject;
        }

        /// <summary>
        /// Objeto relacionado a esta entrada de log.
        /// </summary>
        public object? Subject { get; }
    }
}