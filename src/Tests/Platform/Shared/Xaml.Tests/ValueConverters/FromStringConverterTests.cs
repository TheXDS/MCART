/*
ErrorManagerTests.cs

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

public class FromStringConverterTests
{
    [Test]
    public void Convert_returns_parsed_value()
    {
        var conv = new FromStringConverter();
        var result = conv.Convert("42", typeof(int), null, CultureInfo.InvariantCulture);
        Assert.That(result, Is.AssignableTo<int>());
        Assert.That(result, Is.EqualTo(42));
    }

    [Test]
    public void Convert_returns_null_on_parse_error()
    {
        var conv = new FromStringConverter();
        Assert.That(conv.Convert("ABC", typeof(int), null, CultureInfo.InvariantCulture), Is.Null);
    }

    [Test]
    public void Convert_returns_null_on_unparsable_type()
    {
        var conv = new FromStringConverter();
        Assert.That(conv.Convert("ABC", typeof(Random), null, CultureInfo.InvariantCulture), Is.Null);
    }

    [Test]
    public void Convert_returns_null_on_null_type()
    {
        var conv = new FromStringConverter();
        Assert.That(conv.Convert("ABC", null!, null, CultureInfo.InvariantCulture), Is.Null);
    }

    [Test]
    public void ConvertBack_returns_ToString_result()
    {
        var conv = new FromStringConverter();
        Assert.That(conv.ConvertBack(42, typeof(int), null, CultureInfo.CurrentCulture), Is.EqualTo("42"));
    }

    [Test]
    public void ConvertBack_returns_null_on_null_value()
    {
        var conv = new FromStringConverter();
        Assert.That(conv.ConvertBack(null, typeof(int), null, CultureInfo.CurrentCulture), Is.Null);
    }
}
