//
//  Color.cs
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

namespace TheXDS.MCART.Types
{
	/// <summary>
	/// Estructura universal que describe un color en sus componentes alfa,
	/// rojo, verde y azul.
	/// </summary>
	public partial struct Color : IEquatable<Color>, IFormattable
	{
		float r;
		float g;
		float b;
		float a;
		/// <summary>
		/// Mezcla un color de temperatura basado en el porcentaje.
		/// </summary>
		/// <returns>
		/// El color qaue representa la temperatura del porcentaje.
		/// </returns>
		/// <param name="x">
		/// Valor porcentual utilizado para calcular la temperatura.
		/// </param>
		public static Color BlendHeat(float x)
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
		public static Color BlendHealth(float x)
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
#if PreferExceptions
				(left.r + right.r).Clamp(0.0f, 1.0f),
				(left.g + right.g).Clamp(0.0f, 1.0f),
				(left.b + right.b).Clamp(0.0f, 1.0f),
				(left.a + right.a).Clamp(0.0f, 1.0f)
#else
				left.r + right.r,
				left.g + right.g,
				left.b + right.b,
				left.a + right.a
#endif
			);
		}
		/// <summary>
		/// Sustrae un <see cref="Color"/> de un <see cref="Color"/>, dando
		/// como resultado un <see cref="Color"/>.
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
#if PreferExceptions
				(left.r - right.r).Clamp(0.0f, 1.0f),
				(left.g - right.g).Clamp(0.0f, 1.0f),
				(left.b - right.b).Clamp(0.0f, 1.0f),
				(left.a - right.a).Clamp(0.0f, 1.0f)
#else
				left.r - right.r,
				left.g - right.g,
				left.b - right.b,
				left.a - right.a
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
#if PreferExceptions
				(left.r * right).Clamp(0.0f, 1.0f),
				(left.g * right).Clamp(0.0f, 1.0f),
				(left.b * right).Clamp(0.0f, 1.0f),
				(left.a * right).Clamp(0.0f, 1.0f)
#else
				left.r * right,
				left.g * right,
				left.b * right,
				left.a * right
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
#if PreferExceptions
				((left.r + right.r) / 2).Clamp(0.0f, 1.0f),
				((left.g + right.g) / 2).Clamp(0.0f, 1.0f),
				((left.b + right.b) / 2).Clamp(0.0f, 1.0f),
				((left.a + right.a) / 2).Clamp(0.0f, 1.0f)
#else
				(left.r + right.r) / 2,
				(left.g + right.g) / 2,
				(left.b + right.b) / 2,
				(left.a + right.a) / 2
