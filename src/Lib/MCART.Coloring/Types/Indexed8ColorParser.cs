/*
Rgb233ColorParser.cs

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

using TheXDS.MCART.Helpers;
using TheXDS.MCART.Types.Base;

namespace TheXDS.MCART.Types;

/// <summary>
/// Implementa un <see cref="IColorParser{T}" /> que incluye información de una
/// paleta de colores.
/// </summary>
public class Indexed8ColorParser : IColorParser<byte>
{
    private readonly Color[] palette;

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="Indexed8ColorParser"/>.
    /// </summary>
    /// <param name="palette">
    /// Paleta de colores a asociar con este <see cref="IColorParser{T}"/>.
    /// Debe tener no más de 256 elementos.
    /// </param>
    public Indexed8ColorParser(Color[] palette)
    {
        if (palette.Length > 256) throw new IndexOutOfRangeException();
        this.palette = palette;
    }

    /// <inheritdoc/>
    public Color From(byte value)
    {
        return palette[value];
    }

    /// <inheritdoc/>
    public byte To(Color color)
    {
        return (byte)palette.WithIndex().OrderByDescending(p => Color.Similarity(p.element, color)).FirstOrDefault().index;
    }
}
