/*
RandomExtensionsTests.cs

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

#pragma warning disable CS1591

using System.Collections.Generic;
using TheXDS.MCART.Types;
using Xunit;
using static TheXDS.MCART.Types.Extensions.RandomExtensions;

namespace TheXDS.MCART.Tests.Types.Extensions
{
    public class RandomExtensionsTests
    {
        [Fact]
        public void RndText_Test()
        {
            Assert.Equal(10, Assert.IsType<string>(Rnd.RndText(10)).Length);
        }

        [Fact]
        public void Next_With_Range_Test()
        {
            var r = new Range<int>(1, 100);
            for (var j = 0; j < 1000; j++)
            {
                Assert.InRange(Rnd.Next(r), 1, 100);
            }
        }

        [Fact]
        public void CoinFlip_Test()
        {
            List<bool> l = new();
            for (var j = 0; j < 1000; j++)
            {
                l.Add(Rnd.CoinFlip());
            }
            Assert.Contains(true, l);
            Assert.Contains(false, l);
        }
    }
}