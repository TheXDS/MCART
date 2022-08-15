/*
PointTests.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2022 César Andrés Morgan

Permission is hereby granted, free of charge, to any person obtaining a copy of
this software and associated documentation files (the “Software”), to deal in
the Software without restriction, including without limitation the rights to
use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
of the Software, and to permit persons to whom the Software is furnished to do
so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE. 
*/

using NUnit.Framework;
using System;
using TheXDS.MCART.Types.Base;
using TheXDS.MCART.Types.Entity;

namespace TheXDS.MCART.Ext.ComplexTypes.Tests;

public class PointTests
{
    [Test]
    public void Ctor_test()
    {
        Point c1 = new() { X = 15, Y = 8 };
        Point c2 = new(15, 8);
        Assert.AreEqual(15, c1.X);
        Assert.AreEqual(8, c1.Y);
        Assert.AreEqual(15, c2.X);
        Assert.AreEqual(8, c2.Y);
        Assert.AreEqual(c1, c2);
    }

    [Test]
    public void GetHashCode_test()
    {
        Point c1 = new() { X = 15, Y = 8 };
        Point c2 = new() { X = 15, Y = 8 };
        Point c3 = new() { X = 12, Y = 5 };

        Assert.AreNotSame(c1, c2);
        Assert.AreEqual(c1.GetHashCode(), c2.GetHashCode());
        Assert.AreNotSame(c1, c3);
        Assert.AreNotEqual(c1.GetHashCode(), c3.GetHashCode());
    }

    [Test]
    public void ComplexTypeToNormalTypeTest()
    {
        Point c1 = new() { X = 15, Y = 8 };
        Types.Point c2 = new(15, 8);

        Assert.AreEqual(c2, (Types.Point)c1);
        Assert.AreEqual(c1, (Point)c2);
    }

    [Test]
    public void ToString_Test()
    {
        Point p = new(3, 5);

        Assert.AreEqual("3, 5", p.ToString());
        Assert.AreEqual("3, 5", p.ToString("C"));
        Assert.AreEqual("[3, 5]", p.ToString("B"));
        Assert.AreEqual("X: 3, Y: 5", p.ToString("V"));
        Assert.AreEqual("X: 3\nY: 5", p.ToString("N"));

        Assert.Throws<FormatException>(() => p.ToString("???"));
    }

    [Test]
    public void Equals_Test()
    {
        Point p = new(3, 5);
        Point q = new(3, 5);
        Point r = new(5, 3);

        Assert.True(p.Equals(q));
        Assert.False(p.Equals(r));
        Assert.True(p.Equals((IVector)q));
        Assert.False(p.Equals((IVector)r));
        Assert.True(p.Equals((object?)q));
        Assert.False(p.Equals((object?)r));
        Assert.False(p.Equals(Guid.NewGuid()));
        Assert.False(p.Equals((object?)null));
        Assert.False(p!.Equals((IVector?)null));
    }

    [Test]
    public void NotEquals_Test()
    {
        Point p = new(3, 5);
        Point q = new(3, 5);
        Point r = new(5, 3);

        Assert.True(p != r);
        Assert.False(p != q);
        Assert.True(p != (IVector)r);
        Assert.False(p != (IVector)q);
    }
}
