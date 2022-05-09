/*
AnyVisibleConverterTests.cs

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

namespace TheXDS.MCART.Wpf.Tests.ValueConverters;
using System;
using System.Globalization;
using System.Windows;
using NUnit.Framework;
using TheXDS.MCART.ValueConverters;

public class AnyVisibleConverterTests
{
    [Test]
    public void Convert_Test()
    {
        AnyVisibleConverter c = new();
        Assert.AreEqual(Visibility.Visible, c.Convert(new object[] { Visibility.Visible, Visibility.Collapsed }, typeof(Visibility), null!, CultureInfo.CurrentCulture));
        Assert.AreEqual(Visibility.Collapsed, c.Convert(new object[] { Visibility.Collapsed, Visibility.Collapsed }, typeof(Visibility), null!, CultureInfo.CurrentCulture));
    }

    [Test]
    public void ConvertBack_Test()
    {
        AnyVisibleConverter c = new();
        Assert.Throws<InvalidOperationException>(() =>
            c.ConvertBack(Visibility.Collapsed, new[] { typeof(Visibility), typeof(Visibility) }, null!,
                CultureInfo.CurrentCulture)
        );
    }
}
