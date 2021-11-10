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

using NUnit.Framework;
using System;
using System.Diagnostics.CodeAnalysis;
using TheXDS.MCART.Helpers;
using TheXDS.MCART.Math;
using TheXDS.MCART.Resources;
using TheXDS.MCART.Types;
using TheXDS.MCART.Types.Base;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.Tests.Types
{
    public class ColorTests
    {
        [Theory]
        [TestCase(0f, 0, 0, 255)]
        [TestCase(0.25f, 0, 255, 255)]
        [TestCase(0.5f, 0, 255, 0)]
        [TestCase(0.75f, 255, 255, 0)]
        [TestCase(1f, 255, 0, 0)]
        [CLSCompliant(false)]
        public void BlendHeat_Test(float value, int r, int g, int b)
        {
            var c = Color.BlendHeat(value);
            Assert.True(((int)c.R).IsBetween(r - 1, r + 1));
            Assert.True(((int)c.G).IsBetween(g - 1, g + 1));
            Assert.True(((int)c.B).IsBetween(b - 1, b + 1));
        }

        [Theory]
        [TestCase(0f, 255, 0)]
        [TestCase(0.25f, 255, 127)]
        [TestCase(0.5f, 255, 255)]
        [TestCase(0.75f, 127, 255)]
        [TestCase(1f, 0, 255)]
        [CLSCompliant(false)]
        public void BlendHealth_Test(float value, int r, int g)
        {
            var c = Color.BlendHealth(value);
            Assert.True(((int)c.R).IsBetween(r - 1, r + 1));
            Assert.True(((int)c.G).IsBetween(g - 1, g + 1));
        }

        [Test]
        public void Blend_Test()
        {
            Assert.AreEqual(Colors.Gray.ToString(), Color.Blend(Colors.White, Colors.Black).ToString());
            Assert.AreEqual(Colors.Gray.ToString(), Color.Blend(new[] { Colors.White, Colors.Black }).ToString());
            Assert.Throws<InvalidOperationException>(() => Color.Blend(Array.Empty<Color>()));
        }

        [Test]
        public void Similarity_Test()
        {
            Assert.AreEqual(0f, Color.Similarity(Colors.Black, Colors.White));
            Assert.AreEqual(0f, Color.Similarity(Colors.Red, Colors.Green));
            Assert.True(Color.Similarity(Colors.Black, Colors.IvoryBlack).IsBetween(0.5f, 0.6f));
            Assert.True(Color.Similarity(Colors.Blue, Colors.Blue2).IsBetween(0.9f, 1f));
        }

        [Test]
        public void AreClose_Test()
        {
            Assert.True(Color.AreClose(Colors.Red, Colors.Red2));
            Assert.Throws<ArgumentOutOfRangeException>(() => Color.AreClose(Colors.Red, Colors.Blue, 1.1f));
            Assert.Throws<ArgumentOutOfRangeException>(() => Color.AreClose(Colors.Red, Colors.Blue, -0.1f));
        }


        [CLSCompliant(false)]
        [TestCase("Brown1")]
        [TestCase("#FF4040FF")]
        [TestCase("#4040FF")]
        [TestCase("#F44F")]
        [TestCase("#44F")]
        public void TryParse_Test(string value)
        {
            Assert.True(Color.TryParse(value, out Color c));
            Assert.True(Color.AreClose(Colors.Brown1, c));
        }

        [Test]
        public void TryParse_Fails_Parse_Test()
        {
            Assert.False(Color.TryParse("test", out var c));
            Assert.AreEqual(default(Color), c);
        }

        [CLSCompliant(false)]
        [TestCase("Brown1")]
        [TestCase("#FF4040FF")]
        [TestCase("#4040FF")]
        [TestCase("#F44F")]
        [TestCase("#44F")]
        public void Parse_Test(string value)
        {
            Assert.True(Color.AreClose(Colors.Brown1, Color.Parse(value)));
        }

        [Test]
        public void Parse_Fails_Parse_Test()
        {
            Assert.Throws<FormatException>(() => Color.Parse("test"));
        }

        [Test]
        public void Add_Operator_Test()
        {
            Assert.AreEqual(Colors.Gray, Colors.Gray + Colors.Black);
            Assert.AreEqual(Colors.Yellow, Colors.Red + Colors.Lime);
            Assert.AreEqual(Colors.White, Colors.Red + Colors.Lime + Colors.Blue);
            Assert.AreEqual(new Color(127, 255, 127), Colors.Gray + Colors.Green);
        }

        [Test]
        public void Sustract_Operator_Test()
        {
            Assert.AreEqual(Colors.Gray, (Colors.Gray - Colors.Black).Opaque());
            Assert.AreEqual(Color.Transparent, Colors.White - Colors.Red - Colors.Lime - Colors.Blue);
            Assert.AreEqual(Colors.Red, (Colors.Yellow - Colors.Lime).Opaque());
            Assert.AreEqual(new Color(127, 0, 127), (Colors.Gray - Colors.Green).Opaque());
        }

        [Test]
        public void Multiply_Operator_Test()
        {
            Assert.True(Color.AreClose(Colors.White, Colors.Gray * 2f));
            Assert.AreEqual("#FF204060", (new Color(16, 32, 48) * 2).ToString());
        }

        [Test]
        public void Divide_Operator_Test()
        {
            Assert.True(Color.AreClose(Colors.Gray, (Colors.White / 2f).Opaque()));
            Assert.AreEqual("#7F102030", (new Color(32, 64, 96) / 2f).ToString());
        }

        [Test]
        public void Equals_Operator_Test()
        {
            Assert.True(Colors.TrueGray == new Color(0.5f, 0.5f, 0.5f));
            Assert.False(Colors.Red == new Color(0.5f, 0.5f, 0.5f));
        }

        [Test]
        public void Not_Equals_Operator_Test()
        {
            Assert.False(Colors.TrueGray != new Color(0.5f, 0.5f, 0.5f));
            Assert.True(Colors.Red != new Color(0.5f, 0.5f, 0.5f));
        }

        [Test]
        public void Properties_Test()
        {
            var c = Colors.Black;

            c.R = 127;
            Assert.True(c.ScR.IsBetween(0.49f, 0.51f));
            c.ScR = 0.75f;
            Assert.True(c.R.IsBetween((byte)191, (byte)193));

            c.G = 127;
            Assert.True(c.ScG.IsBetween(0.49f, 0.51f));
            c.ScG = 0.75f;
            Assert.True(c.G.IsBetween((byte)191, (byte)193));

            c.B = 127;
            Assert.True(c.ScB.IsBetween(0.49f, 0.51f));
            c.ScB = 0.75f;
            Assert.True(c.B.IsBetween((byte)191, (byte)193));

            c.A = 127;
            Assert.True(c.ScA.IsBetween(0.49f, 0.51f));
            c.ScA = 0.75f;
            Assert.True(c.A.IsBetween((byte)191, (byte)193));
        }

        [CLSCompliant(false)]
        [TestCase(null, ExpectedResult = "#FF4B0082")]
        [TestCase("", ExpectedResult = "#FF4B0082")]
        [TestCase("H", ExpectedResult = "#FF4B0082")]
        [TestCase("h", ExpectedResult = "#ff4b0082")]
        [TestCase("B", ExpectedResult = "A:255 R:75 G:0 B:130")]
        [TestCase("b", ExpectedResult = "a:255 r:75 g:0 b:130")]
        [TestCase("F", ExpectedResult = "A:1 R:0.29411766 G:0 B:0.50980395")]
        [TestCase("f", ExpectedResult = "a:1 r:0.29411766 g:0 b:0.50980395")]
        [TestCase("-RRGGBB-", ExpectedResult = "-4B0082-")]
        [TestCase("-rrggbb-", ExpectedResult = "-4b0082-")]
        [TestCase("-BBGGRR-", ExpectedResult = "-82004B-")]
        [TestCase("-bbggrr-", ExpectedResult = "-82004b-")]
        public string ToString_With_Formatting_Test(string? format)
        {
            return Colors.Indigo.ToString(format);
        }

        [Test]
        public void ToString_Test()
        {
            Assert.AreEqual("#FF4B0082", Colors.Indigo.ToString());
        }

        [Test]
        public void Clone_Test()
        {
            var c = Colors.Pick();
            var c2 = c.Clone();
            Assert.True(c == c2);
            c2.R = (byte)(c2.R + 50).Wrap(0, 255);
            Assert.False(c == c2);
        }

        [Test]
        public void System_Drawing_Color_Cast_Test()
        {
            var c = Colors.Pick();
            var d = (System.Drawing.Color)c;
            var e = ((ICasteable<System.Drawing.Color>)c).Cast();

            Assert.AreEqual(c.R, d.R);
            Assert.AreEqual(c.G, d.G);
            Assert.AreEqual(c.B, d.B);
            Assert.AreEqual(c.A, d.A);

            Assert.AreEqual(d, e);
        }

        [Test]
        public void CompareTo_Test()
        {
            Assert.AreEqual(-1, Colors.Black.CompareTo(Colors.White));
        }

        [Test]
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

        [Test]
        public void GetHashCode_Test()
        {
            var c = Colors.Pick();
            var d = c.Clone();
            var e = Colors.Pick();

            Assert.AreEqual(c.GetHashCode(), d.GetHashCode());
            Assert.AreNotEqual(c.GetHashCode(), e.GetHashCode());
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
}