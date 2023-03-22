/*
TypeComparer_T.cs

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

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace TheXDS.MCART.Comparison;

/// <summary>
/// Compara el tipo de dos objetos que comparten un tipo base común.
/// </summary>
public class TypeComparer<T> : IEqualityComparer<T>
    where T : notnull
{
    /// <summary>
    /// Determina si los objetos especificados son iguales.
    /// </summary>
    /// <param name="x">Primer objeto a comparar.</param>
    /// <param name="y">Segundo objeto a comparar.</param>
    /// <returns>
    /// <see langword="true"/> si el tipo de ambos objetos es el mismo,
    /// <see langword="false"/> en caso contrario.
    /// </returns>
    public bool Equals([AllowNull] T x, [AllowNull] T y)
    {
        return x?.GetType() == y?.GetType();
    }

    /// <summary>
    /// Obtiene el código Hash para el tipo del objeto especificado.
    /// </summary>
    /// <param name="obj">
    /// Objeto para el cual obtener un código Hash.
    /// </param>
    /// <returns>
    /// El código hash para el tipo del objeto especificado.
    /// </returns>
    public int GetHashCode([DisallowNull] T obj)
    {
        return obj!.GetType().GetHashCode();
    }
}
