﻿//
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
        /// <see cref="System.Windows.Media.Color"/>.
        /// </summary>
        /// <param name="c"><see cref="Color"/> a convertir.</param>
        public static implicit operator System.Windows.Media.Color(Color c)=>System.Windows.Media.Color.FromScRgb(c.ScA,c.ScR,c.ScG,c.ScB);
        /// <summary>
        /// Convierte implícitamente una estructura
        /// <see cref="System.Windows.Media.Color"/> en un <see cref="Color"/>.
        /// </summary>
        /// <param name="c">
        /// <see cref="System.Windows.Media.Color"/> a convertir.
        /// </param>
        public static implicit operator Color(System.Windows.Media.Color c)
        {            
            return new Color(
                c.ScR.Clamp(0.0f, 1.0f),
                c.ScG.Clamp(0.0f, 1.0f),
                c.ScB.Clamp(0.0f, 1.0f),
                c.ScA.Clamp(0.0f, 1.0f));
        }
    }
}