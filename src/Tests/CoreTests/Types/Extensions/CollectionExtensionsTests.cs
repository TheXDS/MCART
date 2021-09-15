/*
CollectionExtensionsTests.cs

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
using System.Collections.ObjectModel;
using System.Linq;
using TheXDS.MCART.Types;
using TheXDS.MCART.Types.Base;
using TheXDS.MCART.Types.Extensions;
using Xunit;

namespace TheXDS.MCART.Tests.Types.Extensions
{
    public class CollectionExtensionsTests
    {
        [Fact]
        public void RemoveOf_Test()
        {
            var e = new Exception();
            var d = DateTime.Now;
            var o = new object();
            var e2 = new InvalidOperationException();
            ICollection<object> c = new[] { e, o, 5, d, e2 }.ToList();
            c.RemoveOf<object, Exception>();

            Assert.Contains(d, c);
            Assert.Contains(o, c);
            Assert.Contains(5, c);
            Assert.DoesNotContain(e, c);
            Assert.DoesNotContain(e2, c);
        }

        [Fact]
        public void RemoveAll_With_Predicate_Test()
        {
            var l = Enumerable.Range(1, 10).ToList();
            List<int> r = new();
            l.RemoveAll(p => p % 2 == 1, p => r.Add(p));
            Assert.Equal(new[]{ 2, 4, 6, 8, 10 }, l.ToArray());
            Assert.Equal(new[]{ 1, 3, 5, 7, 9 }, r.ToArray());
        }
        
        [Fact]
        public void RemoveAll_Without_Predicate_Test()
        {
            var l = Enumerable.Range(1, 10).ToList();
            List<int> r = new();
            l.RemoveAll(p => r.Add(p));
            Assert.Empty(l);
            Assert.Equal(new[]{ 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, r.ToArray());
        }
        
        [Fact]
        public void RemoveAll_With_Predicate_No_Action_Test()
        {
            var l = Enumerable.Range(1, 10).ToList();
            TheXDS.MCART.Types.Extensions.CollectionExtensions.RemoveAll(l, p => p % 2 == 1);
            Assert.Equal(new[]{ 2, 4, 6, 8, 10 }, l.ToArray());
        }
        
        [Fact]
        public void RemoveAll_Without_Params_Test()
        {
            var l = Enumerable.Range(1, 10).ToList();
            l.RemoveAll();
            Assert.Empty(l);
        }

        [Fact]
        public void Push_Test()
        {
            List<Guid> l = new();
            var g = l.Push();
            Assert.Contains(g, l);
        }
        
        [Fact]
        public void PushInto_Test()
        {
            List<Guid> l = new();
            var g = Guid.NewGuid().PushInto(l);
            Assert.Contains(g, l);
        }
        
        [Fact]
        public void Push_With_Base_Type_Test()
        {
            List<object> l = new();
            var g = l.Push<Guid, object>();
            Assert.Contains(g, l);
        }

        [Fact]
        public void ToObservable_Test()
        {
            Assert.IsType<ObservableCollectionWrap<int>>(new Collection<int>().ToObservable());
        }

        [Fact]
        public void AddRange_Test()
        {
            Collection<int> c = new();
            c.AddRange(new[] { 1, 2, 3, 4 });
            Assert.Equal(new[] { 1, 2, 3, 4 }, c.ToArray());
        }

        private class ClonableTestClass : ICloneable<ClonableTestClass>
        {
            public int Value { get; set; }
        }

        [Fact]
        public void AddClones_Test()
        {
            var j = new ClonableTestClass()
            {
                Value = 1
            };
            var k = new ClonableTestClass()
            {
                Value = 2
            };
            Collection<ClonableTestClass> l = new() { j, k };
            Collection<ClonableTestClass> m = new();
            m.AddClones(l);
            var n = m.Single(p => p.Value == j.Value);
            var o = m.Single(p => p.Value == k.Value);

            Assert.NotNull(n);
            Assert.NotNull(o);
            Assert.NotSame(j,n);
            Assert.NotSame(k,o);
        }
        
        [Fact]
        public void AddClone_Test()
        {
            var j = new ClonableTestClass()
            {
                Value = 1
            };
            Collection<ClonableTestClass> m = new();
            m.AddClone(j);
            var n = m.Single(p => p.Value == j.Value);

            Assert.NotNull(n);
            Assert.NotSame(j,n);
        }
    }
}