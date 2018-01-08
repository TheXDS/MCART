using System.IO;
using System.IO.Compression;

namespace TheXDS.MCARTBinUtil
{
    [Arg("")]
    [Arg("deflate")]
    sealed class DeflateGetter : ICompressorGetter
    {
        public Stream GetCompressor(Stream stream)
        {
            return new DeflateStream(stream, CompressionLevel.Optimal);
        }
    }

    [Arg("gzip")]
    [Arg("gz")]
    sealed class GZipGetter : ICompressorGetter
    {
        public Stream GetCompressor(Stream stream)
        {
            return new GZipStream(stream, CompressionLevel.Optimal);
        }
    }
}