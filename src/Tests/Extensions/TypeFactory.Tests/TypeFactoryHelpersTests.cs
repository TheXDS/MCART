/*
TypeExtensionsTests.cs

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

#pragma warning disable CA1822

using NUnit.Framework;
using System;
using System.Diagnostics.CodeAnalysis;
using TheXDS.MCART.Helpers;

namespace TheXDS.MCART.Tests;

public class TypeFactoryHelpersTests
{
    [ExcludeFromCodeCoverage]
    public class Test1
    {
        public int IntProp1 { get; set; }
        public bool TestBool1(bool retVal)
        {
            return retVal;
        }

        public int Add(int x, int y)
        {
            return x + y;
        }
    }

    [ExcludeFromCodeCoverage]
    public class Test2
    {
        public int IntProp2 { get; set; }
        public string TestStr2()
        {
            return "Test";
        }
        public void VoidMethod()
        {
            IntProp2 = 1;
        }
    }

    [ExcludeFromCodeCoverage]
    public class Test3
    {
        public int IntProp1 { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class Test4
    {
        public bool TestBool1(bool retVal)
        {
            return retVal;
        }
    }

    [Test]
    public void Merge_test()
    {
        var a = new Test1
        {
            IntProp1 = 99
        };

        var b = new Test2
        {
            IntProp2 = 15
        };

        dynamic merged = TypeFactoryHelpers.Merge(a, b);

        Assert.AreEqual(a.IntProp1, merged.IntProp1);
        merged.IntProp1 = 200;
        Assert.AreEqual(200, merged.IntProp1);
        Assert.AreEqual(a.IntProp1, merged.IntProp1);
        Assert.AreEqual(b.IntProp2, merged.IntProp2);
        merged.IntProp2 = 155;
        Assert.AreEqual(155, merged.IntProp2);
        Assert.AreEqual(b.IntProp2, merged.IntProp2);

        Assert.IsTrue(merged.TestBool1(true));
        Assert.IsFalse(merged.TestBool1(false));
        Assert.AreEqual(15, merged.Add(5, 10));
        Assert.AreEqual("Test", merged.TestStr2());
        merged.VoidMethod();
        Assert.AreEqual(1, merged.IntProp2);
        Assert.AreEqual(b.IntProp2, merged.IntProp2);
    }

    [Test]
    public void Merge_fails_on_property_collision()
    {
        var a = new Test1();
        var b = new Test3();
        Assert.Throws<InvalidOperationException>(() => TypeFactoryHelpers.Merge(a, b));
    }

    [Test]
    public void Merge_fails_on_method_collision()
    {
        var a = new Test1();
        var b = new Test4();
        Assert.Throws<InvalidOperationException>(() => TypeFactoryHelpers.Merge(a, b));
    }
}
