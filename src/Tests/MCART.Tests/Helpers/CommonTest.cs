/*
CommonTest.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Este archivo contiene todas las pruebas pertenecientes a la clase estática
TheXDS.MCART.Common.

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2023 César Andrés Morgan

Permission is hereby granted, free of charge, to any person obtaining a copy of
this software and associated documentation files (the "Software"), to deal in
the Software without restriction, including without limitation the rights to
use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
of the Software, and to permit persons to whom the Software is furnished to do
so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security;
using TheXDS.MCART.Helpers;
using TheXDS.MCART.Types;
using static TheXDS.MCART.Helpers.Common;
using static TheXDS.MCART.Types.Extensions.SecureStringExtensions;

namespace TheXDS.MCART.Tests.Helpers;

public class CommonTest
{
    [Test]
    public void ReadCharsTest()
    {
        SecureString s = new();
        s.AppendChar('T');
        s.AppendChar('e');
        s.AppendChar('s');
        s.AppendChar('t');
        s.MakeReadOnly();
        Assert.AreEqual("Test".ToCharArray(), s.ReadChars());
    }

    [Theory]
    [CLSCompliant(false)]
    [TestCase(0,1024,ByteUnitType.Binary,"1.0 KiB")]
    [TestCase(0,1000,ByteUnitType.Decimal,"1.0 KB")]
    [TestCase(1,1024,ByteUnitType.Binary,"1.0 MiB")]
    [TestCase(1,1000,ByteUnitType.Decimal,"1.0 MB")]
    public void ByteUnits_Test(byte mag, int val, ByteUnitType unit, string output)
    {
        Assert.AreEqual(output, Common.ByteUnits(val, unit, mag, CultureInfo.InvariantCulture));
    }
    
    [Theory]
    [CLSCompliant(false)]
    [TestCase(0,1536,ByteUnitType.Binary,"1,5 KiB")]
    [TestCase(0,1500,ByteUnitType.Decimal,"1,5 KB")]
    [TestCase(1,1536,ByteUnitType.Binary,"1,5 MiB")]
    [TestCase(1,1500,ByteUnitType.Decimal,"1,5 MB")]
    public void ByteUnits_custom_culture_Test(byte mag, int val, ByteUnitType unit, string output)
    {
        Assert.AreEqual(output, Common.ByteUnits(val, unit, mag, CultureInfo.CreateSpecificCulture("es-es")));
    }

    [Test]
    public void ByteUnits_Contract_Test()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => Common.ByteUnits(1024, (ByteUnitType)byte.MaxValue, 0));
        Assert.Throws<ArgumentOutOfRangeException>(() => Common.ByteUnits(1024, ByteUnitType.Binary, 9));
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

        Assert.Throws<ArgumentOutOfRangeException>(() => _ = Sequence(1, 10, 0).ToList());
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
    public void FlipEndianessTest_UInt16()
    {
        Assert.AreEqual((short)0x0102, ((ushort)0x0201).FlipEndianess());
    }

    [Test]
    public void FlipEndianessTest_UInt32()
    {
        Assert.AreEqual(0x01020304, ((uint)0x04030201).FlipEndianess());
    }

    [Test]
    public void FlipEndianessTest_UInt64()
    {
        Assert.AreEqual(0x0102030405060708, ((ulong)0x0807060504030201).FlipEndianess());
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
    public void IfNotNull_skips_if_null_test()
    {
        ((object?)null).IfNotNull(_ => Assert.Fail());
        ((Random?)null).IfNotNull(_ => Assert.Fail());
        ((int?)null).IfNotNull(_ => Assert.Fail());
    }

    [Test]
    public void IfNotNull_runs_if_not_null_test()
    {
        var pass = false;
        new object().IfNotNull(_ => pass= true);
        Assert.True(pass);

        pass = false;
        new Random().IfNotNull(_ => pass = true);
        Assert.True(pass);

        pass = false;
        ((int?)1).IfNotNull(_ => pass = true);
        Assert.True(pass);
    }

    [Test]
    public void IfNotNull_Contract_test()
    {
        Assert.Throws<ArgumentNullException>(() => ((object?)null).IfNotNull((Action<object?>)null!));
        Assert.Throws<ArgumentNullException>(() => new object().IfNotNull((Action<object?>)null!));
        Assert.Throws<ArgumentNullException>(() => ((int?)null).IfNotNull(null!));
        Assert.Throws<ArgumentNullException>(() => ((int?)1).IfNotNull(null!));
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
        string outp = new[]
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
        Assert.True(((double?)0.5).IsBetween(0.0, 1.0));
        Assert.True(((double?)0.0).IsBetween(0.0, 1.0, true));
        Assert.False(((double?)0.0).IsBetween(0.0, 1.0, false));
        Assert.False(((double?)null).IsBetween(0.0, 1.0));
        Assert.True(((double?)0.5).IsBetween(new Range<double>(0.0, 1.0)));
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
        SecureString s = new();
        s.AppendChar('@');
        s.MakeReadOnly();
        byte[] r = s.ReadBytes();
        Assert.AreEqual(new byte[] { 64, 0 }, r);
    }

    [Test]
    public void ReadInt16Test()
    {
        SecureString s = new();
        s.AppendChar('@');
        s.MakeReadOnly();
        Assert.AreEqual((short)64, s.ReadInt16()[0]);
    }

    [Test]
    public void ReadTest()
    {
        SecureString s = new();
        s.AppendChar('T');
        s.AppendChar('e');
        s.AppendChar('s');
        s.AppendChar('t');
        s.MakeReadOnly();
        Assert.AreEqual("Test", s.Read());
    }

    [Test]
    public void ToBits_WithInt64_Test()
    {
        bool[] c = new bool[sizeof(long) * 8];
        Assert.AreEqual(c, 0L.ToBits());

        c[1] = true; c[3] = true;
        Assert.AreEqual(c, 10L.ToBits());
    }

    [Test]
    public void ToBits_WithUInt64_Test()
    {
        bool[] c = new bool[sizeof(ulong) * 8];
        Assert.AreEqual(c, ((ulong)0).ToBits());

        c[1] = true; c[3] = true;
        Assert.AreEqual(c, ((ulong)10L).ToBits());
    }

    [Test]
    public void ToBits_WithInt32_Test()
    {
        bool[] c = new bool[sizeof(int) * 8];
        Assert.AreEqual(c, 0.ToBits());

        c[1] = true; c[3] = true;
        Assert.AreEqual(c, 10.ToBits());
    }
    
    [Test]
    public void ToBits_WithUInt32_Test()
    {
        bool[] c = new bool[sizeof(uint) * 8];
        Assert.AreEqual(c, ((uint)0).ToBits());

        c[1] = true; c[3] = true;
        Assert.AreEqual(c, ((uint)10).ToBits());
    }

    [Test]
    public void ToBits_WithInt16_Test()
    {
        bool[] c = new bool[sizeof(short) * 8];
        Assert.AreEqual(c, ((short)0).ToBits());

        c[1] = true; c[3] = true;
        Assert.AreEqual(c, ((short)10).ToBits());
    }
    
    [Test]
    public void ToBits_WithUInt16_Test()
    {
        bool[] c = new bool[sizeof(ushort) * 8];
        Assert.AreEqual(c, ((ushort)0).ToBits());

        c[1] = true; c[3] = true;
        Assert.AreEqual(c, ((ushort)10).ToBits());
    }
    
    [Test]
    public void ToBits_WithSInt8_Test()
    {
        bool[] c = new bool[sizeof(sbyte) * 8];
        Assert.AreEqual(c, ((sbyte)0).ToBits());

        c[1] = true; c[3] = true;
        Assert.AreEqual(c, ((sbyte)10).ToBits());
    }
    
    [Test]
    public void ToBits_WithInt8_Test()
    {
        bool[] c = new bool[sizeof(byte) * 8];
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
    [TestCase(57, -128)]
    [TestCase(64, -1)]
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
    [TestCase(25, -128)]
    [TestCase(32, -1)]
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
    [TestCase(9, -128)]
    [TestCase(16, -1)]
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
    
    [CLSCompliant(false)]
    [TestCase(0, 0)]
    [TestCase(2, 10)]
    [TestCase(3, 11)]
    [TestCase(2, 12)]
    [TestCase(7, 127)]
    [TestCase(1, -128)]
    [TestCase(8, -1)]
    public void BitCount_SInt8_Test(byte bitCount, sbyte value)
    {
        Assert.AreEqual(bitCount, value.BitCount());
    }
    
    [CLSCompliant(false)]
    [TestCase(0, (ushort)0)]
    [TestCase(2, (ushort)10)]
    [TestCase(3, (ushort)11)]
    [TestCase(2, (ushort)12)]
    [TestCase(7, (ushort)127)]
    [TestCase(1, (ushort)128)]
    [TestCase(8, (ushort)255)]
    public void BitCount_UInt16_Test(byte bitCount, ushort value)
    {
        Assert.AreEqual(bitCount, value.BitCount());
    }
    
    [CLSCompliant(false)]
    [TestCase(0, (uint)0)]
    [TestCase(2, (uint)10)]
    [TestCase(3, (uint)11)]
    [TestCase(2, (uint)12)]
    [TestCase(7, (uint)127)]
    [TestCase(1, (uint)128)]
    [TestCase(8, (uint)255)]
    public void BitCount_UInt32_Test(byte bitCount, uint value)
    {
        Assert.AreEqual(bitCount, value.BitCount());
    }
    
    [CLSCompliant(false)]
    [TestCase(0, (ulong)0)]
    [TestCase(2, (ulong)10)]
    [TestCase(3, (ulong)11)]
    [TestCase(2, (ulong)12)]
    [TestCase(7, (ulong)127)]
    [TestCase(1, (ulong)128)]
    [TestCase(8, (ulong)255)]
    public void BitCount_UInt64_Test(byte bitCount, ulong value)
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
    [TestCase(100000, (ByteUnitType)255, "100000 Bytes")]
    [TestCase(1100000, ByteUnitType.BinaryLong, "1.1 Mebibytes")]
    [TestCase(1048576, ByteUnitType.DecimalLong, "1.0 Megabytes")]
    public void ByteUnitsTest_Long_ByteUnitType(long bytes, ByteUnitType unit, string result)
    {
        Assert.AreEqual(result, Common.ByteUnits(bytes, unit, CultureInfo.InvariantCulture));
    }

    [CLSCompliant(false)]
    [TestCase(1000, ByteUnitType.Binary, "1000 Bytes")]
    [TestCase(1000, ByteUnitType.Decimal, "1,0 KB")]
    [TestCase(100000, (ByteUnitType)255, "100000 Bytes")]
    [TestCase(1100000, ByteUnitType.BinaryLong, "1,1 Mebibytes")]
    [TestCase(1048576, ByteUnitType.DecimalLong, "1,0 Megabytes")]
    public void ByteUnitsTest_custom_culture_Long_ByteUnitType(long bytes, ByteUnitType unit, string result)
    {
        Assert.AreEqual(result, Common.ByteUnits(bytes, unit, CultureInfo.CreateSpecificCulture("es-es")));
    }
    
    [Theory]
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
    public void ByteUnits_Test_Long(long bytes, string result)
    {
        Assert.AreEqual(result, bytes.ByteUnits(CultureInfo.InvariantCulture));
    }
    
    [Theory]
    [CLSCompliant(false)]
    [TestCase(1000, "1000 Bytes")]
    [TestCase(1024, "1,0 KiB")]
    [TestCase(1536, "1,5 KiB")]
    [TestCase(1768, "1,7 KiB")]
    [TestCase(1048576, "1,0 MiB")]
    [TestCase(1150976, "1,1 MiB")]
    [TestCase(1073741824, "1,0 GiB")]
    [TestCase(1099511627776L, "1,0 TiB")]
    [TestCase(1125899906842624L, "1,0 PiB")]
    [TestCase(1152921504606846976L, "1,0 EiB")]
    public void ByteUnits_custom_culture_test_long(long bytes, string result)
    {
        Assert.AreEqual(result, bytes.ByteUnits(CultureInfo.CreateSpecificCulture("es-es")));
    }

    [Test]
    public void AnyEmptyTest()
    {
        string?[] array = new[] { "0", null, "2", "3", null, "5" };
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
