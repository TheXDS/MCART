/*
CollectionHelpers_BitwiseOps.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Este archivo contiene funciones de manipulación de objetos, 

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2023 César Andrés Morgan

Permission is hereby granted, free of charge, to any person obtaining a copy of
this software and associated documentation files (the "Software"), to deal in
the Software without restriction, including without limitation the rights to
use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
of the Software, and to permit persons to whom the Software is furnished to do
so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

namespace TheXDS.MCART.Helpers;

/// <summary>
/// Funciones auxiliares para trabajar con colecciones y enumeraciones.
/// </summary>
public static partial class CollectionHelpers
{
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
    [CLSCompliant(false)]
    public static sbyte And(this IEnumerable<sbyte> collection)
    {
        And_Contract(collection);
        return collection.Aggregate((sbyte)-1, (current, j) => (sbyte)(current & j));
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
    [CLSCompliant(false)]
    public static uint And(this IEnumerable<uint> collection)
    {
        And_Contract(collection);
        return collection.Aggregate(uint.MaxValue, (current, j) => current & j);
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
    [CLSCompliant(false)]
    public static ulong And(this IEnumerable<ulong> collection)
    {
        And_Contract(collection);
        return collection.Aggregate(ulong.MaxValue, (current, j) => current & j);
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
    [CLSCompliant(false)]
    public static ushort And(this IEnumerable<ushort> collection)
    {
        And_Contract(collection);
        return collection.Aggregate(ushort.MaxValue, (current, j) => (ushort)(current & j));
    }

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
    [CLSCompliant(false)]
    public static sbyte Or(this IEnumerable<sbyte> collection)
    {
        Or_Contract(collection);
        return collection.Aggregate(default(sbyte), (current, j) => (sbyte)(current | j));
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
    [CLSCompliant(false)]
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
    [CLSCompliant(false)]
    public static ulong Or(this IEnumerable<ulong> collection)
    {
        Or_Contract(collection);
        return collection.Aggregate(default(ulong), (current, j) => current | j);
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
    [CLSCompliant(false)]
    public static ushort Or(this IEnumerable<ushort> collection)
    {
        Or_Contract(collection);
        return collection.Aggregate(default(ushort), (current, j) => (ushort)(current | j));
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
    [CLSCompliant(false)]
    public static sbyte Xor(this IEnumerable<sbyte> collection)
    {
        Xor_Contract(collection);
        return collection.Aggregate(default(sbyte), (current, j) => (sbyte)(current ^ j));
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
    [CLSCompliant(false)]
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
    [CLSCompliant(false)]
    public static ulong Xor(this IEnumerable<ulong> collection)
    {
        Xor_Contract(collection);
        return collection.Aggregate(default(ulong), (current, j) => current ^ j);
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
    [CLSCompliant(false)]
    public static ushort Xor(this IEnumerable<ushort> collection)
    {
        Xor_Contract(collection);
        return collection.Aggregate(default(ushort), (current, j) => (ushort)(current ^ j));
    }
}
