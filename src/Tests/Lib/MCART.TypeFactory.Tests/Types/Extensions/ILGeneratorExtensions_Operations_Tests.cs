// ILGeneratorExtensions_Operations_Tests.cs
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

using System.Reflection;
using System.Reflection.Emit;
using TheXDS.MCART.Types.Extensions;
using static TheXDS.MCART.Types.Extensions.ILGeneratorExtensions;

namespace TheXDS.MCART.TypeFactory.Tests.Types.Extensions;

public partial class ILGeneratorExtensions_Tests : TypeFactoryTestClassBase
{
    private static void CreateOperationTest(int a, int b, Func<ILGenerator, ILGenerator> simpleOp, int expectedResult)
    {
        var (builder, ilGn) = NewTestMethod(simpleOp.Method.Name);
        var r = builder.DefineField("Result", typeof(int), FieldAttributes.Public);
        ilGn.LoadArg0()
            .LoadConstant(a)
            .LoadConstant(b);
        simpleOp.Invoke(ilGn)            
            .StoreField(r).Return();
        var obj = InvokeTestMethod(builder, simpleOp.Method.Name);
        Assert.That(GetField(obj, "Result"), Is.EqualTo(expectedResult));
    }

    [Test]
    public void Nop_test()
    {
        var (builder, ilGn) = NewTestMethod();
        ilGn.Nop().Return();
        Assert.That(() => _ = InvokeTestMethod(builder), Throws.Nothing);
    }

    [TestCase(1, 1, 2)]
    [TestCase(1, 2, 3)]
    [TestCase(2, 2, 4)]
    [TestCase(5, 5, 10)]
    [TestCase(10, 14, 24)]
    [TestCase(13, 19, 32)]
    public void Add_test(int a, int b, int r)
    {
        CreateOperationTest(a, b, ILGeneratorExtensions.Add, r);
    }

    [TestCase(1, 1, 0)]
    [TestCase(1, 2, -1)]
    [TestCase(2, 2, 0)]
    [TestCase(5, 5, 0)]
    [TestCase(10, 14, -4)]
    [TestCase(13, 19, -6)]
    public void Subtract_test(int a, int b, int r)
    {
        CreateOperationTest(a, b, ILGeneratorExtensions.Subtract, r);
    }

    [TestCase(1, 1, 1)]
    [TestCase(1, 2, 2)]
    [TestCase(2, 2, 4)]
    [TestCase(5, 5, 25)]
    [TestCase(10, 14, 140)]
    [TestCase(13, 19, 247)]
    public void Multiply_test(int a, int b, int r)
    {
        CreateOperationTest(a, b, ILGeneratorExtensions.Multiply, r);
    }

    [TestCase(1, 1, 1)]
    [TestCase(1, 2, 0)]
    [TestCase(2, 2, 1)]
    [TestCase(5, 5, 1)]
    [TestCase(10, 2, 5)]
    [TestCase(13, 1, 13)]
    public void Divide_test(int a, int b, int r)
    {
        CreateOperationTest(a, b, ILGeneratorExtensions.Divide, r);
    }

    [TestCase(1, 1, 0)]
    [TestCase(1, 2, 1)]
    [TestCase(2, 2, 0)]
    [TestCase(5, 5, 0)]
    [TestCase(14, 10, 4)]
    [TestCase(19, 13, 6)]
    public void Remainder_test(int a, int b, int r)
    {
        CreateOperationTest(a, b, ILGeneratorExtensions.Remainder, r);
    }

    [Test]
    public void StoreInt8_test()
    {
        CreateForBlockUsingArray<sbyte>(ILGeneratorExtensions.StoreInt8Element, il => il.CastAsSByte());
    }

    [Test]
    public void StoreInt16_test()
    {
        CreateForBlockUsingArray<short>(ILGeneratorExtensions.StoreInt16Element, il => il.CastAsShort());
    }

    [Test]
    public void StoreInt32_test()
    {
        CreateForBlockUsingArray<int>(ILGeneratorExtensions.StoreInt32Element, il => il.CastAsInt());
    }

    [Test]
    public void StoreInt64_test()
    {
        CreateForBlockUsingArray<long>(ILGeneratorExtensions.StoreInt64Element, il => il.CastAsLong());
    }

    [Test]
    public void StoreUInt8_test()
    {
        CreateForBlockUsingArray<byte>(ILGeneratorExtensions.StoreInt8Element, il => il.CastAsByte());
    }

    [Test]
    public void StoreUInt16_test()
    {
        CreateForBlockUsingArray<ushort>(ILGeneratorExtensions.StoreInt16Element, il => il.CastAsUShort());
    }

    [Test]
    public void StoreUInt32_test()
    {
        CreateForBlockUsingArray<uint>(ILGeneratorExtensions.StoreInt32Element, il => il.CastAsUInt());
    }

    [Test]
    public void StoreUInt64_test()
    {
        CreateForBlockUsingArray<ulong>(ILGeneratorExtensions.StoreInt64Element, il => il.CastAsULong());
    }

    [Test]
    public void StoreFloat_test()
    {
        CreateForBlockUsingArray<float>(ILGeneratorExtensions.StoreFloatElement, il => il.CastAsFloat());
    }

    [Test]
    public void StoreDouble_test()
    {
        CreateForBlockUsingArray<double>(ILGeneratorExtensions.StoreDoubleElement, il => il.CastAsDouble());
    }

    [Test]
    public void StoreRefElement_test()
    {
        var testCallBack = BuildTest<Random[]>(new[] { typeof(int) }, il => il
            .LoadArg1()
            .NewArray<Random>(out var arr)
            .For(0, 4,
            (il, j, b, n) =>
            {
                il.LoadLocal(arr).LoadLocal(j)
                .NewObject<Random>(Type.EmptyTypes)
                .StoreRefElement();
            })
            .LoadLocal(arr).Return(), $"for_test_{5}_elements");

        Random[] result = (Random[])testCallBack.Invoke(new object[] { 5 })!;
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Length, Is.EqualTo(5));
        foreach (var j in (Random[])testCallBack.Invoke(new object[] { 5 })!)
        {
            Assert.That(j, Is.InstanceOf<Random>());
        }
    }

    private static void CreateForBlockUsingArray<T>(Func<ILGenerator, ILGenerator> operation, Action<ILGenerator>? convert = null) where T : unmanaged
    {
        var testCallBack = BuildTest<T[]>(new[] { typeof(int) }, il => il
            .LoadArg1()
            .NewArray<T>(out var arr)
            .For(0, 4,
            (il, j, b, n) =>
            {
                il.LoadLocal(arr).LoadLocal(j).Duplicate();
                convert?.Invoke(il);
                operation.Invoke(il);
            })
            .LoadLocal(arr).Return(), $"for_test_{5}_elements");
        Assert.That(testCallBack.Invoke(new object[] { 5 }), Is.EquivalentTo(Enumerable.Range(0, 5)));
    }
}
