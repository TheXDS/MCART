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
using Xunit;

namespace TheXDS.MCART.Tests.IO
{
    public class FileStreamUriParserTests
    {
        [Fact]
        public void Open_WithFullUri_Test()
        {
            var f = Path.GetTempFileName();
            var furi = $"file://{f.Replace('\\', '/')}";
            File.WriteAllText(f, "test");

            var fp = new FileStreamUriParser();
            var fu = fp.Open(new Uri(furi))!;

            Assert.NotNull(fu);
            Assert.IsAssignableFrom<Stream>(fu);
            using var r = new StreamReader(fu);
            Assert.Equal("test", r.ReadToEnd());
            fu.Dispose();
            r.Dispose();
            File.Delete(f);
        }

        [Fact]
        public void Open_WithFilePath_Test()
        {
            var f = Path.GetTempFileName();
            File.WriteAllText(f, "test");

            var fp = new FileStreamUriParser();
            var fu = fp.Open(new Uri(f))!;

            Assert.NotNull(fu);
            Assert.IsType<FileStream>(fu);
            using var r = new StreamReader(fu);
            Assert.Equal("test", r.ReadToEnd());
            fu.Dispose();
            r.Dispose();
            File.Delete(f);
        }
    }
}
