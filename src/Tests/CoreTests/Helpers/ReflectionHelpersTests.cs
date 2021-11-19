/*
ReflectionHelpersTests.cs

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

#pragma warning disable IDE0061
#pragma warning disable IDE0062
#pragma warning disable CA1822

using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using TheXDS.MCART.Exceptions;
using TheXDS.MCART.Helpers;
using NUnit.Framework;
using static TheXDS.MCART.Helpers.ReflectionHelpers;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.Tests.Helpers
{
    public class ReflectionHelpersTests
    {
        [Test]
        public void GetMethod_Test()
        {
            var m1 = GetMethod<Test1, Action>(t => t.Test);
            var b1 = typeof(Test1).GetMethod("Test")!;
            var m2 = GetMethod<Test2, Action>(t => t.Test);
            var b2 = typeof(Test2).GetMethod("Test")!;

            Assert.NotNull(m1);
            Assert.NotNull(m2);
            Assert.AreSame(m1, b1);
            Assert.AreSame(m2, b2);
            Assert.AreNotSame(m1, m2);
            Assert.AreNotSame(b1, b2);

            var i = new Test1();
            var n = GetMethod<Func<int>>(() => i.TestInt);
            Assert.IsInstanceOf<MethodInfo>(n);
            Assert.AreEqual("TestInt", n.Name);

            var o = GetMethod<Test1, Func<int>>(t => t.TestInt);
            Assert.IsInstanceOf<MethodInfo>(o);
            Assert.AreEqual("TestInt", o.Name);

            var m = GetMethod<Test1>(t => (Func<int>)t.TestInt);
            Assert.IsInstanceOf<MethodInfo>(m);
            Assert.AreEqual("TestInt", m.Name);
        }

        [Test]
        public void GetCallingMethod_Test()
        {
            MethodBase TestMethod()
            {
                return GetCallingMethod()!;
            }

            Assert.AreEqual(MethodBase.GetCurrentMethod(), TestMethod());
            Assert.Null(GetCallingMethod(int.MaxValue - 1));
            Assert.Throws<OverflowException>(() => GetCallingMethod(int.MaxValue));
            Assert.Throws<ArgumentOutOfRangeException>(() => GetCallingMethod(0));
        }

        [Test]
        public void IsOverriden_Test()
        {
            var t1 = new Test1();
            var t2 = new Test2();
            var m1 = GetMethod<Test1, Action>(t => t.Test);
            var m2 = GetMethod<Test2, Action>(t => t.Test);
            var m3 = GetMethod<Test1, Action>(t => t.TestC<int>);

            Assert.False(m1.IsOverriden(t1));
            Assert.True(m1.IsOverriden(t2));
            Assert.False(m2.IsOverriden(t2));
            Assert.True(m3.IsOverriden(t2));
            Assert.False(m3.IsOverriden(t1));

            Assert.Throws<ArgumentNullException>(() => m1.IsOverriden(null!));
            Assert.Throws<ArgumentNullException>(() => MethodBaseExtensions.IsOverriden(null!, null!));
            Assert.Throws<InvalidTypeException>(() => m2.IsOverriden(t1));
        }

        [Test]
        public void IsOverride_Test()
        {
            var m1 = GetMethod<Test1, Action>(t => t.Test);
            var m2 = GetMethod<Test2, Action>(t => t.Test);

            Assert.True(m2.IsOverride());
            Assert.False(m1.IsOverride());
            Assert.Throws<ArgumentNullException>(() => TheXDS.MCART.Types.Extensions.MethodInfoExtensions.IsOverride(null!));
        }

        [Test]
        public void GetEntryPoint_Test()
        {
            Assert.NotNull(GetEntryPoint());
        }

        [Test]
        public void GetEntryAssembly_Test()
        {
            Assert.NotNull(GetEntryAssembly());
        }

        [Test]
        public void GetMember_Test()
        {
            var m = GetMember<Test1>(t => t.TestInt());
            Assert.IsInstanceOf<MethodInfo>(m);
            Assert.AreEqual("TestInt", m.Name);

            var i = new Test1();
            var n = GetMember(() => i.TestInt());
            Assert.IsInstanceOf<MethodInfo>(n);
            Assert.AreEqual("TestInt", n.Name);

            var p = GetMember((System.Linq.Expressions.Expression<Func<object?>>)(() => i.TestInt()));
            Assert.IsInstanceOf<MethodInfo>(p);
            Assert.AreEqual("TestInt", p.Name);

            var o = GetMember<Test1, int>(t => t.TestInt());
            Assert.IsInstanceOf<MethodInfo>(o);
            Assert.AreEqual("TestInt", o.Name);
        }

        [Test]
        public void GetMember_Contract_Test()
        {
            Assert.Throws<ArgumentException>(() => GetMember<Test1>(t => t.TestInt() + 2));
        }

        [Test]
        public void GetField_Test()
        {
            var m = GetField<Test1, string>(t => t.TestField);
            Assert.IsInstanceOf<FieldInfo>(m);
            Assert.AreEqual("TestField", m.Name);

            var i = new Test1();
            var n = GetField(() => i.TestField);
            Assert.IsInstanceOf<FieldInfo>(n);
            Assert.AreEqual("TestField", n.Name);

            var o = GetField<Test1>(t => t.TestField);
            Assert.IsInstanceOf<FieldInfo>(o);
            Assert.AreEqual("TestField", o.Name);
        }

        [Test]
        public void GetProperty_Test()
        {
            var m = GetProperty<Test1, float>(t => t.TestProperty);
            Assert.IsInstanceOf<PropertyInfo>(m);
            Assert.AreEqual("TestProperty", m.Name);

            var i = new Test1();
            var n = GetProperty(() => i.TestProperty);
            Assert.IsInstanceOf<PropertyInfo>(n);
            Assert.AreEqual("TestProperty", n.Name);

            var o = GetProperty<Test1>(t => t.TestProperty);
            Assert.IsInstanceOf<PropertyInfo>(o);
            Assert.AreEqual("TestProperty", o.Name);
        }

        [ExcludeFromCodeCoverage]
        public class Test1
        {
            public virtual void Test() { }

            public virtual void TestC<T>() { }
            public int TestInt() => default;

            public string TestField = "Test";

            public float TestProperty { get; } = 1.5f;
        }

        [ExcludeFromCodeCoverage]
        public class Test2 : Test1
        {
            public override void Test() { }

            public void TestB() { }

            public override void TestC<T>() { }
        }
    }
}
