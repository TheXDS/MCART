/*
ConstantLoader.cs

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
using System.Reflection.Emit;

namespace TheXDS.MCART.Types.Extensions.ConstantLoaders;

/// <summary>
/// Clase abstracta que define un objeto que permite cargar un valor 
/// constante en la secuencia de instrucciones MSIL.
/// </summary>
/// <typeparam name="T">Tipo de constante a cargar.</typeparam>
public abstract class ConstantLoader<T> : IConstantLoader, IEquatable<IConstantLoader>
{
    /// <summary>
    /// Obtiene una referencia al tipo de constante que esta instancia
    /// puede cargar en la secuencia de instrucciones MSIL.
    /// </summary>
    public Type ConstantType => typeof(T);

    /// <summary>
    /// Carga un valor constante en la secuencia de instrucciones MSIL.
    /// </summary>
    /// <param name="il">
    /// Generador de código a utilizar.
    /// </param>
    /// <param name="value">
    /// Valor constante a cargar. Debe ser de tipo 
    /// <typeparamref name="T"/>.
    /// </param>
    public void Emit(ILGenerator il, object? value)
    {
        Emit(il, (T)value!);
    }

    /// <summary>
    /// Carga un valor constante en la secuencia de instrucciones MSIL.
    /// </summary>
    /// <param name="il">
    /// Generador de código a utilizar.
    /// </param>
    /// <param name="value">
    /// Valor constante a cargar.
    /// </param>
    public abstract void Emit(ILGenerator il, T value);

    /// <summary>
    /// Comprueba la igualdad entre esta instancia y otro 
    /// <see cref="IConstantLoader"/> basado en el tipo de constante
    /// que ambos son capaces de cargar.
    /// </summary>
    /// <param name="other">
    /// Una instancia de <see cref="IConstantLoader"/> contra la cual
    /// comparar.
    /// </param>
    /// <returns>
    /// <see langword="true"/> si ambos <see cref="IConstantLoader"/>
    /// permiten cargar el mismo tipo de valores,
    /// <see langword="false"/> en caso contrario.
    /// </returns>
    public bool Equals(IConstantLoader? other)
    {
        return other is { ConstantType: { } o } && ConstantType.Equals(o);
    }
}
