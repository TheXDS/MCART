/*
Common.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Este archivo contiene operaciones comunes de transformación de datos en los
programas, y de algunas comparaciones especiales.

Algunas de estas funciones también se implementan como extensiones, por lo que
para ser llamadas únicamente es necesario importar el espacio de nombres
"TheXDS.MCART" y utilizar sintáxis de instancia.

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
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Math;
using TheXDS.MCART.Types;
using St = TheXDS.MCART.Resources.Strings;
using static TheXDS.MCART.Types.Extensions.TypeExtensions;

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
        ///     Describe las opciones de búsqueda para el método
        ///     <see cref="Common.TokenSearch"/>
        /// </summary>
        [Flags]
        public enum SearchOptions
        {
            /// <summary>
            ///     Modo de búsqueda predeterminado. Se ignorará el Casing y la
            ///     cadena coincidirá con los términos de búsqueda si contiene al
            ///     menos uno de los tokens. 
            /// </summary>
            Default,
            /// <summary>
            ///     Modo sensible al Casing. La cadena coincidirá con los términos
            ///     de búsqueda si contiene al menos uno de los tokens. 
            /// </summary>
            CaseSensitive = 1,
            /// <summary>
            ///     Modo de búsqueda estricto. Se ignorará el casing, y la cadena
            ///     coincidirá con los términos de búsqueda si contiene todos los
            ///     tokens especificados.
            /// </summary>
            IncludeAll = 2,
            /// <summary>
            ///     Interpretar los tokens por medio del operador Like
            /// </summary>
            WildCard=4
        }

        /// <summary>
        ///     Realiza una búsqueda tokenizada sobre la cadena especificada.
        /// </summary>
        /// <param name="str">
        ///     Cadena en la cual buscar.
        /// </param>
        /// <param name="searchTerms">
        ///     Términos de búsqueda.
        /// </param>
        /// <returns>
        ///     <see langword="true"/> si la cadena coincide con los términos de
        ///     búsqueda especificados, <see langword="false"/> en caso
        ///     contrario.
        /// </returns>
        public static bool TokenSearch(this string str, string searchTerms)
        {
            return TokenSearch(str, searchTerms, SearchOptions.Default);
        }

        /// <summary>
        ///     Realiza una búsqueda tokenizada sobre la cadena especificada.
        /// </summary>
        /// <param name="str">
        ///     Cadena en la cual buscar.
        /// </param>
        /// <param name="searchTerms">
        ///     Términos de búsqueda.
        /// </param>
        /// <param name="options">
        ///     Opciones de búsqueda.
        /// </param>
        /// <returns>
        ///     <see langword="true"/> si la cadena coincide con los términos de
        ///     búsqueda y las opciones especificadas, <see langword="false"/> en
        ///     caso contrario.
        /// </returns>
        public static bool TokenSearch(this string str, string searchTerms, SearchOptions options)
        {
            return TokenSearch(str, searchTerms, ' ', options);
        }

        /// <summary>
        ///     Realiza una búsqueda tokenizada sobre la cadena especificada.
        /// </summary>
        /// <param name="str">
        ///     Cadena en la cual buscar.
        /// </param>
        /// <param name="searchTerms">
        ///     Términos de búsqueda.
        /// </param>
        /// <param name="separator">
        ///     Separador de tokens.
        /// </param>
        /// <param name="options">
        ///     Opciones de búsqueda.
        /// </param>
        /// <returns>
        ///     <see langword="true"/> si la cadena coincide con los términos de
        ///     búsqueda y las opciones especificadas, <see langword="false"/> en
        ///     caso contrario.
        /// </returns>
        public static bool TokenSearch(this string str, string searchTerms, char separator, SearchOptions options)
        {
            var s = options.HasFlag(SearchOptions.CaseSensitive) ? str:str.ToUpper();
            var t = options.HasFlag(SearchOptions.CaseSensitive) ? searchTerms : searchTerms.ToUpper();
            var terms = t.Split(separator);

            if (options.HasFlag(SearchOptions.WildCard))
            {
                return options.HasFlag(SearchOptions.IncludeAll)
                    ? terms.All(j => Regex.IsMatch(s, WildCardToRegular(j)))
                    : terms.Any(j => Regex.IsMatch(s, WildCardToRegular(j)));
            }

            return options.HasFlag(SearchOptions.IncludeAll) 
                ? terms.All(j => s.Contains(j)) 
                : terms.Any(j => s.Contains(j));
        }
        private static string WildCardToRegular(string value)
        {
            return "^" + Regex.Escape(value).Replace("\\?", ".").Replace("\\*", ".*") + "$";
        }
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
            var idx = new List<int>();
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
        ///     Determina si la cadena contiene a cualquiera de los caracteres
        ///     especificados.
        /// </summary>
        /// <returns>
        ///     <see langword="true" /> si la cadena contiene a cualquiera de los caracteres,
        ///     <see langword="false" /> en caso contrario.
        /// </returns>
        /// <param name="stringToCheck">Cadena a verificar.</param>
        /// <param name="chars">Caracteres a buscar.</param>
        [Thunk]
        public static bool ContainsAny(this string stringToCheck, params char[] chars)
        {
            return ContainsAny(stringToCheck, out _, chars);
        }

        /// <summary>
        ///     Determina si la cadena contiene a cualquiera de los caracteres
        ///     especificados.
        /// </summary>
        /// <returns>
        ///     <see langword="true" /> si la cadena contiene a cualquiera de los caracteres,
        ///     <see langword="false" /> en caso contrario.
        /// </returns>
        /// <param name="stringToCheck">Cadena a verificar.</param>
        /// <param name="argNum">
        ///     Parámetro de salida. Si <paramref name="stringToCheck" /> contiene cualquier
        ///     caracter especificado en <paramref name="chars" />, se devolverá el
        ///     índice del argumento contenido; en caso contrario, se devuelve
        ///     <c>-1</c>.
        /// </param>
        /// <param name="chars">Caracteres a buscar.</param>
        public static bool ContainsAny(this string stringToCheck, out int argNum, params char[] chars)
        {
            return stringToCheck.ContainsAny(chars, out argNum);
        }

        /// <summary>
        ///     Determina si la cadena contiene a cualquiera de los caracteres
        ///     especificados.
        /// </summary>
        /// <returns>
        ///     <see langword="true" /> si la cadena contiene a cualquiera de los caracteres,
        ///     <see langword="false" /> en caso contrario.
        /// </returns>
        /// <param name="stringToCheck">Cadena a verificar.</param>
        /// <param name="argNum">
        ///     Parámetro de salida. Si <paramref name="stringToCheck" /> contiene cualquier
        ///     caracter especificado en <paramref name="chars" />, se devolverá el
        ///     índice del argumento contenido; en caso contrario, se devuelve
        ///     <c>-1</c>.
        /// </param>
        /// <param name="chars">Caracteres a buscar.</param>
        public static bool ContainsAny(this string stringToCheck, IEnumerable<char> chars, out int argNum)
        {
            argNum = 0;
            foreach (var j in chars)
            {
                if (stringToCheck.Contains(j)) return true;
                argNum++;
            }

            argNum = -1;
            return false;
        }

        /// <summary>
        ///     Determina si la cadena contiene a cualquiera de las cadenas
        ///     especificadas.
        /// </summary>
        /// <returns>
        ///     <see langword="true" /> si la cadena contiene a cualquiera de los caracteres,
        ///     <see langword="false" /> en caso contrario.
        /// </returns>
        /// <param name="stringToCheck">Cadena a verificar.</param>
        /// <param name="strings">Cadenas a buscar.</param>
        [Thunk]
        public static bool ContainsAny(this string stringToCheck, params string[] strings)
        {
            return ContainsAny(stringToCheck, strings, out _);
        }

        /// <summary>
        ///     Determina si la cadena contiene a cualquiera de las cadenas
        ///     especificadas.
        /// </summary>
        /// <returns>
        ///     <see langword="true" /> si la cadena contiene a cualquiera de los caracteres,
        ///     <see langword="false" /> en caso contrario.
        /// </returns>
        /// <param name="stringToCheck">Cadena a verificar.</param>
        /// <param name="strings">Cadenas a buscar.</param>
        [Thunk]
        public static bool ContainsAny(this string stringToCheck, IEnumerable<string> strings)
        {
            return ContainsAny(stringToCheck, strings, out _);
        }

        /// <summary>
        ///     Determina si la cadena contiene a cualquiera de las cadenas
        ///     especificadas.
        /// </summary>
        /// <returns>
        ///     <see langword="true" /> si la cadena contiene a cualquiera de los caracteres,
        ///     <see langword="false" /> en caso contrario.
        /// </returns>
        /// <param name="stringToCheck">Cadena a verificar.</param>
        /// <param name="argNum">
        ///     Parámetro de salida. Si <paramref name="stringToCheck" /> contiene cualquier
        ///     caracter especificado en <paramref name="strings" />, se devolverá
        ///     el índice del argumento contenido; en caso contrario, se devuelve
        ///     <c>-1</c>.
        /// </param>
        /// <param name="strings">Cadenas a buscar.</param>
        public static bool ContainsAny(this string stringToCheck, out int argNum, params string[] strings)
        {
            return stringToCheck.ContainsAny(strings, out argNum);
        }

        /// <summary>
        ///     Determina si la cadena contiene a cualquiera de las cadenas
        ///     especificadas.
        /// </summary>
        /// <returns>
        ///     <see langword="true" /> si la cadena contiene a cualquiera de los caracteres,
        ///     <see langword="false" /> en caso contrario.
        /// </returns>
        /// <param name="stringToCheck">Cadena a verificar.</param>
        /// <param name="argNum">
        ///     Parámetro de salida. Si <paramref name="stringToCheck" /> contiene cualquier
        ///     caracter especificado en <paramref name="strings" />, se devolverá
        ///     el índice del argumento contenido; en caso contrario, se devuelve
        ///     <c>-1</c>.
        /// </param>
        /// <param name="strings">Cadenas a buscar.</param>
        public static bool ContainsAny(this string stringToCheck, IEnumerable<string> strings, out int argNum)
        {
            argNum = 0;
            foreach (var j in strings)
            {
                if (stringToCheck.Contains(j)) return true;
                argNum++;
            }

            argNum = -1;
            return false;
        }

        /// <summary>
        ///     Verifica si la cadena contiene letras.
        /// </summary>
        /// <returns>
        ///     <see langword="true" /> si la cadena contiene letras: de lo contrario,
        ///     <see langword="false" />.
        /// </returns>
        /// <param name="stringToCheck">Cadena a comprobar.</param>
        public static bool ContainsLetters(this string stringToCheck)
        {
            return stringToCheck.ContainsAny((St.Alpha.ToUpperInvariant() + St.Alpha).ToCharArray());
        }

        /// <summary>
        ///     Verifica si la cadena contiene letras.
        /// </summary>
        /// <returns>
        ///     <see langword="true" /> si la cadena contiene letras: de lo contrario,
        ///     <see langword="false" />.
        /// </returns>
        /// <param name="stringToCheck">Cadena a comprobar.</param>
        /// <param name="ucase">
        ///     Opcional. Especifica el tipo de comprobación a realizar. Si es
        ///     <see langword="true" />, Se tomarán en cuenta únicamente los caracteres en
        ///     mayúsculas, si es <see langword="false" />, se tomarán en cuenta unicamente  los
        ///     caracteres en minúsculas. Si se omite o se establece en <see langword="null" />,
        ///     se tomarán en cuenta ambos casos.
        /// </param>
        public static bool ContainsLetters(this string stringToCheck, bool ucase)
        {
            return stringToCheck.ContainsAny((ucase ? St.Alpha.ToUpperInvariant() : St.Alpha).ToCharArray());
        }

        /// <summary>
        ///     Comprueba si la cadena contiene números
        /// </summary>
        /// <returns>
        ///     <see langword="true" /> si la cadena contiene números; de lo contrario,
        ///     <see langword="false" />.
        /// </returns>
        /// <param name="stringToCheck">Cadena a comprobar.</param>
        public static bool ContainsNumbers(this string stringToCheck)
        {
#if NativeNumbers
            return stringToCheck.ContainsAny(string
                .Join(null, Thread.CurrentThread.CurrentCulture.NumberFormat.NativeDigits).ToCharArray());
#else
            return stringToCheck.ContainsAny("0123456789".ToCharArray());
#endif
        }

        /// <summary>
        ///     Comprueba si un nombre podría tratarse de otro indicado.
        /// </summary>
        /// <returns>
        ///     Un valor porcentual que representa la probabilidad de que
        ///     <paramref name="checkName" /> haga referencia al nombre
        ///     <paramref name="actualName" />.
        /// </returns>
        /// <param name="checkName">Nombre a comprobar.</param>
        /// <param name="actualName">Nombre real conocido.</param>
        /// <exception cref="ArgumentNullException">
        ///     Se produce cuando <paramref name="checkName" /> o
        ///     <paramref name="actualName" /> son cadenas vacías o <see langword="null" />.
        /// </exception>
        public static float CouldItBe(this string checkName, string actualName)
        {
            return CouldItBe(checkName, actualName, 0.75f);
        }

        /// <summary>
        ///     Comprueba si un nombre podría tratarse de otro indicado.
        /// </summary>
        /// <returns>
        ///     Un valor que representa la probabilidad de que
        ///     <paramref name="checkName" /> haga referencia al nombre
        ///     <paramref name="actualName" />.
        /// </returns>
        /// <param name="checkName">Nombre a comprobar.</param>
        /// <param name="actualName">Nombre real conocido.</param>
        /// <param name="tolerance">
        ///     Opcional. <see cref="float" /> entre 0.0 y 1.0 que establece el
        ///     nivel mínimo de similitud aceptado. si no se especifica, se asume
        ///     75% (0.75).
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     Se produce cuando <paramref name="tolerance" /> no es un valor entre
        ///     <c>0.0f</c> y <c>1.0f</c>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        ///     Se produce cuando <paramref name="checkName" /> o
        ///     <paramref name="actualName" /> son cadenas vacías o <see langword="null" />.
        /// </exception>
        public static float CouldItBe(this string checkName, string actualName, float tolerance)
        {
            if (checkName.IsEmpty()) throw new ArgumentNullException(nameof(checkName));
            if (actualName.IsEmpty()) throw new ArgumentNullException(nameof(actualName));
            if (!tolerance.IsBetween(float.Epsilon, 1)) throw new ArgumentOutOfRangeException(nameof(tolerance));
            var n = 0f;
            var m = 0;
            foreach (var j in checkName.Split(' '))
            {
                m++;
                n += actualName.Split(' ').Select(k => j.Likeness(k)).Where(l => l > tolerance).Sum();
            }

            return n / m;
        }

        /// <summary>
        ///     Cuenta los caracteres que contiene una cadena.
        /// </summary>
        /// <returns>
        ///     Un <see cref="int" /> con la cantidad total de caracteres de
        ///     <paramref name="chars" /> que aparecen en <paramref name="stringToCheck" />.
        /// </returns>
        /// <param name="stringToCheck">Cadena a comprobar.</param>
        /// <param name="chars">Caracteres a contar.</param>
        public static int CountChars(this string stringToCheck, params char[] chars)
        {
            return chars.Sum(j => stringToCheck.Count(a => a == j));
        }

        /// <summary>
        ///     Cuenta los caracteres que contiene una cadena.
        /// </summary>
        /// <returns>
        ///     Un <see cref="int" /> con la cantidad total de
        ///     caracteres de <paramref name="chars" /> que aparecen en
        ///     <paramref name="stringToCheck" />.
        /// </returns>
        /// <param name="stringToCheck">Cadena a comprobar.</param>
        /// <param name="chars">Caracteres a contar.</param>
        [Thunk]
        public static int CountChars(this string stringToCheck, string chars)
        {
            return CountChars(stringToCheck, chars.ToCharArray());
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
        /// <typeparam name="TSource">Tipo de datos de orígen.</typeparam>
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
        /// <param name="source">Tipo de datos de orígen.</param>
        /// <param name="target">Tipo de datos de destino.</param>
        /// <returns>
        ///     Un <see cref="TypeConverter" /> capaz de realizar la conversión
        ///     entre los tipos requeridos, o <see langword="null" /> si no se
        ///     ha encontrado un convertidor adecuado.
        /// </returns>
        public static TypeConverter FindConverter(Type source, Type target)
        {
            return Objects.GetTypes<TypeConverter>(true)
                .Select(j => j.New<TypeConverter>(false))
                .FirstOrDefault(t =>
                {
                    if (t is null) return false;
                    return t.CanConvertFrom(source) && t.CanConvertTo(target);
                });
        }

        /// <summary>
        ///     Invierte el Endianess de un valor <see cref="short" />.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Un <see cref="short" /> cuyo Endianess ha sido invertido.</returns>
        public static short FlipEndianess(this short value)
        {
            return BitConverter.ToInt16(BitConverter.GetBytes(value).Reverse().ToArray(), 0);
        }

        /// <summary>
        ///     Invierte el Endianess de un valor <see cref="int" />.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Un <see cref="int" /> cuyo Endianess ha sido invertido.</returns>
        public static int FlipEndianess(this int value)
        {
            return BitConverter.ToInt32(BitConverter.GetBytes(value).Reverse().ToArray(), 0);
        }

        /// <summary>
        ///     Invierte el Endianess de un valor <see cref="long" />.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Un <see cref="long" /> cuyo Endianess ha sido invertido.</returns>
        public static long FlipEndianess(this long value)
        {
            return BitConverter.ToInt64(BitConverter.GetBytes(value).Reverse().ToArray(), 0);
        }

        /// <summary>
        ///     Invierte el Endianess de un valor <see cref="char" />.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Un <see cref="char" /> cuyo Endianess ha sido invertido.</returns>
        public static char FlipEndianess(this char value)
        {
            return BitConverter.ToChar(BitConverter.GetBytes(value).Reverse().ToArray(), 0);
        }

        /// <summary>
        ///     Invierte el Endianess de un valor <see cref="float" />.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Un <see cref="float" /> cuyo Endianess ha sido invertido.</returns>
        public static float FlipEndianess(this float value)
        {
            return BitConverter.ToSingle(BitConverter.GetBytes(value).Reverse().ToArray(), 0);
        }

        /// <summary>
        ///     Invierte el Endianess de un valor <see cref="double" />.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Un <see cref="double" /> cuyo Endianess ha sido invertido.</returns>
        public static double FlipEndianess(this double value)
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
        /// <param name="min">Mínimo del rango de valores, inclusive.</param>
        /// <param name="max">Máximo del rango de valores, inclusive.</param>
        /// <param name="inclusive">Inclusividad. de forma predeterminada, la comprobación es inclusive.</param>
        /// <typeparam name="T">Tipo de objeto a comprobar.</typeparam>
        public static bool IsBetween<T>(this T value, T min, T max, bool inclusive) where T : IComparable<T>
        {
            return inclusive
                ? value.CompareTo(min) >= 0 && value.CompareTo(max) <= 0
                : value.CompareTo(min) > 0 && value.CompareTo(max) < 0;
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
        public static bool IsBetween<T>(this T value, Range<T> range) where T : IComparable<T>
        {
            return IsBetween(value, range, true);
        }

        /// <summary>
        ///     Comprueba que el valor se encuentre en el rango especificado.
        /// </summary>
        /// <typeparam name="T">Tipo de objeto a comprobar.</typeparam>
        /// <param name="value">Valor a comprobar.</param>
        /// <param name="range">Rango de valores inclusivos a comprobar.</param>
        /// <param name="inclusive">Inclusividad. de forma predeterminada, la comprobación es inclusive.</param>
        /// <returns>
        ///     <see langword="true" /> si el valor se encuentra entre los
        ///     especificados; de lo contrario, <see langword="false" />.
        /// </returns>
        public static bool IsBetween<T>(this T value, Range<T> range, bool inclusive) where T : IComparable<T>
        {
            return range.IsWithin(value, inclusive);
        }

        /// <summary>
        ///     Determina si una cadena contiene un valor binario.
        /// </summary>
        /// <param name="str">cadena a comprobar.</param>
        /// <returns>
        ///     <see langword="true" /> si la cadena contiene un valor que puede
        ///     ser interpretado como un número binario,
        ///     <see langword="false" /> en caso contrario.
        /// </returns>
        public static bool IsBinary(this string str)
        {
            if (str.StartsWith("0b", true, CultureInfo.CurrentCulture)
                || str.StartsWith("&b", true, CultureInfo.CurrentCulture)) str = str.Substring(2);
            return str.ToCharArray().All(j => "01".Contains(j));
        }

        /// <summary>
        ///     Determina si una cadena está vacía.
        /// </summary>
        /// <returns>
        ///     <see langword="true" /> si la cadena está vacía o es <see langword="null" />; de lo
        ///     contrario, <see langword="false" />.
        /// </returns>
        /// <param name="stringToCheck">Cadena a comprobar.</param>
        [Thunk]
        public static bool IsEmpty(this string stringToCheck)
        {
            return string.IsNullOrWhiteSpace(stringToCheck);
        }

        /// <summary>
        ///     Se asegura de devolver <see langword="null"/> si la cadena está vacía.
        /// </summary>
        /// <param name="str">Cadena a devolver.</param>
        /// <returns>
        ///     La cadena, o <see langword="null"/> si la cadena está vacía.
        /// </returns>
        public static string OrNull(this string str)
        {
            return str.IsEmpty() ? null : str;
        }

        /// <summary>
        ///     Comprueba si la cadena tiene un formato alfanumérico básico igual
        ///     al especificado.
        /// </summary>
        /// <param name="checkString"><see cref="string" /> a comprobar.</param>
        /// <param name="format">
        ///     Formato alfanumérico básico contra el cual comparar.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> si el formato de la cadena es igual al
        ///     especificado, <see langword="false" /> en caso contrario.
        /// </returns>
        public static bool IsFormattedAs(this string checkString, string format)
        {
            return IsFormattedAs(checkString, format, false);
        }

        /// <summary>
        ///     Comprueba si la cadena tiene un formato alfanumérico básico igual
        ///     al especificado.
        /// </summary>
        /// <param name="checkString"><see cref="string" /> a comprobar.</param>
        /// <param name="format">
        ///     Formato alfanumérico básico contra el cual comparar.
        /// </param>
        /// <param name="checkCase">
        ///     Si se establece en <see langword="true" />, se hará una evaluación
        ///     sensible al Casing de la cadena.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> si el formato de la cadena es igual al
        ///     especificado, <see langword="false" /> en caso contrario.
        /// </returns>
        public static bool IsFormattedAs(this string checkString, string format, bool checkCase)
        {
            var chkS = checkCase ? checkString : checkString.ToUpperInvariant();
            var fS = checkCase ? format : format.ToUpperInvariant();
            if (chkS.Length != fS.Length) return false;
            for (var j = 0; j < chkS.Length; j++)
            {
                var strChar = chkS[j];
                var fChar = fS[j];
                switch (fChar)
                {
                    case '0':
                    case '9':
                        if (!char.IsDigit(strChar)) return false;
                        break;
                    case 'B':
                    case 'b':
                        if (!"01".Contains(strChar)) return false;
                        break;
                    case 'f':
                    case 'F':
                        if (!byte.TryParse($"{strChar}", NumberStyles.HexNumber, null, out _)) return false;
                        break;
                    case 'A':
                    case 'X':
                        if (!char.IsUpper(strChar)) return false;
                        break;
                    case 'a':
                    case 'x':
                        if (!char.IsLower(strChar)) return false;
                        break;
                    default: // Caracteres literales.
                        if (strChar != fChar) return false;
                        break;
                }
            }

            return true;
        }

        /// <summary>
        ///     Determina si una cadena contiene un valor hexadecimal.
        /// </summary>
        /// <param name="str">cadena a comprobar.</param>
        /// <returns>
        ///     <see langword="true" /> si la cadena contiene un valor que puede
        ///     ser interpretado como un número hexadecimal,
        ///     <see langword="false" /> en caso contrario.
        /// </returns>
        public static bool IsHex(this string str)
        {
            if (str.StartsWith("0x") || str.StartsWith("&h", true, CultureInfo.CurrentCulture)) str = str.Substring(2);
            return str.ToCharArray().All(j => "0123456789abcdefABCDEF".Contains(j));
        }

        /// <summary>
        ///     Obtiene una cadena que contenga la cantidad de caracteres
        ///     especificados desde la izquierda de la cadena.
        /// </summary>
        /// <param name="string">
        ///     Instancia de <see cref="string" /> a procesar.
        /// </param>
        /// <param name="length">Longitud de caracteres a obtener.</param>
        /// <returns>
        ///     Una cadena que contiene los caracteres especificados desde la
        ///     izquierda de la cadena.
        /// </returns>
        public static string Left(this string @string, int length)
        {
            if (!length.IsBetween(0, @string.Length))
                throw new ArgumentOutOfRangeException(nameof(length));
            return @string.Substring(0, length);
        }

        /// <summary>
        ///     Calcula el porcentaje de similitud entre dos <see cref="string" />.
        /// </summary>
        /// <returns>El porcentaje de similitud entre las dos cadenas.</returns>
        /// <param name="ofString">Cadena A a comparar.</param>
        /// <param name="toString">Cadena B a comparar.</param>
        public static float Likeness(this string ofString, string toString)
        {
            return Likeness(ofString, toString, 3);
        }

        /// <summary>
        ///     Calcula el porcentaje de similitud entre dos <see cref="string" />.
        /// </summary>
        /// <returns>El porcentaje de similitud entre las dos cadenas.</returns>
        /// <param name="ofString">Cadena A a comparar.</param>
        /// <param name="toString">Cadena B a comparar.</param>
        /// <param name="tolerance">
        ///     Rango de tolerancia de la comparación. Representa la distancia
        ///     máxima permitida de cada caracter que todavía hace a las cadenas
        ///     similares.
        /// </param>
        public static float Likeness(this string ofString, string toString, int tolerance)
        {
            int steps = 0, likes = 0;
            ofString = new string(' ', tolerance - 1) + ofString.ToUpper() + new string(' ', tolerance - 1);
            foreach (var c in toString.ToUpper())
                if (ofString.Substring(steps++, tolerance).Contains(c))
                    likes++;

            return likes / (float) steps;
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
        ///     Obtiene una cadena que contenga la cantidad de caracteres
        ///     especificados desde la izquierda de la cadena.
        /// </summary>
        /// <param name="string">
        ///     Instancia de <see cref="string" /> a procesar.
        /// </param>
        /// <param name="length">Longitud de caracteres a obtener.</param>
        /// <returns>
        ///     Una cadena que contiene los caracteres especificados desde la
        ///     izquierda de la cadena.
        /// </returns>
        public static string Right(this string @string, int length)
        {
            if (!length.IsBetween(0, @string.Length))
                throw new ArgumentOutOfRangeException(nameof(length));
            return @string.Substring(length, @string.Length - length);
        }

        /// <summary>
        ///     Genera una secuencia de números en el rango especificado.
        /// </summary>
        /// <returns>
        ///     Una lista de enteros con la secuencia generada.
        /// </returns>
        /// <param name="top">Valor más alto.</param>
        [Thunk]
        public static IEnumerable<int> Sequence(int top)
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
        public static IEnumerable<int> Sequence(int floor, int top)
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
        ///     Determina si la cadena inicia con cualquiera de las cadenas
        ///     especificadas.
        /// </summary>
        /// <param name="str">Cadena a comprobar.</param>
        /// <param name="strings">
        ///     Colección de cadenas iniciales a determinar.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> si la cadena comienza cun cualquiera de
        ///     las cadenas especificadas, <see langword="false" /> en caso
        ///     contrario.
        /// </returns>
        public static bool StartsWithAny(this string str, IEnumerable<string> strings)
        {
            return strings.Any(str.StartsWith);
        }

        /// <summary>
        ///     Determina si la cadena inicia con cualquiera de las cadenas
        ///     especificadas.
        /// </summary>
        /// <param name="str">Cadena a comprobar.</param>
        /// <param name="strings">
        ///     Colección de cadenas iniciales a determinar.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> si la cadena comienza cun cualquiera de
        ///     las cadenas especificadas, <see langword="false" /> en caso
        ///     contrario.
        /// </returns>
        public static bool StartsWithAny(this string str, params string[] strings)
        {
            return strings.Any(str.StartsWith);
        }

        /// <summary>
        ///     Determina si la cadena inicia con cualquiera de las cadenas
        ///     especificadas.
        /// </summary>
        /// <param name="str">Cadena a comprobar.</param>
        /// <param name="strings">
        ///     Colección de cadenas iniciales a determinar.
        /// </param>
        /// <param name="ignoreCase">
        ///     Si se establece en <see langword="true" />, se tomarán en cuenta
        ///     mayúsculas y minúsculas como iguales, si se establece en
        ///     <see langword="false" />, se tomará en cuenta el casing de los
        ///     caracteres de las cadenas.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> si la cadena comienza cun cualquiera de
        ///     las cadenas especificadas, <see langword="false" /> en caso
        ///     contrario.
        /// </returns>
        public static bool StartsWithAny(this string str, IEnumerable<string> strings, bool ignoreCase)
        {
            return strings.Any(p => str.StartsWith(p, ignoreCase, CultureInfo.CurrentCulture));
        }

        /// <summary>
        ///     Determina si la cadena inicia con cualquiera de las cadenas
        ///     especificadas.
        /// </summary>
        /// <param name="str">Cadena a comprobar.</param>
        /// <param name="strings">
        ///     Colección de cadenas iniciales a determinar.
        /// </param>
        /// <param name="ignoreCase">
        ///     Si se establece en <see langword="true" />, se tomarán en cuenta
        ///     mayúsculas y minúsculas como iguales, si se establece en
        ///     <see langword="false" />, se tomará en cuenta el casing de los
        ///     caracteres de las cadenas.
        /// </param>
        /// <param name="culture">
        ///     Determina la cultura a utilizar para realizar la comprobación.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> si la cadena comienza cun cualquiera de
        ///     las cadenas especificadas, <see langword="false" /> en caso
        ///     contrario.
        /// </returns>
        public static bool StartsWithAny(this string str, IEnumerable<string> strings, bool ignoreCase,
            CultureInfo culture)
        {
            return strings.Any(p => str.StartsWith(p, ignoreCase, culture));
        }

        /// <summary>
        ///     Determina si la cadena inicia con cualquiera de las cadenas
        ///     especificadas.
        /// </summary>
        /// <param name="str">Cadena a comprobar.</param>
        /// <param name="strings">
        ///     Colección de cadenas iniciales a determinar.
        /// </param>
        /// <param name="comparison">
        ///     Especifica la cultura, casing y reglas de ordenado a utilizar
        ///     para realizar la comprobación.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> si la cadena comienza cun cualquiera de
        ///     las cadenas especificadas, <see langword="false" /> en caso
        ///     contrario.
        /// </returns>
        public static bool StartsWithAny(this string str, IEnumerable<string> strings, StringComparison comparison)
        {
            return strings.Any(p => str.StartsWith(p, comparison));
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
        public static string ToHex(this byte @byte)
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
        public static IEnumerable<float> ToPercent(this IEnumerable<float> collection, bool baseZero)
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
        public static IEnumerable<float> ToPercent(this IEnumerable<float> collection, float max)
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
        public static IEnumerable<double> ToPercent(this IEnumerable<double> collection, bool baseZero)
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
        public static IEnumerable<double> ToPercent(this IEnumerable<double> collection, double max)
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
        public static IEnumerable<double> ToPercentDouble(this IEnumerable<int> collection, bool baseZero)
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
        public static IEnumerable<double> ToPercentDouble(this IEnumerable<int> collection, int max)
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
        public static IEnumerable<float> ToPercentSingle(this IEnumerable<int> collection, bool baseZero)
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
        public static IEnumerable<float> ToPercentSingle(this IEnumerable<int> collection, int max)
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
        ///     Permite obtener el contenido de un <see cref="string" /> como un
        ///     <see cref="Stream" /> utilizando la codificación especificada.
        /// </summary>
        /// <param name="string">Cadena a convertir.</param>
        /// <param name="encoding">Codificación de cadena.</param>
        /// <returns>
        ///     Un <see cref="Stream" /> con el contenido de la cadena.
        /// </returns>
        public static Stream ToStream(this string @string, Encoding encoding)
        {
            return new MemoryStream(encoding.GetBytes(@string));
        }

        /// <summary>
        ///     Permite obtener el contenido de un <see cref="string" /> como un
        ///     <see cref="Stream" /> utilizando la codificación UTF8.
        /// </summary>
        /// <param name="string">Cadena a convertir.</param>
        /// <returns>
        ///     Un <see cref="Stream" /> con el contenido de la cadena.
        /// </returns>
        /// <remarks>
        ///     A pesar de que las cadenas en .Net Framework son UTF16, ciertas
        ///     funciones comunes prefieren trabajar con cadenas codificadas en
        ///     UTF8, por lo que este método utiliza dicha codificación para
        ///     realizar la conversión.
        /// </remarks>
        public static Stream ToStream(this string @string)
        {
            return ToStream(@string, Encoding.UTF8);
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
            var outp = new List<short>();
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
            var outp = new List<char>();
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
            var outp = new List<byte>();
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

        /// <summary>
        ///     Convierte un <see cref="string" /> en un <see cref="SecureString" />.
        /// </summary>
        /// <param name="string"><see cref="string" /> a convertir.</param>
        /// <returns>
        ///     Un <see cref="SecureString" /> que contiene todos los caracteres
        ///     originales de la cadena provista.
        /// </returns>
        public static SecureString ToSecureString(this string @string)
        {
            var retVal = new SecureString();
            foreach (var j in @string) retVal.AppendChar(j);
            return retVal;
        }
#pragma warning restore XS0001
    }
}