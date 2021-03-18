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

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Events;
using TheXDS.MCART.Helpers;
using TheXDS.MCART.Resources;
using Xunit;
using static TheXDS.MCART.Helpers.Objects;

namespace TheXDS.MCART.Tests.Modules
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

#pragma warning disable CA1822
            public void TestMethod2(float x)
#pragma warning restore CA1822
            {
                _ = x.ToString();
            }
        }
#pragma warning disable xUnit1013
        public void TestEventHandler(object sender, EventArgs e) { }
#pragma warning restore xUnit1013

        [Theory]
        [CLSCompliant(false)]
        [InlineData(typeof(byte), true)]
        [InlineData(typeof(sbyte), true)]
        [InlineData(typeof(short), true)]
        [InlineData(typeof(ushort), true)]
        [InlineData(typeof(int), true)]
        [InlineData(typeof(uint), true)]
        [InlineData(typeof(long), true)]
        [InlineData(typeof(ulong), true)]
        [InlineData(typeof(decimal), true)]
        [InlineData(typeof(float), true)]
        [InlineData(typeof(double), true)]
        [InlineData(typeof(char), false)]
        [InlineData(typeof(string), false)]
        public void IsNumericTypeTest(Type type, bool result)
        {
            Assert.Equal(result, IsNumericType(type));
        }

        [Fact]
        public void AreAllNullTest()
        {
            Assert.True(AreAllNull(null, null, null));
            Assert.False(AreAllNull(0, null));
        }

        [Fact]
        public void FieldsOfTest()
        {
            var tc = new TestClass();
            Assert.Equal(tc.TestField, tc.FieldsOf<float>().FirstOrDefault());
            Assert.Equal(tc.TestField, tc.GetType().GetFields().FieldsOf<float>(tc).FirstOrDefault());
            Assert.Equal(TestClass.StaticField, tc.GetType().FieldsOf<double>().FirstOrDefault());
            Assert.Equal(TestClass.StaticField, tc.GetType().GetFields().FieldsOf<double>().FirstOrDefault());
            Assert.Equal(TestClass.StaticField, typeof(TestClass).GetFields().FieldsOf<double>().FirstOrDefault());
        }

        [Fact]
        public void FindTypeTest()
        {
            Assert.Equal(typeof(TestClass), FindType<ITestInterface>("FindTypeTest"));
            Assert.Null(FindType<ITestInterface>("FindTypeTest2"));
        }

        [Fact]
        public void GetAttrTest()
        {
            Assert.NotNull(RtInfo.CoreRtAssembly.GetAttr<AssemblyTitleAttribute>());
            Assert.NotNull(MethodBase.GetCurrentMethod()?.GetAttr<FactAttribute>());
            Assert.NotNull(GetAttr<AttrTestAttribute, ObjectsTest>());
            Assert.NotNull(typeof(ObjectsTest).GetAttr<AttrTestAttribute>());
        }

        [Fact]
        public void GetTypesTest()
        {
            Assert.True(GetTypes<IComparable>().Count() > 2);
            Assert.True(GetTypes<Stream>(true).Count() > 2);
            Assert.True(GetTypes<Stream>(true).Count() < GetTypes<Stream>(false).Count());
            Assert.Contains(typeof(Enum), GetTypes<Enum>());
            Assert.Contains(typeof(Enum), GetTypes<Enum>(false));
            Assert.DoesNotContain(typeof(Enum), GetTypes<Enum>(true));
        }

        [Fact]
        public void HasAttrTest_Assembly()
        {
            Assert.True(RtInfo.CoreRtAssembly.HasAttr<AssemblyCopyrightAttribute>());
        }

        [Fact]
        public void IsAnyNullTest()
        {
            Assert.True(IsAnyNull(0, 1, null));
            Assert.False(IsAnyNull(0, 1, 2, 3));
        }

        [Fact]
        public void IsEitherTest()
        {
            var t = typeof(int);
            Assert.True(t.IsEither(typeof(bool), typeof(int)));
            Assert.False(t.IsEither(typeof(bool), typeof(float)));

            Assert.True(t.IsEither(new HashSet<object> {typeof(bool), typeof(int)}));
            Assert.False(t.IsEither(new HashSet<object> {typeof(bool), typeof(float)}));
        }

        [Fact]
        public void IsNeitherTest()
        {
            var t = typeof(int);
            Assert.True(t.IsNeither(typeof(bool), typeof(float)));
            Assert.False(t.IsNeither(typeof(bool), typeof(int)));
        }

        [Fact]
        public void IsNotTest()
        {
            EventArgs ev = new ExceptionEventArgs(null);
            EventArgs e = new ExceptionEventArgs(null);
            Assert.True(e.IsNot(ev));
        }

        [Fact]
        public void IsSignatureCompatibleTest()
        {
            var m = ReflectionHelpers.GetMethod<Action<int>>(() => TestClass.TestMethod)!;            
            Assert.True(m.IsSignatureCompatible<Action<int>>());
            Assert.False(m.IsSignatureCompatible<Action<float>>());
        }

        [Fact]
        public void IsTest()
        {
            var ev = EventArgs.Empty;
            var e = ev;
            Assert.True(e.Is(ev));
        }

        [Fact]
        public void ItselfTest()
        {
            var ex = new ApplicationException();
            Assert.Same(ex, ex.Itself());
            Assert.NotSame(ex, new ApplicationException());
            Assert.NotSame(ex, null);
        }

        [Fact]
        public void PropertiesOfTest()
        {
            var tc = new TestClass();
            Assert.Equal(tc.TestProperty, tc.PropertiesOf<int>().FirstOrDefault());
            Assert.Equal(tc.TestProperty, tc.GetType().GetProperties().PropertiesOf<int>(tc).FirstOrDefault());
            Assert.Equal(TestClass.ByteProperty, tc.GetType().GetProperties().PropertiesOf<byte>().FirstOrDefault());
        }

        [Fact]
        public void ToTypesTest()
        {
            var x = ToTypes(1, "Test", 2.5f).ToArray();
            var y = x.GetEnumerator();
            y.Reset();
            y.MoveNext();
            Assert.Same(typeof(int), y.Current);
            y.MoveNext();
            Assert.Same(typeof(string), y.Current);
            y.MoveNext();
            Assert.Same(typeof(float), y.Current);
        }

        [Fact]
        public void WhichAreNullTest()
        {
            Assert.NotNull(Array.Empty<object>().WhichAreNull());
            Assert.Equal(Array.Empty<int>(), WhichAreNull(new object(), new object()).ToArray());
            Assert.Equal(new[] {1}, WhichAreNull(new object(), null, new object(), new object()).ToArray());
            Assert.Equal(new[] {2, 3}, WhichAreNull(new object(), new object(), null, null).ToArray());
            Assert.Throws<ArgumentNullException>(((IEnumerable<object?>)null!).WhichAreNull().ToArray);
        }

        [Fact]
        public void WhichAreTest()
        {
            var x = new object();
            Assert.Equal(Array.Empty<int>(), x.WhichAre(new object(), 1, 0.0f).ToArray());
            Assert.Equal(new[] {2}, x.WhichAre(new object(), 1, x).ToArray());
            Assert.Equal(new[] {1, 3}, x.WhichAre(new object(), x, 0, x).ToArray());
        }

        [Fact]
        public void WithSignatureTest()
        {
            Assert.Null(typeof(TestClass).GetMethods().WithSignature<Action<short>>().FirstOrDefault());
            var m = typeof(TestClass).GetMethods().WithSignature<Action<int>>().FirstOrDefault()!;
            Assert.NotNull(m);
            m(1);
        }

        [Fact]
        public void WithSignatureTest_object()
        {
            var tc = new TestClass();
            Assert.Null(typeof(TestClass).GetMethods().WithSignature<Action<double>>(tc).FirstOrDefault());
            var m = typeof(TestClass).GetMethods().WithSignature<Action<float>>(tc).FirstOrDefault()!;
            Assert.NotNull(m);
            m(1.0f);
        }

        [Fact]
        public void PublicTypesTest()
        {
            //var d = AppDomain.CreateDomain("test");
            var cd = PublicTypes<Enum>(AppDomain.CurrentDomain).ToArray();
            //var nd = PublicTypes<Exception>(d).ToArray();

            Assert.True(cd.Any());
            //Assert.True(nd.Any());
            //Assert.True(cd.Length > nd.Length);
        }

        [Fact]
        public void TryCreateDelegateTest()
        {
            var m = GetType().GetMethod(nameof(TestEventHandler))!;

            Assert.True(TryCreateDelegate<EventHandler>(m, this, out _));
            Assert.False(TryCreateDelegate<EventHandler>(m, null!, out _));
            Assert.False(TryCreateDelegate<Action>(m, this, out _));
            Assert.False(TryCreateDelegate<Action<int>>(m, this, out _));
            Assert.False(TryCreateDelegate<EventHandler>(null!, out _));
            Assert.False(TryCreateDelegate<Action>(m, out _));
            Assert.False(TryCreateDelegate<Action<int>>(m, out _));
        }
    }
}