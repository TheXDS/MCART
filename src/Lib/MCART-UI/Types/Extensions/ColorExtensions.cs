/*
Color.cs

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

using D = System.Drawing.Color;

namespace TheXDS.MCART.Types.Extensions
{
    /// <summary>
    ///     Extensiones de conversión de colores de MCART en
    ///     <see cref="D"/>.
    /// </summary>
    public static class ColorExtensions
    {
        /// <summary>
        ///     Convierte una estructura <see cref="Color"/> en un
        ///     <see cref="D"/>.
        /// </summary>
        /// <param name="c"><see cref="Color"/> a convertir.</param>
        public static D FromMcartColor(this Color c)
        {
            return D.FromArgb(c.A, c.R, c.G, c.B);
        }
        /// <summary>
        ///     Convierte una estructura <see cref="D"/> en un
        ///     <see cref="Color"/>.
        /// </summary>
        /// <param name="c"><see cref="D"/> a convertir.</param>
        public static Color ToMcartColor(this D c)
        {
            return new Color(c.R, c.G, c.B, c.A);
        }
    }
}
