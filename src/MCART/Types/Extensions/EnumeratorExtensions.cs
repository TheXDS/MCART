/*
EnumeratorExtensions.cs

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

namespace TheXDS.MCART.Types.Factory;
using System;
using System.Collections;

/// <summary>
/// Extensiones para todos los tipos que implementen 
/// <see cref="IEnumerator"/>.
/// </summary>
public static partial class EnumeratorExtensions
{
    /// <summary>
    /// Desplaza el enumerador una cantidad específica de elementos.
    /// </summary>
    /// <param name="enumerator">
    /// Enumerador a recorrer.
    /// </param>
    /// <param name="steps">
    /// Número de elementos a saltar.
    /// </param>
    /// <returns>
    /// La cantidad de elementos saltados exitosamente.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Se produce si <paramref name="steps"/> es inferior a cero.
    /// </exception>
    public static int Skip(this IEnumerator enumerator, int steps)
    {
        Skip_Contract(enumerator, steps);
        int j;
        for (j = 0; j < steps; j++)
        {
            if (!enumerator.MoveNext())
            {
                j++;
                break;
            }
        }
        return j;
    }
}
