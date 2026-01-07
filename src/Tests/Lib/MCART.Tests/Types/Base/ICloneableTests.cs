/*
ICloneableTests.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2026 César Andrés Morgan

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

using System.Diagnostics.CodeAnalysis;
using TheXDS.MCART.Types.Base;

namespace TheXDS.MCART.Tests.Types.Base;

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
        Assert.That(x, Is.Not.SameAs(copy));
        Assert.That(3, Is.EqualTo(copy.Field));
        Assert.That(4f, Is.EqualTo(copy.Property));
        
        object copy2 = ((ICloneable)x).Clone();
        Assert.That(x, Is.Not.SameAs(copy2));
        Assert.That(copy2, Is.AssignableFrom<TestClass>());
        Assert.That(3, Is.EqualTo(((TestClass)copy2).Field));
        Assert.That(4f, Is.EqualTo(((TestClass)copy2).Property));
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
