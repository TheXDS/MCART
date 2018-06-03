﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
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
            if (String.IsNullOrWhiteSpace(sourceArg))
            {
                Console.WriteLine($"/!\\ Se necesita una fuente válida.");
                ShowHelp();
                return;
            }
            if (!Uri.TryCreate(sourceArg, UriKind.Absolute, out var sourceUri))
            {
                Console.WriteLine($"(X) El orígen '{sourceArg}' es inválido.");
                return;
            }

            var compressorArg = FindArg("compressor=", args) ?? string.Empty;

            var inputGetter = FindObject<StreamGetter>(FindArg("input=", args) ?? sourceUri.Scheme);
            if (inputGetter is null)
            {
                Console.WriteLine($"/!\\ Tipo de orígen no soportado.");
                ShowHelp();
                return;
            }

            var resArg = FindArg("res=", args) ?? StreamGetter.InferRes(sourceUri);

            if (!inputGetter.TryGetStream(sourceUri, out var inputStream))
            {
                Console.WriteLine($"(X) Error al acceder al orígen '{sourceArg}'.");
                return;
            }

            const string pth = @"../../../CoreComponents/Core/";

            using (var outputFile = new FileStream($"{pth}Resources/Pack/{resArg}.pack", FileMode.Create))
            {
                Stream compressorStream;
                try
                {
                    compressorStream = FindObject<ICompressorGetter>(compressorArg)?.GetCompressor(outputFile);
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
                var ct = new CancellationTokenSource();
                var t1 = inputStream.CopyToAsync(compressorStream);
                var t2 = Report(inputStream, ct.Token);
                Task.WaitAll(t1);
                ct.Cancel();
                compressorStream.Dispose();
            }

            string csFile;
            using (var sr = new StreamReader($"{pth}Resources/Pack.cs")) { csFile = sr.ReadToEnd(); }
            using (var sw = new StreamWriter($"{pth}Resources/Pack.cs"))
            {
                sw.Write(csFile.Replace("<binutil-marker/>", "<binutil-marker/>" + MakeProperty(resArg, compressorArg)));
            }

            using (var pf = new FileStream($"{pth}Core.projitems", FileMode.Open))
            {
                XmlDocument proj = new XmlDocument();
                proj.Load(pf);
                XmlElement xe = proj.CreateElement("EmbeddedResource");
                xe.SetAttribute("Include", $"$(MSBuildThisFileDirectory)Resources\\Pack\\{resArg}.pack");
                proj.DocumentElement.LastChild.AppendChild(xe);
                pf.SetLength(0);
                proj.Save(pf);
            }
        }

        private static void ShowHelp()
        {
            Console.WriteLine(@"
Utilidad de importación de recursos binarios comprimidos de MCART

Uso:
    binutil [source=]<orígen> [res=<nombre del recurso>] [input=<tipo de orígen>] [compressor=<compresor>]

Argumentos:
    source    : Cadena que describe el orígen del archivo.
    res       : Nombre del nuevo recurso a crear. Algunos tipos de orígenes permiten inferir el nombre.
    input     : Tipo de orígen. De forma predeterminada, se infiere basado en el orígen.
    compressor: Compresor a utilizar. De forma predeterminada, es 'deflate'.
");
            return;
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