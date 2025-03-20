/*
IAvaloniaWindow.cs

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

using Avalonia;
using Avalonia.Controls;
using TheXDS.MCART.Exceptions;

namespace TheXDS.MCART.Types.Base;

/// <summary>
/// Define una serie de miembros accesorios a implementar dentro de las
/// ventanas de Avalonia.
/// </summary>
/// <remarks>
/// Esta interfaz solo debe ser implementada por objetos que deriven de la
/// clase <see cref="Window"/> o de una de sus clases derivadas.
/// </remarks>
public interface IAvaloniaWindow : IWindow
{
    /// <summary>
    /// Obtiene una referencia directa a la ventana.
    /// </summary>
    Window Itself => this as Window ?? throw new InvalidTypeException(GetType());

    Size IWindow.Size
    {
        get => new(Itself.Width, Itself.Height);
        set
        {
            Itself.Width = value.Width;
            Itself.Height = value.Height;
        }
    }

    Point IWindow.Location
    {
        get => new(Itself.Position.X, Itself.Position.Y);
        set => Itself.Position = new PixelPoint((int)value.X, (int)value.Y);
    }

    void ICloseable.Close() => Itself.Close();

    void IWindow.Hide() => Itself.Hide();

    void IWindow.Minimize() => Itself.WindowState = WindowState.Minimized;

    void IWindow.Maximize() => Itself.WindowState = WindowState.Maximized;

    void IWindow.Restore() => Itself.WindowState = WindowState.Normal;

    void IWindow.ToggleMaximize()
    {
        if (Itself.WindowState == WindowState.Normal) Maximize();
        else Restore();
    }

    /// <summary>
    /// Obtiene un valor que indica el estado de la ventana.
    /// </summary>
    WindowState WindowState { get; }
}
