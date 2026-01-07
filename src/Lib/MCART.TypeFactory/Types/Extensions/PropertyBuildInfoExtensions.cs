/*
PropertyBuildInfoExtensions.cs

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
using static TheXDS.MCART.Types.TypeBuilderHelpers;
using Errors = TheXDS.MCART.Resources.TypeFactoryErrors;

namespace TheXDS.MCART.Types.Extensions;

/// <summary>
/// Contains useful extensions for objects of type PropertyBuildInfo.
/// </summary>
public static class PropertyBuildInfoExtensions
{
    /// <summary>
    /// Creates a backing field for the property and configures the property's
    /// get method to read its value.
    /// </summary>
    /// <param name="builder">
    /// PropertyBuildInfo on which the backing field will be defined.
    /// </param>
    /// <param name="field">
    /// FieldBuilder generated that represents the backing field.
    /// </param>
    /// <returns>
    /// The same instance as <paramref name="builder"/>, enabling Fluent syntax.
    /// </returns>
    public static PropertyBuildInfo WithBackingField(this PropertyBuildInfo builder, out FieldBuilder field)
    {
        if (builder.Field is not null) throw Errors.PropFieldAlreadyDefined();
        if ((builder.Getter?.ILOffset ?? 0) != 0) throw Errors.PropGetterAlreadyDefined();
        field = builder.TypeBuilder.DefineField(UndName(builder.Member.Name), builder.Member.PropertyType, FieldAttributes.Private | FieldAttributes.PrivateScope);
        builder.Getter?.GetField(field).Return();
        return builder;
    }

    /// <summary>
    /// Builds a skeleton for a property's setter that notifies of its value
    /// change.
    /// </summary>
    /// <param name="prop">Property for which to define the new setter with
    /// value change notification.</param>
    /// <param name="valueProvider">
    /// Block to invoke to provide the value to the setter.
    /// </param>
    /// <param name="valueSetter">
    /// Block to execute to insert the value into the backing field.
    /// </param>
    /// <param name="propertyType">Type of the property.</param>
    public static void BuildNpcPropSetterSkeleton(this PropertyBuildInfo prop, IlBlockWithExitLabel valueProvider, IlBlockWithExitLabel valueSetter, Type propertyType)
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
        return type.GetMethod(nameof(Equals), BindingFlags.Public | BindingFlags.Instance, null, new[] { type }, null)
            ?? type.GetMethod(nameof(Equals), BindingFlags.Public | BindingFlags.Instance, null, new[] { typeof(object) }, null)!;
    }
}
