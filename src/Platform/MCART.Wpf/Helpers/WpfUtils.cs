/*
WpfUtils.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author:
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2021 César Andrés Morgan

Morgan's CLR Advanced Runtime (MCART) is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

Morgan's CLR Advanced Runtime (MCART) is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

namespace TheXDS.MCART.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TheXDS.MCART.Types.Base;
using TheXDS.MCART.Types.Extensions;
using static TheXDS.MCART.Types.Extensions.TypeExtensions;
using C = Math.Geometry;

/// <summary>
/// Contiene varias herramientas de UI para utilizar en proyectos de
/// Windows Presentation Framework.
/// </summary>
public static class WpfUtils
{
    private static readonly IEnumerable<StreamUriParser> _uriParsers = Objects.FindAllObjects<StreamUriParser>();

    /// <summary>
    /// Obtiene una imagen a partir de un <see cref="Stream" />.
    /// </summary>
    /// <param name="stream">
    /// <see cref="Stream" /> con el contenido de la imagen.
    /// </param>
    /// <returns>
    /// La imagen que ha sido leída desde el <see cref="Stream" />.
    /// </returns>
    public static BitmapImage? GetBitmap(Stream? stream)
    {
        if (stream is null || stream.CanSeek && stream.Length == 0) return null;
        try
        {
            BitmapImage? retVal = new();
            retVal.BeginInit();
            stream.Seek(0, SeekOrigin.Begin);
            retVal.StreamSource = stream;
            retVal.EndInit();
            return retVal;
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// Obtiene una imagen a partir de un <see cref="Uri" />.
    /// </summary>
    /// <param name="uri">
    /// <see cref="Stream" /> con el contenido de la imagen.
    /// </param>
    /// <returns>
    /// La imagen que ha sido leída desde el <see cref="Stream" />.
    /// </returns>
    public static BitmapImage? GetBitmap(Uri uri)
    {
        foreach (StreamUriParser? j in _uriParsers)
        {
            if (!j.Handles(uri)) continue;
            Stream? s = j.OpenFullTransfer(uri);
            if (s is not null) return GetBitmap(s);
        }
        return null;
    }

    /// <summary>
    /// Obtiene una imagen a partir de una ruta especificada.
    /// </summary>
    /// <param name="path">
    /// <see cref="Stream" /> con el contenido de la imagen.
    /// </param>
    /// <returns>
    /// La imagen que ha sido leída desde el <see cref="Stream" />.
    /// </returns>
    public static BitmapImage? GetBitmap(string path)
    {
        using FileStream? fs = new(path, FileMode.Open);
        return GetBitmap(fs);
    }

    /// <summary>
    /// Obtiene una imagen a partir de un <see cref="Uri" /> de forma
    /// asíncrona.
    /// </summary>
    /// <param name="uri">
    /// <see cref="Stream" /> con el contenido de la imagen.
    /// </param>
    /// <returns>
    /// La imagen que ha sido leída desde el <see cref="Stream" />.
    /// </returns>
    public static async Task<BitmapImage?> GetBitmapAsync(Uri uri)
    {
        foreach (StreamUriParser? j in _uriParsers)
        {
            if (!j.Handles(uri)) continue;
            Stream? s = await j.OpenFullTransferAsync(uri);
            if (s is not null) return GetBitmap(s);
        }
        return null;
    }

    /// <summary>
    /// Devuelve una colección de los códecs de mapas de bits disponibles.
    /// Soporta cargar códecs desde cualquier ensamblado cargado.
    /// </summary>
    /// <returns>
    /// Una lista con una nueva instancia de todos los códecs de mapa de
    /// bits disponibles.
    /// </returns>
    public static IEnumerable<BitmapEncoder> GetBitmapEncoders()
    {
        return Objects.GetTypes<BitmapEncoder>(true).Select(p => p.New<BitmapEncoder>());
    }

    /// <summary>
    /// Genera un arco de círculo que puede usarse en Windows Presentation
    /// Framework.
    /// </summary>
    /// <param name="radius">Radio del arco a generar.</param>
    /// <param name="angle">Ángulo, o tamaño del arco.</param>
    /// <param name="thickness">
    /// Grosor del trazo del arco. Ayuda a balancear el grosor del trazo y
    /// el radio para lograr un tamaño más consistente.
    /// </param>
    /// <returns>
    /// Un <see cref="PathGeometry" /> que contiene el arco generado por
    /// esta función.
    /// </returns>
    public static PathGeometry GetCircleArc(double radius, double angle, double thickness = 0)
    {
        double t2 = thickness / 2;
        double rt = radius - t2;
        var p = C.GetArcPoint(rt, 0, angle - 180, 1);
        ArcSegment? arc = new()
        {
            IsLargeArc = angle > 180.0,
            Point = new(p.X + radius, p.Y + radius),
            Size = new(rt, rt),
            SweepDirection = SweepDirection.Clockwise
        };
        PathFigure? pth = new()
        {
            StartPoint = new(radius, t2),
            IsClosed = false
        };
        pth.Segments.Add(arc);
        PathGeometry? outp = new();
        outp.Figures.Add(pth);
        return outp;
    }

    /// <summary>
    /// Genera un arco de círculo que puede usarse en Windows Presentation
    /// Framework.
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
        Point cp = new(radius + thickness / 2, radius + thickness / 2);
        ArcSegment? arc = new()
        {
            IsLargeArc = endAngle - startAngle > 180.0,
            Point = new Point(
                cp.X + System.Math.Sin(C.DegRad * endAngle) * radius,
                cp.Y - System.Math.Cos(C.DegRad * endAngle) * radius),
            Size = new Size(radius, radius),
            SweepDirection = SweepDirection.Clockwise
        };
        PathFigure? pth = new()
        {
            StartPoint = new Point(
                cp.X + System.Math.Sin(C.DegRad * startAngle) * radius,
                cp.Y - System.Math.Cos(C.DegRad * startAngle) * radius),
            IsClosed = false
        };
        pth.Segments.Add(arc);
        PathGeometry? outp = new();
        outp.Figures.Add(pth);
        return outp;
    }
}
