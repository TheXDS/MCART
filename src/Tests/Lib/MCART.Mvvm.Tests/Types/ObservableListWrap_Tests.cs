// ObservableListWrap_Tests.cs
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
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using TheXDS.MCART.Tests;
using TheXDS.MCART.Types;
using TheXDS.MCART.Types.Base;
using static TheXDS.MCART.Tests.EventTestHelpers;

namespace TheXDS.MCART.Mvvm.Tests.Types;

public class ObservableListWrap_Tests : ObservableCollectionTestsBase<ObservableListWrap>
{
    private class RefreshableTest : IRefreshable
    {
        public bool IsRefreshed { get; set; }

        void IRefreshable.Refresh() => IsRefreshed = true;
    }

    private static (ArrayList, ObservableListWrap) GetTestWrap()
    {
        var list = new ArrayList()
        {
            "test 1",
            "test 2",
        };
        var wrap = new ObservableListWrap(list);
        return (list, wrap);
    }

    [Test]
    public void Ctor_without_arguments_test()
    {
        var wrap = new ObservableListWrap();
        Assert.That(wrap.UnderlyingList, Is.Null);
    }

    [Test]
    public void Ctor_with_arguments_test()
    {
        var (dictionary, wrap) = GetTestWrap();
        Assert.That(wrap.UnderlyingList, Is.SameAs(dictionary));
    }

    [Test]
    public void Indexer_get_test()
    {
        var (_, wrap) = GetTestWrap();
        Assert.That(wrap[0], Is.EqualTo("test 1"));
    }

    [Test]
    public void Indexer_without_underlying_list_test()
    {
        var wrap = new ObservableListWrap();
        Assert.Throws<InvalidOperationException>(() => _ = wrap[0]);
    }

    [Test]
    public void Indexer_set_fires_event_test()
    {
        var (_, wrap) = GetTestWrap();
        wrap.PropertyChanged += (_, e) => Assert.That(e.PropertyName, Is.EqualTo("Count"));
        var eventArgs = TestCollectionChanged(wrap, p => p[1] = "test 123");
        Assert.That(wrap[1], Is.EqualTo("test 123"));
        Assert.That(eventArgs.Action, Is.EqualTo(NotifyCollectionChangedAction.Replace));
        Assert.That(eventArgs.OldItems, Is.EquivalentTo(new[] { "test 2" }));
        Assert.That(eventArgs.NewItems, Is.EquivalentTo(new[] { "test 123" }));
        Assert.That(eventArgs.NewStartingIndex, Is.EqualTo(1));
    }

    [Test]
    public void Indexer_set_contract_test()
    {
        var wrap = new ObservableListWrap();
        Assert.Throws<InvalidOperationException>(() => wrap[0] = "test 1234");
    }

    [Test]
    public void RefreshAt_test()
    {
        var (_, wrap) = GetTestWrap();
        var eventArgs = TestCollectionChanged(wrap, p => p.RefreshAt(1));
        Assert.That(eventArgs.Action, Is.EqualTo(NotifyCollectionChangedAction.Replace));
        Assert.That(eventArgs.OldItems, Is.EquivalentTo(new[] { "test 2" }));
        Assert.That(eventArgs.NewItems, Is.EquivalentTo(new[] { "test 2" }));
        Assert.That(eventArgs.OldStartingIndex, Is.EqualTo(1));
        Assert.That(eventArgs.NewStartingIndex, Is.EqualTo(1));
    }

    [Test]
    public void RefreshAt_with_IRefreshable_test()
    {
        var refreshable = new RefreshableTest();
        var wrap = new ObservableListWrap(new ArrayList() { refreshable });
        TestCollectionChanged(wrap, p => p.RefreshAt(0), false);
        Assert.That(refreshable.IsRefreshed);
    }

    [Test]
    public void IsFixedSize_test()
    {
        var (arrayList, wrap) = GetTestWrap();
        Assert.That(wrap.IsFixedSize, Is.EqualTo(arrayList.IsFixedSize));
    }

