﻿/*
InsertingItemEventArgs.cs

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

using System.ComponentModel;

namespace TheXDS.MCART.Types
{
    /// <summary>
    /// Contiene información para el evento
    /// <see cref="ListEx{T}.InsertingItem"/>.
    /// </summary>
    /// <typeparam name="T">Tipo de elementos de la lista.</typeparam>
    public class InsertingItemEventArgs<T> : CancelEventArgs
    {
        /// <summary>
        /// Obtiene el objeto que se insertará en el
        /// <see cref="ListEx{T}"/>.
        /// </summary>
        public T InsertedItem { get; }

        /// <summary>
        /// Obtiene el índice en el cual el objeto será insertado.
        /// </summary>
        public int Index { get; }

        internal InsertingItemEventArgs(int index, T insertedItem)
        {
            Index = index;
            InsertedItem = insertedItem;
        }
    }
}