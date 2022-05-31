/*
Tween.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Este archivo contiene diversas fórmulas de suavizado.

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2022 César Andrés Morgan

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

namespace TheXDS.MCART.Math
{
    /// <summary>
    /// Contiene diversas fórmulas de suavizado.
    /// </summary>
    public static class Tween
    {
        /// <summary>
        /// Describe una función que aplica una transformación de suavizado.
        /// </summary>
        /// <returns>
        /// Un valor correspondiente al suavizado aplicado.
        /// </returns>
        /// <param name="position">
        /// Valor entre <c>0.0</c> y <c>1.0</c> que indica la posición en la línea de tiempo.
        /// </param>
        public delegate double TweenFunction(in double position);

        /// <summary>
        /// Realiza un suavizado lineal de un valor.
        /// </summary>
        /// <returns>
        /// Un valor correspondiente al suavizado aplicado.
        /// </returns>
        /// <param name="position">
        /// Valor entre <c>0.0</c> y <c>1.0</c> que indica la posición en la línea de tiempo.
        /// </param>
        public static double Linear(in double position) => position;

        /// <summary>
        /// Realiza un suavizado cuadrático de un valor.
        /// </summary>
        /// <returns>
        /// Un valor correspondiente al suavizado aplicado.
        /// </returns>
        /// <param name="position">
        /// Valor entre <c>0.0</c> y <c>1.0</c> que indica la posición en la línea de tiempo.
        /// </param>
        public static double Quadratic(in double position)
        {
            double x2 = System.Math.Pow(position, 2);
            return x2 / ((2 * x2) - (2 * position) + 1);
        }

        /// <summary>
        /// Realiza un suavizado cúbico de un valor.
        /// </summary>
        /// <returns>
        /// Un valor correspondiente al suavizado aplicado.
        /// </returns>
        /// <param name="position">
        /// Valor entre <c>0.0</c> y <c>1.0</c> que indica la posición en la línea de tiempo.
        /// </param>
        public static double Cubic(in double position)
        {
            return System.Math.Pow(position, 3) / ((3 * System.Math.Pow(position, 2)) - (3 * position) + 1);
        }

        /// <summary>
        /// Realiza un suavizado cuártico de un valor.
        /// </summary>
        /// <returns>
        /// Un valor correspondiente al suavizado aplicado.
        /// </returns>
        /// <param name="position">
        /// Valor entre <c>0.0</c> y <c>1.0</c> que indica la posición en la línea de tiempo.
        /// </param>
        public static double Quartic(in double position)
        {
            return -System.Math.Pow(position - 1, 4) + 1;
        }

        /// <summary>
        /// Realiza un suavizado con sacudida de un valor.
        /// </summary>
        /// <returns>
        /// Un valor correspondiente al suavizado aplicado.
        /// </returns>
        /// <param name="position">
        /// Valor entre <c>0.0</c> y <c>1.0</c> que indica la posición en la línea de tiempo.
        /// </param>
        public static double Shaky(in double position) => Shaky(position, 10);

        /// <summary>
        /// Realiza un suavizado con sacudida de un valor.
        /// </summary>
        /// <returns>
        /// Un valor correspondiente al suavizado aplicado.
        /// </returns>
        /// <param name="position">
        /// Valor entre <c>0.0</c> y <c>1.0</c> que indica la posición en la línea de tiempo.
        /// </param>
        /// <param name="shakes">Cantidad de sacudidas a realizar.</param>
        public static double Shaky(in double position, in int shakes)
        {
            return 1 - (System.Math.Cos(shakes * System.Math.PI * position) * System.Math.Cos(System.Math.PI / 2 * position));
        }

        /// <summary>
        /// Realiza un suavizado con rebote de un valor.
        /// </summary>
        /// <returns>
        /// Un valor correspondiente al suavizado aplicado.
        /// </returns>
        /// <param name="position">
        /// Valor entre <c>0.0</c> y <c>1.0</c> que indica la posición en la línea de tiempo.
        /// </param>
        public static double Bouncy(in double position) => Bouncy(position, 10);

        /// <summary>
        /// Realiza un suavizado con rebote de un valor.
        /// </summary>
        /// <returns>
        /// Un valor correspondiente al suavizado aplicado.
        /// </returns>
        /// <param name="position">
        /// Valor entre <c>0.0</c> y <c>1.0</c> que indica la posición en la línea de tiempo.
        /// </param>
        /// <param name="bounces">Cantidad de rebotes a calcular.</param>
        public static double Bouncy(in double position, in int bounces) => Bouncy(position, bounces, 8);

        /// <summary>
        /// Realiza un suavizado con rebote de un valor.
        /// </summary>
        /// <returns>
        /// Un valor correspondiente al suavizado aplicado.
        /// </returns>
        /// <param name="position">
        /// Valor entre <c>0.0</c> y <c>1.0</c> que indica la posición en la línea de tiempo.
        /// </param>
        /// <param name="bounces">Cantidad de rebotes a calcular.</param>
        /// <param name="damping">Amortiguación a calcular.</param>
        public static double Bouncy(in double position, in int bounces, in int damping)
        {
            return 1 - (System.Math.Cos(bounces * System.Math.PI * position) * (1 - System.Math.Pow(position, 1 / (double)damping)));
        }
    }
}