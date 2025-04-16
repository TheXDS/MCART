/*
PointTests.cs

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

using System.Numerics;
using TheXDS.MCART.Helpers;
using TheXDS.MCART.Types;
using TheXDS.MCART.Types.Base;

namespace TheXDS.MCART.Tests.Types;

public class PointTests
{
    [TestCase("15,12", 15, 12)]
    [TestCase("15;12", 15, 12)]
    [TestCase("15-12", 15, 12)]
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
        Assert.That(Point.TryParse(value, out Point p));
        Assert.That(x, Is.EqualTo(p.X));
        Assert.That(y, Is.EqualTo(p.Y));
    }

    [Theory]
    [TestCase("Nowhere")]
    [TestCase("")]
    [TestCase("NaN,NaN")]
    [TestCase(null)]
    public void TryParseNowhere_Test(string data)
    {
        Assert.That(Point.TryParse(data, out Point p));
        Assert.That(Point.Nowhere, Is.EqualTo(p));
    }

    [Theory]
    [TestCase("Origin")]
    [TestCase("0")]
    [TestCase("+")]
    [TestCase("0,0")]
    public void TryParseOrigin_Test(string data)
    {
        Assert.That(Point.TryParse(data, out Point p));
        Assert.That(Point.Origin, Is.EqualTo(p));
    }

    [Test]
    public void Parse_Test()
    {
        Assert.That(Point.Parse("Origin"), Is.AssignableFrom<Point>());
        Assert.Throws<FormatException>(() => Point.Parse("Test"));
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

    [Test]
    public void Equals_Test()
    {
        Point p = new(3, 5);
        Point q = new(3, 5);
        Point r = new(5, 3);

        Assert.That(p.Equals(q));
        Assert.That(p.Equals(r), Is.False);
        Assert.That(p.Equals((IVector)q));
        Assert.That(p.Equals((IVector)r), Is.False);
        Assert.That(p.Equals((object?)q));
        Assert.That(p.Equals((object?)r), Is.False);
        Assert.That(p.Equals(Guid.NewGuid()), Is.False);
        Assert.That(p.Equals((object?)null), Is.False);
        Assert.That(p.Equals(null), Is.False);
    }

    [Test]
    public void GetHashCode_Test()
    {
        Assert.That(new Point(3, 5).GetHashCode(), Is.EqualTo(new Point(3, 5).GetHashCode()));
        Assert.That(new Point(3, 5).GetHashCode(), Is.Not.EqualTo(new Point(1, 1).GetHashCode()));
    }

    [Test]
    public void CastingToDrawingPoint_Test()
    {
        Point p = new(3, 5);
        System.Drawing.Point q = (System.Drawing.Point)p;
        Assert.That(q, Is.AssignableFrom<System.Drawing.Point>());
        Assert.That(3, Is.EqualTo(q.X));
        Assert.That(5, Is.EqualTo(q.Y));
        Point r = (Point)q;
        Assert.That(r, Is.AssignableFrom<Point>());
        Assert.That(3.0, Is.EqualTo(r.X));
        Assert.That(5.0, Is.EqualTo(r.Y));
    }

    [TestCase(3, 5, 2, 4, 5, 9)]
    [TestCase(1, 1, 2, 2, 3, 3)]
    [TestCase(-1, -1, 1, 1, 0, 0)]
    [TestCase(-3, 5, 2, -4, -1, 1)]
    public void AddOperator_Test(int x1, int y1, int x2, int y2, int x3, int y3)
    {
        Point p = new(x1, y1);
        Point q = new(x2, y2);
        Point r = p + q;

        Assert.That(x3, Is.EqualTo(r.X));
        Assert.That(y3, Is.EqualTo(r.Y));

        Point s = p + (IVector)q;
        Assert.That(x3, Is.EqualTo(s.X));
        Assert.That(y3, Is.EqualTo(s.Y));

        Assert.That(x3, Is.EqualTo((p + x2).X));
        Assert.That(y3, Is.EqualTo((p + y2).Y));
    }

    [TestCase(3, 5, 2, 4, 1, 1)]
    [TestCase(1, 1, 2, 2, -1, -1)]
    [TestCase(-1, -1, 1, 1, -2, -2)]
    [TestCase(-3, 5, 2, -4, -5, 9)]
    public void SubtractOperator_Test(int x1, int y1, int x2, int y2, int x3, int y3)
    {
        Point p = new(x1, y1);
        Point q = new(x2, y2);
        Point r = p - q;

        Assert.That(x3, Is.EqualTo(r.X));
        Assert.That(y3, Is.EqualTo(r.Y));

        Point s = p - (IVector)q;
        Assert.That(x3, Is.EqualTo(s.X));
        Assert.That(y3, Is.EqualTo(s.Y));

        Assert.That(x3, Is.EqualTo((p - x2).X));
        Assert.That(y3, Is.EqualTo((p - y2).Y));
    }

    [TestCase(3, 5, 2, 4, 6, 20)]
    [TestCase(1, 1, 2, 2, 2, 2)]
    [TestCase(-1, -1, 1, 1, -1, -1)]
    [TestCase(-3, 5, 2, -4, -6, -20)]
    public void MultiplyOperator_Test(int x1, int y1, int x2, int y2, int x3, int y3)
    {
        Point p = new(x1, y1);
        Point q = new(x2, y2);
        Point r = p * q;

        Assert.That(x3, Is.EqualTo(r.X));
        Assert.That(y3, Is.EqualTo(r.Y));

        Point s = p * (IVector)q;
        Assert.That(x3, Is.EqualTo(s.X));
        Assert.That(y3, Is.EqualTo(s.Y));

        Assert.That(x3, Is.EqualTo((p * x2).X));
        Assert.That(y3, Is.EqualTo((p * y2).Y));
    }

    [TestCase(3, 5, 2, 4, 1.5, 1.25)]
    [TestCase(1, 1, 2, 2, 0.5, 0.5)]
    [TestCase(-1, -1, 1, 1, -1, -1)]
    [TestCase(-3, 5, 2, -4, -1.5, -1.25)]
    public void DivideOperator_Test(int x1, int y1, int x2, int y2, double x3, double y3)
    {
        Point p = new(x1, y1);
        Point q = new(x2, y2);
        Point r = p / q;

        Assert.That(x3, Is.EqualTo(r.X));
        Assert.That(y3, Is.EqualTo(r.Y));

        Point s = p / (IVector)q;
        Assert.That(x3, Is.EqualTo(s.X));
        Assert.That(y3, Is.EqualTo(s.Y));

        Assert.That(x3, Is.EqualTo((p / x2).X));
        Assert.That(y3, Is.EqualTo((p / y2).Y));
    }

    [TestCase(3, 5, 2, 4, 1, 1)]
    [TestCase(1, 1, 2, 2, 1, 1)]
    [TestCase(-1, -1, 1, 1, 0, 0)]
    [TestCase(-3, 5, 2, -4, -1, 1)]
    [TestCase(13, 14, 5, 3, 3, 2)]
    public void ModulusOperator_Test(int x1, int y1, int x2, int y2, double x3, double y3)
    {
        Point p = new(x1, y1);
        Point q = new(x2, y2);
        Point r = p % q;

        Assert.That(x3, Is.EqualTo(r.X));
        Assert.That(y3, Is.EqualTo(r.Y));

        Point s = p % (IVector)q;
        Assert.That(x3, Is.EqualTo(s.X));
        Assert.That(y3, Is.EqualTo(s.Y));

        Assert.That(x3, Is.EqualTo((p % x2).X));
        Assert.That(y3, Is.EqualTo((p % y2).Y));
    }

    [Test]
    public void Add1Operator_Test()
    {
        Point p = new(3, 5);
        p++;
        Assert.That(4, Is.EqualTo(p.X));
        Assert.That(6, Is.EqualTo(p.Y));
    }

    [Test]
    public void Subtract1Operator_Test()
    {
        Point p = new(3, 5);
        p--;
        Assert.That(2, Is.EqualTo(p.X));
        Assert.That(4, Is.EqualTo(p.Y));
    }

    [Test]
    public void PlusOperator_Test()
    {
        Point p = +new Point(3, 5);
        Assert.That(3, Is.EqualTo(p.X));
        Assert.That(5, Is.EqualTo(p.Y));

        p = +new Point(-1, -2);
        Assert.That(-1, Is.EqualTo(p.X));
        Assert.That(-2, Is.EqualTo(p.Y));
    }

    [Test]
    public void MinusOperator_Test()
    {
        Point p = -new Point(3, 5);
        Assert.That(-3, Is.EqualTo(p.X));
        Assert.That(-5, Is.EqualTo(p.Y));

        p = -new Point(-1, -2);
        Assert.That(1, Is.EqualTo(p.X));
        Assert.That(2, Is.EqualTo(p.Y));
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

    [TestCase(1, 0, 0)]
    [TestCase(1, 1, System.Math.PI / 4)]
    [TestCase(0, 1, System.Math.PI / 2)]
    [TestCase(0, -1, System.Math.Tau - (System.Math.PI / 2))]
    [TestCase(1, -1, System.Math.Tau - (System.Math.PI / 4))]
    public void Angle_Test(int x, int y, double angle)
    {
        Assert.That((new Point(x, y).Angle() - angle).IsBetween(-0.00000001, 0.00000001));
    }

    [Test]
    public void WithinBox_Test()
    {
        Point p = new(-5, -5);
        Point q = new(5, 5);
        Assert.That(Point.Origin.WithinBox(p, q));
        Assert.That(new Point(10, 10).WithinBox(p, q), Is.False);
        Assert.That(new Point(5, -5).WithinBox(p, q));
        Assert.That(Point.Origin.WithinBox(new(-1, 1), new Range<double>(-1, 1)));
        Assert.That(Point.Origin.WithinBox(new(2, 3), new Range<double>(1, 1)), Is.False);
        Assert.That(Point.Origin.WithinBox(new Size(10, 10), new(-5, 5)));
        Assert.That(Point.Origin.WithinBox(new Size(2, 2), new(-5, 5)), Is.False);
        Assert.That(Point.Origin.WithinBox(new(10, 10)));
        Assert.That(Point.Origin.WithinBox(new(2, 2)));
        Assert.That(new Point(3, 4).WithinBox(new(2, 2)), Is.False);
        Assert.That(Point.Origin.WithinBox(-5, 5, 5, -5));
        Assert.That(Point.Origin.WithinBox(5, -5, -5, 5));
        Assert.That(Point.Origin.WithinBox(-15, -5, -10, 5), Is.False);
        Assert.That(Point.Origin.WithinBox(-15, -5, -10, 5), Is.False);
    }

    [Test]
    public void Magnitude_Test()
    {
        Point p = new(3, 5);
        Assert.That(p.Magnitude(), Is.EqualTo(p.Magnitude(Point.Origin)));
        Assert.That(p.Magnitude(), Is.EqualTo(p.Magnitude(0, 0)));
    }

    [TestCase(0, 0, true)]
    [TestCase(10, 10, false)]
    [TestCase(10, 0, true)]
    [TestCase(0, 10, true)]
    [TestCase(8, 8, false)]
    [TestCase(7, 7, true)]
    [TestCase(-8, -8, false)]
    [TestCase(-7, -7, true)]
    [TestCase(-8, 8, false)]
    [TestCase(-7, 7, true)]
    [TestCase(8, -8, false)]
    [TestCase(7, -7, true)]
    public void WithinCircle_Test(int x, int y, bool result)
    {
        Assert.That(result, Is.EqualTo(new Point(x, y).WithinCircle(Point.Origin, 10)));
    }

    [Test]
    public void Point_can_be_implicitly_converted_to_Vector2()
    {
        Vector2 p = new Point(3, 5);
        Assert.That(p.X, Is.EqualTo(3));
        Assert.That(p.Y, Is.EqualTo(5));
    }

    [Test]
    public void Point_can_be_implicitly_converted_from_Vector2()
    {
        Point p = new Vector2(3, 5);
        Assert.That(p.X, Is.EqualTo(3));
        Assert.That(p.Y, Is.EqualTo(5));
    }
}
