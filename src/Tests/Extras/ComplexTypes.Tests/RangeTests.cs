/*
RangeTests.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2026 César Andrés Morgan

Permission is hereby granted, free of charge, to any person obtaining a copy of
this software and associated documentation files (the “Software”), to deal in
the Software without restriction, including without limitation the rights to
use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
of the Software, and to permit persons to whom the Software is furnished to do
so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE. 
*/

using TheXDS.MCART.Types.Entity;

namespace TheXDS.MCART.Ext.ComplexTypes.Tests;

public class RangeTests
{
    [Theory]
    [TestCase(1, 10, true, true)]
    [TestCase(1, 10, true, false)]
    [TestCase(1, 10, false, true)]
    [TestCase(1, 10, false, false)]
    public void Ctor_full_test(int min, int max, bool minInc, bool maxInc)
    {
        var r = new Range<int>(min, max, minInc, maxInc);

        Assert.That(min, Is.EqualTo(r.Minimum));
        Assert.That(max, Is.EqualTo(r.Maximum));
        Assert.That(minInc, Is.EqualTo(r.MinInclusive));
        Assert.That(maxInc, Is.EqualTo(r.MaxInclusive));
    }

    [Test]
    public void Ctor_overloads_test()
    {
        Range<int> r;

        r = new();
        Assert.That(default(int), Is.EqualTo(r.Minimum));
        Assert.That(default(int), Is.EqualTo(r.Maximum));
        Assert.That(r.MinInclusive);
        Assert.That(r.MaxInclusive);

        r = new(10);
        Assert.That(default(int), Is.EqualTo(r.Minimum));
        Assert.That(10, Is.EqualTo(r.Maximum));
        Assert.That(r.MinInclusive);
        Assert.That(r.MaxInclusive);

        r = new(5, 15);
        Assert.That(5, Is.EqualTo(r.Minimum));
        Assert.That(15, Is.EqualTo(r.Maximum));
        Assert.That(r.MinInclusive);
        Assert.That(r.MaxInclusive);

        r = new(8, false);
        Assert.That(default(int), Is.EqualTo(r.Minimum));
        Assert.That(8, Is.EqualTo(r.Maximum));
        Assert.That(r.MinInclusive, Is.False);
        Assert.That(r.MaxInclusive, Is.False);

        r = new(2, 6 , false);
        Assert.That(2, Is.EqualTo(r.Minimum));
        Assert.That(6, Is.EqualTo(r.Maximum));
        Assert.That(r.MinInclusive, Is.False);
        Assert.That(r.MaxInclusive, Is.False);
    }

    [Test]
    public void Ctor_contract_test()
    {
        Assert.Throws<ArgumentException>(() => _ = new Range<int>(10, 5));
    }

    [Theory]
    [TestCase(1,10, 5, 15, true)]
    [TestCase(1,10, 15, 25, false)]
    [TestCase(1, 10, -5, 5, true)]
    public void Intersects_test(int min1, int max1, int min2, int max2, bool result)
    {
        var r1 = new Range<int>(min1, max1);
        var r2 = new Range<int>(min2, max2);
        Assert.That(result, Is.EqualTo(r1.Intersects(r2)));
    }

    [Theory]
    [TestCase(1, 10, 5, true)]
    [TestCase(1, 10, 15, false)]
    public void IsWithin_test(int min, int max, int value, bool result)
    {
        var r = new Range<int>(min, max);
        Assert.That(result, Is.EqualTo(r.IsWithin(value)));
    }

    [Test]
    public void Implicit_conversion_test()
    {
        Range<int> r1 = new(5, 10, false, true);
        Types.Range<int> r2 = new(5, 10, false, true);

        Assert.That(r1.Minimum, Is.EqualTo(((Range<int>)r2).Minimum));
        Assert.That(r1.Maximum, Is.EqualTo(((Range<int>)r2).Maximum));
        Assert.That(r1.MinInclusive, Is.EqualTo(((Range<int>)r2).MinInclusive));
        Assert.That(r1.MaxInclusive, Is.EqualTo(((Range<int>)r2).MaxInclusive));
        Assert.That(r2.Minimum, Is.EqualTo(((Types.Range<int>)r1).Minimum));
        Assert.That(r2.Maximum, Is.EqualTo(((Types.Range<int>)r1).Maximum));
        Assert.That(r2.MinInclusive, Is.EqualTo(((Types.Range<int>)r1).MinInclusive));
        Assert.That(r2.MaxInclusive, Is.EqualTo(((Types.Range<int>)r1).MaxInclusive));
    }
}
