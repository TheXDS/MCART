/*
ServerProtocol.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright (c) 2011 - 2019 César Andrés Morgan

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
using System.Net.Sockets;
using TheXDS.MCART.Exceptions;
using static TheXDS.MCART.Types.Extensions.TypeExtensions;
using St=TheXDS.MCART.Resources.Strings;

#region Configuración de ReSharper

// ReSharper disable MemberCanBeProtected.Global
// ReSharper disable VirtualMemberNeverOverridden.Global
// ReSharper disable UnusedParameter.Global

#endregion

namespace TheXDS.MCART.Networking.Server
{
    /// <inheritdoc cref="IProtocol"/>
    /// <summary>
    ///     Esta clase abstracta determina una serie de funciones a heredar por
    ///     una clase que provea de protocolos a un servidor.
    /// </summary>
    public abstract class ServerProtocol : IProtocol, IServerProtocol
    {        
        /// <inheritdoc />
        /// <summary>
        ///     Protocolo de atención al cliente
        /// </summary>
        /// <param name="client">Cliente que será atendido.</param>
        /// <param name="data">Datos recibidos desde el cliente.</param>
        public abstract void ClientAttendant(Client client, byte[] data);

        /// <inheritdoc />
        /// <summary>
        ///     Protocolo de desconexión del cliente.
        /// </summary>
        /// <param name="client">Cliente que será atendido.</param>
        public virtual void ClientBye(Client client)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Protocolo de desconexión inesperada del cliente.
        /// </summary>
        /// <param name="client">Cliente que se ha desconectado.</param>
        public virtual void ClientDisconnect(Client client)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Protocolo de bienvenida del cliente.
        /// </summary>
        /// <returns>
        ///     <see langword="true" /> si el cliente fue aceptado por el protocolo,
        ///     <see langword="false" /> en caso contrario.
        /// </returns>
        /// <param name="client">Cliente que será atendido.</param>
        public virtual bool ClientWelcome(Client client)
        {
            return true;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa un nuevo cliente manejado por este protocolo.
        /// </summary>
        /// <param name="tcpClient">
        ///     <see cref="T:System.Net.Sockets.TcpClient" /> de la conexión con el host remoto.
        /// </param>
        /// <returns>
        ///     Un nuevo <see cref="T:TheXDS.MCART.Networking.Server.Client" />.
        /// </returns>
        public virtual Client CreateClient(TcpClient tcpClient) => new Client(tcpClient);

        Server IServerProtocol.MyServer{get;set;}

        /// <summary>
        ///     Obtiene una referencia al servidor activo de esta instancia.
        /// </summary>
        protected Server Server => ((IServerProtocol)this).MyServer;
    }

    /// <inheritdoc cref="IServerProtocol"/>
    /// <summary>
    ///     Esta clase abstracta determina una serie de funciones a heredar por
    ///     una clase que provea de protocolos a un servidor.
    /// </summary>
    /// <typeparam name="T"> Tipo de cliente a atender.</typeparam>
    public abstract class ServerProtocol<T> : IProtocol, IServerProtocol where T : Client
    {
        /// <summary>
        ///     Inicializa la clase <see cref="ServerProtocol"/>
        /// </summary>
        /// <exception cref="InvalidTypeException">
        ///     Se produce si <typeparamref name="T"/> no es una clase derivada
        ///     de <see cref="Client"/> instanciable.
        /// </exception>
        static ServerProtocol()
        {
            if (!typeof(T).IsInstantiable(typeof(TcpClient))) throw new InvalidTypeException(St.XMustBeY(nameof(T),St.Instantiable));
        }

        /// <summary>
        ///     Obtiene una referencia al servidor activo de esta instancia.
        /// </summary>
        /// <typeparam name="T">Tipo de clientes que el servidor atiende.</typeparam>
        protected Server<T> Server => ((IServerProtocol)this).MyServer as Server<T>;

        /// <inheritdoc />
        /// <summary>
        ///     Protocolo de desconexión del cliente.
        /// </summary>
        /// <param name="client">Cliente que será atendido.</param>
        public void ClientBye(Client client) => ClientBye(client as T);

        /// <summary>
        ///     Protocolo de desconexión del cliente.
        /// </summary>
        /// <param name="client">Cliente que será atendido.</param>
        public virtual void ClientBye(T client) { }

        /// <inheritdoc />
        /// <summary>
        ///     Protocolo de desconexión inesperada del cliente.
        /// </summary>
        /// <param name="client">Cliente que se ha desconectado.</param>
        public void ClientDisconnect(Client client) => ClientDisconnect(client as T);

        /// <summary>
        ///     Protocolo de desconexión inesperada del cliente.
        /// </summary>
        /// <param name="client">Cliente que se ha desconectado.</param>
        public virtual void ClientDisconnect(T client)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Protocolo de bienvenida del cliente.
        /// </summary>
        /// <returns>
        ///     <see langword="true" /> si el cliente fue aceptado por el protocolo,
        ///     <see langword="false" /> en caso contrario.
        /// </returns>
        /// <param name="client">Cliente que será atendido.</param>
        public bool ClientWelcome(Client client) => ClientWelcome(client as T);
        /// <summary>
        ///     Protocolo de bienvenida del cliente.
        /// </summary>
        /// <returns>
        ///     <see langword="true" /> si el cliente fue aceptado por el protocolo,
        ///     <see langword="false" /> en caso contrario.
        /// </returns>
        /// <param name="client">Cliente que será atendido.</param>
        public virtual bool ClientWelcome(T client)
        {
            return true;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Protocolo de atención al cliente
        /// </summary>
        /// <param name="client">Cliente que será atendido.</param>
        /// <param name="data">Datos recibidos desde el cliente.</param>
        public void ClientAttendant(Client client, byte[] data) => ClientAttendant(client as T, data);
        
        /// <summary>
        ///     Protocolo de atención al cliente
        /// </summary>
        /// <param name="client">Cliente que será atendido.</param>
        /// <param name="data">Datos recibidos desde el cliente.</param>
        public abstract void ClientAttendant(T client, byte[] data);

        Server IServerProtocol.MyServer { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa un nuevo cliente manejado por este protocolo.
        /// </summary>
        /// <param name="tcpClient">
        ///     <see cref="T:System.Net.Sockets.TcpClient" /> de la conexión con el host remoto.
        /// </param>
        /// <returns>
        ///     Un nuevo <see cref="T:TheXDS.MCART.Networking.Server.Client" />.
        /// </returns>
        /// <exception cref="InvalidTypeException">
        ///     Se produce si no es posible crear una nueva instancia del
        ///     cliente debido a una excepción del constructor.
        /// </exception>
        public virtual Client CreateClient(TcpClient tcpClient)
        {
            try
            {
                return typeof(T).New<T>(tcpClient);
            }
            catch (Exception e)
            {
                throw new InvalidTypeException(St.CantCreateInstance(typeof(T).Name), e, typeof(T));
            }
        }
    }
}