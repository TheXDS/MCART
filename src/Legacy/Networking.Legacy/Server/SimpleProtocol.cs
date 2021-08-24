/*
ServerProtocol.cs

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
    /// Define un protocolo sumamente simple.
    /// </summary>
    public abstract class SimpleProtocol : IProtocol
    {
        /// <summary>
        /// Atiende al cliente
        /// </summary>
        /// <param name="client">Cliente que será atendido.</param>
        /// <param name="data">Datos recibidos desde el cliente.</param>
        public abstract void ClientAttendant(Client client, byte[] data);

        /// <summary>
        /// Protocolo de desconexión del cliente.
        /// </summary>
        /// <param name="client">Cliente que será atendido.</param>
        public void ClientBye(Client client) { }

        /// <summary>
        /// Protocolo de desconexión inesperada del cliente.
        /// </summary>
        /// <param name="client">Cliente que se ha desconectado.</param>
        public void ClientDisconnect(Client client) { }

        /// <summary>
        /// Protocolo de bienvenida del cliente.
        /// </summary>
        /// <returns>
        /// <see langword="true" /> si el cliente fue aceptado por el
        /// protocolo, <see langword="false" /> en caso contrario.
        /// </returns>
        /// <param name="client">Cliente que será atendido.</param>
        public bool ClientWelcome(Client client) => true;

        /// <summary>
        /// Inicializa un nuevo cliente manejado por este protocolo.
        /// </summary>
        /// <param name="tcpClient">
        /// <see cref="TcpClient" /> de la conexión
        /// con el host remoto.
        /// </param>
        /// <returns>
        /// Un nuevo <see cref="Client" />.
        /// </returns>
        public Client CreateClient(TcpClient tcpClient) => new(tcpClient);
    }
}