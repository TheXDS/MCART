/*
ILGeneratorExtensions_ObjInstancing.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2024 César Andrés Morgan

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
using TheXDS.MCART.Exceptions;
using TheXDS.MCART.Helpers;
using TheXDS.MCART.Types.Extensions.ConstantLoaders;
using static System.Reflection.Emit.OpCodes;
using static TheXDS.MCART.Resources.Errors;

namespace TheXDS.MCART.Types.Extensions;

/// <summary>
/// Contiene extensiones útiles para la generación de código por medio
/// de la clase <see cref="ILGenerator"/>.
/// </summary>
public static partial class ILGeneratorExtensions
{
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
        Type t = typeof(T);
        if (_constantLoaders.FirstOrDefault(p => p.ConstantType == t) is { } cl)
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
    /// Inserta una serie de instrucciones que aumentarán el valor del valor
    /// actualmente en la pila en 1 en la secuencia del lenguaje intermedio de
    /// Microsoft® (MSIL).
    /// </summary>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar la serie de operaciones.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
    /// </returns>
    public static ILGenerator Increment(this ILGenerator ilGen)
    {
        return ilGen.LoadConstant(1).Add();
    }

    /// <summary>
    /// Inserta una serie de instrucciones que aumentarán el valor de una
    /// variable local en 1 en la secuencia del lenguaje intermedio de
    /// Microsoft® (MSIL).
    /// </summary>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar la serie de operaciones.
    /// </param>
    /// <param name="local">
    /// Referencia a la variable local sobre la cual ejecutar la operación de 
    /// incremento.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
    /// </returns>
    public static ILGenerator Increment(this ILGenerator ilGen, LocalBuilder local)
    {
        return ilGen.LoadLocal(local).Increment().StoreLocal(local);
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
    /// <exception cref="NotImplementedException">
    /// Se produce al intentar cargar un valor constante desconocido.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Se produce al intentar cargar un valor que no es constante, como
    /// una instancia de objeto.
    /// </exception>
    public static ILGenerator LoadConstant(this ILGenerator ilGen, Type t, object? value)
    {
        if (_constantLoaders.FirstOrDefault(p => p.ConstantType == t) is { } cl)
        {
            cl.Emit(ilGen, value);
        }
        else if (t.IsStruct())
        {
            ilGen.Emit(Newobj, t.GetConstructor(Type.EmptyTypes) ?? throw ClassNotInstantiable(t));
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
        ArgumentNullException.ThrowIfNull(value);
        return LoadConstant(ilGen, value.GetType(), value);
    }

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
    /// Inserta una operación de inicialización de un arreglo unidimensional
    /// del tipo especificado en la secuencia del lenguaje intermedio de
    /// Microsoft® (MSIL).
    /// </summary>
    /// <typeparam name="T">Tipo del nuevo arreglo.</typeparam>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar la operación.
    /// </param>
    /// <param name="local">
    /// Parámetro que contiene la variable local generada para almacenar el
    /// nuevo arreglo.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
    /// </returns>
    /// <remarks>
    /// La operación requiere que la pila contenga la cantidad de elementos que
    /// el arreglo debe contener expresado como un valor <see cref="int"/>.
    /// <br/><br/>
    /// Uso de pila neto: -1
    /// </remarks>
    public static ILGenerator NewArray<T>(this ILGenerator ilGen, out LocalBuilder local) => NewArray(ilGen, typeof(T), out local);

    /// <summary>
    /// Inserta una operación de inicialización de un arreglo unidimensional
    /// del tipo especificado en la secuencia del lenguaje intermedio de
    /// Microsoft® (MSIL).
    /// </summary>
    /// <typeparam name="T">Tipo del nuevo arreglo.</typeparam>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar la operación.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
    /// </returns>
    /// <remarks>
    /// La operación requiere que la pila contenga la cantidad de elementos que
    /// el arreglo debe contener expresado como un valor <see cref="int"/>.
    /// <br/><br/>
    /// Uso de pila neto: 0
    /// </remarks>
    public static ILGenerator NewArray<T>(this ILGenerator ilGen) => NewArray(ilGen, typeof(T));

    /// <summary>
    /// Inserta una operación de inicialización de un arreglo unidimensional
    /// del tipo especificado en la secuencia del lenguaje intermedio de
    /// Microsoft® (MSIL).
    /// </summary>
    /// <param name="arrayType">Tipo del nuevo arreglo.</param>
    /// <param name="local">
    /// Parámetro que contiene la variable local generada para almacenar el nuevo arreglo.
    /// </param>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar la operación.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
    /// </returns>
    public static ILGenerator NewArray(this ILGenerator ilGen, Type arrayType, out LocalBuilder local)
    {
        return ilGen.NewArray(arrayType).StoreNewLocal(arrayType.MakeArrayType(), out local);
    }

    /// <summary>
    /// Inserta una operación de inicialización de un arreglo unidimensional
    /// del tipo especificado en la secuencia del lenguaje intermedio de
    /// Microsoft® (MSIL).
    /// </summary>
    /// <param name="arrayType">Tipo del nuevo arreglo.</param>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar la operación.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
    /// </returns>
    /// <remarks>
    /// La operación requiere que la pila contenga la cantidad de elementos que
    /// el arreglo debe contener expresado como un valor <see cref="int"/>.
    /// <br/><br/>
    /// Uso de pila neto: 0
    /// </remarks>
    public static ILGenerator NewArray(this ILGenerator ilGen, Type arrayType)
    {
        ilGen.Emit(Newarr, arrayType);
        return ilGen;
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
    public static ILGenerator NewObject<T>(this ILGenerator ilGen, object?[] args)
    {
        return NewObject(ilGen, typeof(T), args);
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
    /// Tipo de argumentos aceptados por el constructor del objeto a utilizar.
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
    public static ILGenerator NewObject<T>(this ILGenerator ilGen, Type[] args)
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
        return NewObject(ilGen, type, args.Cast<object?>().ToArray());
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
    public static ILGenerator NewObject(this ILGenerator ilGen, Type type, object?[] args)
    {
        if (type.GetConstructor(args.ToTypes().ToArray()) is not { } c) throw new ClassNotInstantiableException(type);
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
    /// Tipo de argumentos aceptados por el constructor del objeto a utilizar.
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
    public static ILGenerator NewObject(this ILGenerator ilGen, Type type, Type[] args)
    {
        if (type.GetConstructor(args.ToTypes().ToArray()) is not { } c) throw new ClassNotInstantiableException(type);
        ilGen.Emit(Newobj, c);
        return ilGen;
    }
}
