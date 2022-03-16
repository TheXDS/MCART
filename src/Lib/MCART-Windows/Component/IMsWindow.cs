/*
IMsWindow.cs

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
using TheXDS.MCART.Types;
using TheXDS.MCART.PInvoke.Structs;
using static TheXDS.MCART.PInvoke.User32;

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
