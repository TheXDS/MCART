//
//  MathTest.cs
//
//  This file is part of MCART
//
//  Author:
//       César Andrés Morgan <xds_xps_ivx@hotmail.com>
//
//  Copyright (c) 2011 - 2017 César Andrés Morgan
//
//  MCART is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  MCART is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MCART;
using static MCART.Math;

namespace CoreTests.Modules
{
    [TestClass]
    public class MathTest
    {
        [TestMethod]
        public void IsPrimeTest()
        {
            Assert.IsTrue((29).IsPrime());
            Assert.IsFalse((39).IsPrime());
        }
        [TestMethod]
        public void IsValidTest()
        {
            Assert.IsTrue((1.0).IsValid());
            Assert.IsFalse(float.NaN.IsValid());
            Assert.IsFalse(double.NegativeInfinity.IsValid());
        }
        [TestMethod]
        public void AreValidTest()
        {
            Assert.IsTrue(AreValid(1f, 2f, 3f, 4f, 5f));
            Assert.IsFalse(AreValid(1f, 2f, float.NaN, 4f, 5f));

        }
        [TestMethod]
        public void Nearest2PowTest()
        {
            Assert.AreEqual<ulong>(Nearest2Pow(456), 512);
        }
        [TestMethod]
        public void NearestMultiplyUpTest()
        {
            Assert.AreEqual(NearestMultiplyUp(50, 3), 81.0);
        }
        [TestMethod]
        public void ArePositivesTest()
        {
            Assert.IsTrue(ArePositives(1, 2, 3, 4, 5));
            Assert.IsFalse(ArePositives(1, 2, 3, 0));
            Assert.IsFalse(ArePositives(1, 2, 3, -1));
        }
        [TestMethod]
        public void AreNegativesTest()
        {
            Assert.IsTrue(AreNegatives(-1, -2, -3, -4, -5));
            Assert.IsFalse(AreNegatives(-1, -2, -3, 0));
            Assert.IsFalse(AreNegatives(-1, -2, -3, 1));
        }
        [TestMethod]
        public void AreZeroTest()
        {
            Assert.IsTrue(AreZero(0, 0, 0));
            Assert.IsFalse(AreZero(0, 1, 0));
        }
        [TestMethod]
        public void AreNotZeroTest()
        {
            Assert.IsTrue(AreNotZero(1, 2, 3));
            Assert.IsFalse(AreNotZero(1, 2, 0));
        }
        [TestMethod]
        public void ClampTest()
        {
            Assert.AreEqual((5 + 10).Clamp(10), 10);
            Assert.AreEqual((5 - 10).Clamp(10), 0);
            Assert.AreEqual((5 + 10).Clamp(1, 10), 10);
            Assert.AreEqual((5 - 10).Clamp(1, 10), 1);

#if FloatDoubleSpecial
            Assert.IsTrue(float.IsNaN(float.NaN.Clamp(1, 10)));
            Assert.AreEqual((13f).Clamp(1, 10), 10f);
            Assert.AreEqual((-5f).Clamp(1, 10), 1f);
            Assert.AreEqual((-5f).Clamp(10), -5f);
            Assert.AreEqual(float.PositiveInfinity.Clamp(1, 10), 10f);
            Assert.AreEqual(float.NegativeInfinity.Clamp(1, 10), 1f);

            Assert.IsTrue(double.IsNaN(double.NaN.Clamp(1, 10)));
            Assert.AreEqual((13.0).Clamp(1, 10), 10.0);
            Assert.AreEqual((-5.0).Clamp(1, 10), 1.0);
            Assert.AreEqual((-5.0).Clamp(10), -5.0);
            Assert.AreEqual(double.PositiveInfinity.Clamp(1, 10), 10.0);
            Assert.AreEqual(double.NegativeInfinity.Clamp(1, 10), 1.0);
#endif
        }
        [TestMethod]
        public void WrapTest()
        {
            Assert.AreEqual(5f, (16f).Wrap(5, 15));
            Assert.AreEqual(6f, (17f).Wrap(5, 15));
            Assert.AreEqual(15f, (4f).Wrap(5, 15));
            Assert.AreEqual(14f, (3f).Wrap(5, 15));
            Assert.AreEqual(8f, (8f).Wrap(5, 15));            
        }
        [TestMethod]
        public void IsWholeTest()
        {
            Assert.IsTrue((14.0).IsWhole());
            Assert.IsFalse((14.1).IsWhole());
            Assert.IsFalse((14.9).IsWhole());
        }
    }
}
