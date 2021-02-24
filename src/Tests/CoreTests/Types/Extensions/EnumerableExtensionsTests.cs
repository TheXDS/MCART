/*
EnumerableExtensionsTests.cs

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
using System.Threading;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using TheXDS.MCART.Exceptions;
using Xunit;
using static TheXDS.MCART.Types.Extensions.EnumerableExtensions;

namespace TheXDS.MCART.Tests.Types.Extensions
{
    public class EnumerableExtensionsTests
    {
        [Fact]
        public void IsAnyOfTest()
        {
            object?[] objset = { "Test", 1, new Exception(), Guid.NewGuid(), new bool[2] };
            Assert.True(objset.IsAnyOf<string>());
            Assert.False(objset.IsAnyOf<DayOfWeek>());
            Assert.True(objset.IsAnyOf(typeof(int)));
            Assert.False(objset.IsAnyOf(typeof(System.IO.Stream)));
        }

        [Fact]
        public Task LockedTest()
        {
            int[] l = { 1, 2, 3, 4 };
            bool f1 = false;

            var t1 = Task.Run(() => l.Locked(i =>
            {
                Thread.Sleep(500);
                f1 = true;
                return Assert.IsType<int[]>(i);
            }));
            var t2 = Task.Run(() => l.Locked(i =>
            {
                Assert.True(f1);
            }));
            return Task.WhenAll(t1, t2);
        }

        [Fact]
        public async Task SelectAsyncTest()
        {
            await foreach (var j in new string[] { "a", "B", "c" }.SelectAsync(p => Task.FromResult(p.Length)))
            {
                Assert.Equal(1, j);
            }
        }

        [Theory]
        [CLSCompliant(false)]
        [InlineData(new string?[] { "A", "b", "c", ""}, 0)]
        [InlineData(new string?[] { "", null, "a", null}, 2)]
        [InlineData(new string?[] { null, null, null }, 3)]
        public void NullCountTest(string?[] input, int count)
        {
            Assert.Equal(count, input.NullCount());
            Assert.Throws<ArgumentNullException>(() => ((object?[])null!).NullCount());
        }

        [Fact]
        public void GroupByTypeTest()
        {
            var c = new object?[]
            {
                1, 2,
                "Test", "Test2",
                null, null
            };
            var g = c.GroupByType().ToList();

            Assert.Equal(typeof(int), g[0].Key);
            Assert.Contains(1, g[0]);
            Assert.Contains(2, g[0]);

            Assert.Equal(typeof(string), g[1].Key);
            Assert.Contains("Test", g[1]);
            Assert.Contains("Test2", g[1]);

            Assert.Equal(2, g.Count);
        }

        [Fact]
        public void ToGenericTest()
        {
            IEnumerable e = new string[] { "test", "test2" };
            Assert.IsAssignableFrom<IEnumerable<object?>>(e.ToGeneric());
        }

        [Fact]
        public void FirstOfTest()
        {
            object?[] c = {
                "test",
                1.0,
                DayOfWeek.Monday,
                new Exception(),
                Guid.NewGuid()
            };
            Assert.Equal(1.0, c.FirstOf<double>());
            Assert.Null(c.FirstOf<System.IO.Stream>());
        }

        [Fact]
        public void FirstOf_WithType_Test()
        {
            Exception[] c =
            {
                new IndexOutOfRangeException(),                
                new ArgumentNullException(),
                new StackOverflowException()
            };
            Assert.NotNull(Assert.IsAssignableFrom<ArgumentException>(c.FirstOf(typeof(ArgumentException))));
            Assert.Throws<ArgumentNullException>(() => c.FirstOf(null!));
            Assert.Throws<InvalidTypeException>(() => c.FirstOf(typeof(int)));
        }

        [Fact]
        public void OfType_Test()
        {
            object?[] c = {
                "test",
                1.0,
                DayOfWeek.Monday,
                2.0
            };
            Assert.Equal(new object[] { 1.0, 2.0 }, c.OfType(typeof(double)).ToArray());
        }

        [Fact]
        public void NotNullTest()
        {
            Assert.NotNull(((IEnumerable?)null).NotNull().ToGeneric().ToArray());
            Assert.NotNull(((IEnumerable<int?>?)null).NotNull().ToGeneric().ToArray());
            Assert.NotNull(((IEnumerable<Exception?>?)null).NotNull().ToGeneric().ToArray());
            Assert.Equal(new int[] { 1, 2, 4 }, new int?[] { 1, 2, null, 4 }.NotNull());
        }

        [Fact]
        public void OrNullTest()
        {
            Assert.NotNull(new[] { "test" }.OrNull());
            Assert.Null(Array.Empty<string>().OrNull());
        }

        [Fact]
        public void NonDefaults()
        {
            Assert.Equal(new[] { 1, 2, 3 }, new[] { 1, 2, 3 }.NonDefaults());
            Assert.Equal(new[] { 1, 2, 3 }, new[] { 0, 1, 0, 2, 3 }.NonDefaults());
            Assert.Equal(new[] { "test" }, new[] { "test", null }.NonDefaults());
        }

        [Fact]
        public void CopyTest()
        {
            var o1 = new object();
            var o2 = new object();
            Assert.NotSame(o1, o2);

            var a = new[] { o1, o2 };
            var b = a.Copy();
            Assert.NotSame(a, b);

            Assert.Same(a[0], b.First());
            Assert.Same(a[1], b.Last());
        }

        [Fact]
        public void RangeTest()
        {
            var c = new [] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10};

            Assert.Equal(new[] { 1, 2, 3 }, c.Range(0, 3));
            Assert.Equal(new[] { 4, 5, 6 }, c.Range(3, 3));
            Assert.Equal(new[] { 7, 8, 9 }, c.Range(6, 3));
        }

        [Fact]
        public void ToExtendedListTest()
        {
            var c = new[] {1, 2, 3}.ToExtendedList();

            Assert.IsType<MCART.Types.ListEx<int>>(c);
            Assert.Equal(3, c.Count);
        }

        [Fact]
        public void ShiftTest()
        {
            var c = new[] { 1, 2, 3, 4, 5 };

            Assert.Equal(new[] { 1, 2, 3, 4, 5 }, c.Shift(0).ToArray());
            Assert.Equal(new[] { 2, 3, 4, 5, 0 }, c.Shift(1).ToArray());
            Assert.Equal(new[] { 3, 4, 5, 0, 0 }, c.Shift(2).ToArray());
            Assert.Equal(new[] { 0, 1, 2, 3, 4 }, c.Shift(-1).ToArray());
            Assert.Equal(new[] { 0, 0, 1, 2, 3 }, c.Shift(-2).ToArray());
        }

        [Fact]
        public void RotateTest()
        {
            var c = new[] { 1, 2, 3, 4, 5 };

            Assert.Equal(new[] { 1, 2, 3, 4, 5 }, c.Rotate(0).ToArray());
            Assert.Equal(new[] { 2, 3, 4, 5, 1 }, c.Rotate(1).ToArray());
            Assert.Equal(new[] { 3, 4, 5, 1, 2 }, c.Rotate(2).ToArray());
            Assert.Equal(new[] { 5, 1, 2, 3, 4 }, c.Rotate(-1).ToArray());
            Assert.Equal(new[] { 4, 5, 1, 2, 3 }, c.Rotate(-2).ToArray());
        }
        [Fact]
        public void ExceptForTest_WithValues()
        {
            var array = new[] {1, 2, 3, 4, 5};
            var exclusions = new[] {2, 4};
            var result = new[] {1, 3, 5};

            Assert.Equal(result, array.ExceptFor(exclusions));
        }
        [Fact]
        public void ExceptForTest_WithObjects()
        {
            var array = new object[5];
            for(var j = 0; j < 5; j++) array[j] = new object();
            var exclusions = array.Range(3,2).ToArray();
            var result = array.Range(0,3).ToArray();

            Assert.Equal(result, array.ExceptFor(exclusions).ToArray());
        }
    }
}
