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

using NUnit.Framework;
using System.Collections.Generic;
using TheXDS.MCART.Helpers;
using TheXDS.MCART.Types;
using static TheXDS.MCART.Types.Extensions.RandomExtensions;

namespace TheXDS.MCART.Tests.Types.Extensions
{
    public class RandomExtensionsTests
    {
        [Test]
        public void RndText_Test()
        {
            string? str = Rnd.RndText(10);
            Assert.IsAssignableFrom<string>(str);
            Assert.AreEqual(10, str.Length);
        }

        [Test]
        public void Next_With_Range_Test()
        {
            Range<int> r = new(1, 100);
            for (int j = 0; j < 1000; j++)
            {
                Assert.True(Rnd.Next(r).IsBetween(1, 100));
            }
        }

        [Test]
        public void CoinFlip_Test()
        {
            List<bool> l = new();
            for (int j = 0; j < 1000; j++)
            {
                l.Add(Rnd.CoinFlip());
            }
            Assert.Contains(true, l);
            Assert.Contains(false, l);
        }
    }
}