/*
SubtractConverterTests.cs

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

namespace TheXDS.MCART.Common.Tests.ValueConverters.Base;

public abstract partial class PrimitiveMathOpConverterBaseTests<T>(int a, int b, int c, decimal bigA, byte bigB, decimal bigC, decimal downA, long downB, byte downC, double opNullC)
{
    private readonly int _a = a;
    private readonly int _b = b;
    private readonly int _c = c;
    private readonly decimal _bigA = bigA;
    private readonly byte _bigB = bigB;
    private readonly decimal _bigC = bigC;
    private readonly decimal _downA = downA;
    private readonly long _downB = downB;
    private readonly byte _downC = downC;
    private readonly double _opNullC = opNullC;

    [Test]
    public void Integer_operation_Test()
    {
        T c = new();
        Assert.That(c.Convert(_a, typeof(int), _b, CultureInfo.CurrentCulture), Is.EqualTo(_c));
        Assert.That(c.ConvertBack(_c, typeof(int), _b, CultureInfo.CurrentCulture), Is.EqualTo(_a));
    }

    [Test]
    public void Mixed_Types_Test()
    {
        T c = new();
        Assert.That(c.Convert((float)_a, typeof(int), _b.ToString(), CultureInfo.CurrentCulture), Is.EqualTo(_c));
        Assert.That(c.ConvertBack((float)_c, typeof(int), _b.ToString(), CultureInfo.CurrentCulture), Is.EqualTo(_a));
    }

    [Test]
    public void Big_Integer_operation_Test()
    {
        T? c = new();
        Assert.That(c.Convert(_bigA, typeof(decimal), _bigB, CultureInfo.CurrentCulture), Is.EqualTo(_bigC));
    }

    [Test]
    public void Cast_Down_Test()
    {
        T? c = new();
        Assert.That(c.Convert(_downA, typeof(byte), _downB, CultureInfo.CurrentCulture), Is.EqualTo(_downC));
    }

    [Test]
    public void Sanity_Tests()
    {
        T c = new();
        Assert.That(double.NaN, Is.EqualTo(c.Convert(decimal.MaxValue, typeof(double), "Test", CultureInfo.CurrentCulture)));
        Assert.That(float.NaN, Is.EqualTo(c.Convert(double.MaxValue, typeof(float), "Test", CultureInfo.CurrentCulture)));
        Assert.That(_opNullC, Is.EqualTo(c.Convert(5, typeof(double), null, CultureInfo.CurrentCulture)));
        Assert.Throws<OverflowException>(() => c.Convert(600L, typeof(byte), -300m, CultureInfo.CurrentCulture));
        Assert.Throws<NotSupportedException>(() => c.Convert(200L, typeof(byte), "Test", CultureInfo.CurrentCulture));
        Assert.Throws<ArgumentNullException>(() => c.Convert(null, typeof(byte), "5", CultureInfo.CurrentCulture));
    }
}
