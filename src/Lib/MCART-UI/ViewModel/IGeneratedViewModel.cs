/*
IGeneratedViewModel.cs

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

using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using TheXDS.MCART.Annotations;
using TheXDS.MCART.Types;
using static System.Reflection.BindingFlags;

namespace TheXDS.MCART.ViewModel
{
    /// <summary>
    ///     Define una serie de miembros a implementar por una clase que
    ///     contenga una propiedad de acceso de instancia dinámicamente
    ///     generado a partir de una interfaz.
    /// </summary>
    /// <typeparam name="T">
    ///     Tipo de interfaz de acceso a implementar.
    /// </typeparam>
    public interface IGeneratedViewModel<T> : IViewModel<T>, IRefreshable where T : class
    {
        /// <summary>
        ///     Expone a los campos auto generados para este ViewModel por
        ///     medio de la interfaz <typeparamref name="T"/>.
        /// </summary>
        /// <remarks>
        ///     La implementación de este campo debe ser abstracta para
        ///     permitir al constructor de ViewModels invalidarla.
        /// </remarks>
        T Self { get; }
        /// <summary>
        ///     Entidad subyacente que funciona como campo de almacenamiento
        ///     para los datos de este ViewModel.
        /// </summary>
        /// <remarks>
        ///     La implementación de este campo debe ser abstracta para
        ///     permitir al constructor de ViewModels invalidarla.
        /// </remarks>
        new T Entity { get; set; }
    }
    /// <summary>
    ///     Clase base para un ViewModel autogenerado.
    /// </summary>
    /// <typeparam name="T">
    ///     Tipo de interfaz de acceso.
    /// </typeparam>
    public abstract class GeneratedViewModel<T> : ViewModelBase, IGeneratedViewModel<T> where T : class
    {
        /// <summary>
        ///     Expone a los campos auto generados para este ViewModel por
        ///     medio de la interfaz <typeparamref name="T"/>.
        /// </summary>
        public abstract T Self { get; }

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
    }
}