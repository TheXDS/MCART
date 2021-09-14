/*
CollectionExtensionsTests.cs

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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TheXDS.MCART.Exceptions;
using TheXDS.MCART.Types.Extensions;
using Xunit;

namespace TheXDS.MCART.Tests.Types.Extensions
{
    public class CollectionExtensionsTests
    {
        [Fact]
        public void RemoveOf_Test()
        {
            var e = new Exception();
            var d = DateTime.Now;
            var o = new object();
            var e2 = new InvalidOperationException();
            ICollection<object> c = new object[] { e, o, 5, d, e2 }.ToList();
            c.RemoveOf<object, Exception>();

            Assert.Contains(d, c);
            Assert.Contains(o, c);
            Assert.Contains(5, c);
            Assert.DoesNotContain(e, c);
            Assert.DoesNotContain(e2, c);
        }

        [Fact]
        public void RemoveAll_With_Predicate_Test()
        {
            var l = Enumerable.Range(1, 10).ToList();
            List<int> r = new();
            l.RemoveAll(p => p % 2 == 1, p => r.Add(p));
            Assert.Equal(new[]{ 2, 4, 6, 8, 10 }, l.ToArray());
            Assert.Equal(new[]{ 1, 3, 5, 7, 9 }, r.ToArray());
        }
        
        [Fact]
        public void RemoveAll_Without_Predicate_Test()
        {
            var l = Enumerable.Range(1, 10).ToList();
            List<int> r = new();
            l.RemoveAll(p => r.Add(p));
            Assert.Empty(l);
            Assert.Equal(new[]{ 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, r.ToArray());
        }
    }
}