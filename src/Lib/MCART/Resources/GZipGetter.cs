/*
GZipGetter.cs

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

using System.IO.Compression;
using TheXDS.MCART.Attributes;

namespace TheXDS.MCART.Resources;

/// <summary>
/// <see cref="ICompressorGetter"/> que construye un
/// <see cref="GZipStream"/> que puede ser utilizado para extraer
/// información comprimida desde un <see cref="Stream"/>.
/// </summary>
[Tag("gzip")]
[Tag("gz")]
public sealed class GZipGetter : ICompressorGetter
{
    /// <summary>
    /// Obtiene un <see cref="GZipStream"/> para extraer información
    /// comprimida desde <paramref name="inputStream"/>.
    /// </summary>
    /// <param name="inputStream">
    /// <see cref="Stream"/> que contiene la información a extraer.
    /// </param>
    /// <returns>
    /// Un <see cref="GZipStream"/> que puede utilizarse para extraer
    /// información comprimida desde <paramref name="inputStream"/>.
    /// </returns>
    public Stream GetCompressor(Stream inputStream) => new GZipStream(inputStream, CompressionMode.Decompress);
    /// <summary>
    /// Obtiene la extensión utilizada de forma predeterminada para un
    /// recurso comprimido utilizando este
    /// <see cref="ICompressorGetter"/>.
    /// </summary>
    public string Extension => ".gzip";
}
