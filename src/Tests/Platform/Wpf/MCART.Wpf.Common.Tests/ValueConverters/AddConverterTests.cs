/*
AddConverterTests.cs

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

using NUnit.Framework;
using System;
using System.Globalization;
using TheXDS.MCART.ValueConverters;

namespace TheXDS.MCART.Wpf.Tests.ValueConverters;

public class AddConverterTests
{
    [Test]
    public void Integer_Add_Test()
    {
        AddConverter? c = new();

        Assert.AreEqual(3, c.Convert(1, typeof(int), 2, CultureInfo.CurrentCulture));
        Assert.AreEqual(1, c.ConvertBack(3, typeof(int), 2, CultureInfo.CurrentCulture));
    }

    [Test]
    public void Mixed_Types_Test()
    {
        AddConverter? c = new();

        Assert.AreEqual(3, c.Convert(1f, typeof(int), "2", CultureInfo.CurrentCulture));
        Assert.AreEqual(1, c.ConvertBack(3f, typeof(int), "2", CultureInfo.CurrentCulture));
    }

    [Test]
    public void Big_Integer_Add_Test()
    {
        AddConverter? c = new();
        Assert.AreEqual(9999999999999999m, c.Convert(9999999999999990m, typeof(decimal), (byte)9, CultureInfo.CurrentCulture));
    }

    [Test]
    public void Cast_Down_Test()
    {
        AddConverter? c = new();
        Assert.AreEqual((byte)200, c.Convert(100L, typeof(byte), 100m, CultureInfo.CurrentCulture));
    }

    [Test]
    public void Sanity_Tests()
    {
        AddConverter? c = new();
        Assert.AreEqual(double.NaN, c.Convert(decimal.MaxValue, typeof(double), "Test", CultureInfo.CurrentCulture));
        Assert.AreEqual(float.NaN, c.Convert(double.MaxValue, typeof(float), "Test", CultureInfo.CurrentCulture));
        Assert.AreEqual(5, c.Convert(5, typeof(int), null, CultureInfo.CurrentCulture));
        Assert.Throws<OverflowException>(() => c.Convert(200L, typeof(byte), 200m, CultureInfo.CurrentCulture));
        Assert.Throws<NotSupportedException>(() => c.Convert(200L, typeof(byte), "Test", CultureInfo.CurrentCulture));
        Assert.Throws<ArgumentNullException>(() => c.Convert(null, typeof(byte), "5", CultureInfo.CurrentCulture));
    }
}
