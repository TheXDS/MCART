/*
Algebra.cs

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
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Resources;

namespace TheXDS.MCART.Math
{
    /// <summary>
    /// Contiene series, operaciones, ecuaciones y constantes matemáticas adicionales.
    /// </summary>
    public static partial class Algebra
    {
        private static int[]? _primes;

        private static int[] KnownPrimes => _primes ??= ReadKnownPrimes();

        /// <summary>
        /// Comprueba si un número es primo mediante prueba y error.
        /// </summary>
        /// <returns>
        /// <see langword="true" />si el número es primo,
        /// <see langword="false" /> en caso contrario.
        /// </returns>
        /// <param name="number">Número a comprobar.</param>
        public static bool IsPrime(this in long number)
        {
            if (number == 1) return false;
            if (number < KnownPrimes[^1] && KnownPrimes.Contains((int)number)) return true;

            foreach (var prime in KnownPrimes)
            {
                if (number % prime == 0) return false;
            }
            
            var l = (int)System.Math.Sqrt(number);
            for (int k = KnownPrimes[^1] + 2; k <= l; k += 2)
            {
                if (number % k == 0) return false;
            }

            return true;
        }

        /// <summary>
        /// Comprueba si un número es primo mediante prueba y error, ejecutando
        /// la operación en todos los procesadores del sistema.
        /// </summary>
        /// <param name="number">Número a comprobar.</param>
        /// <returns>
        /// <see langword="true" />si el número es primo,
        /// <see langword="false" /> en caso contrario.
        /// </returns>
        public static bool IsPrimeMp(this long number)
        {
            if (number == 1) return false;
            if (number < KnownPrimes[^1] && KnownPrimes.Contains((int)number)) return true;

            var part = Partitioner.Create(KnownPrimes);            
            bool prime = true;

            void TestIfPrime(int j, ParallelLoopState loop)
            {
                if (number % j == 0)
                {
                    loop.Break();
                    prime = false;
                }
            }

            void TestIfPrime2(int j, ParallelLoopState loop)
            {
                if (number % ((j * 2) + 1) == 0)
                {
                    loop.Break();
                    prime = false;
                }
            }

            Parallel.ForEach(part, TestIfPrime);
            if (prime)
            {
                var l = (int)System.Math.Sqrt(number);
                Parallel.For(KnownPrimes[^1] / 2, l, TestIfPrime2);
            }            
            return prime;
        }

        /// <summary>
        /// Comprueba si un número es primo.
        /// </summary>
        /// <returns>
        /// <see langword="true" />si el número es primo, <see langword="false" /> en caso contrario.
        /// </returns>
        /// <param name="number">Número a comprobar.</param>
        public static bool IsPrime(this in int number)
        {
            return ((long) number).IsPrime();
        }

        /// <summary>
        /// Comprueba si un número es primo.
        /// </summary>
        /// <returns>
        /// <see langword="true" />si el número es primo, <see langword="false" /> en caso contrario.
        /// </returns>
        /// <param name="number">Número a comprobar.</param>
        public static bool IsPrime(this in short number)
        {
            return ((long) number).IsPrime();
        }

        /// <summary>
        /// Comprueba si un número es primo.
        /// </summary>
        /// <returns>
        /// <see langword="true" />si el número es primo, <see langword="false" /> en caso contrario.
        /// </returns>
        /// <param name="number">Número a comprobar.</param>
        public static bool IsPrime(this in byte number)
        {
            return ((long) number).IsPrime();
        }

        /// <summary>
        /// Comprueba si un número es primo.
        /// </summary>
        /// <returns>
        /// <see langword="true" />si el número es primo, <see langword="false" /> en caso contrario.
        /// </returns>
        /// <param name="number">Número a comprobar.</param>
        public static bool IsPrimeMp(this in int number)
        {
            return ((long)number).IsPrimeMp();
        }

        /// <summary>
        /// Comprueba si un número es primo.
        /// </summary>
        /// <returns>
        /// <see langword="true" />si el número es primo, <see langword="false" /> en caso contrario.
        /// </returns>
        /// <param name="number">Número a comprobar.</param>
        public static bool IsPrimeMp(this in short number)
        {
            return ((long)number).IsPrimeMp();
        }

        /// <summary>
        /// Comprueba si un número es primo.
        /// </summary>
        /// <returns>
        /// <see langword="true" />si el número es primo, <see langword="false" /> en caso contrario.
        /// </returns>
        /// <param name="number">Número a comprobar.</param>
        public static bool IsPrimeMp(this in byte number)
        {
            return ((long)number).IsPrimeMp();
        }

        /// <summary>
        /// Calcula la potencia de dos más cercana mayor o igual al número
        /// </summary>
        /// <param name="value">Número de entrada. Se buscará una potencia de dos mayor o igual a este valor.</param>
        /// <returns>Un valor <see cref="long" /> que es resultado de la operación.</returns>
        public static long Nearest2Pow(in int value)
        {
            long c = 1;
            while (!(c >= value)) c <<= 1;
            return c;
        }

        /// <summary>
        /// Determina si el valor es una potencia de 2.
        /// </summary>
        /// <param name="value">Valor a comprobar.</param>
        /// <returns>
        /// <see langword="true"/> si el valor es una potencia de 2,
        /// <see langword="false"/> en caso contrario.
        /// </returns>
        public static bool IsTwoPow(in byte value) => value.BitCount() == 1;

        /// <summary>
        /// Determina si el valor es una potencia de 2.
        /// </summary>
        /// <param name="value">Valor a comprobar.</param>
        /// <returns>
        /// <see langword="true"/> si el valor es una potencia de 2,
        /// <see langword="false"/> en caso contrario.
        /// </returns>
        public static bool IsTwoPow(in short value) => value.BitCount() == 1;

        /// <summary>
        /// Determina si el valor es una potencia de 2.
        /// </summary>
        /// <param name="value">Valor a comprobar.</param>
        /// <returns>
        /// <see langword="true"/> si el valor es una potencia de 2,
        /// <see langword="false"/> en caso contrario.
        /// </returns>
        public static bool IsTwoPow(in int value) => value.BitCount() == 1;

        /// <summary>
        /// Determina si el valor es una potencia de 2.
        /// </summary>
        /// <param name="value">Valor a comprobar.</param>
        /// <returns>
        /// <see langword="true"/> si el valor es una potencia de 2,
        /// <see langword="false"/> en caso contrario.
        /// </returns>
        public static bool IsTwoPow(in long value) => value.BitCount() == 1;

        /// <summary>
        /// Devuelve el primer múltiplo de <paramref name="multiplier" /> que es mayor que <paramref name="value" />
        /// </summary>
        /// <param name="value">Número objetivo</param>
        /// <param name="multiplier">
        /// Base multiplicativa. Esta función devolverá un múltiplo de este valor que sea mayor a
        /// <paramref name="value" />
        /// </param>
        /// <returns>
        /// Un <see cref="double" /> que es el primer múltiplo de <paramref name="multiplier" /> que es mayor que
        /// <paramref name="value" />
        /// </returns>
        public static double NearestMultiplyUp(in double value, in double multiplier)
        {
            double a = 1;
            if (!ArePositive(value, multiplier)) return a;
            while (!(a > value))
                a *= multiplier;
            return a;
        }

        /// <summary>
        /// Devuelve <see langword="true" /> si todos los números son positivos.
        /// </summary>
        /// <param name="values">números a comprobar.</param>
        /// <returns>
        /// <see langword="true" /> si todos los números de la colección son positivos,
        /// <see langword="false" /> en caso contrario.
        /// </returns>
        public static bool ArePositive<T>(params T[] values) where T : struct, IComparable<T>
        {
            return ArePositive(values.AsEnumerable());
        }

        /// <summary>
        /// Devuelve <see langword="true" /> si todos los números son negativos.
        /// </summary>
        /// <param name="values">números a comprobar.</param>
        /// <returns>
        /// <see langword="true" /> si todos los números de la colección son negativos,
        /// <see langword="false" /> en caso contrario.
        /// </returns>
        public static bool AreNegative<T>(params T[] values) where T : struct, IComparable<T>
        {
            return AreNegative(values.AsEnumerable());
        }

        /// <summary>
        /// Devuelve <see langword="true" /> si todos los números son iguales a cero.
        /// </summary>
        /// <typeparam name="T">
        /// Tipo de elementos a comprobar.
        /// </typeparam>
        /// <param name="values">números a comprobar.</param>
        /// <returns>
        /// <see langword="true" /> si todos los números de la colección son iguales a
        /// cero, <see langword="false" /> en caso contrario.
        /// </returns>
        public static bool AreZero<T>(params T[] values) where T : struct, IComparable<T>
        {
            return AreZero(values.AsEnumerable());
        }
        
        /// <summary>
        /// Devuelve <see langword="true" /> si todos los números son negativos.
        /// </summary>
        /// <param name="values">números a comprobar.</param>
        /// <returns>
        /// <see langword="true" /> si todos los números de la colección son negativos,
        /// <see langword="false" /> en caso contrario.
        /// </returns>
        public static bool AreNegative<T>(this IEnumerable<T> values) where T : struct, IComparable<T>
        {
            return values.All(j => j.CompareTo(default) < 0);
        }
        
        /// <summary>
        /// Devuelve <see langword="true" /> si todos los números son iguales a cero.
        /// </summary>
        /// <typeparam name="T">
        /// Tipo de elementos a comprobar.
        /// </typeparam>
        /// <param name="values">números a comprobar.</param>
        /// <returns>
        /// <see langword="true" /> si todos los números de la colección son iguales a
        /// cero, <see langword="false" /> en caso contrario.
        /// </returns>
        public static bool AreZero<T>(this IEnumerable<T> values) where T : struct, IComparable<T>
        {
            return values.All(j => j.CompareTo(default) == 0);
        }

        /// <summary>
        /// Devuelve <see langword="true" /> si todos los números son positivos.
        /// </summary>
        /// <param name="values">números a comprobar.</param>
        /// <returns>
        /// <see langword="true" /> si todos los números de la colección son positivos,
        /// <see langword="false" /> en caso contrario.
        /// </returns>
        public static bool ArePositive<T>(this IEnumerable<T> values) where T : struct, IComparable<T>
        {
            return values.All(j => j.CompareTo(default) > 0);
        }
        
        /// <summary>
        /// Determina si un <see cref="double" /> es un número entero.
        /// </summary>
        /// <param name="value">Valor a comprobar.</param>
        /// <returns><see langword="true" /> si el valor es entero; de lo contrario, <see langword="false" /></returns>
        public static bool IsWhole(this in double value)
        {
            return !value.ToString(CultureInfo.InvariantCulture).Contains(".");
        }

        /// <summary>
        /// Determina si un <see cref="double" /> es un número real operable.
        /// </summary>
        /// <param name="value"><see cref="double" /> a comprobar.</param>
        /// <returns>
        /// <see langword="true" /> si <paramref name="value" /> es un número real
        /// <see cref="double" /> operable, en otras palabras, si no es igual a
        /// <see cref="double.NaN" />, <see cref="double.PositiveInfinity" /> o
        /// <see cref="double.NegativeInfinity" />; en cuyo caso se devuelve
        /// <see langword="false" />.
        /// </returns>
        public static bool IsValid(this in double value)
        {
            return !(double.IsNaN(value) || double.IsInfinity(value));
        }

        /// <summary>
        /// Determina si un <see cref="float" /> es un número real operable.
        /// </summary>
        /// <param name="value"><see cref="float" /> a comprobar.</param>
        /// <returns>
        /// <see langword="true" /> si <paramref name="value" /> es un número real
        /// <see cref="float" /> operable, en otras palabras, si no es igual a
        /// <see cref="float.NaN" />, <see cref="float.PositiveInfinity" /> o
        /// <see cref="float.NegativeInfinity" />; en cuyo caso se devuelve
        /// <see langword="false" />.
        /// </returns>
        public static bool IsValid(this in float value)
        {
            return !(float.IsNaN(value) || float.IsInfinity(value));
        }

        /// <summary>
        /// Determina si una colección de <see cref="double" /> son números
        /// reales operables.
        /// </summary>
        /// <param name="values">
        /// Colección  de <see cref="double" /> a comprobar.
        /// </param>
        /// <returns>
        /// <see langword="true" /> si todos los elementos de <paramref name="values" /> son
        /// números operables, en otras palabras, si no son NaN o Infinito; en
        /// caso contrario, se devuelve <see langword="false" />.
        /// </returns>
        public static bool AreValid(params double[] values)
        {
            return AreValid(values.AsEnumerable());
        }

        /// <summary>
        /// Determina si una colección de <see cref="float" /> son números
        /// reales operables.
        /// </summary>
        /// <param name="values">
        /// Colección  de <see cref="float" /> a comprobar.
        /// </param>
        /// <returns>
        /// <see langword="true" /> si todos los elementos de <paramref name="values" /> son
        /// números operables, en otras palabras, si no son NaN o Infinito; en
        /// caso contrario, se devuelve <see langword="false" />.
        /// </returns>
        public static bool AreValid(params float[] values)
        {
            return AreValid(values.AsEnumerable());
        }

        /// <summary>
        /// Determina si una colección de <see cref="float" /> son números
        /// reales operables.
        /// </summary>
        /// <param name="values">
        /// Colección  de <see cref="float" /> a comprobar.
        /// </param>
        /// <returns>
        /// <see langword="true" /> si todos los elementos de <paramref name="values" /> son
        /// números operables, en otras palabras, si no son NaN o Infinito; en
        /// caso contrario, se devuelve <see langword="false" />.
        /// </returns>
        public static bool AreValid(IEnumerable<float> values)
        {
            return values.All(p => IsValid(p));
        }

        /// <summary>
        /// Determina si una colección de <see cref="double" /> son números
        /// reales operables.
        /// </summary>
        /// <param name="values">
        /// Colección  de <see cref="double" /> a comprobar.
        /// </param>
        /// <returns>
        /// <see langword="true" /> si todos los elementos de <paramref name="values" /> son
        /// números operables, en otras palabras, si no son NaN o Infinito; en
        /// caso contrario, se devuelve <see langword="false" />.
        /// </returns>
        public static bool AreValid(IEnumerable<double> values)
        {
            return values.All(p => IsValid(p));
        }

        /// <summary>
        /// Devuelve <see langword="true" /> si todos los números son distintos de cero.
        /// </summary>
        /// <typeparam name="T">
        /// Tipo de elementos a comprobar.
        /// </typeparam>
        /// <param name="x">números a comprobar.</param>
        /// <returns>
        /// <see langword="true" /> si todos los números de la colección son distintos de
        /// cero, <see langword="false" /> en caso contrario.
        /// </returns>
        public static bool AreNotZero<T>(params T[] x) where T : struct, IComparable<T>
        {
            return AreNotZero(x.AsEnumerable());
        }

        /// <summary>
        /// Devuelve <see langword="true" /> si todos los números son distintos de cero.
        /// </summary>
        /// <typeparam name="T">
        /// Tipo de elementos a comprobar.
        /// </typeparam>
        /// <param name="x">números a comprobar.</param>
        /// <returns>
        /// <see langword="true" /> si todos los números de la colección son distintos de
        /// cero, <see langword="false" /> en caso contrario.
        /// </returns>
        public static bool AreNotZero<T>(IEnumerable<T> x) where T : struct, IComparable<T>
        {
            return x.All(j => j.CompareTo(default) != 0);
        }

        private static int[] ReadKnownPrimes()
        {
            var l = new List<int>();
            var a = new Unpacker(typeof(Algebra).Assembly, @"TheXDS.MCART.Resources.Data");
            using var s = a.Unpack("primes", new DeflateGetter());
            using var b = new BinaryReader(s);
            var c = b.ReadInt32();
            while (l.Count < c)
            {
                l.Add(b.ReadInt32());
            }
            return l.ToArray();
        }
    }
}