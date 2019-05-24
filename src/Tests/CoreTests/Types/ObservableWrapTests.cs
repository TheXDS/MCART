/*
RangeTests.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2019 César Andrés Morgan

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

#pragma warning disable CS1591

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using TheXDS.MCART.Types;
using Xunit;
using static System.Collections.Specialized.NotifyCollectionChangedAction;

namespace TheXDS.MCART.Tests.Types
{
    public class ObservableWrapTests
    {
        [Fact]
        public void InstanceTest()
        {
            var c = new List<string> { "1", "2", "3" };
            var o = new ObservableCollectionWrap<string>(c);
            Assert.Equal(c, o.UnderlyingCollection);
            Assert.Equal(3, o.Count);
        }

        [Fact]
        public void AddTest()
        {
            var c = new ObservableCollectionWrap<string>(new List<string> { "1", "2", "3" });
            EventTest(c, () => c.Add("4"), Add, out var evt);
            Assert.Equal("4", (string)evt.Arguments.NewItems[0]);
            Assert.Contains("4", c);
        }

        [Fact]
        public void ClearTest()
        {
            var c = new ObservableCollectionWrap<string>(new List<string> { "1", "2", "3" });
            EventTest(c, c.Clear, Reset, out _);
            Assert.Empty(c);
        }

        [Fact]
        public void RemoveTest()
        {
            var c = new ObservableCollectionWrap<string>(new List<string> { "1", "2", "3" });
            EventTest(c, () => c.Remove("2"), Remove, out var evt);
            Assert.Equal("2", (string)evt.Arguments.OldItems[0]);
            Assert.DoesNotContain("2", c);
        }

        [Fact]
        public void RefreshTest()
        {
            var l = new List<string> { "1", "2", "3" };
            var c = new ObservableCollectionWrap<string>(l);
            Assert.ThrowsAny<Xunit.Sdk.RaisesException>(() => EventTest(c, () => l.Add("4"), Add, out _));
            Assert.Contains("4", c);
            EventTest(c, c.Refresh, Add, out _);
        }

        private void EventTest<T>(ObservableCollectionWrap<T> c, Action action, NotifyCollectionChangedAction nAction, out Assert.RaisedEvent<NotifyCollectionChangedEventArgs> evt)
        {
            NotifyCollectionChangedEventHandler handler = null;

            evt = Assert.Raises<NotifyCollectionChangedEventArgs>(
                h =>
                {
                    handler = new NotifyCollectionChangedEventHandler(h);
                    c.CollectionChanged += handler;
                },
                h => c.CollectionChanged -= handler,
                action);

            Assert.NotNull(evt);
            Assert.True(ReferenceEquals(c, evt.Sender));
            Assert.Equal(nAction, evt.Arguments.Action);
        }
    }
}