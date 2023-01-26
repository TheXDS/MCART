/*
IConstantLoader.cs

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
/// Define una serie de miembros a implementar por un tipo que permita
/// cargar valores constantes en una secuencia de instrucciones MSIL.
/// </summary>
public interface IConstantLoader
{
    /// <summary>
    /// Carga un valor constante en la secuencia de instrucciones MSIL.
    /// </summary>
    /// <param name="il">Generador de IL a utilizar.</param>
    /// <param name="value">
    /// Valor constante a cargar en la secuencia de instrucciones.
    /// </param>
    void Emit(ILGenerator il, object? value);

    /// <summary>
    /// Obtiene el tipo de constante que este 
    /// <see cref="IConstantLoader"/> es capaz de cargar en la
    /// secuencia de instrucciones MSIL.
    /// </summary>
    Type ConstantType { get; }
}
