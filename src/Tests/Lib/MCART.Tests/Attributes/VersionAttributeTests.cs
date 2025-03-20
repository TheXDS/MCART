/*
VersionAttributeTests.cs

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

using TheXDS.MCART.Attributes;

namespace TheXDS.MCART.Tests.Attributes;

public class VersionAttributeTests
{
    [Test]
    public void Ctor_test()
    {
        var a = new VersionAttribute(1, 2, 3, 4);
        Assert.That(1, Is.EqualTo(a.Value.Major));
        Assert.That(2, Is.EqualTo(a.Value.Minor));
        Assert.That(3, Is.EqualTo(a.Value.Build));
        Assert.That(4, Is.EqualTo(a.Value.Revision));

        a = new VersionAttribute(5, 6);
        Assert.That(5, Is.EqualTo(a.Value.Major));
        Assert.That(6, Is.EqualTo(a.Value.Minor));
        Assert.That(0, Is.EqualTo(a.Value.Build));
        Assert.That(0, Is.EqualTo(a.Value.Revision));

        a = new VersionAttribute(7.8);
        Assert.That(7, Is.EqualTo(a.Value.Major));
        Assert.That(8, Is.EqualTo(a.Value.Minor));
        Assert.That(0, Is.EqualTo(a.Value.Build));
        Assert.That(0, Is.EqualTo(a.Value.Revision));

        a = new VersionAttribute(9);
        Assert.That(9, Is.EqualTo(a.Value.Major));
        Assert.That(0, Is.EqualTo(a.Value.Minor));
        Assert.That(0, Is.EqualTo(a.Value.Build));
        Assert.That(0, Is.EqualTo(a.Value.Revision));
    }

    [Theory]
    [TestCase(double.NaN)]
    [TestCase(double.PositiveInfinity)]
    [TestCase(double.NegativeInfinity)]
    public void Ctor_contract_test(double invalidValue)
    {
        Assert.Throws<ArgumentException>(() => _ = new VersionAttribute(invalidValue));
    }
}
