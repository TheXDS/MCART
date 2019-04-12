/*
AlgebraTests.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2019 César Andrés Morgan

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

using Xunit;
using static TheXDS.MCART.Math.Algebra;

namespace TheXDS.MCART.Tests.Math
{
    public class AlgebraTests
    {
        [Fact]
        public void IsPrimeTest()
        {
            Assert.True(29.IsPrime());
            Assert.False(39.IsPrime());
        }
        [Fact]
        public void IsValidTest()
        {
            Assert.True(1.0.IsValid());
            Assert.False(float.NaN.IsValid());
            Assert.False(double.NegativeInfinity.IsValid());
        }
        [Fact]
        public void AreValidTest()
        {
            Assert.True(AreValid(1f, 2f, 3f, 4f, 5f));
            Assert.False(AreValid(1f, 2f, float.NaN, 4f, 5f));
        }
        [Fact]
        public void Nearest2PowTest()
        {
            Assert.Equal(512,Nearest2Pow(456));
        }
        [Fact]
        public void NearestMultiplyUpTest()
        {
            Assert.Equal(81.0, NearestMultiplyUp(50, 3));
        }
        [Fact]
        public void ArePositivesTest()
        {
            Assert.True(ArePositive(1, 2, 3, 4, 5));
            Assert.False(ArePositive(1, 2, 3, 0));
            Assert.False(ArePositive(1, 2, 3, -1));
        }
        [Fact]
        public void AreNegativesTest()
        {
            Assert.True(AreNegative(-1, -2, -3, -4, -5));
            Assert.False(AreNegative(-1, -2, -3, 0));
            Assert.False(AreNegative(-1, -2, -3, 1));
        }
        [Fact]
        public void AreZeroTest()
        {
            Assert.True(AreZero(0, 0, 0));
            Assert.False(AreZero(0, 1, 0));
        }
        [Fact]
        public void AreNotZeroTest()
        {
            Assert.True(AreNotZero(1, 2, 3));
            Assert.False(AreNotZero(1, 2, 0));
        }
        [Fact]
        public void IsWholeTest()
        {
            Assert.True(14.0.IsWhole());
            Assert.False(14.1.IsWhole());
            Assert.False(14.9.IsWhole());
        }
        [Fact]
        public void IsTwoPowTest()
        {
            Assert.True(IsTwoPow(2));
            Assert.True(IsTwoPow(16));
            Assert.True(IsTwoPow(64));
            Assert.True(IsTwoPow(256));
            Assert.True(IsTwoPow(65536));
            Assert.True(IsTwoPow(16777216));
            Assert.False(IsTwoPow(1));
            Assert.False(IsTwoPow(15));
            Assert.False(IsTwoPow(63));
            Assert.False(IsTwoPow(255));
            Assert.False(IsTwoPow(65535));
            Assert.False(IsTwoPow(16777215));
        }
    }
}
