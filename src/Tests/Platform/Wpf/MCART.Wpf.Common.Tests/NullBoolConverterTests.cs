/*
NullBoolConverterTests.cs

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

public class NullBoolConverterTests
{
    [Test]
    public void Ctor_with_true_arg_sets_props()
    {
        var converter = new NullBoolConverter<int>(1);
        Assert.That(converter.True, Is.EqualTo(1));
        Assert.That(converter.False, Is.EqualTo(default(int)));
        Assert.That(converter.Null, Is.EqualTo(default(int)));
    }

    [Test]
    public void Ctor_with_true_false_args_sets_props()
    {
        var converter = new NullBoolConverter<int>(1, 2);
        Assert.That(converter.True, Is.EqualTo(1));
        Assert.That(converter.False, Is.EqualTo(2));
        Assert.That(converter.Null, Is.EqualTo(2));
    }

    [Test]
    public void Ctor_with_true_false_null_args_sets_props()
    {
        var converter = new NullBoolConverter<int>(1, 2, 3);
        Assert.That(converter.True, Is.EqualTo(1));
        Assert.That(converter.False, Is.EqualTo(2));
        Assert.That(converter.Null, Is.EqualTo(3));
    }

    [Test]
    public void Convert_with_true_value_returns_true()
    {
        var converter = new NullBoolConverter<int>(1, 2, 3);
        Assert.That(converter.Convert(true, typeof(int), null, CultureInfo.CurrentCulture), Is.EqualTo(1));
    }

    [Test]
    public void Convert_with_false_value_returns_false()
    {
        var converter = new NullBoolConverter<int>(1, 2, 3);
        Assert.That(converter.Convert(false, typeof(int), null, CultureInfo.CurrentCulture), Is.EqualTo(2));
    }

    [Test]
    public void Convert_with_true_explicitly_as_nullable_bool_value_returns_true()
    {
        var converter = new NullBoolConverter<int>(1, 2, 3);
        Assert.That(converter.Convert((bool?)true, typeof(int), null, CultureInfo.CurrentCulture), Is.EqualTo(1));
    }

    [Test]
    public void Convert_with_false_explicitly_as_nullable_bool_value_returns_false()
    {
        var converter = new NullBoolConverter<int>(1, 2, 3);
        Assert.That(converter.Convert((bool?)false, typeof(int), null, CultureInfo.CurrentCulture), Is.EqualTo(2));
    }

    [Test]
    public void Convert_with_null_value_returns_null()
    {
        var converter = new NullBoolConverter<int>(1, 2, 3);
        Assert.That(converter.Convert(null, typeof(int), null, CultureInfo.CurrentCulture), Is.EqualTo(3));
    }

    [Test]
    public void Convert_with_other_value_returns_default()
    {
        var converter = new NullBoolConverter<int>(1, 2, 3);
        Assert.That(converter.Convert(42, typeof(int), null, CultureInfo.CurrentCulture), Is.EqualTo(default(int)));
    }

    [Test]
    public void ConvertBack_with_true_value_returns_true()
    {
        var converter = new NullBoolConverter<int>(1, 2, 3);
        Assert.That(converter.ConvertBack(1, typeof(int), null, CultureInfo.CurrentCulture), Is.EqualTo(true));
    }

    [Test]
    public void ConvertBack_with_false_value_returns_false()
    {
        var converter = new NullBoolConverter<int>(1, 2, 3);
        Assert.That(converter.ConvertBack(2, typeof(int), null, CultureInfo.CurrentCulture), Is.EqualTo(false));
    }

    [Test]
    public void ConvertBack_with_null_value_returns_null()
    {
        var converter = new NullBoolConverter<int>(1, 2, 3);
        Assert.That(converter.ConvertBack(3, typeof(int), null, CultureInfo.CurrentCulture), Is.Null);
    }

    [Test]
    public void ConvertBack_with_other_value_returns_default()
    {
        var converter = new NullBoolConverter<int>(1, 2, 3);
        Assert.That(converter.ConvertBack("TEST", typeof(int), null, CultureInfo.CurrentCulture), Is.EqualTo(default(int)));
    }
}