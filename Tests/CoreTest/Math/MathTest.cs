//
//  MathTest.cs
//
//  This file is part of Morgan's CLR Advanced Runtime (MCART)
//
//  Author:
//       César Andrés Morgan <xds_xps_ivx@hotmail.com>
//
//  Copyright (c) 2011 - 2018 César Andrés Morgan
//
//  Morgan's CLR Advanced Runtime (MCART) is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  Morgan's CLR Advanced Runtime (MCART) is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using TheXDS.MCART.Math;
using Xunit;

#if FloatDoubleSpecial

#endif

namespace CoreTest.Math
{
    public class MathTest
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
            Assert.True(Algebra.AreValid(1f, 2f, 3f, 4f, 5f));
            Assert.False(Algebra.AreValid(1f, 2f, float.NaN, 4f, 5f));

        }
        [Fact]
        public void Nearest2PowTest()
        {
            Assert.Equal(512,Algebra.Nearest2Pow(456));
        }
        [Fact]
        public void NearestMultiplyUpTest()
        {
            Assert.Equal(81.0, Algebra.NearestMultiplyUp(50, 3));
        }
        [Fact]
        public void ArePositivesTest()
        {
            Assert.True(Algebra.ArePositive(1, 2, 3, 4, 5));
            Assert.False(Algebra.ArePositive(1, 2, 3, 0));
            Assert.False(Algebra.ArePositive(1, 2, 3, -1));
        }
        [Fact]
        public void AreNegativesTest()
        {
            Assert.True(Algebra.AreNegative(-1, -2, -3, -4, -5));
            Assert.False(Algebra.AreNegative(-1, -2, -3, 0));
            Assert.False(Algebra.AreNegative(-1, -2, -3, 1));
        }
        [Fact]
        public void AreZeroTest()
        {
            Assert.True(Algebra.AreZero(0, 0, 0));
            Assert.False(Algebra.AreZero(0, 1, 0));
        }
        [Fact]
        public void AreNotZeroTest()
        {
            Assert.True(Algebra.AreNotZero(1, 2, 3));
            Assert.False(Algebra.AreNotZero(1, 2, 0));
        }
        [Fact]
        public void ClampTest()
        {
            Assert.Equal(10, (5 + 10).Clamp(10));
            Assert.Equal(0, (5 - 10).Clamp(10));
            Assert.Equal(10, (5 + 10).Clamp(1, 10));
            Assert.Equal(1, (5 - 10).Clamp(1, 10));

#if FloatDoubleSpecial
            Assert.True(float.IsNaN(float.NaN.Clamp(1, 10)));
            Assert.Equal(10f, 13f.Clamp(1, 10));
            Assert.Equal(1f, (-5f).Clamp(1, 10));
            Assert.Equal((-5f).Clamp(10), -5f);
            Assert.Equal(10f, float.PositiveInfinity.Clamp(1, 10));
            Assert.Equal(1f, float.NegativeInfinity.Clamp(1, 10));

            Assert.True(double.IsNaN(double.NaN.Clamp(1, 10)));
            Assert.Equal(10.0, 13.0.Clamp(1, 10));
            Assert.Equal(1.0, (-5.0).Clamp(1, 10));
            Assert.Equal((-5.0).Clamp(10), -5.0);
            Assert.Equal(10.0, double.PositiveInfinity.Clamp(1, 10));
            Assert.Equal(1.0, double.NegativeInfinity.Clamp(1, 10));
#endif
        }
        [Fact]
        public void WrapTest()
        {
            Assert.Equal(5f, 16f.Wrap(5, 15));
            Assert.Equal(6f, 17f.Wrap(5, 15));
            Assert.Equal(15f, 4f.Wrap(5, 15));
            Assert.Equal(14f, 3f.Wrap(5, 15));
            Assert.Equal(8f, 8f.Wrap(5, 15));
        }
        [Fact]
        public void IsWholeTest()
        {
            Assert.True(14.0.IsWhole());
            Assert.False(14.1.IsWhole());
            Assert.False(14.9.IsWhole());
        }
    }
}
