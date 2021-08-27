/*
StringExtensions.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

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
using TheXDS.MCART.Helpers;
using St = TheXDS.MCART.Resources.Strings;

namespace TheXDS.MCART.Types.Extensions
{
    /// <summary>
    /// Extensiones de la clase <see cref="string" />.
    /// </summary>
    public static class StringExtensions
    {
        private static (string match, Func<char, bool> test)[]? matchRules;

        /// <summary>
        /// Describe las opciones de búsqueda para el método
        /// <see cref="TokenSearch(string, string, SearchOptions)" />
        /// </summary>
        [Flags]
        public enum SearchOptions
        {
            /// <summary>
            /// Modo de búsqueda predeterminado. Se ignorará el Casing y la
            /// cadena coincidirá con los términos de búsqueda si contiene al
            /// menos uno de los tokens.
            /// </summary>
            Default,

            /// <summary>
            /// Modo sensible al Casing. La cadena coincidirá con los términos
            /// de búsqueda si contiene al menos uno de los tokens.
            /// </summary>
            CaseSensitive = 1,

            /// <summary>
            /// Modo de búsqueda estricto. Se ignorará el casing, y la cadena
            /// coincidirá con los términos de búsqueda si contiene todos los
            /// tokens especificados.
            /// </summary>
            IncludeAll = 2,

            /// <summary>
            /// Interpretar los tokens por medio del operador Like
            /// </summary>
            WildCard = 4
        }
        
        /// <summary>
        /// Elimina una ocurrencia de una cadena a los extremos de otra.
        /// </summary>
        /// <param name="str">Cadena a comprobar.</param>
        /// <param name="toChop">Valor a cortar.</param>
        /// <returns>
        /// Una cadena que no empiece ni termine en
        /// <paramref name="toChop"/>.
        /// </returns>
        public static string Chop(this string str, string toChop)
        {
            return str.ChopStart(toChop).ChopEnd(toChop);
        }

        /// <summary>
        /// Elimina una ocurrencia de una cadena a los extremos de otra.
        /// </summary>
        /// <param name="str">Cadena a comprobar.</param>
        /// <param name="toChop">Valor a cortar.</param>
        /// <returns>
        /// Una cadena que no empiece ni termine en
        /// <paramref name="toChop"/>.
        /// </returns>
        public static string ChopAny(this string str, params string[] toChop)
        {
            foreach (var j in toChop)
            {
                if (str.StartsWith(j)) return str.Remove(0, j.Length);
                if (str.EndsWith(j)) return str.Remove(str.Length - j.Length, j.Length);
            }
            return str;
        }

        /// <summary>
        /// Elimina una ocurrencia de una cadena al final de otra.
        /// </summary>
        /// <param name="str">Cadena a comprobar.</param>
        /// <param name="toChop">Valor a cortar.</param>
        /// <returns>
        /// Una cadena que no termine en <paramref name="toChop"/>.
        /// </returns>
        public static string ChopEnd(this string str, string toChop)
        {
            return str.EndsWith(toChop) ? str.Remove(str.Length - toChop.Length, toChop.Length) : str;
        }

        /// <summary>
        /// Elimina una ocurrencia de una cadena al final de otra.
        /// </summary>
        /// <param name="str">Cadena a comprobar.</param>
        /// <param name="toChop">Valores a cortar.</param>
        /// <returns>
        /// Una cadena que no termine en <paramref name="toChop"/>.
        /// </returns>
        public static string ChopEndAny(this string str, params string[] toChop)
        {
            foreach (var j in toChop)
            {
                if (str.EndsWith(j)) return str.Remove(str.Length - j.Length, j.Length);
            }
            return str;
        }

        /// <summary>
        /// Elimina una ocurrencia de una cadena al principio de otra.
        /// </summary>
        /// <param name="str">Cadena a comprobar.</param>
        /// <param name="toChop">Valor a cortar.</param>
        /// <returns>
        /// Una cadena que no empiece en <paramref name="toChop"/>.
        /// </returns>
        public static string ChopStart(this string str, string toChop)
        {
            return str.StartsWith(toChop) ? str.Remove(0, toChop.Length) : str;
        }

        /// <summary>
        /// Elimina una ocurrencia de una cadena al principio de otra.
        /// </summary>
        /// <param name="str">Cadena a comprobar.</param>
        /// <param name="toChop">Valores a cortar.</param>
        /// <returns>
        /// Una cadena que no empiece en <paramref name="toChop"/>.
        /// </returns>
        public static string ChopStartAny(this string str, params string[] toChop)
        {
            foreach (var j in toChop)
            {
                if (str.StartsWith(j)) return str.Remove(0, j.Length);
            }
            return str;
        }

        /// <summary>
        /// Trunca la longitud de una cadena a un máximo de
        /// <paramref name="length"/> caracteres.
        /// </summary>
        /// <param name="str">Cadena a truncar.</param>
        /// <param name="length">Longitud máxima de la cadena.</param>
        /// <returns></returns>
        public static string Truncate(this string str, int length)
        {
            if (length < 1) throw new ArgumentOutOfRangeException(nameof(length));
            return
                length <= 3 
                ? str.Length > length ? str[0..length] : str
                : str.Length > length 
                    ? $"{str[0..(length-3)]}..." 
                    : str;
        }

        /// <summary>
        /// Separa en líneas de hasta 80 caracteres el contenido de una
        /// cadena larga.
        /// </summary>
        /// <param name="str">Cadena a separar.</param>
        /// <returns>
        /// Un arreglo de cadenas con el contenido de la cadena original 
        /// separado en filas de hasta 80 caracteres.
        /// </returns>
        public static string[] TextWrap(this string str) => TextWrap(str, 80);

        /// <summary>
        /// Separa en líneas el contenido de una cadena larga.
        /// </summary>
        /// <param name="str">Cadena a separar.</param>
        /// <param name="width">
        /// Cantidad de caracteres admitidos por fila. de forma
        /// predeterminada, es de 80 caracteres por columna.
        /// </param>
        /// <returns>
        /// Un arreglo de cadenas con el contenido de la cadena original 
        /// separado en filas.
        /// </returns>
        public static string[] TextWrap(this string str, int width)
        {
            var l = new List<string>
            {
                string.Empty
            };

            foreach (var j in str.Split(' '))
            {
                if (string.IsNullOrEmpty(j)) continue;
                if (l.Last().Length + j.Length > width)
                {
                    l.Add(j);
                }
                else
                {
                    l[^1] += l[^1].IsEmpty() ? j : $" {j}";
                }
            }
            return l.ToArray();
        }

        /// <summary>
        /// Determina si la cadena contiene a cualquiera de los caracteres
        /// especificados.
        /// </summary>
        /// <returns>
        /// <see langword="true" /> si la cadena contiene a cualquiera de los caracteres,
        /// <see langword="false" /> en caso contrario.
        /// </returns>
        /// <param name="stringToCheck">Cadena a verificar.</param>
        /// <param name="chars">Caracteres a buscar.</param>
        [Sugar]
        public static bool ContainsAny(this string stringToCheck, params char[] chars)
        {
            return ContainsAny(stringToCheck, out _, chars);
        }

        /// <summary>
        /// Determina si la cadena contiene a cualquiera de los caracteres
        /// especificados.
        /// </summary>
        /// <returns>
        /// <see langword="true" /> si la cadena contiene a cualquiera de los caracteres,
        /// <see langword="false" /> en caso contrario.
        /// </returns>
        /// <param name="stringToCheck">Cadena a verificar.</param>
        /// <param name="argNum">
        /// Parámetro de salida. Si <paramref name="stringToCheck" /> contiene cualquier
        /// carácter especificado en <paramref name="chars" />, se devolverá el
        /// índice del argumento contenido; en caso contrario, se devuelve
        /// <c>-1</c>.
        /// </param>
        /// <param name="chars">Caracteres a buscar.</param>
        public static bool ContainsAny(this string stringToCheck, out int argNum, params char[] chars)
        {
            return stringToCheck.ContainsAny(chars, out argNum);
        }

        /// <summary>
        /// Determina si la cadena contiene a cualquiera de los caracteres
        /// especificados.
        /// </summary>
        /// <returns>
        /// <see langword="true" /> si la cadena contiene a cualquiera de los caracteres,
        /// <see langword="false" /> en caso contrario.
        /// </returns>
        /// <param name="stringToCheck">Cadena a verificar.</param>
        /// <param name="argNum">
        /// Parámetro de salida. Si <paramref name="stringToCheck" /> contiene cualquier
        /// carácter especificado en <paramref name="chars" />, se devolverá el
        /// índice del argumento contenido; en caso contrario, se devuelve
        /// <c>-1</c>.
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
        /// Determina si la cadena contiene a cualquiera de las cadenas
        /// especificadas.
        /// </summary>
        /// <returns>
        /// <see langword="true" /> si la cadena contiene a cualquiera de los caracteres,
        /// <see langword="false" /> en caso contrario.
        /// </returns>
        /// <param name="stringToCheck">Cadena a verificar.</param>
        /// <param name="strings">Cadenas a buscar.</param>
        [Sugar]
        public static bool ContainsAny(this string stringToCheck, params string[] strings)
        {
            return ContainsAny(stringToCheck, strings, out _);
        }

        /// <summary>
        /// Determina si la cadena contiene a cualquiera de las cadenas
        /// especificadas.
        /// </summary>
        /// <returns>
        /// <see langword="true" /> si la cadena contiene a cualquiera de los caracteres,
        /// <see langword="false" /> en caso contrario.
        /// </returns>
        /// <param name="stringToCheck">Cadena a verificar.</param>
        /// <param name="strings">Cadenas a buscar.</param>
        [Sugar]
        public static bool ContainsAny(this string stringToCheck, IEnumerable<string> strings)
        {
            return ContainsAny(stringToCheck, strings, out _);
        }

        /// <summary>
        /// Determina si la cadena contiene a cualquiera de las cadenas
        /// especificadas.
        /// </summary>
        /// <returns>
        /// <see langword="true" /> si la cadena contiene a cualquiera de los caracteres,
        /// <see langword="false" /> en caso contrario.
        /// </returns>
        /// <param name="stringToCheck">Cadena a verificar.</param>
        /// <param name="argNum">
        /// Parámetro de salida. Si <paramref name="stringToCheck" /> contiene cualquier
        /// carácter especificado en <paramref name="strings" />, se devolverá
        /// el índice del argumento contenido; en caso contrario, se devuelve
        /// <c>-1</c>.
        /// </param>
        /// <param name="strings">Cadenas a buscar.</param>
        public static bool ContainsAny(this string stringToCheck, out int argNum, params string[] strings)
        {
            return stringToCheck.ContainsAny(strings, out argNum);
        }

        /// <summary>
        /// Determina si la cadena contiene a cualquiera de las cadenas
        /// especificadas.
        /// </summary>
        /// <returns>
        /// <see langword="true" /> si la cadena contiene a cualquiera de los caracteres,
        /// <see langword="false" /> en caso contrario.
        /// </returns>
        /// <param name="stringToCheck">Cadena a verificar.</param>
        /// <param name="argNum">
        /// Parámetro de salida. Si <paramref name="stringToCheck" /> contiene cualquier
        /// carácter especificado en <paramref name="strings" />, se devolverá
        /// el índice del argumento contenido; en caso contrario, se devuelve
        /// <c>-1</c>.
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
        /// Verifica si la cadena contiene letras.
        /// </summary>
        /// <returns>
        /// <see langword="true" /> si la cadena contiene letras: de lo contrario,
        /// <see langword="false" />.
        /// </returns>
        /// <param name="stringToCheck">Cadena a comprobar.</param>
        public static bool ContainsLetters(this string stringToCheck)
        {
            return stringToCheck.ContainsAny((St.Constants.AlphaLc.ToUpperInvariant() + St.Constants.AlphaLc).ToCharArray());
        }

        /// <summary>
        /// Verifica si la cadena contiene letras.
        /// </summary>
        /// <returns>
        /// <see langword="true" /> si la cadena contiene letras: de lo contrario,
        /// <see langword="false" />.
        /// </returns>
        /// <param name="stringToCheck">Cadena a comprobar.</param>
        /// <param name="ucase">
        /// Opcional. Especifica el tipo de comprobación a realizar. Si es
        /// <see langword="true" />, Se tomarán en cuenta únicamente los caracteres en
        /// mayúsculas, si es <see langword="false" />, se tomarán en cuenta unicamente  los
        /// caracteres en minúsculas. Si se omite o se establece en <see langword="null" />,
        /// se tomarán en cuenta ambos casos.
        /// </param>
        public static bool ContainsLetters(this string stringToCheck, bool ucase)
        {
            return stringToCheck.ContainsAny((ucase ? St.Constants.AlphaLc.ToUpperInvariant() : St.Constants.AlphaLc).ToCharArray());
        }

        /// <summary>
        /// Comprueba si la cadena contiene números
        /// </summary>
        /// <returns>
        /// <see langword="true" /> si la cadena contiene números; de lo contrario,
        /// <see langword="false" />.
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
        /// Comprueba si un nombre podría tratarse de otro indicado.
        /// </summary>
        /// <returns>
        /// Un valor porcentual que representa la probabilidad de que
        /// <paramref name="checkName" /> haga referencia al nombre
        /// <paramref name="actualName" />.
        /// </returns>
        /// <param name="checkName">Nombre a comprobar.</param>
        /// <param name="actualName">Nombre real conocido.</param>
        /// <exception cref="ArgumentNullException">
        /// Se produce cuando <paramref name="checkName" /> o
        /// <paramref name="actualName" /> son cadenas vacías o <see langword="null" />.
        /// </exception>
        public static float CouldItBe(this string checkName, string actualName)
        {
            return CouldItBe(checkName, actualName, 0.75f);
        }

        /// <summary>
        /// Comprueba si un nombre podría tratarse de otro indicado.
        /// </summary>
        /// <returns>
        /// Un valor que representa la probabilidad de que
        /// <paramref name="checkName" /> haga referencia al nombre
        /// <paramref name="actualName" />.
        /// </returns>
        /// <param name="checkName">Nombre a comprobar.</param>
        /// <param name="actualName">Nombre real conocido.</param>
        /// <param name="tolerance">
        /// Opcional. <see cref="float" /> entre 0.0 y 1.0 que establece el
        /// nivel mínimo de similitud aceptado. si no se especifica, se asume
        /// 75% (0.75).
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Se produce cuando <paramref name="tolerance" /> no es un valor entre
        /// <c>0.0f</c> y <c>1.0f</c>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Se produce cuando <paramref name="checkName" /> o
        /// <paramref name="actualName" /> son cadenas vacías o <see langword="null" />.
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
        /// Cuenta los caracteres que contiene una cadena.
        /// </summary>
        /// <returns>
        /// Un <see cref="int" /> con la cantidad total de caracteres de
        /// <paramref name="chars" /> que aparecen en <paramref name="stringToCheck" />.
        /// </returns>
        /// <param name="stringToCheck">Cadena a comprobar.</param>
        /// <param name="chars">Caracteres a contar.</param>
        public static int CountChars(this string stringToCheck, params char[] chars)
        {
            return chars.Sum(j => stringToCheck.Count(a => a == j));
        }

        /// <summary>
        /// Cuenta los caracteres que contiene una cadena.
        /// </summary>
        /// <returns>
        /// Un <see cref="int" /> con la cantidad total de
        /// caracteres de <paramref name="chars" /> que aparecen en
        /// <paramref name="stringToCheck" />.
        /// </returns>
        /// <param name="stringToCheck">Cadena a comprobar.</param>
        /// <param name="chars">Caracteres a contar.</param>
        [Sugar]
        public static int CountChars(this string stringToCheck, string chars)
        {
            return CountChars(stringToCheck, chars.ToCharArray());
        }

        /// <summary>
        /// Determina si una cadena contiene un valor binario.
        /// </summary>
        /// <param name="str">cadena a comprobar.</param>
        /// <returns>
        /// <see langword="true" /> si la cadena contiene un valor que puede
        /// ser interpretado como un número binario,
        /// <see langword="false" /> en caso contrario.
        /// </returns>
        public static bool IsBinary(this string str)
        {
            if (str.StartsWith("0b", true, CultureInfo.CurrentCulture)
                || str.StartsWith("&b", true, CultureInfo.CurrentCulture)) str = str[2..];
            return str.ToCharArray().All(j => "01".Contains(j));
        }

        /// <summary>
        /// Determina si una cadena está vacía.
        /// </summary>
        /// <returns>
        /// <see langword="true" /> si la cadena está vacía o es <see langword="null" />; de lo
        /// contrario, <see langword="false" />.
        /// </returns>
        /// <param name="stringToCheck">Cadena a comprobar.</param>
        [Sugar]
        public static bool IsEmpty([NotNullWhen(false)]this string? stringToCheck)
        {
            return string.IsNullOrWhiteSpace(stringToCheck);
        }

        /// <summary>
        /// Comprueba si la cadena tiene un formato alfanumérico básico igual
        /// al especificado.
        /// </summary>
        /// <param name="checkString"><see cref="string" /> a comprobar.</param>
        /// <param name="format">
        /// Formato alfanumérico básico contra el cual comparar.
        /// </param>
        /// <returns>
        /// <see langword="true" /> si el formato de la cadena es igual al
        /// especificado, <see langword="false" /> en caso contrario.
        /// </returns>
        public static bool IsFormattedAs(this string checkString, string format)
        {
            return IsFormattedAs(checkString, format, false);
        }

        /// <summary>
        /// Comprueba si la cadena tiene un formato alfanumérico básico igual
        /// al especificado.
        /// </summary>
        /// <param name="checkString"><see cref="string" /> a comprobar.</param>
        /// <param name="format">
        /// Formato alfanumérico básico contra el cual comparar.
        /// </param>
        /// <param name="checkCase">
        /// Si se establece en <see langword="true" />, se hará una evaluación
        /// sensible al Casing de la cadena.
        /// </param>
        /// <returns>
        /// <see langword="true" /> si el formato de la cadena es igual al
        /// especificado, <see langword="false" /> en caso contrario.
        /// </returns>
        public static bool IsFormattedAs(this string checkString, string format, bool checkCase)
        {
            matchRules ??= new (string match, Func<char, bool> test)[] {
                new("09#", p => char.IsDigit(p)),
                new("Bb", p => "01".Contains(p)),
                new("Ff", p => byte.TryParse($"{p}", NumberStyles.HexNumber, null, out _)),
                new("AX", p => char.IsUpper(p)),
                new("ax", p => char.IsLower(p))
            };
            var chkS = checkCase ? checkString : checkString.ToUpperInvariant();
            var fS = checkCase ? format : format.ToUpperInvariant();
            if (chkS.Length != fS.Length) return false;
            for (var j = 0; j < chkS.Length; j++)
            {
                var strChar = chkS[j];
                var fChar = fS[j];
                var rule = matchRules.SingleOrDefault(p => p.match.Contains(fChar)).test ?? (p => fChar == p);
                if (!rule(strChar)) return false;
            }
            return true;
        }

        /// <summary>
        /// Determina si una cadena contiene un valor hexadecimal.
        /// </summary>
        /// <param name="str">cadena a comprobar.</param>
        /// <returns>
        /// <see langword="true" /> si la cadena contiene un valor que puede
        /// ser interpretado como un número hexadecimal,
        /// <see langword="false" /> en caso contrario.
        /// </returns>
        public static bool IsHex(this string str)
        {
            if (str.StartsWith("0x") || str.StartsWith("&h", true, CultureInfo.CurrentCulture)) str = str[2..];
            return str.ToCharArray().All(j => @"0123456789abcdefABCDEF".Contains(j));
        }

        /// <summary>
        /// Obtiene una cadena que contenga la cantidad de caracteres
        /// especificados desde la izquierda de la cadena.
        /// </summary>
        /// <param name="string">
        /// Instancia de <see cref="string" /> a procesar.
        /// </param>
        /// <param name="length">Longitud de caracteres a obtener.</param>
        /// <returns>
        /// Una cadena que contiene los caracteres especificados desde la
        /// izquierda de la cadena.
        /// </returns>
        public static string Left(this string @string, int length)
        {
            if (!length.IsBetween(0, @string.Length))
                throw new ArgumentOutOfRangeException(nameof(length));
            return @string.Substring(0, length);
        }

        /// <summary>
        /// Calcula el porcentaje de similitud entre dos <see cref="string" />.
        /// </summary>
        /// <returns>El porcentaje de similitud entre las dos cadenas.</returns>
        /// <param name="ofString">Cadena A a comparar.</param>
        /// <param name="toString">Cadena B a comparar.</param>
        public static float Likeness(this string ofString, string toString)
        {
            return Likeness(ofString, toString, 3);
        }

        /// <summary>
        /// Calcula el porcentaje de similitud entre dos <see cref="string" />.
        /// </summary>
        /// <returns>El porcentaje de similitud entre las dos cadenas.</returns>
        /// <param name="ofString">Cadena A a comparar.</param>
        /// <param name="toString">Cadena B a comparar.</param>
        /// <param name="tolerance">
        /// Rango de tolerancia de la comparación. Representa la distancia
        /// máxima permitida de cada carácter que todavía hace a las cadenas
        /// similares.
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
        /// Se asegura de devolver <see cref="string.Empty" /> si la cadena
        /// está vacía o es nula.
        /// </summary>
        /// <param name="str">Cadena a devolver.</param>
        /// <returns>
        /// La cadena, o <see cref="string.Empty" /> si la cadena está
        /// vacía o es nula.
        /// </returns>
        public static string OrEmpty(this string? str)
        {
            return OrX(str, string.Empty) ?? string.Empty;
        }

        /// <summary>
        /// Se asegura de devolver <see cref="string.Empty" /> si la cadena
        /// está vacía o es nula.
        /// </summary>
        /// <param name="str">Cadena a devolver.</param>
        /// <param name="notEmptyFormat">
        /// Formato a aplicar en caso de que la cadena no sea
        /// <see cref="string.Empty" />.
        /// </param>
        /// <returns>
        /// La cadena, o <see cref="string.Empty" /> si la cadena está
        /// vacía o es nula.
        /// </returns>
        public static string OrEmpty(this string? str, string notEmptyFormat)
        {
            return !str.IsEmpty() ? string.Format(notEmptyFormat, str) : string.Empty;
        }

        /// <summary>
        /// Se asegura de devolver <see langword="null" /> si la cadena
        /// está vacía.
        /// </summary>
        /// <param name="str">Cadena a devolver.</param>
        /// <returns>
        /// La cadena, o <see langword="null" /> si la cadena está vacía.
        /// </returns>
        public static string? OrNull([NotNullIfNotNull("str")]this string? str)
        {
            return OrX(str, null);
        }

        /// <summary>
        /// Se asegura de devolver <see langword="null" /> si la cadena
        /// está vacía.
        /// </summary>
        /// <param name="str">Cadena a devolver.</param>
        /// <param name="notNullFormat">
        /// Formato a aplicar en caso de que la cadena no sea
        /// <see langword="null" />.
        /// </param>
        /// <returns>
        /// La cadena, o <see langword="null" /> si la cadena está vacía.
        /// </returns>
        public static string? OrNull(this string? str, string notNullFormat)
        {
            return !str.IsEmpty() ? string.Format(notNullFormat, str) : null;
        }

        /// <summary>
        /// Obtiene una cadena que contenga la cantidad de caracteres
        /// especificados desde la izquierda de la cadena.
        /// </summary>
        /// <param name="string">
        /// Instancia de <see cref="string" /> a procesar.
        /// </param>
        /// <param name="length">Longitud de caracteres a obtener.</param>
        /// <returns>
        /// Una cadena que contiene los caracteres especificados desde la
        /// izquierda de la cadena.
        /// </returns>
        public static string Right(this string @string, int length)
        {
            if (!length.IsBetween(0, @string.Length))
                throw new ArgumentOutOfRangeException(nameof(length));
            return @string[length..];
        }

        /// <summary>
        /// Separa cada carácter de una cadena con el <see cref="char" />
        /// especificado.
        /// </summary>
        /// <param name="str">Cadena a procesar.</param>
        /// <param name="separationChar">Carácter de separación a utilizar.</param>
        /// <returns>
        /// Una cadena cuyos caracteres han sido separados con el
        /// <see cref="char" /> especificado.
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
        /// Separa cada carácter de una cadena con un espacio en blanco.
        /// </summary>
        /// <param name="str">Cadena a procesar.</param>
        /// <returns>
        /// Una cadena cuyos caracteres han sido separados con un espacio en blanco.
        /// </returns>
        public static string Spell(this string str)
        {
            return Separate(str, ' ');
        }

        /// <summary>
        /// Determina si la cadena inicia con cualquiera de las cadenas
        /// especificadas.
        /// </summary>
        /// <param name="str">Cadena a comprobar.</param>
        /// <param name="strings">
        /// Colección de cadenas iniciales a determinar.
        /// </param>
        /// <returns>
        /// <see langword="true" /> si la cadena comienza cun cualquiera de
        /// las cadenas especificadas, <see langword="false" /> en caso
        /// contrario.
        /// </returns>
        public static bool StartsWithAny(this string str, IEnumerable<string> strings)
        {
            return strings.Any(str.StartsWith);
        }

        /// <summary>
        /// Determina si la cadena inicia con cualquiera de las cadenas
        /// especificadas.
        /// </summary>
        /// <param name="str">Cadena a comprobar.</param>
        /// <param name="strings">
        /// Colección de cadenas iniciales a determinar.
        /// </param>
        /// <returns>
        /// <see langword="true" /> si la cadena comienza cun cualquiera de
        /// las cadenas especificadas, <see langword="false" /> en caso
        /// contrario.
        /// </returns>
        public static bool StartsWithAny(this string str, params string[] strings)
        {
            return strings.Any(str.StartsWith);
        }

        /// <summary>
        /// Determina si la cadena inicia con cualquiera de las cadenas
        /// especificadas.
        /// </summary>
        /// <param name="str">Cadena a comprobar.</param>
        /// <param name="strings">
        /// Colección de cadenas iniciales a determinar.
        /// </param>
        /// <param name="ignoreCase">
        /// Si se establece en <see langword="true" />, se tomarán en cuenta
        /// mayúsculas y minúsculas como iguales, si se establece en
        /// <see langword="false" />, se tomará en cuenta el casing de los
        /// caracteres de las cadenas.
        /// </param>
        /// <returns>
        /// <see langword="true" /> si la cadena comienza cun cualquiera de
        /// las cadenas especificadas, <see langword="false" /> en caso
        /// contrario.
        /// </returns>
        public static bool StartsWithAny(this string str, IEnumerable<string> strings, bool ignoreCase)
        {
            return strings.Any(p => str.StartsWith(p, ignoreCase, CultureInfo.CurrentCulture));
        }

        /// <summary>
        /// Determina si la cadena inicia con cualquiera de las cadenas
        /// especificadas.
        /// </summary>
        /// <param name="str">Cadena a comprobar.</param>
        /// <param name="strings">
        /// Colección de cadenas iniciales a determinar.
        /// </param>
        /// <param name="ignoreCase">
        /// Si se establece en <see langword="true" />, se tomarán en cuenta
        /// mayúsculas y minúsculas como iguales, si se establece en
        /// <see langword="false" />, se tomará en cuenta el casing de los
        /// caracteres de las cadenas.
        /// </param>
        /// <param name="culture">
        /// Determina la cultura a utilizar para realizar la comprobación.
        /// </param>
        /// <returns>
        /// <see langword="true" /> si la cadena comienza cun cualquiera de
        /// las cadenas especificadas, <see langword="false" /> en caso
        /// contrario.
        /// </returns>
        public static bool StartsWithAny(this string str, IEnumerable<string> strings, bool ignoreCase, CultureInfo culture)
        {
            return strings.Any(p => str.StartsWith(p, ignoreCase, culture));
        }

        /// <summary>
        /// Determina si la cadena inicia con cualquiera de las cadenas
        /// especificadas.
        /// </summary>
        /// <param name="str">Cadena a comprobar.</param>
        /// <param name="strings">
        /// Colección de cadenas iniciales a determinar.
        /// </param>
        /// <param name="comparison">
        /// Especifica la cultura, casing y reglas de ordenado a utilizar
        /// para realizar la comprobación.
        /// </param>
        /// <returns>
        /// <see langword="true" /> si la cadena comienza cun cualquiera de
        /// las cadenas especificadas, <see langword="false" /> en caso
        /// contrario.
        /// </returns>
        public static bool StartsWithAny(this string str, IEnumerable<string> strings, StringComparison comparison)
        {
            return strings.Any(p => str.StartsWith(p, comparison));
        }

        /// <summary>
        /// Realiza una búsqueda tokenizada sobre la cadena especificada.
        /// </summary>
        /// <param name="str">
        /// Cadena en la cual buscar.
        /// </param>
        /// <param name="searchTerms">
        /// Términos de búsqueda.
        /// </param>
        /// <returns>
        /// <see langword="true" /> si la cadena coincide con los términos de
        /// búsqueda especificados, <see langword="false" /> en caso
        /// contrario.
        /// </returns>
        public static bool TokenSearch(this string str, string searchTerms)
        {
            return TokenSearch(str, searchTerms, SearchOptions.Default);
        }

        /// <summary>
        /// Realiza una búsqueda tokenizada sobre la cadena especificada.
        /// </summary>
        /// <param name="str">
        /// Cadena en la cual buscar.
        /// </param>
        /// <param name="searchTerms">
        /// Términos de búsqueda.
        /// </param>
        /// <param name="options">
        /// Opciones de búsqueda.
        /// </param>
        /// <returns>
        /// <see langword="true" /> si la cadena coincide con los términos de
        /// búsqueda y las opciones especificadas, <see langword="false" /> en
        /// caso contrario.
        /// </returns>
        public static bool TokenSearch(this string str, string searchTerms, SearchOptions options)
        {
            return TokenSearch(str, searchTerms, ' ', options);
        }

        /// <summary>
        /// Realiza una búsqueda tokenizada sobre la cadena especificada.
        /// </summary>
        /// <param name="str">
        /// Cadena en la cual buscar.
        /// </param>
        /// <param name="searchTerms">
        /// Términos de búsqueda.
        /// </param>
        /// <param name="separator">
        /// Separador de tokens.
        /// </param>
        /// <param name="options">
        /// Opciones de búsqueda.
        /// </param>
        /// <returns>
        /// <see langword="true" /> si la cadena coincide con los términos de
        /// búsqueda y las opciones especificadas, <see langword="false" /> en
        /// caso contrario.
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
        /// Convierte un <see cref="string" /> en un <see cref="SecureString" />.
        /// </summary>
        /// <param name="string"><see cref="string" /> a convertir.</param>
        /// <returns>
        /// Un <see cref="SecureString" /> que contiene todos los caracteres
        /// originales de la cadena provista.
        /// </returns>
        public static SecureString ToSecureString(this string @string)
        {
            var retVal = new SecureString();
            foreach (var j in @string) retVal.AppendChar(j);
            return retVal;
        }

        /// <summary>
        /// Permite obtener el contenido de un <see cref="string" /> como un
        /// <see cref="Stream" /> utilizando la codificación especificada.
        /// </summary>
        /// <param name="string">Cadena a convertir.</param>
        /// <param name="encoding">Codificación de cadena.</param>
        /// <returns>
        /// Un <see cref="Stream" /> con el contenido de la cadena.
        /// </returns>
        public static Stream ToStream(this string @string, Encoding encoding)
        {
            return new MemoryStream(encoding.GetBytes(@string));
        }

        /// <summary>
        /// Permite obtener el contenido de un <see cref="string" /> como un
        /// <see cref="Stream" /> utilizando la codificación UTF8.
        /// </summary>
        /// <param name="string">Cadena a convertir.</param>
        /// <returns>
        /// Un <see cref="Stream" /> con el contenido de la cadena.
        /// </returns>
        /// <remarks>
        /// A pesar de que las cadenas en .Net Framework son UTF16, ciertas
        /// funciones comunes prefieren trabajar con cadenas codificadas en
        /// UTF8, por lo que este método utiliza dicha codificación para
        /// realizar la conversión.
        /// </remarks>
        public static Stream ToStream(this string @string)
        {
            return ToStream(@string, Encoding.UTF8);
        }

        /// <summary>
        /// Devuelve una nueva cadena sin los caracteres especificados.
        /// </summary>
        /// <param name="string">
        /// Cadena a procesar.
        /// </param>
        /// <param name="chars">
        /// Caracteres a remover.
        /// </param>
        /// <returns>
        /// Una cadena que no contiene ninguno de los caracteres
        /// especificados.
        /// </returns>
        public static string Without(this string @string, params char[] chars)
        {
            return @string.Without(chars.Select(p=>p.ToString()).ToArray());
        }

        /// <summary>
        /// Devuelve una nueva cadena sin las cadenas especificadas.
        /// </summary>
        /// <param name="string">
        /// Cadena a procesar.
        /// </param>
        /// <param name="strings">
        /// Cadenas a remover.
        /// </param>
        /// <returns>
        /// Una cadena que no contiene ninguno de las cadenas
        /// especificadas.
        /// </returns>
        public static string Without(this string @string, params string[] strings)
        {
            foreach (var j in strings)
            {
                @string = @string.Replace(j, string.Empty);
            }
            return @string;
        }

        private static string WildCardToRegular(string value)
        {
            return "^" + Regex.Escape(value).Replace("\\?", ".").Replace("\\*", ".*") + "$";
        }

        private static string? OrX(string? source, string? emptyRetVal)
        {
            return !source.IsEmpty() ? source : emptyRetVal;
        }
    }
}