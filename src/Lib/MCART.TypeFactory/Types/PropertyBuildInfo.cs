/*
PropertyBuildInfo.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2026 César Andrés Morgan

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

using System.Reflection;
using System.Reflection.Emit;
using TheXDS.MCART.Types.Extensions;
using static System.Reflection.MethodAttributes;
using static TheXDS.MCART.Types.TypeBuilderHelpers;

namespace TheXDS.MCART.Types;

/// <summary>
/// Contains information about the construction of a property.
/// </summary>
public class PropertyBuildInfo : MemberBuildInfo<PropertyBuilder>
{
    /// <summary>
    /// Reference to the <see cref="FieldBuilder"/> used to create the
    /// backing field for the property, if one is employed.
    /// </summary>
    /// <value>
    /// The <see cref="FieldBuilder"/> of the backing field, or
    /// <see langword="null"/> if the property does not use a field to
    /// store its current value.
    /// </value>
    public FieldBuilder? Field { get; }

    /// <summary>
    /// Reference to the <see cref="ILGenerator"/> of the property's
    /// <see langword="get"/> method.
    /// </summary>
    /// <value>
    /// An <see cref="ILGenerator"/> that can be used to define the
    /// property's <see langword="get"/> method, or
    /// <see langword="null"/> if the property is auto‑implemented or
    /// if the property has no <see langword="get"/> method.
    /// </value>
    public ILGenerator? Getter { get; }

    /// <summary>
    /// Reference to the <see cref="ILGenerator"/> of the property's
    /// <see langword="set"/> method.
    /// </summary>
    /// <value>
    /// An <see cref="ILGenerator"/> that can be used to define the
    /// property's <see langword="set"/> method, or
    /// <see langword="null"/> if the property is auto‑implemented or
    /// if the property has no <see langword="set"/> method.
    /// </value>
    public ILGenerator? Setter { get; }

    /// <summary>
    /// Implicitly converts a <see cref="PropertyBuildInfo"/> to a
    /// <see cref="FieldInfo"/>.
    /// </summary>
    /// <param name="buildInfo">
    /// The <see cref="PropertyBuildInfo"/> from which to extract the
    /// <see cref="FieldInfo"/>.
    /// </param>
    public static implicit operator FieldInfo?(PropertyBuildInfo buildInfo) => buildInfo.Field;

    /// <summary>
    /// Initializes a new instance of the <see cref="PropertyBuildInfo"/>
    /// class.
    /// </summary>
    /// <param name="typeBuilder">
    /// The <see cref="TypeBuilder"/> instance where the property was
    /// defined.
    /// </param>
    /// <param name="property">
    /// The <see cref="PropertyBuilder"/> that describes the property
    /// that has been created.
    /// </param>
    /// <param name="field">
    /// The backing field defined for the property.
    /// </param>
    public PropertyBuildInfo(TypeBuilder typeBuilder, PropertyBuilder property, FieldBuilder field) : this(typeBuilder, property)
    {
        Field = field;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PropertyBuildInfo"/>
    /// class.
    /// </summary>
    /// <param name="typeBuilder">
    /// The <see cref="TypeBuilder"/> instance where the property was
    /// defined.
    /// </param>
    /// <param name="property">
    /// The <see cref="PropertyBuilder"/> that describes the property
    /// that has been created.
    /// </param>
    /// <param name="getter">
    /// The <see cref="ILGenerator"/> that allows the code for the
    /// property's <see langword="get"/> block to be defined.
    /// </param>
    public PropertyBuildInfo(TypeBuilder typeBuilder, PropertyBuilder property, ILGenerator getter) : this(typeBuilder, property, getter, null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PropertyBuildInfo"/> 
    /// class.
    /// </summary>
    /// <param name="typeBuilder">
    /// Instance of <see cref="TypeBuilder"/> where the property was
    /// defined.
    /// </param>
    /// <param name="property">
    /// <see cref="PropertyBuilder"/> that describes the property that has
    /// been created.
    /// </param>
    /// <param name="getter">
    /// <see cref="ILGenerator"/> that allows the code for the property's
    /// <see langword="get"/> block to be defined.
    /// </param>
    /// <param name="setter">
    /// <see cref="ILGenerator"/> that allows the code for the property's
    /// <see langword="set"/> block to be defined.
    /// </param>
    public PropertyBuildInfo(TypeBuilder typeBuilder, PropertyBuilder property, ILGenerator? getter, ILGenerator? setter) : this(typeBuilder, property)
    {
        Getter = getter;
        Setter = setter;
    }

    private PropertyBuildInfo(TypeBuilder typeBuilder, PropertyBuilder property) : base(typeBuilder, property)
    {
    }

    /// <summary>
    /// Creates a property in the type with no get or set
    /// implementations initially defined.
    /// </summary>
    /// <param name="tb">
    /// Type builder in which to create the new property.
    /// </param>
    /// <param name="name">Name of the new property.</param>
    /// <param name="type">Type of the new property.</param>
    /// <param name="writable">
    /// <see langword="true"/> to create a property that contains a
    /// write accessor (set accessor); <see langword="false"/> to omit
    /// a write accessor.
    /// </param>
    /// <param name="access">Access level of the new property.</param>
    /// <param name="virtual">
    /// If <see langword="true"/>, the property is defined as virtual,
    /// allowing it to be overridden in a derived class.
    /// </param>
    /// <returns>
    /// A <see cref="PropertyBuildInfo"/> that contains information
    /// about the constructed property.
    /// </returns>
    /// <remarks>
    /// The generated property will require the accessors to be
    /// implemented before the type is created.
    /// </remarks>
    public static PropertyBuildInfo Create(TypeBuilder tb, string name, Type type, bool writable, MemberAccess access, bool @virtual)
    {
        ILGenerator? setIl = null;
        PropertyBuilder prop = tb.DefineProperty(name, PropertyAttributes.HasDefault, type, null);
        MethodBuilder getM = MkGet(tb, name, type, access, @virtual);
        ILGenerator getIl = getM.GetILGenerator();
        prop.SetGetMethod(getM);
        if (writable)
        {
            MethodBuilder setM = MkSet(tb, name, type, access, @virtual);
            setIl = setM.GetILGenerator();
            prop.SetSetMethod(setM);
        }
        return new PropertyBuildInfo(tb, prop, getIl, setIl);
    }

    /// <summary>
    /// Creates a write‑only property in the type.
    /// </summary>
    /// <param name="tb">
    /// Type builder in which to create the new property.
    /// </param>
    /// <param name="name">Name of the new property.</param>
    /// <param name="type">Type of the new property.</param>
    /// <param name="access">Access level of the new property.</param>
    /// <param name="virtual">
    /// If <see langword="true"/>, the property is defined as virtual
    /// and can be overridden in a derived class.
    /// </param>
    /// <returns>
    /// A <see cref="PropertyBuildInfo"/> that contains information
    /// about the constructed property.
    /// </returns>
    public static PropertyBuildInfo CreateWriteOnly(TypeBuilder tb, string name, Type type, MemberAccess access, bool @virtual)
    {
        PropertyBuilder prop = tb.DefineProperty(name, PropertyAttributes.HasDefault, type, null);
        MethodBuilder setM = MkSet(tb, name, type, access, @virtual);
        ILGenerator setIl = setM.GetILGenerator();
        prop.SetSetMethod(setM);
        return new PropertyBuildInfo(tb, prop, null, setIl);
    }

    private static MethodBuilder MkGet(TypeBuilder tb, string name, Type t, MemberAccess a, bool v)
    {
        string n = $"get_{name}";
        return tb.DefineMethod(n, MkPFlags(tb, n, a, v), t, null);
    }

    private static MethodBuilder MkSet(TypeBuilder tb, string name, Type t, MemberAccess a, bool v)
    {
        string? n = $"set_{name}";
        return tb.DefineMethod(n, MkPFlags(tb, n, a, v), null, [t]);
    }

    private static MethodAttributes MkPFlags(TypeBuilder tb, string n, MemberAccess a, bool v)
    {
        MethodAttributes f = Access(a) | SpecialName | HideBySig | ReuseSlot;
        if (tb.Overridable(n) ?? v) f |= Virtual;
        return f;
    }
}
