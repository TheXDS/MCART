/*
Size.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright (c) 2011 - 2019 César Andrés Morgan

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
using System.Collections.Generic;
using TheXDS.MCART.Math;

namespace TheXDS.MCART.Types
{
    /// <summary>
    ///     Estructura universal que describe el tamaño de un objeto en ancho y
    ///     alto en un espacio de dos dimensiones.
    /// </summary>
    public struct Size : IEquatable<Size>
    {
        /// <summary>
        ///     Obtiene un valor que no representa ningún tamaño. Este campo es
        ///     de solo lectura.
        /// </summary>
        public static readonly Size Nothing = new Size(double.NaN, double.NaN);

        /// <summary>
        ///     Obtiene un valor que representa un tamaño nulo. Este campo es
        ///     de solo lectura.
        /// </summary>
        public static readonly Size Zero = new Size(0, 0);

        /// <summary>
        ///     Obtiene un valor que representa un tamaño infinito. Este campo
        ///     es de solo lectura.
        /// </summary>
        public static readonly Size Infinity = new Size(double.PositiveInfinity, double.PositiveInfinity);
        
        /// <summary>
        ///     Compara la igualdad entre dos instancias de <see cref="Size"/>.
        /// </summary>
        /// <param name="size1">
        ///     Primer elemento a comparar.
        /// </param>
        /// <param name="size2">
        ///     Segundo elemento a comparar.
        /// </param>
        /// <returns>
        ///     <see langword="true"/> si los tamaños representados en ambos
        ///     objetos son iguales, <see langword="false"/> en caso contrario.
        /// </returns>
        public static bool operator ==(Size size1, Size size2)
        {
            return (size1.Height == size2.Height && size1.Width == size2.Width);
        }

        /// <summary>
        ///     Compara la desigualdad entre dos instancias de 
        ///     <see cref="Size"/>.
        /// </summary>
        /// <param name="size1">
        ///     Primer elemento a comparar.
        /// </param>
        /// <param name="size2">
        ///     Segundo elemento a comparar.
        /// </param>
        /// <returns>
        ///     <see langword="true"/> si los tamaños representados en ambos
        ///     objetos son distintos, <see langword="false"/> en caso
        ///     contrario.
        /// </returns>
        public static bool operator !=(Size size1, Size size2)
        {
            return !(size1 == size2);
        }

        /// <summary>
        ///     Obtiene el componente de altura del tamaño.
        /// </summary>
        public double Height { get; set; }

        /// <summary>
        ///     Obtiene el componente de ancho del tamaño.
        /// </summary>
        public double Width { get; set; }

        /// <summary>
        ///     Inicializa una nueva instancia de la estructura
        ///     <see cref="Size"/>.
        /// </summary>
        /// <param name="width">Valor de ancho.</param>
        /// <param name="height">Valor de alto.</param>
        public Size(double width, double height)
        {
            Width = width;
            Height = height;
        }

        /// <summary>
        ///     Calcula el área cuadrada representada por este tamaño.
        /// </summary>
        public double SquareArea => Height * Width;

        /// <summary>
        ///     Calcula el perímetro cuadrado representado por este tamaño.
        /// </summary>
        public double SquarePerimeter => (Height * 2) + (Width * 2);

        /// <summary>
        ///     Determina si esta instancia representa un tamaño nulo.
        /// </summary>
        /// <returns>
        ///     <see langword="true"/> si el tamaño es nulo,
        ///     <see langword="false"/> si el tamaño contiene área, y
        ///     <see langword="null"/> si alguna magnitud está indefinida.
        /// </returns>
        public bool? IsZero
        {
            get
            {
                var a = SquareArea;
                return a.IsValid() ? a == 0 : (bool?)null;
            }
        }

        /// <summary>
        ///     Convierte este <see cref="Size"/> en un valor
        ///     <see cref="I2DVector"/>.
        /// </summary>
        /// <returns>
        ///     Un valor <see cref="I2DVector"/> cuyos componentes son las
        ///     magnitudes de tamaño de esta instancia.
        /// </returns>
        public I2DVector To2DVector()
        {
            return new _2DVector() { X = Width, Y = Height };
        }

        /// <summary>
        ///     Determina si esta instancia de <see cref="Size"/> es igual a
        ///     otra.
        /// </summary>
        /// <param name="other">
        ///     Instancia de <see cref="Size"/> contra la cual comparar.
        /// </param>
        /// <returns>
        ///     <see langword="true"/> si los tamaños representados en ambos
        ///     objetos son iguales, <see langword="false"/> en caso contrario.
        /// </returns>
        public bool Equals(Size other) => this == other;

        /// <summary>
        ///     Indica si esta instancia y un objeto especificado son iguales.
        /// </summary>
        /// <param name="obj">
        ///     Objeto que se va a compara con la instancia actual.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> si esta instancia y <paramref name="obj" /> son iguales;
        ///     de lo contrario, <see langword="false" />.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (!(obj is Size p)) return false;
            return this == p;
        }

        /// <summary>
        ///     Devuelve el código hash generado para esta instancia.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            var hashCode = -642398537;
            hashCode = hashCode * -1521134295 + Height.GetHashCode();
            hashCode = hashCode * -1521134295 + Width.GetHashCode();
            hashCode = hashCode * -1521134295 + SquareArea.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<bool?>.Default.GetHashCode(IsZero);
            return hashCode;
        }

        /// <summary>
        ///     Convierte implícitamente un objeto
        ///     <see cref="System.Drawing.Size"/> en un <see cref="Size"/>.
        /// </summary>
        /// <param name="size">
        ///     Objeto a convertir.
        /// </param>
        public static implicit operator System.Drawing.Size(Size size)
        {
            return new System.Drawing.Size((int)size.Width, (int)size.Height);
        }

        /// <summary>
        ///     Convierte implícitamente un objeto
        ///     <see cref="System.Drawing.SizeF"/> en un <see cref="Size"/>.
        /// </summary>
        /// <param name="size">
        ///     Objeto a convertir.
        /// </param>
        public static implicit operator System.Drawing.SizeF(Size size)
        {
            return new System.Drawing.SizeF((float)size.Width, (float)size.Height);
        }

        /// <summary>
        ///     Convierte implícitamente un objeto
        ///     <see cref="Size"/> en un <see cref="System.Drawing.Size"/>.
        /// </summary>
        /// <param name="size">
        ///     Objeto a convertir.
        /// </param>
        public static implicit operator Size(System.Drawing.Size size)
        {
            return new Size(size.Width, size.Height);
        }

        /// <summary>
        ///     Convierte implícitamente un objeto
        ///     <see cref="Size"/> en un <see cref="System.Drawing.SizeF"/>.
        /// </summary>
        /// <param name="size">
        ///     Objeto a convertir.
        /// </param>
        public static implicit operator Size(System.Drawing.SizeF size)
        {
            return new Size(size.Width, size.Height);
        }
    }
}