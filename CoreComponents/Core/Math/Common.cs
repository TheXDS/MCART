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
    ///     Contiene métodos de manipulación matemática estándar.
    /// </summary>
    public static class Common
    {
        /// <summary>
        ///     Establece límites de sobreflujo para evaluar una expresión.
        /// </summary>
        /// <typeparam name="T"> Tipo de expresión a limitar. </typeparam>
        /// <param name="expression">Expresión a evaluar.</param>
        /// <param name="max">Límite superior de salida, inclusive.</param>
        /// <param name="min">Límite inferior de salida, inclusive.</param>
        /// <returns>
        ///     El valor evaluado que se encuentra dentro del rango especificado.
        /// </returns>
        public static T Clamp<T>(this T expression, T min, T max) where T : IComparable<T>
        {
            if (expression.CompareTo(max) > 0) return max;
            if (expression.CompareTo(min) < 0) return min;
            return expression;
        }

        /// <summary>
        ///     Establece límites de sobreflujo para evaluar una expresión.
        /// </summary>
        /// <typeparam name="T"> Tipo de expresión a limitar. </typeparam>
        /// <param name="expression">Expresión a evaluar.</param>
        /// <param name="max">Límite superior de salida, inclusive.</param>
        /// <returns>
        ///     El valor evaluado que se encuentra entre 0 y
        ///     <paramref name="max" />.
        /// </returns>
        public static T Clamp<T>(this T expression, T max) where T : IComparable<T>
        {
            return Clamp(expression, default, max);
        }

#if FloatDoubleSpecial

        /// <summary>
        ///     Establece límites de sobreflujo para evaluar una expresión.
        /// </summary>
        /// <param name="expression">Expresión a evaluar.</param>
        /// <param name="min">Límite inferior de salida, inclusive.</param>
        /// <param name="max">Límite superior de salida, inclusive.</param>
        /// <returns>
        ///     El valor evaluado que se encuentra dentro del rango especificado.
        /// </returns>
        /// <remarks>
        ///     Esta implementación se incluye para permitir parámetros de tipo
        ///     <see cref="double.NaN" />, <see cref="double.NegativeInfinity" /> y
        ///     <see cref="double.PositiveInfinity" />.
        /// </remarks>
        public static double Clamp(this double expression, double min, double max)
        {
            if (double.IsNaN(expression)) return double.NaN;
            if (expression > max) return max;
            if (expression < min) return min;
            return expression;
        }

        /// <summary>
        ///     Establece límites de sobreflujo para evaluar una expresión.
        /// </summary>
        /// <param name="expression">Expresión a evaluar.</param>
        /// <param name="max">Límite superior de salida, inclusive.</param>
        /// <returns>
        ///     El valor evaluado que se encuentra dentro del rango especificado.
        /// </returns>
        /// <remarks>
        ///     Esta implementación se incluye para permitir parámetros de tipo
        ///     <see cref="double.NaN" />, <see cref="double.NegativeInfinity" /> y
        ///     <see cref="double.PositiveInfinity" />.
        /// </remarks>
        public static double Clamp(this double expression, double max)
        {
            return Clamp(expression, double.NegativeInfinity, max);
        }

        /// <summary>
        ///     Establece límites de sobreflujo para evaluar una expresión.
        /// </summary>
        /// <param name="expression">Expresión a evaluar.</param>
        /// <param name="max">Límite superior de salida, inclusive.</param>
        /// <returns>
        ///     El valor evaluado que se encuentra dentro del rango especificado.
        /// </returns>
        /// <remarks>
        ///     Esta implementación se incluye para permitir parámetros de tipo
        ///     <see cref="float.NaN" />, <see cref="float.NegativeInfinity" /> y
        ///     <see cref="float.PositiveInfinity" />.
        /// </remarks>
        public static float Clamp(this float expression, float max)
        {
            return Clamp(expression, float.NegativeInfinity, max);
        }
        
#if RatherDRY

        /// <summary>
        ///     Establece límites de sobreflujo para evaluar una expresión.
        /// </summary>
        /// <param name="expression">Expresión a evaluar.</param>
        /// <param name="min">Límite inferior de salida, inclusive.</param>
        /// <param name="max">Límite superior de salida, inclusive.</param>
        /// <returns>
        ///     El valor evaluado que se encuentra dentro del rango especificado.
        /// </returns>
        /// <remarks>
        ///     Esta implementación se incluye para permitir parámetros de tipo
        ///     <see cref="float.NaN" />, <see cref="float.NegativeInfinity" /> y
        ///     <see cref="float.PositiveInfinity" />.
        /// </remarks>
        public static float Clamp(this float expression, float min, float max)
        {
            return (float) Clamp((double) expression, min, max);
        }

#else
        /// <summary>
        ///     Establece límites de sobreflujo para evaluar una expresión.
        /// </summary>
        /// <param name="expression">Expresión a evaluar.</param>
        /// <param name="min">Límite inferior de salida, inclusive.</param>
        /// <param name="max">Límite superior de salida, inclusive.</param>
        /// <returns>
        ///     El valor evaluado que se encuentra dentro del rango especificado.
        /// </returns>
        /// <remarks>
        ///     Esta implementación se incluye para permitir parámetros de tipo
        ///     <see cref="float.NaN" />, <see cref="float.NegativeInfinity" /> y
        ///     <see cref="float.PositiveInfinity" />.
        /// </remarks>
        public static float Clamp(this float expression, float min, float max)
        {
            if (float.IsNaN(expression)) return float.NaN;
            if (expression > max) return max;
            if (expression < min) return min;
            return expression;
        }

#endif

#endif


#if RatherDRY
        /// <summary>
        ///     Establece puntos de sobreflujo intencional para evaluar una expresión.
        /// </summary>
        /// <typeparam name="T"> Tipo de expresión a evaluar. </typeparam>
        /// <param name="expression">Expresión a evaluar.</param>
        /// <param name="max">Límite superior de salida, inclusive.</param>
        /// <param name="min">Límite inferior de salida, inclusive.</param>
        /// <returns>
        ///     El valor evaluado que se encuentra dentro del rango especificado.
        /// </returns>
        public static T Wrap<T>(this T expression, T min, T max) where T : IComparable<T>
        {
            if (expression.CompareTo(max) > 0) return (expression - ((dynamic) 1 + max - min)).Wrap(min, max);
            if (expression.CompareTo(min) < 0) return (expression + ((dynamic) 1 + max - min)).Wrap(min, max);
            return expression;
        }
    
#else

        /// <summary>
        ///     Establece puntos de sobreflujo intencional para evaluar una expresión.
        /// </summary>
        /// <param name="expression">Expresión a evaluar.</param>
        /// <param name="max">Límite superior de salida, inclusive.</param>
        /// <param name="min">Límite inferior de salida, inclusive.</param>
        /// <returns>
        ///     El valor evaluado que se encuentra dentro del rango especificado.
        /// </returns>
        public static int Wrap(this byte expression, byte min, byte max)
        {
            if (expression.CompareTo(max) > 0) return (expression - (1 + max - min)).Wrap(min, max);
            if (expression.CompareTo(min) < 0) return (expression + (1 + max - min)).Wrap(min, max);
            return expression;
        }

        /// <summary>
        ///     Establece puntos de sobreflujo intencional para evaluar una expresión.
        /// </summary>
        /// <param name="expression">Expresión a evaluar.</param>
        /// <param name="max">Límite superior de salida, inclusive.</param>
        /// <param name="min">Límite inferior de salida, inclusive.</param>
        /// <returns>
        ///     El valor evaluado que se encuentra dentro del rango especificado.
        /// </returns>
        public static int Wrap(this short expression, short min, short max)
        {
            if (expression.CompareTo(max) > 0) return (expression - (1 + max - min)).Wrap(min, max);
            if (expression.CompareTo(min) < 0) return (expression + (1 + max - min)).Wrap(min, max);
            return expression;
        }

        /// <summary>
        ///     Establece puntos de sobreflujo intencional para evaluar una expresión.
        /// </summary>
        /// <param name="expression">Expresión a evaluar.</param>
        /// <param name="max">Límite superior de salida, inclusive.</param>
        /// <param name="min">Límite inferior de salida, inclusive.</param>
        /// <returns>
        ///     El valor evaluado que se encuentra dentro del rango especificado.
        /// </returns>
        public static int Wrap(this int expression, int min, int max)
        {
            if (expression.CompareTo(max) > 0) return (expression - (1 + max - min)).Wrap(min, max);
            if (expression.CompareTo(min) < 0) return (expression + (1 + max - min)).Wrap(min, max);
            return expression;
        }

        /// <summary>
        ///     Establece puntos de sobreflujo intencional para evaluar una expresión.
        /// </summary>
        /// <param name="expression">Expresión a evaluar.</param>
        /// <param name="max">Límite superior de salida, inclusive.</param>
        /// <param name="min">Límite inferior de salida, inclusive.</param>
        /// <returns>
        ///     El valor evaluado que se encuentra dentro del rango especificado.
        /// </returns>
        public static long Wrap(this long expression, long min, long max)
        {
            if (expression.CompareTo(max) > 0) return (expression - (1 + max - min)).Wrap(min, max);
            if (expression.CompareTo(min) < 0) return (expression + (1 + max - min)).Wrap(min, max);
            return expression;
        }

        /// <summary>
        ///     Establece puntos de sobreflujo intencional para evaluar una expresión.
        /// </summary>
        /// <param name="expression">Expresión a evaluar.</param>
        /// <param name="max">Límite superior de salida, inclusive.</param>
        /// <param name="min">Límite inferior de salida, inclusive.</param>
        /// <returns>
        ///     El valor evaluado que se encuentra dentro del rango especificado.
        /// </returns>
        public static decimal Wrap(this decimal expression, decimal min, decimal max)
        {
            if (expression.CompareTo(max) > 0) return (expression - (1 + max - min)).Wrap(min, max);
            if (expression.CompareTo(min) < 0) return (expression + (1 + max - min)).Wrap(min, max);
            return expression;
        }

        /// <summary>
        ///     Establece puntos de sobreflujo intencional para evaluar una expresión.
        /// </summary>
        /// <param name="expression">Expresión a evaluar.</param>
        /// <param name="max">Límite superior de salida, inclusive.</param>
        /// <param name="min">Límite inferior de salida, inclusive.</param>
        /// <returns>
        ///     El valor evaluado que se encuentra dentro del rango especificado.
        /// </returns>
        public static double Wrap(this double expression, double min, double max)
        {
            if (double.IsNaN(expression)) return double.NaN;
            if (expression.CompareTo(max) > 0) return (expression - (1 + max - min)).Wrap(min, max);
            if (expression.CompareTo(min) < 0) return (expression + (1 + max - min)).Wrap(min, max);
            return expression;
        }

        /// <summary>
        ///     Establece puntos de sobreflujo intencional para evaluar una expresión.
        /// </summary>
        /// <param name="expression">Expresión a evaluar.</param>
        /// <param name="max">Límite superior de salida, inclusive.</param>
        /// <param name="min">Límite inferior de salida, inclusive.</param>
        /// <returns>
        ///     El valor evaluado que se encuentra dentro del rango especificado.
        /// </returns>
        public static float Wrap(this float expression, float min, float max)
        {
            if (float.IsNaN(expression)) return float.NaN;
            if (expression.CompareTo(max) > 0) return (expression - (1 + max - min)).Wrap(min, max);
            if (expression.CompareTo(min) < 0) return (expression + (1 + max - min)).Wrap(min, max);
            return expression;
        }

#if !CLSCompliance
        /// <summary>
        ///     Establece puntos de sobreflujo intencional para evaluar una expresión.
        /// </summary>
        /// <param name="expression">Expresión a evaluar.</param>
        /// <param name="max">Límite superior de salida, inclusive.</param>
        /// <param name="min">Límite inferior de salida, inclusive.</param>
        /// <returns>
        ///     El valor evaluado que se encuentra dentro del rango especificado.
        /// </returns>
        public static int Wrap(this sbyte expression, sbyte min, sbyte max)
        {
            if (expression.CompareTo(max) > 0) return (expression - (1 + max - min)).Wrap(min, max);
            if (expression.CompareTo(min) < 0) return (expression + (1 + max - min)).Wrap(min, max);
            return expression;
        }

        /// <summary>
        ///     Establece puntos de sobreflujo intencional para evaluar una expresión.
        /// </summary>
        /// <param name="expression">Expresión a evaluar.</param>
        /// <param name="max">Límite superior de salida, inclusive.</param>
        /// <param name="min">Límite inferior de salida, inclusive.</param>
        /// <returns>
        ///     El valor evaluado que se encuentra dentro del rango especificado.
        /// </returns>
        public static int Wrap(this ushort expression, ushort min, ushort max)
        {
            if (expression.CompareTo(max) > 0) return (expression - (1 + max - min)).Wrap(min, max);
            if (expression.CompareTo(min) < 0) return (expression + (1 + max - min)).Wrap(min, max);
            return expression;
        }

        /// <summary>
        ///     Establece puntos de sobreflujo intencional para evaluar una expresión.
        /// </summary>
        /// <param name="expression">Expresión a evaluar.</param>
        /// <param name="max">Límite superior de salida, inclusive.</param>
        /// <param name="min">Límite inferior de salida, inclusive.</param>
        /// <returns>
        ///     El valor evaluado que se encuentra dentro del rango especificado.
        /// </returns>
        public static uint Wrap(this uint expression, uint min, uint max)
        {
            if (expression.CompareTo(max) > 0) return (expression - (1 + max - min)).Wrap(min, max);
            if (expression.CompareTo(min) < 0) return (expression + (1 + max - min)).Wrap(min, max);
            return expression;
        }

        /// <summary>
        ///     Establece puntos de sobreflujo intencional para evaluar una expresión.
        /// </summary>
        /// <param name="expression">Expresión a evaluar.</param>
        /// <param name="max">Límite superior de salida, inclusive.</param>
        /// <param name="min">Límite inferior de salida, inclusive.</param>
        /// <returns>
        ///     El valor evaluado que se encuentra dentro del rango especificado.
        /// </returns>
        public static ulong Wrap(this ulong expression, ulong min, ulong max)
        {
            if (expression.CompareTo(max) > 0) return (expression - (1 + max - min)).Wrap(min, max);
            if (expression.CompareTo(min) < 0) return (expression + (1 + max - min)).Wrap(min, max);
            return expression;
        }
        
     #endif

#endif
        
    }
}