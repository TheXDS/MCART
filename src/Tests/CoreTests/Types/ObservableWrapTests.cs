/*
RangeTests.cs

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

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using TheXDS.MCART.Types;
using NUnit.Framework;
using static System.Collections.Specialized.NotifyCollectionChangedAction;
using System.Linq;

namespace TheXDS.MCART.Tests.Types
{
    public class ObservableWrapTests
    {
        [Test]
        public void InstanceTest()
        {
            List<string>? c = new() { "1", "2", "3" };
            ObservableCollectionWrap<string>? o = new(c);
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

        private static void EventTest<T>(ObservableCollectionWrap<T> c, Action action, NotifyCollectionChangedAction nAction, out (object? Sender, NotifyCollectionChangedEventArgs Arguments) evt)
        {
            (object? Sender, NotifyCollectionChangedEventArgs Arguments)? ev = null;
            void C_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
            {
                ev = (sender, e);
            }

            c.CollectionChanged += C_CollectionChanged;
            action();
            c.CollectionChanged -= C_CollectionChanged;
            evt = ev ?? default;

            Assert.NotNull(evt);
            Assert.True(ReferenceEquals(c, evt.Sender));
            Assert.AreEqual(nAction, evt.Arguments.Action);
        }
    }
}
