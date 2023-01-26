/*
Color.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2023 César Andrés Morgan

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

using System;
using System.ComponentModel.DataAnnotations.Schema;
using TheXDS.MCART.Types.Base;

namespace TheXDS.MCART.Types.Entity;

/// <summary>
/// Estructura universal que describe un color en sus componentes alfa,
/// rojo, verde y azul.
/// </summary>
[ComplexType]
public class Color : IColor, IEquatable<Color>
{
    /// <summary>
    /// Componente Alfa del color.
    /// </summary>
    public byte A { get; set; }

    /// <summary>
    ///  Componente Azul del color.
    /// </summary>
    public byte B { get; set; }

    /// <summary>
    ///  Componente Verde del color.
    /// </summary>
    public byte G { get; set; }

    /// <summary>
    ///  Componente Rojo del color.
    /// </summary>
    public byte R { get; set; }

    /// <summary>
    /// Convierte implícitamente un <see cref="Types.Color"/> en un
    /// <see cref="Color"/>.
    /// </summary>
    /// <param name="color">
    /// <see cref="Types.Color"/> a convertir.
    /// </param>
    public static implicit operator Color(Types.Color color)
    {
        return new Color
        {
            A = color.A,
            B = color.B,
            G = color.G,
            R = color.R,
        };
    }

    /// <summary>
    /// Convierte implícitamente un <see cref="Color"/> en un
    /// <see cref="Types.Color"/>.
    /// </summary>
    /// <param name="color">
    /// <see cref="Color"/> a convertir.
    /// </param>
    public static implicit operator Types.Color(Color color)
    {
        return new Types.Color(color.R, color.G, color.B, color.A);
    }

    /// <inheritdoc/>
    public bool Equals(Color? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return A == other.A && B == other.B && G == other.G && R == other.R;
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == this.GetType() && Equals((Color)obj);
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        return HashCode.Combine(A, B, G, R);
    }
}
