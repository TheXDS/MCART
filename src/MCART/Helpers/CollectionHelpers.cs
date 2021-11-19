/*
CollectionHelpers.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Este archivo contiene funciones de manipulación de objetos, 

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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Exceptions;
using TheXDS.MCART.Math;
using TheXDS.MCART.Types.Extensions;
using static TheXDS.MCART.Misc.Internals;
using static TheXDS.MCART.Types.Extensions.EnumerableExtensions;
using static TheXDS.MCART.Types.Extensions.TypeExtensions;

namespace TheXDS.MCART.Helpers
{
    /// <summary>
    /// Funciones auxiliares para trabajar con colecciones y enumeraciones.
    /// </summary>
    public static partial class CollectionHelpers
    {
        /// <summary>
        /// Aplica un operador OR a una colección de valores
        /// <see cref="bool"/>.
        /// </summary>
        /// <param name="collection">
        /// Colección a procesar.
        /// </param>
        /// <returns>
        /// El resultado de aplicar el operador OR a todos los bits de la 
        /// colección.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Se produce si <paramref name="collection"/> es
        /// <see langword="null"/>.
        /// </exception>
        public static bool Or(this IEnumerable<bool> collection)
        {
            Or_Contract(collection);
            return collection.Aggregate(false, (current, j) => current | j);
        }

        /// <summary>
        /// Aplica un operador OR a una colección de valores
        /// <see cref="byte"/>.
        /// </summary>
        /// <param name="collection">
        /// Colección a procesar.
        /// </param>
        /// <returns>
        /// El resultado de aplicar el operador OR a todos los bits de la 
        /// colección.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Se produce si <paramref name="collection"/> es
        /// <see langword="null"/>.
        /// </exception>
        public static byte Or(this IEnumerable<byte> collection)
        {
            Or_Contract(collection);
            return collection.Aggregate(default(byte), (current, j) => (byte)(current | j));
        }

        /// <summary>
        /// Aplica un operador OR a una colección de valores
        /// <see cref="short"/>.
        /// </summary>
        /// <param name="collection">
        /// Colección a procesar.
        /// </param>
        /// <returns>
        /// El resultado de aplicar el operador OR a todos los bits de la 
        /// colección.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Se produce si <paramref name="collection"/> es
        /// <see langword="null"/>.
        /// </exception>
        public static short Or(this IEnumerable<short> collection)
        {
            Or_Contract(collection);
            return collection.Aggregate(default(short), (current, j) => (short)(current | j));
        }

        /// <summary>
        /// Aplica un operador OR a una colección de valores
        /// <see cref="char"/>.
        /// </summary>
        /// <param name="collection">
        /// Colección a procesar.
        /// </param>
        /// <returns>
        /// El resultado de aplicar el operador OR a todos los bits de la 
        /// colección.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Se produce si <paramref name="collection"/> es
        /// <see langword="null"/>.
        /// </exception>
        public static char Or(this IEnumerable<char> collection)
        {
            Or_Contract(collection);
            return collection.Aggregate(default(char), (current, j) => (char)(current | j));
        }

        /// <summary>
        /// Aplica un operador OR a una colección de valores
        /// <see cref="int"/>.
        /// </summary>
        /// <param name="collection">
        /// Colección a procesar.
        /// </param>
        /// <returns>
        /// El resultado de aplicar el operador OR a todos los bits de la 
        /// colección.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Se produce si <paramref name="collection"/> es
        /// <see langword="null"/>.
        /// </exception>
        public static int Or(this IEnumerable<int> collection)
        {
            Or_Contract(collection);
            return collection.Aggregate(default(int), (current, j) => current | j);
        }

        /// <summary>
        /// Aplica un operador OR a una colección de valores
        /// <see cref="long"/>.
        /// </summary>
        /// <param name="collection">
        /// Colección a procesar.
        /// </param>
        /// <returns>
        /// El resultado de aplicar el operador OR a todos los bits de la 
        /// colección.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Se produce si <paramref name="collection"/> es
        /// <see langword="null"/>.
        /// </exception>
        public static long Or(this IEnumerable<long> collection)
        {
            Or_Contract(collection);
            return collection.Aggregate(default(long), (current, j) => current | j);
        }

        /// <summary>
        /// Aplica un operador AND a una colección de valores
        /// <see cref="bool"/>.
        /// </summary>
        /// <param name="collection">
        /// Colección a procesar.
        /// </param>
        /// <returns>
        /// El resultado de aplicar el operador AND a todos los bits de la 
        /// colección.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Se produce si <paramref name="collection"/> es
        /// <see langword="null"/>.
        /// </exception>
        public static bool And(this IEnumerable<bool> collection)
        {
            And_Contract(collection);
            return collection.Aggregate(true, (current, j) => current & j);
        }

        /// <summary>
        /// Aplica un operador And a una colección de valores
        /// <see cref="byte"/>.
        /// </summary>
        /// <param name="collection">
        /// Colección a procesar.
        /// </param>
        /// <returns>
        /// El resultado de aplicar el operador And a todos los bits de la 
        /// colección.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Se produce si <paramref name="collection"/> es
        /// <see langword="null"/>.
        /// </exception>
        public static byte And(this IEnumerable<byte> collection)
        {
            And_Contract(collection);
            return collection.Aggregate(byte.MaxValue, (current, j) => (byte)(current & j));
        }

        /// <summary>
        /// Aplica un operador And a una colección de valores
        /// <see cref="short"/>.
        /// </summary>
        /// <param name="collection">
        /// Colección a procesar.
        /// </param>
        /// <returns>
        /// El resultado de aplicar el operador And a todos los bits de la 
        /// colección.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Se produce si <paramref name="collection"/> es
        /// <see langword="null"/>.
        /// </exception>
        public static short And(this IEnumerable<short> collection)
        {
            And_Contract(collection);
            return collection.Aggregate((short)-1, (current, j) => (short)(current & j));
        }

        /// <summary>
        /// Aplica un operador And a una colección de valores
        /// <see cref="char"/>.
        /// </summary>
        /// <param name="collection">
        /// Colección a procesar.
        /// </param>
        /// <returns>
        /// El resultado de aplicar el operador And a todos los bits de la 
        /// colección.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Se produce si <paramref name="collection"/> es
        /// <see langword="null"/>.
        /// </exception>
        public static char And(this IEnumerable<char> collection)
        {
            And_Contract(collection);
            return collection.Aggregate(char.MaxValue, (current, j) => (char)(current & j));
        }

        /// <summary>
        /// Aplica un operador And a una colección de valores
        /// <see cref="int"/>.
        /// </summary>
        /// <param name="collection">
        /// Colección a procesar.
        /// </param>
        /// <returns>
        /// El resultado de aplicar el operador And a todos los bits de la 
        /// colección.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Se produce si <paramref name="collection"/> es
        /// <see langword="null"/>.
        /// </exception>
        public static int And(this IEnumerable<int> collection)
        {
            And_Contract(collection);
            return collection.Aggregate(-1, (current, j) => current & j);
        }

        /// <summary>
        /// Aplica un operador And a una colección de valores
        /// <see cref="long"/>.
        /// </summary>
        /// <param name="collection">
        /// Colección a procesar.
        /// </param>
        /// <returns>
        /// El resultado de aplicar el operador And a todos los bits de la 
        /// colección.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Se produce si <paramref name="collection"/> es
        /// <see langword="null"/>.
        /// </exception>
        public static long And(this IEnumerable<long> collection)
        {
            And_Contract(collection);
            return collection.Aggregate((long)-1, (current, j) => current & j);
        }

        /// <summary>
        /// Aplica un operador XOR a una colección de valores
        /// <see cref="bool"/>.
        /// </summary>
        /// <param name="collection">
        /// Colección a procesar.
        /// </param>
        /// <returns>
        /// El resultado de aplicar el operador XOR a todos los bits de la 
        /// colección.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Se produce si <paramref name="collection"/> es
        /// <see langword="null"/>.
        /// </exception>
        public static bool Xor(this IEnumerable<bool> collection)
        {
            Xor_Contract(collection);
            return collection.Aggregate(false, (current, j) => current ^ j);
        }

        /// <summary>
        /// Aplica un operador Xor a una colección de valores
        /// <see cref="byte"/>.
        /// </summary>
        /// <param name="collection">
        /// Colección a procesar.
        /// </param>
        /// <returns>
        /// El resultado de aplicar el operador Xor a todos los bits de la 
        /// colección.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Se produce si <paramref name="collection"/> es
        /// <see langword="null"/>.
        /// </exception>
        public static byte Xor(this IEnumerable<byte> collection)
        {
            Xor_Contract(collection);
            return collection.Aggregate(default(byte), (current, j) => (byte)(current ^ j));
        }

        /// <summary>
        /// Aplica un operador Xor a una colección de valores
        /// <see cref="short"/>.
        /// </summary>
        /// <param name="collection">
        /// Colección a procesar.
        /// </param>
        /// <returns>
        /// El resultado de aplicar el operador Xor a todos los bits de la 
        /// colección.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Se produce si <paramref name="collection"/> es
        /// <see langword="null"/>.
        /// </exception>
        public static short Xor(this IEnumerable<short> collection)
        {
            Xor_Contract(collection);
            return collection.Aggregate(default(short), (current, j) => (short)(current ^ j));
        }

        /// <summary>
        /// Aplica un operador Xor a una colección de valores
        /// <see cref="char"/>.
        /// </summary>
        /// <param name="collection">
        /// Colección a procesar.
        /// </param>
        /// <returns>
        /// El resultado de aplicar el operador Xor a todos los bits de la 
        /// colección.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Se produce si <paramref name="collection"/> es
        /// <see langword="null"/>.
        /// </exception>
        public static char Xor(this IEnumerable<char> collection)
        {
            Xor_Contract(collection);
            return collection.Aggregate(default(char), (current, j) => (char)(current ^ j));
        }

        /// <summary>
        /// Aplica un operador Xor a una colección de valores
        /// <see cref="int"/>.
        /// </summary>
        /// <param name="collection">
        /// Colección a procesar.
        /// </param>
        /// <returns>
        /// El resultado de aplicar el operador Xor a todos los bits de la 
        /// colección.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Se produce si <paramref name="collection"/> es
        /// <see langword="null"/>.
        /// </exception>
        public static int Xor(this IEnumerable<int> collection)
        {
            Xor_Contract(collection);
            return collection.Aggregate(default(int), (current, j) => current ^ j);
        }

        /// <summary>
        /// Aplica un operador Xor a una colección de valores
        /// <see cref="long"/>.
        /// </summary>
        /// <param name="collection">
        /// Colección a procesar.
        /// </param>
        /// <returns>
        /// El resultado de aplicar el operador Xor a todos los bits de la 
        /// colección.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Se produce si <paramref name="collection"/> es
        /// <see langword="null"/>.
        /// </exception>
        public static long Xor(this IEnumerable<long> collection)
        {
            Xor_Contract(collection);
            return collection.Aggregate(default(long), (current, j) => current ^ j);
        }

        /// <summary>
        /// Determina si un conjunto de cadenas están vacías.
        /// </summary>
        /// <returns>
        /// <see langword="true" /> si las cadenas están vacías o son
        /// <see langword="null" />; de lo contrario, <see langword="false" />.
        /// </returns>
        /// <param name="stringCollection">Cadenas a comprobar.</param>
        /// <exception cref="ArgumentNullException">
        /// Se produce si <paramref name="stringCollection"/> es
        /// <see langword="null"/>.
        /// </exception>
        public static bool AllEmpty(this IEnumerable<string?> stringCollection)
        {
            AllEmpty_Contract(stringCollection);
            return stringCollection.All(j => j.IsEmpty());
        }

        /// <summary>
        /// Determina si un conjunto de cadenas están vacías.
        /// </summary>
        /// <returns>
        /// <see langword="true" /> si las cadenas están vacías o son
        /// <see langword="null" />; de lo contrario, <see langword="false" />.
        /// </returns>
        /// <param name="stringCollection">Cadenas a comprobar.</param>
        /// <exception cref="ArgumentNullException">
        /// Se produce si <paramref name="stringCollection"/> es
        /// <see langword="null"/>.
        /// </exception>
        public static async Task<bool> AllEmpty(this IAsyncEnumerable<string?> stringCollection)
        {
            AllEmpty_Contract(stringCollection);
            await foreach (var j in stringCollection.ConfigureAwait(false))
            {
                if (!j.IsEmpty()) return false;
            }
            return true;
        }

        /// <summary>
        /// Determina si alguna cadena está vacía.
        /// </summary>
        /// <returns>
        /// <see langword="true" /> si alguna cadena está vacía o es
        /// <see langword="null" />; de lo contrario, <see langword="false" />.
        /// </returns>
        /// <param name="stringCollection">Cadenas a comprobar.</param>
        /// <exception cref="ArgumentNullException">
        /// Se produce si <paramref name="stringCollection"/> es
        /// <see langword="null"/>.
        /// </exception>
        public static bool AnyEmpty(this IEnumerable<string?> stringCollection)
        {
            AnyEmpty_Contract(stringCollection);
            return stringCollection.Any(j => j.IsEmpty());
        }

        /// <summary>
        /// Determina si alguna cadena está vacía.
        /// </summary>
        /// <returns>
        /// <see langword="true" /> si alguna cadena está vacía o es
        /// <see langword="null" />; de lo contrario, <see langword="false" />.
        /// </returns>
        /// <param name="stringCollection">Cadenas a comprobar.</param>
        /// <exception cref="ArgumentNullException">
        /// Se produce si <paramref name="stringCollection"/> es
        /// <see langword="null"/>.
        /// </exception>
        public static async Task<bool> AnyEmpty(this IAsyncEnumerable<string?> stringCollection)
        {
            AnyEmpty_Contract(stringCollection);
            await foreach (var j in stringCollection.ConfigureAwait(false))
            {
                if (j.IsEmpty()) return true;
            }
            return false;
        }

        /// <summary>
        /// Determina si alguna cadena está vacía.
        /// </summary>
        /// <returns>
        /// <see langword="true" /> si alguna cadena está vacía o es 
        /// <see langword="null" />; de lo contrario, <see langword="false" />.
        /// </returns>
        /// <param name="stringCollection">Cadenas a comprobar.</param>
        /// <param name="index">
        /// Argumento de salida. Índices de las cadenas vacías encontradas.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Se produce si <paramref name="stringCollection"/> es 
        /// <see langword="null"/>.
        /// </exception>
        public static bool AnyEmpty(this IEnumerable<string?> stringCollection, out IEnumerable<int> index)
        {
            AnyEmpty_Contract(stringCollection);
            var idx = new List<int>();
            var c = 0;
            foreach (var j in stringCollection)
            {
                if (j.IsEmpty()) idx.Add(c);
                c++;
            }
            index = idx.AsEnumerable();
            return idx.Any();
        }

        /// <summary>
        /// Determina si alguna cadena está vacía.
        /// </summary>
        /// <returns>
        /// <see langword="true" /> si alguna cadena está vacía o es
        /// <see langword="null" />; de lo contrario, <see langword="false" />.
        /// </returns>
        /// <param name="stringCollection">Cadenas a comprobar.</param>
        /// <param name="firstIndex">
        /// Argumento de salida. Índice de la primera cadena vacía encontrada.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Se produce si <paramref name="stringCollection"/> es
        /// <see langword="null"/>.
        /// </exception>
        public static bool AnyEmpty(this IEnumerable<string?> stringCollection, out int firstIndex)
        {
            AnyEmpty_Contract(stringCollection);
            var r = AnyEmpty(stringCollection, out IEnumerable<int> indexes);
            var a = indexes.ToArray();
            firstIndex = a.Any() ? a.First() : -1;
            return r;
        }

        /// <summary>
        /// Enumera todas las cadenas no nulas ni vacías de una colección.
        /// </summary>
        /// <param name="stringCollection">
        /// Colección desde la cual obtener las cadenas.
        /// </param>
        /// <returns>
        /// Una enumeración de las cadenas no nulas ni vacías de la colección.
        /// </returns>
        public static IEnumerable<string> NotEmpty(this IEnumerable<string?> stringCollection)
        {
            NotEmpty_Contract(stringCollection);
            foreach (var j in stringCollection)
            {
                if (!j.IsEmpty()) yield return j;
            }
        }

        /// <summary>
        /// Convierte los valores de una colección de elementos
        /// <see cref="float" /> a porcentajes.
        /// </summary>
        /// <returns>
        /// Una colección de <see cref="float" /> con sus valores
        /// expresados en porcentaje.
        /// </returns>
        /// <param name="collection">Colección a procesar.</param>
        /// <exception cref="ArgumentNullException">
        /// Se produce si <paramref name="collection"/> es
        /// <see langword="null"/>.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Se produce si el valor mínimo y máximo de la colección son iguales.
        /// </exception>
        public static IEnumerable<float> ToPercent(this IEnumerable<float> collection)
        {
            ToPercent_Contract(collection);
            var enumerable = collection.ToList();
            return ToPercent(enumerable, enumerable.Min(), enumerable.Max());
        }

        /// <summary>
        /// Convierte los valores de una colección de elementos
        /// <see cref="float" /> a porcentajes.
        /// </summary>
        /// <returns>
        /// Una colección de <see cref="float" /> con sus valores
        /// expresados en porcentaje.
        /// </returns>
        /// <param name="collection">Colección a procesar.</param>
        /// <param name="baseZero">
        /// Si es <see langword="true" />, la base de
        /// porcentaje es cero; de lo contrario, se utilizará el valor mínimo
        /// dentro de la colección.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Se produce si <paramref name="collection"/> es
        /// <see langword="null"/>.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Se produce si el valor mínimo y máximo de la colección son iguales.
        /// </exception>
        public static IEnumerable<float> ToPercent(this IEnumerable<float> collection, in bool baseZero)
        {
            ToPercent_Contract(collection);
            var enumerable = collection.ToList();
            return ToPercent(enumerable, baseZero ? 0 : enumerable.Min(), enumerable.Max());
        }

        /// <summary>
        /// Convierte los valores de una colección de elementos
        /// <see cref="float" /> a porcentajes.
        /// </summary>
        /// <returns>
        /// Una colección de <see cref="float" /> con sus valores
        /// expresados en porcentaje.
        /// </returns>
        /// <param name="collection">Colección a procesar.</param>
        /// <param name="max">Valor que representará 100%.</param>
        /// <exception cref="ArgumentNullException">
        /// Se produce si <paramref name="collection"/> es
        /// <see langword="null"/>.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Se produce si el valor mínimo de la colección y
        /// <paramref name="max"/> son iguales.
        /// </exception>
        public static IEnumerable<float> ToPercent(this IEnumerable<float> collection, in float max)
        {
            return ToPercent(collection, 0, max);
        }

        /// <summary>
        /// Convierte los valores de una colección de elementos
        /// <see cref="float" /> a porcentajes.
        /// </summary>
        /// <returns>
        /// Una colección de <see cref="float" /> con sus valores
        /// expresados en porcentaje.
        /// </returns>
        /// <param name="collection">Colección a procesar.</param>
        /// <param name="min">Valor que representará 0%.</param>
        /// <param name="max">Valor que representará 100%.</param>
        /// <exception cref="ArgumentNullException">
        /// Se produce si <paramref name="collection"/> es
        /// <see langword="null"/>.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Se produce si <paramref name="min"/> y <paramref name="max"/> son
        /// iguales.
        /// </exception>
        public static IEnumerable<float> ToPercent(this IEnumerable<float> collection, float min, float max)
        {
            ToPercent_Contract(collection, min, max);
            foreach (var j in collection)
                if (j.IsValid())
                    yield return (j - min) / (max - min).Clamp(1, float.NaN);
                else
                    yield return float.NaN;
        }

        /// <summary>
        /// Convierte los valores de una colección de elementos
        /// <see cref="float" /> a porcentajes.
        /// </summary>
        /// <returns>
        /// Una colección de <see cref="float" /> con sus valores
        /// expresados en porcentaje.
        /// </returns>
        /// <param name="collection">Colección a procesar.</param>
        /// <param name="max">Valor que representará 100%.</param>
        /// <exception cref="ArgumentNullException">
        /// Se produce si <paramref name="collection"/> es
        /// <see langword="null"/>.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Se produce si el valor mínimo de la colección y
        /// <paramref name="max"/> son iguales.
        /// </exception>
        public static IAsyncEnumerable<float> ToPercent(this IAsyncEnumerable<float> collection, in float max)
        {
            return ToPercent(collection, 0, max);
        }

        /// <summary>
        /// Convierte los valores de una colección de elementos
        /// <see cref="float" /> a porcentajes.
        /// </summary>
        /// <returns>
        /// Una colección de <see cref="float" /> con sus valores
        /// expresados en porcentaje.
        /// </returns>
        /// <param name="collection">Colección a procesar.</param>
        /// <param name="min">Valor que representará 0%.</param>
        /// <param name="max">Valor que representará 100%.</param>
        /// <exception cref="ArgumentNullException">
        /// Se produce si <paramref name="collection"/> es
        /// <see langword="null"/>.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Se produce si <paramref name="min"/> y <paramref name="max"/> son
        /// iguales.
        /// </exception>
        public static async IAsyncEnumerable<float> ToPercent(this IAsyncEnumerable<float> collection, float min, float max)
        {
            ToPercent_Contract(collection, min, max);
            await foreach (var j in collection)
                if (j.IsValid())
                    yield return (j - min) / (max - min).Clamp(1, float.NaN);
                else
                    yield return float.NaN;
        }

        /// <summary>
        /// Convierte los valores de una colección de elementos
        /// <see cref="int" /> a porcentajes.
        /// </summary>
        /// <returns>
        /// Una colección de <see cref="double" /> con sus valores
        /// expresados en porcentaje.
        /// </returns>
        /// <param name="collection">Colección a procesar.</param>
        /// <exception cref="ArgumentNullException">
        /// Se produce si <paramref name="collection"/> es
        /// <see langword="null"/>.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Se produce si el valor mínimo y máximo de la colección son iguales.
        /// </exception>
        public static IEnumerable<double> ToPercentDouble(this IEnumerable<int> collection)
        {
            ToPercentDouble_Contract(collection);
            var enumerable = collection.ToList();
            return ToPercentDouble(enumerable, 0, enumerable.Max());
        }

        /// <summary>
        /// Convierte los valores de una colección de elementos
        /// <see cref="int" /> a porcentajes.
        /// </summary>
        /// <returns>
        /// Una colección de <see cref="double" /> con sus valores
        /// expresados en porcentaje.
        /// </returns>
        /// <param name="collection">Colección a procesar.</param>
        /// <param name="baseZero">
        /// Opcional. si es <see langword="true" />, la base de
        /// porcentaje es cero; de lo contrario, se utilizará el valor mínimo
        /// dentro de la colección.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Se produce si <paramref name="collection"/> es
        /// <see langword="null"/>.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Se produce si el valor mínimo y máximo de la colección son iguales.
        /// </exception>
        public static IEnumerable<double> ToPercentDouble(this IEnumerable<int> collection, in bool baseZero)
        {
            ToPercentDouble_Contract(collection);
            var enumerable = collection.ToList();
            return ToPercentDouble(enumerable, baseZero ? 0 : enumerable.Min(), enumerable.Max());
        }

        /// <summary>
        /// Convierte los valores de una colección de elementos
        /// <see cref="int" /> a porcentajes.
        /// </summary>
        /// <returns>
        /// Una colección de <see cref="double" /> con sus valores
        /// expresados en porcentaje.
        /// </returns>
        /// <param name="collection">Colección a procesar.</param>
        /// <param name="max">Valor que representará 100%.</param>
        /// <exception cref="ArgumentNullException">
        /// Se produce si <paramref name="collection"/> es
        /// <see langword="null"/>.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Se produce si el valor mínimo de la colección y
        /// <paramref name="max"/> son iguales.
        /// </exception>
        public static IEnumerable<double> ToPercentDouble(this IEnumerable<int> collection, in int max)
        {
            return ToPercentDouble(collection, 0, max);
        }

        /// <summary>
        /// Convierte los valores de una colección de elementos
        /// <see cref="int" /> a porcentajes.
        /// </summary>
        /// <returns>
        /// Una colección de <see cref="double" /> con sus valores
        /// expresados en porcentaje.
        /// </returns>
        /// <param name="collection">Colección a procesar.</param>
        /// <param name="min">Valor que representará 0%.</param>
        /// <param name="max">Valor que representará 100%.</param>
        /// <exception cref="ArgumentNullException">
        /// Se produce si <paramref name="collection"/> es
        /// <see langword="null"/>.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Se produce si <paramref name="min"/> y <paramref name="max"/> son
        /// iguales.
        /// </exception>
        public static IEnumerable<double> ToPercentDouble(this IEnumerable<int> collection, int min, int max)
        {
            ToPercent_Contract(collection, min, max);
            foreach (var j in collection) yield return (j - min) / (double)(max - min);
        }

        /// <summary>
        /// Convierte los valores de una colección de elementos
        /// <see cref="int" /> a porcentajes.
        /// </summary>
        /// <returns>
        /// Una colección de <see cref="double" /> con sus valores
        /// expresados en porcentaje.
        /// </returns>
        /// <param name="collection">Colección a procesar.</param>
        /// <param name="max">Valor que representará 100%.</param>
        /// <exception cref="ArgumentNullException">
        /// Se produce si <paramref name="collection"/> es
        /// <see langword="null"/>.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Se produce si el valor mínimo de la colección y
        /// <paramref name="max"/> son iguales.
        /// </exception>
        public static IAsyncEnumerable<double> ToPercentDouble(this IAsyncEnumerable<int> collection, in int max)
        {
            return ToPercentDouble(collection, 0, max);
        }

        /// <summary>
        /// Convierte los valores de una colección de elementos
        /// <see cref="int" /> a porcentajes.
        /// </summary>
        /// <returns>
        /// Una colección de <see cref="double" /> con sus valores
        /// expresados en porcentaje.
        /// </returns>
        /// <param name="collection">Colección a procesar.</param>
        /// <param name="min">Valor que representará 0%.</param>
        /// <param name="max">Valor que representará 100%.</param>
        /// <exception cref="ArgumentNullException">
        /// Se produce si <paramref name="collection"/> es
        /// <see langword="null"/>.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Se produce si <paramref name="min"/> y <paramref name="max"/> son
        /// iguales.
        /// </exception>
        public static async IAsyncEnumerable<double> ToPercentDouble(this IAsyncEnumerable<int> collection, int min, int max)
        {
            ToPercent_Contract(collection, min, max);
            await foreach (var j in collection) yield return (j - min) / (double)(max - min);
        }

        /// <summary>
        /// Convierte los valores de una colección de elementos
        /// <see cref="int" /> a porcentajes de precisión simple.
        /// </summary>
        /// <returns>
        /// Una colección de <see cref="float" /> con sus valores
        /// expresados en porcentaje.
        /// </returns>
        /// <param name="collection">Colección a procesar.</param>
        /// <exception cref="ArgumentNullException">
        /// Se produce si <paramref name="collection"/> es
        /// <see langword="null"/>.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Se produce si el valor mínimo y máximo de la colección son iguales.
        /// </exception>
        public static IEnumerable<float> ToPercentSingle(this IEnumerable<int> collection)
        {
            ToPercentSingle_Contract(collection);
            var enumerable = collection.ToList();
            return ToPercentSingle(enumerable, 0, enumerable.Max());
        }

        /// <summary>
        /// Convierte los valores de una colección de elementos
        /// <see cref="int" /> a porcentajes de precisión simple.
        /// </summary>
        /// <returns>
        /// Una colección de <see cref="float" /> con sus valores
        /// expresados en porcentaje.
        /// </returns>
        /// <param name="collection">Colección a procesar.</param>
        /// <param name="baseZero">
        /// Opcional. si es <see langword="true" />, la base de
        /// porcentaje es cero; de lo contrario, se utilizará el valor mínimo
        /// dentro de la colección.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Se produce si <paramref name="collection"/> es
        /// <see langword="null"/>.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Se produce si el valor mínimo y máximo de la colección son iguales.
        /// </exception>
        public static IEnumerable<float> ToPercentSingle(this IEnumerable<int> collection, in bool baseZero)
        {
            ToPercentSingle_Contract(collection);
            var enumerable = collection.ToList();
            return ToPercentSingle(enumerable, baseZero ? 0 : enumerable.Min(), enumerable.Max());
        }

        /// <summary>
        /// Convierte los valores de una colección de elementos
        /// <see cref="int" /> a porcentajes de precisión simple.
        /// </summary>
        /// <returns>
        /// Una colección de <see cref="float" /> con sus valores
        /// expresados en porcentaje.
        /// </returns>
        /// <param name="collection">Colección a procesar.</param>
        /// <param name="max">Valor que representará 100%.</param>
        /// <exception cref="ArgumentNullException">
        /// Se produce si <paramref name="collection"/> es
        /// <see langword="null"/>.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Se produce si el valor mínimo de la colección y
        /// <paramref name="max"/> son iguales.
        /// </exception>
        public static IEnumerable<float> ToPercentSingle(this IEnumerable<int> collection, in int max)
        {
            return ToPercentSingle(collection, 0, max);
        }

        /// <summary>
        /// Convierte los valores de una colección de elementos
        /// <see cref="int" /> a porcentajes de precisión simple.
        /// </summary>
        /// <returns>
        /// Una colección de <see cref="float" /> con sus valores
        /// expresados en porcentaje.
        /// </returns>
        /// <param name="collection">Colección a procesar.</param>
        /// <param name="min">Valor que representará 0%.</param>
        /// <param name="max">Valor que representará 100%.</param>
        /// <exception cref="ArgumentNullException">
        /// Se produce si <paramref name="collection"/> es
        /// <see langword="null"/>.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Se produce si <paramref name="min"/> y <paramref name="max"/> son
        /// iguales.
        /// </exception>
        public static IEnumerable<float> ToPercentSingle(this IEnumerable<int> collection, int min, int max)
        {
            ToPercent_Contract(collection, min, max);
            foreach (var j in collection) yield return (j - min) / (float)(max - min);
        }

        /// <summary>
        /// Convierte los valores de una colección de elementos
        /// <see cref="int" /> a porcentajes de precisión simple.
        /// </summary>
        /// <returns>
        /// Una colección de <see cref="float" /> con sus valores
        /// expresados en porcentaje.
        /// </returns>
        /// <param name="collection">Colección a procesar.</param>
        /// <param name="max">Valor que representará 100%.</param>
        /// <exception cref="ArgumentNullException">
        /// Se produce si <paramref name="collection"/> es
        /// <see langword="null"/>.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Se produce si el valor mínimo de la colección y
        /// <paramref name="max"/> son iguales.
        /// </exception>
        public static IAsyncEnumerable<float> ToPercentSingle(this IAsyncEnumerable<int> collection, in int max)
        {
            return ToPercentSingle(collection, 0, max);
        }

        /// <summary>
        /// Convierte los valores de una colección de elementos
        /// <see cref="int" /> a porcentajes de precisión simple.
        /// </summary>
        /// <returns>
        /// Una colección de <see cref="float" /> con sus valores
        /// expresados en porcentaje.
        /// </returns>
        /// <param name="collection">Colección a procesar.</param>
        /// <param name="min">Valor que representará 0%.</param>
        /// <param name="max">Valor que representará 100%.</param>
        /// <exception cref="ArgumentNullException">
        /// Se produce si <paramref name="collection"/> es
        /// <see langword="null"/>.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Se produce si <paramref name="min"/> y <paramref name="max"/> son
        /// iguales.
        /// </exception>
        public static async IAsyncEnumerable<float> ToPercentSingle(this IAsyncEnumerable<int> collection, int min, int max)
        {
            ToPercent_Contract(collection, min, max);
            await foreach (var j in collection) yield return (j - min) / (float)(max - min);
        }

        /// <summary>
        /// Convierte los valores de una colección de elementos
        /// <see cref="double" /> a porcentajes.
        /// </summary>
        /// <returns>
        /// Una colección de <see cref="double" /> con sus valores
        /// expresados en porcentaje.
        /// </returns>
        /// <param name="collection">Colección a procesar.</param>
        /// <exception cref="ArgumentNullException">
        /// Se produce si <paramref name="collection"/> es
        /// <see langword="null"/>.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Se produce si el valor mínimo y máximo de la colección son iguales.
        /// </exception>
        public static IEnumerable<double> ToPercent(this IEnumerable<double> collection)
        {
            ToPercent_Contract(collection);
            var enumerable = collection.ToList();
            return ToPercent(enumerable, enumerable.Min(), enumerable.Max());
        }

        /// <summary>
        /// Convierte los valores de una colección de elementos
        /// <see cref="double" /> a porcentajes.
        /// </summary>
        /// <returns>
        /// Una colección de <see cref="double" /> con sus valores
        /// expresados en porcentaje.
        /// </returns>
        /// <param name="collection">Colección a procesar.</param>
        /// <param name="baseZero">
        /// Si es <see langword="true" />, la base de
        /// porcentaje es cero; de lo contrario, se utilizará el valor mínimo
        /// dentro de la colección.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Se produce si <paramref name="collection"/> es
        /// <see langword="null"/>.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Se produce si el valor mínimo y máximo de la colección son iguales.
        /// </exception>
        public static IEnumerable<double> ToPercent(this IEnumerable<double> collection, in bool baseZero)
        {
            ToPercent_Contract(collection);
            var enumerable = collection.ToList();
            return ToPercent(enumerable, baseZero ? 0 : enumerable.Min(), enumerable.Max());
        }

        /// <summary>
        /// Convierte los valores de una colección de elementos
        /// <see cref="double" /> a porcentajes.
        /// </summary>
        /// <returns>
        /// Una colección de <see cref="double" /> con sus valores
        /// expresados en porcentaje.
        /// </returns>
        /// <param name="collection">Colección a procesar.</param>
        /// <param name="max">Valor que representará 100%.</param>
        /// <exception cref="ArgumentNullException">
        /// Se produce si <paramref name="collection"/> es
        /// <see langword="null"/>.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Se produce si el valor mínimo de la colección y
        /// <paramref name="max"/> son iguales.
        /// </exception>
        public static IEnumerable<double> ToPercent(this IEnumerable<double> collection, in double max)
        {
            return ToPercent(collection, 0, max);
        }

        /// <summary>
        /// Convierte los valores de una colección de elementos
        /// <see cref="double" /> a porcentajes.
        /// </summary>
        /// <returns>
        /// Una colección de <see cref="double" /> con sus valores
        /// expresados en porcentaje.
        /// </returns>
        /// <param name="collection">Colección a procesar.</param>
        /// <param name="min">Valor que representará 0%.</param>
        /// <param name="max">Valor que representará 100%.</param>
        /// <exception cref="ArgumentNullException">
        /// Se produce si <paramref name="collection"/> es
        /// <see langword="null"/>.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Se produce si <paramref name="min"/> y <paramref name="max"/> son
        /// iguales.
        /// </exception>
        public static IEnumerable<double> ToPercent(this IEnumerable<double> collection, double min, double max)
        {
            ToPercent_Contract(collection, min, max);
            foreach (var j in collection)
                if (j.IsValid())
                    yield return (j - min) / (max - min).Clamp(1, double.NaN);
                else
                    yield return double.NaN;
        }

        /// <summary>
        /// Convierte los valores de una colección de elementos
        /// <see cref="double" /> a porcentajes.
        /// </summary>
        /// <returns>
        /// Una colección de <see cref="double" /> con sus valores
        /// expresados en porcentaje.
        /// </returns>
        /// <param name="collection">Colección a procesar.</param>
        /// <param name="max">Valor que representará 100%.</param>
        /// <exception cref="ArgumentNullException">
        /// Se produce si <paramref name="collection"/> es
        /// <see langword="null"/>.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Se produce si el valor mínimo de la colección y
        /// <paramref name="max"/> son iguales.
        /// </exception>
        public static IAsyncEnumerable<double> ToPercent(this IAsyncEnumerable<double> collection, in double max)
        {
            return ToPercent(collection, 0, max);
        }

        /// <summary>
        /// Convierte los valores de una colección de elementos
        /// <see cref="double" /> a porcentajes.
        /// </summary>
        /// <returns>
        /// Una colección de <see cref="double" /> con sus valores
        /// expresados en porcentaje.
        /// </returns>
        /// <param name="collection">Colección a procesar.</param>
        /// <param name="min">Valor que representará 0%.</param>
        /// <param name="max">Valor que representará 100%.</param>
        /// <exception cref="ArgumentNullException">
        /// Se produce si <paramref name="collection"/> es
        /// <see langword="null"/>.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Se produce si <paramref name="min"/> y <paramref name="max"/> son
        /// iguales.
        /// </exception>
        public static async IAsyncEnumerable<double> ToPercent(this IAsyncEnumerable<double> collection, double min, double max)
        {
            ToPercent_Contract(collection, min, max);
            await foreach (var j in collection)
                if (j.IsValid())
                    yield return (j - min) / (max - min).Clamp(1, double.NaN);
                else
                    yield return double.NaN;
        }

        /// <summary>
        /// Determina si todos los objetos son <see langword="null" />.
        /// </summary>
        /// <returns>
        /// <see langword="true" />, si todos los objetos son <see langword="null" />; de lo contrario,
        /// <see langword="false" />.
        /// </returns>
        /// <param name="collection">Objetos a comprobar.</param>
        /// <exception cref="ArgumentNullException">
        /// Se produce si <paramref name="collection"/> es
        /// <see langword="null"/>.
        /// </exception>
        public static bool AreAllNull(this IEnumerable<object?> collection)
        {
            NullCheck(collection, nameof(collection));
            return collection.All(p => p is null);
        }

        /// <summary>
        /// Obtiene todos los métodos estáticos con firma compatible con el
        /// delegado especificado.
        /// </summary>
        /// <typeparam name="T">
        /// Delegado a utilizar como firma a comprobar.
        /// </typeparam>
        /// <param name="methods">
        /// Colección de métodos en la cual realizar la búsqueda.
        /// </param>
        /// <returns>
        /// Una enumeración de todos los métodos que tienen una firma
        /// compatible con <typeparamref name="T" />.
        /// </returns>
        public static IEnumerable<T> WithSignature<T>(this IEnumerable<MethodInfo> methods) where T : notnull, Delegate
        {
            foreach (var j in methods)
            {
                if (Objects.TryCreateDelegate<T>(j, out var d))
                {
                    yield return d ?? throw new TamperException();
                }
            }
        }

        /// <summary>
        /// Obtiene todos los métodos de instancia con firma compatible con el
        /// delegado especificado.
        /// </summary>
        /// <typeparam name="T">
        /// Delegado a utilizar como firma a comprobar.
        /// </typeparam>
        /// <param name="methods">
        /// Colección de métodos en la cual realizar la búsqueda.
        /// </param>
        /// <param name="instance">
        /// Instancia del objeto sobre el cual construir los delegados.
        /// </param>
        /// <returns>
        /// Una enumeración de todos los métodos que tienen una firma
        /// compatible con <typeparamref name="T" />.
        /// </returns>
        public static IEnumerable<T> WithSignature<T>(this IEnumerable<MethodInfo> methods, object instance) where T : notnull, Delegate
        {
            foreach (var j in methods)
            {
                if (Objects.TryCreateDelegate<T>(j, instance, out var d))
                {
                    yield return d ?? throw new TamperException();
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de los tipos de los objetos especificados.
        /// </summary>
        /// <param name="objects">
        /// Objetos a partir de los cuales generar la colección de tipos.
        /// </param>
        /// <returns>
        /// Una lista compuesta por los tipos de los objetos provistos.
        /// </returns>
        public static IEnumerable<Type> ToTypes(this IEnumerable objects)
        {
            foreach (var j in objects) if (j is not null) yield return j.GetType();
        }

        /// <summary>
        /// Enumera el valor de todas las propiedades que devuelvan valores de
        /// tipo <typeparamref name="T" />.
        /// </summary>
        /// <typeparam name="T">Tipo de propiedades a obtener.</typeparam>
        /// <param name="properties">
        /// Colección de propiedades a analizar.
        /// </param>
        /// <param name="instance">
        /// Instancia desde la cual obtener las propiedades.
        /// </param>
        /// <returns>
        /// Una enumeración de todos los valores de tipo
        /// <typeparamref name="T" /> de la instancia.
        /// </returns>
        public static IEnumerable<T> PropertiesOf<T>(this IEnumerable<PropertyInfo> properties, object? instance)
        {
            return
                from j in properties.Where(p => p.CanRead)
                where j.PropertyType.Implements(typeof(T))
                select (T)j.GetMethod!.Invoke(instance, Array.Empty<object>())!;
        }

        /// <summary>
        /// Enumera el valor de todas las propiedades estáticas que devuelvan
        /// valores de tipo <typeparamref name="T" />.
        /// </summary>
        /// <typeparam name="T">Tipo de propiedades a obtener.</typeparam>
        /// <param name="properties">
        /// Colección de propiedades a analizar.
        /// </param>
        /// <returns>
        /// Una enumeración de todos los valores de tipo
        /// <typeparamref name="T" />.
        /// </returns>
        public static IEnumerable<T> PropertiesOf<T>(this IEnumerable<PropertyInfo> properties)
        {
            return PropertiesOf<T>(properties, null);
        }

        /// <summary>
        /// Determina si cualquiera de los objetos es <see langword="null" />.
        /// </summary>
        /// <returns>
        /// <see langword="true" />, si cualquiera de los objetos es <see langword="null" />; de lo
        /// contrario, <see langword="false" />.
        /// </returns>
        /// <param name="x">Objetos a comprobar.</param>
        public static bool IsAnyNull(this IEnumerable<object?>? x)
        {
            return x?.Any(p => p is null) ?? true;
        }

        /// <summary>
        /// Determina si cualquiera de los objetos es <see langword="null" />.
        /// </summary>
        /// <returns>
        /// Un enumerador con los índices de los objetos que son <see langword="null" />.
        /// </returns>
        /// <param name="collection">Colección de objetos a comprobar.</param>
        public static IEnumerable<int> WhichAreNull(this IEnumerable<object?> collection)
        {
            WhichAreNull_Contract(collection);
            var c = 0;
            foreach (var j in collection)
            {
                if (j is null) yield return c;
                c++;
            }
        }

        /// <summary>
        /// Obtiene una lista de tipos asignables a partir de la interfaz o clase base
        /// especificada dentro del <see cref="AppDomain" /> especificado.
        /// </summary>
        /// <typeparam name="T">Interfaz o clase base a buscar.</typeparam>
        /// <param name="assemblies">
        /// Colección de ensamblados en la cual realizar la búsqueda.
        /// </param>
        /// <returns>
        /// Una lista de tipos de las clases que implementan a la interfaz
        /// o que heredan a la clase base <typeparamref name="T" /> dentro
        /// de <paramref name="assemblies" />.
        /// </returns>
        /// <remarks>
        /// Esta función obtiene todos los tipos (privados y públicos)
        /// definidos dentro de todos los ensamblados dentro de la
        /// colección especificada. Para obtener únicamente aquellos tipos
        /// exportados públicamente, utilice
        /// <see cref="Objects.PublicTypes(Type)"/>,
        /// <see cref="Objects.PublicTypes(Type, AppDomain)"/>,
        /// <see cref="Objects.PublicTypes{T}()"/> o
        /// <see cref="Objects.PublicTypes{T}(AppDomain)"/>.
        /// </remarks>
        [Sugar]
        public static IEnumerable<Type> GetTypes<T>(this IEnumerable<Assembly> assemblies)
        {
            return typeof(T).Derivates(assemblies);
        }

        /// <summary>
        /// Obtiene una lista de tipos asignables a partir de la interfaz o clase base
        /// especificada dentro del <see cref="AppDomain" /> especificado.
        /// </summary>
        /// <typeparam name="T">Interfaz o clase base a buscar.</typeparam>
        /// <param name="assemblies">
        /// Colección de ensamblados en la cual realizar la búsqueda.
        /// </param>
        /// <param name="instantiablesOnly">
        /// Si se establece en <see langword="true" />, únicamente se incluirán aquellos tipos instanciables.
        /// <see langword="false" /> hará que se devuelvan todos los tipos coincidientes.
        /// </param>
        /// <returns>
        /// Una lista de tipos de las clases que implementan a la interfaz o que heredan a la clase base
        /// <typeparamref name="T" /> dentro del dominio predeterminado.
        /// </returns>
        /// <remarks>
        /// Esta función obtiene todos los tipos (privados y públicos)
        /// definidos dentro de todos los ensamblados dentro de la
        /// colección especificada. Para obtener únicamente aquellos tipos
        /// exportados públicamente, utilice
        /// <see cref="Objects.PublicTypes(Type)"/>,
        /// <see cref="Objects.PublicTypes(Type, AppDomain)"/>,
        /// <see cref="Objects.PublicTypes{T}()"/> o
        /// <see cref="Objects.PublicTypes{T}(AppDomain)"/>.
        /// </remarks>
        public static IEnumerable<Type> GetTypes<T>(this IEnumerable<Assembly> assemblies, bool instantiablesOnly)
        {
            Type? TryType(Type k)
            {
                try
                {
                    return typeof(T).IsAssignableFrom(k)
                        || (!instantiablesOnly || !(k.IsInterface || k.IsAbstract || !k.GetConstructors().Any()))
                        ? k : null;
                }
                catch { return null; }
            }
            IEnumerable<Type?> TryAssembly(Assembly j)
            {
                try
                {
                    return j.GetTypes().Select(TryType);
                }
                catch
                {
                    return Array.Empty<Type>();
                }
            }
            return assemblies.SelectMany(TryAssembly).NotNull();
        }
    }
}