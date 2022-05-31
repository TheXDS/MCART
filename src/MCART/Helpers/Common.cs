/*
Common.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Este archivo contiene operaciones comunes de transformación de datos en los
programas, y de algunas comparaciones especiales.

Algunas de estas funciones también se implementan como extensiones, por lo que
para ser llamadas únicamente es necesario importar el espacio de nombres
"TheXDS.MCART" y utilizar sintaxis de instancia.

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2022 César Andrés Morgan

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

namespace TheXDS.MCART.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Math;
using TheXDS.MCART.Resources;
using TheXDS.MCART.Types;
using TheXDS.MCART.Types.Extensions;
using static TheXDS.MCART.Misc.Internals;
using St = Resources.Strings.Common;

/// <summary>
/// Contiene operaciones comunes de transformación de datos en los
/// programas, y de algunas comparaciones especiales.
/// </summary>
/// <remarks>
/// Algunas de estas funciones también se implementan como extensiones, por
/// lo que para ser llamadas únicamente es necesario importar el espacio de
/// nombres <see cref="MCART" /> y utilizar sintaxis de instancia.
/// </remarks>
public static partial class Common
{
    private static IEnumerable<bool> ToBits(this in ulong value, in byte maxBits)
    {
        bool[]? a = new bool[maxBits];
        for (int j = 0; j < maxBits; j++)
        {
            a[j] = (value & (ulong)Math.Pow(2, j)) != 0;
        }
        return a;
    }
    
    private static byte BitCount(ulong value, in byte maxBits)
    {
        byte c = 0;
        byte f = 0;
        while (value != 0 & f++ < maxBits)
        {
            c += (byte)(value & 1);
            value >>= 1;
        }
        return c;
    }

    /// <summary>
    /// Determina si un conjunto de cadenas están vacías.
    /// </summary>
    /// <returns>
    /// <see langword="true" /> si las cadenas están vacías o son 
    /// <see langword="null" />; de lo contrario, <see langword="false" />.
    /// </returns>
    /// <param name="stringArray">Cadenas a comprobar.</param>
    /// <exception cref="ArgumentNullException">
    /// Se produce si <paramref name="stringArray"/> es
    /// <see langword="null"/>.
    /// </exception>
    public static bool AllEmpty(params string?[] stringArray)
    {
        return stringArray.AsEnumerable().AllEmpty();
    }

    /// <summary>
    /// Determina si alguna cadena está vacía.
    /// </summary>
    /// <returns>
    /// <see langword="true" /> si alguna cadena está vacía o es 
    /// <see langword="null" />; de lo contrario, <see langword="false" />.
    /// </returns>
    /// <param name="stringArray">Cadenas a comprobar.</param>
    /// <param name="index">
    /// Argumento de salida. Índices de las cadenas vacías encontradas.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Se produce si <paramref name="stringArray"/> es 
    /// <see langword="null"/>.
    /// </exception>
    public static bool AnyEmpty(out IEnumerable<int> index, params string?[] stringArray)
    {
        return stringArray.AsEnumerable().AnyEmpty(out index);
    }

    /// <summary>
    /// Determina si alguna cadena está vacía.
    /// </summary>
    /// <returns>
    /// <see langword="true" /> si alguna cadena está vacía o es 
    /// <see langword="null" />; de lo contrario, <see langword="false" />.
    /// </returns>
    /// <param name="stringArray">Cadenas a comprobar.</param>
    /// <param name="firstIndex">
    /// Argumento de salida. Índice de la primera cadena vacía encontrada.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Se produce si <paramref name="stringArray"/> es 
    /// <see langword="null"/>.
    /// </exception>
    public static bool AnyEmpty(out int firstIndex, params string?[] stringArray)
    {
        return stringArray.AsEnumerable().AnyEmpty(out firstIndex);
    }

    /// <summary>
    /// Determina si alguna cadena está vacía.
    /// </summary>
    /// <returns>
    /// <see langword="true" /> si alguna cadena está vacía o es
    /// <see langword="null" />; de lo contrario, <see langword="false" />.
    /// </returns>
    /// <param name="stringArray">Cadenas a comprobar.</param>
    /// <exception cref="ArgumentNullException">
    /// Se produce si <paramref name="stringArray"/> es 
    /// <see langword="null"/>.
    /// </exception>
    public static bool AnyEmpty(params string?[] stringArray)
    {
        return stringArray.AsEnumerable().AnyEmpty();
    }

    /// <summary>
    /// Obtiene la cuenta de bits que conforman el valor.
    /// </summary>
    /// <param name="value">
    /// Valor a procesar.
    /// </param>
    /// <returns>
    /// La cantidad de bits establecidos en 1 del valor.
    /// </returns>
    public static byte BitCount(this in byte value) => BitCount(value, 8);

    /// <summary>
    /// Obtiene la cuenta de bits que conforman el valor.
    /// </summary>
    /// <param name="value">
    /// Valor a procesar.
    /// </param>
    /// <returns>
    /// La cantidad de bits establecidos en 1 del valor.
    /// </returns>
    public static byte BitCount(this in int value) => BitCount((ulong)value, 32);

    /// <summary>
    /// Obtiene la cuenta de bits que conforman el valor.
    /// </summary>
    /// <param name="value">
    /// Valor a procesar.
    /// </param>
    /// <returns>
    /// La cantidad de bits establecidos en 1 del valor.
    /// </returns>
    public static byte BitCount(this in long value) => BitCount((ulong)value, 64);

    /// <summary>
    /// Obtiene la cuenta de bits que conforman el valor.
    /// </summary>
    /// <param name="value">
    /// Valor a procesar.
    /// </param>
    /// <returns>
    /// La cantidad de bits establecidos en 1 del valor.
    /// </returns>
    [CLSCompliant(false)]
    public static byte BitCount(this in sbyte value) => BitCount((ulong)value, 8);

    /// <summary>
    /// Obtiene la cuenta de bits que conforman el valor.
    /// </summary>
    /// <param name="value">
    /// Valor a procesar.
    /// </param>
    /// <returns>
    /// La cantidad de bits establecidos en 1 del valor.
    /// </returns>
    public static byte BitCount(this in short value) => BitCount((ulong)value, 16);

    /// <summary>
    /// Obtiene la cuenta de bits que conforman el valor.
    /// </summary>
    /// <param name="value">
    /// Valor a procesar.
    /// </param>
    /// <returns>
    /// La cantidad de bits establecidos en 1 del valor.
    /// </returns>
    [CLSCompliant(false)]
    public static byte BitCount(this in uint value) => BitCount(value, 32);

    /// <summary>
    /// Obtiene la cuenta de bits que conforman el valor.
    /// </summary>
    /// <param name="value">
    /// Valor a procesar.
    /// </param>
    /// <returns>
    /// La cantidad de bits establecidos en 1 del valor.
    /// </returns>
    [CLSCompliant(false)]
    public static byte BitCount(this in ulong value) => BitCount(value, 64);

    /// <summary>
    /// Obtiene la cuenta de bits que conforman el valor.
    /// </summary>
    /// <param name="value">
    /// Valor a procesar.
    /// </param>
    /// <returns>
    /// La cantidad de bits establecidos en 1 del valor.
    /// </returns>
    [CLSCompliant(false)] 
    public static byte BitCount(this in ushort value) => BitCount(value, 16);

    /// <summary>
    /// Convierte un valor <see cref="long"/> que representa una cuenta de
    /// bytes en la unidad de magnitud más fácil de leer.
    /// </summary>
    /// <param name="bytes">Cantidad de bytes a representar.</param>
    /// <param name="unit">Tipo de unidad a utilizar.</param>
    /// <param name="magnitude">
    /// Magnitud inicial de bytes. <c>0</c> indica que el valor de 
    /// <paramref name="bytes"/> debe tratarse directamente como el valor
    /// en bytes de la operación. El valor máximo permitido es <c>8</c>
    /// para indicar Yottabytes.
    /// </param>
    /// <returns>
    /// Una cadena con la cantidad de bytes utilizando la unidad de
    /// magnitud adecuada.
    /// </returns>
    public static string ByteUnits(in this int bytes, in ByteUnitType unit, byte magnitude)
    {
        ByteUnits_Contract(bytes, unit, magnitude);
        return ByteUnits((long)(bytes * System.Math.Pow(unit switch
        {
            ByteUnitType.Binary or ByteUnitType.BinaryLong => 1024,
            ByteUnitType.Decimal or ByteUnitType.DecimalLong => 1000,
            _ => throw Errors.UndefinedEnum(nameof(unit), unit)
        }, magnitude)), unit);
    }

    /// <summary>
    /// Convierte un valor <see cref="long"/> que representa una cuenta de
    /// bytes en la unidad de magnitud más fácil de leer.
    /// </summary>
    /// <param name="bytes">Cantidad de bytes a representar.</param>
    /// <returns>
    /// Una cadena con la cantidad de bytes utilizando la unidad de
    /// magnitud adecuada.
    /// </returns>
    public static string ByteUnits(in this long bytes)
    {
        return ByteUnits(bytes, ByteUnitType.Binary);
    }

    /// <summary>
    /// Convierte un valor <see cref="long"/> que representa una cuenta de
    /// bytes en la unidad de magnitud más fácil de leer.
    /// </summary>
    /// <param name="bytes">Cantidad de bytes a representar.</param>
    /// <param name="unit">Tipo de unidad a utilizar.</param>
    /// <returns>
    /// Una cadena con la cantidad de bytes utilizando la unidad de
    /// magnitud adecuada.
    /// </returns>
    public static string ByteUnits(in this long bytes, in ByteUnitType unit)
    {
        int c = 0;
        double b = bytes;

        (double mag, string[] u) = unit switch
        {
            ByteUnitType.Binary => (1024, new[] { St.KiB, St.MiB, St.GiB, St.TiB, St.PiB, St.EiB, St.ZiB, St.YiB }),
            ByteUnitType.Decimal => (1000, new[] { St.KB, St.MB, St.GB, St.TB, St.PB, St.EB, St.ZB, St.YB }),
            ByteUnitType.BinaryLong => (1024, new[] { St.KiBl, St.MiBl, St.GiBl, St.TiBl, St.PiBl, St.EiBl, St.ZiBl, St.YiBl }),
            ByteUnitType.DecimalLong => (1000, new[] { St.KBl, St.MBl, St.GBl, St.TBl, St.PBl, St.EBl, St.ZBl, St.YBl }),
            _ => (double.PositiveInfinity, Array.Empty<string>())
        };

        while (b > mag - 1 && c < u.Length)
        {
            c++;
            b /= mag;
        }

        return c > 0 ? $"{b + (b / mag):F1} {u[c.Clamp(u.Length) - 1]}" : $"{bytes} {St.Bytes}";
    }

    /// <summary>
    /// Busca y obtiene un <see cref="TypeConverter" /> apropiado para
    /// realizar la conversión entre tipos solicitada.
    /// </summary>
    /// <param name="source">Tipo de datos de origen.</param>
    /// <param name="target">Tipo de datos de destino.</param>
    /// <returns>
    /// Un <see cref="TypeConverter" /> capaz de realizar la conversión
    /// entre los tipos requeridos, o <see langword="null" /> si no se
    /// ha encontrado un convertidor adecuado.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Se produce si <paramref name="source"/> o <paramref name="target"/>
    /// son <see langword="null"/>.
    /// </exception>
    public static TypeConverter? FindConverter(Type source, Type target)
    {
        return FindConverters(source, target).FirstOrDefault();
    }

    /// <summary>
    /// Busca y obtiene un <see cref="TypeConverter" /> apropiado para
    /// realizar la conversión entre <see cref="string" /> y el tipo
    /// especificado.
    /// </summary>
    /// <param name="target">Tipo de datos de destino.</param>
    /// <returns>
    /// Un <see cref="TypeConverter" /> capaz de realizar la conversión
    /// entre <see cref="string" /> y el tipo especificado, o
    /// <see langword="null" /> si no se ha encontrado un convertidor
    /// adecuado.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Se produce si <paramref name="target"/> es <see langword="null"/>.
    /// </exception>
    public static TypeConverter? FindConverter(Type target)
    {
        NullCheck(target, nameof(target));
        return FindConverter(typeof(string), target);
    }

    /// <summary>
    /// Busca y obtiene un <see cref="TypeConverter" /> apropiado para
    /// realizar la conversión entre <see cref="string" /> y el tipo
    /// especificado.
    /// </summary>
    /// <typeparam name="T">Tipo de datos de destino.</typeparam>
    /// <returns>
    /// Un <see cref="TypeConverter" /> capaz de realizar la conversión
    /// entre <see cref="string" /> y el tipo especificado, o
    /// <see langword="null" /> si no se ha encontrado un convertidor
    /// adecuado.
    /// </returns>
    public static TypeConverter? FindConverter<T>()
    {
        return FindConverter(typeof(T));
    }

    /// <summary>
    /// Busca y obtiene un <see cref="TypeConverter" /> apropiado para
    /// realizar la conversión entre tipos solicitada.
    /// </summary>
    /// <typeparam name="TSource">Tipo de datos de origen.</typeparam>
    /// <typeparam name="TTarget">Tipo de datos de destino.</typeparam>
    /// <returns>
    /// Un <see cref="TypeConverter" /> capaz de realizar la conversión
    /// entre los tipos requeridos, o <see langword="null" /> si no se
    /// ha encontrado un convertidor adecuado.
    /// </returns>
    public static TypeConverter? FindConverter<TSource, TTarget>()
    {
        return FindConverter(typeof(TSource), typeof(TTarget));
    }

    /// <summary>
    /// Busca y obtiene un <see cref="TypeConverter" /> apropiado para
    /// realizar la conversión entre tipos solicitada.
    /// </summary>
    /// <param name="source">Tipo de datos de origen.</param>
    /// <param name="target">Tipo de datos de destino.</param>
    /// <returns>
    /// Un <see cref="TypeConverter" /> capaz de realizar la conversión
    /// entre los tipos requeridos, o <see langword="null" /> si no se
    /// ha encontrado un convertidor adecuado.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Se produce si <paramref name="source"/> o <paramref name="target"/>
    /// son <see langword="null"/>.
    /// </exception>
    public static IEnumerable<TypeConverter> FindConverters(Type source, Type target)
    {
        NullCheck(source, nameof(source));
        NullCheck(target, nameof(target));
        try
        {
            return Objects.PublicTypes<TypeConverter>()
                .Where(TypeExtensions.IsInstantiable)
                .Select(j => j.New<TypeConverter>(false, Array.Empty<object>()))
                .NotNull()
                .Where(t => t.CanConvertFrom(source) && t.CanConvertTo(target));
        }
        finally { GC.Collect(); }
    }

    /// <summary>
    /// Invierte el Endianess de un valor <see cref="char" />.
    /// </summary>
    /// <param name="value">Valor cuyos bytes se invertirán.</param>
    /// <returns>
    /// Un <see cref="char" /> cuyo Endianess ha sido invertido.
    /// </returns>
    public static char FlipEndianess(this in char value)
    {
        return BitConverter.ToChar(BitConverter.GetBytes(value).Reverse().ToArray(), 0);
    }

    /// <summary>
    /// Invierte el Endianess de un valor <see cref="double" />.
    /// </summary>
    /// <param name="value">Valor cuyos bytes se invertirán.</param>
    /// <returns>
    /// Un <see cref="double" /> cuyo Endianess ha sido invertido.
    /// </returns>
    public static double FlipEndianess(this in double value)
    {
        return BitConverter.ToDouble(BitConverter.GetBytes(value).Reverse().ToArray(), 0);
    }

    /// <summary>
    /// Invierte el Endianess de un valor <see cref="float" />.
    /// </summary>
    /// <param name="value">Valor cuyos bytes se invertirán.</param>
    /// <returns>
    /// Un <see cref="float" /> cuyo Endianess ha sido invertido.
    /// </returns>
    public static float FlipEndianess(this in float value)
    {
        return BitConverter.ToSingle(BitConverter.GetBytes(value).Reverse().ToArray(), 0);
    }

    /// <summary>
    /// Invierte el Endianess de un valor <see cref="int" />.
    /// </summary>
    /// <param name="value">Valor cuyos bytes se invertirán.</param>
    /// <returns>
    /// Un <see cref="int" /> cuyo Endianess ha sido invertido.
    /// </returns>
    public static int FlipEndianess(this in int value)
    {
        return BitConverter.ToInt32(BitConverter.GetBytes(value).Reverse().ToArray(), 0);
    }

    /// <summary>
    /// Invierte el Endianess de un valor <see cref="long" />.
    /// </summary>
    /// <param name="value">Valor cuyos bytes se invertirán.</param>
    /// <returns>
    /// Un <see cref="long" /> cuyo Endianess ha sido invertido.
    /// </returns>
    public static long FlipEndianess(this in long value)
    {
        return BitConverter.ToInt64(BitConverter.GetBytes(value).Reverse().ToArray(), 0);
    }

    /// <summary>
    /// Invierte el Endianess de un valor <see cref="short" />.
    /// </summary>
    /// <param name="value">Valor cuyos bytes se invertirán.</param>
    /// <returns>
    /// Un <see cref="short" /> cuyo Endianess ha sido invertido.
    /// </returns>
    public static short FlipEndianess(this in short value)
    {
        return BitConverter.ToInt16(BitConverter.GetBytes(value).Reverse().ToArray(), 0);
    }

    /// <summary>
    /// Invierte el Endianess de un valor <see cref="uint" />.
    /// </summary>
    /// <param name="value"></param>
    /// <returns>Un <see cref="uint" /> cuyo Endianess ha sido invertido.</returns>
    [CLSCompliant(false)]
    public static uint FlipEndianess(this in uint value)
    {
        return BitConverter.ToUInt32(BitConverter.GetBytes(value).Reverse().ToArray(), 0);
    }

    /// <summary>
    /// Invierte el Endianess de un valor <see cref="ulong" />.
    /// </summary>
    /// <param name="value"></param>
    /// <returns>Un <see cref="ulong" /> cuyo Endianess ha sido invertido.</returns>
    [CLSCompliant(false)]
    public static ulong FlipEndianess(this in ulong value)
    {
        return BitConverter.ToUInt64(BitConverter.GetBytes(value).Reverse().ToArray(), 0);
    }

    /// <summary>
    /// Invierte el Endianess de un valor <see cref="ushort" />.
    /// </summary>
    /// <param name="value"></param>
    /// <returns>Un <see cref="ushort" /> cuyo Endianess ha sido invertido.</returns>
    [CLSCompliant(false)]
    public static ushort FlipEndianess(this in ushort value)
    {
        return BitConverter.ToUInt16(BitConverter.GetBytes(value).Reverse().ToArray(), 0);
    }

    /// <summary>
    /// Comprueba que el valor se encuentre en el rango especificado.
    /// </summary>
    /// <typeparam name="T">Tipo de objeto a comprobar.</typeparam>
    /// <param name="value">Valor a comprobar.</param>
    /// <param name="range">Rango de valores inclusivos a comprobar.</param>
    /// <returns>
    /// <see langword="true" /> si el valor se encuentra entre los
    /// especificados; de lo contrario, <see langword="false" />.
    /// </returns>
    public static bool IsBetween<T>(this T value, in Range<T> range) where T : IComparable<T>
    {
        return range.IsWithin(value);
    }

    /// <summary>
    /// Comprueba que el valor se encuentre en el rango especificado.
    /// </summary>
    /// <returns>
    /// <see langword="true" /> si el valor se encuentra entre los
    /// especificados; de lo contrario, <see langword="false" />.
    /// </returns>
    /// <param name="value">Valor a comprobar.</param>
    /// <param name="min">Mínimo del rango de valores, inclusive.</param>
    /// <param name="max">Máximo del rango de valores, inclusive.</param>
    /// <typeparam name="T">Tipo de objeto a comprobar.</typeparam>
    public static bool IsBetween<T>(this T value, in T min, in T max) where T : IComparable<T>
    {
        return IsBetween(value, min, max, true);
    }

    /// <summary>
    /// Comprueba que el valor se encuentre en el rango especificado.
    /// </summary>
    /// <returns>
    /// <see langword="true" /> si el valor se encuentra entre los
    /// especificados; de lo contrario, <see langword="false" />.
    /// </returns>
    /// <param name="value">Valor a comprobar.</param>
    /// <param name="min">Mínimo del rango de valores.</param>
    /// <param name="max">Máximo del rango de valores.</param>
    /// <param name="inclusive">Inclusividad. de forma predeterminada, la comprobación es inclusive.</param>
    /// <typeparam name="T">Tipo de objeto a comprobar.</typeparam>
    public static bool IsBetween<T>(this T value, in T min, in T max, in bool inclusive) where T : IComparable<T>
    {
        return IsBetween(value, min, max, inclusive, inclusive);
    }

    /// <summary>
    /// Comprueba que el valor se encuentre en el rango especificado.
    /// </summary>
    /// <returns>
    /// <see langword="true" /> si el valor se encuentra entre los
    /// especificados; de lo contrario, <see langword="false" />.
    /// </returns>
    /// <param name="value">Valor a comprobar.</param>
    /// <param name="min">Mínimo del rango de valores.</param>
    /// <param name="max">Máximo del rango de valores.</param>
    /// <param name="minInclusive">Inclusividad del valor mínimo. de forma predeterminada, la comprobación es inclusive.</param>
    /// <param name="maxInclusive">Inclusividad del valor máximo. de forma predeterminada, la comprobación es inclusive.</param>
    /// <typeparam name="T">Tipo de objeto a comprobar.</typeparam>
    public static bool IsBetween<T>(this T value, in T min, in T max, in bool minInclusive, in bool maxInclusive) where T : IComparable<T>
    {
        return (minInclusive ? value.CompareTo(min) >= 0 : value.CompareTo(min) > 0)
            && (maxInclusive ? value.CompareTo(max) <= 0 : value.CompareTo(max) < 0);
    }

    /// <summary>
    /// Comprueba que el valor se encuentre en el rango especificado.
    /// </summary>
    /// <typeparam name="T">Tipo de objeto a comprobar.</typeparam>
    /// <param name="value">Valor a comprobar.</param>
    /// <param name="range">Rango de valores inclusivos a comprobar.</param>
    /// <returns>
    /// <see langword="true" /> si el valor se encuentra entre los
    /// especificados; de lo contrario, <see langword="false" />.
    /// </returns>
    public static bool IsBetween<T>(this T? value, in Range<T> range) where T : struct, IComparable<T>
    {
        return value.HasValue && range.IsWithin(value.Value);
    }

    /// <summary>
    /// Comprueba que el valor se encuentre en el rango especificado.
    /// </summary>
    /// <returns>
    /// <see langword="true" /> si el valor se encuentra entre los
    /// especificados; de lo contrario, <see langword="false" />.
    /// </returns>
    /// <param name="value">Valor a comprobar.</param>
    /// <param name="min">Mínimo del rango de valores, inclusive.</param>
    /// <param name="max">Máximo del rango de valores, inclusive.</param>
    /// <typeparam name="T">Tipo de objeto a comprobar.</typeparam>
    public static bool IsBetween<T>(this T? value, in T min, in T max) where T : struct, IComparable<T>
    {
        return value.IsBetween(min, max, true, true);
    }

    /// <summary>
    /// Comprueba que el valor se encuentre en el rango especificado.
    /// </summary>
    /// <returns>
    /// <see langword="true" /> si el valor se encuentra entre los
    /// especificados; de lo contrario, <see langword="false" />.
    /// </returns>
    /// <param name="value">Valor a comprobar.</param>
    /// <param name="min">Mínimo del rango de valores.</param>
    /// <param name="max">Máximo del rango de valores.</param>
    /// <param name="inclusive">Inclusividad. de forma predeterminada, la comprobación es inclusive.</param>
    /// <typeparam name="T">Tipo de objeto a comprobar.</typeparam>
    public static bool IsBetween<T>(this T? value, in T min, in T max, in bool inclusive) where T : struct, IComparable<T>
    {
        return IsBetween(value, min, max, inclusive, inclusive);
    }

    /// <summary>
    /// Comprueba que el valor se encuentre en el rango especificado.
    /// </summary>
    /// <returns>
    /// <see langword="true" /> si el valor se encuentra entre los
    /// especificados; de lo contrario, <see langword="false" />.
    /// </returns>
    /// <param name="value">Valor a comprobar.</param>
    /// <param name="min">Mínimo del rango de valores.</param>
    /// <param name="max">Máximo del rango de valores.</param>
    /// <param name="minInclusive">Inclusividad del valor mínimo. de forma predeterminada, la comprobación es inclusive.</param>
    /// <param name="maxInclusive">Inclusividad del valor máximo. de forma predeterminada, la comprobación es inclusive.</param>
    /// <typeparam name="T">Tipo de objeto a comprobar.</typeparam>
    public static bool IsBetween<T>(this T? value, in T min, in T max, in bool minInclusive, in bool maxInclusive) where T : struct, IComparable<T>
    {
        if (!value.HasValue) return false;
        T v = value.Value;
        return (minInclusive ? v.CompareTo(min) >= 0 : v.CompareTo(min) > 0)
               && (maxInclusive ? v.CompareTo(max) <= 0 : v.CompareTo(max) < 0);
    }

    /// <summary>
    /// Condensa una lista en una <see cref="string" />
    /// </summary>
    /// <returns>
    /// Una cadena en formato de lista cuyos miembros están separados por
    /// el separador de línea predeterminado del sistema.
    /// </returns>
    /// <param name="collection">
    /// Lista a condensar. Sus elementos deben ser del
    /// tipo <see cref="string" />.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Se produce si <paramref name="collection"/> es
    /// <see langword="null"/>.
    /// </exception>
    public static string Listed(this IEnumerable<string> collection)
    {
        NullCheck(collection, nameof(collection));
        return string.Join(Environment.NewLine, collection);
    }

    /// <summary>
    /// Genera una secuencia de números en el rango especificado.
    /// </summary>
    /// <returns>
    /// Una lista de enteros con la secuencia generada.
    /// </returns>
    /// <param name="floor">Valor más bajo.</param>
    /// <param name="top">Valor más alto.</param>
    public static IEnumerable<int> Sequence(in int floor, in int top)
    {
        return Sequence(floor, top, 1);
    }

    /// <summary>
    /// Genera una secuencia de números en el rango especificado.
    /// </summary>
    /// <returns>
    /// Una lista de enteros con la secuencia generada.
    /// </returns>
    /// <param name="top">Valor más alto.</param>
    public static IEnumerable<int> Sequence(in int top)
    {
        return Sequence(0, top, 1);
    }

    /// <summary>
    /// Genera una secuencia de números en el rango especificado.
    /// </summary>
    /// <returns>
    /// Una lista de enteros con la secuencia generada.
    /// </returns>
    /// <param name="floor">Valor más bajo.</param>
    /// <param name="top">Valor más alto.</param>
    /// <param name="stepping">Saltos del secuenciador.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Se produce si <paramref name="stepping"/> es igual a <c>0</c>.
    /// </exception>
    public static IEnumerable<int> Sequence(int floor, int top, int stepping)
    {
        Sequence_Contract(top, stepping);
        if (floor > top) stepping *= -1;
        for (int b = floor; stepping > 0 ? b <= top : b >= top; b += stepping)
            yield return b;
    }

    /// <summary>
    /// Convierte un <see cref="byte"/> en una colección de bits.
    /// </summary>
    /// <param name="value">
    /// Valor a convertir.
    /// </param>
    /// <returns>
    /// Una colección de los bits que componen al valor.
    /// </returns>
    public static IEnumerable<bool> ToBits(this in byte value) => ToBits(value, 8);

    /// <summary>
    /// Convierte un <see cref="int"/> en una colección de bits.
    /// </summary>
    /// <param name="value">
    /// Valor a convertir.
    /// </param>
    /// <returns>
    /// Una colección de los bits que componen al valor.
    /// </returns>
    public static IEnumerable<bool> ToBits(this in int value) => ToBits((ulong)value, 32);

    /// <summary>
    /// Convierte un <see cref="long"/> en una colección de bits.
    /// </summary>
    /// <param name="value">
    /// Valor a convertir.
    /// </param>
    /// <returns>
    /// Una colección de los bits que componen al valor.
    /// </returns>
    public static IEnumerable<bool> ToBits(this in long value) => ToBits((ulong)value, 64);

    /// <summary>
    /// Convierte un <see cref="sbyte"/> en una colección de bits.
    /// </summary>
    /// <param name="value">
    /// Valor a convertir.
    /// </param>
    /// <returns>
    /// Una colección de los bits que componen al valor.
    /// </returns>
    [CLSCompliant(false)]
    public static IEnumerable<bool> ToBits(this in sbyte value) => ToBits((ulong)value, 8);

    /// <summary>
    /// Convierte un <see cref="short"/> en una colección de bits.
    /// </summary>
    /// <param name="value">
    /// Valor a convertir.
    /// </param>
    /// <returns>
    /// Una colección de los bits que componen al valor.
    /// </returns>
    public static IEnumerable<bool> ToBits(this in short value) => ToBits((ulong)value, 16);

    /// <summary>
    /// Convierte un <see cref="uint"/> en una colección de bits.
    /// </summary>
    /// <param name="value">
    /// Valor a convertir.
    /// </param>
    /// <returns>
    /// Una colección de los bits que componen al valor.
    /// </returns>
    [CLSCompliant(false)]
    public static IEnumerable<bool> ToBits(this in uint value) => ToBits(value, 32);

    /// <summary>
    /// Convierte un <see cref="ulong"/> en una colección de bits.
    /// </summary>
    /// <param name="value">
    /// Valor a convertir.
    /// </param>
    /// <returns>
    /// Una colección de los bits que componen al valor.
    /// </returns>
    [CLSCompliant(false)]
    public static IEnumerable<bool> ToBits(this in ulong value) => ToBits(value, 64);

    /// <summary>
    /// Convierte un <see cref="ushort"/> en una colección de bits.
    /// </summary>
    /// <param name="value">
    /// Valor a convertir.
    /// </param>
    /// <returns>
    /// Una colección de los bits que componen al valor.
    /// </returns>
    [CLSCompliant(false)]
    public static IEnumerable<bool> ToBits(this in ushort value) => ToBits(value, 16);

    /// <summary>
    /// Atajo de <see cref="BitConverter.ToString(byte[])" /> que no
    /// incluye guiones.
    /// </summary>
    /// <returns>
    /// La representación hexadecimal del arreglo de <see cref="byte" />.
    /// </returns>
    /// <param name="arr">Arreglo de bytes a convertir.</param>
    /// <exception cref="ArgumentNullException">
    /// Se produce si <paramref name="arr"/> es <see langword="null"/>.
    /// </exception>
    [Sugar]
    public static string ToHex(this byte[] arr)
    {
        NullCheck(arr, nameof(arr));
        return BitConverter.ToString(arr).Replace("-", "");
    }

    /// <summary>
    /// Convierte un <see cref="byte" /> en su representación hexadecimal.
    /// </summary>
    /// <returns>
    /// La representación hexadecimal de <paramref name="byte" />.
    /// </returns>
    /// <param name="byte">El <see cref="byte" /> a convertir.</param>
    [Sugar]
    public static string ToHex(this in byte @byte)
    {
        return @byte.ToString("X");
    }
}
