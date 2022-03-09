/*
StatisticsTests.cs

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
using System.Linq;
using TheXDS.MCART.Math;
using NUnit.Framework;
using TheXDS.MCART.Helpers;
using static TheXDS.MCART.Math.Statistics;

namespace TheXDS.MCART.Tests.Math
{
    public class StatisticsTests
    {
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
}
