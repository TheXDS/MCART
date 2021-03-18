/*
Common.cs

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

namespace TheXDS.MCART.Math
{
    /// <summary>
    /// Contiene métodos de manipulación matemática estándar.
    /// </summary>
    public static partial class Common
    {
        /// <summary>
        /// Establece límites de sobreflujo para evaluar una expresión.
        /// </summary>
        /// <typeparam name="T"> Tipo de expresión a limitar. </typeparam>
        /// <param name="expression">Expresión a evaluar.</param>
        /// <param name="max">Límite superior de salida, inclusive.</param>
        /// <param name="min">Límite inferior de salida, inclusive.</param>
        /// <returns>
        /// El valor evaluado que se encuentra dentro del rango especificado.
        /// </returns>
        public static T Clamp<T>(this T expression, in T min, in T max) where T : IComparable<T>
        {
            if (expression.CompareTo(max) > 0) return max;
            if (expression.CompareTo(min) < 0) return min;
            return expression;
        }

        /// <summary>
        /// Establece límites de sobreflujo para evaluar una expresión.
        /// </summary>
        /// <typeparam name="T"> Tipo de expresión a limitar. </typeparam>
        /// <param name="expression">Expresión a evaluar.</param>
        /// <param name="max">Límite superior de salida, inclusive.</param>
        /// <returns>
        /// El valor evaluado que se encuentra entre 0 y
        /// <paramref name="max" />.
        /// </returns>
        public static T Clamp<T>(this T expression, in T max) where T : struct, IComparable<T>
        {
            return Clamp(expression, default, max);
        }

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
        /// <see cref="double.NaN" />, <see cref="double.NegativeInfinity" /> y
        /// <see cref="double.PositiveInfinity" />.
        /// </remarks>
        public static double Clamp(this in double expression, in double min, in double max)
        {
            if (double.IsNaN(expression)) return double.NaN;
            if (expression > max) return max;
            if (expression < min) return min;
            return expression;
        }

        /// <summary>
        /// Establece límites de sobreflujo para evaluar una expresión.
        /// </summary>
        /// <param name="expression">Expresión a evaluar.</param>
        /// <param name="max">Límite superior de salida, inclusive.</param>
        /// <returns>
        /// El valor evaluado que se encuentra dentro del rango especificado.
        /// </returns>
        /// <remarks>
        /// Esta implementación se incluye para permitir parámetros de tipo
        /// <see cref="double.NaN" />, <see cref="double.NegativeInfinity" /> y
        /// <see cref="double.PositiveInfinity" />.
        /// </remarks>
        public static double Clamp(this in double expression, in double max)
        {
            return Clamp(expression, double.NegativeInfinity, max);
        }

        /// <summary>
        /// Establece límites de sobreflujo para evaluar una expresión.
        /// </summary>
        /// <param name="expression">Expresión a evaluar.</param>
        /// <param name="max">Límite superior de salida, inclusive.</param>
        /// <returns>
        /// El valor evaluado que se encuentra dentro del rango especificado.
        /// </returns>
        /// <remarks>
        /// Esta implementación se incluye para permitir parámetros de tipo
        /// <see cref="float.NaN" />, <see cref="float.NegativeInfinity" /> y
        /// <see cref="float.PositiveInfinity" />.
        /// </remarks>
        public static float Clamp(this in float expression, in float max)
        {
            return Clamp(expression, float.NegativeInfinity, max);
        }

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
        /// <see cref="float.NaN" />, <see cref="float.NegativeInfinity" /> y
        /// <see cref="float.PositiveInfinity" />.
        /// </remarks>
        public static float Clamp(this in float expression, in float min, in float max)
        {
#if RatherDRY
            return (float) Clamp((double) expression, min, max);
#else
            if (float.IsNaN(expression)) return float.NaN;
            if (expression > max) return max;
            if (expression < min) return min;
            return expression;
#endif
        }

#endif

#if RatherDRY
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
        public static T Wrap<T>(this T expression, T min, T max) where T : IComparable<T>
        {
            unchecked
            {
                if (expression.CompareTo(max) > 0) return (expression - ((dynamic) 1 + max - min)).Wrap(min, max);
                if (expression.CompareTo(min) < 0) return (expression + ((dynamic) 1 + max - min)).Wrap(min, max);
            }
            return expression;
        }
    
#else

        /// <summary>
        /// Establece puntos de sobreflujo intencional para evaluar una expresión.
        /// </summary>
        /// <param name="expression">Expresión a evaluar.</param>
        /// <param name="max">Límite superior de salida, inclusive.</param>
        /// <param name="min">Límite inferior de salida, inclusive.</param>
        /// <returns>
        /// El valor evaluado que se encuentra dentro del rango especificado.
        /// </returns>
        public static byte Wrap(this in byte expression, in byte min, in byte max)
        {
            unchecked
            {
                if (expression.CompareTo(max) > 0) return (byte)(expression - (1 + max - min)).Wrap(min, max);
                if (expression.CompareTo(min) < 0) return (byte)(expression + (1 + max - min)).Wrap(min, max);
                return expression;
            }
        }

        /// <summary>
        /// Establece puntos de sobreflujo intencional para evaluar una expresión.
        /// </summary>
        /// <param name="expression">Expresión a evaluar.</param>
        /// <param name="max">Límite superior de salida, inclusive.</param>
        /// <param name="min">Límite inferior de salida, inclusive.</param>
        /// <returns>
        /// El valor evaluado que se encuentra dentro del rango especificado.
        /// </returns>
        public static int Wrap(this in short expression, in short min, in short max)
        {
            if (expression.CompareTo(max) > 0) return (expression - (1 + max - min)).Wrap(min, max);
            if (expression.CompareTo(min) < 0) return (expression + (1 + max - min)).Wrap(min, max);
            return expression;
        }

        /// <summary>
        /// Establece puntos de sobreflujo intencional para evaluar una expresión.
        /// </summary>
        /// <param name="expression">Expresión a evaluar.</param>
        /// <param name="max">Límite superior de salida, inclusive.</param>
        /// <param name="min">Límite inferior de salida, inclusive.</param>
        /// <returns>
        /// El valor evaluado que se encuentra dentro del rango especificado.
        /// </returns>
        public static int Wrap(this in char expression, in char min, in char max)
        {
            if (expression.CompareTo(max) > 0) return (expression - (1 + max - min)).Wrap(min, max);
            if (expression.CompareTo(min) < 0) return (expression + (1 + max - min)).Wrap(min, max);
            return expression;
        }

        /// <summary>
        /// Establece puntos de sobreflujo intencional para evaluar una expresión.
        /// </summary>
        /// <param name="expression">Expresión a evaluar.</param>
        /// <param name="max">Límite superior de salida, inclusive.</param>
        /// <param name="min">Límite inferior de salida, inclusive.</param>
        /// <returns>
        /// El valor evaluado que se encuentra dentro del rango especificado.
        /// </returns>
        public static int Wrap(this in int expression, in int min, in int max)
        {
            if (expression.CompareTo(max) > 0) return (expression - (1 + max - min)).Wrap(min, max);
            if (expression.CompareTo(min) < 0) return (expression + (1 + max - min)).Wrap(min, max);
            return expression;
        }

        /// <summary>
        /// Establece puntos de sobreflujo intencional para evaluar una expresión.
        /// </summary>
        /// <param name="expression">Expresión a evaluar.</param>
        /// <param name="max">Límite superior de salida, inclusive.</param>
        /// <param name="min">Límite inferior de salida, inclusive.</param>
        /// <returns>
        /// El valor evaluado que se encuentra dentro del rango especificado.
        /// </returns>
        public static long Wrap(this in long expression, in long min, in long max)
        {
            if (expression.CompareTo(max) > 0) return (expression - (1 + max - min)).Wrap(min, max);
            if (expression.CompareTo(min) < 0) return (expression + (1 + max - min)).Wrap(min, max);
            return expression;
        }

        /// <summary>
        /// Establece puntos de sobreflujo intencional para evaluar una expresión.
        /// </summary>
        /// <param name="expression">Expresión a evaluar.</param>
        /// <param name="max">Límite superior de salida, inclusive.</param>
        /// <param name="min">Límite inferior de salida, inclusive.</param>
        /// <returns>
        /// El valor evaluado que se encuentra dentro del rango especificado.
        /// </returns>
        public static decimal Wrap(this in decimal expression, in decimal min, in decimal max)
        {
            if (expression.CompareTo(max) > 0) return (expression - (1 + max - min)).Wrap(min, max);
            if (expression.CompareTo(min) < 0) return (expression + (1 + max - min)).Wrap(min, max);
            return expression;
        }

        /// <summary>
        /// Establece puntos de sobreflujo intencional para evaluar una expresión.
        /// </summary>
        /// <param name="expression">Expresión a evaluar.</param>
        /// <param name="max">Límite superior de salida, inclusive.</param>
        /// <param name="min">Límite inferior de salida, inclusive.</param>
        /// <returns>
        /// El valor evaluado que se encuentra dentro del rango especificado.
        /// </returns>
        public static double Wrap(this in double expression, in double min, in double max)
        {
            if (double.IsNaN(expression)) return double.NaN;
            if (expression.CompareTo(max) > 0) return (expression - (1 + max - min)).Wrap(min, max);
            if (expression.CompareTo(min) < 0) return (expression + (1 + max - min)).Wrap(min, max);
            return expression;
        }

        /// <summary>
        /// Establece puntos de sobreflujo intencional para evaluar una expresión.
        /// </summary>
        /// <param name="expression">Expresión a evaluar.</param>
        /// <param name="max">Límite superior de salida, inclusive.</param>
        /// <param name="min">Límite inferior de salida, inclusive.</param>
        /// <returns>
        /// El valor evaluado que se encuentra dentro del rango especificado.
        /// </returns>
        public static float Wrap(this in float expression, in float min, in float max)
        {
            if (float.IsNaN(expression)) return float.NaN;
            if (expression.CompareTo(max) > 0) return (expression - (1 + max - min)).Wrap(min, max);
            if (expression.CompareTo(min) < 0) return (expression + (1 + max - min)).Wrap(min, max);
            return expression;
        }
#endif
    }
}