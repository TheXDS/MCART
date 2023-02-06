/*
SizeTests.cs

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

public class SizeTests
{
    [Test]
    public void Constants_test()
    {
        static void Test(Size value, double size)
        {
            Assert.AreEqual(value.Width, size);
            Assert.AreEqual(value.Height, size);
        }
        Test(Size.Nothing, double.NaN);
        Test(Size.Zero, 0.0);
        Test(Size.Infinity, double.PositiveInfinity);
    }

    [Theory]
    [TestCase("15,12", 15, 12)]
    [TestCase("15;12", 15, 12)]
    [TestCase("15:12", 15, 12)]
    [TestCase("15|12", 15, 12)]
    [TestCase("15 12", 15, 12)]
    [TestCase("15, 12", 15, 12)]
    [TestCase("15; 12", 15, 12)]
    [TestCase("15 - 12", 15, 12)]
    [TestCase("15 : 12", 15, 12)]
    [TestCase("15 | 12", 15, 12)]
    public void TryParseNumbers_Test(string value, double x, double y)
    {
        Assert.True(Size.TryParse(value, out Size p));
        Assert.AreEqual(x, p.Width);
        Assert.AreEqual(y, p.Height);
    }

    [Theory]
    [TestCase("Nothing")]
    [TestCase("")]
    [TestCase((string?)null)]
    [TestCase("NaN;NaN")]
    [TestCase(null)]
    public void TryParseNothing_Test(string? data)
    {
        Assert.True(Size.TryParse(data, out Size p));
        Assert.True(Size.Nothing.Equals(p));
    }

    [Theory]
    [TestCase("Zero")]
    [TestCase("0")]
    [TestCase("0;0")]
    public void TryParseZero_Test(string data)
    {
        Assert.True(Size.TryParse(data, out Size p));
        Assert.AreEqual(Size.Zero, p);
    }

    [Theory]
    [TestCase("Infinity")]
    [TestCase("PositiveInfinity")]
    [TestCase("∞")]
    [TestCase("∞;∞")]
    public void TryParseInfinity_Test(string data)
    {
        Assert.True(Size.TryParse(data, out Size p));
        Assert.AreEqual(Size.Infinity, p);
    }

    [Test]
    public void Parse_Test()
    {
        Assert.IsAssignableFrom<Size>(Size.Parse("Zero"));
        Assert.Throws<FormatException>(() => Size.Parse("Test"));
    }

    [Test]
    public void ToString_Test()
    {
        Size p = new(3, 5);

        Assert.AreEqual("3, 5", p.ToString());
        Assert.AreEqual("3, 5", p.ToString("C"));
        Assert.AreEqual("[3, 5]", p.ToString("B"));
        Assert.AreEqual("Width: 3, Height: 5", p.ToString("V"));
        Assert.AreEqual("Width: 3\nHeight: 5", p.ToString("N").Replace("\r\n", "\n"));
        Assert.Throws<FormatException>(() => p.ToString("???"));
    }

    [Test]
    public void Equals_Test()
    {
        Size p = new(3, 5);
        Size q = new(3, 5);
        Size r = new(5, 3);

        Assert.True(p.Equals(q));
        Assert.False(p.Equals(r));
        Assert.True(p.Equals((ISize)q));
        Assert.False(p.Equals((ISize)r));
        Assert.True(p.Equals((IVector)q));
        Assert.False(p.Equals((IVector)r));
        Assert.True(p.Equals((object?)q));
        Assert.False(p.Equals((object?)r));
        Assert.False(p.Equals(Guid.NewGuid()));
        Assert.False(p.Equals((object?)null));
        Assert.False(p.Equals((ISize?)null));
        Assert.False(p.Equals((IVector?)null));
    }

    [Test]
    public void Equals_operator_test()
    {
        Size p = new(3, 5);
        Size q = new(3, 5);
        Size r = new(5, 3);

        Assert.True(p == q);
        Assert.False(p == r);
        Assert.True(p == (ISize)q);
        Assert.False(p == (ISize)r);
        Assert.True(p == (IVector)q);
        Assert.False(p == (IVector)r);
    }

    [Test]
    public void NotEquals_Test()
    {
        Size p = new(3, 5);
        Size q = new(3, 5);
        Size r = new(5, 3);

        Assert.True(p != r);
        Assert.False(p != q);
        Assert.True(p != (ISize)r);
        Assert.False(p != (ISize)q);
        Assert.True(p != (IVector)r);
        Assert.False(p != (IVector)q);
    }

    [Test]
    public void NotEquals_operator_test()
    {
        Size p = new(3, 5);
        Size q = new(3, 5);
        Size r = new(5, 3);

        Assert.True(p != r);
        Assert.False(p != q);
        Assert.True(p != (ISize)r);
        Assert.False(p != (ISize)q);
        Assert.True(p != (IVector)r);
        Assert.False(p != (IVector)q);
    }

    [Test]
    public void GetHashCode_Test()
    {
        Assert.AreEqual(new Size(3, 5).GetHashCode(), new Size(3, 5).GetHashCode());
        Assert.AreNotEqual(new Size(3, 5).GetHashCode(), new Size(1, 1).GetHashCode());
    }

    [Theory]
    [TestCase(3, 5, 2, 4, 5, 9)]
    [TestCase(1, 1, 2, 2, 3, 3)]
    [TestCase(-1, -1, 1, 1, 0, 0)]
    [TestCase(-3, 5, 2, -4, -1, 1)]
    public void AddOperator_Test(int x1, int y1, int x2, int y2, int x3, int y3)
    {
        Size p = new(x1, y1);
        Size q = new(x2, y2);
        Size r = p + q;

        Assert.AreEqual(x3, r.Width);
        Assert.AreEqual(y3, r.Height);

        Size s = p + (ISize)q;
        Assert.AreEqual(x3, s.Width);
        Assert.AreEqual(y3, s.Height);

        Size t = p + (IVector)q;
        Assert.AreEqual(x3, t.Width);
        Assert.AreEqual(y3, t.Height);

        Assert.AreEqual(x3, (p + x2).Width);
        Assert.AreEqual(y3, (p + y2).Height);
    }

    [Theory]
    [TestCase(3, 5, 2, 4, 1, 1)]
    [TestCase(1, 1, 2, 2, -1, -1)]
    [TestCase(-1, -1, 1, 1, -2, -2)]
    [TestCase(-3, 5, 2, -4, -5, 9)]
    public void SubtractOperator_Test(int x1, int y1, int x2, int y2, int x3, int y3)
    {
        Size p = new(x1, y1);
        Size q = new(x2, y2);
        Size r = p - q;

        Assert.AreEqual(x3, r.Width);
        Assert.AreEqual(y3, r.Height);

        Size s = p - (ISize)q;
        Assert.AreEqual(x3, s.Width);
        Assert.AreEqual(y3, s.Height);

        Size t = p - (IVector)q;
        Assert.AreEqual(x3, t.Width);
        Assert.AreEqual(y3, t.Height);

        Assert.AreEqual(x3, (p - x2).Width);
        Assert.AreEqual(y3, (p - y2).Height);
    }

    [Theory]
    [TestCase(3, 5, 2, 4, 6, 20)]
    [TestCase(1, 1, 2, 2, 2, 2)]
    [TestCase(-1, -1, 1, 1,-1, -1)]
    [TestCase(-3, 5, 2, -4, -6, -20)]
    public void MultiplyOperator_Test(int x1, int y1, int x2, int y2, int x3, int y3)
    {
        Size p = new(x1, y1);
        Size q = new(x2, y2);
        Size r = p * q;

        Assert.AreEqual(x3, r.Width);
        Assert.AreEqual(y3, r.Height);

        Size s = p * (ISize)q;
        Assert.AreEqual(x3, s.Width);
        Assert.AreEqual(y3, s.Height);

        Size t = p * (IVector)q;
        Assert.AreEqual(x3, t.Width);
        Assert.AreEqual(y3, t.Height);

        Assert.AreEqual(x3, (p * x2).Width);
        Assert.AreEqual(y3, (p * y2).Height);
    }

    [Theory]
    [TestCase(3, 5, 2, 4, 1.5, 1.25)]
    [TestCase(1, 1, 2, 2, 0.5, 0.5)]
    [TestCase(-1, -1, 1, 1, -1, -1)]
    [TestCase(-3, 5, 2, -4, -1.5, -1.25)]
    public void DivideOperator_Test(int x1, int y1, int x2, int y2, double x3, double y3)
    {
        Size p = new(x1, y1);
        Size q = new(x2, y2);
        Size r = p / q;

        Assert.AreEqual(x3, r.Width);
        Assert.AreEqual(y3, r.Height);

        Size s = p / (ISize)q;
        Assert.AreEqual(x3, s.Width);
        Assert.AreEqual(y3, s.Height);

        Size t = p / (IVector)q;
        Assert.AreEqual(x3, t.Width);
        Assert.AreEqual(y3, t.Height);

        Assert.AreEqual(x3, (p / x2).Width);
        Assert.AreEqual(y3, (p / y2).Height);
    }

    [Theory]
    [TestCase(3, 5, 2, 4, 1, 1)]
    [TestCase(1, 1, 2, 2, 1, 1)]
    [TestCase(-1, -1, 1, 1, 0, 0)]
    [TestCase(-3, 5, 2, -4, -1, 1)]
    [TestCase(13, 14, 5, 3, 3, 2)]
    public void ModulusOperator_Test(int x1, int y1, int x2, int y2, double x3, double y3)
    {
        Size p = new(x1, y1);
        Size q = new(x2, y2);
        Size r = p % q;

        Assert.AreEqual(x3, r.Width);
        Assert.AreEqual(y3, r.Height);

        Size s = p % (ISize)q;
        Assert.AreEqual(x3, s.Width);
        Assert.AreEqual(y3, s.Height);

        Size t = p % (IVector)q;
        Assert.AreEqual(x3, t.Width);
        Assert.AreEqual(y3, t.Height);

        Assert.AreEqual(x3, (p % x2).Width);
        Assert.AreEqual(y3, (p % y2).Height);
    }

    [Test]
    public void Add1Operator_Test()
    {
        Size p = new(3, 5);
        p++;
        Assert.AreEqual(4, p.Width);
        Assert.AreEqual(6, p.Height);
    }

    [Test]
    public void Subtract1Operator_Test()
    {
        Size p = new(3, 5);
        p--;
        Assert.AreEqual(2, p.Width);
        Assert.AreEqual(4, p.Height);
    }

    [Test]
    public void PlusOperator_Test()
    {
        Size p = +new Size(3, 5);
        Assert.AreEqual(3, p.Width);
        Assert.AreEqual(5, p.Height);

        p = +new Size(-1, -2);
        Assert.AreEqual(-1, p.Width);
        Assert.AreEqual(-2, p.Height);
    }

    [Test]
    public void MinusOperator_Test()
    {
        Size p = -new Size(3, 5);
        Assert.AreEqual(-3, p.Width);
        Assert.AreEqual(-5, p.Height);

        p = -new Size(-1, -2);
        Assert.AreEqual(1, p.Width);
        Assert.AreEqual(2, p.Height);
    }

    [Theory]
    [TestCase(2, 2, 4)]
    [TestCase(1, 1, 1)]
    [TestCase(3, 5, 15)]
    public void CubeVolume_test(int width, int height, double area)
    {
        Assert.AreEqual(area, new Size(width, height).SquareArea);
    }

    [Test]
    public void IsZero_test()
    {
        Assert.IsTrue(Size.Zero.IsZero);
        Assert.IsFalse(new Size(1, 2).IsZero);
        Assert.IsFalse(new Size(1, -2).IsZero);
        Assert.IsFalse(new Size(1, double.PositiveInfinity).IsZero);
        Assert.IsFalse(new Size(1, double.NaN).IsZero);
        Assert.IsFalse(Size.Nothing.IsZero);
        Assert.IsFalse(Size.Infinity.IsZero);
    }

    [Test]
    public void IsReal_test()
    {
        Assert.IsFalse(Size.Zero.IsReal);
        Assert.IsTrue(new Size(1, 2).IsReal);
        Assert.IsFalse(new Size(1, -2).IsReal);
        Assert.IsFalse(new Size(1, double.PositiveInfinity).IsReal);
        Assert.IsFalse(new Size(1, double.NaN).IsReal);
        Assert.IsFalse(Size.Nothing.IsReal);
        Assert.IsFalse(Size.Infinity.IsReal);
    }

    [Test]
    public void IsValid_test()
    {
        Assert.IsTrue(Size.Zero.IsValid);
        Assert.IsTrue(new Size(1, 2).IsValid);
        Assert.IsTrue(new Size(1, -2).IsValid);
        Assert.IsFalse(new Size(1, double.PositiveInfinity).IsValid);
        Assert.IsFalse(new Size(1, double.NaN).IsValid);
        Assert.IsFalse(Size.Nothing.IsValid);
        Assert.IsFalse(Size.Infinity.IsValid);
    }

    [Theory]
    [TestCase(1.0, 2.0)]
    [TestCase(3.2, 5.3)]
    [TestCase(-2.1, -3.8)]
    public void Implicit_conversion_test(double x, double y)
    {
        Size s1 = new(x, y);
        var s2 = (System.Drawing.Size)s1;
        var s3 = (System.Drawing.SizeF)s1;
        var s4 = (Size)s2;
        var s5 = (Size)s3;
        Assert.AreEqual((int)x, s2.Width);
        Assert.AreEqual((int)y, s2.Height);
        Assert.IsTrue(s3.Width - x < 0.000001);
        Assert.IsTrue(s3.Height - y < 0.000001);
        Assert.AreEqual((int)x, s4.Width);
        Assert.AreEqual((int)y, s4.Height);
        Assert.IsTrue(s5.Width - x < 0.000001);
        Assert.IsTrue(s5.Height - y < 0.000001);
    }
}