/*
StringExtensionsTests.cs

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
using System.Globalization;
using System.Linq;
using System.Text;
using Xunit;
using static TheXDS.MCART.Types.Extensions.SecureStringExtensions;
using static TheXDS.MCART.Types.Extensions.StringExtensions;

namespace TheXDS.MCART.Tests.Types.Extensions
{
    public class StringExtensionsTests
    {
        [Fact]
        public void IsBinary_Test()
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
        public void IsHex_Test()
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
        public void ContainsLetters_Test()
        {
            Assert.True("abc123".ContainsLetters());
            Assert.False("123456".ContainsLetters());
            Assert.False("abcdef".ContainsLetters(true));
            Assert.True("ABCdef".ContainsLetters(true));
            Assert.False("ABCDEF".ContainsLetters(false));
            Assert.True("ABCdef".ContainsLetters(true));
        }

        [Fact]
        public void ContainsNumbers_Test()
        {
            Assert.True("abc123".ContainsNumbers());
            Assert.False("abcdef".ContainsNumbers());
        }
        [Fact]
        public void ToStream_Test()
        {
            using (var r = new System.IO.StreamReader("Test".ToStream()))
                Assert.Equal("Test", r.ReadToEnd());

            using (var r = new System.IO.StreamReader("Test".ToStream(Encoding.Unicode)))
                Assert.Equal("T\0e\0s\0t\0", r.ReadToEnd());
        }

        [Fact]
        public void ContainsAny_Test()
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
        public void CouldItBe_Test()
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
        public void Truncate_Test()
        {
            Assert.Equal("Test", "Test".Truncate(10));
            Assert.Equal("Test", "Test".Truncate(4));
            Assert.Equal("Test...", "TestTestTestTest".Truncate(7));
            Assert.Equal("T...", "TestTestTestTest".Truncate(4));
            Assert.Equal("Tes", "TestTestTestTest".Truncate(3));
            Assert.Equal("Te", "TestTestTestTest".Truncate(2));
            Assert.Equal("T", "TestTestTestTest".Truncate(1));
            Assert.Equal("length", Assert.Throws<ArgumentOutOfRangeException>(() => "Test".Truncate(0)).ParamName);
        }

        [Fact]
        public void CountChars_Test()
        {
            Assert.Equal(5, "This is a test".CountChars('i', ' '));
            Assert.Equal(5, "This is a test".CountChars("i "));
        }
        [Fact]
        public void IsEmpty_Test()
        {
            Assert.True(string.Empty.IsEmpty());
            Assert.False("Test".IsEmpty());
            Assert.True((null as string).IsEmpty());
        }
        [Fact]
        public void Left_Test()
        {
            //Prueba de valores devueltos...
            Assert.Equal("Te", "Test".Left(2));

            //Pruebas de sanidad de argumentos...
            Assert.Throws<ArgumentOutOfRangeException>(() => "Test".Left(5));
            Assert.Throws<ArgumentOutOfRangeException>(() => "Test".Left(-1));
        }

        [Fact]
        public void Likeness_Test()
        {
            Assert.InRange("Cesar Morgan".Likeness("César Morgan"), 0.9f, 1f);
        }

        [Fact]
        public void Right_Test()
        {
            //Prueba de valores devueltos...
            Assert.Equal("st", "Test".Right(2));

            //Pruebas de sanidad de argumentos...
            Assert.Throws<ArgumentOutOfRangeException>(() => "Test".Right(5));
            Assert.Throws<ArgumentOutOfRangeException>(() => "Test".Right(-1));
        }
        [Fact]
        public void IsFormattedAs_Test()
        {
            Assert.False("XYZ-ABCD".IsFormattedAs("A0"));
            Assert.True("XYZ-1234".IsFormattedAs("XAX-09#9"));
            Assert.False("XYZ-ABCD".IsFormattedAs("XAX-09#9"));
            Assert.True("XYZ-1234".IsFormattedAs("XAX-09#9", true));
            Assert.True("XyZ-1234".IsFormattedAs("AaX-09#9", true));
            Assert.False("xyz-1234".IsFormattedAs("XAA-09#9", true));
            Assert.True("xyz-1234".IsFormattedAs("axa-09#9", true));
            Assert.False("XYZ-1234".IsFormattedAs("axa-09#9", true));
            Assert.False("xyz+1234".IsFormattedAs("axa-09#9", true));
            Assert.True("10101010".IsFormattedAs("bBbBbBbB"));
            Assert.False("12121212".IsFormattedAs("bBbBbBbB"));
            Assert.True("0123456789AbCdEf".IsFormattedAs("fFfFfFfFfFfFfFfF"));
            Assert.False("AbCdEfGhIjKlMnOp".IsFormattedAs("fFfFfFfFfFfFfFfF"));
        }

        [Fact]
        public void String_ToSecureString_Test()
        {
            Assert.Equal("Test", "Test".ToSecureString().Read());
        }

        [Fact]
        public void SecureString_ReadBytes_Test()
        {
            Assert.Equal(new byte[]
                {
                    84, 0, 101, 0, 115, 0, 116, 0
                },
                "Test".ToSecureString().ReadBytes());
        }

        [Fact]
        public void StartsWithAny_Test()
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

        [Fact]
        public void Chop_Test()
        {
            var str = "TestTestStringTestTest";
            Assert.Equal("TestStringTest", str.Chop("Test"));
        }

        [Fact]
        public void OrNull_Test()
        {
            Assert.Null(((string?)null).OrNull());
            Assert.Null(string.Empty.OrNull());
            Assert.Null(((string?)null).OrNull("{0} test"));
            Assert.Null(string.Empty.OrNull("{0} test"));
            Assert.Equal("test", "test".OrNull());
            Assert.Equal("test test", "test".OrNull("{0} test"));
        }
        
        [Fact]
        public void OrEmpty_Test()
        {
            Assert.Equal(string.Empty,string.Empty.OrEmpty());
            Assert.Equal(string.Empty,((string?)null).OrEmpty());
            Assert.NotEqual(string.Empty, "Test".OrEmpty());
            Assert.Equal(string.Empty, ((string?)null).OrEmpty("{0} 1234"));
            Assert.Equal(string.Empty, string.Empty.OrEmpty("{0} 1234"));
            Assert.Equal("test 1234", "test".OrEmpty("{0} 1234"));
        }
        
        [Fact]
        public void Spell_Test()
        {
            Assert.Equal("T e s t", "Test".Spell());
        }

        [Fact]
        public void Separate_Test()
        {
            Assert.Equal(String.Empty, String.Empty.Separate(' '));
            Assert.Equal("T-e-s-t", "Test".Separate('-'));
        }

        [Fact]
        public void ChopAny_Test()
        {
            Assert.Equal("456test123456", "123456test123456".ChopAny("123", "456"));
            Assert.Equal("789456test123", "789456test123456".ChopAny("123", "456"));
            Assert.Equal("test", "test".ChopAny("123", "456"));
        }
        
        [Fact]
        public void ChopEnd_Test()
        {
            Assert.Equal("123456test123", "123456test123456".ChopEnd("456"));
        }
        
        [Fact]
        public void ChopStart_Test()
        {
            Assert.Equal("456test123456", "123456test123456".ChopStart("123"));
        }

        [Fact]
        public void TextWrap_Test()
        {
            void ValidLine(string line)
            {
                Assert.True(line.Length <= 80);
                Assert.False(line.StartsWith(' '));
                Assert.False(line.EndsWith(' '));
                Assert.All(line.Split(), p => Assert.Equal("test", p));
            }
            
            Assert.Collection(new string('x', 120).TextWrap(),
                p => Assert.Equal(80, p.Length),
                p => Assert.Equal(40, p.Length));

            Assert.Collection(string.Join(' ', Enumerable.Range(1, 30).Select(_ => "test")).TextWrap(), ValidLine, ValidLine);
            
            Assert.Equal("test  test", "test  test".TextWrap()[0]);
        }
    }
}
