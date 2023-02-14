﻿/*
NameValueCollectionExtensionsTests.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2023 César Andrés Morgan

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
using System.Text;
using TheXDS.MCART.Types.Extensions;

public class StringBuilderExtensionsTests
{
    [Test]
    public void AppendLineIfNotNull_Test()
    {
        StringBuilder sb = new();
        sb.AppendLineIfNotNull(null);
        Assert.True(sb.ToString().IsEmpty());
        sb.AppendLineIfNotNull("test");
        Assert.AreEqual($"test{Environment.NewLine}", sb.ToString());
        sb.AppendLineIfNotNull(null);
        Assert.AreEqual($"test{Environment.NewLine}", sb.ToString());
    }

    [Test]
    public void AppendAndWrap_Test()
    {
        StringBuilder sb = new();
        string s = new('x', 120);
        sb.AppendAndWrap(s, 80);
        string[]? sa = sb.ToString().Split(Environment.NewLine);
        Assert.AreEqual(80, sa[0].Length);
        Assert.AreEqual(40, sa[1].Length);
    }
}