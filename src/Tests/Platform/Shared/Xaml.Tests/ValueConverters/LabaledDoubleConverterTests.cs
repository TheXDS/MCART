/*
ErrorManagerTests.cs

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
using TheXDS.MCART.ValueConverters;

namespace TheXDS.MCART.Wpf.Common.Tests.ValueConverters;

public class LabaledDoubleConverterTests
{
    [Test]
    public void Convert_returns_value()
    {
        var conv = new LabeledDoubleConverter();
        Assert.That(conv.Convert(4.0, typeof(string), null, CultureInfo.InvariantCulture), Is.EqualTo("4"));
    }

    [Test]
    public void Convert_returns_label_on_value_lt_zero()
    {
        var conv = new LabeledDoubleConverter();
        Assert.That(conv.Convert(-4.0, typeof(string), "Below zero", CultureInfo.InvariantCulture), Is.EqualTo("Below zero"));
    }

    [Test]
    public void Convert_returns_value_on_value_lt_zero_and_no_label()
    {
        var conv = new LabeledDoubleConverter();
        Assert.That(conv.Convert(-4.0, typeof(string), null, CultureInfo.InvariantCulture), Is.EqualTo("-4"));
    }

    [Test]
    public void Convert_throws_InvalidCastException_on_invalid_value()
    {
        var conv = new LabeledDoubleConverter();
        Assert.That(() => conv.Convert("Test", typeof(string), null, CultureInfo.InvariantCulture), Throws.InstanceOf<InvalidCastException>());
    }

    [Test]
    public void ConvertBack_returns_double_value()
    {
        var conv = new LabeledDoubleConverter();
        Assert.That(conv.ConvertBack(4.0, typeof(string), null, CultureInfo.InvariantCulture), Is.EqualTo(4.0));
    }

    [Test]
    public void ConvertBack_returns_value_on_string()
    {
        var conv = new LabeledDoubleConverter();
        Assert.That(conv.ConvertBack("4.0", typeof(string), null, CultureInfo.InvariantCulture), Is.EqualTo(4.0));
    }

    [Test]
    public void ConvertBack_returns_zero_on_string_label()
    {
        var conv = new LabeledDoubleConverter();
        Assert.That(conv.ConvertBack("Test", typeof(string), null, CultureInfo.InvariantCulture), Is.EqualTo(0.0));
    }

    [Test]
    public void ConvertBack_throws_InvalidCastException_on_invalid_value()
    {
        var conv = new LabeledDoubleConverter();
        Assert.That(() => conv.ConvertBack(new Random(), typeof(string), null, CultureInfo.InvariantCulture), Throws.InstanceOf<InvalidCastException>());
    }
}
