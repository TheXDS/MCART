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
using System.Collections.ObjectModel;
using TheXDS.MCART.Exceptions;
using TheXDS.MCART.Types.Extensions;
using Xunit;
using static TheXDS.MCART.Types.Extensions.EnumerableExtensions;

namespace TheXDS.MCART.Tests.Types.Extensions
{
    public class EnumerableExtensionsTests
    {
        [Fact]
        public void Pick_Test()
        {
            var c = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            var l = new List<int>();
            do
            {
                l.Add(c.Pick());
            } while (l.Count > 10);
            Assert.NotEqual(c,l.ToArray());
        }

        [Fact]
        public void ItemsEqual_Test()
        {
            var c1 = new[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10};
            var c2 = new[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10};
            var c3 = new[] {1, 2, 3, 11, 5, 6, 7, 8, 9, 10};
            Assert.True(c1.ItemsEqual(c2));
            Assert.False(c1.ItemsEqual(c3));
        }
        
        [Fact]
        public void IsAnyOf_Test()
        {
            object?[] objset = { "Test", 1, new Exception(), Guid.NewGuid(), new bool[2] };
            Assert.True(objset.IsAnyOf<string>());
            Assert.False(objset.IsAnyOf<DayOfWeek>());
            Assert.True(objset.IsAnyOf(typeof(int)));
            Assert.False(objset.IsAnyOf(typeof(System.IO.Stream)));
        }

        [Fact]
        public async Task CountAsync_Test()
        {
            static async IAsyncEnumerable<int> GetValues()
            {
                yield return 1;
                await Task.CompletedTask;
                yield return 2;
                await Task.CompletedTask;
                yield return 3;
                await Task.CompletedTask;
                yield return 4;
                await Task.CompletedTask;
                yield return 5;
            }
            
            Assert.Equal(5, await GetValues().CountAsync());
        }

