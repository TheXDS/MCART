/*
LicenseUriAttributeTests.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2021 César Andrés Morgan

Morgan's CLR Advanced Runtime (MCART) is free software: you can redistribute it
and/or modify it under the terms of the GNU General Public License as published
by the Free Software Foundation, either version 3 of the License, or (at your
option) any later version.

Morgan's CLR Advanced Runtime (MCART) is distributed in the hope that it will
be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General
Public License for more details.

You should have received a copy of the GNU General Public License along with
this program. If not, see <http://www.gnu.org/licenses/>.
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
