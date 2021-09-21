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
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheXDS.MCART.Types.Extensions;
using Xunit;

namespace TheXDS.MCART.Tests.Types.Extensions
{
    public class NameValueCollectionExtensionsTests
    {
        private static NameValueCollection GetCollection(bool withNull = false)
        {
            var c = new NameValueCollection
            {
                {"name1", "value1"},
                {"name2", "value2"},
                {"name3", "value3"},
                {"name1", "value4"},
                {"name2", "value5"},
                {"name3", "value6"},
            };
            if (withNull)
            {
                c.Add(null, "value7");
                c.Add(null, "value8");
            }
            return c;
        }
        
        [Fact]
        public void ToGroup_Test()
        {
            var g = GetCollection(withNull: true).ToGroup().ToArray();
            Assert.Equal(4, g.Length);
            Assert.Equal("name1", g[0].Key);
            Assert.Equal(new[] { "value1", "value4" },g[0].ToArray());
            Assert.Equal("name2", g[1].Key);
            Assert.Equal(new[] { "value2", "value5" },g[1].ToArray());
            Assert.Equal("name3", g[2].Key);
            Assert.Equal(new[] { "value3", "value6" },g[2].ToArray());
            Assert.Null(g[3].Key);
            Assert.Equal(new[] { "value7", "value8" },g[3].ToArray());
        }
        
        [Fact]
        public void ToNamedObjectCollection_Test()
        {
            var g = GetCollection().ToNamedObjectCollection().ToArray();
            Assert.Equal(3, g.Length);
            Assert.Equal("name1", g[0].Name);
            Assert.Equal(new[] { "value1", "value4" },g[0].Value);
            Assert.Equal("name2", g[1].Name);
            Assert.Equal(new[] { "value2", "value5" },g[1].Value);
            Assert.Equal("name3", g[2].Name);
            Assert.Equal(new[] { "value3", "value6" },g[2].Value);
        }
        
        [Fact]
        public void ToKeyValuePair_Test()
        {
            var g = GetCollection(withNull: true).ToKeyValuePair().ToArray();
            Assert.Equal(4, g.Length);
            Assert.Equal("name1", g[0].Key);
            Assert.Equal("value1,value4", g[0].Value);
            Assert.Equal("name2", g[1].Key);
            Assert.Equal("value2,value5", g[1].Value);
            Assert.Equal("name3", g[2].Key);
            Assert.Equal("value3,value6", g[2].Value);
            Assert.Null(g[3].Key);
            Assert.Equal("value7,value8", g[3].Value);
        }

        [Fact]
        public void ToDictionary_Test()
        {
            var g = GetCollection().ToDictionary();
            Assert.Equal(3, g.Keys.Count);
            Assert.Equal(new[] { "value1", "value4" },g["name1"]);
            Assert.Equal(new[] { "value2", "value5" },g["name2"]);
            Assert.Equal(new[] { "value3", "value6" },g["name3"]);
        }
    }

    public class StreamExtensionsTests
    {
        [Fact]
        public void Destroy_Test()
        {
            using MemoryStream ms = new();
            ms.Write(Enumerable.Range(1, 100).Select(p => (byte) p).ToArray());
            ms.Destroy();
            Assert.Equal(0, ms.Length);
        }
        
        [Fact]
        public void Skip_Test()
        {
            using MemoryStream ms = new();
            ms.Write(Enumerable.Range(1, 100).Select(p => (byte) p).ToArray());
            ms.Seek(0, SeekOrigin.Begin);
            ms.Skip(50);
            Assert.Equal(50, ms.Position);
        }

        [Fact]
        public void Skip_Contract_Test()
        {
            using MemoryStream ms = new();
            ms.Write(Enumerable.Range(1, 100).Select(p => (byte) p).ToArray());
            Assert.Throws<ArgumentOutOfRangeException>(() => ms.Skip(10));
            ms.Seek(0, SeekOrigin.Begin);
            ms.Skip(50);
            Assert.Equal(50, ms.Position);
            Assert.Throws<ArgumentOutOfRangeException>(() => ms.Skip(51));
            Assert.Throws<ArgumentOutOfRangeException>(() => ms.Skip(-1));
        }

