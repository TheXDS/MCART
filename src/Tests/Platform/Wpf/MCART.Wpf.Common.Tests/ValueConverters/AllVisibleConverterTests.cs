﻿/*
AllVisibleConverterTests.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2023 César Andrés Morgan

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

using System;
using System.Globalization;
using System.Windows;
using NUnit.Framework;
using TheXDS.MCART.ValueConverters;

namespace TheXDS.MCART.Wpf.Tests.ValueConverters;

public class AllVisibleConverterTests
{
    [Test]
    public void Convert_Test()
    {
        AllVisibleConverter c = new();
        Assert.AreEqual(Visibility.Visible, c.Convert(new object[] { Visibility.Visible, Visibility.Visible }, typeof(Visibility), null!, CultureInfo.CurrentCulture));
        Assert.AreEqual(Visibility.Collapsed, c.Convert(new object[] { Visibility.Collapsed, Visibility.Visible }, typeof(Visibility), null!, CultureInfo.CurrentCulture));
    }

    [Test]
    public void ConvertBack_Test()
    {
        AllVisibleConverter c = new();
        Assert.Throws<InvalidOperationException>(() =>
            c.ConvertBack(Visibility.Collapsed, new[] { typeof(Visibility), typeof(Visibility) }, null!,
                CultureInfo.CurrentCulture)
        );
    }
}