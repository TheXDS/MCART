/*
ColorTests.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2026 César Andrés Morgan

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

using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;
using TheXDS.MCART.Math;
using TheXDS.MCART.Resources;
using TheXDS.MCART.Types;
using TheXDS.MCART.Types.Base;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.Tests.Types;

public class ColorTests
{
    [Theory]
    [TestCase(0f, 0, 0, 255)]
    [TestCase(0.25f, 0, 255, 255)]
    [TestCase(0.5f, 0, 255, 0)]
    [TestCase(0.75f, 255, 255, 0)]
    [TestCase(1f, 255, 0, 0)]
    public void BlendHeat_Test(float value, int r, int g, int b)
    {
        Color c = Color.BlendHeat(value);
        Assert.Multiple(() =>
        {
            Assert.That(c.R, Is.InRange(r - 1, r + 1));
            Assert.That(c.G, Is.InRange(g - 1, g + 1));
            Assert.That(c.B, Is.InRange(b - 1, b + 1));
        });
    }

    [Theory]
    [TestCase(0f, 255, 0)]
    [TestCase(0.25f, 255, 127)]
    [TestCase(0.5f, 255, 255)]
    [TestCase(0.75f, 127, 255)]
    [TestCase(1f, 0, 255)]
    public void BlendHealth_Test(float value, int r, int g)
    {
        Color c = Color.BlendHealth(value);
        Assert.Multiple(() =>
        {
            Assert.That(c.R, Is.InRange(r - 1, r + 1));
            Assert.That(c.G, Is.InRange(g - 1, g + 1));
        });
    }

    [Test]
    public void Blend_Test()
    {
        Assert.Multiple(() =>
        {
            Assert.That(Color.Blend(Colors.White, Colors.Black).ToString(), Is.EqualTo(Colors.Gray.ToString()));
            Assert.That(Color.Blend(new[] { Colors.White, Colors.Black }).ToString(), Is.EqualTo(Colors.Gray.ToString()));
        });
        Assert.Throws<InvalidOperationException>(() => Color.Blend(Array.Empty<Color>()));
    }

    [Test]
    public void Similarity_Test()
    {
        Assert.Multiple(() =>
        {
            Assert.That(Color.Similarity(Colors.Black, Colors.White), Is.EqualTo(0f));
            Assert.That(Color.Similarity(Colors.Red, Colors.Green), Is.EqualTo(0f));
            Assert.That(Color.Similarity(Colors.Black, Colors.IvoryBlack), Is.InRange(0.5f, 0.6f));
            Assert.That(Color.Similarity(Colors.Blue, Colors.Blue2), Is.InRange(0.9f, 1f));
        });
    }

    [Test]
    public void AreClose_Test()
    {
        Assert.That(Color.AreClose(Colors.Red, Colors.Red2), Is.True);
        Assert.Throws<ArgumentOutOfRangeException>(() => Color.AreClose(Colors.Red, Colors.Blue, 1.1f));
        Assert.Throws<ArgumentOutOfRangeException>(() => Color.AreClose(Colors.Red, Colors.Blue, -0.1f));
    }

    [TestCase("Brown1")]
    [TestCase("#FF4040FF")]
    [TestCase("#4040FF")]
    [TestCase("#F44F")]
    [TestCase("#44F")]
    public void TryParse_Test(string value)
    {
        Assert.Multiple(() =>
        {
            Assert.That(Color.TryParse(value, out Color c), Is.True);
            Assert.That(Color.AreClose(Colors.Brown1, c), Is.True);
        });
    }

    [TestCase("test")]
    [TestCase("#12345678901234567890")]
    [TestCase("1234")]
    [TestCase("#FF")]
    [TestCase("")]
    [TestCase(null)]
    public void TryParse_Fails_Parse_Test(string value)
    {
        Assert.Multiple(() =>
        {
            Assert.That(Color.TryParse(value, out Color c), Is.False);
            Assert.That(c, Is.EqualTo(default(Color)));
        });
    }

    [TestCase("Brown1")]
    [TestCase("#FF4040FF")]
    [TestCase("#4040FF")]
    [TestCase("#F44F")]
    [TestCase("#44F")]
    public void Parse_Test(string value)
    {
        Assert.That(Color.AreClose(Colors.Brown1, Color.Parse(value)), Is.True);
    }

    [TestCase("test")]
    [TestCase("#12345678901234567890")]
    [TestCase("1234")]
    [TestCase("#FF")]
    [TestCase("")]
    [TestCase(null)]
    public void Parse_Fails_Parse_Test(string value)
    {
        Assert.Throws<FormatException>(() => Color.Parse(value));
    }

    [Test]
    public void Add_Operator_Test()
    {
        Assert.Multiple(() =>
        {
            Assert.That(Colors.Gray + Colors.Black, Is.EqualTo(Colors.Gray));
            Assert.That(Colors.Red + Colors.Lime, Is.EqualTo(Colors.Yellow));
            Assert.That(Colors.Red + Colors.Lime + Colors.Blue, Is.EqualTo(Colors.White));
            Assert.That(Colors.Gray + Colors.Green, Is.EqualTo(new Color(127, 255, 127)));
        });
    }

    [Test]
    public void Subtract_Operator_Test()
    {
        Assert.Multiple(() =>
        {
            Assert.That((Colors.Gray - Colors.Black).Opaque(), Is.EqualTo(Colors.Gray));
            Assert.That(Colors.White - Colors.Red - Colors.Lime - Colors.Blue, Is.EqualTo(Color.Transparent));
            Assert.That((Colors.Yellow - Colors.Lime).Opaque(), Is.EqualTo(Colors.Red));
            Assert.That((Colors.Gray - Colors.Green).Opaque(), Is.EqualTo(new Color(127, 0, 127)));
        });
    }

    [Test]
    public void Multiply_Operator_Test()
    {
        Assert.Multiple(() =>
        {
            Assert.That(Color.AreClose(Colors.White, Colors.Gray * 2f), Is.True);
            Assert.That((new Color(16, 32, 48) * 2).ToString(), Is.EqualTo("#FF204060"));
        });
    }

    [Test]
    public void Divide_Operator_Test()
    {
        Assert.Multiple(() =>
        {
            Assert.That(Color.AreClose(Colors.Gray, (Colors.White / 2f).Opaque()), Is.True);
            Assert.That((new Color(32, 64, 96) / 2f).ToString(), Is.EqualTo("#7F102030"));
        });
    }

    [Test]
    public void Equals_Operator_Test()
    {
        Assert.Multiple(() =>
        {
            Assert.That(Colors.TrueGray, Is.EqualTo(new Color(0.5f, 0.5f, 0.5f)));
            Assert.That(Colors.Red, Is.Not.EqualTo(new Color(0.5f, 0.5f, 0.5f)));
        });
    }

    [Test]
    public void Not_Equals_Operator_Test()
    {
        Assert.Multiple(() =>
        {
            Assert.That(Colors.TrueGray, Is.EqualTo(new Color(0.5f, 0.5f, 0.5f)));
            Assert.That(Colors.Red, Is.Not.EqualTo(new Color(0.5f, 0.5f, 0.5f)));
        });
    }

    [Test]
    public void Properties_Test()
    {
        Color c = Colors.Black;

        c.R = 127;
        Assert.That(c.ScR, Is.InRange(0.49f, 0.51f));
        c.ScR = 0.75f;
        Assert.That(c.R, Is.InRange((byte)191, (byte)193));

        c.G = 127;
        Assert.That(c.ScG, Is.InRange(0.49f, 0.51f));
        c.ScG = 0.75f;
        Assert.That(c.G, Is.InRange((byte)191, (byte)193));

        c.B = 127;
        Assert.That(c.ScB, Is.InRange(0.49f, 0.51f));
        c.ScB = 0.75f;
        Assert.That(c.B, Is.InRange((byte)191, (byte)193));

        c.A = 127;
        Assert.That(c.ScA, Is.InRange(0.49f, 0.51f));
        c.ScA = 0.75f;
        Assert.That(c.A, Is.InRange((byte)191, (byte)193));
    }

    [Theory]
    [TestCase(null, ExpectedResult = "#FF4B0082")]
    [TestCase("", ExpectedResult = "#FF4B0082")]
    [TestCase("H", ExpectedResult = "#FF4B0082")]
    [TestCase("h", ExpectedResult = "#ff4b0082")]
    [TestCase("B", ExpectedResult = "A:255 R:75 G:0 B:130")]
    [TestCase("b", ExpectedResult = "a:255 r:75 g:0 b:130")]
    [TestCase("-RRGGBB-", ExpectedResult = "-4B0082-")]
    [TestCase("-rrggbb-", ExpectedResult = "-4b0082-")]
    [TestCase("-BBGGRR-", ExpectedResult = "-82004B-")]
    [TestCase("-bbggrr-", ExpectedResult = "-82004b-")]
    public string ToString_With_Formatting_Test(string? format)
    {
        return Colors.Indigo.ToString(format);
    }
    
    [Theory]
    [TestCase("F", ExpectedResult = "A:1 R:0.29411766 G:0 B:0.50980395")]
    [TestCase("f", ExpectedResult = "a:1 r:0.29411766 g:0 b:0.50980395")]
    public string ToString_with_invariant_culture_test(string? format)
    {
        return Colors.Indigo.ToString(format, CultureInfo.InvariantCulture);
    }

    [Test]
    public void ToString_Test()
    {
        Assert.That(Colors.Indigo.ToString(), Is.EqualTo("#FF4B0082"));
    }

    [Test]
    public void Clone_Test()
    {
        Color c = Colors.Pick();
        Color c2 = c.Clone();
        Assert.That(c, Is.EqualTo(c2));
        c2.R = (byte)(c2.R + 50).Wrap(0, 255);
        Assert.That(c, Is.Not.EqualTo(c2));
    }

    [Test]
    public void System_Drawing_Color_Cast_Test()
    {
        Color c = Colors.Pick();
        System.Drawing.Color d = (System.Drawing.Color)c;
        System.Drawing.Color e = ((ICastable<System.Drawing.Color>)c).Cast();
        Assert.Multiple(() =>
        {
            Assert.That(d.R, Is.EqualTo(c.R));
            Assert.That(d.G, Is.EqualTo(c.G));
            Assert.That(d.B, Is.EqualTo(c.B));
            Assert.That(d.A, Is.EqualTo(c.A));
            Assert.That(e, Is.EqualTo(d));
        });
    }

    [Test]
    public void CompareTo_Test()
    {
        Assert.That(Colors.Black.CompareTo(Colors.White), Is.EqualTo(-1));
    }

    [Test]
    public void Equals_Test()
    {
        Assert.Multiple(() =>
        {
            Assert.That(Colors.Red.Equals(Colors.Red), Is.True);
            Assert.That(Colors.Red.Equals((object?)Colors.Red), Is.True);
            Assert.That(Colors.Red.Equals((object)new ColorTest1 { R = 255 }), Is.True);
            Assert.That(Colors.Red.Equals((object)new ColorTest2 { ScR = 1f }), Is.True);

            Assert.That(Colors.Red.Equals((object)System.Drawing.Color.Red), Is.True);
            Assert.That(Colors.Red.Equals((IColor)Colors.Red), Is.True);
            Assert.That(Colors.Red.Equals((IScColor)Colors.Red), Is.True);

            Assert.That(Colors.Red.Equals(Colors.Green), Is.False);
            Assert.That(Colors.Red.Equals(System.Drawing.Color.Green), Is.False);
            Assert.That(Colors.Red.Equals((IColor)Colors.Green), Is.False);
            Assert.That(Colors.Red.Equals((IScColor)Colors.Green), Is.False);
            Assert.That(Colors.Red.Equals((object?)null), Is.False);
            Assert.That(Colors.Red.Equals(new Exception()), Is.False);
            Assert.That(Colors.Red.Equals(DayOfWeek.Friday), Is.False);
        });
    }

    [Test]
    public void WithAlpha_Test()
    {
        Color a = new(1f, 0.9f, 0.8f, 1f);
        Color b = new(1f, 0.9f, 0.8f, 0.5f);
        Color c = a.WithAlpha(0.5f);
        Assert.Multiple(() =>
        {
            Assert.That(c, Is.EqualTo(b));
            Assert.That(c, Is.Not.EqualTo(a));
        });
    }

    [Test]
    public void WithAlpha_Contract_Test()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => Colors.Red.WithAlpha(1.1f));
        Assert.Throws<ArgumentOutOfRangeException>(() => Colors.Red.WithAlpha(-0.1f));
    }

    [Test]
    public void From_To_Test()
    {
        short sourceValue = 0x7abc;
        (byte a, byte r, byte g, byte b) = (0x77, 0xcc, 0xbb, 0xaa);

        var c = Color.From<short, Abgr4444ColorParser>(sourceValue);
        Assert.Multiple(() =>
        {
            Assert.That(c.A, Is.EqualTo(a));
            Assert.That(c.R, Is.EqualTo(r));
            Assert.That(c.G, Is.EqualTo(g));
            Assert.That(c.B, Is.EqualTo(b));
        });
        var backValue = Color.To<short, Abgr4444ColorParser>(c);
        Assert.That(backValue, Is.EqualTo(sourceValue));
    }

    [Test]
    public void Transparent_Test()
    {
        Color a = new(1f, 0.9f, 0.8f, 1f);
        Color b = new(1f, 0.9f, 0.8f, 0f);
        Color c = a.Transparent();
        Assert.That(c, Is.EqualTo(b));
        Assert.That(c, Is.Not.EqualTo(a));
    }

    [Test]
    public void GetHashCode_Test()
    {
        Color c = Colors.Pick();
        Color d = c.Clone();
        Color e = Colors.Pick();
        Assert.Multiple(() =>
        {
            Assert.That(d.GetHashCode(), Is.EqualTo(c.GetHashCode()));
            Assert.That(e.GetHashCode(), Is.Not.EqualTo(c.GetHashCode()));
        });
    }

    [Test]
    public void Colors_Test()
    {
        foreach (PropertyInfo j in typeof(Colors).GetProperties(BindingFlags.Public | BindingFlags.Static))
        {
            Assert.Multiple(() =>
            {
                Assert.That(j.CanRead, Is.True);
                Assert.That(j.CanWrite, Is.False);
                Assert.That(j.PropertyType, Is.EqualTo(typeof(Color)));
            });
            Assert.That(j.GetValue(null), Is.AssignableFrom<Color>());
        }
    }

    [ExcludeFromCodeCoverage]
    private class ColorTest1 : IColor
    {
        public byte A { get; set; } = 255;
        public byte B { get; set; }
        public byte G { get; set; }
        public byte R { get; set; }
    }

    [ExcludeFromCodeCoverage]
    private class ColorTest2 : IScColor
    {
        public float ScA { get; set; } = 1f;
        public float ScB { get; set; }
        public float ScG { get; set; }
        public float ScR { get; set; }
    }
}
