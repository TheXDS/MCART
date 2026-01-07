/*
EmbeddedLicenseAttributeTests.cs

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

using System.Reflection;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Resources;

namespace TheXDS.MCART.Tests.Attributes;

public class EmbeddedLicenseAttributeTests
{
    private static readonly EmbeddedLicenseAttribute attribute = new("LICENSE", "TheXDS.MCART.Resources.Data");

    [Test]
    public void Ctor_string_string_test()
    {
        var a = new EmbeddedLicenseAttribute("value", "path");
        Assert.That("value", Is.EqualTo(a.Value));
        Assert.That("path", Is.EqualTo(a.Path));
    }

    [Test]
    public void GetLicense_returns_expected_license_from_Assembly()
    {
        VerifyLicense(attribute.GetLicense(typeof(EmbeddedLicenseAttribute).Assembly));
    }

    [Test]
    public void GetLicense_returns_expected_license_from_Type()
    {
        VerifyLicense(attribute.GetLicense(typeof(EmbeddedLicenseAttribute)));
    }

    [Test]
    public void GetLicense_returns_expected_license_from_MemberInfo()
    {
        VerifyLicense(attribute.GetLicense(typeof(EmbeddedLicenseAttribute).GetMethods().First()));
    }

    [Test]
    public void GetLicense_returns_expected_license_from_object()
    {
        VerifyLicense(attribute.GetLicense(attribute));
    }

    [Test]
    public void GetLicense_returns_unspecified_when_path_is_empty()
    {
        EmbeddedLicenseAttribute attribute = new("", "TheXDS.MCART.Resources.Data");
        var license = attribute.GetLicense(Assembly.GetExecutingAssembly());
        Assert.That(license.Name, Is.EqualTo(License.Unspecified.Name));
        Assert.That(license.LicenseContent, Is.EqualTo(License.Unspecified.LicenseContent));
    }

    [Test]
    public void GetLicense_throws_when_context_is_null()
    {
        Assert.Throws<ArgumentNullException>(() => attribute.GetLicense(null!));
    }

    private static void VerifyLicense(License license)
    {
        Assert.That(license, Is.Not.Null);
        Assert.That(license.Name, Is.Not.Null.Or.Empty.Or.WhiteSpace);
        Assert.That(license.LicenseContent, Is.Not.Null.Or.Empty.Or.WhiteSpace);
        Assert.That(license.Name.Contains("MIT License"));
    }
}
