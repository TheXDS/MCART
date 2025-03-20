/*
TypeExtensions_Contracts.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Este archivo contiene numerosas extensiones para el tipo System.Type del CLR,
supliéndolo de nueva funcionalidad previamente no existente, o de invocación
compleja.

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

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.CompilerServices;
using TheXDS.MCART.Exceptions;
using TheXDS.MCART.Helpers;
using TheXDS.MCART.Misc;
using TheXDS.MCART.Resources;
using static TheXDS.MCART.Misc.Internals;

namespace TheXDS.MCART.Types.Extensions;

/// <summary>
/// Extensiones para todos los elementos de tipo <see cref="Type"/>.
/// </summary>
public static partial class TypeExtensions
{
    [Conditional("EnforceContracts")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [DebuggerNonUserCode]
    private static void Derivates_Contract(AppDomain domain)
    {
        ArgumentNullException.ThrowIfNull(domain, nameof(domain));
    }

    [Conditional("EnforceContracts")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [DebuggerNonUserCode]
    private static void Derivates_Contract(IEnumerable<Assembly> assemblies)
    {
        ArgumentNullException.ThrowIfNull(assemblies, nameof(assemblies));
    }

    [Conditional("EnforceContracts")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [DebuggerNonUserCode]
    private static void Derivates_Contract(Type type, IEnumerable<Type> types)
    {
        ArgumentNullException.ThrowIfNull(type, nameof(type));
        ArgumentNullException.ThrowIfNull(types, nameof(types));
    }

    [Conditional("EnforceContracts")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [DebuggerNonUserCode]
    [RequiresDynamicCode(AttributeErrorMessages.MethodCallsDynamicCode)]
    private static void GetCollectionType_Contract([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)]Type collectionType)
    {
        ArgumentNullException.ThrowIfNull(collectionType, nameof(collectionType));
        if (!collectionType.IsCollectionType()) throw Errors.EnumerableTypeExpected(collectionType);
    }

    [Conditional("EnforceContracts")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [DebuggerNonUserCode]
    private static void GetDefinedMethods_Contract(Type type)
    {
        ArgumentNullException.ThrowIfNull(type, nameof(type));
    }

    [Conditional("EnforceContracts")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [DebuggerNonUserCode]
    private static void GetPublicProperties_Contract(Type type)
    {
        ArgumentNullException.ThrowIfNull(type, nameof(type));
    }

    [Conditional("EnforceContracts")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [DebuggerNonUserCode]
    private static void New_Contract([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)]Type type, bool throwOnFail, IEnumerable<object?>? parameters)
    {
        ArgumentNullException.ThrowIfNull(type, nameof(type));
        if (throwOnFail && !type.IsInstantiable(parameters?.ToTypes()))
        {
            throw Errors.ClassNotInstantiable(type);
        }
    }

    [Conditional("EnforceContracts")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [DebuggerNonUserCode]
    private static void NotNullable_Contract(Type t)
    {
        ArgumentNullException.ThrowIfNull(t, nameof(t));
    }

    [Conditional("EnforceContracts")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [DebuggerNonUserCode]
    private static void ToNamedEnum_Contract(Type type)
    {
        ArgumentNullException.ThrowIfNull(type, nameof(type));
        if (!type.IsEnum) throw Errors.EnumExpected(nameof(type), type);
    }

    [Conditional("EnforceContracts")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [DebuggerNonUserCode]
    private static void TryInstance_Contract(Type t, object[]? args)
    {
        ArgumentNullException.ThrowIfNull(t, nameof(t));
        if (args?.IsAnyNull() ?? false) throw new NullItemException(args);
    }
}
