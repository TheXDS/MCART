/*
ILGeneratorExtensions_Operations.cs

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
using static System.Reflection.Emit.OpCodes;
using Op = System.Reflection.Emit.OpCodes;

namespace TheXDS.MCART.Types.Extensions;

public static partial class ILGeneratorExtensions
{
    /// <summary>
    /// Disposes an <see cref="IDisposable"/> object contained in the
    /// specified <see cref="LocalBuilder"/>.
    /// </summary>
    /// <param name="ilGen">
    /// The instruction sequence into which the call is inserted.
    /// </param>
    /// <param name="disposable">
    /// Local variable that holds the <see cref="IDisposable"/> to dispose.
    /// </param>
    /// <returns></returns>
    public static ILGenerator Dispose(this ILGenerator ilGen, LocalBuilder disposable)
    {
        return ilGen.LoadLocal(disposable).Call<IDisposable, Action>(p => p.Dispose);
    }

    /// <summary>
    /// Inserts an addition operation into the Microsoft® Intermediate Language (MSIL)
    /// instruction sequence.
    /// </summary>
    /// <param name="ilGen">
    /// The instruction sequence into which the operation is inserted.
    /// </param>
    /// <returns>
    /// The same instance as <paramref name="ilGen"/>, enabling fluent syntax.
    /// </returns>
    /// <remarks>
    /// This method expects two values on the stack that can be added together.
    /// </remarks>
    public static ILGenerator Add(this ILGenerator ilGen) => OneLiner(ilGen, Op.Add);

    /// <summary>
    /// Inserts a subtraction operation into the Microsoft® Intermediate Language (MSIL)
    /// instruction sequence.
    /// </summary>
    /// <param name="ilGen">
    /// The instruction sequence into which the operation is inserted.
    /// </param>
    /// <returns>
    /// The same instance as <paramref name="ilGen"/>, enabling fluent syntax.
    /// </returns>
    /// <remarks>
    /// This method expects two values on the stack that can be subtracted.
    /// </remarks>
    public static ILGenerator Subtract(this ILGenerator ilGen) => OneLiner(ilGen, Sub);

    /// <summary>
    /// Inserts a multiplication operation into the Microsoft® Intermediate Language (MSIL)
    /// instruction sequence.
    /// </summary>
    /// <param name="ilGen">
    /// The instruction sequence into which the operation is inserted.
    /// </param>
    /// <returns>
    /// The same instance as <paramref name="ilGen"/>, enabling fluent syntax.
    /// </returns>
    /// <remarks>
    /// This method expects two values on the stack that can be multiplied together.
    /// </remarks>
    public static ILGenerator Multiply(this ILGenerator ilGen) => OneLiner(ilGen, Mul);

    /// <summary>
    /// Inserts a division operation into the Microsoft® Intermediate Language (MSIL)
    /// instruction sequence.
    /// </summary>
    /// <param name="ilGen">
    /// The instruction sequence into which the operation is inserted.
    /// </param>
    /// <returns>
    /// The same instance as <paramref name="ilGen"/>, enabling fluent syntax.
    /// </returns>
    /// <remarks>
    /// This method expects two values on the stack that can be divided.
    /// </remarks>
    public static ILGenerator Divide(this ILGenerator ilGen) => OneLiner(ilGen, Div);

    /// <summary>
    /// Inserts a remainder operation into the Microsoft® Intermediate Language (MSIL)
    /// instruction sequence.
    /// </summary>
    /// <param name="ilGen">
    /// The instruction sequence into which the operation is inserted.
    /// </param>
    /// <returns>
    /// The same instance as <paramref name="ilGen"/>, enabling fluent syntax.
    /// </returns>
    /// <remarks>
    /// This method expects two values on the stack that can be used for a
    /// remainder calculation.
    /// </remarks>
    public static ILGenerator Remainder(this ILGenerator ilGen) => OneLiner(ilGen, Rem);

    /// <summary>
    /// Inserts a NOP instruction into the Microsoft® Intermediate Language
    /// (MSIL) instruction sequence.
    /// </summary>
    /// <param name="ilGen">
    /// Instruction sequence into which the operation is inserted.
    /// </param>
    /// <returns>
    /// The same instance as <paramref name="ilGen"/>, enabling fluent syntax.
    /// </returns>
    public static ILGenerator Nop(this ILGenerator ilGen) => OneLiner(ilGen, Op.Nop);

    /// <summary>
    /// Inserts a duplicate operation that copies the value at the top of the
    /// stack into the Microsoft® Intermediate Language (MSIL) instruction
    /// stream.
    /// </summary>
    /// <param name="ilGen">
    /// Instruction sequence into which the operation is inserted.
    /// </param>
    /// <returns>
    /// The same instance as <paramref name="ilGen"/>, enabling fluent syntax.
    /// </returns>
    public static ILGenerator Duplicate(this ILGenerator ilGen) => OneLiner(ilGen, Dup);

    /// <summary>
    /// Inserts a pop operation that removes the value at the top of the
    /// stack into the Microsoft® Intermediate Language (MSIL) instruction
    /// stream.
    /// </summary>
    /// <param name="ilGen">
    /// Instruction sequence into which the operation is inserted.
    /// </param>
    /// <returns>
    /// The same instance as <paramref name="ilGen"/>, enabling fluent syntax.
    /// </returns>
    public static ILGenerator Pop(this ILGenerator ilGen) => OneLiner(ilGen, Op.Pop);

    /// <summary>
    /// Inserts an equality comparison operation between two values at the
    /// top of the stack into the Microsoft® Intermediate Language (MSIL)
    /// instruction sequence.
    /// </summary>
    /// <param name="ilGen">
    /// Instruction sequence into which the operation is inserted.
    /// </param>
    /// <returns>
    /// The same instance as <paramref name="ilGen"/>, enabling fluent syntax.
    /// </returns>
    public static ILGenerator CompareEqual(this ILGenerator ilGen) => OneLiner(ilGen, Ceq);

    /// <summary>
    /// Inserts a greater‑than comparison operation between two values at the
    /// top of the stack into the Microsoft® Intermediate Language (MSIL)
    /// instruction sequence.
    /// </summary>
    /// <param name="ilGen">
    /// Instruction sequence into which the operation is inserted.
    /// </param>
    /// <returns>
    /// The same instance as <paramref name="ilGen"/>, enabling fluent syntax.
    /// </returns>
    public static ILGenerator CompareGreaterThan(this ILGenerator ilGen) => OneLiner(ilGen, Cgt);

    /// <summary>
    /// Inserts a less‑than comparison operation between two values at the
    /// top of the stack into the Microsoft® Intermediate Language (MSIL)
    /// instruction sequence.
    /// </summary>
    /// <param name="ilGen">
    /// Instruction sequence into which the operation is inserted.
    /// </param>
    /// <returns>
    /// The same instance as <paramref name="ilGen"/>, enabling fluent syntax.
    /// </returns>
    public static ILGenerator CompareLessThan(this ILGenerator ilGen) => OneLiner(ilGen, Clt);

    /// <summary>
    /// Inserts an operation that retrieves the number of elements contained
    /// in an array in the Microsoft® Intermediate Language (MSIL) instruction
    /// stream, equivalent to <see cref="Array.Length"/>.
    /// </summary>
    /// <param name="ilGen">
    /// Instruction sequence into which the operation is inserted.
    /// </param>
    /// <returns>
    /// The same instance as <paramref name="ilGen"/>, enabling fluent syntax.
    /// </returns>
    /// <remarks>
    /// The stack must contain a reference to an array at the top when this
    /// call is made. An <see cref="int"/> value will be pushed onto the top
    /// of the stack.
    /// </remarks>
    public static ILGenerator GetArrayLength(this ILGenerator ilGen)
    {
        ilGen.OneLiner(Ldlen).OneLiner(Conv_I4);
        return ilGen;
    }

    /// <summary>
    /// Inserts an operation that stores a byte element in an array at the
    /// specified index onto the Microsoft® IL (MSIL) instruction sequence.
    /// </summary>
    /// <param name="ilGen">
    /// Microsoft® IL (MSIL) instruction stream generator onto which to insert
    /// the operation.
    /// </param>
    /// <param name="index">
    /// Index of the element in the array to be set.
    /// </param>
    /// <param name="element">Value to be set.</param>
    /// <returns>
    /// The same instance as <paramref name="ilGen"/>, allowing for fluent syntax.
    /// </returns>
    public static ILGenerator StoreElement(this ILGenerator ilGen, int index, byte element)
    {
        return ilGen
            .LoadConstant(index)
            .LoadConstant(element)
            .StoreInt8Element();
    }

    /// <summary>
    /// Inserts an operation that stores a short element in an array at the
    /// specified index onto the Microsoft® IL (MSIL) instruction sequence.
    /// </summary>
    /// <param name="ilGen">
    /// Microsoft® IL (MSIL) instruction stream generator onto which to insert
    /// the operation.
    /// </param>
    /// <param name="index">
    /// Index of the element in the array to be set.
    /// </param>
    /// <param name="element">Value to be set.</param>
    /// <returns>
    /// The same instance as <paramref name="ilGen"/>, allowing for fluent syntax.
    /// </returns>
    public static ILGenerator StoreElement(this ILGenerator ilGen, int index, short element)
    {
        return ilGen
            .LoadConstant(index)
            .LoadConstant(element)
            .StoreInt16Element();
    }

    /// <summary>
    /// Inserts an operation that stores an int element in an array at the
    /// specified index onto the Microsoft® IL (MSIL) instruction sequence.
    /// </summary>
    /// <param name="ilGen">
    /// Microsoft® IL (MSIL) instruction stream generator onto which to insert
    /// the operation.
    /// </param>
    /// <param name="index">
    /// Index of the element in the array to be set.
    /// </param>
    /// <param name="element">Value to be set.</param>
    /// <returns>
    /// The same instance as <paramref name="ilGen"/>, allowing for fluent syntax.
    /// </returns>
    public static ILGenerator StoreElement(this ILGenerator ilGen, int index, int element)
    {
        return ilGen
            .LoadConstant(index)
            .LoadConstant(element)
            .StoreInt32Element();
    }

    /// <summary>
    /// Inserts an operation that stores a long element in an array at the
    /// specified index onto the Microsoft® IL (MSIL) instruction sequence.
    /// </summary>
    /// <param name="ilGen">
    /// Microsoft® IL (MSIL) instruction stream generator onto which to insert
    /// the operation.
    /// </param>
    /// <param name="index">
    /// Index of the element in the array to be set.
    /// </param>
    /// <param name="element">Value to be set.</param>
    /// <returns>
    /// The same instance as <paramref name="ilGen"/>, allowing for fluent syntax.
    /// </returns>
    public static ILGenerator StoreElement(this ILGenerator ilGen, int index, long element)
    {
        return ilGen
            .LoadConstant(index)
            .LoadConstant(element)
            .StoreInt64Element();
    }

    /// <summary>
    /// Inserts an operation to store an element into an array at the specified
    /// index onto the Microsoft® IL (MSIL) instruction sequence.
    /// </summary>
    /// <param name="ilGen">
    /// Microsoft® IL (MSIL) instruction stream generator onto which to insert
    /// the operation.
    /// </param>
    /// <param name="index">
    /// Index of the element in the array to be set.
    /// </param>
    /// <param name="element">Value to be set.</param>
    /// <returns>
    /// The same instance as <paramref name="ilGen"/>, allowing for Fluent
    /// syntax.
    /// </returns>
    public static ILGenerator StoreElement(this ILGenerator ilGen, int index, float element)
    {
        return ilGen
            .LoadConstant(index)
            .LoadConstant(element)
            .StoreFloatElement();
    }

    /// <summary>
    /// Inserts an operation to store an element into an array at the specified
    /// index onto the Microsoft® IL (MSIL) instruction sequence.
    /// </summary>
    /// <param name="ilGen">
    /// Microsoft® IL (MSIL) instruction stream generator onto which to insert
    /// the operation.
    /// </param>
    /// <param name="index">
    /// Index of the element in the array to be set.
    /// </param>
    /// <param name="element">Value to be set.</param>
    /// <returns>
    /// The same instance as <paramref name="ilGen"/>, allowing for Fluent
    /// syntax.
    /// </returns>
    public static ILGenerator StoreElement(this ILGenerator ilGen, int index, double element)
    {
        return ilGen
            .LoadConstant(index)
            .LoadConstant(element)
            .StoreDoubleElement();
    }

    /// <summary>
    /// Inserts an operation to store an element into an array at the specified
    /// index onto the Microsoft® IL (MSIL) instruction sequence.
    /// </summary>
    /// <param name="ilGen">
    /// Microsoft® IL (MSIL) instruction stream generator onto which to insert
    /// the operation.
    /// </param>
    /// <param name="index">
    /// Index of the element in the array to be set.
    /// </param>
    /// <param name="element">Value to be set.</param>
    /// <returns>
    /// The same instance as <paramref name="ilGen"/>, allowing for Fluent
    /// syntax.
    /// </returns>
    public static ILGenerator StoreElement(this ILGenerator ilGen, int index, object element)
    {
        return ilGen
            .LoadConstant(index)
            .LoadConstant(element)
            .StoreRefElement();
    }

    /// <summary>
    /// Inserts an operation that stores a <see cref="byte"/> or
    /// <see cref="sbyte"/> value into an array at the specified index in the
    /// Microsoft® Intermediate Language (MSIL) instruction sequence.
    /// </summary>
    /// <param name="ilGen">
    /// Instruction stream into which the operation is inserted.
    /// </param>
    /// <returns>
    /// The same instance as <paramref name="ilGen"/>, allowing for fluent
    /// syntax.
    /// </returns>
    /// <remarks>
    /// The operation requires that the stack contains the <see cref="byte"/>
    /// value to be inserted at the top, and the <see cref="int"/> index
    /// immediately below it.
    /// <br/><br/>
    /// Net stack usage: -2
    /// </remarks>
    public static ILGenerator StoreInt8Element(this ILGenerator ilGen) => ilGen.OneLiner(Stelem_I1);

    /// <summary>
    /// Inserts an operation that stores a <see cref="short"/> or
    /// <see cref="ushort"/> value into an array at the specified index in the
    /// Microsoft® Intermediate Language (MSIL) instruction sequence.
    /// </summary>
    /// <param name="ilGen">
    /// Instruction stream into which the operation is inserted.
    /// </param>
    /// <returns>
    /// The same instance as <paramref name="ilGen"/>, allowing for fluent
    /// syntax.
    /// </returns>
    /// <remarks>
    /// The operation requires that the stack contains the <see cref="short"/>
    /// value to be inserted at the top, and the <see cref="int"/> index
    /// immediately below it.
    /// <br/><br/>
    /// Net stack usage: -2
    /// </remarks>
    public static ILGenerator StoreInt16Element(this ILGenerator ilGen) => ilGen.OneLiner(Stelem_I2);

    /// <summary>
    /// Inserts an operation that stores an <see cref="int"/> or
    /// <see cref="uint"/> value into an array at the specified index in the
    /// Microsoft® Intermediate Language (MSIL) instruction sequence.
    /// </summary>
    /// <param name="ilGen">
    /// Instruction stream into which the operation is inserted.
    /// </param>
    /// <returns>
    /// The same instance as <paramref name="ilGen"/>, allowing for fluent
    /// syntax.
    /// </returns>
    /// <remarks>
    /// The operation requires that the stack contains the <see cref="int"/>
    /// value to be inserted at the top, and the <see cref="int"/> index
    /// immediately below it.
    /// <br/><br/>
    /// Net stack usage: -2
    /// </remarks>
    public static ILGenerator StoreInt32Element(this ILGenerator ilGen) => ilGen.OneLiner(Stelem_I4);

    /// <summary>
    /// Inserts an operation that stores a <see cref="long"/> or
    /// <see cref="ulong"/> value into an array at the specified index in the
    /// Microsoft® Intermediate Language (MSIL) instruction sequence.
    /// </summary>
    /// <param name="ilGen">
    /// Instruction stream into which the operation is inserted.
    /// </param>
    /// <returns>
    /// The same instance as <paramref name="ilGen"/>, allowing for fluent
    /// syntax.
    /// </returns>
    /// <remarks>
    /// The operation requires that the stack contains the <see cref="long"/>
    /// value to be inserted at the top, and the <see cref="int"/> index
    /// immediately below it.
    /// <br/><br/>
    /// Net stack usage: -2
    /// </remarks>
    public static ILGenerator StoreInt64Element(this ILGenerator ilGen) => ilGen.OneLiner(Stelem_I8);

    /// <summary>
    /// Inserts an operation that stores a <see cref="float"/> value into an
    /// array at the specified index in the Microsoft® Intermediate Language
    /// (MSIL) instruction sequence.
    /// </summary>
    /// <param name="ilGen">
    /// Instruction stream into which the operation is inserted.
    /// </param>
    /// <returns>
    /// The same instance as <paramref name="ilGen"/>, allowing for fluent
    /// syntax.
    /// </returns>
    /// <remarks>
    /// The operation requires that the stack contains the <see cref="float"/>
    /// value to be inserted at the top, and the <see cref="int"/> index
    /// immediately below it.
    /// <br/><br/>
    /// Net stack usage: -2
    /// </remarks>
    public static ILGenerator StoreFloatElement(this ILGenerator ilGen) => ilGen.OneLiner(Stelem_R4);

    /// <summary>
    /// Inserts an operation that stores a <see cref="double"/> value into an
    /// array at the specified index in the Microsoft® Intermediate Language
    /// (MSIL) instruction sequence.
    /// </summary>
    /// <param name="ilGen">
    /// Instruction stream into which the operation is inserted.
    /// </param>
    /// <returns>
    /// The same instance as <paramref name="ilGen"/>, allowing for fluent
    /// syntax.
    /// </returns>
    /// <remarks>
    /// The operation requires that the stack contains the <see cref="double"/>
    /// value to be inserted at the top, and the <see cref="int"/> index
    /// immediately below it.
    /// <br/><br/>
    /// Net stack usage: -2
    /// </remarks>
    public static ILGenerator StoreDoubleElement(this ILGenerator ilGen) => ilGen.OneLiner(Stelem_R8);

    /// <summary>
    /// Inserts an operation that stores a reference to an object into an
    /// array at the specified index in the Microsoft® Intermediate Language
    /// (MSIL) instruction sequence.
    /// </summary>
    /// <param name="ilGen">
    /// Instruction stream into which the operation is inserted.
    /// </param>
    /// <returns>
    /// The same instance as <paramref name="ilGen"/>, allowing for fluent
    /// syntax.
    /// </returns>
    /// <remarks>
    /// The operation requires that the stack contains the object reference
    /// to be inserted at the top, and the <see cref="int"/> index immediately
    /// below it.
    /// <br/><br/>
    /// Net stack usage: -2
    /// </remarks>
    public static ILGenerator StoreRefElement(this ILGenerator ilGen) => ilGen.OneLiner(Stelem_Ref);

    /// <summary>
    /// Inserts the current method's return instruction into the MSIL
    /// instruction sequence.
    /// </summary>
    /// <param name="ilGen">
    /// Instruction stream into which the operation is inserted.
    /// </param>
    /// <returns>
    /// The same instance as <paramref name="ilGen"/>, allowing for fluent
    /// syntax.
    /// </returns>
    public static ILGenerator Return(this ILGenerator ilGen) => ilGen.OneLiner(Ret);

    /// <summary>
    /// Inserts the current method's return instruction into the MSIL
    /// instruction sequence, optionally marking an exit label.
    /// </summary>
    /// <param name="ilGen">
    /// Instruction stream into which the operation is inserted.
    /// </param>
    /// <param name="exitLabel">
    /// Previously created label that receives control from a branch to exit
    /// the executing method.
    /// </param>
    /// <returns>
    /// The same instance as <paramref name="ilGen"/>, allowing for fluent
    /// syntax.
    /// </returns>
    public static ILGenerator Return(this ILGenerator ilGen, Label exitLabel)
    {
        ilGen.MarkLabel(exitLabel);
        ilGen.Emit(Ret);
        return ilGen;
    }

    /// <summary>
    /// Inserts an operation that converts the value at the top of the stack
    /// to a <see cref="byte"/> in the Microsoft® Intermediate Language
    /// (MSIL) instruction sequence.
    /// </summary>
    /// <param name="ilGen">
    /// Instruction stream into which the operation is inserted.
    /// </param>
    /// <returns>
    /// The same instance as <paramref name="ilGen"/>, allowing for fluent
    /// syntax.
    /// </returns>
    public static ILGenerator CastAsByte(this ILGenerator ilGen) => OneLiner(ilGen, Conv_U1);

    /// <summary>
    /// Inserts an operation that converts the value at the top of the stack
    /// to a <see cref="sbyte"/> in the Microsoft® Intermediate Language
    /// (MSIL) instruction sequence.
    /// </summary>
    /// <param name="ilGen">
    /// Instruction stream into which the operation is inserted.
    /// </param>
    /// <returns>
    /// The same instance as <paramref name="ilGen"/>, allowing for fluent
    /// syntax.
    /// </returns>
    public static ILGenerator CastAsSByte(this ILGenerator ilGen) => OneLiner(ilGen, Conv_I1);

    /// <summary>
    /// Inserts an operation that converts the value at the top of the stack
    /// to a <see cref="short"/> in the Microsoft® Intermediate Language
    /// (MSIL) instruction sequence.
    /// </summary>
    /// <param name="ilGen">
    /// Instruction stream into which the operation is inserted.
    /// </param>
    /// <returns>
    /// The same instance as <paramref name="ilGen"/>, allowing fluent
    /// syntax.
    /// </returns>
    public static ILGenerator CastAsShort(this ILGenerator ilGen) => OneLiner(ilGen, Conv_I2);

    /// <summary>
    /// Inserts an operation that converts the value at the top of the stack
    /// to a <see cref="ushort"/> in the Microsoft® Intermediate Language
    /// (MSIL) instruction sequence.
    /// </summary>
    /// <param name="ilGen">
    /// Instruction stream into which the operation is inserted.
    /// </param>
    /// <returns>
    /// The same instance as <paramref name="ilGen"/>, allowing fluent
    /// syntax.
    /// </returns>
    public static ILGenerator CastAsUShort(this ILGenerator ilGen) => OneLiner(ilGen, Conv_U2);

    /// <summary>
    /// Inserts an operation that converts the value at the top of the stack
    /// to a <see cref="int"/> in the Microsoft® Intermediate Language
    /// (MSIL) instruction sequence.
    /// </summary>
    /// <param name="ilGen">
    /// Instruction stream into which the operation is inserted.
    /// </param>
    /// <returns>
    /// The same instance as <paramref name="ilGen"/>, allowing fluent
    /// syntax.
    /// </returns>
    public static ILGenerator CastAsInt(this ILGenerator ilGen) => OneLiner(ilGen, Conv_I4);

    /// <summary>
    /// Inserts an operation that converts the value at the top of the stack
    /// to a <see cref="uint"/> in the Microsoft® Intermediate Language
    /// (MSIL) instruction sequence.
    /// </summary>
    /// <param name="ilGen">
    /// Instruction stream into which the operation is inserted.
    /// </param>
    /// <returns>
    /// The same instance as <paramref name="ilGen"/>, allowing fluent
    /// syntax.
    /// </returns>
    public static ILGenerator CastAsUInt(this ILGenerator ilGen) => OneLiner(ilGen, Conv_U4);

    /// <summary>
    /// Inserts an operation that converts the value at the top of the stack
    /// to a <see cref="long"/> in the Microsoft® Intermediate Language
    /// (MSIL) instruction sequence.
    /// </summary>
    /// <param name="ilGen">
    /// Instruction stream into which the operation is inserted.
    /// </param>
    /// <returns>
    /// The same instance as <paramref name="ilGen"/>, allowing fluent
    /// syntax.
    /// </returns>
    public static ILGenerator CastAsLong(this ILGenerator ilGen) => OneLiner(ilGen, Conv_I8);

    /// <summary>
    /// Inserts an operation that converts the value at the top of the stack
    /// to a <see cref="ulong"/> in the Microsoft® Intermediate Language
    /// (MSIL) instruction sequence.
    /// </summary>
    /// <param name="ilGen">
    /// Instruction stream into which the operation is inserted.
    /// </param>
    /// <returns>
    /// The same instance as <paramref name="ilGen"/>, allowing fluent
    /// syntax.
    /// </returns>
    public static ILGenerator CastAsULong(this ILGenerator ilGen) => OneLiner(ilGen, Conv_U8);

    /// <summary>
    /// Inserts an operation that converts the value at the top of the stack
    /// to a <see cref="float"/> in the Microsoft® Intermediate Language
    /// (MSIL) instruction sequence.
    /// </summary>
    /// <param name="ilGen">
    /// Instruction stream into which the operation is inserted.
    /// </param>
    /// <returns>
    /// The same instance as <paramref name="ilGen"/>, allowing fluent
    /// syntax.
    /// </returns>
    public static ILGenerator CastAsFloat(this ILGenerator ilGen) => OneLiner(ilGen, Conv_R4);

    /// <summary>
    /// Inserts an operation that converts the value at the top of the stack
    /// to a <see cref="double"/> in the Microsoft® Intermediate Language
    /// (MSIL) instruction sequence.
    /// </summary>
    /// <param name="ilGen">
    /// Instruction stream into which the operation is inserted.
    /// </param>
    /// <returns>
    /// The same instance as <paramref name="ilGen"/>, allowing fluent
    /// syntax.
    /// </returns>
    public static ILGenerator CastAsDouble(this ILGenerator ilGen) => OneLiner(ilGen, Conv_R8);
}
