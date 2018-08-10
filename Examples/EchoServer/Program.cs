/*
Program.cs

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

using System;
using System.Net.Sockets;
using TheXDS.MCART.Networking.Server;

namespace EchoServer
{
    internal static class Program
    {
        private class EchoProt : Protocol
        {
            /// <inheritdoc />
            /// <summary>
            /// Protocolo de atención al cliente
            /// </summary>
            /// <param name="client">Cliente que será atendido.</param>
            /// <param name="server">Servidor que atiende al cliente.</param>
            /// <param name="data">Datos recibidos desde el cliente.</param>
            public override void ClientAttendant(Client client, Server server, byte[] data)
            {
                client.Send(data);
                Console.WriteLine($"Solicitud de eco atendida. {data.Length}");
                if (data[0]=='z') client.Bye();
            }

            /// <inheritdoc />
            /// <summary>
            /// Protocolo de desconexión del cliente.
            /// </summary>
            /// <param name="client">Cliente que será atendido.</param>
            /// <param name="server">Servidor que atiende al cliente.</param>
            public override void ClientBye(Client client, Server server)
            {
                Console.WriteLine("Cliente desconectado correctamente.");
            }

            /// <inheritdoc />
            /// <summary>
            /// Protocolo de desconexión inesperada del cliente.
            /// </summary>
            /// <param name="client">Cliente que se ha desconectado.</param>
            /// <param name="server">Servidor que atiendía al cliente.</param>
            public override void ClientDisconnect(Client client, Server server)
            {
                Console.WriteLine("Cliente desconectado inesperadamente.");
            }

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
            public override Client CreateClient(TcpClient tcpClient)
            {
                return new Client(tcpClient);
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
            /// <param name="server">Servidor que atiende al cliente.</param>
            public override bool ClientWelcome(Client client, Server server)
            {
                Console.WriteLine("Un cliente se ha conectado.");
                return true;
            }
        }

        private static void Main()
        {
            var srv = new Server(new EchoProt(), 51200);
            srv.Start();
            if (!srv.IsAlive)
            {
                Console.WriteLine("El servidor de eco no se ha iniciado.");
                return;
            }
            Console.WriteLine("Servidor de eco activo. Presione cualquier tecla para salir.");
            Console.ReadKey();
            srv.Stop();
        }
    }
}