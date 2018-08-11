/*
Protocol.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright (c) 2011 - 2018 César Andrés Morgan

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

using System.Diagnostics;
using System.Net.Sockets;
using static TheXDS.MCART.Types.Extensions.TypeExtensions;

#region Configuración de ReSharper

// ReSharper disable MemberCanBeProtected.Global
// ReSharper disable VirtualMemberNeverOverridden.Global
// ReSharper disable UnusedParameter.Global

#endregion

namespace TheXDS.MCART.Networking.Server
{
    /// <inheritdoc />
    /// <summary>
    /// Esta clase abstracta determina una serie de funciones a heredar por
    /// una clase que provea de protocolos a un servidor.
    /// </summary>
    public abstract class Protocol : IProtocol
    {
        /// <inheritdoc />
        /// <summary>
        /// Protocolo de atención al cliente
        /// </summary>
        /// <param name="client">Cliente que será atendido.</param>
        /// <param name="server">Servidor que atiende al cliente.</param>
        /// <param name="data">Datos recibidos desde el cliente.</param>
        public abstract void ClientAttendant(Client client, Server server, byte[] data);
        /// <inheritdoc />
        /// <summary>
        /// Protocolo de bienvenida del cliente.
        /// </summary>
        /// <returns>
        /// <see langword="true" /> si el cliente fue aceptado por el protocolo,
        /// <see langword="false" /> en caso contrario.
        /// </returns>
        /// <param name="client">Cliente que será atendido.</param>
        /// <param name="server">Servidor que atiende al cliente.</param>
        public virtual bool ClientWelcome(Client client, Server server) { return true; }
        /// <inheritdoc />
        /// <summary>
        /// Protocolo de desconexión del cliente.
        /// </summary>
        /// <param name="client">Cliente que será atendido.</param>
        /// <param name="server">Servidor que atiende al cliente.</param>
        public virtual void ClientBye(Client client, Server server) { }
        /// <inheritdoc />
        /// <summary>
        /// Protocolo de desconexión inesperada del cliente.
        /// </summary>
        /// <param name="client">Cliente que se ha desconectado.</param>
        /// <param name="server">Servidor que atiendía al cliente.</param>
        public virtual void ClientDisconnect(Client client, Server server) { }
        /// <inheritdoc />
        /// <summary>
        /// Inicializa un nuevo cliente manejado por este protocolo.
        /// </summary>
        /// <param name="tcpClient">
        /// <see cref="T:System.Net.Sockets.TcpClient" /> de la conexión con el host remoto.
        /// </param>
        /// <returns>
        /// Un nuevo <see cref="T:TheXDS.MCART.Networking.Server.Client" />.
        /// </returns>
        public abstract Client CreateClient(TcpClient tcpClient);
    }

    /// <inheritdoc />
    /// <summary>
    /// Esta clase abstracta determina una serie de funciones a heredar por
    /// una clase que provea de protocolos a un servidor.
    /// </summary>
    /// <typeparam name="T"> Tipo de cliente a atender.</typeparam>
    public abstract class Protocol<T> : IProtocol where T : Client
    {
        /// <inheritdoc />
        /// <summary>
        /// Realiza casting de las interfaces a tipos genéricos específicos
        /// para el protocolo.
        /// </summary>
        /// <param name="client">Cliente que será atendido.</param>
        /// <param name="server">Servidor que atiende al cliente.</param>
        /// <param name="data">Datos recibidos desde el cliente.</param>
        [DebuggerStepThrough]
        public void ClientAttendant(Client client, Server server, byte[] data)
        {
            ClientAttendant((T)client, (Server<T>)server,data);
        }

        /// <summary>
        /// Atiende al cliente
        /// </summary>
        /// <param name="client">Cliente que será atendido.</param>
        /// <param name="server">Servidor que atiende al cliente.</param>
        /// <param name="data">Datos recibidos desde el cliente.</param>
        public abstract void ClientAttendant(T client, Server<T> server, byte[] data);

        /// <inheritdoc />
        /// <summary>
        /// Realiza casting de las interfaces a tipos genéricos específicos
        /// para el protocolo.
        /// </summary>
        /// <returns><see langword="true" /> si el cliente fue aceptado por el protocolo, <see langword="false" /> en caso contrario.</returns>
        /// <param name="client">Cliente que será atendido.</param>
        /// <param name="server">Servidor que atiende al cliente.</param>
        [DebuggerStepThrough]
        public bool ClientWelcome(Client client, Server server)
        {
            return ClientWelcome((T)client, (Server<T>)server);
        }

        /// <summary>
        /// Protocolo de bienvenida del cliente.
        /// </summary>
        /// <returns><see langword="true"/> si el cliente fue aceptado por el protocolo, <see langword="false"/> en caso contrario.</returns>
        /// <param name="client">Cliente que será atendido.</param>
        /// <param name="server">Servidor que atiende al cliente.</param>
        public virtual bool ClientWelcome(T client, Server<T> server) { return true; }

        /// <inheritdoc />
        /// <summary>
        /// Realiza casting de las interfaces a tipos genéricos específicos
        /// para el protocolo.
        /// </summary>
        /// <param name="client">Cliente que será atendido.</param>
        /// <param name="server">Servidor que atiende al cliente.</param>
        [DebuggerStepThrough]
        public void ClientBye(Client client, Server server)
        {
            ClientBye((T)client, (Server<T>)server);
        }

        /// <summary>
        /// Protocolo de desconexión del cliente.
        /// </summary>
        /// <param name="client">Cliente que será atendido.</param>
        /// <param name="server">Servidor que atiende al cliente.</param>
        public virtual void ClientBye(T client, Server<T> server) { }

        /// <inheritdoc />
        /// <summary>
        /// Realiza casting de las interfaces a tipos genéricos específicos
        /// para el protocolo.
        /// </summary>
        /// <param name="client">Cliente que se ha desconectado.</param>
        /// <param name="server">Servidor que atiendía al cliente.</param>
        [DebuggerStepThrough]
        public void ClientDisconnect(Client client, Server server)
        {
            ClientDisconnect((T)client,(Server<T>)server);
        }

        /// <summary>
        /// Protocolo de desconexión inesperada del cliente.
        /// </summary>
        /// <param name="client">Cliente que se ha desconectado.</param>
        /// <param name="server">Servidor que atiendía al cliente.</param>
        public virtual void ClientDisconnect(T client, Server<T> server) { }

        /// <inheritdoc />
        /// <summary>
        /// Inicializa un nuevo cliente manejado por este protocolo.
        /// </summary>
        /// <param name="tcpClient">
        /// <see cref="T:System.Net.Sockets.TcpClient" /> de la conexión con el host remoto.
        /// </param>
        /// <returns>
        /// Un nuevo <see cref="T:TheXDS.MCART.Networking.Server.Client" />.
        /// </returns>
        public virtual Client CreateClient(TcpClient tcpClient)
        {
            return typeof(T).New<T>(tcpClient);
        }
    }
}