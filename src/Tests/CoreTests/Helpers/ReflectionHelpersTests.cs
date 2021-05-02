/*
ReflectionHelpersTests.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Este archivo contiene todas las pruebas pertenecientes a la clase estática
TheXDS.MCART.Common.

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
        public void GetMethodTest()
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
        }

        [Fact]
        public void GetCallingMethodTest()
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
        public void IsOverridenTest()
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
        public void IsOverrideTest()
        {
            var m1 = GetMethod<Test1, Action>(t => t.Test);
            var m2 = GetMethod<Test2, Action>(t => t.Test);

            Assert.True(m2.IsOverride());
            Assert.False(m1.IsOverride());
            Assert.Throws<ArgumentNullException>(() => ReflectionHelpers.IsOverride(null!));
        }

        [Fact]
        public void GetEntryPointTest()
        {
            Assert.NotNull(GetEntryPoint());
        }

        [Fact]
        public void GetEntryAssemblyTest()
        {
            Assert.NotNull(GetEntryAssembly());
        }

        public class Test1
        {
            public virtual void Test() { }

            public virtual void TestC<T>() { }
        }

        public class Test2 : Test1
        {
            public override void Test() { }

            public void TestB() { }

            public override void TestC<T>() { }
        }
    }
}