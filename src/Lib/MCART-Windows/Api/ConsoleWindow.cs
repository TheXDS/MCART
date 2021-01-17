/*
Api.cs

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
using TheXDS.MCART.Windows.Component;
using TheXDS.MCART.Windows.Dwm.Structs;
using TheXDS.MCART.Windows.Dwm;

namespace TheXDS.MCART.Windows.Api
{
    /// <summary>
    /// Clase auxiliar envolvente que permite realizar llamadas de gestión de
    /// la ventana de consola de la aplicación.
    /// </summary>
    public class ConsoleWindow : IWindow
    {
        private Margins _margins;

        internal  ConsoleWindow()
        {
            if (Handle == IntPtr.Zero) PInvoke.AllocConsole();
        }

        /// <inheritdoc/>
        public IntPtr Handle => PInvoke.GetConsoleWindow();

        /// <inheritdoc/>
        public Margins Padding
        { 
            get => _margins;
            set
            {
                _margins = value;
                this.SetClientPadding(value);
            }
        }

        /// <summary>
        /// Cierra la ventana de consola.
        /// </summary>
        public void Close()
        {
            if (Handle != IntPtr.Zero) PInvoke.FreeConsole();
        }

        ///// <summary>
        ///// Destruye la instancia activa de la ventana de consola.
        ///// </summary>
        //~ConsoleWindow() => Close();
    }
}