        [Fact]
        public async Task Locked_Test()
        {
            int[] l = { 1, 2, 3, 4 };
            var f1 = false;

            var t1 = Task.Run(() => l.Locked(i =>
            {
                Thread.Sleep(500);
                f1 = true;
                return Assert.IsType<int[]>(i);
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
        public void Locked_Test2()
        {
            static IEnumerable Enumerate()
            {
                yield return 1;
                yield return 2;
                yield return 3;
            }

            Enumerate().Locked(p =>
            {
                foreach (var j in p)
                {
                    Assert.IsType<int>(j);
                }
            });
            Assert.Equal(3,Enumerate().Locked(p => p.Count()));
        }
        
        [Fact]
        public async Task SelectAsync_Test()
        {
            await foreach (var j in new[] { "a", "B", "c" }.SelectAsync(p => Task.FromResult(p.Length)))
            {
                Assert.Equal(1, j);
            }
        }

        [Fact]
        public void Shuffled_Test()
        {
            var a = TheXDS.MCART.Helpers.Common.Sequence(1, 100).ToArray();
            var b = a.Shuffled().ToArray();
            Assert.Equal(a.Length, b.Length);
            foreach (var j in b)
            {
                Assert.Contains(j, a);
            }
            Assert.NotEqual(a, b);
            Assert.False(a.All(p => a.FindIndexOf(p) == b.FindIndexOf(p)));
        }

        [Fact]
        public void Shuffled_WithRange_Test()
        {
            var a = TheXDS.MCART.Helpers.Common.Sequence(1, 60).ToArray();
            var b = a.Shuffled(20, 39).ToArray();
            Assert.True(a.Take(20).All(p => a.FindIndexOf(p) == b.FindIndexOf(p)));
            Assert.False(a.Skip(20).Take(20).All(p => a.FindIndexOf(p) == b.FindIndexOf(p)));
            Assert.True(a.Skip(40).Take(20).All(p => a.FindIndexOf(p) == b.FindIndexOf(p)));
        }

        [Theory]
        [CLSCompliant(false)]
        [InlineData(new[] { "A", "b", "c", ""}, 0)]
        [InlineData(new[] { "", null, "a", null}, 2)]
        [InlineData(new string?[] { null, null, null }, 3)]
        public void NullCountTest(string?[] input, int count)
        {
            Assert.Equal(count, input.NullCount());
            Assert.Throws<ArgumentNullException>(() => ((object?[])null!).NullCount());
        }

        [Fact]
        public void GroupByType_Test()
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
        public void ToGeneric_Test()
        {
            IEnumerable e = new[] { "test", "test2" };
            Assert.IsAssignableFrom<IEnumerable<object?>>(e.ToGeneric());
        }

        [Fact]
        public void FirstOf_Test()
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
        public void ContainsAll_Test()
        {
            var a = TheXDS.MCART.Helpers.Common.Sequence(1, 100).ToArray();
            var b = TheXDS.MCART.Helpers.Common.Sequence(50, 60).ToArray();
            Assert.True(a.ContainsAll(b));
            Assert.False(new[] {101, 102, 103}.ContainsAll(b));
            
            Assert.True(a.ContainsAll(50, 51, 52));
            Assert.False(new[] {101, 102, 103}.ContainsAll(50, 51, 52));
        }
        
        [Fact]
        public void ContainsAny_Test()
        {
            var a = TheXDS.MCART.Helpers.Common.Sequence(1, 100).ToArray();
            var b = TheXDS.MCART.Helpers.Common.Sequence(95, 110).ToArray();
            Assert.True(a.ContainsAny(b));
            Assert.True(new[] {101, 102, 103}.ContainsAny(b));
            Assert.False(new[] {121, 122, 123}.ContainsAny(b));
            
            Assert.True(new[] {101, 102, 103}.ContainsAny(95, 101, 110));
            Assert.False(new[] {121, 122, 123}.ContainsAny(95, 96, 110));
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
        public void NotNull_Test()
        {
            Assert.NotNull(((IEnumerable?)null).NotNull().ToGeneric().ToArray());
            Assert.NotNull(((IEnumerable<int?>?)null).NotNull().ToGeneric().ToArray());
            Assert.NotNull(((IEnumerable<Exception?>?)null).NotNull().ToGeneric().ToArray());
            Assert.Equal(new[] { 1, 2, 4 }, new int?[] { 1, 2, null, 4 }.NotNull());
            Assert.Equal(new[] { 1, 2, 4 }, ((IEnumerable)new int?[] { 1, 2, null, 4 }).NotNull());
        }

        [Fact]
        public void OrNull_Test()
        {
            Assert.NotNull(new[] { "test" }.OrNull());
            Assert.Null(Array.Empty<string>().OrNull());
        }

        [Fact]
        public void NonDefaults_Test()
        {
            Assert.Equal(new[] { 1, 2, 3 }, new[] { 1, 2, 3 }.NonDefaults());
            Assert.Equal(new[] { 1, 2, 3 }, new[] { 0, 1, 0, 2, 3 }.NonDefaults());
            Assert.Equal(new[] { "test" }, new[] { "test", null }.NonDefaults());
        }

        [Fact]
        public void Copy_Test()
        {
            var o1 = new object();
            var o2 = new object();
            Assert.NotSame(o1, o2);

            var a = new[] { o1, o2 };
            var b = a.Copy().ToArray();
            Assert.NotSame(a, b);

            Assert.Same(a[0], b.First());
            Assert.Same(a[1], b.Last());
        }

        [Fact]
        public void Range_Test()
        {
            var c = new [] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10};

            Assert.Equal(new[] { 1, 2, 3 }, c.Range(0, 3));
            Assert.Equal(new[] { 4, 5, 6 }, c.Range(3, 3));
            Assert.Equal(new[] { 7, 8, 9 }, c.Range(6, 3));
            Assert.Equal(new[] { 9, 10 }, c.Range(8, 3));
            Assert.Throws<IndexOutOfRangeException>(() => c.Range(99, 5).ToArray());
        }

        [Fact]
        public void ToExtendedList_Test()
        {
            var c = new[] {1, 2, 3}.ToExtendedList();

            Assert.IsType<MCART.Types.ListEx<int>>(c);
            Assert.Equal(3, c.Count);
        }

        [Fact]
        public void FindIndexOf_Test()
        {
            static IEnumerable<char> EnumerateChars()
            {
                yield return 'A';
                yield return 'B';
                yield return 'C';
            }
            
            Assert.Equal(1, "ABC".FindIndexOf('B'));
            Assert.Equal(1, "ABC".ToCharArray().AsEnumerable().FindIndexOf('B'));
            Assert.Equal(2, new List<char>("ABC".ToCharArray()).FindIndexOf('C'));
            Assert.Equal(1, EnumerateChars().FindIndexOf('B'));
            Assert.Equal(2, EnumerateChars().FindIndexOf('C'));
            Assert.Equal(-1, EnumerateChars().FindIndexOf('D'));
        }

        [Fact]
        public void Shift_Test()
        {
            var c = new[] { 1, 2, 3, 4, 5 };

            Assert.Equal(new[] { 1, 2, 3, 4, 5 }, c.Shift(0).ToArray());
            Assert.Equal(new[] { 2, 3, 4, 5, 0 }, c.Shift(1).ToArray());
            Assert.Equal(new[] { 3, 4, 5, 0, 0 }, c.Shift(2).ToArray());
            Assert.Equal(new[] { 0, 1, 2, 3, 4 }, c.Shift(-1).ToArray());
            Assert.Equal(new[] { 0, 0, 1, 2, 3 }, c.Shift(-2).ToArray());
        }

        [Theory]
        [InlineData(3)]
        [InlineData(5)]
        [InlineData(7)]
        [InlineData(9)]
#if CLSCompliance
        [CLSCompliant(false)]
#endif
        public void Count_Test(int arrSize)
        {
            IEnumerable<int> Enumerate()
            {
                for (var j = 0; j < arrSize; j++) yield return j;
            }
            
            var a = new int[arrSize];
            Assert.Equal(arrSize, ((IEnumerable)a).Count());

            var s = new string('x', arrSize);
            Assert.Equal(arrSize, ((IEnumerable)s).Count());

            var l = new List<int>(a);
            Assert.Equal(arrSize, ((IEnumerable)l).Count());
            
            var c = new Collection<int>(a);
            Assert.Equal(arrSize, ((IEnumerable)c).Count());
            
            Assert.Equal(arrSize, ((IEnumerable)Enumerate()).Count());

            Assert.Throws<ArgumentNullException>(() => ((IEnumerable)null!).Count());
        }

        [Fact]
        public void AreAllEqual_Test()
        {
            var a = new int[10];
            Assert.True(a.AreAllEqual());
            a[4] = 1;
            Assert.False(a.AreAllEqual());
            a = Array.Empty<int>();
            Assert.Same(a, Assert.Throws<EmptyCollectionException>(() => a.AreAllEqual()).OffendingObject);
            Assert.True(new []{"test", "1234", "abcd"}.AreAllEqual(p => p.Length));
        }
        
        [Fact]
        public void Rotate_Test()
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

        [Fact]
        public void IsPropertyEqual_Test()
        {
            Assert.True(new Exception[]
            {
                new("Test"),
                new("Test"),
                new("Test"),
            }.IsPropertyEqual(p => p.Message));
            Assert.False(new Exception[]
            {
                new("Test1"),
                new("Test2"),
                new("Test3"),
            }.IsPropertyEqual(p => p.Message));
        }
    }
}
