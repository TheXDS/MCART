/*
ConsoleWindow.cs

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

namespace TheXDS.MCART.Component;
using System;
using TheXDS.MCART.PInvoke.Structs;
using TheXDS.MCART.Types.Extensions;
using static TheXDS.MCART.PInvoke.Kernel32;

/// <summary>
/// Clase auxiliar envolvente que permite realizar llamadas de gestión de
/// la ventana de consola de la aplicación.
/// </summary>
public class ConsoleWindow : IMsWindow
{
    private Margins _margins;

    /// <summary>
    /// Obtiene un valor que indica si la aplicación tiene acceso a la
    /// consola.
    /// </summary>
    public static bool HasConsole => GetConsoleWindow() != IntPtr.Zero;

    internal ConsoleWindow()
    {
        if (Handle == IntPtr.Zero) AllocConsole();
    }

    /// <inheritdoc/>
    public IntPtr Handle => GetConsoleWindow();

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
        if (Handle != IntPtr.Zero) FreeConsole();
    }
}
