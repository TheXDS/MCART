/*
EnumeratorExtensionsTests.cs

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

namespace TheXDS.MCART.Tests.Types.Extensions;
using NUnit.Framework;
using System;
using System.Linq;
using TheXDS.MCART.Types.Extensions;

public class EnumeratorExtensionsTests
{
    [Test]
    public void Skip_Test()
    {
        System.Collections.Generic.IEnumerable<int>? a = Enumerable.Range(0, 10);
        using System.Collections.Generic.IEnumerator<int>? e = a.GetEnumerator();
        e.MoveNext();
        Assert.AreEqual(0, e.Current);
        e.MoveNext();
        Assert.AreEqual(1, e.Current);
        Assert.AreEqual(5, e.Skip(5));
        Assert.AreEqual(6, e.Current);
        Assert.AreEqual(4, e.Skip(10));
        Assert.AreEqual(10, e.Current);
        Assert.Throws<ArgumentOutOfRangeException>(() => e.Skip(-1));
    }
}
