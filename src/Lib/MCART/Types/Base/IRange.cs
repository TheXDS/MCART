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
/// Interface that defines a range of values.
/// </summary>
/// <typeparam name="T">Base type of the value range.</typeparam>
public interface IRange<T> where T : IComparable<T>
{
    /// <summary>
    /// Maximum value of the range.
    /// </summary>
    T Maximum { get; set; }

    /// <summary>
    /// Gets or sets a value that determines whether the maximum value
    /// is part of the range.
    /// </summary>
    bool MaxInclusive { get; set; }

    /// <summary>
    /// Minimum value of the range.
    /// </summary>
    T Minimum { get; set; }

    /// <summary>
    /// Gets or sets a value that determines whether the minimum value
    /// is part of the range.
    /// </summary>
    bool MinInclusive { get; set; }

    /// <summary>
    /// Determines if a <see cref="IRange{T}"/> intersects with this.
    /// </summary>
    /// <param name="other">Range to check.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="other"/> intersects
    /// with this <see cref="IRange{T}"/>, <see langword="false"/>
    /// otherwise.
    /// </returns>
    bool Intersects(IRange<T> other) => IsWithin(other.Maximum) || IsWithin(other.Minimum);

    /// <summary>
    /// Checks if a value <typeparamref name="T"/> is within this
    /// <see cref="IRange{T}"/>
    /// </summary>
    /// <param name="value">Value to check.</param>
    /// <returns>
    /// <see langword="true"/> if the value is within this
    /// <see cref="IRange{T}"/>, <see langword="false"/> otherwise.
    /// </returns>
    bool IsWithin(T value) => value.IsBetween(Minimum, Maximum, MinInclusive, MaxInclusive);
}
