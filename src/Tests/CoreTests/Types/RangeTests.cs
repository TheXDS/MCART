/*
RangeTests.cs

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
using TheXDS.MCART.Types;
using NUnit.Framework;

namespace TheXDS.MCART.Tests.Types
{
    public class RangeTests
    {
#if CLSCompliance
        [CLSCompliant(false)]
#endif
        [TestCase("1 - 5")]
        [TestCase("1 5")]
        [TestCase("1, 5")]
        [TestCase("1;5")]
        [TestCase("1:5")]
        [TestCase("1|5")]
        [TestCase("1..5")]
        [TestCase("1...5")]
        [TestCase("1; 5")]
        [TestCase("1 : 5")]
        [TestCase("1 | 5")]
        [TestCase("1,5")]
        [TestCase("1 .. 5")]
        [TestCase("1 => 5")]
        [TestCase("1 -> 5")]
        [TestCase("1=>5")]
        [TestCase("1->5")]
        public void TryParseTest(string testArg)
        {
            Assert.True(Range<int>.TryParse(testArg, out Range<int> range));
            Assert.AreEqual(1, range.Minimum);
            Assert.AreEqual(5, range.Maximum);
        }

        [Test]
        public void TryParseTest_FailingToParse()
        {
            Assert.False(Range<int>.TryParse("TEST", out _));
        }

        [Test]
        public void JoinTest()
        {
            Range<int> a = new(1, 5);
            Range<int> b = new(3, 8);
            Range<int> r = a.Join(b);

            Assert.AreEqual(1, r.Minimum);
            Assert.AreEqual(8, r.Maximum);
        }

        [Test]
        public void IntersectTest()
        {
            Range<int> a = new(1, 5);
            Range<int> b = new(3, 8);
            Range<int> r = a.Intersect(b);

            Assert.AreEqual(3, r.Minimum);
            Assert.AreEqual(5, r.Maximum);
        }

#if CLSCompliance
        [CLSCompliant(false)]
#endif
        [TestCase(1, 3, 2, 5, true, false)]
        [TestCase(1, 2, 3, 4, false, false)]
        [TestCase(1, 2, 2, 3, false, false)]
        [TestCase(1, 2, 2, 3, true, true)]
        public void IntersectsTest(int min1, int max1, int min2, int max2, bool expected, bool inclusively)
        {
            Range<int> a = new(min1, max1, inclusively);
            Range<int> b = new(min2, max2, inclusively);

            Assert.AreEqual(expected, a.Intersects(b));
        }
    }
}