/*
RangeTests.cs

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
using TheXDS.MCART.Types;

namespace TheXDS.MCART.Tests.Types;

public class RangeTests
{
    [TestCase("1-5")]
    [TestCase("1 - 5")]
    [TestCase("1 5")]
    [TestCase("1, 5")]
    [TestCase("1;5")]
    [TestCase("1:5")]
    [TestCase("1|5")]
    [TestCase("1..5")]
    [TestCase("1...5")]
    [TestCase("1; 5")]
    [TestCase("1 : 5")]
    [TestCase("1 | 5")]
    [TestCase("1,5")]
    [TestCase("1 .. 5")]
    [TestCase("1 => 5")]
    [TestCase("1 -> 5")]
    [TestCase("1=>5")]
    [TestCase("1->5")]
    public void TryParse_Test(string testArg)
    {
        Assert.True(Range<int>.TryParse(testArg, out Range<int> range));
        Assert.AreEqual(1, range.Minimum);
        Assert.AreEqual(5, range.Maximum);
    }

    [Test]
    public void TryParse_Failing_To_Parse_Test()
    {
        Assert.False(Range<int>.TryParse("TEST", out _));
    }

    [Test]
    public void Join_Test()
    {
        Range<int> a = new(1, 5);
        Range<int> b = new(3, 8);
        Range<int> r = a.Join(b);

        Assert.AreEqual(1, r.Minimum);
        Assert.AreEqual(8, r.Maximum);
    }

    [Test]
    public void Intersect_Test()
    {
        Range<int> a = new(1, 5);
        Range<int> b = new(3, 8);
        Range<int> r = a.Intersect(b);

        Assert.AreEqual(3, r.Minimum);
        Assert.AreEqual(5, r.Maximum);
    }

    [TestCase(1, 3, 2, 5, true, false)]
    [TestCase(1, 2, 3, 4, false, false)]
    [TestCase(1, 2, 2, 3, false, false)]
    [TestCase(1, 2, 2, 3, true, true)]
    public void Intersects_Test(int min1, int max1, int min2, int max2, bool expected, bool inclusively)
    {
        Range<int> a = new(min1, max1, inclusively);
        Range<int> b = new(min2, max2, inclusively);

        Assert.AreEqual(expected, a.Intersects(b));
    }

    [Test]
    public void Minimum_Test()
    {
        Range<int> x = new(1, 10);
        Assert.AreEqual(1,x.Minimum);
        x.Minimum = 5;
        Assert.AreEqual(5,x.Minimum);
        Assert.Throws<ArgumentOutOfRangeException>(() => x.Minimum = 11);
    }
    
    [Test]
    public void Maximum_Test()
    {
        Range<int> x = new(1, 10);
        Assert.AreEqual(10,x.Maximum);
        x.Maximum = 5;
        Assert.AreEqual(5,x.Maximum);
        Assert.Throws<ArgumentOutOfRangeException>(() => x.Maximum = -1);
    }

    [Test]
    public void Ctor_Contract_Test()
    {
        Assert.Throws<ArgumentException>(() => _ = new Range<int>(10, 1));
    }

    [Test]
    public void Ctor_Test()
    {
        Range<int> r;

        r = new(10);
        Assert.AreEqual(default(int), r.Minimum);
        Assert.AreEqual(10, r.Maximum);
        Assert.True(r.MinInclusive);
        Assert.True(r.MaxInclusive);
        
        r = new(10, true);
        Assert.AreEqual(default(int), r.Minimum);
        Assert.AreEqual(10, r.Maximum);
        Assert.True(r.MinInclusive);
        Assert.True(r.MaxInclusive);
        
        r = new(10, false);
        Assert.AreEqual(default(int), r.Minimum);
        Assert.AreEqual(10, r.Maximum);
        Assert.False(r.MinInclusive);
        Assert.False(r.MaxInclusive);
        
        r = new(5, 10);
        Assert.AreEqual(5, r.Minimum);
        Assert.AreEqual(10, r.Maximum);
        Assert.True(r.MinInclusive);
        Assert.True(r.MaxInclusive);
        
        r = new(5, 10, true);
        Assert.AreEqual(5, r.Minimum);
        Assert.AreEqual(10, r.Maximum);
        Assert.True(r.MinInclusive);
        Assert.True(r.MaxInclusive);
        
        r = new(5, 10, false);
        Assert.AreEqual(5, r.Minimum);
        Assert.AreEqual(10, r.Maximum);
        Assert.False(r.MinInclusive);
        Assert.False(r.MaxInclusive);
        
        r = new(5, 10, true, false);
        Assert.AreEqual(5, r.Minimum);
        Assert.AreEqual(10, r.Maximum);
        Assert.True(r.MinInclusive);
        Assert.False(r.MaxInclusive);
        
        r = new(5, 10, false, true);
        Assert.AreEqual(5, r.Minimum);
        Assert.AreEqual(10, r.Maximum);
        Assert.False(r.MinInclusive);
        Assert.True(r.MaxInclusive);
    }

    [Test]
    public void ToString_Test()
    {
        Range<int> r = new(5, 10);
        Assert.AreEqual("5 - 10", r.ToString());
    }

    [Test]
    public void Parse_Test()
    {
        Assert.AreEqual(new Range<int>(5, 10), Range<int>.Parse("5..10"));
        Assert.Throws<FormatException>(() => Range<int>.Parse("TEST"));
    }
    
    [Test]
    public void Add_Op_Test()
    {
        Range<int> a = new(1, 5);
        Range<int> b = new(3, 8);
        Range<int> r = a + b;

        Assert.AreEqual(1, r.Minimum);
        Assert.AreEqual(8, r.Maximum);
    }
    
    [Test]
    public void Equals_Test()
    {
        Range<int> a = new(1, 5);
        Range<int> b = a.Clone();
        Assert.True(a.Equals(b));
        Assert.True(a.Equals((object)b));
        Assert.True(a == b);
        Assert.False(a != b);
        b.MaxInclusive = false;
        Assert.False(a.Equals(b));
        Assert.False(a == b);
        Assert.True(a != b);
        Assert.False(a.Equals((object)b));
        Assert.False(a.Equals(null));
    }

    [Test]
    public void GetHashCode_Test()
    {
        Range<int> a = new(1, 5);
        Range<int> b = a.Clone();
        Assert.AreEqual(a.GetHashCode(), b.GetHashCode());
        b.MaxInclusive = false;
        Assert.AreNotEqual(a.GetHashCode(), b.GetHashCode());
    }
}
