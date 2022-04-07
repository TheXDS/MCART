/*
User32.cs

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

namespace TheXDS.MCART.PInvoke;
using System;
using System.Runtime.InteropServices;
using TheXDS.MCART.PInvoke.Structs;

internal class User32
{
    public delegate bool MonitorEnumProc(IntPtr monitor, IntPtr hdc, IntPtr lprcMonitor, IntPtr lParam);

    [return: MarshalAs(UnmanagedType.Bool)]
    [DllImport("user32.dll", SetLastError = true)] internal static extern bool GetWindowInfo(IntPtr hWnd, ref WindowInfo nCmdShow);
    [DllImport("user32.dll")] internal static extern long GetWindowLong(IntPtr hwnd, WindowData index);
    [DllImport("user32.dll")] internal static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);
    [DllImport("user32.dll", SetLastError = true)] internal static extern int SetWindowLong(IntPtr hwnd, int index, uint newStyle);
    [DllImport("user32.dll")] internal static extern bool SetWindowPos(IntPtr hwnd, IntPtr hwndInsertAfter, int x, int y, int width, int height, uint flags);
    [DllImport("user32.dll")] internal static extern bool ShowWindow(IntPtr hWnd, ShowWindowFlags nCmdShow);
    [DllImport("user32.dll")] internal static extern bool ShowWindowAsync(IntPtr hWnd, ShowWindowFlags nCmdShow);
    [DllImport("user32.dll")] internal static extern bool GetCursorPos(out Point lpPoint);
    [DllImport("user32.dll", SetLastError = true)] internal static extern bool GetWindowRect(IntPtr hWnd, ref Rect lpRect);
    [DllImport("user32.dll", SetLastError = true)] internal static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);
    [DllImport("user32.dll")] internal static extern bool GetMonitorInfo(IntPtr hmonitor, ref MonitorInfo info);
    [DllImport("user32.dll")] internal static extern bool EnumDisplayMonitors(HandleRef hdc, Rect rcClip, MonitorEnumProc lpfnEnum, IntPtr dwData);
    [DllImport("user32.dll")] internal static extern IntPtr MonitorFromWindow(HandleRef handle, int flags);
    [DllImport("user32.dll")] internal static extern int GetSystemMetrics(int nIndex);
    [DllImport("user32.dll")] internal static extern bool SystemParametersInfo(int nAction, int nParam, ref Rect rc, int nUpdate);
    [DllImport("user32.dll")] internal static extern IntPtr MonitorFromPoint(Point pt, int flags);
}
