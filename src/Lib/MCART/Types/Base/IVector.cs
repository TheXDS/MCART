﻿/*
IVector.cs

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

namespace TheXDS.MCART.Types.Base;

/// <summary>
/// Interfaz que define propiedades comunes para estructuras de datos
/// que describen coordenadas, vectores, magnitudes y tamaños en un
/// espacio de dos dimensiones.
/// </summary>
public interface IVector : IEquatable<IVector>
{
    /// <summary>
    /// Obtiene el componente horizontal (eje X) representado por este
    /// <see cref="IVector"/>.
    /// </summary>
    double X { get; }

    /// <summary>
    /// Obtiene el componente vertical (eje Y) representado por este
    /// <see cref="IVector"/>.
    /// </summary>
    double Y { get; }

    /// <summary>
    /// Converts the current <see cref="IVector"/> to a <see cref="Vector2"/>.
    /// </summary>
    /// <returns>
    /// A new <see cref="Vector2"/> instance with the same X and Y values as this <see cref="IVector"/>.
    /// </returns>
    Vector2 ToVector2()
    {
        return new Vector2((float)X, (float)Y);
    }
}
