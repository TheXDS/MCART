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
/// Defines a range of values.
/// </summary>
/// <typeparam name="T">Base type of the range of values.</typeparam>
[ComplexType]
public class Range<T> : IRange<T> where T : IComparable<T>
{
    /// <summary>
    /// Maximum value of the range.
    /// </summary>
    public T Maximum { get; set; }

    /// <summary>
    /// Gets or sets a value that determines whether the maximum value
    /// is part of the range.
    /// </summary>
    public bool MaxInclusive { get; set; }

    /// <summary>
    /// Minimum value of the range.
    /// </summary>
    public T Minimum { get; set; }

    /// <summary>
    /// Gets or sets a value that determines whether the minimum value
    /// is part of the range.
    /// </summary>
    public bool MinInclusive { get; set; }

    /// <summary>
    /// Initializes a new instance of the structure
    /// <see cref="Range{T}" />.
    /// </summary>
    public Range() : this(default!, default!, true, true) { }

    /// <summary>
    /// Initializes a new instance of the structure
    /// <see cref="Range{T}" />.
    /// </summary>
    /// <param name="maximum">Maximum value of the range, inclusive.</param>
    public Range(T maximum) : this(default!, maximum, true, true) { }

    /// <summary>
    /// Initializes a new instance of the structure
    /// <see cref="Range{T}" />.
    /// </summary>
    /// <param name="minimum">Minimum value of the range, inclusive.</param>
    /// <param name="maximum">Maximum value of the range, inclusive.</param>
    public Range(T minimum, T maximum) : this(minimum, maximum, true, true) { }

    /// <summary>
    /// Initializes a new instance of the structure
    /// <see cref="Range{T}" />.
    /// </summary>
    /// <param name="maximum">Maximum value of the range.</param>
    /// <param name="inclusive">
    /// If set to <see langword="true"/>, the maximum value will
    /// be included within the range.
    /// </param>
    public Range(T maximum, bool inclusive) : this(default!, maximum, inclusive, inclusive)
    {
    }

    /// <summary>
    /// Initializes a new instance of the structure
    /// <see cref="Range{T}" />.
    /// </summary>
    /// <param name="minimum">Minimum value of the range.</param>
    /// <param name="maximum">Maximum value of the range.</param>
    /// <param name="inclusive">
    /// If set to <see langword="true" />, the minimum and
    /// maximum values will be included within the range.
    /// </param>
    public Range(T minimum, T maximum, bool inclusive) : this(minimum, maximum, inclusive, inclusive)
    {
    }

    /// <summary>
    /// Initializes a new instance of the structure
    /// <see cref="Range{T}"/>
    /// </summary>
    /// <param name="minimum">Minimum value of the range.</param>
    /// <param name="maximum">Maximum value of the range.</param>
    /// <param name="minInclusive">
    /// If set to <see langword="true"/>, the minimum value will
    /// be included within the range.
    /// </param>
    /// <param name="maxInclusive">
    /// If set to <see langword="true"/>, the maximum value will
    /// be included within the range.
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
    /// Determines if a <see cref="Range{T}"/> intersects with this one.
    /// </summary>
    /// <param name="other">Range to check.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="other"/> intersects with
    /// this <see cref="Range{T}"/>, <see langword="false"/> otherwise.
    /// </returns>
    public bool Intersects(IRange<T> other)
    {
        return IsWithin(other.Maximum) || IsWithin(other.Minimum);
    }

    /// <summary>
    /// Checks if a value <typeparamref name="T"/> is within this <see cref="Range{T}"/>.
    /// </summary>
    /// <param name="value">Value to check.</param>
    /// <returns>
    /// <see langword="true"/> if the value is within this <see cref="Range{T}"/>,
    /// <see langword="false"/> otherwise.
    /// </returns>
    public bool IsWithin(T value)
    {
        return value.IsBetween(Minimum, Maximum, MinInclusive, MaxInclusive);
    }

    /// <summary>
    /// Implicitly converts a <see cref="Types.Range{T}"/> to a
    /// <see cref="Range{T}"/>.
    /// </summary>
    /// <param name="value">
    /// <see cref="Types.Range{T}"/> to convert.
    /// </param>
    public static implicit operator Range<T>(Types.Range<T> value)
    {
        return new Range<T>(value.Minimum, value.Maximum, value.MinInclusive, value.MaxInclusive);
    }

    /// <summary>
    /// Implicitly converts a <see cref="Range{T}"/> to a
    /// <see cref="Types.Range{T}"/>.
    /// </summary>
    /// <param name="value">
    /// <see cref="Range{T}"/> to convert.
    /// </param>
    public static implicit operator Types.Range<T>(Range<T> value)
    {
        return new Types.Range<T>(value.Minimum, value.Maximum, value.MinInclusive, value.MaxInclusive);
    }
}
