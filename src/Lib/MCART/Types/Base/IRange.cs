/*
IRange.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Este archivo define la estructura Range<TValue>, la cual permite representar rangos
de valores.

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

using TheXDS.MCART.Helpers;

namespace TheXDS.MCART.Types.Base;

/// <summary>
/// Interfaz que define un rango de valores.
/// </summary>
/// <typeparam name="T">Tipo base del rango de valores.</typeparam>
public interface IRange<T> where T : IComparable<T>
{
    /// <summary>
    /// Valor máximo del rango.
    /// </summary>
    T Maximum { get; set; }

    /// <summary>
    /// Obtiene o establece un valor que determina si el valor máximo
    /// es parte del rango.
    /// </summary>
    /// 
    bool MaxInclusive { get; set; }

    /// <summary>
    /// Valor mínimo del rango.
    /// </summary>
    T Minimum { get; set; }

    /// <summary>
    /// Obtiene o establece un valor que determina si el valor mínimo
    /// es parte del rango.
    /// </summary>
    bool MinInclusive { get; set; }

    /// <summary>
    /// Determina si un <see cref="IRange{T}"/> intersecta a este.
    /// </summary>
    /// <param name="other">Rango a comprobar.</param>
    /// <returns>
    /// <see langword="true"/> si <paramref name="other"/> intersecta a
    /// este <see cref="IRange{T}"/>, <see langword="false"/> en caso
    /// contrario.
    /// </returns>
    bool Intersects(IRange<T> other) => IsWithin(other.Maximum) || IsWithin(other.Minimum);

    /// <summary>
    /// Comprueba si un valor <typeparamref name="T"/> se encuentra
    /// dentro de este <see cref="IRange{T}"/>.
    /// </summary>
    /// <param name="value">Valor a comprobar.</param>
    /// <returns>
    /// <see langword="true"/> si el valor se encuentra dentro de este
    /// <see cref="IRange{T}"/>, <see langword="false"/> en caso
    /// contrario.
    /// </returns>
    bool IsWithin(T value) => value.IsBetween(Minimum, Maximum, MinInclusive, MaxInclusive);
}
