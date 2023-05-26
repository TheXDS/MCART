/*
ErrorsTests.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2023 César Andrés Morgan

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

#pragma warning disable CS8974

using NUnit.Framework;
using TheXDS.MCART.Exceptions;
using TheXDS.MCART.Helpers;
using E = TheXDS.MCART.Resources.Errors;

namespace TheXDS.MCART.Tests.Resources;

public class ErrorsTests : ExceptionResourceTestClass
{
    [Test]
    public void InvalidValue_Test()
    {
        ArgumentException ex = TestException(E.InvalidValue("test"));
        Assert.That(ex.ParamName, Is.EqualTo("test"));
    }

    [Test]
    public void ClassNotInstantiable_Test()
    {
        ClassNotInstantiableException ex = TestException(E.ClassNotInstantiable());
        Assert.That(ex.OffendingObject, Is.Null);
    }

    [TestCase(typeof(int))]
    [TestCase(typeof(float))]
    [TestCase(typeof(Delegate))]
    [TestCase(null)]
    public void ClassNotInstantiable_With_Args_Test(Type? testType)
    {
        ClassNotInstantiableException ex = TestException(E.ClassNotInstantiable(testType));
        Assert.That(ex.OffendingObject, Is.EqualTo(testType));
    }

    [TestCase(typeof(int))]
    [TestCase(typeof(float))]
    [TestCase(typeof(Delegate))]
    public void MissingGuidAttr_Test(Type testType)
    {
        IncompleteTypeException ex = TestException(E.MissingGuidAttribute(testType));
        Assert.That(ex.OffendingObject, Is.EqualTo(testType));
    }

    [Test]
    public void Tamper_Test()
    {
       TestException(E.Tamper());
    }

    [Theory]
    [TestCase(typeof(int))]
    [TestCase(typeof(float))]
    [TestCase(typeof(Delegate))]
    public void EnumerableTypeExpected_Test(Type testType)
    {
        InvalidTypeException ex = TestException(E.EnumerableTypeExpected(testType));
        Assert.That(ex.OffendingObject, Is.EqualTo(testType));
    }

    [Test]
    public void CircularOpDetected_Test()
    {
        TestException(E.CircularOpDetected());
    }

    [Test]
    public void InterfaceNotImplemented_T_test()
    {
        TestException(E.InterfaceNotImplemented<ICloneable>());
    }
    
    [Test]
    public void InterfaceNotImplemented_test()
    {
        TestException(E.InterfaceNotImplemented(typeof(ICloneable)));
    }
    
    [Test]
    public void NullArgumentValue_test()
    {
        TestException(E.NullArgumentValue("x.y", "z"));
    }
    
    [Test]
    public void ListMustContainBoth_test()
    {
        TestException(E.ListMustContainBoth());
    }
    
    [Test]
    public void InvalidSelectorExpression_test()
    {
        TestException(E.InvalidSelectorExpression());
    }
        
    [Test]
    public void FormatNotSupported_test()
    {
        TestException(E.FormatNotSupported("ABC"));
    }
            
    [Test]
    public void InvalidValue_test()
    {
        var ex = TestException(E.InvalidValue("argName", "BAD", new TamperException()));
        Assert.That(ex.ParamName, Is.EqualTo("argName"));
        Assert.That(ex.InnerException, Is.InstanceOf<TamperException>());
        
        ex = TestException(E.InvalidValue(null, null, new NullReferenceException()));
        Assert.That(ex.ParamName, Is.Null);
        Assert.That(ex.InnerException, Is.InstanceOf<NullReferenceException>());
        
        ex = TestException(E.InvalidValue("argName"));
        Assert.That(ex.ParamName, Is.EqualTo("argName"));
    }

    [Test]
    public void ValueOutOfRange_test()
    {
        Assert.That(TestException(E.ValueOutOfRange("argName", "A", "B")).ParamName, Is.EqualTo("argName"));
    }

    [Test]
    public void UndefinedEnum_T_test()
    {
        Assert.That(TestException(E.UndefinedEnum("argName", (DayOfWeek)9999)).ParamName, Is.EqualTo("argName"));
    }
    
    [Test]
    public void UndefinedEnum_test()
    {
        Assert.That(TestException(E.UndefinedEnum(typeof(DayOfWeek), "argName", (DayOfWeek)9999)).ParamName, Is.EqualTo("argName"));
    }

    [Test]
    public void CantWriteObj_test()
    {
        TestException(E.CantWriteObj(typeof(int)));
    }

    [Test]
    public void PropIsReadOnly_test()
    {
        TestException(E.PropIsReadOnly(ReflectionHelpers.GetProperty<Exception, string>(e => e.Message)));
    }
    
    [Test]
    public void MissingMember_test()
    {
        TestException(E.MissingMember(typeof(int), ReflectionHelpers.GetProperty<Exception, string>(e => e.Message)));
    }

    [Test]
    public void EnumExpected_test()
    {
        Assert.That(TestException(E.EnumExpected("argName", typeof(Stream))).ParamName, Is.EqualTo("argName"));
    }

    [Test]
    public void MinGtMax_test()
    {
        TestException(E.MinGtMax());
    }
    
    [TestCase(typeof(int))]
    [TestCase(typeof(Stream))]
    [TestCase(typeof(DayOfWeek))]
    [TestCase(null)]
    public void ClassNotInstantiable_test(Type? testType)
    {
        var ex = TestException(E.ClassNotInstantiable(testType));
        Assert.That(ex.OffendingObject, Is.EqualTo(testType));
    }
    
    [TestCase(typeof(int))]
    [TestCase(typeof(Stream))]
    [TestCase(typeof(DayOfWeek))]
    public void MissingGuidAttribute_test(Type testType)
    {
        var ex = TestException(E.MissingGuidAttribute(testType));
        Assert.That(ex.OffendingObject, Is.EqualTo(testType));
    }
    
    [Test]
    public void Tamper_test()
    {
        TestException(E.Tamper());
    }    
    
    [TestCase(typeof(int))]
    [TestCase(typeof(Stream))]
    [TestCase(typeof(DayOfWeek))]
    public void EnumerableTypeExpected_test(Type testType)
    {
        var ex = TestException(E.EnumerableTypeExpected(testType));
        Assert.That(ex.OffendingObject, Is.EqualTo(testType));
    }
    
    [TestCase(typeof(int))]
    [TestCase(typeof(Stream))]
    [TestCase(typeof(DayOfWeek))]
    public void UnexpectedType_test(Type testType)
    {
        var ex = TestException(E.UnexpectedType(testType, typeof(IEnumerable<float>)));
        Assert.That(ex.OffendingObject, Is.EqualTo(testType));
    }
        
    [Test]
    public void CircularOpDetected_test()
    {
        TestException(E.CircularOpDetected());
    }

    [Test]
    public void CannotInstanceClass_test()
    {
        var ex = TestException(E.CannotInstanceClass(typeof(File), new PathTooLongException()));
        Assert.That(ex.InnerException, Is.InstanceOf<PathTooLongException>());
    }
    
    [Test]
    public void DuplicateData_test()
    {
        TestException(E.DuplicateData("X"));
    }
    
    [Test]
    public void InvalidReturnValue_test()
    {
        static int BadDelegate() => 0;
        var ex = TestException(E.InvalidReturnValue(BadDelegate, 0));
        Assert.That(ex.OffendingFunction, Is.EqualTo(BadDelegate));
        Assert.That(ex.OffendingReturnValue, Is.EqualTo(0));
    } 
        
    [Test]
    public void BinaryWriteNotSupported_test()
    {
        TestException(E.BinaryWriteNotSupported(typeof(int), ReflectionHelpers.GetMethod<BinaryWriter, Action<int>>(b => b.Write)));
    }

    [Test]
    public void EmptyCollection_test()
    {
        var ex = TestException(E.EmptyCollection(Array.Empty<int>()));
        Assert.That(ex.InnerException, Is.InstanceOf<EmptyCollectionException>());
        Assert.That(((EmptyCollectionException)ex.InnerException!).OffendingObject, Is.InstanceOf<int[]>());
    }
}
