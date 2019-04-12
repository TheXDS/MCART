/*
ConnectionFailureEventArgs.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2019 César Andrés Morgan

Morgan's CLR Advanced Runtime (MCART) is free software: you can redistribute it
and/or modify it under the terms of the GNU General Public License as published
by the Free Software Foundation, either version 3 of the License, or (at your
option) any later version.

Morgan's CLR Advanced Runtime (MCART) is distributed in the hope that it will
be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General
Public License for more details.

You should have received a copy of the GNU General Public License along with
this program. If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.Net;

#region Configuración de ReSharper

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable UnusedMember.Global

#endregion

namespace TheXDS.MCART.Networking.Client
{
    /// <inheritdoc />
    /// <summary>
    ///     Contiene información de evento para las fallas de conexión
    ///     producidas en un
    ///     <see cref="T:TheXDS.MCART.Networking.Client.ClientBase" />.
    /// </summary>
    public class ConnectionFailureEventArgs : EventArgs
    {
        /// <summary>
        ///     Obtiene un <see cref="IPEndPoint" /> que representa la dirección
        ///     IP y el número de puerto del host al cual se intentó realizar
        ///     la conexión.
        /// </summary>
        public IPEndPoint Address { get; }

        /// <summary>
        ///     Obtiene la excepción producida durante el intento de conexión.
        /// </summary>
        public Exception Exception { get; }

        /// <summary>
        ///     Obtiene el nombre del host al cual se intentó realizar la
        ///     conexión.
        /// </summary>
        public string Host { get; }

        /// <summary>
        ///     Obtiene el número de puerto del host al cual se intentó
        ///     realizar la conexión.
        /// </summary>
        public int Port { get; }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Networking.Client.ConnectionFailureEventArgs" />.
        /// </summary>
        /// <param name="exception">
        ///     Excepción producida durante la falla de conexión.
        /// </param>
        /// <param name="host">
        ///     Host al cual se intentó realizar la conexión.
        /// </param>
        /// <param name="port">
        ///     Puerto de destino del host al cual se intentó realizar la
        ///     conexión.
        /// </param>
        public ConnectionFailureEventArgs(Exception exception, string host, int port)
        {
            Exception = exception;
            Host = host;
            Port = port;

            if (IPAddress.TryParse(host, out var ip)) Address = new IPEndPoint(ip, port);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Networking.Client.ConnectionFailureEventArgs" />.
        /// </summary>
        /// <param name="exception">
        ///     Excepción producida durante la falla de conexión.
        /// </param>
        /// <param name="address">
        ///     Dirección IP/puerto de destino del host al cual se intentó
        ///     realizar la conexión.
        /// </param>
        public ConnectionFailureEventArgs(Exception exception, IPEndPoint address)
        {
            Exception = exception;
            Address = address;
            Host = address.Address.ToString();
            Port = address.Port;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Networking.Client.ConnectionFailureEventArgs" />.
        /// </summary>
        /// <param name="exception">
        ///     Excepción producida durante la falla de conexión.
        /// </param>
        /// <param name="host">
        ///     Dirección IP del host al cual se intentó realizar la conexión.
        /// </param>
        /// <param name="port">
        ///     Puerto de destino del host al cual se intentó realizar la
        ///     conexión.
        /// </param>
        public ConnectionFailureEventArgs(Exception exception, IPAddress host, int port)
        {
            Exception = exception;
            Host = host.ToString();
            Port = port;
            Address = new IPEndPoint(host, port);
        }
    }
}