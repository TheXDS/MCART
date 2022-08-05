/*
ExposedInfoTests.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2022 César Andrés Morgan

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
using TheXDS.MCART.Component;
using TheXDS.MCART.Resources;

namespace TheXDS.MCART.Tests.Component;

public class ExposedInfoTests
{
    [Test]
    public void Get_information_test()
    {
        var a = new ExposedInfo(new ValueExposeInfo()
        {
            Authors = new[] { "test" },
            Copyright = "Copyright (C) 2001 test",
            Description = "Test object",
            License = new TextLicense("Test license", "This is a test"),
            Name = "Test",
            ThirdPartyLicenses = new[] { SpdxLicense.FromId(SpdxLicenseId.CC0_1_0) },
            Version = new(1, 0)
        });
        Assert.IsNotAssignableFrom<ValueExposeInfo>(a);
        Assert.IsNotEmpty(a.Authors!);
        Assert.IsNotEmpty(a.Copyright);
        Assert.IsNotEmpty(a.Description);
        Assert.IsTrue(a.Has3rdPartyLicense);
        Assert.IsTrue(a.HasLicense);
        Assert.IsNotEmpty(a.InformationalVersion);
        Assert.IsNotNull(a.License);
        Assert.IsNotEmpty(a.Name);
        Assert.IsNotEmpty(a.ThirdPartyLicenses!);
        Assert.IsNotNull(a.Version);
    }
}
