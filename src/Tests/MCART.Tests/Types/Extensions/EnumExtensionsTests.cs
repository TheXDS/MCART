/*
EnumExtensionsTests.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2022 César Andrés Morgan

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

namespace TheXDS.MCART.Tests.Types.Extensions;
using NUnit.Framework;
using System;
using System.Linq;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Types;
using static TheXDS.MCART.Types.Extensions.EnumExtensions;

public class EnumExtensionsTests
{
    [Test]
    public void ToBytesTest()
    {
        Assert.AreEqual(new byte[] { 1, 0, 0, 0 }, DayOfWeek.Monday.ToBytes());
        Assert.AreEqual(new byte[] { 0 }, TestByteEnum.Zero.ToBytes());
        Assert.AreEqual(new byte[] { 1 }, TestByteEnum.One.ToBytes());
        Assert.AreEqual(new byte[] { 2 }, TestByteEnum.Two.ToBytes());
    }

    [Test]
    public void ByteConversionMethodTest()
    {
        System.Reflection.MethodInfo? a = ByteConversionMethod<DayOfWeek>();
        Assert.NotNull(a);

        byte[]? b = BitConverter.GetBytes((int)DayOfWeek.Monday);
        byte[]? c = (byte[])a.Invoke(null, new object[] { DayOfWeek.Monday })!;
        Assert.AreEqual(b, c);

        Assert.Throws<ArgumentException>(() => ByteConversionMethod(typeof(bool)));

        System.Reflection.MethodInfo? d = ByteConversionMethod(typeof(DayOfWeek));
        Assert.AreSame(a, d);
    }

    [Test]
    public void ToBytesFunctionTest()
    {
        byte[]? a = BitConverter.GetBytes((int)DayOfWeek.Monday);
        Func<DayOfWeek, byte[]>? b = ToBytes<DayOfWeek>();

        Assert.AreEqual(a, b(DayOfWeek.Monday));
    }

    [Test]
    public void NamedEnumsTest()
    {
        static void TestValue(NamedObject<TestByteEnum> p, string name, TestByteEnum value)
        {
            Assert.AreEqual(name, p.Name);
            Assert.AreEqual(value, p.Value);
        }

        NamedObject<TestByteEnum>[]? l = NamedEnums<TestByteEnum>().ToArray();
        TestValue(l[0], "Number Zero", TestByteEnum.Zero);
        TestValue(l[1], "Number One", TestByteEnum.One);
        TestValue(l[2], "Number Two", TestByteEnum.Two);
    }

    [Test]
    public void ToUnderlyingTypeTest()
    {
        Assert.AreEqual((byte)0, TestByteEnum.Zero.ToUnderlyingType());
        Assert.IsAssignableFrom<byte>(TestByteEnum.Zero.ToUnderlyingType());

        Assert.AreEqual(1, DayOfWeek.Monday.ToUnderlyingType());
        Assert.IsAssignableFrom<int>(DayOfWeek.Monday.ToUnderlyingType());

        Assert.AreEqual((byte)0, ((Enum)TestByteEnum.Zero).ToUnderlyingType());
        Assert.IsAssignableFrom<byte>(((Enum)TestByteEnum.Zero).ToUnderlyingType());

        Assert.AreEqual(1, ((Enum)DayOfWeek.Monday).ToUnderlyingType());
        Assert.IsAssignableFrom<int>(((Enum)DayOfWeek.Monday).ToUnderlyingType());
    }

    private enum TestByteEnum : byte
    {
        [Name("Number Zero")] Zero,
        [MCART.Attributes.Description("Number One")] One,
        [System.ComponentModel.Description("Number Two")] Two
    }
}
