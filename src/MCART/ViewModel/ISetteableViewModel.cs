/*
ISetteableViewModel.cs

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

#nullable enable

using System.ComponentModel;
using TheXDS.MCART.Types;

namespace TheXDS.MCART.ViewModel
{
    /// <summary>
    ///     Define una serie de miembros a implementar por una clase que
    ///     implemente funcionalidades básicas de edición de entidades de
    ///     ViewModel.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISetteableViewModel<T> : IRefreshable, INotifyPropertyChanged
    {
        /// <summary>
        ///     Edita la instancia de <typeparamref name="T"/> dentro de este
        ///     ViewModel.
        /// </summary>
        /// <param name="entity">
        ///     Entidad desde la cual extraer información.
        /// </param>
        void Edit(T entity);
    }
}