/*
ListUpdatingEventArgs.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2020 César Andrés Morgan

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

using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace TheXDS.MCART.Types
{
    /// <summary>
    /// Contiene información para el evento 
    /// <see cref="ListEx{T}.ListUpdating"/>.
    /// </summary>
    /// <typeparam name="T">Tipo de elementos de la lista.</typeparam>
    public class ListUpdatingEventArgs<T> : CancelEventArgs
    {
        /// <summary>
        /// Elementos afectados por la actualización.
        /// </summary>
        public IReadOnlyCollection<T>? AffectedItems { get; }
        /// <summary>
        /// Tipo de actualización a realizar en el
        /// <see cref="ListEx{T}"/> que generó el evento.
        /// </summary>
        public ListUpdateType UpdateType { get; }

        internal ListUpdatingEventArgs(ListUpdateType updateType, IEnumerable<T>? affectedItems)
        {
            UpdateType = updateType;
            AffectedItems = affectedItems?.ToList().AsReadOnly();
        }
    }
}