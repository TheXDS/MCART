/*
CollectionHelpers_nonCLS.cs

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

#if !CLSCompliance
using System.Collections.Generic;
using System.Linq;

namespace TheXDS.MCART.Helpers
{
    /// <summary>
    /// Funciones auxiliares para trabajar con colecciones y enumeraciones.
    /// </summary>
    public static partial class CollectionHelpers
    {
        /// <summary>
        /// Aplica un operador OR a una colección de valores
        /// <see cref="sbyte"/>.
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
        public static sbyte Or(this IEnumerable<sbyte> collection)
        {
            Or_Contract(collection);
            return collection.Aggregate(default(sbyte), (current, j) => (sbyte)(current | j));
        }

        /// <summary>
        /// Aplica un operador OR a una colección de valores
        /// <see cref="ushort"/>.
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
        public static ushort Or(this IEnumerable<ushort> collection)
        {
            Or_Contract(collection);
            return collection.Aggregate(default(ushort), (current, j) => (ushort)(current | j));
        }

        /// <summary>
        /// Aplica un operador OR a una colección de valores
        /// <see cref="uint"/>.
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
        public static uint Or(this IEnumerable<uint> collection)
        {
            Or_Contract(collection);
            return collection.Aggregate(default(uint), (current, j) => current | j);
        }

        /// <summary>
        /// Aplica un operador OR a una colección de valores
        /// <see cref="ulong"/>.
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
        public static ulong Or(this IEnumerable<ulong> collection)
        {
            Or_Contract(collection);
            return collection.Aggregate(default(ulong), (current, j) => current | j);
        }
        
        /// <summary>
        /// Aplica un operador And a una colección de valores
        /// <see cref="sbyte"/>.
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
        public static sbyte And(this IEnumerable<sbyte> collection)
        {
            And_Contract(collection);
            return collection.Aggregate(default(sbyte), (current, j) => (sbyte)(current & j));
        }

        /// <summary>
        /// Aplica un operador And a una colección de valores
        /// <see cref="ushort"/>.
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
        public static ushort And(this IEnumerable<ushort> collection)
        {
            And_Contract(collection);
            return collection.Aggregate(default(ushort), (current, j) => (ushort)(current & j));
        }

        /// <summary>
        /// Aplica un operador And a una colección de valores
        /// <see cref="uint"/>.
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
        public static uint And(this IEnumerable<uint> collection)
        {
            And_Contract(collection);
            return collection.Aggregate(default(uint), (current, j) => current & j);
        }

        /// <summary>
        /// Aplica un operador And a una colección de valores
        /// <see cref="ulong"/>.
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
        public static ulong And(this IEnumerable<ulong> collection)
        {
            And_Contract(collection);
            return collection.Aggregate(default(ulong), (current, j) => current & j);
        }

        /// <summary>
        /// Aplica un operador Xor a una colección de valores
        /// <see cref="sbyte"/>.
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
        public static sbyte Xor(this IEnumerable<sbyte> collection)
        {
            Xor_Contract(collection);
            return collection.Aggregate(default(sbyte), (current, j) => (sbyte)(current ^ j));
        }

        /// <summary>
        /// Aplica un operador Xor a una colección de valores
        /// <see cref="ushort"/>.
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
        public static ushort Xor(this IEnumerable<ushort> collection)
        {
            Xor_Contract(collection);
            return collection.Aggregate(default(ushort), (current, j) => (ushort)(current ^ j));
        }

        /// <summary>
        /// Aplica un operador Xor a una colección de valores
        /// <see cref="uint"/>.
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
        public static uint Xor(this IEnumerable<uint> collection)
        {
            Xor_Contract(collection);
            return collection.Aggregate(default(uint), (current, j) => current ^ j);
        }

        /// <summary>
        /// Aplica un operador Xor a una colección de valores
        /// <see cref="ulong"/>.
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
        public static ulong Xor(this IEnumerable<ulong> collection)
        {
            Xor_Contract(collection);
            return collection.Aggregate(default(ulong), (current, j) => current ^ j);
        }
    }
}
#endif