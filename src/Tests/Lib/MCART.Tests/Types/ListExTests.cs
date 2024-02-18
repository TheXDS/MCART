/*
ListExTests.cs

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

namespace TheXDS.MCART.Tests.Types;
using NUnit.Framework;
using System;
using TheXDS.MCART.Events;
using TheXDS.MCART.Types;
using TheXDS.MCART.Types.Base;

public class ListExTests
{
    [Test]
    public void Ctor_Test()
    {
        ListEx<int> l = new();
        Assert.That(l, Is.Not.Null);
        Assert.That(l.TriggerEvents);
        l = new(new[] {1, 2, 3, 4, 5});
        Assert.That(l, Is.EquivalentTo(new[] {1, 2, 3, 4, 5}));
        Assert.That(l.TriggerEvents);
        l = new(3);
        Assert.That(l, Is.Not.Null);
        Assert.That(l.TriggerEvents);
        l = new() {TriggerEvents = false};
        Assert.That(l.TriggerEvents, Is.False);
    }

    [Test]
    public void Indexer_Basic_Access_Test()
    {
        ListEx<int> l = new(new[] {1, 2, 3, 4, 5});
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

        ListEx<int> l = new(new[] {1, 2, 3, 4, 5}) { TriggerEvents = true };
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
        ListEx<int> l = new(new[] {1, 2, 3, 4, 5}) { TriggerEvents = true };
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

        ListEx<int> l = new(new[] {1, 2, 3, 4, 5}) { TriggerEvents = false };
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
}
