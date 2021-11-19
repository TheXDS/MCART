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

using System.Collections.Specialized;
using System.Linq;
using TheXDS.MCART.Types.Extensions;
using NUnit.Framework;

namespace TheXDS.MCART.Tests.Types.Extensions
{
    public class NameValueCollectionExtensionsTests
    {
        private static NameValueCollection GetCollection(bool withNull = false)
        {
            NameValueCollection? c = new()
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

        [Test]
        public void ToGroup_Test()
        {
            IGrouping<string?, string>[]? g = GetCollection(withNull: true).ToGroup().ToArray();
            Assert.AreEqual(4, g.Length);
            Assert.AreEqual("name1", g[0].Key);
            Assert.AreEqual(new[] { "value1", "value4" }, g[0].ToArray());
            Assert.AreEqual("name2", g[1].Key);
            Assert.AreEqual(new[] { "value2", "value5" }, g[1].ToArray());
            Assert.AreEqual("name3", g[2].Key);
            Assert.AreEqual(new[] { "value3", "value6" }, g[2].ToArray());
            Assert.Null(g[3].Key);
            Assert.AreEqual(new[] { "value7", "value8" }, g[3].ToArray());
        }

        [Test]
        public void ToNamedObjectCollection_Test()
        {
            MCART.Types.NamedObject<string[]>[]? g = GetCollection().ToNamedObjectCollection().ToArray();
            Assert.AreEqual(3, g.Length);
            Assert.AreEqual("name1", g[0].Name);
            Assert.AreEqual(new[] { "value1", "value4" }, g[0].Value);
            Assert.AreEqual("name2", g[1].Name);
            Assert.AreEqual(new[] { "value2", "value5" }, g[1].Value);
            Assert.AreEqual("name3", g[2].Name);
            Assert.AreEqual(new[] { "value3", "value6" }, g[2].Value);
        }

        [Test]
        public void ToKeyValuePair_Test()
        {
            System.Collections.Generic.KeyValuePair<string?, string>[]? g = GetCollection(withNull: true).ToKeyValuePair().ToArray();
            Assert.AreEqual(4, g.Length);
            Assert.AreEqual("name1", g[0].Key);
            Assert.AreEqual("value1,value4", g[0].Value);
            Assert.AreEqual("name2", g[1].Key);
            Assert.AreEqual("value2,value5", g[1].Value);
            Assert.AreEqual("name3", g[2].Key);
            Assert.AreEqual("value3,value6", g[2].Value);
            Assert.Null(g[3].Key);
            Assert.AreEqual("value7,value8", g[3].Value);
        }

        [Test]
        public void ToDictionary_Test()
        {
            System.Collections.Generic.Dictionary<string, string[]>? g = GetCollection().ToDictionary();
            Assert.AreEqual(3, g.Keys.Count);
            Assert.AreEqual(new[] { "value1", "value4" }, g["name1"]);
            Assert.AreEqual(new[] { "value2", "value5" }, g["name2"]);
            Assert.AreEqual(new[] { "value3", "value6" }, g["name3"]);
        }
    }
}