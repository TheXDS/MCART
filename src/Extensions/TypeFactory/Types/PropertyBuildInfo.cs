/*
TypeFactory.cs

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

using System;
using System.Reflection;
using System.Reflection.Emit;

namespace TheXDS.MCART.Types
{
    /// <summary>
    /// Contiene información acerca de la construcción de una propiedad.
    /// </summary>
    public class PropertyBuildInfo
    {
        /// <summary>
        /// Referencia al <see cref="PropertyBuilder"/> utilizado para
        /// construir a la propiedad.
        /// </summary>
        public PropertyBuilder Property { get; }

        /// <summary>
        /// Referencia al <see cref="FieldBuilder"/> utilizado para
        /// construir el campo de almacenamiento de la propiedad en caso de
        /// utilizar uno.
        /// </summary>
        /// <value>
        /// El <see cref="FieldBuilder"/> del campo de almacenamiento, o
        /// <see langword="null"/> si la propiedad no utiliza un campo para
        /// almacenar su valor actual.
        /// </value>
        public FieldBuilder? Field { get; }

        /// <summary>
        /// Referencia al <see cref="ILGenerator"/> del método
        /// <see langword="get"/> de la propiedad.
        /// </summary>
        /// <value>
        /// Un <see cref="ILGenerator"/> que puede utilizarse para definir el
        /// método <see langword="get"/> de la propiedad, o
        /// <see langword="null"/> si la propiedad es definida como una
        /// propiedad automática o si la propiedad no contiene un método
        /// <see langword="get"/>.
        /// </value>
        public ILGenerator? Getter { get; }

        /// <summary>
        /// Referencia al <see cref="ILGenerator"/> del método
        /// <see langword="set"/> de la propiedad.
        /// </summary>
        /// <value>
        /// Un <see cref="ILGenerator"/> que puede utilizarse para definir el
        /// método <see langword="set"/> de la propiedad, o
        /// <see langword="null"/> si la propiedad es definida como una
        /// propiedad automática o si la propiedad no contiene un método
        /// <see langword="set"/>.
        /// </value>
        public ILGenerator? Setter { get; }

        /// <summary>
        /// Convierte implícitamente un valor <see cref="PropertyBuildInfo"/>
        /// en un <see cref="PropertyInfo"/>.
        /// </summary>
        /// <param name="buildInfo">
        /// <see cref="PropertyBuildInfo"/> desde el cual extraer el
        /// <see cref="PropertyInfo"/>.
        /// </param>
        public static implicit operator PropertyInfo(PropertyBuildInfo buildInfo) => buildInfo.Property;

        /// <summary>
        /// Convierte implícitamente un valor <see cref="PropertyBuildInfo"/>
        /// en un <see cref="FieldInfo"/>.
        /// </summary>
        /// <param name="buildInfo">
        /// <see cref="PropertyBuildInfo"/> desde el cual extraer el
        /// <see cref="FieldInfo"/>.
        /// </param>
        public static implicit operator FieldInfo?(PropertyBuildInfo buildInfo) => buildInfo.Field;

        internal PropertyBuildInfo(PropertyBuilder property, FieldBuilder field) : this(property)
        {
            Field = field;
        }
        internal PropertyBuildInfo(PropertyBuilder property, ILGenerator getter) : this(property, getter, null)
        {
        }
        internal PropertyBuildInfo(PropertyBuilder property, ILGenerator? getter, ILGenerator? setter) : this(property)
        {
            Getter = getter;
            Setter = setter;
        }
        private PropertyBuildInfo(PropertyBuilder property)
        {
            Property = property ?? throw new ArgumentNullException(nameof(property));
        }
    }
}
