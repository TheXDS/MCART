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
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using TheXDS.MCART.Annotations;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Math;
using TheXDS.MCART.Types;
using TheXDS.MCART.Types.Extensions;
using St = TheXDS.MCART.Resources.Strings;
using St2 = TheXDS.MCART.Resources.InternalStrings;

#region Configuración de ReSharper

// ReSharper disable IntroduceOptionalParameters.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable MemberCanBeProtected.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable UnusedMember.Global

#endregion

namespace TheXDS.MCART
{
    /// <summary>
    ///     Contiene operaciones comunes de transformación de datos en los
    ///     programas, y de algunas comparaciones especiales.
    /// </summary>
    /// <remarks>
    ///     Algunas de estas funciones también se implementan como extensiones, por
    ///     lo que para ser llamadas únicamente es necesario importar el espacio de
    ///     nombres <see cref="MCART" /> y utilizar sintaxis de instancia.
    /// </remarks>
    [SuppressMessage("ReSharper", "PartialTypeWithSinglePart")]
    public static partial class Common
    {        
        /// <summary>
        ///     Determina si un conjunto de cadenas están vacías.
        /// </summary>
        /// <returns>
        ///     <see langword="true" /> si las cadenas están vacías o son <see langword="null" />; de lo
        ///     contrario, <see langword="false" />.
        /// </returns>
        /// <param name="stringArray">Cadenas a comprobar.</param>
        [Thunk]
        public static bool AllEmpty(params string[] stringArray)
        {
            return stringArray.AsEnumerable().AllEmpty();
        }

        /// <summary>
        ///     Determina si un conjunto de cadenas están vacías.
        /// </summary>
        /// <returns>
        ///     <see langword="true" /> si las cadenas están vacías o son <see langword="null" />; de lo
        ///     contrario, <see langword="false" />.
        /// </returns>
        /// <param name="stringArray">Cadenas a comprobar.</param>
        public static bool AllEmpty(this IEnumerable<string> stringArray)
        {
            return stringArray.All(j => j.IsEmpty());
        }

        /// <summary>
        ///     Determina si alguna cadena está vacía.
        /// </summary>
        /// <returns>
        ///     <see langword="true" /> si alguna cadena está vacía o es <see langword="null" />; de lo
        ///     contrario, <see langword="false" />.
        /// </returns>
        /// <param name="stringArray">Cadenas a comprobar.</param>
        [Thunk]
        public static bool AnyEmpty(params string[] stringArray)
        {
            return stringArray.AsEnumerable().AnyEmpty();
        }

        /// <summary>
        ///     Determina si alguna cadena está vacía.
        /// </summary>
        /// <returns>
        ///     <see langword="true" /> si alguna cadena está vacía o es <see langword="null" />; de lo
        ///     contrario, <see langword="false" />.
        /// </returns>
        /// <param name="stringArray">Cadenas a comprobar.</param>
        [Thunk]
        public static bool AnyEmpty(this IEnumerable<string> stringArray)
        {
            return stringArray.Any(j => j.IsEmpty());
        }

        /// <summary>
        ///     Determina si alguna cadena está vacía.
        /// </summary>
        /// <returns>
        ///     <see langword="true" /> si alguna cadena está vacía o es <see langword="null" />; de lo
        ///     contrario, <see langword="false" />.
        /// </returns>
        /// <param name="stringArray">Cadenas a comprobar.</param>
        /// <param name="index">
        ///     Argumento de salida. Índices de las cadenas vacías encontradas.
        /// </param>
        public static bool AnyEmpty(out IEnumerable<int> index, params string[] stringArray)
        {
            return stringArray.AsEnumerable().AnyEmpty(out index);
        }

        /// <summary>
        ///     Determina si alguna cadena está vacía.
        /// </summary>
        /// <returns>
        ///     <see langword="true" /> si alguna cadena está vacía o es <see langword="null" />; de lo
        ///     contrario, <see langword="false" />.
        /// </returns>
        /// <param name="stringArray">Cadenas a comprobar.</param>
        /// <param name="index">
        ///     Argumento de salida. Índices de las cadenas vacías encontradas.
        /// </param>
        public static bool AnyEmpty(this IEnumerable<string> stringArray, out IEnumerable<int> index)
        {
            var idx = new System.Collections.Generic.List<int>();
            var c = 0;
            var found = false;
            foreach (var j in stringArray)
            {
                if (j.IsEmpty())
                {
                    found = true;
                    idx.Add(c);
                }

                c++;
            }

            index = idx.AsEnumerable();
            return found;
        }

