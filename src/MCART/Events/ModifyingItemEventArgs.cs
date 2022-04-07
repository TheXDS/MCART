/*
ModifyingItemEventArgs.cs

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
using System.ComponentModel;
using TheXDS.MCART.Types;

/// <summary>
/// Contiene información para el evento
/// <see cref="ListEx{T}.ModifyingItem"/>.
/// </summary>
/// <typeparam name="T">Tipo de elementos de la lista.</typeparam>
public class ModifyingItemEventArgs<T> : CancelEventArgs
{
    /// <summary>
    /// Inicializa una nueva instancia de la clase 
    /// <see cref="ModifyingItemEventArgs{T}"/>.
    /// </summary>
    /// <param name="index">
    /// Índice del objeto en el <see cref="ListEx{T}"/> que generó el
    /// evento.
    /// </param>
    /// <param name="oldValue">Valor original del objeto.</param>
    /// <param name="newValue">Nuevo valor del objeto.</param>
    internal ModifyingItemEventArgs(int index, T oldValue, T newValue)
    {
        OldValue = oldValue;
        NewValue = newValue;
        Index = index;
    }

    /// <summary>
    /// Obtiene el nuevo valor del objeto.
    /// </summary>
    public T NewValue { get; }

    /// <summary>
    /// Obtiene el valor actual del objeto.
    /// </summary>
    public T OldValue { get; }

    /// <summary>
    /// Obtiene el índice del objeto dentro del <see cref="ListEx{T}"/>
    /// que generó el evento.
    /// </summary>
    public int Index { get; }
}
