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
using TheXDS.MCART.Security.Password;
using NUnit.Framework;
using System.Linq;

namespace TheXDS.MCART.Security.Legacy.Tests
{
    /* NOTA:
     * =====
     * Esta clase de pruebas se implementa en este proyecto debido a que se
     * implementan componentes de terceros en esta librería, de modo que es
     * posible validar la funcionalidad mediante la cual se accede a dicha
     * información.
     */
    public class AssemblyInfoTests
    {
        [Test]
        public void Get_Information_Test()
        {
            AssemblyInfo? a = new(typeof(PasswordStorage).Assembly);
            Assert.True(a.Has3rdPartyLicense);
            Assert.Contains(typeof(PasswordStorage), a.ThirdPartyComponents.ToArray());
            Assert.True(a.ThirdPartyLicenses.Any(p => p.Name.Contains("MIT", StringComparison.InvariantCultureIgnoreCase)));
        }
    }
}
