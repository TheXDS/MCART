/*
PointExtensions.cs

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

using P = System.Drawing.Point;
using M = TheXDS.MCART.Types.Point;

namespace TheXDS.MCART.Types.Extensions
{
    /// <summary>
    ///     Extensiones de conversión de estructuras <see cref="M"/> en
    ///     <see cref="P"/>.
    /// </summary>
    public static class PointExtensions
    {
        /// <summary>
        ///     Convierte un <see cref="M"/> en un <see cref="P"/>.
        /// </summary>
        /// <param name="x"><see cref="M"/> a convertir.</param>
        /// <returns>
        ///     Un <see cref="P"/> equivalente al <see cref="M"/> especificado.
        /// </returns>
        public static P Point(M x) => new P((int)x.X,(int) x.Y);
        /// <summary>
        ///     Convierte un <see cref="P"/> en un <see cref="M"/>.
        /// </summary>
        /// <param name="x"><see cref="P"/> a convertir.</param>
        /// <returns>
        ///     Un <see cref="M"/> equivalente al <see cref="P"/> especificado.
        /// </returns>
        public static M ToMcartPoint(P x) => new M(x.X, x.Y);
    }
}