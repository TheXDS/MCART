//
//  Math.cs
//
//  This file is part of MCART
//
//  Author:
//       César Andrés Morgan <xds_xps_ivx@hotmail.com>
//
//  Copyright (c) 2011 - 2017 César Andrés Morgan
//
//  MCART is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  MCART is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using MCART.Attributes;
using MCART.Types;

namespace MCART
{
    /// <summary>
    /// Funciones matemáticas varias
    /// </summary>
    public static class Math
    {
        #region Constantes
        /// <summary>
        /// Representa la proporción de 1 grado DEG sobre PI
        /// </summary>
        public const double Deg_Rad = System.Math.PI / 180;
        #endregion

        /// <summary>
        /// Series matemáticas
        /// </summary>
        public static class Series
        {
            /*
			Las series utilizan enumeradores para exponer las series completas
			de una manera infinita. Es necesario silenciar la advertencia en
			MonoDevelop que indica que estas funciones nunca finalizan.

			Es necesario recalcar que, si se utilizan estas funciones de manera
			incorrecta, el programa fallará con un error de sobreflujo o de pila, o
			bien, el programa podría dejar de responder.
			*/
#pragma warning disable RECS0135
            /// <summary>
            /// Expone un enumerador que contiene la secuencia completa de
            /// Fibonacci.
            /// </summary>
            /// <returns>
            /// Un <see cref="IEnumerable{T}"/> con la secuencia infinita de
            /// Fibonacci.
            /// </returns>
            public static IEnumerable<long> Fibonacci()
            {
                long a = 0;
                long b = 1;
                while (true)
                {
                    yield return a;
                    yield return b;
                    a += b;
                    b += a;
                }
            }
            /// <summary>
            /// Expone un enumerador que contiene la secuencia completa de
            /// Lucas.
            /// </summary>
            /// <returns>
            /// Un <see cref="IEnumerable{T}"/> con la secuencia infinita de
            /// Lucas.
            /// </returns>
            public static IEnumerable<long> Lucas()
            {
                long a = 2;
                long b = 1;
                while (true)
                {
                    yield return a;
                    yield return b;
                    a += b;
                    b += a;
                }
            }
#pragma warning restore RECS0135
        }
        /// <summary>
        /// Fórmulas de suavizado.
        /// </summary>
        public static class Tween
        {
            /// <summary>
            /// Realiza un suavizado lineal de un valor.
            /// </summary>
            /// <returns>
            /// Un valor correspondiente al suavizado aplicado.
            /// </returns>
            /// <param name="step">Número de paso a suavizar.</param>
            /// <param name="total">Total de pasos.</param>
            public static float Linear(int step, int total) => (float)step / total;
            /// <summary>
            /// Realiza un suavizado cuadrático de un valor.
            /// </summary>
            /// <returns>
            /// Un valor correspondiente al suavizado aplicado.
            /// </returns>
            /// <param name="step">Número de paso a suavizar.</param>
            /// <param name="total">Total de pasos.</param>
            public static float Quadratic(int step, int total)
            {
                float t = ((float)step / total);
                return (t * t) / (2 * t * t - 2 * t + 1);
            }
            /// <summary>
            /// Realiza un suavizado cúbico de un valor.
            /// </summary>
            /// <returns>
            /// Un valor correspondiente al suavizado aplicado.
            /// </returns>
            /// <param name="step">Número de paso a suavizar.</param>
            /// <param name="total">Total de pasos.</param>
            public static float Cubic(int step, int total)
            {
                float t = ((float)step / total);
                return (t * t * t) / (3 * t * t - 3 * t + 1);
            }
            /// <summary>
            /// Realiza un suavizado cuártico de un valor.
            /// </summary>
            /// <returns>
            /// Un valor correspondiente al suavizado aplicado.
            /// </returns>
            /// <param name="step">Número de paso a suavizar.</param>
            /// <param name="total">Total de pasos.</param>
            public static float Quartic(int step, int total)
            {
                float t = ((float)step / total);
                return -((t - 1) * (t - 1) * (t - 1) * (t - 1)) + 1;
            }
        }
        /// <summary>
        /// Comprueba si un número es primo.
        /// </summary>
        /// <returns>
        /// <c>true</c>si el número es primo, <c>false</c> en caso contrario.
        /// </returns>
        /// <param name="i">Número a comprobar.</param>
        public static bool IsPrime(this long i)
        {
            long s = i / 2;
            for (long j = 3; j < s; j += 2)
                if (i % j == 0) return false;
            return true;
        }
        /// <summary>
        /// Comprueba si un número es primo.
        /// </summary>
        /// <returns>
        /// <c>true</c>si el número es primo, <c>false</c> en caso contrario.
        /// </returns>
        /// <param name="i">Número a comprobar.</param>
        [Thunk] public static bool IsPrime(this int i) => ((long)i).IsPrime();
        /// <summary>
        /// Comprueba si un número es primo.
        /// </summary>
        /// <returns>
        /// <c>true</c>si el número es primo, <c>false</c> en caso contrario.
        /// </returns>
        /// <param name="i">Número a comprobar.</param>
        [Thunk] public static bool IsPrime(this uint i) => ((long)i).IsPrime();
        /// <summary>
        /// Comprueba si un número es primo.
        /// </summary>
        /// <returns>
        /// <c>true</c>si el número es primo, <c>false</c> en caso contrario.
        /// </returns>
        /// <param name="i">Número a comprobar.</param>
        [Thunk] public static bool IsPrime(this short i) => ((long)i).IsPrime();
        /// <summary>
        /// Comprueba si un número es primo.
        /// </summary>
        /// <returns>
        /// <c>true</c>si el número es primo, <c>false</c> en caso contrario.
        /// </returns>
        /// <param name="i">Número a comprobar.</param>
        [Thunk] public static bool IsPrime(this ushort i) => ((long)i).IsPrime();
        /// <summary>
        /// Comprueba si un número es primo.
        /// </summary>
        /// <returns>
        /// <c>true</c>si el número es primo, <c>false</c> en caso contrario.
        /// </returns>
        /// <param name="i">Número a comprobar.</param>
        [Thunk] public static bool IsPrime(this sbyte i) => ((long)i).IsPrime();
        /// <summary>
        /// Comprueba si un número es primo.
        /// </summary>
        /// <returns>
        /// <c>true</c>si el número es primo, <c>false</c> en caso contrario.
        /// </returns>
        /// <param name="i">Número a comprobar.</param>
        [Thunk] public static bool IsPrime(this byte i) => ((long)i).IsPrime();
        /// <summary>
        /// Determina si un <see cref="double"/> es un número real operable.
        /// </summary>
        /// <param name="x"><see cref="double"/> a comprobar.</param>
        /// <returns>
        /// <c>true</c> si <paramref name="x"/> es un número real
        /// <see cref="double"/> operable, en otras palabras, si no es igual a 
        /// <see cref="double.NaN"/>, <see cref="double.PositiveInfinity"/> o
        /// <see cref="double.NegativeInfinity"/>; en cuyo caso se devuelve 
        /// <c>false</c>.
        /// </returns>
        public static bool IsValid(this double x) => !(double.IsNaN(x) || double.IsInfinity(x));
        /// <summary>
        /// Determina si un <see cref="float"/> es un número real operable.
        /// </summary>
        /// <param name="x"><see cref="float"/> a comprobar.</param>
        /// <returns>
        /// <c>true</c> si <paramref name="x"/> es un número real
        /// <see cref="float"/> operable, en otras palabras, si no es igual a 
        /// <see cref="float.NaN"/>, <see cref="float.PositiveInfinity"/> o
        /// <see cref="float.NegativeInfinity"/>; en cuyo caso se devuelve 
        /// <c>false</c>.
        /// </returns>
        public static bool IsValid(this float x) => !(float.IsNaN(x) || float.IsInfinity(x));
        /// <summary>
        /// Determina si una colección de <see cref="double"/> son números 
        /// reales operables.
        /// </summary>
        /// <param name="x">
        /// Colección  de <see cref="double"/> a comprobar.
        /// </param>
        /// <returns>
        /// <c>true</c> si todos los elementos de <paramref name="x"/> son 
        /// números operables, en otras palabras, si no son NaN o Infinito; en 
        /// caso contrario, se devuelve <c>false</c>.
        /// </returns>
        public static bool AreValid(params double[] x)
        {
            foreach (double j in x) if (!IsValid(j)) return false;
            return true;
        }
        /// <summary>
        /// Determina si una colección de <see cref="float"/> son números 
        /// reales operables.
        /// </summary>
        /// <param name="x">
        /// Colección  de <see cref="float"/> a comprobar.
        /// </param>
        /// <returns>
        /// <c>true</c> si todos los elementos de <paramref name="x"/> son 
        /// números operables, en otras palabras, si no son NaN o Infinito; en 
        /// caso contrario, se devuelve <c>false</c>.
        /// </returns>
        public static bool AreValid(params float[] x)
        {
            foreach (float j in x) if (!IsValid(j)) return false;
            return true;
        }
        /// <summary>
        /// Calcula la potencia de dos más cercana mayor o igual al número
        /// </summary>
        /// <param name="x">Número de entrada. Se buscará una potencia de dos mayor o igual a este valor.</param>
        /// <returns>Un valor ULong que es resultado de la operación.</returns>
        public static ulong Nearest2Pow(uint x)
        {
            ulong c = 1;
            while (!(c >= x)) c *= 2;
            return c;
        }
        /// <summary>
        /// Devuelve el primer múltiplo de <paramref name="multiplier"/> que es mayor que <paramref name="x"/>
        /// </summary>
        /// <param name="x">Número objetivo</param>
        /// <param name="multiplier">Base multiplicativa. Esta función devolverá un múltiplo de este valor que sea mayor a <paramref name="x"/></param>
        /// <returns>Un <see cref="double"/> que es el primer múltiplo de <paramref name="multiplier"/> que es mayor que <paramref name="x"/></returns>
        public static double NearestMultiplyUp(double x, double multiplier)
        {
            double a = 1;
            if (ArePositives(x, multiplier))
                while (!(a > x)) a *= multiplier;
            return a;
        }
        /// <summary>
        /// Devuelve <c>True</c> si todos los números son positivos.
        /// </summary>
        /// <param name="x">números a comprobar.</param>
        public static bool ArePositives(params double[] x)
        {
            foreach (double j in x) if (j <= 0) return false;
            return true;
        }
        /// <summary>
        /// Devuelve <c>True</c> si todos los números son negativos.
        /// </summary>
        /// <param name="x">números a comprobar.</param>
        public static bool AreNegatives(params double[] x)
        {
            foreach (double j in x) if (j >= 0) return false;
            return true;
        }
        /// <summary>
        /// Devuelve <c>True</c> si todos los números son iguales a cero.
        /// </summary>
        /// <param name="x">números a comprobar.</param>
        public static bool AreZero<T>(params T[] x) where T : IComparable<T>
        {
            foreach (T j in x) if (j.CompareTo(default(T)) != 0) return false;
            return true;
        }
        /// <summary>
        /// Devuelve <c>true</c> si todos los números son distintos de cero.
        /// </summary>
        /// <param name="x">números a comprobar.</param>
        public static bool AreNotZero<T>(params T[] x) where T : IComparable<T>
        {
            foreach (T j in x) if (j.CompareTo(default(T)) == 0) return false;
            return true;
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
        /// <see cref="double.NaN"/>, <see cref="double.NegativeInfinity"/> y
        /// <see cref="double.PositiveInfinity"/>.
        /// </remarks>
        public static double Clamp(this double expression, double min, double max)
        {
            if (!double.IsNaN(expression))
            {
                if (expression > max) return max;
                if (expression < min) return min;
                return expression;
            }
            return double.NaN;
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
        /// <see cref="double.NaN"/>, <see cref="double.NegativeInfinity"/> y
        /// <see cref="double.PositiveInfinity"/>.
        /// </remarks>
        public static double Clamp(this double expression, double max) => Clamp(expression, double.NegativeInfinity, max);
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
        /// <see cref="float.NaN"/>, <see cref="float.NegativeInfinity"/> y
        /// <see cref="float.PositiveInfinity"/>.
        /// </remarks>
        public static float Clamp(this float expression, float max) => Clamp(expression, float.NegativeInfinity, max);
#endif
        /// <summary>
        /// Establece límites de sobreflujo para evaluar una expresión.
        /// </summary>
        /// <param name="expression">Expresión a evaluar.</param>
        /// <param name="max">Límite superior de salida, inclusive.</param>
        /// <param name="min">Límite inferior de salida, inclusive.</param>
        /// <returns>
        /// El valor evaluado que se encuentra dentro del rango especificado.
        /// </returns>
        public static T Clamp<T>(this T expression, T min, T max) where T : IComparable<T>
        {
            if (expression.CompareTo(max) > 0) return max;
            if (expression.CompareTo(min) < 0) return min;
            return expression;
        }
        /// <summary>
        /// Establece límites de sobreflujo para evaluar una expresión.
        /// </summary>
        /// <param name="expression">Expresión a evaluar.</param>
        /// <param name="max">Límite superior de salida, inclusive.</param>
        /// <returns>
        /// El valor evaluado que se encuentra entre 0 y 
        /// <paramref name="max"/>.
        /// </returns>
        public static T Clamp<T>(this T expression, T max) where T :
            IComparable,
            IComparable<T>
        {
            return Clamp(expression, default(T), max);
        }
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
        public static float Clamp(this float expression, float min, float max)
        {
            if (!float.IsNaN(expression))
            {
                if (expression > max) return max;
                if (expression < min) return min;
                return expression;
            }
            return float.NaN;
        }
#endif

        /// <summary>
        /// Establece puntos de sobreflujo intencional para evaluar una expresión.
        /// </summary>
        /// <param name="expression">Expresión a evaluar.</param>
        /// <param name="max">Límite superior de salida, inclusive.</param>
        /// <param name="min">Límite inferior de salida, inclusive.</param>
        /// <returns>
        /// El valor evaluado que se encuentra dentro del rango especificado.
        /// </returns>
        public static float Wrap(this float expression, float min, float max)
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
        public static double Wrap(this double expression, double min, double max)
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
        public static int Wrap(this int expression, int min, int max)
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
        public static uint Wrap(this uint expression, uint min, uint max)
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
        public static long Wrap(this long expression, long min, long max)
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
        public static ulong Wrap(this ulong expression, ulong min, ulong max)
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
        public static decimal Wrap(this decimal expression, decimal min, decimal max)
        {
            if (expression.CompareTo(max) > 0) return (expression - (1 + max - min)).Wrap(min, max);
            if (expression.CompareTo(min) < 0) return (expression + (1 + max - min)).Wrap(min, max);
            return expression;
        }
#endif
        /// <summary>
        /// Determina si un <see cref="double"/> es un número entero.
        /// </summary>
        /// <param name="x">Valor a comprobar.</param>
        /// <returns><c>True</c> si el valor es entero; de lo contrario, <c>False</c></returns>
        public static bool IsWhole(this double x) => !x.ToString().Contains(".");
        /// <summary>
        /// Obtiene las cooerdenadas X,Y de una posición específica dentro de un
        /// bézier cuadrático
        /// </summary>
        /// <param name="Position">
        /// Posición a obtener. Debe ser un <see cref="double"/> entre 0.0 y 
        /// 1.0.
        /// </param>
        /// <param name="StartPoint">
        /// Punto inicial del bézier cuadrático.
        /// </param>
        /// <param name="ControlPoint">
        /// Punto de control del bézier cuadrático.
        /// </param>
        /// <param name="EndPoint">Punto final del bézier cuadrático.</param>
        /// <returns>
        /// Un <see cref="Point"/> con las coordenadas correspondientes a la
        /// posición dentro del bézier cuadrático dado por
        /// <paramref name="Position"/>.
        /// </returns>
        public static Point GetQuadBezierPoint(double Position, Point StartPoint, Point ControlPoint, Point EndPoint)
        {
            if (!Position.IsBetween(0, 1)) throw new ArgumentOutOfRangeException(nameof(Position));
            double a = 1 - Position;
            double b = a * a;
            double c = 2 * a * Position;
            double d = Position * Position;
            return new Point(
                b * StartPoint.X + c * ControlPoint.X + d * EndPoint.X,
                b * StartPoint.Y + c * ControlPoint.Y + d * EndPoint.Y);
        }
        /// <summary>
        /// Obtiene las coordenadas de un punto dentro de un arco.
        /// </summary>
        /// <param name="radius">Radio del arco.</param>
        /// <param name="startAngle">
        /// Ángulo inicial del arco; en el sentido de las agujas del reloj.
        /// </param>
        /// <param name="endAngle">
        /// Ángulo final del arco; en el sentido de las agujas del reloj.
        /// </param>
        /// <param name="place">Posición a obtener dentro del arco.</param>
        /// <returns>
        /// Un conjunto de coordenadas con la posición del punto solicitado.
        /// </returns>
        public static Point GetArcPoint(double radius, double startAngle, double endAngle, double place = 0.5)
        {
            double x = (startAngle - endAngle) * place * Deg_Rad;
            return new Point(System.Math.Sin(x) * radius, System.Math.Cos(x) * radius);
        }
        /// <summary>
        /// Obtiene las coordenadas de un punto dentro de un círculo.
        /// </summary>
        /// <param name="radius">Radio del círculo.</param>
        /// <param name="place">Posición a obtener dentro del círculo.</param>
        /// <returns>
        /// Un conjunto de coordenadas con la posición del punto solicitado.
        /// </returns>
        [Thunk] public static Point GetCirclePoint(double radius, double place) => GetArcPoint(radius, 0, 360, place);
    }
}