/*
RandomExtensionsTests.cs

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

using TheXDS.MCART.Helpers;
using TheXDS.MCART.Types;
using static TheXDS.MCART.Types.Extensions.RandomExtensions;

namespace TheXDS.MCART.Tests.Types.Extensions;

public class RandomExtensionsTests
{
    [Test]
    public void RndText_Test()
    {
        string? str = Rnd.RndText(10);
        Assert.That(str, Is.AssignableFrom<string>());
        Assert.That(10, Is.EqualTo(str.Length));
    }

    [Test]
    public void Next_With_Range_Test()
    {
        Range<int> r = new(1, 100);
        for (int j = 0; j < 1000; j++)
        {
            Assert.That(Rnd.Next(r).IsBetween(1, 100));
        }
    }

    [Test]
    public void CoinFlip_Test()
    {
        List<bool> l = new();
        for (int j = 0; j < 1000; j++)
        {
            l.Add(Rnd.CoinFlip());
        }
        Assert.That(l, Contains.Item(true));
        Assert.That(l, Contains.Item(false));
    }
}
