//
//  Common.cs
//
//  This file is part of MCART
//
//  Author:
//       César Andrés Morgan <xds_xps_ivx@hotmail.com>
//
//  Copyright (c) 2011 - 2017 César Andrés Morgan
//
//  MCART is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  MCART is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
#region Opciones de compilación
// Utilizar números nativos en las funciones que así lo permitan.
#define UseNativeNumbers
#endregion
using System;
using System.Linq;
using System.Collections.Generic;
using St = MCART.Resources.Strings;
using MCART.Attributes;
namespace MCART
{
    /// <summary>
    /// Contiene operaciones comunes de transformación de datos en los
    /// programas, y de algunas comparaciones especiales.
    /// Además, algunas de estas funciones también se implementan como
    /// extensiones.
    /// </summary>
    public static class Common
    {
        /// <summary>
        /// Limpia la cadena especificada.
        /// </summary>
        /// <param name="str">Cadena a limpiar.</param>
        public static void Clear(this string str) => str = string.Empty;
        /// <summary>
        /// Condensa un arreglo de <see cref="string"/>  en una sola cadena.
        /// </summary>
        /// <returns>La cadena condensada.</returns>
        /// <param name="str">Arreglo a condensar.</param>
        /// <param name="separation">
        /// Establece una cadena de separación entre los elementos de la
        /// cadena. Si de omite, se utilizará <c>null</c>.
        /// </param>
        public static string Condense(this IEnumerable<string> str, string separation = null)
        {
            string outp = string.Empty;
            foreach (string j in str) outp += j + separation;
            return outp;
        }
        /// <summary>
        /// Determina si una cadena está vacía.
        /// </summary>
        /// <returns>
        /// <c>true</c> si la cadena está vacía o es <c>null</c>; de lo
        /// contrario, <c>false</c>.
        /// </returns>
        /// <param name="str">string.</param>
        public static bool IsEmpty(this string str) => str.IsNull() || str == string.Empty;
        /// <summary>
        /// Cuenta los caracteres que contiene una cadena.
        /// </summary>
        /// <returns>Un <see cref="int"/> con la cantidad total de
        /// caracteres de <paramref name="chars"/> que aparecen en
        /// <paramref name="check"/>.</returns>
        /// <param name="check">Cadena a comprobar.</param>
        /// <param name="chars">Caracteres a contar.</param>
        public static int CountChars(this string check, char[] chars)
        {
            int c = 0;
            foreach (char j in chars) c += check.Count((a) => a == j);
            return c;
        }
        /// <summary>
        /// Cuenta los caracteres que contiene una cadena.
        /// </summary>
        /// <returns>Un <see cref="int"/> con la cantidad total de
        /// caracteres de <paramref name="chars"/> que aparecen en
        /// <paramref name="check"/>.</returns>
        /// <param name="check">Cadena a comprobar.</param>
        /// <param name="chars">Caracteres a contar.</param>
        public static int CountChars(this string check, string[] chars)
        {
            int c = 0;
            foreach (string j in chars) c += check.Count((a) => a.ToString() == j);
            return c;
        }
        /// <summary>
        /// Condensa una lista en una <see cref="string"/>
        /// </summary>
        /// <returns></returns>
        /// <param name="lst">Lista a condensar. Sus elementos deben ser del
        /// tipo <see cref="string"/>.</param>
        public static string CollectionListed(this IEnumerable<string> lst)
        {
            System.Text.StringBuilder a = new System.Text.StringBuilder();
            foreach (string j in lst) a.AppendLine(j);
            return a.ToString();
        }
        /// <summary>
        /// Intercambia el valor de los objetos especificados.
        /// </summary>
        /// <param name="a">Objeto A.</param>
        /// <param name="b">Objeto B.</param>
        /// <typeparam name="T">Tipo de los argumentos. Puede omitirse con
        /// seguridad.</typeparam>
        public static void Swap<T>(ref T a, ref T b)
        {
            try
            {
                T c = a;
                a = b;
                b = c;
                c = default(T);
            }
            catch (Exception ex)
            {
                throw new InvalidCastException(string.Empty, ex);
            }
        }
        /// <summary>
        /// Comprueba que el valor se encuentre en el rango especificado.
        /// </summary>
        /// <returns>
        /// <c>true</c>si el valor se encuentra entre los especificados; de lo
        /// contrario, <c>false</c>.
        /// </returns>
        /// <param name="a">Valor a comprobar.</param>
        /// <param name="min">Mínimo del rango de valores, inclusive.</param>
        /// <param name="max">Máximo del rango de valores, inclusive.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static bool IsBetween<T>(this T a, T min, T max)
            where T : IComparable, IComparable<T>, IEquatable<T>
        {
            return (a.CompareTo(min) >= 0 && a.CompareTo(max) <= 0);
        }
        /// <summary>
        /// Genera una secuencia de números en el rango especificad.
        /// </summary>
        /// <returns>
        /// Una lista de enteros con la secuencia generada.
        /// </returns>
        /// <param name="top">Valor más alto.</param>
        /// <param name="floor">Valor más bajo.</param>
        /// <param name="stepping">Saltos del secuenciador.</param>
        public static List<int> Sequencer(int top, int floor = 0, int stepping = 1)
        {
            if (floor > top)
            {
                Swap(ref floor, ref top);
                stepping *= -1;
            }
            if (stepping == 0 || System.Math.Abs(stepping) > System.Math.Abs(top - floor))
                throw new ArgumentOutOfRangeException(nameof(stepping));
            List<int> a = new List<int>();
            for (int b = floor; b <= top; b += stepping) a.Add(b);
            return a;
        }
        /// <summary>
        /// Convierte los valores de una colección de elementos 
        /// <see cref="double"/> a porcentajes.
        /// </summary>
        /// <returns>Una colección de <see cref="double"/> con sus valores
        /// expresados en porcentaje.</returns>
        /// <param name="lst">Colección a procesar.</param>
        /// <param name="baseZero">Opcional. si es <c>true</c>, la base de
        /// porcentaje es cero; de lo contrario, se utilizará el valor mínimo
        /// dentro de la colección.</param>
        public static T ToPercent<T>(
            this T lst, bool baseZero = false) where T : IEnumerable<double>
        {
            return ToPercent(lst, baseZero ? 0 : lst.Min(), lst.Max());
        }
        /// <summary>
        /// Convierte los valores de una colección de elementos 
        /// <see cref="double"/> a porcentajes.
        /// </summary>
        /// <returns>Una colección de <see cref="double"/> con sus valores
        /// expresados en porcentaje.</returns>
        /// <param name="lst">Colección a procesar.</param>
        /// <param name="min">Valor que representará 0%.</param>
        /// <param name="max">Valor que representará 100%.</param>
        public static T ToPercent<T>(
            this T lst, double min, double max) where T : IEnumerable<double>
        {
            if (!min.IsValid()) throw new ArgumentException(string.Empty, nameof(min));
            if (!max.IsValid()) throw new ArgumentException(string.Empty, nameof(max));
            List<double> outp = new List<double>();
            foreach (double j in lst)
            {
                if (j.IsValid())
                    outp.Add((j - min) / (max - min).Clamp(1, double.NaN));
                else outp.Add(double.NaN);
            }
            return (T)outp.AsEnumerable();
        }
        /// <summary>
        /// Calcula el porcentaje de similitud entre dos <see cref="string"/>.
        /// </summary>
        /// <returns>El porcentaje de similitud entre las dos cadenas.</returns>
        /// <param name="ofString">Cadena A a comparar.</param>
        /// <param name="toString">Cadena B a comparar.</param>
        /// <param name="tolerance">
        /// Rango de tolerancia de la comparación. Representa la distancia 
        /// máxima permitida de cada caracter que todavía hace a las cadenas 
        /// similares.
        /// </param>
        public static double Likeness(this string ofString, string toString, int tolerance = 3)
        {
            int steps = 0, likes = 0;
            ofString = ofString.ToUpper().PadLeft(toString.Length + tolerance);
            foreach (char c in toString.ToUpper())
            {
                if (ofString.Substring(steps, tolerance).Contains(c)) likes++;
                steps++;
            }
            return likes / (double)steps;
        }
        /// <summary>
        /// Comprueba si un nombre podría tratarse de otro indicado.
        /// </summary>
        /// <returns>
        /// Un valor que representa la probabilidad de que
        /// <paramref name="checkName"/> haga referencia al nombre
        /// <paramref name="actualName"/>.
        /// </returns>
        /// <param name="checkName">Nombre a comprobar.</param>
        /// <param name="actualName">Nombre real conocido.</param>
        /// <param name="tolerance">
        /// Opcional. <see cref="double"/> entre 0.0 y 1.0 que establece el
        /// nivel mínimo de similitud aceptado. si no se especifica, se asume
        /// 75% (0.75).
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Se produce cuando <paramref name="tolerance"/> no es un valor entre
        /// 0.0 y 1.0.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Se produce cuando <paramref name="checkName"/> o
        /// <paramref name="actualName"/> son cadenas vacías o <c>null</c>.
        /// </exception>
        public static double CouldItBe(this string checkName, string actualName, double tolerance = 0.75)
        {
            if (!tolerance.IsBetween(0, 1)) throw new ArgumentOutOfRangeException(nameof(tolerance));
            if (checkName.IsEmpty()) throw new ArgumentNullException(nameof(checkName));
            if (actualName.IsEmpty()) throw new ArgumentNullException(nameof(actualName));
            double l = 0, n = 0;
            int m = 0;
            foreach (string j in checkName.Split(' '))
            {
                m++;
                foreach (string k in actualName.Split(' '))
                {
                    l = j.Likeness(k);
                    if (l > tolerance) n += l;
                }
            }
            return n / m;
        }
        /// <summary>
        /// <see cref="ThunkAttribute"/> de 
        /// <see cref="BitConverter.ToString(byte[])"/> que no incluye guiones.
        /// </summary>
        /// <returns>
        /// La representación hexadecimal del arreglo de <see cref="byte"/>.
        /// </returns>
        /// <param name="arr">Arreglo de bytes a convertir.</param>
        [Thunk] public static string ToHex(this byte[] arr) => BitConverter.ToString(arr).Replace("-", "");
        /// <summary>
        /// Verifica si la cadena contiene letras.
        /// </summary>
        /// <returns>
        /// <c>true</c> si la cadena contiene letras: de lo contrario,
        /// <c>false</c>.
        /// </returns>
        /// <param name="s">Cadena a comprobar.</param>
        /// <param name="ucase">
        /// Opcional. Especifica el tipo de comprobación a realizar. Si es
        /// <c>true</c>, Se tomarán en cuenta únicamente los caracteres en
        /// mayúsculas, si es <c>false</c>, se tomarán en cuenta unicamente  los
        /// caracteres en minúsculas. Si se omite o se establece en <c>null</c>,
        /// se tomarán en cuenta ambos casos.</param>
        public static bool ContainsLetters(this string s, bool? ucase = null)
        {
            if (ucase.HasValue)
            {
                if (ucase.Value)
                    return CountChars(s, St.Alpha.ToUpper().ToCharArray()) > 0;
                return CountChars(s, St.Alpha.ToLower().ToCharArray()) > 0;
            }
            return CountChars(s, (St.Alpha.ToUpper() + St.Alpha.ToLower()).ToCharArray()) > 0;
        }
        /// <summary>
        /// Comprueba si la cadena contiene números
        /// </summary>
        /// <returns>
        /// <c>true</c> si la cadena contiene números; de lo contrario,
        /// <c>false</c>.
        /// </returns>
        /// <param name="s">Cadena a comprobar.</param>
        public static bool ContainsNumbers(this string s)
        {
#if UseNativeNumbers
            return CountChars(s, System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NativeDigits.Condense().ToCharArray()) > 0;
#else
            return CountChars(s, "0123456789".ToCharArray()) > 0;
#endif
        }
        /// <summary>
        /// Convierte un <see cref="byte"/> en su representación hexadecimal.
        /// </summary>
        /// <returns>
        /// La representación hexadecimal de <paramref name="b"/>.
        /// </returns>
        /// <param name="b">El <see cref="byte"/> a convertir.</param>
        public static string ToHex(this byte b) => (new byte[] { b }).ToHex();
    }
}