/*
ValueConvertersTests.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright (c) 2011 - 2019 César Andrés Morgan

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
using System.Linq;
using System.Windows;
using Xunit;
using System.Windows.Converters;

namespace TheXDS.MCART.WpfTests.Modules
{
    public class ValueConvertersTests
    {
        [Fact]
        public void ThresholdConverterTest()
        {
            var c = new ThresholdConverter<double,Visibility>(Visibility.Collapsed,Visibility.Visible,Visibility.Hidden);

            Assert.Throws<ArgumentNullException>(() => c.Convert(null, typeof(Visibility), 100.0, CultureInfo.CurrentCulture));
            Assert.Throws<ArgumentException>(() => c.Convert("X", typeof(Visibility), 100.0, CultureInfo.CurrentCulture));

            Assert.Equal(Visibility.Visible, c.Convert(105.0,typeof(Visibility), 100.0,CultureInfo.CurrentCulture));
            Assert.Equal(Visibility.Visible, c.Convert(105.0,typeof(Visibility), "100.0",CultureInfo.CurrentCulture));
            Assert.Equal(Visibility.Collapsed, c.Convert(95.0, typeof(Visibility), 100.0, CultureInfo.CurrentCulture));
            Assert.Equal(Visibility.Hidden, c.Convert(100.0, typeof(Visibility), 100.0, CultureInfo.CurrentCulture));

            Assert.Throws<ArgumentNullException>(() => c.Convert(150.0, typeof(Visibility), null, CultureInfo.CurrentCulture));
            Assert.Throws<ArgumentException>(() => c.Convert(150.0, typeof(Visibility), "X", CultureInfo.CurrentCulture));
        }
    }
}
