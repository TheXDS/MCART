﻿/*
GeneratedViewModel.cs

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

using TheXDS.MCART.Annotations;

namespace TheXDS.MCART.ViewModel
{
    /// <summary>
    ///     Clase base para un ViewModel dinámico.
    /// </summary>
    public abstract class DynamicViewModel<T> : ViewModelBase, IDynamicViewModel<T> where T : class
    {
        private readonly object _locker = new object();
        /// <summary>
        ///     Entidad subyacente que funciona como campo de almacenamiento
        ///     para los datos de este ViewModel.
        /// </summary>
        public abstract T Entity { get; set; }

        /// <summary>
        ///     Edita la instancia de <typeparamref name="T"/> expuesta por
        ///     este ViewModel.
        /// </summary>
        /// <param name="entity">
        ///     Instancia de <typeparamref name="T"/> con los valores a
        ///     establecer.
        /// </param>
        public abstract void Edit([NotNull] T entity);

        /// <summary>
        ///     Entidad subyacente que funciona como campo de almacenamiento
        ///     para los datos de este ViewModel.
        /// </summary>
        object IDynamicViewModel.Entity
        {
            get => Entity;
            set
            {
                lock(value ?? _locker) Entity = value as T;
            }
        }
    }
}