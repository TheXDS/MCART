﻿/*
TypeExtensionsTests.cs

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

using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using TheXDS.MCART.Exceptions;
using TheXDS.MCART.Types;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.Tests.Types.Extensions;

public class TypeExtensionsTests
{
    [ExcludeFromCodeCoverage]
    private class ThrowingTest
    {
        public ThrowingTest()
        {
            throw new InvalidOperationException();
        }
    }

    [Test]
    public void AnyAssignableFrom_Test()
    {
        Assert.IsTrue(typeof(ResolveEventArgs).Assignables(typeof(int), typeof(EventArgs), typeof(Exception)).First() == typeof(EventArgs));
        Assert.False(typeof(ResolveEventArgs).Assignables(typeof(int), typeof(Version), typeof(Exception)).Any());
    }

    [Test]
    public void AreAssignableFrom_Test()
    {
        Assert.IsTrue(typeof(ResolveEventArgs).AreAllAssignable(typeof(EventArgs), typeof(ResolveEventArgs)));
        Assert.False(typeof(ResolveEventArgs).AreAllAssignable(typeof(AppContext), typeof(ResolveEventArgs)));
    }

    [Test]
    public void New_Test()
    {
        Assert.NotNull(typeof(Exception).New());
        Assert.NotNull(typeof(Exception).New<Exception>());
        Assert.NotNull(typeof(ResolveEventArgs).New("Test"));
        Assert.AreEqual(typeof(string), Assert.Throws<ClassNotInstantiableException>(() => typeof(string).New(new Exception()))!.OffendingObject);
        Assert.Null(typeof(ThrowingTest).New<ThrowingTest>(false, null));
        Assert.IsInstanceOf<InvalidOperationException>(Assert.Throws<TargetInvocationException>(() => typeof(ThrowingTest).New<ThrowingTest>(true, null))!.InnerException);
    }

    [Test]
    public void NotNullable_Test()
    {
        Assert.AreEqual(typeof(int), typeof(int).NotNullable());
        Assert.AreEqual(typeof(int), typeof(int?).NotNullable());
        Assert.Throws<ArgumentNullException>(() => ((Type)null!).NotNullable());
    }

    [Test]
    public void IsInstantiable_Test()
    {
        Assert.IsTrue(typeof(Exception).IsInstantiable());
        Assert.IsTrue(typeof(Exception).IsInstantiable((IEnumerable<Type>?)null));
        Assert.IsTrue(typeof(Exception).IsInstantiable(typeof(string)));
        Assert.IsFalse(typeof(Exception).IsInstantiable(typeof(int)));
        Assert.IsTrue(typeof(Random).IsInstantiable());
        Assert.IsTrue(typeof(Random).IsInstantiable((IEnumerable<Type>?)null));
        Assert.IsFalse(typeof(FileStream).IsInstantiable());
        Assert.IsFalse(typeof(FileStream).IsInstantiable((IEnumerable<Type>?)null));
        Assert.IsFalse(typeof(IEnumerable<int>).IsInstantiable());
        Assert.IsFalse(typeof(IEnumerable<int>).IsInstantiable((IEnumerable<Type>?)null));
        Assert.IsFalse(typeof(File).IsInstantiable());
        Assert.IsFalse(typeof(File).IsInstantiable((IEnumerable<Type>?)null));
    }

    [Test]
    public void NewAsync_contract_test()
    {
        Assert.ThrowsAsync<ArgumentNullException>(() => ((Type?)null!).NewAsync<Random>());
        Assert.ThrowsAsync<ArgumentNullException>(() => ((Type?)null!).NewAsync(null!));
        Assert.ThrowsAsync<InvalidCastException>(() => typeof(Random).NewAsync<FileInfo>());
        Assert.ThrowsAsync<TypeLoadException>(() => typeof(ThrowingTest).NewAsync<ThrowingTest>());
    }

    [Test]
    public async Task NewAsync_with_default_ctor_test()
    {
        Assert.IsInstanceOf<Random>(await typeof(Random).NewAsync());
        Assert.IsInstanceOf<Random>(await typeof(Random).NewAsync(false));
        Assert.IsInstanceOf<Random>(await typeof(Random).NewAsync(false, null));

        Assert.IsNull(await typeof(Random).NewAsync(false, new object?[] { "Test", DayOfWeek.Monday }));
        Assert.ThrowsAsync<ClassNotInstantiableException>(() => typeof(Random).NewAsync(true, new object?[] { "Test", DayOfWeek.Monday }));
        Assert.ThrowsAsync<ClassNotInstantiableException>(() => typeof(Random).NewAsync(new object?[] { "Test", DayOfWeek.Monday }));
    }

    [Test]
    public async Task NewAsync_T_with_default_ctor_test()
    {
        Assert.IsInstanceOf<Random>(await typeof(Random).NewAsync<Random>());
        Assert.IsInstanceOf<Random>(await typeof(Random).NewAsync<Random>(false));
        Assert.IsInstanceOf<Random>(await typeof(Random).NewAsync<Random>(false, null));

        Assert.IsNull(await typeof(Random).NewAsync<Random>(false, new object?[] { "Test", DayOfWeek.Monday }));
        Assert.ThrowsAsync<ClassNotInstantiableException>(() => typeof(Random).NewAsync<Random>(true, new object?[] { "Test", DayOfWeek.Monday }));
        Assert.ThrowsAsync<ClassNotInstantiableException>(() => typeof(Random).NewAsync<Random>(new object?[] { "Test", DayOfWeek.Monday }));
    }

    [Test]
    public async Task NewAsync_with_parameterized_ctor_test()
    {
        Assert.IsInstanceOf<Exception>(await typeof(Exception).NewAsync(new object?[] { "Test" }));
        Assert.IsInstanceOf<Exception>(await typeof(Exception).NewAsync(false, new object?[] { "Test" }));

        Assert.IsNull(await typeof(Exception).NewAsync(false, new object?[] { 123m, DayOfWeek.Monday }));
        Assert.ThrowsAsync<ClassNotInstantiableException>(() => typeof(Exception).NewAsync(true, new object?[] { 123m, DayOfWeek.Monday }));
        Assert.ThrowsAsync<ClassNotInstantiableException>(() => typeof(Exception).NewAsync(new object?[] { 123m, DayOfWeek.Monday }));
    }

    [Test]
    public async Task NewAsync_T_with_parameterized_ctor_test()
    {
        Assert.IsInstanceOf<Exception>(await typeof(Exception).NewAsync<Exception>(new object?[] { "Test" }));
        Assert.IsInstanceOf<Exception>(await typeof(Exception).NewAsync<Exception>(false, new object?[] { "Test" }));

        Assert.IsNull(await typeof(Exception).NewAsync<Exception>(false, new object?[] { 123m, DayOfWeek.Monday }));
        Assert.ThrowsAsync<ClassNotInstantiableException>(() => typeof(Exception).NewAsync<Exception>(true, new object?[] { 123m, DayOfWeek.Monday }));
        Assert.ThrowsAsync<ClassNotInstantiableException>(() => typeof(Exception).NewAsync<Exception>(new object?[] { 123m, DayOfWeek.Monday }));
    }

    [Test]
    public void ToNamedEnum_Test()
    {
        Assert.IsInstanceOf<IEnumerable<NamedObject<Enum>>>(typeof(DayOfWeek).ToNamedEnum());
        Assert.Throws<ArgumentNullException>(() => _ = TheXDS.MCART.Types.Extensions.TypeExtensions.ToNamedEnum(null!));
        Assert.Throws<ArgumentException>(() => _ = typeof(string).ToNamedEnum());
    }

    [Test]
    public void Default_Test()
    {
        Assert.AreEqual(0, typeof(int).Default());
        Assert.AreEqual(0L, typeof(long).Default());
        Assert.AreEqual(0f, typeof(float).Default());
        Assert.AreEqual(0.0, typeof(double).Default());
        Assert.AreEqual(0m, typeof(decimal).Default());
        Assert.AreEqual(Guid.Empty, typeof(Guid).Default());
        Assert.Null(typeof(string).Default());
        Assert.Null(typeof(object).Default());
    }

    [Test]
    public void IsStruct_Test()
    {
        Assert.IsTrue(typeof(Guid).IsStruct());
        Assert.False(typeof(int).IsStruct());
        Assert.False(typeof(string).IsStruct());
    }

    [Test]
    public void IsCollectionType_Test()
    {
        Assert.False(typeof(int).IsCollectionType());
        Assert.False(typeof(Exception).IsCollectionType());
        Assert.IsTrue(typeof(string).IsCollectionType());
        Assert.IsTrue(typeof(int[]).IsCollectionType());
        Assert.IsTrue(typeof(List<bool>).IsCollectionType());
    }

    [Test]
    public void Derivates_Test()
    {
        Type[]? t = typeof(Exception).Derivates(typeof(Exception).Assembly).ToArray();
        Assert.Contains(typeof(ArgumentNullException), t);
        Assert.False(t.Contains(typeof(TamperException)));
        Assert.False(t.Contains(typeof(int)));
        Assert.False(t.Contains(typeof(Guid)));
        Assert.False(t.Contains(typeof(string)));
        Assert.False(t.Contains(typeof(Enum)));
        Assert.False(t.Contains(typeof(AppDomain)));
        Assert.False(t.Contains(typeof(object)));
    }

    [Test]
    public void GetCollectionType_Test()
    {
        Assert.AreEqual(typeof(int), typeof(int[]).GetCollectionType());
        Assert.AreEqual(typeof(int), typeof(IEnumerable<int>).GetCollectionType());
        Assert.AreEqual(typeof(object), typeof(IEnumerable).GetCollectionType());
        Assert.AreEqual(typeof(string), typeof(Dictionary<int, string>).GetCollectionType());
    }

    [Test]
    public void IsAnyAssignable_Test()
    {
        Assert.IsTrue(typeof(Exception).IsAnyAssignable(typeof(int), typeof(DayOfWeek), typeof(ArgumentNullException)));
        Assert.False(typeof(Exception).IsAnyAssignable(typeof(int), typeof(DayOfWeek), typeof(System.IO.Stream)));
    }

    [Test]
    public void ResolveCollectionType_Test()
    {
        Assert.AreEqual(typeof(int), typeof(List<int>).ResolveCollectionType());
        Assert.AreEqual(typeof(int), typeof(int).ResolveCollectionType());
    }

    [Test]
    public void Implements_Test()
    {
        Assert.IsTrue(typeof(ArgumentException).Implements(typeof(Exception), Type.EmptyTypes));
        Assert.IsTrue(typeof(int[]).Implements(typeof(IEnumerable<>), typeof(int)));
        Assert.IsTrue(typeof(int[]).Implements(typeof(IEnumerable<>)));
        Assert.IsTrue(typeof(string).Implements(typeof(IEnumerable<char>)));
        Assert.IsTrue(typeof(int[]).Implements(typeof(IEnumerable<int>)));
        Assert.IsTrue(typeof(List<int>).Implements(typeof(IEnumerable)));
        Assert.IsTrue(typeof(Dictionary<int, string>).Implements(typeof(IDictionary<,>)));
        Assert.IsTrue(typeof(Array).Implements(typeof(IEnumerable)));
        Assert.IsTrue(typeof(List<int>).Implements(typeof(IEnumerable<int>)));
        Assert.IsTrue(typeof(IEnumerable<float>).Implements(typeof(IEnumerable<>)));
        Assert.IsFalse(typeof(float[]).Implements(typeof(IEnumerable<>), typeof(int)));
        Assert.IsFalse(typeof(Exception).Implements(typeof(IEnumerable<>)));
        Assert.IsFalse(typeof(ValueTask<string>).Implements(typeof(IEnumerable<>)));
        Assert.IsTrue(typeof(List<string>).Implements(new[] {
            typeof(IEnumerable),
            typeof(ICollection<string>),
            typeof(IList)
        }));
        Assert.IsTrue(typeof(List<int>).Implements(new[] {
            typeof(IEnumerable),
            typeof(ICollection<>),
            typeof(IList)
        }));
        Assert.IsTrue(typeof(List<string>).Implements(new[] {
            typeof(IEnumerable),
            typeof(ICollection<string>),
            typeof(IList)
        }));
        Assert.IsTrue(typeof(List<>).Implements(typeof(IEnumerable)));
        Assert.IsTrue(typeof(List<>).Implements(typeof(IEnumerable<>)));
        Assert.IsTrue(typeof(List<int>).Implements(typeof(IEnumerable<>)));
        Assert.IsTrue(typeof(List<int>).Implements(typeof(IEnumerable<>), typeof(int)));
        Assert.IsFalse(typeof(List<>).Implements(typeof(IEnumerable<>), typeof(int)));
    }

    [Test]
    public void TryInstance_Test()
    {
        Assert.IsTrue(typeof(Exception).TryInstance(out Exception? ex, "message"));
        Assert.NotNull(ex);
        Assert.False(typeof(Exception).TryInstance(out Exception? ex2, 1, 2, 3, 4));
        Assert.Null(ex2);
        Assert.False(typeof(ICloneable).TryInstance<ICloneable>(out ICloneable? x));
        Assert.Null(x);
        Assert.IsTrue(typeof(decimal).TryInstance<decimal>(out decimal d));
        Assert.AreEqual(default(decimal), d);
        Assert.False(typeof(ThrowingTest).TryInstance<ThrowingTest>(out ThrowingTest? tt));
        Assert.Null(tt);
    }

    [TestCase(typeof(int))]
    [TestCase(typeof(int?))]
    [TestCase(typeof(decimal))]
    [TestCase(typeof(decimal?))]
    public void ImplementsOperator_returns_true_for_valid_types_Test(Type t)
    {
        Assert.IsTrue(t.ImplementsOperator(Expression.Add));
        Assert.IsTrue(t.ImplementsOperator(Expression.Subtract));
        Assert.IsTrue(t.ImplementsOperator(Expression.Multiply));
        Assert.IsTrue(t.ImplementsOperator(Expression.Divide));
        Assert.IsTrue(t.ImplementsOperator(Expression.Modulo));
    }

    [TestCase(typeof(object))]
    [TestCase(typeof(Guid))]
    [TestCase(typeof(Exception))]
    public void ImplementsOperator_returns_false_for_invalid_types_Test(Type t)
    {
        Assert.IsFalse(t.ImplementsOperator(Expression.Add));
        Assert.IsFalse(t.ImplementsOperator(Expression.Subtract));
        Assert.IsFalse(t.ImplementsOperator(Expression.Multiply));
        Assert.IsFalse(t.ImplementsOperator(Expression.Divide));
        Assert.IsFalse(t.ImplementsOperator(Expression.Modulo));
    }

    [Test]
    public void CSharpName_Test()
    {
        Assert.AreEqual("System.Collections.Generic.List<System.String>", typeof(List<string>).CSharpName());
        Assert.AreEqual("System.Collections.Generic.Dictionary<System.Int32, System.String>", typeof(Dictionary<int, string>).CSharpName());
    }

    [TestCase(typeof(List<>), "System.Collections.Generic.List")]
    [TestCase(typeof(int), "System.Int32")]
    public void CleanFullName_Test(Type type, string typeName)
    {
        Assert.AreEqual(typeName, type.CleanFullName());
        Assert.AreEqual(typeName, type.MakeByRefType().CleanFullName());
        Assert.AreEqual(typeName, type.MakeArrayType().CleanFullName());
        Assert.AreEqual(typeName, type.MakeArrayType().MakeByRefType().CleanFullName());
        Assert.AreEqual(typeName, type.MakePointerType().CleanFullName());
    }
}
