﻿/*
ScColor.cs

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

using System.ComponentModel.DataAnnotations.Schema;
using TheXDS.MCART.Types.Base;

namespace TheXDS.MCART.Types.Entity;

/// <summary>
/// Universal structure that describes a color in its alpha,
/// red, green, and blue components.
/// </summary>
[ComplexType]
public class ScColor : IScColor, IEquatable<ScColor>
{
    /// <summary>
    /// Alpha component of the color.
    /// </summary>
    public float ScA { get; set; }

    /// <summary>
    /// Blue component of the color.
    /// </summary>
    public float ScB { get; set; }

    /// <summary>
    /// Green component of the color.
    /// </summary>
    public float ScG { get; set; }

    /// <summary>
    /// Red component of the color.
    /// </summary>
    public float ScR { get; set; }

    /// <summary>
    /// Implicitly converts a <see cref="Types.Color"/> to a
    /// <see cref="ScColor"/>.
    /// </summary>
    /// <param name="color">
    /// <see cref="Types.Color"/> to convert.
    /// </param>
    public static implicit operator ScColor(Types.Color color)
    {
        return new ScColor
        {
            ScA = color.ScA,
            ScB = color.ScB,
            ScG = color.ScG,
            ScR = color.ScR,
        };
    }

    /// <summary>
    /// Implicitly converts a <see cref="ScColor"/> to a
    /// <see cref="Types.Color"/>.
    /// </summary>
    /// <param name="color">
    /// <see cref="ScColor"/> to convert.
    /// </param>
    public static implicit operator Types.Color(ScColor color)
    {
        return new Types.Color(color.ScR, color.ScG, color.ScB, color.ScA);
    }

    /// <inheritdoc/>
    public bool Equals(ScColor? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return ScA == other.ScA && ScB == other.ScB && ScG == other.ScG && ScR == other.ScR;
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == this.GetType() && Equals((ScColor)obj);
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        return HashCode.Combine(ScA, ScB, ScG, ScR);
    }
}
