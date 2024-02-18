/*
ILGeneratorExtensions_MemberLoading.cs

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

using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Helpers;
using static System.Reflection.Emit.OpCodes;

namespace TheXDS.MCART.Types.Extensions;

/// <summary>
/// Contiene extensiones útiles para la generación de código por medio
/// de la clase <see cref="ILGenerator"/>.
/// </summary>
public static partial class ILGeneratorExtensions
{
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
    /// Inserta el almacenamiento de un valor a una nueva variable local en
    /// la secuencia del lenguaje intermedio de Microsoft® (MSIL).
    /// </summary>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar el almacenamiento de
    /// un valor.
    /// </param>
    /// <param name="localType">
    /// Tipo de la variable local a declarar.
    /// </param>
    /// <param name="local">
    /// Variable local en la cual almacenar el valor.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
    /// </returns>
    public static ILGenerator StoreNewLocal(this ILGenerator ilGen, Type localType, out LocalBuilder local)
    {
        local = ilGen.DeclareLocal(localType);
        ilGen.Emit(Stloc, local);
        return ilGen;
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
    /// <remarks>
    /// Este método determinará automáticamente si es necesario agregar una
    /// referencia a <see langword="this"/> (<see langword="Me"/> en Visual
    /// Basic) en caso que el campo sea estático, por lo que no debe insertar
    /// la llamada a cargar la instancia (<see cref="LoadArg0(ILGenerator)"/>).
    /// <br/><br/>
    /// Uso neto de pila: -1
    /// </remarks>
    public static ILGenerator LoadField(this ILGenerator ilGen, FieldInfo field)
    {
        ilGen.Emit(Ldfld, field);
        return ilGen;
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
    /// <remarks>
    /// Este método determinará automáticamente si es necesario agregar una
    /// referencia a <see langword="this"/> (<see langword="Me"/> en Visual
    /// Basic) en caso que el campo sea estático, por lo que no debe insertar
    /// la llamada a cargar la instancia (<see cref="LoadArg0(ILGenerator)"/>).
    /// <br/><br/>
    /// Uso neto de pila: -1
    /// </remarks>
    public static ILGenerator GetField(this ILGenerator ilGen, FieldInfo field)
    {
        return GetField(ilGen, field, Ldfld);
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
    /// <remarks>
    /// Este método requiere que la pila contenga el valor a almacenar en la
    /// parte superior, seguido inmediatamente de una referencia a la instancia
    /// en caso que el campo sea de instancia. Si el campo es estático, no se
    /// requiere una referencia a <see langword="this"/> (<see langword="Me"/>
    /// en Visual Basic).
    /// <br/><br/>
    /// Uso neto de pila: -1
    /// </remarks>
    /// <seealso cref="LoadArg0(ILGenerator)"/>
    /// <seealso cref="StoreField(ILGenerator, FieldInfo, Action{ILGenerator})"/>
    public static ILGenerator StoreField(this ILGenerator ilGen, FieldInfo field)
    {
        ilGen.Emit(Stfld, field);
        return ilGen;
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
    /// <param name="value">
    /// Llamada que cargará el valor a almacenar en el campo.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
    /// </returns>
    /// <remarks>
    /// Este método no determinará automáticamente si es necesario agregar una
    /// referencia a <see langword="this"/> (<see langword="Me"/> en Visual
    /// Basic) en caso que el campo sea estático, por lo que debe insertar
    /// la llamada a cargar la instancia (<see cref="LoadArg0(ILGenerator)"/>).
    /// <br/><br/>
    /// Uso neto de pila: -1
    /// </remarks>
    /// <seealso cref="SetField(ILGenerator, FieldInfo, Action{ILGenerator})"/>
    public static ILGenerator StoreField(this ILGenerator ilGen, FieldInfo field, Action<ILGenerator> value)
    {
        value.Invoke(ilGen);
        return StoreField(ilGen, field);
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
    /// <param name="value">
    /// Llamada que cargará el valor a almacenar en el campo.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
    /// </returns>
    /// <remarks>
    /// Este método no determinará automáticamente si es necesario agregar una
    /// referencia a <see langword="this"/> (<see langword="Me"/> en Visual
    /// Basic) en caso que el campo sea estático, por lo que debe insertar
    /// la llamada a cargar la instancia (<see cref="LoadArg0(ILGenerator)"/>).
    /// <br/><br/>
    /// Uso neto de pila: -1
    /// </remarks>
    /// <seealso cref="SetField(ILGenerator, FieldInfo, Func{ILGenerator, ILGenerator})"/>
    [Sugar]
    public static ILGenerator StoreField(this ILGenerator ilGen, FieldInfo field, Func<ILGenerator, ILGenerator> value)
    {
        return StoreField(ilGen, field, (Action<ILGenerator>)(il => _ = value(il)));
    }

    /// <summary>
    /// Inserta el almacenamiento de un valor a un campo en la secuencia
    /// del lenguaje intermedio de Microsoft® (MSIL), determinando 
    /// automáticamente si es necesario insertar una referencia a
    /// <see langword="this"/>.
    /// </summary>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar el almacenamiento de
    /// un valor.
    /// </param>
    /// <param name="field">
    /// Campo en el cual almacenar el valor.
    /// </param>
    /// <param name="value">
    /// Llamada que cargará el valor a almacenar en el campo.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
    /// </returns>
    /// <remarks>
    /// Este método determinará automáticamente si es necesario agregar una
    /// referencia a <see langword="this"/> (<see langword="Me"/> en Visual
    /// Basic) en caso que el campo sea estático, por lo que no debe insertar
    /// la llamada a cargar la instancia (<see cref="LoadArg0(ILGenerator)"/>).
    /// <br/><br/>
    /// Uso neto de pila: -1
    /// </remarks>
    /// <seealso cref="StoreField(ILGenerator, FieldInfo, Func{ILGenerator, ILGenerator})"/>
    public static ILGenerator SetField(this ILGenerator ilGen, FieldInfo field, Func<ILGenerator, ILGenerator> value)
    {
        if (!field.IsStatic) ilGen.LoadArg0();
        return StoreField(ilGen, field, value);
    }

    /// <summary>
    /// Inserta el almacenamiento de un valor a un campo en la secuencia
    /// del lenguaje intermedio de Microsoft® (MSIL), determinando 
    /// automáticamente si es necesario insertar una referencia a
    /// <see langword="this"/>.
    /// </summary>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar el almacenamiento de
    /// un valor.
    /// </param>
    /// <param name="field">
    /// Campo en el cual almacenar el valor.
    /// </param>
    /// <param name="value">
    /// Llamada que cargará el valor a almacenar en el campo.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
    /// </returns>
    /// <remarks>
    /// Este método determinará automáticamente si es necesario agregar una
    /// referencia a <see langword="this"/> (<see langword="Me"/> en Visual
    /// Basic) en caso que el campo sea estático, por lo que no debe insertar
    /// la llamada a cargar la instancia (<see cref="LoadArg0(ILGenerator)"/>).
    /// <br/><br/>
    /// Uso neto de pila: -1
    /// </remarks>
    /// <seealso cref="StoreField(ILGenerator, FieldInfo, Action{ILGenerator})"/>
    public static ILGenerator SetField(this ILGenerator ilGen, FieldInfo field, Action<ILGenerator> value)
    {
        if (!field.IsStatic) ilGen.LoadArg0();
        return StoreField(ilGen, field, value);
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
    /// <param name="value">
    /// Llamada que cargará el valor a almacenar en la propiedad.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
    /// </returns>
    /// <remarks>
    /// Este método determinará automáticamente si es necesario agregar una
    /// referencia a <see langword="this"/> (<see langword="Me"/> en Visual
    /// Basic) en caso que la propiedad sea estática, por lo que no debe
    /// insertar la llamada a cargar la instancia por medio de
    /// (<see cref="LoadArg0(ILGenerator)"/>).
    /// <br/><br/>
    /// Uso neto de pila: -1
    /// </remarks>
    public static ILGenerator StoreProperty(this ILGenerator ilGen, PropertyInfo prop, Action<ILGenerator> value)
    {
        if (!prop.SetMethod?.IsStatic ?? throw new InvalidOperationException()) ilGen.LoadArg0();
        value.Invoke(ilGen);
        return StoreProperty(ilGen, prop);
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
    /// <remarks>
    /// Este método requiere que la pila contenga el valor a almacenar en la
    /// parte superior, seguido inmediatamente de una referencia a la instancia
    /// en caso que la propiedad sea de instancia. Si la propiedad es estática,
    /// no se requiere una referencia a <see langword="this"/>
    /// (<see langword="Me"/> en Visual Basic).
    /// <br/><br/>
    /// Uso neto de pila: -1
    /// </remarks>
    /// <seealso cref="LoadArg0(ILGenerator)"/>
    /// <seealso cref="StoreProperty(ILGenerator, PropertyInfo, Action{ILGenerator})"/>
    public static ILGenerator StoreProperty(this ILGenerator ilGen, PropertyInfo prop)
    {
        return ilGen.Call(prop.SetMethod!);
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
    /// <param name="value">
    /// Llamada que cargará el valor a almacenar en la propiedad.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
    /// </returns>
    /// <remarks>
    /// Este método determinará automáticamente si es necesario agregar una
    /// referencia a <see langword="this"/> (<see langword="Me"/> en Visual
    /// Basic) en caso que la propiedad sea estática, por lo que no debe
    /// insertar la llamada a cargar la instancia por medio de
    /// (<see cref="LoadArg0(ILGenerator)"/>).
    /// <br/><br/>
    /// Uso neto de pila: -1
    /// </remarks>
    [Sugar]
    public static ILGenerator StoreProperty(this ILGenerator ilGen, PropertyInfo prop, Func<ILGenerator, ILGenerator> value)
    {
        return StoreProperty(ilGen, prop, (Action<ILGenerator>)(il => _ = value(il)));
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
        return GetField(ilGen, field, Ldflda);
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
        MethodInfo m = property.GetGetMethod()!;
        if (!m.IsStatic) ilGen.LoadArg0();
        return ilGen.Call(m);
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
    /// Inserta la carga del argumento con índice 0 de un método en la
    /// secuencia del lenguaje intermedio de Microsoft® (MSIL).
    /// </summary>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar la operación.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
    /// </returns>
    /// <remarks>
    /// Para métodos de instancia, esta llamada cargará una referencia a la
    /// instancia actual, para métodos estáticos se cargará el primer
    /// parámetro.
    /// <br/><br/>
    /// Uso de pila neto: 1
    /// </remarks>
    public static ILGenerator LoadArg0(this ILGenerator ilGen) => OneLiner(ilGen, Ldarg_0);

    /// <summary>
    /// Inserta la carga del argumento con índice 1 de un método en la
    /// secuencia del lenguaje intermedio de Microsoft® (MSIL).
    /// </summary>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar la operación.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
    /// </returns>
    /// <remarks>
    /// Para métodos de instancia, esta llamada cargará al primer argumento del
    /// método, para métodos estáticos se cargará el segundo parámetro.
    /// <br/><br/>
    /// Uso de pila neto: 1
    /// </remarks>
    public static ILGenerator LoadArg1(this ILGenerator ilGen) => OneLiner(ilGen, Ldarg_1);

    /// <summary>
    /// Inserta la carga del argumento con índice 2 de un método en la
    /// secuencia del lenguaje intermedio de Microsoft® (MSIL).
    /// </summary>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar la operación.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
    /// </returns>
    /// <remarks>
    /// Para métodos de instancia, esta llamada cargará al segundo argumento
    /// del método, para métodos estáticos se cargará el tercer parámetro.
    /// <br/><br/>
    /// Uso de pila neto: 1
    /// </remarks>
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
    /// <remarks>
    /// Para métodos de instancia, esta llamada cargará al tercer argumento del
    /// método, para métodos estáticos se cargará el cuarto parámetro.
    /// <br/><br/>
    /// Uso de pila neto: 1
    /// </remarks>
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
    /// <see langword="this"/>) cuando se llama desde un método de instancia,
    /// causando que los parámetros del método empiecen desde el índice
    /// <c>1</c>.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
    /// </returns>
    /// <remarks>
    /// Uso de pila neto: 1
    /// </remarks>
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
        ilGen.Emit(Ldarg, parameter.Position);
        return ilGen;
    }
}
