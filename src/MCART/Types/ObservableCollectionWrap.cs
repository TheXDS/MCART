/*
ObservableCollectionWrap.cs

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
using TheXDS.MCART.Types.Base;

namespace TheXDS.MCART.Types
{
    /// <summary>
    /// Envuelve una colección para proveerla de eventos de notificación de
    /// cambio de propiedad y contenido.
    /// </summary>
    /// <typeparam name="T">
    /// Tipo de elementos contenidos dentro de la colección.
    /// </typeparam>
    public class ObservableCollectionWrap<T> : ObservableWrap<T, ICollection<T>>
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="ObservableCollectionWrap{T}"/>.
        /// </summary>
        /// <param name="collection">
        /// Colección a establecer com ola colección subyacente.
        /// </param>
        public ObservableCollectionWrap(ICollection<T> collection) : base(collection)
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="ObservableCollectionWrap{T}"/>.
        /// </summary>
        public ObservableCollectionWrap()
        {
        }
    }
}