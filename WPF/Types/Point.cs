//
//  Point.cs
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
    public partial struct Point
    {
        /// <summary>
        /// Convierte implícitamente un <see cref="Point"/> en un
        /// <see cref="System.Windows.Point"/>.
        /// </summary>
        /// <param name="x"><see cref="Point"/> a convertir.</param>
        public static implicit operator System.Windows.Point(Point x) => new System.Windows.Point(x.X, x.Y);
        /// <summary>
        /// Convierte implícitamente un <see cref="System.Windows.Point"/> en 
        /// un <see cref="Point"/>.
        /// </summary>
        /// <param name="x"><see cref="System.Windows.Point"/> a convertir.</param>
        public static implicit operator Point(System.Windows.Point x) => new Point(x.X, x.Y);
    }
}