        /// <summary>
        ///     Determina si alguna cadena está vacía.
        /// </summary>
        /// <returns>
        ///     <see langword="true" /> si alguna cadena está vacía o es <see langword="null" />; de lo
        ///     contrario, <see langword="false" />.
        /// </returns>
        /// <param name="stringArray">Cadenas a comprobar.</param>
        /// <param name="firstIndex">
        ///     Argumento de salida. Índice de la primera cadena vacía encontrada.
        /// </param>
        public static bool AnyEmpty(this IEnumerable<string> stringArray, out int firstIndex)
        {
            var r = AnyEmpty(stringArray, out IEnumerable<int> indexes);
            var a = indexes.ToArray();
            firstIndex = a.Any() ? a.First() : -1;
            return r;
        }

        /// <summary>
        ///     Busca y obtiene un <see cref="TypeConverter" /> apropiado para
        ///     realizar la conversión entre <see cref="string" /> y el tipo
        ///     especificado.
        /// </summary>
        /// <param name="target">Tipo de datos de destino.</param>
        /// <returns>
        ///     Un <see cref="TypeConverter" /> capaz de realizar la conversión
        ///     entre <see cref="string" /> y el tipo especificado, o
        ///     <see langword="null" /> si no se ha encontrado un convertidor
        ///     adecuado.
        /// </returns>
        public static TypeConverter FindConverter(Type target)
        {
            return FindConverter(typeof(string), target);
        }

        /// <summary>
        ///     Busca y obtiene un <see cref="TypeConverter" /> apropiado para
        ///     realizar la conversión entre <see cref="string" /> y el tipo
        ///     especificado.
        /// </summary>
        /// <typeparam name="T">Tipo de datos de destino.</typeparam>
        /// <returns>
        ///     Un <see cref="TypeConverter" /> capaz de realizar la conversión
        ///     entre <see cref="string" /> y el tipo especificado, o
        ///     <see langword="null" /> si no se ha encontrado un convertidor
        ///     adecuado.
        /// </returns>
        public static TypeConverter FindConverter<T>()
        {
            return FindConverter(typeof(T));
        }

        /// <summary>
        ///     Busca y obtiene un <see cref="TypeConverter" /> apropiado para
        ///     realizar la conversión entre tipos solicitada.
        /// </summary>
        /// <typeparam name="TSource">Tipo de datos de origen.</typeparam>
        /// <typeparam name="TTarget">Tipo de datos de destino.</typeparam>
        /// <returns>
        ///     Un <see cref="TypeConverter" /> capaz de realizar la conversión
        ///     entre los tipos requeridos, o <see langword="null" /> si no se
        ///     ha encontrado un convertidor adecuado.
        /// </returns>
        public static TypeConverter FindConverter<TSource, TTarget>()
        {
            return FindConverter(typeof(TSource), typeof(TTarget));
        }

        /// <summary>
        ///     Busca y obtiene un <see cref="TypeConverter" /> apropiado para
        ///     realizar la conversión entre tipos solicitada.
        /// </summary>
        /// <param name="source">Tipo de datos de origen.</param>
        /// <param name="target">Tipo de datos de destino.</param>
        /// <returns>
        ///     Un <see cref="TypeConverter" /> capaz de realizar la conversión
        ///     entre los tipos requeridos, o <see langword="null" /> si no se
        ///     ha encontrado un convertidor adecuado.
        /// </returns>
        public static TypeConverter FindConverter(Type source, Type target)
        {
            try
            {
                return Objects.PublicTypes<TypeConverter>()
                    .Where(TypeExtensions.IsInstantiable)
                    .Select(j => j.New<TypeConverter>(false, new object[0]))
                    .FirstOrDefault(t => !(t is null) && t.CanConvertFrom(source) && t.CanConvertTo(target));
            }
            finally { GC.Collect(); }
        }

        /// <summary>
        ///     Invierte el Endianess de un valor <see cref="short" />.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Un <see cref="short" /> cuyo Endianess ha sido invertido.</returns>
        public static short FlipEndianess(this in short value)
        {
            return BitConverter.ToInt16(BitConverter.GetBytes(value).Reverse().ToArray(), 0);
        }

        /// <summary>
        ///     Invierte el Endianess de un valor <see cref="int" />.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Un <see cref="int" /> cuyo Endianess ha sido invertido.</returns>
        public static int FlipEndianess(this in int value)
        {
            return BitConverter.ToInt32(BitConverter.GetBytes(value).Reverse().ToArray(), 0);
        }

        /// <summary>
        ///     Invierte el Endianess de un valor <see cref="long" />.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Un <see cref="long" /> cuyo Endianess ha sido invertido.</returns>
        public static long FlipEndianess(this in long value)
        {
            return BitConverter.ToInt64(BitConverter.GetBytes(value).Reverse().ToArray(), 0);
        }

