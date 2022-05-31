﻿/*
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
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheXDS.MCART.Types.Extensions;

public class StreamExtensionsTests
{
    [Test]
    public void Destroy_Test()
    {
        using MemoryStream ms = new();
        ms.Write(Enumerable.Range(1, 100).Select(p => (byte)p).ToArray());
        ms.Destroy();
        Assert.AreEqual(0, ms.Length);
    }

    [Test]
    public void Skip_Test()
    {
        using MemoryStream ms = new();
        ms.Write(Enumerable.Range(1, 100).Select(p => (byte)p).ToArray());
        ms.Seek(0, SeekOrigin.Begin);
        ms.Skip(50);
        Assert.AreEqual(50, ms.Position);
    }

    [Test]
    public void Skip_Contract_Test()
    {
        using MemoryStream ms = new();
        ms.Write(Enumerable.Range(1, 100).Select(p => (byte)p).ToArray());
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
        ms.Write(Enumerable.Range(1, 100).Select(p => (byte)p).ToArray());
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
        byte[]? a = Encoding.Default.GetBytes("test");
        using MemoryStream ms = new();
        ms.WriteBytes(a);
        ms.Seek(0, SeekOrigin.Begin);
        Assert.AreEqual(a, ms.ToArray());
    }

    [Test]
    public void WriteSeveralBytes_Test()
    {
        byte[]? a = Encoding.Default.GetBytes("TEST");
        byte[]? b = Encoding.Default.GetBytes("test");
        using MemoryStream ms = new();
        ms.WriteSeveralBytes(a, b);
        ms.Seek(0, SeekOrigin.Begin);
        Assert.AreEqual(a.Concat(b).ToArray(), ms.ToArray());
    }
}
