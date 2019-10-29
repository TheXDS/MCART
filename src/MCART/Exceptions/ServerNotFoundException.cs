﻿/*
ServerNotFoundException.cs

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

#nullable enable

using System;
using System.Runtime.Serialization;
using TheXDS.MCART.Resources;

namespace TheXDS.MCART.Exceptions
{
    /// <inheritdoc />
    /// <summary>
    ///     Excepción que se produce cuando no se puede resolver un nombre DNS,
    ///     o no se encuentra el servidor especificado.
    /// </summary>
    [Serializable]
    public class ServerNotFoundException : Exception
    {
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="ServerNotFoundException" />.
        /// </summary>
        /// <param name="info">
        ///     El objeto que contiene la información de serialización.
        /// </param>
        /// <param name="context">
        ///     La información contextual acerca del orígen o el destino.
        /// </param>
        protected ServerNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="ServerNotFoundException" />.
        /// </summary>
        public ServerNotFoundException() : base(Strings.XNotFound(Strings.TheSrv))
        {
        }

        /// <summary>
        ///     Inicializa una nueva instancia de la clase 
        ///     <see cref="ServerNotFoundException" />.
        /// </summary>
        /// <param name="message">
        ///     Un <see cref="string" /> que describe a la excepción.
        /// </param>
        public ServerNotFoundException(string message) : base(message)
        {
        }

        /// <summary>
        ///     Inicializa una nueva instancia de la clase 
        ///     <see cref="ServerNotFoundException" />.
        /// </summary>
        /// <param name="message">
        ///     Un <see cref="string" /> que describe a la excepción.
        /// </param>
        /// <param name="inner">
        ///     <see cref="Exception" /> que es la causa de esta excepción.
        /// </param>
        public ServerNotFoundException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="ServerNotFoundException" />.
        /// </summary>
        /// <param name="inner">
        ///     <see cref="Exception" /> que es la causa de esta excepción.
        /// </param>
        public ServerNotFoundException(Exception inner) : base(Strings.XNotFound(Strings.TheSrv), inner)
        {
        }
    }
}