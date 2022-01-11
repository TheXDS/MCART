/*
IWpfWindow.cs

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

using System;
using System.Windows;
using System.Windows.Interop;
using TheXDS.MCART.Component;
using TheXDS.MCART.Exceptions;
using TheXDS.MCART.PInvoke.Structs;

namespace TheXDS.MCART.Wpf.Component
{
    /// <summary>
    /// Define una serie de miembros accesorios a implementar dentro de las
    /// ventanas de WPF.
    /// </summary>
    /// <remarks>
    /// Esta interfaz solo debe ser implementada por objetos que deriven de la
    /// clase <see cref="Window"/> o de una de sus clases derivadas.
    /// </remarks>
    public interface IWpfWindow : IMsWindow
    {
        /// <summary>
        /// Obtiene una referencia directa a la ventana.
        /// </summary>
        Window Itself => this as Window ?? throw new InvalidTypeException(GetType());

        /// <summary>
        /// Obtiene el tamaño actual de la ventana.
        /// </summary>
        Types.Size ActualSise => new(Itself.ActualWidth, Itself.ActualHeight);

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

        Types.Size IMsWindow.Size
        {
            get => new(Itself.Width, Itself.Height);
            set
            {
                Itself.Width = value.Width;
                Itself.Height = value.Height;
            }
        }

        Types.Point IMsWindow.Location
        {
            get => new(Itself.Left, Itself.Top);
            set
            {
                Itself.Left = value.X;
                Itself.Top = value.Y;
            }
        }

        void ICloseable.Close() => Itself.Close();

        void IMsWindow.Hide() => Itself.Hide();

        void IMsWindow.Minimize() => Itself.WindowState = WindowState.Minimized;

        void IMsWindow.Maximize() => Itself.WindowState = WindowState.Maximized;

        void IMsWindow.Restore() => Itself.WindowState = WindowState.Normal;

        void IMsWindow.ToggleMaximize()
        {
            if (Itself.WindowState == WindowState.Normal) Maximize();
            else Restore();
        }

        /// <summary>
        /// Obtiene un valor que indica el estado de la ventana.
        /// </summary>
        WindowState WindowState { get; }

        /// <summary>
        /// Inicia una operación de arrastre de la ventana.
        /// </summary>
        void DragMove() => Itself.DragMove();
    }
}