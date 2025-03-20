// IWinFormsWindow.cs
//
// This file is part of Morgan's CLR Advanced Runtime (MCART)
//
// Author(s):
//      César Andrés Morgan <xds_xps_ivx@hotmail.com>
//
// Released under the MIT License (MIT)
// Copyright © 2011 - 2025 César Andrés Morgan
//
// Permission is hereby granted, free of charge, to any person obtaining a copy of
// this software and associated documentation files (the “Software”), to deal in
// the Software without restriction, including without limitation the rights to
// use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
// of the Software, and to permit persons to whom the Software is furnished to do
// so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using TheXDS.MCART.Component;
using TheXDS.MCART.Exceptions;
using TheXDS.MCART.PInvoke.Models;

namespace TheXDS.MCART.Types.Base;

/// <summary>
/// Define una serie de miembros accesorios a implementar dentro de las
/// ventanas de Windows Forms.
/// </summary>
/// <remarks>
/// Esta interfaz solo debe ser implementada por objetos que deriven de la
/// clase <see cref="Form"/> o de una de sus clases derivadas.
/// </remarks>
public interface IWinFormsWindow : IMsWindow
{
    /// <summary>
    /// Obtiene una referencia directa a la ventana.
    /// </summary>
    Form Itself => this as Form ?? throw new InvalidTypeException(GetType());
    
    IntPtr IMsWindow.Handle => Itself.Handle;

    Margins IMsWindow.Padding
    {
        get
        {
            var p = Itself.Padding;
            return new Margins() 
            { 
                Top = p.Top,
                Bottom = p.Bottom,
                Left = p.Left,
                Right = p.Right
            };
        }
        set
        {
            Itself.Padding = new Padding()
            {
                Top = value.Top,
                Bottom = value.Bottom,
                Left = value.Left,
                Right = value.Right
            };
        }
    }

    Size IWindow.Size
    {
        get => new(Itself.Width, Itself.Height);
        set
        {
            Itself.Width = (int)value.Width;
            Itself.Height = (int)value.Height;
        }
    }

    Point IWindow.Location
    {
        get => new(Itself.Left, Itself.Top);
        set
        {
            Itself.Left = (int)value.X;
            Itself.Top = (int)value.Y;
        }
    }

    void ICloseable.Close() => Itself.Close();

    void IWindow.Hide() => Itself.Hide();

    void IWindow.Minimize() => Itself.WindowState = FormWindowState.Minimized;

    void IWindow.Maximize() => Itself.WindowState = FormWindowState.Maximized;

    void IWindow.Restore() => Itself.WindowState = FormWindowState.Normal;

    void IWindow.ToggleMaximize()
    {
        if (Itself.WindowState == FormWindowState.Normal) Maximize();
        else Restore();
    }

    /// <summary>
    /// Obtiene un valor que indica el estado de la ventana.
    /// </summary>
    FormWindowState WindowState { get; }
}
