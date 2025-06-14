/*
EnumeratorExtensionsTests.cs

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

using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.Tests.Types.Extensions;

public class EnumeratorExtensionsTests
{
    [Test]
    public void Skip_Test()
    {
        int[] a = [.. Enumerable.Range(0, 10)];
        using IEnumerator<int>? e = a.AsEnumerable().GetEnumerator();
        e.MoveNext();
        Assert.That(0, Is.EqualTo(e.Current));
        e.MoveNext();
        Assert.That(1, Is.EqualTo(e.Current));
        Assert.That(5, Is.EqualTo(e.Skip(5)));
        Assert.That(6, Is.EqualTo(e.Current));
        Assert.That(3, Is.EqualTo(e.Skip(10)));
    }

    [Test]
    public void Skip_throws_if_steps_is_negative()
    {
        IEnumerable<int>? a = [];
        using IEnumerator<int>? e = a.GetEnumerator();
        Assert.Throws<ArgumentOutOfRangeException>(() => e.Skip(-1));
    }

    [Test]
    public void Skip_skips_zero_if_collection_is_empty()
    {
        IEnumerable<int>? a = [];
        using IEnumerator<int>? e = a.GetEnumerator();
        Assert.That(e.Skip(1), Is.Zero);
        Assert.That(false, Is.EqualTo(e.MoveNext()));
    }

    [Test]
    public void Skip_skips_zero_if_steps_is_zero()
    {
        IEnumerable<int>? a = Enumerable.Range(0, 10);
        using IEnumerator<int>? e = a.GetEnumerator();
        Assert.That(e.Skip(0), Is.Zero);
        Assert.That(true, Is.EqualTo(e.MoveNext()));
    }
}
