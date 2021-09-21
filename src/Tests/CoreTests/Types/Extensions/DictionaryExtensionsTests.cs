/*
DictionaryExtensionsTests.cs

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

using System.Collections.Generic;
using TheXDS.MCART.Types.Extensions;
using Xunit;

namespace TheXDS.MCART.Tests.Types.Extensions
{
    public class DictionaryExtensionsTests
    {
        [Fact]
        public void CheckCircularRef_Test()
        {
            var d = new Dictionary<char, IEnumerable<char>>
            {
                { 'a', new[] { 'b', 'c' } },
                { 'b', new[] { 'c', 'd' } },
                { 'c', new[] { 'd', 'e' } },
            };

            Assert.False(d.CheckCircularRef('a'));
            Assert.False(((IEnumerable<KeyValuePair<char, IEnumerable<char>>>)d).CheckCircularRef('a'));
            d.Add('d', new[] { 'e', 'a' });
            Assert.True(d.CheckCircularRef('a'));
            Assert.True(((IEnumerable<KeyValuePair<char, IEnumerable<char>>>)d).CheckCircularRef('a'));
        }

        [Fact]
        public void CheckCircularRef_Test2()
        {
            var d = new Dictionary<char, ICollection<char>>
            {
                { 'a', new[] { 'b', 'c' } },
                { 'b', new[] { 'c', 'd' } },
                { 'c', new[] { 'd', 'e' } },
            };

            Assert.False(d.CheckCircularRef('a'));
            Assert.False(((IEnumerable<KeyValuePair<char, ICollection<char>>>)d).CheckCircularRef('a'));
            d.Add('d', new[] { 'e', 'a' });
            Assert.True(((IEnumerable<KeyValuePair<char, ICollection<char>>>)d).CheckCircularRef('a'));
            Assert.True(d.CheckCircularRef('a'));
        }
        
        [Fact]
        public void Push_Test()
        {
            var d = new Dictionary<int, string>();
            Assert.IsType<string>(d.Push(1, "test"));
        }

        [Fact]
        public void Pop_Test()
        {
            var d = new Dictionary<int, string>
            {
                { 1, "test" },
                { 2, "test2" }
            };
            
            Assert.True(d.Pop(1, out var s));
            Assert.Equal("test", s);
            Assert.False(d.ContainsKey(1));
            Assert.False(d.ContainsValue("test"));
            Assert.False(d.Pop(3, out var s2));
            Assert.Null(s2);
        }
    }
}
