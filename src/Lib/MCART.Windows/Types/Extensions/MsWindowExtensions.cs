/*
MsWindowExtensions.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2026 César Andrés Morgan

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

using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using TheXDS.MCART.Component;
using TheXDS.MCART.PInvoke.Models;
using static TheXDS.MCART.PInvoke.DwmApi;
using static TheXDS.MCART.PInvoke.User32;

namespace TheXDS.MCART.Types.Extensions;

/// <summary>
/// Contains window management functions via Desktop Window Manager (DWM).
/// </summary>
[ExcludeFromCodeCoverage]
public static class MsWindowExtensions
{
    /// <summary>
    /// Disables all window effects.
    /// </summary>
    /// <param name="window">Window instance to blur.</param>
    public static void DisableEffects(this IMsWindow window)
    {
        SetWindowEffect(window, new AccentPolicy { AccentState = AccentState.ACCENT_DISABLED });
    }

    /// <summary>
    /// Enables blur effects on the window.
    /// </summary>
    /// <param name="window">Window instance to blur.</param>
    public static void EnableBlur(this IMsWindow window)
    {
        CheckHResult(DwmEnableBlurBehindWindow(window.Handle, new DWM_BLURBEHIND(true)));
        var accentPolicy = new AccentPolicy
        {
            AccentState = AccentState.ACCENT_ENABLE_BLURBEHIND | AccentState.ACCENT_ENABLE_GRADIENT,
            GradientColor = (152 << 24) | (0x2B2B2B & 0xFFFFFF),
        };
        SetWindowEffect(window, accentPolicy);
    }

    /// <summary>
    /// Enables acrylic effects on the window.
    /// </summary>
    /// <param name="window">
    /// Window instance in which to activate effects.
    /// </param>
    [SupportedOSPlatform("windows10.0.17134")]
    public static void EnableAcrylic(this IMsWindow window)
    {
        SetWindowEffect(window, new AccentPolicy { AccentState = AccentState.ACCENT_ENABLE_ACRYLICBLURBEHIND });
    }

    /// <summary>
    /// Enables Mica/Acrylic effects on the window.
    /// </summary>
    /// <param name="window">
    /// Window instance in which to activate Mica or Acrylic effects.
    /// </param>
    /// <remarks>
    /// This call will have no effect on Windows 10 16299 and earlier.  
    /// On Windows 10 17134 and later, you can use
    /// <see cref="EnableAcrylic(IMsWindow)"/> to enable an equivalent
    /// acrylic effect; the resulting window will be drawn with Windows 10
    /// borders, even if the application runs on Windows 11.
    /// </remarks>
    public static void EnableMicaIfSupported(this IMsWindow window)
    {
        if (OperatingSystem.IsWindowsVersionAtLeast(10, 0, 22523))
        {
            SetBackdropType(window, SystemBackdropType.MainWindow);
        }
        else if (OperatingSystem.IsWindowsVersionAtLeast(10, 0, 22000))
        {
            SetMica(window, true);
        }
        else if (OperatingSystem.IsWindowsVersionAtLeast(10, 0, 17134))
        {
            EnableAcrylic(window);
        }
    }

    /// <summary>
    /// Sets or clears the Mica effect on the window.
    /// </summary>
    /// <param name="window">
    /// Window instance to modify.
    /// </param>
    /// <param name="state">
    /// <c>true</c> to enable Mica; <c>false</c> to disable it.
    /// </param>
    /// <remarks>
    /// Available for Windows 11 systems from build 22000 until before 22523.
    /// To enable the effect for window backgrounds in Windows 11 22523
    /// and later, use <see cref="SetBackdropType(IMsWindow,
    /// SystemBackdropType)"/>.
    /// </remarks>
    /// <seealso cref="SetBackdropType(IMsWindow, SystemBackdropType)"/>
    [SupportedOSPlatform("windows10.0.22000")]
    [UnsupportedOSPlatform("windows10.0.22523")]
    public static void SetMica(this IMsWindow window, bool state)
    {
        SetWindowAttribute(window, DwmWindowAttribute.DWMWA_MICA, state ? 1 : 0);
    }

    /// <summary>
    /// Sets the type of backdrop to draw on the window.
    /// </summary>
    /// <param name="window">Window instance to modify.</param>
    /// <param name="backdropType">
    /// Type of backdrop to apply.
    /// </param>
    [SupportedOSPlatform("windows10.0.22523")]
    [CLSCompliant(false)]
    public static void SetBackdropType(this IMsWindow window, SystemBackdropType backdropType)
    {
        SetWindowAttribute(window, DwmWindowAttribute.DWMWA_SYSTEMBACKDROP_TYPE, backdropType);
    }

    /// <summary>
    /// Sets the corner rendering options for the window.
    /// </summary>
    /// <param name="window">
    /// Window for which to configure the corner rendering options.
    /// </param>
    /// <param name="cornerPreference">
    /// Border configuration to apply.
    /// </param>
    [SupportedOSPlatform("windows10.0.22000")]
    [CLSCompliant(false)]
    public static void SetCornerPreference(this IMsWindow window, WindowCornerPreference cornerPreference)
    {
        SetWindowAttribute(window, DwmWindowAttribute.DWMWA_WINDOW_CORNER_PREFERENCE, cornerPreference);
    }

    /// <summary>
    /// Enables or disables immersive dark mode for the window.
    /// </summary>
    /// <param name="window">
    /// Window on which to configure immersive dark mode.
    /// </param>
    /// <param name="state">
    /// true to enable immersive dark mode, false to disable.
    /// </param>
    [SupportedOSPlatform("windows10.0.19041")]
    public static void SetImmersiveDarkMode(this IMsWindow window, bool state)
    {
        SetWindowAttribute(window, DwmWindowAttribute.DWMWA_USE_IMMERSIVE_DARK_MODE, state ? 1 : 0);
    }

    /// <summary>
    /// Extends the window frame to include the client area.
    /// </summary>
    /// <param name="window">Window on which to extend the frame.</param>
    public static void ExtendFrameIntoClientArea(this IMsWindow window)
    {
        Margins margins = new(-1);
        CheckHResult(DwmExtendFrameIntoClientArea(window.Handle, ref margins));
    }

    /// <summary>
    /// Sets the window caption color.
    /// </summary>
    /// <param name="window">
    /// Window to set the caption color for.
    /// </param>
    /// <param name="color">Color to apply.</param>
    [SupportedOSPlatform("windows10.0.22000")]
    public static void SetCaptionColor(this IMsWindow window, Color color)
    {
        SetWindowAttribute<ColorRef>(window, DwmWindowAttribute.DWMWA_CAPTION_COLOR, color, Marshal.SizeOf(color));
    }

    /// <summary>
    /// Sets the window caption text color.
    /// </summary>
    /// <param name="window">
    /// Window to set the caption text color for.
    /// </param>
    /// <param name="color">Color to apply.</param>
    [SupportedOSPlatform("windows10.0.22000")]
    public static void SetCaptionTextColor(this IMsWindow window, Color color)
    {
        SetWindowAttribute<ColorRef>(window, DwmWindowAttribute.DWMWA_TEXT_COLOR, color, Marshal.SizeOf(color));
    }

    /// <summary>
    /// Sets an internal margin value for the content of a window.
    /// </summary>
    /// <param name="window">
    /// Window instance for which to configure the internal frame.
    /// </param>
    /// <param name="padding">
    /// Thickness of the window's internal margins.
    /// </param>
    public static void SetClientPadding(this IMsWindow window, Margins padding)
    {
        window.Padding = padding;
        window.SetFramePadding(padding);
    }

    /// <summary>
    /// Sets an internal margin value for the window frame.
    /// </summary>
    /// <param name="window">
    /// Window instance for which to configure the internal frame.
    /// </param>
    /// <param name="padding">
    /// Thickness of the window's internal margins.
    /// </param>
    public static void SetFramePadding(this IMsWindow window, Margins padding)
    {
        if (Helpers.Windows.IsCompositionEnabled())
            if (Marshal.GetExceptionForHR(DwmExtendFrameIntoClientArea(window.Handle, ref padding)) is { } ex) throw ex;
    }

    /// <summary>
    /// Disables and hides the window's close button.
    /// </summary>
    /// <param name="window">Window on which to perform the operation.</param>
    public static void HideClose(this IMsWindow window)
    {
        window.HideGwlStyle(WindowStyles.WS_SYSMENU);
    }

    /// <summary>
    /// Enables and shows the close button of the window.
    /// </summary>
    /// <param name="window">
    /// Window on which to perform the operation.
    /// </param>
    public static void ShowClose(this IMsWindow window)
    {
        window.ShowGwlStyle(WindowStyles.WS_SYSMENU);
    }

    /// <summary>
    /// Disables and hides the maximize button of the window.
    /// </summary>
    /// <param name="window">
    /// Window on which to perform the operation.
    /// </param>
    public static void HideMaximize(this IMsWindow window)
    {
        window.HideGwlStyle(WindowStyles.WS_MAXIMIZEBOX);
    }

    /// <summary>
    /// Enables and shows the maximize button of the window.
    /// </summary>
    /// <param name="window">
    /// Window on which to perform the operation.
    /// </param>
    public static void ShowMaximize(this IMsWindow window)
    {
        window.ShowGwlStyle(WindowStyles.WS_MAXIMIZEBOX);
    }

    /// <summary>
    /// Disables and hides the minimize button of the window.
    /// </summary>
    /// <param name="window">
    /// Window on which to perform the operation.
    /// </param>
    public static void HideMinimize(this IMsWindow window)
    {
        window.HideGwlStyle(WindowStyles.WS_MINIMIZEBOX);
    }

    /// <summary>
    /// Enables and shows the minimize button of the window.
    /// </summary>
    /// <param name="window">
    /// Window on which to perform the operation.
    /// </param>
    public static void ShowMinimize(this IMsWindow window)
    {
        window.ShowGwlStyle(WindowStyles.WS_MINIMIZEBOX);
    }

    /// <summary>
    /// Disables and hides the help button of the window.
    /// </summary>
    /// <param name="window">
    /// Window on which to perform the operation.
    /// </param>
    public static void HideHelp(this IMsWindow window)
    {
        window.SetWindowData(WindowData.GWL_EXSTYLE, p => p & ~WindowStyles.WS_EX_CONTEXTHELP);
    }

    /// <summary>
    /// Enables and shows the help button of the window.
    /// </summary>
    /// <param name="window">
    /// Window on which to perform the operation.
    /// </param>
    public static void ShowHelp(this IMsWindow window)
    {
        window.HideMinimize();
        window.HideMaximize();
        window.SetWindowData(WindowData.GWL_EXSTYLE, p => p | WindowStyles.WS_EX_CONTEXTHELP);
    }

    /// <summary>
    /// Hides the caption bar of the window.
    /// </summary>
    /// <param name="window">
    /// Window on which to perform the operation.
    /// </param>
    public static void HideCaption(this IMsWindow window)
    {
        window.HideGwlStyle(WindowStyles.WS_CAPTION);
    }

    /// <summary>
    /// Hides the border of the window.
    /// </summary>
    /// <param name="window">
    /// Window on which to perform the operation.
    /// </param>
    public static void HideBorder(this IMsWindow window)
    {
        window.HideGwlStyle(WindowStyles.WS_BORDER);
    }

    /// <summary>
    /// Shows the caption bar of the window.
    /// </summary>
    /// <param name="window">
    /// Window on which to perform the operation.
    /// </param>
    public static void ShowCaption(this IMsWindow window)
    {
        window.ShowGwlStyle(WindowStyles.WS_CAPTION);
    }

    /// <summary>
    /// Shows the border of the window.
    /// </summary>
    /// <param name="window">
    /// Window on which to perform the operation.
    /// </param>
    public static void ShowBorder(this IMsWindow window)
    {
        window.ShowGwlStyle(WindowStyles.WS_BORDER);
    }

    /// <summary>
    /// Changes the size of a window using the DWM API.
    /// </summary>
    /// <param name="window">
    /// Window on which to perform the operation.
    /// </param>
    /// <param name="newSize">New size of the window.</param>
    public static void Resize(this IMsWindow window, Size newSize)
    {
        SetWindowPos(window.Handle, IntPtr.Zero,
            0, 0,
            (int)newSize.Width, (int)newSize.Height,
            (uint)(WindowChanges.IgnoreMove | WindowChanges.IgnoreZOrder));
    }

    /// <summary>
    /// Moves a window using the DWM API.
    /// </summary>
    /// <param name="window">
    /// Window on which to perform the operation.
    /// </param>
    /// <param name="newPosition">New position of the window.</param>
    public static void Move(this IMsWindow window, Point newPosition)
    {
        SetWindowPos(window.Handle, IntPtr.Zero,
            (int)newPosition.X, (int)newPosition.Y,
            0, 0,
            (uint)(WindowChanges.IgnoreResize | WindowChanges.IgnoreZOrder));
    }

    /// <summary>
    /// Sends a notification to DWM about a window frame change.
    /// </summary>
    /// <param name="window">
    /// Window on which the frame change has been made.
    /// </param>
    public static void NotifyWindowFrameChange(this IMsWindow window)
    {
        SetWindowPos(window.Handle, IntPtr.Zero, 0, 0, 0, 0,
            (uint)(WindowChanges.IgnoreMove |
            WindowChanges.IgnoreResize |
            WindowChanges.IgnoreZOrder |
            WindowChanges.FrameChanged));
    }

    private unsafe static void SetWindowAttribute<T>(this IMsWindow window, DwmWindowAttribute attribute, T value) where T : unmanaged
    {
        SetWindowAttribute(window, attribute, value, sizeof(T));
    }

    private static void SetWindowAttribute<T>(this IMsWindow window, DwmWindowAttribute attribute, T value, int sizeOf)
    {
        var pinnedValue = GCHandle.Alloc(value, GCHandleType.Pinned);
        var valueAddr = pinnedValue.AddrOfPinnedObject();
        var result = DwmSetWindowAttribute(window.Handle, attribute, ref valueAddr, (uint)sizeOf);
        pinnedValue.Free();
        CheckHResult(result);
    }

    private static void SetWindowEffect(this IMsWindow window, AccentPolicy accent)
    {
        int accentStructSize = Marshal.SizeOf(accent);
        IntPtr accentPtr = Marshal.AllocHGlobal(accentStructSize);
        Marshal.StructureToPtr(accent, accentPtr, false);
        WindowCompositionAttributeData data = new()
        {
            Attribute = WindowCompositionAttribute.WCA_ACCENT_POLICY,
            SizeOfData = accentStructSize,
            Data = accentPtr
        };
        int v = SetWindowCompositionAttribute(window.Handle, ref data);
        Marshal.FreeHGlobal(accentPtr);
        if (Marshal.GetExceptionForHR(v) is { } ex) throw ex;
    }

    private static void ShowGwlStyle(this IMsWindow window, WindowStyles bit)
    {
        window.SetWindowData(WindowData.GWL_STYLE, p => p | bit);
    }

    private static void HideGwlStyle(this IMsWindow window, WindowStyles bit)
    {
        window.SetWindowData(WindowData.GWL_STYLE, p => p & ~bit);
    }

    private static void SetWindowData(this IMsWindow window, WindowData data, Func<WindowStyles, WindowStyles> transform)
    {
        if (SetWindowLongA(
            window.Handle,
            (int)data,
            (uint)transform((WindowStyles)GetWindowLong(window.Handle, data))) == 0)
        {
            int v = Marshal.GetHRForLastWin32Error();
            throw Marshal.GetExceptionForHR(v) ?? new Exception { HResult = v };
        }
    }

    private static int CheckHResult(int result)
    {
        if (Marshal.GetExceptionForHR(result) is { } ex) throw ex;
        return result;
    }
}
