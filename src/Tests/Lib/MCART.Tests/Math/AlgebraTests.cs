/*
AlgebraTests.cs

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

using TheXDS.MCART.Helpers;
using TheXDS.MCART.Math;
using TheXDS.MCART.Types;
using static TheXDS.MCART.Math.Algebra;
using static TheXDS.MCART.Math.Geometry;

namespace TheXDS.MCART.Tests.Math;

public class AlgebraTests
{
    private static IEnumerable<object[]> ObjArray(Type valType, IEnumerable<long> data)
    {
        return data.Select(p => new[] { Convert.ChangeType(p, valType) });
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
            long c = 2L;
            while (c < m)
            {
                yield return c + d;
                c *= 2;
            }
        }

        return ObjArray(valType, EnumeratePows(max, dev));
    }

    [TestCaseSource(typeof(AlgebraTests), nameof(GetKnownPrimes), new object[] { typeof(long), long.MaxValue })]
    public void AssertPrimes_long_Test(long number)
    {
        Assert.That(number.IsPrime());
        Assert.That(number.IsPrimeMp());
    }

    [TestCaseSource(typeof(AlgebraTests), nameof(GetKnownNotPrimes), new object[] { typeof(long), long.MaxValue })]
    public void AssertNotPrimes_long_Test(long number)
    {
        Assert.That(number.IsPrime(), Is.False);
        Assert.That(number.IsPrimeMp(), Is.False);
    }

    [TestCaseSource(typeof(AlgebraTests), nameof(GetKnownPrimes), new object[] { typeof(int), (long)int.MaxValue })]
    public void AssertPrimes_int_Test(int number)
    {
        Assert.That(number.IsPrime());
        Assert.That(number.IsPrimeMp());
    }

    [TestCaseSource(typeof(AlgebraTests), nameof(GetKnownNotPrimes), new object[] { typeof(int), (long)int.MaxValue })]
    public void AssertNotPrimes_int_Test(int number)
    {
        Assert.That(number.IsPrime(), Is.False);
        Assert.That(number.IsPrimeMp(), Is.False);
    }

    [TestCaseSource(typeof(AlgebraTests), nameof(GetKnownPrimes), new object[] { typeof(short), (long)short.MaxValue })]
    public void AssertPrimes_short_Test(short number)
    {
        Assert.That(number.IsPrime());
        Assert.That(number.IsPrimeMp());
    }

    [TestCaseSource(typeof(AlgebraTests), nameof(GetKnownNotPrimes), new object[] { typeof(short), (long)short.MaxValue })]
    public void AssertNotPrimes_short_Test(short number)
    {
        Assert.That(number.IsPrime(), Is.False);
        Assert.That(number.IsPrimeMp(), Is.False);
    }

    [TestCaseSource(typeof(AlgebraTests), nameof(GetKnownPrimes), new object[] { typeof(byte), (long)byte.MaxValue })]
    public void AssertPrimes_byte_Test(byte number)
    {
        Assert.That(number.IsPrime());
        Assert.That(number.IsPrimeMp());
    }

    [TestCaseSource(typeof(AlgebraTests), nameof(GetKnownNotPrimes), new object[] { typeof(byte), (long)byte.MaxValue })]
    public void AssertNotPrimes_byte_Test(byte number)
    {
        Assert.That(number.IsPrime(), Is.False);
        Assert.That(number.IsPrimeMp(), Is.False);
    }
    
    [TestCaseSource(typeof(AlgebraTests), nameof(GetKnownNotPrimes), new object[] { typeof(sbyte), (long)sbyte.MaxValue })]
    public void AssertNotPrimes_sbyte_Test(sbyte number)
    {
        Assert.That(number.IsPrime(), Is.False);
        Assert.That(number.IsPrimeMp(), Is.False);
    }

    [TestCaseSource(typeof(AlgebraTests), nameof(GetKnownNotPrimes), new object[] { typeof(ushort), (long)ushort.MaxValue })]
    public void AssertNotPrimes_ushort_Test(ushort number)
    {
        Assert.That(number.IsPrime(), Is.False);
        Assert.That(number.IsPrimeMp(), Is.False);
    }
    
    [TestCaseSource(typeof(AlgebraTests), nameof(GetKnownNotPrimes), new object[] { typeof(uint), (long)uint.MaxValue })]
    public void AssertNotPrimes_uint_Test(uint number)
    {
        Assert.That(number.IsPrime(), Is.False);
        Assert.That(number.IsPrimeMp(), Is.False);
    }

    [Test]
    public void IsValidTest()
    {
        Assert.That(1.0.IsValid());
        Assert.That(float.NaN.IsValid(), Is.False);
        Assert.That(double.NegativeInfinity.IsValid(), Is.False);
    }

    [Test]
    public void AreValidTest()
    {
        Assert.That(AreValid(1f, 2f, 3f, 4f, 5f));
        Assert.That(AreValid(1f, 2f, float.NaN, 4f, 5f), Is.False);
        Assert.That(new[] { 1f, 2f, 3f, 4f, 5f }.AreValid());
        Assert.That(new[] { 1f, 2f, float.NaN, 4f, 5f }.AreValid(), Is.False);
        Assert.That(AreValid(1.0, 2.0, 3.0, 4.0, 5.0));
        Assert.That(AreValid(1.0, 2.0, double.NaN, 4.0, 5.0), Is.False);
        Assert.That(new[] { 1.0, 2.0, 3.0, 4.0, 5.0 }.AreValid());
        Assert.That(new[] { 1.0, 2.0, double.NaN, 4.0, 5.0 }.AreValid(), Is.False);
    }

    [Test]
    public void Nearest2PowTest()
    {
        Assert.That(512, Is.EqualTo(Nearest2Pow(456)));
    }

    [Test]
    public void NearestMultiplyUpTest()
    {
        Assert.That(81.0, Is.EqualTo(NearestPowerUp(50, 3)));
        Assert.That(1.0, Is.EqualTo(NearestPowerUp(-50, 3)));
        Assert.That(1.0, Is.EqualTo(NearestPowerUp(50, -3)));
        Assert.That(1.0, Is.EqualTo(NearestPowerUp(-50, -3)));
    }

    [Test]
    public void ArePositivesTest()
    {
        Assert.That(ArePositive(1, 2, 3, 4, 5));
        Assert.That(ArePositive(1, 2, 3, 0), Is.False);
        Assert.That(ArePositive(1, 2, 3, -1), Is.False);
        Assert.That(new[] { 1, 2, 3, 4, 5 }.ArePositive());
        Assert.That(new[] { 1, 2, 3, 0 }.ArePositive(), Is.False);
        Assert.That(new[] { 1, 2, 3, -1 }.ArePositive(), Is.False);
    }

    [Test]
    public void AreNegativesTest()
    {
        Assert.That(AreNegative(-1, -2, -3, -4, -5));
        Assert.That(AreNegative(-1, -2, -3, 0), Is.False);
        Assert.That(AreNegative(-1, -2, -3, 1), Is.False);
        Assert.That(new[] { -1, -2, -3, -4, -5 }.AreNegative());
        Assert.That(new[] { -1, -2, -3, 0 }.AreNegative(), Is.False);
        Assert.That(new[] { -1, -2, -3, 1 }.AreNegative(), Is.False);
    }

    [Test]
    public void AreZeroTest()
    {
        Assert.That(AreZero(0, 0, 0));
        Assert.That(AreZero(0, 1, 0), Is.False);
        Assert.That(new[] { 0, 0, 0 }.AreZero());
        Assert.That(new[] { 0, 1, 0 }.AreZero(), Is.False);
    }

    [Test]
    public void AreNotZeroTest()
    {
        Assert.That(AreNotZero(1, 2, 3));
        Assert.That(AreNotZero(1, 2, 0), Is.False);
        Assert.That(new[] { 1, 2, 3 }.AreNotZero());
        Assert.That(new[] { 1, 2, 0 }.AreNotZero(), Is.False);
    }

    [Test]
    public void IsWholeTest()
    {
        Assert.That(14.0.IsWhole());
        Assert.That(14.1.IsWhole(), Is.False);
        Assert.That(14.9.IsWhole(), Is.False);
    }

    [TestCaseSource(typeof(AlgebraTests), nameof(Get2Pows), new object[] { typeof(byte), 128L, 1 })]
    public void IsTwoPow_byte_YieldsFalse_Test(byte value)
    {
        Assert.That(IsTwoPow(value), Is.False);
    }

    [TestCaseSource(typeof(AlgebraTests), nameof(Get2Pows), new object[] { typeof(byte), 128L, 0 })]
    public void IsTwoPow_byte_YieldsTrue_Test(byte value)
    {
        Assert.That(IsTwoPow(value));
    }
    
    [TestCaseSource(typeof(AlgebraTests), nameof(Get2Pows), new object[] { typeof(sbyte), 64L, 1 })]
    public void IsTwoPow_sbyte_YieldsFalse_Test(sbyte value)
    {
        Assert.That(IsTwoPow(value), Is.False);
    }

    [TestCaseSource(typeof(AlgebraTests), nameof(Get2Pows), new object[] { typeof(sbyte), 64L, 0 })]
    public void IsTwoPow_sbyte_YieldsTrue_Test(sbyte value)
    {
        Assert.That(IsTwoPow(value));
    }
    
    [TestCaseSource(typeof(AlgebraTests), nameof(Get2Pows), new object[] { typeof(ushort), 65536L, 1 })]
    public void IsTwoPow_ushort_YieldsFalse_Test(ushort value)
    {
        Assert.That(IsTwoPow(value), Is.False);
    }

    [TestCaseSource(typeof(AlgebraTests), nameof(Get2Pows), new object[] { typeof(ushort), 65536L, 0 })]
    public void IsTwoPow_ushort_YieldsTrue_Test(ushort value)
    {
        Assert.That(IsTwoPow(value));
    }

    [TestCaseSource(typeof(AlgebraTests), nameof(Get2Pows), new object[] { typeof(short), 32768L, 1 })]
    public void IsTwoPow_short_YieldsFalse_Test(short value)
    {
        Assert.That(IsTwoPow(value), Is.False);
    }

    [TestCaseSource(typeof(AlgebraTests), nameof(Get2Pows), new object[] { typeof(short), 32768L, 0 })]
    public void IsTwoPow_short_YieldsTrue_Test(short value)
    {
        Assert.That(IsTwoPow(value));
    }
    
    [TestCaseSource(typeof(AlgebraTests), nameof(Get2Pows), new object[] { typeof(uint), 131072L, 1 })]
    public void IsTwoPow_uint_YieldsFalse_Test(uint value)
    {
        Assert.That(IsTwoPow(value), Is.False);
    }

    [TestCaseSource(typeof(AlgebraTests), nameof(Get2Pows), new object[] { typeof(uint), 131072L, 0 })]
    public void IsTwoPow_uint_YieldsTrue_Test(uint value)
    {
        Assert.That(IsTwoPow(value));
    }
    
    [TestCaseSource(typeof(AlgebraTests), nameof(Get2Pows), new object[] { typeof(int), 131072L, 1 })]
    public void IsTwoPow_int_YieldsFalse_Test(int value)
    {
        Assert.That(IsTwoPow(value), Is.False);
    }

    [TestCaseSource(typeof(AlgebraTests), nameof(Get2Pows), new object[] { typeof(int), 131072L, 0 })]
    public void IsTwoPow_int_YieldsTrue_Test(int value)
    {
        Assert.That(IsTwoPow(value));
    }

    [TestCaseSource(typeof(AlgebraTests), nameof(Get2Pows), new object[] { typeof(ulong), 131072L, 1 })]
    public void IsTwoPow_ulong_YieldsFalse_Test(ulong value)
    {
        Assert.That(IsTwoPow(value), Is.False);
    }

    [TestCaseSource(typeof(AlgebraTests), nameof(Get2Pows), new object[] { typeof(ulong), 131072L, 0 })]
    public void IsTwoPow_ulong_YieldsTrue_Test(ulong value)
    {
        Assert.That(IsTwoPow(value));
    }

    [TestCaseSource(typeof(AlgebraTests), nameof(Get2Pows), new object[] { typeof(long), 131072L, 1 })]
    public void IsTwoPow_long_YieldsFalse_Test(long value)
    {
        Assert.That(IsTwoPow(value), Is.False);
    }

    [TestCaseSource(typeof(AlgebraTests), nameof(Get2Pows), new object[] { typeof(long), 131072L, 0 })]
    public void IsTwoPow_long_YieldsTrue_Test(long value)
    {
        Assert.That(IsTwoPow(value));
    }

    [TestCase(0, 0.5, 0, 1, 0, 0.5)]
    [TestCase(1, 0, 1, 1, 0.75, 0.25)]
    public void GetQuadBezierPoint_Test(double cx, double cy, double ex, double ey, double rx, double ry)
    {
        Point p1 = Point.Origin;
        Point p2 = new(cx, cy);
        Point p3 = new(ex, ey);

        Point pr = GetQuadBezierPoint(0.5, p1, p2, p3);

        Assert.That(rx, Is.EqualTo(pr.X));
        Assert.That(ry, Is.EqualTo(pr.Y));
    }

    [Test]
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
    [TestCase(0, 0, 100)]
    [TestCase(0.125, -70.710678118654, 70.710678118654)]
    [TestCase(0.25, -100, 0)]
    [TestCase(0.375, -70.710678118654, -70.710678118654)]
    [TestCase(0.5, 0, -100)]
    [TestCase(0.625, 70.710678118654, -70.710678118654)]
    [TestCase(0.75, 100, 0)]
    [TestCase(0.875, 70.710678118654, 70.710678118654)]
    public void GetArcPoint_Test(double pos, double px, double py)
    {
        const double epsilon = 1E-12;

        Point pr = GetArcPoint(100, 0, 360, pos);
        Assert.That(pr.X.IsBetween(px - epsilon, px + epsilon));
        Assert.That(pr.Y.IsBetween(py - epsilon, py + epsilon));
    }

    [Theory]
    [TestCase(0, 0, 100)]
    [TestCase(0.125, -70.710678118654, 70.710678118654)]
    [TestCase(0.25, -100, 0)]
    [TestCase(0.375, -70.710678118654, -70.710678118654)]
    [TestCase(0.5, 0, -100)]
    [TestCase(0.625, 70.710678118654, -70.710678118654)]
    [TestCase(0.75, 100, 0)]
    [TestCase(0.875, 70.710678118654, 70.710678118654)]
    public void GetCirclePoint_Test(double pos, double px, double py)
    {
        const double epsilon = 1E-12;

        Point pr = GetCirclePoint(100, pos);
        Assert.That(pr.X.IsBetween(px - epsilon, px + epsilon));
        Assert.That(pr.Y.IsBetween(py - epsilon, py + epsilon));
    }
}