        /// <summary>
        ///     Invierte el Endianess de un valor <see cref="char" />.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Un <see cref="char" /> cuyo Endianess ha sido invertido.</returns>
        public static char FlipEndianess(this in char value)
        {
            return BitConverter.ToChar(BitConverter.GetBytes(value).Reverse().ToArray(), 0);
        }

        /// <summary>
        ///     Invierte el Endianess de un valor <see cref="float" />.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Un <see cref="float" /> cuyo Endianess ha sido invertido.</returns>
        public static float FlipEndianess(this in float value)
        {
            return BitConverter.ToSingle(BitConverter.GetBytes(value).Reverse().ToArray(), 0);
        }

        /// <summary>
        ///     Invierte el Endianess de un valor <see cref="double" />.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Un <see cref="double" /> cuyo Endianess ha sido invertido.</returns>
        public static double FlipEndianess(this in double value)
        {
            return BitConverter.ToDouble(BitConverter.GetBytes(value).Reverse().ToArray(), 0);
        }

        /// <summary>
        ///     Comprueba que el valor se encuentre en el rango especificado.
        /// </summary>
        /// <returns>
        ///     <see langword="true" /> si el valor se encuentra entre los
        ///     especificados; de lo contrario, <see langword="false" />.
        /// </returns>
        /// <param name="value">Valor a comprobar.</param>
        /// <param name="min">Mínimo del rango de valores, inclusive.</param>
        /// <param name="max">Máximo del rango de valores, inclusive.</param>
        /// <typeparam name="T">Tipo de objeto a comprobar.</typeparam>
        public static bool IsBetween<T>(this T value, T min, T max) where T : IComparable<T>
        {
            return IsBetween(value, min, max, true);
        }

        /// <summary>
        ///     Comprueba que el valor se encuentre en el rango especificado.
        /// </summary>
        /// <returns>
        ///     <see langword="true" /> si el valor se encuentra entre los
        ///     especificados; de lo contrario, <see langword="false" />.
        /// </returns>
        /// <param name="value">Valor a comprobar.</param>
        /// <param name="min">Mínimo del rango de valores.</param>
        /// <param name="max">Máximo del rango de valores.</param>
        /// <param name="inclusive">Inclusividad. de forma predeterminada, la comprobación es inclusive.</param>
        /// <typeparam name="T">Tipo de objeto a comprobar.</typeparam>
        public static bool IsBetween<T>(this T value, T min, T max, in bool inclusive) where T : IComparable<T>
        {
            return IsBetween(value, min, max, inclusive, inclusive);
        }

        /// <summary>
        ///     Comprueba que el valor se encuentre en el rango especificado.
        /// </summary>
        /// <returns>
        ///     <see langword="true" /> si el valor se encuentra entre los
        ///     especificados; de lo contrario, <see langword="false" />.
        /// </returns>
        /// <param name="value">Valor a comprobar.</param>
        /// <param name="min">Mínimo del rango de valores.</param>
        /// <param name="max">Máximo del rango de valores.</param>
        /// <param name="minInclusive">Inclusividad del valor mínimo. de forma predeterminada, la comprobación es inclusive.</param>
        /// <param name="maxInclusive">Inclusividad del valor máximo. de forma predeterminada, la comprobación es inclusive.</param>
        /// <typeparam name="T">Tipo de objeto a comprobar.</typeparam>
        public static bool IsBetween<T>(this T value, T min, T max, in bool minInclusive, in bool maxInclusive) where T : IComparable<T>
        {
            return (minInclusive ? value.CompareTo(min) >= 0 : value.CompareTo(min) > 0) 
                && (maxInclusive ? value.CompareTo(max) <= 0 : value.CompareTo(max) < 0);
        }

        /// <summary>
        ///     Comprueba que el valor se encuentre en el rango especificado.
        /// </summary>
        /// <typeparam name="T">Tipo de objeto a comprobar.</typeparam>
        /// <param name="value">Valor a comprobar.</param>
        /// <param name="range">Rango de valores inclusivos a comprobar.</param>
        /// <returns>
        ///     <see langword="true" /> si el valor se encuentra entre los
        ///     especificados; de lo contrario, <see langword="false" />.
        /// </returns>
        public static bool IsBetween<T>(this T value, in Range<T> range) where T : IComparable<T>
        {
            return range.IsWithin(value);
        }

        /// <summary>
        ///     Comprueba que el valor se encuentre en el rango especificado.
        /// </summary>
        /// <returns>
        ///     <see langword="true" /> si el valor se encuentra entre los
        ///     especificados; de lo contrario, <see langword="false" />.
        /// </returns>
        /// <param name="value">Valor a comprobar.</param>
        /// <param name="min">Mínimo del rango de valores, inclusive.</param>
        /// <param name="max">Máximo del rango de valores, inclusive.</param>
        /// <typeparam name="T">Tipo de objeto a comprobar.</typeparam>
        public static bool IsBetween<T>([CanBeNull] this T? value, in T min, in T max) where T : struct, IComparable<T>
        {
            return value.IsBetween(min, max, true, true);
        }

