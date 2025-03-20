/*
SizeTests.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2025 César Andrés Morgan

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
            Assert.That(value.Width, Is.EqualTo(size));
            Assert.That(value.Height, Is.EqualTo(size));
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
        Assert.That(Size.TryParse(value, out Size p));
        Assert.That(x, Is.EqualTo(p.Width));
        Assert.That(y, Is.EqualTo(p.Height));
    }

    [Theory]
    [TestCase("Nothing")]
    [TestCase("")]
    [TestCase((string?)null)]
    [TestCase("NaN;NaN")]
    [TestCase(null)]
    public void TryParseNothing_Test(string? data)
    {
        Assert.That(Size.TryParse(data, out Size p));
        Assert.That(Size.Nothing.Equals(p));
    }

    [Theory]
    [TestCase("Zero")]
    [TestCase("0")]
    [TestCase("0;0")]
    public void TryParseZero_Test(string data)
    {
        Assert.That(Size.TryParse(data, out Size p));
        Assert.That(Size.Zero, Is.EqualTo(p));
    }

    [Theory]
    [TestCase("Infinity")]
    [TestCase("PositiveInfinity")]
    [TestCase("∞")]
    [TestCase("∞;∞")]
    public void TryParseInfinity_Test(string data)
    {
        Assert.That(Size.TryParse(data, out Size p));
        Assert.That(Size.Infinity, Is.EqualTo(p));
    }

    [Test]
    public void Parse_Test()
    {
        Assert.That(Size.Parse("Zero"), Is.AssignableFrom<Size>());
        Assert.Throws<FormatException>(() => Size.Parse("Test"));
    }

    [Test]
    public void ToString_Test()
    {
        Size p = new(3, 5);

        Assert.That("3, 5", Is.EqualTo(p.ToString()));
        Assert.That("3, 5", Is.EqualTo(p.ToString("C")));
        Assert.That("[3, 5]", Is.EqualTo(p.ToString("B")));
        Assert.That("Width: 3, Height: 5", Is.EqualTo(p.ToString("V")));
        Assert.That("Width: 3\nHeight: 5", Is.EqualTo(p.ToString("N").Replace("\r\n", "\n")));
        Assert.Throws<FormatException>(() => p.ToString("???"));
    }

    [Test]
    public void Equals_Test()
    {
        Size p = new(3, 5);
        Size q = new(3, 5);
        Size r = new(5, 3);

        Assert.That(p.Equals(q));
        Assert.That(p.Equals(r), Is.False);
        Assert.That(p.Equals((ISize)q));
        Assert.That(p.Equals((ISize)r), Is.False);
        Assert.That(p.Equals((IVector)q));
        Assert.That(p.Equals((IVector)r), Is.False);
        Assert.That(p.Equals((object?)q));
        Assert.That(p.Equals((object?)r), Is.False);
        Assert.That(p.Equals(Guid.NewGuid()), Is.False);
        Assert.That(p.Equals((object?)null), Is.False);
        Assert.That(p.Equals((ISize?)null), Is.False);
        Assert.That(p.Equals((IVector?)null), Is.False);
    }

    [Test]
    public void Equals_operator_test()
    {
        Size p = new(3, 5);
        Size q = new(3, 5);
        Size r = new(5, 3);

        Assert.That(p == q);
        Assert.That(p == r, Is.False);
        Assert.That(p == (ISize)q);
        Assert.That(p == (ISize)r, Is.False);
        Assert.That(p == (IVector)q);
        Assert.That(p == (IVector)r, Is.False);
    }

    [Test]
    public void NotEquals_Test()
    {
        Size p = new(3, 5);
        Size q = new(3, 5);
        Size r = new(5, 3);

        Assert.That(p != r);
        Assert.That(p != q, Is.False);
        Assert.That(p != (ISize)r);
        Assert.That(p != (ISize)q, Is.False);
        Assert.That(p != (IVector)r);
        Assert.That(p != (IVector)q, Is.False);
    }

    [Test]
    public void NotEquals_operator_test()
    {
        Size p = new(3, 5);
        Size q = new(3, 5);
        Size r = new(5, 3);

        Assert.That(p != r);
        Assert.That(p != q, Is.False);
        Assert.That(p != (ISize)r);
        Assert.That(p != (ISize)q, Is.False);
        Assert.That(p != (IVector)r);
        Assert.That(p != (IVector)q, Is.False);
    }

    [Test]
    public void GetHashCode_Test()
    {
        Assert.That(new Size(3, 5).GetHashCode(), Is.EqualTo(new Size(3, 5).GetHashCode()));
        Assert.That(new Size(3, 5).GetHashCode(), Is.Not.EqualTo(new Size(1, 1).GetHashCode()));
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

        Assert.That(x3, Is.EqualTo(r.Width));
        Assert.That(y3, Is.EqualTo(r.Height));

        Size s = p + (ISize)q;
        Assert.That(x3, Is.EqualTo(s.Width));
        Assert.That(y3, Is.EqualTo(s.Height));

        Size t = p + (IVector)q;
        Assert.That(x3, Is.EqualTo(t.Width));
        Assert.That(y3, Is.EqualTo(t.Height));

        Assert.That(x3, Is.EqualTo((p + x2).Width));
        Assert.That(y3, Is.EqualTo((p + y2).Height));
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

        Assert.That(x3, Is.EqualTo(r.Width));
        Assert.That(y3, Is.EqualTo(r.Height));

        Size s = p - (ISize)q;
        Assert.That(x3, Is.EqualTo(s.Width));
        Assert.That(y3, Is.EqualTo(s.Height));

        Size t = p - (IVector)q;
        Assert.That(x3, Is.EqualTo(t.Width));
        Assert.That(y3, Is.EqualTo(t.Height));

        Assert.That(x3, Is.EqualTo((p - x2).Width));
        Assert.That(y3, Is.EqualTo((p - y2).Height));
    }

    [Theory]
    [TestCase(3, 5, 2, 4, 6, 20)]
    [TestCase(1, 1, 2, 2, 2, 2)]
    [TestCase(-1, -1, 1, 1, -1, -1)]
    [TestCase(-3, 5, 2, -4, -6, -20)]
    public void MultiplyOperator_Test(int x1, int y1, int x2, int y2, int x3, int y3)
    {
        Size p = new(x1, y1);
        Size q = new(x2, y2);
        Size r = p * q;

        Assert.That(x3, Is.EqualTo(r.Width));
        Assert.That(y3, Is.EqualTo(r.Height));

        Size s = p * (ISize)q;
        Assert.That(x3, Is.EqualTo(s.Width));
        Assert.That(y3, Is.EqualTo(s.Height));

        Size t = p * (IVector)q;
        Assert.That(x3, Is.EqualTo(t.Width));
        Assert.That(y3, Is.EqualTo(t.Height));

        Assert.That(x3, Is.EqualTo((p * x2).Width));
        Assert.That(y3, Is.EqualTo((p * y2).Height));
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

        Assert.That(x3, Is.EqualTo(r.Width));
        Assert.That(y3, Is.EqualTo(r.Height));

        Size s = p / (ISize)q;
        Assert.That(x3, Is.EqualTo(s.Width));
        Assert.That(y3, Is.EqualTo(s.Height));

        Size t = p / (IVector)q;
        Assert.That(x3, Is.EqualTo(t.Width));
        Assert.That(y3, Is.EqualTo(t.Height));

        Assert.That(x3, Is.EqualTo((p / x2).Width));
        Assert.That(y3, Is.EqualTo((p / y2).Height));
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

        Assert.That(x3, Is.EqualTo(r.Width));
        Assert.That(y3, Is.EqualTo(r.Height));

        Size s = p % (ISize)q;
        Assert.That(x3, Is.EqualTo(s.Width));
        Assert.That(y3, Is.EqualTo(s.Height));

        Size t = p % (IVector)q;
        Assert.That(x3, Is.EqualTo(t.Width));
        Assert.That(y3, Is.EqualTo(t.Height));

        Assert.That(x3, Is.EqualTo((p % x2).Width));
        Assert.That(y3, Is.EqualTo((p % y2).Height));
    }

    [Test]
    public void Add1Operator_Test()
    {
        Size p = new(3, 5);
        p++;
        Assert.That(4, Is.EqualTo(p.Width));
        Assert.That(6, Is.EqualTo(p.Height));
    }

    [Test]
    public void Subtract1Operator_Test()
    {
        Size p = new(3, 5);
        p--;
        Assert.That(2, Is.EqualTo(p.Width));
        Assert.That(4, Is.EqualTo(p.Height));
    }

    [Test]
    public void PlusOperator_Test()
    {
        Size p = +new Size(3, 5);
        Assert.That(3, Is.EqualTo(p.Width));
        Assert.That(5, Is.EqualTo(p.Height));

        p = +new Size(-1, -2);
        Assert.That(-1, Is.EqualTo(p.Width));
        Assert.That(-2, Is.EqualTo(p.Height));
    }

    [Test]
    public void MinusOperator_Test()
    {
        Size p = -new Size(3, 5);
        Assert.That(-3, Is.EqualTo(p.Width));
        Assert.That(-5, Is.EqualTo(p.Height));

        p = -new Size(-1, -2);
        Assert.That(1, Is.EqualTo(p.Width));
        Assert.That(2, Is.EqualTo(p.Height));
    }

    [Theory]
    [TestCase(2, 2, 4)]
    [TestCase(1, 1, 1)]
    [TestCase(3, 5, 15)]
    public void CubeVolume_test(int width, int height, double area)
    {
        Assert.That(area, Is.EqualTo(new Size(width, height).SquareArea));
    }

    [Test]
    public void IsZero_test()
    {
        Assert.That(Size.Zero.IsZero, Is.True);
        Assert.That(new Size(1, 2).IsZero, Is.False);
        Assert.That(new Size(1, -2).IsZero, Is.False);
        Assert.That(new Size(1, double.PositiveInfinity).IsZero, Is.False);
        Assert.That(new Size(1, double.NaN).IsZero, Is.False);
        Assert.That(Size.Nothing.IsZero, Is.False);
        Assert.That(Size.Infinity.IsZero, Is.False);
    }

    [Test]
    public void IsReal_test()
    {
        Assert.That(Size.Zero.IsReal, Is.False);
        Assert.That(new Size(1, 2).IsReal);
        Assert.That(new Size(1, -2).IsReal, Is.False);
        Assert.That(new Size(1, double.PositiveInfinity).IsReal, Is.False);
        Assert.That(new Size(1, double.NaN).IsReal, Is.False);
        Assert.That(Size.Nothing.IsReal, Is.False);
        Assert.That(Size.Infinity.IsReal, Is.False);
    }

    [Test]
    public void IsValid_test()
    {
        Assert.That(Size.Zero.IsValid);
        Assert.That(new Size(1, 2).IsValid);
        Assert.That(new Size(1, -2).IsValid);
        Assert.That(new Size(1, double.PositiveInfinity).IsValid, Is.False);
        Assert.That(new Size(1, double.NaN).IsValid, Is.False);
        Assert.That(Size.Nothing.IsValid, Is.False);
        Assert.That(Size.Infinity.IsValid, Is.False);
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
        Assert.That((int)x, Is.EqualTo(s2.Width));
        Assert.That((int)y, Is.EqualTo(s2.Height));
        Assert.That(s3.Width - x < 0.000001);
        Assert.That(s3.Height - y < 0.000001);
        Assert.That((int)x, Is.EqualTo(s4.Width));
        Assert.That((int)y, Is.EqualTo(s4.Height));
        Assert.That(s5.Width - x < 0.000001);
        Assert.That(s5.Height - y < 0.000001);
    }
}
