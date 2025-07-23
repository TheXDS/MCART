/*
TypeExtensionsTests.cs

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

using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Reflection;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Exceptions;
using TheXDS.MCART.Helpers;
using TheXDS.MCART.Resources;
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
        Assert.That(typeof(ResolveEventArgs).Assignables(typeof(int), typeof(EventArgs), typeof(Exception)).First() == typeof(EventArgs));
        Assert.That(typeof(ResolveEventArgs).Assignables(typeof(int), typeof(Version), typeof(Exception)).Any(), Is.False);
    }

    [Test]
    public void AreAssignableFrom_Test()
    {
        Assert.That(typeof(ResolveEventArgs).AreAllAssignable(typeof(EventArgs), typeof(ResolveEventArgs)));
        Assert.That(typeof(ResolveEventArgs).AreAllAssignable(typeof(AppContext), typeof(ResolveEventArgs)), Is.False);
    }

    [Test]
    public void New_Test()
    {
        Assert.That(typeof(Exception).New(), Is.Not.Null);
        Assert.That(typeof(Exception).New<Exception>(), Is.Not.Null);
        Assert.That(typeof(ResolveEventArgs).New("Test"), Is.Not.Null);
        Assert.That(typeof(string), Is.EqualTo(Assert.Throws<ClassNotInstantiableException>(() => typeof(string).New(new Exception()))!.OffendingObject));
        Assert.That(typeof(ThrowingTest).New<ThrowingTest>(false, null), Is.Null);
        Assert.That(Assert.Throws<TargetInvocationException>(() => typeof(ThrowingTest).New<ThrowingTest>(true, null))!.InnerException, Is.InstanceOf<InvalidOperationException>());
    }

    [Test]
    public void NotNullable_Test()
    {
        Assert.That(typeof(int), Is.EqualTo(typeof(int).NotNullable()));
        Assert.That(typeof(int), Is.EqualTo(typeof(int?).NotNullable()));
        Assert.Throws<ArgumentNullException>(() => ((Type)null!).NotNullable());
    }

    [Test]
    public void IsInstantiable_Test()
    {
        Assert.That(typeof(Exception).IsInstantiable());
        Assert.That(typeof(Exception).IsInstantiable((IEnumerable<Type>?)null));
        Assert.That(typeof(Exception).IsInstantiable(typeof(string)));
        Assert.That(typeof(Exception).IsInstantiable(typeof(int)), Is.False);
        Assert.That(typeof(Random).IsInstantiable());
        Assert.That(typeof(Random).IsInstantiable((IEnumerable<Type>?)null));
        Assert.That(typeof(FileStream).IsInstantiable(), Is.False);
        Assert.That(typeof(FileStream).IsInstantiable((IEnumerable<Type>?)null), Is.False);
        Assert.That(typeof(IEnumerable<int>).IsInstantiable(), Is.False);
        Assert.That(typeof(IEnumerable<int>).IsInstantiable((IEnumerable<Type>?)null), Is.False);
        Assert.That(typeof(File).IsInstantiable(), Is.False);
        Assert.That(typeof(File).IsInstantiable((IEnumerable<Type>?)null), Is.False);
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
        Assert.That(await typeof(Random).NewAsync(), Is.InstanceOf<Random>());
        Assert.That(await typeof(Random).NewAsync(false), Is.InstanceOf<Random>());
        Assert.That(await typeof(Random).NewAsync(false, null), Is.InstanceOf<Random>());

        Assert.That(await typeof(Random).NewAsync(false, new object?[] { "Test", DayOfWeek.Monday }), Is.Null);
        Assert.ThrowsAsync<ClassNotInstantiableException>(() => typeof(Random).NewAsync(true, new object?[] { "Test", DayOfWeek.Monday }));
        Assert.ThrowsAsync<ClassNotInstantiableException>(() => typeof(Random).NewAsync(new object?[] { "Test", DayOfWeek.Monday }));
    }

    [Test]
    public async Task NewAsync_T_with_default_ctor_test()
    {
        Assert.That(await typeof(Random).NewAsync<Random>(), Is.InstanceOf<Random>());
        Assert.That(await typeof(Random).NewAsync<Random>(false), Is.InstanceOf<Random>());
        Assert.That(await typeof(Random).NewAsync<Random>(false, null), Is.InstanceOf<Random>());

        Assert.That(await typeof(Random).NewAsync<Random>(false, new object?[] { "Test", DayOfWeek.Monday }), Is.Null);
        Assert.ThrowsAsync<ClassNotInstantiableException>(() => typeof(Random).NewAsync<Random>(true, new object?[] { "Test", DayOfWeek.Monday }));
        Assert.ThrowsAsync<ClassNotInstantiableException>(() => typeof(Random).NewAsync<Random>(new object?[] { "Test", DayOfWeek.Monday }));
    }

    [Test]
    public async Task NewAsync_with_parameterized_ctor_test()
    {
        Assert.That(await typeof(Exception).NewAsync(new object?[] { "Test" }), Is.InstanceOf<Exception>());
        Assert.That(await typeof(Exception).NewAsync(false, new object?[] { "Test" }), Is.InstanceOf<Exception>());

        Assert.That(await typeof(Exception).NewAsync(false, new object?[] { 123m, DayOfWeek.Monday }), Is.Null);
        Assert.ThrowsAsync<ClassNotInstantiableException>(() => typeof(Exception).NewAsync(true, new object?[] { 123m, DayOfWeek.Monday }));
        Assert.ThrowsAsync<ClassNotInstantiableException>(() => typeof(Exception).NewAsync(new object?[] { 123m, DayOfWeek.Monday }));
    }

    [Test]
    public async Task NewAsync_T_with_parameterized_ctor_test()
    {
        Assert.That(await typeof(Exception).NewAsync<Exception>(new object?[] { "Test" }), Is.InstanceOf<Exception>());
        Assert.That(await typeof(Exception).NewAsync<Exception>(false, new object?[] { "Test" }), Is.InstanceOf<Exception>());

        Assert.That(await typeof(Exception).NewAsync<Exception>(false, new object?[] { 123m, DayOfWeek.Monday }), Is.Null);
        Assert.ThrowsAsync<ClassNotInstantiableException>(() => typeof(Exception).NewAsync<Exception>(true, new object?[] { 123m, DayOfWeek.Monday }));
        Assert.ThrowsAsync<ClassNotInstantiableException>(() => typeof(Exception).NewAsync<Exception>(new object?[] { 123m, DayOfWeek.Monday }));
    }

    [Test]
    public void ToNamedEnum_Test()
    {
        Assert.That(typeof(DayOfWeek).ToNamedEnum(), Is.InstanceOf<IEnumerable<NamedObject<Enum>>>());
        Assert.Throws<ArgumentNullException>(() => _ = MCART.Types.Extensions.TypeExtensions.ToNamedEnum(null!));
        Assert.Throws<ArgumentException>(() => _ = typeof(string).ToNamedEnum());
    }

    [Test]
    public void Default_Test()
    {
        Assert.That(0, Is.EqualTo(typeof(int).Default()));
        Assert.That(0L, Is.EqualTo(typeof(long).Default()));
        Assert.That(0f, Is.EqualTo(typeof(float).Default()));
        Assert.That(0.0, Is.EqualTo(typeof(double).Default()));
        Assert.That(0m, Is.EqualTo(typeof(decimal).Default()));
        Assert.That(Guid.Empty, Is.EqualTo(typeof(Guid).Default()));
        Assert.That(typeof(string).Default(), Is.Null);
        Assert.That(typeof(object).Default(), Is.Null);
    }

    [Test]
    public void IsStruct_Test()
    {
        Assert.That(typeof(Guid).IsStruct());
        Assert.That(typeof(int).IsStruct(), Is.False);
        Assert.That(typeof(string).IsStruct(), Is.False);
    }

    [Test]
    public void IsCollectionType_Test()
    {
        Assert.That(typeof(int).IsCollectionType(), Is.False);
        Assert.That(typeof(Exception).IsCollectionType(), Is.False);
        Assert.That(typeof(string).IsCollectionType());
        Assert.That(typeof(int[]).IsCollectionType());
        Assert.That(typeof(List<bool>).IsCollectionType());
    }

    [Test]
    public void Derivates_Test()
    {
        Type[]? t = [.. typeof(Exception).FindDerivedTypes(typeof(Exception).Assembly)];
        Assert.That(t, Contains.Item(typeof(ArgumentNullException)));
        Assert.That(t.Contains(typeof(TamperException)), Is.False);
        Assert.That(t.Contains(typeof(int)), Is.False);
        Assert.That(t.Contains(typeof(Guid)), Is.False);
        Assert.That(t.Contains(typeof(string)), Is.False);
        Assert.That(t.Contains(typeof(Enum)), Is.False);
        Assert.That(t.Contains(typeof(AppDomain)), Is.False);
        Assert.That(t.Contains(typeof(object)), Is.False);
    }

    [Test]
    public void GetAttrAlt_Test()
    {
        Assert.That(typeof(CopyrightAttribute).GetAttrAlt<AttributeUsageAttribute>(), Is.Not.Null);
        Assert.That(typeof(CopyrightAttribute).GetAttrAlt<SpdxLicenseAttribute>(), Is.Not.Null);
    }

    [Test]
    public void HasAttrAlt_Test()
    {
        Assert.That(typeof(CopyrightAttribute).HasAttrAlt<AttributeUsageAttribute>(), Is.True);
        Assert.That(typeof(CopyrightAttribute).HasAttrAlt<SpdxLicenseAttribute>(), Is.True);
    }

    [Test]
    public void GetDefinedMethods_Test()
    {
        Assert.That(typeof(Exception).GetDefinedMethods().Any(m => m.Name == nameof(Exception.ToString)));
        Assert.That(typeof(Exception).GetDefinedMethods().Any(m => m.Name == nameof(ReferenceEquals)), Is.False);
    }

    [Test]
    public void GetPublicProperties_Test()
    {
        Assert.That(typeof(Exception).GetPublicProperties().Any(p => p.Name == nameof(Exception.Message)));
    }

    [Test]
    public void GetCollectionType_Test()
    {
        Assert.That(typeof(int), Is.EqualTo(typeof(int[]).GetCollectionType()));
        Assert.That(typeof(int), Is.EqualTo(typeof(IEnumerable<int>).GetCollectionType()));
        Assert.That(typeof(object), Is.EqualTo(typeof(IEnumerable).GetCollectionType()));
        Assert.That(typeof(string), Is.EqualTo(typeof(Dictionary<int, string>).GetCollectionType()));
    }

    [Test]
    public void IsAnyAssignable_Test()
    {
        Assert.That(typeof(Exception).IsAnyAssignable(typeof(int), typeof(DayOfWeek), typeof(ArgumentNullException)));
        Assert.That(typeof(Exception).IsAnyAssignable(typeof(int), typeof(DayOfWeek), typeof(Stream)), Is.False);
    }

    [Test]
    public void ResolveCollectionType_Test()
    {
        Assert.That(typeof(int), Is.EqualTo(typeof(List<int>).ResolveCollectionType()));
        Assert.That(typeof(int), Is.EqualTo(typeof(int).ResolveCollectionType()));
    }

    [Test]
    public void Implements_Test()
    {
        Assert.That(typeof(ArgumentException).Implements(typeof(Exception), Type.EmptyTypes));
        Assert.That(typeof(int[]).Implements(typeof(IEnumerable<>), typeof(int)));
        Assert.That(typeof(int[]).Implements(typeof(IEnumerable<>)));
        Assert.That(typeof(string).Implements(typeof(IEnumerable<char>)));
        Assert.That(typeof(int[]).Implements(typeof(IEnumerable<int>)));
        Assert.That(typeof(List<int>).Implements(typeof(IEnumerable)));
        Assert.That(typeof(Dictionary<int, string>).Implements(typeof(IDictionary<,>)));
        Assert.That(typeof(Array).Implements(typeof(IEnumerable)));
        Assert.That(typeof(List<int>).Implements(typeof(IEnumerable<int>)));
        Assert.That(typeof(IEnumerable<float>).Implements(typeof(IEnumerable<>)));
        Assert.That(typeof(float[]).Implements(typeof(IEnumerable<>), typeof(int)), Is.False);
        Assert.That(typeof(Exception).Implements(typeof(IEnumerable<>)), Is.False);
        Assert.That(typeof(ValueTask<string>).Implements(typeof(IEnumerable<>)), Is.False);
        Assert.That(typeof(List<string>).Implements([
            typeof(IEnumerable),
            typeof(ICollection<string>),
            typeof(IList)
        ]));
        Assert.That(typeof(List<int>).Implements([
            typeof(IEnumerable),
            typeof(ICollection<>),
            typeof(IList)
        ]));
        Assert.That(typeof(List<string>).Implements([
            typeof(IEnumerable),
            typeof(ICollection<string>),
            typeof(IList)
        ]));
        Assert.That(typeof(List<>).Implements(typeof(IEnumerable)));
        Assert.That(typeof(List<>).Implements(typeof(IEnumerable<>)));
        Assert.That(typeof(List<int>).Implements(typeof(IEnumerable<>)));
        Assert.That(typeof(List<int>).Implements(typeof(IEnumerable<>), typeof(int)));
        Assert.That(typeof(List<>).Implements(typeof(IEnumerable<>), typeof(int)), Is.False);
    }

    [Test]
    public void TryInstance_Test()
    {
        Assert.That(typeof(Exception).TryInstance(out Exception? ex, "message"));
        Assert.That(ex, Is.Not.Null);
        Assert.That(typeof(Exception).TryInstance(out Exception? ex2, 1, 2, 3, 4), Is.False);
        Assert.That(ex2, Is.Null);
        Assert.That(typeof(ICloneable).TryInstance(out ICloneable? x), Is.False);
        Assert.That(x, Is.Null);
        Assert.That(typeof(decimal).TryInstance(out decimal d));
        Assert.That(default(decimal), Is.EqualTo(d));
        Assert.That(typeof(ThrowingTest).TryInstance(out ThrowingTest? tt), Is.False);
        Assert.That(tt, Is.Null);
    }

    [TestCase(typeof(int))]
    [TestCase(typeof(int?))]
    [TestCase(typeof(decimal))]
    [TestCase(typeof(decimal?))]
    public void ImplementsOperator_returns_true_for_valid_types_Test(Type t)
    {
        Assert.That(t.ImplementsOperator(Expression.Add));
        Assert.That(t.ImplementsOperator(Expression.Subtract));
        Assert.That(t.ImplementsOperator(Expression.Multiply));
        Assert.That(t.ImplementsOperator(Expression.Divide));
        Assert.That(t.ImplementsOperator(Expression.Modulo));
    }

    [TestCase(typeof(object))]
    [TestCase(typeof(Guid))]
    [TestCase(typeof(Exception))]
    public void ImplementsOperator_returns_false_for_invalid_types_Test(Type t)
    {
        Assert.That(t.ImplementsOperator(Expression.Add), Is.False);
        Assert.That(t.ImplementsOperator(Expression.Subtract), Is.False);
        Assert.That(t.ImplementsOperator(Expression.Multiply), Is.False);
        Assert.That(t.ImplementsOperator(Expression.Divide), Is.False);
        Assert.That(t.ImplementsOperator(Expression.Modulo), Is.False);
    }

    [Test]
    public void CSharpName_Test()
    {
        Assert.That(typeof(List<string>).CSharpName(), Is.EqualTo("System.Collections.Generic.List<System.String>"));
        Assert.That(typeof(Dictionary<int, string>).CSharpName(), Is.EqualTo("System.Collections.Generic.Dictionary<System.Int32, System.String>"));
        Assert.That(typeof(List<KeyValuePair<DayOfWeek, Func<int, bool>>>).CSharpName(), Is.EqualTo("System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<System.DayOfWeek, System.Func<System.Int32, System.Boolean>>>"));
    }

    [TestCase(typeof(List<>), "System.Collections.Generic.List")]
    [TestCase(typeof(int), "System.Int32")]
    public void CleanFullName_Test(Type type, string typeName)
    {
        Assert.That(typeName, Is.EqualTo(type.CleanFullName()));
        Assert.That(typeName, Is.EqualTo(type.MakeByRefType().CleanFullName()));
        Assert.That(typeName, Is.EqualTo(type.MakeArrayType().CleanFullName()));
        Assert.That(typeName, Is.EqualTo(type.MakeArrayType().MakeByRefType().CleanFullName()));
        Assert.That(typeName, Is.EqualTo(type.MakePointerType().CleanFullName()));
    }
}
