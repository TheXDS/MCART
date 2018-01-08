using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

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

            var sourceArg = FindArg("source=", args);
            if (String.IsNullOrWhiteSpace(sourceArg))
            {
                Console.WriteLine($"/!\\ Se necesita una fuente.");
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

            var pth = @"C:\Users\xds_x\src\MCART\Build\Utils\Debug\";
            using (var outputFile = new FileStream($"{pth}{resArg}.pack", FileMode.Create))
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
            Console.ReadKey();
        }

        static void ShowHelp()
        {
            Console.WriteLine(@"
Utilidad de importación de recursos binarios comprimidos de MCART

Uso:
    binutil source=<orígen> [res=<nombre del recurso>] [input=<tipo de orígen>] [compressor=<compresor>]

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
        static long BytesLeft(this FileStream fs) => fs.Length - fs.Position;
    }
}