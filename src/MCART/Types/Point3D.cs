/*
Point3D.cs

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

using System;
using System.Linq;
using TheXDS.MCART.Helpers;
using TheXDS.MCART.Math;
using TheXDS.MCART.Misc;
using TheXDS.MCART.Resources;
using TheXDS.MCART.Types.Base;
using TheXDS.MCART.Types.Extensions;
using CI = System.Globalization.CultureInfo;

namespace TheXDS.MCART.Types;

/// <summary>
/// Tipo universal para un conjunto de coordenadas tridimensionales.
/// </summary>
/// <remarks>
/// Esta estructura se declara como parcial, para permitir a cada
/// implementación de MCART definir métodos para convertir a la clase
/// correspondiente para los diferentes tipos de UI disponibles.
/// </remarks>
public partial struct Point3D : IFormattable, IEquatable<Point3D>, IVector3D
{
    /// <summary>
    /// Obtiene un punto que no representa ninguna posición. Este campo es
    /// de solo lectura.
    /// </summary>
    /// <value>
    /// Un <see cref="Point3D" /> con sus coordenadas establecidas en
    /// <see cref="double.NaN"/>.
    /// </value>
    public static readonly Point3D Nowhere = new(double.NaN, double.NaN, double.NaN);

    /// <summary>
    /// Obtiene un punto en el origen. Este campo es de solo lectura.
    /// </summary>
    /// <value>
    /// Un <see cref="Point3D" /> con sus coordenadas en el origen.
    /// </value>
    public static readonly Point3D Origin = new(0, 0, 0);

    /// <summary>
    /// Obtiene un punto en el origen bidimensional. Este campo es de
    /// solo lectura.
    /// </summary>
    /// <value>
    /// Un <see cref="Point3D" /> con sus coordenadas en el origen 
    /// bidimensional.
    /// </value>
    public static readonly Point3D Origin2D = new(0, 0, double.NaN);

    /// <summary>
    /// Inicializa una nueva instancia de la estructura
    /// <see cref="Point3D" />.
    /// </summary>
    /// <param name="x">Coordenada X.</param>
    /// <param name="y">Coordenada Y.</param>
    /// <param name="z">Coordenada Z.</param>
    public Point3D(double x, double y, double z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    /// <summary>
    /// Inicializa una nueva instancia de la estructura
    /// <see cref="Point3D" /> para un par de coordenadas bidimensionales.
    /// </summary>
    /// <param name="x">Coordenada X.</param>
    /// <param name="y">Coordenada Y.</param>
    public Point3D(double x, double y) : this(x, y, double.NaN)
    {
    }

    /// <summary>
    /// Realiza una operación de suma sobre los puntos.
    /// </summary>
    /// <param name="l">Punto 1.</param>
    /// <param name="r">Punto 2.</param>
    /// <returns>La suma de los vectores de los puntos.</returns>
    public static Point3D operator +(Point3D l, Point3D r)
    {
        return new(l.X + r.X, l.Y + r.Y, l.Z + r.Z);
    }

    /// <summary>
    /// Realiza una operación de suma sobre los puntos.
    /// </summary>
    /// <param name="l">Punto 1.</param>
    /// <param name="r">Punto 2.</param>
    /// <returns>La suma de los vectores de los puntos.</returns>
    public static Point3D operator +(Point3D l, IVector3D r)
    {
        return new(l.X + r.X, l.Y + r.Y, l.Z + r.Z);
    }

    /// <summary>
    /// Realiza una operación de suma sobre el punto.
    /// </summary>
    /// <param name="l">Punto 1.</param>
    /// <param name="r">Operando de suma.</param>
    /// <returns>
    /// Un nuevo <see cref="Point3D" /> cuyos vectores son la suma de los
    /// vectores originales + <paramref name="r" />.
    /// </returns>
    public static Point3D operator +(Point3D l, double r)
    {
        return new(l.X + r, l.Y + r, l.Z + r);
    }

    /// <summary>
    /// Realiza una operación de resta sobre los puntos.
    /// </summary>
    /// <param name="l">Punto 1.</param>
    /// <param name="r">Punto 2.</param>
    /// <returns>La resta de los vectores de los puntos.</returns>
    public static Point3D operator -(Point3D l, IVector3D r)
    {
        return new(l.X - r.X, l.Y - r.Y, l.Z - r.Z);
    }

    /// <summary>
    /// Realiza una operación de resta sobre el punto.
    /// </summary>
    /// <param name="l">Punto 1.</param>
    /// <param name="r">Operando de resta.</param>
    /// <returns>
    /// Un nuevo <see cref="Point3D" /> cuyos vectores son la resta de los
    /// vectores originales - <paramref name="r" />.
    /// </returns>
    public static Point3D operator -(Point3D l, double r)
    {
        return new(l.X - r, l.Y - r, l.Z - r);
    }

    /// <summary>
    /// Realiza una operación de multiplicación sobre los puntos.
    /// </summary>
    /// <param name="l">Punto 1.</param>
    /// <param name="r">Punto 2.</param>
    /// <returns>La multiplicación de los vectores de los puntos.</returns>
    public static Point3D operator *(Point3D l, IVector3D r)
    {
        return new(l.X * r.X, l.Y * r.Y, l.Z * r.Z);
    }

    /// <summary>
    /// Realiza una operación de multiplicación sobre el punto.
    /// </summary>
    /// <param name="l">Punto 1.</param>
    /// <param name="r">Operando de multiplicación.</param>
    /// <returns>
    /// Un nuevo <see cref="Point3D" /> cuyos vectores son la multiplicación
    /// de los vectores originales * <paramref name="r" />.
    /// </returns>
    public static Point3D operator *(Point3D l, double r)
    {
        return new(l.X * r, l.Y * r, l.Z * r);
    }

    /// <summary>
    /// Realiza una operación de división sobre los puntos.
    /// </summary>
    /// <param name="l">Punto 1.</param>
    /// <param name="r">Punto 2.</param>
    /// <returns>La división de los vectores de los puntos.</returns>
    public static Point3D operator /(Point3D l, IVector3D r)
    {
        return new(l.X / r.X, l.Y / r.Y, l.Z / r.Z);
    }

    /// <summary>
    /// Realiza una operación de división sobre el punto.
    /// </summary>
    /// <param name="l">Punto 1.</param>
    /// <param name="r">Operando de división.</param>
    /// <returns>
    /// Un nuevo <see cref="Point3D" /> cuyos vectores son la división de
    /// los vectores originales / <paramref name="r" />.
    /// </returns>
    public static Point3D operator /(Point3D l, double r)
    {
        return new(l.X / r, l.Y / r, l.Z / r);
    }

    /// <summary>
    /// Realiza una operación de residuo sobre los puntos.
    /// </summary>
    /// <param name="l">Punto 1.</param>
    /// <param name="r">Punto 2.</param>
    /// <returns>El residuo de los vectores de los puntos.</returns>
    public static Point3D operator %(Point3D l, IVector3D r)
    {
        return new(l.X % r.X, l.Y % r.Y, l.Z % r.Z);
    }

    /// <summary>
    /// Realiza una operación de residuo sobre el punto.
    /// </summary>
    /// <param name="l">Punto 1.</param>
    /// <param name="r">Operando de residuo.</param>
    /// <returns>
    /// Un nuevo <see cref="Point3D" /> cuyos vectores son el residuo de los
    /// vectores originales % <paramref name="r" />.
    /// </returns>
    public static Point3D operator %(Point3D l, double r)
    {
        return new(l.X % r, l.Y % r, l.Z % r);
    }

    /// <summary>
    /// Incrementa en 1 los vectores del punto.
    /// </summary>
    /// <param name="p">Punto a incrementar.</param>
    /// <returns>Un punto con sus vectores incrementados en 1.</returns>
    public static Point3D operator ++(Point3D p)
    {
        p.X++;
        p.Y++;
        p.Z++;
        return p;
    }

    /// <summary>
    /// Decrementa en 1 los vectores del punto.
    /// </summary>
    /// <param name="p">Punto a decrementar.</param>
    /// <returns>Un punto con sus vectores decrementados en 1.</returns>
    public static Point3D operator --(Point3D p)
    {
        p.X--;
        p.Y--;
        p.Z--;
        return p;
    }

    /// <summary>
    /// Convierte a positivos los vectores del punto.
    /// </summary>
    /// <param name="p">Punto a operar.</param>
    /// <returns>Un punto con sus vectores positivos.</returns>
    public static Point3D operator +(Point3D p)
    {
        return new(+p.X, +p.Y, +p.Z);
    }

    /// <summary>
    /// Invierte el signo de los vectores del punto.
    /// </summary>
    /// <param name="p">Punto a operar.</param>
    /// <returns>Un punto con el signo de sus vectores invertido.</returns>
    public static Point3D operator -(Point3D p)
    {
        return new(-p.X, -p.Y, -p.Z);
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
    public static bool operator ==(Point3D l, IVector3D r)
    {
        return l.X.Equals(r.X) && l.Y.Equals(r.Y) && l.Z.Equals(r.Z);
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
    public static bool operator !=(Point3D l, IVector3D r)
    {
        return !(l == r);
    }

    /// <summary>
    /// Convierte implícitamente un <see cref="Point3D"/> en un
    /// <see cref="Point"/>.
    /// </summary>
    /// <param name="p">Objeto a convertir.</param>
    /// <returns>
    /// Un nuevo <see cref="Point"/> con los mismos valores de
    /// <see cref="X"/> y <see cref="Y"/> que el <see cref="Point3D"/>
    /// original.
    /// </returns>
    public static implicit operator Point(Point3D p) => new(p.X, p.Y);

    /// <summary>
    /// Convierte implícitamente un <see cref="Point"/> en un
    /// <see cref="Point3D"/>.
    /// </summary>
    /// <param name="p">Objeto a convertir.</param>
    /// <returns>
    /// Un nuevo <see cref="Point3D"/> con los mismos valores de
    /// <see cref="X"/> y <see cref="Y"/> que el <see cref="Point"/>
    /// original, y valor en <see cref="Z"/> de <see cref="double.NaN"/>.
    /// </returns>
    public static implicit operator Point3D(Point p) => new(p.X, p.Y, double.NaN);

    /// <summary>
    /// Coordenada X.
    /// </summary>
    public double X { get; set; }

    /// <summary>
    /// Coordenada Y.
    /// </summary>
    public double Y { get; set; }

    /// <summary>
    /// Coordenada Z.
    /// </summary>
    public double Z { get; set; }

    /// <summary>
    /// Intenta crear un <see cref="Point3D"/> a partir de una cadena.
    /// </summary>
    /// <param name="value">
    /// Valor a partir del cual crear un <see cref="Point3D"/>.
    /// </param>
    /// <param name="point">
    /// <see cref="Point3D"/> que ha sido creado.
    /// </param>
    /// <returns>
    /// <see langword="true"/> si la conversión ha tenido éxito,
    /// <see langword="false"/> en caso contrario.
    /// </returns>
    public static bool TryParse(string value, out Point3D point)
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
            case nameof(Origin2D):
                point = Origin2D;
                break;
            default:
                string[] separators =
                {
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
                    };
                return PrivateInternals.TryParseValues<double, Point3D>(
                    separators,
                    value.Without("()[]{}".ToCharArray()),
                    3,
                    l => new Point3D(l[0], l[1], l[2]),
                    out point);
        }
        return true;
    }

    /// <summary>
    /// Crea un <see cref="Point3D"/> a partir de una cadena.
    /// </summary>
    /// <param name="value">
    /// Valor a partir del cual crear un <see cref="Point"/>.
    /// </param>
    /// <exception cref="FormatException">
    /// Se produce si la conversión ha fallado.
    /// </exception>
    /// <returns><see cref="Point3D"/> que ha sido creado.</returns>
    public static Point3D Parse(string value)
    {
        if (TryParse(value, out Point3D retVal)) return retVal;
        throw new FormatException();
    }

    /// <summary>
    /// Compara la igualdad de los vectores de los puntos.
    /// </summary>
    /// <param name="other">
    /// <see cref="Point3D" /> contra el cual comparar.
    /// </param>
    /// <returns>
    /// <see langword="true" /> si todos los vectores de ambos puntos son iguales;
    /// de lo contrario, <see langword="false" />.
    /// </returns>
    public bool Equals(Point3D other)
    {
        return this == other;
    }

    /// <summary>
    /// Determina si el punto se encuentra dentro del cubo formado por los
    /// puntos tridimensionales especificados.
    /// </summary>
    /// <returns>
    /// <see langword="true" /> si el punto se encuentra dentro del cubo formado,
    /// <see langword="false" /> en caso contrario.
    /// </returns>
    /// <param name="p1">Punto 1.</param>
    /// <param name="p2">Punto 2.</param>
    public bool WithinCube(Point3D p1, Point3D p2)
    {
        return X.IsBetween(p1.X, p2.X) && Y.IsBetween(p1.Y, p2.Y) && Z.IsBetween(p1.Z, p2.Z);
    }

    /// <summary>
    /// Determina si el punto se encuentra dentro del cubo formado por los
    /// puntos tridimensionales especificados.
    /// </summary>
    /// <returns>
    /// <see langword="true" /> si el punto se encuentra dentro del cubo formado,
    /// <see langword="false" /> en caso contrario.
    /// </returns>
    /// <param name="x1">La primer coordenada x.</param>
    /// <param name="y1">La primer coordenada y.</param>
    /// <param name="z1">La primer coordenada z.</param>
    /// <param name="x2">La segunda coordenada x.</param>
    /// <param name="y2">La segunda coordenada y.</param>
    /// <param name="z2">La segunda coordenada z.</param>
    public readonly bool WithinCube(in double x1, in double y1, in double z1, in double x2, in double y2, in double z2)
    {
        double[] x = new[] { x1, x2 }.Ordered().ToArray();
        double[] y = new[] { y1, y2 }.Ordered().ToArray();
        double[] z = new[] { z1, z2 }.Ordered().ToArray();
        return X.IsBetween(x[0], x[1]) && Y.IsBetween(y[0], y[1]) && Z.IsBetween(z[0], z[1]);
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
    /// <param name="z">Rango de valores para el eje Z.</param>
    public bool WithinCube(Range<double> x, Range<double> y, Range<double> z)
    {
        return x.IsWithin(X) && y.IsWithin(Y) && z.IsWithin(Z);
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
    /// <param name="topLeftFront">Coordenadas de esquina superior izquierda frontal</param>
    public readonly bool WithinCube(in Size3D size, in Point3D topLeftFront)
    {
        double[] x = new[] { topLeftFront.X, topLeftFront.X + size.Width }.Ordered().ToArray();
        double[] y = new[] { topLeftFront.Y, topLeftFront.Y - size.Height }.Ordered().ToArray();
        double[] z = new[] { topLeftFront.Z, topLeftFront.Z - size.Depth }.Ordered().ToArray();
        return WithinCube(x[0], y[0], z[0], x[1], y[1], z[1]);
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
    public readonly bool WithinCube(in Size3D size)
    {
        return WithinCube(size, new Point3D(-(size.Width / 2), size.Height / 2, size.Depth / 2));
    }

    /// <summary>
    /// Determina si el punto se encuentra dentro de la esfera especificada.
    /// </summary>
    /// <param name="center">Punto central de la esfera.</param>
    /// <param name="radius">Radio del círculo.</param>
    /// <returns>
    /// <see langword="true" /> si el punto se encuentra dentro de la esfera,
    /// <see langword="false" /> en caso contrario.
    /// </returns>
    public bool WithinSphere(Point3D center, double radius)
    {
        return Magnitude(center) <= radius;
    }

    /// <summary>
    /// Calcula la magnitud de las coordenadas.
    /// </summary>
    /// <returns>
    /// La magnitud resultante entre el punto y el origen.
    /// </returns>
    public double Magnitude()
    {
        return System.Math.Sqrt((X * X) + (Y * Y) + (Z * Z));
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
    public double Magnitude(Point3D fromPoint)
    {
        double x = X - fromPoint.X, y = Y - fromPoint.Y, z = Z - fromPoint.Z;
        return System.Math.Sqrt((x * x) + (y * y) + (z * z));
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
    /// <param name="fromZ">Coordenada Z de origen.</param>
    public double Magnitude(double fromX, double fromY, double fromZ)
    {
        double x = X - fromX, y = Y - fromY, z = Z - fromZ;
        return System.Math.Sqrt((x * x) + (y * y) + (z * z));
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
        return obj is Point3D p && this == p;
    }

    /// <summary>
    /// Devuelve el código Hash de esta instancia.
    /// </summary>
    /// <returns>El código Hash de esta instancia.</returns>
    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y, Z);
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
    /// <returns>
    /// Una representación en forma de <see cref="string" /> de este objeto.
    /// </returns>
    public string ToString(string? format)
    {
        return ToString(format, CI.CurrentCulture);
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
            'C' => $"{X}, {Y}, {Z}",
            'B' => $"[{X}, {Y}, {Z}]",
            'V' => $"X: {X}, Y: {Y}, Z: {Z}",
            'N' => $"X: {X}{Environment.NewLine}Y: {Y}{Environment.NewLine}Z: {Z}",
            _ => throw Errors.FormatNotSupported(format),
        };
    }

    /// <summary>
    /// Indica si esta instancia y un objeto especificado son iguales.
    /// </summary>
    /// <param name="other">
    /// Objeto que se va a compara con la instancia actual.
    /// </param>
    /// <returns>
    /// <see langword="true" /> si esta instancia y <paramref name="other" /> son iguales;
    /// de lo contrario, <see langword="false" />.
    /// </returns>
    public bool Equals(IVector? other) => other is not null && X.Equals(other.X) && Y.Equals(other.Y) && !Z.IsValid();

    /// <summary>
    /// Indica si esta instancia y un objeto especificado son iguales.
    /// </summary>
    /// <param name="other">
    /// Objeto que se va a compara con la instancia actual.
    /// </param>
    /// <returns>
    /// <see langword="true" /> si esta instancia y <paramref name="other" /> son iguales;
    /// de lo contrario, <see langword="false" />.
    /// </returns>
    public bool Equals(IVector3D? other) => other is not null && X.Equals(other.X) && Y.Equals(other.Y) && Z.Equals(other.Z);
}
