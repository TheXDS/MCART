/*
TweenTests.cs

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
using System;
using static TheXDS.MCART.Math.Tween;

public class TweenTests
{
    [TestCase(0.0, 0.0)]
    [TestCase(0.25, 0.25)]
    [TestCase(0.5, 0.5)]
    [TestCase(0.75, 0.75)]
    [TestCase(1.0, 1.0)]
    public void LinearTest(in double input, in double output)
    {
        Assert.AreEqual(output, Linear(input));
    }

    [TestCase(0.0, 0.0)]
    [TestCase(0.25, 0.1)]
    [TestCase(0.5, 0.5)]
    [TestCase(0.75, 0.9)]
    [TestCase(1.0, 1.0)]
    public void QuadraticTest(in double input, in double output)
    {
        Assert.AreEqual(output, Quadratic(input));
    }

    [TestCase(0.0, 0.0)]
    [TestCase(0.25, 0.035714285714)]
    [TestCase(0.5, 0.5)]
    [TestCase(0.75, 0.964285714286)]
    [TestCase(1.0, 1.0)]
    public void CubicTest(in double input, in double output)
    {
        Assert.AreEqual(output, Math.Round(Cubic(input), 12));
    }

    [TestCase(0.0, 0.0)]
    [TestCase(0.25, 0.68359375)]
    [TestCase(0.5, 0.9375)]
    [TestCase(0.75, 0.99609375)]
    [TestCase(1.0, 1.0)]
    public void QuarticTest(in double input, in double output)
    {
        Assert.AreEqual(output, Math.Round(Quartic(input), 12));
    }

    [TestCase(0.0, 0.0)]
    [TestCase(0.1, 1.987688340595)]
    [TestCase(0.2, 0.048943483705)]
    [TestCase(0.3, 1.891006524188)]
    [TestCase(0.4, 0.190983005625)]
    [TestCase(0.5, 1.707106781187)]
    [TestCase(0.6, 0.412214747708)]
    [TestCase(0.7, 1.453990499740)]
    [TestCase(0.8, 0.690983005625)]
    [TestCase(0.9, 1.156434465040)]
    [TestCase(1.0, 1.0)]
    public void ShakyTest(in double input, in double output)
    {
        Assert.AreEqual(output, Math.Round(Shaky(input), 12));
    }

    [TestCase(0.0, 0.0)]
    [TestCase(0.1, 1.250105790668)]
    [TestCase(0.2, 0.817765433958)]
    [TestCase(0.3, 1.139719345509)]
    [TestCase(0.4, 0.891779529237)]
    [TestCase(0.5, 1.082995956795)]
    [TestCase(0.6, 0.938142705985)]
    [TestCase(0.7, 1.043605092429)]
    [TestCase(0.8, 0.972492472466)]
    [TestCase(0.9, 1.013083718634)]
    [TestCase(1.0, 1.0)]
    public void BouncyTest(in double input, in double output)
    {
        Assert.AreEqual(output, Math.Round(Bouncy(input), 12));
    }
}
