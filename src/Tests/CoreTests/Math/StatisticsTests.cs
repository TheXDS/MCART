/*
StatisticsTests.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2019 César Andrés Morgan

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

#pragma warning disable CS1591

using Xunit;
using System;
using static TheXDS.MCART.Math.Statistics;
using TheXDS.MCART.Math;

namespace TheXDS.MCART.Tests.Math
{
    public class StatisticsTests
    {
        [Fact]
        public void TendencyTest()
        {
            Assert.Equal(2.0, new double[] { 3, 5, 7 }.MeanTendency());
            Assert.Equal(0.0, new double[] { 3 }.MeanTendency());
            Assert.Equal(double.NaN, Array.Empty<double>().MeanTendency());
            Assert.Equal(0.0, new double[] { 3, 5, 3, 5, 3, 5, 3 }.MeanTendency());
        }

        [Fact]
        public void CorrelationTest()
        {
            Assert.Equal(1, Correlation(new double[] { 1, 2, 3 }, new double[] { 2, 4, 6 }));
            Assert.Equal(-1, Correlation(new double[] { 1, 2, 3 }, new double[] { 6, 4, 2 }));
            Assert.True(Correlation(new double[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, new double[] { 3, 6, 3, 6, 3, 6, 3, 6, 3, 6 }) < 0.25);
            Assert.Equal(double.NaN, Correlation(new double[] { 1, 2, 3 }, new double[] { 1, 1, 1 }));
            Assert.Throws<IndexOutOfRangeException>(() => Correlation(new double[] { 1 }, new double[] { 1, 2, 3 }));
            Assert.Throws<InvalidOperationException>(() => Correlation(Array.Empty<double>(), Array.Empty<double>()));
        }
    }
}
