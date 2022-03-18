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

namespace TheXDS.MCART.Math;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Types;

/// <summary>
/// Contiene funciones de trigonometría y geometría.
/// </summary>
public static partial class Geometry
{
    /// <summary>
    /// Representa la proporción de 1 grado DEG sobre PI
    /// </summary>
    public const double DegRad = 0.0174532925199433D;

    /// <summary>
    /// Obtiene las coordenadas X,Y de una posición específica dentro de un
    /// bezier cuadrático
    /// </summary>
    /// <param name="position">
    /// Posición a obtener. Debe ser un <see cref="double" /> entre 0.0 y
    /// 1.0.
    /// </param>
    /// <param name="startPoint">
    /// Punto inicial del bezier cuadrático.
    /// </param>
    /// <param name="controlPoint">
    /// Punto de control del bezier cuadrático.
    /// </param>
    /// <param name="endPoint">Punto final del bezier cuadrático.</param>
    /// <returns>
    /// Un <see cref="Point" /> con las coordenadas correspondientes a la
    /// posición dentro del bezier cuadrático dado por
    /// <paramref name="position" />.
    /// </returns>
    public static Point GetQuadBezierPoint(in double position, in Point startPoint, in Point controlPoint, in Point endPoint)
    {
        GetQuadBezierPoint_Contract(position, startPoint, controlPoint, endPoint);
        double a = 1 - position;
        double b = a * a;
        double c = 2 * a * position;
        double d = position * position;
        return new Point(
            (b * startPoint.X) + (c * controlPoint.X) + (d * endPoint.X),
            (b * startPoint.Y) + (c * controlPoint.Y) + (d * endPoint.Y));
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
        (double x, double y) = System.Math.SinCos((startAngle - endAngle) * position * DegRad);
        return new Point(x * radius, y * radius);
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
