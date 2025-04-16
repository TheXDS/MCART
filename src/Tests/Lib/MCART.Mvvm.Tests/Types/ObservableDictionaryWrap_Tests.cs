// ObservableDictionaryWrap_Tests.cs
//
// This file is part of Morgan's CLR Advanced Runtime (MCART)
//
// Author(s):
//      César Andrés Morgan <xds_xps_ivx@hotmail.com>
//
// Released under the MIT License (MIT)
// Copyright © 2011 - 2025 César Andrés Morgan
//
// Permission is hereby granted, free of charge, to any person obtaining a copy of
// this software and associated documentation files (the “Software”), to deal in
// the Software without restriction, including without limitation the rights to
// use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
// of the Software, and to permit persons to whom the Software is furnished to do
// so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using System.Collections.Specialized;
using TheXDS.MCART.Types;

namespace TheXDS.MCART.Mvvm.Tests.Types;

public class ObservableDictionaryWrap_Tests : ObservableCollectionTestsBase
{
    private static (Dictionary<int, string> Dictionary, ObservableDictionaryWrap<int, string> Wrap) GetTestWrap()
    {
        var dictionary = new Dictionary<int, string>()
        {
            { 1, "test 1" },
            { 2, "test 2" },
        };
        var wrap = new ObservableDictionaryWrap<int, string>(dictionary);
        return (dictionary, wrap);
    }

    [Test]
    public void Ctor_without_arguments_test()
    {
        var wrap = new ObservableDictionaryWrap<int, string>();
        Assert.That(wrap.UnderlyingCollection, Is.Null);
        Assert.That(wrap.Keys, Is.Empty);
        Assert.That(wrap.Values, Is.Empty);
    }

    [Test]
    public void Ctor_with_arguments_test()
    {
        var (dictionary, wrap) = GetTestWrap();
        Assert.That(wrap.UnderlyingCollection, Is.SameAs(dictionary));
        Assert.That(wrap.Keys, Is.EquivalentTo(new int[] { 1, 2 }));
        Assert.That(wrap.Values, Is.EquivalentTo(new string[] { "test 1", "test 2" }));
    }

    [Test]
    public void Indexer_get_test()
    {
        var (_, wrap) = GetTestWrap();
        Assert.That(wrap[1], Is.EqualTo("test 1"));
    }

    [Test]
    public void Indexer_set_fires_event_test()
    {
        var (_, wrap) = GetTestWrap();
        var eventArgs = TestCollectionChanged(wrap, p => p[1] = "test 3");

        Assert.That(wrap[1], Is.EqualTo("test 3"));
        Assert.That(eventArgs.Action, Is.EqualTo(NotifyCollectionChangedAction.Replace));
        Assert.That(eventArgs.OldItems, Is.EquivalentTo(new[] { new KeyValuePair<int, string>(1, "test 1") }));
        Assert.That(eventArgs.NewItems, Is.EquivalentTo(new[] { new KeyValuePair<int, string>(1, "test 3") }));
    }

    [Test]
    public void Add_fires_event_test()
    {
        var (_, wrap) = GetTestWrap();
        var eventArgs = TestCollectionChanged(wrap, p => p.Add(123, "test 123"));
        Assert.That(eventArgs.Action, Is.EqualTo(NotifyCollectionChangedAction.Add));
        Assert.That(eventArgs.NewItems, Is.EquivalentTo(new[] { new KeyValuePair<int, string>(123, "test 123") }));
    }

    [Test]
    public void Add_contract_test()
    {
        var wrap = new ObservableDictionaryWrap<int, string>();
        Assert.Throws<InvalidOperationException>(() => wrap.Add(99, "test 99"));
    }

    [Test]
    public void ContainsKey_test()
    {
        var (_, wrap) = GetTestWrap();
        Assert.That(wrap.ContainsKey(1));
        Assert.That(wrap.ContainsKey(9), Is.False);
    }

    [Test]
    public void ContainsKey_without_underlying_collection_test()
    {
        var wrap = new ObservableDictionaryWrap<int, string>();
        Assert.That(wrap.Contains(1), Is.False);
        Assert.That(wrap.Contains(9), Is.False);
    }

    [Test]
    public void Remove_fires_event_test()
    {
        var (_, wrap) = GetTestWrap();
        var eventArgs = TestCollectionChanged(wrap, p => Assert.That(p.Remove(1)));
        Assert.That(eventArgs.Action, Is.EqualTo(NotifyCollectionChangedAction.Remove));
        Assert.That(eventArgs.OldItems, Is.EquivalentTo(new[] { new KeyValuePair<int, string>(1, "test 1") }));
    }

    [Test]
    public void Indexer_edge_case_set_fires_event_test()
    {
        var dictionary = new Dictionary<string, byte[]>()
        {
            { "A", "aaa"u8.ToArray() },
            { "B", "bbb"u8.ToArray() },
        };
        var wrap = new ObservableDictionaryWrap<string, byte[]>(dictionary);

        var eventArgs = TestCollectionChanged(wrap, p => p["A"] = "test 3"u8.ToArray());
        Assert.That(wrap["A"], Is.EquivalentTo("test 3"u8.ToArray()));
        Assert.That(eventArgs.Action, Is.EqualTo(NotifyCollectionChangedAction.Replace));
        Assert.That(eventArgs.OldItems, Is.EquivalentTo(new[] { new KeyValuePair<string, byte[]>("A", "aaa"u8.ToArray()) }));
        Assert.That(eventArgs.NewItems, Is.EquivalentTo(new[] { new KeyValuePair<string, byte[]>("A", "test 3"u8.ToArray()) }));
    }

    [Test]
    public void Remove_without_underlying_collection_test()
    {
        var wrap = new ObservableDictionaryWrap<int, string>();
        Assert.Throws<InvalidOperationException>(() => wrap.Remove(1));
    }

    [Test]
    public void TryGetValue_test()
    {
        var (_, wrap) = GetTestWrap();
        Assert.That(wrap.TryGetValue(1, out var item));
        Assert.That(item, Is.EqualTo("test 1"));
        Assert.That(wrap.TryGetValue(456, out _), Is.False);
    }

    [Test]
    public void TryGetValue_without_underlying_collection_test()
    {
        var wrap = new ObservableDictionaryWrap<int, string>();
        Assert.That(wrap.TryGetValue(1, out var _), Is.False);
    }

    [Test]
    public void Indexer_set_contract_test()
    {
        var wrap = new ObservableDictionaryWrap<int, string>();
        Assert.Throws<InvalidOperationException>(() => wrap[99] = "test 99");
    }
}
