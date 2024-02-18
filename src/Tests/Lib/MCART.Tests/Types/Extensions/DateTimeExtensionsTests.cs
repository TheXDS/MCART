﻿/*
DateTimeExtensionsTests.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2024 César Andrés Morgan

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

namespace TheXDS.MCART.Tests.Types.Extensions;
using NUnit.Framework;
using System;
using System.Globalization;
using TheXDS.MCART.Types.Extensions;

public class DateTimeExtensionsTests
{
    [Test]
    public void EpochTest()
    {
        DateTime e = DateTimeExtensions.Epoch(1970);

        Assert.That(1, Is.EqualTo(e.Day));
        Assert.That(1, Is.EqualTo(e.Month));
        Assert.That(1970, Is.EqualTo(e.Year));
    }

    [Test]
    public void Epochs_Test()
    {
        Assert.That(1900, Is.EqualTo(DateTimeExtensions.CenturyEpoch.Year));
        Assert.That(2000, Is.EqualTo(DateTimeExtensions.Y2KEpoch.Year));
        Assert.That(1970, Is.EqualTo(DateTimeExtensions.UnixEpoch.Year));
    }

    [Test]
    public void ToUnixTimestamp_Test()
    {
        DateTime t = new(2038, 1, 19, 3, 14, 7);
        Assert.That(int.MaxValue, Is.EqualTo(t.ToUnixTimestamp()));
    }

    [Test]
    public void ToUnixTimestampMs_Test()
    {
        DateTime t = new(2012, 5, 19, 19, 35, 0);
        Assert.That(1337456100000, Is.EqualTo(t.ToUnixTimestampMs()));
    }

    [Test]
    public void FromUnixTimestamp_Test()
    {
        DateTime t = new(2038, 1, 19, 3, 14, 7);
        Assert.That(DateTimeExtensions.FromUnixTimestamp(int.MaxValue), Is.EqualTo(t));
    }

    [Test]
    public void FromUnixTimestampMs_Test()
    {
        DateTime t = new(2012, 5, 19, 19, 35, 0);
        Assert.That(t, Is.EqualTo(1337456100000.FromUnixTimestampMs()));
    }

    [Test]
    public void MonthName_Test()
    {
        Assert.That(DateTimeExtensions.MonthName(8, CultureInfo.CreateSpecificCulture("en-US")),Is.EqualTo("August"));
        Assert.That(DateTimeExtensions.MonthName(8, CultureInfo.CreateSpecificCulture("es-MX")), Is.EqualTo("agosto"));
        Assert.Throws<ArgumentOutOfRangeException>(() => DateTimeExtensions.MonthName(0, CultureInfo.CurrentCulture));
        Assert.Throws<ArgumentOutOfRangeException>(() => DateTimeExtensions.MonthName(13, CultureInfo.CurrentCulture));

        DateTime t = DateTime.Today;
        Assert.That(t.ToString("MMMM"), Is.EqualTo(DateTimeExtensions.MonthName(t.Month)));
    }
}
