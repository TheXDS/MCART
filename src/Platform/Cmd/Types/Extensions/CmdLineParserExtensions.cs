/*
Helpers.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2019 César Andrés Morgan

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

#nullable enable

using System;
using System.Linq;
using TheXDS.MCART.Component;
using static TheXDS.MCART.Resources.CmdStrings;

namespace TheXDS.MCART.Types.Extensions
{
    /// <summary>
    ///     Extensiones para administrar un <see cref="CmdLineParser"/> bajo la
    ///     interfaz de terminal/consola.
    /// </summary>
    public static class CmdLineParserExtensions
    {
        /// <summary>
        ///     Genera una pantalla de ayuda con los argumentos registrados en
        ///     el <see cref="CmdLineParser"/> especificado.
        /// </summary>
        /// <param name="args">
        ///     Instancia de <see cref="CmdLineParser"/> desde la cual extraer
        ///     la información de ayuda de los argumentos.
        /// </param>
        public static void PrintHelp(this CmdLineParser args)
        {
            IExposeInfo nfo = new AssemblyInfo(ReflectionHelpers.GetEntryPoint().DeclaringType.Assembly);
            Cmd.Helpers.About(nfo);

            var width = args.AvailableArguments.Max(p => p.LongName.Length);
            foreach (var j in args.AvailableArguments)
            {
                Console.Write($"  /{j.LongName.PadRight(width)}  - ");
                var _1st = false;
                foreach (var k in (j.Summary ?? string.Empty).TextWrap(Console.BufferWidth - (width + 7)))
                {
                    if (_1st)
                    {
                        Console.Write(new string(' ', width + 7));
                    }
                    _1st = true;
                    Console.WriteLine(k);
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        ///     Ejecuta automáticamente las tareas asociadas a una lista de
        ///     argumentos.
        /// </summary>
        /// <param name="args">
        ///     Instancia de <see cref="CmdLineParser"/> con los argumentos a
        ///     ejecutar.
        /// </param>
        /// <param name="exitIfInvalidArgs">
        ///     Si se establece en <see langword="true"/>, la aplicación será
        ///     finalizada si existen argumentos inválidos, faltan argumentos
        ///     requeridos o si ocurre un error en una de las operaciones.
        /// </param>
        public static void AutoRun(this CmdLineParser args, bool exitIfInvalidArgs)
        {
            if (args.Invalid.Any())
            {
                foreach (var j in args.Invalid)
                    Console.WriteLine(InvalidArg(j));
                args.PrintHelp();
                if (exitIfInvalidArgs) Environment.Exit(1);
            }
            if (args.Missing.Any())
            {
                foreach (var j in args.Missing)
                    Console.WriteLine(MissingArg(j.LongName));
                args.PrintHelp();
                if (exitIfInvalidArgs) Environment.Exit(1);
            }
            foreach (var j in args.Present)
            {
                try
                {
                    j.Run(args);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    if (exitIfInvalidArgs) Environment.Exit(ex.HResult);
                }
            }
        }

        /// <summary>
        ///     Ejecuta automáticamente las tareas asociadas a una lista de
        ///     argumentos.
        /// </summary>
        /// <param name="args">
        ///     Instancia de <see cref="CmdLineParser"/> con los argumentos a
        ///     ejecutar.
        /// </param>
        public static void AutoRun(this CmdLineParser args) => AutoRun(args, true);
    }
}
