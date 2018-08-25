/*
Color.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright (c) 2011 - 2018 César Andrés Morgan

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
using CI = System.Globalization.CultureInfo;

namespace TheXDS.MCART.Types
{
    /// <summary>
    /// Estructura universal que describe un color en sus componentes alfa,
    /// rojo, verde y azul.
    /// </summary>
    public partial struct Color : IEquatable<Color>, IFormattable, IComparable<Color>
    {
        /// <summary>
        /// constante auxiliar de redondeo para las funciones de conversión.
        /// </summary>
        private const float Ep = 0.499f;
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
            byte r = (byte)(510 - 510 * x).Clamp(0, 255);
            return new Color(r, g, 0);
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
        public static Color Blend(Color left, Color right) => left / right;
        /// <summary>
        /// Determina si los colores son lo suficientemente similares.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> si los colores son similares al menos en un
        /// 95%, <see langword="false"/> en caso contrario.
        /// </returns>
        /// <param name="color1">Primer <see cref="Color"/> a comparar.</param>
        /// <param name="color2">Segundo Color a comparar.</param>
        public static bool AreClose(Color color1, Color color2) => AreClose(color1, color2, 0.95f);
        /// <summary>
        /// Determina si los colores son lo suficientemente similares.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> si los colores son suficientemente similares, 
        /// <see langword="false"/> en caso contrario.
        /// </returns>
        /// <param name="color1">Primer <see cref="Color"/> a comparar.</param>
        /// <param name="color2">Segundo Color a comparar.</param>
        /// <param name="delta">
        /// Porcentaje aceptable de similitud entre os colores.
        /// </param>
        public static bool AreClose(Color color1, Color color2, float delta)
        {
            if (!delta.IsBetween(0.0f, 1.0f)) throw new ArgumentOutOfRangeException(nameof(delta));
            return Similarity(color1, color2) <= delta;
        }
        /// <summary>
        /// Determina el porcentaje de similitud entre dos colores.
        /// </summary>
        /// <returns>
        /// Un <see cref="float"/> que representa el porcentaje de similitud
        /// entre ambos colores.
        /// </returns>
        /// <param name="c1">Primer <see cref="Color"/> a comparar.</param>
        /// <param name="c2">Segundo Color a comparar.</param>
        public static float Similarity(Color c1, Color c2) => 1.0f - (c1.ScA - c2.ScA + (c1.ScR - c2.ScR) + (c1.ScG - c2.ScG) + (c1.ScB - c2.ScB));
        /// <summary>
        /// Intenta crear un <see cref="Color"/> a partir de la cadena
        /// especificada.
        /// </summary>
        /// <param name="from">
        /// Cadena a partir de la cual crear un <see cref="Color"/>.
        /// </param>
        /// <param name="color">
        /// Parámetro de salida. El <see cref="Color"/>que ha sido creado.
        /// </param>
        /// <returns>
        /// <see langword="true"/> si la conversión fue exitosa,
        /// <see langword="false"/> en caso contrario.
        /// </returns>
        public static bool TryParse(string from, out Color color)
        {
            if (from.IsFormattedAs("#FFFFFFFF"))
            {
                color = new ABGR32().From(int.Parse($"0x{from.Substring(1)}"));
                return true;
            }
            if (from.IsFormattedAs("#FFFFFF"))
            {
                color = (new BGR24()).From(int.Parse($"0x{from.Substring(1)}"));
                return true;
            }
            if (from.IsFormattedAs("#FFFF"))
            {
                color = (new ABGR4444()).From(short.Parse($"0x{from.Substring(1)}"));
                return true;
            }
            if (from.IsFormattedAs("#FFF"))
            {
                color = (new BGR12()).From(short.Parse($"0x{from.Substring(1)}"));
                return true;
            }
            var cName = typeof(Resources.Colors).GetProperty(from, typeof(Color));
            if (!(cName is null))
            {
                color = (Color)cName.GetValue(null);
                return true;
            }
            color = default;
            return false;
        }
        /// <summary>
        /// Crea un nuevo <see cref="Color"/> a partir de la cadena
        /// especificada.
        /// </summary>
        /// <param name="from">
        /// Cadena a partir de la cual crear un <see cref="Color"/>.
        /// </param>
        /// <returns>El <see cref="Color"/>que ha sido creado.</returns>
        /// <exception cref="FormatException">
        /// Se produce si no es posible crear un nuevo <see cref="Color"/> a
        /// partir de la cadena especificada.
        /// </exception>
        public static Color Parse(string from)
        {
            if (TryParse(from, out Color color)) return color;
            throw new FormatException();
        }
        /// <summary>
        /// Convierte una estructura compatible en un <see cref="Color"/>.
        /// </summary>
        /// <typeparam name="T">Tipo de valor a convertir.</typeparam>
        /// <typeparam name="TParser">
        /// <see cref="IColorParser{T}"/> a utilizar.
        /// </typeparam>
        /// <param name="from">Valor a convertir.</param>
        /// <returns>
        /// Un <see cref="Color"/> creado a partir del valor especificado.
        /// </returns>
        public static Color From<T, TParser>(T from) where T : struct where TParser : IColorParser<T>, new() => (new TParser()).From(from);
        /// <summary>
        /// Convierte un <see cref="Color"/> en un valor de tipo
        /// <typeparamref name="T"/>, utilizando el
        /// <see cref="IColorParser{T}"/> especificado.
        /// </summary>
        /// <typeparam name="T">Tipo de valor a obtener.</typeparam>
        /// <typeparam name="TParser">
        /// <see cref="IColorParser{T}"/> a utilizar.
        /// </typeparam>
        /// <param name="from"><see cref="Color"/> a convertir.</param>
        /// <returns>
        /// Un valor de tipo <typeparamref name="T"/> creado a partir de este
        /// <see cref="Color"/>.
        /// </returns>
        public static T To<T, TParser>(Color from) where T : struct where TParser : IColorParser<T>, new() => (new TParser()).To(from);
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
        /// <see langword="true"/> ambas instancias de <see cref="Color"/> son iguales,
        /// <see langword="false"/> en caso contrario.
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
        /// <see langword="true"/> ambas instancias de <see cref="Color"/> son distintas,
        /// <see langword="false"/> en caso contrario.
        /// </returns>
        public static bool operator !=(Color left, Color right) => !left.Equals(right);
        float r;
        float g;
        float b;
        float a;
        /// <summary>
        /// Obtiene o establece el valor RGB del canal rojo del color.
        /// </summary>
        public byte R
        {
            get => (byte)(r * byte.MaxValue);
            set => r = (float)value / byte.MaxValue;
        }
        /// <summary>
        /// Obtiene o establece el valor RGB del canal verde del color.
        /// </summary>
        public byte G
        {
            get => (byte)(g * byte.MaxValue);
            set => g = (float)value / byte.MaxValue;
        }
        /// <summary>
        /// Obtiene o establece el valor RGB del canal azul del color.
        /// </summary>
        public byte B
        {
            get => (byte)(b * byte.MaxValue);
            set => b = (float)value / byte.MaxValue;
        }
        /// <summary>
        /// Obtiene o establece el valor RGB del canal alfa del color.
        /// </summary>
        public byte A
        {
            get => (byte)(a * 255);
            set => a = (float)value / byte.MaxValue;
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
        /// Inicializa una nueva instancia de la esctructura 
        /// <see cref="Color"/>.
        /// </summary>
        /// <param name="R">Canal rojo.</param>
        /// <param name="G">Canal verde.</param>
        /// <param name="B">Canal azul.</param>
        public Color(byte R, byte G, byte B) : this(R, G, B, 255) { }
        /// <summary>
        /// Inicializa una nueva instancia de la esctructura 
        /// <see cref="Color"/>.
        /// </summary>
        /// <param name="R">Canal rojo.</param>
        /// <param name="G">Canal verde.</param>
        /// <param name="B">Canal azul.</param>
        /// <param name="A">Canal alfa.</param>
        public Color(byte R, byte G, byte B, byte A)
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
        public Color(float R, float G, float B) : this(R, G, B, 1.0f) { }
        /// <summary>
        /// Inicializa una nueva instancia de la esctructura 
        /// <see cref="Color"/>.
        /// </summary>
        /// <param name="R">Canal rojo.</param>
        /// <param name="G">Canal verde.</param>
        /// <param name="B">Canal azul.</param>
        /// <param name="A">Canal alfa.</param>
        public Color(float R, float G, float B, float A)
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
        /// <inheritdoc />
        /// <summary>
        /// Determina si el <see cref="T:TheXDS.MCART.Types.Color" /> especificado es igual al
        /// <see cref="T:TheXDS.MCART.Types.Color" /> actual.
        /// </summary>
        /// <param name="other">
        /// El <see cref="T:TheXDS.MCART.Types.Color" /> a comparar contra este <see cref="T:TheXDS.MCART.Types.Color" />.
        /// </param>
        /// <returns>
        /// <see langword="true" /> si el <see cref="T:TheXDS.MCART.Types.Color" /> especificado es igual al
        /// <see cref="T:TheXDS.MCART.Types.Color" /> actual, <see langword="false" /> en caso contrario.
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
        /// <inheritdoc />
        /// <summary>
        /// Compara este <see cref="T:TheXDS.MCART.Types.Color" /> contra otro.
        /// </summary>
        /// <param name="other"><see cref="T:TheXDS.MCART.Types.Color" /> a comparar.</param>
        /// <returns>
        /// Un valor que determina la posición ordinal de este color con
        /// respecto al otro.
        /// </returns>
        public int CompareTo(Color other)
        {
            var first = new ABGR32().To(this);
            var second = new ABGR32().To(other);
            return first.CompareTo(second);
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
            return $"#{new[] { A, R, G, B }.ToHex()}";
        }
    }
}