/*
EnumerableExtensionsTests.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2025 César Andrés Morgan

Permission is hereby granted, free of charge, to any person obtaining a copy of
this software and associated documentation files (the "Software"), to deal in
the Software without restriction, including without limitation the rights to
use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
of the Software, and to permit persons to whom the Software is furnished to do
so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using System.Collections;
using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using TheXDS.MCART.Exceptions;
using TheXDS.MCART.Types;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.Tests.Types.Extensions;

public class EnumerableExtensionsTests
{
    private static readonly string[] SelectAsyncInputArray = ["a", "B", "c"];
    private static readonly int[] ArrayFrom101To103 = [101, 102, 103];
    private static readonly int[] ArrayFrom121To123 = [121, 122, 123];

    [Test]
    public void Pick_Test()
    {
        int[] c = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];
        List<int> l = [];
        do
        {
            l.Add(c.Pick());
        } while (l.Count > 10);
        Assert.That(l, Is.Not.EquivalentTo(c));
    }

    [Test]
    public void Pick_contract_test()
    {
        Assert.Throws<InvalidOperationException>(() => Array.Empty<int>().Pick());
    }

    [Test]
    public void ItemsEqual_Test()
    {
        int[] c1 = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];
        int[] c2 = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];
        int[] c3 = [1, 2, 3, 11, 5, 6, 7, 8, 9, 10];
        Assert.That(c1.ItemsEqual(c2));
        Assert.That(c1.ItemsEqual(c3), Is.False);
    }

    [Test]
    public void IsAnyOf_Test()
    {
        object?[] objSet = ["Test", 1, new Exception(), Guid.NewGuid(), new bool[2]];
        Assert.That(objSet.IsAnyOf<string>());
        Assert.That(objSet.IsAnyOf<DayOfWeek>(), Is.False);
        Assert.That(objSet.IsAnyOf(typeof(int)));
        Assert.That(objSet.IsAnyOf(typeof(Stream)), Is.False);
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

        Assert.That(5, Is.EqualTo(await GetValues().CountAsync()));
    }

    [Test]
    public async Task Locked_Test()
    {
        int[] l = [1, 2, 3, 4];
        bool f1 = false;

        Task t1 = Task.Run(() => l.Locked(i =>
        {
            Thread.Sleep(500);
            f1 = true;
            Assert.That(i, Is.AssignableFrom<int[]>());
        }));

        // Wait for t1 to start execution.
        await Task.Delay(250);

        Task t2 = Task.Run(() => l.Locked(i =>
        {
            Assert.That(l, Is.SameAs(i));
            Assert.That(f1);
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
                Assert.That(j, Is.AssignableFrom<int>());
            }
        });
        Assert.That(3, Is.EqualTo(Enumerate().Locked(p => p.Count())));
    }

    [Test]
    public void Locked_Test3()
    {
        ICollection c = new[] { 1, 2, 3 };
        c.Locked(p =>
        {
            foreach (object? j in p)
            {
                Assert.That(j, Is.AssignableFrom<int>());
            }
        });
        Assert.That(3, Is.EqualTo(c.Locked(p => p.Count())));
    }

    [Test]
    public void Locked_Test4()
    {
        ICollection c = new ConcurrentBag<int> { 1, 2, 3 };
        c.Locked(p =>
        {
            foreach (object? j in p)
            {
                Assert.That(j, Is.AssignableFrom<int>());
            }
        });
        Assert.That(3, Is.EqualTo(c.Locked(p => p.Count())));
    }

    [Test]
    public async Task SelectAsync_Test()
    {
        await foreach (int j in SelectAsyncInputArray.SelectAsync(p => Task.FromResult(p.Length)))
        {
            Assert.That(1, Is.EqualTo(j));
        }
    }

    [Test]
    public void Shuffled_Test()
    {
        int[] a = [.. MCART.Helpers.Common.Sequence(1, 100)];
        int[] b = [.. a.Shuffled()];
        Assert.That(a.Length, Is.EqualTo(b.Length));
        foreach (int j in b)
        {
            Assert.That(a, Contains.Item(j));
        }
        Assert.That(a, Is.Not.EqualTo(b));
        Assert.That(a.All(p => a.FindIndexOf(p) == b.FindIndexOf(p)), Is.False);
    }

    [Test]
    public void Shuffled_WithRange_Test()
    {
        int[] a = [.. MCART.Helpers.Common.Sequence(1, 60)];
        int[] b = [.. a.Shuffled(20, 39)];
        Assert.That(a.Take(20).All(p => a.FindIndexOf(p) == b.FindIndexOf(p)));
        Assert.That(a.Skip(20).Take(20).All(p => a.FindIndexOf(p) == b.FindIndexOf(p)), Is.False);
        Assert.That(a.Skip(40).Take(20).All(p => a.FindIndexOf(p) == b.FindIndexOf(p)));
    }

    [Theory]
    [TestCase(new[] { "A", "b", "c", "" }, 0)]
    [TestCase(new[] { "", null, "a", null }, 2)]
    [TestCase(new string?[] { null, null, null }, 3)]
    public void NullCountTest(string?[] input, int count)
    {
        Assert.That(count, Is.EqualTo(input.NullCount()));
        Assert.Throws<ArgumentNullException>(() => ((object?[])null!).NullCount());
    }

    [Test]
    public void GroupByType_Test()
    {
        object?[] c =
        [
            1, 2,
            "Test", "Test2",
            null, null
        ];
        List<IGrouping<Type, object>> g = [.. c.GroupByType()];

        Assert.That(typeof(int), Is.EqualTo(g[0].Key));
        Assert.That(g[0].Contains(1));
        Assert.That(g[0].Contains(2));

        Assert.That(typeof(string), Is.EqualTo(g[1].Key));
        Assert.That(g[1].Contains("Test"));
        Assert.That(g[1].Contains("Test2"));

        Assert.That(2, Is.EqualTo(g.Count));
    }

    [Test]
    public void ToGeneric_Test()
    {
        IEnumerable e = new[] { "test", "test2" };
        Assert.That(e.ToGeneric(), Is.InstanceOf<IEnumerable<object?>>());
    }

    [Test]
    public void FirstOf_Test()
    {
        object?[] c = [
                "test",
                1.0,
                DayOfWeek.Monday,
                new Exception(),
                Guid.NewGuid()
            ];
        Assert.That(1.0, Is.EqualTo(c.FirstOf<double>()));
        Assert.That(c.FirstOf<Stream>(), Is.Null);
    }

    [Test]
    public void ContainsAll_Test()
    {
        int[] a = [.. MCART.Helpers.Common.Sequence(1, 100)];
        int[] b = [.. MCART.Helpers.Common.Sequence(50, 60)];
        Assert.That(a.ContainsAll(b));
        Assert.That(ArrayFrom101To103.ContainsAll(b), Is.False);

        Assert.That(a.ContainsAll(50, 51, 52));
        Assert.That(ArrayFrom101To103.ContainsAll(50, 51, 52), Is.False);
    }

    [Test]
    public void ContainsAny_Test()
    {
        int[] a = [.. MCART.Helpers.Common.Sequence(1, 100)];
        int[] b = [.. MCART.Helpers.Common.Sequence(95, 110)];
        Assert.That(a.ContainsAny(b));
        Assert.That(ArrayFrom101To103.ContainsAny(b));
        Assert.That(ArrayFrom121To123.ContainsAny(b), Is.False);

        Assert.That(ArrayFrom101To103.ContainsAny(95, 101, 110));
        Assert.That(ArrayFrom121To123.ContainsAny(95, 96, 110), Is.False);
    }

    [Test]
    public void FirstOf_WithType_Test()
    {
        Exception[] c =
        [
            new IndexOutOfRangeException(),
            new ArgumentNullException(),
            new StackOverflowException()
        ];
        Exception? e = c.FirstOf(typeof(ArgumentException));
        Assert.That(e, Is.Not.Null);
        Assert.That(e, Is.InstanceOf<ArgumentException>());
        Assert.That(c.FindIndexOf(e), Is.EqualTo(1));
        Assert.Throws<ArgumentNullException>(() => c.FirstOf(null!));
        Assert.Throws<InvalidTypeException>(() => c.FirstOf(typeof(int)));
    }

    [Test]
    public void OfType_Test()
    {
        object?[] c = [
                "test",
                1.0,
                DayOfWeek.Monday,
                2.0
            ];
        Assert.That(c.OfType(typeof(double)), Is.EquivalentTo(new object[] { 1.0, 2.0 }));
    }

    [Test]
    public void NotNull_Test()
    {
        Assert.That(((IEnumerable?)null).NotNull().ToGeneric().ToArray(), Is.Not.Null);
        Assert.That(((IEnumerable<int?>?)null).NotNull().ToGeneric().ToArray(), Is.Not.Null);
        Assert.That(((IEnumerable<Exception?>?)null).NotNull().ToGeneric().ToArray(), Is.Not.Null);
        Assert.That(new int?[] { 1, 2, null, 4 }.NotNull(), Is.EquivalentTo([1, 2, 4]));
        Assert.That(((IEnumerable)new int?[] { 1, 2, null, 4 }).NotNull(), Is.EquivalentTo([1, 2, 4]));
    }

    [Test]
    public void OrNull_Test()
    {
        Assert.That(new[] { "test" }.OrNull(), Is.Not.Null);
        Assert.That(Array.Empty<string>().OrNull(), Is.Null);
    }

    [Test]
    public void NonDefaults_Test()
    {
        Assert.That(new[] { 1, 2, 3 }.NonDefaults(), Is.EquivalentTo([1, 2, 3]));
        Assert.That(new[] { 0, 1, 0, 2, 3 }.NonDefaults(), Is.EquivalentTo([1, 2, 3]));
        Assert.That(new[] { "test", null }.NonDefaults(), Is.EquivalentTo(["test"]));
    }

    [Test]
    public void Copy_Test()
    {
        object o1 = new();
        object o2 = new();
        Assert.That(o1, Is.Not.SameAs(o2));

        object[] a = [o1, o2];
        object[] b = [.. a.Copy()];
        Assert.That(a, Is.Not.SameAs(b));

        Assert.That(a[0], Is.SameAs(b.First()));
        Assert.That(a[1], Is.SameAs(b.Last()));
    }

    [Test]
    public void Range_Test()
    {
        int[] c = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];

        Assert.That(c.Range(0, 3), Is.EquivalentTo([1, 2, 3]));
        Assert.That(c.Range(3, 3), Is.EquivalentTo([4, 5, 6]));
        Assert.That(c.Range(6, 3), Is.EquivalentTo([7, 8, 9]));
        Assert.That(c.Range(8, 3), Is.EquivalentTo([9, 10]));
        Assert.Throws<IndexOutOfRangeException>(() => _ = c.Range(99, 5).ToArray());
    }

    [Obsolete]
    [Test]
    public void ToExtendedList_Test()
    {
        ListEx<int> c = new[] { 1, 2, 3 }.ToExtendedList();

        Assert.That(c, Is.AssignableFrom<ListEx<int>>());
        Assert.That(3, Is.EqualTo(c.Count));
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

        Assert.That(1, Is.EqualTo("ABC".FindIndexOf('B')));
        Assert.That(1, Is.EqualTo("ABC".ToCharArray().AsEnumerable().FindIndexOf('B')));
        Assert.That(2, Is.EqualTo(new List<char>("ABC".ToCharArray()).FindIndexOf('C')));
        Assert.That(1, Is.EqualTo(EnumerateChars().FindIndexOf('B')));
        Assert.That(2, Is.EqualTo(EnumerateChars().FindIndexOf('C')));
        Assert.That(-1, Is.EqualTo(EnumerateChars().FindIndexOf('D')));
    }

    [Test]
    public void Shift_Test()
    {
        int[] c = [1, 2, 3, 4, 5];

        Assert.That(c.Shift(0), Is.EquivalentTo([1, 2, 3, 4, 5]));
        Assert.That(c.Shift(1), Is.EquivalentTo([2, 3, 4, 5, 0]));
        Assert.That(c.Shift(2), Is.EquivalentTo([3, 4, 5, 0, 0]));
        Assert.That(c.Shift(-1), Is.EquivalentTo([0, 1, 2, 3, 4]));
        Assert.That(c.Shift(-2), Is.EquivalentTo([0, 0, 1, 2, 3]));
    }

    [Test]
    public async Task PickAsync_Test()
    {
        int[] c = [1, 2, 3, 4, 5];
        HashSet<int> l = [];
        for (int j = 0; j < 100; j++)
        {
            int i = await c.PickAsync();
            Assert.That(c, Contains.Item(i));
            l.Add(i);
        }
        Assert.That(l.Distinct().Ordered(), Is.EquivalentTo(c));
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
    public void Count_Test(int arrSize)
    {
        IEnumerable<int> Enumerate()
        {
            for (int j = 0; j < arrSize; j++) yield return j;
        }

        int[] a = new int[arrSize];
        Assert.That(arrSize, Is.EqualTo(((IEnumerable)a).Count()));

        string s = new('x', arrSize);
        Assert.That(arrSize, Is.EqualTo(((IEnumerable)s).Count()));

        List<int> l = [.. a];
        Assert.That(arrSize, Is.EqualTo(((IEnumerable)l).Count()));

        Collection<int> c = [.. a];
        Assert.That(arrSize, Is.EqualTo(((IEnumerable)c).Count()));

        Assert.That(arrSize, Is.EqualTo(((IEnumerable)Enumerate()).Count()));

        Assert.Throws<ArgumentNullException>(() => ((IEnumerable)null!).Count());
    }

    [Test]
    public void AreAllEqual_Test()
    {
        int[] a = new int[10];
        Assert.That(a.AreAllEqual());
        a[4] = 1;
        Assert.That(a.AreAllEqual(), Is.False);
        a = [];
        Assert.That(Assert.Throws<EmptyCollectionException>(() => a.AreAllEqual())!.OffendingObject, Is.SameAs(a));
        Assert.That(new[] { "test", "1234", "abcd" }.AreAllEqual(p => p.Length));
    }

    [Test]
    public void Rotate_Test()
    {
        int[] c = [1, 2, 3, 4, 5];

        Assert.That(c.Rotate(0), Is.EquivalentTo([1, 2, 3, 4, 5]));
        Assert.That(c.Rotate(1), Is.EquivalentTo([2, 3, 4, 5, 1]));
        Assert.That(c.Rotate(2), Is.EquivalentTo([3, 4, 5, 1, 2]));
        Assert.That(c.Rotate(-1), Is.EquivalentTo([5, 1, 2, 3, 4]));
        Assert.That(c.Rotate(-2), Is.EquivalentTo([4, 5, 1, 2, 3]));
    }

    [Test]
    public void ExceptForTest_WithValues()
    {
        int[] array = [1, 2, 3, 4, 5];
        int[] exclusions = [2, 4];
        int[] result = [1, 3, 5];

        Assert.That(result, Is.EqualTo(array.ExceptFor(exclusions)));
    }

    [Test]
    public void ExceptForTest_WithObjects()
    {
        object[] array = new object[5];
        for (int j = 0; j < 5; j++) array[j] = new object();
        object[] exclusions = [.. array.Range(3, 2)];
        object[] result = [.. array.Range(0, 3)];

        Assert.That(result, Is.EqualTo(array.ExceptFor(exclusions).ToArray()));
    }

    [Test]
    public void IsPropertyEqual_Test()
    {
        Assert.That(new Exception[]
        {
                new("Test"),
                new("Test"),
                new("Test"),
        }.IsPropertyEqual(p => p.Message));
        Assert.That(new Exception[]
        {
                new("Test1"),
                new("Test2"),
                new("Test3"),
        }.IsPropertyEqual(p => p.Message), Is.False);
    }

    [Test]
    public void Sum_with_TimeSpan_Test()
    {
        TimeSpan[] times = [TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(2), TimeSpan.FromSeconds(3)];
        TimeSpan total = times.Sum();
        Assert.That(total, Is.EqualTo(TimeSpan.FromSeconds(6)));
    }

    [Test]
    public void Quorum_gets_quorum_on_all_equal()
    {
        int[] values = [1, 1, 1, 1, 1];
        Assert.That(values.Quorum(5), Is.EqualTo(1));
    }

    [Test]
    public void Quorum_gets_quorum_on_most_equal()
    {
        int[] values = [1, 1, 2, 2, 2, 2, 2, 3, 3];
        Assert.That(values.Quorum(5), Is.EqualTo(2));
    }

    [Test]
    public void Quorum_fails_on_no_quorum()
    {
        int[] values = [1, 2, 3, 4, 5];
        Assert.Throws<InvalidOperationException>(() => values.Quorum(2));
    }

    [Test]
    public void Quorum_fails_on_empty_collection()
    {
        int[] values = [];
        Assert.Throws<InvalidOperationException>(() => values.Quorum(1));
    }

    [Test]
    public void Quorum_contract_on_null_collection()
    {
        int[]? values = null;
        Assert.Throws<ArgumentNullException>(() => values!.Quorum(1));
    }

    [TestCase(0)]
    [TestCase(-1)]
    public void Quorum_contract_on_zero_or_negative_quorum_size(int quorumSize)
    {
        int[] values = [1, 2, 3, 4, 5];
        Assert.Throws<ArgumentOutOfRangeException>(() => values.Quorum(quorumSize));
    }
}
