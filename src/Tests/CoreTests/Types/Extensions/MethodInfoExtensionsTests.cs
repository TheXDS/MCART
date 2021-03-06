﻿/*
MethodInfoExtensionsTests.cs

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
using TheXDS.MCART.Helpers;
using Xunit;
using static TheXDS.MCART.Types.Extensions.MethodInfoExtensions;

namespace TheXDS.MCART.Tests.Types.Extensions
{
    public class MethodInfoExtensionsTests
    {
        private static string TestStatic() => "TestStatic";
        private string TestInstance() => "TestInstance";

        [Fact]
        public void ToDelegateTest()
        {
            var ts = ReflectionHelpers.GetMethod<Func<string>>(() => TestStatic).ToDelegate<Func<string>>();
            var ti = ReflectionHelpers.GetMethod<Func<string>>(() => TestInstance).ToDelegate<Func<string>>(this);
            
            Assert.NotNull(ts);
            Assert.IsType<Func<string>>(ts);
            Assert.Equal("TestStatic", ts!.Invoke());
            
            Assert.NotNull(ti);
            Assert.IsType<Func<string>>(ti);
            Assert.Equal("TestInstance", ti!.Invoke());
        }

        [Fact]
        public void ToDelegate_Contract_Test()
        {
            Assert.Throws<MemberAccessException>(() =>
                ReflectionHelpers.GetMethod<Func<string>>(() => TestStatic).ToDelegate<Func<string>>(this));
            Assert.Throws<MemberAccessException>(() =>
                ReflectionHelpers.GetMethod<Func<string>>(() => TestInstance).ToDelegate<Func<string>>());
        }

        [Fact]
        public void IsVoidTest()
        {
            Assert.True(ReflectionHelpers.GetMethod<Action>(() => IsVoidTest).IsVoid());
            Assert.False(ReflectionHelpers.GetMethod<Func<object, object, bool>>(() => Equals).IsVoid());
        }
    }
}