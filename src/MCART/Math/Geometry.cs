/*
Common.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2021 César Andrés Morgan

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

using System;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Types;

namespace TheXDS.MCART.Math
{
    /// <summary>
    /// Contiene funciones de trigonometría y geometría.
    /// </summary>
    public static class Geometry
    {
        
        #region Constantes

        /// <summary>
        /// Representa la proporción de 1 grado DEG sobre PI
        /// </summary>
        public const double DegRad = 0.0174532925199433D;

        /// <summary>
        /// Representa la proporción de la circunferencia entre el diámetro de un círculo.
        /// </summary>
        public const double Thau = 6.28318530717959D;

        #endregion

        /// <summary>
        /// Obtiene las cooerdenadas X,Y de una posición específica dentro de un
        /// bézier cuadrático
        /// </summary>
        /// <param name="position">
        /// Posición a obtener. Debe ser un <see cref="double" /> entre 0.0 y
        /// 1.0.
        /// </param>
        /// <param name="startPoint">
        /// Punto inicial del bézier cuadrático.
        /// </param>
        /// <param name="controlPoinr">
        /// Punto de control del bézier cuadrático.
        /// </param>
        /// <param name="endPoint">Punto final del bézier cuadrático.</param>
        /// <returns>
        /// Un <see cref="Point" /> con las coordenadas correspondientes a la
        /// posición dentro del bézier cuadrático dado por
        /// <paramref name="position" />.
        /// </returns>
        public static Point GetQuadBezierPoint(in double position, in Point startPoint, in Point controlPoinr, in Point endPoint)
        {
            if (!position.IsBetween(0, 1)) throw new ArgumentOutOfRangeException(nameof(position));
            var a = 1 - position;
            var b = a * a;
            var c = 2 * a * position;
            var d = position * position;
            return new Point(
                (b * startPoint.X) + (c * controlPoinr.X) + (d * endPoint.X),
                (b * startPoint.Y) + (c * controlPoinr.Y) + (d * endPoint.Y));
        }

        /// <summary>
        /// Obtiene las coordenadas de un punto dentro de un arco.
        /// </summary>
        /// <param name="radius">Radio del arco.</param>
        /// <param name="startAngle">
        /// Ángulo inicial del arco; en el sentido de las agujas del reloj.
        /// </param>
        /// <param name="endAngle">
        /// Ángulo final del arco; en el sentido de las agujas del reloj.
        /// </param>
        /// <param name="position">Posición a obtener dentro del arco.</param>
        /// <returns>
        /// Un conjunto de coordenadas con la posición del punto solicitado.
        /// </returns>
        public static Point GetArcPoint(in double radius, in double startAngle, in double endAngle, in double position)
        {
            var x = (startAngle - endAngle) * position * DegRad;
            return new Point(System.Math.Sin(x) * radius, System.Math.Cos(x) * radius);
        }

        /// <summary>
        /// Obtiene las coordenadas de un punto dentro de un círculo.
        /// </summary>
        /// <param name="radius">Radio del círculo.</param>
        /// <param name="position">Posición a obtener dentro del círculo.</param>
        /// <returns>
        /// Un conjunto de coordenadas con la posición del punto solicitado.
        /// </returns>
        [Sugar]
        public static Point GetCirclePoint(in double radius, in double position)
        {
            return GetArcPoint(radius, 0, 360, position);
        }
    }
}