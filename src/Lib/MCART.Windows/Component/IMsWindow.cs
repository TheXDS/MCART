/*
IMsWindow.cs

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
using TheXDS.MCART.Types;
using TheXDS.MCART.Types.Base;
using static TheXDS.MCART.PInvoke.User32;

namespace TheXDS.MCART.Component;

/// <summary>
/// Define una serie de miembros a implementar por un tipo que represente
/// una ventana de Microsoft Windows.
/// </summary>
public interface IMsWindow : IWindow
{
    /// <summary>
    /// Obtiene el Handle por medio del cual la ventana puede ser
    /// manipulada.
    /// </summary>
    IntPtr Handle { get; }

    /// <summary>
    /// Obtiene o establece el margen interior de espaciado entre los 
    /// bordes de la ventana y su contenido.
    /// </summary>
    Margins Padding { get; set; }

    /// <summary>
    /// Obtiene o establece el tamaño de la ventana.
    /// </summary>
    Size IWindow.Size
    {
        get
        {
            return GetRect().Size;
        }
        set
        {
            Rect info = GetRect();
            MoveWindow(Handle, info.Left, info.Top, (int)value.Width, (int)value.Height, true);
        }
    }

    /// <summary>
    /// Obtiene o establece la posición de la ventana en coordenadas
    /// absolutas de pantalla.
    /// </summary>
    Types.Point IWindow.Location
    {
        get
        {
            return GetRect().Location;
        }
        set
        {
            Rect info = GetRect();
            MoveWindow(Handle, (int)value.X, (int)value.Y, info.Width, info.Height, true);
        }
    }

    /// <summary>
    /// Oculta la ventana sin cerrarla.
    /// </summary>
    void IWindow.Hide()
    {
        CallShowWindow(ShowWindowFlags.Hide);
    }

    /// <summary>
    /// Maximiza la ventana.
    /// </summary>
    void IWindow.Maximize()
    {
        CallShowWindow(ShowWindowFlags.ShowMaximized);
    }

    /// <summary>
    /// Minimiza la ventana.
    /// </summary>
    void IWindow.Minimize()
    {
        CallShowWindow(ShowWindowFlags.Minimize);
    }

    /// <summary>
    /// Restaura el tamaño de la ventana.
    /// </summary>
    void IWindow.Restore()
    {
        CallShowWindow(ShowWindowFlags.Restore);
    }

    /// <summary>
    /// Cambia el estado de la ventana entre Maximizar y Restaurar.
    /// </summary>
    void IWindow.ToggleMaximize()
    {
        WindowStyles s = (WindowStyles)GetWindowLong(Handle, WindowData.GWL_STYLE);
        if (s.HasFlag(WindowStyles.WS_MAXIMIZE)) Restore();
        else Maximize();
    }

    private void CallShowWindow(ShowWindowFlags flags)
    {
        if (Handle != IntPtr.Zero) ShowWindowAsync(Handle, flags);
    }

    private Rect GetRect()
    {
        Rect info = new();
        GetWindowRect(Handle, ref info);
        return info;
    }
}
