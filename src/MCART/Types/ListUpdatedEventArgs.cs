/*
Events.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright (c) 2011 - 2019 César Andrés Morgan

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
using System.Collections.Generic;
using System.Linq;

namespace TheXDS.MCART.Types
{
    /// <summary>
    /// Contiene información para el evento <see cref="ExtendedList{T}.ListUpdated"/>.
    /// </summary>
    /// <typeparam name="T">Tipo de elementos de la lista.</typeparam>
    public class ListUpdatedEventArgs<T> : EventArgs
    {
        /// <summary>
        /// Convierte implícitamente un <see cref="ListUpdatingEventArgs{T}"/>
        /// en un <see cref="ListUpdatedEventArgs{T}"/>
        /// </summary>
        /// <param name="from">
        /// <see cref="ListUpdatingEventArgs{T}"/> a convertir.
        /// </param>
        public static implicit operator ListUpdatedEventArgs<T>(ListUpdatingEventArgs<T> from)=> new ListUpdatedEventArgs<T>(from.UpdateType,from.AffectedItems);
        /// <summary>
        /// Elementos que fueron afectados por la actualización del 
        /// <see cref="ExtendedList{T}"/> que generó el evento.
        /// </summary>
        public IReadOnlyCollection<T> AffectedItems { get; }
        /// <summary>
        /// Tipo de actualización ocurrida en el <see cref="ExtendedList{T}"/> que
        /// generó el evento.
        /// </summary>
        public readonly ListUpdateType UpdateType;
        internal ListUpdatedEventArgs(ListUpdateType updateType, IEnumerable<T> affectedItems)
        {
            UpdateType = updateType;
            AffectedItems = affectedItems?.ToList().AsReadOnly();
        }
    }
}