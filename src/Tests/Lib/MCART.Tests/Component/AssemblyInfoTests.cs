/*
AssemblyInfoTests.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2026 César Andrés Morgan

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

using TheXDS.MCART.Component;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.Tests.Component;

public class AssemblyInfoTests
{
    [Test]
    public void Get_Information_Test()
    {
        AssemblyInfo? a = new(typeof(AssemblyInfo).Assembly);
        Assert.That("MCART", Is.EqualTo(a.Name));
        Assert.That(a.Copyright!.StartsWith("Copyright"));
        Assert.That(a.Description, Is.Not.Null);
        Assert.That(a.Authors!.Contains("César Andrés Morgan"));
        Assert.That(a.Version,Is.AssignableFrom<Version>());
        Assert.That(a.HasLicense);
        Assert.That(a.License, Is.Not.Null);
        Assert.That(a.InformationalVersion, Is.Not.Null);
#if CLSCompliance
        Assert.That(a.ClsCompliant);
#endif
    }

    [Test]
    public void Get_extended_information_test()
    {
        var a = new AssemblyInfo(typeof(object).Assembly);
        Assert.That(a.Beta, Is.False);
        Assert.That(a.Copyright, Is.Not.Empty);
        Assert.That(a.Description, Is.Not.Empty);
        Assert.That(a.Has3rdPartyLicense, Is.False);
        Assert.That(a.InformationalVersion, Is.Not.Empty);
        Assert.That(a.License,Is.Null);
        Assert.That(a.Name, Is.Not.Empty);
        Assert.That(a.Product, Is.Not.Empty);
        Assert.That(a.ThirdPartyComponents, Is.Empty);
        Assert.That(a.ThirdPartyLicenses, Is.Empty);
        Assert.That(a.Trademark, Is.Not.Empty);
        Assert.That(a.Unmanaged, Is.False);
        Assert.That(a.Version, Is.Not.Null);
    }

    [Test]
    public void Self_Information_Test()
    {
        AssemblyInfo? a = new();
        Assert.That(typeof(AssemblyInfoTests).Assembly, Is.SameAs(a.Assembly));
    }
}
