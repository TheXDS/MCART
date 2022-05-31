/*
ICloneableTests.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2022 César Andrés Morgan

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
