/*
SeriesTests.cs

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

namespace TheXDS.MCART.Tests.Math;
using NUnit.Framework;
using System.Linq;
using static TheXDS.MCART.Math.Series;

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
        Assert.AreEqual(b, a);
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
        Assert.AreEqual(b, a);
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
        Assert.AreEqual(b, a);
    }

    [Test]
    public void MakeSeriesAdditive_BreaksOnOverFlow_Test()
    {
        long[]? a = MakeSeriesAdditive(1, long.MaxValue).Take(4).ToArray();
        Assert.AreEqual(1, a[0]);
        Assert.AreEqual(long.MaxValue, a[1]);
        Assert.AreEqual(2, a.Length);
    }
}
