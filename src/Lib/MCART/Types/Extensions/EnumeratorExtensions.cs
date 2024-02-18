/*
EnumeratorExtensions.cs

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

namespace TheXDS.MCART.Types.Extensions;

/// <summary>
/// Extensiones para todos los tipos que implementen 
/// <see cref="IEnumerator"/>.
/// </summary>
public static partial class EnumeratorExtensions
{
    /// <summary>
    /// Desplaza el enumerador una cantidad específica de elementos.
    /// </summary>
    /// <param name="enumerator">
    /// Enumerador a recorrer.
    /// </param>
    /// <param name="steps">
    /// Número de elementos a saltar.
    /// </param>
    /// <returns>
    /// La cantidad de elementos saltados exitosamente.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Se produce si <paramref name="steps"/> es inferior a cero.
    /// </exception>
    public static int Skip(this IEnumerator enumerator, int steps)
    {
        Skip_Contract(enumerator, steps);
        int j;
        for (j = 0; j < steps; j++)
        {
            if (!enumerator.MoveNext())
            {
                j++;
                break;
            }
        }
        return j;
    }
}
