/*
ListExTests.cs

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

using System.Diagnostics.CodeAnalysis;
using TheXDS.MCART.Events;
using TheXDS.MCART.Types;
using TheXDS.MCART.Types.Base;

namespace TheXDS.MCART.Tests.Types;

[Obsolete, ExcludeFromCodeCoverage]
public class ListExTests
{
    [Test]
    public void Ctor_Test()
    {
        ListEx<int> l = [];
        Assert.That(l, Is.Not.Null);
        Assert.That(l.TriggerEvents);
        l = new([1, 2, 3, 4, 5]);
        Assert.That(l, Is.EquivalentTo([1, 2, 3, 4, 5]));
        Assert.That(l.TriggerEvents);
        l = new(3);
        Assert.That(l, Is.Not.Null);
        Assert.That(l.TriggerEvents);
        l = new() { TriggerEvents = false };
        Assert.That(l.TriggerEvents, Is.False);
    }

    [Test]
    public void Indexer_Basic_Access_Test()
    {
        ListEx<int> l = new([1, 2, 3, 4, 5]);
        Assert.That(3, Is.EqualTo(l[2]));
        l[2] = 6;
        Assert.That(6, Is.EqualTo(l[2]));
    }

    [Test]
    public void Indexer_Cancel_Event_Test()
    {
        bool cancelCalled = false;
        void JustCancel(object? sender, ModifyingItemEventArgs<int> e)
        {
            Assert.That(3, Is.EqualTo(e.OldValue));
            Assert.That(6, Is.EqualTo(e.NewValue));
            Assert.That(2, Is.EqualTo(e.Index));
            e.Cancel = true;
            cancelCalled = true;
        }
        void DoNotCancel(object? sender, ModifyingItemEventArgs<int> e)
        {
            Assert.That(3, Is.EqualTo(e.OldValue));
            Assert.That(6, Is.EqualTo(e.NewValue));
            Assert.That(2, Is.EqualTo(e.Index));
            e.Cancel = false;
            cancelCalled = true;
        }

        void CheckModified(object? sender, ItemModifiedEventArgs<int> e) => Assert.Fail();

        ListEx<int> l = new([1, 2, 3, 4, 5]) { TriggerEvents = true };
        l.ModifyingItem += JustCancel;
        l.ModifiedItem += CheckModified;
        l[2] = 6;
        Assert.That(3, Is.EqualTo(l[2]));
        Assert.That(cancelCalled);
        l.ModifiedItem -= CheckModified;
        l.ModifyingItem -= JustCancel;
        l.ModifyingItem += DoNotCancel;
        cancelCalled = false;
        l[2] = 6;
        Assert.That(6, Is.EqualTo(l[2]));
        Assert.That(cancelCalled);
        l.ModifyingItem -= DoNotCancel;
    }

    [Test]
    public void Indexer_Modified_Event_Test()
    {
        bool eventCalled = false;
        void CheckEvent(object? sender, ItemModifiedEventArgs<int> e)
        {
            Assert.That(2, Is.EqualTo(e.Index));
            Assert.That(6, Is.EqualTo(e.Item));
            eventCalled = true;
        }
        ListEx<int> l = new([1, 2, 3, 4, 5]) { TriggerEvents = true };
        l.ModifiedItem += CheckEvent;
        l[2] = 6;
        Assert.That(6, Is.EqualTo(l[2]));
        Assert.That(eventCalled);
        l.ModifiedItem -= CheckEvent;
    }

    [Test]
    public void Indexer_When_TriggerEvents_Is_False_Test()
    {
        void CheckModifying(object? sender, ModifyingItemEventArgs<int> e) => Assert.Fail();
        void CheckModified(object? sender, ItemModifiedEventArgs<int> e) => Assert.Fail();

        ListEx<int> l = new([1, 2, 3, 4, 5]) { TriggerEvents = false };
        l.ModifyingItem += CheckModifying;
        l.ModifiedItem += CheckModified;
        l[2] = 6;
        Assert.That(6, Is.EqualTo(l[2]));
        l.ModifyingItem -= CheckModifying;
        l.ModifiedItem -= CheckModified;
    }

    [Test]
    public void ItemType_Test()
    {
        Assert.That(typeof(int), Is.EqualTo(new ListEx<int>().ItemType));
        Assert.That(typeof(string), Is.EqualTo(new ListEx<string>().ItemType));
        Assert.That(typeof(DayOfWeek), Is.EqualTo(new ListEx<DayOfWeek>().ItemType));
        Assert.That(typeof(IVector), Is.EqualTo(new ListEx<IVector>().ItemType));
        Assert.That(typeof(Exception), Is.EqualTo(new ListEx<Exception>().ItemType));
        Assert.That(typeof(Guid), Is.EqualTo(new ListEx<Guid>().ItemType));
    }

    [Test]
    public void Remove_performs_nothing_on_empty_collection()
    {
        ListEx<int> l = [];
        l.RemovingItem += (s, e) => Assert.Fail();
        l.RemovedItem += (s, e) => Assert.Fail();

        Assert.That(() => l.Remove(0), Throws.Nothing);
        Assert.That(l.Remove(0), Is.False);
    }

    [Test]
    public void Remove_triggers_events()
    {
        bool removingTriggered = false;
        bool removedTriggered = false;
        void removingHandler(object? s, RemovingItemEventArgs<char> e)
        {
            Assert.That(e.RemovedItem, Is.EqualTo('2'));
            Assert.That(e.Index, Is.EqualTo(1));
            removingTriggered = true;
        }
        void removedHandler(object? s, RemovedItemEventArgs<char> e)
        {
            Assert.That(e.RemovedItem, Is.EqualTo('2'));
            removedTriggered = true;
        }
        ListEx<char> l = ['1', '2', '3'];
        l.TriggerEvents = true;
        l.RemovingItem += removingHandler;
        l.RemovedItem += removedHandler;

        Assert.That(l.Remove('2'), Is.True);
        Assert.That(l, Is.EquivalentTo((char[])['1', '3']));
        Assert.That(removingTriggered, Is.True);
        Assert.That(removedTriggered, Is.True);

        l.RemovingItem -= removingHandler;
        l.RemovedItem -= removedHandler;
    }

    [Test]
    public void Remove_can_be_cancelled()
    {
        bool removingTriggered = false;
        bool removedTriggered = false;
        void removingHandler(object? s, RemovingItemEventArgs<char> e)
        {
            removingTriggered = true;
            e.Cancel = true;
        }

        void removedHandler(object? s, RemovedItemEventArgs<char> e) => removedTriggered = true;

        ListEx<char> l = ['1', '2', '3'];
        l.TriggerEvents = true;
        l.RemovingItem += removingHandler;
        l.RemovedItem += removedHandler;

        Assert.That(l.Remove('2'), Is.False);
        Assert.That(l, Is.EquivalentTo((char[])['1', '2', '3']));
        Assert.That(removingTriggered, Is.True);
        Assert.That(removedTriggered, Is.False);

        l.RemovingItem -= removingHandler;
        l.RemovedItem -= removedHandler;
    }

    [Test]
    public void Remove_works_on_disabled_events()
    {
        void removingHandler(object? s, RemovingItemEventArgs<char> e) => Assert.Fail();
        void removedHandler(object? s, RemovedItemEventArgs<char> e) => Assert.Fail();

        ListEx<char> l = ['1', '2', '3'];
        l.TriggerEvents = false;
        l.RemovingItem += removingHandler;
        l.RemovedItem += removedHandler;

        Assert.That(l.Remove('2'), Is.True);
        Assert.That(l, Is.EquivalentTo((char[])['1', '3']));

        l.RemovingItem -= removingHandler;
        l.RemovedItem -= removedHandler;
    }

    [Test]
    public void Remove_works_on_non_subscribed_events()
    {
        ListEx<char> l = ['1', '2', '3'];
        l.TriggerEvents = true;

        Assert.That(l.Remove('2'), Is.True);
        Assert.That(l, Is.EquivalentTo((char[])['1', '3']));
    }

    [Test]
    public void RemoveAt_performs_nothing_on_empty_collection()
    {
        ListEx<int> l = [];
        l.RemovingItem += (s, e) => Assert.Fail();
        l.RemovedItem += (s, e) => Assert.Fail();

        Assert.That(() => l.RemoveAt(0), Throws.Nothing);
    }

    [Test]
    public void RemoveAt_triggers_events()
    {
        bool removingTriggered = false;
        bool removedTriggered = false;
        void removingHandler(object? s, RemovingItemEventArgs<char> e)
        {
            Assert.That(e.RemovedItem, Is.EqualTo('2'));
            Assert.That(e.Index, Is.EqualTo(1));
            removingTriggered = true;
        }
        void removedHandler(object? s, RemovedItemEventArgs<char> e)
        {
            Assert.That(e.RemovedItem, Is.EqualTo('2'));
            removedTriggered = true;
        }

        ListEx<char> l = ['1', '2', '3'];
        l.TriggerEvents = true;
        l.RemovingItem += removingHandler;
        l.RemovedItem += removedHandler;

        l.RemoveAt(1);

        Assert.That(l, Is.EquivalentTo((char[])['1', '3']));
        Assert.That(removingTriggered, Is.True);
        Assert.That(removedTriggered, Is.True);

        l.RemovingItem -= removingHandler;
        l.RemovedItem -= removedHandler;
    }

    [Test]
    public void RemoveAt_can_be_cancelled()
    {
        bool removingTriggered = false;
        bool removedTriggered = false;
        void removingHandler(object? s, RemovingItemEventArgs<char> e)
        {
            removingTriggered = true;
            e.Cancel = true;
        }

        void removedHandler(object? s, RemovedItemEventArgs<char> e) => removedTriggered = true;

        ListEx<char> l = ['1', '2', '3'];
        l.TriggerEvents = true;
        l.RemovingItem += removingHandler;
        l.RemovedItem += removedHandler;

        l.RemoveAt(1);

        Assert.That(l, Is.EquivalentTo((char[])['1', '2', '3']));
        Assert.That(removingTriggered, Is.True);
        Assert.That(removedTriggered, Is.False);

        l.RemovingItem -= removingHandler;
        l.RemovedItem -= removedHandler;
    }

    [Test]
    public void RemoveAt_works_on_disabled_events()
    {
        void removingHandler(object? s, RemovingItemEventArgs<char> e) => Assert.Fail();
        void removedHandler(object? s, RemovedItemEventArgs<char> e) => Assert.Fail();

        ListEx<char> l = ['1', '2', '3'];
        l.TriggerEvents = false;
        l.RemovingItem += removingHandler;
        l.RemovedItem += removedHandler;

        l.RemoveAt(1);

        Assert.That(l, Is.EquivalentTo((char[])['1', '3']));

        l.RemovingItem -= removingHandler;
        l.RemovedItem -= removedHandler;
    }

    [Test]
    public void RemoveAt_works_on_non_subscribed_events()
    {
        ListEx<char> l = ['1', '2', '3'];
        l.TriggerEvents = true;

        l.RemoveAt(1);
        Assert.That(l, Is.EquivalentTo((char[])['1', '3']));
    }
}
