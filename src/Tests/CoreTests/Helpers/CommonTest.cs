/*
CommonTest.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Este archivo contiene todas las pruebas pertenecientes a la clase estática
TheXDS.MCART.Common.

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
using System.Collections.Generic;
using System.Linq;
using System.Security;
using TheXDS.MCART.Helpers;
using TheXDS.MCART.Types;
using NUnit.Framework;
using static TheXDS.MCART.Helpers.Common;
using static TheXDS.MCART.Types.Extensions.SecureStringExtensions;

namespace TheXDS.MCART.Tests.Helpers
{
    public class CommonTest
    {
        [Test]
        public void ReadCharsTest()
        {
            var s = new SecureString();
            s.AppendChar('T');
            s.AppendChar('e');
            s.AppendChar('s');
            s.AppendChar('t');
            s.MakeReadOnly();
            Assert.AreEqual("Test".ToCharArray(), s.ReadChars());
        }

        [Test]
        public void SequenceTest()
        {
            Assert.AreEqual(
                new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 },
                Sequence(10));

            Assert.AreEqual(
                new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 },
                Sequence(1, 10));

            Assert.AreEqual(
                new[] { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 },
                Sequence(10, 1));

            Assert.AreEqual(
                new[] { 1, 3, 5, 7, 9 },
                Sequence(1, 10, 2));

            Assert.AreEqual(
                new[] { 10, 8, 6, 4, 2 },
                Sequence(10, 1, 2));

            Assert.Throws<ArgumentOutOfRangeException>(() => Sequence(1, 10, 0).ToList());
        }

        [Test]
        public void FindConverterTest()
        {
            Assert.NotNull(FindConverter<int>());
            Assert.Null(FindConverter<Exception, Enum>());
            Assert.Null(FindConverter<Exception>());
            Assert.NotNull(FindConverter(typeof(int)));
            Assert.Null(FindConverter(typeof(Exception)));
            Assert.Null(FindConverter(typeof(Exception), typeof(Enum)));
        }

        [Test]
        public void FlipEndianessTest_Char()
        {
            Assert.AreEqual((char)0x0102, ((char)0x0201).FlipEndianess());
        }

        [Test]
        public void FlipEndianessTest_Int16()
        {
            Assert.AreEqual((short)0x0102, ((short)0x0201).FlipEndianess());
        }

        [Test]
        public void FlipEndianessTest_Int32()
        {
            Assert.AreEqual(0x01020304, 0x04030201.FlipEndianess());
        }

        [Test]
        public void FlipEndianessTest_Int64()
        {
            Assert.AreEqual(0x0102030405060708, 0x0807060504030201.FlipEndianess());
        }

        [Test]
        public void FlipEndianessTest_Single()
        {
            Assert.AreEqual(3.02529E-39f, 123456f.FlipEndianess());
        }

        [Test]
        public void FlipEndianessTest_Double()
        {
            Assert.True(System.Math.PI.FlipEndianess().IsBetween(3.20737563067636E-192, 3.20737563067638E-192));            
        }

        [Test]
        public void AreAllEmptyTest()
        {
            Assert.True(AllEmpty(null, " ", string.Empty));
            Assert.False(AllEmpty(null, "Test", string.Empty));
        }

        [Test]
        public void CollectionListedTest()
        {
            var outp = new[]
            {
                "This", "is", "a", "test"
            }.Listed();
            Assert.AreEqual(
                $"This{Environment.NewLine}is{Environment.NewLine}a{Environment.NewLine}test",
                outp);
        }

        [Test]
        public void IsAnyEmptyTest()
        {
            Assert.True(AnyEmpty("Test", string.Empty, ""));
            Assert.False(AnyEmpty("T", "e", "s", "t"));

            Assert.True(AnyEmpty(out IEnumerable<int> i1, "Test", string.Empty, ""));
            Assert.AreEqual(new[] { 1, 2 }, i1);

            Assert.True(AnyEmpty(out IEnumerable<int> i2, null, string.Empty, ""));
            Assert.AreEqual(new[] { 0, 1, 2 }, i2);

            Assert.False(AnyEmpty(out IEnumerable<int> i3, "T", "e", "s", "t"));
            Assert.AreEqual(Array.Empty<int>(), i3);
        }

        [Test]
        public void IsBetweenTest()
        {
            Assert.True(0.5.IsBetween(0.0, 1.0));
            Assert.True(0.IsBetween(0, 1));
            Assert.True(1.0f.IsBetween(0.0f, 1.0f));
            Assert.False(((byte)2).IsBetween((byte)0, (byte)1));
            Assert.False(((sbyte)-50).IsBetween((sbyte)0, (sbyte)1));
            Assert.True("b".IsBetween("a", "c"));
            Assert.False("d".IsBetween("a", "c"));
            Assert.True('b'.IsBetween(new Range<char>('a', 'c')));
            Assert.False('d'.IsBetween(new Range<char>('a', 'c')));
            Assert.True(((double?) 0.5).IsBetween(0.0, 1.0));
            Assert.True(((double?) 0.0).IsBetween(0.0, 1.0,true));
            Assert.False(((double?) 0.0).IsBetween(0.0, 1.0,false));
            Assert.False(((double?) null).IsBetween(0.0, 1.0));
            Assert.True(((double?) 0.5).IsBetween(new Range<double>(0.0, 1.0)));
        }

        [Test]
        public void IsBetween_WithInclusionFlags_Test()
        {
            Assert.True(0.0.IsBetween(0.0, 1.0, true));
            Assert.False(0.0.IsBetween(0.0, 1.0, false));
            Assert.True(1.0.IsBetween(0.0, 1.0, true));
            Assert.False(1.0.IsBetween(0.0, 1.0, false));
            Assert.True(double.Epsilon.IsBetween(0.0, 1.0, false));

            Assert.False(0.0.IsBetween(0.0, 1.0, false, false));
            Assert.False(0.0.IsBetween(0.0, 1.0, false, true));
            Assert.True(0.0.IsBetween(0.0, 1.0, true, false));
            Assert.True(0.0.IsBetween(0.0, 1.0, true, true));

            Assert.False(1.0.IsBetween(0.0, 1.0, false, false));
            Assert.True(1.0.IsBetween(0.0, 1.0, false, true));
            Assert.False(1.0.IsBetween(0.0, 1.0, true, false));
            Assert.True(1.0.IsBetween(0.0, 1.0, true, true));

            double? zero = 0.0;
            double? one = 1.0;

            Assert.True(zero.IsBetween(0.0, 1.0, true));
            Assert.False(zero.IsBetween(0.0, 1.0, false));
            Assert.True(one.IsBetween(0.0, 1.0, true));
            Assert.False(one.IsBetween(0.0, 1.0, false));
            Assert.True(((double?)double.Epsilon).IsBetween(0.0, 1.0, false));

            Assert.False(zero.IsBetween(0.0, 1.0, false, false));
            Assert.False(zero.IsBetween(0.0, 1.0, false, true));
            Assert.True(zero.IsBetween(0.0, 1.0, true, false));
            Assert.True(zero.IsBetween(0.0, 1.0, true, true));

            Assert.False(one.IsBetween(0.0, 1.0, false, false));
            Assert.True(one.IsBetween(0.0, 1.0, false, true));
            Assert.False(one.IsBetween(0.0, 1.0, true, false));
            Assert.True(one.IsBetween(0.0, 1.0, true, true));
        }

        [Test]
        public void ReadBytesTest()
        {
            var s = new SecureString();
            s.AppendChar('@');
            s.MakeReadOnly();
            var r = s.ReadBytes();
            Assert.AreEqual(new byte[] { 64, 0 }, r);
        }

        [Test]
        public void ReadInt16Test()
        {
            var s = new SecureString();
            s.AppendChar('@');
            s.MakeReadOnly();
            Assert.AreEqual((short)64, s.ReadInt16()[0]);
        }

        [Test]
        public void ReadTest()
        {
            var s = new SecureString();
            s.AppendChar('T');
            s.AppendChar('e');
            s.AppendChar('s');
            s.AppendChar('t');
            s.MakeReadOnly();
            Assert.AreEqual("Test", s.Read());
        }

        [Test]
        public void SwapTest()
        {
            int a = 1, b = 2;
            Swap(ref a, ref b);
            Assert.AreEqual(2, a);
            Assert.AreEqual(1, b);
        }

        [Test]
        public void ToBits_WithInt64_Test()
        {
            var c = new bool[sizeof(long) * 8];
            Assert.AreEqual(c, 0L.ToBits());

            c[1] = true; c[3] = true;
            Assert.AreEqual(c, 10L.ToBits());
        }

        [Test]
        public void ToBits_WithInt32_Test()
        {
            var c = new bool[sizeof(int) * 8];
            Assert.AreEqual(c, 0.ToBits());

            c[1] = true; c[3] = true;
            Assert.AreEqual(c, 10.ToBits());
        }

        [Test]
        public void ToBits_WithInt16_Test()
        {
            var c = new bool[sizeof(short) * 8];
            Assert.AreEqual(c, ((short)0).ToBits());

            c[1] = true; c[3] = true;
            Assert.AreEqual(c, ((short)10).ToBits());
        }

        [Test]
        public void ToBits_WithInt8_Test()
        {
            var c = new bool[sizeof(byte) * 8];
            Assert.AreEqual(c, ((byte)0).ToBits());

            c[1] = true; c[3] = true;
            Assert.AreEqual(c, ((byte)10).ToBits());
        }

        [CLSCompliant(false)]
        [TestCase(0, 0)]
        [TestCase(2, 10)]
        [TestCase(3, 11)]
        [TestCase(2, 12)]
        [TestCase(7, 127)]
        [TestCase(1, 128)]
        [TestCase(8, 255)]
        public void BitCount_Int64_Test(byte bitCount, long value)
        {
            Assert.AreEqual(bitCount, value.BitCount());
        }

        [CLSCompliant(false)]
        [TestCase(0, 0)]
        [TestCase(2, 10)]
        [TestCase(3, 11)]
        [TestCase(2, 12)]
        [TestCase(7, 127)]
        [TestCase(1, 128)]
        [TestCase(8, 255)]
        public void BitCount_Int32_Test(byte bitCount, int value)
        {
            Assert.AreEqual(bitCount, value.BitCount());
        }

        [CLSCompliant(false)]
        [TestCase(0, 0)]
        [TestCase(2, 10)]
        [TestCase(3, 11)]
        [TestCase(2, 12)]
        [TestCase(7, 127)]
        [TestCase(1, 128)]
        [TestCase(8, 255)]
        public void BitCount_Int16_Test(byte bitCount, short value)
        {
            Assert.AreEqual(bitCount, value.BitCount());
        }

        [CLSCompliant(false)]
        [TestCase(0, 0)]
        [TestCase(2, 10)]
        [TestCase(3, 11)]
        [TestCase(2, 12)]
        [TestCase(7, 127)]
        [TestCase(1, 128)]
        [TestCase(8, 255)]
        public void BitCount_Int8_Test(byte bitCount, byte value)
        {
            Assert.AreEqual(bitCount, value.BitCount());
        }

        [Test]
        public void ToHexTest1()
        {
            Assert.AreEqual("F0", ((byte)240).ToHex());
        }

        [Test]
        public void ToHexTest2()
        {
            Assert.AreEqual("0A0B0C", new byte[] { 10, 11, 12 }.ToHex());
        }

        [CLSCompliant(false)]
        [TestCase(1000, ByteUnitType.Binary, "1000 Bytes")]
        [TestCase(1000, ByteUnitType.Decimal, "1.0 KB")]
        [TestCase(100000, (ByteUnitType) 255, "100000 Bytes")]
        [TestCase(1100000, ByteUnitType.BinaryLong, "1.1 Mebibytes")]
        [TestCase(1048576, ByteUnitType.DecimalLong, "1.0 Megabytes")]
        public void ByteUnitsTest_Long_ByteUnitType(long bytes, ByteUnitType unit, string result)
        {
            Assert.AreEqual(result, TheXDS.MCART.Helpers.Common.ByteUnits(bytes, unit));
        }
        
        [CLSCompliant(false)]
        [TestCase(1000, "1000 Bytes")]
        [TestCase(1024, "1.0 KiB")]
        [TestCase(1536, "1.5 KiB")]
        [TestCase(1768, "1.7 KiB")]
        [TestCase(1048576, "1.0 MiB")]
        [TestCase(1150976, "1.1 MiB")]
        [TestCase(1073741824, "1.0 GiB")]
        [TestCase(1099511627776L, "1.0 TiB")]
        [TestCase(1125899906842624L, "1.0 PiB")]
        [TestCase(1152921504606846976L, "1.0 EiB")]
        public void ByteUnitsTest_Long(long bytes, string result)
        {
            Assert.AreEqual(result, bytes.ByteUnits());
        }

        [Test]
        public void AnyEmptyTest()
        {
            var array = new [] { "0", null, "2", "3", null, "5" };
            Assert.False(new[] { "0", "1", "2" }.AnyEmpty(out int i));
            Assert.AreEqual(-1, i);
            Assert.True(array.AnyEmpty(out int index));
            Assert.AreEqual(1, index);
            Assert.True(array.AnyEmpty(out IEnumerable<int> indexes));
            Assert.AreEqual(new[] { 1, 4 }, indexes.ToArray());

            Assert.True(AnyEmpty(out int idx, array));
            Assert.AreEqual(1, idx);
        }
    }
}