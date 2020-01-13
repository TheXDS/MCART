/*
WpfColorExtensions.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2019 César Andrés Morgan

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

using TheXDS.MCART.Math;
using System.Windows.Media;
using B = System.Windows.Media.Brush;
using D = System.Windows.Media.Color;
using C = TheXDS.MCART.Types.Color;
using System.Linq;

namespace TheXDS.MCART.Types.Extensions
{
    /// <summary>
    /// Extensiones de conversión de colores de MCART en
    /// <see cref="D"/>.
    /// </summary>
    public static class WpfColorExtensions
    {
        /// <summary>
        /// Convierte una estructura <see cref="C"/> en un
        /// <see cref="D"/>.
        /// </summary>
        /// <param name="c"><see cref="C"/> a convertir.</param>
        public static D Color(this C c)
        {
            return D.FromScRgb(c.ScA, c.ScR, c.ScG, c.ScB);
        }

        /// <summary>
        /// Convierte una estructura <see cref="D"/> en un
        /// <see cref="C"/>.
        /// </summary>
        /// <param name="c"><see cref="D"/> a convertir.</param>
        public static C ToMcartColor(this D c)
        {
            return new C(
                c.ScR.Clamp(0.0f, 1.0f),
                c.ScG.Clamp(0.0f, 1.0f),
                c.ScB.Clamp(0.0f, 1.0f),
                c.ScA.Clamp(0.0f, 1.0f));
        }

        /// <summary>
        /// Convierte un <see cref="SolidColorBrush"/> en un
        /// <see cref="C"/>.
        /// </summary>
        /// <param name="c"><see cref="D"/> a convertir.</param>
        public static C ToMcartColor(this SolidColorBrush c)
        {
            return new C(
                c.Color.ScR.Clamp(0.0f, 1.0f),
                c.Color.ScG.Clamp(0.0f, 1.0f),
                c.Color.ScB.Clamp(0.0f, 1.0f),
                c.Color.ScA.Clamp(0.0f, 1.0f));
        }


        /// <summary>
        /// Convierte una estructura <see cref="C"/> en un
        /// <see cref="B"/>.
        /// </summary>
        /// <param name="c"><see cref="C"/> a convertir.</param>
        /// <returns>
        /// Un <see cref="B"/> de color sólido equivalente al
        /// <see cref="C"/> original.
        /// </returns>
        public static SolidColorBrush Brush(this C c)
        {
            return new SolidColorBrush(Color(c));
        }

        /// <summary>
        /// Mezcla todos los colores de un <see cref="GradientBrush"/> en
        /// un <see cref="C"/>.
        /// </summary>
        /// <param name="g"></param>
        /// <returns>
        /// Un <see cref="C"/> que es la mezcla de todos los colores del
        /// <see cref="GradientBrush"/>.
        /// </returns>
        public static C Blend(this GradientBrush g)
        {
            return C.Blend(g.GradientStops.Select(p => ToMcartColor(p.Color)));
        }

    }
}
