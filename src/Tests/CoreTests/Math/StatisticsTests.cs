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
using TheXDS.MCART.Math;
using NUnit.Framework;
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
    }
}
