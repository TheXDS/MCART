//
//  NetworkingTest.cs
//
//  Author:
//       César Morgan <xds_xps_ivx@hotmail.com>
//
//  Copyright (c) 2017 César Morgan
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Linq;
using TheXDS.MCART.Networking.Server;
using Xunit;
using System.Net;
using System.Threading;

#if ExtrasBuiltIn
using TheXDS.MCART.Networking.Server.Protocols;
using Cl = TheXDS.MCART.Networking.Client.Protocols;
#endif

namespace CoreTest.Networking
{
    public class NetworkingTest
    {
        [Fact]
        public void Server_IntegrationTest()
        {
            var srv = new Server(new Echo(), new IPEndPoint(IPAddress.Loopback, 51220));
            srv.Start();
            Thread.Sleep(500); //Esperar a que el servidor arranque.

            Assert.True(srv.IsAlive);
            Assert.Equal(IPAddress.Loopback, srv.ListeningEndPoint.Address);
            Assert.Equal(51220, srv.ListeningEndPoint.Port);
            Assert.Empty(srv.Clients);
            
            var cl= new Cl.Echo();
            
            cl.Connect("localhost",51220);
            Thread.Sleep(500); //Esperar a que la conexión se realice.

            Assert.Single(srv.Clients);

            byte[] test = { 10, 20, 30, 40, 50 };
            var resp = cl.TalkToServer(test);
            cl.CloseConnection();
            srv.Stop();

            Assert.Equal(test, resp);
        }

        [Fact]
        public void ServerWithData_IntegrationTest()
        {
            var srv = new Server<Client<int>>(new NamedEcho());
            srv.Start();

            var cl = new Cl.Echo();
            cl.Connect("localhost", 51227);
            Thread.Sleep(500); //Esperar a que la conexión se realice.

            Assert.Single(srv.Clients);

            byte[] test = { 10, 20, 30, 40, 50 };
            var resp = cl.TalkToServer(test);
            cl.CloseConnection();
            srv.Stop();

            Assert.Equal(BitConverter.GetBytes(0x123456).Concat(test), resp);
        }


        [Fact]
        public void DownloadTest()
        {
            var ms = new System.IO.MemoryStream();
            TheXDS.MCART.Networking.DownloadHelper.DownloadHttp("http://ipv4.download.thinkbroadband.com/5MB.zip", ms);
            Assert.Equal(5242880, ms.Length);
        }

        [TheXDS.MCART.Networking.Port(51227)]
        internal class NamedEcho : Protocol<Client<int>>
        {
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
            public override bool ClientWelcome(Client<int> client, Server<Client<int>> server)
            {
                client.ClientData = 0x123456;
                return true;
            }

            /// <summary>
            /// Protocolo de atención al cliente
            /// </summary>
            /// <param name="client">Cliente que será atendido.</param>
            /// <param name="server">Servidor que atiende al cliente.</param>
            /// <param name="data">Datos recibidos desde el cliente.</param>
            public override void ClientAttendant(Client<int> client, Server<Client<int>> server, byte[] data)
            {
                client.Send(BitConverter.GetBytes(client.ClientData).Concat(data));
            }
        }

#if !ExtrasBuiltIn
        /// <inheritdoc />
        /// <summary>
        /// Protocolo simple de eco.
        /// </summary>
        /// <remarks>Este protocolo utiliza TCP/IP, no IGMP.</remarks>
        [TheXDS.MCART.Networking.Port(7)]
        private class Echo : Protocol
        {
            /// <inheritdoc />
            /// <summary>
            /// Protocolo de atención al cliente
            /// </summary>
            /// <param name="client">Cliente que será atendido.</param>
            /// <param name="server">Servidor que atiende al cliente.</param>
            /// <param name="data">Datos recibidos desde el cliente.</param>
            public override void ClientAttendant(Client client, Server<Client> server, byte[] data)
            {
                client.Send(data);
            }
        }
#endif
    }
}