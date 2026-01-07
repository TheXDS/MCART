/*
ILGeneratorExtensions_Delegates.cs

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

using System.Reflection.Emit;

namespace TheXDS.MCART.Types.Extensions;

public static partial class ILGeneratorExtensions
{
    /// <summary>
    /// Defines a delegate that describes a <see langword="for"/> block.
    /// </summary>
    /// <param name="il">
    /// Reference to the instruction generator where the <see langword="for"/>
    /// block code is inserted.
    /// </param>
    /// <param name="accumulator">
    /// Reference to the loop accumulator.
    /// </param>
    /// <param name="break">
    /// Exit label for the <see langword="for"/> block.
    /// </param>
    /// <param name="next">
    /// Continuation label for the <see langword="for"/> block.
    /// </param>
    public delegate void ForBlock(ILGenerator il, LocalBuilder accumulator, Label @break, Label next);

    /// <summary>
    /// Defines a delegate that describes a <see langword="foreach"/> block.
    /// </summary>
    /// <param name="il">
    /// Reference to the instruction generator where the <see langword="foreach"/>
    /// block code is inserted.
    /// </param>
    /// <param name="item">
    /// Reference to the loop accumulator.
    /// </param>
    /// <param name="break">
    /// Exit label for the <see langword="foreach"/> block. Must be invoked
    /// through <see cref="Leave(ILGenerator, Label)"/>.
    /// </param>
    /// <param name="continue">
    /// Continuation label for the <see langword="foreach"/> block.
    /// </param>
    public delegate void ForEachBlock(ILGenerator il, LocalBuilder item, Label @break, Label @continue);

    /// <summary>
    /// Defines a delegate that describes a <see langword="try"/> block.
    /// </summary>
    /// <param name="il">
    /// Reference to the instruction generator where the <see langword="try"/>
    /// block code is inserted.
    /// </param>
    /// <param name="leaveTry">
    /// Exit label for the <see langword="try"/> block. Must be invoked
    /// through <see cref="Leave(ILGenerator, Label)"/>.
    /// </param>
    public delegate void TryBlock(ILGenerator il, Label leaveTry);

    /// <summary>
    /// Defines a delegate that describes a <see langword="finally"/> block.
    /// </summary>
    /// <param name="il">
    /// Reference to the instruction generator where the <see langword="finally"/>
    /// block code is inserted.
    /// </param>
    /// <param name="leaveTry">
    /// Exit label for the <see langword="finally"/> block. Must be invoked
    /// through <see cref="Leave(ILGenerator, Label)"/>.
    /// </param>
    public delegate void FinallyBlock(ILGenerator il, Label leaveTry);

    /// <summary>
    /// Defines a delegate that describes a <see langword="using"/> block.
    /// </summary>
    /// <param name="il">
    /// Reference to the instruction generator where the <see langword="using"/>
    /// block code is inserted.
    /// </param>
    /// <param name="disposable">
    /// Reference to the disposable element within the <see langword="using"/> block.
    /// </param>
    /// <param name="leaveTry">
    /// Exit label for the <see langword="using"/> block. Must be invoked
    /// through <see cref="Leave(ILGenerator, Label)"/>.
    /// </param>
    public delegate void UsingBlock(ILGenerator il, LocalBuilder disposable, Label leaveTry);
}
