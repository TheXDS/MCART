// ObservableListWrap_T_Tests.cs
//
// This file is part of Morgan's CLR Advanced Runtime (MCART)
//
// Author(s):
//      César Andrés Morgan <xds_xps_ivx@hotmail.com>
//
// Released under the MIT License (MIT)
// Copyright © 2011 - 2023 César Andrés Morgan
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

using NUnit.Framework;
using System.Collections.Specialized;
using TheXDS.MCART.Types;

namespace TheXDS.MCART.Mvvm.Tests.Types;

public class ObservableListWrap_T_Tests : ObservableCollectionTestsBase<ObservableListWrap<string>>
{
    private static (List<string>, ObservableListWrap<string>) GetTestWrap()
    {
        var list = new List<string>()
        {
            "test 1",
            "test 2",
        };
        var wrap = new ObservableListWrap<string>(list);
        return (list, wrap);
    }

    [Test]
    public void Ctor_without_arguments_test()
    {
        var wrap = new ObservableListWrap<string>();
        Assert.That(wrap.UnderlyingCollection, Is.Null);
    }

    [Test]
    public void Ctor_with_arguments_test()
    {
        var (dictionary, wrap) = GetTestWrap();
        Assert.That(wrap.UnderlyingCollection, Is.SameAs(dictionary));
    }

    [Test]
    public void Indexer_get_test()
    {
        var (_, wrap) = GetTestWrap();
        Assert.That(wrap[0], Is.EqualTo("test 1"));
    }

    [Test]
    public void Indexer_set_fires_event_test()
    {
        var (_, wrap) = GetTestWrap();
        var eventArgs = TestCollectionChanged(wrap, p => p[0] = "test 3");

        Assert.That(wrap[0], Is.EqualTo("test 3"));
        Assert.That(eventArgs.Action, Is.EqualTo(NotifyCollectionChangedAction.Replace));
        Assert.That(eventArgs.OldItems, Is.EquivalentTo(new[] { "test 1" }));
        Assert.That(eventArgs.NewItems, Is.EquivalentTo(new[] { "test 3" }));
    }

    [Test]
    public void IndexOf_test()
    {
        var (_, wrap) = GetTestWrap();
        Assert.That(wrap.IndexOf("test 2"), Is.EqualTo(1));
    }

    [Test]
    public void IndexOf_without_underlying_collection_test()
    {
        var wrap = new ObservableListWrap<string>();
        Assert.That(wrap.IndexOf("test 2"), Is.EqualTo(-1));
    }

    [Test]
    public void Contains_test()
    {
        var (_, wrap) = GetTestWrap();
        Assert.That(wrap.Contains("test 2"));
    }

    [Test]
    public void Contains_without_underlying_collection_test()
    {
        var wrap = new ObservableListWrap<string>();
        Assert.That(wrap.Contains("test 2"), Is.False);
    }

    [Test]
    public void Insert_fires_event_test()
    {
        var (_, wrap) = GetTestWrap();
        wrap.PropertyChanged += (_, e) => Assert.That(e.PropertyName, Is.EqualTo("Count"));
        var eventArgs = TestCollectionChanged(wrap, p => p.Insert(0, "test 123"));
        Assert.That(wrap[0], Is.EqualTo("test 123"));
        Assert.That(wrap[1], Is.EqualTo("test 1"));
        Assert.That(eventArgs.Action, Is.EqualTo(NotifyCollectionChangedAction.Add));
        Assert.That(eventArgs.NewItems, Is.EquivalentTo(new[] { "test 123" }));
    }

    [Test]
    public void Insert_contract_test()
    {
        var wrap = new ObservableListWrap<string>();
        Assert.Throws<InvalidOperationException>(() => wrap.Insert(0, "test 99"));
    }

    [Test]
    public void RemoveAt_fires_event_test()
    {
        var (_, wrap) = GetTestWrap();
        wrap.PropertyChanged += (_, e) => Assert.That(e.PropertyName, Is.EqualTo("Count"));
        var eventArgs = TestCollectionChanged(wrap, p => p.RemoveAt(1));
        Assert.That(eventArgs.Action, Is.EqualTo(NotifyCollectionChangedAction.Remove));
        Assert.That(eventArgs.OldItems, Is.EquivalentTo(new[] { "test 2" }));
    }

    [Test]
    public void RemoveAt_contract_test()
    {
        var wrap = new ObservableListWrap<string>();
        Assert.Throws<InvalidOperationException>(() => wrap.RemoveAt(0));
    }
}
