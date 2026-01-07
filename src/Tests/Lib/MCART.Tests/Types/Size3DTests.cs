/*
Size3DTests.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2026 César Andrés Morgan

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

using System.Numerics;
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
            Assert.That(value.Width, Is.EqualTo(size));
            Assert.That(value.Height, Is.EqualTo(size));
            Assert.That(value.Depth, Is.EqualTo(size));
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
        Assert.That(Size3D.TryParse(value, out Size3D p));
        Assert.That(x, Is.EqualTo(p.Width));
        Assert.That(y, Is.EqualTo(p.Height));
        Assert.That(z, Is.EqualTo(p.Depth));
    }

    [Theory]
    [TestCase("Nothing")]
    [TestCase("")]
    [TestCase((string?)null)]
    [TestCase("NaN;NaN;NaN")]
    [TestCase(null)]
    public void TryParseNothing_Test(string? data)
    {
        Assert.That(Size3D.TryParse(data, out Size3D p));
        Assert.That(Size3D.Nothing.Equals(p));
    }

    [Theory]
    [TestCase("Zero")]
    [TestCase("0")]
    [TestCase("0;0;0")]
    public void TryParseZero_Test(string data)
    {
        Assert.That(Size3D.TryParse(data, out Size3D p));
        Assert.That(Size3D.Zero, Is.EqualTo(p));
    }

    [Theory]
    [TestCase("Infinity")]
    [TestCase("PositiveInfinity")]
    [TestCase("∞")]
    [TestCase("∞;∞;∞")]
    public void TryParseInfinity_Test(string data)
    {
        Assert.That(Size3D.TryParse(data, out Size3D p));
        Assert.That(Size3D.Infinity, Is.EqualTo(p));
    }

    [Test]
    public void Parse_Test()
    {
        Assert.That(Size3D.Parse("Zero"), Is.AssignableFrom<Size3D>());
        Assert.Throws<FormatException>(() => Size3D.Parse("Test"));
    }

    [Test]
    public void ToString_Test()
    {
        Size3D p = new(3, 5, 7);

        Assert.That("3, 5, 7", Is.EqualTo(p.ToString()));
        Assert.That("3, 5, 7", Is.EqualTo(p.ToString("C")));
        Assert.That("[3, 5, 7]", Is.EqualTo(p.ToString("B")));
        Assert.That("Width: 3, Height: 5, Depth: 7", Is.EqualTo(p.ToString("V")));
        Assert.That("Width: 3\nHeight: 5\nDepth: 7", Is.EqualTo(p.ToString("N").Replace("\r\n", "\n")));
        Assert.Throws<FormatException>(() => p.ToString("???"));
    }

    [Test]
    public void Equals_Test()
    {
        Size3D p = new(3, 5, 7);
        Size3D q = new(3, 5, 7);
        Size3D r = new(5, 3, 1);

        Assert.That(p.Equals(q));
        Assert.That(p.Equals(r), Is.False);
        Assert.That(p.Equals((ISize3D)q));
        Assert.That(p.Equals((ISize3D)r), Is.False);
        Assert.That(p.Equals((ISize3D)q));
        Assert.That(p.Equals((ISize3D)r), Is.False);
        Assert.That(p.Equals((IVector3D)q));
        Assert.That(p.Equals((IVector3D)r), Is.False);
        Assert.That(p.Equals((object?)q));
        Assert.That(p.Equals((object?)r), Is.False);
        Assert.That(p.Equals(Guid.NewGuid()), Is.False);
        Assert.That(p.Equals((object?)null), Is.False);
        Assert.That(p.Equals((ISize3D?)null), Is.False);
        Assert.That(p.Equals((IVector3D?)null), Is.False);
        Assert.That(((IEquatable<IVector>)p).Equals(q));
        Assert.That(((IEquatable<IVector>)p).Equals(r), Is.False);
    }

    [Test]
    public void NotEquals_Test()
    {
        Size3D p = new(3, 5, 7);
        Size3D q = new(3, 5, 7);
        Size3D r = new(5, 3, 1);

        Assert.That(p != r);
        Assert.That(p != q, Is.False);
        Assert.That(p != (ISize3D)r);
        Assert.That(p != (ISize3D)q, Is.False);
        Assert.That(p != (IVector3D)r);
        Assert.That(p != (IVector3D)q, Is.False);
    }

    [Test]
    public void GetHashCode_Test()
    {
        Assert.That(new Size3D(3, 5, 7).GetHashCode(), Is.EqualTo(new Size3D(3, 5, 7).GetHashCode()));
        Assert.That(new Size3D(3, 5, 7).GetHashCode(), Is.Not.EqualTo(new Size3D(1, 1, 1).GetHashCode()));
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

        Assert.That(x3, Is.EqualTo(r.Width));
        Assert.That(y3, Is.EqualTo(r.Height));
        Assert.That(z3, Is.EqualTo(r.Depth));

        Size3D s = p + (ISize3D)q;
        Assert.That(x3, Is.EqualTo(s.Width));
        Assert.That(y3, Is.EqualTo(s.Height));
        Assert.That(z3, Is.EqualTo(s.Depth));

        Size3D t = p + (IVector3D)q;
        Assert.That(x3, Is.EqualTo(t.Width));
        Assert.That(y3, Is.EqualTo(t.Height));
        Assert.That(z3, Is.EqualTo(t.Depth));

        Assert.That(x3, Is.EqualTo((p + x2).Width));
        Assert.That(y3, Is.EqualTo((p + y2).Height));
        Assert.That(z3, Is.EqualTo((p + z2).Depth));
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

        Assert.That(x3, Is.EqualTo(r.Width));
        Assert.That(y3, Is.EqualTo(r.Height));
        Assert.That(z3, Is.EqualTo(r.Depth));

        Size3D s = p - (ISize3D)q;
        Assert.That(x3, Is.EqualTo(s.Width));
        Assert.That(y3, Is.EqualTo(s.Height));
        Assert.That(z3, Is.EqualTo(s.Depth));
        
        Size3D t = p - (IVector3D)q;
        Assert.That(x3, Is.EqualTo(t.Width));
        Assert.That(y3, Is.EqualTo(t.Height));
        Assert.That(z3, Is.EqualTo(t.Depth));

        Assert.That(x3, Is.EqualTo((p - x2).Width));
        Assert.That(y3, Is.EqualTo((p - y2).Height));
        Assert.That(z3, Is.EqualTo((p - z2).Depth));
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

        Assert.That(x3, Is.EqualTo(r.Width));
        Assert.That(y3, Is.EqualTo(r.Height));
        Assert.That(z3, Is.EqualTo(r.Depth));

        Size3D s = p * (ISize3D)q;
        Assert.That(x3, Is.EqualTo(s.Width));
        Assert.That(y3, Is.EqualTo(s.Height));
        Assert.That(z3, Is.EqualTo(s.Depth));

        Size3D t = p * (IVector3D)q;
        Assert.That(x3, Is.EqualTo(t.Width));
        Assert.That(y3, Is.EqualTo(t.Height));
        Assert.That(z3, Is.EqualTo(t.Depth));

        Assert.That(x3, Is.EqualTo((p * x2).Width));
        Assert.That(y3, Is.EqualTo((p * y2).Height));
        Assert.That(z3, Is.EqualTo((p * z2).Depth));
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

        Assert.That(x3, Is.EqualTo(r.Width));
        Assert.That(y3, Is.EqualTo(r.Height));
        Assert.That(z3, Is.EqualTo(r.Depth));

        Size3D s = p / (ISize3D)q;
        Assert.That(x3, Is.EqualTo(s.Width));
        Assert.That(y3, Is.EqualTo(s.Height));
        Assert.That(z3, Is.EqualTo(s.Depth));

        Size3D t = p / (IVector3D)q;
        Assert.That(x3, Is.EqualTo(t.Width));
        Assert.That(y3, Is.EqualTo(t.Height));
        Assert.That(z3, Is.EqualTo(t.Depth));

        Assert.That(x3, Is.EqualTo((p / x2).Width));
        Assert.That(y3, Is.EqualTo((p / y2).Height));
        Assert.That(z3, Is.EqualTo((p / z2).Depth));
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

        Assert.That(x3, Is.EqualTo(r.Width));
        Assert.That(y3, Is.EqualTo(r.Height));
        Assert.That(z3, Is.EqualTo(r.Depth));

        Size3D s = p % (ISize3D)q;
        Assert.That(x3, Is.EqualTo(s.Width));
        Assert.That(y3, Is.EqualTo(s.Height));
        Assert.That(z3, Is.EqualTo(s.Depth)); 
        
        Size3D t = p % (IVector3D)q;
        Assert.That(x3, Is.EqualTo(t.Width));
        Assert.That(y3, Is.EqualTo(t.Height));
        Assert.That(z3, Is.EqualTo(t.Depth));

        Assert.That(x3, Is.EqualTo((p % x2).Width));
        Assert.That(y3, Is.EqualTo((p % y2).Height));
        Assert.That(z3, Is.EqualTo((p % z2).Depth));
    }

    [Test]
    public void Add1Operator_Test()
    {
        Size3D p = new(3, 5, 7);
        p++;
        Assert.That(4, Is.EqualTo(p.Width));
        Assert.That(6, Is.EqualTo(p.Height));
        Assert.That(8, Is.EqualTo(p.Depth));
    }

    [Test]
    public void Subtract1Operator_Test()
    {
        Size3D p = new(3, 5, 7);
        p--;
        Assert.That(2, Is.EqualTo(p.Width));
        Assert.That(4, Is.EqualTo(p.Height));
        Assert.That(6, Is.EqualTo(p.Depth));
    }

    [Test]
    public void PlusOperator_Test()
    {
        Size3D p = +new Size3D(3, 5, 7);
        Assert.That(3, Is.EqualTo(p.Width));
        Assert.That(5, Is.EqualTo(p.Height));
        Assert.That(7, Is.EqualTo(p.Depth));

        p = +new Size3D(-1, -2, -3);
        Assert.That(-1, Is.EqualTo(p.Width));
        Assert.That(-2, Is.EqualTo(p.Height));
        Assert.That(-3, Is.EqualTo(p.Depth));
    }

    [Test]
    public void MinusOperator_Test()
    {
        Size3D p = -new Size3D(3, 5, 7);
        Assert.That(-3, Is.EqualTo(p.Width));
        Assert.That(-5, Is.EqualTo(p.Height));
        Assert.That(-7, Is.EqualTo(p.Depth));

        p = -new Size3D(-1, -2, -3);
        Assert.That(1, Is.EqualTo(p.Width));
        Assert.That(2, Is.EqualTo(p.Height));
        Assert.That(3, Is.EqualTo(p.Depth));
    }

    [Theory]
    [TestCase(2, 2, 2, 8)]
    [TestCase(1, 1, 1, 1)]
    [TestCase(3, 5, 7, 105)]
    public void CubeVolume_test(int width, int height, int depth, double vol)
    {
        Assert.That(vol, Is.EqualTo(new Size3D(width, height, depth).CubeVolume));
    }

    [Test]
    public void IsZero_test()
    {
        Assert.That(Size3D.Zero.IsZero);
        Assert.That(new Size3D(1, 2, 3).IsZero, Is.False);
        Assert.That(new Size3D(1, 2, -3).IsZero, Is.False);
        Assert.That(new Size3D(1, 2, double.PositiveInfinity).IsZero, Is.False);
        Assert.That(new Size3D(1, 2, double.NaN).IsZero, Is.False);
        Assert.That(Size3D.Nothing.IsZero, Is.False);
        Assert.That(Size3D.Infinity.IsZero, Is.False);
    }

    [Test]
    public void IsReal_test()
    {
        Assert.That(Size3D.Zero.IsReal, Is.False);
        Assert.That(new Size3D(1, 2, 3).IsReal);
        Assert.That(new Size3D(1, 2, -3).IsReal, Is.False);
        Assert.That(new Size3D(1, 2, double.PositiveInfinity).IsReal, Is.False);
        Assert.That(new Size3D(1, 2, double.NaN).IsReal, Is.False);
        Assert.That(Size3D.Nothing.IsReal, Is.False);
        Assert.That(Size3D.Infinity.IsReal, Is.False);
    }

    [Test]
    public void IsValid_test()
    {
        Assert.That(Size3D.Zero.IsValid);
        Assert.That(new Size3D(1, 2, 3).IsValid);
        Assert.That(new Size3D(1, 2, -3).IsValid);
        Assert.That(new Size3D(1, 2, double.PositiveInfinity).IsValid, Is.False);
        Assert.That(new Size3D(1, 2, double.NaN).IsValid, Is.False);
        Assert.That(Size3D.Nothing.IsValid, Is.False);
        Assert.That(Size3D.Infinity.IsValid, Is.False);
    }

    [TestCase(1, 1, 1, 6)]
    [TestCase(2, 2, 2, 12)]
    [TestCase(5, 3, 2, 20)]
    public void CubePerimeter_test(int width, int height, int depth, double expected)
    {
        Assert.That(new Size3D(width, height, depth).CubePerimeter, Is.EqualTo(expected));
    }

    [Test]
    public void Point3D_can_be_implicitly_converted_to_Vector3()
    {
        Vector3 p = new Size3D(3, 5, 8);
        Assert.That(p.X, Is.EqualTo(3));
        Assert.That(p.Y, Is.EqualTo(5));
        Assert.That(p.Z, Is.EqualTo(8));
    }

    [Test]
    public void Point3D_can_be_implicitly_converted_from_Vector3()
    {
        Size3D p = new Vector3(3, 5, 8);
        Assert.That(p.Width, Is.EqualTo(3));
        Assert.That(p.Height, Is.EqualTo(5));
        Assert.That(p.Depth, Is.EqualTo(8));
    }
}
