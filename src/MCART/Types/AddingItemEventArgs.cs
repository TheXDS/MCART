/*
AddingItemEventArgs.cs

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

using System.ComponentModel;

namespace TheXDS.MCART.Types
{
    /// <summary>
    /// Contiene información para el evento <see cref="ListEx{T}.AddingItem"/>.
    /// </summary>
    /// <typeparam name="T">Tipo de elementos de la lista.</typeparam>
    public class AddingItemEventArgs<T> : CancelEventArgs
    {
        /// <summary>
        /// Obtiene el objeto que se agregará al <see cref="ListEx{T}"/>.
        /// </summary>
        public T NewItem { get; }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="AddingItemEventArgs{T}"/>.
        /// </summary>
        /// <param name="newItem">
        /// Objeto a ser agregado al <see cref="ListEx{T}"/> que generó el 
        /// evento.
        /// </param>
        internal AddingItemEventArgs(T newItem) { NewItem = newItem; }
    }
}