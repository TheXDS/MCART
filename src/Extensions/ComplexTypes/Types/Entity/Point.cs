/*
Point.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2022 César Andrés Morgan

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

namespace TheXDS.MCART.Types.Entity;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using TheXDS.MCART.Resources;
using TheXDS.MCART.Types.Base;
using static TheXDS.MCART.Types.Extensions.StringExtensions;
using CI = System.Globalization.CultureInfo;

/// <summary>
/// Tipo universal para un conjunto de coordenadas bidimensionales.
/// </summary>
[ComplexType]
public class Point : I2DVector, IFormattable, IEquatable<Point>
{
    /// <summary>
    /// Coordenada X.
    /// </summary>
    public double X { get; set; }

    /// <summary>
    /// Coordenada Y.
    /// </summary>
    public double Y { get; set; }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="Point" />.
    /// </summary>
    public Point()
    {
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="Point" />.
    /// </summary>
    /// <param name="x">The x coordinate.</param>
    /// <param name="y">The y coordinate.</param>
    public Point(double x, double y)
    {
        X = x;
        Y = y;
    }

    /// <summary>
    /// Compara la igualdad de los vectores.
    /// </summary>
    /// <param name="other">
    /// <see cref="I2DVector" /> contra el cual comparar.
    /// </param>
    /// <returns>
    /// <see langword="true" /> si todos los vectores de ambos objetos
    /// son iguales, <see langword="false" /> en caso contrario.
    /// </returns>
    public bool Equals(I2DVector? other)
    {
        return other is { } o && this == o;
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
    public bool Equals(Point? other)
    {
        return other is { } o && this == o;
    }

    /// <summary>
    /// Indica si esta instancia y un objeto especificado son iguales.
    /// </summary>
    /// <param name="obj">
    /// Objeto que se va a compara con la instancia actual.
    /// </param>
    /// <returns>
    /// <see langword="true" /> si esta instancia y <paramref name="obj" /> son iguales;
    /// de lo contrario, <see langword="false" />.
    /// </returns>
    public override bool Equals(object? obj)
    {
        return obj is I2DVector p && this == p;
    }

    /// <summary>
    /// Convierte este objeto en su representación como una cadena.
    /// </summary>
    /// <returns>
    /// Una representación en forma de <see cref="string" /> de este objeto.
    /// </returns>
    public override string ToString()
    {
        return ToString(null);
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
    public string ToString(string? format, IFormatProvider? formatProvider)
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
    public string ToString(string? format)
    {
        return ToString(format, CI.CurrentCulture);
    }

    /// <summary>
    /// Devuelve el código Hash de esta instancia.
    /// </summary>
    /// <returns>El código Hash de esta instancia.</returns>
    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }

    /// <summary>
    /// Compara la igualdad de los vectores de los puntos.
    /// </summary>
    /// <param name="l">Punto 1.</param>
    /// <param name="r">Punto 2.</param>
    /// <returns>
    /// <see langword="true" /> si todos los vectores de ambos puntos son iguales;
    /// de lo contrario, <see langword="false" />.
    /// </returns>
    public static bool operator ==(Point l, Point r)
    {
        return l.X == r.X && l.Y == r.Y;
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
    public static bool operator ==(Point l, I2DVector r)
    {
        return l.X == r.X && l.Y == r.Y;
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
        return l.X != r.X || l.Y != r.Y;
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
    public static bool operator !=(Point l, I2DVector r)
    {
        return l.X != r.X || l.Y != r.Y;
    }

    /// <summary>
    /// Convierte implícitamente un <see cref="Point"/> en un
    /// <see cref="Types.Point"/>.
    /// </summary>
    /// <param name="p">
    /// <see cref="Point"/> a convertir.
    /// </param>
    public static implicit operator Types.Point(Point p)
    {
        return new Types.Point(p.X, p.Y);
    }

    /// <summary>
    /// Convierte implícitamente un <see cref="Types.Point"/> en un
    /// <see cref="Point"/>.
    /// </summary>
    /// <param name="p">
    /// <see cref="Types.Point"/> a convertir.
    /// </param>
    public static implicit operator Point(Types.Point p)
    {
        return new Point(p.X, p.Y);
    }
}
