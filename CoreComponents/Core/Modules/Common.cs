/*
Common.cs

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
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using TheXDS.MCART.Attributes;
using St = TheXDS.MCART.Resources.Strings;

namespace TheXDS.MCART
{
    /// <summary>
    /// Contiene operaciones comunes de transformación de datos en los
    /// programas, y de algunas comparaciones especiales.
    /// </summary>
    /// <remarks>
    /// Algunas de estas funciones también se implementan como extensiones, por
    /// lo que para ser llamadas únicamente es necesario importar el espacio de
    /// nombres <see cref="MCART"/>, y utilizar sintáxis de instancia.
    /// </remarks>
    public static class Common
    {
        /// <summary>
        /// Condensa un arreglo de <see cref="string"/>  en una sola cadena.
        /// </summary>
        /// <returns>
        /// Una cadena compuesta por los elementos de la colección, separados
        /// por el caracter especificado.
        /// </returns>
        /// <param name="stringArray">Arreglo a condensar.</param>
        /// <param name="separation">
        /// Establece una cadena de separación entre los elementos de la
        /// cadena. Si de omite, se utilizará un espacio en blanco.
        /// </param>
        /// <example>
        /// </example>
        public static string Condense(this IEnumerable<string> stringArray, string separation)
        {
            StringBuilder outp = new StringBuilder();
            foreach (string j in stringArray)
            {
                outp.Append(j);
                if (j.IsNot(stringArray.Last())) outp.Append(separation);
            }
            return outp.ToString();
        }
        /// <summary>
        /// Condensa un arreglo de <see cref="string"/>  en una sola cadena.
        /// </summary>
        /// <returns>
        /// Una cadena compuesta por los elementos de la colección, separados
        /// por el caracter especificado.
        /// </returns>
        /// <param name="stringArray">Arreglo a condensar.</param>
        /// <example>
        /// </example>
        public static string Condense(this IEnumerable<string> stringArray) => Condense(stringArray, " ");
        /// <summary>
        /// Obtiene una cadena que contenga la cantidad de caracteres 
        /// especificados desde la izquierda de la cadena.
        /// </summary>
        /// <param name="string">
        /// Instancia de <see cref="string"/> a procesar.
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
        /// Obtiene una cadena que contenga la cantidad de caracteres 
        /// especificados desde la izquierda de la cadena.
        /// </summary>
        /// <param name="string">
        /// Instancia de <see cref="string"/> a procesar.
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
            return @string.Substring(length, @string.Length - length);
        }
        /// <summary>
        /// Determina si una cadena está vacía.
        /// </summary>
        /// <returns>
        /// <c>true</c> si la cadena está vacía o es <c>null</c>; de lo
        /// contrario, <c>false</c>.
        /// </returns>
        /// <param name="stringToCheck">Cadena a comprobar.</param>
        [Thunk] public static bool IsEmpty(this string stringToCheck) => string.IsNullOrWhiteSpace(stringToCheck);
        /// <summary>
        /// Determina si un conjunto de cadenas están vacías.
        /// </summary>
        /// <returns>
        /// <c>true</c> si las cadenas están vacías o son <c>null</c>; de lo
        /// contrario, <c>false</c>.
        /// </returns>
        /// <param name="stringArray">Cadenas a comprobar.</param>
        [Thunk] public static bool AreAllEmpty(params string[] stringArray) => stringArray.All(j => j.IsEmpty());
        /// <summary>
        /// Determina si alguna cadena está vacía.
        /// </summary>
        /// <returns>
        /// <c>true</c> si alguna cadena está vacía o es <c>null</c>; de lo
        /// contrario, <c>false</c>.
        /// </returns>
        /// <param name="stringArray">Cadenas a comprobar.</param>
        [Thunk] public static bool IsAnyEmpty(params string[] stringArray) => stringArray.Any(j => j.IsEmpty());
        /// <summary>
        /// Determina si alguna cadena está vacía.
        /// </summary>
        /// <returns>
        /// <c>true</c> si alguna cadena está vacía o es <c>null</c>; de lo
        /// contrario, <c>false</c>.
        /// </returns>
        /// <param name="stringArray">Cadenas a comprobar.</param>
        /// <param name="index">
        /// Argumento de salida. Índices de las cadenas vacías encontradas.
        /// </param>
        public static bool IsAnyEmpty(out IEnumerable<int> index, params string[] stringArray)
        {
            var idx = new List<int>();
            int c = 0;
            bool found = false;
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
        /// Cuenta los caracteres que contiene una cadena.
        /// </summary>
        /// <returns>
        /// Un <see cref="int"/> con la cantidad total de caracteres de
        /// <paramref name="chars"/> que aparecen en <paramref name="stringToCheck"/>.
        /// </returns>
        /// <param name="stringToCheck">Cadena a comprobar.</param>
        /// <param name="chars">Caracteres a contar.</param>
        public static int CountChars(this string stringToCheck, params char[] chars)
        {
            int c = 0;
            foreach (char j in chars) c += stringToCheck.Count((a) => a == j);
            return c;
        }
        /// <summary>
        /// Cuenta los caracteres que contiene una cadena.
        /// </summary>
        /// <returns>Un <see cref="int"/> con la cantidad total de
        /// caracteres de <paramref name="chars"/> que aparecen en
        /// <paramref name="stringToCheck"/>.</returns>
        /// <param name="stringToCheck">Cadena a comprobar.</param>
        /// <param name="chars">Caracteres a contar.</param>
        [Thunk] public static int CountChars(this string stringToCheck, string chars) => CountChars(stringToCheck, chars.ToCharArray());
        /// <summary>
        /// Determina si la cadena contiene a cualquiera de los caracteres
        /// especificados.
        /// </summary>
        /// <returns>
        /// <c>true</c> si la cadena contiene a cualquiera de los caracteres,
        /// <c>false</c> en caso contrario.
        /// </returns>
        /// <param name="stringToCheck">Cadena a verificar.</param>
        /// <param name="chars">Caracteres a buscar.</param>
        [Thunk] public static bool ContainsAny(this string stringToCheck, params char[] chars) => ContainsAny(stringToCheck, out _, chars);
        /// <summary>
        /// Determina si la cadena contiene a cualquiera de los caracteres
        /// especificados.
        /// </summary>
        /// <returns>
        /// <c>true</c> si la cadena contiene a cualquiera de los caracteres,
        /// <c>false</c> en caso contrario.
        /// </returns>
        /// <param name="stringToCheck">Cadena a verificar.</param>
        /// <param name="argNum">
        /// Parámetro de salida. Si <paramref name="stringToCheck"/> contiene cualquier
        /// caracter especificado en <paramref name="chars"/>, se devolverá el
        /// índice del argumento contenido; en caso contrario, se devuelve 
        /// <c>-1</c>.
        /// </param>
        /// <param name="chars">Caracteres a buscar.</param>
        public static bool ContainsAny(this string stringToCheck, out int argNum, params char[] chars)
        {
            argNum = 0;
            foreach (char j in chars)
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
        /// <c>true</c> si la cadena contiene a cualquiera de los caracteres,
        /// <c>false</c> en caso contrario.
        /// </returns>
        /// <param name="stringToCheck">Cadena a verificar.</param>
        /// <param name="strings">Cadenas a buscar.</param>
        [Thunk] public static bool ContainsAny(this string stringToCheck, params string[] strings) => ContainsAny(stringToCheck, out _, strings);
        /// <summary>
        /// Determina si la cadena contiene a cualquiera de las cadenas
        /// especificadas.
        /// </summary>
        /// <returns>
        /// <c>true</c> si la cadena contiene a cualquiera de los caracteres,
        /// <c>false</c> en caso contrario.
        /// </returns>
        /// <param name="stringToCheck">Cadena a verificar.</param>
        /// <param name="argNum">
        /// Parámetro de salida. Si <paramref name="stringToCheck"/> contiene cualquier
        /// caracter especificado en <paramref name="strings"/>, se devolverá
        /// el índice del argumento contenido; en caso contrario, se devuelve 
        /// <c>-1</c>.
        /// </param>
        /// <param name="strings">Cadenas a buscar.</param>
        public static bool ContainsAny(this string stringToCheck, out int argNum, params string[] strings)
        {
            argNum = 0;
            foreach (string j in strings)
            {
                if (stringToCheck.Contains(j)) return true;
                argNum++;
            }
            argNum = -1;
            return false;
        }
        /// <summary>
        /// Condensa una lista en una <see cref="string"/>
        /// </summary>
        /// <returns>
        /// Una cadena en formato de lista cuyos miembros están separados por
        /// el separador de línea predeterminado del sistema.
        /// </returns>
        /// <param name="collection">Lista a condensar. Sus elementos deben ser del
        /// tipo <see cref="string"/>.
        /// </param>
        /// <remarks>
        /// Este método es equivalente a llamar al método
        /// <see cref="Condense(IEnumerable{string}, string)"/> utilizando
        /// <see cref="Environment.NewLine"/> como cadena de separación.
        /// </remarks>
        public static string Listed(this IEnumerable<string> collection)
        {
#if RatherDRY
            return Condense(collection, Environment.NewLine);
#else
            StringBuilder a = new StringBuilder();
            foreach (string j in collection) a.AppendLine(j);
            return a.ToString();
#endif
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
        /// <param name="value">Valor a comprobar.</param>
        /// <param name="min">Mínimo del rango de valores, inclusive.</param>
        /// <param name="max">Máximo del rango de valores, inclusive.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static bool IsBetween<T>(this T value, T min, T max) where T : IComparable<T>
        {
            return (value.CompareTo(min) >= 0 && value.CompareTo(max) <= 0);
        }
        /// <summary>
        /// Genera una secuencia de números en el rango especificado.
        /// </summary>
        /// <returns>
        /// Una lista de enteros con la secuencia generada.
        /// </returns>
        /// <param name="top">Valor más alto.</param>
        [Thunk] public static IEnumerable<int> Sequence(int top) => Sequence(top, 0, 1);
        /// <summary>
        /// Genera una secuencia de números en el rango especificado.
        /// </summary>
        /// <returns>
        /// Una lista de enteros con la secuencia generada.
        /// </returns>
        /// <param name="top">Valor más alto.</param>
        /// <param name="floor">Valor más bajo.</param>
        [Thunk] public static IEnumerable<int> Sequence(int top, int floor) => Sequence(top, floor, 1);
        /// <summary>
        /// Genera una secuencia de números en el rango especificado.
        /// </summary>
        /// <returns>
        /// Una lista de enteros con la secuencia generada.
        /// </returns>
        /// <param name="top">Valor más alto.</param>
        /// <param name="floor">Valor más bajo.</param>
        /// <param name="stepping">Saltos del secuenciador.</param>
        public static IEnumerable<int> Sequence(int top, int floor, int stepping)
        {
            if (floor > top)
            {
                Swap(ref floor, ref top);
                stepping *= -1;
            }
            if (stepping == 0 || System.Math.Abs(stepping) > System.Math.Abs(top - floor))
                throw new ArgumentOutOfRangeException(nameof(stepping));
            for (int b = floor; b <= top; b += stepping) yield return b;
        }
        /// <summary>
        /// Convierte los valores de una colección de elementos 
        /// <see cref="float"/> a porcentajes.
        /// </summary>
        /// <returns>Una colección de <see cref="float"/> con sus valores
        /// expresados en porcentaje.</returns>
        /// <param name="collection">Colección a procesar.</param>
        [Thunk] public static IEnumerable<float> ToPercent(this IEnumerable<float> collection) => ToPercent(collection, collection.Min(), collection.Max());
        /// <summary>
        /// Convierte los valores de una colección de elementos 
        /// <see cref="float"/> a porcentajes.
        /// </summary>
        /// <returns>Una colección de <see cref="float"/> con sus valores
        /// expresados en porcentaje.</returns>
        /// <param name="collection">Colección a procesar.</param>
        /// <param name="baseZero">Si es <c>true</c>, la base de
        /// porcentaje es cero; de lo contrario, se utilizará el valor mínimo
        /// dentro de la colección.</param>
        [Thunk] public static IEnumerable<float> ToPercent(this IEnumerable<float> collection, bool baseZero) => ToPercent(collection, baseZero ? 0 : collection.Min(), collection.Max());
        /// <summary>
        /// Convierte los valores de una colección de elementos 
        /// <see cref="float"/> a porcentajes.
        /// </summary>
        /// <returns>Una colección de <see cref="float"/> con sus valores
        /// expresados en porcentaje.</returns>
        /// <param name="collection">Colección a procesar.</param>
        /// <param name="max">Valor que representará 100%.</param>
        [Thunk] public static IEnumerable<float> ToPercent(this IEnumerable<float> collection, float max) => ToPercent(collection, 0, max);
        /// <summary>
        /// Convierte los valores de una colección de elementos 
        /// <see cref="float"/> a porcentajes.
        /// </summary>
        /// <returns>Una colección de <see cref="float"/> con sus valores
        /// expresados en porcentaje.</returns>
        /// <param name="collection">Colección a procesar.</param>
        /// <param name="min">Valor que representará 0%.</param>
        /// <param name="max">Valor que representará 100%.</param>
        public static IEnumerable<float> ToPercent(this IEnumerable<float> collection, float min, float max)
        {
            if (!min.IsValid()) throw new ArgumentException(St.XIsInvalid(St.XYQuotes(St.TheValue, min.ToString())), nameof(min));
            if (!max.IsValid()) throw new ArgumentException(St.XIsInvalid(St.XYQuotes(St.TheValue, max.ToString())), nameof(max));
            foreach (float j in collection)
            {
                if (j.IsValid()) yield return (j - min) / (max - min).Clamp(1, float.NaN);
                else yield return float.NaN;
            }
        }
        /// <summary>
        /// Convierte los valores de una colección de elementos 
        /// <see cref="double"/> a porcentajes.
        /// </summary>
        /// <returns>Una colección de <see cref="double"/> con sus valores
        /// expresados en porcentaje.</returns>
        /// <param name="collection">Colección a procesar.</param>
        [Thunk] public static IEnumerable<double> ToPercent(this IEnumerable<double> collection) => ToPercent(collection, collection.Min(), collection.Max());
        /// <summary>
        /// Convierte los valores de una colección de elementos 
        /// <see cref="double"/> a porcentajes.
        /// </summary>
        /// <returns>Una colección de <see cref="double"/> con sus valores
        /// expresados en porcentaje.</returns>
        /// <param name="collection">Colección a procesar.</param>
        /// <param name="baseZero">Si es <c>true</c>, la base de
        /// porcentaje es cero; de lo contrario, se utilizará el valor mínimo
        /// dentro de la colección.</param>
        [Thunk] public static IEnumerable<double> ToPercent(this IEnumerable<double> collection, bool baseZero) => ToPercent(collection, baseZero ? 0 : collection.Min(), collection.Max());
        /// <summary>
        /// Convierte los valores de una colección de elementos 
        /// <see cref="double"/> a porcentajes.
        /// </summary>
        /// <returns>Una colección de <see cref="double"/> con sus valores
        /// expresados en porcentaje.</returns>
        /// <param name="collection">Colección a procesar.</param>
        /// <param name="max">Valor que representará 100%.</param>
        [Thunk] public static IEnumerable<double> ToPercent(this IEnumerable<double> collection, double max) => ToPercent(collection, 0, max);
        /// <summary>
        /// Convierte los valores de una colección de elementos 
        /// <see cref="double"/> a porcentajes.
        /// </summary>
        /// <returns>Una colección de <see cref="double"/> con sus valores
        /// expresados en porcentaje.</returns>
        /// <param name="collection">Colección a procesar.</param>
        /// <param name="min">Valor que representará 0%.</param>
        /// <param name="max">Valor que representará 100%.</param>
        public static IEnumerable<double> ToPercent(this IEnumerable<double> collection, double min, double max)
        {
            if (!min.IsValid()) throw new ArgumentException(St.XIsInvalid(St.XYQuotes(St.TheValue, min.ToString())), nameof(min));
            if (!max.IsValid()) throw new ArgumentException(St.XIsInvalid(St.XYQuotes(St.TheValue, max.ToString())), nameof(max));
            foreach (double j in collection)
            {
                if (j.IsValid()) yield return (j - min) / (max - min).Clamp(1, double.NaN);
                else yield return double.NaN;
            }
        }
        /// <summary>
        /// Convierte los valores de una colección de elementos 
        /// <see cref="int"/> a porcentajes de precisión simple.
        /// </summary>
        /// <returns>Una colección de <see cref="float"/> con sus valores
        /// expresados en porcentaje.</returns>
        /// <param name="collection">Colección a procesar.</param>
        [Thunk] public static IEnumerable<float> ToPercentSingle(this IEnumerable<int> collection) => ToPercentSingle(collection, collection.Min(), collection.Max());
        /// <summary>
        /// Convierte los valores de una colección de elementos 
        /// <see cref="int"/> a porcentajes de precisión simple.
        /// </summary>
        /// <returns>Una colección de <see cref="float"/> con sus valores
        /// expresados en porcentaje.</returns>
        /// <param name="collection">Colección a procesar.</param>
        /// <param name="baseZero">Opcional. si es <c>true</c>, la base de
        /// porcentaje es cero; de lo contrario, se utilizará el valor mínimo
        /// dentro de la colección.</param>
        [Thunk] public static IEnumerable<float> ToPercentSingle(this IEnumerable<int> collection, bool baseZero) => ToPercentSingle(collection, baseZero ? 0 : collection.Min(), collection.Max());
        /// <summary>
        /// Convierte los valores de una colección de elementos 
        /// <see cref="int"/> a porcentajes de precisión simple.
        /// </summary>
        /// <returns>Una colección de <see cref="float"/> con sus valores
        /// expresados en porcentaje.</returns>
        /// <param name="collection">Colección a procesar.</param>
        /// <param name="max">Valor que representará 100%.</param>
        [Thunk] public static IEnumerable<float> ToPercentSingle(this IEnumerable<int> collection, int max) => ToPercentSingle(collection, 0, max);
        /// <summary>
        /// Convierte los valores de una colección de elementos 
        /// <see cref="int"/> a porcentajes de precisión simple.
        /// </summary>
        /// <returns>Una colección de <see cref="float"/> con sus valores
        /// expresados en porcentaje.</returns>
        /// <param name="collection">Colección a procesar.</param>
        /// <param name="min">Valor que representará 0%.</param>
        /// <param name="max">Valor que representará 100%.</param>
        public static IEnumerable<float> ToPercentSingle(this IEnumerable<int> collection, int min, int max)
        {
            if (min == max) throw new InvalidOperationException();
            foreach (int j in collection) yield return (j - min) / (float)(max - min);
        }
        /// <summary>
        /// Convierte los valores de una colección de elementos 
        /// <see cref="int"/> a porcentajes.
        /// </summary>
        /// <returns>Una colección de <see cref="double"/> con sus valores
        /// expresados en porcentaje.</returns>
        /// <param name="collection">Colección a procesar.</param>
        [Thunk] public static IEnumerable<double> ToPercentDouble(this IEnumerable<int> collection) => ToPercentDouble(collection, collection.Min(), collection.Max());
        /// <summary>
        /// Convierte los valores de una colección de elementos 
        /// <see cref="int"/> a porcentajes.
        /// </summary>
        /// <returns>Una colección de <see cref="double"/> con sus valores
        /// expresados en porcentaje.</returns>
        /// <param name="collection">Colección a procesar.</param>
        /// <param name="baseZero">Opcional. si es <c>true</c>, la base de
        /// porcentaje es cero; de lo contrario, se utilizará el valor mínimo
        /// dentro de la colección.</param>
        [Thunk] public static IEnumerable<double> ToPercentDouble(this IEnumerable<int> collection, bool baseZero) => ToPercentDouble(collection, baseZero ? 0 : collection.Min(), collection.Max());
        /// <summary>
        /// Convierte los valores de una colección de elementos 
        /// <see cref="int"/> a porcentajes.
        /// </summary>
        /// <returns>Una colección de <see cref="double"/> con sus valores
        /// expresados en porcentaje.</returns>
        /// <param name="collection">Colección a procesar.</param>
        /// <param name="max">Valor que representará 100%.</param>
        [Thunk] public static IEnumerable<double> ToPercentDouble(this IEnumerable<int> collection, int max) => ToPercentDouble(collection, 0, max);
        /// <summary>
        /// Convierte los valores de una colección de elementos 
        /// <see cref="int"/> a porcentajes.
        /// </summary>
        /// <returns>Una colección de <see cref="double"/> con sus valores
        /// expresados en porcentaje.</returns>
        /// <param name="collection">Colección a procesar.</param>
        /// <param name="min">Valor que representará 0%.</param>
        /// <param name="max">Valor que representará 100%.</param>
        public static IEnumerable<double> ToPercentDouble(this IEnumerable<int> collection, int min, int max)
        {
            if (min == max) throw new InvalidOperationException();
            foreach (int j in collection) yield return (j - min) / (double)(max - min);
        }
        /// <summary>
        /// Calcula el porcentaje de similitud entre dos <see cref="string"/>.
        /// </summary>
        /// <returns>El porcentaje de similitud entre las dos cadenas.</returns>
        /// <param name="ofString">Cadena A a comparar.</param>
        /// <param name="toString">Cadena B a comparar.</param>
        public static float Likeness(this string ofString, string toString) => Likeness(ofString, toString, 3);
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
        public static float Likeness(this string ofString, string toString, int tolerance)
        {
            int steps = 0, likes = 0;
            ofString = ofString.ToUpper().PadLeft(toString.Length + tolerance);
            foreach (char c in toString.ToUpper())
            {
                if (ofString.Substring(steps++, tolerance).Contains(c)) likes++;
            }
            return likes / (float)steps;
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
        /// <exception cref="ArgumentNullException">
        /// Se produce cuando <paramref name="checkName"/> o
        /// <paramref name="actualName"/> son cadenas vacías o <c>null</c>.
        /// </exception>
        public static double CouldItBe(this string checkName, string actualName) => CouldItBe(checkName, actualName, 0.75f);
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
        /// <c>0.0f</c> y <c>1.0f</c>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Se produce cuando <paramref name="checkName"/> o
        /// <paramref name="actualName"/> son cadenas vacías o <c>null</c>.
        /// </exception>
        public static double CouldItBe(this string checkName, string actualName, float tolerance = 0.75f)
        {
            if (!tolerance.IsBetween(0, 1)) throw new ArgumentOutOfRangeException(nameof(tolerance));
            if (checkName.IsEmpty()) throw new ArgumentNullException(nameof(checkName));
            if (actualName.IsEmpty()) throw new ArgumentNullException(nameof(actualName));
            float l = 0, n = 0;
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
        /// <param name="stringToCheck">Cadena a comprobar.</param>
        public static bool ContainsLetters(this string stringToCheck)
        {
            return stringToCheck.ContainsAny((St.Alpha.ToUpperInvariant() + St.Alpha).ToCharArray());
        }
        /// <summary>
        /// Verifica si la cadena contiene letras.
        /// </summary>
        /// <returns>
        /// <c>true</c> si la cadena contiene letras: de lo contrario,
        /// <c>false</c>.
        /// </returns>
        /// <param name="stringToCheck">Cadena a comprobar.</param>
        /// <param name="ucase">
        /// Opcional. Especifica el tipo de comprobación a realizar. Si es
        /// <c>true</c>, Se tomarán en cuenta únicamente los caracteres en
        /// mayúsculas, si es <c>false</c>, se tomarán en cuenta unicamente  los
        /// caracteres en minúsculas. Si se omite o se establece en <c>null</c>,
        /// se tomarán en cuenta ambos casos.</param>
        public static bool ContainsLetters(this string stringToCheck, bool ucase)
        {
            return stringToCheck.ContainsAny((ucase ? St.Alpha.ToUpperInvariant() : St.Alpha).ToCharArray());
        }
        /// <summary>
        /// Comprueba si la cadena contiene números
        /// </summary>
        /// <returns>
        /// <c>true</c> si la cadena contiene números; de lo contrario,
        /// <c>false</c>.
        /// </returns>
        /// <param name="stringToCheck">Cadena a comprobar.</param>
        public static bool ContainsNumbers(this string stringToCheck)
        {
#if NativeNumbers
            return stringToCheck.ContainsAny(System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NativeDigits.Condense(string.Empty).ToCharArray());
#else
            return stringToCheck.ContainsAny("0123456789".ToCharArray());
#endif
        }
        /// <summary>
        /// Convierte un <see cref="byte"/> en su representación hexadecimal.
        /// </summary>
        /// <returns>
        /// La representación hexadecimal de <paramref name="byte"/>.
        /// </returns>
        /// <param name="byte">El <see cref="byte"/> a convertir.</param>
        [Thunk] public static string ToHex(this byte @byte) => @byte.ToString("X");
#pragma warning disable XS0001
        /* -= NOTA =-
		 * Oops! Algunas API de Mono parecen no estar completas, esta directiva
		 * deshabilita la advertencia al compilar desde MonoDevelop.
		 * 
		 * SecureString no provee de funcionalidad de encriptado en Mono, lo que
		 * podría ser inseguro.
         */
        /// <summary>
        /// Convierte un <see cref="SecureString"/> en un
        /// <see cref="string"/>.
        /// </summary>
        /// <param name="value">
        /// <see cref="SecureString"/> a convertir.
        /// </param>
        /// <returns>Un <see cref="string"/> de código administrado.</returns>
        /// <remarks>
        /// El uso de este método NO ESTÁ RECOMENDADO, ya que la conversión al
        /// tipo <see cref="string"/> vence el propósito original de
        /// <see cref="SecureString"/>, y se provee como una
        /// alternativa sencilla, en casos en los que el programa no dependa de
        /// que la confidencialidad de una cadena en particular se deba
        /// mantener durante la ejecución.
        /// </remarks>
        [Dangerous]
        public static string Read(this SecureString value)
        {
            IntPtr valuePtr = IntPtr.Zero;
            try
            {
                valuePtr = Marshal.SecureStringToGlobalAllocUnicode(value);
                return Marshal.PtrToStringUni(valuePtr, value.Length);
            }
            finally { Marshal.ZeroFreeGlobalAllocUnicode(valuePtr); }
        }
        /// <summary>
        /// Convierte un <see cref="SecureString"/> en un
        /// arreglo de <see cref="short"/>.
        /// </summary>
        /// <param name="value">
        /// <see cref="SecureString"/> a convertir.
        /// </param>
        /// <returns>
        /// Un arreglo de <see cref="short"/> de código administrado.
        /// </returns>
        public static short[] ReadInt16(this SecureString value)
        {
            List<short> outp = new List<short>();
            IntPtr valuePtr = IntPtr.Zero;
            try
            {
                valuePtr = Marshal.SecureStringToGlobalAllocUnicode(value);
                for (int i = 0; i < value.Length; i++) outp.Add(Marshal.ReadInt16(valuePtr, i * 2));
                return outp.ToArray();
            }
            finally { Marshal.ZeroFreeGlobalAllocUnicode(valuePtr); }
        }
        /// <summary>
        /// Convierte un <see cref="SecureString"/> en un
        /// arreglo de <see cref="byte"/>.
        /// </summary>
        /// <param name="value">
        /// <see cref="SecureString"/> a convertir.
        /// </param>
        /// <returns>
        /// Un arreglo de <see cref="byte"/> de código administrado.
        /// </returns>
        public static byte[] ReadBytes(this SecureString value)
        {
            List<byte> outp = new List<byte>();
            IntPtr valuePtr = IntPtr.Zero;
            try
            {
                valuePtr = Marshal.SecureStringToGlobalAllocUnicode(value);
                for (int i = 0; i < value.Length * 2; i++) outp.Add(Marshal.ReadByte(valuePtr, i));
                return outp.ToArray();
            }
            finally { Marshal.ZeroFreeGlobalAllocUnicode(valuePtr); }
        }
        /// <summary>
        /// Convierte un <see cref="string"/> en un <see cref="SecureString"/>.
        /// </summary>
        /// <param name="string"><see cref="string"/> a convertir.</param>
        /// <returns>
        /// Un <see cref="SecureString"/> que contiene todos los caracteres 
        /// originales de la cadena provista.
        /// </returns>
        public static SecureString ToSecureString(this string @string)
        {
            var retVal = new SecureString();
            foreach (char j in @string) retVal.AppendChar(j);
            return retVal;
        }
#pragma warning restore XS0001
    }
}