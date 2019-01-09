/*
Program.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Este programa es una demostración de la API de MCART para poder descargar
archivos por medio de http, incluyendo una función de monitoreo y reporte del
progreso de la operación. De cierto modo, puede considerársele un clon 
sumamente simplificado de la applicación 'wget' para sistemas basados en las
herramientas de GNU.

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
using static TheXDS.MCART.Networking.DownloadHelper;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TheXDS.MCART;
using TheXDS.MCART.Networking.Reporters;

#if !ExtrasBuiltIn
using TheXDS.MCART.Math;
using static TheXDS.MCART.Networking.Common;
#endif

namespace Downloader
{
	internal static class Program
	{
		private static async Task Main(string[] args)
		{
			foreach (var j in args)
			{
				if (!Uri.TryCreate(j, UriKind.Absolute, out var uri)) continue;
				if (!uri.Scheme.IsEither(Uri.UriSchemeHttp, Uri.UriSchemeHttps)) continue;
				using (var fs = new FileStream(uri.Segments.Last(), FileMode.Create))
				{
					Console.Write($"{j} => {fs.Name} ");
#if ExtrasBuiltIn
					await DownloadHttpAsync(uri, fs, ConsoleReporters.FullWidth, 1000);
#else
                    await DownloadHttpAsync(uri, fs, Report, 1000);
#endif
					Console.WriteLine();
				}
			}
		}

#if !ExtrasBuiltIn
		private static void Report(long? current, long? total, long? speed)
        {
            var col = Console.CursorLeft;
            Console.Write(new string(' ', Console.BufferWidth-col-1));
            Console.CursorLeft = col;
            var text = $"] {current?.ByteUnits() ?? "???"}/{total?.ByteUnits()?? "???"} {speed?.ByteUnits() ?? "??? Bytes"}/s";
            if (speed !=0) text+= $", -{TimeSpan.FromSeconds((total-current??0)/speed??1)}";
            var txtCol = Console.BufferWidth - text.Length - 1;
            if (total.HasValue)
                Console.Write($"[{new string('=', (int)((txtCol - col) * ((current ?? 0) / (float)total)).Clamp(0, Console.BufferWidth))}");
            else
                Console.Write("[=- ?");
            Console.CursorLeft = txtCol;
            Console.Write(text);
            Console.CursorLeft = col;
        }
#endif
    }
}