/*
ObjectsTest.cs

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

#pragma warning disable CA1822

using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Events;
using TheXDS.MCART.Exceptions;
using TheXDS.MCART.Helpers;
using TheXDS.MCART.Resources;
using TheXDS.MCART.Types.Extensions;
using static TheXDS.MCART.Helpers.Objects;

namespace TheXDS.MCART.Tests.Helpers
{
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

        [Theory]
        [CLSCompliant(false)]
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
        public void FindTypeTest()
        {
            Assert.AreEqual(typeof(TestClass), FindType<ITestInterface>("FindTypeTest"));
            Assert.AreEqual(typeof(TestClass), FindType("FindTypeTest"));
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
            var c = FindAllObjects<ITestInterface>(new object[]{ 0 }).ToArray();
            Assert.IsNotEmpty(c);
            Assert.AreEqual(1, c.Length);
        }
        
        [Test]
        public void FindAllObjects_With_Ctor_Args_And_Filter_Test()
        {
            var c = FindAllObjects<ITestInterface>(new object[]{ 0 }, p => p.Name.EndsWith("4")).ToArray();
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
            var c = FindFirstObject<ITestInterface>(new object[]{ 0 });
            Assert.IsNotNull(c);
        }
        
        [Test]
        public void FindFirstObject_With_Ctor_Args_And_Filter_Test()
        {
            var c = FindFirstObject<ITestInterface>(new object[]{ 0 }, p => p.Name.EndsWith("4"));
            Assert.IsNotNull(c);
        }
        
        [Test]
        public void HasAttrTest_Enum()
        {
            Assert.False(TestEnum.Zero.HasAttr<MCART.Attributes.DescriptionAttribute>());
            Assert.True(TestEnum.One.HasAttr<MCART.Attributes.DescriptionAttribute>());

            Assert.False(TestEnum.Zero.HasAttr<MCART.Attributes.DescriptionAttribute>(out MCART.Attributes.DescriptionAttribute? z));
            Assert.True(TestEnum.One.HasAttr<MCART.Attributes.DescriptionAttribute>(out MCART.Attributes.DescriptionAttribute? o));

            Assert.Null(z);
            Assert.IsAssignableFrom<MCART.Attributes.DescriptionAttribute>(o);
            Assert.AreEqual("One", o!.Value);

#if !CLSCompliance && PreferExceptions
            Assert.Throws<ArgumentOutOfRangeException>(() => ((TestEnum) 255).HasAttr<TheXDS.MCART.Attributes.DescriptionAttribute>(out _));
#else
            Assert.False(((TestEnum)255).HasAttr<MCART.Attributes.DescriptionAttribute>(out _));
#endif
        }

        [Test]
        public void GetAttrTest()
        {
            Assert.NotNull(RtInfo.CoreRtAssembly.GetAttr<AssemblyTitleAttribute>());
            Assert.NotNull(MethodBase.GetCurrentMethod()?.GetAttr<TestAttribute>());
            Assert.NotNull(GetAttr<AttrTestAttribute, ObjectsTest>());
            Assert.NotNull(typeof(ObjectsTest).GetAttr<AttrTestAttribute>());
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

        [Test]
        public void HasAttrTest_Assembly()
        {
            Assert.True(RtInfo.CoreRtAssembly.HasAttr<AssemblyCopyrightAttribute>());
        }

        [Test]
        public void HasAttrTest_Object()
        {
            Assert.False(((object)TestEnum.Zero).HasAttr<MCART.Attributes.DescriptionAttribute>(out MCART.Attributes.DescriptionAttribute? z));
            Assert.True(((object)TestEnum.One).HasAttr<MCART.Attributes.DescriptionAttribute>(out MCART.Attributes.DescriptionAttribute? o));
            Assert.Null(z);
            Assert.IsAssignableFrom<MCART.Attributes.DescriptionAttribute>(o);
            Assert.AreEqual("One", o!.Value);
            Assert.True(((object)RtInfo.CoreRtAssembly).HasAttr<AssemblyCopyrightAttribute>());

            Assert.True(((object)MethodBase.GetCurrentMethod()!).HasAttr<TestAttribute>(out _));

            Assert.True(new TestClass().HasAttr<IdentifierAttribute>(out IdentifierAttribute? id));
            Assert.IsAssignableFrom<IdentifierAttribute>(id);
            Assert.AreEqual("FindTypeTest", id!.Value);

            Assert.Throws<ArgumentNullException>(() => ((object)null!).HasAttr<MCART.Attributes.DescriptionAttribute>(out _));
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
        public void PublicTypesTest()
        {
            Type[]? t = PublicTypes().ToArray();
            Assert.False(t.Contains(typeof(TestClass)));
            Assert.Contains(typeof(Exception), t);
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
}