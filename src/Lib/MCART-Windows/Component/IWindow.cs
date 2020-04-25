/*
IWindow.cs

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
using TheXDS.MCART.Windows.Dwm.Structs;
using TheXDS.MCART.Component;

namespace TheXDS.MCART.Windows.Component
{
    /// <summary>
    /// Define una serie de miembros a implementar por un tipo que represente
    /// una ventana de Microsoft Windows.
    /// </summary>
    /// <remarks>
    /// Esta interfaz es candidata a ser migrada a <c>shape</c> en C# 9.0.
    /// </remarks>
    public interface IWindow : ICloseable
    {
        /// <summary>
        /// Obtiene el Handle por medio del cual la ventana puede ser
        /// manipulada.
        /// </summary>
        IntPtr Handle { get; }

        /// <summary>
        /// Obtiene o establece el margen interior de espaciado entre los 
        /// bordes de la ventana y su contenido.
        /// </summary>
        Margins Padding { get; set; }
    }
}
