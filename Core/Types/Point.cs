//
//  Point.cs
//
//  This file is part of MCART
//
//  Author:
//       César Andrés Morgan <xds_xps_ivx@hotmail.com>
//
//  Copyright (c) 2011 - 2017 César Andrés Morgan
//
//  MCART is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  MCART is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
using System;
using St = MCART.Resources.Strings;
using CI = System.Globalization.CultureInfo;
namespace MCART.Types
{
    /// <summary>
    /// Tipo universal para un conjunto de coordenadas bidimensionales.
    /// </summary>
    /// <remarks>
    /// Esta estructura se declara como parcial, para permitir a cada
    /// implementación de MCART definir métodos para convertir a la clase
    /// correspondiente para los diferentes tipos de UI disponibles.
    /// </remarks>
    public partial struct Point : IFormattable
    {
        /// <summary>
        /// Coordenada X.
        /// </summary>
        public double X;
        /// <summary>
        /// Coordenada Y.
        /// </summary>
        public double Y;
        /// <summary>
        /// Inicializa una nueva instancia de la estructura <see cref="T:MCART.Math.Functions.Point"/>.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        public Point(double x, double y) { X = x; Y = y; }
        /// <summary>
        /// Determina si el punto se encuentra dentro del rectángulo formado
        /// por los puntos especificados.
        /// </summary>
        /// <returns>
        /// <c>true</c> si el punto se encuentra dentro del rectángulo
        /// formado; de lo contrario, <c>false</c>.
        /// </returns>
        /// <param name="p1">Punto 1.</param>
        /// <param name="p2">Punto 2.</param>
        public bool FitsInBox(Point p1, Point p2)
        {
            return X.IsBetween(p1.X, p2.X) && Y.IsBetween(p1.Y, p2.Y);
        }
        /// <summary>
        /// Determina si el punto se encuentra dentro del rectángulo formado
        /// por las coordenadas especificadas.
        /// </summary>
        /// <returns>
        /// <c>true</c> si el punto se encuentra dentro del rectángulo
        /// formado; de lo contrario, <c>false</c>.
        /// </returns>
        /// <param name="x1">The first x value.</param>
        /// <param name="y1">The first y value.</param>
        /// <param name="x2">The second x value.</param>
        /// <param name="y2">The second y value.</param>
        public bool FitsInBox(double x1, double y1, double x2, double y2)
        {
            return X.IsBetween(x1, x2) && Y.IsBetween(y1, y2);
        }
        /// <summary>
        /// Calcula la magnitud de las coordenadas.
        /// </summary>
        /// <returns>The magnitude.</returns>
        public double Magnitude()
        {
            return System.Math.Sqrt((X * X) + (Y * Y));
        }
        /// <summary>
        /// Calcula la magnitud de las coordenadas desde el punto
        /// especificado.
        /// </summary>
        /// <returns>The magnitude.</returns>
        /// <param name="From">Punto de referencia para calcular la
        /// magnitud.</param>
        public double Magnitude(Point From)
        {
            double x = X - From.X, y = Y - From.Y;
            return System.Math.Sqrt((x * x) + (y * y));
        }
        /// <summary>
        /// Calcula el ángulo formado por la línea que intersecta el orígen
        /// y este <see cref="Point"/> contra el eje horizontal X.
        /// </summary>
        /// <returns>El ángulo calculado.</returns>
        public double Angle()
        {
            double ang = System.Math.Acos(X / Magnitude());
            if (Y < 0) ang += System.Math.Acos(-1);
            return ang;
        }
        /// <summary>
        /// Obtiene un punto en el orígen. Este campo es de solo lectura.
        /// </summary>
        /// <value>
        /// Un <see cref="Point"/> con sus coordenadas en el orígen.
        /// </value>
        public static Point Origin => new Point(0, 0);
        /// <summary>
        /// Realiza una operación de suma sobre los puntos.
        /// </summary>
        /// <param name="l">Punto 1.</param>
        /// <param name="r">Punto 2.</param>
        /// <returns>La suma de los vectores de los puntos.</returns>
        public static Point operator +(Point l, Point r) => new Point(l.X + r.X, l.Y + r.Y);
        /// <summary>
        /// Realiza una operación de resta sobre los puntos.
        /// </summary>
        /// <param name="l">Punto 1.</param>
        /// <param name="r">Punto 2.</param>
        /// <returns>La resta de los vectores de los puntos.</returns>
        public static Point operator -(Point l, Point r) => new Point(l.X - r.X, l.Y - r.Y);
        /// <summary>
        /// Realiza una operación de multiplicación sobre los puntos.
        /// </summary>
        /// <param name="l">Punto 1.</param>
        /// <param name="r">Punto 2.</param>
        /// <returns>La multiplicación de los vectores de los puntos.</returns>
        public static Point operator *(Point l, Point r) => new Point(l.X * r.X, l.Y * r.Y);
        /// <summary>
        /// Realiza una operación de división sobre los puntos.
        /// </summary>
        /// <param name="l">Punto 1.</param>
        /// <param name="r">Punto 2.</param>
        /// <returns>La división de los vectores de los puntos.</returns>
        public static Point operator /(Point l, Point r) => new Point(l.X / r.X, l.Y / r.Y);
        /// <summary>
        /// Realiza una operación de resíduo sobre los puntos.
        /// </summary>
        /// <param name="l">Punto 1.</param>
        /// <param name="r">Punto 2.</param>
        /// <returns>El resíduo de los vectores de los puntos.</returns>
        public static Point operator %(Point l, Point r) => new Point(l.X % r.X, l.Y % r.Y);
        /// <summary>
        /// Incrementa en 1 los vectores del punto.
        /// </summary>
        /// <param name="p">Punto a incrementar.</param>
        /// <returns>Un punto con sus vectores incrementados en 1.</returns>
        public static Point operator ++(Point p)
        {
            p.X++; p.Y++;
            return p;
        }
        /// <summary>
        /// Decrementa en 1 los vectores del punto.
        /// </summary>
        /// <param name="p">Punto a decrementar.</param>
        /// <returns>Un punto con sus vectores decrementados en 1.</returns>
        public static Point operator --(Point p)
        {
            p.X--; p.Y--;
            return p;
        }
        /// <summary>
        /// Convierte a positivos los vectores del punto.
        /// </summary>
        /// <param name="p">Punto a operar.</param>
        /// <returns>Un punto con sus vectores positivos.</returns>
        public static Point operator +(Point p) => new Point(+p.X, +p.Y);
        /// <summary>
        /// Invierte el signo de los vectores del punto.
        /// </summary>
        /// <param name="p">Punto a operar.</param>
        /// <returns>Un punto con el signo de sus vectores invertido.</returns>
        public static Point operator -(Point p) => new Point(-p.X, -p.Y);
#pragma warning disable RECS0018 // Comparison of floating point numbers with equality operator
        /// <summary>
        /// Compara la igualdad de los vectores de los puntos.
        /// </summary>
        /// <param name="l">Punto 1.</param>
        /// <param name="r">Punto 2.</param>
        /// <returns>
        /// <c>true</c> si todos los vectores de ambos puntos son iguales;
        /// de lo contrario, <c>false</c>.</returns>
        public static bool operator ==(Point l, Point r)
        {
            return (l.X == r.X && l.Y == r.Y);
        }
        /// <summary>
        /// Compara la diferencia de los vectores de los puntos.
        /// </summary>
        /// <param name="l">Punto 1.</param>
        /// <param name="r">Punto 2.</param>
        /// <returns>
        /// <c>true</c> si los vectores de ambos puntos son diferentes;  de lo
        /// contrario, <c>false</c>.</returns>
        public static bool operator !=(Point l, Point r)
        {
            return (l.X != r.X || l.Y != r.Y);
        }
#pragma warning restore RECS0018
        /// <summary>
        /// Indica si esta instancia y un objeto especificado son iguales.
        /// </summary>
        /// <param name="obj">
        /// Objeto que se va a compara con la instancia actual.
        /// </param>
        /// <returns>
        /// <c>true</c> si esta instancia y <paramref name="obj"/> son iguales;
        /// de lo contrario, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj) => base.Equals(obj);
        /// <summary>
        /// Devuelve el código Hash de esta instancia.
        /// </summary>
        /// <returns>El código Hash de esta instancia.</returns>
        public override int GetHashCode() => base.GetHashCode();
        /// <summary>
        /// Convierte este objeto en su representación como una cadena.
        /// </summary>
        /// <returns>Una representación en cadena de este objeto.</returns>
        public override string ToString() => ToString(null, CI.CurrentCulture);
        /// <summary>
        /// Convierte este objeto en su representación como una cadena.
        /// </summary>
        /// <param name="format">Formato a utilizar.</param>
        /// <param name="formatProvider">Parámetro opcional.
        /// Proveedor de formato de la cultura a utilizar para dar formato a
        /// la representación como una cadena de este objeto. Si se omite,
        /// se utilizará <see cref="CI.CurrentCulture"/>.
        /// </param>
        /// <returns></returns>
        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (string.IsNullOrEmpty(format)) format = "C";
            if (formatProvider.IsNull()) formatProvider = CI.CurrentCulture;
            switch (format.ToUpperInvariant()[0])
            {
                case 'C': return $"{X},{Y}";
                case 'B': return $"[{X}, {Y}]";
                case 'V': return $"X: {X}, Y: {Y}";
                default: throw new FormatException(string.Format(St.FormatNotSupported, format));
            }
        }
    }
}