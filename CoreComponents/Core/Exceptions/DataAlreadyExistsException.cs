/*
DataAlreadyExistsException.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright (c) 2011 - 2018 César Andrés Morgan

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

#region Configuración de ReSharper

// ReSharper disable MemberCanBeProtected.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

#endregion

using System;
using System.Runtime.Serialization;
using TheXDS.MCART.Resources;

namespace TheXDS.MCART.Exceptions
{
    /// <inheritdoc />
    /// <summary>
    ///     Excepción que se produce al intentar crear nueva información dentro de
    ///     una base de datos con un identificador que ya existe.
    /// </summary>
    [Serializable]
    public class DataAlreadyExistsException : Exception
    {
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="DataAlreadyExistsException" />.
        /// </summary>
        public DataAlreadyExistsException() : base(Strings.XAlreadyExists(Strings.TheUid))
        {
        }

        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="DataAlreadyExistsException" />.
        /// </summary>
        /// <param name="uid">Uid.</param>
        public DataAlreadyExistsException(string uid) : base(Strings.XAlreadyExists(Strings.XYQuotes(Strings.TheUid, uid)))
        {
        }

        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="DataAlreadyExistsException" />.
        /// </summary>
        /// <param name="uid">Uid.</param>
        /// <param name="inner"><see cref="Exception" /> que es la causa de esta excepción.</param>
        public DataAlreadyExistsException(string uid, Exception inner) : base(
            Strings.XAlreadyExists(Strings.XYQuotes(Strings.TheUid, uid)), inner)
        {
        }

        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="DataAlreadyExistsException" />.
        /// </summary>
        /// <param name="info">
        ///     El objeto que contiene la información de serialización.
        /// </param>
        /// <param name="context">
        ///     La información contextual acerca del orígen o el destino.
        /// </param>
        protected DataAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}