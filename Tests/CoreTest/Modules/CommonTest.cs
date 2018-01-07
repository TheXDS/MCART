//
//  CommonTest.cs
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

using TheXDS.MCART;
using System;
using System.Security;
using Xunit;
using static MCART.Common;

namespace MSTest.Modules
{
    /// <summary>
    /// Contiene pruebas para la clase estática <see cref="Common"/>.
    /// </summary>
    public class CommonTest
    {
        /// <summary>
        /// Prueba del método <see cref="Common.Condense(System.Collections.Generic.IEnumerable{string}, string)"/>
        /// </summary>
        [Fact]
        public void CondenseTest()
        {
            Assert.Equal("A B C", (new string[] { "A", "B", "C" }).Condense());
            Assert.Equal("A, B, C", (new string[] { "A", "B", "C" }).Condense(", "));
        }
        /// <summary>
        /// Prueba del método <see cref="Common.Left(string, int)"/>
        /// </summary>
        [Fact]
        public void LeftTest()
        {
            //Pruebas de sanidad de argumentos...
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                string tst1 = "Test".Left(5);
            });
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                string tst1 = "Test".Left(-1);
            });

            //Prueba de valores devueltos...
            Assert.Equal("Te", "Test".Left(2));
        }
        /// <summary>
        /// Prueba del método <see cref="Common.Right(string, int)"/>
        /// </summary>
        [Fact]
        public void RightTest()
        {
            //Pruebas de sanidad de argumentos...
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                string tst1 = "Test".Right(5);
            });
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                string tst1 = "Test".Right(-1);
            });

            //Prueba de valores devueltos...
            Assert.Equal("st", "Test".Right(2));
        }
        /// <summary>
        /// Prueba del método <see cref="Common.IsEmpty(string)"/>
        /// </summary>
        [Fact]
        public void IsEmptyTest()
        {
            Assert.True(String.Empty.IsEmpty());
            Assert.False("Test".IsEmpty());
            Assert.True((null as string).IsEmpty());
        }
        /// <summary>
        /// Prueba del método <see cref="AreAllEmpty(string[])"/>
        /// </summary>
        [Fact]
        public void AreAllEmptyTest()
        {
            Assert.True(AreAllEmpty(null, " ", string.Empty));
            Assert.False(AreAllEmpty(null, "Test", string.Empty));
        }
        /// <summary>
        /// Prueba del método <see cref="IsAnyEmpty(out int[], string[])"/>
        /// </summary>
        [Fact]
        public void IsAnyEmptyTest()
        {
            Assert.True(IsAnyEmpty("Test", String.Empty, ""));
            Assert.True(IsAnyEmpty(out int[] idxs, "Test", String.Empty, ""));
            Assert.True(idxs.Length == 2 && idxs[0] == 1 && idxs[1] == 2);
            Assert.False(IsAnyEmpty("T", "e", "s", "t"));
        }
        /// <summary>
        /// Prueba del método <see cref="Common.CountChars(string, char[])"/>
        /// </summary>
        [Fact]
        public void CountCharsTest()
        {
            Assert.Equal(5, "This is a test".CountChars('i', ' '));
            Assert.Equal(5, "This is a test".CountChars("i "));
        }
        /// <summary>
        /// Prueba del método <see cref="Common.ContainsAny(string, char[])"/>.
        /// </summary>
        [Fact]
        public void ContainsAnyTest()
        {
            Assert.True("Test".ContainsAny(out int idx, 'q', 't', 'a'));
            Assert.Equal(1, idx);
            Assert.True("Test".ContainsAny(out int idx2, "t", "a"));
            Assert.Equal(0, idx2);
            Assert.False("Test".ContainsAny(out int idx3, 'a', 'd'));
            Assert.Equal(-1, idx3);
        }
        /// <summary>
        /// Prueba del método <see cref="Common.Listed(System.Collections.Generic.IEnumerable{string})"/>
        /// </summary>
        [Fact]
        public void CollectionListedTest()
        {
            string outp = (new string[]
            {
                "This" ,"is", "a", "test"
            }).Listed();
            Assert.Equal($"This{Environment.NewLine}is{Environment.NewLine}a{Environment.NewLine}test{Environment.NewLine}", outp);

        }
        /// <summary>
        /// Prueba del método <see cref="Swap{T}(ref T, ref T)"/>.
        /// </summary>
        [Fact]
        public void SwapTest()
        {
            int a = 1, b = 2;
            Swap(ref a, ref b);
            Assert.Equal(2, a);
            Assert.Equal(1, b);
        }
        /// <summary>
        /// Prueba del método <see cref="Common.IsBetween{T}(T, T, T)"/>.
        /// </summary>
        [Fact]
        public void IsBetweenTest()
        {
            Assert.True((0.5).IsBetween(0.0, 1.0));
            Assert.True((0).IsBetween(0, 1));
            Assert.True((1.0f).IsBetween(0.0f, 1.0f));
            Assert.False(((byte)2).IsBetween((byte)0, (byte)1));
            Assert.False(((sbyte)-50).IsBetween((sbyte)0, (sbyte)1));
        }
        /// <summary>
        /// Prueba del método <see cref="Common.ToHex(byte[])"/>
        /// </summary>
        [Fact]
        public void ToHexTest1()
        {
            Assert.Equal("F0", ((byte)240).ToHex());
        }
        /// <summary>
        /// Prueba del método <see cref="Common.ToHex(byte)"/>
        /// </summary>
        [Fact]
        public void ToHexTest2()
        {
            Assert.Equal("0A0B0C", (new byte[] { 10, 11, 12 }).ToHex());
        }

        [Fact]
        public void ReadStringTest()
        {
            var s = new SecureString();
            s.AppendChar('T');
            s.AppendChar('e');
            s.AppendChar('s');
            s.AppendChar('t');
            s.MakeReadOnly();
            Assert.Equal("Test", s.Read());
        }
        [Fact]
        public void Read16Test()
        {
            var s = new SecureString();
            s.AppendChar('@');
            s.MakeReadOnly();
            Assert.Equal((short)64, s.ReadInt16()[0]);
        }
        [Fact]
        public void Read8Test()
        {
            var s = new SecureString();
            s.AppendChar('@');
            s.MakeReadOnly();
            var r = s.ReadBytes();
            Assert.True(64 == r[0] && 0 == r[1]);
        }

    }
}