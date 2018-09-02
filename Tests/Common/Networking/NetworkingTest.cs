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
        public void DownloadTest()
        {
            var ms = new System.IO.MemoryStream();
            TheXDS.MCART.Networking.DownloadHelper.DownloadHttp("http://speedtest.ftp.otenet.gr/files/test100k.db", ms);
            Assert.Equal(102400, ms.Length);
        }
    }
}