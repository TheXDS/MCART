﻿/*
PropertyInfoExtensions.cs

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

using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Misc;
using TheXDS.MCART.Resources;

namespace TheXDS.MCART.Types.Extensions;

/// <summary>
/// Various extensions for objects of type <see cref="PropertyInfo"/>.
/// </summary>
public static class PropertyInfoExtensions
{
    /// <summary>
    /// Sets the value of a property on an object to its default value.
    /// </summary>
    /// <param name="property">The property to reset.</param>
    /// <param name="instance">
    /// The instance of the object containing the property.
    /// </param>
    [RequiresUnreferencedCode(AttributeErrorMessages.MethodScansForTypes)]
    public static void SetDefault(this PropertyInfo property, object? instance)
    {
        if (instance is null || instance.GetType().GetProperties().Any(p => p.Is(property)))
        {
            SetDefaultValueInternal(instance, property);
        }
        else
        {
            throw Errors.MissingMember(instance.GetType(), property);
        }
    }

    /// <summary>
    /// Sets the value of a static property to its default value.
    /// </summary>
    /// <param name="property">The property to reset.</param>
    [RequiresUnreferencedCode(AttributeErrorMessages.MethodScansForTypes)]
    public static void SetDefault(this PropertyInfo property)
    {
        SetDefault(property, null);
    }

    /// <summary>
    /// Gets a value indicating whether the property supports read and write.
    /// </summary>
    /// <param name="property">
    /// The property to check.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the property supports read and write;
    /// otherwise, <see langword="false"/>.
    /// </returns>
    [Sugar]
    public static bool IsReadWrite(this PropertyInfo property)
    {
        return property.CanRead && property.CanWrite;
    }

    [RequiresUnreferencedCode(AttributeErrorMessages.MethodScansForTypes)]
    private static object? GetDefaultFromInstance(object? instance, PropertyInfo property)
    {
        object? d = null;
        try
        {
            return (instance?.GetType().TryInstance(out d) ?? false) ? property.GetValue(d) : null;
        }
        finally
        {
            if (d is IDisposable i) i.Dispose();
        }
    }

    [RequiresUnreferencedCode(AttributeErrorMessages.MethodScansForTypes)]
    private static void SetDefaultValueInternal(object? instance, PropertyInfo property)
    {
        if (property.SetMethod is null)
        {
            throw Errors.PropIsReadOnly(property);
        }
        property.SetMethod.Invoke(instance,
        [
            property.GetAttribute<DefaultValueAttribute>()?.Value ??
            GetDefaultFromInstance(instance, property) ??
            property.PropertyType.Default()
        ]);
    }
}
