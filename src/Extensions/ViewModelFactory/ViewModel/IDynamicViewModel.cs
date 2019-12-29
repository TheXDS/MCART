/*
IDynamicViewModel.cs

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
using TheXDS.MCART.Types;

namespace TheXDS.MCART.ViewModel
{
    /// <summary>
    /// Define una serie de miembros a implementar por una clase que
    /// permita establecer u obtener un modelo de datos a la cual poder
    /// manipular.
    /// </summary>
    /// <typeparam name="T">
    /// Tipo de interfaz de acceso a implementar.
    /// </typeparam>
    /// <remarks>
    /// Se recomienda que esta interfaz sea implementada de forma
    /// abstracta, ya que la clase <see cref="ViewModelFactory"/> invalidará
    /// estos miembros, por lo que las implementaciones directas de estos
    /// miembros en la clase base se ignorarán por medio de Shadowing.
    /// </remarks>
    public interface IDynamicViewModel<T> : IEntityViewModel<T>, ISetteableViewModel<T>, IDynamicViewModel, IRefreshable, INotifyPropertyChanged
    {
        /// <summary>
        /// Entidad subyacente que funciona como campo de almacenamiento
        /// para los datos de este ViewModel.
        /// </summary>
        /// <remarks>
        /// La implementación de este campo debe ser abstracta para
        /// permitir al constructor de ViewModels invalidarla.
        /// </remarks>
        new T Entity { get; set; }
    }

    /// <summary>
    /// Define una serie de miembros a implementar por una clase que
    /// permita establecer u obtener un modelo de datos a la cual poder
    /// manipular.
    /// </summary>
    /// <remarks>
    /// Se recomienda que esta interfaz sea implementada de forma
    /// abstracta, ya que la clase <see cref="ViewModelFactory"/> invalidará
    /// estos miembros, por lo que las implementaciones directas de estos
    /// miembros en la clase base se ignorarán por medio de Shadowing.
    /// </remarks>
    public interface IDynamicViewModel : IRefreshable, INotifyPropertyChanged
    {
        /// <summary>
        /// Entidad subyacente que funciona como campo de almacenamiento
        /// para los datos de este ViewModel.
        /// </summary>
        /// <remarks>
        /// La implementación de este campo debe ser abstracta para
        /// permitir al constructor de ViewModels invalidarla.
        /// </remarks>
        object Entity { get; set; }
    }
}