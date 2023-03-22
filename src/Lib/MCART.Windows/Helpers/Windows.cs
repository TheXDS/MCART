/*
Windows.cs

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

using System.Drawing;
using System.Runtime.InteropServices;
using System.Security.Principal;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Component;
using TheXDS.MCART.PInvoke.Structs;
using TheXDS.MCART.Types.Extensions;
using static TheXDS.MCART.PInvoke.DwmApi;
using static TheXDS.MCART.PInvoke.Gdi32;
using static TheXDS.MCART.PInvoke.Kernel32;
using static TheXDS.MCART.PInvoke.User32;

namespace TheXDS.MCART.Helpers;

/// <summary>
/// Contiene una serie de métodos auxiliares de la API de Microsoft
/// Windows.
/// </summary>
public static class Windows
{
    private static WindowsInfo? _winInfo;

    /// <summary>
    /// Obtiene un objeto que expone información variada acerca de Windows.
    /// </summary>
    public static WindowsInfo Info => _winInfo ??= new WindowsInfo();

    /// <summary>
    /// Comprueba si el contexto de ejecución actual de la aplicación
    /// contiene permisos administrativos.
    /// </summary>
    /// <returns>
    /// <see langword="true"/> si la aplicación está siendo ejecutada con
    /// permisos administrativos, <see langword="false"/> en caso
    /// contrario.
    /// </returns>
    public static bool IsAdministrator()
    {
        using WindowsIdentity identity = WindowsIdentity.GetCurrent();
        WindowsPrincipal principal = new(identity);
        return principal.IsInRole(WindowsBuiltInRole.Administrator);
    }

    /// <summary>
    /// Libera un objeto COM.
    /// </summary>
    /// <param name="obj">Objeto COM a liberar.</param>
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
    /// Abre una consola para la aplicación.
    /// </summary>
    /// <returns><see langword="true"/> si la llamada obtuvo correctamente una consola, <see langword="false"/> en caso contrario.</returns>
    /// <remarks>
    /// Esta función es exclusiva para sistemas operativos Microsoft
    /// Windows®.
    /// </remarks>
    public static bool TryAllocateConsole() => AllocConsole();

    /// <summary>
    /// Libera la consola de la aplicación.
    /// </summary>
    /// <returns><see langword="true"/> si la llamada liberó correctamente la consola, <see langword="false"/> en caso contrario.</returns>
    /// <remarks>
    /// Esta función es exclusiva para sistemas operativos Microsoft
    /// Windows®.
    /// </remarks>
    public static bool TryFreeConsole() => FreeConsole();

    /// <summary>
    /// Obtiene un objeto que permite controlar la ventana de la consola.
    /// </summary>
    /// <returns>
    /// Un objeto que permite controlar la ventana de la consola.
    /// </returns>
    public static ConsoleWindow GetConsoleWindow() => new();

    /// <summary>
    /// Obtiene el factor de escala de la interfaz gráfica.
    /// </summary>
    /// <returns>
    /// Un valor <see cref="float"/> que representa el factor de escala
    /// utilizado para dibujar la interfaz gráfica del sistema.
    /// </returns>
    [Sugar] public static float GetScalingFactor() => GetScalingFactor(IntPtr.Zero);

    /// <summary>
    /// Obtiene el factor de escala de la ventana especificada.
    /// </summary>
    /// <param name="window">Ventana a verificar.</param>
    /// <returns>
    /// Un valor <see cref="float"/> que representa el factor de escala
    /// utilizado para dibujar la ventana especificada.
    /// </returns>
    public static float GetScalingFactor(this IMsWindow window)
    {
        return GetScalingFactor(window.Handle);
    }

    /// <summary>
    /// Obtiene la resolución horizontal de la pantalla en DPI.
    /// </summary>
    /// <returns>
    /// Un valor entero que indica la resolución horizontal de la
    /// pantalla en Puntos Por Pulgada (DPI).
    /// </returns>
    [Sugar] public static int GetXDpi() => GetXDpi(IntPtr.Zero);

    /// <summary>
    /// Obtiene la resolución vertical de la pantalla en DPI.
    /// </summary>
    /// <returns>
    /// Un valor entero que indica la resolución vertical de la
    /// pantalla en Puntos Por Pulgada (DPI).
    /// </returns>
    [Sugar] public static int GetYDpi() => GetXDpi(IntPtr.Zero);

    /// <summary>
    /// Obtiene la resolución horizontal de la ventana en DPI.
    /// </summary>
    /// <param name="hwnd">Identificador de ventana a verificar.</param>
    /// <returns>
    /// Un valor entero que indica la resolución horizontal de la
    /// ventana  en Puntos Por Pulgada (DPI).
    /// </returns>
    public static int GetXDpi(IntPtr hwnd) => GetDeviceCaps(Graphics.FromHwnd(hwnd).GetHdc(), 88);

    /// <summary>
    /// Obtiene la resolución vertical de la ventana en DPI.
    /// </summary>
    /// <param name="hwnd">Identificador de ventana a verificar.</param>
    /// <returns>
    /// Un valor entero que indica la resolución vertical de la ventana
    /// en Puntos Por Pulgada (DPI).
    /// </returns>
    public static int GetYDpi(IntPtr hwnd) => GetDeviceCaps(Graphics.FromHwnd(hwnd).GetHdc(), 90);

    /// <summary>
    /// Obtiene las resolución horizontal y vertical de la ventana en 
    /// DPI.
    /// </summary>
    /// <param name="hwnd">Identificador de ventana a verificar.</param>
    /// <returns>
    /// Un <see cref="Point"/> que indica la resolución de la ventana
    /// en Puntos Por Pulgada (DPI).
    /// </returns>
    public static Types.Size GetDpi(IntPtr hwnd)
    {
        IntPtr h = Graphics.FromHwnd(hwnd).GetHdc();
        return new Types.Size(GetDeviceCaps(h, 88), GetDeviceCaps(h, 90));
    }

    /// <summary>
    /// Obtiene las resolución horizontal y vertical de la pantalla en
    /// DPI.
    /// </summary>
    /// <returns>
    /// Un <see cref="Point"/> que indica la resolución de la pantalla
    /// en Puntos Por Pulgada (DPI).
    /// </returns>
    [Sugar] public static Types.Size GetDpi() => GetDpi(IntPtr.Zero);

    /// <summary>
    /// Devuelve un <see cref="Brush"/> aleatorio.
    /// </summary>
    /// <returns>
    /// Un <see cref="Brush"/> seleccionado aleatoriamente.
    /// </returns>
    public static Brush PickDrawingBrush()
    {
        return (Brush)typeof(Brushes).GetProperties().Pick().GetValue(null)!;
    }

    /// <summary>
    /// Obtiene las coordenadas absolutas del cursor en la pantalla.
    /// </summary>
    /// <returns>
    /// Un <see cref="Types.Point"/> con las coordenadas absolutas del
    /// cursor en la pantalla.
    /// </returns>
    public static Types.Point GetCursorPosition()
    {
        return GetCursorPos(out PInvoke.Structs.Point p) ? p : Types.Point.Nowhere;
    }

    /// <summary>
    /// Obtiene el color de acento de las ventanas establecido actualmente.
    /// </summary>
    /// <returns></returns>
    public static Types.Color GetAeroAccentColor()
    {
        if (PInvoke.DwmApi.DwmGetColorizationColor(out var color, out var transparent) != 0) return Color.Transparent;
        var c = new Types.Abgr32ColorParser().From((int)color);
        if (!transparent) c.A = 255;
        return c;
    }

    /// <summary>
    /// Intenta borrar un objeto dado su Handle específico.
    /// </summary>
    /// <param name="hwnd">
    /// Handle del objeto de Windows a borrar.
    /// </param>
    /// <returns>
    /// <see langword="true"/> si la operación ha sido exitosa,
    /// <see langword="false"/> en caso contrario.
    /// </returns>
    public static bool TryDeleteObject(IntPtr hwnd)
    {
        return DeleteObject(hwnd);
    }

    private static float GetScalingFactor(IntPtr handle)
    {
        IntPtr h = Graphics.FromHwnd(handle).GetHdc();
        return (float)GetDeviceCaps(h, 10) / GetDeviceCaps(h, 117);
    }

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
}
