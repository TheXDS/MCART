/*
ColorParserTests.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

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
using TheXDS.MCART.Types;
using TheXDS.MCART.Types.Extensions;
using System.Reflection;

namespace TheXDS.MCART.Tests.Types;

public class ColorParserTests
{
    [Theory]
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
    [TestCase(typeof(ColorimetricGrayscaleColorParser), (byte)0xff, 0xff, 0xff, 0xff, 0xff)]
    [TestCase(typeof(ColorimetricGrayscaleColorParser), (byte)0x0, 0xff, 0x0, 0x0, 0x0)]
    [TestCase(typeof(MonochromeColorParser), true, 0xff, 0xff, 0xff, 0xff)]
    [TestCase(typeof(MonochromeColorParser), false, 0xff, 0x00, 0x00, 0x00)]
    [TestCase(typeof(GrayscaleColorParser), (byte)0, 0xff, 0x00, 0x00, 0x00)]
    [TestCase(typeof(GrayscaleColorParser), (byte)0xff, 0xff, 0xff, 0xff, 0xff)]
    [TestCase(typeof(GrayscaleColorParser), (byte)0x40, 0xff, 0x40, 0x40, 0x40)]
    [TestCase(typeof(Rgb233ColorParser), (byte)0b_11000000, 0xff, 0xff, 0x00, 0x00)]
    [TestCase(typeof(Rgb233ColorParser), (byte)0b_00111000, 0xff, 0x00, 0xff, 0x00)]
    [TestCase(typeof(Rgb233ColorParser), (byte)0b_00000111, 0xff, 0x00, 0x00, 0xff)]
    [TestCase(typeof(VgaAttributeByteColorParser), (byte)0x07, 0xff, 0x7f, 0x7f, 0x7f)]
    [TestCase(typeof(VgaAttributeByteColorParser), (byte)0x0f, 0xff, 0xff, 0xff, 0xff)]
    [TestCase(typeof(VgaAttributeByteColorParser), (byte)0x01, 0xff, 0x0, 0x0, 0x7f)]
    public void Convert_From_And_To_Color_Test(Type converter, object sourceValue, byte a, byte r, byte g, byte b)
    {
        object c = converter.New();
        MethodInfo f = converter.GetMethod("From")!;
        MethodInfo t = converter.GetMethod("To")!;

        Color expected = new(r, g, b, a);
        Color actual = (Color)f.Invoke(c, new[] { sourceValue })!;
        object convertBack = t.Invoke(c, new object[] { actual })!;

        Assert.Multiple(() =>
        {
            Assert.That(expected, Is.EqualTo(actual));
            Assert.That(convertBack, Is.EqualTo(sourceValue));
        });
    }
}
