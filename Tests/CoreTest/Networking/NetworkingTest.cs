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

using TheXDS.MCART.Networking.Server;
using Xunit;
using System.Net;
using Cl = TheXDS.MCART.Networking.Client;

#if ExtrasBuiltIn
using TheXDS.MCART.Networking.Server.Protocols;
#endif

namespace CoreTest.Networking
{
    public class NetworkingTest
    {
        [Fact]
        public void TalkTest()
        {
            var srv = new Server(new Echo(), new IPEndPoint(IPAddress.Loopback, 51220));
            srv.Start();
            Assert.True(srv.IsAlive);
            var cl = new Cl.Client();
            cl.Connect("localhost");

            byte[] test = { 10, 20, 30, 40, 50 };
            var resp = cl.TalkToServer(test);
            cl.Disconnect();
            srv.Stop();

            Assert.Equal(test.Length, resp.Length);
            for (byte j = 0; j < 5; j++)
                Assert.True(test[j] == resp[j]);
        }
        [Fact]
        public void DownloadTest()
        {
            var ms = new System.IO.MemoryStream();
            TheXDS.MCART.Networking.DownloadHelper.DownloadHttp("http://ipv4.download.thinkbroadband.com/5MB.zip", ms);
            Assert.Equal(5242880, ms.Length);
        }

#if !ExtrasBuiltIn
        /// <summary>
        /// Protocolo simple de eco.
        /// </summary>
        /// <remarks>Este protocolo utiliza TCP/IP, no IGMP.</remarks>
        [MCART.Networking.Port(7)]
        class Echo : Protocol
        {
            /// <summary>
            /// Protocolo de atención normal.
            /// </summary>
            public override void ClientAttendant(Client client, Server<Client> server, byte[] data)
            {
                client.Send(data);
            }
        }
#endif
    }
}