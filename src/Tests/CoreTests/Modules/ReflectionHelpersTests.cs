/*
ReflectionHelpersTests.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Este archivo contiene todas las pruebas pertenecientes a la clase estática
TheXDS.MCART.Common.

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2020 César Andrés Morgan

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
using Xunit;
using System.Reflection;
using static TheXDS.MCART.ReflectionHelpers;
using TheXDS.MCART.Exceptions;

namespace TheXDS.MCART.Tests.Modules
{
    public class Test1
    {
        public virtual void Test() { }
    }
    public class Test2 : Test1
    {
        public override void Test() { }
        public void TestB() { }
    }

    public class ReflectionHelpersTests
    {
        [Fact]
        public void GetCallingMethodTest()
        {
            MethodBase TestMethod() => GetCallingMethod();            
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
            var m1 = t1.GetType().GetMethod("Test");
            var m2 = t2.GetType().GetMethod("Test");
            var m3 = t2.GetType().GetMethod("TestB");

            Assert.False(m1.IsOverriden(t1));
            Assert.True(m1.IsOverriden(t2));
            Assert.False(m2.IsOverriden(t2));

            Assert.Throws<ArgumentNullException>(() => m1.IsOverriden(null));
            Assert.Throws<ArgumentNullException>(() => ReflectionHelpers.IsOverriden(null, null));
            Assert.Throws<InvalidTypeException>(() => m2.IsOverriden(t1));

        }
    }
}