    [Test]
    public void IsFixedSize_without_underlying_list_test()
    {
        var wrap = new ObservableListWrap();
        Assert.Throws<InvalidOperationException>(() => _ = wrap.IsFixedSize);
    }

    [Test]
    public void IsReadOnly_test()
    {
        var (arrayList, wrap) = GetTestWrap();
        Assert.That(wrap.IsReadOnly, Is.EqualTo(arrayList.IsReadOnly));
    }

    [Test]
    public void IsReadOnly_without_underlying_list_test()
    {
        var wrap = new ObservableListWrap();
        Assert.Throws<InvalidOperationException>(() => _ = wrap.IsReadOnly);
    }

    [Test]
    public void Count_test()
    {
        var (arrayList, wrap) = GetTestWrap();
        Assert.That(wrap.Count, Is.EqualTo(arrayList.Count));
    }

    [Test]
    public void Count_without_underlying_list_test()
    {
        var wrap = new ObservableListWrap();
        Assert.Throws<InvalidOperationException>(() => _ = wrap.Count);
    }

    [Test]
    public void IsSynchronized_test()
    {
        var (arrayList, wrap) = GetTestWrap();
        Assert.That(wrap.IsSynchronized, Is.EqualTo(arrayList.IsSynchronized));
    }

    [Test]
    public void IsSynchronized_without_underlying_list_test()
    {
        var wrap = new ObservableListWrap();
        Assert.Throws<InvalidOperationException>(() => _ = wrap.IsSynchronized);
    }

    [Test]
    public void SyncRoot_test()
    {
        var (arrayList, wrap) = GetTestWrap();
        Assert.That(wrap.SyncRoot, Is.EqualTo(arrayList.SyncRoot));
    }

    [Test]
    public void SyncRoot_without_underlying_list_test()
    {
        var wrap = new ObservableListWrap();
        Assert.Throws<InvalidOperationException>(() => _ = wrap.SyncRoot);
    }

    [Test]
    public void Add_fires_event_test()
    {
        var (_, wrap) = GetTestWrap();
        wrap.PropertyChanged += (_, e) => Assert.That(e.PropertyName, Is.EqualTo("Count"));
        var eventArgs = TestCollectionChanged(wrap, p => Assert.That(p.Add("test 123"), Is.EqualTo(2)));
        Assert.That(wrap[2], Is.EqualTo("test 123"));
        Assert.That(eventArgs.Action, Is.EqualTo(NotifyCollectionChangedAction.Add));
        Assert.That(eventArgs.NewItems, Is.EquivalentTo(new[] { "test 123" }));
        Assert.That(eventArgs.NewStartingIndex, Is.EqualTo(2));
    }

    [Test]
    public void Add_contract_test()
    {
        var wrap = new ObservableListWrap();
        Assert.Throws<InvalidOperationException>(() => wrap.Add("test 99"));
    }

    [Test]
    public void Clear_fires_event_test()
    {
        var (_, wrap) = GetTestWrap();
        wrap.PropertyChanged += (_, e) => Assert.That(e.PropertyName, Is.EqualTo("Count"));
        var eventArgs = TestCollectionChanged(wrap, p => p.Clear());
        Assert.That(wrap, Is.Empty);
        Assert.That(eventArgs.Action, Is.EqualTo(NotifyCollectionChangedAction.Reset));
    }

    [Test]
    public void Clear_contract_test()
    {
        var wrap = new ObservableListWrap();
        Assert.Throws<InvalidOperationException>(() => wrap.Clear());
    }

    [Test]
    public void Contains_test()
    {
        var (_, wrap) = GetTestWrap();
        Assert.That(wrap.Contains("test 1"));
        Assert.That(wrap.Contains("test 999"), Is.False);
    }

    [Test]
    public void Contains_contract_test()
    {
        var wrap = new ObservableListWrap();
        Assert.Throws<InvalidOperationException>(() => wrap.Contains("test"));
    }

