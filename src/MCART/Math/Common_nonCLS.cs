/*
Common_nonCLS.cs

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

#if !(RatherDRY || CLSCompliance)
using System;

namespace TheXDS.MCART.Math
{
    /// <summary>
    /// Contiene métodos de manipulación matemática estándar.
    /// </summary>
    public static partial class Common
    {
        /// <summary>
        /// Establece puntos de sobreflujo intencional para evaluar una expresión.
        /// </summary>
        /// <param name="expression">Expresión a evaluar.</param>
        /// <param name="max">Límite superior de salida, inclusive.</param>
        /// <param name="min">Límite inferior de salida, inclusive.</param>
        /// <returns>
        /// El valor evaluado que se encuentra dentro del rango especificado.
        /// </returns>
        public static int Wrap(this in sbyte expression, in sbyte min, in sbyte max)
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
        public static int Wrap(this in ushort expression, in ushort min, in ushort max)
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
        public static uint Wrap(this in uint expression, in uint min, in uint max)
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
        public static ulong Wrap(this in ulong expression, in ulong min, in ulong max)
        {
            if (expression.CompareTo(max) > 0) return (expression - (1 + max - min)).Wrap(min, max);
            if (expression.CompareTo(min) < 0) return (expression + (1 + max - min)).Wrap(min, max);
            return expression;
        }        
    }
}
#endif