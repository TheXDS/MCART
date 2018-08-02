/*
CommonTest.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Este archivo contiene todas las pruebas pertenecientes a la clase estática
TheXDS.MCART.Common.

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright (c) 2011 - 2018 César Andrés Morgan

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
using System.Globalization;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using TheXDS.MCART;
using TheXDS.MCART.Types;
using Xunit;
using static TheXDS.MCART.Common;

namespace CoreTest.Modules
{
    /// <summary>
    ///     Contiene pruebas para la clase estática <see cref="Common" />.
    /// </summary>
    public class CommonTest
    {
        [Fact]
        public void IsBinaryTest()
        {
            Assert.True("0b1010".IsBinary());
            Assert.True("1010".IsBinary());
            Assert.True("&b1010".IsBinary());

            Assert.False("0b0b1010".IsBinary());
            Assert.False("&b&b1010".IsBinary());
            Assert.False("0b1012".IsBinary());
            Assert.False("1012".IsBinary());
            Assert.False("&b1012".IsBinary());
        }

        [Fact]
        public void IsHexTest()
        {
            Assert.True("0x123F".IsHex());
            Assert.True("123F".IsHex());
            Assert.True("&h123F".IsHex());
            Assert.True("0x123f".IsHex());
            Assert.True("123f".IsHex());
            Assert.True("&h123f".IsHex());

            Assert.False("0x0x123F".IsHex());
            Assert.False("&h&h123F".IsHex());
            Assert.False("0x123J".IsHex());
            Assert.False("123J".IsHex());
            Assert.False("&h123J".IsHex());
        }

        [Fact]
        public void ContainsLettersTest()
        {
            Assert.True("abc123".ContainsLetters());
            Assert.False("123456".ContainsLetters());

            Assert.False("abcdef".ContainsLetters(true));
            Assert.True("ABCdef".ContainsLetters(true));

            Assert.False("ABCDEF".ContainsLetters(false));
            Assert.True("ABCdef".ContainsLetters(true));
        }

        [Fact]
        public void ContainsNumbersTest()
        {
            Assert.True("abc123".ContainsNumbers());
            Assert.False("abcdef".ContainsNumbers());
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
        public void ToStreamTest()
        {
            using (var r = new System.IO.StreamReader("Test".ToStream()))
                Assert.Equal("Test", r.ReadToEnd());

            using (var r = new System.IO.StreamReader("Test".ToStream(Encoding.Unicode)))
                Assert.Equal("T\0e\0s\0t\0", r.ReadToEnd());
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
        public void ContainsAnyTest()
        {
            Assert.True("Test".ContainsAny(new List<string>{ "Ta", "Te" }));
            Assert.True("Test".ContainsAny('q', 't', 'a'));
            Assert.True("Test".ContainsAny(out var idx, 'q', 't', 'a'));
            Assert.Equal(1, idx);
            
            Assert.True("Test".ContainsAny(out var idx2, "t", "a"));
            Assert.Equal(0, idx2);

            Assert.True("Test".ContainsAny("Ta", "Te"));
            Assert.True("Test".ContainsAny(out var idx3, "Ta", "Te"));
            Assert.Equal(1, idx3);

            Assert.False("Test".ContainsAny('a', 'd'));
            Assert.False("Test".ContainsAny(out var idx4, 'a', 'd'));
            Assert.Equal(-1, idx4);

            Assert.False("Test".ContainsAny("Ta", "Ti"));
            Assert.False("Test".ContainsAny(out var idx5, "Ta", "Ti"));
            Assert.Equal(-1, idx5);
        }

        [Fact]
        public void CouldItBeTest()
        {
            Assert.Throws<ArgumentNullException>(() => string.Empty.CouldItBe("Test"));
            Assert.Throws<ArgumentNullException>(() => "Test".CouldItBe(""));
            Assert.Throws<ArgumentOutOfRangeException>(() => "Test".CouldItBe("Test", 0f));
            Assert.Throws<ArgumentOutOfRangeException>(() => "Test".CouldItBe("Test", 2f));

            Assert.Equal(1.0, "César Morgan".CouldItBe("César Andrés Morgan"));
            Assert.Equal(0.0, "Gerardo Belot".CouldItBe("César Andrés Morgan"));
            Assert.InRange("Jarol Darío Rivera".CouldItBe("Harold Rivera Aguilar", 0.6f), 0.55, 0.56);
            Assert.Equal(0.5, "Edith Alvarez".CouldItBe("Edith Mena"));
        }

        [Fact]
        public void CountCharsTest()
        {
            Assert.Equal(5, "This is a test".CountChars('i', ' '));
            Assert.Equal(5, "This is a test".CountChars("i "));
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
        }

        [Fact]
        public void IsEmptyTest()
        {
            Assert.True(string.Empty.IsEmpty());
            Assert.False("Test".IsEmpty());
            Assert.True((null as string).IsEmpty());
        }

        [Fact]
        public void LeftTest()
        {
            //Prueba de valores devueltos...
            Assert.Equal("Te", "Test".Left(2));

            //Pruebas de sanidad de argumentos...
            Assert.Throws<ArgumentOutOfRangeException>(() => "Test".Left(5));
            Assert.Throws<ArgumentOutOfRangeException>(() => "Test".Left(-1));
        }

        [Fact]
        public void LikenessTest()
        {
            Assert.InRange("Cesar Morgan".Likeness("César Morgan"), 0.9f, 1f);
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
        public void RightTest()
        {
            //Prueba de valores devueltos...
            Assert.Equal("st", "Test".Right(2));

            //Pruebas de sanidad de argumentos...
            Assert.Throws<ArgumentOutOfRangeException>(() => "Test".Right(5));
            Assert.Throws<ArgumentOutOfRangeException>(() => "Test".Right(-1));
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

        [Fact]
        public void IsFormattedTest()
        {
            Assert.False("XYZ-ABCD".IsFormattedAs("A0"));

            Assert.True("XYZ-1234".IsFormattedAs("XAX-0909"));
            Assert.False("XYZ-ABCD".IsFormattedAs("XAX-0909"));

            Assert.True("XYZ-1234".IsFormattedAs("XAX-0909", true));
            Assert.True("XyZ-1234".IsFormattedAs("AaX-0909", true));
            Assert.False("xyz-1234".IsFormattedAs("XAA-0909", true));
            Assert.True("xyz-1234".IsFormattedAs("axa-0909", true));
            Assert.False("XYZ-1234".IsFormattedAs("axa-0909", true));
            Assert.False("xyz+1234".IsFormattedAs("axa-0909", true));

            Assert.True("10101010".IsFormattedAs("bBbBbBbB"));
            Assert.False("12121212".IsFormattedAs("bBbBbBbB"));

            Assert.True("0123456789AbCdEf".IsFormattedAs("fFfFfFfFfFfFfFfF"));
            Assert.False("AbCdEfGhIjKlMnOp".IsFormattedAs("fFfFfFfFfFfFfFfF"));
        }

        [Fact]
        public void String_ToSecureStringTest()
        {
            Assert.Equal("Test", "Test".ToSecureString().Read());
        }

        [Fact]
        public void SecureString_ReadBytesTest()
        {
            Assert.Equal(new byte[]
                {
                    84, 0, 101, 0, 115, 0, 116, 0
                },
                "Test".ToSecureString().ReadBytes());
        }

        [Fact]
        public void StartsWithAnyTest()
        {
            Assert.True("Test".StartsWithAny("Ta","Te"));
            Assert.True("Test".StartsWithAny(new List<string>{"Ta","Te"}));
            Assert.False("Test".StartsWithAny("Ta", "Ti"));
            Assert.False("Test".StartsWithAny(new List<string> { "Ta", "Ti" }));

            Assert.True("TEST".StartsWithAny(new List<string> { "ta", "te" },true));
            Assert.False("TEST".StartsWithAny(new List<string> { "ta", "ti" }, true));

            Assert.True("TEST".StartsWithAny(new List<string> { "ta", "te" }, true, CultureInfo.CurrentCulture));
            Assert.False("TEST".StartsWithAny(new List<string> { "ta", "ti" }, true, CultureInfo.CurrentCulture));

            Assert.True("TEST".StartsWithAny(new List<string> { "ta", "te" }, StringComparison.OrdinalIgnoreCase));
            Assert.False("TEST".StartsWithAny(new List<string> { "ta", "ti" }, StringComparison.OrdinalIgnoreCase));
        }
    }
}