/*
Api.cs

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
using System.Runtime.InteropServices;

namespace TheXDS.MCART.Windows.Api
{
    internal static class PInvoke
    {
        [DllImport("kernel32.dll")] internal static extern IntPtr GetConsoleWindow();
        [DllImport("kernel32.dll")] internal static extern bool AllocConsole();
        [DllImport("kernel32.dll")] internal static extern bool FreeConsole();
    }

    /// <summary>
    /// Contiene una serie de métodos auxiliares de la API de Microsoft
    /// Windows.
    /// </summary>
    public static class Api
    {
        /// <summary>
        /// Obtiene un valor que indica si la aplicación tiene acceso a la
        /// consola.
        /// </summary>
        public static bool HasConsole => PInvoke.GetConsoleWindow() != IntPtr.Zero;

        /// <summary>
        /// Obtiene un objeto que permite controlar la ventana de la consola.
        /// </summary>
        /// <returns>
        /// Un objeto que permite controlar la ventana de la consola.
        /// </returns>
        public static ConsoleWindow GetConsoleWindow() => new ConsoleWindow();
    }
}
