/*
DateTimeExtensionsTests.cs

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
using System.Globalization;
using TheXDS.MCART.Types.Extensions;
using Xunit;

namespace TheXDS.MCART.Tests.Types.Extensions
{
    public class DateTimeExtensionsTests
    {
        [Fact]
        public void EpochTest()
        {
            var e = DateTimeExtensions.Epoch(1970);
            
            Assert.Equal(1,e.Day);
            Assert.Equal(1,e.Month);
            Assert.Equal(1970,e.Year);
        }

        [Fact]
        public void Epochs_Test()
        {
            Assert.Equal(1900,DateTimeExtensions.CenturyEpoch.Year);
            Assert.Equal(2000,DateTimeExtensions.Y2KEpoch.Year);
            Assert.Equal(1970,DateTimeExtensions.UnixEpoch.Year);
        }

        [Fact]
        public void ToUnixTimestamp_Test()
        {
            var t = new DateTime(2038, 1, 19, 3, 14, 7);
            Assert.Equal(int.MaxValue,t.ToUnixTimestamp());
        }
        
        [Fact]
        public void ToUnixTimestampMs_Test()
        {
            var t = new DateTime(2012, 5, 19, 19, 35, 0);
            Assert.Equal(1337456100000,t.ToUnixTimestampMs());
        }
        
        [Fact]
        public void FromUnixTimestamp_Test()
        {
            var t = new DateTime(2038, 1, 19, 3, 14, 7);
            Assert.Equal(t,DateTimeExtensions.FromUnixTimestamp(int.MaxValue));
        }
        
        [Fact]
        public void FromUnixTimestampMs_Test()
        {
            var t = new DateTime(2012, 5, 19, 19, 35, 0);
            Assert.Equal(t,1337456100000.FromUnixTimestampMs());
        }
 
        [Fact]
        public void MonthName_Test()
        {
            Assert.Equal("August",DateTimeExtensions.MonthName(8, CultureInfo.CreateSpecificCulture("en-us")));
            Assert.Throws <ArgumentOutOfRangeException>(() => DateTimeExtensions.MonthName(0, CultureInfo.CurrentCulture));
            Assert.Throws <ArgumentOutOfRangeException>(() => DateTimeExtensions.MonthName(13, CultureInfo.CurrentCulture));
            
            var t = DateTime.Today;
            Assert.Equal(t.ToString("MMMM"), DateTimeExtensions.MonthName(t.Month));
        }
    }
}