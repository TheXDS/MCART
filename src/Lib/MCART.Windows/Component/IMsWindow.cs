/*
IMsWindow.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2025 César Andrés Morgan

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

using TheXDS.MCART.PInvoke.Models;
using TheXDS.MCART.Types;
using TheXDS.MCART.Types.Base;
using static TheXDS.MCART.PInvoke.User32;

namespace TheXDS.MCART.Component;

/// <summary>
/// Defines a set of members to be implemented by a type that
/// represents a Microsoft Windows window.
/// </summary>
public interface IMsWindow : IWindow
{
    /// <summary>
    /// Gets the handle through which the window can be manipulated.
    /// </summary>
    IntPtr Handle { get; }

    /// <summary>
    /// Gets or sets the interior margin that spaces the window's
    /// borders from its content.
    /// </summary>
    Margins Padding { get; set; }

    /// <summary>
    /// Gets or sets the window size.
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
    /// Gets or sets the window position in absolute screen
    /// coordinates.
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
    /// Hides the window without closing it.
    /// </summary>
    void IWindow.Hide()
    {
        CallShowWindow(ShowWindowFlags.Hide);
    }

    /// <summary>
    /// Maximizes the window.
    /// </summary>
    void IWindow.Maximize()
    {
        CallShowWindow(ShowWindowFlags.ShowMaximized);
    }

    /// <summary>
    /// Minimizes the window.
    /// </summary>
    void IWindow.Minimize()
    {
        CallShowWindow(ShowWindowFlags.Minimize);
    }

    /// <summary>
    /// Restores the window to its previous size.
    /// </summary>
    void IWindow.Restore()
    {
        CallShowWindow(ShowWindowFlags.Restore);
    }

    /// <summary>
    /// Toggles the window between maximized and restored states.
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
