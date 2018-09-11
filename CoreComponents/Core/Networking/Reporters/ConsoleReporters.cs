/*
DownloadHelper.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Este archivo contiene funciones para la descarga de archivos por medio de
protocolos web que se bansen en TCP, como http y ftp.

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright (c) 2011 - 2018 César Andrés Morgan

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
using TheXDS.MCART.Math;

namespace TheXDS.MCART.Networking.Reporters
{
#if ExtrasBuiltIn
	public static class ConsoleReporters
	{
		public static void FullWidth(long? current, long? total, long? speed)
        {
            var col = Console.CursorLeft;
            Console.Write(new string(' ', Console.BufferWidth - col - 1));
            Console.CursorLeft = col;
            var text = $"] {current?.ByteUnits() ?? "???"}/{total?.ByteUnits() ?? "???"} {speed?.ByteUnits() ?? "??? Bytes"}/s";
            if (speed != 0) text += $", -{TimeSpan.FromSeconds((total - current ?? 0) / speed ?? 1)}";
            var txtCol = Console.BufferWidth - text.Length - 1;
            if (total.HasValue)
                Console.Write($"[{new string('=', (int)((txtCol - col) * ((current ?? 0) / (float)total)).Clamp(0, Console.BufferWidth))}");
            else
                Console.Write("[=- ?");
            Console.CursorLeft = txtCol;
            Console.Write(text);
            Console.CursorLeft = col;
        }
		public static void Simplistic(long? current, long? total, long? speed)
		{
			if (Objects.IsAnyNull(current,total,speed))
			{
				Console.Write('.');
				return;
			}
			var col = Console.CursorLeft;
            Console.Write(new string(' ', Console.BufferWidth - col - 1));
            Console.CursorLeft = col;
			Console.Write($"{current / total:%}");
		}
	}
#endif
}