/*
SpdxLicenceTests.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2024 César Andrés Morgan

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

namespace TheXDS.MCART.Tests.Attributes;
using NUnit.Framework;
using System;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Resources;

public class SpdxLicenceTests
{
    [Test]
    public void SpdxLicenseBasicInstancing_Test()
    {
        SpdxLicenseAttribute? l = new(SpdxLicenseId.GPL_3_0_or_later);
        Assert.That(SpdxLicenseId.GPL_3_0_or_later, Is.EqualTo(l.Id));

        l = new(SpdxLicenseId.BSD0);
        Assert.That(SpdxLicenseId.BSD0, Is.EqualTo(l.Id));
    }

    [Test]
    public void SpdxInstancingWithStringArgs_Test()
    {
        SpdxLicenseAttribute? l = new("GPL_3_0_or_later");
        Assert.That(SpdxLicenseId.GPL_3_0_or_later, Is.EqualTo(l.Id));
    }

    [Test]
    public void GetLicense_test()
    {
        SpdxLicenseAttribute? l = new(SpdxLicenseId.GPL_3_0_or_later);
        Assert.That(l.GetLicense(new object()), Is.Not.Null);

        l = new("test_1_0");
        Assert.That(l.GetLicense(new object()), Is.Not.Null);
    }

    [Test]
    public void SpdxInstancingFailsIfIdNotDefined_Test()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new SpdxLicenseAttribute((SpdxLicenseId)int.MaxValue));
    }
}
