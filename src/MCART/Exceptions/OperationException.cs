/*
OperationException.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2020 César Andrés Morgan

Morgan's CLR Advanced Runtime (MCART) is free software: you can redistribute it
and/or modify it under the terms of the GNU General Public License as published
by the Free Software Foundation, either version 3 of the License, or (at your
option) any later version.

Morgan's CLR Advanced Runtime (MCART) is distributed in the hope that it will be
useful, but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General
Public License for more details.

You should have received a copy of the GNU General Public License along with
this program. If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.Runtime.Serialization;
using TheXDS.MCART.Resources;

namespace TheXDS.MCART.Exceptions
{
    /// <inheritdoc />
    /// <summary>
    /// Excepción que se produce cuando una operación falla.
    /// </summary>
    [Serializable]
    public class OperationException : OffendingException<Delegate>
    {
        /// <inheritdoc />
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="OperationException" />.
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
        protected OperationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="OperationException" />.
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
        /// <param name="offendingOperation">Delegado de operación en donde se ha producido la excepción.</param>
        protected OperationException(SerializationInfo info, StreamingContext context, Delegate offendingOperation) :
            base(info, context, offendingOperation)
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="OperationException" />.
        /// </summary>
        public OperationException() : base(Msg())
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="OperationException" />.
        /// </summary>
        /// <param name="offendingOperation">Delegado de operación en donde se ha producido la excepción.</param>
        public OperationException(Delegate offendingOperation) : base(Msg(offendingOperation), offendingOperation)
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="OperationException" />.
        /// </summary>
        /// <param name="message">
        /// Un <see cref="string" /> que describe a la excepción.
        /// </param>
        public OperationException(string message) : base(message)
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="OperationException" />.
        /// </summary>
        /// <param name="message">
        /// Un <see cref="string" /> que describe a la excepción.
        /// </param>
        /// <param name="offendingOperation">Delegado de operación en donde se ha producido la excepción.</param>
        public OperationException(string message, Delegate offendingOperation) : base(message, offendingOperation)
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="OperationException" />.
        /// </summary>
        /// <param name="inner">
        /// <see cref="Exception" /> que es la causa de esta excepción.
        /// </param>
        public OperationException(Exception inner) : base(Msg(), inner)
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="OperationException" />.
        /// </summary>
        /// <param name="inner">
        /// <see cref="Exception" /> que es la causa de esta excepción.
        /// </param>
        /// <param name="offendingOperation">Delegado de operación en donde se ha producido la excepción.</param>
        public OperationException(Exception inner, Delegate offendingOperation) : base(Msg(offendingOperation), inner,
            offendingOperation)
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="OperationException" />.
        /// </summary>
        /// <param name="message">
        /// Un <see cref="string" /> que describe a la excepción.
        /// </param>
        /// <param name="inner">
        /// <see cref="Exception" /> que es la causa de esta excepción.
        /// </param>
        public OperationException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="OperationException" />.
        /// </summary>
        /// <param name="message">
        /// Un <see cref="string" /> que describe a la excepción.
        /// </param>
        /// <param name="inner">
        /// <see cref="Exception" /> que es la causa de esta excepción.
        /// </param>
        /// <param name="offendingOperation">Delegado de operación en donde se ha producido la excepción.</param>
        public OperationException(string message, Exception inner, Delegate offendingOperation) : base(message, inner,
            offendingOperation)
        {
        }

        private static string Msg()
        {
            return Strings.ExcDoingX(Strings.TheTask.ToLower());
        }

        private static string Msg(Delegate offendingOperation)
        {
            return Strings.ExcDoingX(Strings.XYQuotes(Strings.TheTask.ToLower(), offendingOperation.Method.Name));
        }
    }
}