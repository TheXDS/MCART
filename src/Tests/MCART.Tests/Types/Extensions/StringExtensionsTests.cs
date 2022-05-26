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

using Microsoft.VisualStudio.TestPlatform.Utilities;
using NUnit.Framework.Internal;

namespace TheXDS.MCART.Tests.Types.Extensions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using TheXDS.MCART.Helpers;
using static TheXDS.MCART.Types.Extensions.SecureStringExtensions;
using static TheXDS.MCART.Types.Extensions.StringExtensions;

public class StringExtensionsTests
{
    [Test]
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

    [Test]
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

    [Test]
    public void ContainsLetters_Test()
    {
        Assert.True("abc123".ContainsLetters());
        Assert.False("123456".ContainsLetters());
        Assert.False("abcdef".ContainsLetters(true));
        Assert.True("ABCdef".ContainsLetters(true));
        Assert.False("ABCDEF".ContainsLetters(false));
        Assert.True("ABCdef".ContainsLetters(true));
    }

    [Test]
    public void ContainsNumbers_Test()
    {
        Assert.True("abc123".ContainsNumbers());
        Assert.False("abcdef".ContainsNumbers());
    }
    
    [Test]
    public void ToStream_Test()
    {
        using (System.IO.StreamReader r = new("Test".ToStream()))
            Assert.AreEqual("Test", r.ReadToEnd());

        using (System.IO.StreamReader r = new("Test".ToStream(Encoding.Unicode)))
            Assert.AreEqual("T\0e\0s\0t\0", r.ReadToEnd());
    }

    [Test]
    public void ContainsAny_Test()
    {
        Assert.True("Test".ContainsAny(new List<string> { "Ta", "Te" }));
        Assert.True("Test".ContainsAny('q', 't', 'a'));
        Assert.True("Test".ContainsAny(out int idx, 'q', 't', 'a'));
        Assert.AreEqual(1, idx);
        Assert.True("Test".ContainsAny(out int idx2, "t", "a"));
        Assert.AreEqual(0, idx2);
        Assert.True("Test".ContainsAny("Ta", "Te"));
        Assert.True("Test".ContainsAny(out int idx3, "Ta", "Te"));
        Assert.AreEqual(1, idx3);
        Assert.False("Test".ContainsAny('a', 'd'));
        Assert.False("Test".ContainsAny(out int idx4, 'a', 'd'));
        Assert.AreEqual(-1, idx4);
        Assert.False("Test".ContainsAny("Ta", "Ti"));
        Assert.False("Test".ContainsAny(out int idx5, "Ta", "Ti"));
        Assert.AreEqual(-1, idx5);
    }

    [Test]
    public void CouldItBe_Test()
    {
        Assert.Throws<ArgumentNullException>(() => string.Empty.CouldItBe("Test"));
        Assert.Throws<ArgumentNullException>(() => "Test".CouldItBe(""));
        Assert.Throws<ArgumentOutOfRangeException>(() => "Test".CouldItBe("Test", 0f));
        Assert.Throws<ArgumentOutOfRangeException>(() => "Test".CouldItBe("Test", 2f));
        Assert.AreEqual(1.0, "César Morgan".CouldItBe("César Andrés Morgan"));
        Assert.AreEqual(0.0, "Gerardo Belot".CouldItBe("César Andrés Morgan"));
        Assert.True("Jarol Darío Rivera".CouldItBe("Harold Rivera Aguilar", 0.6f).IsBetween(0.55f, 0.56f));
        Assert.AreEqual(0.5, "Edith Alvarez".CouldItBe("Edith Mena"));
    }

