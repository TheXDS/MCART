/*
Algebra.cs

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
using System.Globalization;
using System.Linq;
using TheXDS.MCART.Attributes;

#region Configuración de ReSharper

// ReSharper disable IntroduceOptionalParameters.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable MemberCanBeProtected.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable UnusedMember.Global

#endregion

namespace TheXDS.MCART.Math
{
    /// <summary>
    ///     Contiene series, operaciones, ecuaciones y constantes matemáticas adicionales.
    /// </summary>
    public static class Algebra
    {
        /// <summary>
        ///     Comprueba si un número es primo mediante prueba y error.
        /// </summary>
        /// <returns>
        ///     <see langword="true" />si el número es primo, <see langword="false" /> en caso contrario.
        /// </returns>
        /// <param name="number">Número a comprobar.</param>
        public static bool IsPrime(this long number)
        {
            var s = number / 2;
            for (long j = 3; j < s; j += 2)
                if (number % j == 0)
                    return false;
            return true;
        }

        /// <summary>
        ///     Comprueba si un número es primo.
        /// </summary>
        /// <returns>
        ///     <see langword="true" />si el número es primo, <see langword="false" /> en caso contrario.
        /// </returns>
        /// <param name="number">Número a comprobar.</param>
        [Thunk]
        public static bool IsPrime(this int number)
        {
            return ((long) number).IsPrime();
        }
#if !CLSCompliance
/// <summary>
/// Comprueba si un número es primo.
/// </summary>
/// <returns>
/// <see langword="true"/>si el número es primo, <see langword="false"/> en caso contrario.
/// </returns>
/// <param name="number">Número a comprobar.</param>
        [Thunk] public static bool IsPrime(this uint number) => ((long)number).IsPrime();
#endif
        /// <summary>
        ///     Comprueba si un número es primo.
        /// </summary>
        /// <returns>
        ///     <see langword="true" />si el número es primo, <see langword="false" /> en caso contrario.
        /// </returns>
        /// <param name="number">Número a comprobar.</param>
        [Thunk]
        public static bool IsPrime(this short number)
        {
            return ((long) number).IsPrime();
        }
#if !CLSCompliance
/// <summary>
/// Comprueba si un número es primo.
/// </summary>
/// <returns>
/// <see langword="true"/>si el número es primo, <see langword="false"/> en caso contrario.
/// </returns>
/// <param name="number">Número a comprobar.</param>
        [Thunk] public static bool IsPrime(this ushort number) => ((long)number).IsPrime();
        /// <summary>
        /// Comprueba si un número es primo.
        /// </summary>
        /// <returns>
        /// <see langword="true"/>si el número es primo, <see langword="false"/> en caso contrario.
        /// </returns>
        /// <param name="number">Número a comprobar.</param>
        [Thunk] public static bool IsPrime(this sbyte number) => ((long)number).IsPrime();
#endif
        /// <summary>
        ///     Comprueba si un número es primo.
        /// </summary>
        /// <returns>
        ///     <see langword="true" />si el número es primo, <see langword="false" /> en caso contrario.
        /// </returns>
        /// <param name="number">Número a comprobar.</param>
        [Thunk]
        public static bool IsPrime(this byte number)
        {
            return ((long) number).IsPrime();
        }

        /// <summary>
        ///     Calcula la potencia de dos más cercana mayor o igual al número
        /// </summary>
        /// <param name="value">Número de entrada. Se buscará una potencia de dos mayor o igual a este valor.</param>
        /// <returns>Un valor <see cref="long" /> que es resultado de la operación.</returns>
        public static long Nearest2Pow(int value)
        {
            long c = 1;
            while (!(c >= value)) c *= 2;
            return c;
        }

        /// <summary>
        ///     Devuelve el primer múltiplo de <paramref name="multiplier" /> que es mayor que <paramref name="value" />
        /// </summary>
        /// <param name="value">Número objetivo</param>
        /// <param name="multiplier">
        ///     Base multiplicativa. Esta función devolverá un múltiplo de este valor que sea mayor a
        ///     <paramref name="value" />
        /// </param>
        /// <returns>
        ///     Un <see cref="double" /> que es el primer múltiplo de <paramref name="multiplier" /> que es mayor que
        ///     <paramref name="value" />
        /// </returns>
        public static double NearestMultiplyUp(double value, double multiplier)
        {
            double a = 1;
            if (!ArePositive(value, multiplier)) return a;
            while (!(a > value))
                a *= multiplier;
            return a;
        }

        /// <summary>
        ///     Devuelve <see langword="true" /> si todos los números son positivos.
        /// </summary>
        /// <param name="values">números a comprobar.</param>
        /// <returns>
        ///     <see langword="true" /> si todos los números de la colección son positivos,
        ///     <see langword="false" /> en caso contrario.
        /// </returns>
        public static bool ArePositive<T>(params T[] values) where T : IComparable<T>
        {
            return ArePositive(values.AsEnumerable());
        }

        /// <summary>
        ///     Devuelve <see langword="true" /> si todos los números son negativos.
        /// </summary>
        /// <param name="values">números a comprobar.</param>
        /// <returns>
        ///     <see langword="true" /> si todos los números de la colección son negativos,
        ///     <see langword="false" /> en caso contrario.
        /// </returns>
        public static bool AreNegative<T>(params T[] values) where T : IComparable<T>
        {
            return AreNegative(values.AsEnumerable());
        }

        /// <summary>
        ///     Devuelve <see langword="true" /> si todos los números son iguales a cero.
        /// </summary>
        /// <typeparam name="T">
        ///     Tipo de elementos a comprobar.
        /// </typeparam>
        /// <param name="values">números a comprobar.</param>
        /// <returns>
        ///     <see langword="true" /> si todos los números de la colección son iguales a
        ///     cero, <see langword="false" /> en caso contrario.
        /// </returns>
        public static bool AreZero<T>(params T[] values) where T : IComparable<T>
        {
            return AreZero(values.AsEnumerable());
        }
        
        /// <summary>
        ///     Devuelve <see langword="true" /> si todos los números son negativos.
        /// </summary>
        /// <param name="values">números a comprobar.</param>
        /// <returns>
        ///     <see langword="true" /> si todos los números de la colección son negativos,
        ///     <see langword="false" /> en caso contrario.
        /// </returns>
        public static bool AreNegative<T>(this IEnumerable<T> values) where T : IComparable<T>
        {
            return values.All(j => j.CompareTo(default) < 0);
        }
        
        /// <summary>
        ///     Devuelve <see langword="true" /> si todos los números son iguales a cero.
        /// </summary>
        /// <typeparam name="T">
        ///     Tipo de elementos a comprobar.
        /// </typeparam>
        /// <param name="values">números a comprobar.</param>
        /// <returns>
        ///     <see langword="true" /> si todos los números de la colección son iguales a
        ///     cero, <see langword="false" /> en caso contrario.
        /// </returns>
        public static bool AreZero<T>(this IEnumerable<T> values) where T : IComparable<T>
        {
            return values.All(j => j.CompareTo(default) == 0);
        }

        /// <summary>
        ///     Devuelve <see langword="true" /> si todos los números son positivos.
        /// </summary>
        /// <param name="values">números a comprobar.</param>
        /// <returns>
        ///     <see langword="true" /> si todos los números de la colección son positivos,
        ///     <see langword="false" /> en caso contrario.
        /// </returns>
        public static bool ArePositive<T>(this IEnumerable<T> values) where T : IComparable<T>
        {
            return values.All(j => j.CompareTo(default) > 0);
        }
        
        /// <summary>
        ///     Determina si un <see cref="double" /> es un número entero.
        /// </summary>
        /// <param name="value">Valor a comprobar.</param>
        /// <returns><see langword="true" /> si el valor es entero; de lo contrario, <c>False</c></returns>
        public static bool IsWhole(this double value)
        {
            return !value.ToString(CultureInfo.InvariantCulture).Contains(".");
        }

        /// <summary>
        ///     Determina si un <see cref="double" /> es un número real operable.
        /// </summary>
        /// <param name="value"><see cref="double" /> a comprobar.</param>
        /// <returns>
        ///     <see langword="true" /> si <paramref name="value" /> es un número real
        ///     <see cref="double" /> operable, en otras palabras, si no es igual a
        ///     <see cref="double.NaN" />, <see cref="double.PositiveInfinity" /> o
        ///     <see cref="double.NegativeInfinity" />; en cuyo caso se devuelve
        ///     <see langword="false" />.
        /// </returns>
        public static bool IsValid(this double value)
        {
            return !(double.IsNaN(value) || double.IsInfinity(value));
        }

        /// <summary>
        ///     Determina si un <see cref="float" /> es un número real operable.
        /// </summary>
        /// <param name="value"><see cref="float" /> a comprobar.</param>
        /// <returns>
        ///     <see langword="true" /> si <paramref name="value" /> es un número real
        ///     <see cref="float" /> operable, en otras palabras, si no es igual a
        ///     <see cref="float.NaN" />, <see cref="float.PositiveInfinity" /> o
        ///     <see cref="float.NegativeInfinity" />; en cuyo caso se devuelve
        ///     <see langword="false" />.
        /// </returns>
        public static bool IsValid(this float value)
        {
            return !(float.IsNaN(value) || float.IsInfinity(value));
        }

        /// <summary>
        ///     Determina si una colección de <see cref="double" /> son números
        ///     reales operables.
        /// </summary>
        /// <param name="values">
        ///     Colección  de <see cref="double" /> a comprobar.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> si todos los elementos de <paramref name="values" /> son
        ///     números operables, en otras palabras, si no son NaN o Infinito; en
        ///     caso contrario, se devuelve <see langword="false" />.
        /// </returns>
        [Thunk]
        public static bool AreValid(params double[] values)
        {
            return values.All(IsValid);
        }

        /// <summary>
        ///     Determina si una colección de <see cref="float" /> son números
        ///     reales operables.
        /// </summary>
        /// <param name="values">
        ///     Colección  de <see cref="float" /> a comprobar.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> si todos los elementos de <paramref name="values" /> son
        ///     números operables, en otras palabras, si no son NaN o Infinito; en
        ///     caso contrario, se devuelve <see langword="false" />.
        /// </returns>
        [Thunk]
        public static bool AreValid(params float[] values)
        {
            return values.All(IsValid);
        }

        /// <summary>
        ///     Determina si una colección de <see cref="float" /> son números
        ///     reales operables.
        /// </summary>
        /// <param name="values">
        ///     Colección  de <see cref="float" /> a comprobar.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> si todos los elementos de <paramref name="values" /> son
        ///     números operables, en otras palabras, si no son NaN o Infinito; en
        ///     caso contrario, se devuelve <see langword="false" />.
        /// </returns>
        [Thunk]
        public static bool AreValid(IEnumerable<float> values)
        {
            return values.All(IsValid);
        }

        /// <summary>
        ///     Determina si una colección de <see cref="double" /> son números
        ///     reales operables.
        /// </summary>
        /// <param name="values">
        ///     Colección  de <see cref="double" /> a comprobar.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> si todos los elementos de <paramref name="values" /> son
        ///     números operables, en otras palabras, si no son NaN o Infinito; en
        ///     caso contrario, se devuelve <see langword="false" />.
        /// </returns>
        [Thunk]
        public static bool AreValid(IEnumerable<double> values)
        {
            return values.All(IsValid);
        }

        /// <summary>
        ///     Devuelve <see langword="true" /> si todos los números son distintos de cero.
        /// </summary>
        /// <typeparam name="T">
        ///     Tipo de elementos a comprobar.
        /// </typeparam>
        /// <param name="x">números a comprobar.</param>
        /// <returns>
        ///     <see langword="true" /> si todos los números de la colección son distintos de
        ///     cero, <see langword="false" /> en caso contrario.
        /// </returns>
        public static bool AreNotZero<T>(params T[] x) where T : IComparable<T>
        {
            return x.All(j => j.CompareTo(default) != 0);
        }

        /// <summary>
        ///     Devuelve <see langword="true" /> si todos los números son distintos de cero.
        /// </summary>
        /// <typeparam name="T">
        ///     Tipo de elementos a comprobar.
        /// </typeparam>
        /// <param name="x">números a comprobar.</param>
        /// <returns>
        ///     <see langword="true" /> si todos los números de la colección son distintos de
        ///     cero, <see langword="false" /> en caso contrario.
        /// </returns>
        public static bool AreNotZero<T>(IEnumerable<T> x) where T : IComparable<T>
        {
            return x.All(j => j.CompareTo(default) != 0);
        }
    }
}