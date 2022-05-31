/*
NetworkingTest.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2022 César Andrés Morgan

Permission is hereby granted, free of charge, to any person obtaining a copy of
this software and associated documentation files (the "Software"), to deal in
the Software without restriction, including without limitation the rights to
use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
of the Software, and to permit persons to whom the Software is furnished to do
so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

namespace TheXDS.MCART.Tests.Networking;
using NUnit.Framework;
using System;
using TheXDS.MCART.Networking;

[Obsolete("Estos objetos utilizan clases deprecadas en .Net 6.")]
public class NetworkingTest
{
    //[Test]
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
        using System.IO.MemoryStream? ms = new();
        DownloadHelper.DownloadHttp("http://speedtest.ftp.otenet.gr/files/test100k.db", ms);
        Assert.AreEqual(102400, ms.Length);
    }
}
