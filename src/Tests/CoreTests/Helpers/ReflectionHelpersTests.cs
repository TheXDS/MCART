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

using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using TheXDS.MCART.Exceptions;
using TheXDS.MCART.Helpers;
using Xunit;
using static TheXDS.MCART.Helpers.ReflectionHelpers;

namespace TheXDS.MCART.Tests.Helpers
{
    public class ReflectionHelpersTests
    {
        [Fact]
        public void GetMethod_Test()
        {
            var m1 = GetMethod<Test1, Action>(t => t.Test);
            var b1 = typeof(Test1).GetMethod("Test")!;
            var m2 = GetMethod<Test2, Action>(t => t.Test);
            var b2 = typeof(Test2).GetMethod("Test")!;

            Assert.NotNull(m1);
            Assert.NotNull(m2);
            Assert.Same(m1, b1);
            Assert.Same(m2, b2);
            Assert.NotSame(m1, m2);
            Assert.NotSame(b1, b2);
            
            var i = new Test1();
            var n = GetMethod<Func<int>>(() => i.TestInt);
            Assert.IsAssignableFrom<MethodInfo>(n);
            Assert.Equal("TestInt", n.Name);
            
            var o = GetMethod<Test1, Func<int>>(t => t.TestInt);
            Assert.IsAssignableFrom<MethodInfo>(o);
            Assert.Equal("TestInt", o.Name);
            
            var m = GetMethod<Test1>(t => (Func<int>)t.TestInt);
            Assert.IsAssignableFrom<MethodInfo>(m);
            Assert.Equal("TestInt", m.Name);
        }

        [Fact]
        public void GetCallingMethod_Test()
        {
            MethodBase TestMethod()
            {
                return GetCallingMethod()!;
            }

            Assert.Equal(MethodBase.GetCurrentMethod(), TestMethod());
            Assert.Null(GetCallingMethod(int.MaxValue - 1));
            Assert.Throws<OverflowException>(() => GetCallingMethod(int.MaxValue));
            Assert.Throws<ArgumentOutOfRangeException>(() => GetCallingMethod(0));
        }

        [Fact]
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
            Assert.Throws<ArgumentNullException>(() => ReflectionHelpers.IsOverriden(null!, null!));
            Assert.Throws<InvalidTypeException>(() => m2.IsOverriden(t1));
        }

        [Fact]
        public void IsOverride_Test()
        {
            var m1 = GetMethod<Test1, Action>(t => t.Test);
            var m2 = GetMethod<Test2, Action>(t => t.Test);

            Assert.True(m2.IsOverride());
            Assert.False(m1.IsOverride());
            Assert.Throws<ArgumentNullException>(() => ReflectionHelpers.IsOverride(null!));
        }

        [Fact]
        public void GetEntryPoint_Test()
        {
            Assert.NotNull(GetEntryPoint());
        }

        [Fact]
        public void GetEntryAssembly_Test()
        {
            Assert.NotNull(GetEntryAssembly());
        }

        [Fact]
        public void GetMember_Test()
        {
            var m = GetMember<Test1>(t => t.TestInt());
            Assert.IsAssignableFrom<MethodInfo>(m);
            Assert.Equal("TestInt", m.Name);

            var i = new Test1();
            var n = GetMember(() => i.TestInt());
            Assert.IsAssignableFrom<MethodInfo>(n);
            Assert.Equal("TestInt", n.Name);
            
            var p = GetMember((System.Linq.Expressions.Expression<Func<object?>>)(()=>i.TestInt()));
            Assert.IsAssignableFrom<MethodInfo>(p);
            Assert.Equal("TestInt", p.Name);

            
            var o = GetMember<Test1, int>(t => t.TestInt());
            Assert.IsAssignableFrom<MethodInfo>(o);
            Assert.Equal("TestInt", o.Name);
        }

        [Fact]
        public void GetMember_Contract_Test()
        {
            Assert.ThrowsAny<ArgumentException>(() => GetMember<Test1>(t => t.TestInt() + 2));
        }

        [Fact]
        public void GetField_Test()
        {
            var m = GetField<Test1, string>(t => t.TestField);
            Assert.IsAssignableFrom<FieldInfo>(m);
            Assert.Equal("TestField", m.Name);
            
            var i = new Test1();
            var n = GetField(() => i.TestField);
            Assert.IsAssignableFrom<FieldInfo>(n);
            Assert.Equal("TestField", n.Name);
            
            var o = GetField<Test1>(t => t.TestField);
            Assert.IsAssignableFrom<FieldInfo>(o);
            Assert.Equal("TestField", o.Name);
        }
        
        [Fact]
        public void GetProperty_Test()
        {
            var m = GetProperty<Test1, float>(t => t.TestProperty);
            Assert.IsAssignableFrom<PropertyInfo>(m);
            Assert.Equal("TestProperty", m.Name);
            
            var i = new Test1();
            var n = GetProperty(() => i.TestProperty);
            Assert.IsAssignableFrom<PropertyInfo>(n);
            Assert.Equal("TestProperty", n.Name);
            
            var o = GetProperty<Test1>(t => t.TestProperty);
            Assert.IsAssignableFrom<PropertyInfo>(o);
            Assert.Equal("TestProperty", o.Name);
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