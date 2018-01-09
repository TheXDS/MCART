using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using System.Xml;

namespace TheXDS.MCARTBinUtil
{
    static class Program
    {
        static void Main(string[] args)
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

            var resArg = FindArg("res=", args) ?? inputGetter.InferRes(sourceUri);

            if (!inputGetter.TryGetStream(sourceUri, out var inputStream))
            {
                Console.WriteLine($"(X) Error al acceder al orígen '{sourceArg}'.");
                return;
            }

            var pth = @"../../../CoreComponents/Core/";

            using (var outputFile = new FileStream($"{pth}Resources/Pack/{resArg}.pack", FileMode.Create))
            {
                Stream compressorStream = null;
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

        static void ShowHelp()
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
        static async Task Report(Stream stream, CancellationToken token)
        {
            await Task.Run(() =>
            {
                // reportar solamente si la copia del stream toma más de 500 milisegundos.
                //Thread.Sleep(500);
                if (stream.CanSeek)
                {
                    while (stream.Position < stream.Length)
                    {
                        if (token.IsCancellationRequested) return;
                        Thread.Sleep(500);
                        Console.WriteLine($"Obteniendo... {stream.Position / (float)stream.Length:%}");
                    }
                }
                else
                {
                    Console.Write("Obteniendo");
                    while (!token.IsCancellationRequested)
                    {
                        Thread.Sleep(50);
                        Console.Write('.');
                    }
                    Console.WriteLine();
                }
            });
        }
        static T FindObject<T>(string arg) where T : class
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
        static string FindArg(string argName, string[] args)
        {
            foreach (var j in args)
            {
                if (j.StartsWith(argName)) return j.TrimStart(argName.ToCharArray());
            }
            return null;
        }
        static string FindArg(string[] args)
        {
            foreach (var j in args)
            {
                if (Uri.TryCreate(j, UriKind.Absolute, out _)) return j;
                if (Uri.TryCreate($"{Environment.CurrentDirectory}{Path.DirectorySeparatorChar}{j}", UriKind.Absolute, out Uri uri)) return uri.AbsolutePath;
            }
            return null;
        }
        static long BytesLeft(this FileStream fs) => fs.Length - fs.Position;
        static string MakeProperty(string resName, string compressor)
        {
            const string funcBody = @"
        /// <summary>
        /// Obtiene el recurso binario comprimido '{0}'.
        /// </summary>
        public static string {0} => GetResource(""{0}"", ""{1}"");";
            return String.Format(funcBody, resName, compressor);
        }
    }
}