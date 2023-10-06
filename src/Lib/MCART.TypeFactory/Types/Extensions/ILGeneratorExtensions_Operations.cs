/*
ILGeneratorExtensions_Operations.cs

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
    /// Inserta una operación que obtiene la cantidad de elementos contenidos
    /// dentro de un arreglo en la secuencia del lenguaje intermedio de
    /// Microsoft® (MSIL), equivalente a <see cref="Array.Length"/>.
    /// </summary>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar la operación.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
    /// </returns>
    /// <remarks>
    /// La pila debe contener una referencia a un arreglo en la parte superior
    /// al efectuar esta llamada. Se colocará un valor <see cref="int"/> en la
    /// parte superior de la pila
    /// </remarks>
    public static ILGenerator GetArrayLength(this ILGenerator ilGen)
    {
        ilGen.OneLiner(Ldlen).OneLiner(Conv_I4);
        return ilGen;
    }

    /// <summary>
    /// Inserta una operación que almacena un valor <see cref="int"/> dentro de
    /// un arreglo en el índice especificado en la secuencia del lenguaje
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
    /// La operación requiere que la pila contenga el valor <see cref="int"/> a
    /// insertar en la parte superior, así como también el índice de tipo
    /// <see cref="int"/> en el cual se insertará dicho valor justo debajo.
    /// <br/><br/>
    /// Uso de pila neto: -2
    /// </remarks>
    public static ILGenerator StoreInt32Element(this ILGenerator ilGen) => ilGen.OneLiner(Stelem_I4);

    /// <summary>
    /// Inserta una operación que almacena un valor <see cref="byte"/> dentro de
    /// un arreglo en el índice especificado en la secuencia del lenguaje
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
    /// La operación requiere que la pila contenga el valor <see cref="byte"/> a
    /// insertar en la parte superior, así como también el índice de tipo
    /// <see cref="int"/> en el cual se insertará dicho valor justo debajo.
    /// <br/><br/>
    /// Uso de pila neto: -2
    /// </remarks>
    public static ILGenerator StoreInt8Element(this ILGenerator ilGen) => ilGen.OneLiner(Stelem_I1);

    /// <summary>
    /// Inserta una operación que almacena un valor <see cref="short"/> dentro de
    /// un arreglo en el índice especificado en la secuencia del lenguaje
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
    /// La operación requiere que la pila contenga el valor <see cref="short"/> a
    /// insertar en la parte superior, así como también el índice de tipo
    /// <see cref="int"/> en el cual se insertará dicho valor justo debajo.
    /// <br/><br/>
    /// Uso de pila neto: -2
    /// </remarks>
    public static ILGenerator StoreInt16Element(this ILGenerator ilGen) => ilGen.OneLiner(Stelem_I2);

    /// <summary>
    /// Inserta una operación que almacena un valor <see cref="long"/> dentro de
    /// un arreglo en el índice especificado en la secuencia del lenguaje
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
    /// La operación requiere que la pila contenga el valor <see cref="long"/> a
    /// insertar en la parte superior, así como también el índice de tipo
    /// <see cref="int"/> en el cual se insertará dicho valor justo debajo.
    /// <br/><br/>
    /// Uso de pila neto: -2
    /// </remarks>
    public static ILGenerator StoreInt64Element(this ILGenerator ilGen) => ilGen.OneLiner(Stelem_I8);

    /// <summary>
    /// Inserta una operación que almacena un valor <see cref="float"/> dentro de
    /// un arreglo en el índice especificado en la secuencia del lenguaje
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
    /// La operación requiere que la pila contenga el valor <see cref="float"/> a
    /// insertar en la parte superior, así como también el índice de tipo
    /// <see cref="int"/> en el cual se insertará dicho valor justo debajo.
    /// <br/><br/>
    /// Uso de pila neto: -2
    /// </remarks>
    public static ILGenerator StoreFloatElement(this ILGenerator ilGen) => ilGen.OneLiner(Stelem_R4);

    /// <summary>
    /// Inserta una operación que almacena un valor <see cref="double"/> dentro de
    /// un arreglo en el índice especificado en la secuencia del lenguaje
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
    /// La operación requiere que la pila contenga el valor <see cref="double"/> a
    /// insertar en la parte superior, así como también el índice de tipo
    /// <see cref="int"/> en el cual se insertará dicho valor justo debajo.
    /// <br/><br/>
    /// Uso de pila neto: -2
    /// </remarks>
    public static ILGenerator StoreDoubleElement(this ILGenerator ilGen) => ilGen.OneLiner(Stelem_R8);

    /// <summary>
    /// Inserta una operación que almacena la referencia a un objeto dentro de
    /// un arreglo en el índice especificado en la secuencia del lenguaje
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
    /// La operación requiere que la pila contenga la referencia al objeto a
    /// insertar en la parte superior, así como también el índice de tipo
    /// <see cref="int"/> en el cual se insertará dicha referencia justo
    /// debajo.
    /// <br/><br/>
    /// Uso de pila neto: -2
    /// </remarks>
    public static ILGenerator StoreRefElement(this ILGenerator ilGen) => ilGen.OneLiner(Stelem_Ref);

    /// <summary>
    /// Inserta el retorno del método actual en la secuencia del lenguaje
    /// intermedio de Microsoft® (MSIL).
    /// </summary>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar la operación.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
    /// </returns>
    public static ILGenerator Return(this ILGenerator ilGen) => ilGen.OneLiner(Ret);

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
}
