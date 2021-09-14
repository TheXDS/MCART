/*
ListExtensionsTests.cs

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
    public class ListExtensionsTests
    {
        [Fact]
        public void RemoveOf_Test()
        {
            var e = new Exception();
            var d = DateTime.Now;
            var o = new object();
            var e2 = new InvalidOperationException();
            var c = new object[] { e, o, 5, d, e2 }.ToList();
            c.RemoveOf<Exception>();

            Assert.Contains(d, c);
            Assert.Contains(o, c);
            Assert.Contains(5, c);
            Assert.DoesNotContain(e, c);
            Assert.DoesNotContain(e2, c);
        }

        [Fact]
        public void Shuffle_Test()
        {
            var l = Enumerable.Range(0, 100).ToList();
            l.Shuffle();
            Assert.False(l.All(p => p == l.FindIndexOf(p)));
            l = Enumerable.Range(0, 100).ToList();
            l.Shuffle(30, 59);
            Assert.All(l.Take(30), p => Assert.Equal(p, l.FindIndexOf(p)));
            Assert.False(l.Skip(30).Take(30).All(p => p == l.FindIndexOf(p)));
            Assert.All(l.Skip(60), p => Assert.Equal(p, l.FindIndexOf(p)));
        }

        [Fact]
        public void Shuffle_Contract_Test()
        {
            var l = Enumerable.Range(0, 100).ToList();
            Assert.IsType<EmptyCollectionException>(Assert.Throws<InvalidOperationException>(() => Array.Empty<int>().ToList().Shuffle()).InnerException);
            Assert.Throws<IndexOutOfRangeException>(() => l.Shuffle(-1, 100, 1));
            Assert.Throws<IndexOutOfRangeException>(() => l.Shuffle(0, 1000, 1));
            Assert.Equal("deepness", Assert.Throws<ArgumentOutOfRangeException>(() => l.Shuffle(10, 50, 1000)).ParamName);
            Assert.Throws<ArgumentException>(() => l.Shuffle(10, 0, 1));
        }

        [Fact]
        public void ApplyRotate_Test()
        {
            var l = Enumerable.Range(0, 10).ToList();
            l.ApplyRotate(-2);
            Assert.Equal(8, l[0]);
            Assert.Equal(9, l[1]);
            Assert.Equal(0, l[2]);
            Assert.Equal(1, l[3]);
            Assert.Equal(2, l[4]);
            Assert.Equal(3, l[5]);
            Assert.Equal(4, l[6]);
            Assert.Equal(5, l[7]);
            Assert.Equal(6, l[8]);
            Assert.Equal(7, l[9]);
            l.ApplyRotate(5);
            Assert.Equal(3, l[0]);
            Assert.Equal(4, l[1]);
            Assert.Equal(5, l[2]);
            Assert.Equal(6, l[3]);
            Assert.Equal(7, l[4]);
            Assert.Equal(8, l[5]);
            Assert.Equal(9, l[6]);
            Assert.Equal(0, l[7]);
            Assert.Equal(1, l[8]);
            Assert.Equal(2, l[9]);
        }
        
        [Fact]
        public void ApplyShift_Test()
        {
            var l = Enumerable.Range(0, 10).ToList();
            l.ApplyShift(-2);
            Assert.Equal(0, l[0]);
            Assert.Equal(0, l[1]);
            Assert.Equal(0, l[2]);
            Assert.Equal(1, l[3]);
            Assert.Equal(2, l[4]);
            Assert.Equal(3, l[5]);
            Assert.Equal(4, l[6]);
            Assert.Equal(5, l[7]);
            Assert.Equal(6, l[8]);
            Assert.Equal(7, l[9]);
            l.ApplyShift(5);
            Assert.Equal(3, l[0]);
            Assert.Equal(4, l[1]);
            Assert.Equal(5, l[2]);
            Assert.Equal(6, l[3]);
            Assert.Equal(7, l[4]);
            Assert.Equal(0, l[5]);
            Assert.Equal(0, l[6]);
            Assert.Equal(0, l[7]);
            Assert.Equal(0, l[8]);
            Assert.Equal(0, l[9]);
        }

        [Fact]
        public async Task Locked_Test()
        {
            IList l = new[] { 1, 2, 3, 4 }.ToList();
            var f1 = false;
            var t1 = Task.Run(() => l.Locked(i =>
            {
                Thread.Sleep(500);
                f1 = true;
                return Assert.IsType<List<int>>(i);
            }));
            // Este debe ser tiempo suficiente para que la tarea t1 inicie ejecución.
            await Task.Delay(250);
            var t2 = Task.Run(() => l.Locked(i =>
            {
                Assert.Same(l, i);
                Assert.True(f1);
            }));
            await Task.WhenAll(t1, t2);
        }

        [Fact]
        public void Swap_Test()
        {
            var l = new[] { "a", "b", "c", "d" }.ToList();
            l.Swap("b", "c");
            Assert.Equal(new[] { "a", "c", "b", "d" }, l.ToArray());
            Assert.ThrowsAny<Exception>(() => l.Swap("b", "e"));
        }
    }
}
