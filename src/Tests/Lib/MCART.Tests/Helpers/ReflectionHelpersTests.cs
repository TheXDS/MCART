/*
ReflectionHelpersTests.cs

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

#pragma warning disable IDE0061
#pragma warning disable IDE0062
#pragma warning disable CA1822

namespace TheXDS.MCART.Tests.Helpers;

using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using NUnit.Framework;
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Exceptions;
using TheXDS.MCART.Types.Extensions;
using static TheXDS.MCART.Helpers.ReflectionHelpers;

public class ReflectionHelpersTests
{
    [AttributeUsage(AttributeTargets.Class)]
    private sealed class AttrTestAttribute : Attribute
    {
    }

    private interface ITestInterface
    {
    }

    [ExcludeFromCodeCoverage]
    [Identifier("ReflectionFindTypeTest")]
    private class TestClass : ITestInterface
    {
        public static readonly double StaticField = 2;

        public readonly float TestField = 1.0f;

        public static byte ByteProperty { get; } = 2;

        public int TestProperty { get; } = 1;

        public static void TestMethod(int x)
        {
            _ = x.ToString();
        }

        public void TestMethod2(float x)
        {
            _ = x.ToString();
        }
    }

    [ExcludeFromCodeCoverage]
    private class TestClass3 : ITestInterface
    {
    }

    [ExcludeFromCodeCoverage]
    private class TestClass2
    {
        public int TestField = 2;
    }

    [ExcludeFromCodeCoverage]
    private class TestClass4 : ITestInterface
    {
        public TestClass4(int value)
        {
            _ = value;
        }
    }

    private enum TestEnum : byte
    {
        Zero,
        [MCART.Attributes.Description("One")] One,
        Two
    }

    [ExcludeFromCodeCoverage]
    public void TestEventHandler(object sender, EventArgs e) { }

    [Test]
    public void GetMethod_Test()
    {
        MethodInfo? m1 = GetMethod<Test1, Action>(t => t.Test);
        MethodInfo? b1 = typeof(Test1).GetMethod("Test")!;
        MethodInfo? m2 = GetMethod<Test2, Action>(t => t.Test);
        MethodInfo? b2 = typeof(Test2).GetMethod("Test")!;

        Assert.NotNull(m1);
        Assert.NotNull(m2);
        Assert.AreSame(m1, b1);
        Assert.AreSame(m2, b2);
        Assert.AreNotSame(m1, m2);
        Assert.AreNotSame(b1, b2);

        Test1? i = new();
        MethodInfo? n = GetMethod<Func<int>>(() => i.TestInt);
        Assert.IsInstanceOf<MethodInfo>(n);
        Assert.AreEqual("TestInt", n.Name);

        MethodInfo? o = GetMethod<Test1, Func<int>>(t => t.TestInt);
        Assert.IsInstanceOf<MethodInfo>(o);
        Assert.AreEqual("TestInt", o.Name);

        MethodInfo? m = GetMethod<Test1>(t => (Func<int>)t.TestInt);
        Assert.IsInstanceOf<MethodInfo>(m);
        Assert.AreEqual("TestInt", m.Name);
    }

    [Test]
    public void GetCallingMethod_Test()
    {
        MethodBase TestMethod()
        {
            return GetCallingMethod()!;
        }

        Assert.AreEqual(MethodBase.GetCurrentMethod(), TestMethod());
        Assert.Null(GetCallingMethod(int.MaxValue - 1));
        Assert.Throws<OverflowException>(() => GetCallingMethod(int.MaxValue));
        Assert.Throws<ArgumentOutOfRangeException>(() => GetCallingMethod(0));
    }

    [Test]
    public void IsOverridden_Test()
    {
        Test1? t1 = new();
        Test2? t2 = new();
        MethodInfo? m1 = GetMethod<Test1, Action>(t => t.Test);
        MethodInfo? m2 = GetMethod<Test2, Action>(t => t.Test);
        MethodInfo? m3 = GetMethod<Test1, Action>(t => t.TestC<int>);

        Assert.False(m1.IsOverridden(t1));
        Assert.True(m1.IsOverridden(t2));
        Assert.False(m2.IsOverridden(t2));
        Assert.True(m3.IsOverridden(t2));
        Assert.False(m3.IsOverridden(t1));

        Assert.Throws<ArgumentNullException>(() => m1.IsOverridden(null!));
        Assert.Throws<ArgumentNullException>(() => MethodBaseExtensions.IsOverridden(null!, null!));
        Assert.Throws<InvalidTypeException>(() => m2.IsOverridden(t1));
    }

    [Test]
    public void IsOverride_Test()
    {
        MethodInfo? m1 = GetMethod<Test1, Action>(t => t.Test);
        MethodInfo? m2 = GetMethod<Test2, Action>(t => t.Test);

        Assert.True(m2.IsOverride());
        Assert.False(m1.IsOverride());
        Assert.Throws<ArgumentNullException>(() => TheXDS.MCART.Types.Extensions.MethodInfoExtensions.IsOverride(null!));
    }

    [Test]
    public void GetEntryPoint_Test()
    {
        Assert.NotNull(GetEntryPoint());
    }

    [Test]
    public void GetEntryAssembly_Test()
    {
        Assert.NotNull(GetEntryAssembly());
    }

    [Test]
    public void GetMember_Test()
    {
        MemberInfo? m = GetMember<Test1>(t => t.TestInt());
        Assert.IsInstanceOf<MethodInfo>(m);
        Assert.AreEqual("TestInt", m.Name);

        Test1? i = new();
        MemberInfo? n = GetMember(() => i.TestInt());
        Assert.IsInstanceOf<MethodInfo>(n);
        Assert.AreEqual("TestInt", n.Name);

        MemberInfo? p = GetMember((System.Linq.Expressions.Expression<Func<object?>>)(() => i.TestInt()));
        Assert.IsInstanceOf<MethodInfo>(p);
        Assert.AreEqual("TestInt", p.Name);

        MemberInfo? o = GetMember<Test1, int>(t => t.TestInt());
        Assert.IsInstanceOf<MethodInfo>(o);
        Assert.AreEqual("TestInt", o.Name);
    }

    [Test]
    public void GetMember_Contract_Test()
    {
        Assert.Throws<InvalidOperationException>(() => GetMember<Test1>(t => t.TestInt() + 2));
    }

    [Test]
    public void GetField_Test()
    {
        FieldInfo? m = GetField<Test1, string>(t => t.TestField);
        Assert.IsInstanceOf<FieldInfo>(m);
        Assert.AreEqual("TestField", m.Name);

        Test1? i = new();
        FieldInfo? n = GetField(() => i.TestField);
        Assert.IsInstanceOf<FieldInfo>(n);
        Assert.AreEqual("TestField", n.Name);

        FieldInfo? o = GetField<Test1>(t => t.TestField);
        Assert.IsInstanceOf<FieldInfo>(o);
        Assert.AreEqual("TestField", o.Name);
    }

    [Test]
    public void GetProperty_Test()
    {
        PropertyInfo? m = GetProperty<Test1, float>(t => t.TestProperty);
        Assert.IsInstanceOf<PropertyInfo>(m);
        Assert.AreEqual("TestProperty", m.Name);

        Test1? i = new();
        PropertyInfo? n = GetProperty(() => i.TestProperty);
        Assert.IsInstanceOf<PropertyInfo>(n);
        Assert.AreEqual("TestProperty", n.Name);

        PropertyInfo? o = GetProperty<Test1>(t => t.TestProperty);
        Assert.IsInstanceOf<PropertyInfo>(o);
        Assert.AreEqual("TestProperty", o.Name);
    }

    [Test]
    public void GetPropertiesOf_Test()
    {
        var c = typeof(Test1).GetPropertiesOf<float>().ToArray();
        Assert.AreEqual(1, c.Length);
        Assert.IsInstanceOf<PropertyInfo>(c[0]);
        Assert.AreEqual("TestProperty", c[0].Name);
    }

    [Test]
    public void GetPropertiesOf_with_BindingFlags_Test()
    {
        var c = typeof(Test1).GetPropertiesOf<float>(BindingFlags.Public | BindingFlags.Instance).ToArray();
        Assert.AreEqual(1, c.Length);
        Assert.IsInstanceOf<PropertyInfo>(c[0]);
        Assert.AreEqual("TestProperty", c[0].Name);
    }

    [Test]
    public void FindTypeTest()
    {
        Assert.AreEqual(typeof(TestClass), FindType<ITestInterface>("ReflectionFindTypeTest"));
        Assert.AreEqual(typeof(TestClass), FindType("ReflectionFindTypeTest"));
        Assert.Null(FindType<ITestInterface>("FindTypeTest2"));
        Assert.Null(FindType("FindTypeTest2"));
    }

    [Test]
    public void FindAllObjects_Simple_Test()
    {
        var c = FindAllObjects<ITestInterface>().ToArray();
        Assert.IsNotEmpty(c);
        Assert.AreEqual(2, c.Length);
    }

    [Test]
    public void FindAllObjects_With_Type_Filter_Test()
    {
        var c = FindAllObjects<ITestInterface>(p => p.Name.EndsWith("3")).ToArray();
        Assert.IsNotEmpty(c);
        Assert.AreEqual(1, c.Length);
    }

    [Test]
    public void FindAllObjects_With_Ctor_Args_Test()
    {
        var c = FindAllObjects<ITestInterface>(new object[] { 0 }).ToArray();
        Assert.IsNotEmpty(c);
        Assert.AreEqual(1, c.Length);
    }

    [Test]
    public void FindAllObjects_With_Ctor_Args_And_Filter_Test()
    {
        var c = FindAllObjects<ITestInterface>(new object[] { 0 }, p => p.Name.EndsWith("4")).ToArray();
        Assert.IsNotEmpty(c);
        Assert.AreEqual(1, c.Length);
    }

    [Test]
    public void FindFirstObject_Simple_Test()
    {
        var c = FindFirstObject<ITestInterface>();
        Assert.IsNotNull(c);
    }

    [Test]
    public void FindFirstObject_With_Type_Filter_Test()
    {
        var c = FindFirstObject<ITestInterface>(p => p.Name.EndsWith("3"));
        Assert.IsNotNull(c);
    }

    [Test]
    public void FindFirstObject_With_Ctor_Args_Test()
    {
        var c = FindFirstObject<ITestInterface>(new object[] { 0 });
        Assert.IsNotNull(c);
    }

    [Test]
    public void FindFirstObject_With_Ctor_Args_And_Filter_Test()
    {
        var c = FindFirstObject<ITestInterface>(new object[] { 0 }, p => p.Name.EndsWith("4"));
        Assert.IsNotNull(c);
    }

    [Test]
    public void PublicTypesTest()
    {
        Type[]? t = PublicTypes().ToArray();
        Assert.False(t.Contains(typeof(TestClass)));
        Assert.Contains(typeof(Exception), t);
    }

    [Test]
    public void GetTypesTest()
    {
        Assert.True(GetTypes<IComparable>().Count() > 2);
        Assert.True(GetTypes<Stream>(true).Count() > 2);
        Assert.True(GetTypes<Stream>(true).Count() < GetTypes<Stream>(false).Count());
        Assert.Contains(typeof(Enum), GetTypes<Enum>().ToArray());
        Assert.Contains(typeof(Enum), GetTypes<Enum>(false).ToArray());
        Assert.False(GetTypes<Enum>(true).Contains(typeof(Enum)));
    }

    [ExcludeFromCodeCoverage]
    public class Test1
    {
        public virtual void Test() { }

        public virtual void TestC<T>() { }
        public int TestInt() => default;

        public string TestField = "Test";

        public float TestProperty { get; } = 1.5f;
    }

    [ExcludeFromCodeCoverage]
    public class Test2 : Test1
    {
        public override void Test() { }

        public void TestB() { }

        public override void TestC<T>() { }
    }
}
