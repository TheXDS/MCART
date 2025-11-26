/*
FieldBuilderExtensions.cs

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
using TheXDS.MCART.Exceptions;
using static System.Reflection.Emit.OpCodes;

namespace TheXDS.MCART.Types.Extensions;

/// <summary>
/// Provides useful extensions for manipulating field constructors via the
/// <see cref="FieldBuilder"/> class.
/// </summary>
public static class FieldBuilderExtensions
{
    /// <summary>
    /// Initializes a field within the specified IL generator.
    /// </summary>
    /// <param name="field">Field to initialize.</param>
    /// <param name="ilGen">IL generator used to initialize the field. Usually
    /// it should be a class constructor.</param>
    /// <param name="value">Constant value with which the field should be initialized.</param>
    public static void InitField(this FieldBuilder field, ILGenerator ilGen, object value)
    {
        ilGen.Emit(Ldarg_0);
        ilGen.LoadConstant(value);
        ilGen.Emit(Stfld, field);
    }

    /// <summary>
    /// Initializes a field within the specified IL generator.
    /// </summary>
    /// <param name="field">Field to initialize.</param>
    /// <param name="ilGen">IL generator used to initialize the field. Usually
    /// it should be a class constructor.</param>
    /// <param name="instanceType">Type of object to instantiate.</param>
    /// <param name="args">Arguments to pass to the constructor of the specified type.</param>
    /// <exception cref="InvalidTypeException">Thrown if the type is not instantiable.</exception>
    public static void InitField(this FieldBuilder field, ILGenerator ilGen, Type instanceType, params object[] args)
    {
        if (instanceType.IsAbstract) throw new InvalidTypeException(instanceType);
        ilGen.Emit(Ldarg_0);
        ilGen.NewObject(instanceType, args);
        ilGen.Emit(Stfld, field);
    }

    /// <summary>
    /// Initializes a field within the specified IL generator.
    /// </summary>
    /// <typeparam name="T">Type of value or object to instantiate.</typeparam>
    /// <param name="field">Field to initialize.</param>
    /// <param name="ilGen">IL generator used to initialize the field. Usually
    /// it should be a class constructor.</param>
    public static void InitField<T>(this FieldBuilder field, ILGenerator ilGen) where T : new()
    {
        if (typeof(T).IsValueType) InitField(field, ilGen, default(T)!);
        else InitField(field, ilGen, typeof(T), []);
    }
}
