/*
Point3D.cs

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
using System.Diagnostics.CodeAnalysis;
using TheXDS.MCART.Helpers;
using TheXDS.MCART.Math;
using TheXDS.MCART.Misc;
using TheXDS.MCART.Types.Base;
using static TheXDS.MCART.Types.Extensions.StringExtensions;
using CI = System.Globalization.CultureInfo;
using St = TheXDS.MCART.Resources.Strings;

namespace TheXDS.MCART.Types
{
    /// <summary>
    /// Tipo universal para un conjunto de coordenadas tridimensionales.
    /// </summary>
    /// <remarks>
    /// Esta estructura se declara como parcial, para permitir a cada
    /// implementación de MCART definir métodos para convertir a la clase
    /// correspondiente para los diferentes tipos de UI disponibles.
    /// </remarks>
    public partial struct Point3D : IFormattable, IEquatable<Point3D>, I3DVector
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
        /// Obtiene un punto en el orígen. Este campo es de solo lectura.
        /// </summary>
        /// <value>
        /// Un <see cref="Point3D" /> con sus coordenadas en el orígen.
        /// </value>
        public static readonly Point3D Origin = new(0, 0, 0);

        /// <summary>
        /// Obtiene un punto en el orígen bidimensional. Este campo es de
        /// solo lectura.
        /// </summary>
        /// <value>
        /// Un <see cref="Point3D" /> con sus coordenadas en el orígen 
        /// bidimensional.
        /// </value>
        public static readonly Point3D Origin2D = new(0, 0, double.NaN);

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
                    var separators = new[]
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
                    return PrivateInternals.TryParseValues<double, Point3D>(separators, value.Without("()[]{}".ToCharArray()), 3, l => new Point3D(l[0], l[1], l[2]), out point);
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
            if (TryParse(value, out var retval)) return retval;
            throw new FormatException();
        }

        /// <summary>
        /// Realiza una operación de suma sobre los puntos.
        /// </summary>
        /// <param name="l">Punto 1.</param>
        /// <param name="r">Punto 2.</param>
        /// <returns>La suma de los vectores de los puntos.</returns>
        public static Point3D operator +(Point3D l, Point3D r)
        {
            return new Point3D(l.X + r.X, l.Y + r.Y, l.Z + r.Z);
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
            return new Point3D(l.X + r, l.Y + r, l.Z + r);
        }

        /// <summary>
        /// Realiza una operación de resta sobre los puntos.
        /// </summary>
        /// <param name="l">Punto 1.</param>
        /// <param name="r">Punto 2.</param>
        /// <returns>La resta de los vectores de los puntos.</returns>
        public static Point3D operator -(Point3D l, I3DVector r)
        {
            return new Point3D(l.X - r.X, l.Y - r.Y, l.Z - r.Z);
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
            return new Point3D(l.X - r, l.Y - r, l.Z - r);
        }

        /// <summary>
        /// Realiza una operación de multiplicación sobre los puntos.
        /// </summary>
        /// <param name="l">Punto 1.</param>
        /// <param name="r">Punto 2.</param>
        /// <returns>La multiplicación de los vectores de los puntos.</returns>
        public static Point3D operator *(Point3D l, I3DVector r)
        {
            return new Point3D(l.X * r.X, l.Y * r.Y, l.Z * r.Z);
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
            return new Point3D(l.X * r, l.Y * r, l.Z * r);
        }

        /// <summary>
        /// Realiza una operación de división sobre los puntos.
        /// </summary>
        /// <param name="l">Punto 1.</param>
        /// <param name="r">Punto 2.</param>
        /// <returns>La división de los vectores de los puntos.</returns>
        public static Point3D operator /(Point3D l, I3DVector r)
        {
            return new Point3D(l.X / r.X, l.Y / r.Y, l.Z / r.Z);
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
            return new Point3D(l.X / r, l.Y / r, l.Z / r);
        }

        /// <summary>
        /// Realiza una operación de resíduo sobre los puntos.
        /// </summary>
        /// <param name="l">Punto 1.</param>
        /// <param name="r">Punto 2.</param>
        /// <returns>El resíduo de los vectores de los puntos.</returns>
        public static Point3D operator %(Point3D l, I3DVector r)
        {
            return new Point3D(l.X % r.X, l.Y % r.Y, l.Z % r.Z);
        }

        /// <summary>
        /// Realiza una operación de resíduo sobre el punto.
        /// </summary>
        /// <param name="l">Punto 1.</param>
        /// <param name="r">Operando de resíduo.</param>
        /// <returns>
        /// Un nuevo <see cref="Point3D" /> cuyos vectores son el resíduo de los
        /// vectores originales % <paramref name="r" />.
        /// </returns>
        public static Point3D operator %(Point3D l, double r)
        {
            return new Point3D(l.X % r, l.Y % r, l.Z % r);
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
            return new Point3D(+p.X, +p.Y, +p.Z);
        }

        /// <summary>
        /// Invierte el signo de los vectores del punto.
        /// </summary>
        /// <param name="p">Punto a operar.</param>
        /// <returns>Un punto con el signo de sus vectores invertido.</returns>
        public static Point3D operator -(Point3D p)
        {
            return new Point3D(-p.X, -p.Y, -p.Z);
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
        public static bool operator ==(Point3D l, I3DVector r)
        {
            return l.X == r.X && l.Y == r.Y && l.Z == r.Z;
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
        public static bool operator !=(Point3D l, I3DVector r)
        {
            return l.X != r.X && l.Y != r.Y && l.Z != r.Z;
        }
        
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
        public bool WithinCube(double x1, double y1, double z1, double x2, double y2, double z2)
        {
            return X.IsBetween(x1, x2) && Y.IsBetween(y1, y2) && Z.IsBetween(z1, z2);
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
        /// La magnitud resultante entre el punto y el orígen.
        /// </returns>
        public double Magnitude()
        {
            return System.Math.Sqrt(X * X + Y * Y + Z * Z);
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
            return System.Math.Sqrt(x * x + y * y + z * z);
        }

        /// <summary>
        /// Calcula la magnitud de las coordenadas desde el punto
        /// especificado.
        /// </summary>
        /// <returns>
        /// La magnitud resultante entre el punto y las coordenadas
        /// especificadas.
        /// </returns>
        /// <param name="fromX">Coordenada X de orígen.</param>
        /// <param name="fromY">Coordenada Y de orígen.</param>
        /// <param name="fromZ">Coordenada Z de orígen.</param>
        public double Magnitude(double fromX, double fromY, double fromZ)
        {
            double x = X - fromX, y = Y - fromY, z = Z - fromZ;
            return System.Math.Sqrt(x * x + y * y + z * z);
        }

        /// <summary>
        /// Convierte este objeto en su representación como una cadena.
        /// </summary>
        /// <param name="format">Formato a utilizar.</param>
        /// <param name="formatProvider">
        /// Parámetro opcional.
        /// Proveedor de formato de la cultura a utilizar para dar formato a
        /// la representación como una cadena de este objeto. Si se omite,
        /// se utilizará <see cref="P:System.Globalization.CultureInfo.CurrentCulture" />.
        /// </param>
        /// <returns>
        /// Una representación en forma de <see cref="string" /> de este objeto.
        /// </returns>
        public string ToString(string? format, IFormatProvider? formatProvider)
        {
            if (format.IsEmpty()) format = "C";
            return format!.ToUpperInvariant()[0] switch
            {
                'C' => $"{X}, {Y}, {Z}",
                'B' => $"[{X}, {Y}, {Z}]",
                'V' => $"X: {X}, Y: {Y}, Z: {Z}",
                'N' => $"X: {X}{Environment.NewLine}Y: {Y}{Environment.NewLine}Z: {Z}",
                _ => throw new FormatException(St.FormatNotSupported(format)),
            };
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
            if (!(obj is Point3D p)) return false;
            return this == p;
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
        /// Indica si esta instancia y un objeto especificado son iguales.
        /// </summary>
        /// <param name="other">
        /// Objeto que se va a compara con la instancia actual.
        /// </param>
        /// <returns>
        /// <see langword="true" /> si esta instancia y <paramref name="other" /> son iguales;
        /// de lo contrario, <see langword="false" />.
        /// </returns>
        public bool Equals([AllowNull] I2DVector other) => X == other?.X && Y == other.Y && !Z.IsValid();
    }
}