// ILGeneratorExtensions_ControlBlocks_Tests.cs
//
// This file is part of Morgan's CLR Advanced Runtime (MCART)
//
// Author(s):
//      César Andrés Morgan <xds_xps_ivx@hotmail.com>
//
// Released under the MIT License (MIT)
// Copyright © 2011 - 2023 César Andrés Morgan
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

using NUnit.Framework;
using System.Reflection;
using TheXDS.MCART.Types.Extensions;
using static TheXDS.MCART.Types.Extensions.ILGeneratorExtensions;

namespace TheXDS.MCART.TypeFactory.Tests.Types.Extensions;

public partial class ILGeneratorExtensions_Tests : TypeFactoryTestClassBase
{
    [Test]
    public void Simple_If_test()
    {
        var (builder, il) = NewTestMethod();
        var p = builder.DefineField("Triggered", typeof(bool), FieldAttributes.Public);
        il.LoadConstant(true).If(il => il.LoadArg0().LoadConstant(true).StoreField(p)).Return();
        var obj = InvokeTestMethod(builder);
        Assert.That(GetField(obj, "Triggered"), Is.EqualTo(true));
    }

    [TestCase(true, true, false)]
    [TestCase(false, false, true)]
    public void If_Else_test(bool condition, bool trueBranch, bool falseBranch)
    {
        var (builder, il) = NewTestMethod();
        var tf = builder.DefineField("TrueTriggered", typeof(bool), FieldAttributes.Public);
        var ff = builder.DefineField("FalseTriggered", typeof(bool), FieldAttributes.Public);
        il
            .LoadConstant(condition)
            .If(il => il.LoadArg0().LoadConstant(true).StoreField(tf), il => il.LoadArg0().LoadConstant(true).StoreField(ff))
            .Return();
        var obj = InvokeTestMethod(builder);
        Assert.That(GetField(obj, "TrueTriggered"), Is.EqualTo(trueBranch));
        Assert.That(GetField(obj, "FalseTriggered"), Is.EqualTo(falseBranch));
    }

    [TestCase(true, true, false)]
    [TestCase(false, false, true)]
    public void If_Else_with_optimizations_test(bool condition, bool trueBranch, bool falseBranch)
    {
        var (builder, il) = NewTestMethod();
        var tf = builder.DefineField("TrueTriggered", typeof(bool), FieldAttributes.Public);
        var ff = builder.DefineField("FalseTriggered", typeof(bool), FieldAttributes.Public);
        il
            .LoadArg0().LoadConstant(true)
            .LoadConstant(condition)
            .If(il => il.StoreField(tf), il => il.StoreField(ff))
            .Return();
        var obj = InvokeTestMethod(builder);
        Assert.That(GetField(obj, "TrueTriggered"), Is.EqualTo(trueBranch));
        Assert.That(GetField(obj, "FalseTriggered"), Is.EqualTo(falseBranch));
    }

    [TestCase(2, 1)]
    [TestCase(3, 3)]
    [TestCase(4, 6)]
    [TestCase(5, 10)]
    public void For_block_test(int limit, int expectedValue)
    {
        var (builder, il) = NewTestMethod();       
        var cf = builder.DefineField("Counter", typeof(int), FieldAttributes.Public);
        var j = il.DeclareLocal(typeof(int));

        il.For(j, 0,
            il => il.LoadLocal(j).LoadConstant(limit).CompareLessThan(),
            il => il.Increment(j),
            (il, c, _, _) => il
                .LoadArg0()
                .GetField(cf)
                .LoadLocal(c)
                .Add()
                .StoreField(cf)
            ).Return();

        var obj = InvokeTestMethod(builder);
        Assert.That(GetField(obj, "Counter"), Is.EqualTo(expectedValue));
    }

    [Test]
    public void Try_block_without_exceptions()
    {
        var (builder, il) = NewTestMethod();
        var tf = builder.DefineField("TryTriggered", typeof(bool), FieldAttributes.Public);
        var cf = builder.DefineField("CatchTriggered", typeof(bool), FieldAttributes.Public);
        il.TryCatch(
            (il, _) => il.SetField(tf, il => il.LoadConstant(true))
            , new[] { new KeyValuePair<Type, TryBlock>(typeof(Exception),
            (il, _) => il.SetField(cf, il => il.LoadConstant(true))) }).Return();
        var obj = InvokeTestMethod(builder);
        Assert.That(GetField(obj, "TryTriggered"), Is.EqualTo(true));
        Assert.That(GetField(obj, "CatchTriggered"), Is.EqualTo(false));
    }

