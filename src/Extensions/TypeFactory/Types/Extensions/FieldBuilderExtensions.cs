/*
FieldBuilderExtensions.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2022 César Andrés Morgan

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

namespace TheXDS.MCART.Types.Extensions;
using System;
using System.Reflection.Emit;
using TheXDS.MCART.Exceptions;
using static System.Reflection.Emit.OpCodes;

/// <summary>
/// Contiene extensiones útiles para la manipulación de constructores de
/// campos por medio de la clase <see cref="FieldBuilder"/>.
/// </summary>
public static class FieldBuilderExtensions
{
    /// <summary>
    /// Inicializa un campo dentro del generador de código especificado.
    /// </summary>
    /// <param name="field">Campo a inicializar.</param>
    /// <param name="ilGen">
    /// Generador de código a utilizar para inicializar el campo.
    /// Generalmente, debe tratarse de un constructor de clase.
    /// </param>
    /// <param name="value">
    /// Valor constante con el cual debe inicializarse el campo.
    /// </param>
    public static void InitField(this FieldBuilder field, ILGenerator ilGen, object value)
    {
        ilGen.Emit(Ldarg_0);
        ilGen.LoadConstant(value);
        ilGen.Emit(Stfld, field);
    }

    /// <summary>
    /// Inicializa un campo dentro del generador de código especificado.
    /// </summary>
    /// <param name="field">Campo a inicializar.</param>
    /// <param name="ilGen">
    /// Generador de código a utilizar para inicializar el campo.
    /// Generalmente, debe tratarse de un constructor de clase.
    /// </param>
    /// <param name="instanceType">Tipo de objeto a instanciar.</param>
    /// <param name="args">
    /// Argumentos a pasar al constructor del tipo especificado.
    /// </param>
    /// <exception cref="InvalidTypeException">
    /// Se produce si el tipo no es instanciable.
    /// </exception>
    public static void InitField(this FieldBuilder field, ILGenerator ilGen, Type instanceType, params object[] args)
    {
        if (instanceType.IsAbstract) throw new InvalidTypeException(instanceType);
        ilGen.Emit(Ldarg_0);
        ilGen.NewObject(instanceType, args);
        ilGen.Emit(Stfld, field);
    }

    /// <summary>
    /// Inicializa un campo dentro del generador de código especificado.
    /// </summary>
    /// <typeparam name="T">Tipo de valor u objeto a instanciar.</typeparam>
    /// <param name="field">Campo a inicializar.</param>
    /// <param name="ilGen">
    /// Generador de código a utilizar para inicializar el campo.
    /// Generalmente, debe tratarse de un constructor de clase.
    /// </param>
    public static void InitField<T>(this FieldBuilder field, ILGenerator ilGen) where T : new()
    {
        if (typeof(T).IsValueType) InitField(field, ilGen, default(T)!);
        else InitField(field, ilGen, typeof(T), Array.Empty<object>());
    }
}
