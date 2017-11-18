//
//  CommonTest.cs
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
using static MCART.Common;

namespace CoreTests.Modules
{
    /// <summary>
    /// Contiene pruebas para la clase estática
    /// <see cref="Common"/>.
    /// </summary>
    [TestClass]
    public class CommonTest
    {
        /// <summary>
        /// Prueba del método <see cref="Common.Condense(System.Collections.Generic.IEnumerable{string}, string)"/>
        /// </summary>
        [TestMethod]
        public void CondenseTest()
        {
            Assert.AreEqual("A, B, C", (new string[] { "A", "B", "C" }).Condense(", "));
        }
        /// <summary>
        /// Prueba del método <see cref="Common.Left(string, int)"/>
        /// </summary>
        [TestMethod]
        public void LeftTest()
        {
            //Pruebas de sanidad de argumentos...
            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            {
                string tst1 = "Test".Left(5);
            });
            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            {
                string tst1 = "Test".Left(-1);
            });

            //Prueba de valores devueltos...
            Assert.AreEqual("Te", "Test".Left(2));
        }
        /// <summary>
        /// Prueba del método <see cref="Common.Right(string, int)"/>
        /// </summary>
        [TestMethod]
        public void RightTest()
        {
            //Pruebas de sanidad de argumentos...
            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            {
                string tst1 = "Test".Right(5);
            });
            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            {
                string tst1 = "Test".Right(-1);
            });

            //Prueba de valores devueltos...
            Assert.AreEqual("st", "Test".Right(2));
        }
        /// <summary>
        /// Prueba del método <see cref="Common.IsEmpty(string)"/>
        /// </summary>
        [TestMethod]
        public void IsEmptyTest()
        {
            Assert.IsTrue(String.Empty.IsEmpty());
            Assert.IsFalse("Test".IsEmpty());
            Assert.IsTrue((null as string).IsEmpty());
        }
        /// <summary>
        /// Prueba del método <see cref="AreAllEmpty(string[])"/>
        /// </summary>
        [TestMethod]
        public void AreAllEmptyTest()
        {
            Assert.IsTrue(AreAllEmpty(null, " ", string.Empty));
            Assert.IsFalse(AreAllEmpty(null, "Test", string.Empty));
        }
        /// <summary>
        /// Prueba del método <see cref="IsAnyEmpty(out int[], string[])"/>
        /// </summary>
        [TestMethod]
        public void IsAnyEmptyTest()
        {
            Assert.IsTrue(IsAnyEmpty("Test", String.Empty, ""));
            Assert.IsTrue(IsAnyEmpty(out int[] idxs, "Test", String.Empty, ""));
            Assert.IsTrue(idxs.Length == 2 && idxs[0] == 1 && idxs[1] == 2);
            Assert.IsFalse(IsAnyEmpty("T", "e", "s", "t"));
        }
        /// <summary>
        /// Prueba del método <see cref="Common.CountChars(string, char[])"/>
        /// </summary>
        [TestMethod]
        public void CountCharsTest()
        {
            Assert.AreEqual(5, "This is a test".CountChars('i', ' '));
            Assert.AreEqual(5, "This is a test".CountChars("i "));
        }
        /// <summary>
        /// Prueba del método <see cref="Common.ContainsAny(string, char[])"/>.
        /// </summary>
        [TestMethod]
        public void ContainsAnyTest()
        {
            Assert.IsTrue("Test".ContainsAny(out int idx, 'q', 't', 'a'));
            Assert.AreEqual(idx, 1);
            Assert.IsTrue("Test".ContainsAny(out int idx2,"t", "a"));
            Assert.AreEqual(idx2, 0);
            Assert.IsFalse("Test".ContainsAny(out int idx3, 'a', 'd'));
            Assert.AreEqual(idx3, -1);
        }
        /// <summary>
        /// Prueba del método <see cref="Common.CollectionListed(System.Collections.Generic.IEnumerable{string})"/>
        /// </summary>
        [TestMethod]
        public void CollectionListedTest()
        {
            string outp = (new string[]
            {
                "This" ,"is", "a", "test"
            }).CollectionListed();
            Assert.AreEqual($"This{Environment.NewLine}is{Environment.NewLine}a{Environment.NewLine}test{Environment.NewLine}", outp);

        }
        /// <summary>
        /// Prueba del método <see cref="Swap{T}(ref T, ref T)"/>.
        /// </summary>
        [TestMethod]
        public void SwapTest()
        {
            int a = 1, b = 2;
            Swap(ref a, ref b);
            Assert.AreEqual(a, 2);
            Assert.AreEqual(b, 1);
        }
        /// <summary>
        /// Prueba del método <see cref="Common.IsBetween{T}(T, T, T)"/>.
        /// </summary>
        [TestMethod]
        public void IsBetweenTest()
        {
            Assert.IsTrue((0.5).IsBetween(0.0, 1.0));
            Assert.IsTrue((0).IsBetween(0, 1));
            Assert.IsTrue((1.0f).IsBetween(0.0f, 1.0f));
            Assert.IsFalse(((byte)2).IsBetween((byte)0, (byte)1));
            Assert.IsFalse(((sbyte)-50).IsBetween((sbyte)0, (sbyte)1));
        }



        /// <summary>
        /// Prueba del método <see cref="Common.ToHex(byte[])"/>
        /// </summary>
        [TestMethod]
        public void ToHexTest1()
        {
            Assert.AreEqual("F0", ((byte)240).ToHex());
        }
        /// <summary>
        /// Prueba del método <see cref="Common.ToHex(byte)"/>
        /// </summary>
        [TestMethod]
        public void ToHexTest2()
        {
            Assert.AreEqual("0A0B0C", (new byte[] { 10, 11, 12 }).ToHex());
        }
    }
}