//
//  Color.cs
//
//  This file is part of MCART
//
//  Author:
//       César Andrés Morgan <xds_xps_ivx@hotmail.com>
//
//  Copyright (c) 2011 - 2017 César Andrés Morgan
//
//  MCART is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  MCART is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

namespace MCART.Types
{
    public partial struct Color
    {
        /// <summary>
        /// Convierte implícitamente una estructura <see cref="Color"/> en un
        /// <see cref="Gdk.Color"/>.
        /// </summary>
        /// <param name="c"><see cref="Color"/> a convertir.</param>
        public static implicit operator Gdk.Color(Color c) => new Gdk.Color(c.R, c.G, c.B);
        /// <summary>
        /// Convierte implícitamente una estructura <see cref="Gdk.Color"/> en un
        /// <see cref="Color"/>.
        /// </summary>
        /// <param name="c"><see cref="Gdk.Color"/> a convertir.</param>
        public static implicit operator Color(Gdk.Color c) =>
            new Color(
                (byte)c.Red.Clamp<ushort>(0, 255),
                (byte)c.Green.Clamp<ushort>(0, 255),
                (byte)c.Blue.Clamp<ushort>(0, 255));
        /// <summary>
        /// Convierte implícitamente una estructura <see cref="Color"/> en un
        /// un <see cref="cairo.Color"/>.
        /// </summary>
        /// <param name="c"><see cref="Color"/> a convertir.</param>
        public static implicit operator Cairo.Color(Color c) =>
            new Cairo.Color(c.ScR, c.ScG, c.ScB, c.ScA);
        /// <summary>
        /// Convierte implícitamente una estructura <see cref="Cairo.Color"/> en
        /// un <see cref="Color"/>.
        /// </summary>
        /// <param name="c"><see cref="Cairo.Color"/> a convertir.</param>
        public static implicit operator Color(Cairo.Color c) =>
            new Color((float)c.R, (float)c.G, (float)c.B, (float)c.A);

    }
}