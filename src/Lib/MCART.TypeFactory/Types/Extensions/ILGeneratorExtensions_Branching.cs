/*
ILGeneratorExtensions_Branching.cs

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

using System.Reflection.Emit;
using static System.Reflection.Emit.OpCodes;

namespace TheXDS.MCART.Types.Extensions;

/// <summary>
/// Contains useful extensions for code generation via the
/// <see cref="ILGenerator"/> class.
/// </summary>
public static partial class ILGeneratorExtensions
{
    /// <summary>
    /// Defines and inserts a new label into the Microsoft Intermediate
    /// Language (MSIL) instruction sequence.
    /// </summary>
    /// <param name="ilGen">
    /// Instruction sequence into which to insert the new label.
    /// </param>
    /// <param name="label">
    /// Label that has been defined and inserted.
    /// </param>
    /// <returns>
    /// The same instance as <paramref name="ilGen"/>, enabling Fluent
    /// syntax.
    /// </returns>
    public static ILGenerator InsertNewLabel(this ILGenerator ilGen, out Label label)
    {
        return ilGen.PutLabel(label = ilGen.DefineLabel());
    }

    /// <summary>
    /// Inserts a label into the MSIL instruction sequence at the current
    /// position.
    /// </summary>
    /// <param name="ilGen">
    /// Instruction sequence into which to insert the label.
    /// </param>
    /// <param name="label">
    /// Label to be inserted.
    /// </param>
    /// <returns>
    /// The same instance as <paramref name="ilGen"/>, enabling Fluent
    /// syntax.
    /// </returns>
    public static ILGenerator PutLabel(this ILGenerator ilGen, Label label)
    {
        ilGen.MarkLabel(label);
        return ilGen;
    }

    /// <summary>
    /// Inserts an unconditional control‑transfer jump into the MSIL
    /// instruction sequence to a label.
    /// </summary>
    /// <param name="ilGen">
    /// Instruction sequence into which to insert the jump.
    /// </param>
    /// <param name="label">
    /// Label that will be the target of the jump.
    /// </param>
    /// <returns>
    /// The same instance as <paramref name="ilGen"/>, enabling Fluent
    /// syntax.
    /// </returns>
    public static ILGenerator Branch(this ILGenerator ilGen, Label label)
    {
        ilGen.Emit(Br, label);
        return ilGen;
    }

    /// <summary>
    /// Inserts a conditional control‑transfer jump into the MSIL
    /// instruction sequence to a new label, jumping if the value on
    /// top of the stack evaluates to <see langword="true"/>.
    /// </summary>
    /// <param name="ilGen">
    /// Instruction sequence into which to insert the jump.
    /// </param>
    /// <param name="label">
    /// Label that will be the target of the jump.
    /// </param>
    /// <returns>
    /// The same instance as <paramref name="ilGen"/>, enabling Fluent
    /// syntax.
    /// </returns>
    public static ILGenerator BranchTrue(this ILGenerator ilGen, Label label)
    {
        return BranchIf(ilGen, label, Brtrue);
    }

    /// <summary>
    /// Inserts a conditional control‑transfer jump into the MSIL
    /// instruction sequence to a new label, jumping if the value on
    /// top of the stack evaluates to <see langword="true"/>.
    /// </summary>
    /// <param name="ilGen">
    /// Instruction sequence into which to insert the jump.
    /// </param>
    /// <param name="label">
    /// New label that will be the target of the jump.
    /// </param>
    /// <returns>
    /// The same instance as <paramref name="ilGen"/>, enabling Fluent
    /// syntax.
    /// </returns>
    public static ILGenerator BranchTrueNewLabel(this ILGenerator ilGen, out Label label)
    {
        return BranchIfNewLabel(ilGen, out label, Brtrue);
    }

    /// <summary>
    /// Inserts a conditional control‑transfer jump into the Microsoft
    /// Intermediate Language (MSIL) instruction sequence to a new
    /// label, jumping if the value on top of the stack evaluates to
    /// <see langword="false"/>.
    /// </summary>
    /// <param name="ilGen">
    /// Instruction sequence into which to insert the jump.
    /// </param>
    /// <param name="label">
    /// Label that will be the target of the jump.
    /// </param>
    /// <returns>
    /// The same instance as <paramref name="ilGen"/>, enabling Fluent
    /// syntax.
    /// </returns>
    public static ILGenerator BranchFalse(this ILGenerator ilGen, Label label)
    {
        return BranchIf(ilGen, label, Brfalse);
    }

    /// <summary>
    /// Inserts a conditional control‑transfer jump into the MSIL
    /// instruction sequence to a new label, jumping if the value on
    /// top of the stack evaluates to <see langword="false"/>.
    /// </summary>
    /// <param name="ilGen">
    /// Instruction sequence into which to insert the jump.
    /// </param>
    /// <param name="label">
    /// New label that will be the target of the jump.
    /// </param>
    /// <returns>
    /// The same instance as <paramref name="ilGen"/>, enabling Fluent
    /// syntax.
    /// </returns>
    public static ILGenerator BranchFalseNewLabel(this ILGenerator ilGen, out Label label)
    {
        return BranchIfNewLabel(ilGen, out label, Brfalse);
    }

    /// <summary>
    /// Inserts a conditional control‑transfer jump into the MSIL
    /// instruction sequence to a new label; jumping if, after comparing
    /// the two values on top of the stack, the first is greater than
    /// the second.
    /// </summary>
    /// <param name="ilGen">
    /// Instruction sequence into which to insert the jump.
    /// </param>
    /// <param name="label">
    /// Label that will be the target of the jump.
    /// </param>
    /// <returns>
    /// The same instance as <paramref name="ilGen"/>, enabling Fluent
    /// syntax.
    /// </returns>
    public static ILGenerator BranchGreaterThan(this ILGenerator ilGen, Label label)
    {
        return BranchIf(ilGen, label, Bgt);
    }

    /// <summary>
    /// Inserts a conditional control‑transfer jump into the MSIL
    /// instruction sequence to a new label; jumping if, after comparing
    /// the two values on top of the stack, the first is greater than
    /// the second.
    /// </summary>
    /// <param name="ilGen">
    /// Instruction sequence into which to insert the jump.
    /// </param>
    /// <param name="label">
    /// New label that will be the target of the jump.
    /// </param>
    /// <returns>
    /// The same instance as <paramref name="ilGen"/>, enabling Fluent
    /// syntax.
    /// </returns>
    public static ILGenerator BranchGreaterThanNewLabel(this ILGenerator ilGen, out Label label)
    {
        return BranchIfNewLabel(ilGen, out label, Bgt);
    }

    /// <summary>
    /// Inserts a conditional control‑transfer jump into the MSIL
    /// instruction sequence to a new label; jumping if, after comparing
    /// the two values on top of the stack, the first is less than the
    /// second.
    /// </summary>
    /// <param name="ilGen">
    /// Instruction sequence into which to insert the jump.
    /// </param>
    /// <param name="label">
    /// Label that will be the target of the jump.
    /// </param>
    /// <returns>
    /// The same instance as <paramref name="ilGen"/>, enabling Fluent
    /// syntax.
    /// </returns>
    public static ILGenerator BranchLessThan(this ILGenerator ilGen, Label label)
    {
        return BranchIf(ilGen, label, Blt);
    }

    /// <summary>
    /// Inserts a conditional branch to a new label in a Microsoft® IL
    /// sequence, branching when the top two stack values are compared
    /// and the first is less than the second.
    /// </summary>
    /// <param name="ilGen">
    /// The ILGenerator where the branch will be inserted.
    /// </param>
    /// <param name="label">
    /// The new label that will become the branch destination.
    /// </param>
    /// <returns>
    /// The same instance as <paramref name="ilGen"/>, enabling fluent
    /// syntax.
    /// </returns>
    public static ILGenerator BranchLessThanNewLabel(this ILGenerator ilGen, out Label label)
    {
        return BranchIfNewLabel(ilGen, out label, Blt);
    }

    /// <summary>
    /// Inserts a conditional branch to a label in a Microsoft® IL
    /// sequence, branching when the top two stack values are equal.
    /// </summary>
    /// <param name="ilGen">
    /// The ILGenerator where the branch will be inserted.
    /// </param>
    /// <param name="label">
    /// The label that will become the branch destination.
    /// </param>
    /// <returns>
    /// The same instance as <paramref name="ilGen"/>, enabling fluent
    /// syntax.
    /// </returns>
    public static ILGenerator BranchEqual(this ILGenerator ilGen, Label label)
    {
        return BranchIf(ilGen, label, Beq);
    }

    /// <summary>
    /// Inserts a conditional branch to a new label in a Microsoft® IL
    /// sequence, branching when the top two stack values are equal.
    /// </summary>
    /// <param name="ilGen">
    /// The ILGenerator where the branch will be inserted.
    /// </param>
    /// <param name="label">
    /// The new label that will become the branch destination.
    /// </param>
    /// <returns>
    /// The same instance as <paramref name="ilGen"/>, enabling fluent
    /// syntax.
    /// </returns>
    public static ILGenerator BranchEqualNewLabel(this ILGenerator ilGen, out Label label)
    {
        return BranchIfNewLabel(ilGen, out label, Beq);
    }

    /// <summary>
    /// Inserts a conditional branch to a label in a Microsoft® IL
    /// sequence, branching when the top two stack values are compared
    /// and the first is greater than or equal to the second.
    /// </summary>
    /// <param name="ilGen">
    /// The ILGenerator where the branch will be inserted.
    /// </param>
    /// <param name="label">
    /// The label that will become the branch destination.
    /// </param>
    /// <returns>
    /// The same instance as <paramref name="ilGen"/>, enabling fluent
    /// syntax.
    /// </returns>
    public static ILGenerator BranchGreaterThanOrEqual(this ILGenerator ilGen, Label label)
    {
        return BranchIf(ilGen, label, Bge);
    }

    /// <summary>
    /// Inserts a conditional branch to a new label in a Microsoft® IL
    /// sequence, branching when the top two stack values are compared
    /// and the first is greater than or equal to the second.
    /// </summary>
    /// <param name="ilGen">
    /// The ILGenerator where the branch will be inserted.
    /// </param>
    /// <param name="label">
    /// The new label that will become the branch destination.
    /// </param>
    /// <returns>
    /// The same instance as <paramref name="ilGen"/>, enabling fluent
    /// syntax.
    /// </returns>
    public static ILGenerator BranchGreaterThanOrEqualNewLabel(this ILGenerator ilGen, out Label label)
    {
        return BranchIfNewLabel(ilGen, out label, Bge);
    }

    /// <summary>
    /// Inserts a conditional branch in a Microsoft® IL sequence to a new
    /// label, branching when the top two stack values are compared and the
    /// first is less than or equal to the second.
    /// </summary>
    /// <param name="ilGen">
    /// The ILGenerator where the branch will be inserted.
    /// </param>
    /// <param name="label">
    /// The label that will be the branch destination.
    /// </param>
    /// <returns>
    /// The same instance as <paramref name="ilGen"/>, enabling fluent
    /// syntax.
    /// </returns>
    public static ILGenerator BranchLessThanOrEqual(this ILGenerator ilGen, Label label)
    {
        return BranchIf(ilGen, label, Ble);
    }

    /// <summary>
    /// Inserts a conditional branch in a Microsoft® IL sequence to a new
    /// label, branching when the top two stack values are compared and the
    /// first is less than or equal to the second.
    /// </summary>
    /// <param name="ilGen">
    /// The ILGenerator where the branch will be inserted.
    /// </param>
    /// <param name="label">
    /// The new label that will be the branch destination.
    /// </param>
    /// <returns>
    /// The same instance as <paramref name="ilGen"/>, enabling fluent
    /// syntax.
    /// </returns>
    public static ILGenerator BranchLessThanOrEqualNewLabel(this ILGenerator ilGen, out Label label)
    {
        return BranchIfNewLabel(ilGen, out label, Ble);
    }

    /// <summary>
    /// Inserts a conditional branch in a Microsoft® IL sequence to a new
    /// label, branching when the top two stack values are compared and the
    /// first is not equal to the second.
    /// </summary>
    /// <param name="ilGen">
    /// The ILGenerator where the branch will be inserted.
    /// </param>
    /// <param name="label">
    /// The label that will be the branch destination.
    /// </param>
    /// <returns>
    /// The same instance as <paramref name="ilGen"/>, enabling fluent
    /// syntax.
    /// </returns>
    public static ILGenerator BranchNotEqual(this ILGenerator ilGen, Label label)
    {
        return BranchIf(ilGen, label, Bne_Un);
    }

    /// <summary>
    /// Inserts a conditional branch in a Microsoft® IL sequence to a new
    /// label, branching when the top two stack values are compared and the
    /// first is not equal to the second.
    /// </summary>
    /// <param name="ilGen">
    /// The ILGenerator where the branch will be inserted.
    /// </param>
    /// <param name="label">
    /// The new label that will be the branch destination.
    /// </param>
    /// <returns>
    /// The same instance as <paramref name="ilGen"/>, enabling fluent
    /// syntax.
    /// </returns>
    public static ILGenerator BranchNotEqualNewLabel(this ILGenerator ilGen, out Label label)
    {
        return BranchIfNewLabel(ilGen, out label, Bne_Un);
    }

    /// <summary>
    /// Inserts an unconditional branch in a Microsoft® IL sequence to a
    /// new label.
    /// </summary>
    /// <param name="ilGen">
    /// The ILGenerator where the branch will be inserted.
    /// </param>
    /// <param name="label">
    /// The new label that will be the branch destination.
    /// </param>
    /// <returns>
    /// The same instance as <paramref name="ilGen"/>, enabling fluent
    /// syntax.
    /// </returns>
    public static ILGenerator BranchNewLabel(this ILGenerator ilGen, out Label label)
    {
        label = ilGen.DefineLabel();
        ilGen.Emit(Br, label);
        return ilGen;
    }
}
