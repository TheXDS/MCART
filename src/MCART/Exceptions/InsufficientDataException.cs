﻿/*
InsufficientDataException.cs

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
using St = TheXDS.MCART.Resources.Strings;

namespace TheXDS.MCART.Exceptions
{
    /// <summary>
    /// Excepción que se produce al no haber suficientes datos
    /// </summary>
    [Serializable]
    public class InsufficientDataException : Exception
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="InsufficientDataException" />.
        /// </summary>
        /// <param name="info">
        /// El objeto que contiene la información de serialización.
        /// </param>
        /// <param name="context">
        /// La información contextual acerca del orígen o el destino.
        /// </param>
        protected InsufficientDataException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="InsufficientDataException" />.
        /// </summary>
        public InsufficientDataException() : base(St.NotEnoughData)
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="InsufficientDataException" />.
        /// </summary>
        /// <param name="message">Un <see cref="string" /> que describe a la excepción.</param>
        public InsufficientDataException(string message) : base(message)
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="InsufficientDataException" />.
        /// </summary>
        /// <param name="inner"><see cref="Exception" /> que es la causa de esta excepción.</param>
        public InsufficientDataException(Exception inner) : base(St.NotEnoughData, inner)
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="InsufficientDataException" />.
        /// </summary>
        /// <param name="message">Un <see cref="string" /> que describe a la excepción.</param>
        /// <param name="inner"><see cref="Exception" /> que es la causa de esta excepción.</param>
        public InsufficientDataException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}