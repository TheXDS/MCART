/*
NamedObjectExtensions.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Este archivo contiene numerosas extensiones para el tipo System.Type del CLR,
supliéndolo de nueva funcionalidad previamente no existente, o de invocación
compleja.

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
using System.Collections.Generic;
using System.Linq;
using TheXDS.MCART.Attributes;

namespace TheXDS.MCART.Types.Extensions;

/// <summary>
/// Funciones misceláneas y extensiones para todos los elementos de
/// tipo <see cref="NamedObject{T}"/>.
/// </summary>
public static partial class NamedObjectExtensions
{
    /// <summary>
    /// Enumera todos los valores de enumeración del tipo especificado
    /// como <see cref="NamedObject{T}"/>.
    /// </summary>
    /// <typeparam name="T">Tipo de enumeración a convertir.</typeparam>
    /// <returns>
    /// Una enumeración de <see cref="NamedObject{T}"/> a partir de los
    /// valores de enumeración del tipo especificado.
    /// </returns>
    [Sugar]
    public static IEnumerable<NamedObject<T>> AsNamedObject<T>() where T : Enum
    {
        return NamedObject<T>.FromEnum();
    }

    /// <summary>
    /// Enumera todos los valores de enumeración del tipo especificado
    /// como <see cref="NamedObject{T}"/>.
    /// </summary>
    /// <param name="t">Tipo de enumeración a convertir.</param>
    /// <returns>
    /// Una enumeración de <see cref="NamedObject{T}"/> a partir de los
    /// valores de enumeración del tipo especificado.
    /// </returns>
    public static IEnumerable<NamedObject<Enum>> AsNamedEnum(this Type t)
    {
        Type? q = AsNamedEnum_Contract(t);
        return q.GetEnumValues().OfType<Enum>().Select(p => new NamedObject<Enum>(p, p.NameOf()));
    }
}
