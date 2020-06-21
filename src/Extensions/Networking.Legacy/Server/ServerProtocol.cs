/*
ServerProtocol.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2020 César Andrés Morgan

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

namespace TheXDS.MCART.Networking.Legacy.Server
{
    /// <inheritdoc cref="IProtocol"/>
    /// <summary>
    /// Esta clase abstracta determina una serie de funciones a heredar por
    /// una clase que provea de protocolos a un servidor.
    /// </summary>
    public abstract class ServerProtocol : ServerProtocol<Client>
    {        
    }

    /// <inheritdoc cref="IServerProtocol"/>
    /// <summary>
    /// Esta clase abstracta determina una serie de funciones a heredar por
    /// una clase que provea de protocolos a un servidor.
    /// </summary>
    /// <typeparam name="T"> Tipo de cliente a atender.</typeparam>
    public abstract class ServerProtocol<T> : IProtocol<T>, IServerProtocol<T> where T : Client
    {
        public Server<T> BuildServer(int port)
        {
            return new Server<T>(this, port);
        }

        /// <summary>
        /// Inicializa la clase <see cref="ServerProtocol"/>
        /// </summary>
        /// <exception cref="InvalidTypeException">
        /// Se produce si <typeparamref name="T"/> no es una clase derivada
        /// de <see cref="Client"/> instanciable.
        /// </exception>
        static ServerProtocol()
        {
            if (!typeof(T).IsInstantiable(typeof(TcpClient))) throw new InvalidTypeException(St.XMustBeY(nameof(T),St.Instantiable));
        }

        /// <summary>
        /// Obtiene una referencia al servidor activo de esta instancia.
        /// </summary>
        protected Server<T> Server => MyServer;

        /// <inheritdoc />
        /// <summary>
        /// Protocolo de desconexión del cliente.
        /// </summary>
        /// <param name="client">Cliente que será atendido.</param>
        public void ClientBye(Client client) => ClientBye((T)client);

        /// <summary>
        /// Protocolo de desconexión del cliente.
        /// </summary>
        /// <param name="client">Cliente que será atendido.</param>
        public virtual void ClientBye(T client) { }

        /// <inheritdoc />
        /// <summary>
        /// Protocolo de desconexión inesperada del cliente.
        /// </summary>
        /// <param name="client">Cliente que se ha desconectado.</param>
        public void ClientDisconnect(Client client) => ClientDisconnect((T)client);

        /// <summary>
        /// Protocolo de desconexión inesperada del cliente.
        /// </summary>
        /// <param name="client">Cliente que se ha desconectado.</param>
        public virtual void ClientDisconnect(T client)
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// Protocolo de bienvenida del cliente.
        /// </summary>
        /// <returns>
        /// <see langword="true" /> si el cliente fue aceptado por el protocolo,
        /// <see langword="false" /> en caso contrario.
        /// </returns>
        /// <param name="client">Cliente que será atendido.</param>
        public bool ClientWelcome(Client client) => ClientWelcome((T)client);
        /// <summary>
        /// Protocolo de bienvenida del cliente.
        /// </summary>
        /// <returns>
        /// <see langword="true" /> si el cliente fue aceptado por el protocolo,
        /// <see langword="false" /> en caso contrario.
        /// </returns>
        /// <param name="client">Cliente que será atendido.</param>
        public virtual bool ClientWelcome(T client)
        {
            return true;
        }

        /// <inheritdoc />
        /// <summary>
        /// Protocolo de atención al cliente
        /// </summary>
        /// <param name="client">Cliente que será atendido.</param>
        /// <param name="data">Datos recibidos desde el cliente.</param>
        public void ClientAttendant(Client client, byte[] data) => ClientAttendant((T)client, data);
        
        /// <summary>
        /// Protocolo de atención al cliente
        /// </summary>
        /// <param name="client">Cliente que será atendido.</param>
        /// <param name="data">Datos recibidos desde el cliente.</param>
        public abstract void ClientAttendant(T client, byte[] data);

        /// <inheritdoc/>
        public Server<T> MyServer { get; set; } = default!;

        /// <inheritdoc />
        T IProtocol<T>.CreateClient(TcpClient tcpClient)
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