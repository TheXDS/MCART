﻿/*
UnusableObjectException.cs

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
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Resources;

namespace TheXDS.MCART.Exceptions
{
    /// <summary>
    /// Excepción que se produce cuando se intenta utilizar un objeto marcado con el atributo
    /// <see cref="UnusableAttribute" />.
    /// </summary>
    [Serializable]
    public class UnusableObjectException : OffendingException<object>
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="UnusableObjectException" />.
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
        protected UnusableObjectException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="UnusableObjectException" />.
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
        /// <param name="unusableObject">Objeto inutilizable que es la causa de esta excepción.</param>
        protected UnusableObjectException(SerializationInfo info, StreamingContext context, object unusableObject) :
            base(info, context, unusableObject)
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="UnusableObjectException" />.
        /// </summary>
        public UnusableObjectException() : base(Strings.UnusableObject)
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="UnusableObjectException" />.
        /// </summary>
        /// <param name="unusableObject">Objeto inutilizable que es la causa de esta excepción.</param>
        public UnusableObjectException(object unusableObject) : base(Strings.UnusableObject, unusableObject)
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="UnusableObjectException" />.
        /// </summary>
        /// <param name="message">
        /// Un <see cref="string" /> que describe a la excepción.
        /// </param>
        public UnusableObjectException(string message) : base(message)
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="UnusableObjectException" />.
        /// </summary>
        /// <param name="message">
        /// Un <see cref="string" /> que describe a la excepción.
        /// </param>
        /// <param name="unusableObject">Objeto inutilizable que es la causa de esta excepción.</param>
        public UnusableObjectException(string message, object unusableObject) : base(message, unusableObject)
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="UnusableObjectException" />.
        /// </summary>
        /// <param name="inner">
        /// <see cref="Exception" /> que es la causa de esta excepción.
        /// </param>
        public UnusableObjectException(Exception inner) : base(Strings.UnusableObject, inner)
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="UnusableObjectException" />.
        /// </summary>
        /// <param name="inner">
        /// <see cref="Exception" /> que es la causa de esta excepción.
        /// </param>
        /// <param name="unusableObject">Objeto inutilizable que es la causa de esta excepción.</param>
        public UnusableObjectException(Exception inner, object unusableObject) : base(Strings.UnusableObject, inner,
            unusableObject)
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="UnusableObjectException" />.
        /// </summary>
        /// <param name="message">
        /// Un <see cref="string" /> que describe a la excepción.
        /// </param>
        /// <param name="inner">
        /// <see cref="Exception" /> que es la causa de esta excepción.
        /// </param>
        public UnusableObjectException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="UnusableObjectException" />.
        /// </summary>
        /// <param name="message">
        /// Un <see cref="string" /> que describe a la excepción.
        /// </param>
        /// <param name="inner">
        /// <see cref="Exception" /> que es la causa de esta excepción.
        /// </param>
        /// <param name="unusableObject">Objeto inutilizable que es la causa de esta excepción.</param>
        public UnusableObjectException(string message, Exception inner, object unusableObject) : base(message, inner,
            unusableObject)
        {
        }
    }
}