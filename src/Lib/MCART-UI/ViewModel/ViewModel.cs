/*
ViewModel.cs

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
using System.Linq;
using System.Reflection;
using static System.Reflection.BindingFlags;

namespace TheXDS.MCART.ViewModel
{
    /// <summary>
    /// Clase base para un <see cref="ViewModelBase"/> cuyos campos de
    /// almacenamiento sean parte de un modelo de entidad.
    /// </summary>
    /// <typeparam name="T">
    /// Tipo de entidad a utilizar como almacenamiento interno de este
    /// ViewModel.
    /// </typeparam>
    public class ViewModel<T> : ViewModelBase, IEntityViewModel<T>, IUpdatableViewModel<T>
    {
        private static readonly HashSet<PropertyInfo> _modelProperties = new HashSet<PropertyInfo>(typeof(T).GetProperties(Public | Instance).Where(p => p.CanRead));
        private static IEnumerable<PropertyInfo> WrittableProperties => _modelProperties.Where(p => p.CanWrite);

        private T _entity = default!;

        /// <summary>
        /// Instancia de la entidad controlada por este ViewModel.
        /// </summary>
        public virtual T Entity
        { 
            get => _entity;
            set => Change(ref _entity, value);
        }

        /// <summary>
        /// Edita la instancia de <typeparamref name="T"/> dentro de este
        /// ViewModel.
        /// </summary>
        /// <param name="entity">
        /// Entidad conlos nuevos valores a establecer en la entidad
        /// actualmente establecida en la propiedad <see cref="Entity"/>.
        /// </param>
        public virtual void Update(T entity)
        {
            foreach (var j in WrittableProperties)
            {
                j.SetValue(Entity, j.GetValue(entity));
            }
            Refresh();
        }

        /// <summary>
        /// Notifica al sistema que las propiedades de este
        /// <see cref="ViewModel{T}"/> han cambiado.
        /// </summary>
        public override void Refresh()
        {
            if (Entity is null) return;
            lock (Entity)
            {
                Notify(nameof(Entity));
            }
        }

        /// <summary>
        /// Convierte implícitamente un <see cref="ViewModel{T}"/>
        /// en un <typeparamref name="T"/>.
        /// </summary>
        /// <param name="vm">
        /// <see cref="ViewModel{T}"/> a convertir.
        /// </param>
        public static implicit operator T(ViewModel<T> vm)
        {
            return vm.Entity;
        }
    }
}