/*
Algebra_nonCLS.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2019 César Andrés Morgan

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

#if !CLSCompliance
using TheXDS.MCART.Attributes;

namespace TheXDS.MCART.Math
{
    public static partial class Algebra
    {
        /// <summary>
        /// Comprueba si un número es primo.
        /// </summary>
        /// <returns>
        /// <see langword="true"/>si el número es primo, <see langword="false"/> en caso contrario.
        /// </returns>
        /// <param name="number">Número a comprobar.</param>
        [Thunk] public static bool IsPrime(this in uint number) => ((long)number).IsPrime();
        
        /// <summary>
        /// Comprueba si un número es primo.
        /// </summary>
        /// <returns>
        /// <see langword="true"/>si el número es primo, <see langword="false"/> en caso contrario.
        /// </returns>
        /// <param name="number">Número a comprobar.</param>
        
        [Thunk] public static bool IsPrime(this in ushort number) => ((long)number).IsPrime();
        /// <summary>
        /// Comprueba si un número es primo.
        /// </summary>
        /// <returns>
        /// <see langword="true"/>si el número es primo, <see langword="false"/> en caso contrario.
        /// </returns>
        /// <param name="number">Número a comprobar.</param>
        [Thunk] public static bool IsPrime(this in sbyte number) => ((long)number).IsPrime();
    
        /// <summary>
        /// Determina si el valor es una potencia de 2.
        /// </summary>
        /// <param name="value">Valor a comprobar.</param>
        /// <returns>
        /// <see langword="true"/> si el valor es una potencia de 2,
        /// <see langword="false"/> en caso contrario.
        /// </returns>
        public static bool IsTwoPow(in sbyte value) => (value & ~1).BitCount() == 1;

        /// <summary>
        /// Determina si el valor es una potencia de 2.
        /// </summary>
        /// <param name="value">Valor a comprobar.</param>
        /// <returns>
        /// <see langword="true"/> si el valor es una potencia de 2,
        /// <see langword="false"/> en caso contrario.
        /// </returns>
        public static bool IsTwoPow(in ushort value) => (value & ~1).BitCount() == 1;

        /// <summary>
        /// Determina si el valor es una potencia de 2.
        /// </summary>
        /// <param name="value">Valor a comprobar.</param>
        /// <returns>
        /// <see langword="true"/> si el valor es una potencia de 2,
        /// <see langword="false"/> en caso contrario.
        /// </returns>
        public static bool IsTwoPow(in uint value) => (value & ~1).BitCount() == 1;

        /// <summary>
        /// Determina si el valor es una potencia de 2.
        /// </summary>
        /// <param name="value">Valor a comprobar.</param>
        /// <returns>
        /// <see langword="true"/> si el valor es una potencia de 2,
        /// <see langword="false"/> en caso contrario.
        /// </returns>
        public static bool IsTwoPow(in ulong value) => unchecked((long)(value & ~1UL)).BitCount() == 1;
    }
}
#endif
