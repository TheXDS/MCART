/*
TimeSpanExtensionsTests.cs

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
using TheXDS.MCART.Resources.Strings;
using TheXDS.MCART.Types.Extensions;

public class TimeSpanExtensionsTests
{
    [Test]
    public void VerboseTest()
    {
        Assert.That(Composition.Seconds(15), Is.EqualTo(TimeSpan.FromSeconds(15).Verbose()));
        Assert.That(Composition.Minutes(3), Is.EqualTo(TimeSpan.FromSeconds(180).Verbose()));
        Assert.That(Composition.Hours(2), Is.EqualTo(TimeSpan.FromSeconds(7200).Verbose()));
        Assert.That(Composition.Days(5), Is.EqualTo(TimeSpan.FromDays(5).Verbose()));

        Assert.That(
            $"{Composition.Minutes(1)}, {Composition.Seconds(5)}",
            Is.EqualTo(TimeSpan.FromSeconds(65).Verbose()));

        Assert.That(
            $"{Composition.Days(2)}, {Composition.Hours(5)}, {Composition.Minutes(45)}, {Composition.Seconds(23)}",
            Is.EqualTo((TimeSpan.FromDays(2) + TimeSpan.FromHours(5) + TimeSpan.FromMinutes(45) + TimeSpan.FromSeconds(23)).Verbose()));
    }

    [Test]
    public void AsTimeTest()
    {
        TimeSpan t = TimeSpan.FromSeconds(60015);
        CultureInfo? c = CultureInfo.InvariantCulture;
        string? r = t.AsTime(c);
        Assert.That("16:40", Is.EqualTo(r));
        Assert.That(string.Format($"{{0:{CultureInfo.CurrentCulture.DateTimeFormat.ShortTimePattern}}}", DateTime.MinValue.Add(t)), Is.EqualTo(t.AsTime()));
    }
}
