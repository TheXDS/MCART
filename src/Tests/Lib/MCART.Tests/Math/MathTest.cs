/*
MathTest.cs

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

namespace TheXDS.MCART.Tests.Math;
using NUnit.Framework;
using TheXDS.MCART.Math;

public class MathTests
{
    [Test]
    public void ClampTest()
    {
        Assert.That(10, Is.EqualTo((5 + 10).Clamp(10)));
        Assert.That(0, Is.EqualTo((5 - 10).Clamp(10)));
        Assert.That(10, Is.EqualTo((5 + 10).Clamp(1, 10)));
        Assert.That(1, Is.EqualTo((5 - 10).Clamp(1, 10)));

        Assert.That(float.IsNaN(float.NaN.Clamp(1, 10)));
        Assert.That(10f, Is.EqualTo(13f.Clamp(1, 10)));
        Assert.That(1f, Is.EqualTo((-5f).Clamp(1, 10)));
        Assert.That((-5f).Clamp(10), Is.EqualTo(-5f));
        Assert.That(10f, Is.EqualTo(float.PositiveInfinity.Clamp(1, 10)));
        Assert.That(1f, Is.EqualTo(float.NegativeInfinity.Clamp(1, 10)));

        Assert.That(double.IsNaN(double.NaN.Clamp(1, 10)));
        Assert.That(10.0, Is.EqualTo(13.0.Clamp(1, 10)));
        Assert.That(1.0, Is.EqualTo((-5.0).Clamp(1, 10)));
        Assert.That((-5.0).Clamp(10), Is.EqualTo(-5.0));
        Assert.That(10.0, Is.EqualTo(double.PositiveInfinity.Clamp(1, 10)));
        Assert.That(1.0, Is.EqualTo(double.NegativeInfinity.Clamp(1, 10)));
    }

    [Test]
    public void WrapTest()
    {
        Assert.That(5f, Is.EqualTo(16f.Wrap(5, 15)));
        Assert.That(6f, Is.EqualTo(17f.Wrap(5, 15)));
        Assert.That(15f, Is.EqualTo(4f.Wrap(5, 15)));
        Assert.That(14f, Is.EqualTo(3f.Wrap(5, 15)));
        Assert.That(8f, Is.EqualTo(8f.Wrap(5, 15)));

        Assert.That(5, Is.EqualTo(16.Wrap(5, 15)));
        Assert.That(6, Is.EqualTo(17.Wrap(5, 15)));
        Assert.That(15, Is.EqualTo(4.Wrap(5, 15)));
        Assert.That(14, Is.EqualTo(3.Wrap(5, 15)));
        Assert.That(8, Is.EqualTo(8.Wrap(5, 15)));
    }
}
