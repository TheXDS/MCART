﻿/*
BooleanConverterTests.cs

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

using System.Globalization;
using TheXDS.MCART.ValueConverters;
using Xunit;

namespace TheXDS.MCART.WpfTests.ValueConverters
{
    public class BooleanConverterTests
    {
        [Fact]
        public void InstanceTest()
        {
            var c = new BooleanConverter<int>(5, -5);

            Assert.Equal(5, c.True);
            Assert.Equal(-5, c.False);
        }

        [Fact]
        public void ConversionTest()
        {
            var c = new BooleanConverter<int>(5, -5);

            Assert.Equal(5, c.Convert(true, typeof(int), null, CultureInfo.CurrentCulture));
            Assert.Equal(-5, c.Convert(false, typeof(int), null, CultureInfo.CurrentCulture));
        }

        [Fact]
        public void FailureTest()
        {
            var c = new BooleanConverter<int>(5, -5);
            Assert.Null(c.Convert(3, typeof(int), null, CultureInfo.CurrentCulture));            
            Assert.Null(c.Convert("Test", typeof(int), null, CultureInfo.CurrentCulture));            
        }

        [Fact]
        public void ConvertBackTest()
        {
            var c = new BooleanConverter<int>(5, -5);

            Assert.True((bool)c.ConvertBack(5, typeof(int), null, CultureInfo.CurrentCulture));
            Assert.False((bool)c.ConvertBack(-5, typeof(int), null, CultureInfo.CurrentCulture));
            Assert.Null(c.ConvertBack(2, typeof(int), null, CultureInfo.CurrentCulture));
        }
    }
}
