/*
ColorTests.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     C�sar Andr�s Morgan <xds_xps_ivx@hotmail.com>

Copyright � 2011 - 2021 C�sar Andr�s Morgan

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

namespace TheXDS.MCART.Ext.ComplexTypes.Tests;
using NUnit.Framework;
using TheXDS.MCART.Types.Entity;

public class ColorTests
{
    [Test]
    public void ComplexTypeToNormalTypeTest()
    {
        Color? c1 = new() { A = 255, B = 128, G = 192, R = 240 };
        Types.Color c2 = new(240, 192, 128, 255);

        Assert.AreEqual(c2, (Types.Color)c1);
        Assert.AreEqual(c1, (Color)c2);
    }
}
