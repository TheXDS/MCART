/*
Point.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2019 César Andrés Morgan

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
using System.ComponentModel.DataAnnotations.Schema;
using TheXDS.MCART.Types.Base;
using static TheXDS.MCART.Types.Extensions.StringExtensions;
using CI = System.Globalization.CultureInfo;
using St = TheXDS.MCART.Resources.Strings;

namespace TheXDS.MCART.Types.Entity
{
    /// <summary>
    /// Tipo universal para un conjunto de coordenadas bidimensionales.
    /// </summary>
    /// <remarks>
    /// Esta estructura se declara como parcial, para permitir a cada
    /// implementación de MCART definir métodos para convertir a la clase
    /// correspondiente para los diferentes tipos de UI disponibles.
    /// </remarks>
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
        
        /// <inheritdoc />
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
        public bool Equals(I2DVector other)
        {
            return this == other;
        }

        /// <inheritdoc />
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
        public bool Equals(Point other)
        {
            return this == other;
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
            if (!(obj is I2DVector p)) return false;
            return this == p;
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

        /// <inheritdoc />
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
            switch (format.ToUpperInvariant()[0])
            {
                case 'C': return $"{X}, {Y}";
                case 'B': return $"[{X}, {Y}]";
                case 'V': return $"X: {X}, Y: {Y}";
                case 'N': return $"X: {X}\nY: {Y}";
                default: throw new FormatException(St.FormatNotSupported(format));
            }
        }

        /// <inheritdoc />
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
}