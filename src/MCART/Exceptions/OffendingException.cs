/*
OffendingException.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2021 César Andrés Morgan

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
    /// <summary>
    /// Excepción estándar producida al encontrarse un problema con un objeto.
    /// </summary>
    [Serializable]
    public class OffendingException<T> : Exception
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="OffendingException{T}" />.
        /// </summary>
        public OffendingException() : base(Strings.XIsInvalid(Strings.TheObj))
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="OffendingException{T}" />.
        /// </summary>
        /// <param name="offendingObject">
        /// Objeto que es la causa de esta excepción.
        /// </param>
        public OffendingException(T offendingObject) : this()
        {
            OffendingObject = offendingObject;
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="OffendingException{T}" />.
        /// </summary>
        /// <param name="message">
        /// Un <see cref="string" /> que describe a la excepción.
        /// </param>
        public OffendingException(string message) : base(message)
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="OffendingException{T}" />.
        /// </summary>
        /// <param name="message">
        /// Un <see cref="string" /> que describe a la excepción.
        /// </param>
        /// <param name="offendingObject">
        /// Objeto que es la causa de esta excepción.
        /// </param>
        public OffendingException(string message, T offendingObject) : base(message)
        {
            OffendingObject = offendingObject;
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="OffendingException{T}" />.
        /// </summary>
        /// <param name="inner">
        /// <see cref="Exception" /> que es la causa de esta excepción.
        /// </param>
        public OffendingException(Exception inner) : base(Strings.XIsInvalid(Strings.TheType), inner)
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="OffendingException{T}" />.
        /// </summary>
        /// <param name="inner">
        /// <see cref="Exception" /> que es la causa de esta excepción.
        /// </param>
        /// <param name="offendingObject">
        /// Objeto que es la causa de esta excepción.
        /// </param>
        public OffendingException(Exception inner, T offendingObject) : this(inner)
        {
            OffendingObject = offendingObject;
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="OffendingException{T}" />.
        /// </summary>
        /// <param name="message">
        /// Un <see cref="string" /> que describe a la excepción.
        /// </param>
        /// <param name="inner">
        /// <see cref="Exception" /> que es la causa de esta excepción.
        /// </param>
        public OffendingException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="OffendingException{T}" />.
        /// </summary>
        /// <param name="message">
        /// Un <see cref="string" /> que describe a la excepción.
        /// </param>
        /// <param name="inner">
        /// <see cref="Exception" /> que es la causa de esta excepción.
        /// </param>
        /// <param name="offendingObject">
        /// Objeto que es la causa de esta excepción.
        /// </param>
        public OffendingException(string message, Exception inner, T offendingObject) : base(message, inner)
        {
            OffendingObject = offendingObject;
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="OffendingException{T}" /> con datos serializados.
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
        protected OffendingException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="OffendingException{T}" /> con datos serializados.
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
        /// <param name="offendingObject">
        /// Objeto que es la causa de esta excepción.
        /// </param>
        protected OffendingException(SerializationInfo info, StreamingContext context, T offendingObject) : base(info,
            context)
        {
            OffendingObject = offendingObject;
        }

        /// <summary>
        /// Objeto que ha causado la excepción.
        /// </summary>
        public T OffendingObject { get; } = default!;
    }
}