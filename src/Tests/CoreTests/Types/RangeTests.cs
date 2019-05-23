/*
RangeTests.cs

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

using TheXDS.MCART.Types;
using Xunit;

namespace TheXDS.MCART.Tests.Types
{
    public class RangeTests
    {
        [Theory]
        [InlineData("1 - 5")]
        [InlineData("1 5")]
        [InlineData("1, 5")]
        [InlineData("1;5")]
        [InlineData("1:5")]
        [InlineData("1|5")]
        [InlineData("1..5")]
        [InlineData("1...5")]
        [InlineData("1 to 5")]
        [InlineData("1 a 5")]
        public void TryParseTest(string testArg)
        {
            Assert.True(Range<int>.TryParse(testArg, out var range));
            Assert.Equal(1, range.Minimum);
            Assert.Equal(5, range.Maximum);
        }

        [Fact]
        public void TryParseTest_FailingToParse()
        {
            Assert.False(Range<int>.TryParse("TEST", out _));
        }

        [Fact]
        public void JoinTest()
        {
            var a = new Range<int>(1, 5);
            var b = new Range<int>(3, 8);
            var r = a.Join(b);

            Assert.Equal(1, r.Minimum);
            Assert.Equal(8, r.Maximum);
        }

        [Fact]
        public void IntersectTest()
        {
            var a = new Range<int>(1, 5);
            var b = new Range<int>(3, 8);
            var r = a.Intersect(b);

            Assert.Equal(3, r.Minimum);
            Assert.Equal(5, r.Maximum);
        }

        [Theory]
        [InlineData(1, 3, 2, 5, true, false)]
        [InlineData(1, 2, 3, 4, false, false)]
        [InlineData(1, 2, 2, 3, false, false)]
        [InlineData(1, 2, 2, 3, true, true)]
        public void IntersectsTest(int min1, int max1, int min2, int max2, bool expected, bool inclusively)
        {
            var a = new Range<int>(min1, max1, inclusively);
            var b = new Range<int>(min2, max2, inclusively);
            
            Assert.Equal(expected, a.Intersects(b));
        }
    }
}