        /// <summary>
        ///     Comprueba que el valor se encuentre en el rango especificado.
        /// </summary>
        /// <returns>
        ///     <see langword="true" /> si el valor se encuentra entre los
        ///     especificados; de lo contrario, <see langword="false" />.
        /// </returns>
        /// <param name="value">Valor a comprobar.</param>
        /// <param name="min">Mínimo del rango de valores.</param>
        /// <param name="max">Máximo del rango de valores.</param>
        /// <param name="inclusive">Inclusividad. de forma predeterminada, la comprobación es inclusive.</param>
        /// <typeparam name="T">Tipo de objeto a comprobar.</typeparam>
        public static bool IsBetween<T>([CanBeNull] this T? value, in T min, in T max, in bool inclusive) where T : struct, IComparable<T>
        {
            return IsBetween(value, min, max, inclusive, inclusive);
        }

        /// <summary>
        ///     Comprueba que el valor se encuentre en el rango especificado.
        /// </summary>
        /// <returns>
        ///     <see langword="true" /> si el valor se encuentra entre los
        ///     especificados; de lo contrario, <see langword="false" />.
        /// </returns>
        /// <param name="value">Valor a comprobar.</param>
        /// <param name="min">Mínimo del rango de valores.</param>
        /// <param name="max">Máximo del rango de valores.</param>
        /// <param name="minInclusive">Inclusividad del valor mínimo. de forma predeterminada, la comprobación es inclusive.</param>
        /// <param name="maxInclusive">Inclusividad del valor máximo. de forma predeterminada, la comprobación es inclusive.</param>
        /// <typeparam name="T">Tipo de objeto a comprobar.</typeparam>
        public static bool IsBetween<T>([CanBeNull] this T? value, in T min, in T max, in bool minInclusive, in bool maxInclusive) where T : struct, IComparable<T>
        {
            if (!value.HasValue) return false;
            var v = value.Value;
            return (minInclusive ? v.CompareTo(min) >= 0 : v.CompareTo(min) > 0)
                   && (maxInclusive ? v.CompareTo(max) <= 0 : v.CompareTo(max) < 0);
        }

        /// <summary>
        ///     Comprueba que el valor se encuentre en el rango especificado.
        /// </summary>
        /// <typeparam name="T">Tipo de objeto a comprobar.</typeparam>
        /// <param name="value">Valor a comprobar.</param>
        /// <param name="range">Rango de valores inclusivos a comprobar.</param>
        /// <returns>
        ///     <see langword="true" /> si el valor se encuentra entre los
        ///     especificados; de lo contrario, <see langword="false" />.
        /// </returns>
        public static bool IsBetween<T>([CanBeNull] this T? value, in Range<T> range) where T : struct, IComparable<T>
        {
            return value.HasValue && range.IsWithin(value.Value);
        }

        /// <summary>
        ///     Condensa una lista en una <see cref="string" />
        /// </summary>
        /// <returns>
        ///     Una cadena en formato de lista cuyos miembros están separados por
        ///     el separador de línea predeterminado del sistema.
        /// </returns>
        /// <param name="collection">
        ///     Lista a condensar. Sus elementos deben ser del
        ///     tipo <see cref="string" />.
        /// </param>
        public static string Listed(this IEnumerable<string> collection)
        {
#if RatherDRY
            return string.Join(Environment.NewLine, collection);
#else
            var a = new StringBuilder();
            foreach (var j in collection) a.AppendLine(j);
            return a.ToString();
#endif
        }

        /// <summary>
        ///     Genera una secuencia de números en el rango especificado.
        /// </summary>
        /// <returns>
        ///     Una lista de enteros con la secuencia generada.
        /// </returns>
        /// <param name="top">Valor más alto.</param>
        [Thunk]
        public static IEnumerable<int> Sequence(in int top)
        {
            return Sequence(0, top, 1);
        }

        /// <summary>
        ///     Genera una secuencia de números en el rango especificado.
        /// </summary>
        /// <returns>
        ///     Una lista de enteros con la secuencia generada.
        /// </returns>
        /// <param name="floor">Valor más bajo.</param>
        /// <param name="top">Valor más alto.</param>
        [Thunk]
        public static IEnumerable<int> Sequence(in int floor, in int top)
        {
            return Sequence(floor, top, 1);
        }

