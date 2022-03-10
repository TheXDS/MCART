/*
UiErrors.cs

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

namespace TheXDS.MCART.Resources;
using System;

/// <summary>
/// Contiene una serie de funciones que generan excepciones para errores
/// ocurridos dentro de las funciones de UI de MCART.
/// </summary>
public static class UiErrors
{
    /// <summary>
    /// Genera una excepción cuando un valor es <see langword="null"/>.
    /// </summary>
    /// <param name="v">
    /// Nombre del valor que es <see langword="null"/>.
    /// </param>
    /// <returns>
    /// Una nueva instancia de la clase <see cref="ArgumentException"/> que
    /// describe el error.
    /// </returns>
    public static Exception NullValue(string v)
    {
        return new ArgumentException(string.Format(Strings.UiErrors.NullValueException, v));
    }
}
