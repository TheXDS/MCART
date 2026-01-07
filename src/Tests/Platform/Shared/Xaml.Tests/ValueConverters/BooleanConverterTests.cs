/*
BooleanConverterTests.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2026 César Andrés Morgan

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

public class BooleanConverterTests
{
    [Test]
    public void Instance_Test()
    {
        BooleanConverter<int>? c = new(5, -5);

        Assert.That(5, Is.EqualTo(c.True));
        Assert.That(-5, Is.EqualTo(c.False));
    }

    [Test]
    public void Conversion_Test()
    {
        BooleanConverter<int>? c = new(5, -5);

        Assert.That(5, Is.EqualTo(c.Convert(true, typeof(int), null, CultureInfo.CurrentCulture)));
        Assert.That(-5, Is.EqualTo(c.Convert(false, typeof(int), null, CultureInfo.CurrentCulture)));
    }

    [Test]
    public void Failure_Test()
    {
        BooleanConverter<int>? c = new(5, -5);
        Assert.That(c.Convert(3, typeof(int), null, CultureInfo.CurrentCulture), Is.Null);
        Assert.That(c.Convert("Test", typeof(int), null, CultureInfo.CurrentCulture), Is.Null);
    }

    [Test]
    public void ConvertBack_Test()
    {
        BooleanConverter<int>? c = new(5, -5);

        Assert.That((bool)c.ConvertBack(5, typeof(int), null, CultureInfo.CurrentCulture)!, Is.True);
        Assert.That((bool)c.ConvertBack(-5, typeof(int), null, CultureInfo.CurrentCulture)!, Is.False);
        Assert.That(c.ConvertBack(2, typeof(int), null, CultureInfo.CurrentCulture), Is.Null);
    }

    [Test]
    public void ConvertBack_returns_null_if_null()
    {
        BooleanConverter<int>? c = new(5, -5);
        Assert.That(c.ConvertBack(null, typeof(int), null, CultureInfo.CurrentCulture), Is.Null);
    }
}