    [Test]
    public void Truncate_Test()
    {
        Assert.AreEqual("Test", "Test".Truncate(10));
        Assert.AreEqual("Test", "Test".Truncate(4));
        Assert.AreEqual("Test...", "TestTestTestTest".Truncate(7));
        Assert.AreEqual("T...", "TestTestTestTest".Truncate(4));
        Assert.AreEqual("Tes", "TestTestTestTest".Truncate(3));
        Assert.AreEqual("Te", "TestTestTestTest".Truncate(2));
        Assert.AreEqual("T", "TestTestTestTest".Truncate(1));
        Assert.AreEqual("length", Assert.Throws<ArgumentOutOfRangeException>(() => "Test".Truncate(0))!.ParamName);
    }

    [Test]
    public void CountChars_Test()
    {
        Assert.AreEqual(5, "This is a test".CountChars('i', ' '));
        Assert.AreEqual(5, "This is a test".CountChars("i "));
    }

    [Test]
    public void IsEmpty_Test()
    {
        Assert.True(string.Empty.IsEmpty());
        Assert.False("Test".IsEmpty());
        Assert.True((null as string).IsEmpty());
    }

    [Test]
    public void Left_Test()
    {
        Assert.AreEqual("Te", "Test".Left(2));
    }

    [Test]
    public void Left_contract_test()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => "Test".Left(5));
        Assert.Throws<ArgumentOutOfRangeException>(() => "Test".Left(-1));
    }

    [Test]
    public void Likeness_Test()
    {
        Assert.True("Cesar Morgan".Likeness("César Morgan").IsBetween(0.9f, 1f));
    }

    [Test]
    public void Right_Test()
    {
        Assert.AreEqual("st", "Test".Right(2));
    }

    [Test]
    public void Right_contract_test()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => "Test".Right(5));
        Assert.Throws<ArgumentOutOfRangeException>(() => "Test".Right(-1));
    }

    [Test]
    public void IsFormattedAs_Test()
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

    [Test]
    public void String_ToSecureString_Test()
    {
        Assert.AreEqual("Test", "Test".ToSecureString().Read());
    }

    [Test]
    public void SecureString_ReadBytes_Test()
    {
        Assert.AreEqual(new byte[] {
            84, 0, 101, 0, 115, 0, 116, 0
        },
        "Test".ToSecureString().ReadBytes());
    }
    
    [Test]
    public void SecureString_ToBase64_Test()
    {
        Assert.AreEqual("VABlAHMAdAA=", "Test".ToSecureString().ToBase64());
    }

    [Test]
    public void StartsWithAny_Test()
    {
        Assert.True("Test".StartsWithAny("Ta", "Te"));
        Assert.True("Test".StartsWithAny(new List<string> { "Ta", "Te" }));
        Assert.False("Test".StartsWithAny("Ta", "Ti"));
        Assert.False("Test".StartsWithAny(new List<string> { "Ta", "Ti" }));
        Assert.True("TEST".StartsWithAny(new List<string> { "ta", "te" }, true));
        Assert.False("TEST".StartsWithAny(new List<string> { "ta", "ti" }, true));
        Assert.True("TEST".StartsWithAny(new List<string> { "ta", "te" }, true, CultureInfo.CurrentCulture));
        Assert.False("TEST".StartsWithAny(new List<string> { "ta", "ti" }, true, CultureInfo.CurrentCulture));
        Assert.True("TEST".StartsWithAny(new List<string> { "ta", "te" }, StringComparison.OrdinalIgnoreCase));
        Assert.False("TEST".StartsWithAny(new List<string> { "ta", "ti" }, StringComparison.OrdinalIgnoreCase));
    }

    [Test]
    public void Chop_Test()
    {
        string str = "TestTestStringTestTest";
        Assert.AreEqual("TestStringTest", str.Chop("Test"));
    }

    [Test]
    public void ChopEndAny_Test()
    {
        string[] s = { "A000A", "A000B", "A000C" };
        Assert.True(s.Select(p => p.ChopEndAny("0A", "0B", "0C")).All(p => p == "A00"));
        Assert.True(s.Select(p => p.ChopEndAny("0D", "0E", "0F")).All(p => p != "A00"));
    }

    [Test]
    public void ChopStartAny_Test()
    {
        string[] s = { "A000A", "B000A", "C000A" };
        Assert.True(s.Select(p => p.ChopStartAny("A0", "B0", "C0")).All(p => p == "00A"));
        Assert.True(s.Select(p => p.ChopStartAny("D0", "E0", "F0")).All(p => p != "00A"));
    }

    [Test]
    public void ToBase64_Test()
    {
        Assert.AreEqual("VGVzdA==", "Test".ToBase64());
    }

    [Test]
    public void ToBase64_Test2()
    {
        Assert.AreEqual("VGVzdA==", "Test".ToBase64(Encoding.Default));
        Assert.AreEqual("VGVzdA==", "Test".ToBase64(Encoding.Latin1));
        Assert.AreEqual("VABlAHMAdAA=", "Test".ToBase64(Encoding.Unicode));
        Assert.AreEqual("AFQAZQBzAHQ=", "Test".ToBase64(Encoding.BigEndianUnicode));
        Assert.AreEqual("VGVzdA==", "Test".ToBase64(Encoding.UTF8));
        Assert.AreEqual("VAAAAGUAAABzAAAAdAAAAA==", "Test".ToBase64(Encoding.UTF32));
        Assert.AreEqual("VGVzdA==", "Test".ToBase64(Encoding.ASCII));
    }
    
    [Test]
    public void OrNull_Test()
    {
        Assert.Null(((string?)null).OrNull());
        Assert.Null(string.Empty.OrNull());
        Assert.Null(((string?)null).OrNull("{0} test"));
        Assert.Null(string.Empty.OrNull("{0} test"));
        Assert.AreEqual("test", "test".OrNull());
        Assert.AreEqual("test test", "test".OrNull("{0} test"));
    }

    [Test]
    public void OrEmpty_Test()
    {
        Assert.AreEqual(string.Empty, string.Empty.OrEmpty());
        Assert.AreEqual(string.Empty, ((string?)null).OrEmpty());
        Assert.AreNotEqual(string.Empty, "Test".OrEmpty());
        Assert.AreEqual(string.Empty, ((string?)null).OrEmpty("{0} 1234"));
        Assert.AreEqual(string.Empty, string.Empty.OrEmpty("{0} 1234"));
        Assert.AreEqual("test 1234", "test".OrEmpty("{0} 1234"));
    }

    [Test]
    public void Spell_Test()
    {
        Assert.AreEqual("T e s t", "Test".Spell());
    }

    [Test]
    public void Separate_Test()
    {
        Assert.AreEqual(String.Empty, String.Empty.Separate(' '));
        Assert.AreEqual("T-e-s-t", "Test".Separate('-'));
    }

    [Test]
    public void ChopAny_Test()
    {
        Assert.AreEqual("456test123456", "123456test123456".ChopAny("123", "456"));
        Assert.AreEqual("789456test123", "789456test123456".ChopAny("123", "456"));
        Assert.AreEqual("test", "test".ChopAny("123", "456"));
    }

    [Test]
    public void ChopEnd_Test()
    {
        Assert.AreEqual("123456test123", "123456test123456".ChopEnd("456"));
    }

    [Test]
    public void ChopStart_Test()
    {
        Assert.AreEqual("456test123456", "123456test123456".ChopStart("123"));
    }

    [Test]
    public void TextWrap_Test()
    {
        static void ValidLine(string line)
        {
            Assert.True(line.Length <= 80);
            Assert.False(line.StartsWith(' '));
            Assert.False(line.EndsWith(' '));
            Assert.True(line.Split().All(p => p == "test"));
        }

        string[] str = new string('x', 120).TextWrap();
        Assert.AreEqual(80, str[0].Length);
        Assert.AreEqual(40, str[1].Length);
        string[] str2 = string.Join(' ', Enumerable.Range(1, 30).Select(_ => "test")).TextWrap();
        foreach (string j in str2) ValidLine(j);
        Assert.AreEqual("test  test", "test  test".TextWrap()[0]);
    }
}
