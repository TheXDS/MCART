/*
IWpfStyle.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright (c) 2011 - 2019 César Andrés Morgan

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

#nullable enable

using System.Windows;
using System.Windows.Media;

namespace TheXDS.MCART.Types
{
    /// <summary>
    ///     Define una serie de miembros a implementar por una clase que
    ///     describa estilos de elementos para Wpf.
    /// </summary>
    public interface IWpfStyle
    {
        /// <summary>
        ///     Obtiene o establece un <see cref="Brush"/> a utilizar para
        ///     rellenar el fondo de un elemento de Wpf.
        /// </summary>
        Brush? Background { get; set; }

        /// <summary>
        ///     Obtiene o establece un <see cref="Brush"/> a utilizar para
        ///     rellenar el frente de un elemento de Wpf.
        /// </summary>
        Brush? Foreground { get; set; }

        /// <summary>
        ///     Obtiene o establece un <see cref="Brush"/> a utilizar para
        ///     dibujar los bordes de un elemento de Wpf.
        /// </summary>
        Brush? BorderBrush { get; set; }

        /// <summary>
        ///     Obtiene o establece el grosor de los bordes a dibujar en el
        ///     elemento de Wpf.
        /// </summary>
        Thickness? BorderThickness { get; set; }
    }
}