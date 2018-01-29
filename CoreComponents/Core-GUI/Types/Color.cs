/*
Color.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright (c) 2011 - 2018 César Andrés Morgan

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

namespace TheXDS.MCART.Types
{
    /// <summary>
    /// Estructura universal que describe un color en sus componentes alfa,
    /// rojo, verde y azul.
    /// </summary>
    public partial struct Color
    {
        /// <summary>
        /// Convierte implícitamente una estructura <see cref="Color"/> en un
        /// un <see cref="System.Drawing.Color"/>.
        /// </summary>
        /// <param name="c"><see cref="Color"/> a convertir.</param>
        public static implicit operator System.Drawing.Color(Color c) => System.Drawing.Color.FromArgb(c.A, c.R, c.G, c.B);
        /// <summary>
        /// Convierte implícitamente una estructura
        /// <see cref="System.Drawing.Color"/> en un <see cref="Color"/>.
        /// </summary>
        /// <param name="c">
        /// <see cref="System.Drawing.Color"/> a convertir.
        /// </param>
        public static implicit operator Color(System.Drawing.Color c) => new Color(c.R, c.G, c.B,c.A);
    }
}