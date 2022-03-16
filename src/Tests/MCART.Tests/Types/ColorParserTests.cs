/*
ColorParserTests.cs

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

namespace TheXDS.MCART.Tests.Types;
using NUnit.Framework;
using System;
using TheXDS.MCART.Types;
using TheXDS.MCART.Types.Factory;

public class ColorParserTests
{
    [Theory]
    [CLSCompliant(false)]
    [TestCase(typeof(Abgr4444ColorParser), (short)0x7abc, 0x77, 0xcc, 0xbb, 0xaa)]
    [TestCase(typeof(Abgr4444ColorParser), (short)0x7f04, 0x77, 0x44, 0x00, 0xff)]
    [TestCase(typeof(Bgr12ColorParser), (short)0xabc, 0xff, 0xcc, 0xbb, 0xaa)]
    [TestCase(typeof(Bgr12ColorParser), (short)0xf04, 0xff, 0x44, 0x00, 0xff)]
    [TestCase(typeof(Bgr24ColorParser), 0x00abcdef, 0xff, 0xef, 0xcd, 0xab)]
    [TestCase(typeof(Abgr16ColorParser), unchecked((short)0xfabc), 0xff, 0xe6, 0xab, 0xf5)]
    [TestCase(typeof(Abgr16ColorParser), unchecked((short)0x801f), 0xff, 0xff, 0x0, 0x0)]
    [TestCase(typeof(Abgr2222ColorParser), (byte)0xC7, 0xff, 0xff, 0x55, 0x0)]
    [TestCase(typeof(Bgr222ColorParser), (byte)0x07, 0xff, 0xff, 0x55, 0x0)]
    [TestCase(typeof(Bgr555ColorParser), (short)0x7abc, 0xff, 0xe6, 0xac, 0xf6)]
    [TestCase(typeof(Bgr555ColorParser), (short)0x001f, 0xff, 0xff, 0x0, 0x0)]
    [TestCase(typeof(Bgr565ColorParser), (short)0x7abc, 0xff, 0xe6, 0x55, 0x7b)]
    [TestCase(typeof(Bgr565ColorParser), (short)0x001f, 0xff, 0xff, 0x0, 0x0)]
    [TestCase(typeof(MonochromeColorParser), true, 0xff, 0xff, 0xff, 0xff)]
    [TestCase(typeof(MonochromeColorParser), false, 0xff, 0x00, 0x00, 0x00)]
    [TestCase(typeof(GrayscaleColorParser), (byte)0, 0xff, 0x00, 0x00, 0x00)]
    [TestCase(typeof(GrayscaleColorParser), (byte)0xff, 0xff, 0xff, 0xff, 0xff)]
    [TestCase(typeof(GrayscaleColorParser), (byte)0x40, 0xff, 0x40, 0x40, 0x40)]
    [TestCase(typeof(VgaAttributeByteColorParser), (byte)0x07, 0xff, 0x7f, 0x7f, 0x7f)]
    [TestCase(typeof(VgaAttributeByteColorParser), (byte)0x0f, 0xff, 0xff, 0xff, 0xff)]
    [TestCase(typeof(VgaAttributeByteColorParser), (byte)0x01, 0xff, 0x0, 0x0, 0x7f)]
    public void Convert_From_And_To_Color_Test(Type converter, object sourceValue, byte a, byte r, byte g, byte b)
    {
        object? c = converter.New();
        System.Reflection.MethodInfo? f = converter.GetMethod("From")!;
        System.Reflection.MethodInfo? t = converter.GetMethod("To")!;

        Color expected = new(r, g, b, a);
        Color actual = (Color)f.Invoke(c, new[] { sourceValue })!;
        object? convertBack = t.Invoke(c, new object[] { actual })!;

        Assert.True(expected == actual);
        Assert.AreEqual(sourceValue, convertBack);
    }
}
