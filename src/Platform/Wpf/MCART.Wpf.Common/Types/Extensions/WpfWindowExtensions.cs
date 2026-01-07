/*
WpfWindowExtensions.cs

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

using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using TheXDS.MCART.Types;
using TheXDS.MCART.Types.Base;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.Helpers;

/// <summary>
/// Contiene extensiones para las clases <see cref="Window"/> y
/// <see cref="IWpfWindow"/>.
/// </summary>
public static class WpfWindowExtensions
{
    /// <summary>
    /// Ejecuta una operación de arrastre de la ventana.
    /// </summary>
    /// <param name="window">
    /// Ventana a arrastrar.
    /// </param>
    /// <param name="e">
    /// Argumentos de arrastre generado en el evento 
    /// <see cref="UIElement.MouseDown"/> del control a utilizar como punto
    /// de arrastre.
    /// </param>
    public static void PerformWindowDrag(this Window? window, MouseButtonEventArgs e)
    {
        PerformWindowDrag(window, 0, e);
    }

    /// <summary>
    /// Ejecuta una operación de arrastre de la ventana.
    /// </summary>
    /// <param name="window">
    /// Ventana a arrastrar.
    /// </param>
    /// <param name="rightChromeWidth">
    /// Ancho del área a la izquierda del cromo de la ventana para excluir
    /// del cálculo de posición de arrastre. Se utiliza al restaurar una
    /// ventana maximizada cuando la misma utiliza un cromo definido por el
    /// usuario, en cuyo caso este valor es la suma del ancho de los
    /// controles a la izquierda del cromo de la ventana.
    /// </param>
    /// <param name="e">
    /// Argumentos de arrastre generado en el evento 
    /// <see cref="UIElement.MouseDown"/> del control a utilizar como punto
    /// de arrastre.
    /// </param>
    /// <remarks>
    /// Este método puede utilizarse enlazado a un evento
    /// <see cref="UIElement.MouseDown"/> de un control que pueda
    /// utilizarse como punto de arrastre para una ventana de WPF.
    /// </remarks>
    public static void PerformWindowDrag(this Window? window, int rightChromeWidth, MouseButtonEventArgs e)
    {
        if (e.LeftButton == MouseButtonState.Pressed && window is not null)
        {
            if (window.WindowState == WindowState.Maximized)
            {
                System.Windows.Point wp = window.PointToScreen(e.GetPosition(window));
                Types.Point mp = Windows.GetCursorPosition();
                double wi = window.ActualWidth;
                window.WindowState = WindowState.Normal;
                window.Left = mp.X - (wp.X * (window.Width - rightChromeWidth) / (wi + rightChromeWidth));
                window.Top = mp.Y - wp.Y;
            }
            window.DragMove();
        }
    }

    /// <summary>
    /// Habilita el botón de ayuda de las ventanas de Windows y conecta
    /// un manejador de eventos al mismo.
    /// </summary>
    /// <param name="window">
    /// Ventana en la cual habilitar el botón de ayuda.
    /// </param>
    /// <param name="handler">
    /// Delegado con la acción a ejecutar al hacer clic en el botón de
    /// ayuda de la ventana.
    /// </param>
    public static void HookHelp(this IWpfWindow window, HandledEventHandler handler)
    {
        window.ShowHelp();
        window.NotifyWindowFrameChange();
        HwndSource.FromHwnd(window.Handle).AddHook(CreateHookDelegate(window.Itself, 0xF180, handler));
    }

    /// <summary>
    /// Habilita el botón de ayuda de las ventanas de Windows y conecta
    /// un manejador de eventos al mismo.
    /// </summary>
    /// <param name="window">
    /// Ventana en la cual habilitar el botón de ayuda.
    /// </param>
    /// <param name="handler">
    /// Delegado con la acción a ejecutar al hacer clic en el botón de
    /// ayuda de la ventana.
    /// </param>
    public static void HookHelp(this Window window, HandledEventHandler handler)
    {
        new WpfWindowWrap(window).HookHelp(handler);
    }

    private static HwndSourceHook CreateHookDelegate(Window window, int systemCommand, HandledEventHandler handler)
    {
        return (IntPtr hwnd, int msg, IntPtr param, IntPtr lParam, ref bool handled) =>
        {
            if (msg != 0x0112 || ((int)param & 0xFFF0) != systemCommand) return IntPtr.Zero;
            HandledEventArgs? e = new();
            handler?.Invoke(window, e);
            handled = e.Handled;
            return IntPtr.Zero;
        };
    }
}
