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
using MCART.Types;
namespace MCART
{
    /// <summary>
    /// Funciones matemáticas varias
    /// </summary>
    public static class Math
    {
        /// <summary>
        /// Series matemáticas
        /// </summary>
        public static class Series
        {
            /// <summary>
            /// Calcula el n-ésimo elemento de la serie de Lucas
            /// </summary>
            /// <param name="Item">Número de elemento a calcular</param>
            /// <returns>Un <see cref="long"/> que es el resultado del cálculo del n-ésimo elemento de la serie</returns>
            public static long LucasNumber(int Item)
            {
                long a = 1;
                long b = 3;
                long c = 0;
                if (Item == 1)
                    return 1;
                if (Item == 2)
                    return 3;
                for (short j = 3; j <= Item; j++)
                {
                    c = a + b;
                    a = b;
                    b = c;
                }
                return c;
            }
            /// <summary>
            /// Devuelve el resíduo de la n-ésima posición de la serie Lucas-Lehmer
            /// </summary>
            /// <param name="Item">Número de elemento</param>
            /// <param name="x">Factor de división</param>
            /// <returns></returns>
            public static long LucasLehmerRemainder(long Item, long x)
            {
                long a = 4;
                //Punto inicial de la serie
                if (x == 0)
                    return 0;
                for (long j = 2; j <= Item; j++) a = (long)((System.Math.Pow(a, 2)) - 2) % x;
                return a;
            }
        }
        /// <summary>
        /// Comprueba si el número NO es primo.
        /// </summary>
        /// <param name="x"><see cref="int"/> a comprobar.</param>
        /// <returns>
        /// Un valor <see cref="bool"/> que es <code>true</code> si el número NO es primo. Si es primo, o si es un 
        /// compuesto que pasa la prueba de Lucas, <code>false</code>.</returns>
        /// <remarks>
        /// Se utiliza la prueba de la serie de Lucas para la comprobación, lo que no garantiza que un número sea 
        /// efectivamente primo.
        /// </remarks>
        public static bool IsNotPrime(this int x)
        {
            return (Series.LucasNumber(x) - 1) % x == 0;
        }
        /// <summary>
		/// Representa la proporción de 1 grado DEG sobre PI
		/// </summary>
		public const double Deg_Rad = System.Math.PI / 180;
        /// <summary>
        /// Determina si un <see cref="double"/> es un número real operable
        /// </summary>
        /// <param name="x"><see cref="double"/> a comprobar</param>
        /// <returns>
        /// un valor booleano que indica si <paramref name="x"/> es un número operable, en otras palabras, si no es 
        /// igual a <see cref="double.NaN"/>, <see cref="double.PositiveInfinity"/> o
        /// <see cref="double.NegativeInfinity"/>.
        /// </returns>
        public static bool IsValid(this double x)
        {
            return !(double.IsNaN(x) || double.IsInfinity(x));
        }
        /// <summary>
        /// Determina si una colección de <see cref="double"/> son números reales operables
        /// </summary>
        /// <param name="x">Colección  de <see cref="double"/> a comprobar</param>
        /// <returns>un valor booleano que indica si todos los elementos de <paramref name="x"/> son números operables, en otras palabras, si no son NaN o Infinito</returns>
        public static bool AreValidDoubles(params double[] x)
        {
            foreach (double j in x) if (!IsValid(j)) return false;
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
            while (!(c >= x))
            {
                c *= 2;
            }
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
            {
                while (!(a > x))
                {
                    a *= multiplier;
                }
            }
            return a;
        }
        /// <summary>
        /// Devuelve <c>True</c> si todos los números son positivos
        /// </summary>
        /// <param name="x">números a comprobar</param>
        public static bool ArePositives(params double[] x)
        {
            foreach (double j in x)
            {
                if (j <= 0)
                    return false;
            }
            return true;
        }
        /// <summary>
        /// Devuelve <c>True</c> si todos los números son negativos
        /// </summary>
        /// <param name="x">números a comprobar</param>
        public static bool AreNegatives(params double[] x)
        {
            foreach (double j in x)
            {
                if (j >= 0)
                    return false;
            }
            return true;
        }
        /// <summary>
        /// Devuelve <c>True</c> si todos los números son iguales a cero
        /// </summary>
        /// <param name="x">números a comprobar</param>
        public static bool AreZero<T>(params T[] x) where T : struct,
            IComparable,
            IComparable<T>,
            IConvertible,
            IEquatable<T>,
            IFormattable
        {
            foreach (T j in x) if (j.CompareTo(0) != 0) return false;
            return true;
        }
        /// <summary>
        /// Devuelve <c>True</c> si todos los números son distintos de cero
        /// </summary>
        /// <param name="x">números a comprobar</param>
        public static bool AreNotZero<T>(params T[] x) where T :
            IComparable,
            IComparable<T>,
            IConvertible,
            IEquatable<T>,
            IFormattable
        {
            foreach (T j in x) if (j.CompareTo(0) == 0) return false;
            return true;
        }
        /// <summary>
        /// Establece límites de sobreflujo para evaluar una expresión.
        /// </summary>
        /// <param name="expression">Expresión a evaluar.</param>
        /// <param name="max">Límite superior de salida, inclusive.</param>
        /// <param name="min">Límite inferior de salida, inclusive.</param>
        /// <returns>
        /// El valor evaluado que se encuentra dentro del rango especificado.
        /// </returns>
        [Obsolete]
        public static double Clamp(this double expression, double max = double.NaN, double min = double.NaN)
        {
            if (IsValid(expression))
            {
                if (!double.IsNaN(max) && expression > max) return max;
                if (!double.IsNaN(min) && expression < min) return min;
                return expression;
            }
            return double.NaN;
        }
        /// <summary>
        /// Establece límites de sobreflujo para evaluar una expresión.
        /// </summary>
        /// <param name="expression">Expresión a evaluar.</param>
        /// <param name="max">Límite superior de salida, inclusive.</param>
        /// <param name="min">Límite inferior de salida, inclusive.</param>
        /// <returns>
        /// El valor evaluado que se encuentra dentro del rango especificado.
        /// </returns>
        public static T Clamp<T>(this T expression, T min, T max) where T :
            IComparable,
            IComparable<T>,
            IConvertible,
            IEquatable<T>,
            IFormattable
        {

            if (expression.CompareTo(max) > 0) return max;
            if (expression.CompareTo(min) < 0) return min;
            return expression;
        }

