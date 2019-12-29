/*
IObservableListWrap.cs

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

using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace TheXDS.MCART.Types.Base
{
    /// <summary>
    /// Define una serie de miembros a implementar por una clase que defina
    /// un envoltorio observable sobre un <see cref="IList"/>.
    /// </summary>
    public interface IObservableListWrap : IList, INotifyCollectionChanged, IRefreshable
    {
        /// <summary>
        /// Obtiene una referencia a la lista subyacente de este envoltorio
        /// observable.
        /// </summary>
        IList UnderlyingList { get; }

        /// <summary>
        /// Sustituye la lista subyacente por una nueva.
        /// </summary>
        /// <param name="newCollection">
        /// Lista a establecer como la lista subyacente.
        /// </param>
        void Substitute(IList newCollection);
    }

    /// <summary>
    /// Define una serie de miembros a implementar por una clase que defina
    /// un envoltorio observable sobre un <see cref="IList{T}"/>.
    /// </summary>
    public interface IObservableListWrap<T> : INotifyCollectionChanged, IList<T>, IRefreshable
    {
        /// <summary>
        /// Obtiene una referencia a la lista subyacente de este envoltorio
        /// observable.
        /// </summary>
        IList<T> UnderlyingList { get; }

        /// <summary>
        /// Sustituye la lista subyacente por una nueva.
        /// </summary>
        /// <param name="newCollection">
        /// Lista a establecer como la lista subyacente.
        /// </param>
        void Substitute(IList<T> newCollection);
    }
}