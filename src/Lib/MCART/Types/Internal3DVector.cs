﻿/*
Internal3DVector.cs

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

using System.Numerics;
using TheXDS.MCART.Types.Base;

namespace TheXDS.MCART.Types;

internal struct Internal3DVector : IVector3D
{
    public double X { get; set; }
    public double Y { get; set; }
    public double Z { get; set; }
    public readonly bool Equals(IVector3D? other) => X.Equals(other?.X) && Y.Equals(other.Y) && Z.Equals(other.Z);
    public readonly bool Equals(IVector? other) => X.Equals(other?.X) && Y.Equals(other.Y);
    public readonly override bool Equals(object? obj)
    {
        return obj switch
        {
            IVector3D v3d => Equals(v3d),
            IVector v2d => Equals(v2d),
            Vector3 v3 => ((IEquatable<Vector3>)this).Equals(v3),
            Vector2 v2 => ((IEquatable<Vector2>)this).Equals(v2),
            _ => false
        };
    }

    public readonly override int GetHashCode() => HashCode.Combine(X, Y, Z);
}
