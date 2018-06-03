using System;
using System.IO;
using System.Net;

namespace TheXDS.MCARTBinUtil
{
    [Arg("web")]
    [Arg("http")]
    [Arg("https")]
    internal sealed class WebGetter : StreamGetter
    {
        public override string InferRes(string source)
        {
            return source.Substring(source.LastIndexOf('/') + 1);
        }
        public override bool TryGetStream(Uri source, out Stream stream)
        {
            try
            {
                if (source.Scheme == Uri.UriSchemeHttp || source.Scheme == Uri.UriSchemeHttps)
                {
                    var wr = WebRequest.Create(source);
                    wr.Timeout = 10000;
                    stream = wr.GetResponse().GetResponseStream();
                    return true;
                }
            }
            finally { }
            stream = null;
            return false;
        }
    }

    [Arg("file")]
    internal sealed class FileGetter : StreamGetter
    {
        public override string InferRes(string source)
        {
            return source.Substring(source.LastIndexOf(Path.DirectorySeparatorChar) + 1);
        }
        public override bool TryGetStream(Uri source, out Stream stream)
        {
            try
            {
                if (source.Scheme == Uri.UriSchemeFile && File.Exists(source.AbsolutePath))
                {
                    stream = new FileStream(source.AbsolutePath, FileMode.Open);
                    return true;
                }
            }
            finally { }
            stream = null;
            return false;
        }
    }
}