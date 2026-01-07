/*
Point.cs

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
using TheXDS.MCART.Helpers;
using TheXDS.MCART.Math;
using TheXDS.MCART.Misc;
using TheXDS.MCART.Resources;
using TheXDS.MCART.Types.Base;
using static System.Math;
using static TheXDS.MCART.Types.Extensions.StringExtensions;
using CI = System.Globalization.CultureInfo;

namespace TheXDS.MCART.Types;

/// <summary>
/// Universal type for a set of two-dimensional coordinates.
/// </summary>
/// <param name="x">The x coordinate.</param>
/// <param name="y">The y coordinate.</param>
public struct Point(double x, double y) : IVector, IFormattable, IEquatable<Point>
{
    /// <summary>
    /// Gets a point that does not represent any position. This field is read-only.
    /// </summary>
    /// <value>
    /// A <see cref="Point" /> with its coordinates set to <see cref="double.NaN"/>.
    /// </value>
    public static readonly Point Nowhere = new(double.NaN, double.NaN);

    /// <summary>
    /// Gets a point at the origin. This field is read-only.
    /// </summary>
    /// <value>
    /// A <see cref="Point" /> with its coordinates at the origin.
    /// </value>
    public static readonly Point Origin = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="Point"/> structure.
    /// </summary>
    public Point() : this(0, 0)
    {
    }

    /// <summary>
    /// Converts a <see cref="Point" /> to a <see cref="System.Drawing.Point" />.
    /// </summary>
    /// <param name="x">The <see cref="Point" /> to convert.</param>
    /// <returns>
    /// A <see cref="System.Drawing.Point" /> equivalent to the specified <see cref="Point" />.
    /// </returns>
    public static implicit operator System.Drawing.Point(Point x) => new((int)x.X, (int)x.Y);

    /// <summary>
    /// Converts a <see cref="System.Drawing.Point" /> to a <see cref="Point" />.
    /// </summary>
    /// <param name="x">The <see cref="System.Drawing.Point" /> to convert.</param>
    /// <returns>
    /// A <see cref="Point" /> equivalent to the specified <see cref="System.Drawing.Point" />.
    /// </returns>
    public static implicit operator Point(System.Drawing.Point x) => new(x.X, x.Y);

    /// <summary>
    /// Performs addition on points.
    /// </summary>
    /// <param name="l">Point 1.</param>
    /// <param name="r">Point 2.</param>
    /// <returns>The sum of the vectors of the points.</returns>
    public static Point operator +(Point l, Point r)
    {
        return new(l.X + r.X, l.Y + r.Y);
    }

    /// <summary>
    /// Performs addition on points.
    /// </summary>
    /// <param name="l">Point 1.</param>
    /// <param name="r">Point 2.</param>
    /// <returns>The sum of the vectors of the points.</returns>
    public static Point operator +(Point l, IVector r)
    {
        return new(l.X + r.X, l.Y + r.Y);
    }

    /// <summary>
    /// Performs addition on a point.
    /// </summary>
    /// <param name="l">Point 1.</param>
    /// <param name="r">Addition operand.</param>
    /// <returns>
    /// A new <see cref="Point" /> whose vectors are the sum of the original
    /// vectors + <paramref name="r"/>.
    /// </returns>
    public static Point operator +(Point l, double r)
    {
        return new(l.X + r, l.Y + r);
    }

    /// <summary>
    /// Performs subtraction on points.
    /// </summary>
    /// <param name="l">Point 1.</param>
    /// <param name="r">Point 2.</param>
    /// <returns>The difference of the vectors of the points.</returns>
    public static Point operator -(Point l, Point r)
    {
        return new(l.X - r.X, l.Y - r.Y);
    }

    /// <summary>
    /// Performs subtraction on points.
    /// </summary>
    /// <param name="l">Point 1.</param>
    /// <param name="r">Point 2.</param>
    /// <returns>The difference of the vectors of the points.</returns>
    public static Point operator -(Point l, IVector r)
    {
        return new(l.X - r.X, l.Y - r.Y);
    }

    /// <summary>
    /// Performs subtraction operation on the point.
    /// </summary>
    /// <param name="l">Point 1.</param>
    /// <param name="r">Subtraction operand.</param>
    /// <returns>
    /// A new <see cref="Point" /> with vectors being the difference of
    /// original vectors - <paramref name="r" />.
    /// </returns>
    public static Point operator -(Point l, double r)
    {
        return new(l.X - r, l.Y - r);
    }

    /// <summary>
    /// Performs multiplication operation on points.
    /// </summary>
    /// <param name="l">Point 1.</param>
    /// <param name="r">Point 2.</param>
    /// <returns>The product of the vectors of the points.</returns>
    public static Point operator *(Point l, Point r)
    {
        return new(l.X * r.X, l.Y * r.Y);
    }

    /// <summary>
    /// Performs multiplication operation on points.
    /// </summary>
    /// <param name="l">Point 1.</param>
    /// <param name="r">Vector operand.</param>
    /// <returns>The product of the vectors of the point and vector.</returns>
    public static Point operator *(Point l, IVector r)
    {
        return new(l.X * r.X, l.Y * r.Y);
    }

    /// <summary>
    /// Performs scalar multiplication on the point.
    /// </summary>
    /// <param name="l">Point 1.</param>
    /// <param name="r">Multiplication operand.</param>
    /// <returns>
    /// A new <see cref="Point" /> with vectors being the product of
    /// original vectors * <paramref name="r" />.
    /// </returns>
    public static Point operator *(Point l, double r)
    {
        return new(l.X * r, l.Y * r);
    }

    /// <summary>
    /// Performs division operation on points.
    /// </summary>
    /// <param name="l">Point 1.</param>
    /// <param name="r">Point 2.</param>
    /// <returns>The quotient of the vectors of the points.</returns>
    public static Point operator /(Point l, Point r)
    {
        return new(l.X / r.X, l.Y / r.Y);
    }

    /// <summary>
    /// Performs division operation on points.
    /// </summary>
    /// <param name="l">Point 1.</param>
    /// <param name="r">Vector operand.</param>
    /// <returns>The quotient of the vectors of the point and vector.</returns>
    public static Point operator /(Point l, IVector r)
    {
        return new(l.X / r.X, l.Y / r.Y);
    }

    /// <summary>
    /// Performs scalar division on the point.
    /// </summary>
    /// <param name="l">Point 1.</param>
    /// <param name="r">Division operand.</param>
    /// <returns>
    /// A new <see cref="Point" /> with vectors being the quotient of
    /// original vectors / <paramref name="r" />.
    /// </returns>
    public static Point operator /(Point l, double r)
    {
        return new(l.X / r, l.Y / r);
    }

    /// <summary>
    /// Performs modulus operation on points.
    /// </summary>
    /// <param name="l">Point 1.</param>
    /// <param name="r">Point 2.</param>
    /// <returns>The modulus of the vectors of the points.</returns>
    public static Point operator %(Point l, Point r)
    {
        return new(l.X % r.X, l.Y % r.Y);
    }

    /// <summary>
    /// Performs modulus operation on points.
    /// </summary>
    /// <param name="l">Point 1.</param>
    /// <param name="r">Vector operand.</param>
    /// <returns>The modulus of the vectors of the point and vector.</returns>
    public static Point operator %(Point l, IVector r)
    {
        return new(l.X % r.X, l.Y % r.Y);
    }

    /// <summary>
    /// Performs a modulus operation on the point.
    /// </summary>
    /// <param name="l">Point 1.</param>
    /// <param name="r">Modulus operand.</param>
    /// <returns>
    /// A new <see cref="Point" /> whose vectors are the modulus of the
    /// original vectors % <paramref name="r" />.
    /// </returns>
    public static Point operator %(Point l, double r)
    {
        return new(l.X % r, l.Y % r);
    }

    /// <summary>
    /// Increments the point's vectors by 1.
    /// </summary>
    /// <param name="p">Point to increment.</param>
    /// <returns>A point with its vectors incremented by 1.</returns>
    public static Point operator ++(Point p)
    {
        p.X++;
        p.Y++;
        return p;
    }

    /// <summary>
    /// Decrements the point's vectors by 1.
    /// </summary>
    /// <param name="p">Point to decrement.</param>
    /// <returns>A point with its vectors decremented by 1.</returns>
    public static Point operator --(Point p)
    {
        p.X--;
        p.Y--;
        return p;
    }

    /// <summary>
    /// Converts the point's vectors to positive values.
    /// </summary>
    /// <param name="p">Point to operate on.</param>
    /// <returns>A point with positive vectors.</returns>
    public static Point operator +(Point p)
    {
        return new(+p.X, +p.Y);
    }

    /// <summary>
    /// Inverts the sign of the point's vectors.
    /// </summary>
    /// <param name="p">Point to operate on.</param>
    /// <returns>A point with inverted vector signs.</returns>
    public static Point operator -(Point p)
    {
        return new(-p.X, -p.Y);
    }

    /// <summary>
    /// Compares the equality of the point vectors.
    /// </summary>
    /// <param name="l">Object to compare.</param>
    /// <param name="r">Object to compare against.</param>
    /// <returns>
    /// <see langword="true" /> if both instances are equal,
    /// <see langword="false" /> otherwise.
    /// </returns>
    public static bool operator ==(Point l, Point r)
    {
        return (l.X == r.X || !new[] { l.X, r.X }.AreValid()) && (l.Y == r.Y || !new[] { l.Y, r.Y }.AreValid());
    }

    /// <summary>
    /// Compares the equality of the point vectors with a 2D vector.
    /// </summary>
    /// <param name="l">Point to compare.</param>
    /// <param name="r">2D vector to compare against.</param>
    /// <returns>
    /// <see langword="true" /> if all vectors of both points are equal;
    /// otherwise, <see langword="false" />.
    /// </returns>
    public static bool operator ==(Point l, IVector r)
    {
        return (l.X == r.X || !new[] { l.X, r.X }.AreValid()) && (l.Y == r.Y || !new[] { l.Y, r.Y }.AreValid());
    }

    /// <summary>
    /// Compares the difference of the point vectors.
    /// </summary>
    /// <param name="l">Point 1.</param>
    /// <param name="r">Point 2.</param>
    /// <returns>
    /// <see langword="true" /> if the vectors of both points are different; 
    /// otherwise, <see langword="false" />.
    /// </returns>
    public static bool operator !=(Point l, Point r)
    {
        return (l.X != r.X || !new[] { l.X, r.X }.AreValid()) && (l.Y != r.Y || !new[] { l.Y, r.Y }.AreValid());
    }

    /// <summary>
    /// Compares the difference of the point vectors with a 2D vector.
    /// </summary>
    /// <param name="l">Point to compare.</param>
    /// <param name="r">2D vector to compare against.</param>
    /// <returns>
    /// <see langword="true" /> if the vectors of both points are different; 
    /// otherwise, <see langword="false" />.
    /// </returns>
    public static bool operator !=(Point l, IVector r)
    {
        return (l.X != r.X || !new[] { l.X, r.X }.AreValid()) && (l.Y != r.Y || !new[] { l.Y, r.Y }.AreValid());
    }

    /// <summary>
    /// X coordinate.
    /// </summary>
    public double X { get; set; } = x;

    /// <summary>
    /// Y coordinate.
    /// </summary>
    public double Y { get; set; } = y;

    /// <summary>
    /// Attempts to create a <see cref="Point"/> from a string.
    /// </summary>
    /// <param name="value">
    /// The value from which to create a <see cref="Point"/>.
    /// </param>
    /// <param name="point">
    /// The created <see cref="Point"/>.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the conversion was successful,
    /// <see langword="false"/> otherwise.
    /// </returns>
    public static bool TryParse(string value, out Point point)
    {
        switch (value)
        {
            case nameof(Nowhere):
            case "":
            case null:
                point = Nowhere;
                break;
            case nameof(Origin):
            case "0":
            case "+":
                point = Origin;
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
                    "-"
                ];
                return PrivateInternals.TryParseValues<double, Point>(new DoubleConverter(), separators, value.Without("()[]{}".ToCharArray()), 2, l => new Point(l[0], l[1]), out point);
        }
        return true;
    }

    /// <summary>
    /// Creates a <see cref="Point"/> from a string.
    /// </summary>
    /// <param name="value">
    /// The value from which to create a <see cref="Point"/>.
    /// </param>
    /// <exception cref="FormatException">
    /// Thrown if the conversion fails.
    /// </exception>
    /// <returns>The created <see cref="Point"/>.</returns>
    public static Point Parse(string value)
    {
        if (TryParse(value, out Point returnValue)) return returnValue;
        throw new FormatException();
    }

    /// <summary>
    /// Calculates the angle formed by the line connecting the origin and
    /// this <see cref="Point" /> with respect to the horizontal X axis.
    /// </summary>
    /// <returns>The calculated angle.</returns>
    public readonly double Angle()
    {
        double ang = Acos(X / Magnitude());
        if (Y < 0) ang = Tau - ang;
        return ang;
    }

    /// <summary>
    /// Compares the equality of the point vectors.
    /// </summary>
    /// <param name="other">
    /// The <see cref="Point" /> to compare against.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if all vectors of both points are equal; 
    /// otherwise, <see langword="false" />.
    /// </returns>
    public readonly bool Equals(Point other)
    {
        return this == other;
    }

    /// <summary>
    /// Determines if the point is within the rectangle formed by the
    /// specified points.
    /// </summary>
    /// <returns>
    /// <see langword="true" /> if the point is within the rectangle; 
    /// <see langword="false" /> otherwise.
    /// </returns>
    /// <param name="p1">Point 1.</param>
    /// <param name="p2">Point 2.</param>
    public readonly bool WithinBox(in Point p1, in Point p2)
    {
        return X.IsBetween(p1.X, p2.X) && Y.IsBetween(p1.Y, p2.Y);
    }

    /// <summary>
    /// Determines if the point is within the rectangle defined by the
    /// specified ranges.
    /// </summary>
    /// <returns>
    /// <see langword="true" /> if the point is within the rectangle; 
    /// <see langword="false" /> otherwise.
    /// </returns>
    /// <param name="x">Range of values for the X axis.</param>
    /// <param name="y">Range of values for the Y axis.</param>
    public readonly bool WithinBox(in Range<double> x, in Range<double> y)
    {
        return x.IsWithin(X) && y.IsWithin(Y);
    }

    /// <summary>
    /// Determines if the point is within the rectangle defined by the
    /// specified coordinates.
    /// </summary>
    /// <returns>
    /// <see langword="true" /> if the point is within the rectangle; 
    /// <see langword="false" /> otherwise.
    /// </returns>
    /// <param name="size">Size of the rectangle.</param>
    /// <param name="topLeft">Coordinates of the top-left corner.</param>
    public readonly bool WithinBox(in Size size, in Point topLeft)
    {
        return WithinBox(topLeft.X, topLeft.Y, topLeft.X + size.Width, topLeft.Y - size.Height);
    }

    /// <summary>
    /// Determines if the point is within the rectangle defined by the
    /// specified size.
    /// </summary>
    /// <returns>
    /// <see langword="true" /> if the point is within the rectangle; 
    /// <see langword="false" /> otherwise.
    /// </returns>
    /// <param name="size">Size of the rectangle.</param>
    public readonly bool WithinBox(in Size size)
    {
        return WithinBox(size, new Point(-(size.Width / 2), size.Height / 2));
    }

    /// <summary>
    /// Determines if the point is within the specified circle.
    /// </summary>
    /// <param name="center">Center point of the circle.</param>
    /// <param name="radius">Radius of the circle.</param>
    /// <returns>
    /// <see langword="true" /> if the point is within the circle; 
    /// <see langword="false" /> otherwise.
    /// </returns>
    public readonly bool WithinCircle(in Point center, in double radius)
    {
        return Magnitude(center) <= radius;
    }

    /// <summary>
    /// Determines if the point is within the rectangle defined by the
    /// specified coordinates.
    /// </summary>
    /// <returns>
    /// <see langword="true" /> if the point is within the rectangle; 
    /// <see langword="false" /> otherwise.
    /// </returns>
    /// <param name="x1">The first X coordinate.</param>
    /// <param name="y1">The first Y coordinate.</param>
    /// <param name="x2">The second X coordinate.</param>
    /// <param name="y2">The second Y coordinate.</param>
    public readonly bool WithinBox(in double x1, in double y1, in double x2, in double y2)
    {
        double minX, maxX, minY, maxY;
        if (x1 <= x2)
        {
            minX = x1;
            maxX = x2;
        }
        else
        {
            minX = x2;
            maxX = x1;
        }
        if (y1 <= y2)
        {
            minY = y1;
            maxY = y2;
        }
        else
        {
            minY = y2;
            maxY = y1;
        }
        return X.IsBetween(minX, maxX) && Y.IsBetween(minY, maxY);
    }

    /// <summary>
    /// Calculates the magnitude of the coordinates.
    /// </summary>
    /// <returns>
    /// The resulting magnitude between the point and the origin.
    /// </returns>
    public readonly double Magnitude()
    {
        return Sqrt((X * X) + (Y * Y));
    }

    /// <summary>
    /// Calculates the magnitude of the coordinates from the specified point.
    /// </summary>
    /// <returns>The resulting magnitude between both points.</returns>
    /// <param name="fromPoint">
    /// Reference point for calculating the magnitude.
    /// </param>
    public readonly double Magnitude(Point fromPoint)
    {
        double x = X - fromPoint.X, y = Y - fromPoint.Y;
        return Sqrt((x * x) + (y * y));
    }

    /// <summary>
    /// Calculates the magnitude of the coordinates from the specified
    /// coordinates.
    /// </summary>
    /// <returns>
    /// The resulting magnitude between the point and the specified
    /// coordinates.
    /// </returns>
    /// <param name="fromX">X coordinate of the origin.</param>
    /// <param name="fromY">Y coordinate of the origin.</param>
    public readonly double Magnitude(double fromX, double fromY)
    {
        double x = X - fromX, y = Y - fromY;
        return Sqrt((x * x) + (y * y));
    }

    /// <summary>
    /// Converts this object to its string representation.
    /// </summary>
    /// <param name="format">Format to use.</param>
    /// <param name="formatProvider">
    /// Optional parameter. Culture-specific format provider to use for
    /// formatting this object's string representation. If omitted,
    /// <see cref="CI.CurrentCulture" /> will be used.
    /// </param>
    /// <returns>
    /// A <see cref="string" /> representation of this object.
    /// </returns>
    public readonly string ToString(string? format, IFormatProvider? formatProvider)
    {
        if (format.IsEmpty()) format = "C";
        return format.ToUpperInvariant()[0] switch
        {
            'C' => $"{X}, {Y}",
            'B' => $"[{X}, {Y}]",
            'V' => $"X: {X}, Y: {Y}",
            'N' => $"X: {X}\nY: {Y}",
            _ => throw Errors.FormatNotSupported(format),
        };
    }

    /// <summary>
    /// Converts this object to its string representation.
    /// </summary>
    /// <param name="format">Format to use.</param>
    /// <returns>
    /// A <see cref="string" /> representation of this object.
    /// </returns>
    public readonly string ToString(string? format)
    {
        return ToString(format, CI.CurrentCulture);
    }

    /// <summary>
    /// Indicates whether this instance and a specified object are equal.
    /// </summary>
    /// <param name="obj">
    /// The object to compare with the current instance.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if this instance and
    /// <paramref name="obj" /> are equal; <see langword="false" />
    /// otherwise.
    /// </returns>
    public override readonly bool Equals(object? obj)
    {
        return obj is IVector p && this == p;
    }

    /// <summary>
    /// Returns the hash code for this instance.
    /// </summary>
    /// <returns>The hash code for this instance.</returns>
    public override readonly int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }

    /// <summary>
    /// Converts this object to its string representation.
    /// </summary>
    /// <returns>
    /// A <see cref="string" /> representation of this object.
    /// </returns>
    public override readonly string ToString()
    {
        return ToString(null);
    }

    /// <summary>
    /// Compares the equality of the vectors.
    /// </summary>
    /// <param name="other">
    /// The <see cref="IVector" /> to compare against.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if all vectors of both objects
    /// are equal; <see langword="false" /> otherwise.
    /// </returns>
    public readonly bool Equals(IVector? other)
    {
        return other is { } o && this == o;
    }

    /// <summary>
    /// Implicitly converts a <see cref="Point"/> to a <see cref="Vector2"/>.
    /// </summary>
    /// <param name="p"><see cref="Point"/> value to be converted.</param>
    public static implicit operator Vector2(Point p) => new((float)p.X, (float)p.Y);

    /// <summary>
    /// Implicitly converts a <see cref="Vector2"/> to a <see cref="Point"/>.
    /// </summary>
    /// <param name="p"><see cref="Vector2"/> value to be converted.</param>
    public static implicit operator Point(Vector2 p) => new(p.X, p.Y);
}
