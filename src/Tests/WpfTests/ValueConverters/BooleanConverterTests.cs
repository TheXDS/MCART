/*
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

using System.Globalization;
using TheXDS.MCART.ValueConverters;
using NUnit.Framework;

namespace TheXDS.MCART.WpfTests.ValueConverters
{
    public class BooleanConverterTests
    {
        [Test]
        public void InstanceTest()
        {
            BooleanConverter<int>? c = new(5, -5);

            Assert.AreEqual(5, c.True);
            Assert.AreEqual(-5, c.False);
        }

        [Test]
        public void ConversionTest()
        {
            BooleanConverter<int>? c = new(5, -5);

            Assert.AreEqual(5, c.Convert(true, typeof(int), null, CultureInfo.CurrentCulture));
            Assert.AreEqual(-5, c.Convert(false, typeof(int), null, CultureInfo.CurrentCulture));
        }

        [Test]
        public void FailureTest()
        {
            BooleanConverter<int>? c = new(5, -5);
            Assert.Null(c.Convert(3, typeof(int), null, CultureInfo.CurrentCulture));
            Assert.Null(c.Convert("Test", typeof(int), null, CultureInfo.CurrentCulture));
        }

        [Test]
        public void ConvertBackTest()
        {
            BooleanConverter<int>? c = new(5, -5);

            Assert.True((bool)c.ConvertBack(5, typeof(int), null, CultureInfo.CurrentCulture)!);
            Assert.False((bool)c.ConvertBack(-5, typeof(int), null, CultureInfo.CurrentCulture)!);
            Assert.Null(c.ConvertBack(2, typeof(int), null, CultureInfo.CurrentCulture));
        }
    }
}
