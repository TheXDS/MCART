/*
AssemblyInfoTests.cs

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
using TheXDS.MCART.Component;
using NUnit.Framework;
using System.Linq;

namespace TheXDS.MCART.Tests.Component
{
    public class AssemblyInfoTests
    {
        [Test]
        public void Get_Information_Test()
        {
            var a = new AssemblyInfo(typeof(AssemblyInfo).Assembly);
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
        public void Self_Information_Test()
        {
            var a = new AssemblyInfo();
            Assert.AreSame(typeof(AssemblyInfoTests).Assembly, a.Assembly);
        }
    }
}
