/*
Math.cs

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
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Types;

#region Configuración de ReSharper

// ReSharper disable IntroduceOptionalParameters.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable MemberCanBeProtected.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable UnusedMember.Global

#endregion

namespace TheXDS.MCART
{
    /// <summary>
    ///     Contiene series, operaciones, ecuaciones y constantes matemáticas adicionales.
    /// </summary>
    public static partial class Algebra
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
            if (ArePositives(value, multiplier))
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
        public static bool ArePositives(params double[] values)
        {
            foreach (var j in values)
                if (j <= 0)
                    return false;
            return true;
        }

        /// <summary>
        ///     Devuelve <see langword="true" /> si todos los números son negativos.
        /// </summary>
        /// <param name="values">números a comprobar.</param>
        /// <returns>
        ///     <see langword="true" /> si todos los números de la colección son negativos,
        ///     <see langword="false" /> en caso contrario.
        /// </returns>
        public static bool AreNegatives(params double[] values)
        {
            foreach (var j in values)
                if (j >= 0)
                    return false;
            return true;
        }

        /// <summary>
        ///     Devuelve <see langword="true" /> si todos los números son iguales a cero.
        /// </summary>
        /// <typeparam name="T">
        ///     Tipo de elementos a comprobar.
        /// </typeparam>
        /// <param name="value">números a comprobar.</param>
        /// <returns>
        ///     <see langword="true" /> si todos los números de la colección son iguales a
        ///     cero, <see langword="false" /> en caso contrario.
        /// </returns>
        public static bool AreZero<T>(params T[] value) where T : IComparable<T>
        {
            foreach (var j in value)
                if (j.CompareTo(default) != 0)
                    return false;
            return true;
        }

#if FloatDoubleSpecial

#endif

#if RatherDRY
#if FloatDoubleSpecial
        /// <summary>
        /// Establece límites de sobreflujo para evaluar una expresión.
        /// </summary>
        /// <param name="expression">Expresión a evaluar.</param>
        /// <param name="min">Límite inferior de salida, inclusive.</param>
        /// <param name="max">Límite superior de salida, inclusive.</param>
        /// <returns>
        /// El valor evaluado que se encuentra dentro del rango especificado.
        /// </returns>
        /// <remarks>
        /// Esta implementación se incluye para permitir parámetros de tipo
        /// <see cref="float.NaN"/>, <see cref="float.NegativeInfinity"/> y
        /// <see cref="float.PositiveInfinity"/>.
        /// </remarks>
        public static float Clamp(this float expression, float min, float max) => (float)Clamp<double>(expression, min, max);
#endif
        /// <summary>
        /// Establece puntos de sobreflujo intencional para evaluar una expresión.
        /// </summary>
        /// <typeparam name="T"> Tipo de expresión a evaluar. </typeparam>
        /// <param name="expression">Expresión a evaluar.</param>
        /// <param name="max">Límite superior de salida, inclusive.</param>
        /// <param name="min">Límite inferior de salida, inclusive.</param>
        /// <returns>
        /// El valor evaluado que se encuentra dentro del rango especificado.
        /// </returns>
        public static T Wrap<T>(this T expression, T min, T max) where T : IEquatable<T>, IFormattable, IComparable, IComparable<T>
        {
            if (expression.CompareTo(max) > 0) return (expression - ((dynamic)1 + max - min)).Wrap(min, max);
            if (expression.CompareTo(min) < 0) return (expression + ((dynamic)1 + max - min)).Wrap(min, max);
            return expression;
        }
#else
#if FloatDoubleSpecial
#endif


#endif
        /// <summary>
        ///     Determina si un <see cref="double" /> es un número entero.
        /// </summary>
        /// <param name="value">Valor a comprobar.</param>
        /// <returns><see langword="true" /> si el valor es entero; de lo contrario, <c>False</c></returns>
        public static bool IsWhole(this double value)
        {
            return !value.ToString().Contains(".");
        }
    }
}