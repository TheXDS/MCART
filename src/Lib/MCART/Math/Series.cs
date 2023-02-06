/*
Series.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Este archivo contiene funciones de enumeración de series matemáticas.

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

namespace TheXDS.MCART.Math;

/// <summary>
/// Esta clase contiene funciones de enumeración de series matemáticas.
/// </summary>
public static class Series
{
    /* -= NOTA =-
     * Las series utilizan enumeradores para exponer las series 
     * completas de una manera infinita.
     *  
     * Es necesario recalcar que, si se utilizan estas funciones de
     * manera incorrecta, el programa fallará con un error de
     * sobreflujo o de pila, o bien, el programa podría dejar de
     * responder.
     *
     * Todas las funciones de enumeración infinita deben utilizarse junto
     * con el método de extensión
     * System.Linq.Enumerable.Take<TSource>(IEnumerable<TSource>, int)
     */

    /// <summary>
    /// Expone un enumerador que contiene la secuencia completa de
    /// Fibonacci.
    /// </summary>
    /// <returns>
    /// Un <see cref="IEnumerable{T}" /> con la secuencia infinita de
    /// Fibonacci.
    /// </returns>
    /// <remarks>
    /// Las series utilizan enumeradores para exponer las series
    /// completas de una manera infinita. Es necesario recalcar que, si
    /// se utilizan estas funciones de manera incorrecta, el programa
    /// fallará con un error de sobreflujo o de pila, o bien, el
    /// programa podría dejar de responder durante un período de tiempo
    /// prolongado.
    /// </remarks>
    /// <seealso cref="System.Linq.Enumerable.Take{TSource}(IEnumerable{TSource}, int)"/>
    public static IEnumerable<long> Fibonacci() => MakeSeriesAdditive(0, 1);

    /// <summary>
    /// Expone un enumerador que contiene la secuencia completa de
    /// Lucas.
    /// </summary>
    /// <returns>
    /// Un <see cref="IEnumerable{T}" /> con la secuencia infinita de
    /// Lucas.
    /// </returns>
    /// <remarks>
    /// Las series utilizan enumeradores para exponer las series
    /// completas de una manera infinita. Es necesario recalcar que, si
    /// se utilizan estas funciones de manera incorrecta, el programa
    /// fallará con un error de sobreflujo o de pila, o bien, el
    /// programa podría dejar de responder durante un período de tiempo
    /// prolongado.
    /// </remarks>
    /// <seealso cref="System.Linq.Enumerable.Take{TSource}(IEnumerable{TSource}, int)"/>
    public static IEnumerable<long> Lucas() => MakeSeriesAdditive(2, 1);

    /// <summary>
    /// Crea un enumerador que genera una serie numérica aditiva
    /// proporcionando los dos elementos iniciales de la serie.
    /// </summary>
    /// <param name="a">Primer elemento inicial.</param>
    /// <param name="b">Segundo elemento inicial.</param>
    /// <returns>
    /// Un enumerador que generará una serie numérica aditiva para cada
    /// iteración.
    /// </returns>
    public static IEnumerable<long> MakeSeriesAdditive(long a, long b)
    {
        while (a >= 0)
        {
            yield return a;
            yield return b;
            a += b;
            b += a;
        }
    }
}
