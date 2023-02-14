﻿/*
ServerAttributeTests.cs

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

using NUnit.Framework;
using System;
using TheXDS.MCART.Attributes;

namespace TheXDS.MCART.Tests.Attributes;

public class ServerAttributeTests
{
    [Test]
    public void Ctor_test()
    {
        var a = new ServerAttribute("test.com", 51200);
        Assert.AreEqual("test.com", a.Server);
        Assert.AreEqual(51200, a.Port);
    }

    [Test]
    public void ToString_test()
    {
        var a = new ServerAttribute("test.com", 51200);
        Assert.AreEqual("test.com:51200", a.ToString());
    }

    [Test]
    public void Attribute_as_IValueAttribute_of_string_test()
    {
        IValueAttribute<string> a = new ServerAttribute("test.com", 51200);
        Assert.AreEqual("test.com:51200", a.Value);
    }

    [Test]
    public void Ctor_contract_test()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _ = new ServerAttribute("test.com", 0));
        Assert.Throws<ArgumentOutOfRangeException>(() => _ = new ServerAttribute("test.com", 65536));
        Assert.Throws<ArgumentException>(() => _ = new ServerAttribute(string.Empty, 1234));
        Assert.Throws<ArgumentNullException>(() => _ = new ServerAttribute(null!, 1234));
        Assert.Throws<ArgumentException>(() => _ = new ServerAttribute("    ", 1234));
    }
}