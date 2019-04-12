/*
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
    /// Contiene información para el evento <see cref="ExtendedList{T}.RemovedItem"/>.
    /// </summary>
    /// <typeparam name="T">Tipo de elementos de la lista.</typeparam>
    public class RemovedItemEventArgs<T> : EventArgs
    {
        /// <summary>
        /// convierte implícitamente un <see cref="RemovingItemEventArgs{T}"/>
        /// en un <see cref="RemovedItemEventArgs{T}"/>.
        /// </summary>
        /// <param name="from">
        /// <see cref="RemovingItemEventArgs{T}"/> a convertir.
        /// </param>
        public static implicit operator RemovedItemEventArgs<T>(RemovingItemEventArgs<T> from)=> new RemovedItemEventArgs<T>(from.RemovedItem);
        /// <summary>
        /// Objeto que fue quitado del <see cref="ExtendedList{T}"/> que generó el
        /// evento.
        /// </summary>
        public T RemovedItem { get; }
        internal RemovedItemEventArgs(T removedItem)
        {
            RemovedItem = removedItem;
        }
    }
}