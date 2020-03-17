/*
MissingResourceException.cs

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

namespace TheXDS.MCART.Exceptions
{
    /// <inheritdoc />
    /// <summary>
    /// Excepción que se produce cuando se hace referencia a un recurso que no existe.
    /// </summary>
    [Serializable]
    public class MissingResourceException : OffendingException<string>
    {
        /// <inheritdoc />
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="MissingResourceException" />.
        /// </summary>
        public MissingResourceException()
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="MissingResourceException" />.
        /// </summary>
        /// <param name="offendingObject">
        /// Id del recurso que ha producido la excepción.
        /// </param>
        public MissingResourceException(string offendingObject) : base(offendingObject)
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="MissingResourceException" />.
        /// </summary>
        /// <param name="inner"><see cref="Exception" /> que es la causa de esta excepción.</param>
        public MissingResourceException(Exception inner) : base(inner)
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="MissingResourceException" />.
        /// </summary>
        /// <param name="message">
        /// Un <see cref="string" /> que describe a la excepción.
        /// </param>
        /// <param name="offendingObject">
        /// Id del recurso que ha producido la excepción.
        /// </param>
        public MissingResourceException(string message, string offendingObject) : base(message, offendingObject)
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="MissingResourceException" />.
        /// </summary>
        /// <param name="inner"><see cref="Exception" /> que es la causa de esta excepción.</param>
        /// <param name="offendingObject">
        /// Id del recurso que ha producido la excepción.
        /// </param>
        public MissingResourceException(Exception inner, string offendingObject) : base(inner, offendingObject)
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="MissingResourceException" />.
        /// </summary>
        /// <param name="message">Un <see cref="string" /> que describe a la excepción.</param>
        /// <param name="inner"><see cref="Exception" /> que es la causa de esta excepción.</param>
        public MissingResourceException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="MissingResourceException" />.
        /// </summary>
        /// <param name="message">Un <see cref="string" /> que describe a la excepción.</param>
        /// <param name="inner"><see cref="Exception" /> que es la causa de esta excepción.</param>
        /// <param name="offendingObject">
        /// Id del recurso que ha producido la excepción.
        /// </param>
        public MissingResourceException(string message, Exception inner, string offendingObject) : base(message, inner, offendingObject)
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="MissingResourceException" />.
        /// </summary>
        /// <param name="info">
        /// El objeto que contiene la información de serialización.
        /// </param>
        /// <param name="context">
        /// La información contextual acerca del orígen o el destino.
        /// </param>
        protected MissingResourceException(SerializationInfo info, StreamingContext context) : base(info, context)
        {            
        }

        /// <inheritdoc />
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="MissingResourceException" />.
        /// </summary>
        /// <param name="info">
        /// El objeto que contiene la información de serialización.
        /// </param>
        /// <param name="context">
        /// La información contextual acerca del orígen o el destino.
        /// </param>
        /// <param name="offendingObject">
        /// Id del recurso que ha producido la excepción.
        /// </param>
        protected MissingResourceException(SerializationInfo info, StreamingContext context, string offendingObject) : base(info, context, offendingObject)
        {
        }
    }
}