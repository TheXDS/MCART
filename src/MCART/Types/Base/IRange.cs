/*
IRange.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Este archivo define la estructura Range<T>, la cual permite representar rangos
de valores.

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

namespace TheXDS.MCART.Types.Base;
using System;
using TheXDS.MCART.Helpers;

/// <summary>
/// Interfaz que define un rango de valores.
/// </summary>
/// <typeparam name="T">Tipo base del rango de valores.</typeparam>
public interface IRange<T> where T : IComparable<T>
{
    /// <summary>
    /// Valor máximo del rango.
    /// </summary>
    T Maximum { get; set; }

    /// <summary>
    /// Obtiene o establece un valor que determina si el valor máximo
    /// es parte del rango.
    /// </summary>
    /// 
    bool MaxInclusive { get; set; }

    /// <summary>
    /// Valor mínimo del rango.
    /// </summary>
    T Minimum { get; set; }

    /// <summary>
    /// Obtiene o establece un valor que determina si el valor mínimo
    /// es parte del rango.
    /// </summary>
    bool MinInclusive { get; set; }

    /// <summary>
    /// Determina si un <see cref="IRange{T}"/> intersecta a este.
    /// </summary>
    /// <param name="other">Rango a comprobar.</param>
    /// <returns>
    /// <see langword="true"/> si <paramref name="other"/> intersecta a
    /// este <see cref="IRange{T}"/>, <see langword="false"/> en caso
    /// contrario.
    /// </returns>
    bool Intersects(IRange<T> other) => IsWithin(other.Maximum) || IsWithin(other.Minimum);

    /// <summary>
    /// Comprueba si un valor <typeparamref name="T"/> se encuentra
    /// dentro de este <see cref="IRange{T}"/>.
    /// </summary>
    /// <param name="value">Valor a comporbar.</param>
    /// <returns>
    /// <see langword="true"/> si el valor se encuentra dentro de este
    /// <see cref="IRange{T}"/>, <see langword="false"/> en caso
    /// contrario.
    /// </returns>
    bool IsWithin(T value) => value.IsBetween(Minimum, Maximum, MinInclusive, MaxInclusive);
}
