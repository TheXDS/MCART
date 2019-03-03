/*
NetworkingTest.cs

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

#pragma warning disable CS1591

using Xunit;
using TheXDS.MCART.Networking;

namespace TheXDS.MCART.Tests.Networking
{
    public class NetworkingTest
    {
        [Fact]
        public void DownloadTest()
        {
            /*
             * Este Test tiene un problema...
             * El método DownloadHttp se prueba realizando una descarga desde
             * cualquier servidor, y dependiendo del servicio de host, es
             * probable que consideren en uso continuo de los mismos para
             * realizar las pruebas como abusivo. A este fin, se espera que en
             * el futuro se implemente un servidor contenido dentro del mismo
             * equipo para realizar pruebas de descarga.
             * Mientras tanto, estos son los servicios desde los cuales no se
             * recomienda probar para evitar caer en situaciones de abuso:
             * - https://www.thinkbroadband.com/download
             */
            using (var ms = new System.IO.MemoryStream())
            {
                DownloadHelper.DownloadHttp("http://speedtest.ftp.otenet.gr/files/test100k.db", ms);
                Assert.Equal(102400, ms.Length);
            }
        }
    }
}