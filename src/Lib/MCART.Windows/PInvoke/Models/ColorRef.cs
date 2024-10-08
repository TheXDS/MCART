// COLORREF.cs
//
// This file is part of Morgan's CLR Advanced Runtime (MCART)
//
// Author(s):
//      César Andrés Morgan <xds_xps_ivx@hotmail.com>
//
// Released under the MIT License (MIT)
// Copyright © 2011 - 2024 César Andrés Morgan
//
// Permission is hereby granted, free of charge, to any person obtaining a copy of
// this software and associated documentation files (the “Software”), to deal in
// the Software without restriction, including without limitation the rights to
// use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
// of the Software, and to permit persons to whom the Software is furnished to do
// so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using System.Runtime.InteropServices;

namespace TheXDS.MCART.PInvoke.Models;

[StructLayout(LayoutKind.Explicit, Size = 4)]
internal struct ColorRef
{
    [FieldOffset(0)] private uint Value;

    [FieldOffset(0)] public byte R;

    [FieldOffset(1)] public byte G;

    [FieldOffset(2)] public byte B;

    [FieldOffset(3)] public byte A;

    /// <summary>Initializes a new instance of the <see cref="ColorRef"/> struct.</summary>
    /// <param name="r">The intensity of the red color.</param>
    /// <param name="g">The intensity of the green color.</param>
    /// <param name="b">The intensity of the blue color.</param>
    public ColorRef(byte r, byte g, byte b)
    {
        Value = 0;
        A = 0;
        R = r;
        G = g;
        B = b;
    }

    /// <summary>A method to darken a color by a percentage of the difference between the color and Black.</summary>
    /// <param name="percent">The percentage by which to darken the original color.</param>
    /// <returns>
    /// The return color's Alpha value will be unchanged, but the RGB content will have been increased by the specified percentage. If
    /// percent is 100 then the returned Color will be Black with original Alpha.
    /// </returns>
    public ColorRef Darken(float percent)
    {
        if (percent < 0 || percent > 1.0)
            throw new ArgumentOutOfRangeException(nameof(percent));

        return new(Conv(R), Conv(G), Conv(B)) { Value = Value };

        byte Conv(byte c) => (byte)(c - (int)(c * percent));
    }

    /// <summary>A method to lighten a color by a percentage of the difference between the color and Black.</summary>
    /// <param name="percent">The percentage by which to lighten the original color.</param>
    /// <returns>
    /// The return color's Alpha value will be unchanged, but the RGB content will have been decreased by the specified percentage. If
    /// percent is 100 then the returned Color will be White with original Alpha.
    /// </returns>
    public ColorRef Lighten(float percent)
    {
        if (percent < 0 || percent > 1.0)
            throw new ArgumentOutOfRangeException(nameof(percent));

        return new(Conv(R), Conv(G), Conv(B)) { Value = Value };

        byte Conv(byte c) => (byte)(c + (int)((255f - c) * percent));
    }

    public static implicit operator Types.Color(ColorRef value) => new(value.R, value.G, value.B, value.A);

    public static implicit operator ColorRef(Types.Color value) => new(value.R, value.G, value.B) { A = value.A };
}
