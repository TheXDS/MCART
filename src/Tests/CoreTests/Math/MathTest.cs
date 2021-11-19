/*
MathTest.cs

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

using TheXDS.MCART.Math;
using NUnit.Framework;

namespace TheXDS.MCART.Tests.Math
{
    public class MathTests
    {
        [Test]
        public void ClampTest()
        {
            Assert.AreEqual(10, (5 + 10).Clamp(10));
            Assert.AreEqual(0, (5 - 10).Clamp(10));
            Assert.AreEqual(10, (5 + 10).Clamp(1, 10));
            Assert.AreEqual(1, (5 - 10).Clamp(1, 10));

#if FloatDoubleSpecial
            Assert.True(float.IsNaN(float.NaN.Clamp(1, 10)));
            Assert.AreEqual(10f, 13f.Clamp(1, 10));
            Assert.AreEqual(1f, (-5f).Clamp(1, 10));
            Assert.AreEqual((-5f).Clamp(10), -5f);
            Assert.AreEqual(10f, float.PositiveInfinity.Clamp(1, 10));
            Assert.AreEqual(1f, float.NegativeInfinity.Clamp(1, 10));

            Assert.True(double.IsNaN(double.NaN.Clamp(1, 10)));
            Assert.AreEqual(10.0, 13.0.Clamp(1, 10));
            Assert.AreEqual(1.0, (-5.0).Clamp(1, 10));
            Assert.AreEqual((-5.0).Clamp(10), -5.0);
            Assert.AreEqual(10.0, double.PositiveInfinity.Clamp(1, 10));
            Assert.AreEqual(1.0, double.NegativeInfinity.Clamp(1, 10));
#endif
        }

        [Test]
        public void WrapTest()
        {
            Assert.AreEqual(5f, 16f.Wrap(5, 15));
            Assert.AreEqual(6f, 17f.Wrap(5, 15));
            Assert.AreEqual(15f, 4f.Wrap(5, 15));
            Assert.AreEqual(14f, 3f.Wrap(5, 15));
            Assert.AreEqual(8f, 8f.Wrap(5, 15));

            Assert.AreEqual(5, 16.Wrap(5, 15));
            Assert.AreEqual(6, 17.Wrap(5, 15));
            Assert.AreEqual(15, 4.Wrap(5, 15));
            Assert.AreEqual(14, 3.Wrap(5, 15));
            Assert.AreEqual(8, 8.Wrap(5, 15));
        }
    }
}