        /// <summary>
        ///     Genera una secuencia de números en el rango especificado.
        /// </summary>
        /// <returns>
        ///     Una lista de enteros con la secuencia generada.
        /// </returns>
        /// <param name="floor">Valor más bajo.</param>
        /// <param name="top">Valor más alto.</param>
        /// <param name="stepping">Saltos del secuenciador.</param>
        public static IEnumerable<int> Sequence(int floor, int top, int stepping)
        {
            if (floor > top) stepping *= -1;
            for (var b = floor; stepping > 0 ? b <= top : b >= top; b += stepping)
                yield return b;
        }
        
        /// <summary>
        ///     Intercambia el valor de los objetos especificados.
        /// </summary>
        /// <param name="a">Objeto A.</param>
        /// <param name="b">Objeto B.</param>
        /// <typeparam name="T">
        ///     Tipo de los argumentos. Puede omitirse con
        ///     seguridad.
        /// </typeparam>
        public static void Swap<T>(ref T a, ref T b)
        {
            var c = a;
            a = b;
            b = c;
        }

        /// <summary>
        ///     <see cref="ThunkAttribute" /> de
        ///     <see cref="BitConverter.ToString(byte[])" /> que no incluye guiones.
        /// </summary>
        /// <returns>
        ///     La representación hexadecimal del arreglo de <see cref="byte" />.
        /// </returns>
        /// <param name="arr">Arreglo de bytes a convertir.</param>
        [Thunk]
        public static string ToHex(this byte[] arr)
        {
            return BitConverter.ToString(arr).Replace("-", "");
        }

        /// <summary>
        ///     Convierte un <see cref="byte" /> en su representación hexadecimal.
        /// </summary>
        /// <returns>
        ///     La representación hexadecimal de <paramref name="byte" />.
        /// </returns>
        /// <param name="byte">El <see cref="byte" /> a convertir.</param>
        [Thunk]
        public static string ToHex(this in byte @byte)
        {
            return @byte.ToString("X");
        }

        /// <summary>
        ///     Convierte los valores de una colección de elementos
        ///     <see cref="float" /> a porcentajes.
        /// </summary>
        /// <returns>
        ///     Una colección de <see cref="float" /> con sus valores
        ///     expresados en porcentaje.
        /// </returns>
        /// <param name="collection">Colección a procesar.</param>
        [Thunk]
        public static IEnumerable<float> ToPercent(this IEnumerable<float> collection)
        {
            var enumerable = collection.ToList();
            return ToPercent(enumerable, enumerable.Min(), enumerable.Max());
        }

        /// <summary>
        ///     Convierte los valores de una colección de elementos
        ///     <see cref="float" /> a porcentajes.
        /// </summary>
        /// <returns>
        ///     Una colección de <see cref="float" /> con sus valores
        ///     expresados en porcentaje.
        /// </returns>
        /// <param name="collection">Colección a procesar.</param>
        /// <param name="baseZero">
        ///     Si es <see langword="true" />, la base de
        ///     porcentaje es cero; de lo contrario, se utilizará el valor mínimo
        ///     dentro de la colección.
        /// </param>
        [Thunk]
        public static IEnumerable<float> ToPercent(this IEnumerable<float> collection, in bool baseZero)
        {
            var enumerable = collection.ToList();
            return ToPercent(enumerable, baseZero ? 0 : enumerable.Min(), enumerable.Max());
        }

        /// <summary>
        ///     Convierte los valores de una colección de elementos
        ///     <see cref="float" /> a porcentajes.
        /// </summary>
        /// <returns>
        ///     Una colección de <see cref="float" /> con sus valores
        ///     expresados en porcentaje.
        /// </returns>
        /// <param name="collection">Colección a procesar.</param>
        /// <param name="max">Valor que representará 100%.</param>
        [Thunk]
        public static IEnumerable<float> ToPercent(this IEnumerable<float> collection, in float max)
        {
            return ToPercent(collection, 0, max);
        }

        /// <summary>
        ///     Convierte los valores de una colección de elementos
        ///     <see cref="float" /> a porcentajes.
        /// </summary>
        /// <returns>
        ///     Una colección de <see cref="float" /> con sus valores
        ///     expresados en porcentaje.
        /// </returns>
        /// <param name="collection">Colección a procesar.</param>
        /// <param name="min">Valor que representará 0%.</param>
        /// <param name="max">Valor que representará 100%.</param>
        public static IEnumerable<float> ToPercent(this IEnumerable<float> collection, float min, float max)
        {
            if (!min.IsValid())
                throw new ArgumentException(
                    St.XIsInvalid(St.XYQuotes(St.TheValue, min.ToString(CultureInfo.CurrentCulture))), nameof(min));
            if (!max.IsValid())
                throw new ArgumentException(
                    St.XIsInvalid(St.XYQuotes(St.TheValue, max.ToString(CultureInfo.CurrentCulture))), nameof(max));
            foreach (var j in collection)
                if (j.IsValid())
                    yield return (j - min) / (max - min).Clamp(1, float.NaN);
                else
                    yield return float.NaN;
        }

