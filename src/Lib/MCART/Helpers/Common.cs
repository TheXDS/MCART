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
Copyright © 2011 - 2025 César Andrés Morgan

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

using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Math;
using TheXDS.MCART.Misc;
using TheXDS.MCART.Resources;
using TheXDS.MCART.Types;
using TheXDS.MCART.Types.Extensions;
using St = TheXDS.MCART.Resources.Strings.Common;

namespace TheXDS.MCART.Helpers;

/// <summary>
/// Defines operations for common data transformation in programs, and some
/// special comparison functions.
/// </summary>
/// <remarks>
/// Some of these functions are also implemented as extensions, so to be
/// called they only need to import the namespace <see cref="MCART" /> and
/// use instance syntax.
/// </remarks>
public static partial class Common
{
    private static Dictionary<Type, Type>? KnownConverters;

    /// <summary>
    /// Determines if a set of strings are all empty.
    /// </summary>
    /// <returns>
    /// <see langword="true" /> if the strings are empty or
    /// <see langword="null" />; <see langword="false" /> otherwise.
    /// </returns>
    /// <param name="stringArray">Strings to check.</param>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="stringArray"/> is
    /// <see langword="null"/>.
    /// </exception>
    public static bool AllEmpty(params string?[] stringArray)
    {
        return stringArray.AsEnumerable().AllEmpty();
    }

    /// <summary>
    /// Determines if any of the strings are empty.
    /// </summary>
    /// <returns>
    /// <see langword="true" /> if any string is empty or
    /// <see langword="null" />; <see langword="false" /> otherwise.
    /// </returns>
    /// <param name="stringArray">Strings to check.</param>
    /// <param name="index">
    /// Output argument. Indexes of the empty strings found.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="stringArray"/> is
    /// <see langword="null"/>.
    /// </exception>
    public static bool AnyEmpty(out IEnumerable<int> index, params string?[] stringArray)
    {
        return stringArray.AsEnumerable().AnyEmpty(out index);
    }

    /// <summary>
    /// Determines if any of the strings are empty.
    /// </summary>
    /// <returns>
    /// <see langword="true" /> if any string is empty or
    /// <see langword="null" />; <see langword="false" /> otherwise.
    /// </returns>
    /// <param name="stringArray">Strings to check.</param>
    /// <param name="firstIndex">
    /// Output argument. Index of the first empty string found.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="stringArray"/> is
    /// <see langword="null"/>.
    /// </exception>
    public static bool AnyEmpty(out int firstIndex, params string?[] stringArray)
    {
        return stringArray.AsEnumerable().AnyEmpty(out firstIndex);
    }

    /// <summary>
    /// Determines if any of the strings are empty.
    /// </summary>
    /// <returns>
    /// <see langword="true" /> if any string is empty or
    /// <see langword="null" />; <see langword="false" /> otherwise.
    /// </returns>
    /// <param name="stringArray">Strings to check.</param>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="stringArray"/> is
    /// <see langword="null"/>.
    /// </exception>
    public static bool AnyEmpty(params string?[] stringArray)
    {
        return stringArray.AsEnumerable().Any(StringExtensions.IsEmpty);
    }

    /// <summary>
    /// Gets the bit count of the value.
    /// </summary>
    /// <param name="value">
    /// Value to process.
    /// </param>
    /// <returns>
    /// The number of bits set to 1 in the value.
    /// </returns>
    public static byte BitCount(this in byte value) => BitCount(value, 8);

    /// <summary>
    /// Gets the bit count of the value.
    /// </summary>
    /// <param name="value">
    /// Value to process.
    /// </param>
    /// <returns>
    /// The number of bits set to 1 in the value.
    /// </returns>
    public static byte BitCount(this in int value) => BitCount((ulong)value, 32);

    /// <summary>
    /// Gets the bit count of the value.
    /// </summary>
    /// <param name="value">
    /// Value to process.
    /// </param>
    /// <returns>
    /// The number of bits set to 1 in the value.
    /// </returns>
    public static byte BitCount(this in long value) => BitCount((ulong)value, 64);

