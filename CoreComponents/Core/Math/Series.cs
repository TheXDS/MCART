/*
Series.cs

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

using System.Collections.Generic;

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
    ///     Series matemáticas
    /// </summary>
    public static class Series
    {
        /* -= NOTA =-
         * Las series utilizan enumeradores para exponer las series 
         * completas de una manera infinita.
         *  
         * Es necesario recalcar que, si se utilizan estas funciones de
         * manera incorrecta, el programa fallará con un error de
         * sobreflujo o de pila, o bien, el programa podría dejar de
         * responder.
         */

        /// <summary>
        ///     Expone un enumerador que contiene la secuencia completa de
        ///     Fibonacci.
        /// </summary>
        /// <returns>
        ///     Un <see cref="IEnumerable{T}" /> con la secuencia infinita de
        ///     Fibonacci.
        /// </returns>
        public static IEnumerable<long> Fibonacci()
        {
            long a = 0;
            long b = 1;
            unchecked
            {
                while (b > 0)
                {
                    yield return a;
                    yield return b;
                    a += b;
                    b += a;
                }
            }
        }

        /// <summary>
        ///     Expone un enumerador que contiene la secuencia completa de
        ///     Lucas.
        /// </summary>
        /// <returns>
        ///     Un <see cref="IEnumerable{T}" /> con la secuencia infinita de
        ///     Lucas.
        /// </returns>
        public static IEnumerable<long> Lucas()
        {
            long a = 2;
            long b = 1;
            unchecked
            {
                while (b > 0)
                {
                    yield return a;
                    yield return b;
                    a += b;
                    b += a;
                }
            }
        }
    }
}