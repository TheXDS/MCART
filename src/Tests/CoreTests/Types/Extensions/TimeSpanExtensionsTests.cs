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
using TheXDS.MCART.Resources.Strings;
using TheXDS.MCART.Types.Extensions;
using NUnit.Framework;

namespace TheXDS.MCART.Tests.Types.Extensions
{
    public class TimeSpanExtensionsTests
    {
        [Test]
        public void VerboseTest()
        {
            Assert.AreEqual(Composition.Seconds(15), TimeSpan.FromSeconds(15).Verbose());
            Assert.AreEqual(Composition.Minutes(3), TimeSpan.FromSeconds(180).Verbose());
            Assert.AreEqual(Composition.Hours(2), TimeSpan.FromSeconds(7200).Verbose());
            Assert.AreEqual(Composition.Days(5), TimeSpan.FromDays(5).Verbose());

            Assert.AreEqual(
                $"{Composition.Minutes(1)}, {Composition.Seconds(5)}",
                TimeSpan.FromSeconds(65).Verbose());

            Assert.AreEqual(
                $"{Composition.Days(2)}, {Composition.Hours(5)}, {Composition.Minutes(45)}, {Composition.Seconds(23)}",
                (TimeSpan.FromDays(2) + TimeSpan.FromHours(5) + TimeSpan.FromMinutes(45) + TimeSpan.FromSeconds(23)).Verbose());
        }

        [Test]
        public void AsTimeTest()
        {
            TimeSpan t = TimeSpan.FromSeconds(60015);
            CultureInfo? c = CultureInfo.InvariantCulture;
            string? r = t.AsTime(c);
            Assert.AreEqual("16:40", r);
            Assert.AreEqual(
                string.Format($"{{0:{CultureInfo.CurrentCulture.DateTimeFormat.ShortTimePattern}}}",
                    DateTime.MinValue.Add(t)), t.AsTime());
        }
    }
}