    /// <summary>
    /// Gets the bit count of the value.
    /// </summary>
    /// <param name="value">
    /// Value to process.
    /// </param>
    /// <returns>
    /// The number of bits set to 1 in the value.
    /// </returns>
    [CLSCompliant(false)]
    public static byte BitCount(this in sbyte value) => BitCount((ulong)value, 8);

    /// <summary>
    /// Gets the bit count of the value.
    /// </summary>
    /// <param name="value">
    /// Value to process.
    /// </param>
    /// <returns>
    /// The number of bits set to 1 in the value.
    /// </returns>
    public static byte BitCount(this in short value) => BitCount((ulong)value, 16);

    /// <summary>
    /// Gets the bit count of the value.
    /// </summary>
    /// <param name="value">
    /// Value to process.
    /// </param>
    /// <returns>
    /// The number of bits set to 1 in the value.
    /// </returns>
    [CLSCompliant(false)]
    public static byte BitCount(this in uint value) => BitCount(value, 32);

    /// <summary>
    /// Gets the bit count of the value.
    /// </summary>
    /// <param name="value">
    /// Value to process.
    /// </param>
    /// <returns>
    /// The number of bits set to 1 in the value.
    /// </returns>
    [CLSCompliant(false)]
    public static byte BitCount(this in ulong value) => BitCount(value, 64);

    /// <summary>
    /// Gets the bit count of the value.
    /// </summary>
    /// <param name="value">
    /// Value to process.
    /// </param>
    /// <returns>
    /// The number of bits set to 1 in the value.
    /// </returns>
    [CLSCompliant(false)] 
    public static byte BitCount(this in ushort value) => BitCount(value, 16);

    /// <summary>
    /// Converts a <see cref="int"/> value representing a byte count to the
    /// easiest-to-read magnitude unit.
    /// </summary>
    /// <param name="bytes">Amount of bytes to represent.</param>
    /// <param name="unit">Type of units to use.</param>
    /// <param name="magnitude">
    /// Initial magnitude of bytes. <c>0</c> indicates that the value of
    /// <paramref name="bytes"/> should be treated directly as the value
    /// in bytes of the operation. The maximum allowed value is <c>8</c>
    /// to indicate Yottabytes.
    /// </param>
    /// <param name="format">String format to utilize.</param>
    /// <returns>
    /// A string with the byte count using the appropriate magnitude unit.
    /// </returns>
    public static string ByteUnits(in this int bytes, in ByteUnitType unit, byte magnitude, IFormatProvider? format = null)
    {
        return ByteUnits((long)bytes, unit, magnitude, format);
    }

    /// <summary>
    /// Converts a <see cref="int"/> value representing a byte count to the
    /// easiest-to-read magnitude unit.
    /// </summary>
    /// <param name="bytes">Amount of bytes to represent.</param>
    /// <param name="format">String format to utilize.</param>
    /// <returns>
    /// A string with the byte count using the appropriate magnitude unit.
    /// </returns>
    public static string ByteUnits(in this int bytes, IFormatProvider? format = null)
    {
        return ByteUnits((long)bytes, format);
    }

    /// <summary>
    /// Converts a <see cref="int"/> value representing a byte count to the
    /// easiest-to-read magnitude unit.
    /// </summary>
    /// <param name="bytes">Amount of bytes to represent.</param>
    /// <param name="unit">Type of units to use.</param>
    /// <param name="format">String format to utilize.</param>
    /// <returns>
    /// A string with the byte count using the appropriate magnitude unit.
    /// </returns>
    public static string ByteUnits(in this int bytes, in ByteUnitType unit, IFormatProvider? format = null)
    {
        return ByteUnits((long)bytes, unit, format);
    }

