/*
ReflectionHelpersTests.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2025 César Andrés Morgan

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
    [Tag("ReflectionFindTypeTest")]
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
    public void GetMethod_gets_method_overrides()
    {
        MethodInfo? m1 = GetMethod<Test1, Action>(t => t.Test);
        MethodInfo? b1 = typeof(Test1).GetMethod("Test")!;
        MethodInfo? m2 = GetMethod<Test2, Action>(t => t.Test);
        MethodInfo? b2 = typeof(Test2).GetMethod("Test")!;
        Assert.That(m1, Is.Not.Null);
        Assert.That(m2, Is.Not.Null);
        Assert.That(m1, Is.SameAs(b1));
        Assert.That(m2, Is.SameAs(b2));
        Assert.That(m1, Is.Not.SameAs(m2));
        Assert.That(b1, Is.Not.SameAs(b2));
    }

    [Test]
    public void GetMethod_with_direct_method_expression()
    {
        Test1? i = new();
        MethodInfo? m = GetMethod<Func<int>>(() => i.TestInt);
        Assert.That(m, Is.InstanceOf<MethodInfo>());
        Assert.That(m.Name, Is.EqualTo("TestInt"));
    }

    [Test]
    public void GetMethod_with_type_reference_and_method_type()
    {
        MethodInfo? m = GetMethod<Test1, Func<int>>(t => t.TestInt);
        Assert.That(m, Is.InstanceOf<MethodInfo>());
        Assert.That(m.Name, Is.EqualTo("TestInt"));
    }

    [Test]
    public void GetMethod_with_type_reference()
    {
        MethodInfo? m = GetMethod<Test1>(t => t.TestInt);
        Assert.That(m, Is.InstanceOf<MethodInfo>());
        Assert.That(m.Name, Is.EqualTo("TestInt"));
    }

    [Test]
    public void GetCallingMethod_Test()
    {
        MethodBase TestMethod()
        {
            return GetCallingMethod()!;
        }

        Assert.That(MethodBase.GetCurrentMethod(), Is.EqualTo(TestMethod()));
        Assert.That(GetCallingMethod(int.MaxValue - 1), Is.Null);
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

        Assert.That(m1.IsOverridden(t1), Is.False);
        Assert.That(m1.IsOverridden(t2));
        Assert.That(m2.IsOverridden(t2), Is.False);
        Assert.That(m3.IsOverridden(t2));
        Assert.That(m3.IsOverridden(t1), Is.False);

        Assert.Throws<ArgumentNullException>(() => m1.IsOverridden(null!));
        Assert.Throws<ArgumentNullException>(() => MethodBaseExtensions.IsOverridden(null!, null!));
        Assert.Throws<InvalidTypeException>(() => m2.IsOverridden(t1));
    }

    [Test]
    public void IsOverride_Test()
    {
        MethodInfo? m1 = GetMethod<Test1, Action>(t => t.Test);
        MethodInfo? m2 = GetMethod<Test2, Action>(t => t.Test);

        Assert.That(m2.IsOverride());
        Assert.That(m1.IsOverride(), Is.False);
        Assert.Throws<ArgumentNullException>(() => MCART.Types.Extensions.MethodInfoExtensions.IsOverride(null!));
    }

    [Test]
    public void GetEntryPoint_Test()
    {
        Assert.That(GetEntryPoint(), Is.Not.Null);
    }

    [Test]
    public void GetEntryAssembly_Test()
    {
        Assert.That(GetEntryAssembly(), Is.Not.Null);
    }

    [Test]
    public void GetMember_Test()
    {
        MemberInfo? m = GetMember<Test1>(t => t.TestInt());
        Assert.That(m, Is.InstanceOf<MethodInfo>());
        Assert.That("TestInt", Is.EqualTo(m.Name));

        Test1? i = new();
        MemberInfo? n = GetMember(() => i.TestInt());
        Assert.That(n, Is.InstanceOf<MethodInfo>());
        Assert.That("TestInt", Is.EqualTo(n.Name));

        MemberInfo? p = GetMember((System.Linq.Expressions.Expression<Func<object?>>)(() => i.TestInt()));
        Assert.That(p, Is.InstanceOf<MethodInfo>());
        Assert.That("TestInt", Is.EqualTo(p.Name));

        MemberInfo? o = GetMember<Test1, int>(t => t.TestInt());
        Assert.That(o, Is.InstanceOf<MethodInfo>());
        Assert.That("TestInt", Is.EqualTo(o.Name));
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
        Assert.That(m, Is.InstanceOf<FieldInfo>());
        Assert.That("TestField", Is.EqualTo(m.Name));

        Test1? i = new();
        FieldInfo? n = GetField(() => i.TestField);
        Assert.That(n, Is.InstanceOf<FieldInfo>());
        Assert.That("TestField", Is.EqualTo(n.Name));

        FieldInfo? o = GetField<Test1>(t => t.TestField);
        Assert.That(o, Is.InstanceOf<FieldInfo>());
        Assert.That("TestField", Is.EqualTo(o.Name));
    }

    [Test]
    public void GetProperty_Test()
    {
        PropertyInfo? m = GetProperty<Test1, float>(t => t.TestProperty);
        Assert.That(m, Is.InstanceOf<PropertyInfo>());
        Assert.That("TestProperty", Is.EqualTo(m.Name));

        Test1? i = new();
        PropertyInfo? n = GetProperty(() => i.TestProperty);
        Assert.That(n, Is.InstanceOf<PropertyInfo>());
        Assert.That("TestProperty", Is.EqualTo(n.Name));

        PropertyInfo? o = GetProperty<Test1>(t => t.TestProperty);
        Assert.That(o, Is.InstanceOf<PropertyInfo>());
        Assert.That("TestProperty", Is.EqualTo(o.Name));
    }

    [Test]
    public void GetPropertiesOf_Test()
    {
        var c = typeof(Test1).GetPropertiesOf<float>().ToArray();
        Assert.That(1, Is.EqualTo(c.Length));
        Assert.That(c[0], Is.InstanceOf<PropertyInfo>());
        Assert.That("TestProperty", Is.EqualTo(c[0].Name));
    }

    [Test]
    public void GetPropertiesOf_with_BindingFlags_Test()
    {
        var c = typeof(Test1).GetPropertiesOf<float>(BindingFlags.Public | BindingFlags.Instance).ToArray();
        Assert.That(1, Is.EqualTo(c.Length));
        Assert.That(c[0], Is.InstanceOf<PropertyInfo>());
        Assert.That("TestProperty", Is.EqualTo(c[0].Name));
    }

    [Test]
    public void FindTypeTest()
    {
        Assert.That(typeof(TestClass), Is.EqualTo(FindType<ITestInterface>("ReflectionFindTypeTest")));
        Assert.That(typeof(TestClass), Is.EqualTo(FindType("ReflectionFindTypeTest")));
        Assert.That(FindType<ITestInterface>("FindTypeTest2"), Is.Null);
        Assert.That(FindType("FindTypeTest2"), Is.Null);
    }

    [Test]
    public void FindAllObjects_Simple_Test()
    {
        var c = FindAllObjects<ITestInterface>().ToArray();
        Assert.That(c, Is.Not.Empty);
        Assert.That(2, Is.EqualTo(c.Length));
    }

    [Test]
    public void FindAllObjects_With_Type_Filter_Test()
    {
        var c = FindAllObjects<ITestInterface>(p => p.Name.EndsWith('3')).ToArray();
        Assert.That(c, Is.Not.Empty);
        Assert.That(1, Is.EqualTo(c.Length));
    }

    [Test]
    public void FindAllObjects_With_Ctor_Args_Test()
    {
        var c = FindAllObjects<ITestInterface>(new object[] { 0 }).ToArray();
        Assert.That(c, Is.Not.Empty);
        Assert.That(1, Is.EqualTo(c.Length));
    }

    [Test]
    public void FindAllObjects_With_Ctor_Args_And_Filter_Test()
    {
        var c = FindAllObjects<ITestInterface>(new object[] { 0 }, p => p.Name.EndsWith('4')).ToArray();
        Assert.That(c, Is.Not.Empty);
        Assert.That(1, Is.EqualTo(c.Length));
    }

    [Test]
    public void FindFirstObject_Simple_Test()
    {
        var c = FindFirstObject<ITestInterface>();
        Assert.That(c, Is.Not.Null);
    }

    [Test]
    public void FindFirstObject_With_Type_Filter_Test()
    {
        var c = FindFirstObject<ITestInterface>(p => p.Name.EndsWith('3'));
        Assert.That(c, Is.Not.Null);
    }

    [Test]
    public void FindFirstObject_With_Ctor_Args_Test()
    {
        var c = FindFirstObject<ITestInterface>(new object[] { 0 });
        Assert.That(c, Is.Not.Null);
    }

    [Test]
    public void FindFirstObject_With_Ctor_Args_And_Filter_Test()
    {
        var c = FindFirstObject<ITestInterface>(new object[] { 0 }, p => p.Name.EndsWith('4'));
        Assert.That(c, Is.Not.Null);
    }

    [Test]
    public void PublicTypesTest()
    {
        Type[]? t = PublicTypes().ToArray();
        Assert.That(t.Contains(typeof(TestClass)), Is.False);
        Assert.That(t, Contains.Item(typeof(Exception)));
    }

    [Test]
    public void GetTypesTest()
    {
        Assert.That(GetTypes<IComparable>().Count() > 2);
        Assert.That(GetTypes<Stream>(true).Count() > 2);
        Assert.That(GetTypes<Stream>(true).Count() < GetTypes<Stream>(false).Count());
        Assert.That(GetTypes<Enum>(), Contains.Item(typeof(Enum)));
        Assert.That(GetTypes<Enum>(false), Contains.Item(typeof(Enum)));
        Assert.That(GetTypes<Enum>(true), Does.Not.Contain(typeof(Enum)));
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
