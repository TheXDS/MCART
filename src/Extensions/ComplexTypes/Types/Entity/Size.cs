/*
Point.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2020 César Andrés Morgan

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

namespace TheXDS.MCART.Types.Entity
{
    /// <summary>
    /// Clase universal que describe el tamaño de un objeto en ancho y
    /// alto en un espacio de dos dimensiones.
    /// </summary>
    [ComplexType]
    public class Size : IEquatable<Size>, ISize
    {
        /// <summary>
        /// Obtiene el componente de altura del tamaño.
        /// </summary>
        public double Height { get; set; }

        /// <summary>
        /// Obtiene el componente de ancho del tamaño.
        /// </summary>
        public double Width { get; set; }

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
        public bool Equals(Size? other) => other is {} o && this == o;


        /// <summary>
        /// Devuelve el código hash generado para esta instancia.
        /// </summary>
        /// <returns>
        /// Un código hash que representa a esta instancia.
        /// </returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(Height, Width);
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
        public static bool operator ==(Size size1, Size size2)
        {
            return (size1.Height == size2.Height && size1.Width == size2.Width);
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
        public static bool operator !=(Size size1, Size size2)
        {
            return !(size1 == size2);
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="Size"/>.
        /// </summary>
        public Size()
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="Size"/>.
        /// </summary>
        /// <param name="width">Valor de ancho.</param>
        /// <param name="height">Valor de alto.</param>
        public Size(double width, double height)
        {
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Indica si esta instancia y un objeto especificado son iguales.
        /// </summary>
        /// <param name="obj">
        /// Objeto que se va a compara con la instancia actual.
        /// </param>
        /// <returns>
        /// <see langword="true" /> si esta instancia y
        /// <paramref name="obj" /> son iguales; de lo contrario,
        /// <see langword="false" />.
        /// </returns>
        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(this, obj))
                return true;

            return obj is Size s && this == s;
        }

        /// <summary>
        /// Convierte implícitamente un <see cref="Size"/> en un
        /// <see cref="Types.Size"/>.
        /// </summary>
        /// <param name="p">
        /// <see cref="Size"/> a convertir.
        /// </param>
        public static implicit operator Types.Size(Size p)
        {
            return new Types.Size(p.Width, p.Height);
        }

        /// <summary>
        /// Convierte implícitamente un <see cref="Types.Size"/> en un
        /// <see cref="Size"/>.
        /// </summary>
        /// <param name="p">
        /// <see cref="Types.Size"/> a convertir.
        /// </param>
        public static implicit operator Size(Types.Size p)
        {
            return new Size(p.Width, p.Height);
        }
    }
}