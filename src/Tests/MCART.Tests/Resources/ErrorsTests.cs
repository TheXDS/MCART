/*
ErrorsTests.cs

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
