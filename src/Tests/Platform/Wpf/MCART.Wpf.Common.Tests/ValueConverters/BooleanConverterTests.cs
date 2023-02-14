﻿/*
BooleanConverterTests.cs

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

using System.Globalization;
using TheXDS.MCART.ValueConverters;
using NUnit.Framework;

namespace TheXDS.MCART.Wpf.Tests.ValueConverters;

public class BooleanConverterTests
{
    [Test]
    public void Instance_Test()
    {
        BooleanConverter<int>? c = new(5, -5);

        Assert.AreEqual(5, c.True);
        Assert.AreEqual(-5, c.False);
    }

    [Test]
    public void Conversion_Test()
    {
        BooleanConverter<int>? c = new(5, -5);

        Assert.AreEqual(5, c.Convert(true, typeof(int), null, CultureInfo.CurrentCulture));
        Assert.AreEqual(-5, c.Convert(false, typeof(int), null, CultureInfo.CurrentCulture));
    }

    [Test]
    public void Failure_Test()
    {
        BooleanConverter<int>? c = new(5, -5);
        Assert.Null(c.Convert(3, typeof(int), null, CultureInfo.CurrentCulture));
        Assert.Null(c.Convert("Test", typeof(int), null, CultureInfo.CurrentCulture));
    }

    [Test]
    public void ConvertBack_Test()
    {
        BooleanConverter<int>? c = new(5, -5);

        Assert.True((bool)c.ConvertBack(5, typeof(int), null, CultureInfo.CurrentCulture)!);
        Assert.False((bool)c.ConvertBack(-5, typeof(int), null, CultureInfo.CurrentCulture)!);
        Assert.Null(c.ConvertBack(2, typeof(int), null, CultureInfo.CurrentCulture));
    }
}