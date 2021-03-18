/*
GtkBinding.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2020 César Andrés Morgan

Morgan's CLR Advanced Runtime (MCART) is free software: you can redistribute it
and/or modify it under the terms of the GNU General Public License as published
by the Free Software Foundation, either version 3 of the License, or (at your
option) any later version.

Morgan's CLR Advanced Runtime (MCART) is distributed in the hope that it will be
useful, but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General
Public License for more details.

You should have received a copy of the GNU General Public License along with
this program. If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.ComponentModel;
using TheXDS.MCART.Types.Extensions;
using System.Reflection;
using System.Linq.Expressions;
using TheXDS.MCART.Helpers;

namespace TheXDS.MCART.Types
{
    /// <summary>
    /// Representa un enlace de datos entre widgets y propiedades.
    /// </summary>
    public class GtkBinding
    {
        /// <summary>
        /// Obtiene una referencia al objeto de origen del enlace de datos.
        /// </summary>
        public INotifyPropertyChanged Source { get; }

        /// <summary>
        /// Obtiene una referencia a la propiedad de orígen del enlace de datos.
        /// </summary>
        public PropertyInfo SourceProperty { get; }

        /// <summary>
        /// Obtiene una referencia al objeto destino del enlace de datos.
        /// </summary>
        public object Target { get; }

        /// <summary>
        /// Obtiene una referencia a la propiedad objetivo de almacenamiento 
        /// </summary>
        public PropertyInfo TargetProperty { get; }

        /// <summary>
        /// Obtiene un valor que describe el tipo de enlace.
        /// </summary>
        public BindDirection Direction { get; }

        private GtkBinding(INotifyPropertyChanged source, PropertyInfo sourceProperty, object target, PropertyInfo targetProperty, BindDirection direction = BindDirection.Infer)
        {
            Source = source;
            SourceProperty = sourceProperty;
            Target = target;
            TargetProperty = targetProperty;
            Direction = direction;
        }

        /// <summary>
        /// Obtiene una nueva instancia de la clase <see cref="GtkBinding"/>.
        /// </summary>
        /// <typeparam name="TSource">
        /// Tipo del objeto de origen del enlace.
        /// </typeparam>
        /// <typeparam name="TTarget">
        /// Tipo del objeto de destino del enlace.
        /// </typeparam>
        /// <param name="source"></param>
        /// <param name="sourceProp">Propiedad de orígen del enlace.</param>
        /// <param name="target">Objeto destino del enlace.</param>
        /// <param name="targetProp">Propiedad de destino del enlace.</param>
        /// <param name="direction">Dirección del enlace de datos.</param>
        /// <returns>
        /// Una nueva instancia de la clase <see cref="GtkBinding"/> con el
        /// enlace especificado.
        /// </returns>
        public static GtkBinding Bind<TSource, TTarget>(TSource source, Expression<Func<TSource, object?>> sourceProp, TTarget target, Expression<Func<TTarget, object?>> targetProp, BindDirection direction) where TSource : notnull, INotifyPropertyChanged where TTarget : notnull
        {
            return new GtkBinding(source, ReflectionHelpers.GetProperty(sourceProp), target, ReflectionHelpers.GetProperty(targetProp), direction);
        }

        /// <summary>
        /// Actualiza el valor del enlace de datos.
        /// </summary>
        /// <param name="source">Objeto de orígen.</param>
        public void UpdateValue(object? source)
        {
            TargetProperty.SetValue(Target, source != null ? SourceProperty.GetValue(source) : SourceProperty.PropertyType.Default());
        }
    }
}