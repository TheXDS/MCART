/*
ErrorsTests.cs

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

namespace TheXDS.MCART.Tests.Resources;
using NUnit.Framework;
using System;
using TheXDS.MCART.Exceptions;
using E = MCART.Resources.Errors;
public class ErrorsTests
{
    [Test]
    public void InvalidValue_Test()
    {
        ArgumentException ex = E.InvalidValue("test");
        Assert.IsAssignableFrom<ArgumentException>(ex);
        Assert.AreEqual("test", ex.ParamName);
    }

    [Test]
    public void ClassNotInstantiable_Test()
    {
        ClassNotInstantiableException ex = E.ClassNotInstantiable();
        Assert.IsAssignableFrom<ClassNotInstantiableException>(ex);
        Assert.IsNull(ex.OffendingObject);
    }

    [Theory]
    [CLSCompliant(false)]
    [TestCase(typeof(int))]
    [TestCase(typeof(float))]
    [TestCase(typeof(Delegate))]
    [TestCase(null)]
    public void ClassNotInstantiable_With_Args_Test(Type? testType)
    {
        ClassNotInstantiableException ex = E.ClassNotInstantiable(testType);
        Assert.IsAssignableFrom<ClassNotInstantiableException>(ex);
        Assert.AreEqual(testType, ex.OffendingObject);
    }

    [Theory]
    [TestCase(typeof(int))]
    [TestCase(typeof(float))]
    [TestCase(typeof(Delegate))]
    public void MissingGuidAttr_Test(Type testType)
    {
        IncompleteTypeException ex = E.MissingGuidAttr(testType);
        Assert.IsAssignableFrom<IncompleteTypeException>(ex);
        Assert.AreEqual(testType, ex.OffendingObject);
    }

    [Test]
    public void Tamper_Test()
    {
        Assert.IsAssignableFrom<TamperException>(E.Tamper());
    }

    [Theory]
    [TestCase(typeof(int))]
    [TestCase(typeof(float))]
    [TestCase(typeof(Delegate))]
    public void EnumerableTypeExpected_Test(Type testType)
    {
        InvalidTypeException ex = E.EnumerableTypeExpected(testType);
        Assert.IsAssignableFrom<InvalidTypeException>(ex);
        Assert.AreEqual(testType, ex.OffendingObject);
    }

    [Test]
    public void CircularOpDetected_Test()
    {
        Assert.IsAssignableFrom<InvalidOperationException>(E.CircularOpDetected());
    }
}
