/*
IWpfWindow.cs

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

using System.Windows;
using System.Windows.Interop;
using TheXDS.MCART.Component;
using TheXDS.MCART.Exceptions;
using TheXDS.MCART.PInvoke.Models;

namespace TheXDS.MCART.Types.Base;

/// <summary>
/// Defines a set of auxiliary members that must be implemented by
/// WPF windows.
/// </summary>
/// <remarks>
/// This interface should only be implemented by objects that derive
/// from <see cref="Window"/> or one of its subclasses.
/// </remarks>
public interface IWpfWindow : IMsWindow
{
    /// <summary>
    /// Gets a direct reference to the window.
    /// </summary>
    Window Itself => this as Window ?? throw new InvalidTypeException(GetType());

    /// <summary>
    /// Gets the current size of the window.
    /// </summary>
    Size ActualSise => new(Itself.ActualWidth, Itself.ActualHeight);

    IntPtr IMsWindow.Handle => new WindowInteropHelper(Itself).Handle;

    Margins IMsWindow.Padding
    {
        get
        {
            return new Margins
            {
                Left = (int)Itself.Padding.Left,
                Right = (int)Itself.Padding.Right,
                Top = (int)Itself.Padding.Top,
                Bottom = (int)Itself.Padding.Bottom
            };
        }
        set
        {
            Itself.Padding = new Thickness
            {
                Left = value.Left,
                Right = value.Right,
                Top = value.Top,
                Bottom = value.Bottom
            };
        }
    }

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
        get => new(Itself.Left, Itself.Top);
        set
        {
            Itself.Left = value.X;
            Itself.Top = value.Y;
        }
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
    /// Gets a value that indicates the window state.
    /// </summary>
    WindowState WindowState { get; }

    /// <summary>
    /// Starts a window drag operation.
    /// </summary>
    void DragMove() => Itself.DragMove();
}
