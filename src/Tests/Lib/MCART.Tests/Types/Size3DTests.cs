/*
Size3DTests.cs

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
using TheXDS.MCART.Types;
using TheXDS.MCART.Types.Base;

namespace TheXDS.MCART.Tests.Types;

public class Size3DTests
{
    [Test]
    public void Constants_test()
    {
        static void Test(Size3D value, double size)
        {
            Assert.AreEqual(value.Width, size);
            Assert.AreEqual(value.Height, size);
            Assert.AreEqual(value.Depth, size);
        }
        Test(Size3D.Nothing, double.NaN);
        Test(Size3D.Zero, 0.0);
        Test(Size3D.Infinity, double.PositiveInfinity);
    }

    [Theory]
    [TestCase("15,12,9", 15, 12, 9)]
    [TestCase("15;12;9", 15, 12, 9)]
    [TestCase("15:12:9", 15, 12, 9)]
    [TestCase("15|12|9", 15, 12, 9)]
    [TestCase("15 12 9", 15, 12, 9)]
    [TestCase("15, 12, 9", 15, 12, 9)]
    [TestCase("15; 12; 9", 15, 12, 9)]
    [TestCase("15 - 12 - 9", 15, 12, 9)]
    [TestCase("15 : 12 : 9", 15, 12, 9)]
    [TestCase("15 | 12 | 9", 15, 12, 9)]
    public void TryParseNumbers_Test(string value, double x, double y, double z)
    {
        Assert.True(Size3D.TryParse(value, out Size3D p));
        Assert.AreEqual(x, p.Width);
        Assert.AreEqual(y, p.Height);
        Assert.AreEqual(z, p.Depth);
    }

    [Theory]
    [TestCase("Nothing")]
    [TestCase("")]
    [TestCase((string?)null)]
    [TestCase("NaN;NaN;NaN")]
    [TestCase(null)]
    public void TryParseNothing_Test(string? data)
    {
        Assert.True(Size3D.TryParse(data, out Size3D p));
        Assert.True(Size3D.Nothing.Equals(p));
    }

    [Theory]
    [TestCase("Zero")]
    [TestCase("0")]
    [TestCase("0;0;0")]
    public void TryParseZero_Test(string data)
    {
        Assert.True(Size3D.TryParse(data, out Size3D p));
        Assert.AreEqual(Size3D.Zero, p);
    }

    [Theory]
    [TestCase("Infinity")]
    [TestCase("PositiveInfinity")]
    [TestCase("∞")]
    [TestCase("∞;∞;∞")]
    public void TryParseInfinity_Test(string data)
    {
        Assert.True(Size3D.TryParse(data, out Size3D p));
        Assert.AreEqual(Size3D.Infinity, p);
    }

    [Test]
    public void Parse_Test()
    {
        Assert.IsAssignableFrom<Size3D>(Size3D.Parse("Zero"));
        Assert.Throws<FormatException>(() => Size3D.Parse("Test"));
    }

    [Test]
    public void ToString_Test()
    {
        Size3D p = new(3, 5, 7);

        Assert.AreEqual("3, 5, 7", p.ToString());
        Assert.AreEqual("3, 5, 7", p.ToString("C"));
        Assert.AreEqual("[3, 5, 7]", p.ToString("B"));
        Assert.AreEqual("Width: 3, Height: 5, Depth: 7", p.ToString("V"));
        Assert.AreEqual("Width: 3\nHeight: 5\nDepth: 7", p.ToString("N").Replace("\r\n", "\n"));
        Assert.Throws<FormatException>(() => p.ToString("???"));
    }

    [Test]
    public void Equals_Test()
    {
        Size3D p = new(3, 5, 7);
        Size3D q = new(3, 5, 7);
        Size3D r = new(5, 3, 1);

        Assert.True(p.Equals(q));
        Assert.False(p.Equals(r));
        Assert.True(p.Equals((ISize3D)q));
        Assert.False(p.Equals((ISize3D)r));
        Assert.True(p.Equals((ISize3D)q));
        Assert.False(p.Equals((ISize3D)r));
        Assert.True(p.Equals((IVector3D)q));
        Assert.False(p.Equals((IVector3D)r));
        Assert.True(p.Equals((object?)q));
        Assert.False(p.Equals((object?)r));
        Assert.False(p.Equals(Guid.NewGuid()));
        Assert.False(p.Equals((object?)null));
        Assert.False(p.Equals((ISize3D?)null));
        Assert.False(p.Equals((IVector3D?)null));
    }

    [Test]
    public void NotEquals_Test()
    {
        Size3D p = new(3, 5, 7);
        Size3D q = new(3, 5, 7);
        Size3D r = new(5, 3, 1);

        Assert.True(p != r);
        Assert.False(p != q);
        Assert.True(p != (ISize3D)r);
        Assert.False(p != (ISize3D)q);
        Assert.True(p != (IVector3D)r);
        Assert.False(p != (IVector3D)q);
    }

    [Test]
    public void GetHashCode_Test()
    {
        Assert.AreEqual(new Size3D(3, 5, 7).GetHashCode(), new Size3D(3, 5, 7).GetHashCode());
        Assert.AreNotEqual(new Size3D(3, 5, 7).GetHashCode(), new Size3D(1, 1, 1).GetHashCode());
    }

    [Theory]
    [TestCase(3, 5, 7, 2, 4, 6, 5, 9, 13)]
    [TestCase(1, 1, 1, 2, 2, 2, 3, 3, 3)]
    [TestCase(-1, -1, -1, 1, 1, 1, 0, 0, 0)]
    [TestCase(-3, 5, -7, 2, -4, 6, -1, 1, -1)]
    public void AddOperator_Test(int x1, int y1, int z1, int x2, int y2, int z2, int x3, int y3, int z3)
    {
        Size3D p = new(x1, y1, z1);
        Size3D q = new(x2, y2, z2);
        Size3D r = p + q;

        Assert.AreEqual(x3, r.Width);
        Assert.AreEqual(y3, r.Height);
        Assert.AreEqual(z3, r.Depth);

        Size3D s = p + (ISize3D)q;
        Assert.AreEqual(x3, s.Width);
        Assert.AreEqual(y3, s.Height);
        Assert.AreEqual(z3, s.Depth);

        Assert.AreEqual(x3, (p + x2).Width);
        Assert.AreEqual(y3, (p + y2).Height);
        Assert.AreEqual(z3, (p + z2).Depth);
    }

    [Theory]
    [TestCase(3, 5, 7, 2, 4, 6, 1, 1, 1)]
    [TestCase(1, 1, 1, 2, 2, 2, -1, -1, -1)]
    [TestCase(-1, -1, -1, 1, 1, 1, -2, -2, -2)]
    [TestCase(-3, 5, -7, 2, -4, 6, -5, 9, -13)]
    public void SubtractOperator_Test(int x1, int y1, int z1, int x2, int y2, int z2, int x3, int y3, int z3)
    {
        Size3D p = new(x1, y1, z1);
        Size3D q = new(x2, y2, z2);
        Size3D r = p - q;

        Assert.AreEqual(x3, r.Width);
        Assert.AreEqual(y3, r.Height);
        Assert.AreEqual(z3, r.Depth);

        Size3D s = p - (ISize3D)q;
        Assert.AreEqual(x3, s.Width);
        Assert.AreEqual(y3, s.Height);
        Assert.AreEqual(z3, s.Depth);

        Assert.AreEqual(x3, (p - x2).Width);
        Assert.AreEqual(y3, (p - y2).Height);
        Assert.AreEqual(z3, (p - z2).Depth);
    }

    [Theory]
    [TestCase(3, 5, 7, 2, 4, 6, 6, 20, 42)]
    [TestCase(1, 1, 1, 2, 2, 2, 2, 2, 2)]
    [TestCase(-1, -1, -1, 1, 1, 1, -1, -1, -1)]
    [TestCase(-3, 5, -7, 2, -4, 6, -6, -20, -42)]
    public void MultiplyOperator_Test(int x1, int y1, int z1, int x2, int y2, int z2, int x3, int y3, int z3)
    {
        Size3D p = new(x1, y1, z1);
        Size3D q = new(x2, y2, z2);
        Size3D r = p * q;

        Assert.AreEqual(x3, r.Width);
        Assert.AreEqual(y3, r.Height);
        Assert.AreEqual(z3, r.Depth);

        Size3D s = p * (ISize3D)q;
        Assert.AreEqual(x3, s.Width);
        Assert.AreEqual(y3, s.Height);
        Assert.AreEqual(z3, s.Depth);

        Assert.AreEqual(x3, (p * x2).Width);
        Assert.AreEqual(y3, (p * y2).Height);
        Assert.AreEqual(z3, (p * z2).Depth);
    }

    [Theory]
    [TestCase(3, 5, 7, 2, 4, 6, 1.5, 1.25, 7.0 / 6.0)]
    [TestCase(1, 1, 1, 2, 2, 2, 0.5, 0.5, 0.5)]
    [TestCase(-1, -1, -1, 1, 1, 1, -1, -1, -1)]
    [TestCase(-3, 5, -7, 2, -4, 6, -1.5, -1.25, -7.0 / 6.0)]
    public void DivideOperator_Test(int x1, int y1, int z1, int x2, int y2, int z2, double x3, double y3, double z3)
    {
        Size3D p = new(x1, y1, z1);
        Size3D q = new(x2, y2, z2);
        Size3D r = p / q;

        Assert.AreEqual(x3, r.Width);
        Assert.AreEqual(y3, r.Height);
        Assert.AreEqual(z3, r.Depth);

        Size3D s = p / (ISize3D)q;
        Assert.AreEqual(x3, s.Width);
        Assert.AreEqual(y3, s.Height);
        Assert.AreEqual(z3, s.Depth);

        Assert.AreEqual(x3, (p / x2).Width);
        Assert.AreEqual(y3, (p / y2).Height);
        Assert.AreEqual(z3, (p / z2).Depth);
    }

    [Theory]
    [TestCase(3, 5, 7, 2, 4, 6, 1, 1, 1)]
    [TestCase(1, 1, 1, 2, 2, 2, 1, 1, 1)]
    [TestCase(-1, -1, -1, 1, 1, 1, 0, 0, 0)]
    [TestCase(-3, 5, -7, 2, -4, 6, -1, 1, -1)]
    [TestCase(13, 14, 15, 5, 3, 7, 3, 2, 1)]
    public void ModulusOperator_Test(int x1, int y1, int z1, int x2, int y2, int z2, double x3, double y3, double z3)
    {
        Size3D p = new(x1, y1, z1);
        Size3D q = new(x2, y2, z2);
        Size3D r = p % q;

        Assert.AreEqual(x3, r.Width);
        Assert.AreEqual(y3, r.Height);
        Assert.AreEqual(z3, r.Depth);

        Size3D s = p % (ISize3D)q;
        Assert.AreEqual(x3, s.Width);
        Assert.AreEqual(y3, s.Height);
        Assert.AreEqual(z3, s.Depth);

        Assert.AreEqual(x3, (p % x2).Width);
        Assert.AreEqual(y3, (p % y2).Height);
        Assert.AreEqual(z3, (p % z2).Depth);
    }

    [Test]
    public void Add1Operator_Test()
    {
        Size3D p = new(3, 5, 7);
        p++;
        Assert.AreEqual(4, p.Width);
        Assert.AreEqual(6, p.Height);
        Assert.AreEqual(8, p.Depth);
    }

    [Test]
    public void Subtract1Operator_Test()
    {
        Size3D p = new(3, 5, 7);
        p--;
        Assert.AreEqual(2, p.Width);
        Assert.AreEqual(4, p.Height);
        Assert.AreEqual(6, p.Depth);
    }

    [Test]
    public void PlusOperator_Test()
    {
        Size3D p = +new Size3D(3, 5, 7);
        Assert.AreEqual(3, p.Width);
        Assert.AreEqual(5, p.Height);
        Assert.AreEqual(7, p.Depth);

        p = +new Size3D(-1, -2, -3);
        Assert.AreEqual(-1, p.Width);
        Assert.AreEqual(-2, p.Height);
        Assert.AreEqual(-3, p.Depth);
    }

    [Test]
    public void MinusOperator_Test()
    {
        Size3D p = -new Size3D(3, 5, 7);
        Assert.AreEqual(-3, p.Width);
        Assert.AreEqual(-5, p.Height);
        Assert.AreEqual(-7, p.Depth);

        p = -new Size3D(-1, -2, -3);
        Assert.AreEqual(1, p.Width);
        Assert.AreEqual(2, p.Height);
        Assert.AreEqual(3, p.Depth);
    }

    [Theory]
    [TestCase(2, 2, 2, 8)]
    [TestCase(1, 1, 1, 1)]
    [TestCase(3, 5, 7, 105)]
    public void CubeVolume_test(int width, int height, int depth, double vol)
    {
        Assert.AreEqual(vol, new Size3D(width, height, depth).CubeVolume);
    }

    [Test]
    public void IsZero_test()
    {
        Assert.IsTrue(Size3D.Zero.IsZero);
        Assert.IsFalse(new Size3D(1, 2, 3).IsZero);
        Assert.IsFalse(new Size3D(1, 2, -3).IsZero);
        Assert.IsFalse(new Size3D(1, 2, double.PositiveInfinity).IsZero);
        Assert.IsFalse(new Size3D(1, 2, double.NaN).IsZero);
        Assert.IsFalse(Size3D.Nothing.IsZero);
        Assert.IsFalse(Size3D.Infinity.IsZero);
    }

    [Test]
    public void IsReal_test()
    {
        Assert.IsFalse(Size3D.Zero.IsReal);
        Assert.IsTrue(new Size3D(1, 2, 3).IsReal);
        Assert.IsFalse(new Size3D(1, 2, -3).IsReal);
        Assert.IsFalse(new Size3D(1, 2, double.PositiveInfinity).IsReal);
        Assert.IsFalse(new Size3D(1, 2, double.NaN).IsReal);
        Assert.IsFalse(Size3D.Nothing.IsReal);
        Assert.IsFalse(Size3D.Infinity.IsReal);
    }

    [Test]
    public void IsValid_test()
    {
        Assert.IsTrue(Size3D.Zero.IsValid);
        Assert.IsTrue(new Size3D(1, 2, 3).IsValid);
        Assert.IsTrue(new Size3D(1, 2, -3).IsValid);
        Assert.IsFalse(new Size3D(1, 2, double.PositiveInfinity).IsValid);
        Assert.IsFalse(new Size3D(1, 2, double.NaN).IsValid);
        Assert.IsFalse(Size3D.Nothing.IsValid);
        Assert.IsFalse(Size3D.Infinity.IsValid);
    }
}
