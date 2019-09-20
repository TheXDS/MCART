/*
InvalidArgumentException.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2019 César Andrés Morgan

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
    ///     Se produce cuando un argumento contiene un valor inválido.
    /// </summary>
    [Serializable]
    public class InvalidArgumentException : ArgumentException
    {
        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="InvalidArgumentException" />.
        /// </summary>
        /// <param name="context">
        ///     El <see cref="StreamingContext" /> que contiene información
        ///     contextual acerca del origen o el destino.
        /// </param>
        /// <param name="info">
        ///     El <see cref="SerializationInfo" /> que contiene la información
        ///     serializada del objeto acerca de la excepción que está siendo
        ///     lanzada.
        /// </param>
        protected InvalidArgumentException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="InvalidArgumentException" />.
        /// </summary>
        public InvalidArgumentException() : base(Strings.InvalidParameterX(Strings.Specified))
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="InvalidArgumentException" />.
        /// </summary>
        /// <param name="argumentName">Nombre del argumento inválido.</param>
        public InvalidArgumentException(string argumentName) : base(Strings.InvalidParameterX(argumentName))
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="InvalidArgumentException" />.
        /// </summary>
        /// <param name="message">
        ///     Un <see cref="string" /> que describe a la excepción.
        /// </param>
        /// <param name="argumentName">Nombre del argumento inválido.</param>
        public InvalidArgumentException(string message, string argumentName) : base(message, argumentName)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="InvalidArgumentException" />.
        /// </summary>
        /// <param name="inner">
        ///     <see cref="Exception" /> que es la causa de esta excepción.
        /// </param>
        public InvalidArgumentException(Exception inner) : base(Strings.InvalidParameterX(Strings.Specified), inner)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="InvalidArgumentException" />.
        /// </summary>
        /// <param name="inner">
        ///     <see cref="Exception" /> que es la causa de esta excepción.
        /// </param>
        /// <param name="argumentName">Nombre del argumento inválido.</param>
        public InvalidArgumentException(Exception inner, string argumentName) : base(Strings.InvalidParameterX(argumentName), inner)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="InvalidArgumentException" />.
        /// </summary>
        /// <param name="message">
        ///     Un <see cref="string" /> que describe a la excepción.
        /// </param>
        /// <param name="inner">
        ///     <see cref="Exception" /> que es la causa de esta excepción.
        /// </param>
        public InvalidArgumentException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="InvalidArgumentException" />.
        /// </summary>
        /// <param name="message">
        ///     Un <see cref="string" /> que describe a la excepción.
        /// </param>
        /// <param name="inner">
        ///     <see cref="Exception" /> que es la causa de esta excepción.
        /// </param>
        /// <param name="argumentName">Nombre del argumento faltante.</param>
        public InvalidArgumentException(string message, Exception inner, string argumentName) : base(message,
            argumentName, inner)
        {
        }
    }
}