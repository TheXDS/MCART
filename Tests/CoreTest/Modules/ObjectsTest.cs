/*
ObjectsTest.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright (c) 2011 - 2018 César Andrés Morgan

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
using System.Collections;
using System.Linq;
using System.Reflection;
using TheXDS.MCART;
using TheXDS.MCART.Resources;
using Xunit;

namespace CoreTest.Modules
{
    [AttrTest]
    public class ObjectsTest
    {
        /// <summary>
        /// Atributo de prueba.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class)]
        sealed class AttrTestAttribute : Attribute { }

        [Fact]
        public void IsAnyNullTest()
        {
            Assert.True(Objects.IsAnyNull(0, 1, null));
            Assert.False(Objects.IsAnyNull(0, 1, 2, 3));
        }
        [Fact]
        public void AreAllNullTest()
        {
            Assert.True(Objects.AreAllNull(null, null, null));
            Assert.False(Objects.AreAllNull(0, null));
        }
        [Fact]
        public void ItselfTest()
        {
            ApplicationException ex = new ApplicationException();
            Assert.Same(ex, ex.Itself());
            Assert.NotSame(ex, new ApplicationException());
            Assert.NotSame(ex, null);
        }
        [Fact]
        public void IsTest()
        {
            EventArgs ev = EventArgs.Empty;
            EventArgs e = ev;
            Assert.True(e.Is(ev));
        }
        [Fact]
        public void IsNotTest()
        {
            EventArgs ev = new TheXDS.MCART.Events.ExceptionEventArgs(null);
            EventArgs e = new TheXDS.MCART.Events.ExceptionEventArgs(null);
            Assert.True(e.IsNot(ev));
        }
        [Fact]
        public void IsEitherTest()
        {
            Type t = typeof(int);
            Assert.True(t.IsEither(typeof(bool), typeof(int)));
            Assert.False(t.IsEither(typeof(bool), typeof(float)));
        }
        [Fact]
        public void IsNeitherTest()
        {
            Type t = typeof(int);
            Assert.True(t.IsNeither(typeof(bool), typeof(float)));
            Assert.False(t.IsNeither(typeof(bool), typeof(int)));
        }
        [Fact]
        public void GetTypesTest()
        {
            Assert.True(Objects.GetTypes<IComparable>().Count() > 2);
        }
        [Fact]
        public void ToTypesTest()
        {
            Type[] x = (new object[] { 1, "Test", 2.5f }).ToTypes().ToArray();
            IEnumerator y = x.GetEnumerator();
            y.Reset();
            y.MoveNext();
            Assert.Same(typeof(int), y.Current);
            y.MoveNext();
            Assert.Same(typeof(string), y.Current);
            y.MoveNext();
            Assert.Same(typeof(float), y.Current);
        }
        [Fact]
        public void GetAttrTest()
        {
            Assert.NotNull(RTInfo.RTAssembly.GetAttr<AssemblyTitleAttribute>());
            Assert.NotNull(MethodBase.GetCurrentMethod().GetAttr<FactAttribute>());
            Assert.NotNull(Objects.GetAttr<AttrTestAttribute, ObjectsTest>());
            Assert.NotNull(typeof(ObjectsTest).GetAttr<AttrTestAttribute>());
        }
    }
}