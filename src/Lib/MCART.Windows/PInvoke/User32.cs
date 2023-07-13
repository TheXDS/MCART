/*
User32.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2023 César Andrés Morgan

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

using System;
using System.Runtime.InteropServices;
using TheXDS.MCART.PInvoke.Structs;

namespace TheXDS.MCART.PInvoke;

internal partial class User32
{
    public delegate bool MonitorEnumProc(IntPtr monitor, IntPtr hdc, IntPtr lprcMonitor, IntPtr lParam);

#if NET7_0_OR_GREATER

    [LibraryImport("user32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static partial bool GetWindowInfo(IntPtr hWnd, ref WindowInfo nCmdShow);

    [LibraryImport("user32.dll")]
    internal static partial IntPtr GetActiveWindow();

    [LibraryImport("user32.dll")]
    internal static partial IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, IntPtr lParam);

    [LibraryImport("user32.dll")]
    internal static partial long GetWindowLong(IntPtr hwnd, WindowData index);

    [LibraryImport("user32.dll")]
    internal static partial int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);

    [LibraryImport("user32.dll", SetLastError = true)]
    internal static partial int SetWindowLong(IntPtr hwnd, int index, uint newStyle);

    [LibraryImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static partial bool SetWindowPos(IntPtr hwnd, IntPtr hwndInsertAfter, int x, int y, int width, int height, uint flags);

    [LibraryImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static partial bool ShowWindow(IntPtr hWnd, ShowWindowFlags nCmdShow);

    [LibraryImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static partial bool ShowWindowAsync(IntPtr hWnd, ShowWindowFlags nCmdShow);

    [LibraryImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static partial bool GetCursorPos(out Point lpPoint);

    [LibraryImport("user32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static partial bool GetWindowRect(IntPtr hWnd, ref Rect lpRect);

    [LibraryImport("user32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static partial bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, [MarshalAs(UnmanagedType.Bool)] bool bRepaint);

    [LibraryImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static partial bool GetMonitorInfo(IntPtr hmonitor, ref MonitorInfo info);

    [LibraryImport("user32.dll")]
    internal static partial int GetSystemMetrics(int nIndex);

    [LibraryImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static partial bool SystemParametersInfo(int nAction, int nParam, ref Rect rc, int nUpdate);

    [LibraryImport("user32.dll")]
    internal static partial IntPtr MonitorFromPoint(Point pt, int flags);

#else

    [DllImport("user32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern bool GetWindowInfo(IntPtr hWnd, ref WindowInfo nCmdShow);

    [DllImport("user32.dll")]
    internal static extern IntPtr GetActiveWindow();

    [DllImport("user32.dll", CharSet = CharSet.Auto)] 
    internal static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, IntPtr lParam);

    [DllImport("user32.dll")] 
    internal static extern long GetWindowLong(IntPtr hwnd, WindowData index);

    [DllImport("user32.dll")]
    internal static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);

    [DllImport("user32.dll", SetLastError = true)]
    internal static extern int SetWindowLong(IntPtr hwnd, int index, uint newStyle);

    [DllImport("user32.dll")]
    internal static extern bool SetWindowPos(IntPtr hwnd, IntPtr hwndInsertAfter, int x, int y, int width, int height, uint flags);

    [DllImport("user32.dll")]
    internal static extern bool ShowWindow(IntPtr hWnd, ShowWindowFlags nCmdShow);

    [DllImport("user32.dll")]
    internal static extern bool ShowWindowAsync(IntPtr hWnd, ShowWindowFlags nCmdShow);

    [DllImport("user32.dll")]
    internal static extern bool GetCursorPos(out Point lpPoint);

    [DllImport("user32.dll", SetLastError = true)]
    internal static extern bool GetWindowRect(IntPtr hWnd, ref Rect lpRect);

    [DllImport("user32.dll", SetLastError = true)]
    internal static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

    [DllImport("user32.dll")]
    internal static extern bool GetMonitorInfo(IntPtr hmonitor, ref MonitorInfo info);

    [DllImport("user32.dll")] 
    internal static extern int GetSystemMetrics(int nIndex);

    [DllImport("user32.dll")]
    internal static extern bool SystemParametersInfo(int nAction, int nParam, ref Rect rc, int nUpdate);

    [DllImport("user32.dll")]
    internal static extern IntPtr MonitorFromPoint(Point pt, int flags);

#endif

    [DllImport("user32.dll")]
    internal static extern bool EnumDisplayMonitors(HandleRef hdc, Rect rcClip, MonitorEnumProc lpfnEnum, IntPtr dwData);

    [DllImport("user32.dll")]
    internal static extern IntPtr MonitorFromWindow(HandleRef handle, int flags);
}
