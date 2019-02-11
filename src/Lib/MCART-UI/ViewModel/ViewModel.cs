/*
ViewModel.cs

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
using System.Linq;
using System.Reflection;
using TheXDS.MCART.Annotations;
using static System.Reflection.BindingFlags;

namespace TheXDS.MCART.ViewModel
{
    /// <summary>
    ///     Clase base para un <see cref="ViewModelBase"/> cuyos campos de
    ///     almacenamiento sean parte de un modelo de entidad.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ViewModel<T> : ViewModelBase, IEntityViewModel<T> where T : new()
    {
        private static readonly HashSet<PropertyInfo> _modelProperties = new HashSet<PropertyInfo>();
        private static IEnumerable<PropertyInfo> WrittableProperties => _modelProperties.Where(p => p.CanWrite);

        /// <summary>
        ///     Inicializa la clase <see cref="ViewModel{T}"/>.
        /// </summary>
        static ViewModel()
        {
            _modelProperties = new HashSet<PropertyInfo>(
                typeof(T)
                .GetProperties(Public | Instance)
                .Where(p => p.CanRead));
        }

        /// <summary>
        ///     Instancia de la entidad controlada por este ViewModel.
        /// </summary>
        public T Entity { get; private set; }

        /// <summary>
        ///     Instancia un nuevo <typeparamref name="T"/> en este ViewModel.
        /// </summary>
        public void New()
        {
            Entity = new T();
            Refresh();
        }

        /// <summary>
        ///     Edita la instancia de <typeparamref name="T"/> dentro de este
        ///     ViewModel.
        /// </summary>
        /// <param name="entity"></param>
        public void Edit([NotNull]T entity)
        {
            foreach (var j in WrittableProperties)
            {
                j.SetValue(Entity, j.GetValue(entity));
            }
            Refresh();
        }

        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="ViewModel{T}"/>.
        /// </summary>
        public ViewModel()
        {
            New();
        }

        /// <summary>
        ///     Notifica al sistema que las propiedades de este
        ///     <see cref="ViewModel{T}"/> han cambiado.
        /// </summary>
        public override void Refresh()
        {
            lock (Entity)
            {
                Notify(_modelProperties.Select(p => p.Name));
            }
        }
    }
}
