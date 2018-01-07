//
//  ColorExtensions.cs
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

using System;

namespace TheXDS.MCART.Types.Extensions
{
    /// <summary>
    /// Extensiones de la estructura <see cref="Color"/>.
    /// </summary>
    public static class ColorExtensions
    {
        /// <summary>
        /// Determina si los colores son lo suficientemente similares.
        /// </summary>
        /// <returns>
        /// <c>true</c> si los colores son suficientemente similares, 
        /// <c>false</c> en caso contrario.</returns>
        /// <param name="c1">Primer <see cref="Color"/> a comparar.</param>
        /// <param name="c2">Segundo Color a comparar.</param>
        /// <param name="margin">
        /// Margen de similitud. de forma predeterminada, se establece en  99%.
        /// </param>
        public static bool AreClose(this Color c1, Color c2, float margin = 0.99f)
        {
            if (!margin.IsBetween(0.0f, 1.0f)) throw new ArgumentOutOfRangeException(nameof(margin));
            return Similarity(c1, c2) <= margin;
        }
        /// <summary>
        /// Determina el porcentaje de similitud entre dos colores.
        /// </summary>
        /// <returns>
        /// Un <see cref="float"/> que representa el porcentaje de similitud
        /// entre ambos colores.
        /// </returns>
        /// <param name="c1">Primer <see cref="Color"/> a comparar.</param>
        /// <param name="c2">Segundo Color a comparar.</param>
        public static float Similarity(this Color c1, Color c2)
        {
            return 1.0f -
                    ((c1.ScA - c2.ScA) +
                     (c1.ScR - c2.ScR) +
                     (c1.ScG - c2.ScG) +
                     (c1.ScB - c2.ScB));
        }
        /// <summary>
        /// Realiza una mezcla entre los colores especificados.
        /// </summary>
        /// <param name="c1">El primer <see cref="Color"/> a mezclar.</param>
        /// <param name="c2">El segundo <see cref="Color"/> a mezclar.</param>
        /// <returns>Una mezcla entre los colores <paramref name="c1"/> y 
        /// <paramref name="c2"/>.
        /// </returns>
        public static Color Blend(this Color c1, Color c2) => c1 / c2;
    }
}