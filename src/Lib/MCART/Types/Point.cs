﻿/*
Point.cs

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

using System.ComponentModel;
using System.Numerics;
using TheXDS.MCART.Helpers;
using TheXDS.MCART.Math;
using TheXDS.MCART.Misc;
using TheXDS.MCART.Resources;
using TheXDS.MCART.Types.Base;
using static System.Math;
using static TheXDS.MCART.Types.Extensions.StringExtensions;
using CI = System.Globalization.CultureInfo;

namespace TheXDS.MCART.Types;

/// <summary>
/// Universal type for a set of two-dimensional coordinates.
/// </summary>
/// <param name="x">The x coordinate.</param>
/// <param name="y">The y coordinate.</param>
public struct Point(double x, double y) : IVector, IFormattable, IEquatable<Point>
{
    /// <summary>
    /// Gets a point that does not represent any position. This field is read-only.
    /// </summary>
    /// <value>
    /// A <see cref="Point" /> with its coordinates set to <see cref="double.NaN"/>.
    /// </value>
    public static readonly Point Nowhere = new(double.NaN, double.NaN);

    /// <summary>
    /// Gets a point at the origin. This field is read-only.
    /// </summary>
    /// <value>
    /// A <see cref="Point" /> with its coordinates at the origin.
    /// </value>
    public static readonly Point Origin = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="Point"/> structure.
    /// </summary>
    public Point() : this(0, 0)
    {
    }

    /// <summary>
    /// Converts a <see cref="Point" /> to a <see cref="System.Drawing.Point" />.
    /// </summary>
    /// <param name="x">The <see cref="Point" /> to convert.</param>
    /// <returns>
    /// A <see cref="System.Drawing.Point" /> equivalent to the specified <see cref="Point" />.
    /// </returns>
    public static implicit operator System.Drawing.Point(Point x) => new((int)x.X, (int)x.Y);

    /// <summary>
    /// Converts a <see cref="System.Drawing.Point" /> to a <see cref="Point" />.
    /// </summary>
    /// <param name="x">The <see cref="System.Drawing.Point" /> to convert.</param>
    /// <returns>
    /// A <see cref="Point" /> equivalent to the specified <see cref="System.Drawing.Point" />.
    /// </returns>
    public static implicit operator Point(System.Drawing.Point x) => new(x.X, x.Y);

    /// <summary>
    /// Performs addition on points.
    /// </summary>
    /// <param name="l">Point 1.</param>
    /// <param name="r">Point 2.</param>
    /// <returns>The sum of the vectors of the points.</returns>
    public static Point operator +(Point l, Point r)
    {
        return new(l.X + r.X, l.Y + r.Y);
    }

    /// <summary>
    /// Performs addition on points.
    /// </summary>
    /// <param name="l">Point 1.</param>
    /// <param name="r">Point 2.</param>
    /// <returns>The sum of the vectors of the points.</returns>
    public static Point operator +(Point l, IVector r)
    {
        return new(l.X + r.X, l.Y + r.Y);
    }

    /// <summary>
    /// Performs addition on a point.
    /// </summary>
    /// <param name="l">Point 1.</param>
    /// <param name="r">Addition operand.</param>
    /// <returns>
    /// A new <see cref="Point" /> whose vectors are the sum of the original
    /// vectors + <paramref name="r"/>.
    /// </returns>
    public static Point operator +(Point l, double r)
    {
        return new(l.X + r, l.Y + r);
    }

    /// <summary>
    /// Performs subtraction on points.
    /// </summary>
    /// <param name="l">Point 1.</param>
    /// <param name="r">Point 2.</param>
    /// <returns>The difference of the vectors of the points.</returns>
    public static Point operator -(Point l, Point r)
    {
        return new(l.X - r.X, l.Y - r.Y);
    }

    /// <summary>
    /// Performs subtraction on points.
    /// </summary>
    /// <param name="l">Point 1.</param>
    /// <param name="r">Point 2.</param>
    /// <returns>The difference of the vectors of the points.</returns>
    public static Point operator -(Point l, IVector r)
    {
        return new(l.X - r.X, l.Y - r.Y);
    }

    /// <summary>
    /// Realiza una operación de resta sobre el punto.
    /// </summary>
    /// <param name="l">Punto 1.</param>
    /// <param name="r">Operando de resta.</param>
    /// <returns>
    /// Un nuevo <see cref="Point" /> cuyos vectores son la resta de los
    /// vectores originales - <paramref name="r" />.
    /// </returns>
    public static Point operator -(Point l, double r)
    {
        return new(l.X - r, l.Y - r);
    }

    /// <summary>
    /// Realiza una operación de multiplicación sobre los puntos.
    /// </summary>
    /// <param name="l">Punto 1.</param>
    /// <param name="r">Punto 2.</param>
    /// <returns>La multiplicación de los vectores de los puntos.</returns>
    public static Point operator *(Point l, Point r)
    {
        return new(l.X * r.X, l.Y * r.Y);
    }

    /// <summary>
    /// Realiza una operación de multiplicación sobre los puntos.
    /// </summary>
    /// <param name="l">Punto 1.</param>
    /// <param name="r">Punto 2.</param>
    /// <returns>La multiplicación de los vectores de los puntos.</returns>
    public static Point operator *(Point l, IVector r)
    {
        return new(l.X * r.X, l.Y * r.Y);
    }

    /// <summary>
    /// Realiza una operación de multiplicación sobre el punto.
    /// </summary>
    /// <param name="l">Punto 1.</param>
    /// <param name="r">Operando de multiplicación.</param>
    /// <returns>
    /// Un nuevo <see cref="Point" /> cuyos vectores son la multiplicación
    /// de los vectores originales * <paramref name="r" />.
    /// </returns>
    public static Point operator *(Point l, double r)
    {
        return new(l.X * r, l.Y * r);
    }

    /// <summary>
    /// Realiza una operación de división sobre los puntos.
    /// </summary>
    /// <param name="l">Punto 1.</param>
    /// <param name="r">Punto 2.</param>
    /// <returns>La división de los vectores de los puntos.</returns>
    public static Point operator /(Point l, Point r)
    {
        return new(l.X / r.X, l.Y / r.Y);
    }

    /// <summary>
    /// Realiza una operación de división sobre los puntos.
    /// </summary>
    /// <param name="l">Punto 1.</param>
    /// <param name="r">Punto 2.</param>
    /// <returns>La división de los vectores de los puntos.</returns>
    public static Point operator /(Point l, IVector r)
    {
        return new(l.X / r.X, l.Y / r.Y);
    }

    /// <summary>
    /// Realiza una operación de división sobre el punto.
    /// </summary>
    /// <param name="l">Punto 1.</param>
    /// <param name="r">Operando de división.</param>
    /// <returns>
    /// Un nuevo <see cref="Point" /> cuyos vectores son la división de los
    /// vectores originales / <paramref name="r" />.
    /// </returns>
    public static Point operator /(Point l, double r)
    {
        return new(l.X / r, l.Y / r);
    }

    /// <summary>
    /// Realiza una operación de residuo sobre los puntos.
    /// </summary>
    /// <param name="l">Punto 1.</param>
    /// <param name="r">Punto 2.</param>
    /// <returns>El residuo de los vectores de los puntos.</returns>
    public static Point operator %(Point l, Point r)
    {
        return new(l.X % r.X, l.Y % r.Y);
    }

    /// <summary>
    /// Realiza una operación de residuo sobre los puntos.
    /// </summary>
    /// <param name="l">Punto 1.</param>
    /// <param name="r">Punto 2.</param>
    /// <returns>El residuo de los vectores de los puntos.</returns>
    public static Point operator %(Point l, IVector r)
    {
        return new(l.X % r.X, l.Y % r.Y);
    }

    /// <summary>
    /// Realiza una operación de residuo sobre el punto.
    /// </summary>
    /// <param name="l">Punto 1.</param>
    /// <param name="r">Operando de residuo.</param>
    /// <returns>
    /// Un nuevo <see cref="Point" /> cuyos vectores son el residuo de los
    /// vectores originales % <paramref name="r" />.
    /// </returns>
    public static Point operator %(Point l, double r)
    {
        return new(l.X % r, l.Y % r);
    }

    /// <summary>
    /// Incrementa en 1 los vectores del punto.
    /// </summary>
    /// <param name="p">Punto a incrementar.</param>
    /// <returns>Un punto con sus vectores incrementados en 1.</returns>
    public static Point operator ++(Point p)
    {
        p.X++;
        p.Y++;
        return p;
    }

    /// <summary>
    /// Decrementa en 1 los vectores del punto.
    /// </summary>
    /// <param name="p">Punto a decrementar.</param>
    /// <returns>Un punto con sus vectores decrementados en 1.</returns>
    public static Point operator --(Point p)
    {
        p.X--;
        p.Y--;
        return p;
    }

    /// <summary>
    /// Convierte a positivos los vectores del punto.
    /// </summary>
    /// <param name="p">Punto a operar.</param>
    /// <returns>Un punto con sus vectores positivos.</returns>
    public static Point operator +(Point p)
    {
        return new(+p.X, +p.Y);
    }

    /// <summary>
    /// Invierte el signo de los vectores del punto.
    /// </summary>
    /// <param name="p">Punto a operar.</param>
    /// <returns>Un punto con el signo de sus vectores invertido.</returns>
    public static Point operator -(Point p)
    {
        return new(-p.X, -p.Y);
    }

    /// <summary>
    /// Compara la igualdad de los vectores de los puntos.
    /// </summary>
    /// <param name="l">Objeto a comparar</param>
    /// <param name="r">Objeto contra el cual comparar.</param>
    /// <returns>
    /// <see langword="true" /> si ambas instancias son iguales,
    /// <see langword="false" /> en caso contrario.
    /// </returns>
    public static bool operator ==(Point l, Point r)
    {
        return (l.X == r.X || !new[] { l.X, r.X }.AreValid()) && (l.Y == r.Y || !new[] { l.Y, r.Y }.AreValid());
    }

    /// <summary>
    /// Compara la igualdad de los vectores de los puntos.
    /// </summary>
    /// <param name="l">Punto a comparar.</param>
    /// <param name="r">Vector bidimensional contra el cual comparar.</param>
    /// <returns>
    /// <see langword="true" /> si todos los vectores de ambos puntos son iguales;
    /// de lo contrario, <see langword="false" />.
    /// </returns>
    public static bool operator ==(Point l, IVector r)
    {
        return (l.X == r.X || !new[] { l.X, r.X }.AreValid()) && (l.Y == r.Y || !new[] { l.Y, r.Y }.AreValid());
    }

    /// <summary>
    /// Compara la diferencia de los vectores de los puntos.
    /// </summary>
    /// <param name="l">Punto 1.</param>
    /// <param name="r">Punto 2.</param>
    /// <returns>
    /// <see langword="true" /> si los vectores de ambos puntos son diferentes;  de lo
    /// contrario, <see langword="false" />.
    /// </returns>
    public static bool operator !=(Point l, Point r)
    {
        return (l.X != r.X || !new[] { l.X, r.X }.AreValid()) && (l.Y != r.Y || !new[] { l.Y, r.Y }.AreValid());
    }

    /// <summary>
    /// Compara la diferencia de los vectores de los puntos.
    /// </summary>
    /// <param name="l">Punto a comparar.</param>
    /// <param name="r">Vector bidimensional contra el cual comparar.</param>
    /// <returns>
    /// <see langword="true" /> si los vectores de ambos puntos son diferentes;  de lo
    /// contrario, <see langword="false" />.
    /// </returns>
    public static bool operator !=(Point l, IVector r)
    {
        return (l.X != r.X || !new[] { l.X, r.X }.AreValid()) && (l.Y != r.Y || !new[] { l.Y, r.Y }.AreValid());
    }

    /// <summary>
    /// Coordenada X.
    /// </summary>
    public double X { get; set; } = x;

    /// <summary>
    /// Coordenada Y.
    /// </summary>
    public double Y { get; set; } = y;

    /// <summary>
    /// Intenta crear un <see cref="Point"/> a partir de una cadena.
    /// </summary>
    /// <param name="value">
    /// Valor a partir del cual crear un <see cref="Point"/>.
    /// </param>
    /// <param name="point">
    /// <see cref="Point"/> que ha sido creado.
    /// </param>
    /// <returns>
    /// <see langword="true"/> si la conversión ha tenido éxito,
    /// <see langword="false"/> en caso contrario.
    /// </returns>
    public static bool TryParse(string value, out Point point)
    {
        switch (value)
        {
            case nameof(Nowhere):
            case "":
            case null:
                point = Nowhere;
                break;
            case nameof(Origin):
            case "0":
            case "+":
                point = Origin;
                break;
            default:
                string[]? separators =
                [
                    ", ",
                    "; ",
                    " - ",
                    " : ",
                    " | ",
                    " ",
                    ",",
                    ";",
                    ":",
                    "|",
                    "-"
                ];
                return PrivateInternals.TryParseValues<double, Point>(new DoubleConverter(), separators, value.Without("()[]{}".ToCharArray()), 2, l => new Point(l[0], l[1]), out point);
        }
        return true;
    }

    /// <summary>
    /// Crea un <see cref="Point"/> a partir de una cadena.
    /// </summary>
    /// <param name="value">
    /// Valor a partir del cual crear un <see cref="Point"/>.
    /// </param>
    /// <exception cref="FormatException">
    /// Se produce si la conversión ha fallado.
    /// </exception>
    /// <returns><see cref="Point"/> que ha sido creado.</returns>
    public static Point Parse(string value)
    {
        if (TryParse(value, out Point returnValue)) return returnValue;
        throw new FormatException();
    }

    /// <summary>
    /// Calcula el ángulo formado por la línea que intersecta el origen y
    /// este <see cref="Point" /> contra el eje horizontal X.
    /// </summary>
    /// <returns>El ángulo calculado.</returns>
    public readonly double Angle()
    {
        double ang = Acos(X / Magnitude());
        if (Y < 0) ang = Tau - ang;
        return ang;
    }

    /// <summary>
    /// Compara la igualdad de los vectores de los puntos.
    /// </summary>
    /// <param name="other">
    /// <see cref="Point" /> contra el cual comparar.
    /// </param>
    /// <returns>
    /// <see langword="true" /> si todos los vectores de ambos puntos son iguales;
    /// de lo contrario, <see langword="false" />.
    /// </returns>
    public readonly bool Equals(Point other)
    {
        return this == other;
    }

    /// <summary>
    /// Determina si el punto se encuentra dentro del rectángulo formado por
    /// los puntos especificados.
    /// </summary>
    /// <returns>
    /// <see langword="true" /> si el punto se encuentra dentro del rectángulo
    /// formado, <see langword="false" /> en caso contrario.
    /// </returns>
    /// <param name="p1">Punto 1.</param>
    /// <param name="p2">Punto 2.</param>
    public readonly bool WithinBox(in Point p1, in Point p2)
    {
        return X.IsBetween(p1.X, p2.X) && Y.IsBetween(p1.Y, p2.Y);
    }

    /// <summary>
    /// Determina si el punto se encuentra dentro del rectángulo formado por
    /// los rangos especificados.
    /// </summary>
    /// <returns>
    /// <see langword="true" /> si el punto se encuentra dentro del rectángulo
    /// formado, <see langword="false" /> en caso contrario.
    /// </returns>
    /// <param name="x">Rango de valores para el eje X.</param>
    /// <param name="y">Rango de valores para el eje Y.</param>
    public readonly bool WithinBox(in Range<double> x, in Range<double> y)
    {
        return x.IsWithin(X) && y.IsWithin(Y);
    }

    /// <summary>
    /// Determina si el punto se encuentra dentro del rectángulo formado por
    /// las coordenadas especificadas.
    /// </summary>
    /// <returns>
    /// <see langword="true" /> si el punto se encuentra dentro del rectángulo
    /// formado, <see langword="false" /> en caso contrario.
    /// </returns>
    /// <param name="size">Tamaño del rectángulo.</param>
    /// <param name="topLeft">Coordenadas de esquina superior izquierda</param>
    public readonly bool WithinBox(in Size size, in Point topLeft)
    {
        return WithinBox(topLeft.X, topLeft.Y, topLeft.X + size.Width, topLeft.Y - size.Height);
    }

    /// <summary>
    /// Determina si el punto se encuentra dentro del rectángulo formado por
    /// las coordenadas especificadas.
    /// </summary>
    /// <returns>
    /// <see langword="true" /> si el punto se encuentra dentro del rectángulo
    /// formado, <see langword="false" /> en caso contrario.
    /// </returns>
    /// <param name="size">Tamaño del rectángulo.</param>
    public readonly bool WithinBox(in Size size)
    {
        return WithinBox(size, new Point(-(size.Width / 2), size.Height / 2));
    }

    /// <summary>
    /// Determina si el punto se encuentra dentro del círculo especificado.
    /// </summary>
    /// <param name="center">Punto central del círculo.</param>
    /// <param name="radius">Radio del círculo.</param>
    /// <returns>
    /// <see langword="true" /> si el punto se encuentra dentro del círculo,
    /// <see langword="false" /> en caso contrario.
    /// </returns>
    public readonly bool WithinCircle(in Point center, in double radius)
    {
        return Magnitude(center) <= radius;
    }

    /// <summary>
    /// Determina si el punto se encuentra dentro del rectángulo formado por
    /// las coordenadas especificadas.
    /// </summary>
    /// <returns>
    /// <see langword="true" /> si el punto se encuentra dentro del rectángulo
    /// formado, <see langword="false" /> en caso contrario.
    /// </returns>
    /// <param name="x1">La primer coordenada x.</param>
    /// <param name="y1">La primer coordenada y.</param>
    /// <param name="x2">La segunda coordenada x.</param>
    /// <param name="y2">La segunda coordenada y.</param>
    public readonly bool WithinBox(in double x1, in double y1, in double x2, in double y2)
    {
        double minX, maxX, minY, maxY;
        if (x1 <= x2)
        {
            minX = x1;
            maxX = x2;
        }
        else
        {
            minX = x2;
            maxX = x1;
        }
        if (y1 <= y2)
        {
            minY = y1;
            maxY = y2;
        }
        else
        {
            minY = y2;
            maxY = y1;
        }
        return X.IsBetween(minX, maxX) && Y.IsBetween(minY, maxY);
    }

    /// <summary>
    /// Calcula la magnitud de las coordenadas.
    /// </summary>
    /// <returns>
    /// La magnitud resultante entre el punto y el origen.
    /// </returns>
    public readonly double Magnitude()
    {
        return Sqrt((X * X) + (Y * Y));
    }

    /// <summary>
    /// Calcula la magnitud de las coordenadas desde el punto
    /// especificado.
    /// </summary>
    /// <returns>La magnitud resultante entre ambos puntos.</returns>
    /// <param name="fromPoint">
    /// Punto de referencia para calcular la
    /// magnitud.
    /// </param>
    public readonly double Magnitude(Point fromPoint)
    {
        double x = X - fromPoint.X, y = Y - fromPoint.Y;
        return Sqrt((x * x) + (y * y));
    }

    /// <summary>
    /// Calcula la magnitud de las coordenadas desde el punto
    /// especificado.
    /// </summary>
    /// <returns>
    /// La magnitud resultante entre el punto y las coordenadas
    /// especificadas.
    /// </returns>
    /// <param name="fromX">Coordenada X de origen.</param>
    /// <param name="fromY">Coordenada Y de origen.</param>
    public readonly double Magnitude(double fromX, double fromY)
    {
        double x = X - fromX, y = Y - fromY;
        return Sqrt((x * x) + (y * y));
    }

    /// <summary>
    /// Convierte este objeto en su representación como una cadena.
    /// </summary>
    /// <param name="format">Formato a utilizar.</param>
    /// <param name="formatProvider">
    /// Parámetro opcional.
    /// Proveedor de formato de la cultura a utilizar para dar formato a
    /// la representación como una cadena de este objeto. Si se omite,
    /// se utilizará <see cref="CI.CurrentCulture" />.
    /// </param>
    /// <returns>
    /// Una representación en forma de <see cref="string" /> de este objeto.
    /// </returns>
    public readonly string ToString(string? format, IFormatProvider? formatProvider)
    {
        if (format.IsEmpty()) format = "C";
        return format.ToUpperInvariant()[0] switch
        {
            'C' => $"{X}, {Y}",
            'B' => $"[{X}, {Y}]",
            'V' => $"X: {X}, Y: {Y}",
            'N' => $"X: {X}\nY: {Y}",
            _ => throw Errors.FormatNotSupported(format),
        };
    }

    /// <summary>
    /// Convierte este objeto en su representación como una cadena.
    /// </summary>
    /// <param name="format">Formato a utilizar.</param>
    /// <returns>
    /// Una representación en forma de <see cref="string" /> de este objeto.
    /// </returns>
    public readonly string ToString(string? format)
    {
        return ToString(format, CI.CurrentCulture);
    }

    /// <summary>
    /// Indica si esta instancia y un objeto especificado son iguales.
    /// </summary>
    /// <param name="obj">
    /// Objeto que se va a compara con la instancia actual.
    /// </param>
    /// <returns>
    /// <see langword="true" /> si esta instancia y
    /// <paramref name="obj" /> son iguales, <see langword="false" />
    /// en caso contrario.
    /// </returns>
    public override readonly bool Equals(object? obj)
    {
        return obj is IVector p && this == p;
    }

    /// <summary>
    /// Devuelve el código Hash de esta instancia.
    /// </summary>
    /// <returns>El código Hash de esta instancia.</returns>
    public override readonly int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }

    /// <summary>
    /// Convierte este objeto en su representación como una cadena.
    /// </summary>
    /// <returns>
    /// Una representación en forma de <see cref="string" /> de este objeto.
    /// </returns>
    public override readonly string ToString()
    {
        return ToString(null);
    }

    /// <summary>
    /// Compara la igualdad de los vectores.
    /// </summary>
    /// <param name="other">
    /// <see cref="IVector" /> contra el cual comparar.
    /// </param>
    /// <returns>
    /// <see langword="true" /> si todos los vectores de ambos objetos
    /// son iguales, <see langword="false" /> en caso contrario.
    /// </returns>
    public readonly bool Equals(IVector? other)
    {
        return other is { } o && this == o;
    }

    /// <summary>
    /// Implicitly converts a <see cref="Point"/> to a <see cref="Vector2"/>.
    /// </summary>
    /// <param name="p"><see cref="Point"/> value to be converted.</param>
    public static implicit operator Vector2(Point p) => new((float)p.X, (float)p.Y);

    /// <summary>
    /// Implicitly converts a <see cref="Vector2"/> to a <see cref="Point"/>.
    /// </summary>
    /// <param name="p"><see cref="Vector2"/> value to be converted.</param>
    public static implicit operator Point(Vector2 p) => new(p.X, p.Y);
}
