/*
AlgebraTests.cs

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
using System.Collections.Generic;
using System.Linq;
using TheXDS.MCART.Helpers;
using TheXDS.MCART.Math;
using TheXDS.MCART.Types;
using Xunit;
using static TheXDS.MCART.Math.Algebra;
using static TheXDS.MCART.Math.Geometry;

namespace TheXDS.MCART.Tests.Math
{
    public class AlgebraTests
    {
        private static IEnumerable<object[]> ObjArray(Type valType, IEnumerable<long> data)
        {
            return data.Select(p => new [] { Convert.ChangeType(p, valType) });
        }

        public static IEnumerable<object[]> GetKnownPrimes(Type valType, long max)
        {
            return ObjArray(valType, new long[] {
                2, 3, 5, 7, 11, 13, 17, 19, 23, 29,
                8191, 131071, 524287, 6700417, 1000003, 2000003
            }.Where(p => p <= max));
        }

        public static IEnumerable<object[]> GetKnownNotPrimes(Type valType, long max)
        {
            return ObjArray(valType, new long[] {
                1, 4, 6, 8, 9, 10, 12, 14, 15, 16,
                18, 20, 21, 22, 24, 25, 26, 27, 28, 39, 1000001, 2000001, 4000064000087
            }.Where(p => p <= max));
        }

        public static IEnumerable<object[]> Get2Pows(Type valType, long max, int dev)
        {
            static IEnumerable<long> EnumeratePows(long m, int d)
            {
                var c = 2L;
                while (c < m)
                {
                    yield return c + d;
                    c *= 2;
                }
            }

            return ObjArray(valType, EnumeratePows(max, dev));
        }

        [Theory]
        [CLSCompliant(false)]
        [MemberData(nameof(GetKnownPrimes), typeof(long), long.MaxValue)]
        public void AssertPrimes_long_Test(long number)
        {
            Assert.True(number.IsPrime());
            Assert.True(number.IsPrimeMp());
        }

        [Theory]
        [CLSCompliant(false)]
        [MemberData(nameof(GetKnownNotPrimes), typeof(long), long.MaxValue)]
        public void AssertNotPrimes_long_Test(long number)
        {
            Assert.False(number.IsPrime());
            Assert.False(number.IsPrimeMp());
        }

        [Theory]
        [CLSCompliant(false)]
        [MemberData(nameof(GetKnownPrimes), typeof(int), (long)int.MaxValue)]
        public void AssertPrimes_int_Test(int number)
        {
            Assert.True(number.IsPrime());
            Assert.True(number.IsPrimeMp());
        }

        [Theory]
        [CLSCompliant(false)]
        [MemberData(nameof(GetKnownNotPrimes), typeof(int), (long)int.MaxValue)]
        public void AssertNotPrimes_int_Test(int number)
        {
            Assert.False(number.IsPrime());
            Assert.False(number.IsPrimeMp());
        }

        [Theory]
        [CLSCompliant(false)]
        [MemberData(nameof(GetKnownPrimes), typeof(short), (long)short.MaxValue)]
        public void AssertPrimes_short_Test(short number)
        {
            Assert.True(number.IsPrime());
            Assert.True(number.IsPrimeMp());
        }

        [Theory]
        [CLSCompliant(false)]
        [MemberData(nameof(GetKnownNotPrimes), typeof(short), (long)short.MaxValue)]
        public void AssertNotPrimes_short_Test(short number)
        {
            Assert.False(number.IsPrime());
            Assert.False(number.IsPrimeMp());
        }

        [Theory]
        [CLSCompliant(false)]
        [MemberData(nameof(GetKnownPrimes), typeof(byte), (long)byte.MaxValue)]
        public void AssertPrimes_byte_Test(byte number)
        {
            Assert.True(number.IsPrime());
            Assert.True(number.IsPrimeMp());
        }

        [Theory]
        [CLSCompliant(false)]
        [MemberData(nameof(GetKnownNotPrimes), typeof(byte), (long)byte.MaxValue)]
        public void AssertNotPrimes_byte_Test(byte number)
        {
            Assert.False(number.IsPrime());
            Assert.False(number.IsPrimeMp());
        }

        [Fact]
        public void IsValidTest()
        {
            Assert.True(1.0.IsValid());
            Assert.False(float.NaN.IsValid());
            Assert.False(double.NegativeInfinity.IsValid());
        }

        [Fact]
        public void AreValidTest()
        {
            Assert.True(AreValid(1f, 2f, 3f, 4f, 5f));
            Assert.False(AreValid(1f, 2f, float.NaN, 4f, 5f));
            Assert.True(new[] { 1f, 2f, 3f, 4f, 5f }.AreValid());
            Assert.False(new[] { 1f, 2f, float.NaN, 4f, 5f }.AreValid());
            Assert.True(AreValid(1.0, 2.0, 3.0, 4.0, 5.0));
            Assert.False(AreValid(1.0, 2.0, double.NaN, 4.0, 5.0));
            Assert.True(new[] { 1.0, 2.0, 3.0, 4.0, 5.0 }.AreValid());
            Assert.False(new[] { 1.0, 2.0, double.NaN, 4.0, 5.0 }.AreValid());
        }

        [Fact]
        public void Nearest2PowTest()
        {
            Assert.Equal(512, Nearest2Pow(456));
        }

        [Fact]
        public void NearestMultiplyUpTest()
        {
            Assert.Equal(81.0, NearestMultiplyUp(50, 3));
        }

        [Fact]
        public void ArePositivesTest()
        {
            Assert.True(ArePositive(1, 2, 3, 4, 5));
            Assert.False(ArePositive(1, 2, 3, 0));
            Assert.False(ArePositive(1, 2, 3, -1));
            Assert.True(new[] { 1, 2, 3, 4, 5 }.ArePositive());
            Assert.False(new[] { 1, 2, 3, 0 }.ArePositive());
            Assert.False(new[] { 1, 2, 3, -1 }.ArePositive());
        }

        [Fact]
        public void AreNegativesTest()
        {
            Assert.True(AreNegative(-1, -2, -3, -4, -5));
            Assert.False(AreNegative(-1, -2, -3, 0));
            Assert.False(AreNegative(-1, -2, -3, 1));
            Assert.True(new[] { -1, -2, -3, -4, -5 }.AreNegative());
            Assert.False(new[] { -1, -2, -3, 0 }.AreNegative());
            Assert.False(new[] { -1, -2, -3, 1 }.AreNegative());
        }

        [Fact]
        public void AreZeroTest()
        {
            Assert.True(AreZero(0, 0, 0));
            Assert.False(AreZero(0, 1, 0));
            Assert.True(new[] { 0, 0, 0 }.AreZero());
            Assert.False(new[] { 0, 1, 0 }.AreZero());
        }

        [Fact]
        public void AreNotZeroTest()
        {
            Assert.True(AreNotZero(1, 2, 3));
            Assert.False(AreNotZero(1, 2, 0));
            Assert.True(new[] { 1, 2, 3 }.AreNotZero());
            Assert.False(new[] { 1, 2, 0 }.AreNotZero());
        }

        [Fact]
        public void IsWholeTest()
        {
            Assert.True(14.0.IsWhole());
            Assert.False(14.1.IsWhole());
            Assert.False(14.9.IsWhole());
        }

        [Theory]
        [CLSCompliant(false)]
        [MemberData(nameof(Get2Pows), typeof(byte), 128L, 1)]
        public void IsTwoPow_byte_YieldsFalse_Test(byte value)
        {
            Assert.False(IsTwoPow(value));
        }

        [Theory]
        [CLSCompliant(false)]
        [MemberData(nameof(Get2Pows), typeof(byte), 128L, 0)]
        public void IsTwoPow_byte_YieldsTrue_Test(byte value)
        {
            Assert.True(IsTwoPow(value));
        }

        [Theory]
        [CLSCompliant(false)]
        [MemberData(nameof(Get2Pows), typeof(short), 32768L, 1)]
        public void IsTwoPow_short_YieldsFalse_Test(short value)
        {
            Assert.False(IsTwoPow(value));
        }

        [Theory]
        [CLSCompliant(false)]
        [MemberData(nameof(Get2Pows), typeof(short), 32768L, 0)]
        public void IsTwoPow_short_YieldsTrue_Test(short value)
        {
            Assert.True(IsTwoPow(value));
        }

        [Theory]
        [CLSCompliant(false)]
        [MemberData(nameof(Get2Pows), typeof(int), 131072L, 1)]
        public void IsTwoPow_int_YieldsFalse_Test(int value)
        {
            Assert.False(IsTwoPow(value));
        }

        [Theory]
        [CLSCompliant(false)]
        [MemberData(nameof(Get2Pows), typeof(int), 131072L, 0)]
        public void IsTwoPow_int_YieldsTrue_Test(int value)
        {
            Assert.True(IsTwoPow(value));
        }

        [Theory]
        [CLSCompliant(false)]
        [MemberData(nameof(Get2Pows), typeof(long), 131072L, 1)]
        public void IsTwoPow_long_YieldsFalse_Test(long value)
        {
            Assert.False(IsTwoPow(value));
        }

        [Theory]
        [CLSCompliant(false)]
        [MemberData(nameof(Get2Pows), typeof(long), 131072L, 0)]
        public void IsTwoPow_long_YieldsTrue_Test(long value)
        {
            Assert.True(IsTwoPow(value));
        }

        [Theory]
        [CLSCompliant(false)]
        [InlineData(0, 0.5, 0, 1, 0, 0.5)]
        [InlineData(1, 0, 1, 1, 0.75, 0.25)]
        public void GetQuadBezierPoint_Test(double cx, double cy, double ex, double ey, double rx, double ry)
        {
            var p1 = Point.Origin;
            var p2 = new Point(cx, cy);
            var p3 = new Point(ex, ey);

            var pr = GetQuadBezierPoint(0.5, p1, p2, p3);
            
            Assert.Equal(rx, pr.X);
            Assert.Equal(ry, pr.Y);
        }

        [Fact]
        public void GetQuadBezierPoint_Contract_Test()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                GetQuadBezierPoint(1.2, Point.Origin, Point.Origin, Point.Origin));

            Assert.Throws<ArgumentException>(() =>
                GetQuadBezierPoint(0.5, Point.Nowhere, Point.Origin, Point.Origin));
            Assert.Throws<ArgumentException>(() =>
                GetQuadBezierPoint(0.5, Point.Origin, Point.Nowhere, Point.Origin));
            Assert.Throws<ArgumentException>(() =>
                GetQuadBezierPoint(0.5, Point.Origin, Point.Origin, Point.Nowhere));
        }
        
        [Theory]
        [CLSCompliant(false)]
        [InlineData(0, 0, 100)]
        [InlineData(0.125, -70.710678118654, 70.710678118654)]
        [InlineData(0.25, -100, 0)]
        [InlineData(0.375, -70.710678118654, -70.710678118654)]
        [InlineData(0.5, 0, -100)]
        [InlineData(0.625, 70.710678118654, -70.710678118654)]
        [InlineData(0.75, 100, 0)]
        [InlineData(0.875, 70.710678118654, 70.710678118654)]
        public void GetArcPoint_Test(double pos, double px, double py)
        {
            const double epsilon = 1E-12;
            
            var pr = GetArcPoint(100, 0, 360, pos);
            Assert.True(pr.X.IsBetween(px - epsilon, px + epsilon));
            Assert.True(pr.Y.IsBetween(py - epsilon, py + epsilon));
        }

        [Theory]
        [CLSCompliant(false)]
        [InlineData(0, 0, 100)]
        [InlineData(0.125, -70.710678118654, 70.710678118654)]
        [InlineData(0.25, -100, 0)]
        [InlineData(0.375, -70.710678118654, -70.710678118654)]
        [InlineData(0.5, 0, -100)]
        [InlineData(0.625, 70.710678118654, -70.710678118654)]
        [InlineData(0.75, 100, 0)]
        [InlineData(0.875, 70.710678118654, 70.710678118654)]
        public void GetCirclePoint_Test(double pos, double px, double py)
        {
            const double epsilon = 1E-12;
            
            var pr = GetCirclePoint(100, pos);
            Assert.True(pr.X.IsBetween(px - epsilon, px + epsilon));
            Assert.True(pr.Y.IsBetween(py - epsilon, py + epsilon));
        }
    }
}
