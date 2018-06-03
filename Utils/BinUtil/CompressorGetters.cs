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