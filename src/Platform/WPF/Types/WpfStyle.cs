/*
WpfStyle.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2019 César Andrés Morgan

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

using System.Windows;
using System.Windows.Media;

namespace TheXDS.MCART.Types
{
    /// <summary>
    /// Referencia común de estilos para WPF.
    /// </summary>
    public class WpfStyle : IWpfStyle
    {
        /// <summary>
        /// <see cref="Brush"/> de fondo a aplicar al elemento.
        /// </summary>
        public Brush? Background { get; set; }

        /// <summary>
        /// <see cref="Brush"/> de frente a aplicar al elemento.
        /// </summary>
        public Brush? Foreground { get; set; }

        /// <summary>
        /// <see cref="Brush"/> a aplicar a los bordes del elemento.
        /// </summary>
        public Brush? BorderBrush { get; set; }

        /// <summary>
        /// Definición de bordes a aplicar al elemento.
        /// </summary>
        public Thickness? BorderThickness { get; set; }
    }
}