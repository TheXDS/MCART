/*
SecureStringToStringConverterTests.cs

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
using System.Security;
using TheXDS.MCART.Types.Extensions;
using TheXDS.MCART.ValueConverters;

namespace TheXDS.MCART.Wpf.Common.Tests.ValueConverters;

public class SecureStringToStringConverterTests
{
    private static readonly SecureStringToStringConverter c = new();

    [Test]
    public void Convert_converts_SecureString_to_string()
    {
        string s = c.Convert("Test".ToSecureString(), null, CultureInfo.CurrentCulture)!;
        Assert.That(s, Is.Not.Null);
        Assert.That(s, Is.EqualTo("Test"));
    }

    [Test]
    public void ConvertBack_converts_string_to_SecureString()
    {
        SecureString s = c.ConvertBack("Test", null, CultureInfo.CurrentCulture)!;
        Assert.That(s, Is.Not.Null);
        Assert.That(s.Read(), Is.EqualTo("Test"));
    }

    [Test]
    public void Convert_converts_null_to_null()
    {
        Assert.That(c.Convert(null, null, CultureInfo.CurrentCulture), Is.Null);
    }

    [Test]
    public void Convert_converts_empty_SecureString_to_empty_string()
    {
        string s = c.Convert(new SecureString(), null, CultureInfo.CurrentCulture)!;
        Assert.That(s, Is.Not.Null);
        Assert.That(s, Is.EqualTo(string.Empty));
    }

    [Test]
    public void ConvertBack_converts_null_to_null()
    {
        Assert.That(c.ConvertBack(null, null, CultureInfo.CurrentCulture), Is.Null);
    }

    [Test]
    public void ConvertBack_converts_empty_string_to_empty_SecureString()
    {
        SecureString s = c.ConvertBack(string.Empty, null, CultureInfo.CurrentCulture)!;
        Assert.That(s, Is.Not.Null);
        Assert.That(s.Read(), Is.EqualTo(string.Empty));
    }
}
