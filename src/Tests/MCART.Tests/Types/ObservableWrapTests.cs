/*
RangeTests.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2022 César Andrés Morgan

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

namespace TheXDS.MCART.Tests.Types;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using TheXDS.MCART.Types;
using static System.Collections.Specialized.NotifyCollectionChangedAction;

public class ObservableWrapTests
{
    [Test]
    public void InstanceTest()
    {
        ObservableCollectionWrap<string>? o = new();
        Assert.IsNull(o.UnderlyingCollection);
        List<string>? c = new() { "1", "2", "3" };
        o = new(c);
        Assert.AreEqual(c, o.UnderlyingCollection);
        Assert.AreEqual(3, o.Count);
        o = new ObservableCollectionWrap<string>();
        Assert.IsEmpty(o);
    }

    [Test]
    public void AddTest()
    {
        ObservableCollectionWrap<string>? c = new(new List<string> { "1", "2", "3" });
        EventTest(c, () => c.Add("4"), Add, out (object? Sender, NotifyCollectionChangedEventArgs Arguments) evt);
        Assert.AreEqual("4", (string)evt.Arguments.NewItems![0]!);
        Assert.Contains("4", c.ToArray());
    }

    [Test]
    public void ClearTest()
    {
        ObservableCollectionWrap<string>? c = new(new List<string> { "1", "2", "3" });
        EventTest(c, c.Clear, Reset, out _);
        Assert.IsEmpty(c);
    }

    [Test]
    public void RemoveTest()
    {
        ObservableCollectionWrap<string>? c = new(new List<string> { "1", "2", "3" });
        EventTest(c, () => c.Remove("2"), Remove, out (object? Sender, NotifyCollectionChangedEventArgs Arguments) evt);
        Assert.AreEqual("2", (string)evt.Arguments.OldItems![0]!);
        Assert.False(c.Contains("2"));
    }

    [Test]
    public void RefreshTest()
    {
        List<string>? l = new() { "1", "2", "3" };
        ObservableCollectionWrap<string>? c = new(l);
        AssertionException? ex = Assert.Throws<AssertionException>(() => EventTest(c, () => l.Add("4"), Add, out _));
        Assert.Contains("4", c.ToArray());
        EventTest(c, c.Refresh, Add, out _);
    }

    [Test]
    public void IsReadOnlyTest()
    {
        ObservableCollectionWrap<int> c = new();
        Assert.IsTrue(c.IsReadOnly);
        c = new(new List<int>());
        Assert.IsFalse(c.IsReadOnly);
        c = new(Array.Empty<int>());
        Assert.IsTrue(c.IsReadOnly);
    }

    [Test]
    public void CountTest()
    {
        ObservableCollectionWrap<int> c = new();
        Assert.Zero(c.Count);
        c = new(Array.Empty<int>());
        Assert.Zero(c.Count);
        c = new(new int[] { 1, 2, 3, 4, 5 });
        Assert.NotZero(c.Count);
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
        Assert.IsFalse(c.Remove("test"));
    }

    [Test]
    public void Remove_returns_false_if_collection_didnt_contain_item()
    {
        ObservableCollectionWrap<string> c = new(new List<string>());
        Assert.IsFalse(c.Remove("test"));
    }

    [Test]
    public void Remove_returns_true_if_collection_contains_item()
    {
        ObservableCollectionWrap<string> c = new(new List<string>(new[] { "test" }));
        Assert.IsTrue(c.Remove("test"));
    }

    [Test]
    public void GetEnumerator_is_not_null()
    {
        ObservableCollectionWrap<string> c = new();
        Assert.NotNull(((IEnumerable<string>)c).GetEnumerator());
        Assert.NotNull(((IEnumerable)c).GetEnumerator());
    }

    private static void EventTest<T>(ObservableCollectionWrap<T> c, Action action, NotifyCollectionChangedAction nAction, out (object? Sender, NotifyCollectionChangedEventArgs Arguments) evt)
    {
        (object? Sender, NotifyCollectionChangedEventArgs Arguments)? ev = null;
        void C_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) => ev = (sender, e);

        c.CollectionChanged += C_CollectionChanged;
        action();
        c.CollectionChanged -= C_CollectionChanged;
        evt = ev ?? default;

        Assert.NotNull(evt);
        Assert.True(ReferenceEquals(c, evt.Sender));
        Assert.AreEqual(nAction, evt.Arguments.Action);
    }

}
