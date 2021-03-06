﻿/*
CompressorGetters.cs

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

using System.IO;
using System.IO.Compression;
using TheXDS.MCART.Attributes;

namespace TheXDS.MCART.Resources
{
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
    /// <summary>
    /// <see cref="ICompressorGetter"/> que construye un
    /// <see cref="GZipStream"/> que puede ser utilizado para extraer
    /// información comprimida desde un <see cref="Stream"/>.
    /// </summary>
    [Identifier("gzip")]
    [Identifier("gz")]
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

    /// <summary>
    /// <see cref="ICompressorGetter"/> que expone directamente un 
    /// <see cref="Stream"/> cuando el mismo no ha sido escrito utilizando
    /// un compresor.
    /// </summary>
    public sealed class NullGetter : ICompressorGetter
    {
        /// <summary>
        /// Obtiene un <see cref="Stream"/>  que expone a 
        /// <paramref name="inputStream"/> directamente.
        /// </summary>
        /// <param name="inputStream">
        /// <see cref="Stream"/> que contiene la información a extraer.
        /// </param>
        /// <returns>
        /// El mismo <see cref="Stream"/> que
        /// <paramref name="inputStream"/>.
        /// </returns>
        public Stream GetCompressor(Stream inputStream)
        {
            return inputStream;
        }

        /// <summary>
        /// Obtiene la extensión utilizada de forma predeterminada para un
        /// recurso comprimido utilizando este
        /// <see cref="ICompressorGetter"/>.
        /// </summary>
        public string? Extension => null;
    }
}