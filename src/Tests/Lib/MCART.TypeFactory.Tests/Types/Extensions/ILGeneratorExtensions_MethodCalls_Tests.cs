// ILGeneratorExtensions_MethodCalls_Tests.cs
//
// This file is part of Morgan's CLR Advanced Runtime (MCART)
//
// Author(s):
//      César Andrés Morgan <xds_xps_ivx@hotmail.com>
//
// Released under the MIT License (MIT)
// Copyright © 2011 - 2026 César Andrés Morgan
//
// Permission is hereby granted, free of charge, to any person obtaining a copy of
// this software and associated documentation files (the “Software”), to deal in
// the Software without restriction, including without limitation the rights to
// use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
// of the Software, and to permit persons to whom the Software is furnished to do
// so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using System.Diagnostics.CodeAnalysis;
using TheXDS.MCART.Types.Extensions;
using static TheXDS.MCART.Types.Extensions.ILGeneratorExtensions;

namespace TheXDS.MCART.TypeFactory.Tests.Types.Extensions;

public partial class ILGeneratorExtensions_Tests
{
    [ExcludeFromCodeCoverage]
    public abstract class TestCtorBaseClass1
    {
        private readonly int _intParam;
        private readonly string _stringParam;

        public TestCtorBaseClass1(int intParam, string stringParam)
        {
            _intParam = intParam;
            _stringParam = stringParam;
        }

        public void Assertions(int intParam, string stringParam)
        {
            Assert.That(_intParam, Is.EqualTo(intParam));
            Assert.That(_stringParam, Is.EqualTo(stringParam));
        }
    }

    [ExcludeFromCodeCoverage]
    public abstract class TestCtorBaseClass2
    {
        public TestCtorBaseClass2()
        {
        }
    }

    [Test]
    public void CallBaseCtor_calls_base_ctor_with_parameterCallback()
    {
        var t = Factory.NewType<TestCtorBaseClass1>($"TestCtorBaseClass1_Class");
        t.Builder.AddPublicConstructor()
            .CallBaseCtor<TestCtorBaseClass1>(
                new[] {typeof(int), typeof(string) }, il => il
                .LoadConstant(1)
                .LoadConstant("Test")
            ).Return();
        var obj = t.Builder.CreateType()!.New<TestCtorBaseClass1>();
        Assert.That(() => obj.Assertions(1, "Test"), Throws.Nothing);
    }

    [Test]
    public void CallBaseCtor_calls_base_ctor_with_manual_setup()
    {
        var t = Factory.NewType<TestCtorBaseClass1>($"TestCtorBaseClass1_Class");
        t.Builder.AddPublicConstructor()
            .LoadArg0()
            .LoadConstant(1)
            .LoadConstant("Test")
            .CallBaseCtor<TestCtorBaseClass1>(new[] { typeof(int), typeof(string) }, null)
            .Return();
        var obj = t.Builder.CreateType()!.New<TestCtorBaseClass1>();
        Assert.That(() => obj.Assertions(1, "Test"), Throws.Nothing);
    }

    [Test]
    public void CallBaseCtor_calls_base_ctor_with_no_params()
    {
        var t = Factory.NewType<TestCtorBaseClass2>($"TestCtorBaseClass2_Class");
        t.Builder.AddPublicConstructor()
            .CallBaseCtor<TestCtorBaseClass2>(Type.EmptyTypes, null)
            .Return();
        
        Assert.That(t.Builder.CreateType()!.New<TestCtorBaseClass2>(), Is.Not.Null);
    }

    [Test]
    public void CallBaseCtor_throws_if_paramCallback_is_set_for_parameterless_ctor()
    {
        var t = Factory.NewType<TestCtorBaseClass2>($"TestCtorBaseClass2_Class");
        Assert.That(() => { 
            t.Builder.AddPublicConstructor()
                .CallBaseCtor<TestCtorBaseClass1>(Type.EmptyTypes, _ => { })
                .Return();
        }, Throws.InvalidOperationException);
    }
}
