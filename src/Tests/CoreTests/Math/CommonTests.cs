/*
CommonTests.cs

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

#pragma warning disable CS1591

using System;
using Xunit;
using M = TheXDS.MCART.Math.Common;

namespace TheXDS.MCART.Tests.Math
{
    public class CommonTests
    {
        [Fact]
        public void ClampTest()
        {
            Assert.Equal(2, M.Clamp(1 + 1, 0, 3));
            Assert.Equal(2, M.Clamp(1 + 3, 0, 2));
            Assert.Equal(2, M.Clamp(1 + 0, 2, 4));
        }

        [Fact]
        public void ClampTest_double()
        {
            Assert.Equal(2.0, M.Clamp(1.0+1.0,0.0,3.0));
            Assert.Equal(2.0, M.Clamp(1.0+3.0,0.0,2.0));
            Assert.Equal(2.0, M.Clamp(1.0-1.0,2.0,3.0));
            Assert.Equal(double.NaN, M.Clamp(double.NaN, 0.0,1.0));
            Assert.Equal(5.0, M.Clamp(double.PositiveInfinity, -5.0, 5.0));
            Assert.Equal(-5.0, M.Clamp(double.NegativeInfinity, -5.0, 5.0));
        }

        [Fact]
        public void ClampTest_float()
        {
            Assert.Equal(2.0f, M.Clamp(1.0f + 1.0f, 0.0f, 3.0f));
            Assert.Equal(2.0f, M.Clamp(1.0f + 3.0f, 0.0f, 2.0f));
            Assert.Equal(2.0f, M.Clamp(1.0f - 1.0f, 2.0f, 3.0f));
            Assert.Equal(float.NaN, M.Clamp(float.NaN, 0.0f, 1.0f));
            Assert.Equal(5.0f, M.Clamp(float.PositiveInfinity, -5.0f, 5.0f));
            Assert.Equal(-5.0f, M.Clamp(float.NegativeInfinity, -5.0f, 5.0f));
        }

        [Theory]
        [CLSCompliant(false)]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        [InlineData(3, 3)]
        [InlineData(0, 15)]
        [InlineData(-1, 14)]
        [InlineData(-2, 13)]
        [InlineData(14, 14)]
        [InlineData(15, 15)]
        [InlineData(16, 1)]
        [InlineData(17, 2)]
        public void WrapTest(int expression, int wrapped)
        {
            Assert.Equal((short)wrapped, M.Wrap((short)expression, (short)1, (short)15));
            Assert.Equal(wrapped, M.Wrap(expression, 1, 15));
            Assert.Equal(wrapped, M.Wrap(expression, 1L, 15L));
            Assert.Equal(wrapped, M.Wrap(expression, 1f, 15f));
            Assert.Equal(wrapped, M.Wrap(expression, 1.0, 15.0));
            Assert.Equal(wrapped, M.Wrap(expression, 1M, 15M));

            // Para tipos sin signo, no se deben realizar los tests con valores negativos.
            if (expression >= 0)
            {
                Assert.Equal((byte)wrapped, M.Wrap((byte)expression, (byte)1, (byte)15));
                Assert.Equal((char)wrapped, M.Wrap((char)expression, (char)1, (char)15));
            }
        }
    }
}