    [Test]
    public void CopyTo_test()
    {
        var destination = new object[2];
        var (_, wrap) = GetTestWrap();
        wrap.CopyTo(destination, 0);
        Assert.That(destination, Is.EquivalentTo(wrap));
    }

    [Test]
    public void CopyTo_contract_test()
    {
        var wrap = new ObservableListWrap();
        Assert.Throws<InvalidOperationException>(() => wrap.CopyTo(Array.Empty<object>(), 0));
    }

    [Test]
    public void GetEnumerator_test()
    {
        var (list, wrap) = GetTestWrap();
        var wrapEnumerator = wrap.GetEnumerator();
        var listEnumerator = list.GetEnumerator();
        Assert.That(wrapEnumerator.GetType(), Is.SameAs(listEnumerator.GetType()));

        while(wrapEnumerator.MoveNext() && listEnumerator.MoveNext())
        {
            Assert.That(wrapEnumerator.Current, Is.EqualTo(listEnumerator.Current));
        }
        Assert.Multiple(() => {
            Assert.That(wrapEnumerator.MoveNext(), Is.False);
            Assert.That(listEnumerator.MoveNext(), Is.False);
        });
    }

    [Test]
    public void GetEnumerator_contract_test()
    {
        var wrap = new ObservableListWrap();
        Assert.Throws<InvalidOperationException>(() => _ = wrap.GetEnumerator());
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
        var wrap = new ObservableListWrap();
        Assert.Throws<InvalidOperationException>(() => _ = wrap.IndexOf("test 2"));
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
        Assert.That(eventArgs.NewStartingIndex, Is.EqualTo(0));
    }

    [Test]
    public void Insert_contract_test()
    {
        var wrap = new ObservableListWrap();
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
        Assert.That(eventArgs.OldStartingIndex, Is.EqualTo(1));
    }

    [Test]
    public void RemoveAt_contract_test()
    {
        var wrap = new ObservableListWrap();
        Assert.Throws<InvalidOperationException>(() => wrap.RemoveAt(1));
    }

    [Test]
    public void Remove_fires_event_test()
    {
        var (_, wrap) = GetTestWrap();
        wrap.PropertyChanged += (_, e) => Assert.That(e.PropertyName, Is.EqualTo("Count"));
        var eventArgs = TestCollectionChanged(wrap, p => p.Remove("test 2"));
        Assert.That(eventArgs.Action, Is.EqualTo(NotifyCollectionChangedAction.Remove));
        Assert.That(eventArgs.OldItems, Is.EquivalentTo(new[] { "test 2" }));
        Assert.That(eventArgs.OldStartingIndex, Is.EqualTo(1));
    }

    [Test]
    public void Remove_contract_test()
    {
        var wrap = new ObservableListWrap();
        Assert.Throws<InvalidOperationException>(() => wrap.Remove("test 2"));
    }

    [Test]
    public void Refresh_test()
    {
        var (list, wrap) = GetTestWrap();

        bool resetEvt = false;
        bool addEvt = false;

        TestEvents(wrap, p => p.Refresh(), new[] {
            new EventTestEntry<ObservableListWrap, NotifyCollectionChangedEventArgs>(
                typeof(NotifyCollectionChangedEventHandler),
                nameof(ObservableWrapBase.CollectionChanged),
                evt => {
                    switch (evt.Action)
                    {
                        case NotifyCollectionChangedAction.Reset when resetEvt == false && addEvt == false:
                            resetEvt = true;
                            break;
                        case NotifyCollectionChangedAction.Add when resetEvt == true && addEvt == false:
                            addEvt = true;
                            Assert.That(evt.NewItems, Is.EquivalentTo(list));
                            Assert.That(evt.NewStartingIndex, Is.EqualTo(0));
                            break;
                        default:
                            Assert.Fail("Evento de refresco inesperado.");
                            return;
                    }
                })});
    }

    [Test]
    public void Refresh_contract_test()
    {
        var wrap = new ObservableListWrap();
        Assert.Throws<InvalidOperationException>(() => wrap.Refresh());
    }
}
