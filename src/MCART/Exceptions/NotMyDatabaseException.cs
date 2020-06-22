/*
NotMyDatabaseException.cs

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
    /// <summary>
    /// Excepción que se produce cuando una base de datos no corresponde a la
    /// aplicación.
    /// </summary>
    [Serializable]
    public class NotMyDatabaseException : Exception
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="NotMyDatabaseException" />.
        /// </summary>
        /// <param name="info">
        /// El objeto que contiene la información de serialización.
        /// </param>
        /// <param name="context">
        /// La información contextual acerca del orígen o el destino.
        /// </param>
        protected NotMyDatabaseException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="NotMyDatabaseException" />.
        /// </summary>
        public NotMyDatabaseException() : base(Strings.DBDoesntBelong)
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="NotMyDatabaseException" />.
        /// </summary>
        /// <param name="message">
        /// Un <see cref="string" /> que describe a la excepción.
        /// </param>
        public NotMyDatabaseException(string message) : base(message)
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="NotMyDatabaseException"/>.
        /// </summary>
        /// <param name="message">
        /// Un <see cref="string" /> que describe a la excepción.
        /// </param>
        /// <param name="inner">
        /// <see cref="Exception" /> que es la causa de esta
        /// excepción.
        /// </param>
        public NotMyDatabaseException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="NotMyDatabaseException"/>.
        /// </summary>
        /// <param name="inner">
        /// <see cref="Exception"/> que es la causa de esta
        /// excepción.
        /// </param>
        public NotMyDatabaseException(Exception inner) : base(Strings.DBDoesntBelong,inner)
        {
        }
    }
}