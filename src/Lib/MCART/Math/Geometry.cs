/*
Common.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2025 César Andrés Morgan

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
/// Contains trigonometry and geometry functions.
/// </summary>
public static partial class Geometry
{
    /// <summary>
    /// Represents the ratio of 1 degree to PI.
    /// </summary>
    public const double DegRad = 0.0174532925199433D;

    /// <summary>
    /// Gets the X, Y coordinates of a specific position within a 
    /// quadratic Bézier curve.
    /// </summary>
    /// <param name="position">
    /// The position to get, which must be a <see cref="double"/> between 
    /// 0.0 and 1.0.
    /// </param>
    /// <param name="startPoint">
    /// The starting point of the quadratic Bézier curve.
    /// </param>
    /// <param name="controlPoint">
    /// The control point of the quadratic Bézier curve.
    /// </param>
    /// <param name="endPoint">
    /// The ending point of the quadratic Bézier curve.
    /// </param>
    /// <returns>
    /// A <see cref="Point"/> with the coordinates corresponding to the 
    /// position within the given quadratic Bézier curve.
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
    /// Gets the coordinates of a point within an arc.
    /// </summary>
    /// <param name="radius">The radius of the arc.</param>
    /// <param name="startAngle">
    /// The starting angle of the arc, in a clockwise direction.
    /// </param>
    /// <param name="endAngle">
    /// The ending angle of the arc, in a clockwise direction.
    /// </param>
    /// <param name="position">The position to get within the arc.</param>
    /// <returns>
    /// A set of coordinates with the position of the requested point.
    /// </returns>
    public static Point GetArcPoint(in double radius, in double startAngle, in double endAngle, in double position)
    {
        (double x, double y) = System.Math.SinCos((startAngle - endAngle) * position * DegRad);
        return new Point(x * radius, y * radius);
    }

    /// <summary>
    /// Gets the coordinates of a point within a circle.
    /// </summary>
    /// <param name="radius">The radius of the circle.</param>
    /// <param name="position">The position to get within the circle.</param>
    /// <returns>
    /// A set of coordinates with the position of the requested point.
    /// </returns>
    [Sugar]
    public static Point GetCirclePoint(in double radius, in double position)
    {
        return GetArcPoint(radius, 0, 360, position);
    }
}