    /// <summary>
    /// Converts a <see cref="long"/> value representing a byte count to the
    /// easiest-to-read magnitude unit.
    /// </summary>
    /// <param name="bytes">Amount of bytes to represent.</param>
    /// <param name="unit">Type of units to use.</param>
    /// <param name="magnitude">
    /// Initial magnitude of bytes. <c>0</c> indicates that the value of
    /// <paramref name="bytes"/> should be treated directly as the value
    /// in bytes of the operation. The maximum allowed value is <c>8</c>
    /// to indicate Yottabytes.
    /// </param>
    /// <param name="format">String format to utilize.</param>
    /// <returns>
    /// A string with the byte count using the appropriate magnitude unit.
    /// </returns>
    public static string ByteUnits(in this long bytes, in ByteUnitType unit, byte magnitude, IFormatProvider? format = null)
    {
        ByteUnits_Contract(bytes, unit, magnitude);
        return ByteUnits((long)(bytes * System.Math.Pow(unit switch
        {
            ByteUnitType.Binary or ByteUnitType.BinaryLong => 1024,
            ByteUnitType.Decimal or ByteUnitType.DecimalLong => 1000,
            _ => throw Errors.UndefinedEnum(nameof(unit), unit)
        }, magnitude)), unit, format);
    }

    /// <summary>
    /// Converts a <see cref="long"/> value representing a byte count to the
    /// easiest-to-read magnitude unit.
    /// </summary>
    /// <param name="bytes">Amount of bytes to represent.</param>
    /// <param name="format">String format to utilize.</param>
    /// <returns>
    /// A string with the byte count using the appropriate magnitude unit.
    /// </returns>
    public static string ByteUnits(in this long bytes, IFormatProvider? format = null)
    {
        return ByteUnits(bytes, ByteUnitType.Binary, format);
    }

    /// <summary>
    /// Converts a <see cref="long"/> value representing a byte count to the
    /// easiest-to-read magnitude unit.
    /// </summary>
    /// <param name="bytes">Amount of bytes to represent.</param>
    /// <param name="unit">Type of units to use.</param>
    /// <param name="format">String format to utilize.</param>
    /// <returns>
    /// A string with the byte count using the appropriate magnitude unit.
    /// </returns>
    public static string ByteUnits(in this long bytes, in ByteUnitType unit, IFormatProvider? format = null)
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

