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
using NUnit.Framework;

namespace TheXDS.MCART.Tests.Types.Extensions
{
    public class CollectionExtensionsTests
    {
        [Test]
        public void RemoveOf_Test()
        {
            var e = new Exception();
            var d = DateTime.Now;
            var o = new object();
            var e2 = new InvalidOperationException();
            ICollection<object> c = new[] { e, o, 5, d, e2 }.ToList();
            c.RemoveOf<object, Exception>();

            Assert.True(c.Contains(d));
            Assert.True(c.Contains(o));
            Assert.True(c.Contains(5));
            Assert.False(c.Contains(e));
            Assert.False(c.Contains(e2));
        }

        [Test]
        public void RemoveAll_With_Predicate_Test()
        {
            var l = Enumerable.Range(1, 10).ToList();
            List<int> r = new();
            l.RemoveAll(p => p % 2 == 1, p => r.Add(p));
            Assert.AreEqual(new[]{ 2, 4, 6, 8, 10 }, l.ToArray());
            Assert.AreEqual(new[]{ 1, 3, 5, 7, 9 }, r.ToArray());
        }
        
        [Test]
        public void RemoveAll_Without_Predicate_Test()
        {
            var l = Enumerable.Range(1, 10).ToList();
            List<int> r = new();
            l.RemoveAll(p => r.Add(p));
            Assert.IsEmpty(l);
            Assert.AreEqual(new[]{ 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, r.ToArray());
        }
        
        [Test]
        public void RemoveAll_With_Predicate_No_Action_Test()
        {
            var l = Enumerable.Range(1, 10).ToList();
            TheXDS.MCART.Types.Extensions.CollectionExtensions.RemoveAll(l, p => p % 2 == 1);
            Assert.AreEqual(new[]{ 2, 4, 6, 8, 10 }, l.ToArray());
        }
        
        [Test]
        public void RemoveAll_Without_Params_Test()
        {
            var l = Enumerable.Range(1, 10).ToList();
            l.RemoveAll();
            Assert.IsEmpty(l);
        }

        [Test]
        public void Push_Test()
        {
            List<Guid> l = new();
            var g = l.Push();
            Assert.Contains(g, l);
        }
        
        [Test]
        public void PushInto_Test()
        {
            List<Guid> l = new();
            var g = Guid.NewGuid().PushInto(l);
            Assert.Contains(g, l);
        }
        
        [Test]
        public void Push_With_Base_Type_Test()
        {
            List<object> l = new();
            var g = l.Push<Guid, object>();
            Assert.Contains(g, l);
        }

        [Test]
        public void ToObservable_Test()
        {
            Assert.IsAssignableFrom<ObservableCollectionWrap<int>>(new Collection<int>().ToObservable());
        }

        [Test]
        public void AddRange_Test()
        {
            Collection<int> c = new();
            c.AddRange(new[] { 1, 2, 3, 4 });
            Assert.AreEqual(new[] { 1, 2, 3, 4 }, c.ToArray());
        }

        private class ClonableTestClass : ICloneable<ClonableTestClass>
        {
            public int Value { get; set; }
        }

        [Test]
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
            Assert.AreNotSame(j,n);
            Assert.AreNotSame(k,o);
        }
        
        [Test]
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
            Assert.AreNotSame(j,n);
        }
    }
}