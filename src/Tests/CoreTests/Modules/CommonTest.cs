/*
CommonTest.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Este archivo contiene todas las pruebas pertenecientes a la clase estática
TheXDS.MCART.Common.

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using TheXDS.MCART.Types;
using Xunit;
using static TheXDS.MCART.Common;
using static TheXDS.MCART.Types.Extensions.SecureStringExtensions;

namespace TheXDS.MCART.Tests.Modules
{
    /// <summary>
    ///     Contiene pruebas para la clase estática <see cref="Common" />.
    /// </summary>
    public class CommonTest
    {
        [Fact]
        public void ClampTest()
        {
            Assert.Equal(2,TheXDS.MCART.Math.Common.Clamp(1 + 1, 0, 3));
            Assert.Equal(2,TheXDS.MCART.Math.Common.Clamp(1 + 3, 0, 2));
            Assert.Equal(2,TheXDS.MCART.Math.Common.Clamp(1 + 0, 2, 4));
        }

        [Fact]
        public void ClampTest_double()
        {
            Assert.Equal(2.0, TheXDS.MCART.Math.Common.Clamp(1.0+1.0,0.0,3.0));
            Assert.Equal(2.0, TheXDS.MCART.Math.Common.Clamp(1.0+3.0,0.0,2.0));
            Assert.Equal(2.0, TheXDS.MCART.Math.Common.Clamp(1.0-1.0,2.0,3.0));
            Assert.Equal(double.NaN, TheXDS.MCART.Math.Common.Clamp(double.NaN, 0.0,1.0));
            Assert.Equal(5.0, TheXDS.MCART.Math.Common.Clamp(double.PositiveInfinity, -5.0, 5.0));
            Assert.Equal(-5.0, TheXDS.MCART.Math.Common.Clamp(double.NegativeInfinity, -5.0, 5.0));
        }

        [Fact]
        public void ClampTest_float()
        {
            Assert.Equal(2.0f, TheXDS.MCART.Math.Common.Clamp(1.0f + 1.0f, 0.0f, 3.0f));
            Assert.Equal(2.0f, TheXDS.MCART.Math.Common.Clamp(1.0f + 3.0f, 0.0f, 2.0f));
            Assert.Equal(2.0f, TheXDS.MCART.Math.Common.Clamp(1.0f - 1.0f, 2.0f, 3.0f));
            Assert.Equal(float.NaN, TheXDS.MCART.Math.Common.Clamp(float.NaN, 0.0f, 1.0f));
            Assert.Equal(5.0f, TheXDS.MCART.Math.Common.Clamp(float.PositiveInfinity, -5.0f, 5.0f));
            Assert.Equal(-5.0f, TheXDS.MCART.Math.Common.Clamp(float.NegativeInfinity, -5.0f, 5.0f));
        }


        [Fact]
        public void ToPercentTestDouble()
        {
            var c = new[] { 1, 2, 3, 4, 5 };

            Assert.Equal(new[] { 0.2, 0.4, 0.6, 0.8, 1.0 }, c.ToPercentDouble());
            Assert.Equal(new[] { 0.2, 0.4, 0.6, 0.8, 1.0 }, c.ToPercentDouble(true));
            Assert.Equal(new[] { 0.0, 0.25, 0.5, 0.75, 1.0 }, c.ToPercentDouble(false));
            Assert.Equal(new[] { 0.1, 0.2, 0.3, 0.4, 0.5 }, c.ToPercentDouble(10));
            Assert.Equal(new[] { 0.0, 0.25, 0.5, 0.75, 1.0 }, c.ToPercentDouble(1, 5));
            Assert.Throws<InvalidOperationException>(() => c.ToPercentDouble(1, 1).ToList());
        }

        [Fact]
        public void ToPercentTestSingle()
        {
            var c = new[] { 1, 2, 3, 4, 5 };

            Assert.Equal(new[] { 0.2f, 0.4f, 0.6f, 0.8f, 1.0f }, c.ToPercentSingle());
            Assert.Equal(new[] { 0.2f, 0.4f, 0.6f, 0.8f, 1.0f }, c.ToPercentSingle(true));
            Assert.Equal(new[] { 0.0f, 0.25f, 0.5f, 0.75f, 1.0f }, c.ToPercentSingle(false));
            Assert.Equal(new[] { 0.1f, 0.2f, 0.3f, 0.4f, 0.5f }, c.ToPercentSingle(10));
            Assert.Equal(new[] { 0.0f, 0.25f, 0.5f, 0.75f, 1.0f }, c.ToPercentSingle(1, 5));
            Assert.Throws<InvalidOperationException>(() => c.ToPercentSingle(1, 1).ToList());
        }


        [Fact]
        public void ReadCharsTest()
        {
            var s = new SecureString();
            s.AppendChar('T');
            s.AppendChar('e');
            s.AppendChar('s');
            s.AppendChar('t');
            s.MakeReadOnly();
            Assert.Equal("Test".ToCharArray(), s.ReadChars());
        }

        [Fact]
        public void SequenceTest()
        {
            Assert.Equal(
                new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 },
                Sequence(10));

            Assert.Equal(
                new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 },
                Sequence(1, 10));

            Assert.Equal(
                new[] { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 },
                Sequence(10, 1));

            Assert.Equal(
                new[] { 1, 3, 5, 7, 9 },
                Sequence(1, 10, 2));

            Assert.Equal(
                new[] { 10, 8, 6, 4, 2 },
                Sequence(10, 1, 2));
        }

        [Fact]
        public void FindConverterTest()
        {
            Assert.NotNull(FindConverter<int>());
            Assert.Null(FindConverter<Exception, Enum>());
        }

        [Fact]
        public void FlipEndianessTest_Char()
        {
            Assert.Equal((char)0x0102, ((char)0x0201).FlipEndianess());
        }

        [Fact]
        public void FlipEndianessTest_Int16()
        {
            Assert.Equal((short)0x0102, ((short)0x0201).FlipEndianess());
        }

        [Fact]
        public void FlipEndianessTest_Int32()
        {
            Assert.Equal(0x01020304, 0x04030201.FlipEndianess());
        }

        [Fact]
        public void FlipEndianessTest_Int64()
        {
            Assert.Equal(0x0102030405060708, 0x0807060504030201.FlipEndianess());
        }

        [Fact]
        public void FlipEndianessTest_Single()
        {
            Assert.Equal(3.02529E-39f, 123456f.FlipEndianess());
        }

        [Fact]
        public void FlipEndianessTest_Double()
        {
            Assert.InRange(System.Math.PI.FlipEndianess(), 3.20737563067636E-192, 3.20737563067638E-192);
        }

        [Fact]
        public void AreAllEmptyTest()
        {
            Assert.True(AllEmpty(null, " ", string.Empty));
            Assert.False(AllEmpty(null, "Test", string.Empty));
        }

        [Fact]
        public void CollectionListedTest()
        {
            var outp = new[]
            {
                "This", "is", "a", "test"
            }.Listed();
            Assert.Equal(
                $"This{Environment.NewLine}is{Environment.NewLine}a{Environment.NewLine}test{Environment.NewLine}",
                outp);
        }

        [Fact]
        public void IsAnyEmptyTest()
        {
            Assert.True(AnyEmpty("Test", string.Empty, ""));
            Assert.False(AnyEmpty("T", "e", "s", "t"));

            Assert.True(AnyEmpty(out var i1, "Test", string.Empty, ""));
            Assert.Equal(new[] { 1, 2 }, i1);

            Assert.True(AnyEmpty(out var i2, null, string.Empty, ""));
            Assert.Equal(new[] { 0, 1, 2 }, i2);

            Assert.False(AnyEmpty(out var i3, "T", "e", "s", "t"));
            Assert.Equal(new int[] { }, i3);
        }

        [Fact]
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


        [Fact]
        public void ReadBytesTest()
        {
            var s = new SecureString();
            s.AppendChar('@');
            s.MakeReadOnly();
            var r = s.ReadBytes();
            Assert.True(64 == r[0] && 0 == r[1]);
        }

        [Fact]
        public void ReadInt16Test()
        {
            var s = new SecureString();
            s.AppendChar('@');
            s.MakeReadOnly();
            Assert.Equal((short)64, s.ReadInt16()[0]);
        }

        [Fact]
        public void ReadTest()
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
        public void SwapTest()
        {
            int a = 1, b = 2;
            Swap(ref a, ref b);
            Assert.Equal(2, a);
            Assert.Equal(1, b);
        }

        [Fact]
        public void ToHexTest1()
        {
            Assert.Equal("F0", ((byte)240).ToHex());
        }

        [Fact]
        public void ToHexTest2()
        {
            Assert.Equal("0A0B0C", new byte[] { 10, 11, 12 }.ToHex());
        }

        [Fact]
        public void ToPercentTest_Single()
        {
            Assert.Equal(
                new[] { 0f, 0.25f, 0.5f, 0.75f, 1.0f },
                new[] { 1f, 2f, 3f, 4f, 5f }.ToPercent());

            Assert.Equal(
                new[] { 0f, 0.25f, 0.5f, 0.75f, 1.0f },
                new[] { 1f, 2f, 3f, 4f, 5f }.ToPercent(false));

            Assert.Equal(
                new[] { 0.25f, 0.5f, 0.75f, 1.0f },
                new[] { 1f, 2f, 3f, 4f }.ToPercent(true));

            Assert.Equal(
                new[] { 0.1f, 0.2f, 0.3f, 0.4f },
                new[] { 1f, 2f, 3f, 4f }.ToPercent(10f));

            Assert.Equal(
                new[] { -0.8f, float.NaN, -0.4f, -0.2f },
                new[] { 1f, float.NaN, 3f, 4f }.ToPercent(5f, 10f));

            Assert.Throws<ArgumentException>(() => new[] { 1f }.ToPercent(float.NaN, float.NaN).ToList());
            Assert.Throws<ArgumentException>(() => new[] { 1f }.ToPercent(0f, float.NaN).ToList());

        }

        [Fact]
        public void ToPercentTest_Double()
        {
            Assert.Equal(
                new[] { 0, 0.25, 0.5, 0.75, 1.0 },
                new[] { 1.0, 2.0, 3.0, 4.0, 5.0 }.ToPercent());

            Assert.Equal(
                new[] { 0, 0.25, 0.5, 0.75, 1.0 },
                new[] { 1.0, 2.0, 3.0, 4.0, 5.0 }.ToPercent(false));

            Assert.Equal(
                new[] { 0.25, 0.5, 0.75, 1.0 },
                new[] { 1.0, 2.0, 3.0, 4.0 }.ToPercent(true));

            Assert.Equal(
                new[] { 0.1, 0.2, 0.3, 0.4 },
                new[] { 1.0, 2.0, 3.0, 4.0 }.ToPercent(10.0));

            Assert.Equal(
                new[] { -0.8, double.NaN, -0.4, -0.2 },
                new[] { 1.0, double.NaN, 3.0, 4.0 }.ToPercent(5.0, 10.0));

            Assert.Throws<ArgumentException>(() => new[] { 1.0 }.ToPercent(double.NaN, double.NaN).ToList());
            Assert.Throws<ArgumentException>(() => new[] { 1.0 }.ToPercent(0.0, double.NaN).ToList());
        }
        [Theory]
        [CLSCompliant(false)]
        [InlineData(1000, ByteUnitType.Binary, "1000 Bytes")]
        [InlineData(1000, ByteUnitType.Decimal, "1.0 KB")]
        [InlineData(100000, (ByteUnitType) 2, "100000 Bytes")]
        public void ByteUnitsTest_Long_ByteUnitType(long bytes, ByteUnitType unit, string result)
        {
            Assert.Equal(result,ByteUnits(bytes,unit));
        }
        
        [Theory]
        [CLSCompliant(false)]
        [InlineData(1000, "1000 Bytes")]
        [InlineData(1024, "1.0 KiB")]
        [InlineData(1536, "1.5 KiB")]
        [InlineData(1048576, "1.0 MiB")]
        [InlineData(1073741824, "1.0 GiB")]
        [InlineData(1099511627776L, "1.0 TiB")]
        [InlineData(1125899906842624L, "1.0 PiB")]
        [InlineData(1152921504606846976L, "1.0 EiB")]
        public void ByteUnitsTest_Long(long bytes, string result)
        {
            Assert.Equal(result,bytes.ByteUnits());
        }

        [Fact]
        public void AnyEmptyTest()
        {
            var array = new [] { "0", null, "2", "3", null, "5" };
            Assert.False(new[]{ "0", "1", "2" }.AnyEmpty(out int i));
            Assert.Equal(-1, i);
            Assert.True(array.AnyEmpty(out int index));
            Assert.Equal(1, index);
            Assert.True(array.AnyEmpty(out IEnumerable<int> indexes));
            Assert.Equal(new[]{ 1, 4 },indexes.ToArray());
        }
    }
}