/*
ColorTests.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

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
using System.Diagnostics.CodeAnalysis;
using TheXDS.MCART.Helpers;
using TheXDS.MCART.Math;
using TheXDS.MCART.Resources;
using TheXDS.MCART.Types;
using TheXDS.MCART.Types.Base;
using TheXDS.MCART.Types.Extensions;
using Xunit;

namespace TheXDS.MCART.Tests.Types
{
    public class ColorTests
    {
        [Theory]
        [InlineData(0f, 0, 0, 255)]
        [InlineData(0.25f, 0, 255, 255)]
        [InlineData(0.5f, 0, 255, 0)]
        [InlineData(0.75f, 255, 255, 0)]
        [InlineData(1f, 255, 0, 0)]
        [CLSCompliant(false)]
        public void BlendHeat_Test(float value, int r, int g, int b)
        {
            var c = Color.BlendHeat(value);
            Assert.True(((int)c.R).IsBetween(r - 1, r + 1));
            Assert.True(((int)c.G).IsBetween(g - 1, g + 1));
            Assert.True(((int)c.B).IsBetween(b - 1, b + 1));
        }
        
        [Theory]
        [InlineData(0f, 255, 0)]
        [InlineData(0.25f, 255, 127)]
        [InlineData(0.5f, 255, 255)]
        [InlineData(0.75f, 127, 255)]
        [InlineData(1f, 0, 255)]
        [CLSCompliant(false)]
        public void BlendHealth_Test(float value, int r, int g)
        {
            var c = Color.BlendHealth(value);
            Assert.True(((int)c.R).IsBetween(r - 1, r + 1));
            Assert.True(((int)c.G).IsBetween(g - 1, g + 1));
        }

        [Fact]
        public void Blend_Test()
        {
            Assert.Equal(Colors.Gray.ToString(), Color.Blend(Colors.White, Colors.Black).ToString());
            Assert.Equal(Colors.Gray.ToString(), Color.Blend(new[] { Colors.White, Colors.Black }).ToString());
            Assert.Throws<InvalidOperationException>(() => Color.Blend(Array.Empty<Color>()));
        }

        [Fact]
        public void Similarity_Test()
        {
            Assert.Equal(0f, Color.Similarity(Colors.Black, Colors.White));
            Assert.Equal(0f, Color.Similarity(Colors.Red, Colors.Green));
            Assert.InRange(Color.Similarity(Colors.Black, Colors.IvoryBlack), 0.5f, 0.6f);
            Assert.InRange(Color.Similarity(Colors.Blue, Colors.Blue2), 0.9f, 1f);
        }

        [Fact]
        public void AreClose_Test()
        {
            Assert.True(Color.AreClose(Colors.Red, Colors.Red2));
            Assert.Throws<ArgumentOutOfRangeException>(() => Color.AreClose(Colors.Red, Colors.Blue, 1.1f));
            Assert.Throws<ArgumentOutOfRangeException>(() => Color.AreClose(Colors.Red, Colors.Blue, -0.1f));
        }

        
        [Theory, CLSCompliant(false)]
        [InlineData("Brown1")]
        [InlineData("#FF4040FF")]
        [InlineData("#4040FF")]
        [InlineData("#F44F")]
        [InlineData("#44F")]
        public void TryParse_Test(string value)
        {
            Assert.True(Color.TryParse(value, out Color c));
            Assert.True(Color.AreClose(Colors.Brown1, c));
        }

        [Fact]
        public void TryParse_Fails_Parse_Test()
        {
            Assert.False(Color.TryParse("test", out var c));
            Assert.Equal(default, c);
        }
        
        [Theory, CLSCompliant(false)]
        [InlineData("Brown1")]
        [InlineData("#FF4040FF")]
        [InlineData("#4040FF")]
        [InlineData("#F44F")]
        [InlineData("#44F")]
        public void Parse_Test(string value)
        {
            Assert.True(Color.AreClose(Colors.Brown1, Color.Parse(value)));
        }
        
        [Fact]
        public void Parse_Fails_Parse_Test()
        {
            Assert.Throws<FormatException>(() => Color.Parse("test"));
        }

        [Fact]
        public void Add_Operator_Test()
        {
            Assert.Equal(Colors.Gray, Colors.Gray + Colors.Black);
            Assert.Equal(Colors.Yellow, Colors.Red + Colors.Lime);
            Assert.Equal(Colors.White, Colors.Red + Colors.Lime + Colors.Blue);
            Assert.Equal(new Color(127,255,127), Colors.Gray + Colors.Green);
        }

        [Fact]
        public void Sustract_Operator_Test()
        {
            Assert.Equal(Colors.Gray, (Colors.Gray - Colors.Black).Opaque());
            Assert.Equal(Color.Transparent, Colors.White - Colors.Red - Colors.Lime - Colors.Blue);
            Assert.Equal(Colors.Red, (Colors.Yellow - Colors.Lime).Opaque());
            Assert.Equal(new Color(127,0,127), (Colors.Gray - Colors.Green).Opaque());
        }

        [Fact]
        public void Multiply_Operator_Test()
        {
            Assert.True(Color.AreClose(Colors.White, Colors.Gray * 2f));
            Assert.Equal("#FF204060", (new Color(16, 32, 48) * 2).ToString());
        }
        
        [Fact]
        public void Divide_Operator_Test()
        {
            Assert.True(Color.AreClose(Colors.Gray, (Colors.White / 2f).Opaque()));
            Assert.Equal("#7F102030", (new Color(32, 64, 96) / 2f).ToString());
        }

        [Fact]
        public void Equals_Operator_Test()
        {
            Assert.True(Colors.TrueGray == new Color(0.5f, 0.5f,0.5f));
            Assert.False(Colors.Red == new Color(0.5f, 0.5f,0.5f));
        }
        
        [Fact]
        public void Not_Equals_Operator_Test()
        {
            Assert.False(Colors.TrueGray != new Color(0.5f, 0.5f,0.5f));
            Assert.True(Colors.Red != new Color(0.5f, 0.5f,0.5f));
        }
        
        [Fact]
        public void Properties_Test()
        {
            var c = Colors.Black;

            c.R = 127;
            Assert.InRange(c.ScR, 0.49f, 0.51f);
            c.ScR = 0.75f;
            Assert.InRange(c.R, 191, 193);
            
            c.G = 127;
            Assert.InRange(c.ScG, 0.49f, 0.51f);
            c.ScG = 0.75f;
            Assert.InRange(c.G, 191, 193);
            
            c.B = 127;
            Assert.InRange(c.ScB, 0.49f, 0.51f);
            c.ScB = 0.75f;
            Assert.InRange(c.B, 191, 193);
            
            c.A = 127;
            Assert.InRange(c.ScA, 0.49f, 0.51f);
            c.ScA = 0.75f;
            Assert.InRange(c.A, 191, 193);
        }

        [Theory, CLSCompliant(false)]
        [InlineData("#FF4B0082", null)]
        [InlineData("#FF4B0082", "")]
        [InlineData("#FF4B0082", "H")]
        [InlineData("#ff4b0082", "h")]
        [InlineData("A:255 R:75 G:0 B:130", "B")]
        [InlineData("a:255 r:75 g:0 b:130", "b")]
        [InlineData("A:1 R:0.29411766 G:0 B:0.50980395", "F")]
        [InlineData("a:1 r:0.29411766 g:0 b:0.50980395", "f")]
        [InlineData("-4B0082-", "-RRGGBB-")]
        [InlineData("-4b0082-", "-rrggbb-")]
        [InlineData("-82004B-", "-BBGGRR-")]
        [InlineData("-82004b-", "-bbggrr-")]
        public void ToString_With_Formatting_Test(string result, string? format)
        {
            Assert.Equal(result, Colors.Indigo.ToString(format));
        }

        [Fact]
        public void ToString_Test()
        {
            Assert.Equal("#FF4B0082", Colors.Indigo.ToString());
        }

        [Fact]
        public void Clone_Test()
        {
            var c = Colors.Pick();
            var c2 = c.Clone();
            Assert.True(c == c2);
            c2.R = (byte)(c2.R + 50).Wrap(0, 255);
            Assert.False(c == c2);
        }

        [Fact]
        public void System_Drawing_Color_Cast_Test()
        {
            var c = Colors.Pick();
            var d = (System.Drawing.Color) c;
            var e = ((ICasteable<System.Drawing.Color>) c).Cast();
            
            Assert.Equal(c.R, d.R);
            Assert.Equal(c.G, d.G);
            Assert.Equal(c.B, d.B);
            Assert.Equal(c.A, d.A);
            
            Assert.Equal(d, e);
        }

        [Fact]
        public void CompareTo_Test()
        {
            Assert.Equal(-1, Colors.Black.CompareTo(Colors.White));
        }

        [Fact]
        public void Equals_Test()
        {
            Assert.True(Colors.Red.Equals(Colors.Red));
            Assert.True(Colors.Red.Equals((object?)Colors.Red));
            Assert.True(Colors.Red.Equals((object?)new ColorTest1 { R = 255 }));
            Assert.True(Colors.Red.Equals((object?)new ColorTest2 { ScR = 1f }));
            
            Assert.True(Colors.Red.Equals((object?)System.Drawing.Color.Red));
            Assert.True(Colors.Red.Equals((IColor)Colors.Red));
            Assert.True(Colors.Red.Equals((IScColor)Colors.Red));
            
            Assert.False(Colors.Red.Equals(Colors.Green));
            Assert.False(Colors.Red.Equals(System.Drawing.Color.Green));
            Assert.False(Colors.Red.Equals((IColor)Colors.Green));
            Assert.False(Colors.Red.Equals((IScColor)Colors.Green));
            Assert.False(Colors.Red.Equals((object?)null));
            Assert.False(Colors.Red.Equals(new Exception()));
            Assert.False(Colors.Red.Equals(DayOfWeek.Friday));
        }

        [Fact]
        public void GetHashCode_Test()
        {
            var c = Colors.Pick();
            var d = c.Clone();
            var e = Colors.Pick();
            
            Assert.Equal(c.GetHashCode(), d.GetHashCode());
            Assert.NotEqual(c.GetHashCode(), e.GetHashCode());
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

    public class ColorParserTests
    {
        [Theory]
        [CLSCompliant(false)]
        [InlineData(typeof(Abgr4444ColorParser), (short)0x7abc, 0x77, 0xcc, 0xbb, 0xaa)]
        [InlineData(typeof(Abgr4444ColorParser), (short)0x7f04, 0x77, 0x44, 0x00, 0xff)]
        [InlineData(typeof(Bgr12ColorParser), (short)0xabc, 0xff, 0xcc, 0xbb, 0xaa)]
        [InlineData(typeof(Bgr12ColorParser), (short)0xf04, 0xff, 0x44, 0x00, 0xff)]
        [InlineData(typeof(Bgr24ColorParser), 0x00abcdef, 0xff, 0xef, 0xcd, 0xab)]
        [InlineData(typeof(Abgr16ColorParser), unchecked((short)0xfabc), 0xff, 0xe6, 0xab, 0xf5)]
        [InlineData(typeof(Abgr16ColorParser), unchecked((short)0x801f), 0xff, 0xff, 0x0, 0x0)]
        [InlineData(typeof(Abgr2222ColorParser), (byte)0xC7, 0xff, 0xff, 0x55, 0x0)]
        [InlineData(typeof(Bgr222ColorParser), (byte)0x07, 0xff, 0xff, 0x55, 0x0)]
        [InlineData(typeof(Bgr555ColorParser), (short)0x7abc, 0xff, 0xe6, 0xac, 0xf6)]
        [InlineData(typeof(Bgr555ColorParser), (short)0x001f, 0xff, 0xff, 0x0, 0x0)]
        [InlineData(typeof(MonochromeColorParser), true, 0xff, 0xff, 0xff, 0xff)]
        [InlineData(typeof(MonochromeColorParser), false, 0xff, 0x00, 0x00, 0x00)]
        [InlineData(typeof(GrayscaleColorParser), (byte)0, 0xff, 0x00, 0x00, 0x00)]
        [InlineData(typeof(GrayscaleColorParser), (byte)0xff, 0xff, 0xff, 0xff, 0xff)]
        [InlineData(typeof(GrayscaleColorParser), (byte)0x40, 0xff, 0x40, 0x40, 0x40)]
        [InlineData(typeof(VgaAttributeByteColorParser), (byte)0x07, 0xff, 0x7f, 0x7f, 0x7f)]
        [InlineData(typeof(VgaAttributeByteColorParser), (byte)0x0f, 0xff, 0xff, 0xff, 0xff)]
        [InlineData(typeof(VgaAttributeByteColorParser), (byte)0x01, 0xff, 0x0, 0x0, 0x7f)]
        public void Convert_From_And_To_Color_Test(Type converter, object sourceValue, byte a, byte r, byte g, byte b)
        {
            var c = converter.New();
            var f = converter.GetMethod("From")!;
            var t = converter.GetMethod("To")!;

            var expected = new Color(r, g, b, a);
            var actual = (Color)f.Invoke(c, new [] { sourceValue })!;
            var convertBack = t.Invoke(c, new object[] { actual })!;
            
            Assert.True(expected == actual);
            Assert.Equal(sourceValue, convertBack);
        }
    }
}