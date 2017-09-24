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

using MCART.Networking.Server;
using MCART.Networking.Server.Protocols;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using Cl = MCART.Networking.Client;

namespace CoreTests.Networking
{
    [TestClass]
    public class NetworkingTest
    {
        [TestMethod]
        public void TalkTest()
        {
            Server srv = new Server(new Echo(), new IPEndPoint(IPAddress.Any, 51220));
            srv.Start();
            Assert.IsTrue(srv.IsAlive);
            Cl.Client cl = new Cl.Client();
            cl.Connect("localhost");

            byte[] test = new byte[] { 10, 20, 30, 40, 50 };
            byte[] resp = cl.TalkToServer(test);
            cl.Disconnect();
            srv.Stop();
            cl = null;
            srv = null;

            Assert.AreEqual(test.Length, resp.Length);
            for (byte j = 0; j < 5; j++)
                Assert.IsTrue(test[j] == resp[j]);
        }
    }
}