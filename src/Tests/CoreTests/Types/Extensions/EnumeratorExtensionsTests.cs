/*
EnumeratorExtensionsTests.cs

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
using TheXDS.MCART.Types.Extensions;
using Xunit;

namespace TheXDS.MCART.Tests.Types.Extensions
{
    public class EnumeratorExtensionsTests
    {
        [Fact]
        public void Skip_Test()
        {
            var a = Enumerable.Range(0, 10);
            using var e = a.GetEnumerator();
            e.MoveNext();
            Assert.Equal(0, e.Current);
            e.MoveNext();
            Assert.Equal(1, e.Current);
            Assert.Equal(5, e.Skip(5));
            Assert.Equal(6, e.Current);
            Assert.Equal(4, e.Skip(10));
            Assert.Equal(10, e.Current);

            Assert.Throws<ArgumentOutOfRangeException>(() => e.Skip(-1));
        }
    }
}