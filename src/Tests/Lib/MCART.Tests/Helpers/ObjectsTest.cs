/*
ObjectsTest.cs

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

#pragma warning disable CA1822

using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Events;
using TheXDS.MCART.Exceptions;
using TheXDS.MCART.Helpers;
using TheXDS.MCART.Types.Extensions;
using static TheXDS.MCART.Helpers.Objects;

namespace TheXDS.MCART.Tests.Helpers;

[AttrTest]
public class ObjectsTest
{
    [AttributeUsage(AttributeTargets.Class)]
    private sealed class AttrTestAttribute : Attribute
    {
    }

    private interface ITestInterface
    {
    }

    [ExcludeFromCodeCoverage]
    [Identifier("FindTypeTest")]
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

    [Theory]
    [TestCase(typeof(byte), true)]
    [TestCase(typeof(sbyte), true)]
    [TestCase(typeof(short), true)]
    [TestCase(typeof(ushort), true)]
    [TestCase(typeof(int), true)]
    [TestCase(typeof(uint), true)]
    [TestCase(typeof(long), true)]
    [TestCase(typeof(ulong), true)]
    [TestCase(typeof(decimal), true)]
    [TestCase(typeof(float), true)]
    [TestCase(typeof(double), true)]
    [TestCase(typeof(char), false)]
    [TestCase(typeof(string), false)]
    public void IsNumericTypeTest(Type type, bool result)
    {
        Assert.AreEqual(result, type.IsNumericType());
    }

    [Test]
    public void AreAllNullTest()
    {
        Assert.True(AreAllNull(null, null, null));
        Assert.False(AreAllNull(0, null));
    }

    [Test]
    public void FieldsOfTest()
    {
        TestClass? tc = new();

        Assert.Throws<NullItemException>(() => ReflectionHelpers.FieldsOf<int>(new FieldInfo[] { null! }));
        Assert.Throws<ArgumentNullException>(() => ((FieldInfo[])null!).FieldsOf<int>());
        Assert.Throws<MissingFieldException>(() => ReflectionHelpers.FieldsOf<int>(typeof(TestClass2).GetFields(), tc));

        Assert.AreEqual(tc.TestField, tc.FieldsOf<float>().FirstOrDefault());
        Assert.AreEqual(tc.TestField, ReflectionHelpers.FieldsOf<float>(tc.GetType().GetFields(), tc).FirstOrDefault());
        Assert.AreEqual(TestClass.StaticField, tc.GetType().FieldsOf<double>().FirstOrDefault());
        Assert.AreEqual(TestClass.StaticField, ReflectionHelpers.FieldsOf<double>(tc.GetType().GetFields()).FirstOrDefault());
        Assert.AreEqual(TestClass.StaticField, ReflectionHelpers.FieldsOf<double>(typeof(TestClass).GetFields()).FirstOrDefault());
    }

    
    [Test]
    public void HasAttrTest_Enum()
    {
        Assert.False(TestEnum.Zero.HasAttribute<MCART.Attributes.DescriptionAttribute>());
        Assert.True(TestEnum.One.HasAttribute<MCART.Attributes.DescriptionAttribute>());

        Assert.False(TestEnum.Zero.HasAttribute(out MCART.Attributes.DescriptionAttribute? z));
        Assert.True(TestEnum.One.HasAttribute(out MCART.Attributes.DescriptionAttribute? o));

        Assert.Null(z);
        Assert.IsAssignableFrom<MCART.Attributes.DescriptionAttribute>(o);
        Assert.AreEqual("One", o!.Value);

        Assert.False(((TestEnum)255).HasAttribute<MCART.Attributes.DescriptionAttribute>(out _));
    }

    [Test]
    public void GetAttrTest()
    {
        Assert.NotNull(typeof(MCART.Types.Point).Assembly.GetAttribute<AssemblyTitleAttribute>());
        Assert.NotNull(MethodBase.GetCurrentMethod()?.GetAttribute<TestAttribute>());
        Assert.NotNull(GetAttribute<AttrTestAttribute, ObjectsTest>());
        Assert.NotNull(typeof(ObjectsTest).GetAttribute<AttrTestAttribute>());
    }

    [Test]
    public void HasAttrTest_Assembly()
    {
        Assert.True(typeof(MCART.Types.Point).Assembly.HasAttribute<AssemblyCopyrightAttribute>());
    }

    [Test]
    public void HasAttrTest_Object()
    {
        Assert.False(((object)TestEnum.Zero).HasAttribute(out MCART.Attributes.DescriptionAttribute? z));
        Assert.True(((object)TestEnum.One).HasAttribute(out MCART.Attributes.DescriptionAttribute? o));
        Assert.Null(z);
        Assert.IsAssignableFrom<MCART.Attributes.DescriptionAttribute>(o);
        Assert.AreEqual("One", o!.Value);
        Assert.True(((object)typeof(MCART.Types.Point).Assembly).HasAttribute<AssemblyCopyrightAttribute>());

        Assert.True(((object)MethodBase.GetCurrentMethod()!).HasAttribute<TestAttribute>(out _));

        Assert.True(new TestClass().HasAttribute(out IdentifierAttribute? id));
        Assert.IsAssignableFrom<IdentifierAttribute>(id);
        Assert.AreEqual("FindTypeTest", id!.Value);

        Assert.Throws<ArgumentNullException>(() => ((object)null!).HasAttribute<MCART.Attributes.DescriptionAttribute>(out _));
    }

    [Test]
    public void HasAttrValueTest_Object()
    {
        Assert.True(((object)TestEnum.One).HasAttrValue<MCART.Attributes.DescriptionAttribute, string?>(out string? o));
        Assert.AreEqual("One", o);

        Assert.True(new TestClass().HasAttrValue<IdentifierAttribute, string?>(out string? id));
        Assert.AreEqual("FindTypeTest", id);
    }

    [Test]
    public void IsAnyNullTest()
    {
        Assert.True(IsAnyNull(0, 1, null));
        Assert.False(IsAnyNull(0, 1, 2, 3));
    }

    [Test]
    public void IsEitherTest()
    {
        Type? t = typeof(int);
        Assert.True(t.IsEither(typeof(bool), typeof(int)));
        Assert.False(t.IsEither(typeof(bool), typeof(float)));

        Assert.True(t.IsEither(new HashSet<object> { typeof(bool), typeof(int) }));
        Assert.False(t.IsEither(new HashSet<object> { typeof(bool), typeof(float) }));
    }

    [Test]
    public void IsNeitherTest()
    {
        Type? t = typeof(int);
        Assert.True(t.IsNeither(typeof(bool), typeof(float)));
        Assert.False(t.IsNeither(typeof(bool), typeof(int)));
    }

    [Test]
    public void IsNotTest()
    {
        EventArgs ev = new ExceptionEventArgs(null);
        EventArgs e = new ExceptionEventArgs(null);
        Assert.True(e.IsNot(ev));
    }

    [Test]
    public void IsSignatureCompatibleTest()
    {
        MethodInfo? m = ReflectionHelpers.GetMethod<Action<int>>(() => TestClass.TestMethod)!;
        Assert.True(m.IsSignatureCompatible<Action<int>>());
        Assert.False(m.IsSignatureCompatible<Action<float>>());
    }

    [Test]
    public void IsTest()
    {
        EventArgs? ev = EventArgs.Empty;
        EventArgs? e = ev;
        Assert.True(e.Is(ev));
    }

    [Test]
    public void ItselfTest()
    {
        ApplicationException? ex = new();
        Assert.AreSame(ex, ex.Itself());
        Assert.AreNotSame(ex, new ApplicationException());
        Assert.AreNotSame(ex, null);
    }

    [Test]
    public void PropertiesOfTest()
    {
        TestClass? tc = new();
        Assert.AreEqual(tc.TestProperty, tc.PropertiesOf<int>().FirstOrDefault());
        Assert.AreEqual(tc.TestProperty, tc.GetType().GetProperties().PropertiesOf<int>(tc).FirstOrDefault());
        Assert.AreEqual(TestClass.ByteProperty, tc.GetType().GetProperties().PropertiesOf<byte>().FirstOrDefault());
    }

    [Test]
    public void ToTypesTest()
    {
        Type[]? x = ToTypes(1, "Test", 2.5f).ToArray();
        System.Collections.IEnumerator? y = x.GetEnumerator();
        y.Reset();
        y.MoveNext();
        Assert.AreSame(typeof(int), y.Current);
        y.MoveNext();
        Assert.AreSame(typeof(string), y.Current);
        y.MoveNext();
        Assert.AreSame(typeof(float), y.Current);
    }

    [Test]
    public void WhichAreNullTest()
    {
        Assert.NotNull(Array.Empty<object>().WhichAreNull());
        Assert.AreEqual(Array.Empty<int>(), WhichAreNull(new object(), new object()).ToArray());
        Assert.AreEqual(new[] { 1 }, WhichAreNull(new object(), null, new object(), new object()).ToArray());
        Assert.AreEqual(new[] { 2, 3 }, WhichAreNull(new object(), new object(), null, null).ToArray());
        Assert.Throws<ArgumentNullException>(() => ((IEnumerable<object?>)null!).WhichAreNull().ToArray());
    }

    [Test]
    public void WhichAreTest()
    {
        object? x = new();
        Assert.AreEqual(Array.Empty<int>(), x.WhichAre(new object(), 1, 0.0f).ToArray());
        Assert.AreEqual(new[] { 2 }, x.WhichAre(new object(), 1, x).ToArray());
        Assert.AreEqual(new[] { 1, 3 }, x.WhichAre(new object(), x, 0, x).ToArray());
    }

    [Test]
    public void WithSignatureTest()
    {
        Assert.Null(typeof(TestClass).GetMethods().WithSignature<Action<short>>().FirstOrDefault());
        Action<int>? m = typeof(TestClass).GetMethods().WithSignature<Action<int>>().FirstOrDefault()!;
        Assert.NotNull(m);
        m(1);
    }

    [Test]
    public void WithSignatureTest_object()
    {
        TestClass? tc = new();
        Assert.Null(typeof(TestClass).GetMethods().WithSignature<Action<double>>(tc).FirstOrDefault());
        Action<float>? m = typeof(TestClass).GetMethods().WithSignature<Action<float>>(tc).FirstOrDefault()!;
        Assert.NotNull(m);
        m(1.0f);
    }

    [Test]
    public void TryCreateDelegateTest()
    {
        MethodInfo? m = GetType().GetMethod(nameof(TestEventHandler))!;

        Assert.True(TryCreateDelegate<EventHandler>(m, this, out _));
        Assert.False(TryCreateDelegate<EventHandler>(m, null!, out _));
        Assert.False(TryCreateDelegate<Action>(m, this, out _));
        Assert.False(TryCreateDelegate<Action<int>>(m, this, out _));
        Assert.False(TryCreateDelegate<EventHandler>(null!, out _));
        Assert.False(TryCreateDelegate<Action>(m, out _));
        Assert.False(TryCreateDelegate<Action<int>>(m, out _));
    }

    [Test]
    public void FromBytes_Test()
    {
        Assert.AreEqual(1000000, FromBytes<int>(new byte[] { 64, 66, 15, 0 }));
        Assert.AreEqual(123456.789m, FromBytes<decimal>(new byte[] { 0, 0, 3, 0, 0, 0, 0, 0, 21, 205, 91, 7, 0, 0, 0, 0 }));
    }

    [Test]
    public void GetBytes_Test()
    {
        Assert.AreEqual(new byte[] { 64, 66, 15, 0 }, GetBytes(1000000));
        Assert.AreEqual(new byte[] { 0, 0, 3, 0, 0, 0, 0, 0, 21, 205, 91, 7, 0, 0, 0, 0 }, GetBytes(123456.789m));
    }
}
