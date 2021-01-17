/*
HostConnectionInfoEventArgs.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2021 César Andrés Morgan

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
using System.Net.Sockets;

namespace TheXDS.MCART.Networking.Legacy.Client
{
    /// <summary>
    /// Contiene información de evento que describe un evento de conexión
    /// satisfactorio.
    /// </summary>
    public class HostConnectionInfoEventArgs : EventArgs
    {
        /// <summary>
        /// Nomrbe del host remoto.
        /// </summary>
        public string Host { get; }

        /// <summary>
        /// Punto de red local utilizado para realizar la conexión.
        /// </summary>
        public IPEndPoint LocalEndPoint { get; }

        /// <summary>
        /// Punto de red del host remoto.
        /// </summary>
        public IPEndPoint RemoteEndpoint { get; }

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="HostConnectionInfoEventArgs" />.
        /// </summary>
        /// <param name="remoteEndpoint">
        /// Punto de acceso remoto del servidor.
        /// </param>
        /// <param name="localEndPoint">
        /// Punto de acceso local utilizado para realizar la conexión.
        /// </param>
        public HostConnectionInfoEventArgs(IPEndPoint remoteEndpoint, IPEndPoint localEndPoint) : this(
            remoteEndpoint.Address.ToString(), remoteEndpoint, localEndPoint)
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="HostConnectionInfoEventArgs" />.
        /// </summary>
        /// <param name="host">Nombre del host remoto.</param>
        /// <param name="remoteEndpoint">
        /// Punto de acceso remoto del servidor.
        /// </param>
        /// <param name="localEndPoint">
        /// Punto de acceso local utilizado para realizar la conexión.
        /// </param>
        public HostConnectionInfoEventArgs(string host, IPEndPoint remoteEndpoint, IPEndPoint localEndPoint)
        {
            Host = host;
            RemoteEndpoint = remoteEndpoint;
            LocalEndPoint = localEndPoint;
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="HostConnectionInfoEventArgs" />.
        /// </summary>
        /// <param name="client">
        /// Cliente TCP utilizado para establecer la conexión.
        /// </param>
        public HostConnectionInfoEventArgs(TcpClient client) : this((IPEndPoint) client.Client.RemoteEndPoint,
            (IPEndPoint) client.Client.LocalEndPoint)
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="HostConnectionInfoEventArgs" />.
        /// </summary>
        /// <param name="host">Nombre del host remoto.</param>
        /// <param name="client">
        /// Cliente TCP utilizado para establecer la conexión.
        /// </param>
        public HostConnectionInfoEventArgs(string host, TcpClient client) : this(host,
            (IPEndPoint) client.Client.RemoteEndPoint, (IPEndPoint) client.Client.LocalEndPoint)
        {
        }
    }
}