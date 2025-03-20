/*
PropertyBuildInfo.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2025 César Andrés Morgan

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

    /// <summary>
    /// Inicializa una nueva instancia de la clase 
    /// <see cref="PropertyBuildInfo"/>.
    /// </summary>
    /// <param name="typeBuilder">
    /// Instancia de <see cref="TypeBuilder"/> en la cual se ha definido la
    /// propiedad.
    /// </param>
    /// <param name="property">
    /// <see cref="PropertyBuilder"/> que describe a la propiedad que ha sido
    /// creada.
    /// </param>
    /// <param name="field">
    /// Campo de almacenamiento definido para la propiedad.
    /// </param>
    public PropertyBuildInfo(TypeBuilder typeBuilder, PropertyBuilder property, FieldBuilder field) : this(typeBuilder, property)
    {
        Field = field;
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase 
    /// <see cref="PropertyBuildInfo"/>.
    /// </summary>
    /// <param name="typeBuilder">
    /// Instancia de <see cref="TypeBuilder"/> en la cual se ha definido la
    /// propiedad.
    /// </param>
    /// <param name="property">
    /// <see cref="PropertyBuilder"/> que describe a la propiedad que ha sido
    /// creada.
    /// </param>
    /// <param name="getter">
    /// <see cref="ILGenerator"/> que permite definir el código a ejecutar para
    /// el bloque <see langword="get"/> de la propiedad.
    /// </param>
    public PropertyBuildInfo(TypeBuilder typeBuilder, PropertyBuilder property, ILGenerator getter) : this(typeBuilder, property, getter, null)
    {
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase 
    /// <see cref="PropertyBuildInfo"/>.
    /// </summary>
    /// <param name="typeBuilder">
    /// Instancia de <see cref="TypeBuilder"/> en la cual se ha definido la
    /// propiedad.
    /// </param>
    /// <param name="property">
    /// <see cref="PropertyBuilder"/> que describe a la propiedad que ha sido
    /// creada.
    /// </param>
    /// <param name="getter">
    /// <see cref="ILGenerator"/> que permite definir el código a ejecutar para
    /// el bloque <see langword="get"/> de la propiedad.
    /// </param>
    /// <param name="setter">
    /// <see cref="ILGenerator"/> que permite definir el código a ejecutar para
    /// el bloque <see langword="set"/> de la propiedad.
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
    /// Crea una propiedad en el tipo sin implementaciones de
    /// <see langword="get"/> ni <see langword="set"/> establecidas.
    /// </summary>
    /// <param name="tb">
    /// Constructor del tipo en el cual crear la nueva propiedad.
    /// </param>
    /// <param name="name">Nombre de la nueva propiedad.</param>
    /// <param name="type">Tipo de la nueva propiedad.</param>
    /// <param name="writable">
    /// <see langword="true"/> para crear una propiedad que contiene
    /// accesor de escritura (accesor <see langword="set"/>),
    /// <see langword="false"/> para no incluir un accesor de escritura en
    /// la propiedad.
    /// </param>
    /// <param name="access">Nivel de acceso de la nueva propiedad.</param>
    /// <param name="virtual">
    /// Si se establece en <see langword="true"/>, la propiedad será
    /// definida como virtual, por lo que podrá ser reemplazada en una
    /// clase derivada. 
    /// </param>
    /// <returns>
    /// Un <see cref="PropertyBuildInfo"/> que contiene información sobre
    /// la propiedad que ha sido construida.
    /// </returns>
    /// <remarks>
    /// La propiedad generada requerirá que se implementen los accesores
    /// antes de construir el tipo.
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
    /// Crea una propiedad de solo escritura en el tipo.
    /// </summary>
    /// <param name="tb">
    /// Constructor del tipo en el cual crear la nueva propiedad.
    /// </param>
    /// <param name="name">Nombre de la nueva propiedad.</param>
    /// <param name="type">Tipo de la nueva propiedad.</param>
    /// <param name="access">Nivel de acceso de la nueva propiedad.</param>
    /// <param name="virtual">
    /// Si se establece en <see langword="true"/>, la propiedad será
    /// definida como virtual, por lo que podrá ser reemplazada en una
    /// clase derivada. 
    /// </param>
    /// <returns>
    /// Un <see cref="PropertyBuildInfo"/> que contiene información sobre
    /// la propiedad que ha sido construida.
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
        return tb.DefineMethod(n, MkPFlags(tb, n, a, v), null, new[] { t });
    }

    private static MethodAttributes MkPFlags(TypeBuilder tb, string n, MemberAccess a, bool v)
    {
        MethodAttributes f = Access(a) | SpecialName | HideBySig | ReuseSlot;
        if (tb.Overridable(n) ?? v) f |= Virtual;
        return f;
    }
}
