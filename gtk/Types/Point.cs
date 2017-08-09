//
//  Point.cs
//
//  This file is part of MCART
//
//  Author:
//       César Morgan <xds_xps_ivx@hotmail.com>
//
//  Copyright (c) 2011 - 2017 César Morgan
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
    public partial struct Point
    {
        /// <summary>
        /// Convierte implícitamente un <see cref="Point"/> en un
        /// <see cref="Cairo.Point"/>.
        /// </summary>
        /// <param name="x"><see cref="Point"/> a convertir.</param>
        public static implicit operator Cairo.Point(Point p) => new Cairo.Point((int)p.X, (int)p.Y);
        /// <summary>
        /// Convierte implícitamente un <see cref="Point"/> en un
        /// <see cref="Cairo.PointD"/>.
        /// </summary>
        /// <param name="x"><see cref="Point"/> a convertir.</param>
        public static implicit operator Cairo.PointD(Point p) => new Cairo.PointD(p.X, p.Y);
        /// <summary>
        /// Convierte implícitamente un <see cref="Cairo.Point"/> en un
        /// <see cref="Point"/>.
        /// </summary>
        /// <param name="x"><see cref="Cairo.Point"/> a convertir.</param>
        public static implicit operator Point(Cairo.Point p) => new Point(p.X, p.Y);
        /// <summary>
        /// Convierte implícitamente un <see cref="Cairo.PointD"/> en un
        /// <see cref="Point"/>.
        /// </summary>
        /// <param name="x"><see cref="Cairo.PointD"/> a convertir.</param>
        public static implicit operator Point(Cairo.PointD p) => new Point(p.X, p.Y);
    }
}