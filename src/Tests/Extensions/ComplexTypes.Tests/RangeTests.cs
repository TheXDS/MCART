/*
RangeTests.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2023 César Andrés Morgan

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

using NUnit.Framework;
using System;
using TheXDS.MCART.Types.Entity;

namespace TheXDS.MCART.Ext.ComplexTypes.Tests;

public class RangeTests
{
    [Theory]
    [TestCase(1, 10, true, true)]
    [TestCase(1, 10, true, false)]
    [TestCase(1, 10, false, true)]
    [TestCase(1, 10, false, false)]
    [CLSCompliant(false)]
    public void Ctor_full_test(int min, int max, bool minInc, bool maxInc)
    {
        var r = new Range<int>(min, max, minInc, maxInc);

        Assert.AreEqual(min, r.Minimum);
        Assert.AreEqual(max, r.Maximum);
        Assert.AreEqual(minInc, r.MinInclusive);
        Assert.AreEqual(maxInc, r.MaxInclusive);
    }

    [Test]
    public void Ctor_overloads_test()
    {
        Range<int> r;

        r = new();
        Assert.AreEqual(default(int), r.Minimum);
        Assert.AreEqual(default(int), r.Maximum);
        Assert.IsTrue(r.MinInclusive);
        Assert.IsTrue(r.MaxInclusive);

        r = new(10);
        Assert.AreEqual(default(int), r.Minimum);
        Assert.AreEqual(10, r.Maximum);
        Assert.IsTrue(r.MinInclusive);
        Assert.IsTrue(r.MaxInclusive);

        r = new(5, 15);
        Assert.AreEqual(5, r.Minimum);
        Assert.AreEqual(15, r.Maximum);
        Assert.IsTrue(r.MinInclusive);
        Assert.IsTrue(r.MaxInclusive);

        r = new(8, false);
        Assert.AreEqual(default(int), r.Minimum);
        Assert.AreEqual(8, r.Maximum);
        Assert.IsFalse(r.MinInclusive);
        Assert.IsFalse(r.MaxInclusive);

        r = new(2, 6 , false);
        Assert.AreEqual(2, r.Minimum);
        Assert.AreEqual(6, r.Maximum);
        Assert.IsFalse(r.MinInclusive);
        Assert.IsFalse(r.MaxInclusive);
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
    [CLSCompliant(false)]
    public void Intersects_test(int min1, int max1, int min2, int max2, bool result)
    {
        var r1 = new Range<int>(min1, max1);
        var r2 = new Range<int>(min2, max2);
        Assert.AreEqual(result, r1.Intersects(r2));
    }

    [Theory]
    [TestCase(1, 10, 5, true)]
    [TestCase(1, 10, 15, false)]
    [CLSCompliant(false)]
    public void IsWithin_test(int min, int max, int value, bool result)
    {
        var r = new Range<int>(min, max);
        Assert.AreEqual(result, r.IsWithin(value));
    }

    [Test]
    public void Implicit_conversion_test()
    {
        Range<int> r1 = new(5, 10, false, true);
        Types.Range<int> r2 = new(5, 10, false, true);

        Assert.AreEqual(r1.Minimum, ((Range<int>)r2).Minimum);
        Assert.AreEqual(r1.Maximum, ((Range<int>)r2).Maximum);
        Assert.AreEqual(r1.MinInclusive, ((Range<int>)r2).MinInclusive);
        Assert.AreEqual(r1.MaxInclusive, ((Range<int>)r2).MaxInclusive);
        Assert.AreEqual(r2.Minimum, ((Types.Range<int>)r1).Minimum);
        Assert.AreEqual(r2.Maximum, ((Types.Range<int>)r1).Maximum);
        Assert.AreEqual(r2.MinInclusive, ((Types.Range<int>)r1).MinInclusive);
        Assert.AreEqual(r2.MaxInclusive, ((Types.Range<int>)r1).MaxInclusive);
    }
}