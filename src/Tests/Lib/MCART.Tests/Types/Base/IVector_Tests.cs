/*
IVector_Tests.cs

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

using Moq;
using System.Numerics;
using TheXDS.MCART.Types.Base;

namespace TheXDS.MCART.Tests.Types.Base;

public class IVector_Tests
{
    private class TestVector(double x, double y) : IVector
    {
        public double X { get; set; } = x;
        public double Y { get; set; } = y;
    }

    [TestCase(1.0, 1.0, 2.0, 2.0, true)]
    [TestCase(1.0, 2.0, 2.0, 2.0, false)]
    [TestCase(1.0, 1.0, 2.0, 1.0, false)]
    [TestCase(1.0, 2.0, 2.0, 1.0, false)]
    public void Equals_for_IVector_checks_for_equality(double x1, double x2, double y1, double y2, bool expectedResult)
    {
        IEquatable<IVector> v1 = new TestVector(x1, y1);
        IVector v2 = new TestVector(x2, y2);
        Assert.That(v1.Equals(v2), Is.EqualTo(expectedResult));
    }

    [Test]
    public void Equals_for_IVector_returns_false_for_null()
    {
        IEquatable<IVector> v1 = new TestVector(1.0, 1.0);
        IVector? v2 = null;
        Assert.That(v1.Equals(v2), Is.False);
    }

    [TestCase(1.0, 1f, 2.0, 2f, true)]
    [TestCase(1.0, 2f, 2.0, 2f, false)]
    [TestCase(1.0, 1f, 2.0, 1f, false)]
    [TestCase(1.0, 2f, 2.0, 1f, false)]
    public void Equals_for_Vector2_checks_for_equality(double x1, float x2, double y1, float y2, bool expectedResult)
    {
        IEquatable<Vector2> v1 = new TestVector(x1, y1);
        Vector2 v2 = new(x2, y2);
        Assert.That(v1.Equals(v2), Is.EqualTo(expectedResult));
    }

    [Test]
    public void ToVector2_returns_proper_Vector2_instance()
    {
        IVector v = new TestVector(1.5, 2.5);
        Vector2 vec = v.ToVector2();
        Assert.That(vec.X, Is.EqualTo(1.5f));
        Assert.That(vec.Y, Is.EqualTo(2.5f));
    }
}

public class IVector3D_Tests
{
    private class TestVector(double x, double y, double z) : IVector3D
    {
        public double X { get; set; } = x;
        public double Y { get; set; } = y;
        public double Z { get; set; } = z;
    }

    [TestCase(1.0, 1.0, 2.0, 2.0, 3.0, 3.0, true)]
    [TestCase(1.0, 2.0, 2.0, 2.0, 3.0, 3.0, false)]
    [TestCase(1.0, 1.0, 2.0, 1.0, 3.0, 3.0, false)]
    [TestCase(1.0, 1.0, 2.0, 2.0, 3.0, 4.0, false)]
    [TestCase(1.0, 2.0, 2.0, 1.0, 3.0, 4.0, false)]
    public void Equals_for_IVector3D_checks_for_equality(double x1, double x2, double y1, double y2, double z1, double z2, bool expectedResult)
    {
        IEquatable<IVector3D> v1 = new TestVector(x1, y1, z1);
        IVector3D v2 = new TestVector(x2, y2, z2);
        Assert.That(v1.Equals(v2), Is.EqualTo(expectedResult));
    }

    [TestCase(1.0, 1.0, 2.0, 2.0, double.NaN, true)]
    [TestCase(1.0, 2.0, 2.0, 2.0, double.NaN, false)]
    [TestCase(1.0, 1.0, 2.0, 1.0, double.NaN, false)]
    [TestCase(1.0, 2.0, 2.0, 1.0, double.NaN, false)]
    [TestCase(1.0, 1.0, 2.0, 2.0, 1.0, false)]
    public void Equals_for_IVector_checks_for_equality(double x1, double x2, double y1, double y2, double z, bool expectedResult)
    {
        IEquatable<IVector> v1 = new TestVector(x1, y1, z);
        IVector v2 = new TestVector(x2, y2, z);
        Assert.That(v1.Equals(v2), Is.EqualTo(expectedResult));
    }

    [Test]
    public void Equals_for_IVector3D_returns_false_for_null()
    {
        IEquatable<IVector3D> v1 = new TestVector(1.0, 1.0, 1.0);
        IVector3D? v2 = null;
        Assert.That(v1.Equals(v2), Is.False);
    }

    [Test]
    public void Equals_for_IVector_returns_false_for_null()
    {
        IEquatable<IVector> v1 = new TestVector(1.0, 1.0, 1.0);
        IVector? v2 = null;
        Assert.That(v1.Equals(v2), Is.False);
    }

    [TestCase(1.0, 1f, 2.0, 2f, double.NaN, true)]
    [TestCase(1.0, 2f, 2.0, 2f, double.NaN, false)]
    [TestCase(1.0, 1f, 2.0, 1f, double.NaN, false)]
    [TestCase(1.0, 2f, 2.0, 1f, double.NaN, false)]
    [TestCase(1.0, 1f, 2.0, 2f, 1.0, false)]
    public void Equals_for_Vector2_checks_for_equality(double x1, float x2, double y1, float y2, double z, bool expectedResult)
    {
        IEquatable<Vector2> v1 = new TestVector(x1, y1, z);
        Vector2 v2 = new(x2, y2);
        Assert.That(v1.Equals(v2), Is.EqualTo(expectedResult));
    }

    [TestCase(1.0, 1f, 2.0, 2f, 3.0, 3f, true)]
    [TestCase(1.0, 2f, 2.0, 2f, 3.0, 3f, false)]
    [TestCase(1.0, 1f, 2.0, 1f, 3.0, 3f, false)]
    [TestCase(1.0, 1f, 2.0, 2f, 3.0, 4f, false)]
    [TestCase(1.0, 2f, 2.0, 1f, 3.0, 4f, false)]
    public void Equals_for_Vector3_checks_for_equality(double x1, float x2, double y1, float y2, double z1, float z2, bool expectedResult)
    {
        IEquatable<Vector3> v1 = new TestVector(x1, y1,z1);
        Vector3 v2 = new(x2, y2, z2);
        Assert.That(v1.Equals(v2), Is.EqualTo(expectedResult));
    }

    [Test]
    public void ToVector3_returns_proper_Vector2_instance()
    {
        IVector3D v = new TestVector(1.5, 2.5, 3.5);
        Vector3 vec = v.ToVector3();
        Assert.That(vec.X, Is.EqualTo(1.5f));
        Assert.That(vec.Y, Is.EqualTo(2.5f));
        Assert.That(vec.Z, Is.EqualTo(3.5f));
    }
}