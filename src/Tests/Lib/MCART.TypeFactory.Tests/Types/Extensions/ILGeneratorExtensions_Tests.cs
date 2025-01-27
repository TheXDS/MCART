// ILGeneratorExtensions_Tests.cs
//
// This file is part of Morgan's CLR Advanced Runtime (MCART)
//
// Author(s):
//      César Andrés Morgan <xds_xps_ivx@hotmail.com>
//
// Released under the MIT License (MIT)
// Copyright © 2011 - 2025 César Andrés Morgan
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

using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using TheXDS.MCART.Types.Extensions;
using static TheXDS.MCART.Types.Extensions.ILGeneratorExtensions;

namespace TheXDS.MCART.TypeFactory.Tests.Types.Extensions;

public partial class ILGeneratorExtensions_Tests : TypeFactoryTestClassBase
{
    private static Func<object[], object?> BuildTest<TResult>(Type[] parameters, Action<ILGenerator> buildCallback, [CallerMemberName] string testName = null!)
    {
        var t = Factory.NewClass($"{testName}_Class");
        buildCallback.Invoke(t.DefineMethod<TResult>(testName, parameters).GetILGenerator());
        var obj = t.New();
        var method = obj.GetType().GetMethod(testName)!;
        return p => method.Invoke(obj, p);
    }

    private static string IfElse_ExpectedSample(int input)
    {
        if (input < 1)
            return "A";
        else
            return "B";
    }

    private static string If_ExpectedSample(int input)
    {
        var result = "X";
        if (input < 1) result = "A";
        return result;
    }

    [Test]
    public void If_else_control_block_test()
    {
        var testCallBack = BuildTest<string>(new[] { typeof(int) }, il =>
        {
            il
            .LoadConstant(1)
            .LoadArg1()
            .CompareGreaterThan()
            .If(
                il => il.LoadConstant("A"),
                il => il.LoadConstant("B"))
            .Return();
        });

        Assert.That(testCallBack.Invoke(new object[] { 0 }), Is.EqualTo(IfElse_ExpectedSample(0)));
        Assert.That(testCallBack.Invoke(new object[] { 1 }), Is.EqualTo(IfElse_ExpectedSample(1)));
        Assert.That(testCallBack.Invoke(new object[] { 2 }), Is.EqualTo(IfElse_ExpectedSample(2)));
    }

    [Test]
    public void If_control_block_test()
    {
        var testCallBack = BuildTest<string>(new[] { typeof(int) }, il =>
        {
            il
            .LoadConstant("X")
            .StoreNewLocal<string>(out var local1)
            .LoadConstant(1)
            .LoadArg1()
            .CompareGreaterThan()
            .If(il => il.LoadConstant("A").StoreLocal(local1))
            .LoadLocal(local1)
            .Return();
        });

        Assert.That(testCallBack.Invoke(new object[] { 0 }), Is.EqualTo(If_ExpectedSample(0)));
        Assert.That(testCallBack.Invoke(new object[] { 1 }), Is.EqualTo(If_ExpectedSample(1)));
        Assert.That(testCallBack.Invoke(new object[] { 2 }), Is.EqualTo(If_ExpectedSample(2)));
    }

    [Test]
    public void For_control_block_with_constant_start_value()
    {
        var testCallBack = BuildTest<int[]>(new[] { typeof(int) }, il => il
            .LoadArg1()
            .NewArray<int>(out var arr)
            .For(0,
            j => il.LoadLocal(j).LoadLocal(arr).GetArrayLength().CompareLessThan(),
            (il, j, b, n) => il.LoadLocal(arr).LoadLocal(j).Duplicate().StoreInt32Element())
            .LoadLocal(arr).Return());
        Assert.That(testCallBack.Invoke(new object[] { 3 }), Is.EquivalentTo(new int[] { 0, 1, 2 }));
        Assert.That(testCallBack.Invoke(new object[] { 10 }), Is.EquivalentTo(new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }));
    }

    [TestCase(3)]
    [TestCase(5)]
    [TestCase(7)]
    public void For_control_block_with_constant_start_and_end_values(int elements)
    {
        var testCallBack = BuildTest<int[]>(new[] { typeof(int) }, il => il
            .LoadArg1()
            .NewArray<int>(out var arr)
            .For(0, elements - 1,
            (il, j, b, n) => il.LoadLocal(arr).LoadLocal(j).Duplicate().StoreInt32Element())
            .LoadLocal(arr).Return(), $"for_test_{elements}_elements");
        Assert.That(testCallBack.Invoke(new object[] { elements }), Is.EquivalentTo(Enumerable.Range(0, elements)));
    }

    [TestCase(3)]
    [TestCase(5)]
    [TestCase(7)]
    public void For_control_block_with_Range(int elements)
    {
        var testCallBack = BuildTest<int[]>(new[] { typeof(int) }, il => il
            .LoadArg1()
            .NewArray<int>(out var arr)
            .For(new MCART.Types.Range<int>(0, elements, true, false),
            (il, j, b, n) => il.LoadLocal(arr).LoadLocal(j).Duplicate().StoreInt32Element())
            .LoadLocal(arr).Return(), $"for_test_{elements}_elements");

        Assert.That(testCallBack.Invoke(new object[] { elements }), Is.EquivalentTo(Enumerable.Range(0, elements)));
    }

    [TestCase(3)]
    [TestCase(5)]
    [TestCase(7)]
    public void For_control_block_with_Range_max_inclusive(int elements)
    {
        var testCallBack = BuildTest<int[]>(new[] { typeof(int) }, il => il
            .LoadArg1()
            .NewArray<int>(out var arr)
            .For(new MCART.Types.Range<int>(0, elements, true, true),
            (il, j, b, n) => il.LoadLocal(arr).LoadLocal(j).Duplicate().StoreInt32Element())
            .LoadLocal(arr).Return(), $"for_test_{elements}_elements");

        Assert.That(testCallBack.Invoke(new object[] { elements + 1 }), Is.EquivalentTo(Enumerable.Range(0, elements + 1)));
    }
}
