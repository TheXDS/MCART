// ILGeneratorExtensions_Tests.cs
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

using System.Reflection;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.TypeFactory.Tests.Types.Extensions;

public partial class ILGeneratorExtensions_Tests : TypeFactoryTestClassBase
{
    [Test]
    public void NewArray_with_explicit_size_test()
    {
        var (builder, il) = NewTestMethod();
        var cf = builder.DefineField("Arr", typeof(int[]), FieldAttributes.Public);
        il
            .NewArray<int>(3, out var arrLocal)
            .SetField(cf, i => i.LoadLocal(arrLocal))
            .Return();
        var obj = InvokeTestMethod(builder);
        Assert.That(GetField(obj, "Arr"), Is.EquivalentTo(new int[3]));
    }

    [Test]
    public void NewArray_T_with_local_store_test()
    {
        var testCallBack = BuildTest<int[]>([typeof(int)], il => il.LoadArg1().NewArray<int>(out var arr).LoadLocal(arr).Return());
        Assert.That(testCallBack.Invoke([3]), Is.EquivalentTo(new int[3]));
        Assert.That(testCallBack.Invoke([5]), Is.EquivalentTo(new int[5]));
        Assert.That(testCallBack.Invoke([7]), Is.EquivalentTo(new int[7]));
    }

    [Test]
    public void NewArray_T_test()
    {
        var testCallBack = BuildTest<int[]>([typeof(int)], il => il.LoadArg1().NewArray<int>().Return());
        Assert.That(testCallBack.Invoke([3]), Is.EquivalentTo(new int[3]));
        Assert.That(testCallBack.Invoke([5]), Is.EquivalentTo(new int[5]));
        Assert.That(testCallBack.Invoke([7]), Is.EquivalentTo(new int[7]));
    }

    [Test]
    public void NewArray_with_local_store_test()
    {
        var testCallBack = BuildTest<int[]>([typeof(int)], il => il.LoadArg1().NewArray(typeof(int), out var arr).LoadLocal(arr).Return());
        Assert.That(testCallBack.Invoke([3]), Is.EquivalentTo(new int[3]));
        Assert.That(testCallBack.Invoke([5]), Is.EquivalentTo(new int[5]));
        Assert.That(testCallBack.Invoke([7]), Is.EquivalentTo(new int[7]));
    }

    [Test]
    public void NewArray_test()
    {
        var testCallBack = BuildTest<int[]>([typeof(int)], il => il.LoadArg1().NewArray(typeof(int)).Return());
        Assert.That(testCallBack.Invoke([3]), Is.EquivalentTo(new int[3]));
        Assert.That(testCallBack.Invoke([5]), Is.EquivalentTo(new int[5]));
        Assert.That(testCallBack.Invoke([7]), Is.EquivalentTo(new int[7]));
    }
}