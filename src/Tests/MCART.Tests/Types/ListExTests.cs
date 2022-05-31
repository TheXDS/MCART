/*
ListExTests.cs

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
using TheXDS.MCART.Events;
using TheXDS.MCART.Types;
using TheXDS.MCART.Types.Base;

public class ListExTests
{
    [Test]
    public void Ctor_Test()
    {
        ListEx<int> l = new();
        Assert.NotNull(l);
        Assert.True(l.TriggerEvents);
        l = new(new[] {1, 2, 3, 4, 5});
        Assert.AreEqual(new[] {1, 2, 3, 4, 5}, l.ToArray());
        Assert.True(l.TriggerEvents);
        l = new(3);
        Assert.NotNull(l);
        Assert.True(l.TriggerEvents);
        l = new() {TriggerEvents = false};
        Assert.False(l.TriggerEvents);
    }

    [Test]
    public void Indexer_Basic_Access_Test()
    {
        ListEx<int> l = new(new[] {1, 2, 3, 4, 5});
        Assert.AreEqual(3, l[2]);
        l[2] = 6;
        Assert.AreEqual(6, l[2]);
    }

    [Test]
    public void Indexer_Cancel_Event_Test()
    {
        bool cancelCalled = false;
        void JustCancel(object? sender, ModifyingItemEventArgs<int> e)
        {
            Assert.AreEqual(3,e.OldValue);
            Assert.AreEqual(6,e.NewValue);
            Assert.AreEqual(2,e.Index);
            e.Cancel = true;
            cancelCalled = true;
        }
        void DontCancel(object? sender, ModifyingItemEventArgs<int> e)
        {
            Assert.AreEqual(3,e.OldValue);
            Assert.AreEqual(6,e.NewValue);
            Assert.AreEqual(2,e.Index);
            e.Cancel = false;
            cancelCalled = true;
        }

        void CheckModified(object? sender, ItemModifiedEventArgs<int> e) => Assert.Fail();

        ListEx<int> l = new(new[] {1, 2, 3, 4, 5}) { TriggerEvents = true };
        l.ModifyingItem += JustCancel;
        l.ModifiedItem += CheckModified;
        l[2] = 6;
        Assert.AreEqual(3, l[2]);
        Assert.True(cancelCalled);
        l.ModifiedItem -= CheckModified;
        l.ModifyingItem -= JustCancel;
        l.ModifyingItem += DontCancel;
        cancelCalled = false;
        l[2] = 6;
        Assert.AreEqual(6, l[2]);
        Assert.True(cancelCalled);
        l.ModifyingItem -= DontCancel;
    }

    [Test]
    public void Indexer_Modified_Event_Test()
    {
        bool eventCalled = false;
        void CheckEvent(object? sender, ItemModifiedEventArgs<int> e)
        {
            Assert.AreEqual(2, e.Index);
            Assert.AreEqual(6, e.Item);
            eventCalled = true;
        }
        ListEx<int> l = new(new[] {1, 2, 3, 4, 5}) { TriggerEvents = true };
        l.ModifiedItem += CheckEvent;
        l[2] = 6;
        Assert.AreEqual(6, l[2]);
        Assert.True(eventCalled);
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
        Assert.AreEqual(6, l[2]);
        l.ModifyingItem -= CheckModifying;
        l.ModifiedItem -= CheckModified;
    }

    [Test]
    public void ItemType_Test()
    {
        Assert.AreEqual(typeof(int), new ListEx<int>().ItemType);
        Assert.AreEqual(typeof(string), new ListEx<string>().ItemType);
        Assert.AreEqual(typeof(DayOfWeek), new ListEx<DayOfWeek>().ItemType);
        Assert.AreEqual(typeof(I2DVector), new ListEx<I2DVector>().ItemType);
        Assert.AreEqual(typeof(Exception), new ListEx<Exception>().ItemType);
        Assert.AreEqual(typeof(Guid), new ListEx<Guid>().ItemType);
    }
}
