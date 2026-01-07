/*
Range.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

This file defines the Range<TValue> structure, which allows representing
value ranges.

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2026 César Andrés Morgan

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

using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using TheXDS.MCART.Helpers;
using TheXDS.MCART.Misc;
using TheXDS.MCART.Resources;
using TheXDS.MCART.Types.Base;

namespace TheXDS.MCART.Types;

/// <summary>
/// Defines a range of values.
/// </summary>
/// <typeparam name="T">Base type of the value range.</typeparam>
public struct Range<T> : IRange<T>, IEquatable<IRange<T>>, ICloneable<Range<T>> where T : IComparable<T>
{
    private T _minimum;
    private T _maximum;

    /// <summary>
    /// Initializes a new instance of the <see cref="Range{T}" /> structure.
    /// </summary>
    /// <param name="maximum">Maximum value of the range, inclusive.</param>
    public Range(T maximum) : this(default!, maximum, true, true) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Range{T}" /> structure.
    /// </summary>
    /// <param name="minimum">Minimum value of the range, inclusive.</param>
    /// <param name="maximum">Maximum value of the range, inclusive.</param>
    public Range(T minimum, T maximum) : this(minimum, maximum, true, true) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Range{T}" /> structure.
    /// </summary>
    /// <param name="maximum">Maximum value of the range.</param>
    /// <param name="inclusive">
    /// If set to <see langword="true"/>, the maximum value will be
    /// included in the range.
    /// </param>
    public Range(T maximum, bool inclusive) : this(default!, maximum, inclusive, inclusive)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Range{T}" /> structure.
    /// </summary>
    /// <param name="minimum">Minimum value of the range.</param>
    /// <param name="maximum">Maximum value of the range.</param>
    /// <param name="inclusive">
    /// If set to <see langword="true" />, both minimum and maximum values
    /// will be included in the range.
    /// </param>
    public Range(T minimum, T maximum, bool inclusive) : this(minimum, maximum, inclusive, inclusive)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Range{T}"/> structure.
    /// </summary>
    /// <param name="minimum">Minimum value of the range.</param>
    /// <param name="maximum">Maximum value of the range.</param>
    /// <param name="minInclusive">
    /// If set to <see langword="true"/>, the minimum value will be
    /// included in the range.
    /// </param>
    /// <param name="maxInclusive">
    /// If set to <see langword="true"/>, the maximum value will be
    /// included in the range.
    /// </param>
    public Range(T minimum, T maximum, bool minInclusive, bool maxInclusive)
    {
        if (minimum.CompareTo(maximum) > 0) throw Errors.MinGtMax();
        _minimum = minimum;
        _maximum = maximum;
        MinInclusive = minInclusive;
        MaxInclusive = maxInclusive;
    }

    /// <summary>
    /// Compares two instances of <see cref="Range{T}"/> for equality.
    /// </summary>
    /// <param name="left">Object to compare.</param>
    /// <param name="right">Object to compare against.</param>
    /// <returns>
    /// <see langword="true" /> if both instances are equal,
    /// <see langword="false" /> otherwise.
    /// </returns>
    public static bool operator ==(Range<T> left, Range<T> right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// Compares two instances of <see cref="Range{T}"/>, and returns
    /// <see langword="true"/> if they are different from each other.
    /// </summary>
    /// <param name="left">Object to compare.</param>
    /// <param name="right">Object to compare against.</param>
    /// <returns>
    /// <see langword="true" /> if both instances are different,
    /// <see langword="false" /> otherwise.
    /// </returns>
    public static bool operator !=(Range<T> left, Range<T> right)
    {
        return !(left == right);
    }

    /// <summary>
    /// Minimum value of the range.
    /// </summary>
    public T Minimum
    {
        readonly get => _minimum;
        set
        {
            if (value.CompareTo(Maximum) > 0) throw new ArgumentOutOfRangeException(nameof(value));
            _minimum = value;
        }
    }

    /// <summary>
    /// Maximum value of the range.
    /// </summary>
    public T Maximum
    {
        readonly get => _maximum;
        set
        {
            if (value.CompareTo(Minimum) < 0) throw new ArgumentOutOfRangeException(nameof(value));
            _maximum = value;
        }
    }

    /// <summary>
    /// Gets or sets a value that determines whether the minimum value
    /// is part of the range.
    /// </summary>
    public bool MinInclusive { get; set; }

    /// <summary>
    /// Gets or sets a value that determines whether the maximum value
    /// is part of the range.
    /// </summary>
    public bool MaxInclusive { get; set; }

    /// <summary>
    /// Converts this <see cref="Range{T}"/> to its string representation.
    /// </summary>
    /// <returns>
    /// A string representation of this <see cref="Range{T}"/>.
    /// </returns>
    public override readonly string ToString()
    {
        return $"{Minimum} - {Maximum}";
    }

    /// <summary>
    /// Checks if a <typeparamref name="T"/> value is within this <see cref="Range{T}"/>.
    /// </summary>
    /// <param name="value">Value to check.</param>
    /// <returns>
    /// <see langword="true"/> if the value is within this <see cref="Range{T}"/>,
    /// <see langword="false"/> otherwise.
    /// </returns>
    public readonly bool IsWithin(T value)
    {
        return value.IsBetween(Minimum, Maximum, MinInclusive, MaxInclusive);
    }

    /// <summary>
    /// Creates a <see cref="Range{T}"/> from a string.
    /// </summary>
    /// <param name="value">
    /// Value from which to create a <see cref="Range{T}"/>.
    /// </param>
    /// <exception cref="FormatException">
    /// Thrown if the conversion fails.
    /// </exception>
    /// <returns>The created <see cref="Range{T}"/>.</returns>
    [RequiresUnreferencedCode(AttributeErrorMessages.MethodScansForTypes)]
    [RequiresDynamicCode(AttributeErrorMessages.MethodCallsDynamicCode)]
    public static Range<T> Parse(string value)
    {
        if (TryParse(value, out Range<T> returnValue)) return returnValue;
        throw new FormatException();
    }

    /// <summary>
    /// Attempts to create a <see cref="Range{T}"/> from a string.
    /// </summary>
    /// <param name="value">
    /// Value from which to create a <see cref="Range{T}"/>.
    /// </param>
    /// <param name="range">
    /// The created <see cref="Range{T}"/>.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the conversion succeeded,
    /// <see langword="false" /> otherwise.
    /// </returns>
    [RequiresUnreferencedCode(AttributeErrorMessages.MethodScansForTypes)]
    [RequiresDynamicCode(AttributeErrorMessages.MethodCallsDynamicCode)]
    public static bool TryParse(string value, out Range<T> range)
    {
        return TryParse(Common.FindConverter<T>() ?? throw new InvalidCastException(), value, out range);
    }

    /// <summary>
    /// Combines this <see cref="Range{T}"/> with another.
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public readonly Range<T> Join(IRange<T> other)
    {
        return new(new[] { Minimum, other.Minimum }.Min()!, new[] { Maximum, other.Maximum }.Max()!);
    }

    /// <summary>
    /// Gets an intersection range from this and another specified range.
    /// </summary>
    /// <param name="other">Range to intersect.</param>
    /// <returns>
    /// The intersection between this range and <paramref name="other"/>.
    /// </returns>
    public readonly Range<T> Intersect(IRange<T> other)
    {
        return new(new[] { Minimum, other.Minimum }.Max()!, new[] { Maximum, other.Maximum }.Min()!);
    }

    /// <summary>
    /// Determines if a <see cref="Range{T}"/> intersects with this one.
    /// </summary>
    /// <param name="other">Range to check.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="other"/> intersects this
    /// <see cref="Range{T}"/>, <see langword="false"/> otherwise.
    /// </returns>
    public readonly bool Intersects(IRange<T> other)
    {
        return IsWithin(other.Maximum) || IsWithin(other.Minimum);
    }

    /// <summary>
    /// Unites two value ranges.
    /// </summary>
    /// <param name="left">Left operand.</param>
    /// <param name="right">Right operand.</param>
    /// <returns>
    /// The union of both <see cref="Range{T}"/>. If the ranges don't
    /// intersect, all missing values will be included.
    /// </returns>
    public static Range<T> operator +(Range<T> left, Range<T> right)
    {
        return left.Join(right);
    }

    /// <summary>
    /// Indicates whether this instance and a specified object are equal.
    /// </summary>
    /// <param name="obj">
    /// The object to compare with the current instance.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if this instance and
    /// <paramref name="obj" /> are equal, <see langword="false" />
    /// otherwise.
    /// </returns>
    public override readonly bool Equals(object? obj)
    {
        return Equals(obj as IRange<T>);
    }

    /// <summary>
    /// Returns the hash code for this instance.
    /// </summary>
    /// <returns>The hash code for this instance.</returns>
    public override readonly int GetHashCode()
    {
        return HashCode.Combine(Minimum, Maximum, MinInclusive, MaxInclusive);
    }

    /// <summary>
    /// Indicates whether this instance and a specified object are equal.
    /// </summary>
    /// <param name="other">
    /// The object to compare with the current instance.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if this instance and
    /// <paramref name="other" /> are equal, <see langword="false" />
    /// otherwise.
    /// </returns>
    public readonly bool Equals(IRange<T>? other)
    {
        if (other is null) return false;
        return
            Minimum.CompareTo(other.Minimum) == 0 &&
            Maximum.CompareTo(other.Maximum) == 0 &&
            MinInclusive == other.MinInclusive &&
            MaxInclusive == other.MaxInclusive;
    }

    /// <inheritdoc/>
    public readonly Range<T> Clone()
    {
        return new(_minimum, _maximum, MinInclusive, MaxInclusive);
    }

    internal static Range<T> Parse(TypeConverter converter, string value)
    {
        if (TryParse(converter, value, out Range<T> returnValue)) return returnValue;
        throw new FormatException();
    }

    internal static bool TryParse(TypeConverter converter, string value, out Range<T> range)
    {
        string[] separators =
        [
            ", ",
            "...",
            " - ",
            "; ",
            " : ",
            " | ",
            " ",
            ",",
            ";",
            ":",
            "|",
            " .. ",
            "..",
            " => ",
            " -> ",
            "=>",
            "->",
            "-"
        ];
        return PrivateInternals.TryParseValues<T, Range<T>>(converter, separators, value, 2, l => new Range<T>(l[0], l[1]), out range);
    }
}
