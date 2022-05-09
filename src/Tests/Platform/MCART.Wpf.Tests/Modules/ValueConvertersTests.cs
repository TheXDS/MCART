/*
ValueConvertersTests.cs

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

using NUnit.Framework;
using System;
using System.Globalization;
using System.Windows;
using TheXDS.MCART.ValueConverters;

namespace TheXDS.MCART.Wpf.Tests.Modules;

public class ValueConvertersTests
{
    [Test]
    public void ThresholdConverterTest()
    {
        ThresholdConverter<double, Visibility>? c = new(Visibility.Collapsed, Visibility.Visible, Visibility.Hidden);
        Assert.Throws<ArgumentNullException>(() => c.Convert(null, typeof(Visibility), 100.0, CultureInfo.CurrentCulture));
        Assert.Throws<ArgumentException>(() => c.Convert("X", typeof(Visibility), 100.0, CultureInfo.CurrentCulture));
        Assert.AreEqual(Visibility.Visible, c.Convert(105.0, typeof(Visibility), 100.0, CultureInfo.CurrentCulture));
        Assert.AreEqual(Visibility.Visible, c.Convert(105.0, typeof(Visibility), "100.0", CultureInfo.CurrentCulture));
        Assert.AreEqual(Visibility.Collapsed, c.Convert(95.0, typeof(Visibility), 100.0, CultureInfo.CurrentCulture));
        Assert.AreEqual(Visibility.Hidden, c.Convert(100.0, typeof(Visibility), 100.0, CultureInfo.CurrentCulture));
        Assert.Throws<ArgumentNullException>(() => c.Convert(150.0, typeof(Visibility), null, CultureInfo.CurrentCulture));
        Assert.Throws<ArgumentException>(() => c.Convert(150.0, typeof(Visibility), "X", CultureInfo.CurrentCulture));
    }
}
