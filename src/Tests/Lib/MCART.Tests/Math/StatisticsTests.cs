/*
StatisticsTests.cs

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
using System.Linq;
using TheXDS.MCART.Helpers;
using TheXDS.MCART.Math;
using static TheXDS.MCART.Math.Statistics;

public class StatisticsTests
{
    [TestCase(100, 10, 100, 100)]
    [TestCase(100, 1, 50, 75)]
    [TestCase(100, 2, 50, 66.66666667)]
    [TestCase(100, 3, 80, 85)]
    [TestCase(100, 19, 80, 81)]
    public void Average_Test(double value, int samples, double oldAverage, double expectedAverage)
    {
        const double epsilon = 0.00001;
        Assert.IsTrue((value.Average(samples, oldAverage) - expectedAverage).IsBetween(-epsilon, epsilon));
    }

    [Test]
    public void GeometricMean_Test()
    {
        var d = new double[] { 2, 4, 6, 8, 10 }.GeometricMean();
        Assert.True(d.IsBetween(5.2103421, 5.2103422));
    }
    
    [Test]
    public void HarmonicMean_Test()
    {
        var d = new double[] { 2, 4, 6, 8, 10 }.HarmonicMean();
        Assert.True(d.IsBetween(4.3795, 4.3797));
    }
    
    [Test]
    public void Median_Test()
    {
        var d = new [] { 2.5, 4.75, 6.11, 9.14 }.Median();
        Assert.AreEqual(3.625 , d);
    }
    
    [Test]
    public void Mode_Test()
    {
        var d = new [] { 2.5, 4.75, 6.11, 9.14, 2.5, 4.75 }.Mode();
        Assert.AreEqual(new [] { 2.5, 4.75 } , d);
    }

    [Test]
    public void TendencyTest()
    {
        Assert.AreEqual(2.0, new double[] { 3, 5, 7 }.MeanTendency());
        Assert.AreEqual(0.0, new double[] { 3 }.MeanTendency());
        Assert.AreEqual(double.NaN, Array.Empty<double>().MeanTendency());
        Assert.AreEqual(0.0, new double[] { 3, 5, 3, 5, 3, 5, 3 }.MeanTendency());
    }

    [Test]
    public void CorrelationTest()
    {
        Assert.AreEqual(1, Correlation(new double[] { 1, 2, 3 }, new double[] { 2, 4, 6 }));
        Assert.AreEqual(-1, Correlation(new double[] { 1, 2, 3 }, new double[] { 6, 4, 2 }));
        Assert.True(Correlation(new double[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, new double[] { 3, 6, 3, 6, 3, 6, 3, 6, 3, 6 }) < 0.25);
        Assert.AreEqual(double.NaN, Correlation(new double[] { 1, 2, 3 }, new double[] { 1, 1, 1 }));
        Assert.Throws<IndexOutOfRangeException>(() => Correlation(new double[] { 1 }, new double[] { 1, 2, 3 }));
        Assert.Throws<InvalidOperationException>(() => Correlation(Array.Empty<double>(), Array.Empty<double>()));
    }

    [Test]
    public void Forecast_Test()
    {
        double[] a = TheXDS.MCART.Helpers.Common.Sequence(0,100).Select(p => (double)p).ToArray();
        double[] b = TheXDS.MCART.Helpers.Common.Sequence(0,200,2).Select(p => (double)p).ToArray();
        Assert.AreEqual(400, Forecast(200, a, b));
    }

    [Test]
    public void Forecast_Contract_Test()
    {
        double[] a = TheXDS.MCART.Helpers.Common.Sequence(0,10).Select(p => (double)p).ToArray();
        double[] b = TheXDS.MCART.Helpers.Common.Sequence(0,3).Select(p => (double)p).ToArray();
        Assert.Throws<IndexOutOfRangeException>(() => Forecast(1, a, b));
    }

    [Test]
    public void Variate_Test()
    {
        double x = 100.0.Variate(5.0);
        Assert.True(x.IsBetween(95.0, 105.0));
        Assert.AreNotEqual(100.0, x);
    }

    [Test]
    public void Normalize_Test()
    {
        double[] a = {10, 15, 20};
        var b = a.Normalize(2).ToArray();
        Assert.AreEqual(8,b[0].Minimum);
        Assert.AreEqual(12,b[0].Maximum);
        Assert.AreEqual(13,b[1].Minimum);
        Assert.AreEqual(17,b[1].Maximum);
        Assert.AreEqual(18,b[2].Minimum);
        Assert.AreEqual(22,b[2].Maximum);
    }
}
