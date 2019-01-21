/*
Echo.cs

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

#if ExtrasBuiltIn

using System;
using System.Diagnostics.CodeAnalysis;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.Networking.Server.Protocols
{
    /// <inheritdoc />
    /// <summary>
    ///     Protocolo de sincronización de hora basado en el estándar RFC 868.
    /// </summary>
    [Port(37)]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class Time : IProtocol
    {
        /// <inheritdoc />
        /// <summary>
        ///     Atiende al cliente (en caso de una conexión UDP)
        /// </summary>
        /// <param name="client">Cliente que será atendido.</param>
        /// <param name="data">Datos recibidos desde el cliente.</param>
        public void ClientAttendant(Client client, byte[] data)
        {
            Send(client);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Protocolo de desconexión del cliente.
        /// </summary>
        /// <param name="client">Cliente que será atendido.</param>
        public void ClientBye(Client client)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Protocolo de desconexión inesperada del cliente.
        /// </summary>
        /// <param name="client">Cliente que se ha desconectado.</param>
        public void ClientDisconnect(Client client)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Protocolo de bienvenida del cliente.
        /// </summary>
        /// <returns>
        ///     Esta implementación siempre devuelve <see langword="false" />,
        ///     lo cual cerrará la conexión luego de enviada la respuesta, tal
        ///     como lo dicta el estándar RFC 868.
        /// </returns>
        /// <param name="client">Cliente que será atendido.</param>
        public bool ClientWelcome(Client client)
        {
            Send(client);
            return false;
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
        public Client CreateClient(System.Net.Sockets.TcpClient tcpClient)
        {
            return new Client(tcpClient);
        }

        private static void Send(Client client)
        {
            unchecked
            {
                var l = (int)DateTime.Now.ToTimestamp(DateTimeExtensions.CenturyEpoch);
                if (BitConverter.IsLittleEndian) l = l.FlipEndianess();
                client.Send(BitConverter.GetBytes(l));
            }
        }
    }
}

#endif