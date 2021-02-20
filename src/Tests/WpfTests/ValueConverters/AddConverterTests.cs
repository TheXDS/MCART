/*
AddConverterTests.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2020 César Andrés Morgan

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
using System.Globalization;
using TheXDS.MCART.ValueConverters;
using Xunit;

namespace TheXDS.MCART.WpfTests.ValueConverters
{
    public class AddConverterTests
    {
        [Fact]
        public void IntegerAddTest()
        {
            var c = new AddConverter();

            Assert.Equal(3, c.Convert(1, typeof(int), 2, CultureInfo.CurrentCulture));
            Assert.Equal(1, c.ConvertBack(3, typeof(int), 2, CultureInfo.CurrentCulture));
        }

        [Fact]
        public void MixedTypesTest()
        {
            var c = new AddConverter();

            Assert.Equal(3, c.Convert(1f, typeof(int), "2", CultureInfo.CurrentCulture));
            Assert.Equal(1, c.ConvertBack(3f, typeof(int), "2", CultureInfo.CurrentCulture));

        }

        [Fact]
        public void BigIntegerAddTest()
        {
            var c = new AddConverter();
            Assert.Equal(9999999999999999m, c.Convert(9999999999999990m, typeof(decimal), (byte)9, CultureInfo.CurrentCulture));
        }

        [Fact]
        public void CastDownTest()
        {
            var c = new AddConverter();
            Assert.Equal((byte)200, c.Convert(100L, typeof(byte), 100m, CultureInfo.CurrentCulture));
        }

        [Fact]
        public void SanityTests()
        {
            var c = new AddConverter();
            Assert.Equal(double.NaN, c.Convert(decimal.MaxValue, typeof(double), "Test", CultureInfo.CurrentCulture));
            Assert.Equal(float.NaN, c.Convert(double.MaxValue, typeof(float), "Test", CultureInfo.CurrentCulture));
            Assert.Equal(5, c.Convert(5, typeof(int), null, CultureInfo.CurrentCulture));
            Assert.Throws<OverflowException>(() => c.Convert(200L, typeof(byte), 200m, CultureInfo.CurrentCulture));
            Assert.Throws<NotSupportedException>(() => c.Convert(200L, typeof(byte), "Test", CultureInfo.CurrentCulture));
            Assert.Throws<ArgumentNullException>(() => c.Convert(null, typeof(byte), "5", CultureInfo.CurrentCulture));
        }
    }
}