        [Fact]
        public void ReadString_Test()
        {
            using MemoryStream ms = new();
            ms.Write(Encoding.Default.GetBytes("TESTtest"));
            ms.Seek(0, SeekOrigin.Begin);
            Assert.Equal("TEST", ms.ReadString(4));
            Assert.Equal("test", ms.ReadString(4));
        }

        [Fact]
        public void RemainingBytes_Test()
        {
            using MemoryStream ms = new();
            ms.Write(Enumerable.Range(1, 100).Select(p => (byte) p).ToArray());
            ms.Seek(0, SeekOrigin.Begin);
            Assert.Equal(100, ms.RemainingBytes());
            ms.Skip(50);
            Assert.Equal(50, ms.RemainingBytes());
        }
        
        [Fact]
        public async Task ReadStringAsync_Test()
        {
            await using MemoryStream ms = new();
            await ms.WriteAsync(Encoding.Default.GetBytes("TESTtest"));
            ms.Seek(0, SeekOrigin.Begin);
            Assert.Equal("TEST", await ms.ReadStringAsync(4));
            Assert.Equal("test", await ms.ReadStringAsync(4));
            ms.Seek(0, SeekOrigin.Begin);
            Assert.Equal("TEST", await ms.ReadStringAsync(4, Encoding.Default));
            Assert.Equal("test", await ms.ReadStringAsync(4, Encoding.Default));
        }
        
        [Fact]
        public async Task ReadStringToEndAsync_Test()
        {
            await using MemoryStream ms = new();
            await ms.WriteAsync(Encoding.Default.GetBytes("TESTtest"));
            ms.Seek(0, SeekOrigin.Begin);
            Assert.Equal("TESTtest", await ms.ReadStringToEndAsync());
        }
        
        [Fact]
        public async Task ReadStringToAsync_Test()
        {
            await using MemoryStream ms = new();
            await ms.WriteAsync(Encoding.Default.GetBytes("TESTtest"));
            Assert.Equal("test", await ms.ReadStringToAsync(4));
            Assert.Equal(0, ms.RemainingBytes());
            Assert.Equal("TESTtest", await ms.ReadStringToAsync(0));
        }
        
        [Fact]
        public void WriteBytes_Test()
        {
            var a = Encoding.Default.GetBytes("test");
            using MemoryStream ms = new();
            ms.WriteBytes(a);
            ms.Seek(0, SeekOrigin.Begin);
            Assert.Equal(a, ms.ToArray());
        }
        
                
        [Fact]
        public void WriteSeveralBytes_Test()
        {
            var a = Encoding.Default.GetBytes("TEST");
            var b = Encoding.Default.GetBytes("test");
            using MemoryStream ms = new();
            ms.WriteSeveralBytes(a, b);
            ms.Seek(0, SeekOrigin.Begin);
            Assert.Equal(a.Concat(b).ToArray(), ms.ToArray());
        }
    }

    public class StringBuilderExtensionsTests
    {
        [Fact]
        public void AppendLineIfNotNull_Test()
        {
            StringBuilder sb = new();
            sb.AppendLineIfNotNull(null);
            Assert.True(sb.ToString().IsEmpty());
            sb.AppendLineIfNotNull("test");
            Assert.Equal($"test{Environment.NewLine}", sb.ToString());
            sb.AppendLineIfNotNull(null);
            Assert.Equal($"test{Environment.NewLine}", sb.ToString());
        }

        [Fact]
        public void AppendAndWrap_Test()
        {
            StringBuilder sb = new();
            string s = new('x', 120);
            sb.AppendAndWrap(s, 80);
            Assert.Collection(sb.ToString().Split(Environment.NewLine),
                x => Assert.Equal(80, x.Length),
                x => Assert.Equal(40, x.Length));
        }
    }
}