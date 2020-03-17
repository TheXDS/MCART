/*
Dwm.cs

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

using System;
using System.Runtime.InteropServices;
using TheXDS.MCART.Windows.Component;
using TheXDS.MCART.Windows.Dwm.Structs;

namespace TheXDS.MCART.Windows.Dwm
{
    /// <summary>
    /// Contiene funciones de gestión de ventanas por medio de Desktop Window Manager (DWM).
    /// </summary>
    public static class Dwm
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
            return PInvoke.DwmIsCompositionEnabled();
        }

        /// <summary>
        /// Deshabilita todos los efectos de la ventana.
        /// </summary>
        /// <param name="window">Instancia de ventana a difuminar.</param>
        public static void DisableEffects(this IWindow window)
        {
            SetWindowEffect(window.Handle, new AccentPolicy { AccentState = AccentState.ACCENT_DISABLED });
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
        public static void SetClientPadding(this IWindow window, Margins padding)
        {
            window.Padding = padding;
            SetFramePadding(window, padding);
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
        public static void SetFramePadding(this IWindow window, Margins padding)
        {
            if (IsCompositionEnabled())
            {
                PInvoke.DwmExtendFrameIntoClientArea(window.Handle, ref padding);
            }
        }

        /// <summary>
        /// Habilita los efectos de difuminado en la ventana.
        /// </summary>
        /// <param name="window">Instancia de ventana a difuminar.</param>
        public static void EnableBlur(this IWindow window)
        {
            SetWindowEffect(window.Handle, new AccentPolicy { AccentState = AccentState.ACCENT_ENABLE_BLURBEHIND });
        }

        /// <summary>
        /// Deshabilita y oculta el botón de cerrar de la ventana.
        /// </summary>
        /// <param name="window">
        /// Ventana sobre la cual realizar la operación.
        /// </param>
        public static void HideClose(this IWindow window)
        {
            HideGwlStyle(window, WindowStyles.WS_SYSMENU);
        }

        /// <summary>
        /// Habilita y muestra el botón de cerrar de la ventana.
        /// </summary>
        /// <param name="window">
        /// Ventana sobre la cual realizar la operación.
        /// </param>
        public static void ShowClose(this IWindow window)
        {
            ShowGwlStyle(window, WindowStyles.WS_SYSMENU);
        }

        /// <summary>
        /// Deshabilita y oculta el botón de maximizar de la ventana.
        /// </summary>
        /// <param name="window">
        /// Ventana sobre la cual realizar la operación.
        /// </param>
        public static void HideMaximize(this IWindow window)
        {
            HideGwlStyle(window, WindowStyles.WS_MAXIMIZEBOX);
        }

        /// <summary>
        /// Habilita y muestra el botón de maximizar de la ventana.
        /// </summary>
        /// <param name="window">
        /// Ventana sobre la cual realizar la operación.
        /// </param>
        public static void ShowMaximize(this IWindow window)
        {
            ShowGwlStyle(window, WindowStyles.WS_MAXIMIZEBOX);
        }
        
        /// <summary>
        /// Deshabilita y oculta el botón de minimizar de la ventana.
        /// </summary>
        /// <param name="window">
        /// Ventana sobre la cual realizar la operación.
        /// </param>
        public static void HideMinimize(this IWindow window)
        {
            HideGwlStyle(window, WindowStyles.WS_MINIMIZEBOX);
        }

        /// <summary>
        /// Habilita y muestra el botón de minimizar de la ventana.
        /// </summary>
        /// <param name="window">
        /// Ventana sobre la cual realizar la operación.
        /// </param>
        public static void ShowMinimize(this IWindow window)
        {
            ShowGwlStyle(window, WindowStyles.WS_MINIMIZEBOX);
        }

        /// <summary>
        /// Envía una notificación a DWM sobre un cambio en el marco de la ventana.
        /// </summary>
        /// <param name="window">
        /// Ventana sobre la cual se ha realizado el cambio en el marco.
        /// </param>
        public static void NotifyWindowFrameChange(this IWindow window)
        {
            PInvoke.SetWindowPos(window.Handle, IntPtr.Zero, 0, 0, 0, 0,
                (uint)(WindowChanges.SWP_NOMOVE |
                WindowChanges.SWP_NOSIZE |
                WindowChanges.SWP_NOZORDER |
                WindowChanges.SWP_FRAMECHANGED));
        }

        private static void SetWindowEffect(IntPtr window, AccentPolicy accent)
        {
            var accentStructSize = Marshal.SizeOf(accent);
            var accentPtr = Marshal.AllocHGlobal(accentStructSize);
            Marshal.StructureToPtr(accent, accentPtr, false);
            var data = new WindowCompositionAttributeData
            {
                Attribute = WindowCompositionAttribute.WCA_ACCENT_POLICY,
                SizeOfData = accentStructSize,
                Data = accentPtr
            };
            PInvoke.SetWindowCompositionAttribute(window, ref data);
            Marshal.FreeHGlobal(accentPtr);
        }

        private static void ShowGwlStyle(this IWindow window, WindowStyles bit)
        {
            SetWindowData(window, WindowData.GWL_STYLE, p => p | bit);
        }

        private static void HideGwlStyle(this IWindow window, WindowStyles bit)
        {
            SetWindowData(window, WindowData.GWL_STYLE, p => p & ~bit);
        }

        private static void SetWindowData(this IWindow window, WindowData data, Func<WindowStyles, WindowStyles> transform)
        {
            PInvoke.SetWindowLong(
                window.Handle,
                (int)data,
                (uint)transform((WindowStyles)PInvoke.GetWindowLong(window.Handle, (int)data)));
        }
    }
}