/*
WpfUtils.cs

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

using Avalonia;
using Avalonia.Media;
using C = TheXDS.MCART.Math.Geometry;

namespace TheXDS.MCART.Helpers;

/// <summary>
/// Contiene varias herramientas de UI para utilizar en proyectos de
/// Windows Presentation Framework.
/// </summary>
public static class AvaloniaUtils
{
    /// <summary>
    /// Genera un arco de círculo que puede usarse en Avalonia.
    /// </summary>
    /// <param name="radius">Radio del arco a generar.</param>
    /// <param name="startAngle">Ángulo inicial del arco.</param>
    /// <param name="endAngle">Ángulo final del arco.</param>
    /// <param name="thickness">
    /// Grosor del trazo del arco. Ayuda a balancear el grosor del trazo y
    /// el radio para lograr un tamaño más consistente.
    /// </param>
    /// <returns>
    /// Un <see cref="PathGeometry" /> que contiene el arco generado por
    /// esta función.
    /// </returns>
    public static PathGeometry GetCircleArc(double radius, double startAngle, double endAngle, double thickness)
    {
        Point cp = new(radius + (thickness / 2), radius + (thickness / 2));
        ArcSegment? arc = new()
        {
            IsLargeArc = endAngle - startAngle > 180.0,
            Point = new Point(
                cp.X + (System.Math.Sin(C.DegRad * endAngle) * radius),
                cp.Y - (System.Math.Cos(C.DegRad * endAngle) * radius)),
            Size = new Size(radius, radius),
            SweepDirection = SweepDirection.Clockwise
        };
        PathFigure? pth = new()
        {
            StartPoint = new Point(
                cp.X + (System.Math.Sin(C.DegRad * startAngle) * radius),
                cp.Y - (System.Math.Cos(C.DegRad * startAngle) * radius)),
            IsClosed = false,
            Segments = [],            
        };
        pth.Segments!.Add(arc);
        PathGeometry returnValue = new() { Figures = [] };
        returnValue.Figures.Add(pth);
        return returnValue;
    }
}
