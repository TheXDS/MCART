/*
ObjectsTest.cs

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

#pragma warning disable IDE0079
#pragma warning disable CA1822
#pragma warning disable IDE0044
#pragma warning disable IDE0051
#pragma warning disable CS0414

using System.Diagnostics.CodeAnalysis;
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
    [Tag("FindTypeTest")]
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

    [ExcludeFromCodeCoverage]
    private class TestClass5
    {
        private int PrivateIntField = 1;
        private readonly float PrivateFloatField = 1.5f;

        public int TestIntField = 3;
        public readonly double TestDoubleField = 4.0;

        public short ShortProperty { get; set; } = 4000;

        public byte ByteProperty => (byte)(ShortProperty % 128);
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
        Assert.That(result, Is.EqualTo(type.IsNumericType()));
    }

    [Test]
    public void AreAllNullTest()
    {
        Assert.That(AreAllNull(null, null, null));
        Assert.That(AreAllNull(0, null), Is.False);
    }

    [Test]
    public void FieldsOfTest()
    {
        TestClass? tc = new();

        Assert.Throws<NullItemException>(() => ReflectionHelpers.FieldsOf<int>([null!]));
        Assert.Throws<ArgumentNullException>(() => ((FieldInfo[])null!).FieldsOf<int>());

        Assert.That(tc.TestField, Is.EqualTo(tc.FieldsOf<float>().FirstOrDefault()));
        Assert.That(tc.TestField, Is.EqualTo(ReflectionHelpers.FieldsOf<float>(tc.GetType().GetFields(), tc).FirstOrDefault()));
        Assert.That(TestClass.StaticField, Is.EqualTo(tc.GetType().FieldsOf<double>().FirstOrDefault()));
        Assert.That(TestClass.StaticField, Is.EqualTo(ReflectionHelpers.FieldsOf<double>(tc.GetType().GetFields()).FirstOrDefault()));
        Assert.That(TestClass.StaticField, Is.EqualTo(ReflectionHelpers.FieldsOf<double>(typeof(TestClass).GetFields()).FirstOrDefault()));
    }
    
    [Test]
    public void HasAttrTest_Enum()
    {
        Assert.That(TestEnum.Zero.HasAttribute<MCART.Attributes.DescriptionAttribute>(), Is.False);
        Assert.That(TestEnum.One.HasAttribute<MCART.Attributes.DescriptionAttribute>());

        Assert.That(TestEnum.Zero.HasAttribute(out MCART.Attributes.DescriptionAttribute? z), Is.False);
        Assert.That(TestEnum.One.HasAttribute(out MCART.Attributes.DescriptionAttribute? o));

        Assert.That(z, Is.Null);
        Assert.That(o,Is.AssignableFrom<MCART.Attributes.DescriptionAttribute>());
        Assert.That("One", Is.EqualTo(o!.Value));

        Assert.That(((TestEnum)255).HasAttribute<MCART.Attributes.DescriptionAttribute>(out _), Is.False);
    }

    [Test]
    public void GetAttrTest()
    {
        Assert.That(typeof(MCART.Types.Point).Assembly.GetAttribute<AssemblyTitleAttribute>(), Is.Not.Null);
        Assert.That(MethodBase.GetCurrentMethod()?.GetAttribute<TestAttribute>(), Is.Not.Null);
        Assert.That(GetAttribute<AttrTestAttribute, ObjectsTest>(), Is.Not.Null);
        Assert.That(typeof(ObjectsTest).GetAttribute<AttrTestAttribute>(), Is.Not.Null);
    }

    [Test]
    public void HasAttrTest_Assembly()
    {
        Assert.That(typeof(MCART.Types.Point).Assembly.HasAttribute<AssemblyCopyrightAttribute>());
    }

    [Test]
    public void HasAttrTest_Object()
    {
        Assert.That(((object)TestEnum.Zero).HasAttribute(out MCART.Attributes.DescriptionAttribute? z), Is.False);
        Assert.That(((object)TestEnum.One).HasAttribute(out MCART.Attributes.DescriptionAttribute? o));
        Assert.That(z, Is.Null);
        Assert.That(o, Is.AssignableFrom<MCART.Attributes.DescriptionAttribute>());
        Assert.That("One", Is.EqualTo(o!.Value));
        Assert.That(((object)typeof(MCART.Types.Point).Assembly).HasAttribute<AssemblyCopyrightAttribute>());

        Assert.That(((object)MethodBase.GetCurrentMethod()!).HasAttribute<TestAttribute>(out _));

        Assert.That(new TestClass().HasAttribute(out TagAttribute? id));
        Assert.That(id, Is.AssignableFrom<TagAttribute>());
        Assert.That("FindTypeTest", Is.EqualTo(id!.Value));

        Assert.Throws<ArgumentNullException>(() => ((object)null!).HasAttribute<MCART.Attributes.DescriptionAttribute>(out _));
    }

    [Test]
    public void HasAttrValueTest_Object()
    {
        Assert.That(((object)TestEnum.One).HasAttrValue<MCART.Attributes.DescriptionAttribute, string?>(out string? o));
        Assert.That("One", Is.EqualTo(o));

        Assert.That(new TestClass().HasAttrValue<TagAttribute, string?>(out string? id));
        Assert.That("FindTypeTest", Is.EqualTo(id));
    }

    [Test]
    public void IsAnyNullTest()
    {
        Assert.That(IsAnyNull(0, 1, null));
        Assert.That(IsAnyNull(0, 1, 2, 3), Is.False);
    }

    [Test]
    public void IsEitherTest()
    {
        Type? t = typeof(int);
        Assert.That(t.IsEither(typeof(bool), typeof(int)));
        Assert.That(t.IsEither(typeof(bool), typeof(float)), Is.False);

        Assert.That(t.IsEither(new HashSet<object> { typeof(bool), typeof(int) }));
        Assert.That(t.IsEither(new HashSet<object> { typeof(bool), typeof(float) }), Is.False);
    }

    [Test]
    public void IsNeitherTest()
    {
        Type? t = typeof(int);
        Assert.That(t.IsNeither(typeof(bool), typeof(float)));
        Assert.That(t.IsNeither(typeof(bool), typeof(int)), Is.False);
    }

    [Test]
    public void IsNotTest()
    {
        EventArgs ev = new ExceptionEventArgs(null);
        EventArgs e = new ExceptionEventArgs(null);
        Assert.That(e.IsNot(ev));
    }

    [Test]
    public void IsSignatureCompatibleTest()
    {
        MethodInfo? m = ReflectionHelpers.GetMethod<Action<int>>(() => TestClass.TestMethod)!;
        Assert.That(m.IsSignatureCompatible<Action<int>>());
        Assert.That(m.IsSignatureCompatible<Action<float>>(), Is.False);
    }

    [Test]
    public void IsTest()
    {
        EventArgs? ev = EventArgs.Empty;
        EventArgs? e = ev;
        Assert.That(e.Is(ev));
    }

    [Test]
    public void ItselfTest()
    {
        ApplicationException? ex = new();
        Assert.That(ex.Itself(), Is.Not.Null);
        Assert.That(ex.Itself(), Is.SameAs(ex));
        Assert.That(ex.Itself(), Is.Not.SameAs(new ApplicationException()));
    }

    [Test]
    public void PropertiesOfTest()
    {
        TestClass? tc = new();
        Assert.That(tc.TestProperty, Is.EqualTo(tc.PropertiesOf<int>().FirstOrDefault()));
        Assert.That(tc.TestProperty, Is.EqualTo(tc.GetType().GetProperties().PropertiesOf<int>(tc).FirstOrDefault()));
        Assert.That(TestClass.ByteProperty, Is.EqualTo(tc.GetType().GetProperties().PropertiesOf<byte>().FirstOrDefault()));
    }

    [Test]
    public void ShallowCloneTest()
    {
        TestClass5 source = new()
        {
            TestIntField = 99,
            ShortProperty = 9000,
        };
        TestClass5 clone = source.ShallowClone();

        Assert.That(source, Is.Not.SameAs(clone));
        Assert.That(source.TestIntField, Is.EqualTo(clone.TestIntField));
        Assert.That(source.ShortProperty, Is.EqualTo(clone.ShortProperty));
    }

    [Test]
    public void ShallowCopyToTest()
    {
        TestClass5 obj1 = new()
        {
            TestIntField = 99,
            ShortProperty = 9000,
        };
        TestClass5 obj2 = new();
        Assert.That(obj1.TestIntField, Is.Not.EqualTo(obj2.TestIntField));
        Assert.That(obj1.ShortProperty, Is.Not.EqualTo(obj2.ShortProperty));
        obj1.ShallowCopyTo(obj2);
        Assert.That(obj1, Is.Not.SameAs(obj2));
        Assert.That(obj1.TestIntField, Is.EqualTo(obj2.TestIntField));
        Assert.That(obj1.ShortProperty, Is.EqualTo(obj2.ShortProperty));
    }

    [Test]
    public void ShallowCopyTo_copies_with_specified_type()
    {
        TestClass5 obj1 = new()
        {
            TestIntField = 99,
            ShortProperty = 9000,
        };
        TestClass5 obj2 = new();
        Assert.That(obj1.TestIntField, Is.Not.EqualTo(obj2.TestIntField));
        Assert.That(obj1.ShortProperty, Is.Not.EqualTo(obj2.ShortProperty));
        obj1.ShallowCopyTo(obj2, typeof(TestClass5));
        Assert.That(obj1, Is.Not.SameAs(obj2));
        Assert.That(obj1.TestIntField, Is.EqualTo(obj2.TestIntField));
        Assert.That(obj1.ShortProperty, Is.EqualTo(obj2.ShortProperty));
    }

    [Test]
    public void ShallowCopyTo_with_specific_type_throws_if_origin_type_does_not_match_copy_type()
    {
        Random obj1 = new();
        TestClass5 obj2 = new();
        Assert.That(() => obj1.ShallowCopyTo(obj2, typeof(TestClass5)), Throws.ArgumentException);
    }
    [Test]
    public void ShallowCopyTo_with_specific_type_throws_if_Destination_type_does_not_match_copy_type()
    {
        TestClass5 obj1 = new();
        Random obj2 = new();
        Assert.That(() => obj1.ShallowCopyTo(obj2, typeof(TestClass5)), Throws.ArgumentException);
    }

    [Test]
    public void ShallowCopyTo_throws_on_null_parameters()
    {
        Assert.That(() => Objects.ShallowCopyTo(null!, new TestClass5()), Throws.ArgumentNullException);
        Assert.That(() => new TestClass5().ShallowCopyTo(null!), Throws.ArgumentNullException);
    }

    [Test]
    public void ToTypesTest()
    {
        Type[]? x = [.. ToTypes(1, "Test", 2.5f)];
        System.Collections.IEnumerator? y = x.GetEnumerator();
        y.Reset();
        y.MoveNext();
        Assert.That(typeof(int), Is.SameAs(y.Current));
        y.MoveNext();
        Assert.That(typeof(string), Is.SameAs(y.Current));
        y.MoveNext();
        Assert.That(typeof(float), Is.SameAs(y.Current));
    }

    [Test]
    public void WhichAreNullTest()
    {
        Assert.That(Array.Empty<object>().WhichAreNull(), Is.Not.Null);
        Assert.That(Array.Empty<object>().WhichAreNull(), Is.Empty);
        Assert.That(WhichAreNull(new object(), new object()), Is.Not.Null);
        Assert.That(WhichAreNull(new object(), new object()), Is.Empty);
        Assert.That(WhichAreNull(new object(), null, new object(), new object()), Is.EquivalentTo([1]));
        Assert.That(WhichAreNull(new object(), new object(), null, null), Is.EquivalentTo([2, 3]));
        Assert.Throws<ArgumentNullException>(() => ((IEnumerable<object?>)null!).WhichAreNull().ToArray());
    }

    [Test]
    public void WhichAreTest()
    {
        object? x = new();
        Assert.That(x.WhichAre(new object(), 1, 0.0f), Is.Empty);
        Assert.That(x.WhichAre(new object(), 1, x), Is.EquivalentTo([2]));
        Assert.That(x.WhichAre(new object(), x, 0, x), Is.EquivalentTo([1, 3]));
    }

    [Test]
    public void WithSignatureTest()
    {
        Assert.That(typeof(TestClass).GetMethods().WithSignature<Action<short>>().FirstOrDefault(), Is.Null);
        Action<int>? m = typeof(TestClass).GetMethods().WithSignature<Action<int>>().FirstOrDefault()!;
        Assert.That(m, Is.Not.Null);
        Assert.That(() => m(1), Throws.Nothing);
    }

    [Test]
    public void WithSignatureTest_object()
    {
        TestClass? tc = new();
        Assert.That(typeof(TestClass).GetMethods().WithSignature<Action<double>>(tc).FirstOrDefault(), Is.Null);
        Action<float>? m = typeof(TestClass).GetMethods().WithSignature<Action<float>>(tc).FirstOrDefault()!;
        Assert.That(m, Is.Not.Null);
        Assert.That(() => m(1.0f), Throws.Nothing);
    }

    [Test]
    public void TryCreateDelegateTest()
    {
        MethodInfo? m = GetType().GetMethod(nameof(TestEventHandler))!;

        Assert.That(TryCreateDelegate<EventHandler>(m, this, out _));
        Assert.That(TryCreateDelegate<EventHandler>(m, null!, out _), Is.False);
        Assert.That(TryCreateDelegate<Action>(m, this, out _), Is.False);
        Assert.That(TryCreateDelegate<Action<int>>(m, this, out _), Is.False);
        Assert.That(TryCreateDelegate<EventHandler>(null!, out _), Is.False);
        Assert.That(TryCreateDelegate<Action>(m, out _), Is.False);
        Assert.That(TryCreateDelegate<Action<int>>(m, out _), Is.False);
    }

    [Test]
    public void FromBytes_Test()
    {
        Assert.That(1000000, Is.EqualTo(FromBytes<int>([64, 66, 15, 0])));
        Assert.That(123456.789m, Is.EqualTo(FromBytes<decimal>([0, 0, 3, 0, 0, 0, 0, 0, 21, 205, 91, 7, 0, 0, 0, 0])));
    }

    [Test]
    public void GetBytes_Test()
    {
        Assert.That(new byte[] { 64, 66, 15, 0 }, Is.EqualTo(GetBytes(1000000)));
        Assert.That(new byte[] { 0, 0, 3, 0, 0, 0, 0, 0, 21, 205, 91, 7, 0, 0, 0, 0 }, Is.EqualTo(GetBytes(123456.789m)));
    }
}