        /// <summary>
        ///     Convierte los valores de una colección de elementos
        ///     <see cref="double" /> a porcentajes.
        /// </summary>
        /// <returns>
        ///     Una colección de <see cref="double" /> con sus valores
        ///     expresados en porcentaje.
        /// </returns>
        /// <param name="collection">Colección a procesar.</param>
        [Thunk]
        public static IEnumerable<double> ToPercent(this IEnumerable<double> collection)
        {
            var enumerable = collection.ToList();
            return ToPercent(enumerable, enumerable.Min(), enumerable.Max());
        }

        /// <summary>
        ///     Convierte los valores de una colección de elementos
        ///     <see cref="double" /> a porcentajes.
        /// </summary>
        /// <returns>
        ///     Una colección de <see cref="double" /> con sus valores
        ///     expresados en porcentaje.
        /// </returns>
        /// <param name="collection">Colección a procesar.</param>
        /// <param name="baseZero">
        ///     Si es <see langword="true" />, la base de
        ///     porcentaje es cero; de lo contrario, se utilizará el valor mínimo
        ///     dentro de la colección.
        /// </param>
        [Thunk]
        public static IEnumerable<double> ToPercent(this IEnumerable<double> collection, in bool baseZero)
        {
            var enumerable = collection.ToList();
            return ToPercent(enumerable, baseZero ? 0 : enumerable.Min(), enumerable.Max());
        }

        /// <summary>
        ///     Convierte los valores de una colección de elementos
        ///     <see cref="double" /> a porcentajes.
        /// </summary>
        /// <returns>
        ///     Una colección de <see cref="double" /> con sus valores
        ///     expresados en porcentaje.
        /// </returns>
        /// <param name="collection">Colección a procesar.</param>
        /// <param name="max">Valor que representará 100%.</param>
        [Thunk]
        public static IEnumerable<double> ToPercent(this IEnumerable<double> collection, in double max)
        {
            return ToPercent(collection, 0, max);
        }

        /// <summary>
        ///     Convierte los valores de una colección de elementos
        ///     <see cref="double" /> a porcentajes.
        /// </summary>
        /// <returns>
        ///     Una colección de <see cref="double" /> con sus valores
        ///     expresados en porcentaje.
        /// </returns>
        /// <param name="collection">Colección a procesar.</param>
        /// <param name="min">Valor que representará 0%.</param>
        /// <param name="max">Valor que representará 100%.</param>
        public static IEnumerable<double> ToPercent(this IEnumerable<double> collection, double min, double max)
        {
            if (!min.IsValid())
                throw new ArgumentException(
                    St.XIsInvalid(St.XYQuotes(St.TheValue, min.ToString(CultureInfo.CurrentCulture))), nameof(min));
            if (!max.IsValid())
                throw new ArgumentException(
                    St.XIsInvalid(St.XYQuotes(St.TheValue, max.ToString(CultureInfo.CurrentCulture))), nameof(max));
            foreach (var j in collection)
                if (j.IsValid())
                    yield return (j - min) / (max - min).Clamp(1, double.NaN);
                else
                    yield return double.NaN;
        }

        /// <summary>
        ///     Convierte los valores de una colección de elementos
        ///     <see cref="int" /> a porcentajes.
        /// </summary>
        /// <returns>
        ///     Una colección de <see cref="double" /> con sus valores
        ///     expresados en porcentaje.
        /// </returns>
        /// <param name="collection">Colección a procesar.</param>
        [Thunk]
        public static IEnumerable<double> ToPercentDouble(this IEnumerable<int> collection)
        {
            var enumerable = collection.ToList();
            return ToPercentDouble(enumerable, 0, enumerable.Max());
        }

        /// <summary>
        ///     Convierte los valores de una colección de elementos
        ///     <see cref="int" /> a porcentajes.
        /// </summary>
        /// <returns>
        ///     Una colección de <see cref="double" /> con sus valores
        ///     expresados en porcentaje.
        /// </returns>
        /// <param name="collection">Colección a procesar.</param>
        /// <param name="baseZero">
        ///     Opcional. si es <see langword="true" />, la base de
        ///     porcentaje es cero; de lo contrario, se utilizará el valor mínimo
        ///     dentro de la colección.
        /// </param>
        [Thunk]
        public static IEnumerable<double> ToPercentDouble(this IEnumerable<int> collection, in bool baseZero)
        {
            var enumerable = collection.ToList();
            return ToPercentDouble(enumerable, baseZero ? 0 : enumerable.Min(), enumerable.Max());
        }

