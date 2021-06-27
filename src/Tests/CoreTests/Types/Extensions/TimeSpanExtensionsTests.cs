/*
TimeSpanExtensionsTests.cs

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
using TheXDS.MCART.Resources;
using TheXDS.MCART.Types.Extensions;
using Xunit;

namespace TheXDS.MCART.Tests.Types.Extensions
{
    public class TimeSpanExtensionsTests
    {
        [Fact]
        public void VerboseTest()
        {
            Assert.Equal(Strings.Seconds(15),TimeSpan.FromSeconds(15).Verbose());
            Assert.Equal(Strings.Minutes(3),TimeSpan.FromSeconds(180).Verbose());
            Assert.Equal(Strings.Hours(2),TimeSpan.FromSeconds(7200).Verbose());
            Assert.Equal(Strings.Days(5),TimeSpan.FromDays(5).Verbose());

            Assert.Equal(
                $"{Strings.Minutes(1)}, {Strings.Seconds(5)}",
                TimeSpan.FromSeconds(65).Verbose());
            
            Assert.Equal(
                $"{Strings.Days(2)}, {Strings.Hours(5)}, {Strings.Minutes(45)}, {Strings.Seconds(23)}",
                (TimeSpan.FromDays(2) + TimeSpan.FromHours(5) + TimeSpan.FromMinutes(45) + TimeSpan.FromSeconds(23)).Verbose());
        }

        [Fact]
        public void AsTimeTest()
        {
            var t = TimeSpan.FromSeconds(60015);
            var c = CultureInfo.GetCultureInfo("en-UK");
            var r = t.AsTime(c);
            Assert.Equal("4:40 p.\u00A0m.", r);
            Assert.Equal(
                string.Format($"{{0:{CultureInfo.CurrentCulture.DateTimeFormat.ShortTimePattern}}}",
                    DateTime.MinValue.Add(t)), t.AsTime());
        }
    }
}