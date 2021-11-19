/*
CopyrightAttributeTests.cs

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

using TheXDS.MCART.Attributes;
using NUnit.Framework;
using TheXDS.MCART.Types;

namespace TheXDS.MCART.Tests.Attributes
{
    public class CopyrightAttributeTests
    {
        [Test]
        public void CopyrightAttributeBasicInstancing_Test()
        {
            var l = new CopyrightAttribute("Copyright (C) Test");
            Assert.AreEqual("Copyright (C) Test", l.Value);
            Assert.AreEqual("Copyright (C) Test", ((IValueAttribute<string?>)l).Value);

            l = new CopyrightAttribute("Test");
            Assert.AreEqual("Copyright © Test", l.Value);

            l = new CopyrightAttribute(1985, "Test");
            Assert.AreEqual("Copyright © 1985 Test", l.Value);

            l = new CopyrightAttribute(new Range<ushort>(1985, 2001), "Test");
            Assert.AreEqual("Copyright © 1985-2001 Test", l.Value);
        }
    }
}
