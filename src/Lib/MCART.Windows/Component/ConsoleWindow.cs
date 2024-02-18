/*
ConsoleWindow.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2024 César Andrés Morgan

Permission is hereby granted, free of charge, to any person obtaining a copy of
this software and associated documentation files (the "Software"), to deal in
the Software without restriction, including without limitation the rights to
use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
of the Software, and to permit persons to whom the Software is furnished to do
so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using TheXDS.MCART.PInvoke.Structs;
using TheXDS.MCART.Types.Extensions;
using static TheXDS.MCART.PInvoke.Kernel32;

namespace TheXDS.MCART.Component;

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
