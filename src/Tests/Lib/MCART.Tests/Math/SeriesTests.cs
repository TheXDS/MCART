/*
SeriesTests.cs

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

using static TheXDS.MCART.Math.Series;

namespace TheXDS.MCART.Tests.Math;

public class SeriesTests
{
    [Test]
    public void FibonacciTest()
    {
        long[]? a = Fibonacci().Take(16).ToArray();
        long[]? b = new long[]
        {
            0,   1,   1,   2,
            3,   5,   8,   13,
            21,  34,  55,  89,
            144, 233, 377, 610
        };
        Assert.That(b, Is.EqualTo(a));
    }

    [Test]
    public void LucasTest()
    {
        long[]? a = Lucas().Take(16).ToArray();
        long[]? b = new long[]
        {
            2,   1,   3,   4,
            7,   11,  18,  29,
            47,  76,  123, 199,
            322, 521, 843, 1364
        };
        Assert.That(b, Is.EqualTo(a));
    }

    [Test]
    public void MakeSeriesAdditiveTest()
    {
        long[]? a = MakeSeriesAdditive(0, 1).Take(16).ToArray();
        long[]? b = new long[]
        {
            0,   1,   1,   2,
            3,   5,   8,   13,
            21,  34,  55,  89,
            144, 233, 377, 610
        };
        Assert.That(b, Is.EqualTo(a));
    }

    [Test]
    public void MakeSeriesAdditive_BreaksOnOverFlow_Test()
    {
        long[]? a = MakeSeriesAdditive(1, long.MaxValue).Take(4).ToArray();
        Assert.That(1, Is.EqualTo(a[0]));
        Assert.That(long.MaxValue, Is.EqualTo(a[1]));
        Assert.That(2, Is.EqualTo(a.Length));
    }
}
