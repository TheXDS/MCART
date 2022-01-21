﻿/*
Windows.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2021 César Andrés Morgan

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
using System.Security.Principal;
using TheXDS.MCART.Component;

namespace TheXDS.MCART.Helpers
{
    /// <summary>
    /// Contiene una serie de métodos auxiliares de la API de Microsoft
    /// Windows.
    /// </summary>
    public static class Windows
    {
        private static WindowsInfo? _winInfo;

        /// <summary>
        /// Obtiene un objeto que expone información variada acerca de Windows.
        /// </summary>
        public static WindowsInfo Info => _winInfo ??= new WindowsInfo();

        /// <summary>
        /// Comprueba si el contexto de ejecución actual de la aplicación
        /// contiene permisos administrativos.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> si la aplicación está siendo ejecutada con
        /// permisos administrativos, <see langword="false"/> en caso
        /// contrario.
        /// </returns>
        public static bool IsAdministrator()
        {
            using WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }

        /// <summary>
        /// Libera un objeto COM.
        /// </summary>
        /// <param name="obj">Objeto COM a liberar.</param>
        public static void ReleaseComObject(object obj)
        {
            try
            {
                Marshal.ReleaseComObject(obj);
            }
            finally
            {
                GC.Collect();
            }
        }
    }
}