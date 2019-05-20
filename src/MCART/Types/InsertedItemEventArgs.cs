﻿/*
Events.cs

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

using System;

namespace TheXDS.MCART.Types
{
    /// <summary>
    /// Contiene información para el evento <see cref="ExtendedList{T}.InsertedItem"/>.
    /// </summary>
    /// <typeparam name="T">Tipo de elementos de la lista.</typeparam>
    public class InsertedItemEventArgs<T> : EventArgs
    {
        /// <summary>
        /// Convierte implícitamente un <see cref="InsertingItemEventArgs{T}"/>
        /// en un <see cref="InsertedItemEventArgs{T}"/>.
        /// </summary>
        /// <param name="from">
        /// <see cref="InsertingItemEventArgs{T}"/> a convertir.
        /// </param>
        public static implicit operator InsertedItemEventArgs<T>(InsertingItemEventArgs<T> from) => new InsertedItemEventArgs<T>(from.Index, from.InsertedItem);
        /// <summary>
        /// Elemento que fue insertado en el <see cref="ExtendedList{T}"/> que generó
        /// el evento.
        /// </summary>
        public T InsertedItem { get; }
        /// <summary>
        /// Índice del objeto dentro del <see cref="ExtendedList{T}"/> que generó el
        /// evento.
        /// </summary>
        public int Index { get; }
        internal InsertedItemEventArgs(int index, T insertedItem)
        {
            Index = index;
            InsertedItem = insertedItem;
        }
    }
}