/*
ILGeneratorExtensions_Privates.cs

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

using System.Reflection;
using System.Reflection.Emit;
using TheXDS.MCART.Exceptions;
using TheXDS.MCART.Helpers;
using TheXDS.MCART.Types.Extensions.ConstantLoaders;
using static System.Reflection.Emit.OpCodes;

namespace TheXDS.MCART.Types.Extensions;

/// <summary>
/// Contiene extensiones útiles para la generación de código por medio
/// de la clase <see cref="ILGenerator"/>.
/// </summary>
public static partial class ILGeneratorExtensions
{
    private static readonly HashSet<IConstantLoader> _constantLoaders = new(ReflectionHelpers.FindAllObjects<IConstantLoader>(), new ConstantLoaderComparer());

    private static ILGenerator GetField(ILGenerator ilGen, FieldInfo field, OpCode opCode)
    {
        if (field.IsStatic)
        {
            ilGen.Emit(opCode, field);
        }
        else
        {
            ilGen.Emit(Ldarg_0);
            ilGen.Emit(opCode, field);
        }
        return ilGen;
    }

    private static ILGenerator BranchIf(ILGenerator ilGen, Label label, OpCode op)
    {
        ilGen.Emit(op, label);
        return ilGen;
    }

    private static ILGenerator BranchIfNewLabel(ILGenerator ilGen, out Label label, OpCode op)
    {
        return BranchIf(ilGen, label = ilGen.DefineLabel(), op);
    }

    private static ILGenerator OneLiner(this ILGenerator ilGen, OpCode op)
    {
        ilGen.Emit(op);
        return ilGen;
    }

    private static Label InsertTryBlock(ILGenerator ilGen, TryBlock block)
    {
        Label endTry = ilGen.BeginExceptionBlock();
        block(ilGen, endTry);
        return endTry;
    }

    private static void InsertCatchBlocks(ILGenerator ilGen, TryBlock tryBlock, IEnumerable<KeyValuePair<Type, TryBlock>> catchBlocks)
    {
        Label endTry = InsertTryBlock(ilGen, tryBlock);
        foreach (KeyValuePair<Type, TryBlock> j in catchBlocks)
        {
            if (!j.Key.Implements<Exception>()) throw new InvalidTypeException(j.Key);
            ilGen.BeginCatchBlock(j.Key);
            j.Value(ilGen, endTry);
        }
    }
}
