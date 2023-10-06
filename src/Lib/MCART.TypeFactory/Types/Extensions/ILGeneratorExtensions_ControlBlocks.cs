/*
ILGeneratorExtensions_ControlBlocks.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2023 César Andrés Morgan

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
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using TheXDS.MCART.Exceptions;
using TheXDS.MCART.Helpers;
using TheXDS.MCART.Types.Extensions.ConstantLoaders;
using static System.Reflection.Emit.OpCodes;
using Op = System.Reflection.Emit.OpCodes;

namespace TheXDS.MCART.Types.Extensions;

/// <summary>
/// Contiene extensiones útiles para la generación de código por medio
/// de la clase <see cref="ILGenerator"/>.
/// </summary>
public static partial class ILGeneratorExtensions
{
    /// <summary>
    /// Inserta un bloque <see langword="if"/> estructurado en la secuencia
    /// del lenguaje intermedio de Microsoft® (MSIL).
    /// </summary>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar el bloque
    /// <see langword="if"/>.
    /// </param>
    /// <param name="trueBranch">
    /// Acción que permite definir las instrucciones a insertar cuando el
    /// valor superior de la pila sea evaluado como <see langword="true"/>.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
    /// </returns>
    public static ILGenerator If(this ILGenerator ilGen, Action<ILGenerator> trueBranch)
    {
        ilGen.BranchFalseNewLabel(out Label endIf);
        trueBranch(ilGen);
        return ilGen.PutLabel(endIf);
    }

    /// <summary>
    /// Inserta un bloque <see langword="if"/> estructurado en la secuencia
    /// del lenguaje intermedio de Microsoft® (MSIL).
    /// </summary>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar el bloque
    /// <see langword="if"/>.
    /// </param>
    /// <param name="trueBranch">
    /// Acción que permite definir las instrucciones a insertar cuando el
    /// valor superior de la pila sea evaluado como <see langword="true"/>.
    /// </param>
    /// <param name="falseBranch">
    /// Acción que permite definir las instrucciones a insertar cuando el
    /// valor superior de la pila sea evaluado como
    /// <see langword="false"/>.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
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
    /// Inserta un bloque <see langword="for"/> estructurado en la
    /// secuencia del lenguaje intermedio de Microsoft® (MSIL).
    /// </summary>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar el bloque
    /// <see langword="for"/>.
    /// </param>
    /// <param name="accumulator">
    /// Variable local a utilizar como acumulador del bloque
    /// <see langword="for"/>.
    /// </param>
    /// <param name="initialValue">
    /// Valor al cual inicializar el acumulador.
    /// </param>
    /// <param name="condition">
    /// Acción que permite definir la evaluación inicial del bloque
    /// <see langword="for"/>.
    /// </param>
    /// <param name="incrementor">
    /// Acción que permite definir la acción de incremento a ejecutar al
    /// final del bloque <see langword="for"/>.
    /// </param>
    /// <param name="forBlock">
    /// Acción que permite definir las acciones a ejecutar dentro del
    /// bloque <see langword="for"/>. La acción incluye un parámetro que
    /// puede usarse como la etiqueta de salida para finalizar el bloque
    /// <see langword="for"/>.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
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
    /// Inserta un bloque <see langword="for"/> estructurado en la
    /// secuencia del lenguaje intermedio de Microsoft® (MSIL).
    /// </summary>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar el bloque
    /// <see langword="for"/>.
    /// </param>
    /// <param name="initialValue">
    /// Valor inicial del acumulador del bloque <see langword="for"/>.
    /// </param>
    /// <param name="condition">
    /// Acción que permite definir la evaluación inicial del bloque
    /// <see langword="for"/>. La acción incluye una referencia al 
    /// <see cref="FieldBuilder"/> del acumulador del bloque
    /// <see langword="for"/>.
    /// </param>
    /// <param name="incrementor">
    /// Acción que permite definir la acción de incremento a ejecutar al
    /// final del bloque <see langword="for"/>. La acción incluye una referencia al 
    /// <see cref="FieldBuilder"/> del acumulador del bloque
    /// <see langword="for"/>.
    /// </param>
    /// <param name="forBlock">
    /// Acción que permite definir las acciones a ejecutar dentro del
    /// bloque <see langword="for"/>. La acción incluye una referencia al 
    /// <see cref="FieldBuilder"/> del acumulador del bloque
    /// <see langword="for"/> y un parámetro que puede usarse como la
    /// etiqueta de salida para finalizar el bloque <see langword="for"/>.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
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
    /// Inserta un bloque <see langword="for"/> estructurado en la
    /// secuencia del lenguaje intermedio de Microsoft® (MSIL).
    /// </summary>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar el bloque
    /// <see langword="for"/>.
    /// </param>
    /// <param name="initialValue">
    /// Valor inicial del acumulador del bloque <see langword="for"/>.
    /// </param>
    /// <param name="condition">
    /// Acción que permite definir la evaluación inicial del bloque
    /// <see langword="for"/>. La acción incluye una referencia al 
    /// <see cref="FieldBuilder"/> del acumulador del bloque
    /// <see langword="for"/>.
    /// </param>
    /// <param name="forBlock">
    /// Acción que permite definir las acciones a ejecutar dentro del
    /// bloque <see langword="for"/>. La acción incluye una referencia al 
    /// <see cref="FieldBuilder"/> del acumulador del bloque
    /// <see langword="for"/> y un parámetro que puede usarse como la
    /// etiqueta de salida para finalizar el bloque <see langword="for"/>.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
    /// </returns>
    public static ILGenerator For<T>(this ILGenerator ilGen, T initialValue, Action<LocalBuilder> condition, ForBlock forBlock)
    {
        return For(ilGen, initialValue, condition, accumulator => ilGen.LoadLocal(accumulator).LoadConstant(1).Add().StoreLocal(accumulator), forBlock);
    }

    /// <summary>
    /// Inserta un bloque <see langword="for"/> estructurado simple con un
    /// acumulador <see cref="int"/> en la secuencia del lenguaje
    /// intermedio de Microsoft® (MSIL).
    /// </summary>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar el bloque
    /// <see langword="for"/>.
    /// </param>
    /// <param name="startValue">
    /// Valor inicial del acumulador, inclusive.
    /// </param>
    /// <param name="endValue">
    /// Valor final del acumulador, inclusive.
    /// </param>
    /// <param name="forBlock">
    /// Acción que permite definir las acciones a ejecutar dentro del
    /// bloque <see langword="for"/>. La acción incluye una referencia al 
    /// <see cref="FieldBuilder"/> del acumulador del bloque
    /// <see langword="for"/> y un parámetro que puede usarse como la
    /// etiqueta de salida para finalizar el bloque <see langword="for"/>.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
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
    /// Inserta un bloque <see langword="for"/> estructurado simple con un
    /// acumulador <see cref="int"/> en la secuencia del lenguaje
    /// intermedio de Microsoft® (MSIL).
    /// </summary>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar el bloque
    /// <see langword="for"/>.
    /// </param>
    /// <param name="range">
    /// Rango de valores a utilizar en el acumulador.
    /// </param>
    /// <param name="forBlock">
    /// Acción que permite definir las acciones a ejecutar dentro del
    /// bloque <see langword="for"/>. La acción incluye una referencia al 
    /// <see cref="FieldBuilder"/> del acumulador del bloque
    /// <see langword="for"/> y un parámetro que puede usarse como la
    /// etiqueta de salida para finalizar el bloque <see langword="for"/>.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
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
    /// Inserta un bloque <see langword="foreach"/> estructurado en la
    /// secuencia del lenguaje intermedio de Microsoft® (MSIL).
    /// </summary>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar el bloque
    /// <see langword="for"/>.
    /// </param>
    /// <param name="foreachBlock">
    /// Delegado que permite definir las acciones a ejecutar dentro del
    /// bloque <see langword="foreach"/>. La acción incluye una referencia al 
    /// <see cref="LocalBuilder"/> del elemento actual del bloque
    /// <see langword="foreach"/>.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
    /// </returns>
    /// <remarks>
    /// Esta instrucción espera que el valor en la parte superior de la
    /// pila implemente la interfaz <see cref="IEnumerable{T}"/>.
    /// </remarks>
    public static ILGenerator ForEach<T>(this ILGenerator ilGen, ForEachBlock foreachBlock)
    {
        LocalBuilder itm = ilGen.DeclareLocal(typeof(T));
        return ilGen
            .Call<IEnumerable<T>, Func<IEnumerator<T>>>(p => p.GetEnumerator)
            .StoreNewLocal<IEnumerator<T>>(out LocalBuilder? enumerator)
            .Using(enumerator, (ilGen, _, @break) =>
            {
                ilGen
                    .BranchNewLabel(out Label moveNext)
                    .InsertNewLabel(out Label loopStart)
                    .LoadLocalAddress(enumerator)
                    .Call(ReflectionHelpers.GetProperty<IEnumerator<T>>(p => p.Current).GetMethod!)
                    .StoreLocal(itm);
                foreachBlock(ilGen,itm, @break, moveNext);
                ilGen
                    .PutLabel(moveNext)
                    .LoadLocalAddress(enumerator)
                    .Call<IEnumerator<T>, Func<bool>>(p => p.MoveNext)
                    .BranchTrue(loopStart);
            });
    }

    /// <summary>
    /// Inserta un bloque <see langword="using"/> estructurado en la
    /// secuencia del lenguaje intermedio de Microsoft® (MSIL).
    /// </summary>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar el bloque
    /// <see langword="using"/>.
    /// </param>
    /// <param name="disposable">
    /// Referencia a un <see cref="LocalBuilder"/> que contiene la instancia
    /// del objeto desechable a utilizar dentro del bloque 
    /// <see langword="using"/>.
    /// </param>
    /// <param name="usingBlock">
    /// Delegado que define las acciones a ejecutar dentro del bloque
    /// <see langword="using"/>.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
    /// </returns>
    public static ILGenerator Using(this ILGenerator ilGen, LocalBuilder disposable, UsingBlock usingBlock)
    {
        return ilGen.TryFinally((ilGen, @break) => usingBlock(ilGen, disposable, @break), ilGen => ilGen.Dispose(disposable));
    }

    /// <summary>
    /// Inserta un bloque <see langword="using"/> estructurado en la
    /// secuencia del lenguaje intermedio de Microsoft® (MSIL).
    /// </summary>
    /// <typeparam name="T">
    /// Tipo de objeto desechable a instanciar para utilizar dentro del
    /// bloque <see langword="using"/>.
    /// </typeparam>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar el bloque
    /// <see langword="using"/>.
    /// </param>
    /// <param name="usingBlock">
    /// Delegado que define las acciones a ejecutar dentro del bloque
    /// <see langword="using"/>.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
    /// </returns>
    public static ILGenerator Using<T>(this ILGenerator ilGen, UsingBlock usingBlock) where T : IDisposable, new()
    {
        ilGen.NewObject<T>().StoreNewLocal<T>(out LocalBuilder? disposable);
        return ilGen.TryFinally((ilGen, @break) => usingBlock(ilGen, disposable, @break), ilGen => ilGen.Dispose(disposable));
    }

    /// <summary>
    /// Inserta un salto de transferencia de control incondicional que sale
    /// de una región protegida de código (generalmente un bloque
    /// <see langword="try"/> o <see langword="catch"/>) en la secuencia
    /// del lenguaje intermedio de Microsoft® (MSIL).
    /// </summary>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar el salto.
    /// </param>
    /// <param name="label"></param>
    /// <returns></returns>
    public static ILGenerator Leave(this ILGenerator ilGen, Label label)
    {
        ilGen.Emit(Op.Leave, label);
        return ilGen;
    }

    /// <summary>
    /// Inserta un bloque <see langword="try"/>/<see langword="finally"/>
    /// estructurado en la secuencia del lenguaje intermedio de Microsoft®
    /// (MSIL).
    /// </summary>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar el bloque
    /// <see langword="try"/>.
    /// </param>
    /// <param name="tryBlock">
    /// Acción que permite definir las instrucciones a insertar en el
    /// bloque <see langword="try"/>. La acción incluye un parámetro que
    /// puede usarse como la etiqueta de salida para finalizar el bloque
    /// <see langword="try"/> por medio de una instrucción
    /// <see cref="Leave(ILGenerator, Label)"/>.
    /// </param>
    /// <param name="finallyBlock">
    /// Acción que permite definir las instrucciones a insertar en el
    /// bloque <see langword="finally"/>.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
    /// </returns>
    public static ILGenerator TryFinally(this ILGenerator ilGen, TryBlock tryBlock, Action<ILGenerator> finallyBlock)
    {
        tryBlock(ilGen, ilGen.BeginExceptionBlock());
        ilGen.BeginFinallyBlock();
        finallyBlock(ilGen);
        ilGen.EndExceptionBlock();
        return ilGen;
    }

    /// <summary>
    /// Inserta un bloque <see langword="try"/>/<see langword="catch"/>
    /// estructurado en la secuencia del lenguaje intermedio de Microsoft®
    /// (MSIL).
    /// </summary>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar el bloque
    /// <see langword="for"/>.
    /// </param>
    /// <param name="tryBlock">
    /// Acción que permite definir las instrucciones a insertar en el
    /// bloque <see langword="try"/>. La acción incluye un parámetro que
    /// puede usarse como la etiqueta de salida para finalizar el bloque
    /// <see langword="try"/> por medio de una instrucción
    /// <see cref="Leave(ILGenerator, Label)"/>.
    /// </param>
    /// <param name="catchBlocks">
    /// Colección de bloques <see langword="catch"/> a insertar. Cada
    /// acción incluye un parámetro que puede usarse como la etiqueta de
    /// salida para finalizar el bloque <see langword="catch"/> por medio
    /// de una instrucción <see cref="Leave(ILGenerator, Label)"/>.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
    /// </returns>
    public static ILGenerator TryCatch(this ILGenerator ilGen, TryBlock tryBlock, IEnumerable<KeyValuePair<Type, TryBlock>> catchBlocks)
    {
        InsertCatchBlocks(ilGen, tryBlock, catchBlocks);
        ilGen.EndExceptionBlock();
        return ilGen;
    }

    /// <summary>
    /// Inserta un bloque
    /// <see langword="try"/>/<see langword="catch"/>/<see langword="finally"/>
    /// estructurado en la secuencia del lenguaje intermedio de Microsoft®
    /// (MSIL).
    /// </summary>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar el bloque
    /// <see langword="try"/>.
    /// </param>
    /// <param name="tryBlock">
    /// Acción que permite definir las instrucciones a insertar en el
    /// bloque <see langword="try"/>. La acción incluye un parámetro que
    /// puede usarse como la etiqueta de salida para finalizar el bloque
    /// <see langword="try"/> por medio de una instrucción
    /// <see cref="Leave(ILGenerator, Label)"/>.
    /// </param>
    /// <param name="catchBlocks">
    /// Colección de bloques <see langword="catch"/> a insertar. Cada
    /// acción incluye un parámetro que puede usarse como la etiqueta de
    /// salida para finalizar el bloque <see langword="catch"/> por medio
    /// de una instrucción <see cref="Leave(ILGenerator, Label)"/>.
    /// </param>
    /// <param name="finallyBlock">
    /// Acción que permite definir las instrucciones a insertar en el
    /// bloque <see langword="finally"/>.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
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
    /// Inserta la istanciación y lanzamiento de una excepción sin argumentos
    /// en la secuencia del lenguaje intermedio de Microsoft® (MSIL).
    /// </summary>
    /// <typeparam name="T">
    /// Tipo de la excepción a lanzar. Debe contener un constructor publico sin
    /// parámetros.
    /// </typeparam>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar lanzamiento de la
    /// excepción.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
    /// </returns>
    public static ILGenerator Throw<T>(this ILGenerator ilGen) where T: Exception, new()
    {
        return ilGen.NewObject<T>(Type.EmptyTypes).Throw();
    }

    /// <summary>
    /// Inserta el lanzamiento de una excepción ya instanciada en la pila en la
    /// secuencia del lenguaje intermedio de Microsoft® (MSIL).
    /// </summary>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar lanzamiento de la
    /// excepción.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
    /// </returns>
    public static ILGenerator Throw(this ILGenerator ilGen) => OneLiner(ilGen, Op.Throw);
}
