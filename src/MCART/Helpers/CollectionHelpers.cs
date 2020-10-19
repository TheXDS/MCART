/*
CollectionHelpers.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Este archivo contiene funciones de manipulación de objetos, 

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2020 César Andrés Morgan

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
using System.Threading.Tasks;
using TheXDS.MCART.Math;
using TheXDS.MCART.Types.Extensions;
using static TheXDS.MCART.Misc.Internals;

namespace TheXDS.MCART
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
        /// <exception cref="System.ArgumentNullException">
        /// Se produce si <paramref name="collection"/> es
        /// <see langword="null"/>.
        /// </exception>
        public static bool Or(this IEnumerable<bool> collection)
        {
            NullCheck(collection, nameof(collection));
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
        /// <exception cref="System.ArgumentNullException">
        /// Se produce si <paramref name="collection"/> es
        /// <see langword="null"/>.
        /// </exception>
        public static byte Or(this IEnumerable<byte> collection)
        {
            NullCheck(collection, nameof(collection));
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
        /// <exception cref="System.ArgumentNullException">
        /// Se produce si <paramref name="collection"/> es
        /// <see langword="null"/>.
        /// </exception>
        public static short Or(this IEnumerable<short> collection)
        {
            NullCheck(collection, nameof(collection));
            return collection.Aggregate(default(short), (current, j) => (short)(current | j));
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
        /// <exception cref="System.ArgumentNullException">
        /// Se produce si <paramref name="collection"/> es
        /// <see langword="null"/>.
        /// </exception>
        public static int Or(this IEnumerable<int> collection)
        {
            NullCheck(collection, nameof(collection));
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
        /// <exception cref="System.ArgumentNullException">
        /// Se produce si <paramref name="collection"/> es
        /// <see langword="null"/>.
        /// </exception>
        public static long Or(this IEnumerable<long> collection)
        {
            NullCheck(collection, nameof(collection));
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
        /// <exception cref="System.ArgumentNullException">
        /// Se produce si <paramref name="collection"/> es
        /// <see langword="null"/>.
        /// </exception>
        public static bool And(this IEnumerable<bool> collection)
        {
            NullCheck(collection, nameof(collection));
            return collection.Aggregate(false, (current, j) => current & j);
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
        /// <exception cref="System.ArgumentNullException">
        /// Se produce si <paramref name="collection"/> es
        /// <see langword="null"/>.
        /// </exception>
        public static byte And(this IEnumerable<byte> collection)
        {
            NullCheck(collection, nameof(collection));
            return collection.Aggregate(default(byte), (current, j) => (byte)(current & j));
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
        /// <exception cref="System.ArgumentNullException">
        /// Se produce si <paramref name="collection"/> es
        /// <see langword="null"/>.
        /// </exception>
        public static short And(this IEnumerable<short> collection)
        {
            NullCheck(collection, nameof(collection));
            return collection.Aggregate(default(short), (current, j) => (short)(current & j));
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
        /// <exception cref="System.ArgumentNullException">
        /// Se produce si <paramref name="collection"/> es
        /// <see langword="null"/>.
        /// </exception>
        public static int And(this IEnumerable<int> collection)
        {
            NullCheck(collection, nameof(collection));
            return collection.Aggregate(default(int), (current, j) => current & j);
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
        /// <exception cref="System.ArgumentNullException">
        /// Se produce si <paramref name="collection"/> es
        /// <see langword="null"/>.
        /// </exception>
        public static long And(this IEnumerable<long> collection)
        {
            NullCheck(collection, nameof(collection));
            return collection.Aggregate(default(long), (current, j) => current & j);
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
        /// <exception cref="System.ArgumentNullException">
        /// Se produce si <paramref name="collection"/> es
        /// <see langword="null"/>.
        /// </exception>
        public static bool Xor(this IEnumerable<bool> collection)
        {
            NullCheck(collection, nameof(collection));
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
        /// <exception cref="System.ArgumentNullException">
        /// Se produce si <paramref name="collection"/> es
        /// <see langword="null"/>.
        /// </exception>
        public static byte Xor(this IEnumerable<byte> collection)
        {
            NullCheck(collection, nameof(collection));
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
        /// <exception cref="System.ArgumentNullException">
        /// Se produce si <paramref name="collection"/> es
        /// <see langword="null"/>.
        /// </exception>
        public static short Xor(this IEnumerable<short> collection)
        {
            NullCheck(collection, nameof(collection));
            return collection.Aggregate(default(short), (current, j) => (short)(current ^ j));
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
        /// <exception cref="System.ArgumentNullException">
        /// Se produce si <paramref name="collection"/> es
        /// <see langword="null"/>.
        /// </exception>
        public static int Xor(this IEnumerable<int> collection)
        {
            NullCheck(collection, nameof(collection));
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
        /// <exception cref="System.ArgumentNullException">
        /// Se produce si <paramref name="collection"/> es
        /// <see langword="null"/>.
        /// </exception>
        public static long Xor(this IEnumerable<long> collection)
        {
            NullCheck(collection, nameof(collection));
            return collection.Aggregate(default(long), (current, j) => current ^ j);
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
        public static bool AllEmpty(this IEnumerable<string?> stringArray)
        {
            NullCheck(stringArray, nameof(stringArray));
            return stringArray.All(j => j.IsEmpty());
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
        public static async Task<bool> AllEmpty(this IAsyncEnumerable<string?> stringArray)
        {
            NullCheck(stringArray, nameof(stringArray));
            await foreach (var j in stringArray.ConfigureAwait(false))
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
        /// <param name="stringArray">Cadenas a comprobar.</param>
        /// <exception cref="ArgumentNullException">
        /// Se produce si <paramref name="stringArray"/> es
        /// <see langword="null"/>.
        /// </exception>
        public static bool AnyEmpty(this IEnumerable<string?> stringArray)
        {
            NullCheck(stringArray, nameof(stringArray));
            return stringArray.Any(j => j.IsEmpty());
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
        public static async Task<bool> AnyEmpty(this IAsyncEnumerable<string?> stringArray)
        {
            NullCheck(stringArray, nameof(stringArray));
            await foreach (var j in stringArray.ConfigureAwait(false))
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
        /// <param name="stringArray">Cadenas a comprobar.</param>
        /// <param name="index">
        /// Argumento de salida. Índices de las cadenas vacías encontradas.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Se produce si <paramref name="stringArray"/> es 
        /// <see langword="null"/>.
        /// </exception>
        public static bool AnyEmpty(this IEnumerable<string?> stringArray, out IEnumerable<int> index)
        {
            NullCheck(stringArray, nameof(stringArray));
            var idx = new List<int>();
            var c = 0;
            var found = false;
            foreach (var j in stringArray)
            {
                if (found = j.IsEmpty())
                {
                    idx.Add(c);
                }
                c++;
            }
            index = idx.AsEnumerable();
            return found;
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
        public static bool AnyEmpty(this IEnumerable<string?> stringArray, out int firstIndex)
        {
            NullCheck(stringArray, nameof(stringArray));
            var r = AnyEmpty(stringArray, out IEnumerable<int> indexes);
            var a = indexes.ToArray();
            firstIndex = a.Any() ? a.First() : -1;
            return r;
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
            NullCheck(collection, nameof(collection));
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
            NullCheck(collection, nameof(collection));
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
            NullCheck(collection, nameof(collection));
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
            NullCheck(collection, nameof(collection));
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
            foreach (var j in collection) yield return (j - min) / (double) (max - min);
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
            NullCheck(collection, nameof(collection));
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
            NullCheck(collection, nameof(collection));
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
            foreach (var j in collection) yield return (j - min) / (float) (max - min);
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
            NullCheck(collection, nameof(collection));
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
            NullCheck(collection, nameof(collection));
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
    }
}