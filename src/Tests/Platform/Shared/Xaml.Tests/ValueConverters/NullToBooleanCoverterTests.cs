﻿/*
ErrorManagerTests.cs

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

using System.Globalization;
using TheXDS.MCART.ValueConverters;

namespace TheXDS.MCART.Wpf.Common.Tests.ValueConverters;

public class NullToBooleanCoverterTests
{
    [Test]
    public void Convert_returns_true_if_null()
    {
        var conv = new NullToBooleanConverter();
        Assert.That(conv.Convert(null, typeof(bool), null, CultureInfo.InvariantCulture), Is.True);
    }

    [Test]
    public void Convert_returns_false_if_not_null()
    {
        var conv = new NullToBooleanConverter();
        Assert.That(conv.Convert(new object(), typeof(bool), null, CultureInfo.InvariantCulture), Is.False);
    }

    [Test]
    public void ConvertBack_returns_instance_on_false()
    {
        var conv = new NullToBooleanConverter();
        Assert.That(conv.ConvertBack(false, typeof(Random), null, CultureInfo.InvariantCulture), Is.InstanceOf<Random>());
    }

    [Test]
    public void ConvertBack_returns_null_on_true()
    {
        var conv = new NullToBooleanConverter();
        Assert.That(conv.ConvertBack(true, typeof(Random), null, CultureInfo.InvariantCulture), Is.Null);
    }

    [Test]
    public void ConvertBack_throws_InvalidCastException_on_invalid_value()
    {
        var conv = new NullToBooleanConverter();
        Assert.That(() => conv.ConvertBack("Test", typeof(Random), null, CultureInfo.InvariantCulture), Throws.InstanceOf<InvalidCastException>());
    }
}