/*
Point3DTests.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2021 César Andrés Morgan

Morgan's CLR Advanced Runtime (MCART) is free software: you can redistribute it
and/or modify it under the terms of the GNU General Public License as published
by the Free Software Foundation, either version 3 of the License, or (at your
option) any later version.

Morgan's CLR Advanced Runtime (MCART) is distributed in the hope that it will
be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General
Public License for more details.

You should have received a copy of the GNU General Public License along with
this program. If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using NUnit.Framework;
using TheXDS.MCART.Types;
using TheXDS.MCART.Types.Base;

namespace TheXDS.MCART.Tests.Types
{
    public class Point3DTests
    {
#if CLSCompliance
        [CLSCompliant(false)]
#endif
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
#if CLSCompliance
        [CLSCompliant(false)]
#endif
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
#if CLSCompliance
        [CLSCompliant(false)]
#endif
        public void TryParseOrigin_Test(string data)
        {
            Assert.True(Point3D.TryParse(data, out Point3D p));
            Assert.AreEqual(Point3D.Origin, p);
        }

        [Theory]
        [TestCase("Origin2D")]
        [TestCase("0,0,NaN")]
#if CLSCompliance
        [CLSCompliant(false)]
#endif
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
            Assert.True(p.Equals((I3DVector)q));
            Assert.False(p.Equals((I3DVector)r));
            Assert.True(p.Equals((object?)q));
            Assert.False(p.Equals((object?)r));
            Assert.False(p.Equals(Guid.NewGuid()));
            Assert.False(p.Equals((object?)null));
            Assert.False(p.Equals((I3DVector?)null));
        }

        [Test]
        public void Equals_With_2DVector_Test()
        {
            Assert.True(Point3D.Origin2D.Equals((I2DVector)Point.Origin));
            Assert.True(new Point3D(3, 5).Equals((I2DVector)new Point(3, 5)));
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
#if CLSCompliance
        [CLSCompliant(false)]
#endif
        public void AddOperator_Test(int x1, int y1, int z1, int x2, int y2, int z2, int x3, int y3, int z3)
        {
            Point3D p = new(x1, y1, z1);
            Point3D q = new(x2, y2, z2);
            Point3D r = p + q;

            Assert.AreEqual(x3, r.X);
            Assert.AreEqual(y3, r.Y);
            Assert.AreEqual(z3, r.Z);

            Point3D s = p + (I3DVector)q;
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
#if CLSCompliance
        [CLSCompliant(false)]
#endif
        public void SubstractOperator_Test(int x1, int y1, int z1, int x2, int y2, int z2, int x3, int y3, int z3)
        {
            Point3D p = new(x1, y1, z1);
            Point3D q = new(x2, y2, z2);
            Point3D r = p - q;

            Assert.AreEqual(x3, r.X);
            Assert.AreEqual(y3, r.Y);
            Assert.AreEqual(z3, r.Z);

            Point3D s = p - (I3DVector)q;
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
#if CLSCompliance
        [CLSCompliant(false)]
#endif
        public void MultiplyOperator_Test(int x1, int y1, int z1, int x2, int y2, int z2, int x3, int y3, int z3)
        {
            Point3D p = new(x1, y1, z1);
            Point3D q = new(x2, y2, z2);
            Point3D r = p * q;

            Assert.AreEqual(x3, r.X);
            Assert.AreEqual(y3, r.Y);
            Assert.AreEqual(z3, r.Z);

            Point3D s = p * (I3DVector)q;
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
#if CLSCompliance
        [CLSCompliant(false)]
#endif
        public void DivideOperator_Test(int x1, int y1, int z1, int x2, int y2, int z2, double x3, double y3, double z3)
        {
            Point3D p = new(x1, y1, z1);
            Point3D q = new(x2, y2, z2);
            Point3D r = p / q;

            Assert.AreEqual(x3, r.X);
            Assert.AreEqual(y3, r.Y);
            Assert.AreEqual(z3, r.Z);

            Point3D s = p / (I3DVector)q;
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
#if CLSCompliance
        [CLSCompliant(false)]
#endif
        public void ModulusOperator_Test(int x1, int y1, int z1, int x2, int y2, int z2, double x3, double y3, double z3)
        {
            Point3D p = new(x1, y1, z1);
            Point3D q = new(x2, y2, z2);
            Point3D r = p % q;

            Assert.AreEqual(x3, r.X);
            Assert.AreEqual(y3, r.Y);
            Assert.AreEqual(z3, r.Z);

            Point3D s = p % (I3DVector)q;
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
        public void NotEquals_Test()
        {
            Point3D p = new(3, 5, 7);
            Point3D q = new(3, 5, 7);
            Point3D r = new(5, 3, 1);

            Assert.True(p != r);
            Assert.False(p != q);
            Assert.True(p != (I3DVector)r);
            Assert.False(p != (I3DVector)q);
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
#if CLSCompliance
        [CLSCompliant(false)]
#endif
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
}