/*
IObservableCollectionWrap.cs

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

using System.Collections.Specialized;
using System.Collections.Generic;

namespace TheXDS.MCART.Types.Base
{
    /// <summary>
    /// Define una serie de miembros a implementar por una clase que defina
    /// un envoltorio observable sobre un <see cref="ICollection{T}"/>.
    /// </summary>
    public interface IObservableCollectionWrap<T> : INotifyCollectionChanged, ICollection<T>, IRefreshable
    {
        /// <summary>
        /// Obtiene una referencia a la colección subyacente de este
        /// envoltorio observable.
        /// </summary>
        ICollection<T> UnderlyingCollection { get; }

        /// <summary>
        /// Sustituye la colección subyacente por una nueva.
        /// </summary>
        /// <param name="newCollection">
        /// Colección a establecer como la colección subyacente.
        /// </param>
        void Substitute(ICollection<T> newCollection);
    }
}