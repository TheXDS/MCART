/*
LicenseTextAttributeTests.cs

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

public class LicenseTextAttributeTests
{
    [Test]
    public void BasicInstancingTest()
    {
        MCART.Resources.License? a = new LicenseTextAttribute("Title", "Test").GetLicense(null!);

        Assert.AreEqual("Title", a.Name);
        Assert.AreEqual("Test", a.LicenseContent);

        a = new LicenseTextAttribute("Title\nTest").GetLicense(null!);
        Assert.AreEqual("Title", a.Name);
        Assert.AreEqual("Title\nTest", a.LicenseContent);
    }

    [Test]
    public void ConstructorContract_Test()
    {
        Assert.Throws<ArgumentNullException>(() => new LicenseTextAttribute(null!));
        Assert.Throws<ArgumentNullException>(() => new LicenseTextAttribute(null!, "Test"));
        Assert.Throws<ArgumentNullException>(() => new LicenseTextAttribute("Test", null!));
        Assert.Throws<ArgumentNullException>(() => new LicenseTextAttribute(null!, null!));
    }
}