        /// <summary>
        /// Determina si un <see cref="double"/> es un número entero.
        /// </summary>
        /// <param name="x">Valor a comprobar.</param>
        /// <returns><c>True</c> si el valor es entero; de lo contrario, <c>False</c></returns>
        public static bool IsWhole(this double x) { return !x.ToString().Contains("."); }
        /// <summary>
        /// Obtiene las cooerdenadas X,Y de una posición específica dentro de un bézier cuadrático
        /// </summary>
        /// <param name="Position">Posición a obtener. Debe ser un <see cref="double"/> entre 0.0 y 1.0</param>
        /// <param name="StartPoint">Punto inicial del bézier cuadrático</param>
        /// <param name="ControlPoint">Punto de control del bézier cuadrático</param>
        /// <param name="EndPoint">Punto final del bézier cuadrático</param>
        /// <returns>Un <see cref="Point"/> con las coordenadas correspondientes a la posición dentro del bézier cuadrático dado por <paramref name="Position"/></returns>
        public static Point GetQuadBezierPoint(double Position, Point StartPoint, Point ControlPoint, Point EndPoint)
        {
            if (!Position.IsBetween(0, 1)) throw new ArgumentOutOfRangeException(nameof(Position));
            return new Point((1 - Position) * (1 - Position) * StartPoint.X + 2 * (1 - Position) * Position * ControlPoint.X + Position * Position * EndPoint.X, (1 - Position) * (1 - Position) * StartPoint.Y + 2 * (1 - Position) * Position * ControlPoint.Y + Position * Position * EndPoint.Y);
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
            double x = (startAngle - endAngle) * place;
            return new Point(System.Math.Sin(Deg_Rad * x) * radius, System.Math.Cos(Deg_Rad * x) * radius);
        }
        /// <summary>
        /// Obtiene las coordenadas de un punto dentro de un círculo.
        /// </summary>
        /// <param name="radius">Radio del círculo.</param>
        /// <param name="place">Posición a obtener dentro del círculo.</param>
        /// <returns>
        /// Un conjunto de coordenadas con la posición del punto solicitado.
        /// </returns>
        [Attributes.Thunk] public static Point GetCirclePoint(double radius, double place) => GetArcPoint(radius, 0, 360, place);
    }
}