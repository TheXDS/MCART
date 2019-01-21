/*
Program.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright (c) 2011 - 2019 César Andrés Morgan

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

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Xml;

namespace TheXDS.MCARTBinUtil
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            if (!args.Any()
                || args.Contains("--help")
                || args.Contains("-?")
                || args.Contains("/help")
                || args.Contains("/help"))
            {
                ShowHelp();
                return;
            }

            var sourceArg = FindArg("source=", args) ?? FindArg(args);
            if (string.IsNullOrWhiteSpace(sourceArg))
            {
                Console.WriteLine(@"/!\ Se necesita una fuente válida.");
                ShowHelp();
                return;
            }
            if (!Uri.TryCreate(sourceArg, UriKind.Absolute, out var sourceUri))
            {
                Console.WriteLine($"(X) El origen '{sourceArg}' es inválido.");
                return;
            }

            var compressorArg = FindArg("compressor=", args) ?? string.Empty;

            var inputGetter = FindObject<StreamGetter>(FindArg("input=", args) ?? sourceUri.Scheme);
            if (inputGetter is null)
            {
                Console.WriteLine(@"/!\ Tipo de origen no soportado.");
                ShowHelp();
                return;
            }

            var resArg = FindArg("res=", args) ?? StreamGetter.InferRes(sourceUri);

            if (!inputGetter.TryGetStream(sourceUri, out var inputStream))
            {
                Console.WriteLine($"(X) Error al acceder al origen '{sourceArg}'.");
                return;
            }

            const string pth = @"../../../MCART/WPF/";

            var cg = FindObject<ICompressorGetter>(compressorArg);

            using (var outputFile = new FileStream($"{pth}Resources/Icons/{resArg}{cg?.Extension}", FileMode.Create))
            {
                Stream compressorStream;
                try
                {
                    compressorStream = cg?.GetCompressor(outputFile);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"(X) Error al escribir archivo '{outputFile.Name}': {ex.Message}");
                    return;
                }
                if (compressorStream is null)
                {
                    Console.WriteLine($"/!\\ Compresor no soportado: '{compressorArg}'");
                    ShowHelp();
                    return;
                }
                //var ct = new CancellationTokenSource();
                inputStream.CopyTo(compressorStream);
                //var t2 = Report(inputStream, ct.Token);
                //Task.WaitAll(t1, t2);
                //ct.Cancel();
                compressorStream.Dispose();
            }
            
//            string csFile;

//            if (!File.Exists($"{pth}Resources/Pack.cs"))
//            {
//                using (var sw = new StreamWriter($"{pth}Resources/Pack.cs"))
//                {
//                    sw.Write(@"/*
//Pack.cs

//This file is part of Morgan's CLR Advanced Runtime (MCART)

//Author(s):
//     César Andrés Morgan <xds_xps_ivx@hotmail.com>

//Copyright (c) 2011 - 2019 César Andrés Morgan

//Morgan's CLR Advanced Runtime (MCART) is free software: you can redistribute it
//and/or modify it under the terms of the GNU General Public License as published
//by the Free Software Foundation, either version 3 of the License, or (at your
//option) any later version.

//Morgan's CLR Advanced Runtime (MCART) is distributed in the hope that it will
//be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General
//Public License for more details.

//You should have received a copy of the GNU General Public License along with
//this program. If not, see <http://www.gnu.org/licenses/>.
//*/

//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.IO.Compression;
//using System.Text;
//using TheXDS.MCART.Attributes;
//using St = TheXDS.MCART.Resources.Strings;
//namespace TheXDS.MCART.Resources
//{
//    /// <summary>
//    /// Recursos misceláneos comprimidos.
//    /// </summary>
//    public static class Pack
//    {
//        static string GetResource(string identifier) => GetResource(identifier, """");
//        static string GetResource(string identifier, string compressor)
//        {
//            var c = Objects.FindType<ICompressorGetter>(compressor)?.New<ICompressorGetter>()
//#if PreferExceptions
//                ?? throw new NotSupportedException();
//#else
//                ;
//            if (c is null)
//            {
//                System.Diagnostics.Debug.Print(St.Warn(St.XIsInvalid(compressor)));
//                return null;
//            }
//#endif
//            try
//            {
//                using (var s = RTInfo.RTAssembly.GetManifestResourceStream($""{typeof(Pack).FullName}.{identifier}.pack""))
//                using (var r = new StreamReader(c.GetCompressor(s))) return r.ReadToEnd();
//            }
//            catch
//            {
//#if PreferExceptions
//                throw;
//#else
//                return St.Warn(St.XNotFound(St.XYQuotes(St.TheResource, identifier)));
//#endif
//            }
//        }

//        /* -= NOTA =-
//         * La siguiente línea es un marcador de posición en este archivo de
//         * recursos binarios. Indica la posición en la cual BinUtil podrá
//         * agregar elementos.
//         * 
//         * ... Eventualmente BinUtil, junto con otras utilidades desarrolladas
//         * para MCART, serán extensiones de Visual Studio / MonoDevelop que
//         * automatizarán de una mejor manera el proceso de agregar recursos
//         * binarios.
//         */
//        // <binutil-marker/>

