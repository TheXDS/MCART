/*
FileStreamUriParserTests.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Este archivo contiene todas las pruebas pertenecientes a la clase estática
TheXDS.MCART.Common.

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

using System;
using System.IO;
using TheXDS.MCART.IO;
using NUnit.Framework;

namespace TheXDS.MCART.Tests.IO
{
    //[Obsolete("Estos objetos utilizan clases deprecadas en .Net 6.")]
    public class FileStreamUriParserTests
    {
        [Test]
        public void Open_WithFullUri_Test()
        {
            string? f = Path.GetTempFileName();
            string? furi = $"file://{f.Replace('\\', '/')}";
            File.WriteAllText(f, "test");

            FileStreamUriParser? fp = new();
            Stream? fu = fp.Open(new Uri(furi))!;

            Assert.NotNull(fu);
            Assert.IsInstanceOf<Stream>(fu);
            using StreamReader? r = new(fu);
            Assert.AreEqual("test", r.ReadToEnd());
            fu.Dispose();
            r.Dispose();
            File.Delete(f);
        }

        [Test]
        public void Open_WithFilePath_Test()
        {
            string? f = Path.GetTempFileName();
            File.WriteAllText(f, "test");

            FileStreamUriParser? fp = new();
            Stream? fu = fp.Open(new Uri(f))!;

            Assert.NotNull(fu);
            Assert.IsInstanceOf<FileStream>(fu);
            using StreamReader? r = new(fu);
            Assert.AreEqual("test", r.ReadToEnd());
            fu.Dispose();
            r.Dispose();
            File.Delete(f);
        }
    }
}