        /// <summary>
        ///     Convierte los valores de una colección de elementos
        ///     <see cref="int" /> a porcentajes.
        /// </summary>
        /// <returns>
        ///     Una colección de <see cref="double" /> con sus valores
        ///     expresados en porcentaje.
        /// </returns>
        /// <param name="collection">Colección a procesar.</param>
        /// <param name="max">Valor que representará 100%.</param>
        [Thunk]
        public static IEnumerable<double> ToPercentDouble(this IEnumerable<int> collection, in int max)
        {
            return ToPercentDouble(collection, 0, max);
        }

        /// <summary>
        ///     Convierte los valores de una colección de elementos
        ///     <see cref="int" /> a porcentajes.
        /// </summary>
        /// <returns>
        ///     Una colección de <see cref="double" /> con sus valores
        ///     expresados en porcentaje.
        /// </returns>
        /// <param name="collection">Colección a procesar.</param>
        /// <param name="min">Valor que representará 0%.</param>
        /// <param name="max">Valor que representará 100%.</param>
        public static IEnumerable<double> ToPercentDouble(this IEnumerable<int> collection, int min, int max)
        {
            if (min == max) throw new InvalidOperationException();
            foreach (var j in collection) yield return (j - min) / (double) (max - min);
        }

        /// <summary>
        ///     Convierte los valores de una colección de elementos
        ///     <see cref="int" /> a porcentajes de precisión simple.
        /// </summary>
        /// <returns>
        ///     Una colección de <see cref="float" /> con sus valores
        ///     expresados en porcentaje.
        /// </returns>
        /// <param name="collection">Colección a procesar.</param>
        [Thunk]
        public static IEnumerable<float> ToPercentSingle(this IEnumerable<int> collection)
        {
            var enumerable = collection.ToList();
            return ToPercentSingle(enumerable, 0, enumerable.Max());
        }

        /// <summary>
        ///     Convierte los valores de una colección de elementos
        ///     <see cref="int" /> a porcentajes de precisión simple.
        /// </summary>
        /// <returns>
        ///     Una colección de <see cref="float" /> con sus valores
        ///     expresados en porcentaje.
        /// </returns>
        /// <param name="collection">Colección a procesar.</param>
        /// <param name="baseZero">
        ///     Opcional. si es <see langword="true" />, la base de
        ///     porcentaje es cero; de lo contrario, se utilizará el valor mínimo
        ///     dentro de la colección.
        /// </param>
        [Thunk]
        public static IEnumerable<float> ToPercentSingle(this IEnumerable<int> collection, in bool baseZero)
        {
            var enumerable = collection.ToList();
            return ToPercentSingle(enumerable, baseZero ? 0 : enumerable.Min(), enumerable.Max());
        }

        /// <summary>
        ///     Convierte los valores de una colección de elementos
        ///     <see cref="int" /> a porcentajes de precisión simple.
        /// </summary>
        /// <returns>
        ///     Una colección de <see cref="float" /> con sus valores
        ///     expresados en porcentaje.
        /// </returns>
        /// <param name="collection">Colección a procesar.</param>
        /// <param name="max">Valor que representará 100%.</param>
        [Thunk]
        public static IEnumerable<float> ToPercentSingle(this IEnumerable<int> collection, in int max)
        {
            return ToPercentSingle(collection, 0, max);
        }

