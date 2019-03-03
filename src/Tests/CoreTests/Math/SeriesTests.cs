/*
SeriesTests.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright (c) 2011 - 2019 César Andrés Morgan

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

using System.Linq;
using Xunit;
using static TheXDS.MCART.Math.Series;

namespace Common.Math
{
    public class SeriesTests
    {
        [Fact]
        public void FibonacciTest()
        {
            var a = Fibonacci().Take(16).ToArray();
            var b = new long[]
            {
                0,   1,   1,   2,
                3,   5,   8,   13,
                21,  34,  55,  89,
                144, 233, 377, 610
            };
            Assert.Equal(b, a);
        }
    }
}
