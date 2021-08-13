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
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Math;
using TheXDS.MCART.Types;
using TheXDS.MCART.Types.Extensions;
using static TheXDS.MCART.Misc.Internals;
using St = TheXDS.MCART.Resources.Strings.Common;

namespace TheXDS.MCART.Helpers
{
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
            var a = new bool[maxBits];
            for (var j = 0; j < maxBits; j++)
            {
                a[j] = (value & (ulong)System.Math.Pow(2, j)) != 0;
            }
            return a;
        }
        private static byte BitCount(ulong value, in byte maxBits)
        {
            byte c = 0;
            byte f = 0;
            while (value != 0 || f++ == maxBits)
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
        /// <exception cref="ArgumentNullException">
        /// Se produce si <paramref name="stringArray"/> es 
        /// <see langword="null"/>.
        /// </exception>
        public static bool AnyEmpty(params string?[] stringArray)
        {
            return stringArray.AsEnumerable().AnyEmpty();
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
        public static TypeConverter? FindConverter(Type source, Type target)
        {
            return FindConverters(source, target).FirstOrDefault();
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
        public static byte BitCount(this in byte value) => BitCount(value, 8);

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
            var v = value.Value;
            return (minInclusive ? v.CompareTo(min) >= 0 : v.CompareTo(min) > 0)
                   && (maxInclusive ? v.CompareTo(max) <= 0 : v.CompareTo(max) < 0);
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
        /// <param name="floor">Valor más bajo.</param>
        /// <param name="top">Valor más alto.</param>
        /// <param name="stepping">Saltos del secuenciador.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Se produce si <paramref name="stepping"/> es igual a <c>0</c>.
        /// </exception>
        public static IEnumerable<int> Sequence(int floor, int top, int stepping)
        {
            Sequence_Contract(stepping);
            if (floor > top) stepping *= -1;
            for (var b = floor; stepping > 0 ? b <= top : b >= top; b += stepping)
                yield return b;
        }
        
        /// <summary>
        /// Intercambia el valor de los objetos especificados.
        /// </summary>
        /// <param name="a">Objeto A.</param>
        /// <param name="b">Objeto B.</param>
        /// <typeparam name="T">
        /// Tipo de los argumentos. Puede omitirse con
        /// seguridad.
        /// </typeparam>
        public static void Swap<T>(ref T a, ref T b)
        {
            var c = a;
            a = b;
            b = c;
        }

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
        public static string ByteUnits(in long bytes, in ByteUnitType unit)
        {
            var c = 0;
            var b = (double)bytes;

            (double mag, string[] u) = unit switch
            {
                ByteUnitType.Binary => (1024, new[] { St.KiB, St.MiB, St.GiB, St.TiB, St.PiB, St.EiB, St.ZiB, St.YiB }),
                ByteUnitType.Decimal => (1000, new[] { St.KB, St.MB, St.GB, St.TB, St.PB, St.EB, St.ZB, St.YB }),
                ByteUnitType.BinaryLong => (1024, new[] { St.KiBl, St.MiBl, St.GiBl, St.TiBl, St.PiBl, St.EiBl, St.ZiBl, St.YiBl }),
                ByteUnitType.DecimalLong => (1000, new[] { St.KBl, St.MBl, St.GBl, St.TBl, St.PBl, St.EBl, St.ZBl, St.YBl }),                
#if PreferExceptions
                _ => throw new ArgumentOutOfRangeException(nameof(unit), unit, null)
#else
                _ => (double.PositiveInfinity, Array.Empty<string>())
#endif
            };

            while (b > mag - 1 && c < u.Length)
            {
                c++;
                b /= mag;
            }

            return c > 0 ? $"{b + (b / mag):F1} {u[c.Clamp(u.Length) - 1]}" : $"{bytes} {St.Bytes}";
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
        /// Enumera los tipos de unidades que se pueden utilizar para
        /// representar grandes cantidades de bytes.
        /// </summary>
        public enum ByteUnitType : byte
        {
            /// <summary>
            /// Numeración binaria. Cada orden de magnitud equivale a 1024 de su inferior.
            /// </summary>
            Binary,
            /// <summary>
            /// Numeración decimal. Cada orden de magnitud equivale a 1000 de su inferior. 
            /// </summary>
            Decimal,
            /// <summary>
            /// Numeración binaria con nombre largo. Cada orden de magnitud equivale a 1024 de su inferior.
            /// </summary>
            BinaryLong,
            /// <summary>
            /// Numeración decimal con nombre largo. Cada orden de magnitud equivale a 1000 de su inferior. 
            /// </summary>
            DecimalLong
        }
    }
}