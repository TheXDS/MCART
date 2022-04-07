/*
WpfWindowWrap.cs

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

namespace TheXDS.MCART.Component;
using System.Windows;

/// <summary>
/// Envuelve una ventana de Windows Presentation Framework para brindar
/// servicios adicionales de gestión de ventana.
/// </summary>
public class WpfWindowWrap : IWpfWindow
{
    private readonly Window _window;

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="WpfWindowWrap"/>.
    /// </summary>
    /// <param name="window">Ventana a envolver.</param>
    public WpfWindowWrap(Window window)
    {
        _window = window;
    }

    /// <inheritdoc/>
    public WindowState WindowState => _window.WindowState;

    Window IWpfWindow.Itself => _window;

    /// <summary>
    /// Convierte implícitamente un <see cref="WpfWindowWrap"/> en un
    /// <see cref="Window"/>.
    /// </summary>
    /// <param name="wrap">Objeto a convertir.</param>
    public static implicit operator Window(WpfWindowWrap wrap)
    {
        return wrap._window;
    }

    /// <summary>
    /// Convierte implícitamente un <see cref="Window"/> en un
    /// <see cref="WpfWindowWrap"/>.
    /// </summary>
    /// <param name="window">Objeto a convertir.</param>
    public static implicit operator WpfWindowWrap(Window window)
    {
        return new WpfWindowWrap(window);
    }
}
