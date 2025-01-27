﻿/*
TypeConstantLoader.cs

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
using static System.Reflection.Emit.OpCodes;

namespace TheXDS.MCART.Types.Extensions.ConstantLoaders;

/// <summary>
/// Carga un valor constante <see cref="Type"/> en la secuencia de
/// instrucciones MSIL.
/// </summary>
public class TypeConstantLoader : ConstantLoader<Type>
{
    /// <summary>
    /// Carga un valor constante <see cref="Type"/> en la secuencia de
    /// instrucciones MSIL.
    /// </summary>
    /// <param name="il">Generador de IL a utilizar.</param>
    /// <param name="value">
    /// Valor constante a cargar en la secuencia de instrucciones.
    /// </param>
    public override void Emit(ILGenerator il, Type value)
    {
        il.Emit(Ldtoken, value);
        il.Emit(Call, typeof(Type).GetMethod(nameof(Type.GetTypeFromHandle))!);
    }
}
