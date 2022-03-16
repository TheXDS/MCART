/*
PropertyBuildInfo.cs

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

namespace TheXDS.MCART.Types;
using System.Reflection;
using System.Reflection.Emit;

/// <summary>
/// Contiene información acerca de la construcción de una propiedad.
/// </summary>
public class PropertyBuildInfo : MemberBuildInfo<PropertyBuilder>
{
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
    /// en un <see cref="FieldInfo"/>.
    /// </summary>
    /// <param name="buildInfo">
    /// <see cref="PropertyBuildInfo"/> desde el cual extraer el
    /// <see cref="FieldInfo"/>.
    /// </param>
    public static implicit operator FieldInfo?(PropertyBuildInfo buildInfo) => buildInfo.Field;

    internal PropertyBuildInfo(TypeBuilder typeBuilder, PropertyBuilder property, FieldBuilder field) : this(typeBuilder, property)
    {
        Field = field;
    }
    internal PropertyBuildInfo(TypeBuilder typeBuilder, PropertyBuilder property, ILGenerator getter) : this(typeBuilder, property, getter, null)
    {
    }
    internal PropertyBuildInfo(TypeBuilder typeBuilder, PropertyBuilder property, ILGenerator? getter, ILGenerator? setter) : this(typeBuilder, property)
    {
        Getter = getter;
        Setter = setter;
    }
    private PropertyBuildInfo(TypeBuilder typeBuilder, PropertyBuilder property) : base(typeBuilder, property)
    {
    }
}
