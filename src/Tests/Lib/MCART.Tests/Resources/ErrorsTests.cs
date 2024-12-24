/*
ErrorsTests.cs

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

#pragma warning disable CS8974

using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using TheXDS.MCART.Exceptions;
using TheXDS.MCART.Helpers;
using E = TheXDS.MCART.Resources.Errors;

namespace TheXDS.MCART.Tests.Resources;

public class ErrorsTests : ExceptionResourceTestClass
{
    [Test]
    public void InvalidArgumentException_Test()
    {
        var argName = "testArg";
        var msg = $"Test message {typeof(InvalidArgumentException)}";
        var inner = new Exception(msg);
        InvalidArgumentException newEx;

        TestException(new InvalidArgumentException());
        Assert.That(TestException(new InvalidArgumentException(argName)).ParamName, Is.EqualTo(argName));
        Assert.That(TestException(new InvalidArgumentException(inner)).InnerException, Is.EqualTo(inner));

        newEx = TestException(new InvalidArgumentException(msg, inner));
        Assert.That(newEx.Message.StartsWith(msg));
        Assert.That(newEx.InnerException, Is.EqualTo(inner));

        newEx = TestException(new InvalidArgumentException(msg, argName));
        Assert.That(newEx.Message.StartsWith(msg));
        Assert.That(newEx.ParamName, Is.EqualTo(argName));

        newEx = TestException(new InvalidArgumentException(inner, argName));
        Assert.That(newEx.InnerException, Is.EqualTo(inner));
        Assert.That(newEx.ParamName, Is.EqualTo(argName));

        newEx = TestException(new InvalidArgumentException(msg, inner, argName));
        Assert.That(newEx.Message.StartsWith(msg));
        Assert.That(newEx.InnerException, Is.EqualTo(inner));
        Assert.That(newEx.ParamName, Is.EqualTo(argName));
    }

    [Test]
    public void InvalidValue_Test()
    {
        ArgumentException ex = TestException(E.InvalidValue("test"));
        Assert.That(ex.ParamName, Is.EqualTo("test"));
    }

    [Test]
    public void ClassNotInstantiableException_Ctors_Test()
    {
        TestOffendingExceptionCtors<ClassNotInstantiableException, Type?>(typeof(float));
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
    public void EmptyCollectionException_Ctors_Test()
    {
        TestOffendingExceptionCtors<EmptyCollectionException, IEnumerable>(Array.Empty<int>());
    }

    [Test]
    public void IncompleteTypeException_Ctors_Test()
    {
        TestOffendingExceptionCtors<IncompleteTypeException, Type>(typeof(int));
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
    public void InvalidMethodSignatureException_Ctors_Test()
    {
        TestOffendingExceptionCtors<InvalidMethodSignatureException, MethodInfo>(ReflectionHelpers.GetMethod<int>(i => (Func<string>)i.ToString));
    }

    [Test]
    public void InvalidTypeException_Ctors_Test()
    {
        TestOffendingExceptionCtors<InvalidTypeException, Type>(typeof(int));
    }

    [Test]
    public void InvalidUriException_Ctors_Test()
    {
        TestOffendingExceptionCtors<InvalidUriException, Uri>(new Uri("http://test/"));
    }

    [Test]
    public void MissingTypeException_Ctors_Test()
    {
        TestOffendingExceptionCtors<MissingTypeException, Type>(typeof(int));
    }

    [Test]
    public void NullArgumentValue_test()
    {
        TestException(E.NullArgumentValue("x.y", "z"));
    }

    [Test]
    public void NullItemException_Ctors_Test()
    {
        TestOffendingExceptionCtors<NullItemException, IList>(new object?[] { null });

        var ex = new NullItemException(new object?[] { "", null }) { NullIndex = 1 };
        Assert.That(ex.NullIndex, Is.EqualTo(1));
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
    public void InvalidReturnValueException_test()
    {
        [ExcludeFromCodeCoverage]
        static int BadDelegate() => 0;
        var msg = $"Test message {typeof(InvalidReturnValueException)}";
        var inner = new Exception(msg);
        InvalidReturnValueException newEx;
        TestException(new InvalidReturnValueException());

        newEx = TestException(new InvalidReturnValueException(BadDelegate));
        Assert.That(newEx.OffendingFunction, Is.EqualTo(BadDelegate));
        Assert.That(newEx.OffendingFunctionName!.Contains(nameof(BadDelegate)));

        newEx = TestException(new InvalidReturnValueException(msg, inner));
        Assert.That(newEx.Message, Is.EqualTo(msg));
        Assert.That(newEx.InnerException, Is.EqualTo(inner));

        newEx = TestException(new InvalidReturnValueException(nameof(BadDelegate)));
        Assert.That(newEx.OffendingFunctionName!.Contains(nameof(BadDelegate)));

        newEx = TestException(new InvalidReturnValueException(nameof(BadDelegate), 0));
        Assert.That(newEx.OffendingFunction, Is.Null);
        Assert.That(newEx.OffendingFunctionName!.Contains(nameof(BadDelegate)));
        Assert.That(newEx.OffendingReturnValue, Is.EqualTo(0));

        newEx = TestException(new InvalidReturnValueException(BadDelegate, 0, inner));
        Assert.That(newEx.OffendingFunction, Is.EqualTo(BadDelegate));
        Assert.That(newEx.OffendingFunctionName!.Contains(nameof(BadDelegate)));
        Assert.That(newEx.OffendingReturnValue, Is.EqualTo(0));
        Assert.That(newEx.InnerException, Is.EqualTo(inner));

        newEx = TestException(new InvalidReturnValueException(nameof(BadDelegate), 0, inner));
        Assert.That(newEx.OffendingFunction, Is.Null);
        Assert.That(newEx.OffendingFunctionName!.Contains(nameof(BadDelegate)));
        Assert.That(newEx.OffendingReturnValue, Is.EqualTo(0));
        Assert.That(newEx.InnerException, Is.EqualTo(inner));
    }

    [Test]
    public void InvalidReturnValue_test()
    {
        static int BadDelegate() => 0;
        var ex = TestException(E.InvalidReturnValue(BadDelegate, 0));
        Assert.That(ex.OffendingFunction, Is.EqualTo(BadDelegate));
        Assert.That(ex.OffendingReturnValue, Is.EqualTo(0));
        Assert.That(ex.OffendingFunctionName!.Contains(nameof(BadDelegate)));
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

    [Test]
    public void MissingResourceException_Ctor_test()
    {
        TestOffendingExceptionCtors<MissingResourceException,string>("testId");
    }

    [Test]
    public void StackUnderflowException_Ctor_Test()
    {
        TestExceptionType<EmptyStackException>();
    }

    [Test]
    public void TamperException_Ctor_Test()
    {
        TestExceptionType<TamperException>();
    }
}
