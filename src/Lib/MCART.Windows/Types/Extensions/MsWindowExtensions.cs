/*
MsWindowExtensions.cs

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

using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using TheXDS.MCART.Component;
using TheXDS.MCART.PInvoke.Models;
using static TheXDS.MCART.PInvoke.DwmApi;
using static TheXDS.MCART.PInvoke.User32;

namespace TheXDS.MCART.Types.Extensions;

/// <summary>
/// Contiene funciones de gestión de ventanas por medio de Desktop Window Manager (DWM).
/// </summary>
[ExcludeFromCodeCoverage]
public static class MsWindowExtensions
{
    /// <summary>
    /// Deshabilita todos los efectos de la ventana.
    /// </summary>
    /// <param name="window">Instancia de ventana a difuminar.</param>
    public static void DisableEffects(this IMsWindow window)
    {
        SetWindowEffect(window, new AccentPolicy { AccentState = AccentState.ACCENT_DISABLED });
    }

    /// <summary>
    /// Habilita los efectos de difuminado en la ventana.
    /// </summary>
    /// <param name="window">Instancia de ventana a difuminar.</param>
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
    /// Habilita los efectos acrílicos en la ventana.
    /// </summary>
    /// <param name="window">
    /// Instancia de ventana en la cual activar los efectos.
    /// </param>
    [SupportedOSPlatform("windows10.0.17134")]
    public static void EnableAcrylic(this IMsWindow window)
    {
        SetWindowEffect(window, new AccentPolicy { AccentState = AccentState.ACCENT_ENABLE_ACRYLICBLURBEHIND });
    }

    /// <summary>
    /// Habilita los efectos de Mica/Acrylic en la ventana.
    /// </summary>
    /// <param name="window">
    /// Ventana en la cual habilitar el efecto de Mica/Acrylic.
    /// </param>
    /// <remarks>
    /// Esta llamada no tendrá efecto en sistemas operativos Windows 10 16299 y
    /// anteriores. En Windows 10 17134 en adelante, puede utilizar
    /// <see cref="EnableAcrylic(IMsWindow)"/> para habilitar un efecto
    /// acrílico equivalente, en cuyo caso la ventana resultante se dibujará
    /// con bordes de Windows 10, incluso si la aplicación se ejecuta en
    /// Windows 11.
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
    /// Habilita o deshabilita los efectos de Mica en la ventana.
    /// </summary>
    /// <param name="window">
    /// Ventana en la cual habilitar o deshabilitar el efecto de Mica.
    /// </param>
    /// <param name="state">
    /// <see langword="true"/> para habilitar el efecto de Mica en la ventana,
    /// <see langword="false"/> para deshabilitarlo.
    /// </param>
    /// <remarks>
    /// Esta llamada se encuentra disponible para sistemas Windows 11, desde
    /// 22000 hasta antes de 22523. Para habilitar el efecto para el fondo de
    /// las ventanas en Windows 11 22523 en adelante, utilice
    /// <see cref="SetBackdropType(IMsWindow, SystemBackdropType)"/>.
    /// </remarks>
    /// <seealso cref="SetBackdropType(IMsWindow, SystemBackdropType)"/>.
    [SupportedOSPlatform("windows10.0.22000")]
    [UnsupportedOSPlatform("windows10.0.22523")]
    public static void SetMica(this IMsWindow window, bool state)
    {
        SetWindowAttribute(window, DwmWindowAttribute.DWMWA_MICA, state ? 1 : 0);
    }

    /// <summary>
    /// Establece un tipo de fondo a dibujar en la ventana.
    /// </summary>
    /// <param name="window">Ventana a configurar.</param>
    /// <param name="backdropType">Tipo de fondo a dibujar.</param>
    [SupportedOSPlatform("windows10.0.22523")]
    [CLSCompliant(false)]
    public static void SetBackdropType(this IMsWindow window, SystemBackdropType backdropType)
    {
        SetWindowAttribute(window, DwmWindowAttribute.DWMWA_SYSTEMBACKDROP_TYPE, backdropType);
    }

    /// <summary>
    /// Establece las opciones de dibujado de esquinas en la ventana.
    /// </summary>
    /// <param name="window">
    /// Ventana para la cual configurar el dibujado de bordes.
    /// </param>
    /// <param name="cornerPreference">
    /// Configuración de bordes a aplicar.
    /// </param>
    [SupportedOSPlatform("windows10.0.22000")]
    [CLSCompliant(false)]
    public static void SetCornerPreference(this IMsWindow window, WindowCornerPreference cornerPreference)
    {
        SetWindowAttribute(window, DwmWindowAttribute.DWMWA_WINDOW_CORNER_PREFERENCE, cornerPreference);
    }

    /// <summary>
    /// Habilita o deshabilita el tema oscuro inmersivo dentro de la ventana.
    /// </summary>
    /// <param name="window">
    /// Ventana sobre la cual configurar el tema de modo oscuro inmersivo.
    /// </param>
    /// <param name="state">
    /// <see langword="true"/> para habilitar el tema oscuro inmersivo,
    /// <see langword="false"/> para deshabilitarlo.
    /// </param>
    [SupportedOSPlatform("windows10.0.19041")]
    public static void SetImmersiveDarkMode(this IMsWindow window, bool state)
    {
        SetWindowAttribute(window, DwmWindowAttribute.DWMWA_USE_IMMERSIVE_DARK_MODE, state ? 1 : 0);
    }

    /// <summary>
    /// Extiende el borde de la ventana para incluir el área de cliente de la
    /// ventana.
    /// </summary>
    /// <param name="window">Ventana sobre la cual extender el borde.</param>
    public static void ExtendFrameIntoClientArea(this IMsWindow window)
    {
        Margins margins = new(-1);
        CheckHResult(DwmExtendFrameIntoClientArea(window.Handle, ref margins));
    }

    /// <summary>
    /// Establece el color de título de la ventana.
    /// </summary>
    /// <param name="window">
    /// Ventana a la cual establecerle el color de título.
    /// </param>
    /// <param name="color">Color a establecer.</param>
    [SupportedOSPlatform("windows10.0.22000")]
    public static void SetCaptionColor(this IMsWindow window, Color color)
    {
        SetWindowAttribute<ColorRef>(window, DwmWindowAttribute.DWMWA_CAPTION_COLOR, color, Marshal.SizeOf(color));
    }

    /// <summary>
    /// Establece el color de texto de título de la ventana.
    /// </summary>
    /// <param name="window">
    /// Ventana a la cual establecerle el color de texto de título.
    /// </param>
    /// <param name="color">Color a establecer.</param>
    [SupportedOSPlatform("windows10.0.22000")]
    public static void SetCaptionTextColor(this IMsWindow window, Color color)
    {
        SetWindowAttribute<ColorRef>(window, DwmWindowAttribute.DWMWA_TEXT_COLOR, color, Marshal.SizeOf(color));
    }

    /// <summary>
    /// Establece un valor de margen interno para el contenido de una
    /// ventana.
    /// </summary>
    /// <param name="window">
    /// Instancia de ventana para la cual configurar el marco interno.
    /// </param>
    /// <param name="padding">
    /// Grosor de los márgenes internos de la ventana.
    /// </param>
    public static void SetClientPadding(this IMsWindow window, Margins padding)
    {
        window.Padding = padding;
        window.SetFramePadding(padding);
    }

    /// <summary>
    /// Establece un valor de margen interno para el recuadro de una
    /// ventana.
    /// </summary>
    /// <param name="window">
    /// Instancia de ventana para la cual configurar el marco interno.
    /// </param>
    /// <param name="padding">
    /// Grosor de los márgenes internos de la ventana.
    /// </param>
    public static void SetFramePadding(this IMsWindow window, Margins padding)
    {
        if (Helpers.Windows.IsCompositionEnabled())
            if (Marshal.GetExceptionForHR(DwmExtendFrameIntoClientArea(window.Handle, ref padding)) is { } ex) throw ex;
    }

    /// <summary>
    /// Deshabilita y oculta el botón de cerrar de la ventana.
    /// </summary>
    /// <param name="window">
    /// Ventana sobre la cual realizar la operación.
    /// </param>
    public static void HideClose(this IMsWindow window)
    {
        window.HideGwlStyle(WindowStyles.WS_SYSMENU);
    }

    /// <summary>
    /// Habilita y muestra el botón de cerrar de la ventana.
    /// </summary>
    /// <param name="window">
    /// Ventana sobre la cual realizar la operación.
    /// </param>
    public static void ShowClose(this IMsWindow window)
    {
        window.ShowGwlStyle(WindowStyles.WS_SYSMENU);
    }

    /// <summary>
    /// Deshabilita y oculta el botón de maximizar de la ventana.
    /// </summary>
    /// <param name="window">
    /// Ventana sobre la cual realizar la operación.
    /// </param>
    public static void HideMaximize(this IMsWindow window)
    {
        window.HideGwlStyle(WindowStyles.WS_MAXIMIZEBOX);
    }

    /// <summary>
    /// Habilita y muestra el botón de maximizar de la ventana.
    /// </summary>
    /// <param name="window">
    /// Ventana sobre la cual realizar la operación.
    /// </param>
    public static void ShowMaximize(this IMsWindow window)
    {
        window.ShowGwlStyle(WindowStyles.WS_MAXIMIZEBOX);
    }

    /// <summary>
    /// Deshabilita y oculta el botón de minimizar de la ventana.
    /// </summary>
    /// <param name="window">
    /// Ventana sobre la cual realizar la operación.
    /// </param>
    public static void HideMinimize(this IMsWindow window)
    {
        window.HideGwlStyle(WindowStyles.WS_MINIMIZEBOX);
    }

    /// <summary>
    /// Habilita y muestra el botón de minimizar de la ventana.
    /// </summary>
    /// <param name="window">
    /// Ventana sobre la cual realizar la operación.
    /// </param>
    public static void ShowMinimize(this IMsWindow window)
    {
        window.ShowGwlStyle(WindowStyles.WS_MINIMIZEBOX);
    }

    /// <summary>
    /// Deshabilita y oculta el botón de ayuda de la ventana.
    /// </summary>
    /// <param name="window">
    /// Ventana sobre la cual realizar la operación.
    /// </param>
    public static void HideHelp(this IMsWindow window)
    {
        window.SetWindowData(WindowData.GWL_EXSTYLE, p => p & ~WindowStyles.WS_EX_CONTEXTHELP);
    }

    /// <summary>
    /// Habilita y muestra el botón de ayuda de la ventana.
    /// </summary>
    /// <param name="window">
    /// Ventana sobre la cual realizar la operación.
    /// </param>
    public static void ShowHelp(this IMsWindow window)
    {
        window.HideMinimize();
        window.HideMaximize();
        window.SetWindowData(WindowData.GWL_EXSTYLE, p => p | WindowStyles.WS_EX_CONTEXTHELP);
    }

    /// <summary>
    /// Oculta el texto de título de la ventana.
    /// </summary>
    /// <param name="window">
    /// Ventana sobre la cual realizar la operación.
    /// </param>
    public static void HideCaption(this IMsWindow window)
    {
        window.HideGwlStyle(WindowStyles.WS_CAPTION);
    }

    /// <summary>
    /// Oculta el borde de la ventana.
    /// </summary>
    /// <param name="window">
    /// Ventana sobre la cual realizar la operación.
    /// </param>
    public static void HideBorder(this IMsWindow window)
    {
        window.HideGwlStyle(WindowStyles.WS_BORDER);
    }

    /// <summary>
    /// Muestra es texto de título de la ventana.
    /// </summary>
    /// <param name="window">
    /// Ventana sobre la cual realizar la operación.
    /// </param>
    public static void ShowCaption(this IMsWindow window)
    {
        window.ShowGwlStyle(WindowStyles.WS_CAPTION);
    }

    /// <summary>
    /// Muestra el borde de la ventana.
    /// </summary>
    /// <param name="window">
    /// Ventana sobre la cual realizar la operación.
    /// </param>
    public static void ShowBorder(this IMsWindow window)
    {
        window.ShowGwlStyle(WindowStyles.WS_BORDER);
    }

    /// <summary>
    /// Cambia el tamaño de una ventana por medio de la API de DWM.
    /// </summary>
    /// <param name="window">
    /// Ventana sobre la cual realizar la operación.
    /// </param>
    /// <param name="newSize">Tamaño nuevo de la ventana.</param>
    public static void Resize(this IMsWindow window, Size newSize)
    {
        SetWindowPos(window.Handle, IntPtr.Zero,
            0, 0,
            (int)newSize.Width, (int)newSize.Height,
            (uint)(WindowChanges.IgnoreMove | WindowChanges.IgnoreZOrder));
    }

    /// <summary>
    /// Mueve una ventana por medio de la API de DWM.
    /// </summary>
    /// <param name="window">
    /// Ventana sobre la cual realizar la operación.
    /// </param>
    /// <param name="newPosition">Nueva posición de la ventana.</param>
    public static void Move(this IMsWindow window, Point newPosition)
    {
        SetWindowPos(window.Handle, IntPtr.Zero,
            (int)newPosition.X, (int)newPosition.Y,
            0, 0,
            (uint)(WindowChanges.IgnoreResize | WindowChanges.IgnoreZOrder));
    }

    /// <summary>
    /// Envía una notificación a DWM sobre un cambio en el marco de la ventana.
    /// </summary>
    /// <param name="window">
    /// Ventana sobre la cual se ha realizado el cambio en el marco.
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
