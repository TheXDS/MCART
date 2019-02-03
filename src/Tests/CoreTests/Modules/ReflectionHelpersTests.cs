/*
CommonTest.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Este archivo contiene todas las pruebas pertenecientes a la clase estática
TheXDS.MCART.Common.

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright (c) 2011 - 2019 César Andrés Morgan

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
using Xunit;
using System.Reflection;
using static TheXDS.MCART.ReflectionHelpers;

namespace TheXDS.MCART.Tests.Modules
{
    public class ReflectionHelpersTests
    {
        [Fact]
        public void GetCallingMethodTest()
        {
            MethodBase TestMethod()
            {
                return GetCallingMethod();
            }

            Assert.Equal(MethodBase.GetCurrentMethod(), TestMethod());
            Assert.Null(GetCallingMethod(int.MaxValue-1));
            Assert.Throws<OverflowException>(() => GetCallingMethod(int.MaxValue));
            Assert.Throws<ArgumentOutOfRangeException>(() => GetCallingMethod(0));
        }
    }
}