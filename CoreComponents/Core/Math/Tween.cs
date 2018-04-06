/*
Tween.cs

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
    ///     Contiene fórmulas de suavizado.
    /// </summary>
    public static class Tween
    {
        /// <summary>
        ///     Realiza un suavizado lineal de un valor.
        /// </summary>
        /// <returns>
        ///     Un valor correspondiente al suavizado aplicado.
        /// </returns>
        /// <param name="step">Número de paso a suavizar.</param>
        /// <param name="total">Total de pasos.</param>
        public static float Linear(int step, int total)
        {
            return (float) step / total;
        }

        /// <summary>
        ///     Realiza un suavizado cuadrático de un valor.
        /// </summary>
        /// <returns>
        ///     Un valor correspondiente al suavizado aplicado.
        /// </returns>
        /// <param name="step">Número de paso a suavizar.</param>
        /// <param name="total">Total de pasos.</param>
        public static float Quadratic(int step, int total)
        {
            var t = (float) step / total;
            return t * t / (2 * t * t - 2 * t + 1);
        }

        /// <summary>
        ///     Realiza un suavizado cúbico de un valor.
        /// </summary>
        /// <returns>
        ///     Un valor correspondiente al suavizado aplicado.
        /// </returns>
        /// <param name="step">Número de paso a suavizar.</param>
        /// <param name="total">Total de pasos.</param>
        public static float Cubic(int step, int total)
        {
            var t = (float) step / total;
            return t * t * t / (3 * t * t - 3 * t + 1);
        }

        /// <summary>
        ///     Realiza un suavizado cuártico de un valor.
        /// </summary>
        /// <returns>
        ///     Un valor correspondiente al suavizado aplicado.
        /// </returns>
        /// <param name="step">Número de paso a suavizar.</param>
        /// <param name="total">Total de pasos.</param>
        public static float Quartic(int step, int total)
        {
            var t = (float) step / total;
            return -((t - 1) * (t - 1) * (t - 1) * (t - 1)) + 1;
        }
    }
}