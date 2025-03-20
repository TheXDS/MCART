/*
AnyVisibleConverterTests.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2025 César Andrés Morgan

Permission is hereby granted, free of charge, to any person obtaining a copy of
this software and associated documentation files (the "Software"), to deal in
the Software without restriction, including without limitation the rights to
use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
of the Software, and to permit persons to whom the Software is furnished to do
so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using System.Globalization;
using System.Windows;
using TheXDS.MCART.ValueConverters;

namespace TheXDS.MCART.Wpf.Common.Tests.ValueConverters;

public class AnyVisibleConverterTests
{
    [Test]
    public void Convert_Test()
    {
        AnyVisibleConverter c = new();
        Assert.That(Visibility.Visible, Is.EqualTo(c.Convert(new object[] { Visibility.Visible, Visibility.Collapsed }, typeof(Visibility), null!, CultureInfo.CurrentCulture)));
        Assert.That(Visibility.Collapsed, Is.EqualTo(c.Convert(new object[] { Visibility.Collapsed, Visibility.Collapsed }, typeof(Visibility), null!, CultureInfo.CurrentCulture)));
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