        return c > 0 ? $"{(b + (b / mag)).ToString("0.0", format ?? CultureInfo.CurrentCulture)} {u[c.Clamp(u.Length) - 1]}" : $"{bytes} {St.Bytes}";
    }

    /// <summary>
    /// Searches and retrieves an appropriate <see cref="TypeConverter" /> to
    /// perform the conversion between the requested types.
    /// </summary>
    /// <param name="source">Source type.</param>
    /// <param name="target">Destination type.</param>
    /// <returns>
    /// A <see cref="TypeConverter" /> capable of performing the conversion
    /// between the required types, or <see langword="null" /> if no suitable
    /// converter has been found.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="source"/> or <paramref name="target"/>
    /// are <see langword="null"/>.
    /// </exception>
    [RequiresUnreferencedCode(AttributeErrorMessages.MethodScansForTypes)]
    [RequiresDynamicCode(AttributeErrorMessages.MethodCallsDynamicCode)]
    public static TypeConverter? FindConverter(Type source, Type target)
    {
        /* BUG:
         * ====
         * In .Net, primitive type converters tend to not filter
         * correctly the types they can convert, for example,
         * System.ComponentModel.ByteConverter reports that it can convert to
         * System.Double, despite the documentation forbidding it.
         */

        if (source == typeof(string) && (KnownConverters ??= new Dictionary<Type, Type>
        {
            { typeof(byte), typeof(ByteConverter) },
            { typeof(sbyte), typeof(SByteConverter) },
            { typeof(short), typeof(Int16Converter) },
            { typeof(ushort), typeof(UInt16Converter) },
            { typeof(int), typeof(Int32Converter) },
            { typeof(uint), typeof(UInt32Converter) },
            { typeof(long), typeof(Int64Converter) },
            { typeof(ulong), typeof(UInt64Converter) },
            { typeof(float), typeof(SingleConverter) },
            { typeof(double), typeof(DoubleConverter) },
            { typeof(decimal), typeof(DecimalConverter) },
        }).TryGetValue(target, out var cType))
        {
            return cType.New<TypeConverter>(false, Array.Empty<object>());
        }
        return FindConverters(source, target).FirstOrDefault();
    }

    /// <summary>
    /// Searches and retrieves an appropriate <see cref="TypeConverter" /> to
    /// perform the conversion between <see cref="string" /> and the specified
    /// type.
    /// </summary>
    /// <param name="target">Destination type.</param>
    /// <returns>
    /// A <see cref="TypeConverter" /> capable of performing the conversion
    /// between <see cref="string" /> and the specified type, or
    /// <see langword="null" /> if no suitable converter has been found.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="target"/> is <see langword="null"/>.
    /// </exception>
    [RequiresUnreferencedCode(AttributeErrorMessages.MethodScansForTypes)]
    [RequiresDynamicCode(AttributeErrorMessages.MethodCallsDynamicCode)]
    public static TypeConverter? FindConverter(Type target)
    {
        ArgumentNullException.ThrowIfNull(target);
        return FindConverter(typeof(string), target);
    }

    /// <summary>
    /// Searches and retrieves an appropriate <see cref="TypeConverter" /> to
    /// perform the conversion between <see cref="string" /> and the specified
    /// type.
    /// </summary>
    /// <typeparam name="T">Destiation type.</typeparam>
    /// <returns>
    /// A <see cref="TypeConverter" /> capable of performing the conversion
    /// between <see cref="string" /> and the specified type, or
    /// <see langword="null" /> if no suitable converter has been found.
    /// </returns>
    [RequiresUnreferencedCode(AttributeErrorMessages.MethodScansForTypes)]
    [RequiresDynamicCode(AttributeErrorMessages.MethodCallsDynamicCode)]
    public static TypeConverter? FindConverter<T>()
    {
        return FindConverter(typeof(T));
    }

    /// <summary>
    /// Searches and retrieves an appropriate <see cref="TypeConverter" /> to
    /// perform the conversion between the requested types.
    /// </summary>
    /// <typeparam name="TSource">Source type.</typeparam>
    /// <typeparam name="TTarget">Destination typt.</typeparam>
    /// <returns>
    /// A <see cref="TypeConverter" /> capable of performing the conversion
    /// between the required types, or <see langword="null" /> if no suitable
    /// converter has been found.
    /// </returns>
    [RequiresUnreferencedCode(AttributeErrorMessages.MethodScansForTypes)]
    [RequiresDynamicCode(AttributeErrorMessages.MethodCallsDynamicCode)]
    public static TypeConverter? FindConverter<TSource, TTarget>()
    {
        return FindConverter(typeof(TSource), typeof(TTarget));
    }

    /// <summary>
    /// Searches and retrieves an appropriate <see cref="TypeConverter" /> to
    /// perform the conversion between the requested types.
    /// </summary>
    /// <param name="source">Source type.</param>
    /// <param name="target">Destination type.</param>
    /// <returns>
    /// A <see cref="TypeConverter" /> capable of performing the conversion
    /// between the required types, or <see langword="null" /> if no suitable
    /// converter has been found.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="source"/> or <paramref name="target"/>
    /// are <see langword="null"/>.
    /// </exception>
    [RequiresUnreferencedCode(AttributeErrorMessages.MethodScansForTypes)]
    [RequiresDynamicCode(AttributeErrorMessages.MethodCallsDynamicCode)]
    public static IEnumerable<TypeConverter> FindConverters(Type source, Type target)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(target);
        try
        {
            return ReflectionHelpers.PublicTypes<TypeConverter>()
                .Where(TypeExtensions.IsInstantiable)
                .Select(j => j.New<TypeConverter>(false, Array.Empty<object>()))
                .NotNull()
                .Where(t => t.CanConvertFrom(source) && t.CanConvertTo(target));
        }
        finally { GC.Collect(); }
    }

    /// <summary>
    /// Inverts the Endianness of a <see cref="char" /> value.
    /// </summary>
    /// <param name="value">Value whose endianness will be inverted.</param>
    /// <returns>
    /// A <see cref="char" /> whose endianness has been inverted.
    /// </returns>
    public static char FlipEndianness(this in char value)
    {
        return BitConverter.ToChar(BitConverter.GetBytes(value).Reverse().ToArray(), 0);
    }

    /// <summary>
    /// Inverts the Endianness of a <see cref="double" /> value.
    /// </summary>
    /// <param name="value">Value whose endianness will be inverted.</param>
    /// <returns>
    /// A <see cref="double" /> whose endianness has been inverted.
    /// </returns>
    public static double FlipEndianness(this in double value)
    {
        return BitConverter.ToDouble(BitConverter.GetBytes(value).Reverse().ToArray(), 0);
    }

    /// <summary>
    /// Inverts the Endianness of a <see cref="float" /> value.
    /// </summary>
    /// <param name="value">Value whose endianness will be inverted.</param>
    /// <returns>
    /// A <see cref="float" /> whose endianness has been inverted.
    /// </returns>
    public static float FlipEndianness(this in float value)
    {
        return BitConverter.ToSingle(BitConverter.GetBytes(value).Reverse().ToArray(), 0);
    }

    /// <summary>
    /// Inverts the Endianness of a <see cref="int" /> value.
    /// </summary>
    /// <param name="value">Value whose endianness will be inverted.</param>
    /// <returns>
    /// An <see cref="int" /> whose endianness has been inverted.
    /// </returns>
    public static int FlipEndianness(this in int value)
    {
        return BitConverter.ToInt32(BitConverter.GetBytes(value).Reverse().ToArray(), 0);
    }

    /// <summary>
    /// Inverts the Endianness of a <see cref="long" /> value.
    /// </summary>
    /// <param name="value">Value whose endianness will be inverted.</param>
    /// <returns>
    /// A <see cref="long" /> whose endianness has been inverted.
    /// </returns>
    public static long FlipEndianness(this in long value)
    {
        return BitConverter.ToInt64(BitConverter.GetBytes(value).Reverse().ToArray(), 0);
    }

    /// <summary>
    /// Inverts the Endianness of a <see cref="short" /> value.
    /// </summary>
    /// <param name="value">Value whose endianness will be inverted.</param>
    /// <returns>
    /// A <see cref="short" /> whose endianness has been inverted.
    /// </returns>
    public static short FlipEndianness(this in short value)
    {
        return BitConverter.ToInt16(BitConverter.GetBytes(value).Reverse().ToArray(), 0);
    }

    /// <summary>
    /// Inverts the Endianness of a <see cref="uint" /> value.
    /// </summary>
    /// <param name="value">Value whose endianness will be inverted.</param>
    /// <returns>
    /// A <see cref="uint" /> whose endianness has been inverted.
    /// </returns>
    [CLSCompliant(false)]
    public static uint FlipEndianness(this in uint value)
    {
        return BitConverter.ToUInt32(BitConverter.GetBytes(value).Reverse().ToArray(), 0);
    }

    /// <summary>
    /// Inverts the Endianness of a <see cref="ulong" /> value.
    /// </summary>
    /// <param name="value">Value whose endianness will be inverted.</param>
    /// <returns>
    /// A <see cref="ulong" /> whose endianness has been inverted.
    /// </returns>
    [CLSCompliant(false)]
    public static ulong FlipEndianness(this in ulong value)
    {
        return BitConverter.ToUInt64(BitConverter.GetBytes(value).Reverse().ToArray(), 0);
    }

    /// <summary>
    /// Inverts the Endianness of a <see cref="ushort" /> value.
    /// </summary>
    /// <param name="value">Value whose endianness will be inverted.</param>
    /// <returns>
    /// A <see cref="ushort" /> whose endianness has been inverted.
    /// </returns>
    [CLSCompliant(false)]
    public static ushort FlipEndianness(this in ushort value)
    {
        return BitConverter.ToUInt16(BitConverter.GetBytes(value).Reverse().ToArray(), 0);
    }

    /// <summary>
    /// Executes an operation if a value is not <see langword="null" />.
    /// </summary>
    /// <typeparam name="T">Type of value to check.</typeparam>
    /// <param name="value">Value to check.</param>
    /// <param name="operation">
    /// Operation to execute if <paramref name="value" /> is not
    /// <see langword="null" />.
    /// </param>
    public static void IfNotNull<T>(this T? value, Action<T> operation) where T : class
    {
        ArgumentNullException.ThrowIfNull(operation);
        if (value is not null) operation(value);
    }

    /// <summary>
    /// Executes an operation if a value is not <see langword="null" />.
    /// </summary>
    /// <typeparam name="T">
    /// Type of value to check.
    /// </typeparam>
    /// <param name="value">Value to check.</param>
    /// <param name="operation">
    /// Operation to execute if <paramref name="value" /> is not
    /// <see langword="null" />.
    /// </param>
    public static void IfNotNull<T>(this T? value, Action<T> operation) where T : struct
    {
        ArgumentNullException.ThrowIfNull(operation);
        if (value is not null) operation(value.Value);
    }

    /// <summary>
    /// Checks that the value is within the specified range.
    /// </summary>
    /// <typeparam name="T">Type of value to check.</typeparam>
    /// <param name="value">Value to check.</param>
    /// <param name="range">Range of values to check.</param>
    /// <returns>
    /// <see langword="true" /> if the value is inside the specified range,
    /// <see langword="false" /> otherwise.
    /// </returns>
    public static bool IsBetween<T>(this T value, in Range<T> range) where T : IComparable<T>
    {
        return range.IsWithin(value);
    }

    /// <summary>
    /// Checks that the value is within the specified range.
    /// </summary>
    /// <typeparam name="T">Type of value to check.</typeparam>
    /// <param name="value">Value to check.</param>
    /// <param name="min">Minimum range value, inclusive.</param>
    /// <param name="max">Maximum range value, inclusive.</param>
    /// <returns>
    /// <see langword="true" /> if the value is inside the specified range,
    /// <see langword="false" /> otherwise.
    /// </returns>
    public static bool IsBetween<T>(this T value, in T min, in T max) where T : IComparable<T>
    {
        return IsBetween(value, min, max, true);
    }

    /// <summary>
    /// Checks that the value is within the specified range.
    /// </summary>
    /// <typeparam name="T">Type of value to check.</typeparam>
    /// <param name="value">Value to check.</param>
    /// <param name="min">Minimum range value.</param>
    /// <param name="max">Maximum range value.</param>
    /// <param name="inclusive">
    /// If set to <see langword="true"/>, the minimum and maximum values will be inclusive.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if the value is inside the specified range,
    /// <see langword="false" /> otherwise.
    /// </returns>
    public static bool IsBetween<T>(this T value, in T min, in T max, in bool inclusive) where T : IComparable<T>
    {
        return IsBetween(value, min, max, inclusive, inclusive);
    }

    /// <summary>
    /// Checks that the value is within the specified range.
    /// </summary>
    /// <typeparam name="T">Type of value to check.</typeparam>
    /// <param name="value">Value to check.</param>
    /// <param name="min">Minimum range value.</param>
    /// <param name="max">Maximum range value.</param>
    /// <param name="minInclusive">
    /// If set to <see langword="true"/>, the minimum value will be inclusive
    /// in the range.
    /// </param>
    /// <param name="maxInclusive">
    /// If set to <see langword="true"/>, the maximum value will be inclusive 
    /// in the range.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if the value is inside the specified range,
    /// <see langword="false" /> otherwise.
    /// </returns>
    public static bool IsBetween<T>(this T value, in T min, in T max, in bool minInclusive, in bool maxInclusive) where T : IComparable<T>
    {
        return (minInclusive ? value.CompareTo(min) >= 0 : value.CompareTo(min) > 0)
            && (maxInclusive ? value.CompareTo(max) <= 0 : value.CompareTo(max) < 0);
    }

    /// <summary>
    /// Checks that the value is within the specified range.
    /// </summary>
    /// <typeparam name="T">Type of value to check.</typeparam>
    /// <param name="value">Value to check.</param>
    /// <param name="range">Range of values to check.</param>
    /// <returns>
    /// <see langword="true" /> if the value is inside the specified range,
    /// <see langword="false" /> otherwise.
    /// </returns>
    public static bool IsBetween<T>(this T? value, in Range<T> range) where T : struct, IComparable<T>
    {
        return value.HasValue && range.IsWithin(value.Value);
    }

    /// <summary>
    /// Checks that the value is within the specified range.
    /// </summary>
    /// <typeparam name="T">Type of value to check.</typeparam>
    /// <param name="value">Value to check.</param>
    /// <param name="min">Minimum range value, inclusive.</param>
    /// <param name="max">Maximum range value, inclusive.</param>
    /// <returns>
    /// <see langword="true" /> if the value is inside the specified range,
    /// <see langword="false" /> otherwise.
    /// </returns>
    public static bool IsBetween<T>(this T? value, in T min, in T max) where T : struct, IComparable<T>
    {
        return value.IsBetween(min, max, true, true);
    }

    /// <summary>
    /// Checks that the value is within the specified range.
    /// </summary>
    /// <typeparam name="T">Type of value to check.</typeparam>
    /// <param name="value">Value to check.</param>
    /// <param name="min">Minimum range value.</param>
    /// <param name="max">Maximum range value.</param>
    /// <param name="inclusive">
    /// If set to <see langword="true"/>, the minimum and maximum values will be inclusive.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if the value is inside the specified range,
    /// <see langword="false" /> otherwise.
    /// </returns>
    public static bool IsBetween<T>(this T? value, in T min, in T max, in bool inclusive) where T : struct, IComparable<T>
    {
        return IsBetween(value, min, max, inclusive, inclusive);
    }

    /// <summary>
    /// Checks that the value is within the specified range.
    /// </summary>
    /// <typeparam name="T">Type of value to check.</typeparam>
    /// <param name="value">Value to check.</param>
    /// <param name="min">Minimum range value.</param>
    /// <param name="max">Maximum range value.</param>
    /// <param name="minInclusive">
    /// If set to <see langword="true"/>, the minimum value will be inclusive
    /// in the range.
    /// </param>
    /// <param name="maxInclusive">
    /// If set to <see langword="true"/>, the maximum value will be inclusive 
    /// in the range.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if the value is inside the specified range,
    /// <see langword="false" /> otherwise.
    /// </returns>
    public static bool IsBetween<T>(this T? value, in T min, in T max, in bool minInclusive, in bool maxInclusive) where T : struct, IComparable<T>
    {
        if (!value.HasValue) return false;
        T v = value.Value;
        return (minInclusive ? v.CompareTo(min) >= 0 : v.CompareTo(min) > 0)
               && (maxInclusive ? v.CompareTo(max) <= 0 : v.CompareTo(max) < 0);
    }

    /// <summary>
    /// Condenses a list into a string where each element is separated by a new
    ///  line.
    /// </summary>
    /// <returns>
    /// A string where each element of the collection is separated by the
    /// operating system's line separator string.
    /// </returns>
    /// <param name="collection">
    /// Collection of strings to be condensed.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="collection"/> is <see langword="null"/>.
    /// </exception>
    public static string Listed(this IEnumerable<string> collection)
    {
        ArgumentNullException.ThrowIfNull(collection, nameof(collection));
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
        ArgumentNullException.ThrowIfNull(arr, nameof(arr));
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
        return @byte.ToString("X2");
    }

    private static IEnumerable<bool> ToBits(this in ulong value, in byte maxBits)
    {
        bool[]? a = new bool[maxBits];
        for (int j = 0; j < maxBits; j++)
        {
            a[j] = (value & (ulong)System.Math.Pow(2, j)) != 0;
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
}
