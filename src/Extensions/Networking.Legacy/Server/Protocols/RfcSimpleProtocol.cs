﻿/*
RfcSimpleProtocol.cs

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

#if ExtrasBuiltIn

using System.Net.Sockets;

namespace TheXDS.MCART.Networking.Legacy.Server.Protocols
{
    /// <summary>
    /// Clase base para los protocolos simples definidos en los RFC de
    /// estándares de internet que envían una respuesta a un cliente y cierran
    /// la conexión.
    /// </summary>
    public abstract class RfcSimpleProtocol : ISimpleProtocol
    {
        /// <summary>
        /// Atiende al cliente (en caso de una conexión UDP)
        /// </summary>
        /// <param name="client">Cliente que será atendido.</param>
        /// <param name="data">Datos recibidos desde el cliente.</param>
        public void ClientAttendant(Client client, byte[] data)
        {
            Send(client);
        }

        /// <summary>
        /// Protocolo de desconexión del cliente.
        /// </summary>
        /// <param name="client">Cliente que será atendido.</param>
        public void ClientBye(Client client)
        {
        }

        /// <summary>
        /// Protocolo de desconexión inesperada del cliente.
        /// </summary>
        /// <param name="client">Cliente que se ha desconectado.</param>
        public void ClientDisconnect(Client client)
        {
        }

        /// <summary>
        /// Protocolo de bienvenida del cliente.
        /// </summary>
        /// <returns>
        /// Esta implementación siempre devuelve <see langword="false" />,
        /// lo cual cerrará la conexión luego de enviada la respuesta, tal
        /// como lo dicta el estándar RFC 868.
        /// </returns>
        /// <param name="client">Cliente que será atendido.</param>
        public bool ClientWelcome(Client client)
        {
            Send(client);
            return false;
        }

        /// <summary>
        /// Inicializa un nuevo cliente manejado por este protocolo.
        /// </summary>
        /// <param name="tcpClient">
        /// <see cref="TcpClient" /> de la conexión con el host remoto.
        /// </param>
        /// <returns>
        /// Un nuevo <see cref="Client" />.
        /// </returns>
        public Client CreateClient(TcpClient tcpClient)
        {
            return new Client(tcpClient);
        }

        /// <summary>
        /// Envía una respuesta al cliente antes de iniciar la desconexión.
        /// </summary>
        /// <param name="client">Cliente al cual enviar una respuesta.</param>
        protected abstract void Send(Client client);
    }
}

#endif