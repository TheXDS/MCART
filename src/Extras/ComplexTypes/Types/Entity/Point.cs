/*
Point.cs

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
using TheXDS.MCART.Math;
using TheXDS.MCART.Resources;
using TheXDS.MCART.Types.Base;
using static TheXDS.MCART.Types.Extensions.StringExtensions;
using CI = System.Globalization.CultureInfo;

namespace TheXDS.MCART.Types.Entity;

/// <summary>
/// Universal type that describes a set of coordinated in a two dimensional
/// plane.
/// </summary>
[ComplexType]
public class Point : IVector, IFormattable, IEquatable<Point>
{
    /// <summary>
    /// Gets or sets the X coordinates.
    /// </summary>
    public double X { get; set; }

    /// <summary>
    /// Gets or sets the Y coordinates.
    /// </summary>
    public double Y { get; set; }

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="Point" /> class.
    /// </summary>
    public Point()
    {
    }

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="Point" /> class.
    /// </summary>
    /// <param name="x">The x coordinate.</param>
    /// <param name="y">The y coordinate.</param>
    public Point(double x, double y)
    {
        X = x;
        Y = y;
    }

    /// <summary>
    /// Compares the equality between two vectors.
    /// </summary>
    /// <param name="other">
    /// <see cref="IVector" /> to compare this instance against.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if the vectors in both this and the other
    /// vector are equal, <see langword="false" /> otherwise.
    /// </returns>
    public bool Equals(IVector? other)
    {
        return other is { } o && this == o;
    }

    /// <summary>
    /// Compares the equality of two points.
    /// </summary>
    /// <param name="other">
    /// <see cref="Point" /> to compare this intance against.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if the vectors in both this and the other point
    /// are equal, <see langword="false" /> otherwise.
    /// </returns>
    public bool Equals(Point? other)
    {
        return other is { } o && this == o;
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        return obj is IVector p && this == p;
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        return ToString(null);
    }

    /// <summary>
    /// Converts this object to its string representation.
    /// </summary>
    /// <param name="format">Format to be used.</param>
    /// <param name="formatProvider">
    /// Format provider to use to get the culture to be used during the
    /// conversion. If ommitted, <see cref="CI.CurrentCulture" /> will be used.
    /// </param>
    /// <returns>
    /// A string representation of the current object.
    /// </returns>
    public string ToString(string? format, IFormatProvider? formatProvider)
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
    /// <param name="format">Format to be used.</param>
    /// <returns>
    /// A string representation of the current object.
    /// </returns>
    public string ToString(string? format)
    {
        return ToString(format, CI.CurrentCulture);
    }

    /// <summary>
    /// Gets a hash code that uniquely identifies this instance.
    /// </summary>
    /// <returns>A hash code that uniquely identifies this instance.</returns>
    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }

    /// <summary>
    /// Compares the equality of two points.
    /// </summary>
    /// <param name="l">First point.</param>
    /// <param name="r">Second point.</param>
    /// <returns>
    /// <see langword="true" /> if all the coordinates in both points are
    /// equal, <see langword="false" /> otherwise.
    /// </returns>
    public static bool operator ==(Point l, Point r)
    {
        return CompareValues(l.X, r.X) && CompareValues(l.Y, r.Y);
    }

    /// <summary>
    /// Compares the equality of two vectors.
    /// </summary>
    /// <param name="l">Point to compare.</param>
    /// <param name="r">Two-dimensional vector to compare against.</param>
    /// <returns>
    /// <see langword="true" /> if all the coordinates in both objects are
    /// equal, <see langword="false" /> otherwise.
    /// </returns>
    public static bool operator ==(Point l, IVector r)
    {
        return CompareValues(l.X, r.X) && CompareValues(l.Y, r.Y);
    }

    /// <summary>
    /// Compares the inequality of two points.
    /// </summary>
    /// <param name="l">First point.</param>
    /// <param name="r">Second point.</param>
    /// <returns>
    /// <see langword="true" /> if any of the coordinates in the points are not
    /// equal between them, <see langword="false" /> otherwise.
    /// </returns>
    public static bool operator !=(Point l, Point r)
    {
        return !(l == r);
    }

    /// <summary>
    /// Compares the inequality of two vectors.
    /// </summary>
    /// <param name="l">Point to compare.</param>
    /// <param name="r">Two-dimensional vector to compare against.</param>
    /// <returns>
    /// <see langword="true" /> if any of the coordinates in the objects are
    /// not equal between them, <see langword="false" /> otherwise.
    /// </returns>
    public static bool operator !=(Point l, IVector r)
    {
        return !(l == r);
    }

    /// <summary>
    /// Implicitly converts a <see cref="Point"/> into a
    /// <see cref="Types.Point"/>.
    /// </summary>
    /// <param name="p">
    /// <see cref="Point"/> to be converted.
    /// </param>
    public static implicit operator Types.Point(Point p)
    {
        return new Types.Point(p.X, p.Y);
    }

    /// <summary>
    /// Implicitly converts <see cref="Types.Point"/> into a
    /// <see cref="Point"/>.
    /// </summary>
    /// <param name="p">
    /// <see cref="Types.Point"/> to be converted.
    /// </param>
    public static implicit operator Point(Types.Point p)
    {
        return new Point(p.X, p.Y);
    }

    private static bool CompareValues(double x,  double y)
    {
        return (Algebra.AreValid(x, y) && x == y) || (!x.IsValid() && !y.IsValid());
    }
}
