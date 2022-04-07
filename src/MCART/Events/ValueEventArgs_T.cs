/*
ValueEventArgs_T.cs

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

namespace TheXDS.MCART.Events;
using System;

/// <summary>
/// Incluye información de evento para cualquier clase con eventos que
/// incluyan tipos de valor.
/// </summary>
/// <typeparam name="T">
/// Tipo del valor almacenado por esta instancia.
/// </typeparam>
public class ValueEventArgs<T> : EventArgs
{
    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="ValueEventArgs{T}"/> con el valor provisto.
    /// </summary>
    /// <param name="value">Valor asociado al evento generado.</param>
    public ValueEventArgs(T value)
    {
        Value = value;
    }

    /// <summary>
    /// Crea implícitamente un <see cref="ValueEventArgs{T}"/> a partir
    /// de un valor <typeparamref name="T"/>.
    /// </summary>
    /// <param name="value">
    /// Valor a partir del cual crear el nuevo
    /// <see cref="ValueEventArgs{T}"/>.
    /// </param>
    public static implicit operator ValueEventArgs<T>(T value) => new(value);

    /// <summary>
    /// Devuelve el valor asociado a este evento.
    /// </summary>
    /// <returns>
    /// Un valor de tipo <typeparamref name="T" /> con el valor asociado a
    /// este evento.
    /// </returns>
    public T Value { get; }
}
