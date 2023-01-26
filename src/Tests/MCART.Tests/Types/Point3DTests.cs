/*
Point3DTests.cs

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

namespace TheXDS.MCART.Tests.Types;
using NUnit.Framework;
using System;
using TheXDS.MCART.Types;
using TheXDS.MCART.Types.Base;

public class Point3DTests
{
    [CLSCompliant(false)]
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
        Assert.True(Point3D.TryParse(value, out Point3D p));
        Assert.AreEqual(x, p.X);
        Assert.AreEqual(y, p.Y);
        Assert.AreEqual(z, p.Z);
    }

    [Theory]
    [TestCase("Nowhere")]
    [TestCase("")]
    [TestCase("NaN,NaN,NaN")]
    [TestCase(null)]
    [CLSCompliant(false)]
    public void TryParseNowhere_Test(string data)
    {
        Assert.True(Point3D.TryParse(data, out Point3D p));
        Assert.True(Point3D.Nowhere.Equals(p));
    }

    [Theory]
    [TestCase("Origin")]
    [TestCase("0")]
    [TestCase("+")]
    [TestCase("0,0,0")]
    [CLSCompliant(false)]
    public void TryParseOrigin_Test(string data)
    {
        Assert.True(Point3D.TryParse(data, out Point3D p));
        Assert.AreEqual(Point3D.Origin, p);
    }

    [Theory]
    [TestCase("Origin2D")]
    [TestCase("0,0,NaN")]
    [CLSCompliant(false)]
    public void TryParseOrigin2D_Test(string data)
    {
        Assert.True(Point3D.TryParse(data, out Point3D p));
        Assert.AreEqual(Point3D.Origin2D, p);
    }

    [Test]
    public void Parse_Test()
    {
        Assert.IsAssignableFrom<Point3D>(Point3D.Parse("Origin"));
        Assert.Throws<FormatException>(() => Point3D.Parse("Test"));
    }

    [Test]
    public void ToString_Test()
    {
        Point3D p = new(3, 5, 7);

        Assert.AreEqual("3, 5, 7", p.ToString());
        Assert.AreEqual("3, 5, 7", p.ToString("C"));
        Assert.AreEqual("[3, 5, 7]", p.ToString("B"));
        Assert.AreEqual("X: 3, Y: 5, Z: 7", p.ToString("V"));
        Assert.AreEqual("X: 3\nY: 5\nZ: 7", p.ToString("N").Replace("\r\n", "\n"));
        Assert.Throws<FormatException>(() => p.ToString("???"));
    }

    [Test]
    public void Equals_Test()
    {
        Point3D p = new(3, 5, 7);
        Point3D q = new(3, 5, 7);
        Point3D r = new(5, 3, 1);

        Assert.True(p.Equals(q));
        Assert.False(p.Equals(r));
        Assert.True(p.Equals((IVector3D)q));
        Assert.False(p.Equals((IVector3D)r));
        Assert.True(p.Equals((object?)q));
        Assert.False(p.Equals((object?)r));
        Assert.False(p.Equals(Guid.NewGuid()));
        Assert.False(p.Equals((object?)null));
        Assert.False(p.Equals((IVector3D?)null));
    }

    [Test]
    public void Equals_With_2DVector_Test()
    {
        Assert.True(Point3D.Origin2D.Equals((IVector)Point.Origin));
        Assert.True(new Point3D(3, 5).Equals((IVector)new Point(3, 5)));
        Assert.True(new Point(3, 5).Equals(new Point3D(3, 5)));
    }

    [Test]
    public void GetHashCode_Test()
    {
        Assert.AreEqual(new Point3D(3, 5, 7).GetHashCode(), new Point3D(3, 5, 7).GetHashCode());
        Assert.AreNotEqual(new Point3D(3, 5, 7).GetHashCode(), new Point3D(1, 1, 1).GetHashCode());
    }

    [TestCase(3, 5, 7, 2, 4, 6, 5, 9, 13)]
    [TestCase(1, 1, 1, 2, 2, 2, 3, 3, 3)]
    [TestCase(-1, -1, -1, 1, 1, 1, 0, 0, 0)]
    [TestCase(-3, 5, -7, 2, -4, 6, -1, 1, -1)]
    [CLSCompliant(false)]
    public void AddOperator_Test(int x1, int y1, int z1, int x2, int y2, int z2, int x3, int y3, int z3)
    {
        Point3D p = new(x1, y1, z1);
        Point3D q = new(x2, y2, z2);
        Point3D r = p + q;

        Assert.AreEqual(x3, r.X);
        Assert.AreEqual(y3, r.Y);
        Assert.AreEqual(z3, r.Z);

        Point3D s = p + (IVector3D)q;
        Assert.AreEqual(x3, s.X);
        Assert.AreEqual(y3, s.Y);
        Assert.AreEqual(z3, s.Z);
        
        Assert.AreEqual(x3, (p + x2).X);
        Assert.AreEqual(y3, (p + y2).Y);
        Assert.AreEqual(z3, (p + z2).Z);
    }

    [TestCase(3, 5, 7, 2, 4, 6, 1, 1, 1)]
    [TestCase(1, 1, 1, 2, 2, 2, -1, -1, -1)]
    [TestCase(-1, -1, -1, 1, 1, 1, -2, -2, -2)]
    [TestCase(-3, 5, -7, 2, -4, 6, -5, 9, -13)]
    [CLSCompliant(false)]
    public void SubstractOperator_Test(int x1, int y1, int z1, int x2, int y2, int z2, int x3, int y3, int z3)
    {
        Point3D p = new(x1, y1, z1);
        Point3D q = new(x2, y2, z2);
        Point3D r = p - q;

        Assert.AreEqual(x3, r.X);
        Assert.AreEqual(y3, r.Y);
        Assert.AreEqual(z3, r.Z);

        Point3D s = p - (IVector3D)q;
        Assert.AreEqual(x3, s.X);
        Assert.AreEqual(y3, s.Y);
        Assert.AreEqual(z3, s.Z);
        
        Assert.AreEqual(x3, (p - x2).X);
        Assert.AreEqual(y3, (p - y2).Y);
        Assert.AreEqual(z3, (p - z2).Z);
    }

    [TestCase(3, 5, 7, 2, 4, 6, 6, 20, 42)]
    [TestCase(1, 1, 1, 2, 2, 2, 2, 2, 2)]
    [TestCase(-1, -1, -1, 1, 1, 1, -1, -1, -1)]
    [TestCase(-3, 5, -7, 2, -4, 6, -6, -20, -42)]
    [CLSCompliant(false)]
    public void MultiplyOperator_Test(int x1, int y1, int z1, int x2, int y2, int z2, int x3, int y3, int z3)
    {
        Point3D p = new(x1, y1, z1);
        Point3D q = new(x2, y2, z2);
        Point3D r = p * q;

        Assert.AreEqual(x3, r.X);
        Assert.AreEqual(y3, r.Y);
        Assert.AreEqual(z3, r.Z);

        Point3D s = p * (IVector3D)q;
        Assert.AreEqual(x3, s.X);
        Assert.AreEqual(y3, s.Y);
        Assert.AreEqual(z3, s.Z);

        Assert.AreEqual(x3, (p * x2).X);
        Assert.AreEqual(y3, (p * y2).Y);
        Assert.AreEqual(z3, (p * z2).Z);
    }

    [TestCase(3, 5, 7, 2, 4, 6, 1.5, 1.25, 7.0 / 6.0)]
    [TestCase(1, 1, 1, 2, 2, 2, 0.5, 0.5, 0.5)]
    [TestCase(-1, -1, -1, 1, 1, 1, -1, -1, -1)]
    [TestCase(-3, 5, -7, 2, -4, 6, -1.5, -1.25, -7.0 / 6.0)]
    [CLSCompliant(false)]
    public void DivideOperator_Test(int x1, int y1, int z1, int x2, int y2, int z2, double x3, double y3, double z3)
    {
        Point3D p = new(x1, y1, z1);
        Point3D q = new(x2, y2, z2);
        Point3D r = p / q;

        Assert.AreEqual(x3, r.X);
        Assert.AreEqual(y3, r.Y);
        Assert.AreEqual(z3, r.Z);

        Point3D s = p / (IVector3D)q;
        Assert.AreEqual(x3, s.X);
        Assert.AreEqual(y3, s.Y);
        Assert.AreEqual(z3, s.Z);

        Assert.AreEqual(x3, (p / x2).X);
        Assert.AreEqual(y3, (p / y2).Y);
        Assert.AreEqual(z3, (p / z2).Z);
    }

    [TestCase(3, 5, 7, 2, 4, 6, 1, 1, 1)]
    [TestCase(1, 1, 1, 2, 2, 2, 1, 1, 1)]
    [TestCase(-1, -1, -1, 1, 1, 1, 0, 0, 0)]
    [TestCase(-3, 5, -7, 2, -4, 6, -1, 1, -1)]
    [TestCase(13, 14, 15, 5, 3, 7,  3, 2, 1)]
    [CLSCompliant(false)]
    public void ModulusOperator_Test(int x1, int y1, int z1, int x2, int y2, int z2, double x3, double y3, double z3)
    {
        Point3D p = new(x1, y1, z1);
        Point3D q = new(x2, y2, z2);
        Point3D r = p % q;

        Assert.AreEqual(x3, r.X);
        Assert.AreEqual(y3, r.Y);
        Assert.AreEqual(z3, r.Z);

        Point3D s = p % (IVector3D)q;
        Assert.AreEqual(x3, s.X);
        Assert.AreEqual(y3, s.Y);
        Assert.AreEqual(z3, s.Z);

        Assert.AreEqual(x3, (p % x2).X);
        Assert.AreEqual(y3, (p % y2).Y);
        Assert.AreEqual(z3, (p % z2).Z);
    }

    [Test]
    public void Add1Operator_Test()
    {
        Point3D p = new(3, 5, 7);
        p++;
        Assert.AreEqual(4, p.X);
        Assert.AreEqual(6, p.Y);
        Assert.AreEqual(8, p.Z);
    }

    [Test]
    public void Substract1Operator_Test()
    {
        Point3D p = new(3, 5, 7);
        p--;
        Assert.AreEqual(2, p.X);
        Assert.AreEqual(4, p.Y);
        Assert.AreEqual(6, p.Z);
    }

    [Test]
    public void PlusOperator_Test()
    {
        Point3D p = +new Point3D(3, 5, 7);
        Assert.AreEqual(3, p.X);
        Assert.AreEqual(5, p.Y);
        Assert.AreEqual(7, p.Z);

        p = +new Point3D(-1, -2, -3);
        Assert.AreEqual(-1, p.X);
        Assert.AreEqual(-2, p.Y);
        Assert.AreEqual(-3, p.Z);
    }

    [Test]
    public void MinusOperator_Test()
    {
        Point3D p = -new Point3D(3, 5, 7);
        Assert.AreEqual(-3, p.X);
        Assert.AreEqual(-5, p.Y);
        Assert.AreEqual(-7, p.Z);

        p = -new Point3D(-1, -2, -3);
        Assert.AreEqual(1, p.X);
        Assert.AreEqual(2, p.Y);
        Assert.AreEqual(3, p.Z);
    }
    
    [Test]
    public void Implicit_Point_to_Point3D_Operator_Test()
    {
        Point3D p = new Point(3, 5);
        Assert.AreEqual(3, p.X);
        Assert.AreEqual(5, p.Y);
        Assert.AreEqual(double.NaN, p.Z);
    }

    [Test]
    public void NotEquals_Test()
    {
        Point3D p = new(3, 5, 7);
        Point3D q = new(3, 5, 7);
        Point3D r = new(5, 3, 1);

        Assert.True(p != r);
        Assert.False(p != q);
        Assert.True(p != (IVector3D)r);
        Assert.False(p != (IVector3D)q);
    }

    [Test]
    public void WithinCube_Test()
    {
        Point3D p = new(-5, -5, -5);
        Point3D q = new(5, 5, 5);
        Assert.True(Point3D.Origin.WithinCube(p, q));
        Assert.False(new Point3D(10, 10, 10).WithinCube(p, q));
        Assert.True(new Point3D(5, -5, 5).WithinCube(p, q));
        Assert.True(Point3D.Origin.WithinCube(new(-1, 1), new(-1, 1), new(-1, 1)));
        Assert.False(Point3D.Origin.WithinCube(new(2, 3), new(1, 1), new(4, 5)));
        Assert.True(Point3D.Origin.WithinCube(new Size3D(10, 10, 10), new Point3D(-5, 5, 5)));
        Assert.False(Point3D.Origin.WithinCube(new Size3D(2, 2, 2), new Point3D(-5, 5, 5)));
        Assert.True(Point3D.Origin.WithinCube(new Size3D(10, 10, 10)));
        Assert.True(Point3D.Origin.WithinCube(new Size3D(2, 2, 2)));
        Assert.False(new Point3D(3, 4, 6).WithinCube(new Size3D(2, 2, 2)));
        Assert.True(Point3D.Origin.WithinCube(-5, 5, -5, 5, -5, 5));
        Assert.True(Point3D.Origin.WithinCube(5, -5, 5, -5, 5, -5));
        Assert.False(Point3D.Origin.WithinCube(-15, -5, -15, -10, 5, -10));
        Assert.False(Point3D.Origin.WithinCube(-15, -5, -15, -10, 5, -10));
    }

    [TestCase(0, 0, 0, true)]
    [TestCase(10, 10, 10, false)]
    [TestCase(10, 0, 0, true)]
    [TestCase(0, 10, 0 ,true)]
    [TestCase(8, 8, 8, false)]
    [TestCase(6, 6, 6, false)]
    [TestCase(5, 5, 5, true)]
    [TestCase(-8, -8, -8, false)]
    [TestCase(-6, -6, -6, false)]
    [TestCase(-5, -5, -5, true)]
    [TestCase(-8, 8, -8, false)]
    [TestCase(-6, 6, -6, false)]
    [TestCase(-5, 5, -5, true)]
    [TestCase(8, -8, 8, false)]
    [TestCase(6, -6, 6, false)]
    [TestCase(5, -5, 5, true)]
    [CLSCompliant(false)]
    public void WithinSphere_Test(int x, int y, int z, bool result)
    {
        Assert.AreEqual(result, new Point3D(x, y, z).WithinSphere(Point3D.Origin, 10));
    }
    
    [Test]
    public void Magnitude_Test()
    {
        Point3D p = new(3, 5, 8);
        Assert.AreEqual(p.Magnitude(), p.Magnitude(Point3D.Origin));
        Assert.AreEqual(p.Magnitude(), p.Magnitude(0, 0, 0));
    }
}
