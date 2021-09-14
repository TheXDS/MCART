/*
PointTests.cs

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
using TheXDS.MCART.Helpers;
using TheXDS.MCART.Types;
using TheXDS.MCART.Types.Base;
using Xunit;

namespace TheXDS.MCART.Tests.Types
{
    public class PointTests
    {
#if CLSCompliance
        [CLSCompliant(false)]
#endif
        [Theory]
        [InlineData("15,12", 15, 12)]
        [InlineData("15;12", 15, 12)]
        [InlineData("15-12", 15, 12)]
        [InlineData("15:12", 15, 12)]
        [InlineData("15|12", 15, 12)]
        [InlineData("15 12", 15, 12)]
        [InlineData("15, 12", 15, 12)]
        [InlineData("15; 12", 15, 12)]
        [InlineData("15 - 12", 15, 12)]
        [InlineData("15 : 12", 15, 12)]
        [InlineData("15 | 12", 15, 12)]
        public void TryParseNumbers_Test(string value, double x, double y)
        {
            Assert.True(Point.TryParse(value, out var p));
            Assert.Equal(x, p.X);
            Assert.Equal(y, p.Y);
        }

        [Theory]
        [InlineData("Nowhere")]
        [InlineData("")]
        [InlineData("NaN,NaN")]
        [InlineData(null)]
        public void TryParseNowhere_Test(string data)
        {
            Assert.True(Point.TryParse(data, out var p));
            Assert.Equal(Point.Nowhere, p);
        }
        
        [Theory]
        [InlineData("Origin")]
        [InlineData("0")]
        [InlineData("+")]
        [InlineData("0,0")]
        public void TryParseOrigin_Test(string data)
        {
            Assert.True(Point.TryParse(data, out var p));
            Assert.Equal(Point.Origin, p);
        }

        [Fact]
        public void Parse_Test()
        {
            Assert.IsType<Point>(Point.Parse("Origin"));
            Assert.Throws<FormatException>(() => Point.Parse("Test"));
        }

        [Fact]
        public void ToString_Test()
        {
            var p = new Point(3, 5);
            
            Assert.Equal("3, 5", p.ToString());
            Assert.Equal("3, 5", p.ToString("C"));
            Assert.Equal("[3, 5]", p.ToString("B"));
            Assert.Equal("X: 3, Y: 5", p.ToString("V"));
            Assert.Equal("X: 3\nY: 5", p.ToString("N"));

            Assert.Throws<FormatException>(() => p.ToString("???"));
        }

        [Fact]
        public void Equals_Test()
        {
            var p = new Point(3, 5);
            var q = new Point(3, 5);
            var r = new Point(5, 3);

            Assert.True(p.Equals(q));
            Assert.False(p.Equals(r));
            Assert.True(p.Equals((I2DVector)q));
            Assert.False(p.Equals((I2DVector)r));
            Assert.True(p.Equals((object?)q));
            Assert.False(p.Equals((object?)r));
            Assert.False(p.Equals(Guid.NewGuid()));
            Assert.False(p.Equals((object?)null));
            Assert.False(p.Equals((I2DVector?)null));
        }

        [Fact]
        public void GetHashCode_Test()
        {
            Assert.Equal(new Point(3, 5).GetHashCode(), new Point(3, 5).GetHashCode());
            Assert.NotEqual(new Point(3, 5).GetHashCode(), new Point(1, 1).GetHashCode());
        }

        [Fact]
        public void CastingToDrawingPoint_Test()
        {
            var p = new Point(3, 5);
            var q = (System.Drawing.Point)p;
            Assert.IsType<System.Drawing.Point>(q);
            Assert.Equal(3, q.X);
            Assert.Equal(5, q.Y);
            var r = (Point)q;
            Assert.IsType<Point>(r);
            Assert.Equal(3.0, r.X);
            Assert.Equal(5.0, r.Y);
        }

        [Theory]
        [InlineData(3, 5, 2, 4, 5, 9)]
        [InlineData(1, 1, 2, 2, 3, 3)]
        [InlineData(-1, -1, 1, 1, 0, 0)]
        [InlineData(-3, 5, 2, -4, -1, 1)]
        public void AddOperator_Test(int x1, int y1, int x2, int y2, int x3, int y3)
        {
            var p = new Point(x1, y1);
            var q = new Point(x2, y2);
            var r = p + q;
            
            Assert.Equal(x3, r.X);
            Assert.Equal(y3, r.Y);

            var s = p + (I2DVector) q;
            Assert.Equal(x3, s.X);
            Assert.Equal(y3, s.Y);

            Assert.Equal(x3, (p + x2).X);
            Assert.Equal(y3, (p + y2).Y);
        }
        
        [Theory]
        [InlineData(3, 5, 2, 4, 1, 1)]
        [InlineData(1, 1, 2, 2, -1, -1)]
        [InlineData(-1, -1, 1, 1, -2, -2)]
        [InlineData(-3, 5, 2, -4, -5, 9)]
        public void SubstractOperator_Test(int x1, int y1, int x2, int y2, int x3, int y3)
        {
            var p = new Point(x1, y1);
            var q = new Point(x2, y2);
            var r = p - q;
            
            Assert.Equal(x3, r.X);
            Assert.Equal(y3, r.Y);

            var s = p - (I2DVector) q;
            Assert.Equal(x3, s.X);
            Assert.Equal(y3, s.Y);

            Assert.Equal(x3, (p - x2).X);
            Assert.Equal(y3, (p - y2).Y);
        }
        
        [Theory]
        [InlineData(3, 5, 2, 4, 6, 20)]
        [InlineData(1, 1, 2, 2, 2, 2)]
        [InlineData(-1, -1, 1, 1, -1, -1)]
        [InlineData(-3, 5, 2, -4, -6, -20)]
        public void MultiplyOperator_Test(int x1, int y1, int x2, int y2, int x3, int y3)
        {
            var p = new Point(x1, y1);
            var q = new Point(x2, y2);
            var r = p * q;
            
            Assert.Equal(x3, r.X);
            Assert.Equal(y3, r.Y);

            var s = p * (I2DVector) q;
            Assert.Equal(x3, s.X);
            Assert.Equal(y3, s.Y);

            Assert.Equal(x3, (p * x2).X);
            Assert.Equal(y3, (p * y2).Y);
        }

        [Theory]
        [InlineData(3, 5, 2, 4, 1.5, 1.25)]
        [InlineData(1, 1, 2, 2, 0.5, 0.5)]
        [InlineData(-1, -1, 1, 1, -1, -1)]
        [InlineData(-3, 5, 2, -4, -1.5, -1.25)]
        public void DivideOperator_Test(int x1, int y1, int x2, int y2, double x3, double y3)
        {
            var p = new Point(x1, y1);
            var q = new Point(x2, y2);
            var r = p / q;
            
            Assert.Equal(x3, r.X);
            Assert.Equal(y3, r.Y);

            var s = p / (I2DVector) q;
            Assert.Equal(x3, s.X);
            Assert.Equal(y3, s.Y);

            Assert.Equal(x3, (p / x2).X);
            Assert.Equal(y3, (p / y2).Y);
        }
        
        [Theory]
        [InlineData(3, 5, 2, 4, 1, 1)]
        [InlineData(1, 1, 2, 2, 1, 1)]
        [InlineData(-1, -1, 1, 1, 0, 0)]
        [InlineData(-3, 5, 2, -4, -1, 1)]
        [InlineData(13, 14, 5, 3, 3, 2)]
        public void ModulusOperator_Test(int x1, int y1, int x2, int y2, double x3, double y3)
        {
            var p = new Point(x1, y1);
            var q = new Point(x2, y2);
            var r = p % q;
            
            Assert.Equal(x3, r.X);
            Assert.Equal(y3, r.Y);

            var s = p % (I2DVector) q;
            Assert.Equal(x3, s.X);
            Assert.Equal(y3, s.Y);

            Assert.Equal(x3, (p % x2).X);
            Assert.Equal(y3, (p % y2).Y);
        }

        [Fact]
        public void Add1Operator_Test()
        {
            var p = new Point(3, 5);
            p++;
            Assert.Equal(4, p.X);
            Assert.Equal(6, p.Y);
        }
        
        [Fact]
        public void Substract1Operator_Test()
        {
            var p = new Point(3, 5);
            p--;
            Assert.Equal(2, p.X);
            Assert.Equal(4, p.Y);
        }
        
        [Fact]
        public void PlusOperator_Test()
        {
            var p = +new Point(3, 5);
            Assert.Equal(3, p.X);
            Assert.Equal(5, p.Y);

            p = +new Point(-1, -2);
            Assert.Equal(-1, p.X);
            Assert.Equal(-2, p.Y);
        }
                
        [Fact]
        public void MinusOperator_Test()
        {
            var p = -new Point(3, 5);
            Assert.Equal(-3, p.X);
            Assert.Equal(-5, p.Y);

            p = -new Point(-1, -2);
            Assert.Equal(1, p.X);
            Assert.Equal(2, p.Y);
        }
        
        [Fact]
        public void NotEquals_Test()
        {
            var p = new Point(3, 5);
            var q = new Point(3, 5);
            var r = new Point(5, 3);

            Assert.True(p != r);
            Assert.False(p != q);
            Assert.True(p != (I2DVector)r);
            Assert.False(p != (I2DVector)q);
        }

        [Theory]
        [InlineData(1, 0, 0)]
        [InlineData(1, 1, System.Math.PI / 4)]
        [InlineData(0, 1, System.Math.PI / 2)]
        [InlineData(0, -1, System.Math.Tau - (System.Math.PI / 2))]
        [InlineData(1, -1, System.Math.Tau - (System.Math.PI / 4))]
        public void Angle_Test(int x, int y, double angle)
        {
            Assert.True((new Point(x, y).Angle() - angle).IsBetween(-0.00000001, 0.00000001));
        }

        [Fact]
        public void WithinBox_Test()
        {
            var p = new Point(-5, -5);
            var q = new Point(5, 5);
            Assert.True(Point.Origin.WithinBox(p, q));
            Assert.False(new Point(10, 10).WithinBox(p, q));
            Assert.True(new Point(5, -5).WithinBox(p, q));
            Assert.True(Point.Origin.WithinBox(new Range<double>(-1, 1), new Range<double>(-1, 1)));
            Assert.False(Point.Origin.WithinBox(new Range<double>(2, 3), new Range<double>(1, 1)));
            Assert.True(Point.Origin.WithinBox(new Size(10, 10), new Point(-5, 5)));
            Assert.False(Point.Origin.WithinBox(new Size(2, 2), new Point(-5, 5)));
            Assert.True(Point.Origin.WithinBox(new Size(10, 10)));
            Assert.True(Point.Origin.WithinBox(new Size(2, 2)));
            Assert.False(new Point(3, 4).WithinBox(new Size(2, 2)));
            Assert.True(Point.Origin.WithinBox(-5, 5, 5, -5));
            Assert.True(Point.Origin.WithinBox(5, -5, -5, 5));
            Assert.False(Point.Origin.WithinBox(-15, -5, -10, 5));
            Assert.False(Point.Origin.WithinBox(-15, -5, -10, 5));
        }

        [Fact]
        public void Magnitude_Test()
        {
            var p = new Point(3, 5);
            Assert.Equal(p.Magnitude(), p.Magnitude(Point.Origin));
            Assert.Equal(p.Magnitude(), p.Magnitude(0, 0));
        }

        [Theory]
        [InlineData(0, 0, true)]
        [InlineData(10, 10, false)]
        [InlineData(10, 0, true)]
        [InlineData(0, 10, true)]
        [InlineData(8, 8, false)]
        [InlineData(7, 7, true)]
        [InlineData(-8, -8, false)]
        [InlineData(-7, -7, true)]
        [InlineData(-8, 8, false)]
        [InlineData(-7, 7, true)]
        [InlineData(8, -8, false)]
        [InlineData(7, -7, true)]
        public void WithinCircle_Test(int x, int y, bool result)
        {
            Assert.Equal(result, new Point(x, y).WithinCircle(Point.Origin, 10));
        }
    }
}