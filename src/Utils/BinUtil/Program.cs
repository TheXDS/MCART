/*
Program.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2020 César Andrés Morgan

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
using System.IO;
using System.IO.Compression;
using System.CommandLine;
using System.Threading.Tasks;
using St = TheXDS.MCART.Utils.BinUtil.Resources.Strings;
using System.CommandLine.Invocation;

namespace TheXDS.MCART.Utils.BinUtil
{
    /// <summary>
    /// Aplicación de compresión de recursos "BinUtil"
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Punto de entrada principal de la aplicación.
        /// </summary>
        public static async Task Main(string[] args)
        {
            var version = new RootCommand
            {
                new Option<bool>(new string[]{"-v", "--version" }, "Muestra la versión de esta herramienta.")
            };
            version.Handler = CommandHandler.Create((bool version) =>
            {
                if (version)
                {
                    Console.WriteLine("BinUtil 1.0.0.0");
                    Environment.Exit(0);
                }
            });
            version.Invoke(args);
            Console.WriteLine("Test");
            Console.ReadKey();
        }

        private static async Task CompressAsync(Stream input, Stream output)
        {
            try
            {
                using var ds = new DeflateStream(output, CompressionLevel.Optimal);
                await input.CopyToAsync(ds);
            }
            catch (Exception ex)
            {
                using var wr = new StreamWriter(Console.OpenStandardError());
                wr.WriteLine(ex.Message);
            }
        }

        private static Task ProcessPipes()
        {
            using var stdin = Console.OpenStandardInput();
            using var stdout = Console.OpenStandardOutput();
            return CompressAsync(stdin, stdout);
        }

        private static Task ProcessFile(string filePath)
        {
            using var fi = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            return ProcessFile(fi, $"{Path.GetFullPath(filePath)}.deflate");
        }

        private static Task ProcessFile(Stream input, string output)
        {
            using var fo = new FileStream(output, FileMode.Create, FileAccess.Write);
            return CompressAsync(input, fo);
        }

        private static async Task CompressFiles(string[] args)
        {
            foreach (var j in args)
            {
                if (File.Exists(j))
                {
                }
                else
                {
                    Console.WriteLine(string.Format(St.ErrFileDoesntExists, j));
                }
            }
        }
    }
}