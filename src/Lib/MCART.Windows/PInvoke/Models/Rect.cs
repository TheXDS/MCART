/*
Rect.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
    PInvoke.net Community <http://www.pinvoke.net>
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

using System.Runtime.InteropServices;

namespace TheXDS.MCART.PInvoke.Models;

[StructLayout(LayoutKind.Sequential)]
internal struct Rect
{
    public int Left, Top, Right, Bottom;

    public Rect(int left, int top, int right, int bottom)
    {
        Left = left;
        Top = top;
        Right = right;
        Bottom = bottom;
    }

    public Rect(System.Drawing.Rectangle r) : this(r.Left, r.Top, r.Right, r.Bottom) { }

    public int X
    {
        get => Left;
        set { Right -= Left - value; Left = value; }
    }

    public int Y
    {
        get => Top;
        set { Bottom -= Top - value; Top = value; }
    }

    public int Height
    {
        get => Bottom - Top;
        set => Bottom = value + Top;
    }

    public int Width
    {
        get => Right - Left;
        set => Right = value + Left;
    }

    public System.Drawing.Point Location
    {
        get => new(Left, Top);
        set { X = value.X; Y = value.Y; }
    }

    public System.Drawing.Size Size
    {
        get => new(Width, Height);
        set { Width = value.Width; Height = value.Height; }
    }

    public static implicit operator System.Drawing.Rectangle(Rect r)
    {
        return new System.Drawing.Rectangle(r.Left, r.Top, r.Width, r.Height);
    }

    public static implicit operator Rect(System.Drawing.Rectangle r)
    {
        return new Rect(r);
    }

    public static bool operator ==(Rect r1, Rect r2)
    {
        return r1.Equals(r2);
    }

    public static bool operator !=(Rect r1, Rect r2)
    {
        return !r1.Equals(r2);
    }

    public bool Equals(Rect r)
    {
        return r.Left == Left && r.Top == Top && r.Right == Right && r.Bottom == Bottom;
    }

    public override bool Equals(object? obj)
    {
        return obj switch
        {
            Rect rect => Equals(rect),
            System.Drawing.Rectangle rectangle => Equals(new Rect(rectangle)),
            _ => false,
        };
    }

    public override int GetHashCode()
    {
        return ((System.Drawing.Rectangle)this).GetHashCode();
    }

    public override string ToString()
    {
        return $"{{Left={Left}, Top={Top}, Right={Right}, Bottom={Bottom}}}";
    }
}
