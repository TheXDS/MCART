/*
Size3D.cs

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
using TheXDS.MCART.Math;
using TheXDS.MCART.Misc;
using TheXDS.MCART.Types.Base;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.Types
{
    /// <summary>
    /// Estructura universal que describe el tamaño de un objeto en ancho y
    /// alto en un espacio de dos dimensiones.
    /// </summary>
    public struct Size3D : IEquatable<Size3D>, I3DSize
    {
        /// <summary>
        /// Obtiene un valor que no representa ningún tamaño. Este campo es
        /// de solo lectura.
        /// </summary>
        public static readonly Size3D Nothing = new(double.NaN, double.NaN, double.NaN);

        /// <summary>
        /// Obtiene un valor que representa un tamaño nulo. Este campo es
        /// de solo lectura.
        /// </summary>
        public static readonly Size3D Zero = new(0, 0, 0);

        /// <summary>
        /// Obtiene un valor que representa un tamaño infinito. Este campo
        /// es de solo lectura.
        /// </summary>
        public static readonly Size3D Infinity = new(double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity);

        /// <summary>
        /// Intenta crear un <see cref="Size"/> a partir de una cadena.
        /// </summary>
        /// <param name="value">
        /// Valor a partir del cual crear un <see cref="Size"/>.
        /// </param>
        /// <param name="size">
        /// <see cref="Size"/> que ha sido creado.
        /// </param>
        /// <returns>
        /// <see langword="true"/> si la conversión ha tenido éxito,
        /// <see langword="false"/> en caso contrario.
        /// </returns>
        public static bool TryParse(string value, out Size3D size)
        {
            switch (value)
            {
                case nameof(Nothing):
                case null:
                    size = Nothing;
                    break;
                case nameof(Zero):
                case "0":
                    size = Zero;
                    break;
                default:
                    string[]? separators = new[]
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
                    return PrivateInternals.TryParseValues<double, Size3D>(separators, value.Without("()[]{}".ToCharArray()), 3, l => new(l[0], l[1], l[2]), out size);
            }
            return true;
        }

        /// <summary>
        /// Crea un <see cref="Size"/> a partir de una cadena.
        /// </summary>
        /// <param name="value">
        /// Valor a partir del cual crear un <see cref="Size"/>.
        /// </param>
        /// <exception cref="FormatException">
        /// Se produce si la conversión ha fallado.
        /// </exception>
        /// <returns><see cref="Size"/> que ha sido creado.</returns>
        public static Size3D Parse(string value)
        {
            if (TryParse(value, out Size3D retval)) return retval;
            throw new FormatException();
        }

        /// <summary>
        /// Compara la igualdad entre dos instancias de <see cref="Size"/>.
        /// </summary>
        /// <param name="size1">
        /// Primer elemento a comparar.
        /// </param>
        /// <param name="size2">
        /// Segundo elemento a comparar.
        /// </param>
        /// <returns>
        /// <see langword="true"/> si los tamaños representados en ambos
        /// objetos son iguales, <see langword="false"/> en caso contrario.
        /// </returns>
        public static bool operator ==(Size3D size1, Size3D size2)
        {
            return size1.Height == size2.Height && size1.Width == size2.Width && size1.Depth == size2.Depth;
        }

        /// <summary>
        /// Compara la desigualdad entre dos instancias de 
        /// <see cref="Size"/>.
        /// </summary>
        /// <param name="size1">
        /// Primer elemento a comparar.
        /// </param>
        /// <param name="size2">
        /// Segundo elemento a comparar.
        /// </param>
        /// <returns>
        /// <see langword="true"/> si los tamaños representados en ambos
        /// objetos son distintos, <see langword="false"/> en caso
        /// contrario.
        /// </returns>
        public static bool operator !=(Size3D size1, Size3D size2)
        {
            return !(size1 == size2);
        }

        /// <summary>
        /// Obtiene el componente de altura del tamaño.
        /// </summary>
        public double Height { get; set; }

        /// <summary>
        /// Obtiene el componente de ancho del tamaño.
        /// </summary>
        public double Width { get; set; }
        
        /// <summary>
        /// Obtiene el componente de profundidad del tamaño.
        /// </summary>
        public double Depth { get; set; }

        /// <summary>
        /// Inicializa una nueva instancia de la estructura
        /// <see cref="Size"/>.
        /// </summary>
        /// <param name="width">Valor de ancho.</param>
        /// <param name="height">Valor de alto.</param>
        /// <param name="depth">Valor de profundidad.</param>
        public Size3D(double width, double height, double depth)
        {
            Width = width;
            Height = height;
            Depth = depth;
        }

        /// <summary>
        /// Calcula el área cuadrada representada por este tamaño.
        /// </summary>
        public double CubeVolume => Height * Width * Depth;

        /// <summary>
        /// Calcula el perímetro cuadrado representado por este tamaño.
        /// </summary>
        public double CubePerimeter => (Height * 2) + (Width * 2) + (Depth * 2);

        /// <summary>
        /// Determina si esta instancia representa un tamaño nulo.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> si el tamaño es nulo,
        /// <see langword="false"/> si el tamaño no contiene volumen, y
        /// <see langword="null"/> si alguna magnitud está indefinida.
        /// </returns>
        public bool? IsZero
        {
            get
            {
                double a = CubeVolume;
                return a.IsValid() ? a == 0 : (bool?)null;
            }
        }

        /// <summary>
        /// Convierte este <see cref="Size"/> en un valor
        /// <see cref="I2DVector"/>.
        /// </summary>
        /// <returns>
        /// Un valor <see cref="I2DVector"/> cuyos componentes son las
        /// magnitudes de tamaño de esta instancia.
        /// </returns>
        public I3DVector To3DVector()
        {
            return new _3DVector { X = Width, Y = Height, Z = Depth};
        }

        /// <summary>
        /// Determina si esta instancia de <see cref="Size"/> es igual a
        /// otra.
        /// </summary>
        /// <param name="other">
        /// Instancia de <see cref="Size"/> contra la cual comparar.
        /// </param>
        /// <returns>
        /// <see langword="true"/> si los tamaños representados en ambos
        /// objetos son iguales, <see langword="false"/> en caso contrario.
        /// </returns>
        public bool Equals(Size3D other) => this == other;

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
            if (obj is not Size3D p) return false;
            return this == p;
        }

        /// <summary>
        /// Devuelve el código hash generado para esta instancia.
        /// </summary>
        /// <returns>
        /// Un código hash que representa a esta instancia.
        /// </returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(Height, Width, Depth);
        }
    }
}