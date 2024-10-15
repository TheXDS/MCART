/*
ValueEventArgs_T.cs

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

namespace TheXDS.MCART.Events;

/// <summary>
/// Incluye información de evento para cualquier clase con eventos que
/// incluyan tipos de valor.
/// </summary>
/// <typeparam name="T">
/// Tipo del valor almacenado por esta instancia.
/// </typeparam>
/// <param name="value">Valor asociado al evento generado.</param>
public class ValueEventArgs<T>(T value) : EventArgs
{
    /// <summary>
    /// Crea implícitamente un <see cref="ValueEventArgs{T}"/> a partir
    /// de un valor <typeparamref name="T"/>.
    /// </summary>
    /// <param name="value">
    /// Valor a partir del cual crear el nuevo
    /// <see cref="ValueEventArgs{T}"/>.
    /// </param>
    public static implicit operator ValueEventArgs<T>(T value) => new(value);

    /// <summary>
    /// Devuelve el valor asociado a este evento.
    /// </summary>
    /// <returns>
    /// Un valor de tipo <typeparamref name="T" /> con el valor asociado a
    /// este evento.
    /// </returns>
    public T Value { get; } = value;
}
