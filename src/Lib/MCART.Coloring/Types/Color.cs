/*
Color.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2026 César Andrés Morgan

Permission is hereby granted, free of charge, to any person obtaining a copy of
this software and associated documentation files (the "Software"), to deal in
the Software without restriction, including without limitation the rights to
use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
of the Software, and to permit persons to whom the Software is furnished to do
so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using TheXDS.MCART.Helpers;
using TheXDS.MCART.Math;
using TheXDS.MCART.Resources;
using TheXDS.MCART.Types.Base;
using static System.Math;
using static TheXDS.MCART.Types.Extensions.StringExtensions;
using CI = System.Globalization.CultureInfo;
using DR = System.Drawing;

namespace TheXDS.MCART.Types;

/// <summary>
/// Estructura universal que describe un color en sus componentes alfa,
/// rojo, verde y azul.
/// </summary>
public partial struct Color : IEquatable<Color>, IFormattable, IComparable<Color>, IColor, IScColor, ICastable<DR.Color>, ICloneable<Color>
{
    /// <summary>
    /// Obtiene una referencia al color transparente.
    /// </summary>
    public static Color Transparent => new(0, 0, 0, 0);

    /// <summary>
    /// Mezcla un color de temperatura basado en el porcentaje.
    /// </summary>
    /// <returns>
    /// El color que representa la temperatura del porcentaje.
    /// </returns>
    /// <param name="x">
    /// Valor porcentual utilizado para calcular la temperatura.
    /// </param>
    public static Color BlendHeat(in float x)
    {
        byte r = (byte)(Sqrt(2) * Cos((x + 1) * PI) * 255).Clamp(0, 255);
        byte g = (byte)(Sqrt(2) * Sin(x * PI) * 255).Clamp(0, 255);
        byte b = (byte)(Sqrt(2) * Cos(x * PI) * 255).Clamp(0, 255);
        return new Color(r, g, b);
    }

    /// <summary>
    /// Mezcla un color de salud basado en el porcentaje.
    /// </summary>
    /// <returns>El color que representa la salud del porcentaje.</returns>
    /// <param name="x">The x coordinate.</param>
    public static Color BlendHealth(in float x)
    {
        byte g = (byte)(510 * x).Clamp(0, 255);
        byte r = (byte)(510 - (510 * x)).Clamp(0, 255);
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
    public static Color Blend(in Color left, in Color right) => left / right;

    /// <summary>
    /// Realiza una mezcla entre los colores especificados.
    /// </summary>
    /// <param name="colors">colección de colores a mezclar.
    /// </param>
    /// <returns>Una mezcla entre los colores especificados.</returns>
    /// <exception cref="InvalidOperationException">
    /// Se produce si <paramref name="colors"/> no contiene elementos.
    /// </exception>
    public static Color Blend(in IEnumerable<Color> colors)
    {
        Blend_Contract(colors);
        float r = 0f, g = 0f, b = 0f, a = 0f;
        int c = 0;
        foreach (Color j in colors)
        {
            r += j.ScR;
            g += j.ScG;
            b += j.ScB;
            a += j.ScA;
            c++;
        }
        return new Color(r / c, g / c, b / c, a / c);
    }

    /// <summary>
    /// Determina si los colores son lo suficientemente similares.
    /// </summary>
    /// <returns>
    /// <see langword="true"/> si los colores son similares al menos en un
    /// 95%, <see langword="false"/> en caso contrario.
    /// </returns>
    /// <param name="color1">Primer <see cref="Color"/> a comparar.</param>
    /// <param name="color2">Segundo Color a comparar.</param>
    public static bool AreClose(in Color color1, in Color color2) => AreClose(color1, color2, 0.9f);

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
    public static bool AreClose(in Color color1, in Color color2, in float delta)
    {
        AreClose_Contract(delta);
        return Similarity(color1, color2) >= delta;
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
    public static float Similarity(in Color c1, in Color c2) => 1.0f - (Abs(c1.ScA - c2.ScA) + Abs(c1.ScR - c2.ScR) + Abs(c1.ScG - c2.ScG) + Abs(c1.ScB - c2.ScB)).Clamp(0f, 1f);

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
        Color? TryWith<TParser, TValue>(string format, Func<string, int, TValue> valueParser)
            where TParser : IColorParser<TValue>, new()
            where TValue : struct
            => from.IsFormattedAs(format) ? new TParser().From(valueParser($"0x{from[1..]}", 16)) : null;

        Color? TryName()
        {
            System.Reflection.PropertyInfo? cName = typeof(Colors).GetProperty(from, typeof(Color));
            return cName is not null ? (Color)cName.GetValue(null)! : null;
        }

        if (!from.IsEmpty() && (
            TryWith<Abgr32ColorParser, int>("#FFFFFFFF", Convert.ToInt32) ??
            TryWith<Bgr24ColorParser, int>("#FFFFFF", Convert.ToInt32) ??
            TryWith<Abgr4444ColorParser, short>("#FFFF", Convert.ToInt16) ??
            TryWith<Bgr12ColorParser, short>("#FFF", Convert.ToInt16) ??
            TryName()) is Color c)
        {
            color = c;
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
    public static Color From<T, TParser>(in T from) where T : struct where TParser : IColorParser<T>, new() => new TParser().From(from);

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
    public static T To<T, TParser>(in Color from) where T : struct where TParser : IColorParser<T>, new() => (new TParser()).To(from);

    /// <summary>
    /// Adds a <see cref="Color"/> to a <see cref="Color"/>, yielding a new
    /// <see cref="Color"/>.
    /// </summary>
    /// <param name="left">The first <see cref="Color"/> to add.</param>
    /// <param name="right">The second <see cref="Color"/> to add.</param>
    /// <returns>The <see cref="Color"/> that is the sum of the values of 
    /// <paramref name="left"/> and <paramref name="right"/>.</returns>
    public static Color operator +(in Color left, in Color right)
    {
        return new(
            (left._r + right._r).Clamp(0.0f, 1.0f),
            (left._g + right._g).Clamp(0.0f, 1.0f),
            (left._b + right._b).Clamp(0.0f, 1.0f),
            (left._a + right._a).Clamp(0.0f, 1.0f));
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
    public static Color operator -(in Color left, in Color right)
    {
        return new(
            (left._r - right._r).Clamp(0.0f, 1.0f),
            (left._g - right._g).Clamp(0.0f, 1.0f),
            (left._b - right._b).Clamp(0.0f, 1.0f),
            (left._a - right._a).Clamp(0.0f, 1.0f));
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
    public static Color operator *(in Color left, in float right)
    {
        return new(
            (left._r * right).Clamp(0.0f, 1.0f),
            (left._g * right).Clamp(0.0f, 1.0f),
            (left._b * right).Clamp(0.0f, 1.0f),
            (left._a * right).Clamp(0.0f, 1.0f));
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
    public static Color operator /(in Color left, in Color right)
    {
        return new(
            ((left._r + right._r) / 2).Clamp(0.0f, 1.0f),
            ((left._g + right._g) / 2).Clamp(0.0f, 1.0f),
            ((left._b + right._b) / 2).Clamp(0.0f, 1.0f),
            ((left._a + right._a) / 2).Clamp(0.0f, 1.0f));
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
    public static Color operator /(in Color left, in float right)
    {
        return new(
            (left._r / right).Clamp(0.0f, 1.0f),
            (left._g / right).Clamp(0.0f, 1.0f),
            (left._b / right).Clamp(0.0f, 1.0f),
            (left._a / right).Clamp(0.0f, 1.0f));
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
    public static bool operator ==(in Color left, in Color right) => left.Equals(right);

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
    public static bool operator !=(in Color left, in Color right) => !left.Equals(right);

    float _r, _g, _b, _a;

    /// <summary>
    /// Obtiene o establece el valor RGB del canal rojo del color.
    /// </summary>
    public byte R
    { readonly get => (byte)(_r * byte.MaxValue);
        set => _r = (float)value / byte.MaxValue;
    }

    /// <summary>
    /// Obtiene o establece el valor RGB del canal verde del color.
    /// </summary>
    public byte G
    { readonly get => (byte)(_g * byte.MaxValue);
        set => _g = (float)value / byte.MaxValue;
    }

    /// <summary>
    /// Obtiene o establece el valor RGB del canal azul del color.
    /// </summary>
    public byte B
    { readonly get => (byte)(_b * byte.MaxValue);
        set => _b = (float)value / byte.MaxValue;
    }

    /// <summary>
    /// Obtiene o establece el valor RGB del canal alfa del color.
    /// </summary>
    public byte A
    { readonly get => (byte)(_a * 255);
        set => _a = (float)value / byte.MaxValue;
    }

    /// <summary>
    /// Obtiene o establece el valor ScRGB del canal rojo del color.
    /// </summary>
    public float ScR
    { readonly get => _r;
        set => _r = value.Clamp(0.0f, 1.0f);
    }

    /// <summary>
    /// Obtiene o establece el valor ScRGB del canal verde del color.
    /// </summary>
    public float ScG
    { readonly get => _g;
        set => _g = value.Clamp(0.0f, 1.0f);
    }

    /// <summary>
    /// Obtiene o establece el valor ScRGB del canal azul del color.
    /// </summary>
    public float ScB
    { readonly get => _b;
        set => _b = value.Clamp(0.0f, 1.0f);
    }

    /// <summary>
    /// Obtiene o establece el valor ScRGB del canal alfa del color.
    /// </summary>
    public float ScA
    { readonly get => _a;
        set => _a = value.Clamp(0.0f, 1.0f);
    }

    /// <summary>
    /// Inicializa una nueva instancia de la estructura 
    /// <see cref="Color"/>.
    /// </summary>
    /// <param name="r">Canal rojo.</param>
    /// <param name="g">Canal verde.</param>
    /// <param name="b">Canal azul.</param>
    public Color(in byte r, in byte g, in byte b) : this(r, g, b, 255) { }

    /// <summary>
    /// Inicializa una nueva instancia de la estructura 
    /// <see cref="Color"/>.
    /// </summary>
    /// <param name="r">Canal rojo.</param>
    /// <param name="g">Canal verde.</param>
    /// <param name="b">Canal azul.</param>
    /// <param name="a">Canal alfa.</param>
    public Color(in byte r, in byte g, in byte b, in byte a)
    {
        _r = (float)r / 255;
        _g = (float)g / 255;
        _b = (float)b / 255;
        _a = (float)a / 255;
    }

    /// <summary>
    /// Inicializa una nueva instancia de la estructura 
    /// <see cref="Color"/>.
    /// </summary>
    /// <param name="r">Canal rojo.</param>
    /// <param name="g">Canal verde.</param>
    /// <param name="b">Canal azul.</param>
    public Color(in float r, in float g, in float b) : this(r, g, b, 1f) { }

    /// <summary>
    /// Inicializa una nueva instancia de la estructura 
    /// <see cref="Color"/>.
    /// </summary>
    /// <param name="r">Canal rojo.</param>
    /// <param name="g">Canal verde.</param>
    /// <param name="b">Canal azul.</param>
    /// <param name="a">Canal alfa.</param>
    public Color(in float r, in float g, in float b, in float a)
    {
        _a = a.Clamp(0.0f, 1.0f);
        _r = r.Clamp(0.0f, 1.0f);
        _g = g.Clamp(0.0f, 1.0f);
        _b = b.Clamp(0.0f, 1.0f);
    }

    /// <summary>
    /// Determina si el <see cref="IScColor" /> especificado es igual al
    /// <see cref="Color" /> actual.
    /// </summary>
    /// <param name="other">
    /// El <see cref="Color" /> a comparar contra este <see cref="Color" />.
    /// </param>
    /// <returns>
    /// <see langword="true" /> si el <see cref="Color" /> especificado es igual al
    /// <see cref="Color" /> actual, <see langword="false" /> en caso contrario.
    /// </returns>
    public readonly bool Equals(IScColor other)
    {
        return _a == other.ScA && _r == other.ScR && _g == other.ScG && _b == other.ScB;
    }

    /// <summary>
    /// Determina si el <see cref="IColor" /> especificado es igual al
    /// <see cref="Color" /> actual.
    /// </summary>
    /// <param name="other">
    /// El <see cref="Color" /> a comparar contra este <see cref="Color" />.
    /// </param>
    /// <returns>
    /// <see langword="true" /> si el <see cref="Color" /> especificado es igual al
    /// <see cref="Color" /> actual, <see langword="false" /> en caso contrario.
    /// </returns>
    public readonly bool Equals(IColor other)
    {
        return A == other.A && R == other.R && G == other.G && B == other.B;
    }

    /// <summary>
    /// Determina si el <see cref="Color" /> especificado es igual al
    /// <see cref="Color" /> actual.
    /// </summary>
    /// <param name="other">
    /// El <see cref="Color" /> a comparar contra este <see cref="Color" />.
    /// </param>
    /// <returns>
    /// <see langword="true" /> si el <see cref="Color" /> especificado es igual al
    /// <see cref="Color" /> actual, <see langword="false" /> en caso contrario.
    /// </returns>
    public readonly bool Equals(Color other)
    {
        return _a == other._a && _r == other._r && _g == other._g && _b == other._b;
    }

    /// <summary>
    /// Returns a <see cref="string"/> that represents the current 
    /// <see cref="Color"/>.
    /// </summary>
    /// <returns>
    /// A <see cref="string"/> that represents the current 
    /// <see cref="Color"/>.
    /// </returns>
    /// <param name="format">Format.</param>
    public readonly string ToString(string? format)
    {
        return ToString(format, null);
    }

    /// <summary>
    /// Returns a <see cref="string"/> that represents the current 
    /// <see cref="Color"/>.
    /// </summary>
    /// <returns>
    /// A <see cref="string"/> that represents the current 
    /// <see cref="Color"/>.
    /// </returns>
    /// <param name="format">Format.</param>
    /// <param name="formatProvider">Format provider.</param>
    public readonly string ToString(string? format, IFormatProvider? formatProvider)
    {
        if (format.IsEmpty()) format = "H";
        formatProvider ??= CI.CurrentCulture;
        return format! switch
        {
            "H" => $"#{new[] { A, R, G, B }.ToHex()}",
            "h" => $"#{new[] { A, R, G, B }.ToHex().ToLower((CI)formatProvider)}",
            "B" => $"A:{A} R:{R} G:{G} B:{B}",
            "b" => $"a:{A} r:{R} g:{G} b:{B}",
            "F" => $"A:{_a.ToString(formatProvider)} R:{_r.ToString(formatProvider)} G:{_g.ToString(formatProvider)} B:{_b.ToString(formatProvider)}",
            "f" => $"a:{_a.ToString(formatProvider)} r:{_r.ToString(formatProvider)} g:{_g.ToString(formatProvider)} b:{_b.ToString(formatProvider)}",
            _ => CustomFormat(format, (CI)formatProvider)
        };
    }

    /// <summary>
    /// Compara este <see cref="Color" /> contra otro.
    /// </summary>
    /// <param name="other"><see cref="Color" /> a comparar.</param>
    /// <returns>
    /// Un valor que determina la posición ordinal de este color con
    /// respecto al otro.
    /// </returns>
    public readonly int CompareTo(Color other)
    {
        int first = new Abgr32ColorParser().To(this);
        int second = new Abgr32ColorParser().To(other);
        return first.CompareTo(second);
    }

    /// <summary>
    /// Indica si este objeto y el especificado son la misma instancia.
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override readonly bool Equals(object? obj)
    {
        return obj switch
        {
            Color c => this == c,
            DR.Color c => this == (Color)c,
            IColor c => Equals(c),
            IScColor c => Equals(c),
            _ => base.Equals(obj)
        };
    }

    /// <summary>
    /// Obtiene el código Hash pasa esta instancia.
    /// </summary>
    /// <returns></returns>
    public override readonly int GetHashCode()
    {
        return HashCode.Combine(_r, _g, _b, _a);
    }

    /// <summary>
    /// Returns a <see cref="string"/> that represents the current 
    /// <see cref="Color"/>.
    /// </summary>
    /// <returns>
    /// A <see cref="string"/> that represents the current 
    /// <see cref="Color"/>.
    /// </returns>
    public override readonly string ToString()
    {
        return $"#{new[] { A, R, G, B }.ToHex()}";
    }

    /// <inheritdoc/>
    public readonly Color Clone()
    {
        return new(_r, _g, _b, _a);
    }

    /// <summary>
    /// Convierte implícitamente un <see cref="Color"/> en un
    /// <see cref="DR.Color"/>.
    /// </summary>
    /// <param name="color"></param>
    public static implicit operator DR.Color(in Color color)
    {
        return DR.Color.FromArgb(color.A, color.R, color.G, color.B);
    }

    /// <summary>
    /// Convierte implícitamente un <see cref="DR.Color"/> en un
    /// <see cref="Color"/>.
    /// </summary>
    /// <param name="color"></param>
    public static implicit operator Color(in DR.Color color)
    {
        return new(color.R, color.G, color.B, color.A);
    }

    private readonly string CustomFormat(string format, CI formatProvider)
    {
        format = format.Replace("AA", A.ToString("x2").ToUpper(formatProvider));
        format = format.Replace("aa", A.ToString("x2").ToLower(formatProvider));
        format = format.Replace("RR", R.ToString("x2").ToUpper(formatProvider));
        format = format.Replace("rr", R.ToString("x2").ToLower(formatProvider));
        format = format.Replace("GG", G.ToString("x2").ToUpper(formatProvider));
        format = format.Replace("gg", G.ToString("x2").ToLower(formatProvider));
        format = format.Replace("BB", B.ToString("x2").ToUpper(formatProvider));
        format = format.Replace("bb", B.ToString("x2").ToLower(formatProvider));
        return format;
    }

    readonly DR.Color ICastable<DR.Color>.Cast() => this;
}
