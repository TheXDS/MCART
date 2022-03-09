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
using System.Diagnostics.CodeAnalysis;
using TheXDS.MCART.Math;
using NUnit.Framework;
using M = TheXDS.MCART.Math.Common;

namespace TheXDS.MCART.Tests.Math
{
    public class CommonTests
    {
        [Test]
        public void OrIfInvalid_double_Test()
        {
            [ExcludeFromCodeCoverage]double Get() => 1.0;
            Assert.AreEqual(5.0, 5.0.OrIfInvalid(1.0));
            Assert.AreEqual(1.0, (5.0 / 0.0).OrIfInvalid(1.0));
            Assert.AreEqual(5.0, 5.0.OrIfInvalid(Get));
            Assert.AreEqual(1.0, (5.0 / 0.0).OrIfInvalid(() => 1.0));
        }
        
        [Test]
        public void OrIfInvalid_float_Test()
        {
            [ExcludeFromCodeCoverage]float Get() => 1.0f;
            Assert.AreEqual(5.0f, 5.0f.OrIfInvalid(1.0f));
            Assert.AreEqual(1.0f, (5.0f / 0.0f).OrIfInvalid(1.0f));
            Assert.AreEqual(5.0f, 5.0f.OrIfInvalid(Get));
            Assert.AreEqual(1.0f, (5.0f / 0.0f).OrIfInvalid(() => 1.0f));
        }

        [Test]
        public void ClampTest()
        {
            Assert.AreEqual(2, (1 + 1).Clamp(0, 3));
            Assert.AreEqual(2, (1 + 3).Clamp(0, 2));
            Assert.AreEqual(2, (1 + 0).Clamp(2, 4));
        }

        [Test]
        public void ClampTest_double()
        {
            Assert.AreEqual(2.0, (1.0 + 1.0).Clamp(0.0, 3.0));
            Assert.AreEqual(2.0, (1.0 + 3.0).Clamp(0.0, 2.0));
            Assert.AreEqual(2.0, (1.0 - 1.0).Clamp(2.0, 3.0));
            Assert.AreEqual(double.NaN, double.NaN.Clamp(0.0, 1.0));
            Assert.AreEqual(5.0, double.PositiveInfinity.Clamp(-5.0, 5.0));
            Assert.AreEqual(-5.0, double.NegativeInfinity.Clamp(-5.0, 5.0));
        }

        [Test]
        public void ClampTest_float()
        {
            Assert.AreEqual(2.0f, (1.0f + 1.0f).Clamp(0.0f, 3.0f));
            Assert.AreEqual(2.0f, (1.0f + 3.0f).Clamp(0.0f, 2.0f));
            Assert.AreEqual(2.0f, (1.0f - 1.0f).Clamp(2.0f, 3.0f));
            Assert.AreEqual(float.NaN, float.NaN.Clamp(0.0f, 1.0f));
            Assert.AreEqual(5.0f, float.PositiveInfinity.Clamp(-5.0f, 5.0f));
            Assert.AreEqual(-5.0f, float.NegativeInfinity.Clamp(-5.0f, 5.0f));
        }

        [Theory]
        [CLSCompliant(false)]
        [TestCase(1, 1)]
        [TestCase(2, 2)]
        [TestCase(3, 3)]
        [TestCase(0, 15)]
        [TestCase(-1, 14)]
        [TestCase(-2, 13)]
        [TestCase(14, 14)]
        [TestCase(15, 15)]
        [TestCase(16, 1)]
        [TestCase(17, 2)]
        public void WrapTest(int expression, int wrapped)
        {
            Assert.AreEqual((short)wrapped, ((short)expression).Wrap((short)1, (short)15));
            Assert.AreEqual(wrapped, expression.Wrap(1, 15));
            Assert.AreEqual(wrapped, M.Wrap(expression, 1L, 15L));
            Assert.AreEqual(wrapped, M.Wrap(expression, 1f, 15f));
            Assert.AreEqual(wrapped, M.Wrap(expression, 1.0, 15.0));
            Assert.AreEqual(wrapped, M.Wrap(expression, 1M, 15M));

            // Para tipos sin signo, no se deben realizar los tests con valores negativos.
            if (expression >= 0)
            {
                Assert.AreEqual((byte)wrapped, ((byte)expression).Wrap((byte)1, (byte)15));
                Assert.AreEqual((char)wrapped, ((char)expression).Wrap((char)1, (char)15));
            }
        }

        [Test]
        public void Wrap_WithFPNaNValues_Test()
        {
            Assert.AreEqual(float.NaN, float.NaN.Wrap(1, 2));
            Assert.AreEqual(double.NaN, double.NaN.Wrap(1, 2));
        }
    }
}
