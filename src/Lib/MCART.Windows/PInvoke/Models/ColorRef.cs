// ColorRef.cs
//
// This file is part of Morgan's CLR Advanced Runtime (MCART)
//
// Author(s):
//      César Andrés Morgan <xds_xps_ivx@hotmail.com>
//
// Released under the MIT License (MIT)
// Copyright © 2011 - 2025 César Andrés Morgan
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
internal struct ColorRef(byte r, byte g, byte b, byte a)
{
    [FieldOffset(0)] private uint Value;

    [FieldOffset(0)] public byte R = r;

    [FieldOffset(1)] public byte G = g;

    [FieldOffset(2)] public byte B = b;

    [FieldOffset(3)] public byte A = a;

    public ColorRef(byte r, byte g, byte b) : this(r, g, b, 0)
    {
    }

    public ColorRef Darken(float percent)
    {
        if (percent < 0 || percent > 1.0)
            throw new ArgumentOutOfRangeException(nameof(percent));

        return new(Conv(R), Conv(G), Conv(B)) { Value = Value };

        byte Conv(byte c) => (byte)(c - (int)(c * percent));
    }

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
