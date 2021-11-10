/*
LicenseFileAttributeTests.cs

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

using System;
using TheXDS.MCART.Attributes;
using NUnit.Framework;
using St = TheXDS.MCART.Resources.Strings;

namespace TheXDS.MCART.Tests.Attributes
{
    [Obsolete("El atributo se ha reemplazado por LicenseUriAttribute")]
    public class LicenseFileAttributeTests
    {
        [Test]
        public void LicenseFileAttributeBasicInstancing_Test()
        {
            var l = new LicenseFileAttribute(@"C:\Test.txt");
            Assert.AreEqual(@"C:\Test.txt", l.Value);
            Assert.AreEqual(@"C:\Test.txt", ((IValueAttribute<string?>)l).Value);
        }

        [Test]
        public void ReadLicenseFromLicenseFileAttribute_Test()
        {
            const string LicenseContents = "Test.";
            var f = System.IO.Path.GetTempFileName();
            System.IO.File.WriteAllText(f, LicenseContents);

            var l = new LicenseFileAttribute(f);
            Assert.AreEqual(LicenseContents, l.ReadLicense());

            try { System.IO.File.Delete(f); }
            catch { }
        }

        [Test]
        public void LicenseFileWithFileNotFoundDoesntFail_Test()
        {
            var f = System.IO.Path.GetFullPath(System.IO.Path.Combine(
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                $"{Guid.NewGuid()}.txt"));
            var l = new LicenseFileAttribute(f);
            Assert.AreEqual(St.Composition.Warn(St.Common.UnspecifiedLicense), l.ReadLicense());
        }
    }
}