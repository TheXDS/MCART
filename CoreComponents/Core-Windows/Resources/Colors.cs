//
//  Colors.cs
//
//  This file is part of MCART
//
//  Author:
//       César Andrés Morgan <xds_xps_ivx@hotmail.com>
//
//  Copyright (c) 2011 - 2018 César Andrés Morgan
//
//  MCART is free software: you can Redistribute it and/or modify
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

using MCART.Types;

namespace MCART.Resources
{
    public static partial class Colors
    {
        /// <summary>
        /// Obtiene un <see cref="Color"/> correspondiente a las barras de
        /// progreso de Microsoft® Windows®.
        /// </summary>
        public static Color ProgressBar => new Color(0x1, 0xd3, 0x28);
    }
}
