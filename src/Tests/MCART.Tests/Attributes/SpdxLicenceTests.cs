/*
SpdxLicenceTests.cs

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
using TheXDS.MCART.Resources;

public class SpdxLicenceTests
{
    [Test]
    public void SpdxLicenseBasicInstancing_Test()
    {
        SpdxLicenseAttribute? l = new(SpdxLicenseId.GPL_3_0_or_later);
        Assert.AreEqual(SpdxLicenseId.GPL_3_0_or_later, l.Id);
    }

    [Test]
    public void SpdxInstancingWithStringArgs_Test()
    {
        SpdxLicenseAttribute? l = new("GPL_3_0_or_later");
        Assert.AreEqual(SpdxLicenseId.GPL_3_0_or_later, l.Id);
    }

    [Test]
    public void SpdxInstancingFailsIfIdNotDefined_Test()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new SpdxLicenseAttribute((SpdxLicenseId)int.MaxValue));
    }
}
