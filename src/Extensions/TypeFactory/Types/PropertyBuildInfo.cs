/*
PropertyBuildInfo.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2022 César Andrés Morgan

Permission is hereby granted, free of charge, to any person obtaining a copy of
this software and associated documentation files (the "Software"), to deal in
the Software without restriction, including without limitation the rights to
use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
of the Software, and to permit persons to whom the Software is furnished to do
so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
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
