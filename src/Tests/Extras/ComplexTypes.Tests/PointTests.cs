/*
PointTests.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2024 César Andrés Morgan

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
        Assert.That(15, Is.EqualTo(c1.X));
        Assert.That(8, Is.EqualTo(c1.Y));
        Assert.That(15, Is.EqualTo(c2.X));
        Assert.That(8, Is.EqualTo(c2.Y));
        Assert.That(c1, Is.EqualTo(c2));
    }

    [Test]
    public void GetHashCode_test()
    {
        Point c1 = new() { X = 15, Y = 8 };
        Point c2 = new() { X = 15, Y = 8 };
        Point c3 = new() { X = 12, Y = 5 };

        Assert.That(c1, Is.Not.SameAs(c2));
        Assert.That(c1.GetHashCode(), Is.EqualTo(c2.GetHashCode()));
        Assert.That(c1, Is.Not.SameAs(c3));
        Assert.That(c1.GetHashCode(), Is.Not.EqualTo(c3.GetHashCode()));
    }

    [Test]
    public void ComplexTypeToNormalTypeTest()
    {
        Point c1 = new() { X = 15, Y = 8 };
        Types.Point c2 = new(15, 8);

        Assert.That(c2, Is.EqualTo((Types.Point)c1));
        Assert.That(c1, Is.EqualTo((Point)c2));
    }

    [Test]
    public void ToString_Test()
    {
        Point p = new(3, 5);

        Assert.That("3, 5", Is.EqualTo(p.ToString()));
        Assert.That("3, 5", Is.EqualTo(p.ToString("C")));
        Assert.That("[3, 5]", Is.EqualTo(p.ToString("B")));
        Assert.That("X: 3, Y: 5", Is.EqualTo(p.ToString("V")));
        Assert.That("X: 3\nY: 5", Is.EqualTo(p.ToString("N")));

        Assert.Throws<FormatException>(() => p.ToString("???"));
    }

    [TestCase(3, 5)]
    [TestCase(5, double.NaN)]
    [TestCase(double.NaN, 3)]
    [TestCase(double.NaN, double.NaN)]
    public void Equals_Test(double x, double y)
    {
        Point p = new(x, y);
        Point q = new(x, y);
        Point r = new(5, 3);

        Assert.That(p.Equals(q));
        Assert.That(p.Equals(r), Is.False);
        Assert.That(p.Equals(null), Is.False);
        Assert.That(p!.Equals((IVector)q));
        Assert.That(p.Equals((IVector)r), Is.False);
        Assert.That(p.Equals((object?)q));
        Assert.That(p.Equals((object?)r), Is.False);
        Assert.That(p.Equals(Guid.NewGuid()), Is.False);
        Assert.That(p.Equals((object?)null), Is.False);
        Assert.That(p!.Equals((IVector?)null), Is.False);
    }

    [Test]
    public void NotEquals_Test()
    {
        Point p = new(3, 5);
        Point q = new(3, 5);
        Point r = new(5, 3);

        Assert.That(p != r);
        Assert.That(p != q, Is.False);
        Assert.That(p != (IVector)r);
        Assert.That(p != (IVector)q, Is.False);
    }
}
