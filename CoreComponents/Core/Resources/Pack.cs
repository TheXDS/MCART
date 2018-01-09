using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using TheXDS.MCART.Attributes;
using St = TheXDS.MCART.Resources.Strings;
namespace TheXDS.MCART.Resources
{
    /// <summary>
    /// Recursos misceláneos comprimidos.
    /// </summary>
    public static class Pack
    {
        static string GetResource(string identifier) => GetResource(identifier, "");
        static string GetResource(string identifier, string compressor)
        {
            var c = Objects.FindType<ICompressorGetter>(compressor)?.New<ICompressorGetter>()
#if PreferExceptions
                ?? throw new NotSupportedException();
#else
                ;
            if (c is null)
            {
                System.Diagnostics.Debug.Print(St.Warn(St.XIsInvalid(compressor)));
                return null;
            }
#endif
            try
            {
                using (var s = RTInfo.RTAssembly.GetManifestResourceStream($"{typeof(Pack).FullName}.{identifier}.pack"))
                using (var r = new StreamReader(c.GetCompressor(s))) return r.ReadToEnd();
            }
            catch
            {
#if PreferExceptions
                throw;
#else
                return St.Warn(St.XNotFound(St.XYQuotes(St.TheResource, identifier)));
#endif
            }
        }

        /* -= NOTA =-
         * La siguiente línea es un marcador de posición en este archivo de
         * recursos binarios. Indica la posición en la cual BinUtil podrá
         * agregar elementos.
         * 
         * ... Eventualmente BinUtil, junto con otras utilidades desarrolladas
         * para MCART, serán extensiones de Visual Studio / MonoDevelop que
         * automatizarán de una mejor manera el proceso de agregar recursos
         * binarios.
         */
        // <binutil-marker/>

    }

    interface ICompressorGetter
    {
        Stream GetCompressor(Stream stream);
    }

    [Identifier("")]
    [Identifier("deflate")]
    sealed class DeflateGetter : ICompressorGetter
    {
        public Stream GetCompressor(Stream stream)
        {
            return new DeflateStream(stream, CompressionMode.Decompress);
        }
    }

    [Identifier("gzip")]
    [Identifier("gz")]
    sealed class GZipGetter : ICompressorGetter
    {
        public Stream GetCompressor(Stream stream)
        {
            return new GZipStream(stream, CompressionMode.Decompress);
        }
    }
}