        /// <summary>
        ///     Convierte los valores de una colección de elementos
        ///     <see cref="int" /> a porcentajes de precisión simple.
        /// </summary>
        /// <returns>
        ///     Una colección de <see cref="float" /> con sus valores
        ///     expresados en porcentaje.
        /// </returns>
        /// <param name="collection">Colección a procesar.</param>
        /// <param name="min">Valor que representará 0%.</param>
        /// <param name="max">Valor que representará 100%.</param>
        public static IEnumerable<float> ToPercentSingle(this IEnumerable<int> collection, int min, int max)
        {
            if (min == max) throw new InvalidOperationException();
            foreach (var j in collection) yield return (j - min) / (float) (max - min);
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
        public static string ByteUnits(long bytes, in ByteUnitType unit)
        {
            var c = 0;
            var f = 0.0f;

            int mag;
            string[] u;

            switch (unit)
            {
                case ByteUnitType.Binary:
                    mag = 1024;
                    u = new[] {St2.KiB, St2.MiB, St2.GiB, St2.TiB, St2.PiB, St2.EiB, St2.ZiB, St2.YiB};
                    break;
                case ByteUnitType.Decimal:
                    mag = 1000;
                    u = new[] {St2.KB, St2.MB, St2.GB, St2.TB, St2.PB, St2.EB, St2.ZB, St2.YB};
                    break;
                default:

#if PreferExceptions
                    throw new ArgumentOutOfRangeException(nameof(unit), unit, null);
#else
                    return $"{bytes} {St2.Bytes}";
#endif
            }
            
            while (bytes > mag - 1)
            {
                c++;
                f = (int)(bytes % mag);
                bytes /= mag;
            }
			f /= mag;

            return c > 0 ? $"{bytes + f:F1} {u[c.Clamp(7)-1]}" : $"{bytes} {St2.Bytes}";
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
        ///     Enumera los tipos de unidades que se pueden utilizar para
        ///     representar grandes cantidades de bytes.
        /// </summary>
        public enum ByteUnitType : byte
        {
            /// <summary>
            ///     Numeración binaria. Cada orden de magnitud equivale a 1024 de su inferior.
            /// </summary>
            Binary,
            /// <summary>
            ///     Numeración decimal. Cada orden de magnitud equivale a 1000 de su inferior. 
            /// </summary>
            Decimal
        }

#pragma warning disable XS0001
        /* -= NOTA =-
		 * Oops! Algunas API de Mono parecen no estar completas, esta directiva
		 * deshabilita la advertencia al compilar desde MonoDevelop.
		 * 
		 * SecureString no provee de funcionalidad de encriptado en Mono, lo que
		 * podría ser inseguro.
         */
        /// <summary>
        ///     Convierte un <see cref="SecureString" /> en un
        ///     <see cref="string" />.
        /// </summary>
        /// <param name="value">
        ///     <see cref="SecureString" /> a convertir.
        /// </param>
        /// <returns>Un <see cref="string" /> de código administrado.</returns>
        /// <remarks>
        ///     El uso de este método NO ESTÁ RECOMENDADO, ya que la conversión al
        ///     tipo <see cref="string" /> vence el propósito original de
        ///     <see cref="SecureString" />, y se provee como una
        ///     alternativa sencilla, en casos en los que el programa no dependa de
        ///     que la confidencialidad de una cadena en particular se deba
        ///     mantener durante la ejecución.
        /// </remarks>
        [Dangerous]
        public static string Read(this SecureString value)
        {
            var valuePtr = IntPtr.Zero;
            try
            {
                valuePtr = Marshal.SecureStringToGlobalAllocUnicode(value);
                return Marshal.PtrToStringUni(valuePtr, value.Length);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
            }
        }

        /// <summary>
        ///     Convierte un <see cref="SecureString" /> en un
        ///     arreglo de <see cref="short" />.
        /// </summary>
        /// <param name="value">
        ///     <see cref="SecureString" /> a convertir.
        /// </param>
        /// <returns>
        ///     Un arreglo de <see cref="short" /> de código administrado.
        /// </returns>
        public static short[] ReadInt16(this SecureString value)
        {
            const int sz = sizeof(short);
            var outp = new System.Collections.Generic.List<short>();
            var valuePtr = IntPtr.Zero;
            try
            {
                valuePtr = Marshal.SecureStringToGlobalAllocUnicode(value);
                for (var i = 0; i < value.Length * sz; i += sz) outp.Add(Marshal.ReadInt16(valuePtr, i));
                return outp.ToArray();
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
            }
        }

        /// <summary>
        ///     Convierte un <see cref="SecureString" /> en un
        ///     arreglo de <see cref="char" />.
        /// </summary>
        /// <param name="value">
        ///     <see cref="SecureString" /> a convertir.
        /// </param>
        /// <returns>
        ///     Un arreglo de <see cref="char" /> de código administrado.
        /// </returns>
        public static char[] ReadChars(this SecureString value)
        {
            const int sz = sizeof(char);
            var outp = new System.Collections.Generic.List<char>();
            var valuePtr = IntPtr.Zero;
            try
            {
                valuePtr = Marshal.SecureStringToGlobalAllocUnicode(value);
                for (var i = 0; i < value.Length * sz; i += sz) outp.Add((char) Marshal.ReadInt16(valuePtr, i));
                return outp.ToArray();
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
            }
        }

        /// <summary>
        ///     Convierte un <see cref="SecureString" /> en un
        ///     arreglo de <see cref="byte" />.
        /// </summary>
        /// <param name="value">
        ///     <see cref="SecureString" /> a convertir.
        /// </param>
        /// <returns>
        ///     Un arreglo de <see cref="byte" /> de código administrado.
        /// </returns>
        public static byte[] ReadBytes(this SecureString value)
        {
            var outp = new System.Collections.Generic.List<byte>();
            var valuePtr = IntPtr.Zero;
            try
            {
                valuePtr = Marshal.SecureStringToGlobalAllocUnicode(value);
                for (var i = 0; i < value.Length * 2; i++) outp.Add(Marshal.ReadByte(valuePtr, i));
                return outp.ToArray();
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
            }
        }
#pragma warning restore XS0001
    }
}