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

#pragma warning disable CS1591

using System;
using Xunit;
using static TheXDS.MCART.Types.Extensions.EnumExtensions;
using static TheXDS.MCART.Types.Extensions.NamedObjectExtensions;

namespace TheXDS.MCART.Tests.Types.Extensions
{
    public class EnumExtensionsTests
    {
        [Fact]
        public void ToBytesTest()
        {
            Assert.Equal(new byte[] { 1, 0, 0, 0 }, DayOfWeek.Monday.ToBytes());
            Assert.Equal(new byte[] { 0 }, TestByteEnum.Zero.ToBytes());
            Assert.Equal(new byte[] { 1 }, TestByteEnum.One.ToBytes());
            Assert.Equal(new byte[] { 2 }, TestByteEnum.Two.ToBytes());
        }

        [Fact]
        public void ByteConversionMethodTest()
        {
            var a = ByteConversionMethod<DayOfWeek>();
            Assert.NotNull(a);

            var b = BitConverter.GetBytes((int)DayOfWeek.Monday);
            var c = (byte[]) a.Invoke(null, new object[] { DayOfWeek.Monday })!;
            Assert.Equal(b, c);

            Assert.Throws<ArgumentException>(() => ByteConversionMethod(typeof(bool)));

            var d = ByteConversionMethod(typeof(DayOfWeek));
            Assert.Same(a,d);
        }

        [Fact]
        public void ToBytesFunctionTest()
        {
            var a = BitConverter.GetBytes((int)DayOfWeek.Monday);
            var b = ToBytes<DayOfWeek>();

            Assert.Equal(a, b(DayOfWeek.Monday));
        }

        private enum TestByteEnum : byte
        {
            Zero,
            One,
            Two
        }
    }
    public class NamedObjectExtensionsTests
    {
        [Fact]
        public void AsNamedEnumTest()
        {
            var e = typeof(DayOfWeek).AsNamedEnum();

            foreach (var j in e)
            {
                Assert.Equal(j.Value.ToString(), j.Name);
            }
        }
    }
}