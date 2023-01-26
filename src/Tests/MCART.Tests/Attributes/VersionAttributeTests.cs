/*
VersionAttributeTests.cs

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

public class VersionAttributeTests
{
    [Test]
    public void Ctor_test()
    {
        var a = new VersionAttribute(1, 2, 3, 4);
        Assert.AreEqual(1, a.Value.Major);
        Assert.AreEqual(2, a.Value.Minor);
        Assert.AreEqual(3, a.Value.Build);
        Assert.AreEqual(4, a.Value.Revision);

        a = new VersionAttribute(5, 6);
        Assert.AreEqual(5, a.Value.Major);
        Assert.AreEqual(6, a.Value.Minor);
        Assert.AreEqual(0, a.Value.Build);
        Assert.AreEqual(0, a.Value.Revision);

        a = new VersionAttribute(7.8);
        Assert.AreEqual(7, a.Value.Major);
        Assert.AreEqual(8, a.Value.Minor);
        Assert.AreEqual(0, a.Value.Build);
        Assert.AreEqual(0, a.Value.Revision);

        a = new VersionAttribute(9);
        Assert.AreEqual(9, a.Value.Major);
        Assert.AreEqual(0, a.Value.Minor);
        Assert.AreEqual(0, a.Value.Build);
        Assert.AreEqual(0, a.Value.Revision);
    }

    [Theory]
    [TestCase(double.NaN)]
    [TestCase(double.PositiveInfinity)]
    [TestCase(double.NegativeInfinity)]
    public void Ctor_contract_test(double invalidValue)
    {
        Assert.Throws<ArgumentException>(() => _ = new VersionAttribute(invalidValue));
    }
}