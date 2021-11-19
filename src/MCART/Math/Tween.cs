/*
Tween.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Este archivo contiene diversas fórmulas de suavizado.

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