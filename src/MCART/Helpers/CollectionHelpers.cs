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

using System.Collections.Generic;
using System.Linq;
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
    }
}