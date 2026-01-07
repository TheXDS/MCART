/*
WpfUtils.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2026 César Andrés Morgan

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

using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TheXDS.MCART.Types.Base;
using TheXDS.MCART.Types.Extensions;
using static TheXDS.MCART.Types.Extensions.TypeExtensions;
using C = TheXDS.MCART.Math.Geometry;

namespace TheXDS.MCART.Helpers;

/// <summary>
/// Contains several UI tools for use in Windows Presentation Framework projects.
/// </summary>
public static class WpfUtils
{
    private static readonly IEnumerable<StreamUriParser> _uriParsers = ReflectionHelpers.FindAllObjects<StreamUriParser>();

    /// <summary>
    /// Creates a new <see cref="BitmapImage"/> from a <see cref="Stream"/>.
    /// </summary>
    /// <param name="stream">
    /// <see cref="Stream"/> with the image content.
    /// </param>
    /// <returns>
    /// A new <see cref="BitmapImage"/> created from the <see cref="Stream"/>.
    /// </returns>
    public static BitmapImage? GetBitmap(Stream? stream)
    {
        if (stream is null || (stream.CanSeek && stream.Length == 0)) return null;
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
    /// Obtains an image from a <see cref="Uri"/>.
    /// </summary>
    /// <param name="uri">
    /// <see cref="Uri"/> with the image content.
    /// </param>
    /// <returns>
    /// The image that has been read from the <see cref="Uri"/>.
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
    /// Obtains an image from a specified path.
    /// </summary>
    /// <param name="path">
    /// Path to the image file.
    /// </param>
    /// <returns>
    /// The image that has been read from the specified path.
    /// </returns>
    public static BitmapImage? GetBitmap(string path)
    {
        using FileStream? fs = new(path, FileMode.Open);
        return GetBitmap(fs);
    }

    /// <summary>
    /// Obtains an image from a <see cref="Uri"/> asynchronously.
    /// </summary>
    /// <param name="uri">
    /// <see cref="Uri"/> with the image content.
    /// </param>
    /// <returns>
    /// The image that has been read from the <see cref="Uri"/>.
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
    /// Returns a collection of available bitmap encoders. Supports loading encoders from any loaded assembly.
    /// </summary>
    /// <returns>
    /// A list with a new instance of all available bitmap encoders.
    /// </returns>
    public static IEnumerable<BitmapEncoder> GetBitmapEncoders()
    {
        return ReflectionHelpers.GetTypes<BitmapEncoder>(true).Select(p => p.New<BitmapEncoder>());
    }

    /// <summary>
    /// Generates a circle arc that can be used in Windows Presentation Framework.
    /// </summary>
    /// <param name="radius">Radius of the arc to generate.</param>
    /// <param name="startAngle">Initial angle of the arc.</param>
    /// <param name="endAngle">Final angle of the arc.</param>
    /// <param name="thickness">
    /// Thickness of the arc stroke. Helps balance the stroke thickness and radius to achieve a more consistent size.
    /// </param>
    /// <returns>
    /// A <see cref="PathGeometry"/> that contains the arc generated by this function.
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
            IsClosed = false
        };
        pth.Segments.Add(arc);
        PathGeometry? returnValue = new();
        returnValue.Figures.Add(pth);
        return returnValue;
    }
}
