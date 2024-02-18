/*
Common.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2024 César Andrés Morgan

Permission is hereby granted, free of charge, to any person obtaining a copy of
this software and associated documentation files (the "Software"), to deal in
the Software without restriction, including without limitation the rights to
use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
of the Software, and to permit persons to whom the Software is furnished to do
so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using TheXDS.MCART.Attributes;
using TheXDS.MCART.Types;

namespace TheXDS.MCART.Math;

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
    /// Bézier cuadrático
    /// </summary>
    /// <param name="position">
    /// Posición a obtener. Debe ser un <see cref="double" /> entre 0.0 y
    /// 1.0.
    /// </param>
    /// <param name="startPoint">
    /// Punto inicial del Bézier cuadrático.
    /// </param>
    /// <param name="controlPoint">
    /// Punto de control del Bézier cuadrático.
    /// </param>
    /// <param name="endPoint">Punto final del Bézier cuadrático.</param>
    /// <returns>
    /// Un <see cref="Point" /> con las coordenadas correspondientes a la
    /// posición dentro del Bézier cuadrático dado por
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
