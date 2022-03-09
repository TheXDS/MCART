/*
Series.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Este archivo contiene funciones de enumeración de series matemáticas.

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2021 César Andrés Morgan

Morgan's CLR Advanced Runtime (MCART) is free software: you can redistribute it
and/or modify it under the terms of the GNU General Public License as published
by the Free Software Foundation, either version 3 of the License, or (at your
option) any later version.

Morgan's CLR Advanced Runtime (MCART) is distributed in the hope that it will
be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General
Public License for more details.

You should have received a copy of the GNU General Public License along with
this program. If not, see <http://www.gnu.org/licenses/>.
*/

namespace TheXDS.MCART.Math;
using System.Collections.Generic;

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
        while (true)
        {
            yield return a;
            yield return b;
            a += b;
            b += a;
        }
    }
}
