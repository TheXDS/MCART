/*
CommonTests.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2024 César Andrés Morgan

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

using System.Diagnostics.CodeAnalysis;
using TheXDS.MCART.Math;
using M = TheXDS.MCART.Math.Common;

namespace TheXDS.MCART.Tests.Math;

public class CommonTests
{
    [Test]
    public void OrIfInvalid_double_Test()
    {
        [ExcludeFromCodeCoverage] static double Get() => 1.0;
        Assert.That(5.0, Is.EqualTo(5.0.OrIfInvalid(1.0)));
        Assert.That(1.0, Is.EqualTo((5.0 / 0.0).OrIfInvalid(1.0)));
        Assert.That(5.0, Is.EqualTo(5.0.OrIfInvalid(Get)));
        Assert.That(1.0, Is.EqualTo((5.0 / 0.0).OrIfInvalid(() => 1.0)));
    }
    
    [Test]
    public void OrIfInvalid_float_Test()
    {
        [ExcludeFromCodeCoverage] static float Get() => 1.0f;
        Assert.That(5.0f, Is.EqualTo(5.0f.OrIfInvalid(1.0f)));
        Assert.That(1.0f, Is.EqualTo((5.0f / 0.0f).OrIfInvalid(1.0f)));
        Assert.That(5.0f, Is.EqualTo(5.0f.OrIfInvalid(Get)));
        Assert.That(1.0f, Is.EqualTo((5.0f / 0.0f).OrIfInvalid(() => 1.0f)));
    }

    [Test]
    public void ClampTest()
    {
        Assert.That(2, Is.EqualTo((1 + 1).Clamp(0, 3)));
        Assert.That(2, Is.EqualTo((1 + 3).Clamp(0, 2)));
        Assert.That(2, Is.EqualTo((1 + 0).Clamp(2, 4)));
    }

    [Test]
    public void ClampTest_double()
    {
        Assert.That(2.0, Is.EqualTo((1.0 + 1.0).Clamp(0.0, 3.0)));
        Assert.That(2.0, Is.EqualTo((1.0 + 3.0).Clamp(0.0, 2.0)));
        Assert.That(2.0, Is.EqualTo((1.0 - 1.0).Clamp(2.0, 3.0)));
        Assert.That(double.NaN, Is.EqualTo(double.NaN.Clamp(0.0, 1.0)));
        Assert.That(5.0, Is.EqualTo(double.PositiveInfinity.Clamp(-5.0, 5.0)));
        Assert.That(-5.0, Is.EqualTo(double.NegativeInfinity.Clamp(-5.0, 5.0)));
    }

    [Test]
    public void ClampTest_float()
    {
        Assert.That(2.0f, Is.EqualTo((1.0f + 1.0f).Clamp(0.0f, 3.0f)));
        Assert.That(2.0f, Is.EqualTo((1.0f + 3.0f).Clamp(0.0f, 2.0f)));
        Assert.That(2.0f, Is.EqualTo((1.0f - 1.0f).Clamp(2.0f, 3.0f)));
        Assert.That(float.NaN, Is.EqualTo(float.NaN.Clamp(0.0f, 1.0f)));
        Assert.That(5.0f, Is.EqualTo(float.PositiveInfinity.Clamp(-5.0f, 5.0f)));
        Assert.That(-5.0f, Is.EqualTo(float.NegativeInfinity.Clamp(-5.0f, 5.0f)));
    }

    [Theory]
    [TestCase(1, 1)]
    [TestCase(2, 2)]
    [TestCase(3, 3)]
    [TestCase(0, 15)]
    [TestCase(-1, 14)]
    [TestCase(-2, 13)]
    [TestCase(14, 14)]
    [TestCase(15, 15)]
    [TestCase(16, 1)]
    [TestCase(17, 2)]
    public void WrapTest(int expression, int wrapped)
    {
        Assert.That((sbyte)wrapped, Is.EqualTo(((sbyte)expression).Wrap(1, 15)));
        Assert.That((short)wrapped, Is.EqualTo(((short)expression).Wrap(1, 15)));
        Assert.That(wrapped, Is.EqualTo(expression.Wrap(1, 15)));
        Assert.That(wrapped, Is.EqualTo(M.Wrap(expression, 1L, 15L)));
        Assert.That(wrapped, Is.EqualTo(M.Wrap(expression, 1f, 15f)));
        Assert.That(wrapped, Is.EqualTo(M.Wrap(expression, 1.0, 15.0))); 
        Assert.That(wrapped, Is.EqualTo(M.Wrap(expression, 1M, 15M)));

        // Para tipos sin signo, no se deben realizar los tests con valores negativos.
        if (expression >= 0)
        {
            Assert.That((byte)wrapped, Is.EqualTo(((byte)expression).Wrap(1, 15)));
            Assert.That((char)wrapped, Is.EqualTo(((char)expression).Wrap((char)1, (char)15)));
            Assert.That((ushort)wrapped, Is.EqualTo(((ushort)expression).Wrap(1, 15)));
            Assert.That((uint)wrapped, Is.EqualTo(((uint)expression).Wrap(1, 15)));
            Assert.That((ulong)wrapped, Is.EqualTo(((ulong)expression).Wrap(1, 15)));
        }
    }

    [Test]
    public void Wrap_WithFPNaNValues_Test()
    {
        Assert.That(float.NaN, Is.EqualTo(float.NaN.Wrap(1, 2)));
        Assert.That(double.NaN, Is.EqualTo(double.NaN.Wrap(1, 2)));
    }
}
