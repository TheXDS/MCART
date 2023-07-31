/*
ILGeneratorExtensions_Branching.cs

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

using System.Reflection.Emit;
using static System.Reflection.Emit.OpCodes;

namespace TheXDS.MCART.Types.Extensions;

/// <summary>
/// Contiene extensiones útiles para la generación de código por medio
/// de la clase <see cref="ILGenerator"/>.
/// </summary>
public static partial class ILGeneratorExtensions
{
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
}
