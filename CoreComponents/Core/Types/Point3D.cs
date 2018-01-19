//
//  Point.cs
//
//  This file is part of Morgan's CLR Advanced Runtime (MCART)
//
//  Author:
//       César Andrés Morgan <xds_xps_ivx@hotmail.com>
//
//  Copyright (c) 2011 - 2018 César Andrés Morgan
//
//  Morgan's CLR Advanced Runtime (MCART) is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  Morgan's CLR Advanced Runtime (MCART) is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
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
    public partial struct Point3D : IFormattable, IEquatable<Point3D>
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
        /// Coordenada Z.
        /// </summary>
        public double Z;
        /// <summary>
        /// Inicializa una nueva instancia de la estructura
        /// <see cref="Point3D"/>.
        /// </summary>
        /// <param name="x">Coordenada X.</param>
        /// <param name="y">Coordenada Y.</param>
        /// <param name="z">Coordenada Z.</param>
        public Point3D(double x, double y, double z) { X = x; Y = y; Z = z; }
        /// <summary>
        /// Determina si el punto se encuentra dentro del cubo formado por los
        /// puntos tridimensionales especificados.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> si el punto se encuentra dentro del cubo formado,
        /// <see langword="false"/> en caso contrario.
        /// </returns>
        /// <param name="p1">Punto 1.</param>
        /// <param name="p2">Punto 2.</param>
        public bool FitsInCube(Point3D p1, Point3D p2) => X.IsBetween(p1.X, p2.X) && Y.IsBetween(p1.Y, p2.Y) && Z.IsBetween(p1.Z, p2.Z);
        /// <summary>
        /// Determina si el punto se encuentra dentro del cubo formado por los
        /// puntos tridimensionales especificados.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> si el punto se encuentra dentro del cubo formado,
        /// <see langword="false"/> en caso contrario.
        /// </returns>
        /// <param name="x1">La primer coordenada x.</param>
        /// <param name="y1">La primer coordenada y.</param>
        /// <param name="z1">La primer coordenada z.</param>
        /// <param name="x2">La segunda coordenada x.</param>
        /// <param name="y2">La segunda coordenada y.</param>
        /// <param name="z2">La segunda coordenada z.</param>
        public bool FitsInCube(double x1, double y1, double z1, double x2, double y2, double z2) => X.IsBetween(x1, x2) && Y.IsBetween(y1, y2) && Z.IsBetween(z1, z2);
        /// <summary>
        /// Calcula la magnitud de las coordenadas.
        /// </summary>
        /// <returns>
        /// La magnitud resultante entre el punto y el orígen.
        /// </returns>
        public double Magnitude() => System.Math.Sqrt((X * X) + (Y * Y) + (Z * Z));
        /// <summary>
        /// Calcula la magnitud de las coordenadas desde el punto
        /// especificado.
        /// </summary>
        /// <returns>La magnitud resultante entre ambos puntos.</returns>
        /// <param name="fromPoint">Punto de referencia para calcular la
        /// magnitud.</param>
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
        /// <param name="fromX">Coordenada X de orígen.</param>
        /// <param name="fromY">Coordenada Y de orígen.</param>
        /// <param name="fromZ">Coordenada Z de orígen.</param>
        public double Magnitude(double fromX, double fromY, double fromZ)
        {
            double x = X - fromX, y = Y - fromY, z = Z - fromZ;
            return System.Math.Sqrt((x * x) + (y * y) + (z * z));
        }
        /// <summary>
        /// Obtiene un punto en el orígen. Este campo es de solo lectura.
        /// </summary>
        /// <value>
        /// Un <see cref="Point3D"/> con sus coordenadas en el orígen.
        /// </value>
        public static Point3D Origin => new Point3D(0, 0, 0);
        /// <summary>
        /// Obtiene un punto que no representa ninguna posición. Este campo es
        /// de solo lectura.
        /// </summary>
        /// <value>The nowhere.</value>
        public static Point3D Nowhere => new Point3D(double.NaN, double.NaN, double.NaN);
        /// <summary>
        /// Realiza una operación de suma sobre los puntos.
        /// </summary>
        /// <param name="l">Punto 1.</param>
        /// <param name="r">Punto 2.</param>
        /// <returns>La suma de los vectores de los puntos.</returns>
        public static Point3D operator +(Point3D l, Point3D r) => new Point3D(l.X + r.X, l.Y + r.Y, l.Z + r.Z);
        /// <summary>
        /// Realiza una operación de suma sobre el punto.
        /// </summary>
        /// <param name="l">Punto 1.</param>
        /// <param name="r">Operando de suma.</param>
        /// <returns>
        /// Un nuevo <see cref="Point3D"/> cuyos vectores son la suma de los
        /// vectores originales + <paramref name="r"/>.</returns>
        public static Point3D operator +(Point3D l, double r) => new Point3D(l.X + r, l.Y + r, l.Z + r);
        /// <summary>
        /// Realiza una operación de resta sobre los puntos.
        /// </summary>
        /// <param name="l">Punto 1.</param>
        /// <param name="r">Punto 2.</param>
        /// <returns>La resta de los vectores de los puntos.</returns>
        public static Point3D operator -(Point3D l, Point3D r) => new Point3D(l.X - r.X, l.Y - r.Y, l.Z - r.Z);
        /// <summary>
        /// Realiza una operación de resta sobre el punto.
        /// </summary>
        /// <param name="l">Punto 1.</param>
        /// <param name="r">Operando de resta.</param>
        /// <returns>
        /// Un nuevo <see cref="Point3D"/> cuyos vectores son la resta de los
        /// vectores originales - <paramref name="r"/>.</returns>
        public static Point3D operator -(Point3D l, double r) => new Point3D(l.X - r, l.Y - r, l.Z - r);
        /// <summary>
        /// Realiza una operación de multiplicación sobre los puntos.
        /// </summary>
        /// <param name="l">Punto 1.</param>
        /// <param name="r">Punto 2.</param>
        /// <returns>La multiplicación de los vectores de los puntos.</returns>
        public static Point3D operator *(Point3D l, Point3D r) => new Point3D(l.X * r.X, l.Y * r.Y, l.Z * r.Z);
        /// <summary>
        /// Realiza una operación de multiplicación sobre el punto.
        /// </summary>
        /// <param name="l">Punto 1.</param>
        /// <param name="r">Operando de multiplicación.</param>
        /// <returns>
        /// Un nuevo <see cref="Point3D"/> cuyos vectores son la multiplicación
        /// de los vectores originales * <paramref name="r"/>.</returns>
        public static Point3D operator *(Point3D l, double r) => new Point3D(l.X * r, l.Y * r, l.Z * r);
        /// <summary>
        /// Realiza una operación de división sobre los puntos.
        /// </summary>
        /// <param name="l">Punto 1.</param>
        /// <param name="r">Punto 2.</param>
        /// <returns>La división de los vectores de los puntos.</returns>
        public static Point3D operator /(Point3D l, Point3D r) => new Point3D(l.X / r.X, l.Y / r.Y, l.Z / r.Z);
        /// <summary>
        /// Realiza una operación de división sobre el punto.
        /// </summary>
        /// <param name="l">Punto 1.</param>
        /// <param name="r">Operando de división.</param>
        /// <returns>
        /// Un nuevo <see cref="Point3D"/> cuyos vectores son la división de
        /// los vectores originales / <paramref name="r"/>.</returns>
        public static Point3D operator /(Point3D l, double r) => new Point3D(l.X / r, l.Y / r, l.Z / r);
        /// <summary>
        /// Realiza una operación de resíduo sobre los puntos.
        /// </summary>
        /// <param name="l">Punto 1.</param>
        /// <param name="r">Punto 2.</param>
        /// <returns>El resíduo de los vectores de los puntos.</returns>
        public static Point3D operator %(Point3D l, Point3D r) => new Point3D(l.X % r.X, l.Y % r.Y, l.Z % r.Z);
        /// <summary>
        /// Realiza una operación de resíduo sobre el punto.
        /// </summary>
        /// <param name="l">Punto 1.</param>
        /// <param name="r">Operando de resíduo.</param>
        /// <returns>
        /// Un nuevo <see cref="Point3D"/> cuyos vectores son el resíduo de los
        /// vectores originales % <paramref name="r"/>.</returns>
        public static Point3D operator %(Point3D l, double r) => new Point3D(l.X % r, l.Y % r, l.Z % r);
        /// <summary>
        /// Incrementa en 1 los vectores del punto.
        /// </summary>
        /// <param name="p">Punto a incrementar.</param>
        /// <returns>Un punto con sus vectores incrementados en 1.</returns>
        public static Point3D operator ++(Point3D p)
        {
            p.X++; p.Y++; p.Z++;
            return p;
        }
        /// <summary>
        /// Decrementa en 1 los vectores del punto.
        /// </summary>
        /// <param name="p">Punto a decrementar.</param>
        /// <returns>Un punto con sus vectores decrementados en 1.</returns>
        public static Point3D operator --(Point3D p)
        {
            p.X--; p.Y--; p.Z--;
            return p;
        }
        /// <summary>
        /// Convierte a positivos los vectores del punto.
        /// </summary>
        /// <param name="p">Punto a operar.</param>
        /// <returns>Un punto con sus vectores positivos.</returns>
        public static Point3D operator +(Point3D p) => new Point3D(+p.X, +p.Y, +p.Z);
        /// <summary>
        /// Invierte el signo de los vectores del punto.
        /// </summary>
        /// <param name="p">Punto a operar.</param>
        /// <returns>Un punto con el signo de sus vectores invertido.</returns>
        public static Point3D operator -(Point3D p) => new Point3D(-p.X, -p.Y, -p.Z);
