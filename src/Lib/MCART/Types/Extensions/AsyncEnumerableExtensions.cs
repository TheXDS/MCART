/*
EnumerableExtensions.cs

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

namespace TheXDS.MCART.Types.Extensions;

/// <summary>
/// Extensiones para todos los elementos de tipo
/// <see cref="IAsyncEnumerable{T}" />.
/// </summary>
public static partial class AsyncEnumerableExtensions
{
    /// <summary>
    /// Obtiene la cuenta de elementos de un
    /// <see cref="IAsyncEnumerable{T}"/>, enumerándolo de forma asíncrona.
    /// </summary>
    /// <param name="e">
    /// <see cref="IAsyncEnumerable{T}"/> para el cual obtener la cuenta de
    /// elementos.
    /// </param>
    /// <param name="ct">Token que permite cancelar la operación.</param>
    /// <typeparam name="T">Tipo de elementos de la colección.</typeparam>
    /// <returns>
    /// La cantidad de elementos contenidos en la colección.
    /// </returns>
    /// <exception cref="TaskCanceledException">
    /// Ocurre cuando la tarea es cancelada.
    /// </exception>
    public static async ValueTask<int> CountAsync<T>(this IAsyncEnumerable<T> e, CancellationToken ct)
    {
        IAsyncEnumerator<T> n = e.GetAsyncEnumerator(ct);
        if (ct.IsCancellationRequested) throw new TaskCanceledException();
        int c = 0;
        while (await n.MoveNextAsync())
        {
            c++;
            if (ct.IsCancellationRequested) throw new TaskCanceledException();
        }
        return c;
    }

    /// <summary>
    /// Obtiene la cuenta de elementos de un
    /// <see cref="IAsyncEnumerable{T}"/>, enumerándolo de forma asíncrona.
    /// </summary>
    /// <param name="e">
    /// <see cref="IAsyncEnumerable{T}"/> para el cual obtener la cuenta de
    /// elementos.
    /// </param>
    /// <typeparam name="T">Tipo de elementos de la colección.</typeparam>
    /// <returns>
    /// La cantidad de elementos contenidos en la colección.
    /// </returns>
    /// <exception cref="TaskCanceledException">
    /// Ocurre cuando la tarea es cancelada.
    /// </exception>
    public static ValueTask<int> CountAsync<T>(this IAsyncEnumerable<T> e)
    {
        return CountAsync(e, CancellationToken.None);
    }
}