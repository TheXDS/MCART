/*
ILGeneratorExtensions.cs

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

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
public static class ILGeneratorExtensions
{
    /// <summary>
    /// Define un delegado que describe un bloque <see langword="for"/>.
    /// </summary>
    /// <param name="accumulator">
    /// Referencia al acumulador del ciclo.
    /// </param>
    /// <param name="break">
    /// Etiqueta de salida del bloque <see langword="for"/>.
    /// </param>
    /// <param name="next">
    /// Etiqueta de continuación del bloque <see langword="for"/>.
    /// </param>
    public delegate void ForBlock(LocalBuilder accumulator, Label @break, Label next);

    /// <summary>
    /// Define un delegado que describe un bloque <see langword="foreach"/>.
    /// </summary>
    /// <param name="item">
    /// Referencia al acumulador del ciclo.
    /// </param>
    /// <param name="break">
    /// Etiqueta de salida del bloque <see langword="foreach"/>. Debe ser
    /// invocada por medio de <see cref="Leave(ILGenerator, Label)"/>.
    /// </param>
    /// <param name="continue">
    /// Etiqueta de continuación del bloque <see langword="foreach"/>.
    /// </param>
    public delegate void ForEachBlock(LocalBuilder item, Label @break, Label @continue);

    /// <summary>
    /// Define un delegado que describe un bloque <see langword="try"/>.
    /// </summary>
    /// <param name="leaveTry">
    /// Etiqueta de salida del bloque <see langword="try"/>. Debe ser
    /// invocada por medio de <see cref="Leave(ILGenerator, Label)"/>.
    /// </param>
    public delegate void TryBlock(Label leaveTry);

    /// <summary>
    /// Define un delegado que describe un bloque <see langword="using"/>.
    /// </summary>
    /// <param name="disposable">
    /// Referencia al elemento desechable dentro del bloque <see langword="using"/>.
    /// </param>
    /// <param name="leaveTry">
    /// Etiqueta de salida del bloque <see langword="using"/>. Debe ser
    /// invocada por medio de <see cref="Leave(ILGenerator, Label)"/>.
    /// </param>
    public delegate void UsingBlock(LocalBuilder disposable, Label leaveTry);

    private static readonly HashSet<IConstantLoader> _constantLoaders = new(Objects.FindAllObjects<IConstantLoader>(), new ConstantLoaderComparer());

    /// <summary>
    /// Registra un <see cref="IConstantLoader"/> para el método
    /// <see cref="LoadConstant{T}(ILGenerator, T)"/>.
    /// </summary>
    /// <param name="loader">
    /// <see cref="IConstantLoader"/> a registrar.
    /// </param>
    public static void RegisterConstantLoader(IConstantLoader loader)
    {
        _constantLoaders.Add(loader);
    }

    /// <summary>
    /// Inserta una constante en la secuencia del lenguaje intermedio 
    /// de Microsoft® (MSIL).
    /// </summary>
    /// <typeparam name="T">
    /// Tipo de valor constante a insertar.
    /// </typeparam>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar la carga de la
    /// constante.
    /// </param>
    /// <param name="value">
    /// Valor constante a insertar.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
    /// </returns>
    /// <exception cref="NotImplementedException">
    /// Se produce al intentar cargar un valor constante desconocido.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Se produce al intentar cargar un valor que no es constante, como
    /// una instancia de objeto.
    /// </exception>
    public static ILGenerator LoadConstant<T>(this ILGenerator ilGen, T value)
    {
        Type? t = typeof(T);
        if (_constantLoaders.FirstOrDefault(p => p.ConstantType == t) is IConstantLoader cl)
        {
            cl.Emit(ilGen, value);
        }
        else if (t.IsStruct())
        {
            ilGen.Emit(Newobj, t.GetConstructor(Type.EmptyTypes)!);
        }
        else
        {
            ilGen.Emit(Ldnull);
        }
        return ilGen;
    }

    /// <summary>
    /// Inserta una constante en la secuencia del lenguaje intermedio 
    /// de Microsoft® (MSIL).
    /// </summary>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar la carga de la
    /// constante.
    /// </param>
    /// <param name="t">
    /// Tipo de valor constante a insertar.
    /// </param>
    /// <param name="value">
    /// Valor constante a insertar.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
    /// </returns>
    /// <exception cref="T:System.NotImplementedException">
    /// Se produce al intentar cargar un valor constante desconocido.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    /// Se produce al intentar cargar un valor que no es constante, como
    /// una instancia de objeto.
    /// </exception>
    public static ILGenerator LoadConstant(this ILGenerator ilGen, Type t, object? value)
    {
        if (_constantLoaders.FirstOrDefault(p => p.ConstantType == t) is IConstantLoader cl)
        {
            cl.Emit(ilGen, value);
        }
        else if (t.IsStruct())
        {
            ilGen.Emit(Newobj, t.GetConstructor(Type.EmptyTypes)!);
        }
        else
        {
            ilGen.Emit(Ldnull);
        }
        return ilGen;
    }

    /// <summary>
    /// Inserta una constante en la secuencia del lenguaje intermedio 
    /// de Microsoft® (MSIL).
    /// </summary>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar la carga de la
    /// constante.
    /// </param>
    /// <param name="value">
    /// Valor constante a insertar.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
    /// </returns>
    /// <exception cref="NotImplementedException">
    /// Se produce al intentar cargar un valor constante desconocido.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Se produce al intentar cargar un valor que no es constante, como
    /// una instancia de objeto.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// Se produce si <paramref name="value"/> es <see langword="null"/>.
    /// Si necesita cargar un valor constante <see langword="null"/>,
    /// utilice el método <see cref="LoadNull(ILGenerator)"/>.
    /// </exception>
    public static ILGenerator LoadConstant(this ILGenerator ilGen, object? value)
    {
        if (value is null) throw new ArgumentNullException(nameof(value));
        return LoadConstant(ilGen, value.GetType(), value);
    }

    /// <summary>
    /// Inserta la instanciación de un objeto en la secuencia del
    /// lenguaje intermedio de Microsoft® (MSIL).
    /// </summary>
    /// <typeparam name="T">
    /// Tipo de objeto a instanciar.
    /// </typeparam>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar la instanciación
    /// del objeto.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
    /// </returns>
    /// <exception cref="ClassNotInstantiableException">
    /// Se produce si la clase no es instanciable, o si no existe un 
    /// constructor que acepte los argumentos especificados.
    /// También puede producirse si uno de los parámetros es un objeto,
    /// y no contiene un constructor predeterminado sin argumentos, en
    /// cuyo caso, la excepción indicará el tipo que no puede
    /// instanciarse.
    /// </exception>
    public static ILGenerator NewObject<T>(this ILGenerator ilGen)
    {
        return NewObject(ilGen, typeof(T));
    }

    /// <summary>
    /// Inserta la instanciación de un objeto en la secuencia del
    /// lenguaje intermedio de Microsoft® (MSIL).
    /// </summary>
    /// <typeparam name="T">
    /// Tipo de objeto a instanciar.
    /// </typeparam>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar la instanciación
    /// del objeto.
    /// </param>
    /// <param name="args">
    /// Argumentos a pasar al constructor del objeto.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
    /// </returns>
    /// <exception cref="ClassNotInstantiableException">
    /// Se produce si la clase no es instanciable, o si no existe un 
    /// constructor que acepte los argumentos especificados.
    /// También puede producirse si uno de los parámetros es un objeto,
    /// y no contiene un constructor predeterminado sin argumentos, en
    /// cuyo caso, la excepción indicará el tipo que no puede
    /// instanciarse.
    /// </exception>
    public static ILGenerator NewObject<T>(this ILGenerator ilGen, IEnumerable args)
    {
        return NewObject(ilGen, typeof(T), args);
    }

    /// <summary>
    /// Inserta la instanciación de un objeto en la secuencia del
    /// lenguaje intermedio de Microsoft® (MSIL).
    /// </summary>
    /// <param name="type">
    /// Tipo de objeto a instanciar.
    /// </param>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar la instanciación
    /// del objeto.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
    /// </returns>
    /// <exception cref="ClassNotInstantiableException">
    /// Se produce si la clase no es instanciable, o si no existe un 
    /// constructor que acepte los argumentos especificados.
    /// También puede producirse si uno de los parámetros es un objeto,
    /// y no contiene un constructor predeterminado sin argumentos, en
    /// cuyo caso, la excepción indicará el tipo que no puede
    /// instanciarse.
    /// </exception>
    public static ILGenerator NewObject(this ILGenerator ilGen, Type type)
    {
        return NewObject(ilGen, type, Array.Empty<object>());
    }

    /// <summary>
    /// Inserta la instanciación de un objeto en la secuencia del
    /// lenguaje intermedio de Microsoft® (MSIL).
    /// </summary>
    /// <param name="type">
    /// Tipo de objeto a instanciar.
    /// </param>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar la instanciación
    /// del objeto.
    /// </param>
    /// <param name="args">
    /// Argumentos a pasar al constructor del objeto.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
    /// </returns>
    /// <exception cref="ClassNotInstantiableException">
    /// Se produce si la clase no es instanciable, o si no existe un 
    /// constructor que acepte los argumentos especificados.
    /// También puede producirse si uno de los parámetros es un objeto,
    /// y no contiene un constructor predeterminado sin argumentos, en
    /// cuyo caso, la excepción indicará el tipo que no puede
    /// instanciarse.
    /// </exception>
    public static ILGenerator NewObject(this ILGenerator ilGen, Type type, IEnumerable args)
    {
        if (type.GetConstructor(args.ToTypes().ToArray()) is not ConstructorInfo c)
            throw new ClassNotInstantiableException(type);
        foreach (object? j in args)
        {
            if (j is null)
                ilGen.LoadNull();
            else if (j.GetType().IsClass)
                NewObject(ilGen, j.GetType());
            else
                LoadConstant(ilGen, j);
        }
        ilGen.Emit(Newobj, c);
        return ilGen;
    }

    /// <summary>
    /// Inserta una llamada al método de instancia especificado en la
    /// secuencia del lenguaje intermedio de Microsoft® (MSIL).
    /// </summary>
    /// <typeparam name="TClass">
    /// Clase en la que reside el método de instancia.
    /// </typeparam>
    /// <typeparam name="TMethod">
    /// Delegado que describe al método a llamar.
    /// </typeparam>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar la llamada al método
    /// de instancia.
    /// </param>
    /// <param name="methodSelector">
    /// Expresión que permite seleccionar al método de instancia a llamar.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
    /// </returns>
    public static ILGenerator Call<TClass, TMethod>(this ILGenerator ilGen, Expression<Func<TClass, TMethod>> methodSelector) where TMethod : Delegate
    {
        return Call(ilGen, ReflectionHelpers.GetMethod(methodSelector));
    }

    /// <summary>
    /// Inserta una llamada al constructor de instancia especificado del
    /// tipo base en la secuencia del lenguaje intermedio de Microsoft®
    /// (MSIL).
    /// </summary>
    /// <typeparam name="TClass">
    /// Clase desde la cual se llamará al constructor de instancia del tipo
    /// base.
    /// </typeparam>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar la llamada al método
    /// de instancia.
    /// </param>
    /// <param name="baseCtorArgs">
    /// Arreglo de tipos de argumentos del constructor a llamar.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
    /// </returns>
    public static ILGenerator CallBaseCtor<TClass>(this ILGenerator ilGen, Type[] baseCtorArgs)
    {
        ilGen.This();
        ilGen.Emit(Op.Call, typeof(TClass).GetConstructor(baseCtorArgs)
            ?? typeof(TClass).GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance).FirstOrDefault(p => p.GetParameters().Select(p => p.ParameterType).ItemsEqual(baseCtorArgs))
            ?? throw new MissingMemberException());
        return ilGen;
    }

    /// <summary>
    /// Inserta una llamada al constructor de instancia del tipo base sin
    /// argumentos en la secuencia del lenguaje intermedio de Microsoft®
    /// (MSIL).
    /// </summary>
    /// <typeparam name="TClass">
    /// Clase desde la cual se llamará al constructor de instancia del tipo
    /// base.
    /// </typeparam>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar la llamada al método
    /// de instancia.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
    /// </returns>
    public static ILGenerator CallBaseCtor<TClass>(this ILGenerator ilGen) => CallBaseCtor<TClass>(ilGen, Type.EmptyTypes);

    /// <summary>
    /// Inserta una llamada al método estático especificado en la secuencia
    /// del lenguaje intermedio de Microsoft® (MSIL).
    /// </summary>
    /// <typeparam name="TMethod">
    /// Delegado que describe al método a llamar.
    /// </typeparam>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar la llamada al método
    /// estático.
    /// </param>
    /// <param name="methodSelector">
    /// Expresión que permite seleccionar al método estático a llamar.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
    /// </returns>
    public static ILGenerator Call<TMethod>(this ILGenerator ilGen, Expression<Func<TMethod>> methodSelector) where TMethod : Delegate
    {
        return Call(ilGen, ReflectionHelpers.GetMethod(methodSelector));
    }

    /// <summary>
    /// Inserta una llamada al método estático especificado en la secuencia
    /// del lenguaje intermedio de Microsoft® (MSIL).
    /// </summary>
    /// <typeparam name="TMethod">
    /// Delegado que describe al método a llamar.
    /// </typeparam>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar la llamada al método
    /// estático.
    /// </param>
    /// <param name="method">
    /// Método estático a llamar.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
    /// </returns>
    public static ILGenerator Call<TMethod>(this ILGenerator ilGen, TMethod method) where TMethod : Delegate
    {
        return Call(ilGen, method.Method);
    }

    /// <summary>
    /// Inserta una llamada al método especificado.
    /// </summary>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar la llamada al método
    /// estático.
    /// </param>
    /// <param name="method">Método a llamar.</param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
    /// </returns>
    public static ILGenerator Call(this ILGenerator ilGen, MethodInfo method)
    {
        ilGen.Emit(method.IsVirtual ? Callvirt : Op.Call, method);
        return ilGen;
    }

    /// <summary>
    /// Inserta una llamada explícitamente virtual al método especificado.
    /// </summary>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar la llamada.
    /// </param>
    /// <param name="method">Método a llamar.</param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
    /// </returns>
    public static ILGenerator CallVirt(this ILGenerator ilGen, MethodInfo method)
    {
        ilGen.Emit(Callvirt, method);
        return ilGen;
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
    public static ILGenerator For(this ILGenerator ilGen, LocalBuilder accumulator, object? initialValue, Action condition, Action incrementor, ForBlock forBlock)
    {
        Label next = ilGen.DefineLabel();
        ilGen
            .InitLocal(accumulator, initialValue)
            .InsertNewLabel(out Label @for);
        condition();
        ilGen.BranchFalseNewLabel(out Label endFor);
        forBlock(accumulator, endFor, next);
        ilGen.PutLabel(next);
        incrementor();
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
        forBlock(acc, endFor, next);
        ilGen.PutLabel(next);
        incrementor(acc);
        return ilGen
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
        forBlock(acc, endFor, next);
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
        forBlock(acc, endFor, next);

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
        LocalBuilder? itm = ilGen.DeclareLocal(typeof(T));
        return ilGen
            .Call<IEnumerable<T>, Func<IEnumerator<T>>>(p => p.GetEnumerator)
            .StoreNewLocal<IEnumerator<T>>(out LocalBuilder? enumerator)
            .Using(enumerator, (_, @break) =>
            {
                ilGen
                    .BranchNewLabel(out Label moveNext)
                    .InsertNewLabel(out Label loopStart)
                    .LoadLocalAddress(enumerator)
                    .Call(ReflectionHelpers.GetProperty<IEnumerator<T>>(p => p.Current).GetMethod!)
                    .StoreLocal(itm);
                foreachBlock(itm, @break, moveNext);
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
        return ilGen.TryFinally(@break => usingBlock(disposable, @break), () => ilGen.Dispose(disposable));
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
        return ilGen.TryFinally(@break => usingBlock(disposable, @break), () => ilGen.Dispose(disposable));
    }

    /// <summary>
    /// Desecha un objeto <see cref="IDisposable"/> contenido en el
    /// <see cref="LocalBuilder"/> especificado.
    /// </summary>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar la llamada.
    /// </param>
    /// <param name="disposable">
    /// Variable local que contiene el objeto <see cref="IDisposable"/> a
    /// desechar.
    /// </param>
    /// <returns></returns>
    public static ILGenerator Dispose(this ILGenerator ilGen, LocalBuilder disposable)
    {
        return ilGen.LoadLocalAddress(disposable).Call<IDisposable, Action>(p => p.Dispose);
    }

    /// <summary>
    /// Define e inserta una nueva etiqueta en la secuencia del lenguaje
    /// intermedio de Microsoft® (MSIL).
    /// </summary>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar la nueva etiqueta.
    /// </param>
    /// <param name="label">
    /// Etiqueta que ha sido definida e insertada.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
    /// </returns>
    public static ILGenerator InsertNewLabel(this ILGenerator ilGen, out Label label)
    {
        return ilGen.PutLabel(label = ilGen.DefineLabel());
    }

    /// <summary>
    /// Inserta la etiqueta en la secuencia del lenguaje intermedio (MSIL) 
    /// de Microsoft® en la posición actual.
    /// </summary>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar la etiqueta.
    /// </param>
    /// <param name="label">
    /// Etiqueta que será insertada.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
    /// </returns>
    public static ILGenerator PutLabel(this ILGenerator ilGen, Label label)
    {
        ilGen.MarkLabel(label);
        return ilGen;
    }

    /// <summary>
    /// Inserta un salto de transferencia de control incondicional en la
    /// secuencia del lenguaje intermedio de Microsoft® (MSIL) a una 
    /// etiqueta.
    /// </summary>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar el salto.
    /// </param>
    /// <param name="label">
    /// Etiqueta que será el destino del salto.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
    /// </returns>
    public static ILGenerator Branch(this ILGenerator ilGen, Label label)
    {
        ilGen.Emit(Br, label);
        return ilGen;
    }

    /// <summary>
    /// Inserta un salto de transferencia de control incondicional en la
    /// secuencia del lenguaje intermedio de Microsoft® (MSIL) a una nueva
    /// etiqueta.
    /// </summary>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar el salto.
    /// </param>
    /// <param name="label">
    /// Nueva etiqueta que será el destino del salto.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
    /// </returns>
    public static ILGenerator BranchNewLabel(this ILGenerator ilGen, out Label label)
    {
        label = ilGen.DefineLabel();
        ilGen.Emit(Br, label);
        return ilGen;
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
    /// Inserta un salto condicional de transferencia de control en la
    /// secuencia del lenguaje intermedio de Microsoft® (MSIL) a una nueva
    /// etiqueta, realizando el salto si el valor en la parte superior de
    /// la pila se evalúa como <see langword="true"/>.
    /// </summary>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar el salto.
    /// </param>
    /// <param name="label">
    /// Etiqueta que será el destino del salto.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
    /// </returns>
    public static ILGenerator BranchTrue(this ILGenerator ilGen, Label label)
    {
        return BranchIf(ilGen, label, Brtrue);
    }

    /// <summary>
    /// Inserta un salto condicional de transferencia de control en la
    /// secuencia del lenguaje intermedio de Microsoft® (MSIL) a una nueva
    /// etiqueta, realizando el salto si el valor en la parte superior de
    /// la pila se evalúa como <see langword="true"/>.
    /// </summary>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar el salto.
    /// </param>
    /// <param name="label">
    /// Nueva etiqueta que será el destino del salto.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
    /// </returns>
    public static ILGenerator BranchTrueNewLabel(this ILGenerator ilGen, out Label label)
    {
        return BranchIfNewLabel(ilGen, out label, Brtrue);
    }

    /// <summary>
    /// Inserta un salto condicional de transferencia de control en la
    /// secuencia del lenguaje intermedio de Microsoft® (MSIL) a una nueva
    /// etiqueta, realizando el salto si el valor en la parte superior de
    /// la pila se evalúa como <see langword="false"/>.
    /// </summary>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar el salto.
    /// </param>
    /// <param name="label">
    /// Etiqueta que será el destino del salto.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
    /// </returns>
    public static ILGenerator BranchFalse(this ILGenerator ilGen, Label label)
    {
        return BranchIf(ilGen, label, Brfalse);
    }

    /// <summary>
    /// Inserta un salto condicional de transferencia de control en la
    /// secuencia del lenguaje intermedio de Microsoft® (MSIL) a una nueva
    /// etiqueta, realizando el salto si el valor en la parte superior de
    /// la pila se evalúa como <see langword="false"/>.
    /// </summary>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar el salto.
    /// </param>
    /// <param name="label">
    /// Nueva etiqueta que será el destino del salto.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
    /// </returns>
    public static ILGenerator BranchFalseNewLabel(this ILGenerator ilGen, out Label label)
    {
        return BranchIfNewLabel(ilGen, out label, Brfalse);
    }

    /// <summary>
    /// Inserta un salto condicional de transferencia de control en la
    /// secuencia del lenguaje intermedio de Microsoft® (MSIL) a una nueva
    /// etiqueta; realizando el salto si luego de comparar los dos valores
    /// en la parte superior de la pila, el primero resulta ser mayor que
    /// el segundo.
    /// </summary>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar el salto.
    /// </param>
    /// <param name="label">
    /// Etiqueta que será el destino del salto.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
    /// </returns>
    public static ILGenerator BranchGreaterThan(this ILGenerator ilGen, Label label)
    {
        return BranchIf(ilGen, label, Bgt);
    }

    /// <summary>
    /// Inserta un salto condicional de transferencia de control en la
    /// secuencia del lenguaje intermedio de Microsoft® (MSIL) a una nueva
    /// etiqueta; realizando el salto si luego de comparar los dos valores
    /// en la parte superior de la pila, el primero resulta ser mayor que
    /// el segundo.
    /// </summary>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar el salto.
    /// </param>
    /// <param name="label">
    /// Nueva etiqueta que será el destino del salto.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
    /// </returns>
    public static ILGenerator BranchGreaterThanNewLabel(this ILGenerator ilGen, out Label label)
    {
        return BranchIfNewLabel(ilGen, out label, Bgt);
    }

    /// <summary>
    /// Inserta un salto condicional de transferencia de control en la
    /// secuencia del lenguaje intermedio de Microsoft® (MSIL) a una nueva
    /// etiqueta; realizando el salto si luego de comparar los dos valores
    /// en la parte superior de la pila, el primero resulta ser menor que
    /// el segundo.
    /// </summary>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar el salto.
    /// </param>
    /// <param name="label">
    /// Etiqueta que será el destino del salto.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
    /// </returns>
    public static ILGenerator BranchLessThan(this ILGenerator ilGen, Label label)
    {
        return BranchIf(ilGen, label, Blt);
    }

    /// <summary>
    /// Inserta un salto condicional de transferencia de control en la
    /// secuencia del lenguaje intermedio de Microsoft® (MSIL) a una nueva
    /// etiqueta; realizando el salto si luego de comparar los dos valores
    /// en la parte superior de la pila, el primero resulta ser menor que
    /// el segundo.
    /// </summary>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar el salto.
    /// </param>
    /// <param name="label">
    /// Nueva etiqueta que será el destino del salto.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
    /// </returns>
    public static ILGenerator BranchLessThanNewLabel(this ILGenerator ilGen, out Label label)
    {
        return BranchIfNewLabel(ilGen, out label, Blt);
    }

    /// <summary>
    /// Inserta un salto condicional de transferencia de control en la
    /// secuencia del lenguaje intermedio de Microsoft® (MSIL) a una nueva
    /// etiqueta; realizando el salto si luego de comparar los dos valores
    /// en la parte superior de la pila, el primero resulta ser igual al
    /// segundo.
    /// </summary>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar el salto.
    /// </param>
    /// <param name="label">
    /// Etiqueta que será el destino del salto.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
    /// </returns>
    public static ILGenerator BranchEqual(this ILGenerator ilGen, Label label)
    {
        return BranchIf(ilGen, label, Beq);
    }

    /// <summary>
    /// Inserta un salto condicional de transferencia de control en la
    /// secuencia del lenguaje intermedio de Microsoft® (MSIL) a una nueva
    /// etiqueta; realizando el salto si luego de comparar los dos valores
    /// en la parte superior de la pila, el primero resulta ser igual al
    /// segundo.
    /// </summary>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar el salto.
    /// </param>
    /// <param name="label">
    /// Nueva etiqueta que será el destino del salto.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
    /// </returns>
    public static ILGenerator BranchEqualNewLabel(this ILGenerator ilGen, out Label label)
    {
        return BranchIfNewLabel(ilGen, out label, Beq);
    }

    /// <summary>
    /// Inserta un salto condicional de transferencia de control en la
    /// secuencia del lenguaje intermedio de Microsoft® (MSIL) a una nueva
    /// etiqueta; realizando el salto si luego de comparar los dos valores
    /// en la parte superior de la pila, el primero resulta ser mayor o
    /// igual que el segundo.
    /// </summary>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar el salto.
    /// </param>
    /// <param name="label">
    /// Etiqueta que será el destino del salto.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
    /// </returns>
    public static ILGenerator BranchGreaterThanOrEqual(this ILGenerator ilGen, Label label)
    {
        return BranchIf(ilGen, label, Bge);
    }

    /// <summary>
    /// Inserta un salto condicional de transferencia de control en la
    /// secuencia del lenguaje intermedio de Microsoft® (MSIL) a una nueva
    /// etiqueta; realizando el salto si luego de comparar los dos valores
    /// en la parte superior de la pila, el primero resulta ser mayor o
    /// igual que el segundo.
    /// </summary>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar el salto.
    /// </param>
    /// <param name="label">
    /// Nueva etiqueta que será el destino del salto.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
    /// </returns>
    public static ILGenerator BranchGreaterThanOrEqualNewLabel(this ILGenerator ilGen, out Label label)
    {
        return BranchIfNewLabel(ilGen, out label, Bge);
    }

    /// <summary>
    /// Inserta un salto condicional de transferencia de control en la
    /// secuencia del lenguaje intermedio de Microsoft® (MSIL) a una nueva
    /// etiqueta; realizando el salto si luego de comparar los dos valores
    /// en la parte superior de la pila, el primero resulta ser menor o
    /// igual que el segundo.
    /// </summary>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar el salto.
    /// </param>
    /// <param name="label">
    /// Etiqueta que será el destino del salto.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
    /// </returns>
    public static ILGenerator BranchLessThanOrEqual(this ILGenerator ilGen, Label label)
    {
        return BranchIf(ilGen, label, Ble);
    }

    /// <summary>
    /// Inserta un salto condicional de transferencia de control en la
    /// secuencia del lenguaje intermedio de Microsoft® (MSIL) a una nueva
    /// etiqueta; realizando el salto si luego de comparar los dos valores
    /// en la parte superior de la pila, el primero resulta ser menor o
    /// igual que el segundo.
    /// </summary>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar el salto.
    /// </param>
    /// <param name="label">
    /// Nueva etiqueta que será el destino del salto.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
    /// </returns>
    public static ILGenerator BranchLessThanOrEqualNewLabel(this ILGenerator ilGen, out Label label)
    {
        return BranchIfNewLabel(ilGen, out label, Ble);
    }

    /// <summary>
    /// Inserta un salto condicional de transferencia de control en la
    /// secuencia del lenguaje intermedio de Microsoft® (MSIL) a una nueva
    /// etiqueta; realizando el salto si luego de comparar los dos valores
    /// en la parte superior de la pila, el primero resulta ser distinto al
    /// segundo.
    /// </summary>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar el salto.
    /// </param>
    /// <param name="label">
    /// Etiqueta que será el destino del salto.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
    /// </returns>
    public static ILGenerator BranchNotEqual(this ILGenerator ilGen, Label label)
    {
        return BranchIf(ilGen, label, Bne_Un);
    }

    /// <summary>
    /// Inserta un salto condicional de transferencia de control en la
    /// secuencia del lenguaje intermedio de Microsoft® (MSIL) a una nueva
    /// etiqueta; realizando el salto si luego de comparar los dos valores
    /// en la parte superior de la pila, el primero resulta ser distinto al
    /// segundo.
    /// </summary>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar el salto.
    /// </param>
    /// <param name="label">
    /// Nueva etiqueta que será el destino del salto.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
    /// </returns>
    public static ILGenerator BranchNotEqualNewLabel(this ILGenerator ilGen, out Label label)
    {
        return BranchIfNewLabel(ilGen, out label, Bne_Un);
    }

    /// <summary>
    /// Inserta la carga de la referencia a una variable local en la
    /// secuencia del lenguaje intermedio de Microsoft® (MSIL).
    /// </summary>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar la carga de la
    /// referencia.
    /// </param>
    /// <param name="local">
    /// Variable local para la cual cargar la referencia.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
    /// </returns>
    public static ILGenerator LoadLocalAddress(this ILGenerator ilGen, LocalBuilder local)
    {
        ilGen.Emit(Ldloca, local);
        return ilGen;
    }

    /// <summary>
    /// Inserta la carga del valor de una variable local en la secuencia
    /// del lenguaje intermedio de Microsoft® (MSIL).
    /// </summary>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar la carga del
    /// valor.
    /// </param>
    /// <param name="local">
    /// Variable local desde la cual cargar el valor.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
    /// </returns>
    public static ILGenerator LoadLocal(this ILGenerator ilGen, LocalBuilder local)
    {
        ilGen.Emit(Ldloc, local);
        return ilGen;
    }

    /// <summary>
    /// Inserta el almacenamiento de un valor a una variable local en la
    /// secuencia del lenguaje intermedio de Microsoft® (MSIL).
    /// </summary>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar el almacenamiento de
    /// un valor.
    /// </param>
    /// <param name="local">
    /// Variable local en la cual almacenar el valor.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
    /// </returns>
    public static ILGenerator StoreLocal(this ILGenerator ilGen, LocalBuilder local)
    {
        ilGen.Emit(Stloc, local);
        return ilGen;
    }

    /// <summary>
    /// Inserta el almacenamiento de un valor a una nueva variable local en
    /// la secuencia del lenguaje intermedio de Microsoft® (MSIL).
    /// </summary>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar el almacenamiento de
    /// un valor.
    /// </param>
    /// <param name="local">
    /// Variable local en la cual almacenar el valor.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
    /// </returns>
    public static ILGenerator StoreNewLocal<T>(this ILGenerator ilGen, out LocalBuilder local)
    {
        local = ilGen.DeclareLocal(typeof(T));
        ilGen.Emit(Stloc, local);
        return ilGen;
    }

    /// <summary>
    /// Inserta la inicialización de una nueva variable local en la
    /// secuencia del lenguaje intermedio de Microsoft® (MSIL).
    /// </summary>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar la inicialización de
    /// una nueva variable local.
    /// </param>
    /// <param name="local">
    /// Variable local en la cual almacenar el valor.
    /// </param>
    /// <param name="value">
    /// Valor constante a almacenar en la variable local.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
    /// </returns>
    public static ILGenerator InitNewLocal<T>(this ILGenerator ilGen, T value, out LocalBuilder local)
    {
        return InitLocal(ilGen, local = ilGen.DeclareLocal(typeof(T)), value);
    }

    /// <summary>
    /// Inserta la inicialización de una variable local en la secuencia del
    /// lenguaje intermedio de Microsoft® (MSIL).
    /// </summary>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar la inicialización de
    /// una variable local.
    /// </param>
    /// <param name="local">
    /// Variable local en la cual almacenar el valor.
    /// </param>
    /// <param name="value">
    /// Valor constante a almacenar en la variable local.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
    /// </returns>
    public static ILGenerator InitLocal(this ILGenerator ilGen, LocalBuilder local, object? value)
    {
        return ilGen.LoadConstant(local.LocalType, value).StoreLocal(local);
    }

    /// <summary>
    /// Inserta la carga del valor de un campo en la secuencia del lenguaje
    /// intermedio de Microsoft® (MSIL).
    /// </summary>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar la carga del valor.
    /// </param>
    /// <param name="field">
    /// Campo desde el cual cargar el valor.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
    /// </returns>
    public static ILGenerator LoadField(this ILGenerator ilGen, FieldInfo field)
    {
        return LoadField(ilGen, field, Ldfld);
    }

    /// <summary>
    /// Inserta el almacenamiento de un valor a un campo en la secuencia
    /// del lenguaje intermedio de Microsoft® (MSIL).
    /// </summary>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar el almacenamiento de
    /// un valor.
    /// </param>
    /// <param name="field">
    /// Campo en el cual almacenar el valor.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
    /// </returns>
    public static ILGenerator StoreField(this ILGenerator ilGen, FieldInfo field)
    {
        ilGen.Emit(Stfld, field);
        return ilGen;
    }

    /// <summary>
    /// Inserta el almacenamiento de un valor a una propiedad en la
    /// secuencia del lenguaje intermedio de Microsoft® (MSIL).
    /// </summary>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar el almacenamiento de
    /// un valor.
    /// </param>
    /// <param name="prop">
    /// Propiedad en la cual almacenar el valor.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
    /// </returns>
    public static ILGenerator StoreProperty(this ILGenerator ilGen, PropertyInfo prop)
    {
        if (!prop.SetMethod?.IsStatic ?? throw new InvalidOperationException()) ilGen.This();
        return ilGen.Call(prop.SetMethod!);
    }

    /// <summary>
    /// Inserta la carga de la referencia a un campo en la secuencia del
    /// lenguaje intermedio de Microsoft® (MSIL).
    /// </summary>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar la carga de la
    /// referencia.
    /// </param>
    /// <param name="field">
    /// Campo para el cual cargar la referencia.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
    /// </returns>
    public static ILGenerator LoadFieldAddress(this ILGenerator ilGen, FieldInfo field)
    {
        return LoadField(ilGen, field, Ldflda);
    }

    /// <summary>
    /// Inserta la carga del valor de una propiedad en la secuencia del
    /// lenguaje intermedio de Microsoft® (MSIL).
    /// </summary>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar la carga del valor.
    /// </param>
    /// <param name="property">
    /// Propiedad desde la cual cargar el valor.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
    /// </returns>
    public static ILGenerator LoadProperty(this ILGenerator ilGen, PropertyInfo property)
    {
        MethodInfo? m = property.GetGetMethod()!;
        if (m.IsStatic)
        {
            return ilGen.Call(m);
        }
        else
        {
            return ilGen.This().Call(m);
        }
    }

    /// <summary>
    /// Inserta la carga del valor de una propiedad en la secuencia del
    /// lenguaje intermedio de Microsoft® (MSIL).
    /// </summary>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar la carga del valor.
    /// </param>
    /// <param name="property">
    /// Propiedad desde la cual cargar el valor.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
    /// </returns>
    public static ILGenerator LoadProperty(this ILGenerator ilGen, PropertyBuildInfo property)
    {
        return LoadProperty(ilGen, property.Member);
    }

    /// <summary>
    /// Inserta la carga del valor de una propiedad en la secuencia del
    /// lenguaje intermedio de Microsoft® (MSIL).
    /// </summary>
    /// <typeparam name="T">
    /// Tipo de objeto desde el cual seleccionar la propiedad a cargar.
    /// </typeparam>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar la carga del valor.
    /// </param>
    /// <param name="propertySelector">
    /// Expresión que indica qué propiedad del tipo debe devolverse.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
    /// </returns>
    public static ILGenerator LoadProperty<T>(this ILGenerator ilGen, Expression<Func<T, object?>> propertySelector)
    {
        return LoadProperty(ilGen, ReflectionHelpers.GetProperty(propertySelector));
    }

    /// <summary>
    /// Inserta un bloque <see langword="try"/>/<see langword="finally"/>
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
    /// <param name="finallyBlock">
    /// Acción que permite definir las instrucciones a insertar en el
    /// bloque <see langword="finally"/>.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
    /// </returns>
    public static ILGenerator TryFinally(this ILGenerator ilGen, TryBlock tryBlock, Action finallyBlock)
    {
        tryBlock(ilGen.BeginExceptionBlock());
        ilGen.BeginFinallyBlock();
        finallyBlock();
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
    /// <param name="finallyBlock">
    /// Acción que permite definir las instrucciones a insertar en el
    /// bloque <see langword="finally"/>.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
    /// </returns>
    public static ILGenerator TryCatchFinally(this ILGenerator ilGen, TryBlock tryBlock, IEnumerable<KeyValuePair<Type, TryBlock>> catchBlocks, Action finallyBlock)
    {
        InsertCatchBlocks(ilGen, tryBlock, catchBlocks);
        ilGen.BeginFinallyBlock();
        finallyBlock();
        ilGen.EndExceptionBlock();
        return ilGen;
    }

    /// <summary>
    /// Inserta una operación de suma en la secuencia del lenguaje 
    /// intermedio de Microsoft® (MSIL).
    /// </summary>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar la operación.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
    /// </returns>
    /// <remarks>
    /// Este método espera que existan dos valores en la pila sobre los
    /// cuales pueda aplicarse la operación de suma.
    /// </remarks>
    public static ILGenerator Add(this ILGenerator ilGen) => OneLiner(ilGen, Op.Add);

    /// <summary>
    /// Inserta una operación de resta en la secuencia del lenguaje 
    /// intermedio de Microsoft® (MSIL).
    /// </summary>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar la operación.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
    /// </returns>
    /// <remarks>
    /// Este método espera que existan dos valores en la pila sobre los
    /// cuales pueda aplicarse la operación de resta.
    /// </remarks>
    public static ILGenerator Subtract(this ILGenerator ilGen) => OneLiner(ilGen, Sub);

    /// <summary>
    /// Inserta una operación de multiplicación en la secuencia del
    /// lenguaje intermedio de Microsoft® (MSIL).
    /// </summary>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar la operación.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
    /// </returns>
    /// <remarks>
    /// Este método espera que existan dos valores en la pila sobre los
    /// cuales pueda aplicarse la operación de multiplicación.
    /// </remarks>
    public static ILGenerator Multiply(this ILGenerator ilGen) => OneLiner(ilGen, Mul);

    /// <summary>
    /// Inserta una operación de división en la secuencia del lenguaje 
    /// intermedio de Microsoft® (MSIL).
    /// </summary>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar la operación.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
    /// </returns>
    /// <remarks>
    /// Este método espera que existan dos valores en la pila sobre los
    /// cuales pueda aplicarse la operación de división.
    /// </remarks>
    public static ILGenerator Divide(this ILGenerator ilGen) => OneLiner(ilGen, Div);

    /// <summary>
    /// Inserta una operación de residuo en la secuencia del lenguaje 
    /// intermedio de Microsoft® (MSIL).
    /// </summary>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar la operación.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
    /// </returns>
    /// <remarks>
    /// Este método espera que existan dos valores en la pila sobre los
    /// cuales pueda aplicarse la operación de residuo.
    /// </remarks>
    public static ILGenerator Remainder(this ILGenerator ilGen) => OneLiner(ilGen, Rem);

    /// <summary>
    /// Inserta una instrucción NOP en la secuencia del lenguaje intermedio de
    /// Microsoft® (MSIL).
    /// </summary>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar la operación.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
    /// </returns>
    public static ILGenerator Nop(this ILGenerator ilGen) => OneLiner(ilGen, OpCodes.Nop);

    /// <summary>
    /// Inserta la carga de la referencia a la instancia del tipo (el valor
    /// <see langword="this"/>) en la secuencia del lenguaje intermedio de
    /// Microsoft® (MSIL).
    /// </summary>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar la operación.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
    /// </returns>
    public static ILGenerator This(this ILGenerator ilGen) => OneLiner(ilGen, Ldarg_0);

    /// <summary>
    /// Inserta la carga del primer argumento de un método en la secuencia
    /// del lenguaje intermedio de Microsoft® (MSIL).
    /// </summary>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar la operación.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
    /// </returns>
    public static ILGenerator LoadArg1(this ILGenerator ilGen) => OneLiner(ilGen, Ldarg_1);

    /// <summary>
    /// Inserta la carga del segundo argumento de un método en la secuencia
    /// del lenguaje intermedio de Microsoft® (MSIL).
    /// </summary>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar la operación.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
    /// </returns>
    public static ILGenerator LoadArg2(this ILGenerator ilGen) => OneLiner(ilGen, Ldarg_2);

    /// <summary>
    /// Inserta la carga del tercer argumento de un método en la secuencia
    /// del lenguaje intermedio de Microsoft® (MSIL).
    /// </summary>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar la operación.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
    /// </returns>
    public static ILGenerator LoadArg3(this ILGenerator ilGen) => OneLiner(ilGen, Ldarg_3);

    /// <summary>
    /// Inserta la carga de un argumento en la secuencia del lenguaje
    /// intermedio de Microsoft® (MSIL).
    /// </summary>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar la operación.
    /// </param>
    /// <param name="argIndex">
    /// Índice del argumento a cargar en la pila. El valor de <c>0</c>
    /// cargará una referencia a la instancia del tipo actual (el valor
    /// <see langword="this"/>).
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
    /// </returns>
    public static ILGenerator LoadArg(this ILGenerator ilGen, short argIndex)
    {
        if (argIndex < 0) throw new ArgumentOutOfRangeException(nameof(argIndex));
        ilGen.Emit(Ldarg, argIndex);
        return ilGen;
    }

    /// <summary>
    /// Inserta la carga de la referencia a un argumento en la secuencia
    /// del lenguaje intermedio de Microsoft® (MSIL).
    /// </summary>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar la operación.
    /// </param>
    /// <param name="argIndex">
    /// Índice del argumento para el cual cargar una referencia. El valor
    /// de <c>0</c> cargará una referencia a la instancia del tipo actual
    /// (el valor <see langword="this"/>).
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
    /// </returns>
    public static ILGenerator LoadArgAddress(this ILGenerator ilGen, short argIndex)
    {
        if (argIndex < 0) throw new ArgumentOutOfRangeException(nameof(argIndex));
        ilGen.Emit(Ldarga, argIndex);
        return ilGen;
    }

    /// <summary>
    /// Inserta la carga de un parámetro en la secuencia del lenguaje
    /// intermedio de Microsoft® (MSIL).
    /// </summary>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar la operación.
    /// </param>
    /// <param name="parameter">Parámetro a cargar.</param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
    /// </returns>
    public static ILGenerator LoadParameter(this ILGenerator ilGen, ParameterInfo parameter)
    {
        ilGen.Emit(Ldarg, parameter.Position + 1);
        return ilGen;
    }

    /// <summary>
    /// Inserta una operación de duplicado del valor en la parte superior
    /// de la pila en la secuencia del lenguaje intermedio de Microsoft®
    /// (MSIL).
    /// </summary>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar la operación.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
    /// </returns>
    public static ILGenerator Duplicate(this ILGenerator ilGen) => OneLiner(ilGen, Dup);

    /// <summary>
    /// Inserta una operación de remoción del valor en la parte superior
    /// de la pila en la secuencia del lenguaje intermedio de Microsoft®
    /// (MSIL).
    /// </summary>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar la operación.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
    /// </returns>
    public static ILGenerator Pop(this ILGenerator ilGen) => OneLiner(ilGen, Op.Pop);

    /// <summary>
    /// Inserta una operación de carga de la constante
    /// <see langword="null"/> en la secuencia del lenguaje intermedio de
    /// Microsoft® (MSIL).
    /// </summary>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar la operación.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
    /// </returns>
    public static ILGenerator LoadNull(this ILGenerator ilGen) => OneLiner(ilGen, Ldnull);

    /// <summary>
    /// Inserta una operación de comparación de igualdad entre dos valores en
    /// la parte superior de la pila en la secuencia del lenguaje
    /// intermedio de Microsoft® (MSIL).
    /// </summary>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar la operación.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
    /// </returns>
    public static ILGenerator CompareEqual(this ILGenerator ilGen) => OneLiner(ilGen, Ceq);

    /// <summary>
    /// Inserta una operación de comparación entre dos valores en la parte
    /// superior de la pila en la secuencia del lenguaje intermedio de
    /// Microsoft® (MSIL).
    /// </summary>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar la operación.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
    /// </returns>
    public static ILGenerator CompareGreaterThan(this ILGenerator ilGen) => OneLiner(ilGen, Cgt);

    /// <summary>
    /// Inserta una operación de comparación entre dos valores en la parte
    /// superior de la pila en la secuencia del lenguaje intermedio de
    /// Microsoft® (MSIL).
    /// </summary>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar la operación.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
    /// </returns>
    public static ILGenerator CompareLessThan(this ILGenerator ilGen) => OneLiner(ilGen, Clt);

    /// <summary>
    /// Inserta el retorno del método actual en la secuencia del lenguaje
    /// intermedio de Microsoft® (MSIL).
    /// </summary>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar la operación.
    /// </param>
    public static void Return(this ILGenerator ilGen) => ilGen.Emit(Ret);

    /// <summary>
    /// Inserta el retorno del método actual en la secuencia del lenguaje
    /// intermedio de Microsoft® (MSIL).
    /// </summary>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar la operación.
    /// </param>
    /// <param name="exitLabel">
    /// Etiqueta opcional creada anteriormente que recibe el control desde
    /// un salto para salir del método en ejecución.
    /// </param>
    public static void Return(this ILGenerator ilGen, Label exitLabel)
    {
        ilGen.MarkLabel(exitLabel);
        ilGen.Emit(Ret);
    }

    /// <summary>
    /// Inserta la instanciación de un objeto en la secuencia del
    /// lenguaje intermedio de Microsoft® (MSIL).
    /// </summary>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar la instanciación
    /// del objeto.
    /// </param>
    /// <param name="newObjectType">
    /// Tipo de objeto a instanciar.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
    /// </returns>
    public static ILGenerator NewObj(this ILGenerator ilGen, Type newObjectType)
    {
        ilGen.Emit(Newobj, newObjectType);
        return ilGen;
    }

    /// <summary>
    /// Inserta la instanciación de un objeto en la secuencia del
    /// lenguaje intermedio de Microsoft® (MSIL).
    /// </summary>
    /// <typeparam name="T">
    /// Tipo de objeto a instanciar.
    /// </typeparam>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar la instanciación
    /// del objeto.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
    /// </returns>
    public static ILGenerator NewObj<T>(this ILGenerator ilGen)
    {
        return NewObj(ilGen, typeof(T));
    }

    private static ILGenerator LoadField(ILGenerator ilGen, FieldInfo field, OpCode opCode)
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
        block(endTry);
        return endTry;
    }

    private static void InsertCatchBlocks(ILGenerator ilGen, TryBlock tryBlock, IEnumerable<KeyValuePair<Type, TryBlock>> catchBlocks)
    {
        Label endTry = InsertTryBlock(ilGen, tryBlock);
        foreach (KeyValuePair<Type, TryBlock> j in catchBlocks)
        {
            if (!j.Key.Implements<Exception>()) throw new InvalidTypeException(j.Key);
            ilGen.BeginCatchBlock(j.Key);
            j.Value(endTry);
        }
    }
}
