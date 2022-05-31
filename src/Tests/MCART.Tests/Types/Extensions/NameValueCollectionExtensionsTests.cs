/*
NameValueCollectionExtensionsTests.cs

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

namespace TheXDS.MCART.Tests.Types.Extensions;
using NUnit.Framework;
using System.Collections.Specialized;
using System.Linq;
using TheXDS.MCART.Types.Extensions;

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
