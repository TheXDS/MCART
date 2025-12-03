/*
Windows.cs

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

using System.Drawing;
using System.Runtime.InteropServices;
using System.Security.Principal;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Component;
using TheXDS.MCART.PInvoke.Models;
using TheXDS.MCART.Types.Extensions;
using static TheXDS.MCART.PInvoke.DwmApi;
using static TheXDS.MCART.PInvoke.Gdi32;
using static TheXDS.MCART.PInvoke.Kernel32;
using static TheXDS.MCART.PInvoke.User32;

namespace TheXDS.MCART.Helpers;

/// <summary>
/// Contains a set of helper methods for the Microsoft Windows API.
/// </summary>
public static class Windows
{
    private static WindowsInfo? _winInfo;

    /// <summary>
    /// Obtains an object that exposes varied information about
    /// Windows.
    /// </summary>
    public static WindowsInfo Info => _winInfo ??= new WindowsInfo();

    /// <summary>
    /// Checks whether the current execution context of the
    /// application contains administrative permissions.
    /// </summary>
    /// <returns>
    /// <see langword="true"/> if the application runs with
    /// administrative privileges, otherwise <see langword="false"/>.
    /// </returns>
    public static bool IsAdministrator()
    {
        using WindowsIdentity identity = WindowsIdentity.GetCurrent();
        WindowsPrincipal principal = new(identity);
        return principal.IsInRole(WindowsBuiltInRole.Administrator);
    }

    /// <summary>
    /// Releases a COM object.
    /// </summary>
    /// <param name="obj">COM object to release.</param>
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

    /// <summary>
    /// Opens a console for the application.
    /// </summary>
    /// <returns>
    /// <see langword="true"/> if a console was successfully allocated,
    /// otherwise <see langword="false"/>.
    /// </returns>
    /// <remarks>
    /// This function is exclusive to Microsoft Windows® operating
    /// systems.
    /// </remarks>
    public static bool TryAllocateConsole() => AllocConsole();

    /// <summary>
    /// Frees the console of the application.
    /// </summary>
    /// <returns>
    /// <see langword="true"/> if the console was successfully freed,
    /// otherwise <see langword="false"/>.
    /// </returns>
    /// <remarks>
    /// This function is exclusive to Microsoft Windows® operating
    /// systems.
    /// </remarks>
    public static bool TryFreeConsole() => FreeConsole();

    /// <summary>
    /// Obtains an object that allows controlling the console window.
    /// </summary>
    /// <returns>
    /// An object that controls the console window.
    /// </returns>
    public static ConsoleWindow GetConsoleWindow() => new();

    /// <summary>
    /// Obtains the GUI scaling factor.
    /// </summary>
    /// <returns>
    /// A <see cref="float"/> that represents the system GUI scaling
    /// factor.
    /// </returns>
    [Sugar] public static float GetScalingFactor() => GetScalingFactor(IntPtr.Zero);

    /// <summary>
    /// Obtains the scaling factor of the specified window.
    /// </summary>
    /// <param name="window">Window to check.</param>
    /// <returns>
    /// A <see cref="float"/> representing the scaling factor used
    /// to draw the specified window.
    /// </returns>
    public static float GetScalingFactor(this IMsWindow window)
    {
        return GetScalingFactor(window.Handle);
    }

    /// <summary>
    /// Obtains the horizontal screen resolution in DPI.
    /// </summary>
    /// <returns>
    /// An integer indicating the horizontal screen resolution in
    /// Dots Per Inch (DPI).
    /// </returns>
    [Sugar] public static int GetXDpi() => GetXDpi(IntPtr.Zero);

    /// <summary>
    /// Gets the vertical screen resolution in DPI.
    /// </summary>
    /// <returns>
    /// An integer indicating the vertical screen resolution in
    /// Dots Per Inch (DPI).
    /// </returns>
    [Sugar] public static int GetYDpi() => GetXDpi(IntPtr.Zero);

    /// <summary>
    /// Gets the horizontal resolution of the window in DPI.
    /// </summary>
    /// <param name="hwnd">Window identifier to check.</param>
    /// <returns>
    /// An integer indicating the horizontal window resolution in
    /// Dots Per Inch (DPI).
    /// </returns>
    public static int GetXDpi(IntPtr hwnd) => GetDeviceCaps(Graphics.FromHwnd(hwnd).GetHdc(), 88);

    /// <summary>
    /// Gets the vertical resolution of the window in DPI.
    /// </summary>
    /// <param name="hwnd">Window identifier to check.</param>
    /// <returns>
    /// An integer indicating the vertical window resolution in
    /// Dots Per Inch (DPI).
    /// </returns>
    public static int GetYDpi(IntPtr hwnd) => GetDeviceCaps(Graphics.FromHwnd(hwnd).GetHdc(), 90);

    /// <summary>
    /// Gets the horizontal and vertical resolution of the window in DPI.
    /// </summary>
    /// <param name="hwnd">Window identifier to check.</param>
    /// <returns>
    /// A <see cref="Types.Size"/> that indicates the window resolution
    /// in Dots Per Inch (DPI).
    /// </returns>
    public static Types.Size GetDpi(IntPtr hwnd)
    {
        IntPtr h = Graphics.FromHwnd(hwnd).GetHdc();
        return new Types.Size(GetDeviceCaps(h, 88), GetDeviceCaps(h, 90));
    }

    /// <summary>
    /// Gets the horizontal and vertical screen resolution in DPI.
    /// </summary>
    /// <returns>
    /// A <see cref="Types.Size"/> that indicates the screen resolution
    /// in Dots Per Inch (DPI).
    /// </returns>
    [Sugar] public static Types.Size GetDpi() => GetDpi(IntPtr.Zero);

    /// <summary>
    /// Returns a randomly chosen drawing brush.
    /// </summary>
    /// <returns>
    /// A <see cref="Brush"/> selected at random.
    /// </returns>
    public static Brush PickDrawingBrush()
    {
        return (Brush)typeof(Brushes).GetProperties().Pick().GetValue(null)!;
    }

    /// <summary>
    /// Gets the absolute cursor coordinates on the screen.
    /// </summary>
    /// <returns>
    /// A <see cref="Types.Point"/> containing the cursor's absolute
    /// screen coordinates.
    /// </returns>
    public static Types.Point GetCursorPosition()
    {
        return GetCursorPos(out PInvoke.Models.Point p) ? p : Types.Point.Nowhere;
    }

    /// <summary>
    /// Gets the current Aero accent color of Windows.
    /// </summary>
    /// <returns>
    /// The current Aero accent color as a <see cref="Types.Color"/>.
    /// </returns>
    public static Types.Color GetAeroAccentColor()
    {
        if (DwmGetColorizationColor(out var color, out var transparent) != 0) return Color.Transparent;
        var c = new Types.Abgr32ColorParser().From((int)color);
        if (!transparent) c.A = 255;
        return c;
    }

    /// <summary>
    /// Attempts to delete a Windows object by its specific handle.
    /// </summary>
    /// <param name="hwnd">Handle of the Windows object to delete.</param>
    /// <returns>
    /// <see langword="true"/> if the operation succeeded; otherwise,
    /// <see langword="false"/>.
    /// </returns>
    public static bool TryDeleteObject(IntPtr hwnd)
    {
        return DeleteObject(hwnd);
    }

    /// <summary>
    /// Checks whether window composition is available on the system.
    /// </summary>
    /// <returns>
    /// <see langword="true"/> if window composition is enabled;
    /// otherwise, <see langword="false"/>.
    /// </returns>
    public static bool IsCompositionEnabled()
    {
        return DwmIsCompositionEnabled();
    }

    private static float GetScalingFactor(IntPtr handle)
    {
        IntPtr h = Graphics.FromHwnd(handle).GetHdc();
        return (float)GetDeviceCaps(h, 10) / GetDeviceCaps(h, 117);
    }
}
