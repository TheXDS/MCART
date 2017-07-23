//
//  Color.cs
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

#region Opciones de compilación
// Usar Clamping en lugar de excepciones
#define UseClamping
#endregion

using System;
using CI = System.Globalization.CultureInfo;
namespace MCART.Types
{
    /// <summary>
    /// Estructura universal que describe un color en sus componentes alfa,
    /// rojo, verde y azul.
    /// </summary>
    public partial struct Color : IEquatable<Color>, IFormattable
    {
        /// <summary>
        /// Mezcla un color de temperatura basado en el porcentaje.
        /// </summary>
        /// <returns>
        /// El color qaue representa la temperatura del porcentaje.
        /// </returns>
        /// <param name="x">
        /// Valor porcentual utilizado para calcular la temperatura.
        /// </param>
        public static Color BlendHeatColor(float x)
        {
            byte r = (byte)(1020 * (float)(x + 0.5) - 1020).Clamp(0, 255);
            byte g = (byte)((float)(-System.Math.Abs(2040 * (x - 0.5)) + 1020) / 2).Clamp(0, 255);
            byte b = (byte)(-1020 * (float)(x + 0.5) + 1020).Clamp(0, 255);
            return new Color(r, g, b);
        }
        /// <summary>
        /// Mezcla un color de salud basado en el porcentaje.
        /// </summary>
        /// <returns>El color qaue representa la salud del porcentaje.</returns>
        /// <param name="x">The x coordinate.</param>
        public static Color BlendHealthColor(float x)
        {
            byte g = (byte)(510 * x).Clamp(0, 255);
            byte r = (byte)(510 - (510 * x)).Clamp(0, 255);
            return new Color(r, g, 0);
        }
        /// <summary>
        /// Adds a <see cref="Color"/> to a <see cref="Color"/>, yielding a new
        /// <see cref="Color"/>.
        /// </summary>
        /// <param name="left">The first <see cref="Color"/> to add.</param>
        /// <param name="right">The second <see cref="Color"/> to add.</param>
        /// <returns>The <see cref="Color"/> that is the sum of the values of 
        /// <paramref name="left"/> and <paramref name="right"/>.</returns>
        public static Color operator +(Color left, Color right)
        {
            return new Color(
#if UseClamping
                // La verificación del clamping se realiza en el costructor.
                left.a + right.a,
                left.r + right.r,
                left.g + right.g,
                left.b + right.b
#else
                // La verificación del clamping se realiza aquí.
                (left.a + right.a).Clamp(0.0f, 1.0f),
                (left.r + right.r).Clamp(0.0f, 1.0f),
                (left.g + right.g).Clamp(0.0f, 1.0f),
                (left.b + right.b).Clamp(0.0f, 1.0f)
#endif
            );
        }
        /// <summary>
        /// Subtracts a <see cref="Color"/> from a <see cref="Color"/>, yielding
        ///  a new <see cref="Color"/>.
        /// </summary>
        /// <param name="left">
        /// The <see cref="Color"/> to subtract from (the minuend).
        /// </param>
        /// <param name="right">
        /// The <see cref="Color"/> to subtract (the subtrahend).
        /// </param>
        /// <returns>
        /// The <see cref="Color"/> that is <paramref name="left"/> minus
        /// <paramref name="right"/>.
        /// </returns>
        public static Color operator -(Color left, Color right)
        {
            return new Color(
#if UseClamping
                // La verificación del clamping se realiza en el costructor.
                left.a - right.a,
                left.r - right.r,
                left.g - right.g,
                left.b - right.b
#else
                // La verificación del clamping se realiza aquí.
                (left.a - right.a).Clamp(0.0f, 1.0f),
                (left.r - right.r).Clamp(0.0f, 1.0f),
                (left.g - right.g).Clamp(0.0f, 1.0f),
                (left.b - right.b).Clamp(0.0f, 1.0f)
#endif
            );
        }
        /// <summary>
        /// Computes the product of <paramref name="left"/> and 
        /// <paramref name="right"/>, yielding a new <see cref="Color"/>.
        /// </summary>
        /// <param name="left">The <see cref="Color"/> to multiply.</param>
        /// <param name="right">The <see cref="float"/> to multiply by.</param>
        /// <returns>
        /// The <see cref="Color"/> that is <paramref name="left"/> * 
        /// <paramref name="right"/>.
        /// </returns>
        public static Color operator *(Color left, float right)
        {
            return new Color(
#if UseClamping
                // La verificación del clamping se realiza en el costructor.
                left.a * right,
                left.r * right,
                left.g * right,
                left.b * right
#else
                // La verificación del clamping se realiza aquí.
                (left.a * right).Clamp(0.0f, 1.0f),
                (left.r * right).Clamp(0.0f, 1.0f),
                (left.g * right).Clamp(0.0f, 1.0f),
                (left.b * right).Clamp(0.0f, 1.0f)
#endif
            );
        }
        /// <summary>
        /// Realiza una mezcla entre los colores especificados.
        /// </summary>
        /// <param name="left">El primer <see cref="Color"/> a mezclar.</param>
        /// <param name="right">
        /// El segundo <see cref="Color"/> a mezclar.
        /// </param>
        /// <returns>Una mezcla entre los colores <paramref name="left"/> y 
        /// <paramref name="right"/>.</returns>
		public static Color operator /(Color left, Color right)
        {
            return new Color(
#if UseClamping
                // La verificación del clamping se realiza en el costructor.
                (left.a + right.a) / 2,
                (left.r + right.r) / 2,
                (left.g + right.g) / 2,
                (left.b + right.b) / 2
#else
                // La verificación del clamping se realiza aquí.
                ((left.a + right.a) / 2).Clamp(0.0f, 1.0f),
                ((left.r + right.r) / 2).Clamp(0.0f, 1.0f),
                ((left.g + right.g) / 2).Clamp(0.0f, 1.0f),
                ((left.b + right.b) / 2).Clamp(0.0f, 1.0f)
#endif
            );
        }
        private float a;
        private float r;
        private float g;
        private float b;
        /// <summary>
        /// Initializes a new instance of the <see cref="Color"/> struct.
        /// </summary>
        /// <param name="R">R.</param>
        /// <param name="G">G.</param>
        /// <param name="B">B.</param>
        public Color(byte R, byte G, byte B)
        {
            a = 1.0f;
            r = (float)R / 255;
            g = (float)G / 255;
            b = (float)B / 255;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Color"/> struct.
        /// </summary>
        /// <param name="A">A.</param>
        /// <param name="R">R.</param>
        /// <param name="G">G.</param>
        /// <param name="B">B.</param>
		public Color(byte A, byte R, byte G, byte B)
        {
            a = (float)A / 255;
            r = (float)R / 255;
            g = (float)G / 255;
            b = (float)B / 255;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Color"/> struct.
        /// </summary>
        /// <param name="R">R.</param>
        /// <param name="G">G.</param>
        /// <param name="B">B.</param>
		public Color(float R, float G, float B)
        {
            a = 1.0f;
#if UseClamping
            r = R.Clamp(0.0f, 1.0f);
            g = G.Clamp(0.0f, 1.0f);
            b = B.Clamp(0.0f, 1.0f);
#else
            r = R.IsBetween(0.0f, 1.0f) ? R : throw new ArgumentOutOfRangeException(nameof(R));
            g = G.IsBetween(0.0f, 1.0f) ? G : throw new ArgumentOutOfRangeException(nameof(G));
            b = B.IsBetween(0.0f, 1.0f) ? B : throw new ArgumentOutOfRangeException(nameof(B));
#endif
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Color"/> struct.
        /// </summary>
        /// <param name="A">A.</param>
        /// <param name="R">R.</param>
        /// <param name="G">G.</param>
        /// <param name="B">B.</param>
		public Color(float A, float R, float G, float B)
        {
#if UseClamping
            a = A.Clamp(0.0f, 1.0f);
            r = R.Clamp(0.0f, 1.0f);
            g = G.Clamp(0.0f, 1.0f);
            b = B.Clamp(0.0f, 1.0f);
#else
            a = A.IsBetween(0.0f, 1.0f) ? A : throw new ArgumentOutOfRangeException(nameof(A));
            r = R.IsBetween(0.0f, 1.0f) ? R : throw new ArgumentOutOfRangeException(nameof(R));
            g = G.IsBetween(0.0f, 1.0f) ? G : throw new ArgumentOutOfRangeException(nameof(G));
            b = B.IsBetween(0.0f, 1.0f) ? B : throw new ArgumentOutOfRangeException(nameof(B));
#endif
        }

        /// <summary>
        /// Obtiene o establece el valor RGB del canal alfa del color.
        /// </summary>
        public byte A
        {
            get => (byte)(a * 255);
            set => a = (float)value / 255;
        }
        /// <summary>
        /// Obtiene o establece el valor RGB del canal rojo del color.
        /// </summary>
        public byte R
        {
            get => (byte)(r * 255);
            set => r = (float)value / 255;
        }
        /// <summary>
        /// Obtiene o establece el valor RGB del canal verde del color.
        /// </summary>
        public byte G
        {
            get => (byte)(g * 255);
            set => g = (float)value / 255;
        }
        /// <summary>
        /// Obtiene o establece el valor RGB del canal azul del color.
        /// </summary>
        public byte B
        {
            get => (byte)(b * 255);
            set => b = (float)value / 255;
        }
        /// <summary>
        /// Obtiene o establece el valor ScRGB del canal alfa del color.
        /// </summary>
        public float ScA
        {
            get => a;
#if UseClamping
            set => a = value.Clamp(0.0f, 1.0f);
#else
            set => a = value.IsBetween(0.0f, 1.0f) ? value : throw new ArgumentOutOfRangeException(nameof(value));
#endif
        }
        /// <summary>
        /// Obtiene o establece el valor ScRGB del canal rojo del color.
        /// </summary>
        public float ScR
        {
            get => r;
#if UseClamping
            set => r = value.Clamp(0.0f, 1.0f);
#else
            set => r = value.IsBetween(0.0f, 1.0f) ? value : throw new ArgumentOutOfRangeException(nameof(value));
#endif
        }
        /// <summary>
        /// Obtiene o establece el valor ScRGB del canal verde del color.
        /// </summary>
        public float ScG
        {
            get => g;
#if UseClamping
            set => g = value.Clamp(0.0f, 1.0f);
#else
            set => g = value.IsBetween(0.0f, 1.0f) ? value : throw new ArgumentOutOfRangeException(nameof(value));
#endif
        }
        /// <summary>
        /// Obtiene o establece el valor ScRGB del canal azul del color.
        /// </summary>
        public float ScB
        {
            get => b;
#if UseClamping
            set => b = value.Clamp(0.0f, 1.0f);
#else
            set => b = value.IsBetween(0.0f, 1.0f) ? value : throw new ArgumentOutOfRangeException(nameof(value));
#endif
        }
        /// <summary>
        /// Determines whether the specified <see cref="Color"/> is equal to the
        /// current <see cref="Color"/>.
        /// </summary>
        /// <param name="other">
        /// The <see cref="Color"/> to compare with the current 
        /// <see cref="Color"/>.
        /// </param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="Color"/> is equal to the
        /// current <see cref="Color"/>; otherwise, <c>false</c>.
        /// </returns>
        public bool Equals(Color other)
        {
            return a == other.a && r == other.r && g == other.g && b == other.b;
        }
        /// <summary>
        /// Returns a <see cref="String"/> that represents the current 
        /// <see cref="Color"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="String"/> that represents the current 
        /// <see cref="Color"/>.
        /// </returns>
        public override string ToString()
        {
            return $"#{(new byte[] { A, R, G, B }).ToHex()}";
        }
        /// <summary>
        /// Returns a <see cref="String"/> that represents the current 
        /// <see cref="Color"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="String"/> that represents the current 
        /// <see cref="Color"/>.
        /// </returns>
        /// <param name="format">Format.</param>
        /// <param name="formatProvider">Format provider.</param>
        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (string.IsNullOrEmpty(format)) format = "#AARRGGBB";
            if (formatProvider.IsNull()) formatProvider = CI.CurrentCulture;
            switch (format)
            {
                case "H": return $"#{(new byte[] { A, R, G, B }).ToHex()}";
                case "h": return $"#{(new byte[] { A, R, G, B }).ToHex().ToLower((CI)formatProvider)}";
                case "b": return $"a:{A} r:{R} g:{G} b:{B}";
                case "B": return $"A:{A} R:{R} G:{G} B:{B}";
                case "f": return $"a:{a} r:{r} g:{g} b:{b}";
                case "F": return $"A:{a} R:{r} G:{g} B:{b}";
                default:
                    format = format.Replace("AA", A.ToHex());
                    format = format.Replace("aa", A.ToHex().ToLower((CI)formatProvider));
                    format = format.Replace("RR", R.ToHex());
                    format = format.Replace("rr", R.ToHex().ToLower((CI)formatProvider));
                    format = format.Replace("GG", G.ToHex());
                    format = format.Replace("gg", G.ToHex().ToLower((CI)formatProvider));
                    format = format.Replace("BB", B.ToHex());
                    format = format.Replace("bb", B.ToHex().ToLower((CI)formatProvider));
                    return format;
            }
        }
    }
}