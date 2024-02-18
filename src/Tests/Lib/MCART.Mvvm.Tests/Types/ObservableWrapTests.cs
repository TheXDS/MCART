/*
RangeTests.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2024 César Andrés Morgan

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
using System.Collections.Specialized;
using TheXDS.MCART.Types;
using static System.Collections.Specialized.NotifyCollectionChangedAction;

namespace TheXDS.MCART.Mvvm.Tests.Types;

public class ObservableWrapTests
{
    [Test]
    public void InstanceTest()
    {
        ObservableCollectionWrap<string>? o = new();
        Assert.That(o.UnderlyingCollection, Is.Null);
        List<string>? c = new() { "1", "2", "3" };
        o = new(c);
        Assert.That(c, Is.EqualTo(o.UnderlyingCollection));
        Assert.That(3, Is.EqualTo(o.Count));
        o = new ObservableCollectionWrap<string>();
        Assert.That(o, Is.Empty);
    }

    [Test]
    public void AddTest()
    {
        ObservableCollectionWrap<string>? c = new(new List<string> { "1", "2", "3" });
        EventTest(c, () => c.Add("4"), Add, out (object? Sender, NotifyCollectionChangedEventArgs Arguments) evt);
        Assert.That("4", Is.EqualTo((string)evt.Arguments.NewItems![0]!));
        Assert.That(c, Contains.Item("4"));
    }

    [Test]
    public void ClearTest()
    {
        ObservableCollectionWrap<string>? c = new(new List<string> { "1", "2", "3" });
        EventTest(c, c.Clear, Reset, out _);
        Assert.That(c, Is.Empty);
    }

    [Test]
    public void RemoveTest()
    {
        ObservableCollectionWrap<string>? c = new(new List<string> { "1", "2", "3" });
        EventTest(c, () => c.Remove("2"), Remove, out (object? Sender, NotifyCollectionChangedEventArgs Arguments) evt);
        Assert.That("2", Is.EqualTo((string)evt.Arguments.OldItems![0]!));
        Assert.That(c, Does.Not.Contain("2"));
    }

    [Test]
    public void RefreshTest()
    {
        List<string>? l = new() { "1", "2", "3" };
        ObservableCollectionWrap<string>? c = new(l);
        AssertionException? ex = Assert.Throws<AssertionException>(() => EventTest(c, () => l.Add("4"), Add, out _));
        Assert.That(c, Contains.Item("4"));
        EventTest(c, c.Refresh, Add, out _);
    }

    [Test]
    public void Replace_test() 
    {
        List<string>? l1 = new() { "1", "2", "3" };
        List<string>? l2 = new() { "4", "5", "6" };
        ObservableCollectionWrap<string>? c = new(l1);
        Assert.That(c.UnderlyingCollection, Is.EquivalentTo(l1.ToArray()));
        c.Replace(l2);
        Assert.That(c.UnderlyingCollection, Is.EquivalentTo(l2.ToArray()));
    }

    [Test]
    public void IsReadOnlyTest()
    {
        ObservableCollectionWrap<int> c = new();
        Assert.That(c.IsReadOnly);
        c = new(new List<int>());
        Assert.That(c.IsReadOnly, Is.False);
        c = new(Array.Empty<int>());
        Assert.That(c.IsReadOnly);
    }

    [Test]
    public void CountTest()
    {
        ObservableCollectionWrap<int> c = new();
        Assert.That(c.Count, Is.Zero);
        c = new(Array.Empty<int>());
        Assert.That(c.Count, Is.Zero);
        c = new(new int[] { 1, 2, 3, 4, 5 });
        Assert.That(c.Count, Is.Not.Zero);
    }

    [Test]
    public void Add_throws_if_null_underlying_collection()
    {
        ObservableCollectionWrap<int> c = new();
        Assert.Throws<InvalidOperationException>(() => c.Add(1));
    }

    [Test]
    public void Remove_returns_false_if_null_underlying_collection()
    {
        ObservableCollectionWrap<string> c = new();
        Assert.That(c.Remove("test"), Is.False);
    }

    [Test]
    public void Remove_returns_false_if_collection_did_not_contain_item()
    {
        ObservableCollectionWrap<string> c = new(new List<string>());
        Assert.That(c.Remove("test"), Is.False);
    }

    [Test]
    public void Remove_returns_true_if_collection_contains_item()
    {
        ObservableCollectionWrap<string> c = new(new List<string>(new[] { "test" }));
        Assert.That(c.Remove("test"));
    }

    [Test]
    public void GetEnumerator_is_not_null()
    {
        ObservableCollectionWrap<string> c = new();
        Assert.That(((IEnumerable<string>)c).GetEnumerator(),Is.Not.Null);
        Assert.That(((IEnumerable)c).GetEnumerator(), Is.Not.Null);
    }

    private static void EventTest<T>(ObservableCollectionWrap<T> c, Action action, NotifyCollectionChangedAction nAction, out (object? Sender, NotifyCollectionChangedEventArgs Arguments) evt)
    {
        (object? Sender, NotifyCollectionChangedEventArgs Arguments)? ev = null;
        void C_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) => ev = (sender, e);

        c.CollectionChanged += C_CollectionChanged;
        action();
        c.CollectionChanged -= C_CollectionChanged;
        evt = ev ?? default;

        Assert.That(evt, Is.Not.Null);
        Assert.That(ReferenceEquals(c, evt.Sender));
        Assert.That(nAction, Is.EqualTo(evt.Arguments.Action));
    }
}
