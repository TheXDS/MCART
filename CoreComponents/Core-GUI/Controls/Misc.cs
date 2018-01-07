//
//  Misc.cs
//
//  This file is part of Morgan's CLR Advanced Runtime (MCART)
//
//  Author:
//       César Andrés Morgan <xds_xps_ivx@hotmail.com>
//
//  Copyright (c) 2011 - 2018 César Andrés Morgan
//
//  Morgan's CLR Advanced Runtime (MCART) is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  Morgan's CLR Advanced Runtime (MCART) is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System.Collections.Generic;

namespace TheXDS.MCART.Controls
{
    /// <summary>
    /// Funciones misceláneas para controles/widgets.
    /// </summary>
    public static class Misc
    {
        /// <summary>
        /// Obtiene la suma de todos los valores de una colección de
        /// <see cref="Slice"/>.
        /// </summary>
        /// <param name="c">
        /// Colección de <see cref="Slice"/> para la cual obtener la suma.
        /// </param>
        /// <returns>
        /// Un <see cref="double"/> con la suma de los valores de una colección
        /// de <see cref="Slice"/>.
        /// </returns>
        public static double GetTotal(this IEnumerable<Slice> c)
        {
            double tot = 0.0;
            foreach (var k in c) tot += k.Value;
            return tot;
        }
    }
}
