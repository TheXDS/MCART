/*
PropertyBuildInfoExtensions.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2023 César Andrés Morgan

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
using static TheXDS.MCART.Types.TypeBuilderHelpers;
using Errors = TheXDS.MCART.Resources.TypeFactoryErrors;

namespace TheXDS.MCART.Types.Extensions;

/// <summary>
/// Contiene extensiones útiles para objetos de tipo
/// <see cref="PropertyBuildInfo"/>.
/// </summary>
public static class PropertyBuildInfoExtensions
{
    /// <summary>
    /// Crea un campo de almacenamiento para la propiedad y configura el
    /// método <see langword="get"/> de la propiedad para leer el valor del
    /// mismo.
    /// </summary>
    /// <param name="builder">
    /// <see cref="PropertyBuildInfo"/> en el cual se definirá el campo de
    /// almacenamiento.
    /// </param>
    /// <param name="field">
    /// <see cref="FieldBuilder"/> generado que representa al campo de
    /// almacenamiento.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="builder"/>, permitiendo el
    /// uso de sintaxis Fluent.
    /// </returns>
    public static PropertyBuildInfo WithBackingField(this PropertyBuildInfo builder, out FieldBuilder field)
    {
        if (builder.Field is not null) throw Errors.PropFieldAlreadyDefined();
        if ((builder.Getter?.ILOffset ?? 0) != 0) throw Errors.PropGetterAlreadyDefined();
        field = builder.TypeBuilder.DefineField(UndName(builder.Member.Name), builder.Member.PropertyType, FieldAttributes.Private | FieldAttributes.PrivateScope);
        builder.Getter?.LoadField(field).Return();
        return builder;
    }

    /// <summary>
    /// Construye un esqueleto para el setter de una propiedad que notifica el cambio de su valor.
    /// </summary>
    /// <param name="prop">Propiedad para la cual definir el nuevo setter con notificación de cambio de valor.</param>
    /// <param name="valueProvider"></param>
    /// <param name="valueSetter"></param>
    /// <param name="propertyType"></param>
    public static void BuildNpcPropSetterSkeleton(this PropertyBuildInfo prop, Action<Label, ILGenerator> valueProvider, Action<Label, ILGenerator> valueSetter, Type propertyType)
    {
        var setRet = prop.Setter!.DefineLabel();
        valueProvider(setRet, prop.Setter);
        if (propertyType.IsValueType)
        {
            prop.Setter.LoadArg1()
                .CompareEqual()
                .BranchTrue(setRet);
        }
        else
        {
            prop.Setter.BranchFalseNewLabel(out var fieldIsNull);
            valueProvider(setRet, prop.Setter);
            prop.Setter
                .LoadArg1()
                .Call(GetEqualsMethod(propertyType))
                .BranchTrue(setRet)
                .BranchNewLabel(out var doSet)
                .PutLabel(fieldIsNull)
                .LoadArg1()
                .BranchFalse(setRet)
                .PutLabel(doSet);
        }
        valueSetter(setRet, prop.Setter);
        prop.Setter.Return(setRet);
    }

    private static MethodInfo GetEqualsMethod(Type type)
    {
        return type.GetMethod(nameof(object.Equals), BindingFlags.Public | BindingFlags.Instance, null, new[] { type }, null)
            ?? type.GetMethod(nameof(object.Equals), BindingFlags.Public | BindingFlags.Instance, null, new[] { typeof(object) }, null)!;
    }
}
