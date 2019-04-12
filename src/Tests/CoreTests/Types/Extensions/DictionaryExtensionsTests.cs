/*
DictionaryExtensionsTests.cs

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

using static TheXDS.MCART.Types.Extensions.DictionaryExtensions;
using Xunit;
using System.Collections.Generic;

namespace TheXDS.MCART.Tests.Types.Extensions
{
    public class DictionaryExtensionsTests
    {
        [Fact]
        public void CheckCircularRefTest()
        {
            var d = new Dictionary<char, IEnumerable<char>>
            {
                { 'a', new[] { 'b', 'c' } },
                { 'b', new[] { 'c', 'd' } },
                { 'c', new[] { 'd', 'e' } },
            };

            Assert.False(d.CheckCircularRef('a'));

            d.Add('d', new[] { 'e', 'a' });

            Assert.True(d.CheckCircularRef('a'));
        }
    }
}
