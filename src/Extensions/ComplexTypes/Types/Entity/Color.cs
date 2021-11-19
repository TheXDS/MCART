/*
Color.cs

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
using System.ComponentModel.DataAnnotations.Schema;
using TheXDS.MCART.Types.Base;

namespace TheXDS.MCART.Types.Entity
{
    /// <summary>
    /// Estructura universal que describe un color en sus componentes alfa,
    /// rojo, verde y azul.
    /// </summary>
    [ComplexType]
    public class Color : IColor, IEquatable<Color>
    {
        /// <summary>
        /// Componente Alfa del color.
        /// </summary>
        public byte A { get; set; }

        /// <summary>
        ///  Componente Azul del color.
        /// </summary>
        public byte B { get; set; }

        /// <summary>
        ///  Componente Verde del color.
        /// </summary>
        public byte G { get; set; }

        /// <summary>
        ///  Componente Rojo del color.
        /// </summary>
        public byte R { get; set; }

        /// <summary>
        /// Convierte implícitamente un <see cref="Types.Color"/> en un
        /// <see cref="Color"/>.
        /// </summary>
        /// <param name="color">
        /// <see cref="Types.Color"/> a convertir.
        /// </param>
        public static implicit operator Color(Types.Color color)
        {
            return new Color
            {
                A = color.A,
                B = color.B,
                G = color.G,
                R = color.R,
            };
        }

        /// <summary>
        /// Convierte implícitamente un <see cref="Color"/> en un
        /// <see cref="Types.Color"/>.
        /// </summary>
        /// <param name="color">
        /// <see cref="Color"/> a convertir.
        /// </param>
        public static implicit operator Types.Color(Color color)
        {
            return new Types.Color(color.R, color.G, color.B, color.A);
        }

        /// <inheritdoc/>
        public bool Equals(Color? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return A == other.A && B == other.B && G == other.G && R == other.R;
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == this.GetType() && Equals((Color)obj);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(A, B, G, R);
        }
    }
}