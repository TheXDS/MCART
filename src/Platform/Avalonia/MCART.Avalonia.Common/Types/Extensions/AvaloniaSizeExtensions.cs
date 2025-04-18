﻿/*
AvaloniaSizeExtensions.cs

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

using A = Avalonia.Size;

namespace TheXDS.MCART.Types.Extensions;

/// <summary>
/// Extensiones de la clase <see cref="Size"/> para Avalonia.
/// </summary>
public static class AvaloniaSizeExtensions
{
    /// <summary>
    /// Convierte un <see cref="Size"/> en un
    /// <see cref="A"/>.
    /// </summary>
    /// <param name="size">
    /// <see cref="Size"/> a convertir.
    /// </param>
    /// <returns>
    /// Un nuevo <see cref="A"/> creado a partir del
    /// <see cref="Size"/> especificado.
    /// </returns>
    public static A ToSize(this Size size)
    {
        return new A(size.Width, size.Height);
    }

    /// <summary>
    /// Convierte un <see cref="A"/> en un
    /// <see cref="Size"/>.
    /// </summary>
    /// <param name="size">
    /// <see cref="A"/> a convertir.
    /// </param>
    /// <returns>
    /// Un nuevo <see cref="Size"/> creado a partir del
    /// <see cref="A"/> especificado.
    /// </returns>
    public static Size ToMcartSize(this A size)
    {
        return new Size(size.Width, size.Height);
    }
}
