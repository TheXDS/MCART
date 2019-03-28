/*
CollectionHelpers.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Este archivo contiene funciones de manipulación de objetos, 

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

#nullable enable

using System.Collections.Generic;
using System.Linq;

namespace TheXDS.MCART
{
    /// <summary>
    ///     Funciones auxiliares para trabajar con colecciones y enumeraciones.
    /// </summary>
    public static class CollectionHelpers
    {
        /// <summary>
        ///     Aplica un operador OR a una colección de valores
        ///     <see cref="bool"/>.
        /// </summary>
        /// <param name="collection">
        ///     Colección a procesar.
        /// </param>
        /// <returns>
        ///     El resultado de aplicar el operador OR a todos los bits de la 
        ///     colección.
        /// </returns>
        public static bool Or(this IEnumerable<bool> collection)
        {
            return collection.Aggregate(false, (current, j) => current | j);
        }

        /// <summary>
        ///     Aplica un operador OR a una colección de valores
        ///     <see cref="byte"/>.
        /// </summary>
        /// <param name="collection">
        ///     Colección a procesar.
        /// </param>
        /// <returns>
        ///     El resultado de aplicar el operador OR a todos los bits de la 
        ///     colección.
        /// </returns>
        public static byte Or(this IEnumerable<byte> collection)
        {
            return collection.Aggregate(default(byte), (current, j) => (byte)(current | j));
        }

        /// <summary>
        ///     Aplica un operador OR a una colección de valores
        ///     <see cref="short"/>.
        /// </summary>
        /// <param name="collection">
        ///     Colección a procesar.
        /// </param>
        /// <returns>
        ///     El resultado de aplicar el operador OR a todos los bits de la 
        ///     colección.
        /// </returns>
        public static short Or(this IEnumerable<short> collection)
        {
            return collection.Aggregate(default(short), (current, j) => (short)(current | j));
        }

        /// <summary>
        ///     Aplica un operador OR a una colección de valores
        ///     <see cref="int"/>.
        /// </summary>
        /// <param name="collection">
        ///     Colección a procesar.
        /// </param>
        /// <returns>
        ///     El resultado de aplicar el operador OR a todos los bits de la 
        ///     colección.
        /// </returns>
        public static int Or(this IEnumerable<int> collection)
        {
            return collection.Aggregate(default(int), (current, j) => current | j);
        }

        /// <summary>
        ///     Aplica un operador OR a una colección de valores
        ///     <see cref="long"/>.
        /// </summary>
        /// <param name="collection">
        ///     Colección a procesar.
        /// </param>
        /// <returns>
        ///     El resultado de aplicar el operador OR a todos los bits de la 
        ///     colección.
        /// </returns>
        public static long Or(this IEnumerable<long> collection)
        {
            return collection.Aggregate(default(long), (current, j) => current | j);
        }

        /// <summary>
        ///     Aplica un operador AND a una colección de valores
        ///     <see cref="bool"/>.
        /// </summary>
        /// <param name="collection">
        ///     Colección a procesar.
        /// </param>
        /// <returns>
        ///     El resultado de aplicar el operador AND a todos los bits de la 
        ///     colección.
        /// </returns>
        public static bool And(this IEnumerable<bool> collection)
        {
            return collection.Aggregate(false, (current, j) => current & j);
        }

        /// <summary>
        ///     Aplica un operador And a una colección de valores
        ///     <see cref="byte"/>.
        /// </summary>
        /// <param name="collection">
        ///     Colección a procesar.
        /// </param>
        /// <returns>
        ///     El resultado de aplicar el operador And a todos los bits de la 
        ///     colección.
        /// </returns>
        public static byte And(this IEnumerable<byte> collection)
        {
            return collection.Aggregate(default(byte), (current, j) => (byte)(current & j));
        }

        /// <summary>
        ///     Aplica un operador And a una colección de valores
        ///     <see cref="short"/>.
        /// </summary>
        /// <param name="collection">
        ///     Colección a procesar.
        /// </param>
        /// <returns>
        ///     El resultado de aplicar el operador And a todos los bits de la 
        ///     colección.
        /// </returns>
        public static short And(this IEnumerable<short> collection)
        {
            return collection.Aggregate(default(short), (current, j) => (short)(current & j));
        }

        /// <summary>
        ///     Aplica un operador And a una colección de valores
        ///     <see cref="int"/>.
        /// </summary>
        /// <param name="collection">
        ///     Colección a procesar.
        /// </param>
        /// <returns>
        ///     El resultado de aplicar el operador And a todos los bits de la 
        ///     colección.
        /// </returns>
        public static int And(this IEnumerable<int> collection)
        {
            return collection.Aggregate(default(int), (current, j) => current & j);
        }

        /// <summary>
        ///     Aplica un operador And a una colección de valores
        ///     <see cref="long"/>.
        /// </summary>
        /// <param name="collection">
        ///     Colección a procesar.
        /// </param>
        /// <returns>
        ///     El resultado de aplicar el operador And a todos los bits de la 
        ///     colección.
        /// </returns>
        public static long And(this IEnumerable<long> collection)
        {
            return collection.Aggregate(default(long), (current, j) => current & j);
        }

        /// <summary>
        ///     Aplica un operador XOR a una colección de valores
        ///     <see cref="bool"/>.
        /// </summary>
        /// <param name="collection">
        ///     Colección a procesar.
        /// </param>
        /// <returns>
        ///     El resultado de aplicar el operador XOR a todos los bits de la 
        ///     colección.
        /// </returns>
        public static bool Xor(this IEnumerable<bool> collection)
        {
            return collection.Aggregate(false, (current, j) => current ^ j);
        }

        /// <summary>
        ///     Aplica un operador Xor a una colección de valores
        ///     <see cref="byte"/>.
        /// </summary>
        /// <param name="collection">
        ///     Colección a procesar.
        /// </param>
        /// <returns>
        ///     El resultado de aplicar el operador Xor a todos los bits de la 
        ///     colección.
        /// </returns>
        public static byte Xor(this IEnumerable<byte> collection)
        {
            return collection.Aggregate(default(byte), (current, j) => (byte)(current ^ j));
        }

        /// <summary>
        ///     Aplica un operador Xor a una colección de valores
        ///     <see cref="short"/>.
        /// </summary>
        /// <param name="collection">
        ///     Colección a procesar.
        /// </param>
        /// <returns>
        ///     El resultado de aplicar el operador Xor a todos los bits de la 
        ///     colección.
        /// </returns>
        public static short Xor(this IEnumerable<short> collection)
        {
            return collection.Aggregate(default(short), (current, j) => (short)(current ^ j));
        }

        /// <summary>
        ///     Aplica un operador Xor a una colección de valores
        ///     <see cref="int"/>.
        /// </summary>
        /// <param name="collection">
        ///     Colección a procesar.
        /// </param>
        /// <returns>
        ///     El resultado de aplicar el operador Xor a todos los bits de la 
        ///     colección.
        /// </returns>
        public static int Xor(this IEnumerable<int> collection)
        {
            return collection.Aggregate(default(int), (current, j) => current ^ j);
        }

        /// <summary>
        ///     Aplica un operador Xor a una colección de valores
        ///     <see cref="long"/>.
        /// </summary>
        /// <param name="collection">
        ///     Colección a procesar.
        /// </param>
        /// <returns>
        ///     El resultado de aplicar el operador Xor a todos los bits de la 
        ///     colección.
        /// </returns>
        public static long Xor(this IEnumerable<long> collection)
        {
            return collection.Aggregate(default(long), (current, j) => current ^ j);
        }

#if !CLSCompliance

        /// <summary>
        ///     Aplica un operador OR a una colección de valores
        ///     <see cref="sbyte"/>.
        /// </summary>
        /// <param name="collection">
        ///     Colección a procesar.
        /// </param>
        /// <returns>
        ///     El resultado de aplicar el operador OR a todos los bits de la 
        ///     colección.
        /// </returns>
        public static sbyte Or(this IEnumerable<sbyte> collection)
        {
            return collection.Aggregate(default(sbyte), (current, j) => (sbyte)(current | j));
        }

        /// <summary>
        ///     Aplica un operador OR a una colección de valores
        ///     <see cref="ushort"/>.
        /// </summary>
        /// <param name="collection">
        ///     Colección a procesar.
        /// </param>
        /// <returns>
        ///     El resultado de aplicar el operador OR a todos los bits de la 
        ///     colección.
        /// </returns>
        public static ushort Or(this IEnumerable<ushort> collection)
        {
            return collection.Aggregate(default(ushort), (current, j) => (ushort)(current | j));
        }

        /// <summary>
        ///     Aplica un operador OR a una colección de valores
        ///     <see cref="uint"/>.
        /// </summary>
        /// <param name="collection">
        ///     Colección a procesar.
        /// </param>
        /// <returns>
        ///     El resultado de aplicar el operador OR a todos los bits de la 
        ///     colección.
        /// </returns>
        public static uint Or(this IEnumerable<uint> collection)
        {
            return collection.Aggregate(default(uint), (current, j) => current | j);
        }

        /// <summary>
        ///     Aplica un operador OR a una colección de valores
        ///     <see cref="ulong"/>.
        /// </summary>
        /// <param name="collection">
        ///     Colección a procesar.
        /// </param>
        /// <returns>
        ///     El resultado de aplicar el operador OR a todos los bits de la 
        ///     colección.
        /// </returns>
        public static ulong Or(this IEnumerable<ulong> collection)
        {
            return collection.Aggregate(default(ulong), (current, j) => current | j);
        }
        
        /// <summary>
        ///     Aplica un operador And a una colección de valores
        ///     <see cref="sbyte"/>.
        /// </summary>
        /// <param name="collection">
        ///     Colección a procesar.
        /// </param>
        /// <returns>
        ///     El resultado de aplicar el operador And a todos los bits de la 
        ///     colección.
        /// </returns>
        public static sbyte And(this IEnumerable<sbyte> collection)
        {
            return collection.Aggregate(default(sbyte), (current, j) => (sbyte)(current & j));
        }

        /// <summary>
        ///     Aplica un operador And a una colección de valores
        ///     <see cref="ushort"/>.
        /// </summary>
        /// <param name="collection">
        ///     Colección a procesar.
        /// </param>
        /// <returns>
        ///     El resultado de aplicar el operador And a todos los bits de la 
        ///     colección.
        /// </returns>
        public static ushort And(this IEnumerable<ushort> collection)
        {
            return collection.Aggregate(default(ushort), (current, j) => (ushort)(current & j));
        }

        /// <summary>
        ///     Aplica un operador And a una colección de valores
        ///     <see cref="uint"/>.
        /// </summary>
        /// <param name="collection">
        ///     Colección a procesar.
        /// </param>
        /// <returns>
        ///     El resultado de aplicar el operador And a todos los bits de la 
        ///     colección.
        /// </returns>
        public static uint And(this IEnumerable<uint> collection)
        {
            return collection.Aggregate(default(uint), (current, j) => current & j);
        }

        /// <summary>
        ///     Aplica un operador And a una colección de valores
        ///     <see cref="ulong"/>.
        /// </summary>
        /// <param name="collection">
        ///     Colección a procesar.
        /// </param>
        /// <returns>
        ///     El resultado de aplicar el operador And a todos los bits de la 
        ///     colección.
        /// </returns>
        public static ulong And(this IEnumerable<ulong> collection)
        {
            return collection.Aggregate(default(ulong), (current, j) => current & j);
        }

        /// <summary>
        ///     Aplica un operador Xor a una colección de valores
        ///     <see cref="sbyte"/>.
        /// </summary>
        /// <param name="collection">
        ///     Colección a procesar.
        /// </param>
        /// <returns>
        ///     El resultado de aplicar el operador Xor a todos los bits de la 
        ///     colección.
        /// </returns>
        public static sbyte Xor(this IEnumerable<sbyte> collection)
        {
            return collection.Aggregate(default(sbyte), (current, j) => (sbyte)(current ^ j));
        }

        /// <summary>
        ///     Aplica un operador Xor a una colección de valores
        ///     <see cref="ushort"/>.
        /// </summary>
        /// <param name="collection">
        ///     Colección a procesar.
        /// </param>
        /// <returns>
        ///     El resultado de aplicar el operador Xor a todos los bits de la 
        ///     colección.
        /// </returns>
        public static ushort Xor(this IEnumerable<ushort> collection)
        {
            return collection.Aggregate(default(ushort), (current, j) => (ushort)(current ^ j));
        }

        /// <summary>
        ///     Aplica un operador Xor a una colección de valores
        ///     <see cref="uint"/>.
        /// </summary>
        /// <param name="collection">
        ///     Colección a procesar.
        /// </param>
        /// <returns>
        ///     El resultado de aplicar el operador Xor a todos los bits de la 
        ///     colección.
        /// </returns>
        public static uint Xor(this IEnumerable<uint> collection)
        {
            return collection.Aggregate(default(uint), (current, j) => current ^ j);
        }

        /// <summary>
        ///     Aplica un operador Xor a una colección de valores
        ///     <see cref="ulong"/>.
        /// </summary>
        /// <param name="collection">
        ///     Colección a procesar.
        /// </param>
        /// <returns>
        ///     El resultado de aplicar el operador Xor a todos los bits de la 
        ///     colección.
        /// </returns>
        public static ulong Xor(this IEnumerable<ulong> collection)
        {
            return collection.Aggregate(default(ulong), (current, j) => current ^ j);
        }

#endif
    }
}