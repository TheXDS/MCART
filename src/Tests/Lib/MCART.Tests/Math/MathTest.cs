/*
MathTest.cs

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

namespace TheXDS.MCART.Tests.Math;
using NUnit.Framework;
using TheXDS.MCART.Math;

public class MathTests
{
    [Test]
    public void ClampTest()
    {
        Assert.AreEqual(10, (5 + 10).Clamp(10));
        Assert.AreEqual(0, (5 - 10).Clamp(10));
        Assert.AreEqual(10, (5 + 10).Clamp(1, 10));
        Assert.AreEqual(1, (5 - 10).Clamp(1, 10));

        Assert.True(float.IsNaN(float.NaN.Clamp(1, 10)));
        Assert.AreEqual(10f, 13f.Clamp(1, 10));
        Assert.AreEqual(1f, (-5f).Clamp(1, 10));
        Assert.AreEqual((-5f).Clamp(10), -5f);
        Assert.AreEqual(10f, float.PositiveInfinity.Clamp(1, 10));
        Assert.AreEqual(1f, float.NegativeInfinity.Clamp(1, 10));

        Assert.True(double.IsNaN(double.NaN.Clamp(1, 10)));
        Assert.AreEqual(10.0, 13.0.Clamp(1, 10));
        Assert.AreEqual(1.0, (-5.0).Clamp(1, 10));
        Assert.AreEqual((-5.0).Clamp(10), -5.0);
        Assert.AreEqual(10.0, double.PositiveInfinity.Clamp(1, 10));
        Assert.AreEqual(1.0, double.NegativeInfinity.Clamp(1, 10));
    }

    [Test]
    public void WrapTest()
    {
        Assert.AreEqual(5f, 16f.Wrap(5, 15));
        Assert.AreEqual(6f, 17f.Wrap(5, 15));
        Assert.AreEqual(15f, 4f.Wrap(5, 15));
        Assert.AreEqual(14f, 3f.Wrap(5, 15));
        Assert.AreEqual(8f, 8f.Wrap(5, 15));

        Assert.AreEqual(5, 16.Wrap(5, 15));
        Assert.AreEqual(6, 17.Wrap(5, 15));
        Assert.AreEqual(15, 4.Wrap(5, 15));
        Assert.AreEqual(14, 3.Wrap(5, 15));
        Assert.AreEqual(8, 8.Wrap(5, 15));
    }
}
