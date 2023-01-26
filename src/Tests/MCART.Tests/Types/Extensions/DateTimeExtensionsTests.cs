/*
DateTimeExtensionsTests.cs

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

        Assert.AreEqual(1, e.Day);
        Assert.AreEqual(1, e.Month);
        Assert.AreEqual(1970, e.Year);
    }

    [Test]
    public void Epochs_Test()
    {
        Assert.AreEqual(1900, DateTimeExtensions.CenturyEpoch.Year);
        Assert.AreEqual(2000, DateTimeExtensions.Y2KEpoch.Year);
        Assert.AreEqual(1970, DateTimeExtensions.UnixEpoch.Year);
    }

    [Test]
    public void ToUnixTimestamp_Test()
    {
        DateTime t = new(2038, 1, 19, 3, 14, 7);
        Assert.AreEqual(int.MaxValue, t.ToUnixTimestamp());
    }

    [Test]
    public void ToUnixTimestampMs_Test()
    {
        DateTime t = new(2012, 5, 19, 19, 35, 0);
        Assert.AreEqual(1337456100000, t.ToUnixTimestampMs());
    }

    [Test]
    public void FromUnixTimestamp_Test()
    {
        DateTime t = new(2038, 1, 19, 3, 14, 7);
        Assert.AreEqual(t, DateTimeExtensions.FromUnixTimestamp(int.MaxValue));
    }

    [Test]
    public void FromUnixTimestampMs_Test()
    {
        DateTime t = new(2012, 5, 19, 19, 35, 0);
        Assert.AreEqual(t, 1337456100000.FromUnixTimestampMs());
    }

    [Test]
    public void MonthName_Test()
    {
        Assert.AreEqual("August", DateTimeExtensions.MonthName(8, CultureInfo.CreateSpecificCulture("en-US")));
        Assert.AreEqual("agosto", DateTimeExtensions.MonthName(8, CultureInfo.CreateSpecificCulture("es-MX")));
        Assert.Throws<ArgumentOutOfRangeException>(() => DateTimeExtensions.MonthName(0, CultureInfo.CurrentCulture));
        Assert.Throws<ArgumentOutOfRangeException>(() => DateTimeExtensions.MonthName(13, CultureInfo.CurrentCulture));

        DateTime t = DateTime.Today;
        Assert.AreEqual(t.ToString("MMMM"), DateTimeExtensions.MonthName(t.Month));
    }
}
