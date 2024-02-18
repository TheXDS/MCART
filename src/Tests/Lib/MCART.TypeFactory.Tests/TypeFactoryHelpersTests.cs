/*
TypeExtensionsTests.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2024 César Andrés Morgan

Permission is hereby granted, free of charge, to any person obtaining a copy of
this software and associated documentation files (the "Software"), to deal in
the Software without restriction, including without limitation the rights to
use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
of the Software, and to permit persons to whom the Software is furnished to do
so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

#pragma warning disable CA1822

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

        Assert.That(a.IntProp1, Is.EqualTo(merged.IntProp1));
        merged.IntProp1 = 200;
        Assert.That(200, Is.EqualTo(merged.IntProp1));
        Assert.That(a.IntProp1, Is.EqualTo(merged.IntProp1));
        Assert.That(b.IntProp2, Is.EqualTo(merged.IntProp2));
        merged.IntProp2 = 155;
        Assert.That(155, Is.EqualTo(merged.IntProp2));
        Assert.That(b.IntProp2, Is.EqualTo(merged.IntProp2));

        Assert.That(merged.TestBool1(true));
        Assert.That(merged.TestBool1(false), Is.False);
        Assert.That(15, Is.EqualTo(merged.Add(5, 10)));
        Assert.That("Test", Is.EqualTo(merged.TestStr2()));
        merged.VoidMethod();
        Assert.That(1, Is.EqualTo(merged.IntProp2));
        Assert.That(b.IntProp2, Is.EqualTo(merged.IntProp2));
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
