/*
Size.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

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
using System.Numerics;
using TheXDS.MCART.Math;
using TheXDS.MCART.Misc;
using TheXDS.MCART.Resources;
using TheXDS.MCART.Types.Base;
using TheXDS.MCART.Types.Extensions;
using CI = System.Globalization.CultureInfo;

namespace TheXDS.MCART.Types;

/// <summary>
/// Universal structure that describes the size of an object in width and
/// height in a two-dimensional space.
/// </summary>
/// <param name="width">Width value.</param>
/// <param name="height">Height value.</param>
public struct Size(double width, double height) : IFormattable, IEquatable<Size>, IEquatable<ISize>, IEquatable<IVector>, ISize, IVector
{
    /// <summary>
    /// Gets a value that does not represent any size. This field is
    /// read-only.
    /// </summary>
    public static readonly Size Nothing = new(double.NaN, double.NaN);

    /// <summary>
    /// Gets a value that represents a null size. This field is
    /// read-only.
    /// </summary>
    public static readonly Size Zero = new(0, 0);

    /// <summary>
    /// Gets a value that represents an infinite size. This field
    /// is read-only.
    /// </summary>
    public static readonly Size Infinity = new(double.PositiveInfinity, double.PositiveInfinity);

    /// <summary>
    /// Performs an addition operation on the points.
    /// </summary>
    /// <param name="l">Point 1.</param>
    /// <param name="r">Point 2.</param>
    /// <returns>The sum of the vectors of the points.</returns>
    public static Size operator +(Size l, Size r)
    {
        return new(l.Width + r.Width, l.Height + r.Height);
    }

    /// <summary>
    /// Performs an addition operation on the points.
    /// </summary>
    /// <param name="l">Point 1.</param>
    /// <param name="r">Point 2.</param>
    /// <returns>The sum of the vectors of the points.</returns>
    public static Size operator +(Size l, ISize r)
    {
        return new(l.Width + r.Width, l.Height + r.Height);
    }

    /// <summary>
    /// Performs an addition operation on the points.
    /// </summary>
    /// <param name="l">Point 1.</param>
    /// <param name="r">Point 2.</param>
    /// <returns>The sum of the vectors of the points.</returns>
    public static Size operator +(Size l, IVector r)
    {
        return new(l.Width + r.X, l.Height + r.Y);
    }

    /// <summary>
    /// Performs an addition operation on the point.
    /// </summary>
    /// <param name="l">Point 1.</param>
    /// <param name="r">Addition operand.</param>
    /// <returns>
    /// A new <see cref="Size" /> whose vectors are the sum of the
    /// original vectors + <paramref name="r" />.
    /// </returns>
    public static Size operator +(Size l, double r)
    {
        return new(l.Width + r, l.Height + r);
    }

    /// <summary>
    /// Performs a subtraction operation on the points.
    /// </summary>
    /// <param name="l">Point 1.</param>
    /// <param name="r">Point 2.</param>
    /// <returns>The difference of the vectors of the points.</returns>
    public static Size operator -(Size l, Size r)
    {
        return new(l.Width - r.Width, l.Height - r.Height);
    }

    /// <summary>
    /// Performs a subtraction operation on the points.
    /// </summary>
    /// <param name="l">Point 1.</param>
    /// <param name="r">Point 2.</param>
    /// <returns>The difference of the vectors of the points.</returns>
    public static Size operator -(Size l, ISize r)
    {
        return new(l.Width - r.Width, l.Height - r.Height);
    }

    /// <summary>
    /// Performs a subtraction operation on the point.
    /// </summary>
    /// <param name="l">Point 1.</param>
    /// <param name="r">Subtraction operand.</param>
    /// <returns>
    /// A new <see cref="Size" /> whose vectors are the difference of the
    /// original vectors - <paramref name="r" />.
    /// </returns>
    public static Size operator -(Size l, double r)
    {
        return new(l.Width - r, l.Height - r);
    }

    /// <summary>
    /// Performs a subtraction operation on the points.
    /// </summary>
    /// <param name="l">Point 1.</param>
    /// <param name="r">Point 2.</param>
    /// <returns>The difference of the vectors of the points.</returns>
    public static Size operator -(Size l, IVector r)
    {
        return new(l.Width - r.X, l.Height - r.Y);
    }

    /// <summary>
    /// Performs a multiplication operation on the points.
    /// </summary>
    /// <param name="l">Point 1.</param>
    /// <param name="r">Point 2.</param>
    /// <returns>The multiplication of the vectors of the points.</returns>
    public static Size operator *(Size l, Size r)
    {
        return new(l.Width * r.Width, l.Height * r.Height);
    }

    /// <summary>
    /// Performs a multiplication operation on the points.
    /// </summary>
    /// <param name="l">Point 1.</param>
    /// <param name="r">Point 2.</param>
    /// <returns>The multiplication of the vectors of the points.</returns>
    public static Size operator *(Size l, ISize r)
    {
        return new(l.Width * r.Width, l.Height * r.Height);
    }

    /// <summary>
    /// Performs a multiplication operation on the point.
    /// </summary>
    /// <param name="l">Point 1.</param>
    /// <param name="r">Multiplication operand.</param>
    /// <returns>
    /// A new <see cref="Size" /> whose vectors are the multiplication
    /// of the original vectors * <paramref name="r" />.
    /// </returns>
    public static Size operator *(Size l, double r)
    {
        return new(l.Width * r, l.Height * r);
    }

    /// <summary>
    /// Performs a multiplication operation on the points.
    /// </summary>
    /// <param name="l">Point 1.</param>
    /// <param name="r">Point 2.</param>
    /// <returns>The multiplication of the vectors of the points.</returns>
    public static Size operator *(Size l, IVector r)
    {
        return new(l.Width * r.X, l.Height * r.Y);
    }

    /// <summary>
    /// Performs a division operation on the points.
    /// </summary>
    /// <param name="l">Point 1.</param>
    /// <param name="r">Point 2.</param>
    /// <returns>The division of the vectors of the points.</returns>
    public static Size operator /(Size l, Size r)
    {
        return new(l.Width / r.Width, l.Height / r.Height);
    }

    /// <summary>
    /// Performs a division operation on the points.
    /// </summary>
    /// <param name="l">Point 1.</param>
    /// <param name="r">Point 2.</param>
    /// <returns>The division of the vectors of the points.</returns>
    public static Size operator /(Size l, ISize r)
    {
        return new(l.Width / r.Width, l.Height / r.Height);
    }

    /// <summary>
    /// Performs a division operation on the point.
    /// </summary>
    /// <param name="l">Point 1.</param>
    /// <param name="r">Division operand.</param>
    /// <returns>
    /// A new <see cref="Size" /> whose vectors are the division of
    /// the original vectors / <paramref name="r" />.
    /// </returns>
    public static Size operator /(Size l, double r)
    {
        return new(l.Width / r, l.Height / r);
    }

    /// <summary>
    /// Performs a division operation on the points.
    /// </summary>
    /// <param name="l">Point 1.</param>
    /// <param name="r">Point 2.</param>
    /// <returns>The division of the vectors of the points.</returns>
    public static Size operator /(Size l, IVector r)
    {
        return new(l.Width / r.X, l.Height / r.Y);
    }

    /// <summary>
    /// Performs a remainder operation on the points.
    /// </summary>
    /// <param name="l">Point 1.</param>
    /// <param name="r">Point 2.</param>
    /// <returns>The remainder of the vectors of the points.</returns>
    public static Size operator %(Size l, Size r)
    {
        return new(l.Width % r.Width, l.Height % r.Height);
    }

    /// <summary>
    /// Performs a remainder operation on the points.
    /// </summary>
    /// <param name="l">Point 1.</param>
    /// <param name="r">Point 2.</param>
    /// <returns>The remainder of the vectors of the points.</returns>
    public static Size operator %(Size l, ISize r)
    {
        return new(l.Width % r.Width, l.Height % r.Height);
    }

    /// <summary>
    /// Performs a remainder operation on the point.
    /// </summary>
    /// <param name="l">Point 1.</param>
    /// <param name="r">Remainder operand.</param>
    /// <returns>
    /// A new <see cref="Size" /> whose vectors are the remainder of the
    /// original vectors % <paramref name="r" />.
    /// </returns>
    public static Size operator %(Size l, double r)
    {
        return new(l.Width % r, l.Height % r);
    }

    /// <summary>
    /// Performs a remainder operation on the points.
    /// </summary>
    /// <param name="l">Point 1.</param>
    /// <param name="r">Point 2.</param>
    /// <returns>
    /// A new <see cref="Size" /> whose vectors are the remainder of the
    /// original vectors % <paramref name="r" />.
    /// </returns>
    public static Size operator %(Size l, IVector r)
    {
        return new(l.Width % r.X, l.Height % r.Y);
    }

    /// <summary>
    /// Increments by 1 the vectors of the point.
    /// </summary>
    /// <param name="p">Point to increment.</param>
    /// <returns>A point with its vectors incremented by 1.</returns>
    public static Size operator ++(Size p)
    {
        p.Width++;
        p.Height++;
        return p;
    }

    /// <summary>
    /// Decrements by 1 the vectors of the point.
    /// </summary>
    /// <param name="p">Point to decrement.</param>
    /// <returns>A point with its vectors decremented by 1.</returns>
    public static Size operator --(Size p)
    {
        p.Width--;
        p.Height--;
        return p;
    }

    /// <summary>
    /// Converts to positive the vectors of the point.
    /// </summary>
    /// <param name="p">Point to operate.</param>
    /// <returns>A point with its vectors positive.</returns>
    public static Size operator +(Size p)
    {
        return new(+p.Width, +p.Height);
    }

    /// <summary>
    /// Inverts the sign of the vectors of the point.
    /// </summary>
    /// <param name="p">Point to operate.</param>
    /// <returns>A point with the sign of its vectors inverted.</returns>
    public static Size operator -(Size p)
    {
        return new(-p.Width, -p.Height);
    }

    /// <summary>
    /// Compares equality between two instances of <see cref="Size"/>.
    /// </summary>
    /// <param name="size1">
    /// First element to compare.
    /// </param>
    /// <param name="size2">
    /// Second element to compare.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the sizes represented in both
    /// objects are equal, <see langword="false"/> otherwise.
    /// </returns>
    public static bool operator ==(Size size1, ISize size2)
    {
        return size1.Equals(size2);
    }

    /// <summary>
    /// Compares equality between two instances of <see cref="Size"/>.
    /// </summary>
    /// <param name="size1">
    /// First element to compare.
    /// </param>
    /// <param name="size2">
    /// Second element to compare.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the sizes represented in both
    /// objects are equal, <see langword="false"/> otherwise.
    /// </returns>
    public static bool operator ==(Size size1, Size size2)
    {
        return size1.Equals(size2);
    }

    /// <summary>
    /// Compares equality between two instances of <see cref="Size"/>.
    /// </summary>
    /// <param name="size1">
    /// First element to compare.
    /// </param>
    /// <param name="size2">
    /// Second element to compare.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the sizes represented in both
    /// objects are equal, <see langword="false"/> otherwise.
    /// </returns>
    public static bool operator ==(Size size1, IVector size2)
    {
        return size1.Equals(size2);
    }

    /// <summary>
    /// Compares inequality between two instances of 
    /// <see cref="Size"/>.
    /// </summary>
    /// <param name="size1">
    /// First element to compare.
    /// </param>
    /// <param name="size2">
    /// Second element to compare.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the sizes represented in both
    /// objects are different, <see langword="false"/> otherwise.
    /// </returns>
    public static bool operator !=(Size size1, IVector size2)
    {
        return !(size1 == size2);
    }

    /// <summary>
    /// Compares inequality between two instances of 
    /// <see cref="Size"/>.
    /// </summary>
    /// <param name="size1">
    /// First element to compare.
    /// </param>
    /// <param name="size2">
    /// Second element to compare.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the sizes represented in both
    /// objects are different, <see langword="false"/> otherwise.
    /// </returns>
    public static bool operator !=(Size size1, Size size2)
    {
        return !(size1 == size2);
    }

    /// <summary>
    /// Compares inequality between two instances of 
    /// <see cref="Size"/>.
    /// </summary>
    /// <param name="size1">
    /// First element to compare.
    /// </param>
    /// <param name="size2">
    /// Second element to compare.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the sizes represented in both
    /// objects are different, <see langword="false"/> otherwise.
    /// </returns>
    public static bool operator !=(Size size1, ISize size2)
    {
        return !(size1 == size2);
    }

    /// <summary>
    /// Implicitly converts a <see cref="System.Drawing.Size"/> object
    /// to a <see cref="Size"/>.
    /// </summary>
    /// <param name="size">
    /// Object to convert.
    /// </param>
    public static implicit operator System.Drawing.Size(Size size)
    {
        return new((int)size.Width, (int)size.Height);
    }

    /// <summary>
    /// Implicitly converts a <see cref="System.Drawing.SizeF"/> object
    /// to a <see cref="Size"/>.
    /// </summary>
    /// <param name="size">
    /// Object to convert.
    /// </param>
    public static implicit operator System.Drawing.SizeF(Size size)
    {
        return new((float)size.Width, (float)size.Height);
    }

    /// <summary>
    /// Implicitly converts a <see cref="Size"/> object to a
    /// <see cref="System.Drawing.Size"/>.
    /// </summary>
    /// <param name="size">
    /// Object to convert.
    /// </param>
    public static implicit operator Size(System.Drawing.Size size)
    {
        return new(size.Width, size.Height);
    }

    /// <summary>
    /// Implicitly converts a <see cref="Size"/> object to a
    /// <see cref="System.Drawing.SizeF"/>.
    /// </summary>
    /// <param name="size">
    /// Object to convert.
    /// </param>
    public static implicit operator Size(System.Drawing.SizeF size)
    {
        return new(size.Width, size.Height);
    }

    /// <summary>
    /// Gets the height component of the size.
    /// </summary>
    public double Height { get; set; } = height;

    /// <summary>
    /// Gets the width component of the size.
    /// </summary>
    public double Width { get; set; } = width;

    /// <summary>
    /// Calculates the square area represented by this size.
    /// </summary>
    public readonly double SquareArea => Height * Width;

    /// <summary>
    /// Calculates the square perimeter represented by this size.
    /// </summary>
    public readonly double SquarePerimeter => (Height * 2) + (Width * 2);

    /// <summary>
    /// Determines if this instance represents a null size.
    /// </summary>
    /// <returns>
    /// <see langword="true"/> if the size is null,
    /// <see langword="false"/> if the size contains area, and
    /// <see langword="null"/> if any magnitude is undefined.
    /// </returns>
    public readonly bool? IsZero => Height.IsValid() && Height == 0 && Width.IsValid() && Width == 0;

    /// <summary>
    /// Gets a value indicating whether the size is valid in a real physical context.
    /// </summary>
    public readonly bool IsReal => Height.IsValid() && Height > 0 && Width.IsValid() && Width > 0;

    /// <summary>
    /// Gets a value indicating whether all size magnitudes of this
    /// instance are valid.
    /// </summary>
    public readonly bool IsValid => Height.IsValid() && Width.IsValid();

    readonly double IVector.X => Width;

    readonly double IVector.Y => Height;

    /// <summary>
    /// Determines if this instance of <see cref="Size"/> is equal to
    /// another.
    /// </summary>
    /// <param name="other">
    /// Instance of <see cref="Size"/> to compare against.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the sizes represented in both
    /// objects are equal, <see langword="false"/> otherwise.
    /// </returns>
    public readonly bool Equals(Size other)
    {
        return (Height == other.Height || (!Height.IsValid() && !other.Height.IsValid()))
            && (Width == other.Width || (!Width.IsValid() && !other.Width.IsValid()));
    }

    /// <summary>
    /// Determines if this instance of <see cref="ISize3D"/> is equal to
    /// another.
    /// </summary>
    /// <param name="other">
    /// Instance of <see cref="ISize3D"/> to compare against.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the sizes represented in both
    /// objects are equal, <see langword="false"/> otherwise.
    /// </returns>
    public readonly bool Equals(ISize? other)
    {
        return other is not null
            && (Height == other.Height || (!Height.IsValid() && !other.Height.IsValid()))
            && (Width == other.Width || (!Width.IsValid() && !other.Width.IsValid()));
    }

    /// <summary>
    /// Determines if this instance of <see cref="IVector3D"/> is equal to
    /// another.
    /// </summary>
    /// <param name="other">
    /// Instance of <see cref="IVector3D"/> to compare against.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the sizes represented in both
    /// objects are equal, <see langword="false"/> otherwise.
    /// </returns>
    public readonly bool Equals(IVector? other)
    {
        return other is not null
            && (Height == other.Y || (!Height.IsValid() && !other.Y.IsValid()))
            && (Width == other.X || (!Width.IsValid() && !other.X.IsValid()));
    }

    /// <summary>
    /// Indicates whether this instance and a specified object are equal.
    /// </summary>
    /// <param name="obj">
    /// Object to compare with the current instance.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if this instance and <paramref name="obj" /> are equal;
    /// otherwise, <see langword="false" />.
    /// </returns>
    public override readonly bool Equals(object? obj)
    {
        if (obj is not ISize p) return false;
        return Equals(p);
    }

    /// <summary>
    /// Returns the hash code generated for this instance.
    /// </summary>
    /// <returns>
    /// A hash code that represents this instance.
    /// </returns>
    public override readonly int GetHashCode()
    {
        return HashCode.Combine(Height, Width);
    }

    /// <summary>
    /// Creates a <see cref="Size"/> from a string.
    /// </summary>
    /// <param name="value">
    /// Value to create a <see cref="Size"/> from.
    /// </param>
    /// <exception cref="FormatException">
    /// Thrown if the conversion failed.
    /// </exception>
    /// <returns>The created <see cref="Size"/>.</returns>
    public static Size Parse(string value)
    {
        if (TryParse(value, out Size returnValue)) return returnValue;
        throw new FormatException();
    }

    /// <summary>
    /// Converts this object to its string representation.
    /// </summary>
    /// <returns>
    /// A string representation of this object.
    /// </returns>
    public override readonly string ToString()
    {
        return ToString(null);
    }

    /// <summary>
    /// Converts this object to its string representation.
    /// </summary>
    /// <param name="format">Format to use.</param>
    /// <returns>
    /// A string representation of this object.
    /// </returns>
    public readonly string ToString(string? format)
    {
        return ToString(format, CI.CurrentCulture);
    }

    /// <summary>
    /// Converts this object to its string representation.
    /// </summary>
    /// <param name="format">Format to use.</param>
    /// <param name="formatProvider">
    /// Optional parameter. Format provider for the culture to use for formatting
    /// the string representation of this object. If omitted,
    /// <see cref="CI.CurrentCulture" /> will be used.
    /// </param>
    /// <returns>
    /// A string representation of this object.
    /// </returns>
    public readonly string ToString(string? format, IFormatProvider? formatProvider)
    {
        if (format.IsEmpty()) format = "C";
        return format.ToUpperInvariant()[0] switch
        {
            'C' => $"{Width}, {Height}",
            'B' => $"[{Width}, {Height}]",
            'V' => $"Width: {Width}, Height: {Height}",
            'N' => $"Width: {Width}{Environment.NewLine}Height: {Height}",
            _ => throw Errors.FormatNotSupported(format),
        };
    }

    /// <summary>
    /// Attempts to create a <see cref="Size"/> from a string.
    /// </summary>
    /// <param name="value">
    /// Value to create a <see cref="Size"/> from.
    /// </param>
    /// <param name="size">
    /// The created <see cref="Size"/>.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the conversion was successful,
    /// <see langword="false"/> otherwise.
    /// </returns>
    public static bool TryParse(string? value, out Size size)
    {
        switch (value)
        {
            case nameof(Nothing):
            case "":
            case null:
                size = Nothing;
                break;
            case nameof(Zero):
            case "0":
                size = Zero;
                break;
            case nameof(Infinity):
            case "PositiveInfinity":
            case "∞":
                size = Infinity;
                break;
            default:
                string[]? separators =
                [
                    ", ",
                    "; ",
                    " - ",
                    " : ",
                    " | ",
                    " ",
                    ",",
                    ";",
                    ":",
                    "|",
                ];
                return PrivateInternals.TryParseValues<double, Size>(new DoubleConverter(), separators, value.Without("()[]{}".ToCharArray()), 2, l => new(l[0], l[1]), out size);
        }
        return true;
    }

    /// <summary>
    /// Implicitly converts a <see cref="Size"/> to a <see cref="Vector2"/>.
    /// </summary>
    /// <param name="p"><see cref="Size"/> value to be converted.</param>
    public static implicit operator Vector2(Size p) => new((float)p.Width, (float)p.Height);

    /// <summary>
    /// Implicitly converts a <see cref="Vector2"/> to a <see cref="Size"/>.
    /// </summary>
    /// <param name="p"><see cref="Vector2"/> value to be converted.</param>
    public static implicit operator Size(Vector2 p) => new(p.X, p.Y);
}
