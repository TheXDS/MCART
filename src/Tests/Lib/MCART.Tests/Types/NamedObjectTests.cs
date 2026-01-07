/*
NamedObjectTests.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2026 César Andrés Morgan

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

using System.Diagnostics.CodeAnalysis;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Exceptions;
using TheXDS.MCART.Helpers;
using TheXDS.MCART.Types;
using TheXDS.MCART.Types.Base;
using TheXDS.MCART.Types.Extensions;
using static TheXDS.MCART.Types.Extensions.NamedObjectExtensions;

namespace TheXDS.MCART.Tests.Types;

public class NamedObjectTests
{
    [ExcludeFromCodeCoverage]
    private class NameableClass : INameable
    {
        [Name("Name property")] public string Name => "Nameable class";
    }

    private class TestClass2
    {
        public override string ToString() => null!;
    }

    [Name("Test enum")]
    private enum TestEnum
    {
        [Name("Test A")] A,
        [Name("Test B")] B,
        [Name("Test C")] C
    }

    [Test]
    public void AsNamedEnumTest()
    {
        IEnumerable<NamedObject<Enum>>? x = typeof(TestEnum).AsNamedEnum();
        Assert.That("Test A", Is.EqualTo(x.First().Name));

        Assert.Throws<ArgumentNullException>(() => ((Type)null!).AsNamedEnum());
        Assert.Throws<InvalidTypeException>(() => typeof(string).AsNamedEnum());
        Assert.Throws<InvalidTypeException>(() => typeof(int?).AsNamedEnum());
    }

    [Test]
    public void FromEnumTest()
    {
        IEnumerable<NamedObject<TestEnum>>? x = NamedObject.FromEnum<TestEnum>();
        Assert.That("Test A", Is.EqualTo(x.First().Name));
    }

    [Test]
    public void AsNamedObjectTest()
    {
        IEnumerable<NamedObject<TestEnum>>? x = AsNamedObject<TestEnum>();
        Assert.That("Test A", Is.EqualTo(x.First().Name));
    }

    [Test]
    public void Infer_Test()
    {
        NameableClass? x = new();
        Assert.That("Nameable class", Is.EqualTo(NamedObject<object?>.Infer(x)));
        Assert.That("Name property", Is.EqualTo(NamedObject<object?>.Infer(ReflectionHelpers.GetProperty(() => x.Name))));
        Assert.That("Test enum", Is.EqualTo(NamedObject<object?>.Infer(typeof(TestEnum))));
        Assert.That("Exception", Is.EqualTo(NamedObject<object?>.Infer(typeof(Exception))));
        Assert.That("Test A", Is.EqualTo(NamedObject<object?>.Infer(TestEnum.A)));
        Assert.That("TestClass2", Is.EqualTo(NamedObject<object?>.Infer(new TestClass2())));
        Assert.Throws<ArgumentNullException>(() => NamedObject<object?>.Infer(null));
    }

    [Test]
    public void Implicit_Operators_Test()
    {
        NamedObject<TestEnum> v = NamedObject.FromEnum<TestEnum>().ToArray()[1];
        Assert.That(TestEnum.B, Is.EqualTo((TestEnum)v));
        Assert.That("Test B", Is.EqualTo(((KeyValuePair<string, TestEnum>)v).Key));
        Assert.That(TestEnum.B, Is.EqualTo(((KeyValuePair<string, TestEnum>)v).Value));
        Assert.That("Test B", Is.EqualTo((string)(NamedObject<TestEnum>)new KeyValuePair<string, TestEnum>("Test B", TestEnum.B)));
    }

    [Test]
    public void Equals_Test()
    {
        NamedObject<TestEnum> v = NamedObject.FromEnum<TestEnum>().ToArray()[1];
        Assert.That(v.Equals(NamedObject.FromEnum<TestEnum>().ToArray()[1]));
        Assert.That(v.Equals(NamedObject.FromEnum<TestEnum>().ToArray()[2]), Is.False);
        Assert.That(v.Equals(TestEnum.B));
        Assert.That(v.Equals(TestEnum.C), Is.False);
        Assert.That(new NamedObject<int?>("Test", null).Equals(null));
        Assert.That(v.Equals(null), Is.False);
        Assert.That(v.Equals(new Exception()), Is.False);
    }

    [Test]
    public void Equals_Operator_Test()
    {
        NamedObject<TestEnum> v1 = NamedObject.FromEnum<TestEnum>().ToArray()[1];
        NamedObject<TestEnum> v2 = NamedObject.FromEnum<TestEnum>().ToArray()[1];
        NamedObject<TestEnum> v3 = NamedObject.FromEnum<TestEnum>().ToArray()[2];

        Assert.That(v1 == v2);
        Assert.That(v1 == v3, Is.False);
        Assert.That(v1 != v2, Is.False);
        Assert.That(v1 != v3);
    }

    [Test]
    public void GetHashCode_Test()
    {
        NamedObject<TestEnum> v1 = NamedObject.FromEnum<TestEnum>().ToArray()[1];
        NamedObject<TestEnum> v2 = NamedObject.FromEnum<TestEnum>().ToArray()[1];
        NamedObject<TestEnum> v3 = NamedObject.FromEnum<TestEnum>().ToArray()[2];

        Assert.That(v1.GetHashCode() == v2.GetHashCode());
        Assert.That(v1.GetHashCode() == v3.GetHashCode(), Is.False);
    }

    [Test]
    public void From_string_TValue_tuple_implicit_conversion()
    {
        (string, int) tuple = ("1", 1);
        NamedObject<int> n = tuple;
        Assert.Multiple(() => {
            Assert.That(n.Name == "1");
            Assert.That(n.Value == 1);
        });
    }

    [Test]
    public void From_TValue_string_tuple_implicit_conversion()
    {
        (int, string) tuple = (1, "1");
        NamedObject<int> n = tuple;
        Assert.Multiple(() => {
            Assert.That(n.Name == "1");
            Assert.That(n.Value == 1);
        });
    }

    [Test]
    public void To_string_TValue_tuple_implicit_conversion()
    {
        NamedObject<int> n = new("1", 1);
        (string Name, int Value) tuple = n;
        Assert.Multiple(() => {
            Assert.That(tuple.Name == "1");
            Assert.That(tuple.Value == 1);
        });
    }

    [Test]
    public void To_TValue_string_tuple_implicit_conversion()
    {
        NamedObject<int> n = new("1", 1);
        (int Value, string Name) tuple = n;
        Assert.Multiple(() => {
            Assert.That(tuple.Name == "1");
            Assert.That(tuple.Value == 1);
        });
    }
}
