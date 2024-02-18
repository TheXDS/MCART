/*
Range.cs

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

using System.ComponentModel.DataAnnotations.Schema;
using TheXDS.MCART.Helpers;
using TheXDS.MCART.Resources;
using TheXDS.MCART.Types.Base;

namespace TheXDS.MCART.Types.Entity;

/// <summary>
/// Define un rango de valores.
/// </summary>
/// <typeparam name="T">Tipo base del rango de valores.</typeparam>
[ComplexType]
public class Range<T> : IRange<T> where T : IComparable<T>
{
    /// <summary>
    /// Valor máximo del rango.
    /// </summary>
    public T Maximum { get; set; }

    /// <summary>
    /// Obtiene o establece un valor que determina si el valor máximo
    /// es parte del rango.
    /// </summary>
    public bool MaxInclusive { get; set; }

    /// <summary>
    /// Valor mínimo del rango.
    /// </summary>
    public T Minimum { get; set; }

    /// <summary>
    /// Obtiene o establece un valor que determina si el valor mínimo
    /// es parte del rango.
    /// </summary>
    public bool MinInclusive { get; set; }

    /// <summary>
    /// Inicializa una nueva instancia de la estructura
    /// <see cref="Range{T}" />.
    /// </summary>
    public Range() : this(default!, default!, true, true) { }

    /// <summary>
    /// Inicializa una nueva instancia de la estructura
    /// <see cref="Range{T}" />.
    /// </summary>
    /// <param name="maximum">Valor máximo del rango, inclusive.</param>
    public Range(T maximum) : this(default!, maximum, true, true) { }

    /// <summary>
    /// Inicializa una nueva instancia de la estructura
    /// <see cref="Range{T}" />.
    /// </summary>
    /// <param name="minimum">Valor mínimo del rango, inclusive.</param>
    /// <param name="maximum">Valor máximo del rango, inclusive.</param>
    public Range(T minimum, T maximum) : this(minimum, maximum, true, true) { }

    /// <summary>
    /// Inicializa una nueva instancia de la estructura
    /// <see cref="Range{T}" />.
    /// </summary>
    /// <param name="maximum">Valor máximo del rango.</param>
    /// <param name="inclusive">
    /// Si se establece en <see langword="true"/>, el valor máximo será
    /// incluido dentro del rango.
    /// </param>
    public Range(T maximum, bool inclusive) : this(default!, maximum, inclusive, inclusive)
    {
    }

    /// <summary>
    /// Inicializa una nueva instancia de la estructura
    /// <see cref="Range{T}" />.
    /// </summary>
    /// <param name="minimum">Valor mínimo del rango.</param>
    /// <param name="maximum">Valor máximo del rango.</param>
    /// <param name="inclusive">
    /// Si se establece en <see langword="true" />, los valores mínimo y
    /// máximo serán incluidos dentro del rango.
    /// </param>
    public Range(T minimum, T maximum, bool inclusive) : this(minimum, maximum, inclusive, inclusive)
    {
    }

    /// <summary>
    /// Inicializa una nueva instancia de la estructura
    /// <see cref="Range{T}"/>
    /// </summary>
    /// <param name="minimum">Valor mínimo del rango.</param>
    /// <param name="maximum">Valor máximo del rango.</param>
    /// <param name="minInclusive">
    /// Si se establece en <see langword="true"/>, el valor mínimo será
    /// incluido dentro del rango.
    /// </param>
    /// <param name="maxInclusive">
    /// Si se establece en <see langword="true"/>, el valor máximo será
    /// incluido dentro del rango.
    /// </param>
    public Range(T minimum, T maximum, bool minInclusive, bool maxInclusive)
    {
        if (minimum.CompareTo(maximum) > 0) throw Errors.MinGtMax();
        Minimum = minimum;
        Maximum = maximum;
        MinInclusive = minInclusive;
        MaxInclusive = maxInclusive;
    }

    /// <summary>
    /// Determina si un <see cref="Range{T}"/> intersecta a este.
    /// </summary>
    /// <param name="other">Rango a comprobar.</param>
    /// <returns>
    /// <see langword="true"/> si <paramref name="other"/> intersecta a
    /// este <see cref="Range{T}"/>, <see langword="false"/> en caso
    /// contrario.
    /// </returns>
    public bool Intersects(IRange<T> other)
    {
        return IsWithin(other.Maximum) || IsWithin(other.Minimum);
    }

    /// <summary>
    /// Comprueba si un valor <typeparamref name="T"/> se encuentra dentro de este <see cref="Range{T}"/>.
    /// </summary>
    /// <param name="value">Valor a comprobar.</param>
    /// <returns>
    /// <see langword="true"/> si el valor se encuentra dentro de este <see cref="Range{T}"/>,
    /// <see langword="false"/> en caso contrario.
    /// </returns>
    public bool IsWithin(T value)
    {
        return value.IsBetween(Minimum, Maximum, MinInclusive, MaxInclusive);
    }

    /// <summary>
    /// Convierte implícitamente un <see cref="Types.Range{T}"/> en un
    /// <see cref="Range{T}"/>.
    /// </summary>
    /// <param name="value">
    /// <see cref="Types.Range{T}"/> a convertir.
    /// </param>
    public static implicit operator Range<T>(Types.Range<T> value)
    {
        return new Range<T>(value.Minimum, value.Maximum, value.MinInclusive, value.MaxInclusive);
    }

    /// <summary>
    /// Convierte implícitamente un <see cref="Range{T}"/> en un
    /// <see cref="Types.Range{T}"/>.
    /// </summary>
    /// <param name="value">
    /// <see cref="Range{T}"/> a convertir.
    /// </param>
    public static implicit operator Types.Range<T>(Range<T> value)
    {
        return new Types.Range<T>(value.Minimum, value.Maximum, value.MinInclusive, value.MaxInclusive);
    }
}
