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

using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TheXDS.MCART.Exceptions;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.Tests.Types.Extensions;

public class EnumerableExtensionsTests
{
    [Test]
    public void Pick_Test()
    {
        int[] c = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        List<int> l = new();
        do
        {
            l.Add(c.Pick());
        } while (l.Count > 10);
        Assert.AreNotEqual(c, l.ToArray());
    }

    [Test]
    public void ItemsEqual_Test()
    {
        int[] c1 = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        int[] c2 = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        int[] c3 = { 1, 2, 3, 11, 5, 6, 7, 8, 9, 10 };
        Assert.True(c1.ItemsEqual(c2));
        Assert.False(c1.ItemsEqual(c3));
    }

    [Test]
    public void IsAnyOf_Test()
    {
        object?[] objset = { "Test", 1, new Exception(), Guid.NewGuid(), new bool[2] };
        Assert.True(objset.IsAnyOf<string>());
        Assert.False(objset.IsAnyOf<DayOfWeek>());
        Assert.True(objset.IsAnyOf(typeof(int)));
        Assert.False(objset.IsAnyOf(typeof(System.IO.Stream)));
    }

    [Test]
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

        Assert.AreEqual(5, await GetValues().CountAsync());
    }

    [Test]
    public async Task Locked_Test()
    {
        int[] l = { 1, 2, 3, 4 };
        bool f1 = false;

        Task t1 = Task.Run(() => l.Locked(i =>
        {
            Thread.Sleep(500);
            f1 = true;
            Assert.IsAssignableFrom<int[]>(i);
        }));

        // Este debe ser tiempo suficiente para que la tarea t1 inicie ejecución.
        await Task.Delay(250);

        Task t2 = Task.Run(() => l.Locked(i =>
        {
            Assert.AreSame(l, i);
            Assert.True(f1);
        }));
        await Task.WhenAll(t1, t2);
    }

    [Test]
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
            foreach (object? j in p)
            {
                Assert.IsAssignableFrom<int>(j);
            }
        });
        Assert.AreEqual(3, Enumerate().Locked(p => p.Count()));
    }

    [Test]
    public async Task SelectAsync_Test()
    {
        await foreach (int j in new[] { "a", "B", "c" }.SelectAsync(p => Task.FromResult(p.Length)))
        {
            Assert.AreEqual(1, j);
        }
    }

    [Test]
    public void Shuffled_Test()
    {
        int[] a = MCART.Helpers.Common.Sequence(1, 100).ToArray();
        int[] b = a.Shuffled().ToArray();
        Assert.AreEqual(a.Length, b.Length);
        foreach (int j in b)
        {
            Assert.Contains(j, a);
        }
        Assert.AreNotEqual(a, b);
        Assert.False(a.All(p => a.FindIndexOf(p) == b.FindIndexOf(p)));
    }

    [Test]
    public void Shuffled_WithRange_Test()
    {
        int[] a = MCART.Helpers.Common.Sequence(1, 60).ToArray();
        int[] b = a.Shuffled(20, 39).ToArray();
        Assert.True(a.Take(20).All(p => a.FindIndexOf(p) == b.FindIndexOf(p)));
        Assert.False(a.Skip(20).Take(20).All(p => a.FindIndexOf(p) == b.FindIndexOf(p)));
        Assert.True(a.Skip(40).Take(20).All(p => a.FindIndexOf(p) == b.FindIndexOf(p)));
    }

    [Theory]
    [CLSCompliant(false)]
    [TestCase(new[] { "A", "b", "c", "" }, 0)]
    [TestCase(new[] { "", null, "a", null }, 2)]
    [TestCase(new string?[] { null, null, null }, 3)]
    public void NullCountTest(string?[] input, int count)
    {
        Assert.AreEqual(count, input.NullCount());
        Assert.Throws<ArgumentNullException>(() => ((object?[])null!).NullCount());
    }

    [Test]
    public void GroupByType_Test()
    {
        object?[] c =
        {
            1, 2,
            "Test", "Test2",
            null, null
        };
        List<IGrouping<Type, object>> g = c.GroupByType().ToList();

        Assert.AreEqual(typeof(int), g[0].Key);
        Assert.True(g[0].Contains(1));
        Assert.True(g[0].Contains(2));

        Assert.AreEqual(typeof(string), g[1].Key);
        Assert.True(g[1].Contains("Test"));
        Assert.True(g[1].Contains("Test2"));

        Assert.AreEqual(2, g.Count);
    }

    [Test]
    public void ToGeneric_Test()
    {
        IEnumerable e = new[] { "test", "test2" };
        Assert.IsInstanceOf<IEnumerable<object?>>(e.ToGeneric());
    }

    [Test]
    public void FirstOf_Test()
    {
        object?[] c = {
                "test",
                1.0,
                DayOfWeek.Monday,
                new Exception(),
                Guid.NewGuid()
            };
        Assert.AreEqual(1.0, c.FirstOf<double>());
        Assert.Null(c.FirstOf<System.IO.Stream>());
    }

    [Test]
    public void ContainsAll_Test()
    {
        int[] a = MCART.Helpers.Common.Sequence(1, 100).ToArray();
        int[] b = MCART.Helpers.Common.Sequence(50, 60).ToArray();
        Assert.True(a.ContainsAll(b));
        Assert.False(new[] { 101, 102, 103 }.ContainsAll(b));

        Assert.True(a.ContainsAll(50, 51, 52));
        Assert.False(new[] { 101, 102, 103 }.ContainsAll(50, 51, 52));
    }

    [Test]
    public void ContainsAny_Test()
    {
        int[] a = MCART.Helpers.Common.Sequence(1, 100).ToArray();
        int[] b = MCART.Helpers.Common.Sequence(95, 110).ToArray();
        Assert.True(a.ContainsAny(b));
        Assert.True(new[] { 101, 102, 103 }.ContainsAny(b));
        Assert.False(new[] { 121, 122, 123 }.ContainsAny(b));

        Assert.True(new[] { 101, 102, 103 }.ContainsAny(95, 101, 110));
        Assert.False(new[] { 121, 122, 123 }.ContainsAny(95, 96, 110));
    }

    [Test]
    public void FirstOf_WithType_Test()
    {
        Exception[] c =
        {
                new IndexOutOfRangeException(),
                new ArgumentNullException(),
                new StackOverflowException()
            };
        Exception? e = c.FirstOf(typeof(ArgumentException));
        Assert.NotNull(e);
        Assert.IsInstanceOf<ArgumentException>(e);
        Assert.Throws<ArgumentNullException>(() => c.FirstOf(null!));
        Assert.Throws<InvalidTypeException>(() => c.FirstOf(typeof(int)));
    }

    [Test]
    public void OfType_Test()
    {
        object?[] c = {
                "test",
                1.0,
                DayOfWeek.Monday,
                2.0
            };
        Assert.AreEqual(new object[] { 1.0, 2.0 }, c.OfType(typeof(double)).ToArray());
    }

    [Test]
    public void NotNull_Test()
    {
        Assert.NotNull(((IEnumerable?)null).NotNull().ToGeneric().ToArray());
        Assert.NotNull(((IEnumerable<int?>?)null).NotNull().ToGeneric().ToArray());
        Assert.NotNull(((IEnumerable<Exception?>?)null).NotNull().ToGeneric().ToArray());
        Assert.AreEqual(new[] { 1, 2, 4 }, new int?[] { 1, 2, null, 4 }.NotNull());
        Assert.AreEqual(new[] { 1, 2, 4 }, ((IEnumerable)new int?[] { 1, 2, null, 4 }).NotNull());
    }

    [Test]
    public void OrNull_Test()
    {
        Assert.NotNull(new[] { "test" }.OrNull());
        Assert.Null(Array.Empty<string>().OrNull());
    }

    [Test]
    public void NonDefaults_Test()
    {
        Assert.AreEqual(new[] { 1, 2, 3 }, new[] { 1, 2, 3 }.NonDefaults());
        Assert.AreEqual(new[] { 1, 2, 3 }, new[] { 0, 1, 0, 2, 3 }.NonDefaults());
        Assert.AreEqual(new[] { "test" }, new[] { "test", null }.NonDefaults());
    }

    [Test]
    public void Copy_Test()
    {
        object o1 = new();
        object o2 = new();
        Assert.AreNotSame(o1, o2);

        object[] a = { o1, o2 };
        object[] b = a.Copy().ToArray();
        Assert.AreNotSame(a, b);

        Assert.AreSame(a[0], b.First());
        Assert.AreSame(a[1], b.Last());
    }

    [Test]
    public void Range_Test()
    {
        int[] c = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        Assert.AreEqual(new[] { 1, 2, 3 }, c.Range(0, 3));
        Assert.AreEqual(new[] { 4, 5, 6 }, c.Range(3, 3));
        Assert.AreEqual(new[] { 7, 8, 9 }, c.Range(6, 3));
        Assert.AreEqual(new[] { 9, 10 }, c.Range(8, 3));
        Assert.Throws<IndexOutOfRangeException>(() => c.Range(99, 5).ToArray());
    }

    [Test]
    public void ToExtendedList_Test()
    {
        MCART.Types.ListEx<int> c = new[] { 1, 2, 3 }.ToExtendedList();

        Assert.IsAssignableFrom<MCART.Types.ListEx<int>>(c);
        Assert.AreEqual(3, c.Count);
    }

    [Test]
    public void FindIndexOf_Test()
    {
        static IEnumerable<char> EnumerateChars()
        {
            yield return 'A';
            yield return 'B';
            yield return 'C';
        }

        Assert.AreEqual(1, "ABC".FindIndexOf('B'));
        Assert.AreEqual(1, "ABC".ToCharArray().AsEnumerable().FindIndexOf('B'));
        Assert.AreEqual(2, new List<char>("ABC".ToCharArray()).FindIndexOf('C'));
        Assert.AreEqual(1, EnumerateChars().FindIndexOf('B'));
        Assert.AreEqual(2, EnumerateChars().FindIndexOf('C'));
        Assert.AreEqual(-1, EnumerateChars().FindIndexOf('D'));
    }

    [Test]
    public void Shift_Test()
    {
        int[] c = { 1, 2, 3, 4, 5 };

        Assert.AreEqual(new[] { 1, 2, 3, 4, 5 }, c.Shift(0).ToArray());
        Assert.AreEqual(new[] { 2, 3, 4, 5, 0 }, c.Shift(1).ToArray());
        Assert.AreEqual(new[] { 3, 4, 5, 0, 0 }, c.Shift(2).ToArray());
        Assert.AreEqual(new[] { 0, 1, 2, 3, 4 }, c.Shift(-1).ToArray());
        Assert.AreEqual(new[] { 0, 0, 1, 2, 3 }, c.Shift(-2).ToArray());
    }

    [Test]
    public async Task PickAsync_Test()
    {
        int[] c = { 1, 2, 3, 4, 5 };
        HashSet<int> l = new();
        for (int j = 0; j < 100; j++)
        {
            int i = await c.PickAsync();
            Assert.Contains(i, c);
            l.Add(i);
        }
        Assert.AreEqual(c, l.Distinct().Ordered().ToArray());
    }

    [Test]
    public void PickAsync_Contract_Test()
    {
        Assert.ThrowsAsync<InvalidOperationException>(async () => await Array.Empty<int>().PickAsync());
    }

    [Theory]
    [TestCase(3)]
    [TestCase(5)]
    [TestCase(7)]
    [TestCase(9)]
    [CLSCompliant(false)]
    public void Count_Test(int arrSize)
    {
        IEnumerable<int> Enumerate()
        {
            for (int j = 0; j < arrSize; j++) yield return j;
        }

        int[] a = new int[arrSize];
        Assert.AreEqual(arrSize, ((IEnumerable)a).Count());

        string s = new('x', arrSize);
        Assert.AreEqual(arrSize, ((IEnumerable)s).Count());

        List<int> l = new(a);
        Assert.AreEqual(arrSize, ((IEnumerable)l).Count());

        Collection<int> c = new(a);
        Assert.AreEqual(arrSize, ((IEnumerable)c).Count());

        Assert.AreEqual(arrSize, ((IEnumerable)Enumerate()).Count());

        Assert.Throws<ArgumentNullException>(() => ((IEnumerable)null!).Count());
    }

    [Test]
    public void AreAllEqual_Test()
    {
        int[] a = new int[10];
        Assert.True(a.AreAllEqual());
        a[4] = 1;
        Assert.False(a.AreAllEqual());
        a = Array.Empty<int>();
        Assert.AreSame(a, Assert.Throws<EmptyCollectionException>(() => a.AreAllEqual())!.OffendingObject);
        Assert.True(new[] { "test", "1234", "abcd" }.AreAllEqual(p => p.Length));
    }

    [Test]
    public void Rotate_Test()
    {
        int[] c = { 1, 2, 3, 4, 5 };

        Assert.AreEqual(new[] { 1, 2, 3, 4, 5 }, c.Rotate(0).ToArray());
        Assert.AreEqual(new[] { 2, 3, 4, 5, 1 }, c.Rotate(1).ToArray());
        Assert.AreEqual(new[] { 3, 4, 5, 1, 2 }, c.Rotate(2).ToArray());
        Assert.AreEqual(new[] { 5, 1, 2, 3, 4 }, c.Rotate(-1).ToArray());
        Assert.AreEqual(new[] { 4, 5, 1, 2, 3 }, c.Rotate(-2).ToArray());
    }

    [Test]
    public void ExceptForTest_WithValues()
    {
        int[] array = { 1, 2, 3, 4, 5 };
        int[] exclusions = { 2, 4 };
        int[] result = { 1, 3, 5 };

        Assert.AreEqual(result, array.ExceptFor(exclusions));
    }

    [Test]
    public void ExceptForTest_WithObjects()
    {
        object[] array = new object[5];
        for (int j = 0; j < 5; j++) array[j] = new object();
        object[] exclusions = array.Range(3, 2).ToArray();
        object[] result = array.Range(0, 3).ToArray();

        Assert.AreEqual(result, array.ExceptFor(exclusions).ToArray());
    }

    [Test]
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