    [Test]
    public void Try_block_when_exception_is_thrown()
    {
        var (builder, il) = NewTestMethod();
        var tf = builder.DefineField("TryTriggered", typeof(bool), FieldAttributes.Public);
        var cf = builder.DefineField("CatchTriggered", typeof(bool), FieldAttributes.Public);
        il.TryCatch(
            (il, _) => {
                il.ThrowException(typeof(Exception));
                il.SetField(tf, il => il.LoadConstant(true));
            }
            , new[] { new KeyValuePair<Type, TryBlock>(typeof(Exception),
            (il, _) => il.SetField(cf, il => il.LoadConstant(true))) }).Return();
        var obj = InvokeTestMethod(builder);
        Assert.That(GetField(obj, "TryTriggered"), Is.EqualTo(false));
        Assert.That(GetField(obj, "CatchTriggered"), Is.EqualTo(true));
    }

    [Test]
    public void Try_Finally_block_without_exceptions()
    {
        var (builder, il) = NewTestMethod();
        var tf = builder.DefineField("TryTriggered", typeof(bool), FieldAttributes.Public);
        var cf = builder.DefineField("FinallyTriggered", typeof(bool), FieldAttributes.Public);
        il.TryFinally(
            (il, _) => il.SetField(tf, il => il.LoadConstant(true)),
            il => il.SetField(cf, il => il.LoadConstant(true))).Return();
        var obj = InvokeTestMethod(builder);
        Assert.That(GetField(obj, "TryTriggered"), Is.EqualTo(true));
        Assert.That(GetField(obj, "FinallyTriggered"), Is.EqualTo(true));
    }

    [Test]
    public void Try_block_with_early_exit_exceptions()
    {
        var (builder, il) = NewTestMethod();
        var tf = builder.DefineField("TryTriggered", typeof(bool), FieldAttributes.Public);
        var cf = builder.DefineField("CatchTriggered", typeof(bool), FieldAttributes.Public);
        il.TryCatch(
            (il, exit) => il.Leave(exit).SetField(tf, il => il.LoadConstant(true))
            , new[] { new KeyValuePair<Type, TryBlock>(typeof(Exception),
            (il, _) => il.SetField(cf, il => il.LoadConstant(true))) }).Return();
        var obj = InvokeTestMethod(builder);
        Assert.That(GetField(obj, "TryTriggered"), Is.EqualTo(false));
        Assert.That(GetField(obj, "CatchTriggered"), Is.EqualTo(false));
    }

    [Test]
    public void Try_catch_finally_block_without_exceptions()
    {
        var (builder, il) = NewTestMethod();
        var tf = builder.DefineField("TryTriggered", typeof(bool), FieldAttributes.Public);
        var cf = builder.DefineField("CatchTriggered", typeof(bool), FieldAttributes.Public);
        var ff = builder.DefineField("FinallyTriggered", typeof(bool), FieldAttributes.Public);
        il.TryCatchFinally(
            (il, _) => il.SetField(tf, il => il.LoadConstant(true))
            , new[] { new KeyValuePair<Type, TryBlock>(typeof(Exception),
            (il, _) => il.SetField(cf, il => il.LoadConstant(true))) },
            il => il.SetField(ff, il => il.LoadConstant(true))).Return();
        var obj = InvokeTestMethod(builder);
        Assert.That(GetField(obj, "TryTriggered"), Is.EqualTo(true));
        Assert.That(GetField(obj, "CatchTriggered"), Is.EqualTo(false));
        Assert.That(GetField(obj, "FinallyTriggered"), Is.EqualTo(true));
    }

    [Test]
    public void Try_catch_finally_block_when_exception_is_thrown()
    {
        var (builder, il) = NewTestMethod();
        var tf = builder.DefineField("TryTriggered", typeof(bool), FieldAttributes.Public);
        var cf = builder.DefineField("CatchTriggered", typeof(bool), FieldAttributes.Public);
        var ff = builder.DefineField("FinallyTriggered", typeof(bool), FieldAttributes.Public);
        il.TryCatchFinally(
            (il, _) => {
                il.ThrowException(typeof(Exception));
                il.SetField(tf, il => il.LoadConstant(true));
            }
            , new[] { new KeyValuePair<Type, TryBlock>(typeof(Exception),
            (il, _) => il.SetField(cf, il => il.LoadConstant(true))) },
            il => il.SetField(ff, il => il.LoadConstant(true))).Return();
        var obj = InvokeTestMethod(builder);
        Assert.That(GetField(obj, "TryTriggered"), Is.EqualTo(false));
        Assert.That(GetField(obj, "CatchTriggered"), Is.EqualTo(true));
        Assert.That(GetField(obj, "FinallyTriggered"), Is.EqualTo(true));
    }
}
