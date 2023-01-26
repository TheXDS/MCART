/*
AssemblyInfoTests.cs

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
using System.Linq;
using TheXDS.MCART.Component;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.Tests.Component;

public class AssemblyInfoTests
{
    [Test]
    public void Get_Information_Test()
    {
        AssemblyInfo? a = new(typeof(AssemblyInfo).Assembly);
        Assert.AreEqual("MCART", a.Name);
        Assert.IsTrue(a.Copyright!.StartsWith("Copyright"));
        Assert.NotNull(a.Description);
        Assert.IsTrue(a.Authors!.Contains("César Andrés Morgan"));
        Assert.IsAssignableFrom<Version>(a.Version);
        Assert.True(a.HasLicense);
        Assert.NotNull(a.License);
        Assert.NotNull(a.InformationalVersion);
#if CLSCompliance
        Assert.True(a.ClsCompliant);
#endif
    }

    [Test]
    public void Get_extended_information_test()
    {
        var a = new AssemblyInfo(typeof(object).Assembly);
        Assert.IsFalse(a.Beta);
        Assert.IsNotEmpty(a.Copyright);
        Assert.IsNotEmpty(a.Description);
        Assert.IsFalse(a.Has3rdPartyLicense);
        Assert.IsNotEmpty(a.InformationalVersion);
        Assert.IsNull(a.License);
        Assert.IsNotEmpty(a.Name);
        Assert.IsNotEmpty(a.Product);
        Assert.IsEmpty(a.ThirdPartyComponents);
        Assert.IsEmpty(a.ThirdPartyLicenses);
        Assert.IsNotEmpty(a.Trademark);
        Assert.IsFalse(a.Unmanaged);
        Assert.IsNotNull(a.Version);
    }

    [Test]
    public void Self_Information_Test()
    {
        AssemblyInfo? a = new();
        Assert.AreSame(typeof(AssemblyInfoTests).Assembly, a.Assembly);
    }
}
