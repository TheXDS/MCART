/*
NameValueCollectionExtensionsTests.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheXDS.MCART.Types.Extensions;
using NUnit.Framework;

namespace TheXDS.MCART.Tests.Types.Extensions
{
    public class StreamExtensionsTests
    {
        [Test]
        public void Destroy_Test()
        {
            using MemoryStream ms = new();
            ms.Write(Enumerable.Range(1, 100).Select(p => (byte) p).ToArray());
            ms.Destroy();
            Assert.AreEqual(0, ms.Length);
        }
        
        [Test]
        public void Skip_Test()
        {
            using MemoryStream ms = new();
            ms.Write(Enumerable.Range(1, 100).Select(p => (byte) p).ToArray());
            ms.Seek(0, SeekOrigin.Begin);
            ms.Skip(50);
            Assert.AreEqual(50, ms.Position);
        }

        [Test]
        public void Skip_Contract_Test()
        {
            using MemoryStream ms = new();
            ms.Write(Enumerable.Range(1, 100).Select(p => (byte) p).ToArray());
            Assert.Throws<ArgumentOutOfRangeException>(() => ms.Skip(10));
            ms.Seek(0, SeekOrigin.Begin);
            ms.Skip(50);
            Assert.AreEqual(50, ms.Position);
            Assert.Throws<ArgumentOutOfRangeException>(() => ms.Skip(51));
            Assert.Throws<ArgumentOutOfRangeException>(() => ms.Skip(-1));
        }

        [Test]
        public void ReadString_Test()
        {
            using MemoryStream ms = new();
            ms.Write(Encoding.Default.GetBytes("TESTtest"));
            ms.Seek(0, SeekOrigin.Begin);
            Assert.AreEqual("TEST", ms.ReadString(4));
            Assert.AreEqual("test", ms.ReadString(4));
        }

        [Test]
        public void RemainingBytes_Test()
        {
            using MemoryStream ms = new();
            ms.Write(Enumerable.Range(1, 100).Select(p => (byte) p).ToArray());
            ms.Seek(0, SeekOrigin.Begin);
            Assert.AreEqual(100, ms.RemainingBytes());
            ms.Skip(50);
            Assert.AreEqual(50, ms.RemainingBytes());
        }
        
        [Test]
        public async Task ReadStringAsync_Test()
        {
            await using MemoryStream ms = new();
            await ms.WriteAsync(Encoding.Default.GetBytes("TESTtest"));
            ms.Seek(0, SeekOrigin.Begin);
            Assert.AreEqual("TEST", await ms.ReadStringAsync(4));
            Assert.AreEqual("test", await ms.ReadStringAsync(4));
            ms.Seek(0, SeekOrigin.Begin);
            Assert.AreEqual("TEST", await ms.ReadStringAsync(4, Encoding.Default));
            Assert.AreEqual("test", await ms.ReadStringAsync(4, Encoding.Default));
        }
        
        [Test]
        public async Task ReadStringToEndAsync_Test()
        {
            await using MemoryStream ms = new();
            await ms.WriteAsync(Encoding.Default.GetBytes("TESTtest"));
            ms.Seek(0, SeekOrigin.Begin);
            Assert.AreEqual("TESTtest", await ms.ReadStringToEndAsync());
        }
        
        [Test]
        public async Task ReadStringToAsync_Test()
        {
            await using MemoryStream ms = new();
            await ms.WriteAsync(Encoding.Default.GetBytes("TESTtest"));
            Assert.AreEqual("test", await ms.ReadStringToAsync(4));
            Assert.AreEqual(0, ms.RemainingBytes());
            Assert.AreEqual("TESTtest", await ms.ReadStringToAsync(0));
        }
        
        [Test]
        public void WriteBytes_Test()
        {
            var a = Encoding.Default.GetBytes("test");
            using MemoryStream ms = new();
            ms.WriteBytes(a);
            ms.Seek(0, SeekOrigin.Begin);
            Assert.AreEqual(a, ms.ToArray());
        }
        
                
        [Test]
        public void WriteSeveralBytes_Test()
        {
            var a = Encoding.Default.GetBytes("TEST");
            var b = Encoding.Default.GetBytes("test");
            using MemoryStream ms = new();
            ms.WriteSeveralBytes(a, b);
            ms.Seek(0, SeekOrigin.Begin);
            Assert.AreEqual(a.Concat(b).ToArray(), ms.ToArray());
        }
    }
}