#pragma warning disable RECS0018 // Comparison of floating point numbers with equality operator
        /// <summary>
        /// Compara la igualdad de los vectores de los puntos.
        /// </summary>
        /// <param name="l">Punto 1.</param>
        /// <param name="r">Punto 2.</param>
        /// <returns>
        /// <see langword="true"/> si todos los vectores de ambos puntos son iguales;
        /// de lo contrario, <see langword="false"/>.</returns>
        public static bool operator ==(Point3D l, Point3D r) => (l.X == r.X && l.Y == r.Y && l.Z == r.Z);
        /// <summary>
        /// Compara la diferencia de los vectores de los puntos.
        /// </summary>
        /// <param name="l">Punto 1.</param>
        /// <param name="r">Punto 2.</param>
        /// <returns>
        /// <see langword="true"/> si los vectores de ambos puntos son diferentes;  de lo
        /// contrario, <see langword="false"/>.</returns>
        public static bool operator !=(Point3D l, Point3D r) => (l.X != r.X && l.Y != r.Y && l.Z != r.Z);
#pragma warning restore RECS0018
        /// <summary>
        /// Indica si esta instancia y un objeto especificado son iguales.
        /// </summary>
        /// <param name="obj">
        /// Objeto que se va a compara con la instancia actual.
        /// </param>
        /// <returns>
        /// <see langword="true"/> si esta instancia y <paramref name="obj"/> son iguales;
        /// de lo contrario, <see langword="false"/>.
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
        /// <returns>
        /// Una representación en forma de <see cref="string"/> de este objeto.
        /// </returns>
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
        /// <returns>
        /// Una representación en forma de <see cref="string"/> de este objeto.
        /// </returns>
        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (format.IsEmpty()) format = "C";
            if (formatProvider is null) formatProvider = CI.CurrentCulture;
            switch (format.ToUpperInvariant()[0])
            {
                case 'C': return $"{X},{Y},{Z}";
                case 'B': return $"[{X}, {Y}, {Z}]";
                case 'V': return $"X: {X}, Y: {Y}, Z: {Z}";
                case 'N': return $"X: {X}\nY: {Y}\nZ: {Z}";
                default: throw new FormatException(St.FormatNotSupported(format));
            }
        }
        /// <summary>
        /// Compara la igualdad de los vectores de los puntos.
        /// </summary>
        /// <param name="other">
        /// <see cref="Point3D"/> contra el cual comparar.
        /// </param>
        /// <returns>
        /// <see langword="true"/> si todos los vectores de ambos puntos son iguales;
        /// de lo contrario, <see langword="false"/>.</returns>
        public bool Equals(Point3D other)
        {
            return this == other;
        }

    }
}