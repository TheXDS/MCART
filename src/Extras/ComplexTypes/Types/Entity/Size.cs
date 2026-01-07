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

using System.ComponentModel.DataAnnotations.Schema;
using TheXDS.MCART.Types.Base;

namespace TheXDS.MCART.Types.Entity;

/// <summary>
/// Universal class that describes the size of an object in width and
/// height in a two-dimensional space.
/// </summary>
[ComplexType]
public class Size : IEquatable<Size>, ISize
{
    /// <summary>
    /// Gets the height component of the size.
    /// </summary>
    public double Height { get; set; }

    /// <summary>
    /// Gets the width component of the size.
    /// </summary>
    public double Width { get; set; }

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
    public bool Equals(Size? other) => other is { } o && this == o;

    /// <summary>
    /// Returns the hash code generated for this instance.
    /// </summary>
    /// <returns>
    /// A hash code that represents this instance.
    /// </returns>
    public override int GetHashCode()
    {
        return HashCode.Combine(Height, Width);
    }

    /// <summary>
    /// Compares the equality between two instances of <see cref="Size"/>.
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
        return (size1.Height == size2.Height && size1.Width == size2.Width);
    }

    /// <summary>
    /// Compares the inequality between two instances of 
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
    /// Initializes a new instance of the <see cref="Size"/> class.
    /// </summary>
    public Size()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Size"/> class.
    /// </summary>
    /// <param name="width">Width value.</param>
    /// <param name="height">Height value.</param>
    public Size(double width, double height)
    {
        Width = width;
        Height = height;
    }

    /// <summary>
    /// Indicates whether this instance and a specified object are equal.
    /// </summary>
    /// <param name="obj">
    /// The object to compare with the current instance.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if this instance and
    /// <paramref name="obj"/> are equal; otherwise,
    /// <see langword="false"/>.
    /// </returns>
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj))
            return true;

        return obj is Size s && this == s;
    }

    /// <summary>
    /// Implicitly converts a <see cref="Size"/> to a
    /// <see cref="Types.Size"/>.
    /// </summary>
    /// <param name="p">
    /// <see cref="Size"/> to convert.
    /// </param>
    public static implicit operator Types.Size(Size p)
    {
        return new Types.Size(p.Width, p.Height);
    }

    /// <summary>
    /// Implicitly converts a <see cref="Types.Size"/> to a
    /// <see cref="Size"/>.
    /// </summary>
    /// <param name="p">
    /// <see cref="Types.Size"/> to convert.
    /// </param>
    public static implicit operator Size(Types.Size p)
    {
        return new Size(p.Width, p.Height);
    }
}