#endif
			);
		}
        /// <summary>
        /// Determina si dos instancias de <see cref="Color"/> son iguales.
        /// </summary>
		/// <param name="left">
        /// El primer <see cref="Color"/> a comprobar.
        /// </param>
		/// <param name="right">
		/// El segundo <see cref="Color"/> a comprobar.
		/// </param>
        /// <returns>
        /// <c>true</c> ambas instancias de <see cref="Color"/> son iguales,
        /// <c>false</c> en caso contrario.
        /// </returns>
        public static bool operator ==(Color left, Color right) => left.Equals(right);
        /// <summary>
        /// Determina si dos instancias de <see cref="Color"/> son distintas.
        /// </summary>
        /// <param name="left">
        /// El primer <see cref="Color"/> a comprobar.
        /// </param>
        /// <param name="right">
        /// El segundo <see cref="Color"/> a comprobar.
        /// </param>
        /// <returns>
        /// <c>true</c> ambas instancias de <see cref="Color"/> son distintas,
        /// <c>false</c> en caso contrario.
        /// </returns>
        public static bool operator !=(Color left, Color right) => !left.Equals(right);
        /// <summary>
        /// Inicializa una nueva instancia de la esctructura 
        /// <see cref="Color"/>.
        /// </summary>
        /// <param name="R">Canal rojo.</param>
        /// <param name="G">Canal verde.</param>
        /// <param name="B">Canal azul.</param>
        /// <param name="A">
        /// Parámetro opcional. Canal alfa. Si se omite, el color tendrá una 
        /// opacidad de 100%.
        /// </param>
        public Color(byte R, byte G, byte B, byte A = 255)
		{
			r = (float)R / 255;
			g = (float)G / 255;
			b = (float)B / 255;
			a = (float)A / 255;
		}
		/// <summary>
		/// Inicializa una nueva instancia de la esctructura 
		/// <see cref="Color"/>.
		/// </summary>
		/// <param name="R">Canal rojo.</param>
		/// <param name="G">Canal verde.</param>
		/// <param name="B">Canal azul.</param>
		/// <param name="A">
		/// Parámetro opcional. Canal alfa. Si se omite, el color tendrá una 
		/// opacidad de 100%.
		/// </param>
		public Color(float R, float G, float B, float A = 1.0f)
		{
#if PreferExceptions
			a = A.IsBetween(0.0f, 1.0f) ? A : throw new ArgumentOutOfRangeException(nameof(A));
			r = R.IsBetween(0.0f, 1.0f) ? R : throw new ArgumentOutOfRangeException(nameof(R));
			g = G.IsBetween(0.0f, 1.0f) ? G : throw new ArgumentOutOfRangeException(nameof(G));
			b = B.IsBetween(0.0f, 1.0f) ? B : throw new ArgumentOutOfRangeException(nameof(B));
#else
			a = A.Clamp(0.0f, 1.0f);
			r = R.Clamp(0.0f, 1.0f);
			g = G.Clamp(0.0f, 1.0f);
			b = B.Clamp(0.0f, 1.0f);
#endif
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
		/// Obtiene o establece el valor RGB del canal alfa del color.
		/// </summary>
		public byte A
		{
			get => (byte)(a * 255);
			set => a = (float)value / 255;
		}
		/// <summary>
		/// Obtiene o establece el valor ScRGB del canal rojo del color.
		/// </summary>
		public float ScR
		{
			get => r;
#if PreferExceptions
			set => r = value.IsBetween(0.0f, 1.0f) ? value : throw new ArgumentOutOfRangeException(nameof(value));
#else
			set => r = value.Clamp(0.0f, 1.0f);
#endif
		}
		/// <summary>
		/// Obtiene o establece el valor ScRGB del canal verde del color.
		/// </summary>
		public float ScG
		{
			get => g;
#if PreferExceptions
			set => g = value.IsBetween(0.0f, 1.0f) ? value : throw new ArgumentOutOfRangeException(nameof(value));
#else
			set => g = value.Clamp(0.0f, 1.0f);
#endif
		}
		/// <summary>
		/// Obtiene o establece el valor ScRGB del canal azul del color.
		/// </summary>
		public float ScB
		{
			get => b;
#if PreferExceptions
			set => b = value.IsBetween(0.0f, 1.0f) ? value : throw new ArgumentOutOfRangeException(nameof(value));
#else
			set => b = value.Clamp(0.0f, 1.0f);
#endif
		}
		/// <summary>
		/// Obtiene o establece el valor ScRGB del canal alfa del color.
		/// </summary>
		public float ScA
		{
			get => a;
#if PreferExceptions
			set => a = value.IsBetween(0.0f, 1.0f) ? value : throw new ArgumentOutOfRangeException(nameof(value));
#else
			set => a = value.Clamp(0.0f, 1.0f);
#endif
		}
        /// <summary>
        /// Determina si el <see cref="Color"/> especificado es igual al
        /// <see cref="Color"/> actual.
        /// </summary>
        /// <param name="other">
        /// El <see cref="Color"/> a comparar contra este <see cref="Color"/>.
        /// </param>
        /// <returns>
        /// <c>true</c> si el <see cref="Color"/> especificado es igual al
        /// <see cref="Color"/> actual, <c>false</c> en caso contrario.
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
			if (format.IsEmpty()) format = "#AARRGGBB";
			if (formatProvider is null) formatProvider = CI.CurrentCulture;
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
		/// <summary>
		/// Convierte este <see cref="Color"/> en su representación como un
		/// <see cref="int"/>, sin el canal alfa.
		/// </summary>
		/// <returns>
		/// Un <see cref="int"/> con la representación binaria de este
		/// <see cref="Color"/>, en formato RGB de 24 bits.
		/// </returns>
		public int ToInt24() => R | G << 8 | B << 16;
		/// <summary>
		/// Convierte este <see cref="Color"/> en su representación como un
		/// <see cref="int"/>.
		/// </summary>
		/// <returns>
		/// Un <see cref="int"/> con la representación binaria de este
		/// <see cref="Color"/>, en formato RGBA de 32 bits.
		/// </returns>
		public int ToInt32() => ToInt24() | A << 24;
		/// <summary>
		/// constante auxiliar de redondeo para las funciones de conversión.
		/// </summary>
		const float ep = 0.499f;
		/// <summary>
		/// Convierte este <see cref="Color"/> en su representación como un
		/// <see cref="short"/>.
		/// </summary>
		/// <returns>
		/// Un <see cref="short"/> con la representación binaria de este
		/// <see cref="Color"/>, en formato RGBA de 16 bits.
		/// </returns>
		public short ToShort() => unchecked((short)(
			(int)((r + ep) * 31) |
			(int)((g + ep) * 31) << 5 |
			(int)((b + ep) * 31) << 10 |
			(int)((a + ep)) << 15));
		/// <summary>
		/// Convierte este <see cref="Color"/> en su representación como un
		/// <see cref="short"/>.
		/// </summary>
		/// <returns>
		/// Un <see cref="short"/> con la representación binaria de este
		/// <see cref="Color"/>, en formato RGB 5-6-5 de 16 bits.
		/// </returns>
		public short ToShort565() => unchecked((short)(
			(int)((r + ep) * 31) |
			(int)((g + ep) * 63) << 5 |
			(int)((b + ep) * 31) << 11));
		/// <summary>
		/// Convierte este <see cref="Color"/> en su representación como un
		/// <see cref="byte"/>.
		/// </summary>
		/// <returns>
		/// Un <see cref="byte"/> con la representación binaria de este
		/// <see cref="Color"/>, en formato RGBA de 8 bits.
		/// </returns>
		public byte ToByte() => unchecked((byte)(
			(int)((r + ep) * 3) |
			(int)((g + ep) * 3) << 2 |
			(int)((b + ep) * 3) << 4 |
			(int)((a + ep) * 3) << 6));
		/// <summary>
		/// Convierte este <see cref="Color"/> en su representación como un
		/// <see cref="byte"/> de atributo VGA.
		/// </summary>
		/// <returns>
		/// Un <see cref="byte"/> con la representación binaria de este
		/// <see cref="Color"/>, en formato RGB de 4 bits.
		/// </returns>
		public byte ToVGAByte()
		{
			byte q = (byte)((r + ep) * 2);
			byte w = (byte)((g + ep) * 2);
			byte e = (byte)((b + ep) * 2);
			return unchecked((byte)(q | (w << 1) | (B << 2) | ((q == 2 || w == 2 || B == 2) ? 4 : 0)));
		}
        /// <summary>
        /// Indica si este objeto y el especificado son la misma instancia.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        /// <summary>
        /// Obtiene el código Hash pasa esta instancia.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}