//    }

//    interface ICompressorGetter
//    {
//        Stream GetCompressor(Stream stream);
//    }

//    [Identifier("""")]
//    [Identifier(""deflate"")]
//    sealed class DeflateGetter : ICompressorGetter
//    {
//        public Stream GetCompressor(Stream stream)
//        {
//            return new DeflateStream(stream, CompressionMode.Decompress);
//        }
//    }

//    [Identifier(""gzip"")]
//    [Identifier(""gz"")]
//    sealed class GZipGetter : ICompressorGetter
//    {
//        public Stream GetCompressor(Stream stream)
//        {
//            return new GZipStream(stream, CompressionMode.Decompress);
//        }
//    }
//}");
//                }
//            }

            //using (var sr = new StreamReader($"{pth}Resources/Pack.cs")) { csFile = sr.ReadToEnd(); }
            //using (var sw = new StreamWriter($"{pth}Resources/Pack.cs"))
            //{
            //    sw.Write(csFile.Replace("<binutil-marker/>", "<binutil-marker/>" + MakeProperty(resArg, compressorArg)));
            //}

            //using (var pf = new FileStream($"{pth}Core.projitems", FileMode.Open))
            //{
            //    var proj = new XmlDocument();
            //    proj.Load(pf);
            //    var xe = proj.CreateElement("EmbeddedResource");
            //    xe.SetAttribute("Include", $"$(MSBuildThisFileDirectory)Resources\\Pack\\{resArg}.pack");
            //    proj.DocumentElement?.LastChild.AppendChild(xe);
            //    pf.SetLength(0);
            //    proj.Save(pf);
            //}
            
        }

        private static void ShowHelp()
        {
            Console.WriteLine(@"
Utilidad de importación de recursos binarios comprimidos de MCART

Uso:
    binutil [source=]<origen> [res=<nombre del recurso>] [input=<tipo de origen>] [compressor=<compresor>]

Argumentos:
    source    : Cadena que describe el origen del archivo.
    res       : Nombre del nuevo recurso a crear. Algunos tipos de orígenes permiten inferir el nombre.
    input     : Tipo de origen. De forma predeterminada, se infiere basado en el origen.
    compressor: Compresor a utilizar. De forma predeterminada, es 'deflate'.
");
        }

        private static async Task Report(Stream stream, CancellationToken token)
        {
            // reportar solamente si la copia del stream toma más de 500 milisegundos.
            //Thread.Sleep(500);
            if (stream.CanSeek)
            {
                Console.Write("Obteniendo... ");
                var row = Console.CursorTop;
                var col = Console.CursorLeft;
                while (stream.Position <= stream.Length)
                {
                    if (token.IsCancellationRequested) break;
                    await Task.Delay(50, token);
                    Console.CursorTop = row;
                    Console.CursorLeft = col;
                    Console.Write($"{stream.Position / (float)stream.Length:P1}");
                }
            }
            else
            {
                Console.Write("Obteniendo... ");
                var row = Console.CursorTop;
                var col = Console.CursorLeft;
                while (!token.IsCancellationRequested)
                {
                    await Task.Delay(50, token);
                    Console.CursorTop = row;
                    Console.CursorLeft = col;
                    Console.Write($"{(float)stream.Length/1024:F0} KB");
                }
                Console.WriteLine();
            }
        }

        private static T FindObject<T>(string arg) where T : class
        {
            foreach (var j in typeof(Program).Assembly.GetTypes().Where(p => typeof(T).IsAssignableFrom(p)))
            {
                foreach (ArgAttribute k in j.GetCustomAttributes(typeof(ArgAttribute), false))
                {
                    if (k.Value == arg) return j.GetConstructor(new Type[] { })?.Invoke(new object[] { }) as T;
                }
            }
            return null;
        }

        private static string FindArg(string argName, string[] args)
        {
            foreach (var j in args)
            {
                if (j.StartsWith(argName)) return j.TrimStart(argName.ToCharArray());
            }
            return null;
        }

        private static string FindArg(IEnumerable<string> args)
        {
            foreach (var j in args)
            {
                if (Uri.TryCreate(j, UriKind.Absolute, out _)) return j;
                if (Uri.TryCreate($"{Environment.CurrentDirectory}{Path.DirectorySeparatorChar}{j}", UriKind.Absolute, out Uri uri)) return uri.AbsolutePath;
            }
            return null;
        }

        private static long BytesLeft(this Stream fs) => fs.Length - fs.Position;

        private static string MakeProperty(string resName, string compressor)
        {
            const string funcBody = @"
        /// <summary>
        /// Obtiene el recurso binario comprimido '{0}'.
        /// </summary>
        public static string {0} => GetResource(""{0}"", ""{1}"");";
            return string.Format(funcBody, resName, compressor);
        }
    }
}