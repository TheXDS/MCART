/*
StringExtensions.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

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
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Exceptions;
using St = TheXDS.MCART.Resources.Strings;

// ReSharper disable UnusedMember.Global

namespace TheXDS.MCART.Types.Extensions
{
    /// <summary>
    ///     Extensiones de la clase <see cref="string" />.
    /// </summary>
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public static class StringExtensions
    {
        /// <summary>
        ///     Describe las opciones de búsqueda para el método
        ///     <see cref="TokenSearch(string, string, SearchOptions)" />
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
            WildCard = 4
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
        [Sugar]
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
        ///     carácter especificado en <paramref name="chars" />, se devolverá el
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
        ///     carácter especificado en <paramref name="chars" />, se devolverá el
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
        [Sugar]
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
        [Sugar]
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
        ///     carácter especificado en <paramref name="strings" />, se devolverá
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
        ///     carácter especificado en <paramref name="strings" />, se devolverá
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
        [Sugar]
        public static int CountChars(this string stringToCheck, string chars)
        {
            return CountChars(stringToCheck, chars.ToCharArray());
        }

        /// <author>H.A. Sullivan</author>
        /// <date>04/11/2016</date>
        /// <summary>
        ///     Comprueba si una cadena coincide con un WildCard especificado.
        /// </summary>
        /// <license>
        ///     MIT License
        ///     Copyright(c) [2016]
        ///     [H.A. Sullivan]
        ///     Permission is hereby granted, free of charge, to any person obtaining a copy
        ///     of this software and associated documentation files (the "Software"), to deal
        ///     in the Software without restriction, including without limitation the rights
        ///     to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
        ///     copies of the Software, and to permit persons to whom the Software is
        ///     furnished to do so, subject to the following conditions:
        ///     The above copyright notice and this permission notice shall be included in all
        ///     copies or substantial portions of the Software.
        ///     THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
        ///     IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
        ///     FITNESS FOR A PARTICULAR PURPOSE AND NON-INFRINGEMENT. IN NO EVENT SHALL THE
        ///     AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
        ///     LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
        ///     OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
        ///     SOFTWARE.
        /// </license>
        /// <param name="text">
        ///     Cadena a comprobar.
        /// </param>
        /// <param name="wildcardString">
        ///     WildCard contra el cual comprobar.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> si la cadena coincide con el Wildcard
        ///     especificado, <see langword="false" /> en caso contrario.
        /// </returns>
        /// <remarks>
        ///     Versión original del algoritmo por H.A. Sullivan, bajo licencia MIT.
        ///     <h1>Cambios en esta versión:</h1>
        ///     <list type="bullet">
        ///         <item>
        ///             <description>Optimizaciones sugeridas por ReSharper.</description>
        ///         </item>
        ///         <item>
        ///             <description>Traducción de documentación.</description>
        ///         </item>
        ///     </list>
        /// </remarks>
        // ReSharper disable once CyclomaticComplexity
        public static bool EqualsWildcard(this string text, string wildcardString)
        {
            var isLike = true;
            byte matchCase = 0;
            char[] reversedFilter;
            char[] reversedWord;
            var currentPatternStartIndex = 0;
            var lastCheckedHeadIndex = 0;
            var lastCheckedTailIndex = 0;
            var reversedWordIndex = 0;
            var reversedPatterns = new List<char[]>();

            if (text == null || wildcardString == null) return false;

            var word = text.ToCharArray();
            var filter = wildcardString.ToCharArray();

            //Set which case will be used (0 = no wildcards, 1 = only ?, 2 = only *, 3 = both ? and *
            if (filter.Any(t => t == '?'))
                matchCase += 1;

            if (filter.Any(t => t == '*'))
                matchCase += 2;

            if ((matchCase == 0 || matchCase == 1) && word.Length != filter.Length) return false;

            switch (matchCase)
            {
                case 0:
                    isLike = text == wildcardString;
                    break;

                case 1:
                    for (var i = 0; i < text.Length; i++)
                        if (word[i] != filter[i] && filter[i] != '?')
                            isLike = false;
                    break;

                case 2:
                    //Search for matches until first *
                    for (var i = 0; i < filter.Length; i++)
                        if (filter[i] != '*')
                        {
                            if (filter[i] != word[i]) return false;
                        }
                        else
                        {
                            lastCheckedHeadIndex = i;
                            break;
                        }

                    //Search Tail for matches until first *
                    for (var i = 0; i < filter.Length; i++)
                        if (filter[filter.Length - 1 - i] != '*')
                        {
                            if (filter[filter.Length - 1 - i] != word[word.Length - 1 - i]) return false;
                        }
                        else
                        {
                            lastCheckedTailIndex = i;
                            break;
                        }


                    //Create a reverse word and filter for searching in reverse. The reversed word and filter do not include already checked chars
                    reversedWord = new char[word.Length - lastCheckedHeadIndex - lastCheckedTailIndex];
                    reversedFilter = new char[filter.Length - lastCheckedHeadIndex - lastCheckedTailIndex];

                    for (var i = 0; i < reversedWord.Length; i++)
                        reversedWord[i] = word[word.Length - (i + 1) - lastCheckedTailIndex];
                    for (var i = 0; i < reversedFilter.Length; i++)
                        reversedFilter[i] = filter[filter.Length - (i + 1) - lastCheckedTailIndex];

                    //Cut up the filter into separate patterns, exclude * as they are not longer needed
                    for (var i = 0; i < reversedFilter.Length; i++)
                    {
                        if (reversedFilter[i] != '*') continue;
                        if (i - currentPatternStartIndex > 0)
                        {
                            var pattern = new char[i - currentPatternStartIndex];
                            for (var j = 0; j < pattern.Length; j++)
                                pattern[j] = reversedFilter[currentPatternStartIndex + j];
                            reversedPatterns.Add(pattern);
                        }

                        currentPatternStartIndex = i + 1;
                    }

                    //Search for the patterns
                    foreach (var t in reversedPatterns)
                        for (var j = 0; j < t.Length; j++)
                        {
                            if (reversedWordIndex > reversedWord.Length - 1) return false;

                            if (t[j] != reversedWord[reversedWordIndex + j])
                            {
                                reversedWordIndex += 1;
                                j = -1;
                            }
                            else
                            {
                                if (j == t.Length - 1) reversedWordIndex = reversedWordIndex + t.Length;
                            }
                        }

                    break;

                case 3:
                    //Same as Case 2 except ? is considered a match
                    //Search Head for matches util first *
                    for (var i = 0; i < filter.Length; i++)
                        if (filter[i] != '*')
                        {
                            if (filter[i] != word[i] && filter[i] != '?') return false;
                        }
                        else
                        {
                            lastCheckedHeadIndex = i;
                            break;
                        }

                    //Search Tail for matches until first *
                    for (var i = 0; i < filter.Length; i++)
                        if (filter[filter.Length - 1 - i] != '*')
                        {
                            if (filter[filter.Length - 1 - i] != word[word.Length - 1 - i] &&
                                filter[filter.Length - 1 - i] != '?') return false;
                        }
                        else
                        {
                            lastCheckedTailIndex = i;
                            break;
                        }

                    // Reverse and trim word and filter
                    reversedWord = new char[word.Length - lastCheckedHeadIndex - lastCheckedTailIndex];
                    reversedFilter = new char[filter.Length - lastCheckedHeadIndex - lastCheckedTailIndex];

                    for (var i = 0; i < reversedWord.Length; i++)
                        reversedWord[i] = word[word.Length - (i + 1) - lastCheckedTailIndex];
                    for (var i = 0; i < reversedFilter.Length; i++)
                        reversedFilter[i] = filter[filter.Length - (i + 1) - lastCheckedTailIndex];

                    for (var i = 0; i < reversedFilter.Length; i++)
                    {
                        if (reversedFilter[i] != '*') continue;
                        if (i - currentPatternStartIndex > 0)
                        {
                            var pattern = new char[i - currentPatternStartIndex];
                            for (var j = 0; j < pattern.Length; j++)
                                pattern[j] = reversedFilter[currentPatternStartIndex + j];
                            reversedPatterns.Add(pattern);
                        }

                        currentPatternStartIndex = i + 1;
                    }

                    //Search for the patterns
                    foreach (var t in reversedPatterns)
                        for (var j = 0; j < t.Length; j++)
                        {
                            if (reversedWordIndex > reversedWord.Length - 1) return false;

                            if (t[j] != '?' && t[j] != reversedWord[reversedWordIndex + j])
                            {
                                reversedWordIndex += 1;
                                j = -1;
                            }
                            else
                            {
                                if (j == t.Length - 1) reversedWordIndex = reversedWordIndex + t.Length;
                            }
                        }

                    break;
                default:
                    throw new NotImplementedException();
            }

            return isLike;
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
        [Sugar]
        public static bool IsEmpty(this string stringToCheck)
        {
            return string.IsNullOrWhiteSpace(stringToCheck);
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
        [SuppressMessage("ReSharper", "StringLiteralTypo")]
        public static bool IsHex(this string str)
        {
            if (str.StartsWith("0x") || str.StartsWith("&h", true, CultureInfo.CurrentCulture)) str = str.Substring(2);
            return str.ToCharArray().All(j => @"0123456789abcdefABCDEF".Contains(j));
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
        ///     máxima permitida de cada carácter que todavía hace a las cadenas
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
        ///     Se asegura de devolver <see cref="string.Empty" /> si la cadena
        ///     está vacía.
        /// </summary>
        /// <param name="str">Cadena a devolver.</param>
        /// <returns>
        ///     La cadena, o <see cref="string.Empty" /> si la cadena está
        ///     vacía.
        /// </returns>
        public static string OrEmpty(this string str)
        {
            return OrX(str, string.Empty);
        }

        /// <summary>
        ///     Se asegura de devolver <see cref="string.Empty" /> si la cadena
        ///     está vacía.
        /// </summary>
        /// <param name="str">Cadena a devolver.</param>
        /// <param name="notEmptyFormat">
        ///     Formato a aplicar en caso de que la cadena no sea
        ///     <see cref="string.Empty" />.
        /// </param>
        /// <returns>
        ///     La cadena, o <see cref="string.Empty" /> si la cadena está
        ///     vacía.
        /// </returns>
        public static string OrEmpty(this string str, string notEmptyFormat)
        {
            return !str.IsEmpty() ? string.Format(notEmptyFormat, str) : string.Empty;
        }

        /// <summary>
        ///     Se asegura de devolver <see langword="null" /> si la cadena
        ///     está vacía.
        /// </summary>
        /// <param name="str">Cadena a devolver.</param>
        /// <returns>
        ///     La cadena, o <see langword="null" /> si la cadena está vacía.
        /// </returns>
        public static string OrNull(this string str)
        {
            return OrX(str, null);
        }

        /// <summary>
        ///     Se asegura de devolver <see langword="null" /> si la cadena
        ///     está vacía.
        /// </summary>
        /// <param name="str">Cadena a devolver.</param>
        /// <param name="notNullFormat">
        ///     Formato a aplicar en caso de que la cadena no sea
        ///     <see langword="null" />.
        /// </param>
        /// <returns>
        ///     La cadena, o <see langword="null" /> si la cadena está vacía.
        /// </returns>
        public static string OrNull(this string str, string notNullFormat)
        {
            return !str.IsEmpty() ? string.Format(notNullFormat, str) : null;
        }

        private static string OrX(string source, string emptyRetVal)
        {
            return !source.IsEmpty() ? source : emptyRetVal;
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
        ///     Separa cada carácter de una cadena con el <see cref="char" />
        ///     especificado.
        /// </summary>
        /// <param name="str">Cadena a procesar.</param>
        /// <param name="separationChar">Carácter de separación a utilizar.</param>
        /// <returns>
        ///     Una cadena cuyos caracteres han sido separados con el
        ///     <see cref="char" /> especificado.
        /// </returns>
        public static string Separate(this string str, char separationChar)
        {
            IEnumerable<char> InsertSpace()
            {
                var e = str.ToCharArray().GetEnumerator();
                e.Reset();
                if (!e.MoveNext()) yield break;
                while (true)
                {
                    yield return (char) (e.Current ?? throw new TamperException());
                    if (!e.MoveNext()) break;
                    yield return separationChar;
                }
            }

            return new string(InsertSpace().ToArray());
        }

        /// <summary>
        ///     Separa cada carácter de una cadena con un espacio en blanco.
        /// </summary>
        /// <param name="str">Cadena a procesar.</param>
        /// <returns>
        ///     Una cadena cuyos caracteres han sido separados con un espacio en blanco.
        /// </returns>
        public static string Spell(this string str)
        {
            return Separate(str, ' ');
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
        ///     Realiza una búsqueda tokenizada sobre la cadena especificada.
        /// </summary>
        /// <param name="str">
        ///     Cadena en la cual buscar.
        /// </param>
        /// <param name="searchTerms">
        ///     Términos de búsqueda.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> si la cadena coincide con los términos de
        ///     búsqueda especificados, <see langword="false" /> en caso
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
        ///     <see langword="true" /> si la cadena coincide con los términos de
        ///     búsqueda y las opciones especificadas, <see langword="false" /> en
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
        ///     <see langword="true" /> si la cadena coincide con los términos de
        ///     búsqueda y las opciones especificadas, <see langword="false" /> en
        ///     caso contrario.
        /// </returns>
        public static bool TokenSearch(this string str, string searchTerms, char separator, SearchOptions options)
        {
            var s = options.HasFlag(SearchOptions.CaseSensitive) ? str : str.ToUpper();
            var t = options.HasFlag(SearchOptions.CaseSensitive) ? searchTerms : searchTerms.ToUpper();
            var terms = t.Split(separator);

            if (options.HasFlag(SearchOptions.WildCard))
                return options.HasFlag(SearchOptions.IncludeAll)
                    ? terms.All(j => Regex.IsMatch(s, WildCardToRegular(j)))
                    : terms.Any(j => Regex.IsMatch(s, WildCardToRegular(j)));

            return options.HasFlag(SearchOptions.IncludeAll)
                ? terms.All(j => s.Contains(j))
                : terms.Any(j => s.Contains(j));
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

        private static string WildCardToRegular(string value)
        {
            return "^" + Regex.Escape(value).Replace("\\?", ".").Replace("\\*", ".*") + "$";
        }
    }
}