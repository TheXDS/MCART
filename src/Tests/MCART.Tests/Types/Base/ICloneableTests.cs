/*
ICloneableTests.cs

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

namespace TheXDS.MCART.Tests.Types.Base;
using NUnit.Framework;
using System;
using System.Diagnostics.CodeAnalysis;
using TheXDS.MCART.Types.Base;

public class ICloneableTests
{
    [ExcludeFromCodeCoverage]
    private class TestClass : ICloneable<TestClass>
    {
        public int Field;
        public float Property { get; set; }
    }

    [ExcludeFromCodeCoverage]
    private class BrokenTestClass : ICloneable<Exception>
    {
        public int Field;
        public float Property { get; set; }
    }

    [Test]
    public void Clone_Test()
    {
        TestClass x = new()
        {
            Field = 3,
            Property = 4f
        };
        
        TestClass copy = ((ICloneable<TestClass>)x).Clone();
        Assert.AreNotSame(x, copy);
        Assert.AreEqual(3, copy.Field);
        Assert.AreEqual(4f, copy.Property);
        
        object copy2 = ((ICloneable)x).Clone();
        Assert.AreNotSame(x, copy2);
        Assert.IsAssignableFrom<TestClass>(copy2);
        Assert.AreEqual(3, ((TestClass)copy2).Field);
        Assert.AreEqual(4f, ((TestClass)copy2).Property);
    }

    [Test]
    public void Clone_Contract_Test()
    {
        BrokenTestClass x = new()
        {
            Field = 3,
            Property = 4f
        };

        Assert.Throws<InvalidCastException>(() => ((ICloneable<Exception>)x).Clone());
    }
}
