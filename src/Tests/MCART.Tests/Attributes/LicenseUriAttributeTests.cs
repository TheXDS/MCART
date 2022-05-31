﻿/*
LicenseUriAttributeTests.cs

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

namespace TheXDS.MCART.Tests.Attributes;
using NUnit.Framework;
using System;
using TheXDS.MCART.Attributes;
using St = MCART.Resources.Strings;

public class LicenseUriAttributeTests
{
    [Test]
    public void LicenseUriAttributeBasicInstancing_Test()
    {
        const string f = @"C:\Test.txt";
        LicenseUriAttribute? l = new(f);
        Assert.AreEqual(f, l.Value);
        Assert.AreEqual(f, ((IValueAttribute<string?>)l).Value);
    }

    [Test]
    public void ReadLicenseFromFile_Test()
    {
        const string LicenseContents = "Test.";
        string? f = System.IO.Path.GetTempFileName();
        System.IO.File.WriteAllText(f, LicenseContents);

        LicenseUriAttribute? l = new(f);
        Assert.AreEqual(LicenseContents, l.GetLicense().LicenseContent);

        try { System.IO.File.Delete(f); }
        catch { }
    }

    [Test]
    public void ReadLicenseFromNowhere_Test()
    {
        string? f = System.IO.Path.GetFullPath(System.IO.Path.Combine(
            Guid.NewGuid().ToString(),
            Guid.NewGuid().ToString(),
            Guid.NewGuid().ToString(),
            $"{Guid.NewGuid()}.txt"));

        string? l = new LicenseUriAttribute(f).GetLicense().LicenseContent;
        Assert.AreEqual(St.Composition.Warn(St.Errors.ErrorLoadingLicense), l);
    }
}
