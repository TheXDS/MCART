/*
Point3D.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

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

using System.ComponentModel;
using System.Numerics;
using TheXDS.MCART.Helpers;
using TheXDS.MCART.Math;
using TheXDS.MCART.Misc;
using TheXDS.MCART.Resources;
using TheXDS.MCART.Types.Base;
using TheXDS.MCART.Types.Extensions;
using CI = System.Globalization.CultureInfo;

namespace TheXDS.MCART.Types;

/// <summary>
/// Universal type for a set of three-dimensional coordinates.
/// </summary>
/// <remarks>
/// This structure is declared as partial to allow each MCART
/// implementation to define methods for converting to the
/// corresponding class for different available UI types.
/// </remarks>
/// <param name="x">X coordinate.</param>
/// <param name="y">Y coordinate.</param>
/// <param name="z">Z coordinate.</param>
public partial struct Point3D(double x, double y, double z) : IFormattable, IEquatable<Point3D>, IVector3D
{
    /// <summary>
    /// Gets a point that represents no position. This field is read-only.
    /// </summary>
    /// <value>
    /// A <see cref="Point3D"/> with all coordinates set to
    /// <see cref="double.NaN"/>.
    /// </value>
    public static readonly Point3D Nowhere = new(double.NaN, double.NaN, double.NaN);

    /// <summary>
    /// Gets a point at the origin. This field is read-only.
    /// </summary>
    /// <value>
    /// A <see cref="Point3D"/> with coordinates at the origin.
    /// </value>
    public static readonly Point3D Origin = new(0, 0, 0);

    /// <summary>
    /// Gets a point at the 2D origin. This field is read-only.
    /// </summary>
    /// <value>
    /// A <see cref="Point3D"/> with coordinates at the 2D origin.
    /// </value>
    public static readonly Point3D Origin2D = new(0, 0, double.NaN);

    /// <summary>
    /// Initializes a new instance of the <see cref="Point3D"/> structure
    /// for a pair of two-dimensional coordinates.
    /// </summary>
    /// <param name="x">X coordinate.</param>
    /// <param name="y">Y coordinate.</param>
    public Point3D(double x, double y) : this(x, y, double.NaN)
    {
    }

    /// <summary>
    /// Performs addition operation on points.
    /// </summary>
    /// <param name="l">First point.</param>
    /// <param name="r">Second point.</param>
    /// <returns>The sum of the vectors from the points.</returns>
    public static Point3D operator +(Point3D l, Point3D r)
    {
        return new(l.X + r.X, l.Y + r.Y, l.Z + r.Z);
    }

    /// <summary>
    /// Performs addition operation on a point and a vector.
    /// </summary>
    /// <param name="l">Point.</param>
    /// <param name="r">Vector.</param>
    /// <returns>The sum of the point's vectors and the vector.</returns>
    public static Point3D operator +(Point3D l, IVector3D r)
    {
        return new(l.X + r.X, l.Y + r.Y, l.Z + r.Z);
    }

    /// <summary>
    /// Performs addition operation on a point and a scalar.
    /// </summary>
    /// <param name="l">Point.</param>
    /// <param name="r">Scalar value.</param>
    /// <returns>A new <see cref="Point3D"/> with each coordinate increased by r.</returns>
    public static Point3D operator +(Point3D l, double r)
    {
        return new(l.X + r, l.Y + r, l.Z + r);
    }

    /// <summary>
    /// Performs subtraction operation on a point and a vector.
    /// </summary>
    /// <param name="l">Point.</param>
    /// <param name="r">Vector.</param>
    /// <returns>The difference of the point's vectors and the vector.</returns>
    public static Point3D operator -(Point3D l, IVector3D r)
    {
        return new(l.X - r.X, l.Y - r.Y, l.Z - r.Z);
    }

    /// <summary>
    /// Performs subtraction operation on a point and a scalar.
    /// </summary>
    /// <param name="l">Point.</param>
    /// <param name="r">Scalar value.</param>
    /// <returns>
    /// A new <see cref="Point3D"/> with each coordinate decreased by r.
    /// </returns>
    public static Point3D operator -(Point3D l, double r)
    {
        return new(l.X - r, l.Y - r, l.Z - r);
    }

    /// <summary>
    /// Performs multiplication operation on a point and a vector.
    /// </summary>
    /// <param name="l">First point.</param>
    /// <param name="r">Second vector.</param>
    /// <returns>The product of the vectors from the point and vector.</returns>
    public static Point3D operator *(Point3D l, IVector3D r)
    {
        return new(l.X * r.X, l.Y * r.Y, l.Z * r.Z);
    }

    /// <summary>
    /// Performs multiplication operation on a point and a scalar.
    /// </summary>
    /// <param name="l">Point.</param>
    /// <param name="r">Scalar value.</param>
    /// <returns>
    /// A new <see cref="Point3D"/> with each coordinate multiplied by r.
    /// </returns>
    public static Point3D operator *(Point3D l, double r)
    {
        return new(l.X * r, l.Y * r, l.Z * r);
    }

    /// <summary>
    /// Performs division operation on a point and a vector.
    /// </summary>
    /// <param name="l">First point.</param>
    /// <param name="r">Second vector.</param>
    /// <returns>The quotient of the vectors from the point and vector.</returns>
    public static Point3D operator /(Point3D l, IVector3D r)
    {
        return new(l.X / r.X, l.Y / r.Y, l.Z / r.Z);
    }

    /// <summary>
    /// Performs division operation on a point and a scalar.
    /// </summary>
    /// <param name="l">Point.</param>
    /// <param name="r">Scalar value.</param>
    /// <returns>
    /// A new <see cref="Point3D"/> with each coordinate divided by r.
    /// </returns>
    public static Point3D operator /(Point3D l, double r)
    {
        return new(l.X / r, l.Y / r, l.Z / r);
    }

    /// <summary>
    /// Performs modulo operation on a point and a vector.
    /// </summary>
    /// <param name="l">First point.</param>
    /// <param name="r">Second vector.</param>
    /// <returns>The remainder of the vectors from the point and vector.</returns>
    public static Point3D operator %(Point3D l, IVector3D r)
    {
        return new(l.X % r.X, l.Y % r.Y, l.Z % r.Z);
    }

    /// <summary>
    /// Performs modulo operation on a point and a scalar.
    /// </summary>
    /// <param name="l">Point.</param>
    /// <param name="r">Scalar value.</param>
    /// <returns>
    /// A new <see cref="Point3D"/> with each coordinate modulo r.
    /// </returns>
    public static Point3D operator %(Point3D l, double r)
    {
        return new(l.X % r, l.Y % r, l.Z % r);
    }

    /// <summary>
    /// Increments all coordinates of the point by one.
    /// </summary>
    /// <param name="p">Point to increment.</param>
    /// <returns>A point with all coordinates incremented by one.</returns>
    public static Point3D operator ++(Point3D p)
    {
        p.X++;
        p.Y++;
        p.Z++;
        return p;
    }

    /// <summary>
    /// Decrements all coordinates of the point by one.
    /// </summary>
    /// <param name="p">Point to decrement.</param>
    /// <returns>A point with all coordinates decremented by one.</returns>
    public static Point3D operator --(Point3D p)
    {
        p.X--;
        p.Y--;
        p.Z--;
        return p;
    }

    /// <summary>
    /// Converts all coordinates of the point to positive values.
    /// </summary>
    /// <param name="p">Point to operate on.</param>
    /// <returns>A point with all coordinates positive.</returns>
    public static Point3D operator +(Point3D p)
    {
        return new(+p.X, +p.Y, +p.Z);
    }

    /// <summary>
    /// Inverts the sign of all coordinates of the point.
    /// </summary>
    /// <param name="p">Point to operate on.</param>
    /// <returns>A point with inverted signs of all coordinates.</returns>
    public static Point3D operator -(Point3D p)
    {
        return new(-p.X, -p.Y, -p.Z);
    }

    /// <summary>
    /// Compares the equality of vectors from both points.
    /// </summary>
    /// <param name="l">First point.</param>
    /// <param name="r">Second vector.</param>
    /// <returns>
    /// <see langword="true" /> if all coordinates from both points are equal;
    /// otherwise, <see langword="false" />.
    /// </returns>
    public static bool operator ==(Point3D l, IVector3D r)
    {
        return l.X.Equals(r.X) && l.Y.Equals(r.Y) && l.Z.Equals(r.Z);
    }

    /// <summary>
    /// Compares the inequality of vectors from both points.
    /// </summary>
    /// <param name="l">First point.</param>
    /// <param name="r">Second vector.</param>
    /// <returns>
    /// <see langword="true" /> if coordinates from both points are different;
    /// otherwise, <see langword="false" />.
    /// </returns>
    public static bool operator !=(Point3D l, IVector3D r)
    {
        return !(l == r);
    }

    /// <summary>
    /// Implicitly converts a <see cref="Point3D"/> to a
    /// <see cref="Point"/>.
    /// </summary>
    /// <param name="p">Object to convert.</param>
    /// <returns>
    /// A new <see cref="Point"/> with the same values of
    /// <see cref="X"/> and <see cref="Y"/> as the original
    /// <see cref="Point3D"/>.
    /// </returns>
    public static implicit operator Point(Point3D p) => new(p.X, p.Y);

    /// <summary>
    /// Implicitly converts a <see cref="Point"/> to a
    /// <see cref="Point3D"/>.
    /// </summary>
    /// <param name="p">Object to convert.</param>
    /// <returns>
    /// A new <see cref="Point3D"/> with the same values of
    /// <see cref="X"/> and <see cref="Y"/> as the original
    /// <see cref="Point"/>, and a <see cref="Z"/> value of <see cref="double.NaN"/>.
    /// </returns>
    public static implicit operator Point3D(Point p) => new(p.X, p.Y, double.NaN);

    /// <summary>
    /// X coordinate.
    /// </summary>
    public double X { get; set; } = x;

    /// <summary>
    /// Y coordinate.
    /// </summary>
    public double Y { get; set; } = y;

    /// <summary>
    /// Z coordinate.
    /// </summary>
    public double Z { get; set; } = z;

    /// <summary>
    /// Attempts to create a <see cref="Point3D"/> from a string.
    /// </summary>
    /// <param name="value">
    /// Value from which to create a <see cref="Point3D"/>.
    /// </param>
    /// <param name="point">
    /// <see cref="Point3D"/> that has been created.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the conversion was successful,
    /// <see langword="false"/> otherwise.
    /// </returns>
    public static bool TryParse(string value, out Point3D point)
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
            case nameof(Origin2D):
                point = Origin2D;
                break;
            default:
                string[] separators =
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
                return PrivateInternals.TryParseValues<double, Point3D>(
                    new DoubleConverter(),
                    separators,
                    value.Without("()[]{}".ToCharArray()),
                    3,
                    l => new Point3D(l[0], l[1], l[2]),
                    out point);
        }
        return true;
    }

    /// <summary>
    /// Creates a <see cref="Point3D"/> from a string.
    /// </summary>
    /// <param name="value">
    /// Value from which to create a <see cref="Point"/>.
    /// </param>
    /// <exception cref="FormatException">
    /// Occurs if the conversion fails.
    /// </exception>
    /// <returns><see cref="Point3D"/> that has been created.</returns>
    public static Point3D Parse(string value)
    {
        if (TryParse(value, out Point3D retVal)) return retVal;
        throw new FormatException();
    }

    /// <summary>
    /// Compares the equality of vectors from both points.
    /// </summary>
    /// <param name="other">
    /// <see cref="Point3D" /> to compare against.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if all coordinates from both points are equal;
    /// otherwise, <see langword="false" />.
    /// </returns>
    public readonly bool Equals(Point3D other)
    {
        return this == other;
    }

    /// <summary>
    /// Determines whether the point is within the cube formed by the specified
    /// 3D points.
    /// </summary>
    /// <returns>
    /// <see langword="true" /> if the point is within the cube formed,
    /// <see langword="false" /> otherwise.
    /// </returns>
    /// <param name="p1">First point.</param>
    /// <param name="p2">Second point.</param>
    public readonly bool WithinCube(Point3D p1, Point3D p2)
    {
        return X.IsBetween(p1.X, p2.X) && Y.IsBetween(p1.Y, p2.Y) && Z.IsBetween(p1.Z, p2.Z);
    }

    /// <summary>
    /// Determines whether the point is within the cube formed by the specified
    /// 3D points.
    /// </summary>
    /// <returns>
    /// <see langword="true" /> if the point is within the cube formed,
    /// <see langword="false" /> otherwise.
    /// </returns>
    /// <param name="x1">The first X coordinate.</param>
    /// <param name="y1">The first Y coordinate.</param>
    /// <param name="z1">The first Z coordinate.</param>
    /// <param name="x2">The second X coordinate.</param>
    /// <param name="y2">The second Y coordinate.</param>
    /// <param name="z2">The second Z coordinate.</param>
    public readonly bool WithinCube(in double x1, in double y1, in double z1, in double x2, in double y2, in double z2)
    {
        double[] x = [.. new[] { x1, x2 }.Ordered()];
        double[] y = [.. new[] { y1, y2 }.Ordered()];
        double[] z = [.. new[] { z1, z2 }.Ordered()];
        return X.IsBetween(x[0], x[1]) && Y.IsBetween(y[0], y[1]) && Z.IsBetween(z[0], z[1]);
    }

    /// <summary>
    /// Determines whether the point is within the rectangle formed by the
    /// specified ranges.
    /// </summary>
    /// <returns>
    /// <see langword="true" /> if the point is within the rectangle
    /// formed, <see langword="false" /> otherwise.
    /// </returns>
    /// <param name="x">Range of values for the X axis.</param>
    /// <param name="y">Range of values for the Y axis.</param>
    /// <param name="z">Range of values for the Z axis.</param>
    public readonly bool WithinCube(Range<double> x, Range<double> y, Range<double> z)
    {
        return x.IsWithin(X) && y.IsWithin(Y) && z.IsWithin(Z);
    }

    /// <summary>
    /// Determines whether the point is within the rectangle formed by the
    /// specified coordinates.
    /// </summary>
    /// <returns>
    /// <see langword="true" /> if the point is within the rectangle
    /// formed, <see langword="false" /> otherwise.
    /// </returns>
    /// <param name="size">Size of the rectangle.</param>
    /// <param name="topLeftFront">Coordinates of the top-left front corner</param>
    public readonly bool WithinCube(in Size3D size, in Point3D topLeftFront)
    {
        double[] x = [.. new[] { topLeftFront.X, topLeftFront.X + size.Width }.Ordered()];
        double[] y = [.. new[] { topLeftFront.Y, topLeftFront.Y - size.Height }.Ordered()];
        double[] z = [.. new[] { topLeftFront.Z, topLeftFront.Z - size.Depth }.Ordered()];
        return WithinCube(x[0], y[0], z[0], x[1], y[1], z[1]);
    }

    /// <summary>
    /// Determines whether the point is within the rectangle formed by the
    /// specified coordinates.
    /// </summary>
    /// <returns>
    /// <see langword="true" /> if the point is within the rectangle
    /// formed, <see langword="false" /> otherwise.
    /// </returns>
    /// <param name="size">Size of the rectangle.</param>
    public readonly bool WithinCube(in Size3D size)
    {
        return WithinCube(size, new Point3D(-(size.Width / 2), size.Height / 2, size.Depth / 2));
    }

    /// <summary>
    /// Determines whether the point is within the specified sphere.
    /// </summary>
    /// <param name="center">Center point of the sphere.</param>
    /// <param name="radius">Radius of the sphere.</param>
    /// <returns>
    /// <see langword="true" /> if the point is within the sphere,
    /// <see langword="false" /> otherwise.
    /// </returns>
    public readonly bool WithinSphere(Point3D center, double radius)
    {
        return Magnitude(center) <= radius;
    }

    /// <summary>
    /// Calculates the magnitude of the coordinates.
    /// </summary>
    /// <returns>
    /// The resulting magnitude between the point and the origin.
    /// </returns>
    public readonly double Magnitude()
    {
        return System.Math.Sqrt((X * X) + (Y * Y) + (Z * Z));
    }

    /// <summary>
    /// Calculates the magnitude of the coordinates from the specified point.
    /// </summary>
    /// <returns>The resulting magnitude between both points.</returns>
    /// <param name="fromPoint">
    /// Reference point to calculate the
    /// magnitude.
    /// </param>
    public readonly double Magnitude(Point3D fromPoint)
    {
        double x = X - fromPoint.X, y = Y - fromPoint.Y, z = Z - fromPoint.Z;
        return System.Math.Sqrt((x * x) + (y * y) + (z * z));
    }

    /// <summary>
    /// Calculates the magnitude of the coordinates from the specified point.
    /// </summary>
    /// <returns>
    /// The resulting magnitude between the point and the specified coordinates.
    /// </returns>
    /// <param name="fromX">Source X coordinate.</param>
    /// <param name="fromY">Source Y coordinate.</param>
    /// <param name="fromZ">Source Z coordinate.</param>
    public readonly double Magnitude(double fromX, double fromY, double fromZ)
    {
        double x = X - fromX, y = Y - fromY, z = Z - fromZ;
        return System.Math.Sqrt((x * x) + (y * y) + (z * z));
    }

    /// <summary>
    /// Indicates whether this instance and a specified object are equal.
    /// </summary>
    /// <param name="obj">
    /// The object to compare with the current instance.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if this instance and <paramref name="obj" /> are equal;
    /// otherwise, <see langword="false" />.
    /// </returns>
    public override readonly bool Equals(object? obj)
    {
        return obj is Point3D p && this == p;
    }

    /// <summary>
    /// Returns the hash code for this instance.
    /// </summary>
    /// <returns>The hash code for this instance.</returns>
    public override readonly int GetHashCode()
    {
        return HashCode.Combine(X, Y, Z);
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
    /// Converts this object to its string representation.
    /// </summary>
    /// <param name="format">Format to use.</param>
    /// <param name="formatProvider">
    /// Optional parameter.
    /// Format provider of the culture to use for formatting
    /// the string representation of this object. If omitted,
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
            'C' => $"{X}, {Y}, {Z}",
            'B' => $"[{X}, {Y}, {Z}]",
            'V' => $"X: {X}, Y: {Y}, Z: {Z}",
            'N' => $"X: {X}{Environment.NewLine}Y: {Y}{Environment.NewLine}Z: {Z}",
            _ => throw Errors.FormatNotSupported(format),
        };
    }

    /// <summary>
    /// Indicates whether this instance and a specified object are equal.
    /// </summary>
    /// <param name="other">
    /// The object to compare with the current instance.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if this instance and <paramref name="other" /> are equal;
    /// otherwise, <see langword="false" />.
    /// </returns>
    public readonly bool Equals(IVector? other) => other is not null && X.Equals(other.X) && Y.Equals(other.Y);

    /// <summary>
    /// Indicates whether this instance and a specified object are equal.
    /// </summary>
    /// <param name="other">
    /// The object to compare with the current instance.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if this instance and <paramref name="other" /> are equal;
    /// otherwise, <see langword="false" />.
    /// </returns>
    public readonly bool Equals(IVector3D? other) => other is not null && X.Equals(other.X) && Y.Equals(other.Y) && Z.Equals(other.Z);

    /// <summary>
    /// Implicitly converts a <see cref="Point3D"/> to a <see cref="Vector3"/>.
    /// </summary>
    /// <param name="p"><see cref="Point3D"/> value to be converted.</param>
    public static implicit operator Vector3(Point3D p) => new((float)p.X, (float)p.Y, (float)p.Z);

    /// <summary>
    /// Implicitly converts a <see cref="Vector3"/> to a <see cref="Point3D"/>.
    /// </summary>
    /// <param name="p"><see cref="Vector3"/> value to be converted.</param>
    public static implicit operator Point3D(Vector3 p) => new(p.X, p.Y, p.Z);
}
