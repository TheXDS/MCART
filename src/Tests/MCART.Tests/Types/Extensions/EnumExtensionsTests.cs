/*
EnumExtensionsTests.cs

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

namespace TheXDS.MCART.Tests.Types.Extensions;
using NUnit.Framework;
using System;
using System.Linq;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Types;
using static TheXDS.MCART.Types.Factory.EnumExtensions;

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
