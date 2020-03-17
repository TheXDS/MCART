/*
WinUi.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2020 César Andrés Morgan

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

using TheXDS.MCART.Attributes;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART
{
    /// <summary>
    /// Módulo de funciones universales de UI para sistemas operativos
    /// Microsoft® Windows®.
    /// </summary>
    public static class WinUi
    {
        [DllImport("kernel32.dll")] static extern IntPtr GetConsoleWindow();

        /// <summary>
        /// Obtiene un valor que indica si la aplicación tiene acceso a la
        /// consola.
        /// </summary>
        public static bool HasConsole => GetConsoleWindow() != IntPtr.Zero;

        /// <summary>
        /// Abre una consola para la aplicación.
        /// </summary>
        /// <returns><see langword="true"/> si la llamada obtuvo correctamente una consola, <see langword="false"/> en caso contrario.</returns>
        /// <remarks>
        /// Esta función es exclusiva para sistemas operativos Microsoft
        /// Windows®.
        /// </remarks>
        [DllImport("kernel32.dll")] public static extern bool AllocConsole();

        /// <summary>
        /// Libera la consola de la aplicación.
        /// </summary>
        /// <returns><see langword="true"/> si la llamada liberó correctamente la consola, <see langword="false"/> en caso contrario.</returns>
        /// <remarks>
        /// Esta función es exclusiva para sistemas operativos Microsoft
        /// Windows®.
        /// </remarks>
        [DllImport("kernel32.dll")] public static extern bool FreeConsole();

        /// <summary>
        /// Obtiene la información física del contexto del dispositivo gráfico
        /// especificado.
        /// </summary>
        /// <param name="hdc">Identificador de contexto a verificar.</param>
        /// <param name="nIndex">Propiedad a obtener.</param>
        /// <returns>
        /// Un <see cref="int"/> que representa el valor obtenido.
        /// </returns>
        /// <remarks>
        /// Esta función es exclusiva para sistemas operativos Microsoft
        /// Windows®.
        /// </remarks>
        [DllImport("gdi32.dll")] public static extern int GetDeviceCaps(IntPtr hdc, int nIndex);

        /// <summary>
        /// Obtiene el factor de escala de la interfaz gráfica.
        /// </summary>
        /// <returns>
        /// Un valor <see cref="float"/> que representa el factor de escala
        /// utilizado para dibujar la interfaz gráfica del sistema.
        /// </returns>
        [Sugar] public static float GetScalingFactor() => GetScalingFactor(IntPtr.Zero);

        /// <summary>
        /// Obtiene el factor de escala de la ventana especificada por
        /// el <see cref="IntPtr"/> <paramref name="hwnd"/>.
        /// </summary>
        /// <param name="hwnd">Identificador de ventana a verificar.</param>
        /// <returns>
        /// Un valor <see cref="float"/> que representa el factor de escala
        /// utilizado para dibujar la ventana especificada por 
        /// <paramref name="hwnd"/>.
        /// </returns>
        public static float GetScalingFactor(IntPtr hwnd)
        {
            var h = Graphics.FromHwnd(hwnd).GetHdc();
            return (float)GetDeviceCaps(h, 10) / GetDeviceCaps(h, 117);
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
        public static Point GetDpi(IntPtr hwnd)
        {
            var h = Graphics.FromHwnd(hwnd).GetHdc();
            return new Point(GetDeviceCaps(h, 88), GetDeviceCaps(h, 90));
        }

        /// <summary>
        /// Obtiene las resolución horizontal y vertical de la pantalla en
        /// DPI.
        /// </summary>
        /// <returns>
        /// Un <see cref="Point"/> que indica la resolución de la pantalla
        /// en Puntos Por Pulgada (DPI).
        /// </returns>
        [Sugar] public static Point GetDpi() => GetDpi(IntPtr.Zero);

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
    }
}