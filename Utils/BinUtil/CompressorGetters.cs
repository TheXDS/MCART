/*
CompressorGetters.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright (c) 2011 - 2018 César Andrés Morgan

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

namespace TheXDS.MCARTBinUtil
{
    [Arg("")]
    [Arg("deflate")]
    internal sealed class DeflateGetter : ICompressorGetter
    {
        public Stream GetCompressor(Stream stream)
        {
            return new DeflateStream(stream, CompressionLevel.Optimal);
        }
    }

    [Arg("gzip")]
    [Arg("gz")]
    internal sealed class GZipGetter : ICompressorGetter
    {
        public Stream GetCompressor(Stream stream)
        {
            return new GZipStream(stream, CompressionLevel.Optimal);
        }
    }

    [Arg("none")]
    [Arg("null")]
    internal sealed class NullGetter : ICompressorGetter
    {
        public Stream GetCompressor(Stream stream)
        {
            return stream;
        }
    }
}