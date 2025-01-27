/*
StringExtensionsTests.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Este archivo contiene todas las pruebas pertenecientes a la clase estática
TheXDS.MCART.Common.

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2025 César Andrés Morgan

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

using System.Globalization;
using System.Text;
using TheXDS.MCART.Helpers;
using static TheXDS.MCART.Types.Extensions.SecureStringExtensions;
using static TheXDS.MCART.Types.Extensions.StringExtensions;

namespace TheXDS.MCART.Tests.Types.Extensions;

public class StringExtensionsTests
{
    [Test]
    public void IsBinary_Test()
    {
        Assert.That("0b1010".IsBinary());
        Assert.That("1010".IsBinary());
        Assert.That("&b1010".IsBinary());
        Assert.That("0b0b1010".IsBinary(), Is.False);
        Assert.That("&b&b1010".IsBinary(), Is.False);
        Assert.That("0b1012".IsBinary(), Is.False);
        Assert.That("1012".IsBinary(), Is.False);
        Assert.That("&b1012".IsBinary(), Is.False);
    }

    [Test]
    public void IsHex_Test()
    {
        Assert.That("0x123F".IsHex());
        Assert.That("123F".IsHex());
        Assert.That("&h123F".IsHex());
        Assert.That("0x123f".IsHex());
        Assert.That("123f".IsHex());
        Assert.That("&h123f".IsHex());
        Assert.That("0x0x123F".IsHex(), Is.False);
        Assert.That("&h&h123F".IsHex(), Is.False);
        Assert.That("0x123J".IsHex(), Is.False);
        Assert.That("123J".IsHex(), Is.False);
        Assert.That("&h123J".IsHex(), Is.False);
    }

    [Test]
    public void ContainsLetters_Test()
    {
        Assert.That("abc123".ContainsLetters());
        Assert.That("123456".ContainsLetters(), Is.False);
        Assert.That("abcdef".ContainsLetters(true), Is.False);
        Assert.That("ABCdef".ContainsLetters(true));
        Assert.That("ABCDEF".ContainsLetters(false), Is.False);
        Assert.That("ABCdef".ContainsLetters(true));
    }

    [Test]
    public void ContainsNumbers_Test()
    {
        Assert.That("abc123".ContainsNumbers());
        Assert.That("abcdef".ContainsNumbers(), Is.False);
    }
    
    [Test]
    public void ToStream_Test()
    {
        using (StreamReader r = new("Test".ToStream()))
            Assert.That("Test", Is.EqualTo(r.ReadToEnd()));

        using (StreamReader r = new("Test".ToStream(Encoding.Unicode)))
            Assert.That("T\0e\0s\0t\0", Is.EqualTo(r.ReadToEnd()));
    }

    [Test]
    public void ContainsAny_Test()
    {
        Assert.That("Test".ContainsAny(new List<string> { "Ta", "Te" }));
        Assert.That("Test".ContainsAny('q', 't', 'a'));
        Assert.That("Test".ContainsAny(out int idx, 'q', 't', 'a'));
        Assert.That(1, Is.EqualTo(idx));
        Assert.That("Test".ContainsAny(out int idx2, "t", "a"));
        Assert.That(0, Is.EqualTo(idx2));
        Assert.That("Test".ContainsAny("Ta", "Te"));
        Assert.That("Test".ContainsAny(out int idx3, "Ta", "Te"));
        Assert.That(1, Is.EqualTo(idx3));
        Assert.That("Test".ContainsAny('a', 'd'), Is.False);
        Assert.That("Test".ContainsAny(out int idx4, 'a', 'd'), Is.False);
        Assert.That(-1, Is.EqualTo(idx4));
        Assert.That("Test".ContainsAny("Ta", "Ti"), Is.False);
        Assert.That("Test".ContainsAny(out int idx5, "Ta", "Ti"), Is.False);
        Assert.That(-1, Is.EqualTo(idx5));
    }

    [Test]
    public void CouldItBe_Test()
    {
        Assert.Throws<ArgumentNullException>(() => string.Empty.CouldItBe("Test"));
        Assert.Throws<ArgumentNullException>(() => "Test".CouldItBe(""));
        Assert.Throws<ArgumentOutOfRangeException>(() => "Test".CouldItBe("Test", 0f));
        Assert.Throws<ArgumentOutOfRangeException>(() => "Test".CouldItBe("Test", 2f));
        Assert.That(1.0, Is.EqualTo("César Morgan".CouldItBe("César Andrés Morgan")));
        Assert.That(0.0, Is.EqualTo("Gerardo Belot".CouldItBe("César Andrés Morgan")));
        Assert.That("Jarol Darío Rivera".CouldItBe("Harold Rivera Aguilar", 0.6f).IsBetween(0.55f, 0.56f));
        Assert.That(0.5, Is.EqualTo("Edith Alvarez".CouldItBe("Edith Mena")));
    }

    [Test]
    public void Truncate_Test()
    {
        Assert.That("Test", Is.EqualTo("Test".Truncate(10)));
        Assert.That("Test", Is.EqualTo("Test".Truncate(4)));
        Assert.That("Test...", Is.EqualTo("TestTestTestTest".Truncate(7)));
        Assert.That("T...", Is.EqualTo("TestTestTestTest".Truncate(4)));
        Assert.That("Tes", Is.EqualTo("TestTestTestTest".Truncate(3)));
        Assert.That("Te", Is.EqualTo("TestTestTestTest".Truncate(2)));
        Assert.That("T", Is.EqualTo("TestTestTestTest".Truncate(1)));
        Assert.That(Assert.Throws<ArgumentOutOfRangeException>(() => "Test".Truncate(0))!.ParamName, Is.EqualTo("length"));
    }

    [Test]
    public void CountChars_Test()
    {
        Assert.That(5, Is.EqualTo("This is a test".CountChars('i', ' ')));
        Assert.That(5, Is.EqualTo("This is a test".CountChars("i ")));
    }

    [Test]
    public void IsEmpty_Test()
    {
        Assert.That(string.Empty.IsEmpty());
        Assert.That("Test".IsEmpty(), Is.False);
        Assert.That((null as string).IsEmpty());
    }

    [Test]
    public void Left_Test()
    {
        Assert.That("Te", Is.EqualTo("Test".Left(2)));
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
        Assert.That("Cesar Morgan".Likeness("César Morgan").IsBetween(0.9f, 1f));
    }

    [Test]
    public void Right_Test()
    {
        Assert.That("st", Is.EqualTo("Test".Right(2)));
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
        Assert.That("XYZ-ABCD".IsFormattedAs("A0"), Is.False);
        Assert.That("XYZ-1234".IsFormattedAs("XAX-0909"));
        Assert.That("XYZ-ABCD".IsFormattedAs("XAX-0909"), Is.False);
        Assert.That("XYZ-1234".IsFormattedAs("XAX-0909", true));
        Assert.That("XyZ-1234".IsFormattedAs("AaX-0909", true));
        Assert.That("xyz-1234".IsFormattedAs("XAA-0909", true), Is.False);
        Assert.That("xyz-1234".IsFormattedAs("axa-0909", true));
        Assert.That("XYZ-1234".IsFormattedAs("axa-0909", true), Is.False);
        Assert.That("xyz+1234".IsFormattedAs("axa-0909", true), Is.False);
        Assert.That("10101010".IsFormattedAs("bBbBbBbB"));
        Assert.That("12121212".IsFormattedAs("bBbBbBbB"), Is.False);
        Assert.That("0123456789AbCdEf".IsFormattedAs("fFfFfFfFfFfFfFfF"));
        Assert.That("AbCdEfGhIjKlMnOp".IsFormattedAs("fFfFfFfFfFfFfFfF"), Is.False);
    }

    [Test]
    public void String_ToSecureString_Test()
    {
        Assert.That("Test", Is.EqualTo("Test".ToSecureString().Read()));
    }

    [Test]
    public void SecureString_ReadBytes_Test()
    {
        Assert.That(new byte[] {
            84, 0, 101, 0, 115, 0, 116, 0
        },
        Is.EqualTo("Test".ToSecureString().ReadBytes()));
    }
    
    [Test]
    public void SecureString_ToBase64_Test()
    {
        Assert.That("VABlAHMAdAA=", Is.EqualTo("Test".ToSecureString().ToBase64()));
    }

    [Test]
    public void StartsWithAny_Test()
    {
        Assert.That("Test".StartsWithAny("Ta", "Te"));
        Assert.That("Test".StartsWithAny(new List<string> { "Ta", "Te" }));
        Assert.That("Test".StartsWithAny("Ta", "Ti"), Is.False);
        Assert.That("Test".StartsWithAny(new List<string> { "Ta", "Ti" }), Is.False);
        Assert.That("TEST".StartsWithAny(new List<string> { "ta", "te" }, true));
        Assert.That("TEST".StartsWithAny(new List<string> { "ta", "ti" }, true), Is.False);
        Assert.That("TEST".StartsWithAny(new List<string> { "ta", "te" }, true, CultureInfo.CurrentCulture));
        Assert.That("TEST".StartsWithAny(new List<string> { "ta", "ti" }, true, CultureInfo.CurrentCulture), Is.False);
        Assert.That("TEST".StartsWithAny(new List<string> { "ta", "te" }, StringComparison.OrdinalIgnoreCase));
        Assert.That("TEST".StartsWithAny(new List<string> { "ta", "ti" }, StringComparison.OrdinalIgnoreCase), Is.False);
    }

    [TestCase("test", false, "Test")]
    [TestCase("TEST", false, "Test")]
    [TestCase("Test", false, "Test")]
    [TestCase("tEST", false, "Test")]
    [TestCase("tEsT", false, "Test")]
    [TestCase("TeSt", false, "Test")]
    [TestCase("test", true, "Test")]
    [TestCase("TEST", true, "TEST")]
    [TestCase("Test", true, "Test")]
    [TestCase("tEST", true, "TEST")]
    [TestCase("TeSt", true, "TeSt")]
    [TestCase("tEsT", true, "TEsT")]
    public void Capitalize_Test(string testString, bool keepCasing, string expected)
    {
        Assert.That(testString.Capitalize(keepCasing), Is.EqualTo(expected));
    }

    [Test]
    public void Chop_Test()
    {
        string str = "TestTestStringTestTest";
        Assert.That("TestStringTest", Is.EqualTo(str.Chop("Test")));
    }

    [Test]
    public void ChopEndAny_Test()
    {
        string[] s = { "A000A", "A000B", "A000C" };
        Assert.That(s.Select(p => p.ChopEndAny("0A", "0B", "0C")).All(p => p == "A00"));
        Assert.That(s.Select(p => p.ChopEndAny("0D", "0E", "0F")).All(p => p != "A00"));
    }

    [Test]
    public void ChopStartAny_Test()
    {
        string[] s = { "A000A", "B000A", "C000A" };
        Assert.That(s.Select(p => p.ChopStartAny("A0", "B0", "C0")).All(p => p == "00A"));
        Assert.That(s.Select(p => p.ChopStartAny("D0", "E0", "F0")).All(p => p != "00A"));
    }

    [Test]
    public void ToBase64_Test()
    {
        Assert.That("VGVzdA==", Is.EqualTo("Test".ToBase64()));
    }

    [Test]
    public void ToBase64_Test2()
    {
        Assert.That("VGVzdA==", Is.EqualTo("Test".ToBase64(Encoding.Default)));
        Assert.That("VGVzdA==", Is.EqualTo("Test".ToBase64(Encoding.Latin1)));
        Assert.That("VABlAHMAdAA=", Is.EqualTo("Test".ToBase64(Encoding.Unicode)));
        Assert.That("AFQAZQBzAHQ=", Is.EqualTo("Test".ToBase64(Encoding.BigEndianUnicode)));
        Assert.That("VGVzdA==", Is.EqualTo("Test".ToBase64(Encoding.UTF8)));
        Assert.That("VAAAAGUAAABzAAAAdAAAAA==", Is.EqualTo("Test".ToBase64(Encoding.UTF32)));
        Assert.That("VGVzdA==", Is.EqualTo("Test".ToBase64(Encoding.ASCII)));
    }
    
    [Test]
    public void OrNull_Test()
    {
        Assert.That(((string?)null).OrNull(), Is.Null);
        Assert.That(string.Empty.OrNull(), Is.Null);
        Assert.That(((string?)null).OrNull("{0} test"), Is.Null);
        Assert.That(string.Empty.OrNull("{0} test"), Is.Null);
        Assert.That("test", Is.EqualTo("test".OrNull()));
        Assert.That("test test", Is.EqualTo("test".OrNull("{0} test")));
    }

    [Test]
    public void OrEmpty_Test()
    {
        Assert.That(string.Empty, Is.EqualTo(string.Empty.OrEmpty()));
        Assert.That(string.Empty, Is.EqualTo(((string?)null).OrEmpty()));
        Assert.That(string.Empty, Is.Not.EqualTo("Test".OrEmpty()));
        Assert.That(string.Empty, Is.EqualTo(((string?)null).OrEmpty("{0} 1234")));
        Assert.That(string.Empty, Is.EqualTo(string.Empty.OrEmpty("{0} 1234")));
        Assert.That("test 1234", Is.EqualTo("test".OrEmpty("{0} 1234")));
    }

    [Test]
    public void Spell_Test()
    {
        Assert.That("T e s t", Is.EqualTo("Test".Spell()));
    }

    [Test]
    public void SplitByCase_test()
    {
        Assert.That(((string?)null).SplitByCase(), Is.EquivalentTo(Array.Empty<string>()));
        Assert.That("".SplitByCase(), Is.EquivalentTo(Array.Empty<string>()));
        Assert.That("Abc".SplitByCase(), Is.EquivalentTo(new[] { "Abc" }));
        Assert.That("abc".SplitByCase(), Is.EquivalentTo(new[] { "abc" }));
        Assert.That("AbcDef".SplitByCase(), Is.EquivalentTo(new[] { "Abc", "Def" }));
        Assert.That("AbcDefGhi".SplitByCase(), Is.EquivalentTo(new[] { "Abc", "Def", "Ghi" }));
        Assert.That("ABC".SplitByCase(), Is.EquivalentTo(new[] { "A", "B", "C" }));
    }

    [Test]
    public void Separate_Test()
    {
        Assert.That(string.Empty, Is.EqualTo(string.Empty.Separate(' ')));
        Assert.That("T-e-s-t", Is.EqualTo("Test".Separate('-')));
    }

    [Test]
    public void ChopAny_Test()
    {
        Assert.That("456test123456", Is.EqualTo("123456test123456".ChopAny("123", "456")));
        Assert.That("789456test123", Is.EqualTo("789456test123456".ChopAny("123", "456")));
        Assert.That("test", Is.EqualTo("test".ChopAny("123", "456")));
    }

    [Test]
    public void ChopEnd_Test()
    {
        Assert.That("123456test123", Is.EqualTo("123456test123456".ChopEnd("456")));
    }

    [Test]
    public void ChopStart_Test()
    {
        Assert.That("456test123456", Is.EqualTo("123456test123456".ChopStart("123")));
    }

    [Test]
    public void TextWrap_Test()
    {
        static void ValidLine(string line)
        {
            Assert.That(line.Length <= 80);
            Assert.That(line.StartsWith(' '), Is.False);
            Assert.That(line.EndsWith(' '), Is.False);
            Assert.That(line.Split().All(p => p == "test"));
        }

        string[] str = new string('x', 120).TextWrap();
        Assert.That(80, Is.EqualTo(str[0].Length));
        Assert.That(40, Is.EqualTo(str[1].Length));
        string[] str2 = string.Join(' ', Enumerable.Range(1, 30).Select(_ => "test")).TextWrap();
        foreach (string j in str2) ValidLine(j);
        Assert.That("test  test", Is.EqualTo("test  test".TextWrap()[0]));
    }

    [Test]
    public void TextWrap_respects_leading_space_Test()
    {
        Assert.That(string.Concat("  test  test".TextWrap()), Is.EqualTo("  test  test"));
        Assert.That(string.Concat("    test    test".TextWrap()), Is.EqualTo("    test    test"));
    }
}
