/*
NamedObjectTests.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2022 César Andrés Morgan

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

namespace TheXDS.MCART.Tests.Types;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Exceptions;
using TheXDS.MCART.Helpers;
using TheXDS.MCART.Types;
using TheXDS.MCART.Types.Base;
using TheXDS.MCART.Types.Extensions;
using NUnit.Framework;
using static TheXDS.MCART.Types.Extensions.NamedObjectExtensions;

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
        [Name("Elemento A")] A,
        [Name("Elemento B")] B,
        [Name("Elemento C")] C
    }

    [Test]
    public void AsNamedEnumTest()
    {
        IEnumerable<NamedObject<Enum>>? x = typeof(TestEnum).AsNamedEnum();
        Assert.AreEqual("Elemento A", x.First().Name);

        Assert.Throws<ArgumentNullException>(() => ((Type)null!).AsNamedEnum());
        Assert.Throws<InvalidTypeException>(() => typeof(string).AsNamedEnum());
        Assert.Throws<InvalidTypeException>(() => typeof(int?).AsNamedEnum());
    }

    [Test]
    public void FromEnumTest()
    {
        IEnumerable<NamedObject<TestEnum>>? x = NamedObject<TestEnum>.FromEnum();
        Assert.AreEqual("Elemento A", x.First().Name);

        Assert.Throws<InvalidTypeException>(() => NamedObject<string>.FromEnum());
    }

    [Test]
    public void AsNamedObjectTest()
    {
        IEnumerable<NamedObject<TestEnum>>? x = AsNamedObject<TestEnum>();
        Assert.AreEqual("Elemento A", x.First().Name);
    }

    [Test]
    public void Infer_Test()
    {
        NameableClass? x = new();
        Assert.AreEqual("Nameable class", NamedObject<object?>.Infer(x));
        Assert.AreEqual("Name property", NamedObject<object?>.Infer(ReflectionHelpers.GetProperty(() => x.Name)));
        Assert.AreEqual("Test enum", NamedObject<object?>.Infer(typeof(TestEnum)));
        Assert.AreEqual("Exception", NamedObject<object?>.Infer(typeof(Exception)));
        Assert.AreEqual("Elemento A", NamedObject<object?>.Infer(TestEnum.A));
        Assert.AreEqual("TestClass2", NamedObject<object?>.Infer(new TestClass2()));
        Assert.Throws<ArgumentNullException>(() => NamedObject<object?>.Infer(null));
    }

    [Test]
    public void Implicit_Operators_Test()
    {
        NamedObject<TestEnum> v = NamedObject<TestEnum>.FromEnum().ToArray()[1];
        Assert.AreEqual(TestEnum.B, (TestEnum)v);
        Assert.AreEqual("Elemento B", ((KeyValuePair<string, TestEnum>)v).Key);
        Assert.AreEqual(TestEnum.B, ((KeyValuePair<string, TestEnum>)v).Value);
        Assert.AreEqual("Elemento B", (string)(NamedObject<TestEnum>)new KeyValuePair<string, TestEnum>("Elemento B", TestEnum.B));
    }

    [Test]
    public void Equals_Test()
    {
        NamedObject<TestEnum> v = NamedObject<TestEnum>.FromEnum().ToArray()[1];
        Assert.True(v.Equals(NamedObject<TestEnum>.FromEnum().ToArray()[1]));
        Assert.False(v.Equals(NamedObject<TestEnum>.FromEnum().ToArray()[2]));
        Assert.True(v.Equals(TestEnum.B));
        Assert.False(v.Equals(TestEnum.C));
        Assert.True(new NamedObject<int?>(null, "Test").Equals(null));
        Assert.False(v.Equals(null));
        Assert.False(v.Equals(new Exception()));
    }

    [Test]
    public void Equals_Operator_Test()
    {
        NamedObject<TestEnum> v1 = NamedObject<TestEnum>.FromEnum().ToArray()[1];
        NamedObject<TestEnum> v2 = NamedObject<TestEnum>.FromEnum().ToArray()[1];
        NamedObject<TestEnum> v3 = NamedObject<TestEnum>.FromEnum().ToArray()[2];

        Assert.True(v1 == v2);
        Assert.False(v1 == v3);
        Assert.False(v1 != v2);
        Assert.True(v1 != v3);
    }

    [Test]
    public void GetHashCode_Test()
    {
        NamedObject<TestEnum> v1 = NamedObject<TestEnum>.FromEnum().ToArray()[1];
        NamedObject<TestEnum> v2 = NamedObject<TestEnum>.FromEnum().ToArray()[1];
        NamedObject<TestEnum> v3 = NamedObject<TestEnum>.FromEnum().ToArray()[2];

        Assert.True(v1.GetHashCode() == v2.GetHashCode());
        Assert.False(v1.GetHashCode() == v3.GetHashCode());
    }
}
