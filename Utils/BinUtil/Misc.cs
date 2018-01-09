using System;
using System.IO;
using System.Linq;

namespace TheXDS.MCARTBinUtil
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class ArgAttribute : Attribute
    {
        public string Value { get; }
        public ArgAttribute(string text) { Value = text; }
    }

    public abstract class StreamGetter
    {
        public bool TryGetStream(string source, out Stream stream)
        {
            stream = null;
            return (Uri.TryCreate(source, UriKind.Absolute, out Uri uriResult) && TryGetStream(uriResult, out stream));
        }
        public abstract bool TryGetStream(Uri source, out Stream stream);
        public abstract string InferRes(string source);
        public string InferRes(Uri source) => System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(source.Segments.Last()).Replace('.', '_').Replace('-', '_').Replace(" ", "");
    }

    public interface ICompressorGetter
    {
        Stream GetCompressor(Stream stream);
    }
}