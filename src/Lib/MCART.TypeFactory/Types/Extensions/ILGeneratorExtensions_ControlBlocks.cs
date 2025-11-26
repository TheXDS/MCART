/*
ILGeneratorExtensions_ControlBlocks.cs

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
using System.Reflection.Emit;
using TheXDS.MCART.Helpers;
using static System.Reflection.Emit.OpCodes;
using Op = System.Reflection.Emit.OpCodes;

namespace TheXDS.MCART.Types.Extensions;

public static partial class ILGeneratorExtensions
{
    /// <summary>
    /// Inserts a structured <see langword="if"/> block into the Microsoft®
    /// IL instruction sequence.
    /// </summary>
    /// <param name="ilGen">
    /// ILGenerator where the <see langword="if"/> block will be inserted.
    /// </param>
    /// <param name="trueBranch">
    /// Action that defines the instructions to insert when the top value
    /// on the stack evaluates to <see langword="true"/>.
    /// </param>
    /// <returns>
    /// The same instance as <paramref name="ilGen"/>, enabling fluent
    /// syntax.
    /// </returns>
    public static ILGenerator If(this ILGenerator ilGen, Action<ILGenerator> trueBranch)
    {
        ilGen.BranchFalseNewLabel(out Label endIf);
        trueBranch(ilGen);
        return ilGen.PutLabel(endIf);
    }

    /// <summary>
    /// Inserts a structured <see langword="if"/> block into the Microsoft®
    /// IL instruction sequence.
    /// </summary>
    /// <param name="ilGen">
    /// ILGenerator where the <see langword="if"/> block will be inserted.
    /// </param>
    /// <param name="trueBranch">
    /// Action that defines the instructions to insert when the top value
    /// on the stack evaluates to <see langword="true"/>.
    /// </param>
    /// <param name="falseBranch">
    /// Action that defines the instructions to insert when the top value
    /// evaluates to <see langword="false"/>.
    /// </param>
    /// <returns>
    /// The same instance as <paramref name="ilGen"/>, enabling fluent
    /// syntax.
    /// </returns>
    public static ILGenerator If(this ILGenerator ilGen, Action<ILGenerator> trueBranch, Action<ILGenerator> falseBranch)
    {
        Label endIf = ilGen.DefineLabel();
        Label @else = ilGen.DefineLabel();
        ilGen.Emit(Brfalse, @else);
        trueBranch(ilGen);
        ilGen.Emit(Br, endIf);
        ilGen.MarkLabel(@else);
        falseBranch(ilGen);
        ilGen.MarkLabel(endIf);
        return ilGen;
    }

    /// <summary>
    /// Inserts a structured <see langword="for"/> block into the Microsoft®
    /// IL instruction sequence.
    /// </summary>
    /// <param name="ilGen">
    /// ILGenerator where the <see langword="for"/> block will be inserted.
    /// </param>
    /// <param name="accumulator">
    /// Local variable used as the accumulator for the <see langword="for"/>
    /// block.
    /// </param>
    /// <param name="initialValue">
    /// Value used to initialize the accumulator.
    /// </param>
    /// <param name="condition">
    /// Action that defines the initial evaluation of the <see langword="for"/>
    /// block.
    /// </param>
    /// <param name="incrementor">
    /// Action that defines the increment operation to execute at the end
    /// of each <see langword="for"/> iteration.
    /// </param>
    /// <param name="forBlock">
    /// Action that defines the statements to execute inside the <see
    /// langword="for"/> block. The action receives a parameter that can be
    /// used as the exit label to terminate the block.
    /// </param>
    /// <returns>
    /// The same instance as <paramref name="ilGen"/>, enabling fluent
    /// syntax.
    /// </returns>
    public static ILGenerator For(this ILGenerator ilGen, LocalBuilder accumulator, object? initialValue, Action<ILGenerator> condition, Action<ILGenerator> incrementor, ForBlock forBlock)
    {
        Label next = ilGen.DefineLabel();
        ilGen
            .InitLocal(accumulator, initialValue)
            .InsertNewLabel(out Label @for);
        condition(ilGen);
        ilGen.BranchFalseNewLabel(out Label endFor);
        forBlock(ilGen, accumulator, endFor, next);
        ilGen.PutLabel(next);
        incrementor(ilGen);
        return ilGen
            .Branch(@for)
            .PutLabel(endFor);
    }

    /// <summary>
    /// Inserts a structured <see langword="for"/> block into the Microsoft®
    /// IL instruction sequence.
    /// </summary>
    /// <param name="ilGen">
    /// ILGenerator where the <see langword="for"/> block will be inserted.
    /// </param>
    /// <param name="initialValue">
    /// Initial value of the accumulator for the <see langword="for"/> block.
    /// </param>
    /// <param name="condition">
    /// Action that defines the initial evaluation of the <see langword="for"/> 
    /// block. The action receives a reference to the <see cref="FieldBuilder"/> 
    /// of the accumulator.
    /// </param>
    /// <param name="incrementor">
    /// Action that defines the increment operation to execute at the end of 
    /// each <see langword="for"/> iteration. The action receives a reference 
    /// to the <see cref="FieldBuilder"/> of the accumulator.
    /// </param>
    /// <param name="forBlock">
    /// Action that defines the statements to execute inside the <see
    /// langword="for"/> block. The action receives a reference to the <see
    /// cref="FieldBuilder"/> of the accumulator and a parameter that can be 
    /// used as the exit label to terminate the block.
    /// </param>
    /// <returns>
    /// The same instance as <paramref name="ilGen"/>, enabling fluent syntax.
    /// </returns>
    public static ILGenerator For<T>(this ILGenerator ilGen, T initialValue, Action<LocalBuilder> condition, Action<LocalBuilder> incrementor, ForBlock forBlock)
    {
        Label next = ilGen.DefineLabel();
        ilGen
            .InitNewLocal(initialValue, out LocalBuilder? acc)
            .InsertNewLabel(out Label @for);
        condition(acc);
        ilGen.BranchFalseNewLabel(out Label endFor);
        forBlock(ilGen, acc, endFor, next);
        ilGen.PutLabel(next);
        incrementor(acc);
        return ilGen
            .Branch(@for)
            .PutLabel(endFor);
    }

    /// <summary>
    /// Inserts a structured <see langword="for"/> block into the Microsoft®
    /// IL instruction sequence.
    /// </summary>
    /// <param name="ilGen">
    /// ILGenerator where the <see langword="for"/> block will be inserted.
    /// </param>
    /// <param name="initialValue">
    /// Initial value of the accumulator for the <see langword="for"/> block.
    /// </param>
    /// <param name="condition">
    /// Action that defines the initial evaluation of the <see langword="for"/> 
    /// block. The action receives a reference to the <see cref="FieldBuilder"/> 
    /// of the accumulator.
    /// </param>
    /// <param name="forBlock">
    /// Action that defines the statements to execute inside the <see
    /// langword="for"/> block. The action receives a reference to the <see
    /// cref="FieldBuilder"/> of the accumulator and a parameter that can be 
    /// used as the exit label to terminate the block.
    /// </param>
    /// <returns>
    /// The same instance as <paramref name="ilGen"/>, enabling fluent syntax.
    /// </returns>
    public static ILGenerator For<T>(this ILGenerator ilGen, T initialValue, Action<LocalBuilder> condition, ForBlock forBlock)
    {
        return For(ilGen, initialValue, condition, accumulator => ilGen.LoadLocal(accumulator).LoadConstant(1).Add().StoreLocal(accumulator), forBlock);
    }

    /// <summary>
    /// Inserts a simple structured <see langword="for"/> block with an
    /// <see cref="int"/> accumulator into the Microsoft® Intermediate
    /// Language (MSIL) instruction sequence.
    /// </summary>
    /// <param name="ilGen">
    /// The IL instruction sequence into which the <see langword="for"/> block
    /// will be inserted.
    /// </param>
    /// <param name="startValue">
    /// The inclusive starting value of the accumulator.
    /// </param>
    /// <param name="endValue">
    /// The inclusive ending value of the accumulator.
    /// </param>
    /// <param name="forBlock">
    /// Action that defines the instructions to execute inside the
    /// <see langword="for"/> block. It receives the <see cref="LocalBuilder"/>
    /// of the accumulator and a label that can be used as the exit label
    /// to break the loop.
    /// </param>
    /// <returns>
    /// The same <paramref name="ilGen"/> instance, enabling fluent syntax.
    /// </returns>
    public static ILGenerator For(this ILGenerator ilGen, int startValue, int endValue, ForBlock forBlock)
    {
        Label next = ilGen.DefineLabel();
        ilGen
            .InitNewLocal(startValue, out LocalBuilder? acc)
            .InsertNewLabel(out Label @for)
            .LoadLocal(acc)
            .LoadConstant(endValue)
            .BranchGreaterThanNewLabel(out Label endFor);
        forBlock(ilGen, acc, endFor, next);
        return ilGen
            .PutLabel(next)
            .LoadLocal(acc)
            .OneLiner(Ldc_I4_1)
            .Add()
            .StoreLocal(acc)
            .Branch(@for)
            .PutLabel(endFor);
    }

    /// <summary>
    /// Inserts a simple structured <see langword="for"/> block with an
    /// <see cref="int"/> accumulator into the Microsoft® Intermediate
    /// Language (MSIL) instruction sequence.
    /// </summary>
    /// <param name="ilGen">
    /// The IL instruction sequence into which the <see langword="for"/> block
    /// will be inserted.
    /// </param>
    /// <param name="range">
    /// The range of values to use for the accumulator.
    /// </param>
    /// <param name="forBlock">
    /// Action that defines the instructions to execute inside the
    /// <see langword="for"/> block. It receives the <see cref="LocalBuilder"/>
    /// of the accumulator and a label that can be used as the exit label
    /// to break the loop.
    /// </param>
    /// <returns>
    /// The same <paramref name="ilGen"/> instance, enabling fluent syntax.
    /// </returns>
    public static ILGenerator For(this ILGenerator ilGen, Range<int> range, ForBlock forBlock)
    {
        Label next = ilGen.DefineLabel();
        Label endFor = ilGen.DefineLabel();
        ilGen
            .InitNewLocal(range.MinInclusive ? range.Minimum : range.Minimum + 1, out LocalBuilder? acc)
            .InsertNewLabel(out Label @for)
            .LoadLocal(acc)
            .LoadConstant(range.Maximum);

        if (range.MaxInclusive)
        {
            ilGen.BranchGreaterThan(endFor);
        }
        else
        {
            ilGen.BranchGreaterThanOrEqual(endFor);
        }
        forBlock(ilGen, acc, endFor, next);

        return ilGen
            .PutLabel(next)
            .LoadLocal(acc)
            .OneLiner(Ldc_I4_1)
            .Add()
            .StoreLocal(acc)
            .Branch(@for)
            .PutLabel(endFor);
    }

    /// <summary>
    /// Inserts a structured <see langword="foreach"/> block into the Microsoft
    /// Intermediate Language (MSIL) sequence.
    /// </summary>
    /// <param name="ilGen">
    /// The instruction sequence into which the <see langword="foreach"/> block
    /// will be inserted.
    /// </param>
    /// <param name="foreachBlock">
    /// Delegate that defines the actions to execute inside the
    /// <see langword="foreach"/> block. The action receives a reference to the
    /// <see cref="LocalBuilder"/> representing the current element.
    /// </param>
    /// <returns>
    /// The same instance as <paramref name="ilGen"/>, enabling fluent syntax.
    /// </returns>
    /// <remarks>
    /// This instruction expects the value on the top of the stack to implement
    /// <see cref="IEnumerable{T}"/>.
    /// </remarks>
    public static ILGenerator ForEach<T>(this ILGenerator ilGen, ForEachBlock foreachBlock)
    {
        LocalBuilder itm = ilGen.DeclareLocal(typeof(T));
        return ilGen
            .Call<IEnumerable<T>, Func<IEnumerator<T>>>(p => p.GetEnumerator)
            .StoreNewLocal<IEnumerator<T>>(out LocalBuilder? enumerator)
            .Using(enumerator, (ilGen, _, leaveForEach) =>
            {
                ilGen
                    .BranchNewLabel(out Label moveNext)
                    .InsertNewLabel(out Label loopStart)
                    .LoadLocal(enumerator)
                    .Call(ReflectionHelpers.GetProperty<IEnumerator<T>>(p => p.Current).GetMethod!)
                    .StoreLocal(itm);
                foreachBlock.Invoke(ilGen, itm, leaveForEach, moveNext);
                ilGen
                    .PutLabel(moveNext)
                    .LoadLocal(enumerator)
                    .Call<IEnumerator, Func<bool>>(p => p.MoveNext)
                    .BranchTrue(loopStart);
            });
    }

    /// <summary>
    /// Inserts a structured <see langword="using"/> block into the Microsoft
    /// Intermediate Language (MSIL) sequence.
    /// </summary>
    /// <param name="ilGen">
    /// The instruction sequence into which the <see langword="using"/> block
    /// will be inserted.
    /// </param>
    /// <param name="disposable">
    /// Reference to a <see cref="LocalBuilder"/> holding the disposable
    /// instance used within the <see langword="using"/> block.
    /// </param>
    /// <param name="usingBlock">
    /// Delegate that defines the actions to execute inside the
    /// <see langword="using"/> block.
    /// </param>
    /// <returns>
    /// The same instance as <paramref name="ilGen"/>, enabling fluent syntax.
    /// </returns>
    public static ILGenerator Using(this ILGenerator ilGen, LocalBuilder disposable, UsingBlock usingBlock)
    {
        return ilGen.TryFinally((ilGen, leaveTry) => usingBlock.Invoke(ilGen, disposable, leaveTry), (ilGen, leaveFinally) =>
        {
            ilGen
            .LoadLocal(disposable)
            .BranchFalse(leaveFinally)
            .Dispose(disposable);
        });
    }

    /// <summary>
    /// Inserts a structured <see langword="using"/> block into the Microsoft
    /// Intermediate Language (MSIL) sequence.
    /// </summary>
    /// <typeparam name="T">
    /// Type of disposable object to instantiate for use within the
    /// <see langword="using"/> block.
    /// </typeparam>
    /// <param name="ilGen">
    /// The instruction sequence into which the <see langword="using"/> block
    /// will be inserted.
    /// </param>
    /// <param name="usingBlock">
    /// Delegate that defines the actions to execute inside the
    /// <see langword="using"/> block.
    /// </param>
    /// <returns>
    /// The same instance as <paramref name="ilGen"/>, enabling fluent syntax.
    /// </returns>
    public static ILGenerator Using<T>(this ILGenerator ilGen, UsingBlock usingBlock) where T : IDisposable, new()
    {
        ilGen.NewObject<T>().StoreNewLocal<T>(out LocalBuilder? disposable);
        return Using(ilGen, disposable, usingBlock);
    }

    /// <summary>
    /// Inserts an unconditional control‑transfer instruction that exits a
    /// protected code region (typically a <see langword="try"/> or
    /// <see langword="catch"/> block) into the Microsoft Intermediate
    /// Language (MSIL) sequence.
    /// </summary>
    /// <param name="ilGen">
    /// The instruction sequence into which the jump will be inserted.
    /// </param>
    /// <param name="label">Destination label for the jump.</param>
    /// <returns>The same <paramref name="ilGen"/> instance, enabling fluent syntax.</returns>
    public static ILGenerator Leave(this ILGenerator ilGen, Label label)
    {
        ilGen.Emit(Op.Leave, label);
        return ilGen;
    }

    /// <summary>
    /// Inserts a structured <see langword="try"/>/<see langword="finally"/> block
    /// into the Microsoft Intermediate Language (MSIL) sequence.
    /// </summary>
    /// <param name="ilGen">
    /// The instruction sequence into which the <see langword="try"/> block
    /// will be inserted.
    /// </param>
    /// <param name="tryBlock">
    /// Action that defines the instructions to emit in the <see langword="try"/>
    /// block. The action receives a label that may be used as the exit
    /// target via <see cref="Leave(ILGenerator, Label)"/>.
    /// </param>
    /// <param name="finallyBlock">
    /// Action that defines the instructions to emit in the <see langword="finally"/>
    /// block.
    /// </param>
    /// <returns>
    /// The same <paramref name="ilGen"/> instance, enabling fluent syntax.
    /// </returns>
    public static ILGenerator TryFinally(this ILGenerator ilGen, TryBlock tryBlock, FinallyBlock finallyBlock)
    {
        var leaveTry = ilGen.BeginExceptionBlock();
        tryBlock.Invoke(ilGen, leaveTry);
        ilGen.Leave(leaveTry);
        ilGen.BeginFinallyBlock();
        var leaveFinally = ilGen.DefineLabel();
        finallyBlock.Invoke(ilGen, leaveFinally);
        ilGen.PutLabel(leaveFinally);
        ilGen.Emit(Endfinally);
        ilGen.EndExceptionBlock();
        return ilGen;
    }

    /// <summary>
    /// Inserts a structured <see langword="try"/>/<see langword="catch"/> block
    /// into the Microsoft Intermediate Language (MSIL) sequence.
    /// </summary>
    /// <param name="ilGen">
    /// The instruction sequence into which the <see langword="try"/> block
    /// will be inserted.
    /// </param>
    /// <param name="tryBlock">
    /// Action that defines the instructions to emit in the <see langword="try"/>
    /// block. The action receives a label that may be used as the exit
    /// target via <see cref="Leave(ILGenerator, Label)"/>.
    /// </param>
    /// <param name="catchBlocks">
    /// Collection of <see langword="catch"/> blocks to insert. Each action
    /// receives a label that may be used as the exit target via
    /// <see cref="Leave(ILGenerator, Label)"/>.
    /// </param>
    /// <returns>
    /// The same <paramref name="ilGen"/> instance, enabling fluent syntax.
    /// </returns>
    public static ILGenerator TryCatch(this ILGenerator ilGen, TryBlock tryBlock, IEnumerable<KeyValuePair<Type, TryBlock>> catchBlocks)
    {
        InsertCatchBlocks(ilGen, tryBlock, catchBlocks);
        ilGen.EndExceptionBlock();
        return ilGen;
    }

    /// <summary>
    /// Inserts a structured <see langword="try"/>/<see langword="catch"/>
    /// /<see langword="finally"/> block into the Microsoft Intermediate
    /// Language (MSIL) sequence.
    /// </summary>
    /// <param name="ilGen">
    /// The instruction sequence into which the <see langword="try"/> block
    /// will be inserted.
    /// </param>
    /// <param name="tryBlock">
    /// Action that defines the instructions to emit in the <see langword="try"/>
    /// block. The action receives a label that may be used as the exit
    /// target via <see cref="Leave(ILGenerator, Label)"/>.
    /// </param>
    /// <param name="catchBlocks">
    /// Collection of <see langword="catch"/> blocks to insert. Each action
    /// receives a label that may be used as the exit target via
    /// <see cref="Leave(ILGenerator, Label)"/>.
    /// </param>
    /// <param name="finallyBlock">
    /// Action that defines the instructions to emit in the <see langword="finally"/>
    /// block.
    /// </param>
    /// <returns>
    /// The same <paramref name="ilGen"/> instance, enabling fluent syntax.
    /// </returns>
    public static ILGenerator TryCatchFinally(this ILGenerator ilGen, TryBlock tryBlock, IEnumerable<KeyValuePair<Type, TryBlock>> catchBlocks, Action<ILGenerator> finallyBlock)
    {
        InsertCatchBlocks(ilGen, tryBlock, catchBlocks);
        ilGen.BeginFinallyBlock();
        finallyBlock(ilGen);
        ilGen.EndExceptionBlock();
        return ilGen;
    }

    /// <summary>
    /// Emits code that creates and throws an exception with no arguments
    /// into the Microsoft Intermediate Language (MSIL) sequence.
    /// </summary>
    /// <typeparam name="T">
    /// The exception type to throw. Must have a public parameterless
    /// constructor.
    /// </typeparam>
    /// <param name="ilGen">
    /// The instruction sequence into which the throw will be emitted.
    /// </param>
    /// <returns>
    /// The same <paramref name="ilGen"/> instance, enabling fluent syntax.
    /// </returns>
    public static ILGenerator Throw<T>(this ILGenerator ilGen) where T: Exception, new()
    {
        return ilGen.NewObject<T>(Type.EmptyTypes).Throw();
    }

    /// <summary>
    /// Emits a throw instruction for an exception that is already
    /// instantiated on the stack into the Microsoft Intermediate Language
    /// (MSIL) sequence.
    /// </summary>
    /// <param name="ilGen">
    /// The instruction sequence into which the throw will be emitted.
    /// </param>
    /// <returns>
    /// The same <paramref name="ilGen"/> instance, enabling fluent syntax.
    /// </returns>
    public static ILGenerator Throw(this ILGenerator ilGen) => OneLiner(ilGen, Op.Throw);
}
