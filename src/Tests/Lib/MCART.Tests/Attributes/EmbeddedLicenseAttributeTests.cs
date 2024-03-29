﻿/*
EmbeddedLicenseAttributeTests.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2024 César Andrés Morgan

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

using TheXDS.MCART.Attributes;
using TheXDS.MCART.Exceptions;
using TheXDS.MCART.Resources;

namespace TheXDS.MCART.Tests.Attributes;

public class EmbeddedLicenseAttributeTests
{
    [Test]
    public void Ctor_string_string_test()
    {
        var a = new EmbeddedLicenseAttribute("value", "path");
        Assert.That("value", Is.EqualTo(a.Value));
        Assert.That("path", Is.EqualTo(a.Path));
        Assert.That(typeof(NullGetter), Is.EqualTo(a.CompressorType));
    }

    [Test]
    public void Ctor_string_string_type_test()
    {
        var a = new EmbeddedLicenseAttribute("value", "path", typeof(DeflateGetter));
        Assert.That("value", Is.EqualTo(a.Value));
        Assert.That("path", Is.EqualTo(a.Path));
        Assert.That(typeof(DeflateGetter), Is.EqualTo(a.CompressorType));
    }

    [Test]
    public void Ctor_string_string_type_contract_test()
    {
        Assert.Throws<InvalidTypeException>(()=> _ = new EmbeddedLicenseAttribute("value", "path", typeof(int)));
    }
}
