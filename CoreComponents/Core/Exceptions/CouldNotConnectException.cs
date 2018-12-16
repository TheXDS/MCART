/*
CouldNotConnectException.cs

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
using System.Net;
using System.Runtime.Serialization;
using TheXDS.MCART.Resources;

namespace TheXDS.MCART.Exceptions
{
    /// <inheritdoc />
    /// <summary>
    ///     Excepción que se produce cuando no se puede realizar la conexión
    /// </summary>
    [Serializable]
    public class CouldNotConnectException : Exception
    {
        private readonly IPEndPoint _offendingEndPoint;

        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="CouldNotConnectException" />.
        /// </summary>
        public CouldNotConnectException() : base(Strings.CldntConnect(Strings.TheSrv.ToLower()))
        {
        }

        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="CouldNotConnectException" />.
        /// </summary>
        /// <param name="offendingEndPoint">End point.</param>
        public CouldNotConnectException(IPEndPoint offendingEndPoint) : base(
            Strings.CldntConnect($"{offendingEndPoint.Address}:{offendingEndPoint.Port}"))
        {
            _offendingEndPoint = offendingEndPoint;
        }

        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="CouldNotConnectException" />.
        /// </summary>
        /// <param name="message">Un <see cref="string" /> que describe a la excepción.</param>
        public CouldNotConnectException(string message) : base(message)
        {
        }

        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="CouldNotConnectException" />.
        /// </summary>
        /// <param name="message">Un <see cref="string" /> que describe a la excepción.</param>
        /// <param name="inner"><see cref="Exception" /> que es la causa de esta excepción.</param>
        public CouldNotConnectException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="CouldNotConnectException" />.
        /// </summary>
        /// <param name="offendingEndPoint">End point.</param>
        /// <param name="message">Un <see cref="string" /> que describe a la excepción.</param>
        public CouldNotConnectException(IPEndPoint offendingEndPoint, string message) : base(message)
        {
            _offendingEndPoint = offendingEndPoint;
        }

        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="CouldNotConnectException" />.
        /// </summary>
        /// <param name="offendingEndPoint">End point.</param>
        /// <param name="inner"><see cref="Exception" /> que es la causa de esta excepción.</param>
        public CouldNotConnectException(IPEndPoint offendingEndPoint, Exception inner) : base(
            Strings.CldntConnect($"{offendingEndPoint.Address}:{offendingEndPoint.Port}"), inner)
        {
            _offendingEndPoint = offendingEndPoint;
        }

        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="CouldNotConnectException" />.
        /// </summary>
        /// <param name="offendingEndPoint">End point.</param>
        /// <param name="message">Un <see cref="string" /> que describe a la excepción.</param>
        /// <param name="inner"><see cref="Exception" /> que es la causa de esta excepción.</param>
        public CouldNotConnectException(IPEndPoint offendingEndPoint, string message, Exception inner) : base(message,
            inner)
        {
            _offendingEndPoint = offendingEndPoint;
        }

        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="CouldNotConnectException" />.
        /// </summary>
        /// <param name="info">
        ///     El objeto que contiene la información de serialización.
        /// </param>
        /// <param name="context">
        ///     La información contextual acerca del orígen o el destino.
        /// </param>
        protected CouldNotConnectException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        /// <summary>
        ///     <see cref="IPEndPoint" /> que fue la causa de esta excepción.
        /// </summary>
        public IPEndPoint OffendingEndPoint => _offendingEndPoint;
    }
}