/*
IProtocol.cs

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

using System.Net.Sockets;

namespace TheXDS.MCART.Networking.Legacy.Server
{
    /// <summary>
    /// Determina una serie de funciones a implementar por una clase que provea
    /// de protocolos a un servidor.
    /// </summary>
    public interface ISimpleProtocol : IProtocol<Client>
    {
    }

    /// <summary>
    /// Determina una serie de funciones a implementar por una clase que provea
    /// de protocolos a un servidor.
    /// </summary>
    /// <typeparam name="T">
    /// Tipo de cliente atendido por el protocolo.
    /// </typeparam>
    public interface IProtocol<in T> : IProtocol where T : Client
    {
        /// <summary>
        /// Atiende al cliente
        /// </summary>
        /// <param name="client">Cliente que será atendido.</param>
        /// <param name="data">Datos recibidos desde el cliente.</param>
        void ClientAttendant(T client, byte[] data);

        /// <summary>
        /// Protocolo de desconexión del cliente.
        /// </summary>
        /// <param name="client">Cliente que será atendido.</param>
        void ClientBye(T client);

        /// <summary>
        /// Protocolo de desconexión inesperada del cliente.
        /// </summary>
        /// <param name="client">Cliente que se ha desconectado.</param>
        void ClientDisconnect(T client);

        /// <summary>
        /// Protocolo de bienvenida del cliente.
        /// </summary>
        /// <returns>
        /// <see langword="true" /> si el cliente fue aceptado por el
        /// protocolo, <see langword="false" /> en caso contrario.
        /// </returns>
        /// <param name="client">Cliente que será atendido.</param>
        bool ClientWelcome(T client);        
    }

    /// <summary>
    /// Define una serie de mirmbros a implementar por un tipo que permita
    /// construir clientes como parte de las acciones de un protocolo.
    /// </summary>
    public interface IProtocol
    {
        /// <summary>
        /// Inicializa un nuevo cliente manejado por este protocolo.
        /// </summary>
        /// <param name="tcpClient">
        /// <see cref="TcpClient" /> de la conexión con el host remoto.
        /// </param>
        /// <returns>
        /// Un nuevo <see cref="Client"/>.
        /// </returns>
        Client CreateClient(TcpClient tcpClient);
    }
}