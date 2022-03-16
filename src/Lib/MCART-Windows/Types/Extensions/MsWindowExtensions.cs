/*
MsWindowExtensions.cs

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

namespace TheXDS.MCART.Types.Extensions;
using System;
using System.Runtime.InteropServices;
using TheXDS.MCART.Component;
using TheXDS.MCART.PInvoke.Structs;
using static TheXDS.MCART.PInvoke.DwmApi;
using static TheXDS.MCART.PInvoke.User32;

/// <summary>
/// Contiene funciones de gestión de ventanas por medio de Desktop Window Manager (DWM).
/// </summary>
public static class MsWindowExtensions
{
    /// <summary>
    /// Comprueba si la composición de ventanas está disponible en el
    /// sistema.
    /// </summary>
    /// <returns>
    /// <see langword="true"/> si la composición de ventanas está
    /// disponible, <see langword="false"/> en caso contrario.
    /// </returns>
    public static bool IsCompositionEnabled()
    {
        return DwmIsCompositionEnabled();
    }

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
        SetWindowEffect(window, new AccentPolicy { AccentState = AccentState.ACCENT_ENABLE_BLURBEHIND });
    }

    /// <summary>
    /// Habilita los efectos acrílicos en la ventana.
    /// </summary>
    /// <param name="window">
    /// Instancia de ventana en la cual activar los efectos.
    /// </param>
    public static void EnableAcrylic(this IMsWindow window)
    {
        SetWindowEffect(window, new AccentPolicy { AccentState = AccentState.ACCENT_ENABLE_ACRYLICBLURBEHIND });
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
        if (IsCompositionEnabled())
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
    public static void Move(this IMsWindow window, Types.Point newPosition)
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
        if (SetWindowLong(
            window.Handle,
            (int)data,
            (uint)transform((WindowStyles)GetWindowLong(window.Handle, data))) == 0)
        {
            int v = Marshal.GetHRForLastWin32Error();
            throw Marshal.GetExceptionForHR(v) ?? new Exception { HResult = v };
        }
    }
}
