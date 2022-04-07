/*
DeflateGetter.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2021 César Andrés Morgan

Morgan's CLR Advanced Runtime (MCART) is free software: you can redistribute it
and/or modify it under the terms of the GNU General Public License as published
by the Free Software Foundation, either version 3 of the License, or (at your
option) any later version.

Morgan's CLR Advanced Runtime (MCART) is distributed in the hope that it will
be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General
Public License for more details.

You should have received a copy of the GNU General Public License along with
this program. If not, see <http://www.gnu.org/licenses/>.
*/

namespace TheXDS.MCART.Resources;
using System.IO;
using System.IO.Compression;
using TheXDS.MCART.Attributes;

/// <summary>
/// <see cref="ICompressorGetter"/> que construye un
/// <see cref="DeflateStream"/> que puede ser utilizado para extraer
/// información comprimida desde un <see cref="Stream"/>.
/// </summary>
[Identifier("deflate")]
public sealed class DeflateGetter : ICompressorGetter
{
    /// <summary>
    /// Obtiene un <see cref="DeflateStream"/> para extraer información
    /// comprimida desde <paramref name="inputStream"/>.
    /// </summary>
    /// <param name="inputStream">
    /// <see cref="Stream"/> que contiene la información a extraer.
    /// </param>
    /// <returns>
    /// Un <see cref="DeflateStream"/> que puede utilizarse para extraer
    /// información comprimida desde <paramref name="inputStream"/>.
    /// </returns>
    public Stream GetCompressor(Stream inputStream) => new DeflateStream(inputStream, CompressionMode.Decompress);

    /// <summary>
    /// Obtiene la extensión utilizada de forma predeterminada para un
    /// recurso comprimido utilizando este
    /// <see cref="ICompressorGetter"/>.
    /// </summary>
    public string Extension => ".deflate";
}
